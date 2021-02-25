using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    class Laplacian1 : ConvFilter
    {
        public Laplacian1(double c)  // laplacian filter (type 1)
        {
            Weight = 1;
            Offset = 0;
            Kernel = new double[,]
            {
                {-c, -c, -c},
                {-c, 8*c + 1, -c},
                {-c, -c, -c},
            };            
        }
    }

    class Laplacian2 : ConvFilter
    {
        public Laplacian2(double c) // laplacian filter (type 2)
        {
            Weight = 1;
            Offset = 0;
            Kernel = new double[,]
            {
                  {0, -c, 0},
                  {-c, 4*c + 1, -c},
                  {0, -c, 0},
            };
        }        
    }
}
