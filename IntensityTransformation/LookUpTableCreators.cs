using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    class NegativeLUTCreator : IFilterCreator       //intensity transformation filter creators
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new Negative();
        }
    }

    class ContrastStretchingLUTCreator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new ConstrastStretching(input.ImageStats.Min, input.ImageStats.Max);
        }
    }

    class LogarithmicLUTCreator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new Logarithmic();
        }
    }

    class EqualizedHistoLUTCreator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new HistoEq(ImageStats.EqualizeHisto(input.ImageStats.Histo, input.Bmp.Width, input.Bmp.Height));
        }
    }

    class ThresholdingLUTCreator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new Thresholding((byte)parser.Intensity, 0, 255);
        }
    }

    class SlicingLUTCreator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new Slicing((byte)parser.Intensity, (byte)parser.Range.Lower, (byte)parser.Range.Upper, parser.Choice, 0);
        }
    }

    class GammaLUTCreator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input)
        {
            return new Gamma(parser.Constant);
        }
    }

    class PiecewiseLUTCreator : IFilterCreator
    {
        public IFilter FactoryMethod(ParseManager parser, ImageFile input) 
        {
            return new CustomIntensity(parser.Table, parser.Choice);
        }

    }
}
