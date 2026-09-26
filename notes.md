**Image Processing Methods**
	**Negation** (Negálás)
		Equation:
			new_pixel_data = 255 - old_pixel_data
		Links:
			https://abhijitnathwani.github.io/blog/2017/12/21/Negative-Image-using-C
	**Gamma transformation** (Gamma transzformáció)
		Equation
		𝑆 = 𝑐 ∗ 𝑅 𝛾  S = output pixel, c constans (1), R input pixel, 𝛾 scale of transformation.
		Links
			https://www.programmingalgorithms.com/algorithm/gamma/
			https://stackoverflow.com/questions/11211260/gamma-correction-power-law-transformation
	**Logarithmic transformation** (Logaritmikus transzformáció)		
		Equation
				S = c * log (1 + r)		  
				where,
				R = input pixel value,
				C = scaling constant and
				S = output pixel value
		Links
			https://www.slideshare.net/slideshow/log-transformation-in-image-processing-with-example/168718438
			https://www.geeksforgeeks.org/python/log-transformation-of-an-image-using-python-and-opencv/
	**Grayscale**(Szürkítés)
		Equation
			0.299 * R + 0.587 * G + 0.114 * B
		Links
			https://stackoverflow.com/questions/2265910/convert-an-image-to-grayscale			
	**Histogram equalization**(Hisztogram kiegyenlítés)
			Equation
			
			Links