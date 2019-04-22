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
                    lb.ForeColor = Color.MediumBlue;
                    lb.Font = new Font(FontFamily.GenericSansSerif,35.0F, FontStyle.Bold);
                    lb.Left = 50;
                    lb.Top = 50;
                    Label lb2 = new Label();
                    //lb2.AutoSize = true;
                    lb2.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer cursus dui id eros condimentum feugiat. Praesent et mauris nec nibh convallis aliquet in id magna. Quisque id dui vitae ipsum malesuada vulputate. Ut eu pulvinar enim. Duis dolor sapien, rutrum eget ultrices convallis, aliquam eu ex. Vestibulum eget erat quis purus pulvinar ultricies ac vel ipsum. Suspendisse nibh erat, consequat vitae consectetur sed, vehicula vel quam. Vivamus bibendum arcu tortor, sit amet porta tellus sollicitudin eu. Sed pharetra a ante sed tincidunt. In accumsan velit mauris, vitae placerat leo gravida nec. Cras condimentum massa et faucibus posuere.\n\n\nSuspendisse tempus fringilla lectus ac feugiat. Quisque eget elementum lorem. In euismod finibus dui et mollis. Sed auctor tincidunt urna vel dignissim. Vivamus sed leo nec mauris porta interdum nec et nibh.";
                    lb2.Top = 150;
                    lb2.Left = 50;
                    lb2.Width = 1200;
                    lb2.Height = 700;
                    panel1.Controls.Add(lb);
                    panel1.Controls.Add(lb2);
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
            var confirmResult = MessageBox.Show("Are you sure to quit Business Planner ?\nIf you are sure then click the Yes button else click No button.","Exit Business Planner",MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                this.Dispose();
            }  

        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to quit Business Planner ?\nIf you are sure then click the Yes button else click No button.", "Exit Business Planner", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                e.Cancel = false;
                this.Dispose();
            }
            else
            {
                e.Cancel = true;
                return;
            }
        }
    }
}
