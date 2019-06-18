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
    public partial class FStep6Dialog : Form
    {
        private String mData { get; set; }

        public FStep6Dialog()
        {
            InitializeComponent();
            if (AppUtilities.mainData.ContainsKey("sellType"))
            {
                var cb = this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Text == AppUtilities.mainData["sellType"].ToString());
                cb.Checked = true;
            }
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            FStep5Dialog st6 = new FStep5Dialog();
            this.Hide();
            st6.ShowDialog();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var cb = this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            this.mData = cb.Text;
            AppUtilities.CreateOrUpdateDict("sellType", this.mData);
            FStep7Dialog st8 = new FStep7Dialog();
            this.Hide();
            st8.ShowDialog();
        }
    }
}
