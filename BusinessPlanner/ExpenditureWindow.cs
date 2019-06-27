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
    public partial class ExpenditureWindow : Form
    {
        MainWindow mw;
        DocumentProgressor dgp;
        SectionInstructions secIns;
        public ExpenditureWindow(MainWindow maw)
        {
            InitializeComponent();
            mw = maw;
            dgp = new DocumentProgressor();
            secIns = new SectionInstructions();
            instruction_box.Text = secIns.instructionItems.FirstOrDefault(x => x.section == "Company Expenditure").instruction;
            if (File.Exists(ProjectConfig.projectPath + "\\data.xls"))
            {
                ExcelReader excelReader = new ExcelReader();
                excelReader.readExcelToDataGridView(dataGridView1, "Expenditures");
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
                    new string[1] { "Expenditures" }
                );
                TableGenerator tbl = new TableGenerator();
                tbl.Generate(dataGridView1,"expenditure.rtf");
                ChartGenerator cgen = new ChartGenerator();
                cgen.generatePieChart(dataGridView1,"generated.png","Company Expenditures");
                cgen.ImageToRtf("expenditure.rtf", "generated.png");
                Label l = mw.Controls.Find("label4", true)[0] as Label;
                ProgressBar pbar = mw.Controls.Find("progressBar1", true)[0] as ProgressBar;
                dgp.updateProgress("expenditure.rtf",dataGridView1.Rows.Count > 0 ? 1 : 0);
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
