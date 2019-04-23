using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessPlanner
{
    public partial class _TableInputForm : Form
    {
        public decimal rows { get; set; }
        public decimal columns { get; set; }

        public decimal col_width { get; set; }

        public _TableInputForm()
        {
            InitializeComponent();
        }



        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.rows = numericUpDown1.Value;
            this.columns = numericUpDown2.Value;
            if (numericUpDown3.Value > 0 && numericUpDown3.Value < 11)
                this.col_width = numericUpDown3.Value;
            else
                MessageBox.Show("Please enter correct value !");
        }
    }
}
