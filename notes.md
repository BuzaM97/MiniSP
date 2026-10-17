**Image Processing Methods**
**Negation** (Negálás)
Equation:
new_pixel_data = 255 - old_pixel_data
Links:
https://abhijitnathwani.github.io/blog/2017/12/21/Negative-Image-using-C

**Gamma transformation** (Gamma transzformáció)
Equation
𝑆 = 𝑐 ∗ 𝑅 𝛾 S = output pixel, c constans (1), R input pixel, 𝛾 scale of transformation.
Links
https://www.programmingalgorithms.com/algorithm/gamma/
https://stackoverflow.com/questions/11211260/gamma-correction-power-law-transformation

**Logarithmic transformation** (Logaritmikus transzformáció)
Equation
S = c \_ log (1 + r)  
 where,
R = input pixel value,
C = scaling constant and
S = output pixel value
Links
https://www.slideshare.net/slideshow/log-transformation-in-image-processing-with-example/168718438
https://www.geeksforgeeks.org/python/log-transformation-of-an-image-using-python-and-opencv/

**Grayscale**(Szürkítés)
Equation
0.299 _ R + 0.587 _ G + 0.114 \_ B
Links
https://stackoverflow.com/questions/2265910/convert-an-image-to-grayscale

**Histogram equalization**(Hisztogram kiegyenlítés)
Equation
h(v) = round( (cdf(v) - cdf_min) / (N - cdf_min) \* (L - 1) )

**Histogram**(Hisztogram készítés)
Equation
h(v) = number of pixels with value v, v = 0..255
grey value: 0.299 _ R + 0.587 _ G + 0.114 \_ B
pixels: 10, 10, 90, 10, 90 --> h(10) = 3, h(90) = 2, every other h(v) = 0
Links
https://homepages.inf.ed.ac.uk/rbf/HIPR2/histgram.htm

**Sobel edge detector**(Sobel éldetektor)
Equation
[ -1 0 1 ] [ -1 -2 -1 ]
Gx = [ -2 0 2 ] Gy = [ 0 0 0 ]
[ -1 0 1 ] [ 1 2 1 ]

G = |Gx| + |Gy| (max 255)

[ 10 10 90 ]
[ 10 10 90 ]
[ 10 10 90 ]

Gx = (-10 + 90) + (-20 + 180) + (-10 + 90) = 320
Gy = (-10 - 20 - 90) + (10 + 20 + 90) = 0
G = 320 + 0 = 320 --> 255 (edge)
Links
https://homepages.inf.ed.ac.uk/rbf/HIPR2/sobel.htm

**Laplace edge detector**(Laplace éldetektor)
Equation
[ 0 1 0 ]
L = [ 1 -4 1 ]
[ 0 1 0 ]

new pixel = | 10 + 10 + 10 + 10 - 4 \* 90 |
[ 10 10 10 ]
[ 10 90 10 ] --> | 40 - 360 | = 320 --> 255
[ 10 10 10 ]
flat area (all 10): | 40 - 40 | = 0
Links
https://homepages.inf.ed.ac.uk/rbf/HIPR2/log.htm

**Feature point detection**(Jellemzőpontok detektálása, Harris)
Equation
Ix, Iy = Sobel Gx, Gy
A = Σ Ix _ Ix, B = Σ Iy _ Iy, C = Σ Ix _ Iy (3x3 window)
R = (A _ B - C _ C) - k _ (A + B)^2, k = 0.04

edge: A = 90000, B = 0, C = 0 --> R = 0 - 0.04 \* 90000^2 < 0
corner: A = 90000, B = 90000, C = 0 --> R = 8.1e9 - 1.3e9 > 0
corner point: R > threshold and R is a local maximum
Links
https://en.wikipedia.org/wiki/Harris_corner_detector

Links
https://stackoverflow.com/questions/74326873/histogram-equalization-on-a-color-image
https://stackoverflow.com/questions/74326873/histogram-equalization-on-a-color-image
**Box filter**(Átlagoló szűrő (Box szűrő))
Equation
Original pixels:  
[ 10 10 10 ]
[ 10 90 10 ] --> (10 + 10 + 10 + 10 + 90 + 10 + 10 + 10 + 10) / 9 = 170 / 9 ≈ 19
[ 10 10 10 ]
90 --> 19
Links
https://gist.github.com/aspose-net/f13d69ecf94b72ab54b29145a8591175
https://gist.github.com/aspose-net/f13d69ecf94b72ab54b29145a8591175
**Gauss filter**(Gauss szűrő)
Equation
[ 10 10 10 ]
[ 10 90 10 ]  
[ 10 10 10 ]

    [ 1  2  1 ]

K = [ 2 4 2 ]  
 [ 1 2 1 ]

1 + 2 + 1 + 2 + 4 + 2 + 1 + 2 + 1 = 16

new pixels = = (10 + 20 + 10 + 20 + 360 + 20 + 10 + 20 + 10) / 16
= 480 / 16
= 30
Links
https://lodev.org/cgtutor/filtering.html

**Used classes**
OpenFileDialog
Usage: For browse images
https://learn.microsoft.com/hu-hu/dotnet/api/system.windows.forms.openfiledialog?view=windowsdesktop-10.0

Bitmap Class:
Usage: Define pixel data to work with it
https://learn.microsoft.com/en-us/dotnet/api/system.drawing.bitmap?view=net-11.0-pp

Pontonkénti műveletek:
https://pippin.gimp.org/image-processing/chap_point.html

Pixlek elérése:
https://learn.microsoft.com/hu-hu/dotnet/api/system.drawing.bitmap.lockbits?view=net-11.0-pp

Stopwatch xeample:
https://learn.microsoft.com/en-us/dotnet/standard/parallel-programming/how-to-write-a-simple-parallel-for-loop
