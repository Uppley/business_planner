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
    public partial class Step7Dialog : Form
    {
        private String mData { get; set; }

        public Step7Dialog()
        {
            InitializeComponent();
            if (Utilities.mainData.ContainsKey("step7"))
            {
                var cb = this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Text == Utilities.mainData["step7"].ToString());
                cb.Checked = true;
            }
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            Step6Dialog st6 = new Step6Dialog();
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
            Utilities.CreateOrUpdateDict("step7", this.mData);
            Step8Dialog st8 = new Step8Dialog();
            this.Hide();
            st8.ShowDialog();
        }
    }
}
