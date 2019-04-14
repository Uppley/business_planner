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
    public partial class Step2Dialog : Form
    {
        public Int32 mData { get; set; }

        public Step2Dialog()
        {
            InitializeComponent();
            this.comboBox1.SelectedIndex = 0;
            if (Utilities.mainData.ContainsKey("step2"))
            {
                this.comboBox1.SelectedIndex = (Int32) Utilities.mainData["step2"];
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
            Step3Dialog st3 = new Step3Dialog();
            this.mData = this.comboBox1.SelectedIndex;
            Utilities.CreateOrUpdateDict("step2", this.mData);
            this.Hide();
            st3.ShowDialog();
        }
    }
}
