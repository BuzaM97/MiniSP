using System.Diagnostics;

namespace MiniPhotoshop
{
    public partial class MainForm : Form
    {
        private Bitmap? originalBitmap;
        private Bitmap? resultBitmap;
        private Bitmap? histogramBitmap;

        private ImageData? originalImage;
        private ImageData? currentImage;   // a következő művelet ebből dolgozik

        public MainForm()
        {
            InitializeComponent();
            chkParallel.Checked = ImageOps.UseParallel;
        }

        #region Betöltés, visszaállítás, beállítások

        private void btnBrowseImg_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Válasszon egy képet";
                dialog.Filter = "Képfájlok|*.jpg;*.jpeg;*.png;*.bmp|Minden fájl|*.*";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    // másolat, különben a fájl zárolva marad
                    Bitmap loaded;
                    using (Bitmap fromFile = new Bitmap(dialog.FileName))
                        loaded = new Bitmap(fromFile);

                    ClearImages();

                    originalBitmap = loaded;
                    pbOrig.Image = originalBitmap;

                    originalImage = ImageOps.FromBitmap(originalBitmap);
                    currentImage = originalImage;
                    ShowResult(new Bitmap(originalBitmap));

                    lblSelectImg.Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("A kép nem tölthető be: " + ex.Message);
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (originalBitmap == null) return;

            currentImage = originalImage;
            ShowResult(new Bitmap(originalBitmap));
            lstHistory.Items.Clear();
        }

        private void chkParallel_CheckedChanged(object sender, EventArgs e)
        {
            ImageOps.UseParallel = chkParallel.Checked;
        }

        #endregion

        #region Funkciók

        private async void btnNegate_Click(object sender, EventArgs e)
        {
            await RunOperation("Negálás", ImageOps.Negate);
        }

        private async void btnGamma_Click(object sender, EventArgs e)
        {
            double gamma = (double)numGamma.Value;
            await RunOperation($"Gamma ({gamma})", img => ImageOps.Gamma(img, gamma));
        }

        private async void btnLog_Click(object sender, EventArgs e)
        {
            await RunOperation("Logaritmikus", ImageOps.Logarithmic);
        }

        private async void btnGrayscale_Click(object sender, EventArgs e)
        {
            await RunOperation("Szürkítés", ImageOps.Grayscale);
        }

        // a képet nem változtatja, csak kirajzolja a diagramot
        private async void btnHistogram_Click(object sender, EventArgs e)
        {
            if (currentImage == null)
            {
                MessageBox.Show("Előbb tölts be egy képet.");
                return;
            }

            ImageData src = currentImage;
            SetButtonsEnabled(false);

            try
            {
                var (hist, ms) = await Task.Run(() =>
                {
                    var sw = Stopwatch.StartNew();
                    int[] h = ImageOps.ComputeHistogram(src);
                    sw.Stop();
                    return (h, sw.Elapsed.TotalMilliseconds);
                });

                pbHistogram.Image = null;
                histogramBitmap?.Dispose();
                histogramBitmap = ImageOps.DrawHistogram(hist, pbHistogram.Width, pbHistogram.Height);
                pbHistogram.Image = histogramBitmap;

                AddToHistory("Hisztogram", ms);
            }
            finally
            {
                SetButtonsEnabled(true);
            }
        }

        private async void btnEqualize_Click(object sender, EventArgs e)
        {
            await RunOperation("Hisztogram kiegyenlítés", ImageOps.EqualizeHistogram);
        }

        private async void btnBox_Click(object sender, EventArgs e)
        {
            await RunOperation("Box szűrő", ImageOps.BoxFilter);
        }

        private async void btnGauss_Click(object sender, EventArgs e)
        {
            await RunOperation("Gauss szűrő", ImageOps.GaussFilter);
        }

        private async void btnSobel_Click(object sender, EventArgs e)
        {
            await RunOperation("Sobel", ImageOps.Sobel);
        }

        private async void btnLaplace_Click(object sender, EventArgs e)
        {
            await RunOperation("Laplace", ImageOps.Laplace);
        }

        // csak jelöli a pontokat, különben a piros négyzetek is a kép részévé válnának
        private async void btnHarris_Click(object sender, EventArgs e)
        {
            await RunOperation("Jellemzőpontok (Harris)", ImageOps.Harris, changesImage: false);
        }

        #endregion

        #region Segédfüggvények

        private async Task RunOperation(string name, Func<ImageData, ImageData> operation, bool changesImage = true)
        {
            if (currentImage == null)
            {
                MessageBox.Show("Előbb tölts be egy képet.");
                return;
            }

            ImageData src = currentImage;
            SetButtonsEnabled(false);

            try
            {
                // csak a műveletet mérjük, a megjelenítést nem
                var (result, ms) = await Task.Run(() =>
                {
                    var sw = Stopwatch.StartNew();
                    ImageData r = operation(src);
                    sw.Stop();
                    return (r, sw.Elapsed.TotalMilliseconds);
                });

                if (changesImage)
                    currentImage = result;

                ShowResult(ImageOps.ToBitmap(result));
                AddToHistory(name, ms);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba ({name}): {ex.Message}");
            }
            finally
            {
                SetButtonsEnabled(true);
            }
        }

        private void AddToHistory(string name, double ms)
        {
            string mode = ImageOps.UseParallel ? "párhuzamos" : "egyszálú";
            lstHistory.Items.Add($"{lstHistory.Items.Count + 1}. {name}: {ms:F2} ms ({mode})");
        }

        private void ShowResult(Bitmap newResult)
        {
            pbResult.Image = null;
            resultBitmap?.Dispose();
            resultBitmap = newResult;
            pbResult.Image = resultBitmap;
        }

        private void ClearImages()
        {
            pbOrig.Image = null;
            pbResult.Image = null;
            pbHistogram.Image = null;

            originalBitmap?.Dispose();
            resultBitmap?.Dispose();
            histogramBitmap?.Dispose();
            originalBitmap = null;
            resultBitmap = null;
            histogramBitmap = null;

            originalImage = null;
            currentImage = null;
            lstHistory.Items.Clear();
        }

        // futás közben semmit ne lehessen elindítani
        private void SetButtonsEnabled(bool enabled)
        {
            btnBrowseImg.Enabled = enabled;
            btnReset.Enabled = enabled;
            chkParallel.Enabled = enabled;

            btnNegate.Enabled = enabled;
            btnGamma.Enabled = enabled;
            numGamma.Enabled = enabled;
            btnLog.Enabled = enabled;
            btnGrayscale.Enabled = enabled;
            btnHistogram.Enabled = enabled;
            btnEqualize.Enabled = enabled;
            btnBox.Enabled = enabled;
            btnGauss.Enabled = enabled;
            btnSobel.Enabled = enabled;
            btnLaplace.Enabled = enabled;
            btnHarris.Enabled = enabled;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            ClearImages();
            base.OnFormClosed(e);
        }

        #endregion
    }
}
