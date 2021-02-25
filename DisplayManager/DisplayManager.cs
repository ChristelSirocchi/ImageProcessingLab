using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ImageProcessingWF
{
    class DisplayManager
    {
        private readonly PictureBox pic;    //PictureBox displaying the image
        private readonly PictureBox histo;  //PictureBox displaying the histogram
        private readonly TextBox max;       //TextBoxes displaying the statistics
        private readonly TextBox min;
        private readonly TextBox avg;
        private readonly TextBox sdv;
        private readonly GroupBox group;    //GroupBox containing all statistics
        private readonly Chart chart;       //Chart displaying the intensity transformation graph

        public DisplayManager(PictureBox pic, PictureBox histo, TextBox max, TextBox min, TextBox avg, TextBox sdv, GroupBox group) //constructor 
        {
            this.pic = pic;
            this.histo = histo;
            this.max = max;
            this.min = min;
            this.avg = avg;
            this.sdv = sdv;
            this.group = group;
        }

        public DisplayManager(Chart chart)  //constructor 
        {
            this.chart = chart;
        }
        public void DisplayImage(ImageFile image)   //diplay image, stats and histogram graph
        {
            pic.Image = image.Bmp.CopyToSquareCanvas(pic.Width);
            histo.Image = DrawHistogram(image.ImageStats.Histo, histo.Width, histo.Height);
            max.Text = image.ImageStats.Max.ToString();
            min.Text = image.ImageStats.Min.ToString();
            avg.Text = string.Format("{0:0.00}", image.ImageStats.Avg);
            sdv.Text = string.Format("{0:0.00}", image.ImageStats.Sdv);
            group.Visible = true;
        }

        public void DisplayGraph(IFilter filter, Transformation index) //diplay intensity transformation graph 
        {
            if (index == Transformation.intensity)
            {
                LookupTable table = (LookupTable)filter;
                table.DrawGraph(chart);
                chart.Visible = true;
            }
            else
            {
                chart.Visible = false;
            }
        }

        public static Bitmap DrawHistogram(int[] histo, int canvasWidth, int canvasHeight) //draw histogram graph
        {
            var bitmap = new Bitmap(canvasWidth, canvasHeight);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                for (int i = 0; i < histo.Length; i++)
                {
                    float unit = (float)histo[i] / histo.Max();

                    graphics.DrawLine(Pens.Black,
                        new Point(i, canvasHeight - 5),
                        new Point(i, canvasHeight - 5 - (int)(unit * canvasHeight)));
                }
            }
            return bitmap;
        }
    }

}
