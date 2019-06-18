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

namespace BusinessPlanner.Partials
{
    public partial class _CurrencyDialog : Form
    {
        public string oldName { get; set; }
        public string newName { get; set; }
        public _CurrencyDialog()
        {
            InitializeComponent();
            
        }

        private void ModifyBt_Click(object sender, EventArgs e)
        {
            BPSettings bps = new BPSettings();
            var set_path = ProjectConfig.projectPath + "//" + "settings.json";
            try
            {
                var se = bps.ReadSettings(set_path);
                var i = se.FindIndex(x => x.property == "Currency");
                se[i].value = currencyName.Text;
                using (StreamWriter file = File.CreateText(set_path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, se);
                }
                newName = currencyName.SelectedItem.ToString();

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void CancelBt_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _ProjectNameDialog_Load(object sender, EventArgs e)
        {
            currencyName.SelectedItem = oldName;
        }
    }
}
