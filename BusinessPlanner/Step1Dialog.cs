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
    public partial class Step1Dialog : Form
    {
        
        private String mData { get; set; }

        public Step1Dialog()
        {
            InitializeComponent();
            if(AppUtilities.mainData.ContainsKey("step1"))
            {
                var cb = this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Text == AppUtilities.mainData["step1"].ToString());
                cb.Checked = true;
            }
        }


        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {

            var cb = this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            this.mData = cb.Text;
            AppUtilities.CreateOrUpdateDict("step1", this.mData);
            if(this.mData== "Quick Plan")
            {
                QStep2Dialog qst2 = new QStep2Dialog();
                this.Hide();
                qst2.ShowDialog();
            }else if(this.mData == "Standard Plan")
            {
                SStep2Dialog st2 = new SStep2Dialog();
                this.Hide();
                st2.ShowDialog();
            }
            else if(this.mData == "Financial Plan")
            {
                FStep2Dialog fst2 = new FStep2Dialog();
                this.Hide();
                fst2.ShowDialog();
            }
            
        }

        
    }
}
