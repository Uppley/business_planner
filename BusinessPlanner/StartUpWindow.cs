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
    public partial class StartUpWindow : Form
    {
        public StartUpWindow()
        {
            InitializeComponent();
            if (File.Exists(ProjectConfig.projectPath + "\\data.xls"))
            {
                _LoadingDialog ld = new _LoadingDialog(AppMessages.messages["data_load"]);
                try
                {
                    ld.Show();
                    Application.DoEvents();
                    ExcelReader excelReader = new ExcelReader();
                    excelReader.readExcelToDataGridView(dataGridView1, "StartUp Cost");
                    excelReader.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Exception: " + e.Message);
                }
                finally
                {
                    ld.Close();
                }

            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            _LoadingDialog ld = new _LoadingDialog(AppMessages.messages["data_save"]);
            try
            {
                ld.Show();
                Application.DoEvents();
                ExcelReader excelReader = new ExcelReader();
                excelReader.saveExcelFromDataGridView(
                    new DataGridView[1] { dataGridView1 },
                    new int[1] { 1 },
                    new string[1] { "StartUp Cost" }
                );
                excelReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            finally
            {
                ld.Close();
            }
        }
    }
}
