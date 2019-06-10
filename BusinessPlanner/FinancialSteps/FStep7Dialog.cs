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
    public partial class FStep7Dialog : Form
    {
        private String mData { get; set; }
        
        public FStep7Dialog()
        {
            InitializeComponent();
           
            if (AppUtilities.mainData.ContainsKey("step7"))
            {
                var cb = this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Text == AppUtilities.mainData["step7"].ToString());
                cb.Checked = true;
            }
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            FStep6Dialog st7 = new FStep6Dialog();
            this.Hide();
            st7.ShowDialog();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var cb = this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            this.mData = cb.Text;
            AppUtilities.CreateOrUpdateDict("step7", this.mData);
            LoadingSpinner ls = new LoadingSpinner(this,AppMessages.messages["document_created"]);
            
            try
            {
                ls.show();
                DocumentCreator dc = new DocumentCreator();
                string proPath = dc.createFinancialPackage();
                string tempPath = Path.Combine(ProjectConfig.projectBase,"~temp_"+AppUtilities.mainData["step5"].ToString());
                ProjectLoader.load(proPath,tempPath);
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
    }
}
