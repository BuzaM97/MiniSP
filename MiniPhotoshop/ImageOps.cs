using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace MiniPhotoshop
{
    public class ImageData
    {
        public int Width;
        public int Height;
        public byte[] Pixels;

        public ImageData(int width, int height)
        {
            Width = width;
            Height = height;
            Pixels = new byte[width * height * 4];
        }
    }

    public static class ImageOps
    {
        // Párhuzamos feldolgozás be/ki
        public static bool UseParallel = true;

        // Soronkénti ciklus: párhuzamosan vagy egy szálon
        private static void ForEachRow(int height, Action<int> rowAction)
        {
            if (UseParallel)
            {
                Parallel.For(0, height, rowAction);
            }
            else
            {
                for (int y = 0; y < height; y++)
                    rowAction(y);
            }
        }
        public static ImageData FromBitmap(Bitmap bmp)
        {
            var img = new ImageData(bmp.Width, bmp.Height);
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

            // 32 bites formátumban kérjük el, így minden sor pontosan Width * 4 bájt
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            try
            {
                Marshal.Copy(data.Scan0, img.Pixels, 0, img.Pixels.Length);
            }
            finally
            {
                bmp.UnlockBits(data);
            }

            return img;
        }

        public static Bitmap ToBitmap(ImageData img)
        {
            var bmp = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb);
            var rect = new Rectangle(0, 0, img.Width, img.Height);

            BitmapData data = bmp.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            try
            {
                Marshal.Copy(img.Pixels, 0, data.Scan0, img.Pixels.Length);
            }
            finally
            {
                bmp.UnlockBits(data);
            }

            return bmp;
        }

       
        public static ImageData ApplyLut(ImageData src, byte[] lut)
        {
            var dst = new ImageData(src.Width, src.Height);
            byte[] s = src.Pixels;
            byte[] d = dst.Pixels;
            int rowBytes = src.Width * 4;

            // Soronként párhuzamosan, minden szál a saját sorain dolgozik
            ForEachRow(src.Height, y =>
            {
                int start = y * rowBytes;
                int end = start + rowBytes;

                for (int i = start; i < end; i += 4)
                {
                    d[i]     = lut[s[i]];      // B
                    d[i + 1] = lut[s[i + 1]];  // G
                    d[i + 2] = lut[s[i + 2]];  // R
                    d[i + 3] = s[i + 3];       // A
                }
            });

            return dst;
        }

        // Negálás: S = 255 - R
        public static ImageData Negate(ImageData src)
        {
            byte[] lut = new byte[256];
            for (int i = 0; i < 256; i++)
                lut[i] = (byte)(255 - i);

            return ApplyLut(src, lut);
        }
        // Gamma transzformáció: S = 255 * (R / 255)^gamma
        // gamma < 1: világosít, gamma > 1: sötétít, gamma = 1: nem változik
        public static ImageData Gamma(ImageData src, double gamma)
        {
            byte[] lut = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                double value = 255.0 * Math.Pow(i / 255.0, gamma);
                lut[i] = (byte)Math.Min(255, (int)(value + 0.5));   // kerekítés + levágás
            }

            return ApplyLut(src, lut);
        }

        // Logaritmikus transzformáció: S = c * log(1 + R)
        // c = 255 / log(256), így a 255-ös bemenetből 255 lesz.
        // A sötét részeket kiemeli, a világosakat összenyomja.
        public static ImageData Logarithmic(ImageData src)
        {
            double c = 255.0 / Math.Log(256);

            byte[] lut = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                double value = c * Math.Log(1 + i);
                lut[i] = (byte)Math.Min(255, (int)(value + 0.5));
            }

            return ApplyLut(src, lut);
        }
        // Szürkítés: Y = 0.299 R + 0.587 G + 0.114 B
        // Egész számokkal: az együtthatók 256-tal felszorozva (77 + 150 + 29 = 256),
        // a végén >> 8 osztja vissza. A szorzásokat táblázatok váltják ki.
        public static ImageData Grayscale(ImageData src)
        {
            int[] lutR = new int[256];
            int[] lutG = new int[256];
            int[] lutB = new int[256];
            for (int i = 0; i < 256; i++)
            {
                lutR[i] = 77 * i;
                lutG[i] = 150 * i;
                lutB[i] = 29 * i;
            }

            var dst = new ImageData(src.Width, src.Height);
            byte[] s = src.Pixels;
            byte[] d = dst.Pixels;
            int rowBytes = src.Width * 4;

            ForEachRow(src.Height, y =>
            {
                int start = y * rowBytes;
                int end = start + rowBytes;

                for (int i = start; i < end; i += 4)
                {
                    // +128 a kerekítéshez (fél egység), >> 8 = osztás 256-tal
                    byte gray = (byte)((lutB[s[i]] + lutG[s[i + 1]] + lutR[s[i + 2]] + 128) >> 8);

                    d[i] = gray;      // B
                    d[i + 1] = gray;      // G
                    d[i + 2] = gray;      // R
                    d[i + 3] = s[i + 3];  // A
                }
            });

            return dst;
        }
    }
}
