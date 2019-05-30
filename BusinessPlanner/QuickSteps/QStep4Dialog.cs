using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessPlanner
{
    public partial class QStep4Dialog : Form
    {
        public Dictionary<string, Boolean> mData;
        public QStep4Dialog()
        {
            InitializeComponent();
            mData = new Dictionary<string, bool>();
            if (AppUtilities.mainData.ContainsKey("step4"))
            {
                Dictionary<string, Boolean> data = new Dictionary<string, bool>();
                data = (Dictionary<string, Boolean>)AppUtilities.mainData["step4"];
                checkBox1.Checked = data["swot"];
                checkBox2.Checked = data["web"];
            }
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            QStep3Dialog st5 = new QStep3Dialog();
            this.Hide();
            st5.ShowDialog();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.mData["swot"] = checkBox1.Checked;
            this.mData["web"] = checkBox2.Checked;
            AppUtilities.CreateOrUpdateDict("step4",this.mData);
            Debug.WriteLine(AppUtilities.mainData);
            QStep5Dialog st7 = new QStep5Dialog();
            this.Hide();
            st7.ShowDialog();
        }
    }
}
