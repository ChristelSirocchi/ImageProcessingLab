using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    class Embossing1 : ConvFilter
    {
        public Embossing1() // embossing filter (type 1)
        {
            Weight = 1;
            Offset = 127;
            Kernel = new double[,]
                {
                    {-1, 0, 0},
                    {0, 0, 0},
                    {0, 0, 1},
                };
        }

        
    }

    class Embossing2 : ConvFilter
    {
        public Embossing2() // embossing filter (type 2)
        {

            Weight = 1;
            Offset = 127;
            Kernel = new double[,]
            {
                  {-1, -1, 0},
                  {-1, 0, 1},
                  {0, 1, 1},

            };

        }
    }

    class EmbossingIntense : ConvFilter
    {
        public EmbossingIntense()   // embossing filter (type 3)
        {
            Weight = 1;
            Offset = 127;
            Kernel = new double[,]
            {
                 { -1, -1, -1, -1,  0, },
                 { -1, -1, -1,  0,  1, },
                 { -1, -1,  0,  1,  1, },
                 { -1,  0,  1,  1,  1, },
                 {  0,  1,  1,  1,  1, },
            };


        }
    }
}
