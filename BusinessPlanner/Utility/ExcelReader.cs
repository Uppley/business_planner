using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BusinessPlanner.Utility
{
    class ExcelReader
    {  
        public void createExcelFile(string filename,Dictionary<string,object> data)
        {
            ExcelPackage excelPackage = new ExcelPackage();
            if (AppUtilities.mainData["step1"].ToString() != "Quick Plan")
            {
                // First Sheet
                int duration = (int)data["plan_duration"];
                string[] months = ((List<string>)data["months"]).ToArray();
                ExcelWorksheet sheet1 = excelPackage.Workbook.Worksheets.Add("Sales Forecast");
                sheet1.Cells[1, 1].Value = "Product/Service";
                sheet1.Cells[1, 2].Value = "VAT (%)";
                for (int i = 0; i < duration; i++)
                {
                    sheet1.Cells[1, 3 + i].Value = months[i];
                }
                sheet1.Protection.IsProtected = false;
                sheet1.Protection.AllowSelectLockedCells = false;
                // Second Sheet
                ExcelWorksheet sheet2 = excelPackage.Workbook.Worksheets.Add("Cost Of Sales");
                sheet2.Cells[1, 1].Value = "Product/Service";
                sheet2.Cells[1, 2].Value = "VAT (%)";
                for (int i = 0; i < duration; i++)
                {
                    sheet2.Cells[1, 3 + i].Value = months[i];
                }
                sheet2.Protection.IsProtected = false;
                sheet2.Protection.AllowSelectLockedCells = false;

                // Third Sheet
                ExcelWorksheet sheet3 = excelPackage.Workbook.Worksheets.Add("Expenditure");
                sheet3.Cells[1, 1].Value = "Name";
                sheet3.Cells[1, 2].Value = "Amount";
                sheet3.Protection.IsProtected = false;
                sheet3.Protection.AllowSelectLockedCells = false;
            }
            

            if ((string)data["is_startup"] == "yes")
            {
                ExcelWorksheet sheet4 = excelPackage.Workbook.Worksheets.Add("StartUp Cost");
                sheet4.Cells[1, 1].Value = "Name";
                sheet4.Cells[1, 2].Value = "Amount";
                sheet4.Protection.IsProtected = false;
                sheet4.Protection.AllowSelectLockedCells = false;
            }

            // Fifth Sheet
            ExcelWorksheet sheet5 = excelPackage.Workbook.Worksheets.Add("Market Analysis");
            sheet5.Cells[1, 1].Value = "Group";
            sheet5.Cells[1, 2].Value = "Percentage (%)";
            sheet5.Protection.IsProtected = false;
            sheet5.Protection.AllowSelectLockedCells = false;

            excelPackage.SaveAs(new FileInfo(filename));
            
        }

        public DataGridView readExcelToDataGridView(string sheet_name)
        {
            DataGridView dgv = new DataGridView();
            try
            {
                FileInfo fileInfo = new FileInfo(ProjectConfig.projectPath + "\\" + "data.xls");
                using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[sheet_name];
                    int colCount = worksheet.Dimension.End.Column;
                    int rowCount = worksheet.Dimension.End.Row;
                    dgv.ColumnCount = colCount;
                    dgv.RowCount = rowCount;
                    for (int i = 1; i <= rowCount; i++)
                    {
                        if (i == 1)
                        {
                            for (int j = 1; j <= colCount; j++)
                            {
                                if (worksheet.Cells[i, j] != null && worksheet.Cells[i, j].Value != null)
                                {
                                    dgv.Columns[j - 1].HeaderText = worksheet.Cells[i, j].Value.ToString();
                                }
                            }
                        }
                        else
                        {
                            for (int j = 1; j <= colCount; j++)
                            {
                                if (worksheet.Cells[i, j] != null && worksheet.Cells[i, j].Value != null)
                                {
                                    dgv.Rows[i - 2].Cells[j - 1].Value = worksheet.Cells[i, j].Value.ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error encountered " + e.Message);
            }
            
            return dgv;
        }

        public void readExcelToDataGridView(DataGridView dgv, string sheet_name)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(ProjectConfig.projectPath + "\\" + "data.xls");
                using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[sheet_name];
                    int colCount = worksheet.Dimension.End.Column;
                    int rowCount = worksheet.Dimension.End.Row;
                    dgv.ColumnCount = colCount;
                    dgv.RowCount = rowCount;
                    for (int j = 1; j <= colCount; j++)
                    {
                        dgv.Columns[j - 1].HeaderText = worksheet.Cells[1, j].Value.ToString();
                    }

                    for (int i = 2; i <= rowCount; i++)
                    {
                        for (int j = 1; j <= colCount; j++)
                        {
                            if (worksheet.Cells[i, j] != null && worksheet.Cells[i, j].Value != null)
                            {
                                dgv.Rows[i - 2].Cells[j - 1].Value = worksheet.Cells[i, j].Value.ToString();
                            }
                        }
                    }


                }
    
            }catch(Exception e)
            {
                Console.WriteLine("Error encountered "+e.Message);
            }
            
        }

        public void saveExcelFromDataGridView(DataGridView[] dgvs,int[] sheets,string[] sheetNames)
        {
            try
            {

                FileInfo fileInfo = new FileInfo(ProjectConfig.projectPath+"//"+"data.xls");
                using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
                {
                    for (int s = 0; s < sheets.Length; s++)
                    {
                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[sheetNames[s]];

                        for (int i = 0; i < dgvs[s].Rows.Count - 1; i++)
                        {
                            for (int j = 0; j < dgvs[s].Columns.Count; j++)
                            {
                                worksheet.Cells[i + 2, j + 1].Value = dgvs[s].Rows[i].Cells[j].Value.ToString();
                            }
                        }
                        excelPackage.Save();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error encountered " + e.Message);
            }
            
        }

        
    }
}
