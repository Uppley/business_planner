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
    public partial class Step8Dialog : Form
    {
        public String[] mainData;
        public Step8Dialog()
        {
            InitializeComponent();
       
        }

        public void setFormData(String[] data)
        {
            this.mainData = data;
         
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Step7Dialog st7 = new Step7Dialog();
            this.Hide();
            st7.ShowDialog();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Step9Dialog st9 = new Step9Dialog();
            this.Hide();
            st9.ShowDialog();
        }
    }
}
