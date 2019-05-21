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
    public partial class Step4Dialog : Form
    {
        public String mData { get; set; }
        public Step4Dialog()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
            if (AppUtilities.mainData.ContainsKey("step4"))
            {
                string[] data  = AppUtilities.mainData["step4"].ToString().Split(',');
                comboBox1.SelectedItem = data[0];
                comboBox2.SelectedItem = data[1];
            }
        }

       

        private void Button2_Click(object sender, EventArgs e)
        {
            Step3Dialog st3 = new Step3Dialog();
            this.Hide();
            st3.ShowDialog();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            String month = comboBox1.SelectedItem.ToString();
            String year = comboBox2.SelectedItem.ToString();
            this.mData = year+"-"+month + '-' +"01";
            AppUtilities.CreateOrUpdateDict("step4", this.mData);
            Step5Dialog st5 = new Step5Dialog();
            this.Hide();
            st5.ShowDialog();
        }
    }
}
