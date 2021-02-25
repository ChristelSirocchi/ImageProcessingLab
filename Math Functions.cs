using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    class MathF
    {
        public static int LinearInterpolation(Point a, Point b, int x)
        {
            int y = (a.Y + (x - a.X) * (b.Y - a.Y) / (b.X - a.X));
            return y;
        }

        public static int NewSize(int length, double factor)
        {
            int lengthout = (int)factor * length;
            return lengthout;
        }

        public static int CheckRange(int value) // check if value falls within intensity range
        {
            if (value < 0)
                value = 0;
            if (value > 255)
                value = 255;
            return value;
        }

        public static int Distance(int n1, int n2)
        {
            int result = (int)Math.Sqrt((n1 * n1) + (n2 * n2));
            return result;
        }

        public static int GetMax(Bitmap bmp)
        {
            int max = bmp.GetPixel(0, 0).R;

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    max = bmp.GetPixel(x, y).R > max ? bmp.GetPixel(x, y).R : max;
                }
            }
            return max;
        }

        public static int GetMin(Bitmap bmp)
        {
            int min = bmp.GetPixel(0, 0).R;

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    min = bmp.GetPixel(x, y).R < min ? bmp.GetPixel(x, y).R : min;
                }
            }
            return min;
        }

        public static double GetAvg(Bitmap bmp)
        {
            double avg;
            double value = 0.0;

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    value += bmp.GetPixel(x, y).R;
                }
            }
            avg = value / (bmp.Height * bmp.Width);

            return avg;
        }

        public static double GetSdv(Bitmap bmp)
        {
            double avg = GetAvg(bmp);
            double value = 0.0;
            double sdv;

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    value += Math.Sqrt(Math.Pow((bmp.GetPixel(x, y).R - avg), 2));
                }
            }
            sdv = Math.Sqrt(value / (bmp.Height * bmp.Width));

            return sdv;
        }
    }
}
