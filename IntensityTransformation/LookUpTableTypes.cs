using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ImageProcessingWF
{

    class Negative : LookupTable            //negative filter
    {
        public Negative()
        {
            for (int i = 0; i < Table.Length; i++)
            {
                Table[i] = (byte)(maxIntensity - i);
            }
            Name = "negative filter";
        }
    }

    class Thresholding : LookupTable        // thresholding filter
    {
        public Thresholding(byte threshold, byte min, byte max)
        {
            for (int i = 0; i < Table.Length; i++)
            {
                Table[i] = i < threshold ? min : max;
            }
            Name = "threshold filter";
        }
    }

    class ConstrastStretching : LookupTable  // constrast stretching filter
    {
        public ConstrastStretching(byte minImage, byte maxImage)
        {
            for (int i = 0; i < Table.Length; i++)
            {
                Table[i] = (byte)(((i - minImage) * (maxIntensity - minIntensity) / (maxImage - minImage)) + minIntensity);
            }
            Name = "linear contrast stretching filter";
        }
    }

    class Logarithmic : LookupTable         // logarithmic filter
    {
        public Logarithmic()
        {
            double correction = maxIntensity / (Math.Log10(maxIntensity));

            for (int i = 0; i < Table.Length; i++)
            {
                Table[i] = (byte)Math.Floor(correction * Math.Log10(i + 1));
            }
            Name = "logarithmic filter";
        }
    }

    class Gamma : LookupTable                // gamma correction filter
    {
        public Gamma(double constant)
        {
            double correction = maxIntensity / Math.Pow(maxIntensity, constant);

            for (int i = 0; i < Table.Length; i++)
            {
                Table[i] = (byte)Math.Floor(correction * Math.Pow(i, constant));
            }
            Name = "gamma correction filter";
        }
    }

    class Slicing : LookupTable                 // slicing filter
    {
        public Slicing(byte intensityband, byte lower, byte upper, bool option, byte intensityback)
        {
            if (option == true) // preserve intensity values outside selected band
            {
                for (int i = 0; i < Table.Length; i++)
                {
                    Table[i] = (i < lower | i > upper) ? (byte)i : intensityband;
                }
            }
            else
            {
                for (int i = 0; i < Table.Length; i++)
                {
                    Table[i] = (i < lower | i > upper) ? intensityback : intensityband;
                }
            }
            Name = "slicing filter";
        }
    }

    class CustomIntensity : LookupTable     // custom filter
    {
        public CustomIntensity(DataTable table, bool choice)
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(0, 0));    // add first and last point to the list
            points.Add(new Point(255, 255));
            for (int i = 0; i < table.Rows.Count; i++)
            {
                points.Add(new Point((byte)table.Rows[i][0], (byte)table.Rows[i][1])); // convert list from DataTable type to look up table (Adapter)
            }
            points = points.OrderBy(order => order.X).ToList(); // sort list

            if (choice)
            {
                for (int i = 0; i < points.Count - 1; i++)      //piece-wise linear
                {
                    for (int j = points[i].X; j <= points[i + 1].X; j++)
                    {
                        Table[j] = (byte)MathF.LinearInterpolation(points[i], points[i + 1], j);
                    }
                }
                Name = "piecewise linear filter";
            }
            else
            {
                for (int i = 0; i < points.Count - 1; i++)      //steps
                {
                    for (int j = points[i].X; j < points[i + 1].X; j++)
                    {
                        Table[j] = (byte)points[i].Y;
                    }
                }
                Name = "steps filter";
            }
        }
    }

    class PiecewiseLinear : LookupTable
    {
        public PiecewiseLinear(List<Point> points)
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                for (int j = points[i].X; j <= points[i + 1].X; j++)
                {
                    Table[j] = (byte)MathF.LinearInterpolation(points[i], points[i + 1], j);
                }
            }
            Name = "piecewise linear filter";
        }
    }

    class Steps : LookupTable
    {
        public Steps(List<Point> points)
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                for (int j = points[i].X; j < points[i + 1].X; j++)
                {
                    Table[j] = (byte)points[i].Y;
                }
            }
            Name = "steps filter";
        }
    }


    class HistoEq : LookupTable // convert equalized histogram to look-up table (Adapter)
    {
        public HistoEq(int[] histo)
        {
            for (int i = 0; i < Table.Length; i++)
            {
                Table[i] = (byte)histo[i];
            }
            Name = "histogram filter";
        }
    }
}
