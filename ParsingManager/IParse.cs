using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    public interface IParse
    {
        bool Parse(); // parse paramenters

        string GetError(); // get error message
        
        void Accept(IVisitor visitor);  // accept visit
    }
}
