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
    public class Palette : IFilter
    {
        private RemapColours remap;

        private Color[] rgbValues = new Color[256];

        public Color[] RGBValues { get { return rgbValues; } }  //remapping look-up table

        public Palette(RemapColours remap)                      //constructor
        {
            this.remap = remap;                                 //generate palette from a list of points
            GenerateColorPalette(remap.Points);
        }

        public void GenerateColorPalette(List<Color> remap)
        {

            for (int i = 0; i < remap.Count - 1; i++)
            {
                int gray1 = (int)Math.Ceiling((decimal)(256 / (remap.Count - 1) * i));
                int gray2 = (int)Math.Ceiling((decimal)(256 / (remap.Count - 1) * (i + 1)));

                for (int j = gray1; j < gray2; j++)
                {
                    int r = MathF.LinearInterpolation(new Point(gray1, remap[i].R), new Point(gray2, remap[i + 1].R), j);
                    int g = MathF.LinearInterpolation(new Point(gray1, remap[i].G), new Point(gray2, remap[i + 1].G), j);
                    int b = MathF.LinearInterpolation(new Point(gray1, remap[i].B), new Point(gray2, remap[i + 1].B), j);
                    RGBValues[j] = Color.FromArgb(r, g, b);
                }
            }
        }
                                    

        public Bitmap ApplyFilter(Bitmap image)  //intensity tranformation function
        {
            int width = image.Width;
            int height = image.Height;
            BitmapData srcData = image.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb); //Lock bitmap bits to system memory
            int bytes = srcData.Stride * srcData.Height;
            byte[] buffer = new byte[bytes];                    //Declare array in which intensity values will be stored
            byte[] result = new byte[bytes];
            Marshal.Copy(srcData.Scan0, buffer, 0, bytes);      //Copy intensity values in array

            image.UnlockBits(srcData);
            int current;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    current = y * srcData.Stride + x * 4;

                    Color colour = RGBValues[buffer[current]];

                    result[current + 0] = colour.R;             //Calculate new intensity values
                    result[current + 1] = colour.G;
                    result[current + 2] = colour.B;
                    result[current + 3] = 255;
                }
            }
            Bitmap output = new Bitmap(width, height);

            BitmapData outdata = output.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(result, 0, outdata.Scan0, bytes);      //Copy changed intensity values back to the bitmap

            output.UnlockBits(outdata);                         //Unlock bits from system memory

            return output;

        }
    }
    /*
    public Bitmap ApplyFilter(Bitmap input)
        {
            Bitmap output = new Bitmap(input.Width, input.Height);

            for (int i = 0; i < input.Width; i++)
            {
                for (int j = 0; j < input.Height; j++)
                {
                    int op = input.GetPixel(i, j).R;

                    Color np = RGBValues[op];

                    output.SetPixel(i, j, np);
                }
            }
            return output;
        }
    }
    */
}
