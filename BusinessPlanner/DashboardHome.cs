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
using System.Windows.Forms.DataVisualization.Charting;

namespace BusinessPlanner
{
    public partial class DashboardHome : Form
    {
        public DashboardHome()
        {
            InitializeComponent();
            chart1.Titles.Add("Sales Forecast");
            _LoadingDialog ld = new _LoadingDialog(AppMessages.messages["data_load"]);
            try
            {
                ld.Show();
                Application.DoEvents();
                populateSalesForecast();
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message);
            }
            finally
            {
                ld.Close();
            }
            chart2.Series[0].ChartType = SeriesChartType.Pie;
            List<SalesItem> sdata = new List<SalesItem>()
            {
                new SalesItem(){ Id=1,Month="Jan",Amount=1000 },
                new SalesItem(){ Id=2,Month="Feb",Amount=1200 },
                new SalesItem(){ Id=3,Month="Mar",Amount=800 },
                new SalesItem(){ Id=4,Month="Apr",Amount=1100 },
                new SalesItem(){ Id=5,Month="May",Amount=1300 },
                new SalesItem(){ Id=6,Month="Jun",Amount=1500 },
                new SalesItem(){ Id=7,Month="Jul",Amount=900 },
                new SalesItem(){ Id=8,Month="Aug",Amount=1000 },
                new SalesItem(){ Id=9,Month="Sep",Amount=1400 },
                new SalesItem(){ Id=10,Month="Oct",Amount=700 },
                new SalesItem(){ Id=11,Month="Nov",Amount=800 },
                new SalesItem(){ Id=12,Month="Dec",Amount=1400 },
            };
            List<SalesItem> csdata = new List<SalesItem>()
            {
                new SalesItem(){ Id=1,Month="Jan",Amount=800 },
                new SalesItem(){ Id=2,Month="Feb",Amount=900 },
                new SalesItem(){ Id=3,Month="Mar",Amount=700 },
                new SalesItem(){ Id=4,Month="Apr",Amount=1000 },
                new SalesItem(){ Id=5,Month="May",Amount=1100 },
                new SalesItem(){ Id=6,Month="Jun",Amount=1200 },
                new SalesItem(){ Id=7,Month="Jul",Amount=1000 },
                new SalesItem(){ Id=8,Month="Aug",Amount=800 },
                new SalesItem(){ Id=9,Month="Sep",Amount=1000 },
                new SalesItem(){ Id=10,Month="Oct",Amount=500 },
                new SalesItem(){ Id=11,Month="Nov",Amount=700 },
                new SalesItem(){ Id=12,Month="Dec",Amount=1100 },
            };
            List<ExpenditureItem> lex = new List<ExpenditureItem>()
            {
                new ExpenditureItem(){name="Rent",amount=50000},
                new ExpenditureItem(){name="Utilities",amount=10000},
                new ExpenditureItem(){name="Equipment",amount=5000},
                new ExpenditureItem(){name="Fixtures",amount=20000},
                new ExpenditureItem(){name="Inventory",amount=40000},
                new ExpenditureItem(){name="Marketing Budget",amount=15000},
            };
            
