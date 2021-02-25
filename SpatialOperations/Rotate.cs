using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    class Rotate : IFilter
    { 
        public Bitmap ApplyFilter(Bitmap input) // apply rotation
        {
            Bitmap output = (Bitmap)input.Clone();
            output.RotateFlip(RotateFlipType.RotateNoneFlipX);
            return output;
        }
    }
}
