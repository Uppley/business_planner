using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessPlanner
{
    partial class AboutBP : Form
    {
        public AboutBP()
        {
            InitializeComponent();
        }

       

        private void OkButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
