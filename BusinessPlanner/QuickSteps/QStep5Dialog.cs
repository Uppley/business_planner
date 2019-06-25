using BusinessPlanner.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessPlanner
{
    public partial class QStep5Dialog : Form
    {
        private String mData { get; set; }
        int t = 0;
        public QStep5Dialog()
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
            QStep4Dialog st6 = new QStep4Dialog();
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
            LoadingSpinner ls = new LoadingSpinner(this,AppMessages.messages["document_created"]);
            //this.Close();
            try
            {
                ls.show();
                DocumentCreator dc = new DocumentCreator();
                string proPath = dc.createQuickPackage();
                string tempPath = Path.Combine(ProjectConfig.projectBase, "~temp_" + AppUtilities.mainData["step3"].ToString());
                ProjectLoader.load(proPath, tempPath);
                t = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            finally
            {
                ls.hide();
                this.Close();
                MainWindow mf = new MainWindow();
                mf.Show();
            }
        }

        private void QStep5Dialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(t==0)
                System.Windows.Forms.Application.Exit();
        }
    }
}
