using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    class CompositeFilter : IFilter
    {
        public ConvFilter FilterX { get; set; } // filter horizontal direction

        public ConvFilter FilterY { get; set; } // filter vertical direction

        public CompositeFilter(ConvFilter filterX)  // constructor
        {
            FilterX = filterX;
            FilterY = filterX;
            FilterY.Kernel = ConvMath.GetSimmetricSquareMatrix(filterX.Kernel, filterX.KernelWidth);
        }
            
        public Bitmap ApplyFilter(Bitmap input)     // apply transformation
        {
            Bitmap output = new Bitmap(input.Width, input.Height);

            int offsetX = (int)(FilterX.KernelWidth / 2);
            int offsetY = (int)(FilterX.KernelHeight / 2);

            int xmin = offsetX;
            int xmax = input.Width - offsetX - 1;

            int ymin = offsetY;
            int ymax = input.Height - offsetY - 1;

            int op;
            int np, np1, np2;
            double v1, v2;

            for (int x = xmin; x <= xmax; x++)
            {
                for (int y = ymin; y <= ymax; y++)
                {
                    v1 = v2 = 0;
                    for (int c = 0; c < FilterX.KernelWidth; c++)
                    {
                        for (int r = 0; r < FilterX.KernelHeight; r++)
                        {
                            op = input.GetPixel(x - offsetX + c, y - offsetY + r).R;
                            v1 += op * FilterX.Kernel[c, r];

                            v2 += op * FilterY.Kernel[c, r];
                        }
                    }
                    np1 = (int)(FilterX.Offset + v1 / FilterX.Weight);

                    np2 = (int)(FilterY.Offset + v2 / FilterY.Weight);

                    np = MathF.Distance(np1, np2);  // apply distance operation to the output of the two filters
                    np = MathF.CheckRange(np);

                    output.SetPixel(x, y, Color.FromArgb(np, np, np));
                }
            }
            return output;
        }
    }
}
