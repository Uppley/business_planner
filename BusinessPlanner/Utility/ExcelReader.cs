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

        public DataGridView readExcelToDataGridView(string filename)
        {
            DataGridView dgv = new DataGridView();
            try
            {
                xlWorkbook = xlApp.Workbooks.Open(ProjectConfig.projectPath + "\\" + filename);
                xlWorksheet = xlWorkbook.Sheets[1];
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

        public void readExcelToDataGridView(string filename,DataGridView dgv)
        {
            try
            {
                xlWorkbook = xlApp.Workbooks.Open(ProjectConfig.projectPath + "\\" + filename);
                xlWorksheet = xlWorkbook.Sheets[1];
                xlRange = xlWorksheet.UsedRange;

                int rowCount = xlRange.Rows.Count;
                int colCount = xlRange.Columns.Count;

                // dt.Column = colCount;  
                dgv.ColumnCount = colCount;
                dgv.RowCount = rowCount;

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

        public void saveExcelFromDataGridView(DataGridView dgv)
        {
            try
            { 
                xlWorkbook = xlApp.Workbooks.Add(Type.Missing);
                xlWorksheet = null;
                xlWorksheet = xlWorkbook.Sheets["Sheet1"];
                xlWorksheet = xlWorkbook.ActiveSheet;
                xlWorksheet.Name = "Sales Forecast";
                for (int i = 1; i < dgv.Columns.Count + 1; i++)
                {
                    xlWorksheet.Cells[1, i] = dgv.Columns[i - 1].HeaderText;
                }
                for (int i = 0; i < dgv.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dgv.Columns.Count; j++)
                    {
                        xlWorksheet.Cells[i + 2, j + 1] = dgv.Rows[i].Cells[j].Value.ToString();
                    }
                }
                if (File.Exists(ProjectConfig.projectPath + "\\data.xls"))
                {
                    File.Delete(ProjectConfig.projectPath + "\\data.xls");
                }
                xlWorkbook.SaveAs(ProjectConfig.projectPath + "\\data.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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
