using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    class ResizeCreator : IFilterCreator  // spatial operation transformation creators
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new Resize(parser.Constant, parser.Choice, input);
        }
    }

    class RotateCreator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new Rotate();
        }
    }

    class MirrorCreator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new Mirror();
        }
    }
}
