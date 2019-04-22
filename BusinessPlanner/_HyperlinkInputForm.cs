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
    public partial class _HyperlinkInputForm : Form
    {
        public string linkText { get; set; }
        public string linkUrl { get; set; }

        public _HyperlinkInputForm()
        {
            InitializeComponent();
        }



        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.linkText = textBox1.Text;
            this.linkUrl = textBox2.Text;
            if (this.linkText=="" || this.linkUrl =="")
                MessageBox.Show("Please enter correct value !");
        }
    }
}
