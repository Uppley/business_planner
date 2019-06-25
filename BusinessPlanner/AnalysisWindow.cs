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
    public partial class AnalysisWindow : Form
    {
        MainWindow mw;
        DocumentProgressor dgp;
        SectionInstructions secIns;
        public AnalysisWindow(MainWindow maw)
        {
            InitializeComponent();
            mw = maw;
            dgp = new DocumentProgressor();
            secIns = new SectionInstructions();
            instruction_box.Text = secIns.instructionItems.FirstOrDefault(x => x.section == "Analysis Table").instruction;
            if (File.Exists(ProjectConfig.projectPath + "\\data.xls"))
            {
                ExcelReader excelReader = new ExcelReader();
                excelReader.readExcelToDataGridView(dataGridView1, "Market Analysis");
                excelReader.Close();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(validateData())
            {
                LoadingSpinner ls = new LoadingSpinner(this, AppMessages.messages["data_save"]);
                try
                {
                    ls.show();
                    ExcelReader excelReader = new ExcelReader();
                    excelReader.saveExcelFromDataGridView(
                        new DataGridView[1] { dataGridView1 },
                        new int[1] { 1 },
                        new string[1] { "Market Analysis" }
                    );
                    TableGenerator tbl = new TableGenerator();
                    tbl.Generate(dataGridView1, "market_analysis.rtf");
                    ChartGenerator cgen = new ChartGenerator();
                    cgen.generatePieChart(dataGridView1, "generated.png", "Market Analysis");
                    cgen.ImageToRtf("market_analysis.rtf", "generated.png");
                    excelReader.Close();
                    Label l = mw.Controls.Find("label4", true)[0] as Label;
                    ProgressBar pbar = mw.Controls.Find("progressBar1", true)[0] as ProgressBar;
                    dgp.updateProgress("market_analysis.rtf", dataGridView1.Rows.Count > 0 ? 1 : 0);
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
            else
            {
                MessageBox.Show("Percentage cannot exceed 100 !");
            }
            
        }

        private bool validateData()
        {
            int sum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                sum += Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
            }
            if(sum>100)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
