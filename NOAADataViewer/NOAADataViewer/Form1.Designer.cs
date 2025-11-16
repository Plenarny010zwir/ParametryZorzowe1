namespace NOAADataViewer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelKP;
        private System.Windows.Forms.FlowLayoutPanel headerKP;
        private System.Windows.Forms.Label labelKPTitle;
        private System.Windows.Forms.Label labelKPValue;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartKP;

        private System.Windows.Forms.Panel panelWindSpeed;
        private System.Windows.Forms.FlowLayoutPanel headerWindSpeed;
        private System.Windows.Forms.Label labelWindSpeedTitle;
        private System.Windows.Forms.Label labelWindSpeedValue;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartWindSpeed;

        private System.Windows.Forms.Panel panelDensity;
        private System.Windows.Forms.FlowLayoutPanel headerDensity;
        private System.Windows.Forms.Label labelDensityTitle;
        private System.Windows.Forms.Label labelDensityValue;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDensity;

        private System.Windows.Forms.Panel panelBT;
        private System.Windows.Forms.FlowLayoutPanel headerBT;
        private System.Windows.Forms.Label labelBTTitle;
        private System.Windows.Forms.Label labelBTValue;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartBT;

        private System.Windows.Forms.Panel panelBZ;
        private System.Windows.Forms.FlowLayoutPanel headerBZ;
        private System.Windows.Forms.Label labelBZTitle;
        private System.Windows.Forms.Label labelBZValue;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartBZ;

        private System.Windows.Forms.Timer updateTimer;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();

            this.panelKP = new System.Windows.Forms.Panel();
            this.headerKP = new System.Windows.Forms.FlowLayoutPanel();
            this.labelKPTitle = new System.Windows.Forms.Label();
            this.labelKPValue = new System.Windows.Forms.Label();
            this.chartKP = new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.panelWindSpeed = new System.Windows.Forms.Panel();
            this.headerWindSpeed = new System.Windows.Forms.FlowLayoutPanel();
            this.labelWindSpeedTitle = new System.Windows.Forms.Label();
            this.labelWindSpeedValue = new System.Windows.Forms.Label();
            this.chartWindSpeed = new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.panelDensity = new System.Windows.Forms.Panel();
            this.headerDensity = new System.Windows.Forms.FlowLayoutPanel();
            this.labelDensityTitle = new System.Windows.Forms.Label();
            this.labelDensityValue = new System.Windows.Forms.Label();
            this.chartDensity = new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.panelBT = new System.Windows.Forms.Panel();
            this.headerBT = new System.Windows.Forms.FlowLayoutPanel();
            this.labelBTTitle = new System.Windows.Forms.Label();
            this.labelBTValue = new System.Windows.Forms.Label();
            this.chartBT = new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.panelBZ = new System.Windows.Forms.Panel();
            this.headerBZ = new System.Windows.Forms.FlowLayoutPanel();
            this.labelBZTitle = new System.Windows.Forms.Label();
            this.labelBZValue = new System.Windows.Forms.Label();
            this.chartBZ = new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.updateTimer = new System.Windows.Forms.Timer(this.components);

            this.tableLayoutPanel1.SuspendLayout();

            this.panelKP.SuspendLayout();
            this.headerKP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartKP)).BeginInit();

            this.panelWindSpeed.SuspendLayout();
            this.headerWindSpeed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartWindSpeed)).BeginInit();

            this.panelDensity.SuspendLayout();
            this.headerDensity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDensity)).BeginInit();

            this.panelBT.SuspendLayout();
            this.headerBT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartBT)).BeginInit();

            this.panelBZ.SuspendLayout();
            this.headerBZ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartBZ)).BeginInit();

            this.SuspendLayout();
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                | System.Windows.Forms.AnchorStyles.Left)
                                                                                | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelKP, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelWindSpeed, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelDensity, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panelBT, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panelBZ, 0, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 8);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 434);
            this.tableLayoutPanel1.TabIndex = 0;
            //
            // panelKP (header + chart)
            //
            this.panelKP.Controls.Add(this.chartKP);
            this.panelKP.Controls.Add(this.headerKP);
            this.panelKP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelKP.Location = new System.Drawing.Point(4, 4);
            this.panelKP.Margin = new System.Windows.Forms.Padding(4);
            this.panelKP.Name = "panelKP";
            this.panelKP.Padding = new System.Windows.Forms.Padding(0);
            this.panelKP.Size = new System.Drawing.Size(776, 80);
            this.panelKP.TabIndex = 0;

            // headerKP
            this.headerKP.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerKP.AutoSize = true;
            this.headerKP.Controls.Add(this.labelKPTitle);
            this.headerKP.Controls.Add(this.labelKPValue);
            this.headerKP.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.headerKP.Margin = new System.Windows.Forms.Padding(0);
            this.headerKP.Padding = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.headerKP.Name = "headerKP";
            this.headerKP.TabIndex = 0;
            // labelKPTitle
            this.labelKPTitle.AutoSize = true;
            this.labelKPTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelKPTitle.Text = "KP";
            this.labelKPTitle.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            // labelKPValue
            this.labelKPValue.AutoSize = true;
            this.labelKPValue.Text = "—";
            this.labelKPValue.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);

            // chartKP
            this.chartKP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartKP.Location = new System.Drawing.Point(0, 24);
            this.chartKP.Margin = new System.Windows.Forms.Padding(4);
            this.chartKP.Name = "chartKP";
            this.chartKP.Size = new System.Drawing.Size(776, 56);
            this.chartKP.TabIndex = 1;
            this.chartKP.Text = "chartKP";

            //
            // panelWindSpeed
            //
            this.panelWindSpeed.Controls.Add(this.chartWindSpeed);
            this.panelWindSpeed.Controls.Add(this.headerWindSpeed);
            this.panelWindSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWindSpeed.Location = new System.Drawing.Point(4, 92);
            this.panelWindSpeed.Margin = new System.Windows.Forms.Padding(4);
            this.panelWindSpeed.Name = "panelWindSpeed";
            this.panelWindSpeed.Size = new System.Drawing.Size(776, 80);
            this.panelWindSpeed.TabIndex = 1;

            // headerWindSpeed
            this.headerWindSpeed.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerWindSpeed.AutoSize = true;
            this.headerWindSpeed.Controls.Add(this.labelWindSpeedTitle);
            this.headerWindSpeed.Controls.Add(this.labelWindSpeedValue);
            this.headerWindSpeed.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.headerWindSpeed.Margin = new System.Windows.Forms.Padding(0);
            this.headerWindSpeed.Padding = new System.Windows.Forms.Padding(4, 4, 4, 0);
            // labelWindSpeedTitle
            this.labelWindSpeedTitle.AutoSize = true;
            this.labelWindSpeedTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelWindSpeedTitle.Text = "WINDSPEED";
            this.labelWindSpeedTitle.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            // labelWindSpeedValue
            this.labelWindSpeedValue.AutoSize = true;
            this.labelWindSpeedValue.Text = "—";

            // chartWindSpeed
            this.chartWindSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartWindSpeed.Location = new System.Drawing.Point(0, 24);
            this.chartWindSpeed.Margin = new System.Windows.Forms.Padding(4);
            this.chartWindSpeed.Name = "chartWindSpeed";
            this.chartWindSpeed.Size = new System.Drawing.Size(776, 56);
            this.chartWindSpeed.TabIndex = 1;
            this.chartWindSpeed.Text = "chartWindSpeed";

            //
            // panelDensity
            //
            this.panelDensity.Controls.Add(this.chartDensity);
            this.panelDensity.Controls.Add(this.headerDensity);
            this.panelDensity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDensity.Location = new System.Drawing.Point(4, 180);
            this.panelDensity.Margin = new System.Windows.Forms.Padding(4);
            this.panelDensity.Name = "panelDensity";
            this.panelDensity.Size = new System.Drawing.Size(776, 80);
            this.panelDensity.TabIndex = 2;

            // headerDensity
            this.headerDensity.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerDensity.AutoSize = true;
            this.headerDensity.Controls.Add(this.labelDensityTitle);
            this.headerDensity.Controls.Add(this.labelDensityValue);
            this.headerDensity.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.headerDensity.Margin = new System.Windows.Forms.Padding(0);
            this.headerDensity.Padding = new System.Windows.Forms.Padding(4, 4, 4, 0);
            // labelDensityTitle
            this.labelDensityTitle.AutoSize = true;
            this.labelDensityTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelDensityTitle.Text = "DENSITY";
            this.labelDensityTitle.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            // labelDensityValue
            this.labelDensityValue.AutoSize = true;
            this.labelDensityValue.Text = "—";

            // chartDensity
            this.chartDensity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartDensity.Location = new System.Drawing.Point(0, 24);
            this.chartDensity.Margin = new System.Windows.Forms.Padding(4);
            this.chartDensity.Name = "chartDensity";
            this.chartDensity.Size = new System.Drawing.Size(776, 56);
            this.chartDensity.TabIndex = 1;
            this.chartDensity.Text = "chartDensity";

            //
            // panelBT
            //
            this.panelBT.Controls.Add(this.chartBT);
            this.panelBT.Controls.Add(this.headerBT);
            this.panelBT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBT.Location = new System.Drawing.Point(4, 268);
            this.panelBT.Margin = new System.Windows.Forms.Padding(4);
            this.panelBT.Name = "panelBT";
            this.panelBT.Size = new System.Drawing.Size(776, 80);
            this.panelBT.TabIndex = 3;

            // headerBT
            this.headerBT.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerBT.AutoSize = true;
            this.headerBT.Controls.Add(this.labelBTTitle);
            this.headerBT.Controls.Add(this.labelBTValue);
            this.headerBT.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.headerBT.Margin = new System.Windows.Forms.Padding(0);
            this.headerBT.Padding = new System.Windows.Forms.Padding(4, 4, 4, 0);
            // labelBTTitle
            this.labelBTTitle.AutoSize = true;
            this.labelBTTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelBTTitle.Text = "BT";
            this.labelBTTitle.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            // labelBTValue
            this.labelBTValue.AutoSize = true;
            this.labelBTValue.Text = "—";

            // chartBT
            this.chartBT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartBT.Location = new System.Drawing.Point(0, 24);
            this.chartBT.Margin = new System.Windows.Forms.Padding(4);
            this.chartBT.Name = "chartBT";
            this.chartBT.Size = new System.Drawing.Size(776, 56);
            this.chartBT.TabIndex = 1;
            this.chartBT.Text = "chartBT";

            //
            // panelBZ
            //
            this.panelBZ.Controls.Add(this.chartBZ);
            this.panelBZ.Controls.Add(this.headerBZ);
            this.panelBZ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBZ.Location = new System.Drawing.Point(4, 356);
            this.panelBZ.Margin = new System.Windows.Forms.Padding(4);
            this.panelBZ.Name = "panelBZ";
            this.panelBZ.Size = new System.Drawing.Size(776, 74);
            this.panelBZ.TabIndex = 4;

            // headerBZ
            this.headerBZ.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerBZ.AutoSize = true;
            this.headerBZ.Controls.Add(this.labelBZTitle);
            this.headerBZ.Controls.Add(this.labelBZValue);
            this.headerBZ.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.headerBZ.Margin = new System.Windows.Forms.Padding(0);
            this.headerBZ.Padding = new System.Windows.Forms.Padding(4, 4, 4, 0);
            // labelBZTitle
            this.labelBZTitle.AutoSize = true;
            this.labelBZTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelBZTitle.Text = "BZ";
            this.labelBZTitle.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            // labelBZValue
            this.labelBZValue.AutoSize = true;
            this.labelBZValue.Text = "—";

            // chartBZ
            this.chartBZ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartBZ.Location = new System.Drawing.Point(0, 24);
            this.chartBZ.Margin = new System.Windows.Forms.Padding(4);
            this.chartBZ.Name = "chartBZ";
            this.chartBZ.Size = new System.Drawing.Size(776, 50);
            this.chartBZ.TabIndex = 1;
            this.chartBZ.Text = "chartBZ";

            //
            // updateTimer
            //
            this.updateTimer.Interval = 60000;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);

            //
            // Form1
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "NOAA Data Viewer";
            this.Load += new System.EventHandler(this.Form1_Load);

            this.tableLayoutPanel1.ResumeLayout(false);

            this.panelKP.ResumeLayout(false);
            this.panelKP.PerformLayout();
            this.headerKP.ResumeLayout(false);
            this.headerKP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartKP)).EndInit();

            this.panelWindSpeed.ResumeLayout(false);
            this.panelWindSpeed.PerformLayout();
            this.headerWindSpeed.ResumeLayout(false);
            this.headerWindSpeed.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartWindSpeed)).EndInit();

            this.panelDensity.ResumeLayout(false);
            this.panelDensity.PerformLayout();
            this.headerDensity.ResumeLayout(false);
            this.headerDensity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDensity)).EndInit();

            this.panelBT.ResumeLayout(false);
            this.panelBT.PerformLayout();
            this.headerBT.ResumeLayout(false);
            this.headerBT.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartBT)).EndInit();

            this.panelBZ.ResumeLayout(false);
            this.panelBZ.PerformLayout();
            this.headerBZ.ResumeLayout(false);
            this.headerBZ.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartBZ)).EndInit();

            this.ResumeLayout(false);
        }

        #endregion
    }
}
