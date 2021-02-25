using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessingWF
{
    abstract class ParseDataTable : IParse
    {
        private string error = "Please select at least two custom points"; // error message
        private DataTable table = new DataTable();      // data table containing list of parsed points
        public abstract DataTable GetTable();           

        public abstract bool Parse();
    
        public string GetError() { return error; }  // get error message

        public void Accept(IVisitor visitor)        // accept visitor
        {
            visitor.Visit(this);
        }
        public abstract void InitializeDataTable(); 

        public abstract void AddPoint();

        public abstract void ClearPoints();

        public bool ParseString(int[] array, TextBox custom) // parse string from Textbox
        {
            bool parsing;

            string[] split = custom.Text.Split(';', '.', ',');  // split string

            int[] values = new int[split.Length];

            for (int i = 0, j = 0; i < split.Length; i++)
            {
                parsing = int.TryParse(split[i], out int value);    // parse values
                if (parsing == true)
                {
                    value = MathF.CheckRange(value);
                    values[j] = value;
                    j++;
                }
            }

            if (values.Length >= array.Length)  // if parsed values are the expected number, add to array
            {
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = values[i];
                }
                parsing = true;
            }
            else
            {
                parsing = false;        // if parsed number are less than expectes, error message
                MessageBox.Show("Please insert " + array.Length + " values separated by a comma");
            }
            return parsing;
        }
    }

    class ParseColourPoints : ParseDataTable
    {
        private DataTable table;
        private TextBox text;
        private DataGridView grid;
        public ParseColourPoints(TextBox text, DataGridView grid) // constructor
        {
            this.text = text;
            this.grid = grid;
            table = new DataTable();
        }
        public override bool Parse()
        { 
            return (table == null | table.Rows.Count >= 2);
        }
        public override void InitializeDataTable()  // initialize table
        {            
            table.Columns.Add("Red", typeof(byte));
            table.Columns.Add("Green", typeof(byte));
            table.Columns.Add("Blue", typeof(byte));
            grid.DataSource = table;
        }

        public override void AddPoint()             // add parsed point to table
        {
            int[] array = new int[3];
            if (ParseString(array, text))
            {
                table.Rows.Add(array[0], array[1], array[2]);
                text.Text = "";
                grid.DataSource = table;            // update table
            }
        }

        public override void ClearPoints()          // clear all points in table
        {
            table.Clear();
            grid.DataSource = table;
        }

        public override DataTable GetTable()        // get table
        {
            return table;
        }
    }

    class ParseIntensityPoints : ParseDataTable
    {
        private DataTable table;
        private TextBox text;
        private DataGridView grid;
        public ParseIntensityPoints(TextBox text, DataGridView grid) // constructor
        {
            this.text = text;
            this.grid = grid;
            table = new DataTable();
        }
        public override bool Parse()        
        {
            return (table == null | table.Rows.Count >= 2);
        }
        public override void InitializeDataTable()      // initialize table
        {          
            table.Columns.Add("Intensity IN", typeof(byte));
            table.Columns.Add("Intensity OUT", typeof(byte));
            grid.DataSource = table;

        }

        public override void AddPoint()     // add parsing point
        {
            int[] array = new int[2];
            if (ParseString(array, text))
            {
                table.Rows.Add(array[0], array[1]);
                text.Text = "";
                grid.DataSource = table;
            }
        }

        public override void ClearPoints()  // clear all points
        {
            table.Clear();
            grid.DataSource = table;
        }

        public override DataTable GetTable()    // get table
        {
            return table;
        }
    }
}
