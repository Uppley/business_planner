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
    public partial class Step5Dialog : Form
    {
        public String mData { get; set; }
        public Step5Dialog()
        {
            InitializeComponent();
            if(AppUtilities.mainData.ContainsKey("step5"))
            {
                textBox1.Text = AppUtilities.mainData["step5"].ToString();
            }
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            Step4Dialog st4 = new Step4Dialog();
            this.Hide();
            st4.ShowDialog();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.mData = textBox1.Text;
            AppUtilities.CreateOrUpdateDict("step5", this.mData);
            Step6Dialog st6 = new Step6Dialog();
            this.Hide();
            st6.ShowDialog();
        }
    }
}
