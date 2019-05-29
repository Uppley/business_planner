using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BusinessPlanner.Utility
{
    class ChartGenerator
    {
        public void generatePieChart(DataGridView dgv,string fname,string title)
        {
            List<string> names = new List<string>();
            List<float> values = new List<float>();
            for (int rows = 0; rows < dgv.Rows.Count - 1; rows++)
            {
                names.Add(dgv.Rows[rows].Cells[0].Value.ToString());
                values.Add(float.Parse(dgv.Rows[rows].Cells[1].Value.ToString()));
                //expenditures.Add(new ExpenditureItem() { name = dgv.Rows[rows].Cells[0].Value.ToString(), amount = float.Parse(dgv.Rows[rows].Cells[1].Value.ToString()) });
            }
            Chart chart = new Chart();
            ChartArea chartArea1 = new ChartArea();
            chartArea1.Name = "ChartArea1";
            chart.ChartAreas.Add(chartArea1);
            chart.Height = 500;
            chart.Width = 500;
            chart.Series.Add("series");
            
            chart.Series["series"].ChartType = SeriesChartType.Pie;
            chart.Titles.Add(title);
            int x = 0;
            foreach (var v in values)
            {
                chart.Series["series"].Points.AddXY(names[x], v);
                x++;
            }
           
            chart.SaveImage(fname, ChartImageFormat.Png);
            chart.Invalidate();
            
        }

        public void generateBarChart(DataGridView dgv, string fname, string title)
        {
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
            List<float> values = new List<float>() { col1, col2, col3, col4, col5, col6 };
            Chart chart = new Chart();
            ChartArea chartArea1 = new ChartArea();
            chartArea1.Name = "ChartArea1";
            chart.ChartAreas.Add(chartArea1);
            chart.Height = 500;
            chart.Width = 800;
            chart.Series.Add("series");

            chart.Series["series"].ChartType = SeriesChartType.Bar;
            chart.Titles.Add(title);
            int x = 0;
            foreach (var v in values)
            {
                chart.Series["series"].Points.AddXY(months[x], v);
                x++;
            }

            chart.SaveImage(fname, ChartImageFormat.Png);
            chart.Invalidate();
        }

        public void ImageToRtf(string fname,string name)
        {
            try
            {
                var img = Image.FromFile(name);
                Clipboard.SetImage(img);
                img.Dispose();
                RichTextBox rtb = new RichTextBox();
                rtb.LoadFile(ProjectConfig.projectPath + "\\" + fname);
                rtb.AppendText("\n\n" );
                rtb.Paste();
                rtb.SaveFile(ProjectConfig.projectPath + "\\" + fname);
                rtb.Dispose();
                if(File.Exists(name))
                    File.Delete(name);
                Clipboard.Clear();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
}
