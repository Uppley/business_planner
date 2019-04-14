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
    public partial class Step6Dialog : Form
    {
        public String mData { get; set; }
        public Step6Dialog()
        {
            InitializeComponent();
       
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            Step5Dialog st5 = new Step5Dialog();
            this.Hide();
            st5.ShowDialog();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            
            
            Step7Dialog st7 = new Step7Dialog();
            this.Hide();
            st7.ShowDialog();
        }
    }
}
