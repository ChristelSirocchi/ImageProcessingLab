using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    class ImageStats
    {
        private readonly byte max;      //maximum  intensity value in Bitmap
        private readonly byte min;      //minimum  intensity value in Bitmap
        private readonly float avg;     //average  intensity value in Bitmap
        private readonly float sdv;     //standard deviation  intensity valuesin Bitmap
        private readonly int[] histo;  //histogram representing intensity frequency in Bitmap

        //Properties to access private attribute 
        public byte Max { get => max; }
        public byte Min { get => min; }
        public double Avg { get => avg; }
        public double Sdv { get => sdv; }
        public int[] Histo { get => histo; }

        public ImageStats(Bitmap bmp)    //constructor from Bitmap
        {
            GetStats(bmp, out max, out min, out avg, out histo);
            sdv = GetSdv(bmp, avg);
        }

        public static void GetStats(Bitmap bmp, out byte max, out byte min, out float avg, out int[] histo)
        {
            float value = 0;
            int c;
            histo = new int[256];
            max = bmp.GetPixel(0, 0).R;
            min = bmp.GetPixel(0, 0).R;

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    max = bmp.GetPixel(x, y).R > max ? bmp.GetPixel(x, y).R : max;  // find maximum value
                    min = bmp.GetPixel(x, y).R < min ? bmp.GetPixel(x, y).R : min;  // find minimum value
                    value += bmp.GetPixel(x, y).R;
                    
                    c = bmp.GetPixel(x, y).R;
                    histo[c]++;
                }
            }
            avg = value / (bmp.Height * bmp.Width);                                 // calculate average
        }

        public static float GetSdv(Bitmap bmp, float avg)
        {
            double value = 0.0;

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    value += Math.Sqrt(Math.Pow((bmp.GetPixel(x, y).R - avg), 2));    // calculate standard deviation
                }
            }
            float sdv = (float)Math.Sqrt(value / (bmp.Height * bmp.Width));

            return sdv;
        }

        public static int[] GetHisto(Bitmap Bmp)        // calculate image histogram
        {
            int[] histo = new int[256];

            for (int i = 0; i < Bmp.Width; i++)
            {
                for (int j = 0; j < Bmp.Height; j++)
                {
                    int c = Bmp.GetPixel(i, j).R;

                    histo[c]++;
                }
            }
            return histo;
        }

        public static int[] EqualizeHisto(int[] histo, int imageWidth, int imageHeight)         //equalize image histogram
        {
            double[] frequency = new double[256];

            for (int i = 0; i < 256; i++)
            {
                frequency[i] = histo[i] / (double)(imageWidth * imageHeight);       // calculate pixel frequency
            }

            int[] equalizedhisto = new int[256];
            double sum;

            for (int i = 0; i < 256; i++)
            {
                sum = 0;
                for (int j = 0; j <= i; j++)
                {
                    sum += frequency[j];
                }
                equalizedhisto[i] = (int)Math.Floor(255 * sum);                     // calculate equalized probability
            }
            return equalizedhisto;
        }

    }
}
