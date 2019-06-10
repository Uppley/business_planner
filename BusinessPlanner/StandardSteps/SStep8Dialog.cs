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
    public partial class SStep8Dialog : Form
    {
        private String mData { get; set; }
        
        public SStep8Dialog()
        {
            InitializeComponent();
           
            if (AppUtilities.mainData.ContainsKey("step8"))
            {
                var cb = this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Text == AppUtilities.mainData["step8"].ToString());
                cb.Checked = true;
            }
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            SStep7Dialog st7 = new SStep7Dialog();
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
            AppUtilities.CreateOrUpdateDict("step8", this.mData);
            LoadingSpinner ls = new LoadingSpinner(this,AppMessages.messages["document_created"]);
            
            try
            {
                ls.show();
                DocumentCreator dc = new DocumentCreator();
                string proPath = dc.createStandardPackage();
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
