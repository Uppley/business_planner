using BusinessPlanner.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace BusinessPlanner
{
    public partial class ForecastWindow : Form
    {
        MainWindow mw;
        DocumentProgressor dgp;
        public ForecastWindow(MainWindow maw)
        {
            InitializeComponent();
            mw = maw;
            dgp = new DocumentProgressor();
            if (File.Exists(ProjectConfig.projectPath+"\\data.xls"))
            {
                ExcelReader excelReader = new ExcelReader();
                excelReader.readExcelToDataGridView(dataGridView1, "Sales Forecast");
                excelReader.readExcelToDataGridView(dataGridView2, "Cost Of Sales");

            }
        }

        

        private void Button1_Click_1(object sender, EventArgs e)
        {
            LoadingSpinner ls = new LoadingSpinner(this,AppMessages.messages["data_save"]);
            try
            {
                ls.show();
                ExcelReader excelReader = new ExcelReader();
                excelReader.saveExcelFromDataGridView(
                    new DataGridView[2]{dataGridView1,dataGridView2}, 
                    new int[2]{1,2}, 
                    new string[2]{"Sales Forecast","Cost Of Sales"}
                );

                TableGenerator tbl = new TableGenerator();
                tbl.GenerateMultipleTable(new DataGridView[2] { dataGridView1, dataGridView2 }, "sales_forecast_table.rtf", new string[2] { "Sales Forecast", "Cost Of Sales" });
                ChartGenerator cgr = new ChartGenerator();
                cgr.generateBarChart(dataGridView1, "generate.png", "Forecast Sales");
                cgr.ImageToRtf("sales_forecast_table.rtf", "generate.png");
                cgr.generateBarChart(dataGridView2, "generate1.png", "Cost of Sales");
                cgr.ImageToRtf("sales_forecast_table.rtf", "generate1.png");
                Label l = mw.Controls.Find("label4", true)[0] as Label;
                ProgressBar pbar = mw.Controls.Find("progressBar1", true)[0] as ProgressBar;
                dgp.updateProgress("sales_forecast_table.rtf", dataGridView1.Rows.Count > 0 && dataGridView2.Rows.Count > 0 ? 1 : 0);
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
