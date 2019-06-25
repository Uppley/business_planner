using BP.Instructions;
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
        MainWindow mw;
        DocumentProgressor dgp;
        SectionInstructions secIns;
        public StartUpWindow(MainWindow maw)
        {
            InitializeComponent();
            mw = maw;
            dgp = new DocumentProgressor();
            secIns = new SectionInstructions();
            instruction_box.Text = secIns.instructionItems.FirstOrDefault(x => x.section == "Start Up Investment").instruction;
            if (File.Exists(ProjectConfig.projectPath + "\\data.xls"))
            { 
                ExcelReader excelReader = new ExcelReader();
                excelReader.readExcelToDataGridView(dataGridView1, "StartUp Cost");
                excelReader.Close();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            LoadingSpinner ls = new LoadingSpinner(this,AppMessages.messages["data_save"]);
            try
            {
                ls.show();
                ExcelReader excelReader = new ExcelReader();
                excelReader.saveExcelFromDataGridView(
                    new DataGridView[1] { dataGridView1 },
                    new int[1] { 1 },
                    new string[1] { "StartUp Cost" }
                );
                excelReader.Close();
                TableGenerator tbl = new TableGenerator();
                tbl.Generate(dataGridView1, "startup_table.rtf");
                ChartGenerator cgen = new ChartGenerator();
                cgen.generatePieChart(dataGridView1, "generated.png","Start Up Investment");
                cgen.ImageToRtf("startup_table.rtf", "generated.png");
                Label l = mw.Controls.Find("label4", true)[0] as Label;
                ProgressBar pbar = mw.Controls.Find("progressBar1", true)[0] as ProgressBar;
                dgp.updateProgress("startup_table.rtf", dataGridView1.Rows.Count > 0 ? 1 : 0);
                l.Text = dgp.completedSteps().ToString() + " /";
                pbar.Value = dgp.completedSteps();
                l.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            finally
            {
                ls.hide();
            }
        }
    }
}
