using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingWF
{
    public class Range
    {
        private readonly int upper;
        private readonly int lower;

        public int Upper { get => upper; }
        public int Lower { get => lower; }

        public Range(int lower, int upper)
        {
            this.lower = lower;
            this.upper = upper;
        }
    }
}
