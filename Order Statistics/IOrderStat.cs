using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    interface IOrderStat : IFilter
    {
        new Bitmap ApplyFilter(Bitmap image); //Apply Transformation
        void Add(IOrderStat filter); //Add
        void Remove(IOrderStat filter); //Remove
        IOrderStat GetFilter(int index); //Get one child
        IEnumerable<IOrderStat> GetFilterList(); //Get all filters
    }
}
