public Form1()
{
    InitializeComponent();

    // ustaw szerokoœæ okna (rozmiar klienta)
    this.ClientSize = new System.Drawing.Size(300, 600); // szerokoœæ 1024 px, wysokoœæ 600 px

    // ograniczenia zmiany rozmiaru
    this.MinimumSize = new System.Drawing.Size(100, 400);
    this.MaximumSize = new System.Drawing.Size(1920, 1200); // opcjonalnie

    // opcjonalnie zablokuj skalowanie i maksymalizacjê
    // this.FormBorderStyle = FormBorderStyle.FixedSingle;
    // this.MaximizeBox = false;

    // wycentrowanie okna na ekranie
    this.StartPosition = FormStartPosition.CenterScreen;
}