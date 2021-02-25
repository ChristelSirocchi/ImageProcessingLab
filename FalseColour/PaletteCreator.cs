using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    class Palette1Creator : IFilterCreator          //palette creators
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new Palette(new Thermometer());
        }
    }

    class Palette2Creator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new Palette(new Earthy());
        }
    }


    class Palette3Creator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new Palette(new Summer());
        }
    }


    class Palette4Creator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new Palette(new Rouge());
        }
    }


    class Palette5Creator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new Palette(new Pastels());
        }
    }

    class CustomPaletteCreator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new Palette((new CustomPalette(parser.Table)));
        }
    }

}
