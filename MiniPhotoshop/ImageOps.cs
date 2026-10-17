using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace MiniPhotoshop
{
    // Kép bájttömbként, pixelenként 4 bájt: B, G, R, A
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
        public static bool UseParallel = true;

        private static void ProcessRows(int count, Action<int> processRow)
        {
            if (UseParallel)
            {
                Parallel.For(0, count, processRow);
            }
            else
            {
                for (int y = 0; y < count; y++)
                    processRow(y);
            }
        }

        #region Bitmap <-> tömb

        public static ImageData FromBitmap(Bitmap bmp)
        {
            var img = new ImageData(bmp.Width, bmp.Height);
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

            // 32 bit -> minden sor pontosan Width * 4 bájt
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

        // szürke érték pixelenként 1 bájt (Sobel, Laplace, Harris használja)
        private static byte[] ToGrayArray(ImageData src)
        {
            int w = src.Width;
            byte[] s = src.Pixels;
            byte[] gray = new byte[w * src.Height];

            ProcessRows(src.Height, y =>
            {
                int p = y * w;
                int i = p * 4;
                for (int x = 0; x < w; x++, p++, i += 4)
                    gray[p] = (byte)((29 * s[i] + 150 * s[i + 1] + 77 * s[i + 2] + 128) >> 8);
            });

            return gray;
        }

        private static ImageData FromGrayArray(byte[] gray, int w, int h)
        {
            var dst = new ImageData(w, h);
            byte[] d = dst.Pixels;

            ProcessRows(h, y =>
            {
                int p = y * w;
                int i = p * 4;
                for (int x = 0; x < w; x++, p++, i += 4)
                {
                    d[i] = d[i + 1] = d[i + 2] = gray[p];
                    d[i + 3] = 255;
                }
            });

            return dst;
        }

        #endregion

        #region Pontonkénti műveletek (LUT)

        public static ImageData ApplyLut(ImageData src, byte[] lut)
        {
            var dst = new ImageData(src.Width, src.Height);
            byte[] s = src.Pixels;
            byte[] d = dst.Pixels;
            int rowBytes = src.Width * 4;

            ProcessRows(src.Height, y =>
            {
                int end = (y + 1) * rowBytes;
                for (int i = y * rowBytes; i < end; i += 4)
                {
                    d[i] = lut[s[i]];
                    d[i + 1] = lut[s[i + 1]];
                    d[i + 2] = lut[s[i + 2]];
                    d[i + 3] = s[i + 3];   // alfa marad
                }
            });

            return dst;
        }

        public static ImageData Negate(ImageData src)
        {
            byte[] lut = new byte[256];
            for (int i = 0; i < 256; i++)
                lut[i] = (byte)(255 - i);

            return ApplyLut(src, lut);
        }

        // S = 255 * (R/255)^gamma  (gamma < 1 világosít)
        public static ImageData Gamma(ImageData src, double gamma)
        {
            byte[] lut = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                double value = 255.0 * Math.Pow(i / 255.0, gamma);
                lut[i] = (byte)Math.Min(255, (int)(value + 0.5));
            }

            return ApplyLut(src, lut);
        }

        // S = c * log(1 + R), c = 255 / log(256) -> 255-ből 255 lesz
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

        // 0.299 R + 0.587 G + 0.114 B, 256-tal felszorozva: 77 + 150 + 29 = 256
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

            ProcessRows(src.Height, y =>
            {
                int end = (y + 1) * rowBytes;
                for (int i = y * rowBytes; i < end; i += 4)
                {
                    // +128 kerekítés, >> 8 = /256
                    byte gray = (byte)((lutB[s[i]] + lutG[s[i + 1]] + lutR[s[i + 2]] + 128) >> 8);
                    d[i] = d[i + 1] = d[i + 2] = gray;
                    d[i + 3] = s[i + 3];
                }
            });

            return dst;
        }

        #endregion

        #region Hisztogram

        // sávokra bontva, mert ha két szál ugyanazt a számlálót növeli, elveszhet egy
        public static int[] ComputeHistogram(ImageData src)
        {
            int bands = Environment.ProcessorCount;
            int[][] partial = new int[bands][];
            int rowBytes = src.Width * 4;
            byte[] s = src.Pixels;

            ProcessRows(bands, b =>
            {
                int[] h = new int[256];
                int from = src.Height * b / bands * rowBytes;
                int to = src.Height * (b + 1) / bands * rowBytes;

                for (int i = from; i < to; i += 4)
                    h[(29 * s[i] + 150 * s[i + 1] + 77 * s[i + 2] + 128) >> 8]++;

                partial[b] = h;
            });

            int[] hist = new int[256];
            foreach (int[] h in partial)
                for (int v = 0; v < 256; v++)
                    hist[v] += h[v];

            return hist;
        }

        public static Bitmap DrawHistogram(int[] hist, int width, int height)
        {
            var bmp = new Bitmap(width, height);
            int max = Math.Max(hist.Max(), 1);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                for (int v = 0; v < 256; v++)
                {
                    int x = v * width / 256;
                    int bar = (int)((long)hist[v] * (height - 1) / max);
                    g.DrawLine(Pens.Black, x, height - 1, x, height - 1 - bar);
                }
            }

            return bmp;
        }

        // lut[v] = (cdf[v] - cdfMin) * 255 / (N - cdfMin)
        public static ImageData EqualizeHistogram(ImageData src)
        {
            int[] hist = ComputeHistogram(src);

            int[] cdf = new int[256];
            cdf[0] = hist[0];
            for (int v = 1; v < 256; v++)
                cdf[v] = cdf[v - 1] + hist[v];

            int cdfMin = cdf.First(c => c > 0);
            long range = (long)src.Width * src.Height - cdfMin;

            byte[] lut = new byte[256];
            for (int v = 0; v < 256; v++)
            {
                if (range == 0)
                    lut[v] = (byte)v;   // egyszínű kép
                else
                    lut[v] = (byte)((Math.Max(0, cdf[v] - cdfMin) * 255L + range / 2) / range);
            }

            return ApplyLut(src, lut);
        }

        #endregion

        #region Szűrők (3x3)

        // szélen a szélső pixel ismétlődik
        public static ImageData BoxFilter(ImageData src)
        {
            int w = src.Width;
            int h = src.Height;

            // összeg max 9 * 255 = 2295, osztás helyett táblázat
            byte[] div9 = new byte[9 * 255 + 1];
            for (int sum = 0; sum < div9.Length; sum++)
                div9[sum] = (byte)((sum + 4) / 9);

            var dst = new ImageData(w, h);
            byte[] s = src.Pixels;
            byte[] d = dst.Pixels;
            int rowBytes = w * 4;

            ProcessRows(h, y =>
            {
                int up = Math.Max(y - 1, 0) * rowBytes;
                int mid = y * rowBytes;
                int down = Math.Min(y + 1, h - 1) * rowBytes;

                for (int x = 0; x < w; x++)
                {
                    int l = Math.Max(x - 1, 0) * 4;
                    int m = x * 4;
                    int r = Math.Min(x + 1, w - 1) * 4;

                    for (int c = 0; c < 3; c++)
                    {
                        int sum = s[up + l + c] + s[up + m + c] + s[up + r + c]
                                + s[mid + l + c] + s[mid + m + c] + s[mid + r + c]
                                + s[down + l + c] + s[down + m + c] + s[down + r + c];

                        d[mid + m + c] = div9[sum];
                    }
                    d[mid + m + 3] = s[mid + m + 3];
                }
            });

            return dst;
        }

        // [1 2 1; 2 4 2; 1 2 1] / 16
        public static ImageData GaussFilter(ImageData src)
        {
            int w = src.Width;
            int h = src.Height;

            var dst = new ImageData(w, h);
            byte[] s = src.Pixels;
            byte[] d = dst.Pixels;
            int rowBytes = w * 4;

            ProcessRows(h, y =>
            {
                int up = Math.Max(y - 1, 0) * rowBytes;
                int mid = y * rowBytes;
                int down = Math.Min(y + 1, h - 1) * rowBytes;

                for (int x = 0; x < w; x++)
                {
                    int l = Math.Max(x - 1, 0) * 4;
                    int m = x * 4;
                    int r = Math.Min(x + 1, w - 1) * 4;

                    for (int c = 0; c < 3; c++)
                    {
                        int sum = s[up + l + c] + 2 * s[up + m + c] + s[up + r + c]
                                + 2 * s[mid + l + c] + 4 * s[mid + m + c] + 2 * s[mid + r + c]
                                + s[down + l + c] + 2 * s[down + m + c] + s[down + r + c];

                        d[mid + m + c] = (byte)((sum + 8) >> 4);   // /16 kerekítve
                    }
                    d[mid + m + 3] = s[mid + m + 3];
                }
            });

            return dst;
        }

        #endregion

        #region Éldetektálás

        // Gx = [-1 0 1; -2 0 2; -1 0 1],  Gy = [-1 -2 -1; 0 0 0; 1 2 1]
        // előjellel kell, a Harris is ezt használja
        private static (short[] ix, short[] iy) SobelGradients(byte[] g, int w, int h)
        {
            short[] ix = new short[w * h];
            short[] iy = new short[w * h];

            ProcessRows(h, y =>
            {
                int up = Math.Max(y - 1, 0) * w;
                int mid = y * w;
                int down = Math.Min(y + 1, h - 1) * w;

                for (int x = 0; x < w; x++)
                {
                    int l = Math.Max(x - 1, 0);
                    int r = Math.Min(x + 1, w - 1);

                    ix[mid + x] = (short)(g[up + r] + 2 * g[mid + r] + g[down + r]
                                        - g[up + l] - 2 * g[mid + l] - g[down + l]);

                    iy[mid + x] = (short)(g[down + l] + 2 * g[down + x] + g[down + r]
                                        - g[up + l] - 2 * g[up + x] - g[up + r]);
                }
            });

            return (ix, iy);
        }

        // G = |Gx| + |Gy|, max 255
        public static ImageData Sobel(ImageData src)
        {
            int w = src.Width;
            int h = src.Height;
            var (ix, iy) = SobelGradients(ToGrayArray(src), w, h);

            // |Gx| + |Gy| max 2040
            byte[] clamp = new byte[2041];
            for (int v = 0; v < clamp.Length; v++)
                clamp[v] = (byte)Math.Min(v, 255);

            byte[] edges = new byte[w * h];
            ProcessRows(h, y =>
            {
                int end = (y + 1) * w;
                for (int p = y * w; p < end; p++)
                    edges[p] = clamp[Math.Abs(ix[p]) + Math.Abs(iy[p])];
            });

            return FromGrayArray(edges, w, h);
        }

        // [0 1 0; 1 -4 1; 0 1 0], eredmény |L|
        public static ImageData Laplace(ImageData src)
        {
            int w = src.Width;
            int h = src.Height;
            byte[] g = ToGrayArray(src);

            // |L| max 4 * 255 = 1020
            byte[] clamp = new byte[1021];
            for (int v = 0; v < clamp.Length; v++)
                clamp[v] = (byte)Math.Min(v, 255);

            byte[] edges = new byte[w * h];
            ProcessRows(h, y =>
            {
                int up = Math.Max(y - 1, 0) * w;
                int mid = y * w;
                int down = Math.Min(y + 1, h - 1) * w;

                for (int x = 0; x < w; x++)
                {
                    int l = Math.Max(x - 1, 0);
                    int r = Math.Min(x + 1, w - 1);

                    int sum = g[up + x] + g[mid + l] + g[mid + r] + g[down + x] - 4 * g[mid + x];
                    edges[mid + x] = clamp[Math.Abs(sum)];
                }
            });

            return FromGrayArray(edges, w, h);
        }

        #endregion

        #region Jellemzőpontok (Harris)

        public static ImageData Harris(ImageData src)
        {
            int w = src.Width;
            int h = src.Height;

            var (ix, iy) = SobelGradients(ToGrayArray(src), w, h);
            long[] harrisR = HarrisResponse(ix, iy, w, h);
            List<int>[] corners = FindCorners(harrisR, w, h);

            return DrawCorners(src, corners);
        }

        // R = (A*B - C*C) - k*(A+B)^2,  A = ΣIx², B = ΣIy², C = ΣIxIy (3x3)
        private static long[] HarrisResponse(short[] ix, short[] iy, int w, int h)
        {
            long[] harrisR = new long[w * h];

            ProcessRows(h, y =>
            {
                int[] rows = { Math.Max(y - 1, 0) * w, y * w, Math.Min(y + 1, h - 1) * w };

                for (int x = 0; x < w; x++)
                {
                    int[] cols = { Math.Max(x - 1, 0), x, Math.Min(x + 1, w - 1) };

                    long a = 0, b = 0, c = 0;
                    foreach (int row in rows)
                    {
                        foreach (int col in cols)
                        {
                            int gx = ix[row + col];
                            int gy = iy[row + col];
                            a += gx * gx;
                            b += gy * gy;
                            c += gx * gy;
                        }
                    }

                    // k = 0.04 = 1/25, így nem kell double
                    harrisR[y * w + x] = (a * b - c * c) - (a + b) * (a + b) / 25;
                }
            });

            return harrisR;
        }

        // sarok: R > max/100 és a 3x3 környezetben ő a legnagyobb
        private static List<int>[] FindCorners(long[] harrisR, int w, int h)
        {
            long[] rowMax = new long[h];
            ProcessRows(h, y =>
            {
                long m = long.MinValue;
                for (int p = y * w; p < (y + 1) * w; p++)
                    m = Math.Max(m, harrisR[p]);
                rowMax[y] = m;
            });

            long threshold = rowMax.Max() / 100;
            List<int>[] corners = new List<int>[h];

            ProcessRows(h, y =>
            {
                corners[y] = new List<int>();
                if (threshold <= 0) return;   // nincs sarok, pl. egyszínű kép

                for (int x = 0; x < w; x++)
                {
                    long v = harrisR[y * w + x];
                    if (v > threshold && IsLocalMax(harrisR, w, h, x, y, v))
                        corners[y].Add(x);
                }
            });

            return corners;
        }

        private static bool IsLocalMax(long[] harrisR, int w, int h, int x, int y, long v)
        {
            for (int yy = Math.Max(y - 1, 0); yy <= Math.Min(y + 1, h - 1); yy++)
                for (int xx = Math.Max(x - 1, 0); xx <= Math.Min(x + 1, w - 1); xx++)
                    if (harrisR[yy * w + xx] > v)
                        return false;

            return true;
        }

        // egy szálon, mert a négyzetek átfedhetnek
        private static ImageData DrawCorners(ImageData src, List<int>[] corners)
        {
            int w = src.Width;
            int h = src.Height;
            var dst = new ImageData(w, h);
            Array.Copy(src.Pixels, dst.Pixels, src.Pixels.Length);
            byte[] d = dst.Pixels;

            int radius = Math.Max(2, w / 400);   // 4K képen kb. 9 px, kisebbnél nem látszana

            for (int y = 0; y < h; y++)
            {
                foreach (int x in corners[y])
                {
                    for (int yy = Math.Max(y - radius, 0); yy <= Math.Min(y + radius, h - 1); yy++)
                    {
                        for (int xx = Math.Max(x - radius, 0); xx <= Math.Min(x + radius, w - 1); xx++)
                        {
                            int i = (yy * w + xx) * 4;
                            d[i] = 0;
                            d[i + 1] = 0;
                            d[i + 2] = 255;   // piros
                            d[i + 3] = 255;
                        }
                    }
                }
            }

            return dst;
        }

        #endregion
    }
}
