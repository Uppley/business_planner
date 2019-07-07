using BusinessPlanner.Utility;
using Newtonsoft.Json;
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
                savefornew();
                parentForm.Dispose();
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

        private void savefornew()
        {
            LoadingSpinner ls = new LoadingSpinner(this, AppMessages.messages["project_save"]);
            try
            {
                ls.show();
                saveProgress();
                string dPath = ProjectConfig.projectPath.Replace("~temp_", "");
                ProjectLoader.save(dPath, ProjectConfig.projectPath);
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message);
            }
            finally
            {
                ls.hide();
            }
        }

        private void saveProgress()
        {
            BPSettings bp = new BPSettings();
            var set_path = ProjectConfig.projectPath + "//" + "settings.json";
            try
            {
                var se = bp.ReadSettings(set_path);
                var i = se.FindIndex(x => x.property == "progress");
                se[i].value = ProjectConfig.projectSettings["progress"];
                using (StreamWriter file = File.CreateText(set_path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, se);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message);
            }

        }
    }
}
