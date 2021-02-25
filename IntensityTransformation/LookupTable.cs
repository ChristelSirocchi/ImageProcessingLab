using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms.DataVisualization.Charting;

namespace ImageProcessingWF
{
    class LookupTable : IFilter
    {
        private readonly byte[] table = new byte[256];  // look up table for intensity remapping
                                                       
        protected const byte maxIntensity = 255;        // maximum intensity value
        protected const byte minIntensity = 0;          // minimum intensity value
        
  
        public byte[] Table { get => table; }  
        public string Name { get; set; }

        public void Print() // print all values to standard output (debugging)
        {
            for (int i = 0; i < Table.Length; i++)
            {
                Console.WriteLine("{0}\n", Table[i]);
            }
        }

        public Bitmap ApplyFilter(Bitmap image)  //intensity tranformation function
        {
            int width = image.Width;
            int height = image.Height;
            BitmapData srcData = image.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb); //Lock bitmap bits to system memory
            int bytes = srcData.Stride * srcData.Height;
            byte[] buffer = new byte[bytes];                //Declare array in which intensity values will be stored
            byte[] result = new byte[bytes];
            Marshal.Copy(srcData.Scan0, buffer, 0, bytes);  //Copy intensity values in array

            image.UnlockBits(srcData);
            int current;
            int cChannels = 3;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    current = y * srcData.Stride + x * 4;
                    for (int i = 0; i < cChannels; i++)
                    {

                        result[current + i] = Table[buffer[current + i]]; //Calculate new intensity values
                    }
                    result[current + 3] = 255;
                }
            }
            Bitmap output = new Bitmap(width, height);

            BitmapData outdata = output.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(result, 0, outdata.Scan0, bytes);      //Copy changed intensity values back to the bitmap

            output.UnlockBits(outdata);                         //Unlock bits from system memory

            return output;
        
        }

        public void DrawGraph(Chart chart)              //draw intensity tranformation graph
        {
            chart.Series["Series1"].Points.Clear();
            chart.Titles.Clear();
            int[] xAxis = new int[Table.Length];
            int count = 0;

            for (int i = 0; i < Table.Length; i++)
            {
                xAxis[i] = count;
                count++;

                chart.Series["Series1"].Points.AddXY(xAxis[i], Table[i]);
            }
            chart.Titles.Add(Name);
            chart.Series["Series1"].ChartType = SeriesChartType.FastLine;
            chart.Series["Series1"].Color = Color.Red;
            chart.ChartAreas[0].AxisX.Maximum = 256;
            chart.ChartAreas[0].AxisX.Minimum = 0;
            chart.ChartAreas[0].AxisY.Maximum = 256;
            chart.ChartAreas[0].AxisY.Minimum = 0;
            chart.ChartAreas[0].AxisX.Interval = 50;
            chart.ChartAreas[0].AxisY.Interval = 50;
            chart.Visible = true;
        }
    }


    /*GetPixel Set Pixel method (slow)
    public static Bitmap Intensity(Bitmap image, LookupTable table)
    {
        Bitmap output = new Bitmap(image.Width, image.Height);

        for (int i = 0; i < image.Width; i++)
        {
            for (int j = 0; j < image.Height; j++)
            {
                int c = image.GetPixel(i, j).R;

                c = table.Table[c];

                output.SetPixel(i, j, Color.FromArgb(c, c, c));
            }
        }
        return output;
    }
    */
}

