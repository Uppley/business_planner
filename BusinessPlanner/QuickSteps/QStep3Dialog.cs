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
    public partial class QStep3Dialog : Form
    {
        public String mData { get; set; }
        public QStep3Dialog()
        {
            InitializeComponent();
            if(AppUtilities.mainData.ContainsKey("step3"))
            {
                textBox1.Text = AppUtilities.mainData["step3"].ToString();
            }
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            QStep2Dialog st4 = new QStep2Dialog();
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
            AppUtilities.CreateOrUpdateDict("step3", this.mData);
            QStep4Dialog st6 = new QStep4Dialog();
            this.Hide();
            st6.ShowDialog();
        }
    }
}
