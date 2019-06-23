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
    public partial class Home : Form
    {
        public Form parentForm { get; set; }
        public Home()
        {
            InitializeComponent();
        }


        private void Label2_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(AppMessages.messages["new_body"], AppMessages.messages["new_head"], MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                parentForm.Close();
                if (parentForm.IsDisposed)
                {
                    Step1Dialog st = new Step1Dialog();
                    st.ShowDialog();
                }

            }
            else
            {
                return;
            }
        }

        private void Label9_Click(object sender, EventArgs e)
        {
            ReviewWindow rew = new ReviewWindow();
            rew.Show();
        }

        private void Label11_Click(object sender, EventArgs e)
        {
            ResourceWindow rsw = new ResourceWindow();
            rsw.Show();
        }
    }
}
