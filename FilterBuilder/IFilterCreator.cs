using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    interface IFilterCreator
    {
        IFilter FactoryMethod(ParseManager parser, ImageFile input);        //factory method
    }
}
