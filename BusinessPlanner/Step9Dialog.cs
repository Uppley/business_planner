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
    public partial class Step9Dialog : Form
    {
        private String mData { get; set; }
        
        public Step9Dialog()
        {
            InitializeComponent();
           
            if (Utilities.mainData.ContainsKey("step9"))
            {
                var cb = this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Text == Utilities.mainData["step9"].ToString());
                cb.Checked = true;
            }
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            Step8Dialog st8 = new Step8Dialog();
            this.Hide();
            st8.ShowDialog();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var cb = this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            this.mData = cb.Text;
            Utilities.CreateOrUpdateDict("step9", this.mData);
            AppConfig.updateNodes(2,AppConfig.getId()+1,"Mission");
            //DocumentRecord.DocumentList.Find(x => x.ItemName == "Mission").IsActive = 1;
            //MainWindow mw = Utilities.mainForm as MainWindow;
            //mw.updateTreeNodes();
            _LoadingDialog ld = new _LoadingDialog(AppMessages.messages["document_created"]);
            this.Close();
            try
            {
                ld.Show();
                Application.DoEvents();
                DocumentCreator dc = new DocumentCreator();
                string proPath = dc.createPackage();
                string tempPath = Path.Combine(ProjectConfig.projectBase,"temp_"+Utilities.mainData["step5"].ToString());
                DocumentLoader.load(proPath,tempPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            finally
            {
                ld.Close();
                MainWindow mf = new MainWindow();
                mf.Show();
            }
           
            
        }
    }
}
