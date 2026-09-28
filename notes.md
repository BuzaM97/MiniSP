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

Links
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
