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
    public partial class SStep5Dialog : Form
    {
        public String mData { get; set; }
        public SStep5Dialog()
        {
            InitializeComponent();
            if(AppUtilities.mainData.ContainsKey("step5"))
            {
                textBox1.Text = AppUtilities.mainData["step5"].ToString();
            }
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            SStep4Dialog st4 = new SStep4Dialog();
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
            if(this.mData=="")
            {
                label5.Text = "This field is required !";
            }
            else
            {
                label5.Text = "";
                AppUtilities.CreateOrUpdateDict("step5", this.mData);
                SStep6Dialog st6 = new SStep6Dialog();
                this.Hide();
                st6.ShowDialog();
            }
            
        }

       

        private void SStep5Dialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
