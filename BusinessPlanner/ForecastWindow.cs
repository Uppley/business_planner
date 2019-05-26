﻿using BusinessPlanner.Utility;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace BusinessPlanner
{
    public partial class ForecastWindow : Form
    {
        
        public ForecastWindow()
        {
            InitializeComponent();
            if(File.Exists(ProjectConfig.projectPath+"\\data.xls"))
            {
                _LoadingDialog ld = new _LoadingDialog(AppMessages.messages["data_load"]);
                try
                {
                    ld.Show();
                    Application.DoEvents();
                    ExcelReader excelReader = new ExcelReader();
                    excelReader.readExcelToDataGridView(dataGridView1, "Sales Forecast");
                    excelReader.readExcelToDataGridView(dataGridView2, "Cost Of Sales");
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

        

        private void Button1_Click_1(object sender, EventArgs e)
        {
            _LoadingDialog ld = new _LoadingDialog(AppMessages.messages["data_save"]);
            try
            {
                ld.Show();
                Application.DoEvents();
                ExcelReader excelReader = new ExcelReader();
                excelReader.saveExcelFromDataGridView(
                    new DataGridView[2]{dataGridView1,dataGridView2}, 
                    new int[2]{1,2}, 
                    new string[2]{"Sales Forecast","Cost Of Sales"}
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
