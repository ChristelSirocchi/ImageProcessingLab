using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    class CompositeOrderStatsFilter : IOrderStat
    {
        private IList<IOrderStat> filterList = new List<IOrderStat>();  // filter list
        public void Add(IOrderStat filter) => filterList.Add(filter);   // add filter
        public void Remove(IOrderStat filter) => filterList.Remove(filter);     // remove filter
        public IEnumerable<IOrderStat> GetFilterList() => filterList;           // get all filters

        public IOrderStat GetFilter(int index)                                  // get specific filter
        {
            if (index < filterList.Count)
            {
                return filterList[index];
            }
            return null;
        }

        public Bitmap ApplyFilter(Bitmap image)             // apply all filters one by one
        {
            Bitmap output = image;

            foreach (var c in GetFilterList())
            {
                output = c.ApplyFilter(output);
            }
            return output;
        }
    }

    class MinMaxFilter : CompositeOrderStatsFilter // Opening Filter
    {
        public MinMaxFilter(int kernelsize)
        {
            Add(new MinFilter(kernelsize));
            Add(new MaxFilter(kernelsize));
        }
    }

    class MaxMinFilter : CompositeOrderStatsFilter // Closing Filter
    {
        public MaxMinFilter(int kernelsize)
        {
            Add(new MaxFilter(kernelsize));
            Add(new MinFilter(kernelsize));
        }
    }
}
