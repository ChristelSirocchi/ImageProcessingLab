using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    abstract class SimpleOrderStatsFilter : IOrderStat
    {
        private IList<IOrderStat> filterList = new List<IOrderStat>(); // filter list
        public int KernelSize { get; set; }     // kernel size
        public void Add(IOrderStat gift) { }    // add filter
        public void Remove(IOrderStat gift) { } // remove filter
        public IOrderStat GetFilter(int index) => null; // get filter
        public IEnumerable<IOrderStat> GetFilterList() => filterList; // get all filters

        public abstract int SelectPixel(int[] array);

        public Bitmap ApplyFilter(Bitmap image) // apply transformation
        {
            Bitmap output = new Bitmap(image.Width, image.Height);

            int offsetX = (KernelSize - 1) / 2;
            int offsetY = (KernelSize - 1) / 2;

            int xmin = offsetX;
            int xmax = image.Width - offsetX - 1;

            int ymin = offsetY;
            int ymax = image.Height - offsetY - 1;

            int[] op = new int[KernelSize * KernelSize];
            int i;
            int np;

            for (int x = xmin; x <= xmax; x++)
            {
                for (int y = ymin; y <= ymax; y++)
                {
                    i = 0;
                    for (int c = 0; c < KernelSize; c++)
                    {
                        for (int r = 0; r < KernelSize; r++)
                        {
                            op[i] = image.GetPixel(x - offsetX + c, y - offsetY + r).R;
                            i++;
                        }
                    }
                    np = SelectPixel(op);
                    MathF.CheckRange(np);
                    output.SetPixel(x, y, Color.FromArgb(np, np, np));
                }
            }
            return output;
        }

    }

    class MedianFilter : SimpleOrderStatsFilter  // median filter
    {
        public MedianFilter(int kernelsize)
        {
            this.KernelSize = kernelsize;
        }
        public override int SelectPixel(int[] array)
        {
            Array.Sort(array);
            return array[KernelSize / 2 + 1];
        }
    }

    class MaxFilter : SimpleOrderStatsFilter      // dilation filter
    {
        public MaxFilter(int kernelsize)
        {
            this.KernelSize = kernelsize;
        }
        public override int SelectPixel(int[] array)
        {
            return array.Max();
        }
    }

    class MinFilter : SimpleOrderStatsFilter        // erosion filter
    {
        public MinFilter(int kernelsize)
        {
            this.KernelSize = kernelsize;
        }
        public override int SelectPixel(int[] array)
        {
            return array.Min();
        }
    }
}
