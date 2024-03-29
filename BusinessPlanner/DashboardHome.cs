﻿using BusinessPlanner.Models;
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
    public partial class DashboardHome : Form
    {
        List<string> months = new List<string>() { "Month1", "Month2", "Month3", "Month4", "Month5", "Month6" };
        List<float> salesAmount = new List<float>() { 0,0,0,0,0,0};
        List<float> costSales = new List<float>() { 0, 0, 0, 0, 0, 0 };
        List<ExpenditureItem> expenditures = new List<ExpenditureItem>();
        List<ExpenditureItem> startupInvestments = new List<ExpenditureItem>();
        List<MarketItem> marketAnalysis = new List<MarketItem>();
        public DashboardHome(MainWindow mw)
        {
            InitializeComponent();
            
            LoadingSpinner ls = new LoadingSpinner(mw,AppMessages.messages["data_load"]);
            try
            {
                ls.show();
                fetchAllData();
                if (ProjectConfig.projectSettings["PlanType"].ToString() == "Quick Plan")
                {
                    populateStartUpInvestment();
                    populateMarket();
                    chart3.Visible = false;
                    chart4.Visible = false;
                }
                else
                {
                    populateSalesForecast();
                    populateSalesVsCost();
                    populateExpenditure();
                    populateGrossProfit();
                }
                
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

        private void populateMarket()
        {
            chart1.Series[0].ChartType = SeriesChartType.Pie;
            chart1.DataSource = marketAnalysis;
            chart1.Series[0].XValueMember = "group";
            chart1.Series[0].YValueMembers = "percentage";
            chart1.Titles.Add("Market Analysis");
            chart1.DataBind();
        }

        private void populateStartUpInvestment()
        {
            chart2.Series[0].ChartType = SeriesChartType.Pie;
            chart2.DataSource = startupInvestments;
            chart2.Series[0].XValueMember = "name";
            chart2.Series[0].YValueMembers = "amount";
            chart2.Titles.Add("Start Up Investments");
            chart2.DataBind();
        }

        private void fetchAllData()
        {
            if (File.Exists(ProjectConfig.projectPath + "\\data.xls"))
            {
                if(ProjectConfig.projectSettings["PlanType"].ToString() == "Quick Plan")
                {
                    DataGridView dgv1 = new DataGridView();
                    ExcelReader excelReader = new ExcelReader();
                    dgv1 = excelReader.readExcelToDataGridView("StartUp Cost");
                    for (int rows = 0; rows < dgv1.Rows.Count - 1; rows++)
                    {
                        startupInvestments.Add(new ExpenditureItem() { name = dgv1.Rows[rows].Cells[0].Value.ToString(), amount = float.Parse(dgv1.Rows[rows].Cells[1].Value.ToString()) });
                    }
                    dgv1.Rows.Clear();
                    dgv1.Columns.Clear();
                    dgv1 = excelReader.readExcelToDataGridView("Market Analysis");
                    for (int rows = 0; rows < dgv1.Rows.Count - 1; rows++)
                    {
                        marketAnalysis.Add(new MarketItem() { group = dgv1.Rows[rows].Cells[0].Value.ToString(), percentage = float.Parse(dgv1.Rows[rows].Cells[1].Value.ToString()) });
                    }
                    dgv1.Rows.Clear();
                    dgv1.Columns.Clear();
                    dgv1.Dispose();
                }
                else
                {
                    DataGridView dgv1 = new DataGridView();
                    ExcelReader excelReader = new ExcelReader();
                    dgv1 = excelReader.readExcelToDataGridView("Sales Forecast");

                    months = new List<string>();
                    for (int col = 2; col < dgv1.Rows[0].Cells.Count; col++)
                    {
                        months.Add(dgv1.Columns[col].HeaderText);
                    }

                    float col1 = 0, col2 = 0, col3 = 0, col4 = 0, col5 = 0, col6 = 0;
                    for (int rows = 0; rows < dgv1.Rows.Count - 1; rows++)
                    {
                        col1 += float.Parse(dgv1.Rows[rows].Cells[2].Value.ToString());
                        col2 += float.Parse(dgv1.Rows[rows].Cells[3].Value.ToString());
                        col3 += float.Parse(dgv1.Rows[rows].Cells[4].Value.ToString());
                        col4 += float.Parse(dgv1.Rows[rows].Cells[5].Value.ToString());
                        col5 += float.Parse(dgv1.Rows[rows].Cells[6].Value.ToString());
                        col6 += float.Parse(dgv1.Rows[rows].Cells[7].Value.ToString());

                    }
                    this.salesAmount = new List<float>() { col1, col2, col3, col4, col5, col6 };
                    dgv1.Rows.Clear();
                    dgv1.Columns.Clear();
                    dgv1 = excelReader.readExcelToDataGridView("Cost Of Sales");
                    col1 = 0; col2 = 0; col3 = 0; col4 = 0; col5 = 0; col6 = 0;
                    for (int rows = 0; rows < dgv1.Rows.Count - 1; rows++)
                    {
                        col1 += float.Parse(dgv1.Rows[rows].Cells[2].Value.ToString());
                        col2 += float.Parse(dgv1.Rows[rows].Cells[3].Value.ToString());
                        col3 += float.Parse(dgv1.Rows[rows].Cells[4].Value.ToString());
                        col4 += float.Parse(dgv1.Rows[rows].Cells[5].Value.ToString());
                        col5 += float.Parse(dgv1.Rows[rows].Cells[6].Value.ToString());
                        col6 += float.Parse(dgv1.Rows[rows].Cells[7].Value.ToString());

                    }
                    this.costSales = new List<float>() { col1, col2, col3, col4, col5, col6 };
                    dgv1.Rows.Clear();
                    dgv1.Columns.Clear();
                    dgv1 = excelReader.readExcelToDataGridView("Expenditures");
                    for (int rows = 0; rows < dgv1.Rows.Count - 1; rows++)
                    {
                        expenditures.Add(new ExpenditureItem() { name = dgv1.Rows[rows].Cells[0].Value.ToString(), amount = float.Parse(dgv1.Rows[rows].Cells[1].Value.ToString()) });
                    }
                    dgv1.Rows.Clear();
                    dgv1.Columns.Clear();
                    dgv1.Dispose();
                }
                

            }
        }

        private void populateExpenditure()
        {
            chart2.Series[0].ChartType = SeriesChartType.Pie;
            chart2.DataSource = expenditures;
            chart2.Series[0].XValueMember = "name";
            chart2.Series[0].YValueMembers = "amount";
            chart2.Titles.Add("Company Expenditures");
            chart2.DataBind();
        }

        public void populateSalesForecast()
        {
            chart1.Titles.Add("Sales Forecast");
            Series SalesForecast = new Series(Name = "Sales Amount");
            chart1.Series.Remove(chart1.Series[0]);
            chart1.Series.Add(SalesForecast);  
            float[] values = this.salesAmount.ToArray();
            string[] s = this.months.ToArray();
            int x = 0;

            foreach (var v in values)
            {
                SalesForecast.Points.AddXY(s[x], v);
                x++;
            }
        }

        public void populateSalesVsCost()
        {
            chart3.Titles.Add("Sales vs Cost Of Sales");
            chart3.Series.Remove(chart3.Series[0]);
            chart3.Series.Add("Sales");
            chart3.Series["Sales"].ChartType = SeriesChartType.Area;
            float[] values = this.salesAmount.ToArray();
            string[] s = months.ToArray();
            int x = 0;
            foreach (var v in values)
            {
                chart3.Series["Sales"].Points.AddXY(s[x], v);
                x++;
            }    
            chart3.Series.Add("Cost Of Sales");
            chart3.Series["Cost Of Sales"].ChartType = SeriesChartType.Area;
            float[] values1 = this.costSales.ToArray();
            int x1 = 0;
            foreach (var v in values1)
            {
                chart3.Series["Cost Of Sales"].Points.AddXY(s[x1], v);
                x1++;
            }
            
        }

        private void populateGrossProfit()
        {
            List<float> grossProfit = new List<float>();
            for(int i=0;i<salesAmount.Count();i++)
            {
                grossProfit.Add(salesAmount[i] - costSales[i]);
            }
            chart4.Titles.Add("Gross Profit Forecasting");
            chart4.Series.Remove(chart4.Series[0]);
            chart4.Series.Add("Gross Profit");
            chart4.Series[0].ChartType = SeriesChartType.Line;
            chart4.Series[0].MarkerStyle = MarkerStyle.Diamond;
            chart4.Series[0].MarkerColor = Color.DarkRed;
            chart4.Series[0].MarkerSize = 9;
            chart4.Series[0].BorderWidth = 2;
            string[] s = this.months.ToArray();
            float[] gP = grossProfit.ToArray();
            int x = 0;
            foreach (var v in gP)
            {
                chart4.Series["Gross Profit"].Points.AddXY(s[x], v);
                x++;
            }

        }
    }
}
