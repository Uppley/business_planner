using BP.Instructions;
using BusinessPlanner.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessPlanner
{
    public partial class FinancialWindow : Form
    {
        MainWindow mw;
        DocumentProgressor dgp;
        List<ExpenditureItem> expenditures = new List<ExpenditureItem>();
        float totalSales = 0;
        float totalCostSales = 0;
        float grossProfit = 0;
        float totalExpenditures = 0;
        float operatingProfit = 0;
        SectionInstructions secIns;
        public FinancialWindow(MainWindow maw)
        {
            InitializeComponent();
            mw = maw;
            dgp = new DocumentProgressor();
            saveBt.Left = (saveBt.Parent.Width - saveBt.Width) / 2;
            secIns = new SectionInstructions();
            instruction_box.Text = secIns.instructionItems.FirstOrDefault(x => x.section == "Financial Statement").instruction;
            try
            {
                fetchAllData();
                generateStatement();
            }
            catch
            {
                MessageBox.Show("Unable to fetch data !");
            }
        }

        private void fetchAllData()
        {
            
            DataGridView dgv1 = new DataGridView();
            ExcelReader excelReader = new ExcelReader();
            dgv1 = excelReader.readExcelToDataGridView("Sales Forecast");
            
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
            totalSales = col1+col2+col3+col4+col5+col6;
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
            totalCostSales = col1 + col2 + col3 + col4 + col5 + col6;
            grossProfit = totalSales - totalCostSales;
            dgv1.Rows.Clear();
            dgv1.Columns.Clear();
            if(ProjectConfig.projectSettings["StartUp"].ToString() == "yes")
            {
                dgv1 = excelReader.readExcelToDataGridView("StartUp Cost");
                for (int rows = 0; rows < dgv1.Rows.Count - 1; rows++)
                {
                    expenditures.Add(new ExpenditureItem() { name = dgv1.Rows[rows].Cells[0].Value.ToString(), amount = float.Parse(dgv1.Rows[rows].Cells[1].Value.ToString()) });
                }
                dgv1.Rows.Clear();
                dgv1.Columns.Clear();
            }
            
            dgv1 = excelReader.readExcelToDataGridView("Expenditures");
            for (int rows = 0; rows < dgv1.Rows.Count - 1; rows++)
            {
                expenditures.Add(new ExpenditureItem() { name = dgv1.Rows[rows].Cells[0].Value.ToString(), amount = float.Parse(dgv1.Rows[rows].Cells[1].Value.ToString()) });
            }
            dgv1.Rows.Clear();
            dgv1.Columns.Clear();
            dgv1.Dispose();
            foreach(var e in expenditures)
            {
                totalExpenditures += e.amount;
            }
            operatingProfit = grossProfit - totalExpenditures;
        }

        private void generateStatement()
        {
            StringBuilder rtfTableString = new StringBuilder();
            rtfTableString.Append(@"{\rtf1\ansi\deff0\fs25{\colortbl;\red0\green0\blue0;\red255\green0\blue0;}");
            rtfTableString.Append(@"\line");
            rtfTableString.Append(@"{\fs20\b\ Forecast Duration = "+ ProjectConfig.projectSettings["Duration"].ToString() + @"months, Currency = "+ ProjectConfig.projectSettings["Currency"].ToString() + @"}");
            rtfTableString.Append(@"\row\pard\par");

            //sales amount
            rtfTableString.Append(@"\trowd\trgaph144");
            rtfTableString.Append(@"\clbrdrt\brdrs\clbrdrl\brdrs\clbrdrb\brdrs\clbrdrr\brdrs"); // Top border
            rtfTableString.Append(@"\cellx4000");
            rtfTableString.Append(@"\clbrdrt\brdrs\clbrdrl\brdrs\clbrdrb\brdrs\clbrdrr\brdrs"); // Bottom border
            rtfTableString.Append(@"\cellx7000");

            rtfTableString.Append(@"{\fs30\b Sales}\intbl\cell");
            rtfTableString.Append(@" ");
            rtfTableString.Append(@""+totalSales+@"\intbl\cell");
            rtfTableString.Append(@"\row");

            //cost of sales amount
            rtfTableString.Append(@"\trowd\trgaph144");
            rtfTableString.Append(@"\clbrdrt\brdrs\clbrdrl\brdrs\clbrdrb\brdrs\clbrdrr\brdrs"); // Top border
            rtfTableString.Append(@"\cellx4000");
            rtfTableString.Append(@"\clbrdrt\brdrs\clbrdrl\brdrs\clbrdrb\brdrs\clbrdrr\brdrs"); // Bottom border
            rtfTableString.Append(@"\cellx7000");
            
            rtfTableString.Append(@"{\fs30\b Cost of Sales}\intbl\cell");
            rtfTableString.Append(@" ");
            rtfTableString.Append(@"" + totalCostSales + @"\intbl\cell");
            rtfTableString.Append(@"\row");

            //gross profit amount
            rtfTableString.Append(@"\trowd\trgaph144");
            rtfTableString.Append(@"\clbrdrt\brdrs\clbrdrl\brdrs\clbrdrb\brdrs\clbrdrr\brdrs"); // Top border
            rtfTableString.Append(@"\cellx4000");
            rtfTableString.Append(@"\clbrdrt\brdrs\clbrdrl\brdrs\clbrdrb\brdrs\clbrdrr\brdrs"); // Bottom border
            rtfTableString.Append(@"\cellx7000");

            rtfTableString.Append(@"{\fs30\b Gross Profit}\intbl\cell");
            rtfTableString.Append(@" ");
            rtfTableString.Append(@"" + grossProfit + @"\intbl\cell");
            rtfTableString.Append(@"\row");

            //expenditures
            rtfTableString.Append(@"\trowd\trgaph144");
            rtfTableString.Append(@"\clbrdrt\brdrs\clbrdrl\brdrs\clbrdrb\brdrs\clbrdrr\brdrs"); // Top border
            rtfTableString.Append(@"\cellx4000");
            rtfTableString.Append(@"\clbrdrt\brdrs\clbrdrl\brdrs\clbrdrb\brdrs\clbrdrr\brdrs"); // Bottom border
            rtfTableString.Append(@"\cellx7000");

            rtfTableString.Append(@"{\fs30\b Expenditures}\intbl\cell");
            rtfTableString.Append(@" ");
            rtfTableString.Append(@"\intbl\cell");
            rtfTableString.Append(@"\row");
            foreach (var e in expenditures)
            {
                rtfTableString.Append(@"\trowd\trgaph144");
                rtfTableString.Append(@"\clbrdrt\brdrs\clbrdrl\brdrs\clbrdrb\brdrs\clbrdrr\brdrs"); // Top border
                rtfTableString.Append(@"\cellx4000");
                rtfTableString.Append(@"\clbrdrt\brdrs\clbrdrl\brdrs\clbrdrb\brdrs\clbrdrr\brdrs"); // Bottom border
                rtfTableString.Append(@"\cellx7000");

                rtfTableString.Append(@"" + e.name + @"\intbl\cell");
                rtfTableString.Append(@" ");
                rtfTableString.Append(@"" + e.amount + @"\intbl\cell");

                rtfTableString.Append(@"\row");
            }

            //total expenditures
            rtfTableString.Append(@"\trowd\trgaph144");
            rtfTableString.Append(@"\clbrdrt\brdrs\clbrdrl\brdrs\clbrdrb\brdrs\clbrdrr\brdrs"); // Top border
            rtfTableString.Append(@"\cellx4000");
            rtfTableString.Append(@"\clbrdrt\brdrs\clbrdrl\brdrs\clbrdrb\brdrs\clbrdrr\brdrs"); // Bottom border
            rtfTableString.Append(@"\cellx7000");

            rtfTableString.Append(@"{\fs30\b Total}\intbl\cell");
            rtfTableString.Append(@" ");
            rtfTableString.Append(@"" + totalExpenditures + @"\intbl\cell");
            rtfTableString.Append(@"\row");

            //blank
            rtfTableString.Append(@"\trowd\trgaph144");
            rtfTableString.Append(@"\clbrdrt\brdrs\clbrdrl\brdrs\clbrdrb\brdrs\clbrdrr\brdrs"); // Top border
            rtfTableString.Append(@"\cellx4000");
            rtfTableString.Append(@"\clbrdrt\brdrs\clbrdrl\brdrs\clbrdrb\brdrs\clbrdrr\brdrs"); // Bottom border
            rtfTableString.Append(@"\cellx7000");

            rtfTableString.Append(@" \intbl\cell");
            rtfTableString.Append(@" ");
            rtfTableString.Append(@" \intbl\cell");
            rtfTableString.Append(@"\row");

            //operating profit
            rtfTableString.Append(@"\trowd\trgaph144");
            rtfTableString.Append(@"\clbrdrt\brdrs\clbrdrl\brdrs\clbrdrb\brdrs\clbrdrr\brdrs"); // Top border
            rtfTableString.Append(@"\cellx4000");
            rtfTableString.Append(@"\clbrdrt\brdrs\clbrdrl\brdrs\clbrdrb\brdrs\clbrdrr\brdrs"); // Bottom border
            rtfTableString.Append(@"\cellx7000");

            rtfTableString.Append(@"{\fs30\b Operating Profit}\intbl\cell");
            rtfTableString.Append(@" ");
            rtfTableString.Append(@"" + operatingProfit + @"\intbl\cell");
            rtfTableString.Append(@"\row");
            rtfTableString.Append(@"\row\pard\par");
            rtfTableString.Append(@"{\fs20\b\cf2 Note: The above figures are exclusive of VAT}");
            rtfTableString.Append(@"}");

            richTextBox1.SelectedRtf = rtfTableString.ToString();
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox1.DeselectAll();
        }

        private void SaveBt_Click(object sender, EventArgs e)
        {
            
            try
            {
                
                this.richTextBox1.SaveFile(ProjectConfig.projectPath + "\\" + "financial_statement.rtf");
                Label l = mw.Controls.Find("label4", true)[0] as Label;
                ProgressBar pbar = mw.Controls.Find("progressBar1", true)[0] as ProgressBar;
                dgp.updateProgress("financial_statement.rtf", richTextBox1.TextLength == 0 ? 0 : 1);
                l.Text = dgp.completedSteps().ToString() + " /";
                pbar.Value = dgp.completedSteps();
                l.Refresh();
                MessageBox.Show("Saved successfully !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            
        }
    }
}
