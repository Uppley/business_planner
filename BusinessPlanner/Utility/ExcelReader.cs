using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace BusinessPlanner.Utility
{
    class ExcelReader
    {
        private Excel.Application xlApp;
        private Excel.Workbook xlWorkbook;
        private Excel._Worksheet xlWorksheet;
        private Excel.Range xlRange;

        public ExcelReader()
        {
            xlApp = new Excel.Application();
        }

        public void createExcelFile(string filename,Dictionary<string,object> data)
        {
            int duration = (int) data["plan_duration"];
            string[] months = ((List<string>)data["months"]).ToArray();
            object missing = Type.Missing;
            xlApp.Visible = false;
            xlWorkbook = xlApp.Workbooks.Add(missing);

            // First Sheet
            Excel.Worksheet oSheet = xlWorkbook.ActiveSheet as Excel.Worksheet;
            oSheet.Name = "Sales Forecast";
            oSheet.Cells[1, 1] = "Product/Service";
            oSheet.Cells[1, 2] = "VAT (%)";
            for(int i = 0; i < duration; i++)
            {
                oSheet.Cells[1, 3+i] = months[i];
            }
            
            // Second Sheet
            Excel.Worksheet oSheet2 = xlWorkbook.Sheets.Add(missing, missing, 1, missing) as Excel.Worksheet;
            oSheet2.Name = "Cost Of Sales";
            oSheet2.Cells[1, 1] = "Product/Service";
            oSheet2.Cells[1, 2] = "VAT (%)";
            for (int i = 0; i < duration; i++)
            {
                oSheet2.Cells[1, 3 + i] = months[i];
            }

            // Third Sheet
            Excel.Worksheet oSheet3 = xlWorkbook.Sheets.Add(missing, missing, 1, missing) as Excel.Worksheet;
            oSheet3.Name = "Expenditures";
            oSheet3.Cells[1, 1] = "Name";
            oSheet3.Cells[1, 2] = "Amount";


            xlWorkbook.SaveAs(filename, Excel.XlFileFormat.xlOpenXMLWorkbook,
                missing, missing, missing, missing,
                Excel.XlSaveAsAccessMode.xlNoChange,
                missing, missing, missing, missing, missing);
            xlWorkbook.Close(missing, missing, missing);
            xlApp.UserControl = true;
            this.Close();
        }

        public DataGridView readExcelToDataGridView(int sheet)
        {
            DataGridView dgv = new DataGridView();
            try
            {
                xlWorkbook = xlApp.Workbooks.Open(ProjectConfig.projectPath + "\\" + "data.xls");
                xlWorksheet = xlWorkbook.Sheets[sheet];
                xlRange = xlWorksheet.UsedRange;

                int rowCount = xlRange.Rows.Count;
                int colCount = xlRange.Columns.Count;
  
                dgv.ColumnCount = colCount;
                dgv.RowCount = rowCount;
                
                for (int i = 1; i <= rowCount; i++)
                {
                    if (i == 1)
                    {
                        for (int j = 1; j <= colCount; j++)
                        {
                            if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                            {
                                dgv.Columns[j - 1].HeaderText = xlRange.Cells[i, j].Value2.ToString();
                            }
                        }
                    }
                    else
                    {
                        for (int j = 1; j <= colCount; j++)
                        {
                            if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                            {
                                dgv.Rows[i - 2].Cells[j - 1].Value = xlRange.Cells[i, j].Value2.ToString();
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error encountered " + e.Message);
            }
            finally
            {
               
                xlWorkbook.Close();
            }
            return dgv;
        }

        public void readExcelToDataGridView(DataGridView dgv, int sheet)
        {
            try
            {
                xlWorkbook = xlApp.Workbooks.Open(ProjectConfig.projectPath + "\\" + "data.xls");
                xlWorksheet = xlWorkbook.Sheets[sheet];
                xlRange = xlWorksheet.UsedRange;

                int rowCount = xlRange.Rows.Count;
                int colCount = xlRange.Columns.Count;

                // dt.Column = colCount;  
                dgv.ColumnCount = colCount;
                dgv.RowCount = rowCount;
                for (int j = 1; j <= colCount; j++)
                {
                    dgv.Columns[j-1].HeaderText = xlRange.Cells[1, j].Value2.ToString();
                }

                for (int i = 2; i <= rowCount; i++)
                {
                    for (int j = 1; j <= colCount; j++)
                    {
                        if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                        {
                            dgv.Rows[i - 2].Cells[j - 1].Value = xlRange.Cells[i, j].Value2.ToString();
                        }
                    }
                }
                
            }catch(Exception e)
            {
                Console.WriteLine("Error encountered "+e.Message);
            }
            finally
            {
                xlWorkbook.Close();
            }
        }

        public void saveExcelFromDataGridView(DataGridView[] dgvs,int[] sheets,string[] sheetNames)
        {
            try
            {
                xlApp.Visible = false;
                xlApp.DisplayAlerts = false;
                xlWorkbook = xlApp.Workbooks.Open(ProjectConfig.projectPath+"//"+"data.xls");
                xlWorksheet = (Worksheet) xlWorkbook.Worksheets[sheetNames[0]];
                for (int i = 0; i < dgvs[0].Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dgvs[0].Columns.Count; j++)
                    {
                        xlWorksheet.Cells[i + 2, j + 1] = dgvs[0].Rows[i].Cells[j].Value.ToString();
                    }
                }


                xlWorksheet = (Worksheet)xlWorkbook.Worksheets[sheetNames[1]];
                for (int i = 0; i < dgvs[1].Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dgvs[1].Columns.Count; j++)
                    {
                        xlWorksheet.Cells[i + 2, j + 1] = dgvs[1].Rows[i].Cells[j].Value.ToString();
                    }
                }

                xlWorkbook.SaveAs(ProjectConfig.projectPath + "\\data.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                xlWorkbook.Close(true, Type.Missing, Type.Missing);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error encountered " + e.Message);
            }
            
        }

        public void Close()
        {
            xlApp.Quit();
        }
    }
}
