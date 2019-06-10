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
    public partial class FStep5Dialog : Form
    {
        public String mData { get; set; }
        public FStep5Dialog()
        {
            InitializeComponent();
            if(AppUtilities.mainData.ContainsKey("step5"))
            {
                textBox1.Text = AppUtilities.mainData["step5"].ToString();
            }
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            FStep4Dialog st4 = new FStep4Dialog();
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
            if (this.mData == "")
            {
                label5.Text = "This field is required !";
            }
            else
            {
                label5.Text = "";
                AppUtilities.CreateOrUpdateDict("step5", this.mData);
                FStep6Dialog st6 = new FStep6Dialog();
                this.Hide();
                st6.ShowDialog();
            }
        }
    }
}
