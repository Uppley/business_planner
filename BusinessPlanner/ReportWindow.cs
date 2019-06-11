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

namespace BusinessPlanner
{
    public partial class ReportWindow : Form
    {
        public Dictionary<string,string> report_files = new Dictionary<string,string>();
        public ReportWindow()
        {
            InitializeComponent();
            
            if (File.Exists(Path.Combine(ProjectConfig.projectPath, "startup_table.rtf")))
                report_files.Add("Start Up Investment", "startup_table.rtf");
            
            if (File.Exists(Path.Combine(ProjectConfig.projectPath, "expenditure.rtf")))
                report_files.Add("Company Expenditure", "expenditure.rtf");

            if (File.Exists(Path.Combine(ProjectConfig.projectPath, "market_segmentation.rtf")))
                report_files.Add("Market Segmentation", "market_analysis.rtf");

            if (File.Exists(Path.Combine(ProjectConfig.projectPath, "sales_forecast_table.rtf")))
                report_files.Add("Sales Forecast", "sales_forecast_table.rtf");

            comboBox1.DataSource = new BindingSource(report_files,null);
            comboBox1.DisplayMember = "Key";
            comboBox1.ValueMember = "Value";
            Debug.WriteLine(ProjectConfig.projectPath + "\\" + comboBox1.SelectedValue);
            try
            {
                richTextBox1.LoadFile(ProjectConfig.projectPath + "\\" + comboBox1.SelectedValue);
                richTextBox1.SelectAll();
                richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
                richTextBox1.DeselectAll();
                if (richTextBox1.TextLength == 0)
                    richTextBox1.Text = "Nothing to display ! Please fill data to view report.";
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        

        private void ZoomIn_Click(object sender, EventArgs e)
        {
            if (richTextBox1.ZoomFactor < 2.25f)
                richTextBox1.ZoomFactor += 0.1f;
            else
                richTextBox1.ZoomFactor = 2.25f;
        }

        private void ZoomOut_Click(object sender, EventArgs e)
        {
            if (richTextBox1.ZoomFactor > 0.25f)
                richTextBox1.ZoomFactor -= 0.1f;
            else
                richTextBox1.ZoomFactor = 0.25f;
        }

        

        private void ComboBox1_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.LoadFile(ProjectConfig.projectPath + "\\" + comboBox1.SelectedValue);
                richTextBox1.SelectAll();
                richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
                richTextBox1.DeselectAll();
                if (richTextBox1.TextLength == 0)
                    richTextBox1.Text = "Nothing to display ! Please fill data to view report.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
