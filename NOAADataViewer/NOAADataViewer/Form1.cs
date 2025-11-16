using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace NOAADataViewer
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient http = new HttpClient();
        private readonly string[] urls = new[]
        {
            "https://services.swpc.noaa.gov/json/planetary_k_index_1m.json",
            "https://services.swpc.noaa.gov/json/rtsw/rtsw_wind_1m.json",
            "https://services.swpc.noaa.gov/json/rtsw/rtsw_mag_1m.json"
        };

        // kolejnoœæ wykresów: KP, WINDSPEED, DENSITY, BT, BZ
        private readonly string[] chartTitles = new[] { "KP", "WINDSPEED", "DENSITY", "BT", "BZ" };
        private readonly Chart[] charts;
        private Label[] valueLabels;
        private Label[] titleLabels;
        private bool isFetching = false;
        private readonly TimeZoneInfo polandTimeZone;

        // przechowujemy ostatnie dane ¿eby móc rysowaæ gradient w PostPaint
        private List<List<(DateTime time, double value)>> lastDatasets = new();

        public Form1()
        {
            InitializeComponent();
            // ustaw szerokoœæ okna (rozmiar klienta)
            this.ClientSize = new System.Drawing.Size(500, 1440);
            // opcjonalnie zablokuj skalowanie i maksymalizacjê
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.Manual;
            var wa = Screen.PrimaryScreen.WorkingArea;
            // umieszczamy okno przy lewej krawêdzi i wyrównujemy do góry pulpitu
            this.Location = new Point(wa.Left, wa.Top);
            // wycentrowanie okna na ekranie
            // ustawienie strefy czasowej Polski (Windows) z fallbackem dla systemów UNIX
            TimeZoneInfo tz = null;
            try
            {
                tz = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            }
            catch
            {
                try
                {
                    tz = TimeZoneInfo.FindSystemTimeZoneById("Europe/Warsaw");
                }
                catch
                {
                    tz = null;
                }
            }
            polandTimeZone = tz;

            charts = new[] { chartKP, chartWindSpeed, chartDensity, chartBT, chartBZ };
            valueLabels = new[] { labelKPValue, labelWindSpeedValue, labelDensityValue, labelBTValue, labelBZValue };
            titleLabels = new[] { labelKPTitle, labelWindSpeedTitle, labelDensityTitle, labelBTTitle, labelBZTitle };

            ConfigureCharts();

            // pod³¹cz PostPaint do wykresów, które wymagaj¹ gradientów
            chartKP.PostPaint += Chart_PostPaint;
            chartWindSpeed.PostPaint += Chart_PostPaint;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _ = FetchAndPlotAsync();
            updateTimer.Start();
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            _ = FetchAndPlotAsync();
        }

        private void ConfigureCharts()
        {
            var baseColors = new[]
            {
                Color.OrangeRed,
                Color.DodgerBlue,
                Color.MediumPurple,
                Color.Green,
            };

            for (int i = 0; i < charts.Length; i++)
            {
                var chart = charts[i];
                chart.Series.Clear();
                chart.ChartAreas.Clear();

                // tytu³ ustawiany w labelu (nag³ówek nad wykresem)
                titleLabels[i].Text = chartTitles[i];
                valueLabels[i].Text = "—";

                var areaName = "Area";
                var area = new ChartArea(areaName);
                area.AxisX.LabelStyle.Format = "HH:mm";
                area.AxisX.Title = "Czas (Europe/Warsaw)";
                area.AxisX.MajorGrid.Enabled = false;
                area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

                chart.ChartAreas.Add(area);

                if (i == 4)
                {
                    var negFill = new Series("BZ_Neg")
                    {
                        ChartType = SeriesChartType.Area,
                        XValueType = ChartValueType.DateTime,
                        BorderWidth = 0,
                        ChartArea = areaName,
                        Color = Color.FromArgb(160, Color.Red),
                    };
                    negFill["AreaDrawingStyle"] = "Gradient";

                    var posFill = new Series("BZ_Pos")
                    {
                        ChartType = SeriesChartType.Area,
                        XValueType = ChartValueType.DateTime,
                        BorderWidth = 0,
                        ChartArea = areaName,
                        Color = Color.FromArgb(160, Color.Green),
                    };
                    posFill["AreaDrawingStyle"] = "Gradient";

                    var lineNeg = new Series("BZ_Line_Neg")
                    {
                        ChartType = SeriesChartType.Line,
                        XValueType = ChartValueType.DateTime,
                        BorderWidth = 2,
                        ChartArea = areaName,
                        Color = Color.DarkRed,
                    };
                    var linePos = new Series("BZ_Line_Pos")
                    {
                        ChartType = SeriesChartType.Line,
                        XValueType = ChartValueType.DateTime,
                        BorderWidth = 2,
                        ChartArea = areaName,
                        Color = Color.DarkGreen,
                    };

                    chart.Series.Add(negFill);
                    chart.Series.Add(posFill);
                    chart.Series.Add(lineNeg);
                    chart.Series.Add(linePos);

                    valueLabels[i].ForeColor = Color.Black;
                }
                else if (i == 0)
                {
                    var kpGreen = new Series("KP_Green")
                    {
                        ChartType = SeriesChartType.StackedArea,
                        XValueType = ChartValueType.DateTime,
                        BorderWidth = 0,
                        ChartArea = areaName,
                        Color = Color.FromArgb(180, Color.Green),
                    };
                    kpGreen["AreaDrawingStyle"] = "Gradient";

                    var kpYellow = new Series("KP_Yellow")
                    {
                        ChartType = SeriesChartType.StackedArea,
                        XValueType = ChartValueType.DateTime,
                        BorderWidth = 0,
                        ChartArea = areaName,
                        Color = Color.FromArgb(200, Color.Gold),
                    };
                    kpYellow["AreaDrawingStyle"] = "Gradient";

                    var kpRed = new Series("KP_Red")
                    {
                        ChartType = SeriesChartType.StackedArea,
                        XValueType = ChartValueType.DateTime,
                        BorderWidth = 0,
                        ChartArea = areaName,
                        Color = Color.FromArgb(200, Color.OrangeRed),
                    };
                    kpRed["AreaDrawingStyle"] = "Gradient";

                    var kpLine = new Series("KP_Line")
                    {
                        ChartType = SeriesChartType.Line,
                        XValueType = ChartValueType.DateTime,
                        BorderWidth = 2,
                        ChartArea = areaName,
                        Color = Color.Black
                    };

                    chart.Series.Add(kpGreen);
                    chart.Series.Add(kpYellow);
                    chart.Series.Add(kpRed);
                    chart.Series.Add(kpLine);

                    valueLabels[i].ForeColor = Color.Black;
                }
                else if (i == 1)
                {
                    var wGreen = new Series("Wind_Green")
                    {
                        ChartType = SeriesChartType.StackedArea,
                        XValueType = ChartValueType.DateTime,
                        BorderWidth = 0,
                        ChartArea = areaName,
                        Color = Color.FromArgb(160, Color.Green),
                    };
                    wGreen["AreaDrawingStyle"] = "Gradient";

                    var wYellow = new Series("Wind_Yellow")
                    {
                        ChartType = SeriesChartType.StackedArea,
                        XValueType = ChartValueType.DateTime,
                        BorderWidth = 0,
                        ChartArea = areaName,
                        Color = Color.FromArgb(200, Color.Gold),
                    };
                    wYellow["AreaDrawingStyle"] = "Gradient";

                    var wRed = new Series("Wind_Red")
                    {
                        ChartType = SeriesChartType.StackedArea,
                        XValueType = ChartValueType.DateTime,
                        BorderWidth = 0,
                        ChartArea = areaName,
                        Color = Color.FromArgb(200, Color.OrangeRed),
                    };
                    wRed["AreaDrawingStyle"] = "Gradient";

                    var wLine = new Series("Wind_Line")
                    {
                        ChartType = SeriesChartType.Line,
                        XValueType = ChartValueType.DateTime,
                        BorderWidth = 2,
                        ChartArea = areaName,
                        Color = Color.Black
                    };

                    chart.Series.Add(wGreen);
                    chart.Series.Add(wYellow);
                    chart.Series.Add(wRed);
                    chart.Series.Add(wLine);

                    valueLabels[i].ForeColor = Color.Black;
                }
                else
                {
                    var baseColor = baseColors[i % baseColors.Length];
                    var fillColor = Color.FromArgb(180, baseColor.R, baseColor.G, baseColor.B);
                    var borderColor = Color.FromArgb(255, baseColor.R, baseColor.G, baseColor.B);

                    var series = new Series("Value")
                    {
                        ChartType = SeriesChartType.Area,
                        XValueType = ChartValueType.DateTime,
                        BorderWidth = 2,
                        ChartArea = areaName,
                        Color = fillColor,
                        BorderColor = borderColor
                    };

                    series["AreaDrawingStyle"] = "Gradient";
                    series["PixelPointWidth"] = "2";

                    chart.Series.Add(series);
                    valueLabels[i].ForeColor = borderColor;
                }
            }
        }

        private async Task FetchAndPlotAsync()
        {
            if (isFetching) return;
            isFetching = true;

            try
            {
                var tasks = urls.Select(u => http.GetStringAsync(u)).ToArray();
                await Task.WhenAll(tasks);

                var kpJson = tasks[0].Result;
                var windJson = tasks[1].Result;
                var magJson = tasks[2].Result;

                var kp = ExtractFieldSeries(kpJson, new[] { "kp_index", "kp", "k_index", "kp_value" });
                var windSpeed = ExtractFieldSeries(windJson, new[] { "proton_speed" });
                var density = ExtractFieldSeries(windJson, new[] { "density", "proton_density", "n", "number_density" });
                var bt = ExtractFieldSeries(magJson, new[] { "bt", "bt_gsm", "magnetic_field_total", "bt_gse" });
                var bz = ExtractFieldSeries(magJson, new[] { "bz_gsm", "bz", "bz_gse", "bz_gsm" });

                var results = new List<List<(DateTime time, double value)>> { kp, windSpeed, density, bt, bz };

                // zapamiêtaj do rysowania gradientów w PostPaint
                lastDatasets = results.Select(d => d.ToList()).ToList();

                if (InvokeRequired)
                {
                    Invoke((Action)(() => UpdateCharts(results)));
                }
                else
                {
                    UpdateCharts(results);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fetch error: " + ex);
            }
            finally
            {
                isFetching = false;
            }
        }

        private List<(DateTime time, double value)> ExtractFieldSeries(string json, string[] fieldCandidates)
        {
            var list = new List<(DateTime time, double value)>();
            try
            {
                using var doc = JsonDocument.Parse(json);
                if (doc.RootElement.ValueKind != JsonValueKind.Array) return list;

                foreach (var el in doc.RootElement.EnumerateArray())
                {
                    if (el.ValueKind != JsonValueKind.Object) continue;

                    if (el.TryGetProperty("source", out var srcProp))
                    {
                        if (srcProp.ValueKind != JsonValueKind.String ||
                            !string.Equals(srcProp.GetString(), "ACE", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                    }

                    if (!TryGetDate(el, out DateTime time)) continue;

                    double? val = null;
                    foreach (var candidate in fieldCandidates)
                    {
                        if (el.TryGetProperty(candidate, out var prop))
                        {
                            if (prop.ValueKind == JsonValueKind.Number && prop.TryGetDouble(out double d))
                            {
                                val = d;
                                break;
                            }
                            else if (prop.ValueKind == JsonValueKind.String)
                            {
                                var s = prop.GetString();
                                if (double.TryParse(s, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out d))
                                {
                                    val = d;
                                    break;
                                }
                            }
                        }
                    }

                    if (val.HasValue)
                    {
                        DateTime polishTime = ConvertToPolandTime(time);
                        list.Add((polishTime, val.Value));
                    }
                }

                list.Sort((a, b) => a.time.CompareTo(b.time));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ExtractFieldSeries parse error: " + ex);
            }

            return list;
        }

        private DateTime ConvertToPolandTime(DateTime dt)
        {
            try
            {
                if (polandTimeZone != null)
                {
                    DateTime utc = dt.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(dt, DateTimeKind.Utc) : dt.ToUniversalTime();
                    return TimeZoneInfo.ConvertTimeFromUtc(utc, polandTimeZone);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Time conversion error: " + ex);
            }

            return dt.ToLocalTime();
        }

        private bool TryGetDate(JsonElement el, out DateTime dt)
        {
            dt = default;
            string[] timeFields = new[] { "time_tag", "time", "timestamp", "iso_timestamp" };
            foreach (var f in timeFields)
            {
                if (el.TryGetProperty(f, out var prop))
                {
                    if (prop.ValueKind == JsonValueKind.String)
                    {
                        var s = prop.GetString();
                        if (DateTime.TryParse(s, null, System.Globalization.DateTimeStyles.AssumeUniversal | System.Globalization.DateTimeStyles.AdjustToUniversal, out dt))
                        {
                            return true;
                        }
                    }
                    else if (prop.ValueKind == JsonValueKind.Number && prop.TryGetDouble(out double unix))
                    {
                        try
                        {
                            dt = DateTimeOffset.FromUnixTimeSeconds((long)unix).UtcDateTime;
                            return true;
                        }
                        catch { }
                    }
                }
            }

            foreach (var prop in el.EnumerateObject())
            {
                if (prop.Name.IndexOf("time", StringComparison.OrdinalIgnoreCase) >= 0 && prop.Value.ValueKind == JsonValueKind.String)
                {
                    var s = prop.Value.GetString();
                    if (DateTime.TryParse(s, null, System.Globalization.DateTimeStyles.AssumeUniversal | System.Globalization.DateTimeStyles.AdjustToUniversal, out dt))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private double? GetCurrentValueForDataset(List<(DateTime time, double value)> pts, DateTime target)
        {
            if (pts == null || pts.Count == 0) return null;
            var seq = pts.Where(p => p.time <= target).ToList();
            if (seq.Count == 0) return null;
            return seq.OrderByDescending(p => p.time).First().value;
        }

        // now using e.ChartGraphics.Graphics and ChartArea from e.ChartElement
        private void Chart_PostPaint(object sender, ChartPaintEventArgs e)
        {
            if (!(sender is Chart chart)) return;
            if (!(e.ChartElement is ChartArea area)) return;

            int idx = Array.IndexOf(charts, chart);
            if (idx < 0) return;
            if (lastDatasets == null || lastDatasets.Count <= idx) return;
            var pts = lastDatasets.ElementAtOrDefault(idx);
            if (pts == null || pts.Count == 0) return;

            // use e.ChartGraphics for drawing (it is in device pixels)
            if (idx == 0)
            {
                // KP: thresholds 3 and 6, szerszy fade = 1.2 jednostki
                DrawGradientUnderCurve(chart, area, e.ChartGraphics, pts, 0.0, 9.0, new[] { Color.Green, Color.Gold, Color.OrangeRed }, new[] { 3.0, 6.0 }, new[] { 1.2, 1.2 });
            }
            else if (idx == 1)
            {
                double maxY = Math.Max(area.AxisY.Maximum, 700.0);
                DrawGradientUnderCurve(chart, area, e.ChartGraphics, pts, 0.0, maxY, new[] { Color.Green, Color.Gold, Color.OrangeRed }, new[] { 400.0, 700.0 }, new[] { 200.0, 200.0 });
            }
        }

        // Draws gradient using ChartGraphics (device pixels) and correct inner-plot rect
        private void DrawGradientUnderCurve(Chart chart, ChartArea area, ChartGraphics cg, List<(DateTime time, double value)> pts, double baselineValue, double topValue, Color[] keyColors, double[] thresholds, double[] fadeWidths)
        {
            if (pts == null || pts.Count == 0) return;
            if (keyColors == null || thresholds == null || fadeWidths == null) return;
            if (keyColors.Length != thresholds.Length + 1) return;

            var g = cg.Graphics;

            // przygotuj wspó³rzêdne punktów w pikselach (urz¹dzenie)
            var ptsPixels = new List<PointF>();
            foreach (var p in pts)
            {
                double xVal = p.time.ToOADate();
                double xPx = area.AxisX.ValueToPixelPosition(xVal);
                double yPx = area.AxisY.ValueToPixelPosition(p.value);
                ptsPixels.Add(new PointF((float)xPx, (float)yPx));
            }

            // compute inner plot pixel rect via ChartGraphics
            var inner = cg.GetAbsoluteRectangle(new RectangleF(
                area.InnerPlotPosition.X,
                area.InnerPlotPosition.Y,
                area.InnerPlotPosition.Width,
                area.InnerPlotPosition.Height
            ));

            if (inner.Height <= 0 || ptsPixels.Count == 0) return;

            // baseline pixel
            float baselinePx = (float)area.AxisY.ValueToPixelPosition(baselineValue);

            using (var path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                path.StartFigure();
                path.AddLine(ptsPixels.First().X, baselinePx, ptsPixels.First().X, ptsPixels.First().Y);
                for (int i = 0; i < ptsPixels.Count; i++)
                {
                    var pt = ptsPixels[i];
                    path.AddLine(pt.X, pt.Y, pt.X, pt.Y);
                }
                path.AddLine(ptsPixels.Last().X, ptsPixels.Last().Y, ptsPixels.Last().X, baselinePx);
                path.CloseFigure();

                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(inner, Color.Transparent, Color.Transparent, 90f))
                {
                    var posList = new List<float>();
                    var colList = new List<Color>();

                    posList.Add(0f);
                    colList.Add(keyColors[0]);

                    for (int tIdx = 0; tIdx < thresholds.Length; tIdx++)
                    {
                        double t = thresholds[tIdx];
                        double f = fadeWidths[tIdx];

                        float startPos = (float)((t - f - baselineValue) / (topValue - baselineValue));
                        float endPos = (float)((t + f - baselineValue) / (topValue - baselineValue));
                        startPos = Math.Max(0f, Math.Min(1f, startPos));
                        endPos = Math.Max(0f, Math.Min(1f, endPos));

                        if (startPos <= posList.Last()) startPos = Math.Min(1f, posList.Last() + 0.0001f);
                        if (endPos <= startPos) endPos = Math.Min(1f, startPos + 0.0001f);

                        posList.Add(startPos);
                        colList.Add(keyColors[tIdx]);

                        posList.Add(endPos);
                        colList.Add(keyColors[tIdx + 1]);
                    }

                    if (posList.Last() < 1f)
                    {
                        posList.Add(1f);
                        colList.Add(keyColors.Last());
                    }

                    // oryginalne odwrócenie pozycji (przyczyna b³êdu pozostawiona w stanie sprzed zmian)
                    var invertedPositions = posList.Select(p => 1f - p).ToArray();

                    var cb = new System.Drawing.Drawing2D.ColorBlend
                    {
                        Positions = invertedPositions,
                        Colors = colList.ToArray()
                    };

                    var oldClip = g.Clip;
                    try
                    {
                        g.SetClip(path);
                        g.FillRectangle(brush, inner);
                    }
                    finally
                    {
                        g.Clip = oldClip;
                    }
                }
            }
        }

        private void UpdateCharts(List<List<(DateTime time, double value)>> datasets)
        {
            var allTimes = datasets.SelectMany(d => d.Select(x => x.time)).ToList();
            if (allTimes.Count == 0) return;

            var globalMax = allTimes.Max();
            var windowStart = globalMax.AddHours(-4);
            var windowEnd = globalMax.AddMinutes(1);

            for (int i = 0; i < charts.Length; i++)
            {
                var chart = charts[i];

                var pts = datasets.ElementAtOrDefault(i) ?? new List<(DateTime time, double value)>();

                if (i == 4)
                {
                    var sNeg = chart.Series["BZ_Neg"];
                    var sPos = chart.Series["BZ_Pos"];
                    var sLineNeg = chart.Series["BZ_Line_Neg"];
                    var sLinePos = chart.Series["BZ_Line_Pos"];
                    sNeg.Points.Clear();
                    sPos.Points.Clear();
                    sLineNeg.Points.Clear();
                    sLinePos.Points.Clear();

                    if (pts.Count > 0)
                    {
                        int start = Math.Max(0, pts.Count - 1000);
                        for (int j = start; j < pts.Count; j++)
                        {
                            var p = pts[j];
                            double posVal = Math.Max(0.0, p.value);
                            double negVal = Math.Min(0.0, p.value);

                            sPos.Points.AddXY(p.time.ToOADate(), posVal);
                            sNeg.Points.AddXY(p.time.ToOADate(), negVal);

                            if (p.value >= 0)
                            {
                                sLinePos.Points.AddXY(p.time.ToOADate(), p.value);
                                sLineNeg.Points.AddXY(p.time.ToOADate(), double.NaN);
                            }
                            else
                            {
                                sLineNeg.Points.AddXY(p.time.ToOADate(), p.value);
                                sLinePos.Points.AddXY(p.time.ToOADate(), double.NaN);
                            }
                        }

                        var last = pts.Last();
                        valueLabels[i].Text = $"{last.value:F2}  {last.time:HH:mm:ss}";

                        var currentVal = GetCurrentValueForDataset(pts, globalMax);
                        if (currentVal.HasValue)
                        {
                            if (currentVal.Value < 0) valueLabels[i].ForeColor = Color.DarkRed;
                            else if (currentVal.Value > 0) valueLabels[i].ForeColor = Color.DarkGreen;
                            else valueLabels[i].ForeColor = Color.DimGray;
                        }
                    }
                    else
                    {
                        valueLabels[i].Text = "—";
                    }
                }
                else if (i == 0)
                {
                    var sGreen = chart.Series["KP_Green"];
                    var sYellow = chart.Series["KP_Yellow"];
                    var sRed = chart.Series["KP_Red"];
                    var sLine = chart.Series["KP_Line"];
                    sGreen.Points.Clear();
                    sYellow.Points.Clear();
                    sRed.Points.Clear();
                    sLine.Points.Clear();

                    if (pts.Count > 0)
                    {
                        int start = Math.Max(0, pts.Count - 1000);
                        for (int j = start; j < pts.Count; j++)
                        {
                            var p = pts[j];
                            double g = Math.Min(p.value, 3.0);
                            double y = Math.Min(Math.Max(p.value - 3.0, 0.0), 3.0);
                            double r = Math.Min(Math.Max(p.value - 6.0, 0.0), 3.0);

                            sGreen.Points.AddXY(p.time.ToOADate(), g);
                            sYellow.Points.AddXY(p.time.ToOADate(), y);
                            sRed.Points.AddXY(p.time.ToOADate(), r);

                            sLine.Points.AddXY(p.time.ToOADate(), p.value);
                        }

                        var last = pts.Last();
                        valueLabels[i].Text = $"{last.value:F2}  {last.time:HH:mm:ss}";

                        var currentVal = GetCurrentValueForDataset(pts, globalMax);
                        if (currentVal.HasValue)
                        {
                            if (currentVal.Value <= 3) valueLabels[i].ForeColor = Color.DarkGreen;
                            else if (currentVal.Value <= 6) valueLabels[i].ForeColor = Color.Goldenrod;
                            else valueLabels[i].ForeColor = Color.DarkRed;
                        }
                    }
                    else
                    {
                        valueLabels[i].Text = "—";
                    }
                }
                else if (i == 1)
                {
                    var sG = chart.Series["Wind_Green"];
                    var sY = chart.Series["Wind_Yellow"];
                    var sR = chart.Series["Wind_Red"];
                    var sLine = chart.Series["Wind_Line"];
                    sG.Points.Clear();
                    sY.Points.Clear();
                    sR.Points.Clear();
                    sLine.Points.Clear();

                    if (pts.Count > 0)
                    {
                        int start = Math.Max(0, pts.Count - 1000);
                        double maxObserved = 0;
                        for (int j = start; j < pts.Count; j++)
                        {
                            var p = pts[j];
                            maxObserved = Math.Max(maxObserved, p.value);
                            double g = Math.Min(p.value, 400.0);
                            double y = Math.Min(Math.Max(p.value - 400.0, 0.0), 300.0);
                            double r = Math.Max(p.value - 700.0, 0.0);

                            sG.Points.AddXY(p.time.ToOADate(), g);
                            sY.Points.AddXY(p.time.ToOADate(), y);
                            sR.Points.AddXY(p.time.ToOADate(), r);
                            sLine.Points.AddXY(p.time.ToOADate(), p.value);
                        }

                        var last = pts.Last();
                        valueLabels[i].Text = $"{last.value:F2}  {last.time:HH:mm:ss}";

                        var currentVal = GetCurrentValueForDataset(pts, globalMax);
                        if (currentVal.HasValue)
                        {
                            if (currentVal.Value < 400) valueLabels[i].ForeColor = Color.DarkGreen;
                            else if (currentVal.Value < 700) valueLabels[i].ForeColor = Color.Goldenrod;
                            else valueLabels[i].ForeColor = Color.DarkRed;
                        }

                        var area = chart.ChartAreas[0];
                        double suggestedMax = Math.Max(700, Math.Ceiling((Math.Max(maxObserved, 700) + 50) / 50.0) * 50.0);
                        area.AxisY.Maximum = suggestedMax;
                    }
                    else
                    {
                        valueLabels[i].Text = "—";
                    }
                }
                else
                {
                    var series = chart.Series["Value"];
                    series.Points.Clear();

                    if (pts.Count > 0)
                    {
                        int start = Math.Max(0, pts.Count - 1000);
                        for (int j = start; j < pts.Count; j++)
                        {
                            var p = pts[j];
                            series.Points.AddXY(p.time.ToOADate(), p.value);
                        }

                        var last = pts.Last();
                        valueLabels[i].Text = $"{last.value:F2}  {last.time:HH:mm:ss}";
                    }
                    else
                    {
                        valueLabels[i].Text = "—";
                    }
                }

                var areaCommon = chart.ChartAreas[0];
                areaCommon.AxisX.Minimum = windowStart.ToOADate();
                areaCommon.AxisX.Maximum = windowEnd.ToOADate();
                areaCommon.AxisX.IntervalType = DateTimeIntervalType.Hours;
                areaCommon.AxisX.Interval = 1;
            }

            // trigger PostPaint so gradient is drawn (lastDatasets must be set prior)
            foreach (var c in charts) c.Invalidate();
        }
    }
}