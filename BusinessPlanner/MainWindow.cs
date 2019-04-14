using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Office2007Renderer;
using System.Windows.Forms;

namespace BusinessPlanner
{
    public partial class MainWindow : Form
    {
        Dashboard dashboard;
        Home home = new Home();
        
        public MainWindow()
        {
            InitializeComponent();
            ToolStripManager.Renderer = new Office2007Renderer.Office2007Renderer();
            toolStrip1.Renderer = new Office2007Renderer.Office2007Renderer();
            this.home.TopLevel = false;
            panel1.Controls.Clear();
            panel1.Controls.Add(this.home);
            this.home.Show();
            
        }

        private void treeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
           
            if (this.treeView1.SelectedNode.Name == "home")
            {
               
                panel1.Controls.Clear();
                panel1.Controls.Add(this.home);
                this.home.Show();
            }
            else
            {
                if(this.treeView1.SelectedNode.Parent != null)
                {
                    this.dashboard = new Dashboard(this.treeView1.SelectedNode.Name);
                    this.dashboard.TopLevel = false;
                    panel1.Controls.Clear();
                    panel1.Controls.Add(this.dashboard);
                    this.dashboard.Show();
                }
                else
                {
                    panel1.Controls.Clear();
                    Label lb = new Label();
                    lb.AutoSize = true;
                    lb.Text = this.treeView1.SelectedNode.Text;
                    lb.ForeColor = Color.Black;
                    lb.Font = new Font(FontFamily.GenericSansSerif,25.0F, FontStyle.Bold);
                    panel1.Controls.Add(lb);
                }
                
            }
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            Step1Dialog st = new Step1Dialog();
            st.ShowDialog();
        }

        private void AboutVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBP abp = new AboutBP();
            abp.ShowDialog();
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to exit this program ? If you are sure then click the Yes button else click No button.","Exit Business Planner",MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                this.Dispose();
            }  

        }
    }
}
