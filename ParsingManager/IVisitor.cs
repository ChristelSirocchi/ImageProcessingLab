using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    public interface IVisitor
    {
        void Visit(IParse parser); // visit parser
    }
}
