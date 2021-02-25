using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    class Resize : IFilter
    {
        private double constant;
        private int width;
        private int height;
        private bool choice;

        public Resize(double constant, bool choice, ImageFile input)    // constructor
        {
            this.constant = constant;
            this.choice = choice;
            this.width = MathF.NewSize(input.Bmp.Width, constant);
            this.height = MathF.NewSize(input.Bmp.Height, constant);
        }

        public Bitmap ApplyFilter(Bitmap input)                         // apply resizing
        {
            Bitmap output = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(output))
            {
                if (choice)
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
                }
                else
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
                }
                g.DrawImage(input, 0, 0, width, height);
            }
            return output;
        }
    }
}
