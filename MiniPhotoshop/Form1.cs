using System.Diagnostics;

namespace MiniPhotoshop
{
    public partial class MainForm : Form
    {
        // A megjelenített képek
        private Bitmap? originalBitmap;
        private Bitmap? resultBitmap;

        // Az eredeti kép pixele
        private ImageData? originalImage;
        private ImageData? currentImage;
        public MainForm()
        {
            InitializeComponent();
            chkParallel.Checked = ImageOps.UseParallel;
        }

        private void lblSelectImg_Click(object sender, EventArgs e)
        {
        }

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
                    // Másolatot készítünk, hogy a fájl ne maradjon zárolva
                    Bitmap loaded;
                    using (Bitmap fromFile = new Bitmap(dialog.FileName))
                    {
                        loaded = new Bitmap(fromFile);
                    }

                    ClearImages();

                    originalBitmap = loaded;
                    pbOrig.Image = originalBitmap;

                    // Pixelek kiolvasása egyszer, betöltéskor
                    originalImage = ImageOps.FromBitmap(originalBitmap);

                    //Betöltéskor a két kép megegyezik
                    currentImage = originalImage;

                    // Jobbra egy független másolat kerül
                    ShowResult(new Bitmap(originalBitmap));

                    lblSelectImg.Visible = false;
                    lblTime.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("A kép nem tölthető be: " + ex.Message);
                }
            }
        }

        private async void btnNegate_Click(object sender, EventArgs e)
        {
            await RunOperation("Negálás", ImageOps.Negate);
        }

        // Közös futtató minden funkcióhoz:
        // háttérszálon lefuttatja a műveletet, méri az időt, majd megjeleníti az eredményt.
        private async Task RunOperation(string name, Func<ImageData, ImageData> operation)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Előbb tölts be egy képet.");
                return;
            }

            ImageData src = currentImage;
            SetButtonsEnabled(false);

            try
            {
                // Csak magát a műveletet mérjük, a megjelenítést nem
                var (result, ms) = await Task.Run(() =>
                {
                    var sw = Stopwatch.StartNew();
                    ImageData r = operation(src);
                    sw.Stop();
                    return (r, sw.Elapsed.TotalMilliseconds);
                });
                currentImage = result;
                ShowResult(ImageOps.ToBitmap(result));
                string mode = ImageOps.UseParallel ? "párhuzamos" : "egyszálú";
                string text = $"{name}: {ms:F2} ms ({mode})";

                lblTime.Text = text;
                lstHistory.Items.Add($"{lstHistory.Items.Count + 1}. {text}");
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

        // Futás közben a gombok le vannak tiltva
        private void SetButtonsEnabled(bool enabled)
        {
            btnBrowseImg.Enabled = enabled;
            btnNegate.Enabled = enabled;
            chkParallel.Enabled = enabled;
            btnGamma.Enabled = enabled;
            numGamma.Enabled = enabled;
            btnReset.Enabled = enabled;
            btnLog.Enabled = enabled;
            btnGrayscale.Enabled = enabled;
        }

        // Mindkét kép levétele és felszabadítása 
        private void ClearImages()
        {
            pbOrig.Image = null;
            pbResult.Image = null;

            originalBitmap?.Dispose();
            originalBitmap = null;

            resultBitmap?.Dispose();
            resultBitmap = null;

            originalImage = null;
            currentImage = null;

            lstHistory.Items.Clear();
        }

        // Új eredmény megjelenítése, az előző eredmény felszabadítása
        private void ShowResult(Bitmap newResult)
        {
            pbResult.Image = null;
            resultBitmap?.Dispose();
            resultBitmap = newResult;
            pbResult.Image = resultBitmap;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            ClearImages();
            base.OnFormClosed(e);
        }

        private void chkParallel_CheckedChanged(object sender, EventArgs e)
        {
            ImageOps.UseParallel = chkParallel.Checked;

        }

        private async void btnGamma_Click(object sender, EventArgs e)
        {
            double gamma = (double)numGamma.Value;
            await RunOperation($"Gamma ({gamma})", img => ImageOps.Gamma(img, gamma));
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (originalBitmap == null) return;

            currentImage = originalImage;
            ShowResult(new Bitmap(originalBitmap));
            lblTime.Text = "";
            lstHistory.Items.Clear();
        }

    
        private async void btnGrayscale_Click(object sender, EventArgs e)
        {
            await RunOperation("Szürkítés", ImageOps.Grayscale);
        }
    }
}