            chart2.DataSource = lex;
            chart2.Series[0].XValueMember = "name";
            chart2.Series[0].YValueMembers = "amount";
            chart2.Titles.Add("Company Expenditures");
            chart2.DataBind();
            DataTable dtAll = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            dt1.Columns.Add("Month");
            dt1.Columns.Add("Sales");
            foreach (var item in sdata)
            {
                var row = dt1.NewRow();
                row["Month"] = item.Month;
                row["Sales"] = Convert.ToString(item.Amount);
                dt1.Rows.Add(row);
            }
            dt2.Columns.Add("Month");
            dt2.Columns.Add("Cost Of Sales");
            foreach (var item in csdata)
            {
                var row = dt2.NewRow();
                row["Month"] = item.Month;
                row["Cost Of Sales"] = Convert.ToString(item.Amount);
                dt2.Rows.Add(row);
            }
            dt1.PrimaryKey = new DataColumn[]
            {
                dt1.Columns["Month"]
            };
            dt2.PrimaryKey = new DataColumn[]
            {
                dt2.Columns["Month"]
            };
            dt1.Merge(dt2);
            dtAll = dt1;
            chart3.Series.Remove(chart3.Series[0]);
            chart3.Series.Add("Sales");
            chart3.Series["Sales"].ChartType = SeriesChartType.Area;
            chart3.Series["Sales"].XValueMember = "Month";
            chart3.Series["Sales"].YValueMembers = "Sales";
            chart3.Series.Add("Cost Of Sales");
            chart3.Series["Cost Of Sales"].ChartType = SeriesChartType.Area;
            chart3.Series["Cost Of Sales"].YValueMembers = "Cost Of Sales";
            chart3.DataSource = dtAll;
            chart3.Titles.Add("Sales vs Cost Of Sales");
            chart3.DataBind();
            DataTable dtNew = new DataTable();
            dtNew = dtAll;
            dtNew.Columns.Add("Gross Profit", typeof(float));
            chart4.Series[0].ChartType = SeriesChartType.Line;
            chart4.Series[0].MarkerStyle = MarkerStyle.Diamond;
            chart4.Series[0].MarkerColor = Color.DarkRed;
            chart4.Series[0].MarkerSize = 9;
            chart4.Series[0].BorderWidth = 2;
            foreach (DataRow row in dtNew.Rows)
            {

                var gs = float.Parse(row["Sales"].ToString()) - float.Parse(row["Cost Of Sales"].ToString());
                if (gs < 0)
                    gs = 0;
                row["Gross Profit"] = gs;
                
            }
            
            chart4.Series[0].XValueMember = "Month";
            chart4.Series[0].YValueMembers = "Gross Profit";
            chart4.Titles.Add("Gross Profit Forecasting");
            chart4.Series[0].LegendText = "Gross Profit";
            chart4.DataSource = dtNew;
            chart4.DataBind();
        }

        public void populateSalesForecast()
        {
            if(File.Exists(ProjectConfig.projectPath + "\\data.xls"))
            {
                List<SaleItem> saleList = new List<SaleItem>();
                DataGridView dgv = new DataGridView();
                ExcelReader excelReader = new ExcelReader();
                dgv = excelReader.readExcelToDataGridView("data.xls");
                excelReader.Close();
                List<string> months = new List<string>();
                for (int col = 2; col < dgv.Rows[0].Cells.Count; col++)
                {
                    months.Add(dgv.Columns[col].HeaderText);
                }
                
                float col1 = 0, col2 = 0, col3 = 0, col4 = 0, col5 = 0, col6 = 0;
                for (int rows = 0; rows < dgv.Rows.Count - 1; rows++)
                {
                    col1 += float.Parse(dgv.Rows[rows].Cells[2].Value.ToString());
                    col2 += float.Parse(dgv.Rows[rows].Cells[3].Value.ToString());
                    col3 += float.Parse(dgv.Rows[rows].Cells[4].Value.ToString());
                    col4 += float.Parse(dgv.Rows[rows].Cells[5].Value.ToString());
                    col5 += float.Parse(dgv.Rows[rows].Cells[6].Value.ToString());
                    col6 += float.Parse(dgv.Rows[rows].Cells[7].Value.ToString());
                    
                }
                
                Series SalesForecast = new Series(Name = "Sales Amount");
                chart1.Series.Remove(chart1.Series[0]);
                chart1.Series.Add(SalesForecast);
                
                float[] values = { col1, col2, col3, col4, col5, col6 };
                string[] s = months.ToArray();
                int x = 0;

                foreach (var v in values)
                {
                    SalesForecast.Points.AddXY(s[x], v);
                    x++;
                }
            }
            
            
        }
    }
}
