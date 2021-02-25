using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ImageProcessingWF
{
    class ConvFilter : IFilter
    {
        //Properties
        public double[,] Kernel { get; set; }   //convolution kernel
        public double Weight { get; set; }      //value that divides kernel values before convolution
        public double Offset { get; set; }      //value added to the result of convolution
        public int KernelHeight
        {
            get { return Kernel.GetUpperBound(0) + 1; } //width of kernel
        }
        public int KernelWidth
        {
            get { return Kernel.GetUpperBound(1) + 1; } //height of kernel
        }

        // methods
        public void Normalize()     //sets the weight of the kernel as the sum of all kernel values
        {
            double v = 0;
            for (int i = 0; i < KernelHeight; i++)
            {
                for (int j = 0; j < KernelWidth; j++)
                {
                    v += Kernel[i, j];
                }
            }
            Weight = v;
        }

        public void ZeroKernel()    //sets the central value of the kernel so that the sum of all kernel values is zero
        {
            int xcentral = (KernelHeight - 1) / 2;
            int ycentral = (KernelWidth - 1) / 2;

            double v = 0;
            for (int i = 0; i < KernelHeight; i++)
            {
                for (int j = 0; j < KernelWidth; j++)
                {
                    v += Kernel[i, j];
                }
            }
            v -= Kernel[xcentral, ycentral];

            Kernel[xcentral, ycentral] = v;
        }
        public Bitmap ApplyFilter(Bitmap srcImage)  // apply tranformation (LockBits method)
        {
            int width = srcImage.Width;
            int height = srcImage.Height;
            BitmapData srcData = srcImage.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int bytes = srcData.Stride * srcData.Height;
            byte[] buffer = new byte[bytes];
            byte[] result = new byte[bytes];
            Marshal.Copy(srcData.Scan0, buffer, 0, bytes);
            srcImage.UnlockBits(srcData);
            int colorChannels = 3;
            double[] rgb = new double[colorChannels];
            int foff = (Kernel.GetLength(0) - 1) / 2;
            int kcenter;
            int kpixel;
            for (int y = foff; y < height - foff; y++)
            {
                for (int x = foff; x < width - foff; x++)
                {
                    for (int c = 0; c < colorChannels; c++)
                    {
                        rgb[c] = 0.0;
                    }
                    kcenter = y * srcData.Stride + x * 4;
                    for (int fy = -foff; fy <= foff; fy++)
                    {
                        for (int fx = -foff; fx <= foff; fx++)
                        {
                            kpixel = kcenter + fy * srcData.Stride + fx * 4;
                            for (int c = 0; c < colorChannels; c++)
                            {
                                rgb[c] += (double)(buffer[kpixel + c]) * Kernel[fy + foff, fx + foff];
                            }
                        }
                    }
                    for (int c = 0; c < colorChannels; c++)
                    {
                        rgb[c] = (int)(Offset + rgb[c] / Weight);

                        if (rgb[c] > 255)
                        {
                            rgb[c] = 255;
                        }
                        else if (rgb[c] < 0)
                        {
                            rgb[c] = 0;
                        }
                    }
                    for (int c = 0; c < colorChannels; c++)
                    {
                        result[kcenter + c] = (byte)rgb[c];
                    }
                    result[kcenter + 3] = 255;
                }
            }
            Bitmap resultImage = new Bitmap(width, height);
            BitmapData resultData = resultImage.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(result, 0, resultData.Scan0, bytes);
            resultImage.UnlockBits(resultData);
            return resultImage;
        }

        /* GetPixel SetPixel method (slow)
        public Bitmap ApplyFilter(Bitmap input) 
        {
            Bitmap output = new Bitmap(input.Width, input.Height);

            int offsetX = KernelWidth / 2;
            int offsetY = KernelHeight / 2;

            int xmin = offsetX;
            int xmax = input.Width - offsetX - 1;

            int ymin = offsetY;
            int ymax = input.Height - offsetY - 1;

            int op, np;
            double v;

            for (int x = xmin; x <= xmax; x++)
            {
                for (int y = ymin; y <= ymax; y++)
                {
                    v = 0;
                    for (int c = 0; c < KernelWidth; c++)
                    {
                        for (int r = 0; r < KernelHeight; r++)
                        {
                            op = input.GetPixel(x - offsetX + c, y - offsetY + r).R;
                            v += op * Kernel[c, r];
                        }
                    }
                    np = (int)(Offset + v / Weight);
                    np = MathF.CheckRange(np);

                    output.SetPixel(x, y, Color.FromArgb(np, np, np));
                }
            }
            return output;
        }
        */

    }
}
