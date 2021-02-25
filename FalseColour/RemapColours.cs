using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ImageProcessingWF
{
    public class RemapColours
    {
        public List<Color> Points = new List<Color>();
    }

    class Thermometer : RemapColours
    {
        public Thermometer()
        {
            Points.Add(Color.FromArgb(0, 0, 128));
            Points.Add(Color.FromArgb(0, 255, 0));
            Points.Add(Color.FromArgb(255, 255, 0));
            Points.Add(Color.FromArgb(255, 128, 0));
            Points.Add(Color.FromArgb(255, 0, 0));
        }
    }

    class Earthy : RemapColours
    {
        public Earthy()
        {
            Points.Add(Color.FromArgb(140, 81, 10));
            Points.Add(Color.FromArgb(216, 179, 101));
            Points.Add(Color.FromArgb(246, 232, 194));
            Points.Add(Color.FromArgb(245, 245, 245));
            Points.Add(Color.FromArgb(199, 234, 229));
            Points.Add(Color.FromArgb(90, 180, 172));
            Points.Add(Color.FromArgb(1, 102, 94));
        }
    }

    class Summer : RemapColours
    {
        public Summer()
        {
            Points.Add(Color.FromArgb(166, 206, 227));
            Points.Add(Color.FromArgb(31, 120, 180));
            Points.Add(Color.FromArgb(178, 223, 138));
            Points.Add(Color.FromArgb(51, 160, 44));
            Points.Add(Color.FromArgb(251, 154, 153));
            Points.Add(Color.FromArgb(227, 26, 28));
            Points.Add(Color.FromArgb(253, 191, 111));
            Points.Add(Color.FromArgb(225, 127, 0));
            Points.Add(Color.FromArgb(202, 178, 214));
            Points.Add(Color.FromArgb(202, 178, 154));
        }
    }

    class Rouge : RemapColours
    {
        public Rouge()
        {
            Points.Add(Color.FromArgb(103, 0, 31));
            Points.Add(Color.FromArgb(214, 96, 77));
            Points.Add(Color.FromArgb(247, 247, 247));
            Points.Add(Color.FromArgb(67, 147, 195));
            Points.Add(Color.FromArgb(5, 48, 97));
        }
    }

    /*
    public static void SetPalette5(List<Color> Points) //pastels
    {
        Points.Clear();
        Points.Add(Color.FromArgb(179,226,205));
        Points.Add(Color.FromArgb(252,203,172));
        Points.Add(Color.FromArgb(203,213,232));
        Points.Add(Color.FromArgb(244,202,228));
        Points.Add(Color.FromArgb(230,245,201));
        Points.Add(Color.FromArgb(255,242,174));
        Points.Add(Color.FromArgb(241,226,204));
        Points.Add(Color.FromArgb(204,204,204));
    }
    */

    class Pastels : RemapColours
    {
        public Pastels()
        {
            Points.Add(Color.FromArgb(2, 23, 54));
            Points.Add(Color.FromArgb(0, 70, 106));
            Points.Add(Color.FromArgb(57, 145, 165));
            Points.Add(Color.FromArgb(61, 211, 200));
            Points.Add(Color.FromArgb(180, 229, 200));
            Points.Add(Color.FromArgb(228, 175, 195));
            Points.Add(Color.FromArgb(139, 86, 128));
            Points.Add(Color.FromArgb(131, 61, 85));
            Points.Add(Color.FromArgb(100, 35, 78));
            Points.Add(Color.FromArgb(52, 27, 57));
        }
    }

    class CustomPalette : RemapColours  //adapter : convert DataTable to RemapColours
    {
        public CustomPalette(DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Points.Add(Color.FromArgb((byte)table.Rows[i][0], (byte)table.Rows[i][1], (byte)table.Rows[i][2]));
            }
        }
    }
}


