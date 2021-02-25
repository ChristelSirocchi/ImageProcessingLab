using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    class Mirror : IFilter
    {
        public Bitmap ApplyFilter(Bitmap input) // apply mirror transformation
        {
            Bitmap output = (Bitmap)input.Clone();
            output.RotateFlip(RotateFlipType.Rotate90FlipNone);
            return output;
        }
    }
}
