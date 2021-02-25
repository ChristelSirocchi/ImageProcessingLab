using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    class Sobel : ConvFilter
    {
        public Sobel()  // edge detection filter (type 1)
        {
            Weight = 1;
            Offset = 0;
             Kernel = new double[,]
                {
                    {-1, 0, 1},
                    {-2, 0, 2},
                    {-1, 0, 1},
                };

        }
    }

    class Prewitt : ConvFilter
    {
        public Prewitt()    // edge detection filter (type 2)
        {
            Weight = 1;
            Offset = 0;
            Kernel = new double[,]
            {
                  {1, 0, -1},
                  {1, 0, -1},
                  {1, 0, -1},
            };
        }
    }
}