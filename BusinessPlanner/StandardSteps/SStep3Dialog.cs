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
    public partial class SStep3Dialog : Form
    {
        public String mData { get; set; }
        
        public SStep3Dialog()
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
            SStep2Dialog st2 = new SStep2Dialog();
            this.Hide();
            st2.ShowDialog();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            SStep4Dialog st4 = new SStep4Dialog();
            var cb = this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            this.mData = cb.Text;
            AppUtilities.CreateOrUpdateDict("step3", this.mData);
            this.Hide();
            st4.ShowDialog();
        }

        private void SStep3Dialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Close();
            System.Windows.Forms.Application.Exit();
        }

        private void SStep3Dialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
