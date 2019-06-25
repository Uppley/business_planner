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
    public partial class FStep3Dialog : Form
    {
        public String mData { get; set; }
        
        public FStep3Dialog()
        {
            InitializeComponent();
            if (AppUtilities.mainData.ContainsKey("step3"))
            {
                var cb = this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Text == AppUtilities.mainData["step3"].ToString());
                cb.Checked = true;
            }
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            FStep2Dialog st2 = new FStep2Dialog();
            this.Hide();
            st2.ShowDialog();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            FStep4Dialog st4 = new FStep4Dialog();
            var cb = this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            this.mData = cb.Text;
            AppUtilities.CreateOrUpdateDict("step3", this.mData);
            this.Hide();
            st4.ShowDialog();
        }

        private void FStep3Dialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
