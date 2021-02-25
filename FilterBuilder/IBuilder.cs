using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    interface IBuilder
    {
        void BuildSpatialOpList();

        void BuildConvList();

        void BuildOrderStatList();

        void BuildLookUpTableList();

        void BuildPaletteList();

        void Reset();
    }
}
