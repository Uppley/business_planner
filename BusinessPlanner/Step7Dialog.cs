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
    public partial class Step7Dialog : Form
    {
        public String[] mainData;
        public Step7Dialog()
        {
            InitializeComponent();
       
        }

        public void setFormData(String[] data)
        {
            this.mainData = data;
         
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Step6Dialog st6 = new Step6Dialog();
            this.Hide();
            st6.ShowDialog();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Step8Dialog st8 = new Step8Dialog();
            this.Hide();
            st8.ShowDialog();
        }
    }
}
