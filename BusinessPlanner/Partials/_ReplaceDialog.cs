using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessPlanner.Partials
{
    public partial class _ReplaceDialog : Form
    {
        public string searchTerm { get; set; }
        public string newTerm { get; set; }
        public _ReplaceDialog()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.searchTerm = textBox1.Text;
            this.newTerm = textBox2.Text;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
