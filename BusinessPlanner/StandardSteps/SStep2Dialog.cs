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
    public partial class SStep2Dialog : Form
    {
        public Int32 mData { get; set; }

        public SStep2Dialog()
        {
            InitializeComponent();
            this.comboBox1.SelectedIndex = 0;
            if (AppUtilities.mainData.ContainsKey("step2"))
            {
                this.comboBox1.SelectedIndex = (Int32) AppUtilities.mainData["step2"];
            }
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            Step1Dialog st1 = new Step1Dialog();
            this.Hide();
            st1.ShowDialog();
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            SStep3Dialog st3 = new SStep3Dialog();
            this.mData = this.comboBox1.SelectedIndex;
            AppUtilities.CreateOrUpdateDict("step2", this.mData);
            this.Hide();
            st3.ShowDialog();
        }

        

        private void SStep2Dialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
