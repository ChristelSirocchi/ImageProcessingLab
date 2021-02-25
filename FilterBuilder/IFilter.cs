using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    interface IFilter
    {
        Bitmap ApplyFilter(Bitmap image);       //apply transformation
    }
}
