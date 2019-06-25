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
    public partial class QStep2Dialog : Form
    {
        public String mData { get; set; }
        
        public QStep2Dialog()
        {
            InitializeComponent();
            if (AppUtilities.mainData.ContainsKey("step2"))
            {
                var cb = this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Text == AppUtilities.mainData["step2"].ToString());
                cb.Checked = true;
            }
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            Step1Dialog st2 = new Step1Dialog();
            this.Hide();
            st2.ShowDialog();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            QStep3Dialog st4 = new QStep3Dialog();
            var cb = this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            this.mData = cb.Text;
            AppUtilities.CreateOrUpdateDict("step2", this.mData);
            this.Hide();
            st4.ShowDialog();
        }

        private void QStep2Dialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
