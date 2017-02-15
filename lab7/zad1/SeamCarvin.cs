using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ASD
{
    public class SeamCarvin
    {
        /// <summary>
        /// Znajduje seam - ścieżke o minimalnej energii w tablicy.
        /// Uwaga: w result powinny znajdować się numery elementów w kolejnych wierszach.
        /// To znaczy array[i,result[i]] jest punktem na ścieżce         
        /// </summary>
        /// <param name="array"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static int CalculateSeam(int[,] array, out int[] result)
        {
            result = new int[0]; //usunąć
            //TODO

            return 0;
        }

        /// <summary>
        /// Skaluje bitmapę za pomocą obliczonego seam
        /// </summary>
        /// <param name="bmp">bitmapa</param>
        /// <param name="seam">tablica numerów pikseli w kolejnych wierzchołkach</param>
        /// <returns></returns>
        public static Bitmap ScaleBitmap(Bitmap bmp, int[] seam)
        {
            int depth = System.Drawing.Bitmap.GetPixelFormatSize(bmp.PixelFormat);
            int step = depth / 8;

            BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);

            int heightBmp = bmp.Height;
            int imgWidthBmp = bmp.Width;
            int widthBmp = Math.Abs(bitmapData.Stride) ;
			if (seam.Length != heightBmp) throw new ArgumentException("Długość znalezionego seam nie odpowiada wysokości bitmapy");
            int pixelCountBmp = widthBmp * heightBmp;
            
            byte[] pixelsBmp = new byte[pixelCountBmp ];
            Marshal.Copy(bitmapData.Scan0, pixelsBmp, 0, pixelsBmp.Length);
            bmp.UnlockBits(bitmapData);

            Bitmap newBmp = new Bitmap(bmp.Width - 1, bmp.Height, bmp.PixelFormat);

            BitmapData newBitmapData = newBmp.LockBits(new Rectangle(0, 0, bmp.Width-1, bmp.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, bmp.PixelFormat);

            int widthNewBmp = Math.Abs(newBitmapData.Stride) ;

            int pixelCountNewBmp = widthNewBmp * newBmp.Height;
            byte[] pixelsNewBmp = new byte[pixelCountNewBmp ];

            for (int y = 0; y < heightBmp; y++)
            {
                for (int x = 0; x < imgWidthBmp; x++)
                {

                    int i = y * widthBmp + x * step;
                    int j = -1;
                    if (x < seam[y])
                        j = y * widthNewBmp+ x * step;
                    else if (x > seam[y])
                        j = y * widthNewBmp + (x-1) * step;

                    if(j!=-1)
                        for (int k = 0; k < step; k++)
                        {
                            pixelsNewBmp[j + k] = pixelsBmp[i + k];
                        }
                }
            }

            Marshal.Copy(pixelsNewBmp, 0, newBitmapData.Scan0, pixelsNewBmp.Length);
            newBmp.UnlockBits(newBitmapData);

            return newBmp;
        }

        /// <summary>
        /// Oblicza energię dla bitmapy
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns>Tablicze energii poszczególnych pikseli</returns>
        public static int[,] ComputeEnergyTable(Bitmap bmp)
        {
            int[,] result = new int[ bmp.Height, bmp.Width];

            int depth = System.Drawing.Bitmap.GetPixelFormatSize(bmp.PixelFormat);
            int step = depth / 8;

            BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);
            int width = Math.Abs(bitmapData.Stride); //width+wyrównanie * step
            int bmpHeight = bitmapData.Height;
            int bmpWidth = bmp.Width;

            int pixelCount = width * bmpHeight;
 
            byte[] pixels = new byte[pixelCount ];

            // Copy data from pointer to array
            Marshal.Copy(bitmapData.Scan0, pixels, 0, pixels.Length);
            
            for (int y = 0; y < bmpHeight; y++)
                for (int x = 0; x < bmpWidth; x++)
                {
                    int i = y * width +x * step;
                    int r = pixels[i];
                    int g = pixels[i + 1];
                    int b = pixels[i + 2];
                    int c = 0;
                    int diff = 0;
                    if (x > 0)
                    {
                        c++;
                        int j = i - step;
                        diff += Math.Abs(r - pixels[j]) + Math.Abs(g - pixels[j + 1]) + Math.Abs(b - pixels[j + 2]);
                    }
                    if (x < bmpWidth - 1)
                    {
                        c++;
                        int j = i + step;
                        diff += Math.Abs(r - pixels[j]) + Math.Abs(g - pixels[j + 1]) + Math.Abs(b - pixels[j + 2]);
                    }
                    if (y > 0)
                    {
                        c++;
                        int j = i - width ;
                        diff += Math.Abs(r - pixels[j]) + Math.Abs(g - pixels[j + 1]) + Math.Abs(b - pixels[j + 2]);
                    }
                    if (y < bmpHeight - 1)
                    {
                        c++;
                        int j = i + width ;
                        diff += Math.Abs(r - pixels[j]) + Math.Abs(g - pixels[j + 1]) + Math.Abs(b - pixels[j + 2]);
                    }
                    diff /= c;
                    result[y,x] = diff;
                  
                }

            bmp.UnlockBits(bitmapData);
            return result;
        }
    }
}
