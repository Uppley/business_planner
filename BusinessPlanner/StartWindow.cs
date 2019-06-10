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
            if(ProjectConfig.projectList().Count() > 0)
            {
                foreach (var p in ProjectConfig.projectList())
                {
                    string[] pnames = p.Split(new char[] { '\\' });
                    string pname = pnames[pnames.Count() - 1];
                    if (i < 4)
                    {
                        LinkLabel linkLabel = new LinkLabel();
                        linkLabel.Dock = DockStyle.Fill;
                        linkLabel.Font = new Font("Arial", 10, FontStyle.Regular);

                        linkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        linkLabel.Text = pname;
                        linkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(this.loadProject);
                        tableLayoutPanel2.Controls.Add(linkLabel, 1, i);
                        i++;
                    }
                    else
                    {
                        break;
                    }

                }
                
            }
            else
            {
                Label label = new Label();
                label.Dock = DockStyle.Fill;
                label.Font = new Font("Arial", 10, FontStyle.Regular);

                label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                label.Text = "No previous projects";
                tableLayoutPanel2.Controls.Add(label, 1, i);
            }
            if (ProjectConfig.projectList().Count() <= 3)
            {
                panel1.Visible = false;
            }

        }

        private void loadProject(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnb = sender as LinkLabel;
            _LoadingDialog ld = new _LoadingDialog(AppMessages.messages["project_loading"]);
            try
            {
                Task.Factory.StartNew(() => {
                    ld.ShowDialog();
                });
                Application.DoEvents();
                string proPath = Path.Combine(ProjectConfig.projectBase, lnb.Text);
                string tempPath = Path.Combine(ProjectConfig.projectBase, "~temp_" + lnb.Text.Replace(ProjectConfig.projectExtension,""));
                ProjectLoader.load(proPath, tempPath);
                MainWindow mf = new MainWindow();
                mf.Show();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            finally
            {
                Invoke(new MethodInvoker(() =>
                {
                    ld.Close();
                }));
                this.Close();
            }
            
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //MainWindow mf = new MainWindow();
            Step1Dialog st = new Step1Dialog();
            //mf.Show();
            st.ShowDialog();
            this.Close();
        }

        private void LinkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.Filter = "BUPX Files (*.bupx;)|*.bupx;";
            //string dPath = ProjectConfig.projectPath.Replace("temp_", "") + ProjectConfig.projectExtension;
            if (opnfd.ShowDialog() == DialogResult.OK)
            {
               
                    _LoadingDialog ld = new _LoadingDialog(AppMessages.messages["project_loading"]);
                    try
                    {
                        Task.Factory.StartNew(() => {
                            ld.ShowDialog();
                        });
                        string proPath = opnfd.FileName;
                        string tempPath = Path.Combine(Path.GetDirectoryName(opnfd.FileName), "~temp_" + opnfd.SafeFileName.Replace(ProjectConfig.projectExtension, ""));
                        
                        ProjectLoader.load(proPath, tempPath);
                        MainWindow mf = new MainWindow();
                        mf.Show();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Exception: " + ex.Message);
                    }
                    finally
                    {
                        Invoke(new MethodInvoker(() =>
                        {
                            ld.Close();
                        }));
                        this.Close();
                }
                
            }
        }
    }
}
