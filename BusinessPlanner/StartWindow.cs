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
    public partial class StartWindow : Form
    {
        public StartWindow()
        {
            InitializeComponent();
            int i = 1;
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute,100F));
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            foreach (var p in ProjectConfig.projectList())
            {
                string[] pnames = p.Split(new char[] {'\\'});
                string pname = pnames[pnames.Count()-1];
                if(i<4)
                {
                    LinkLabel linkLabel = new LinkLabel();
                    linkLabel.Dock = DockStyle.Fill;
                    linkLabel.Font = new Font("Arial", 10, FontStyle.Regular);
                    linkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    linkLabel.Text = pname;
                    linkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(this.loadProject);
                    tableLayoutPanel2.Controls.Add(linkLabel,1,i);
                    i++;
                }
                else
                {
                    break;
                }
                
            }
        }

        private void loadProject(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnb = sender as LinkLabel;
            ProjectConfig.projectPath = Path.Combine(ProjectConfig.projectBase,lnb.Text);
            MainWindow mf = new MainWindow();
            mf.Show();
            this.Close();
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MainWindow mf = new MainWindow();
            Step1Dialog st = new Step1Dialog();
            mf.Show();
            st.ShowDialog();
            this.Close();
        }
    }
}
