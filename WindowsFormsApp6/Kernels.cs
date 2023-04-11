using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp6
{
    public class Kernels
    {
        public static object PictureBox2 { get; private set; }

        static void Kernel(string[] args)
        {
            // Define an image kernel
            float[,] kernel = {
            { -1, 0, 1 },
            { -2, 0, 2 },
            { -1, 0, 1 }
        };

            // Load an image
            Bitmap image = new Bitmap("openFile.FileName.png");

            // Apply the kernel to the image
            Bitmap filteredImage = Convolution(image, kernel);

            // Save the filtered image
            filteredImage.Save("openFile.FileName.png");
        }

        static Bitmap Convolution(Bitmap image, float[,] kernel)
        {
            int width = image.Width;
            int height = image.Height;

            // Create a new bitmap to store the filtered image
            Bitmap filteredImage = new Bitmap(width, height);

            // Lock the bitmap data to improve performance
            BitmapData bitmapData = filteredImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytesPerPixel = 4;
            int stride = bitmapData.Stride;
            IntPtr scan0 = bitmapData.Scan0;

            unsafe
            {
                byte* ptr = (byte*)scan0;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        float r = 0, g = 0, b = 0;

                        // Apply the kernel to each pixel in the image
                        for (int ky = 0; ky < kernel.GetLength(0); ky++)
                        {
                            for (int kx = 0; kx < kernel.GetLength(1); kx++)
                            {
                                int px = x + kx - 1;
                                int py = y + ky - 1;

                                if (px >= 0 && px < width && py >= 0 && py < height)
                                {
                                    Color pixelColor = image.GetPixel(px, py);

                                    r += kernel[ky, kx] * pixelColor.R;
                                    g += kernel[ky, kx] * pixelColor.G;
                                    b += kernel[ky, kx] * pixelColor.B;
                                }
                            }
                        }

                        // Set the color of the filtered pixel in the bitmap
                        ptr[(y * stride) + (x * bytesPerPixel) + 3] = 255;
                        ptr[(y * stride) + (x * bytesPerPixel) + 2] = (byte)Math.Max(0, Math.Min(255, r));
                        ptr[(y * stride) + (x * bytesPerPixel) + 1] = (byte)Math.Max(0, Math.Min(255, g));
                        ptr[(y * stride) + (x * bytesPerPixel) + 0] = (byte)Math.Max(0, Math.Min(255, b));
                    }
                }
            }

            // Unlock the bitmap data
            filteredImage.UnlockBits(bitmapData);

            return filteredImage;
        }

        internal string Kernel(Kernels thiskernel)
        {
            throw new NotImplementedException();
        }

        internal string Kernel(Bitmap ker)
        {
            throw new NotImplementedException();
        }
    }
}

