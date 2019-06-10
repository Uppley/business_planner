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
using Microsoft.Office.Interop.Word;
using Microsoft.Office;
using System.Windows.Forms;
using Rectangle = System.Drawing.Rectangle;
using Point = System.Drawing.Point;
using Application = System.Windows.Forms.Application;
using BusinessPlanner.Utility;
using System.Diagnostics;
using Newtonsoft.Json;
using Task = System.Threading.Tasks.Task;

namespace BusinessPlanner
{
    public partial class MainWindow : Form
    {
        Home home = new Home();
        Dashboard dashboard;
        String project_name;
        List<DocumentItem> documentList = new List<DocumentItem>();
        
        public MainWindow()
        {
            InitializeComponent();
            ToolStripManager.Renderer = new Office2007Renderer.Office2007Renderer();
            toolStrip1.Renderer = new Office2007Renderer.Office2007Renderer();
            this.home.TopLevel = false;
            panel1.Controls.Clear();
            panel1.Controls.Add(this.home);
            this.home.parentForm = this;
            this.home.Show();
            AppUtilities.mainForm = this;
            project_name = ProjectConfig.projectSettings["Title"].ToString().ToUpper();
            label1.Text = project_name.ToUpper();
            currency.Text = ProjectConfig.projectSettings.ContainsKey("Currency")?ProjectConfig.projectSettings["Currency"].ToString():"N.A.";
            setTreeNodes();
            DocumentProgressor dpg = new DocumentProgressor();
            label4.Text = dpg.completedSteps().ToString() + " /";
            label5.Text = dpg.totalSteps().ToString() + " tasks completed";
            progressBar1.Maximum = dpg.totalSteps();
            progressBar1.Value = dpg.completedSteps();
            

        }


        public void setTreeNodes()
        {
            PopulateTreeView(0, null);
        }

        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {
            int total_files = Directory.GetFiles(ProjectConfig.projectPath).Length;
            List<TreeViewItem> filteredItems = null;
            if(ProjectConfig.projectSettings["PlanType"].ToString()== "Standard Plan")
            {
                documentList = StandardDocument.DocumentList;
                filteredItems = StandardConfig.getProjectNodes().Where(item => item.ParentID == parentId).ToList();
            }else if (ProjectConfig.projectSettings["PlanType"].ToString() == "Quick Plan")
            {
                documentList = QuickDocument.DocumentList;
                filteredItems = QuickConfig.getProjectNodes().Where(item => item.ParentID == parentId).ToList();
            }
            else if (ProjectConfig.projectSettings["PlanType"].ToString() == "Financial Plan")
            {
                documentList = FinancialDocument.DocumentList;
                filteredItems = FinancialConfig.getProjectNodes().Where(item => item.ParentID == parentId).ToList();
            }
            else
            {
                MessageBox.Show("We encountered some config issues");
            }
            
            TreeNode childNode;
            foreach (var i in filteredItems)
            {
                if (parentNode == null)
                    childNode = this.treeView1.Nodes.Add(i.Text);
                else
                    childNode = parentNode.Nodes.Add(i.Text);

                PopulateTreeView(i.ID, childNode);
            }
        }

        private void treeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            
            if (this.treeView1.SelectedNode.Text == "Home")
            {
               
                panel1.Controls.Clear();
                panel1.Controls.Add(this.home);
                this.home.Show();
            }
            else
            {
                if(this.treeView1.SelectedNode.Parent != null)
                {
                    ProjectConfig.projectFile = documentList.Find(item => item.ItemName == this.treeView1.SelectedNode.Text).DocumentName;
                    LoadingSpinner ls = new LoadingSpinner(this, AppMessages.messages["loading"]);
                    try
                    {
                        ls.show();
                        if (this.treeView1.SelectedNode.Text == "Sales Forecast Table")
                        {
                            ForecastWindow frw = new ForecastWindow(this);
                            frw.TopLevel = false;
                            panel1.Controls.Clear();
                            panel1.Controls.Add(frw);
                            frw.Show();
                        }else if(this.treeView1.SelectedNode.Text == "Start Up Investment")
                        {
                            StartUpWindow stw = new StartUpWindow(this);
                            stw.TopLevel = false;
                            panel1.Controls.Clear();
                            panel1.Controls.Add(stw);
                            stw.Show();
                        }
                        else if (this.treeView1.SelectedNode.Text == "Company Expenditure")
                        {
                            ExpenditureWindow stw = new ExpenditureWindow(this);
                            stw.TopLevel = false;
                            panel1.Controls.Clear();
                            panel1.Controls.Add(stw);
                            stw.Show();
                        }
                        else if (this.treeView1.SelectedNode.Text == "Analysis Table")
                        {
                            AnalysisWindow stw = new AnalysisWindow(this);
                            stw.TopLevel = false;
                            panel1.Controls.Clear();
                            panel1.Controls.Add(stw);
                            stw.Show();
                        }
                        else
                        {
                            dashboard = new Dashboard(this, ProjectConfig.projectFile);
                            dashboard.TopLevel = false;
                            panel1.Controls.Clear();
                            panel1.Controls.Add(dashboard);
                            dashboard.Show();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                    }
                    finally
                    {
                        ls.hide();
                    }
                }
                else
                {
                    SectionIntro si = new SectionIntro(this.treeView1.SelectedNode.Text);
                    si.TopLevel = false;
                    panel1.Controls.Clear();
                    panel1.Controls.Add(si);
                    si.Show();
                    
                }
                
            }
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            this.CloseForNew();
            
        }

        private void AboutVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBP abp = new AboutBP();
            abp.ShowDialog();
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(AppMessages.messages["exit_body"],AppMessages.messages["exit_head"],MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                this.saveProjectBeforeClose();
                this.Dispose();
            }  

        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            var confirmResult = MessageBox.Show(AppMessages.messages["exit_body"], AppMessages.messages["exit_head"], MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                this.saveProjectBeforeClose();
                e.Cancel = false;
                this.Dispose();
            }
            else
            {
                e.Cancel = true;
                return;
            }
        }

        private void saveProjectBeforeClose()
        {
            LoadingSpinner ls = new LoadingSpinner(this,AppMessages.messages["project_save"]);
            try
            {
                ls.show();
                saveProgress();
                string dPath = ProjectConfig.projectPath.Replace("~temp_", "");
                ProjectLoader.save(dPath, ProjectConfig.projectPath);
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message);
            }
            finally
            {
                ls.hide();
            }
        }

        private void saveProgress()
        {
            BPSettings bp = new BPSettings();
            var set_path = ProjectConfig.projectPath + "//" + "settings.json";
            try
            {
                var se = bp.ReadSettings(set_path);
                var i = se.FindIndex(x => x.property == "progress");
                se[i].value = ProjectConfig.projectSettings["progress"];
                using (StreamWriter file = File.CreateText(set_path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, se);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message);
            }
            
        }

        private void ToolStripButton21_Click(object sender, EventArgs e)
        {
            DashboardHome dh = new DashboardHome(this);
            dh.TopLevel = false;
            panel1.Controls.Clear();
            panel1.Controls.Add(dh);
            dh.Dock = DockStyle.Fill;
            dh.Show();
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CloseForNew();
            
        }

        private void MSWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string mssg = "";
            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog1.DefaultExt = "docx";
            saveFileDialog1.Filter = "docx files (*.docx)|*.docx|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                RichTextBox rtb1 = new RichTextBox();
                RichTextBox rtb2 = new RichTextBox();

                List<DocumentItem> allFiles = documentList.FindAll(x=>x.Ftype=="rtf" && File.Exists(ProjectConfig.projectPath + "//" + x.DocumentName)).OrderBy(x => x.Seq).ToList();
                foreach(DocumentItem doc in allFiles)
                {
                    rtb1.LoadFile(ProjectConfig.projectPath+"\\"+doc.DocumentName);
                    rtb1.SelectAll();
                    rtb1.Copy();
                    rtb2.SelectionFont = new System.Drawing.Font("Arial", 18, FontStyle.Bold |FontStyle.Underline);
                    rtb2.AppendText("\n\n"+doc.Seq+". "+doc.ItemName+"\n\n");
                    rtb2.SelectionFont = new System.Drawing.Font(rtb1.Font, FontStyle.Regular);
                    rtb2.Paste();
                }
                ExportGenerator document = new ExportGenerator();
                document.open();
                document.generateCoverPage(project_name);
                document.getFooterWithPageNumber();
                LoadingSpinner ls = new LoadingSpinner(this,AppMessages.messages["exporting"]);
                try
                {
                    ls.show();
                    System.Windows.Forms.Application.DoEvents();
                    Clipboard.SetText(rtb2.Rtf, TextDataFormat.Rtf);
                    rtb1.Dispose();
                    rtb2.Dispose();
                    document.getContent();
                    object filename = saveFileDialog1.FileName;
                    document.saveAsWord(filename);
                    document.close();
                    mssg = AppMessages.messages["export_success"];
                    ls.hide();
                }
                catch (Exception ex)
                {
                    mssg=ex.Message;
                    ls.hide();
                }
                finally
                {
                    MessageBox.Show(mssg);
                    
                    document = null;
                    Clipboard.Clear();
                    GC.Collect();
                }
                
            }
            
        }

        private void AdobePDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog2.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog2.DefaultExt = "pdf";
            saveFileDialog2.Filter = "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
            if (saveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                RichTextBox rtb1 = new RichTextBox();
                RichTextBox rtb2 = new RichTextBox();
                List<DocumentItem> allFiles = documentList.FindAll(x => x.Ftype == "rtf" && File.Exists(ProjectConfig.projectPath + "//" + x.DocumentName)).OrderBy(x => x.Seq).ToList();
                foreach (DocumentItem doc in allFiles)
                {
                    rtb1.LoadFile(ProjectConfig.projectPath + "\\" + doc.DocumentName);
                    rtb1.SelectAll();
                    rtb1.Copy();
                    rtb2.SelectionFont = new System.Drawing.Font("Arial", 18, FontStyle.Bold | FontStyle.Underline);
                    rtb2.AppendText("\n\n" + doc.Seq + ". " + doc.ItemName + "\n\n");
                    rtb2.SelectionFont = new System.Drawing.Font(rtb1.Font, FontStyle.Regular);
                    rtb2.Paste();
                }
                ExportGenerator document = new ExportGenerator();
                document.open();
                document.generateCoverPage(project_name);
                document.getFooterWithPageNumber();
                LoadingSpinner ls = new LoadingSpinner(this,AppMessages.messages["exporting"]);
                try
                {
                    ls.show();
                    Clipboard.SetText(rtb2.Rtf, TextDataFormat.Rtf);
                    document.getContent();
                    rtb1.Dispose();
                    rtb2.Dispose();
                    object filename = saveFileDialog2.FileName;
                    object fileformat = WdSaveFormat.wdFormatPDF;
                    document.saveAsPdf(filename);
                    document.close();
                    
                    MessageBox.Show(AppMessages.messages["export_success"]);
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    ls.hide();
                    document = null;
                    GC.Collect();
                }
                
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.Filter = "BUPX Files (*.bupx;)|*.bupx;";
            string dPath = ProjectConfig.projectPath.Replace("~temp_", "")+ProjectConfig.projectExtension;
            if (opnfd.ShowDialog() == DialogResult.OK)
            {
                if (opnfd.FileName.Equals(dPath))
                {
                    MessageBox.Show(AppMessages.messages["project_active"]);
                }
                else
                {
                    
                    try
                    {
                        
                        string proPath = opnfd.FileName;
                        string tempPath = Path.Combine(Path.GetDirectoryName(opnfd.FileName), "~temp_" + opnfd.SafeFileName.Replace(ProjectConfig.projectExtension, ""));
                        this.Close();
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
                        
                    }
                }
            }
        }

        public void CloseForNew()
        {
            var confirmResult = MessageBox.Show(AppMessages.messages["new_body"], AppMessages.messages["new_head"], MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                this.Close();
                if(this.IsDisposed)
                {
                    Step1Dialog st = new Step1Dialog();
                    st.ShowDialog();
                }
                
            }
            else
            {
                return;
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadingSpinner ls = new LoadingSpinner(this,AppMessages.messages["project_save"]);
            try
            {
                ls.show();
                string dPath = ProjectConfig.projectPath.Replace("~temp_", "");
                ProjectLoader.saveOnly(dPath, ProjectConfig.projectPath);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            finally
            {
                ls.hide();
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog4.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog4.DefaultExt = "bupx";
            saveFileDialog4.Filter = "BUPX files (*.bupx)|*.bupx";
            if (saveFileDialog4.ShowDialog() == DialogResult.OK)
            {
                Debug.WriteLine(saveFileDialog4.FileName);
                LoadingSpinner ls = new LoadingSpinner(this,AppMessages.messages["project_save"]);
                try
                {
                    ls.show();
                    string dPath = saveFileDialog4.FileName;
                    ProjectLoader.saveAsOnly(dPath, ProjectConfig.projectPath);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception: " + ex.Message);
                }
                finally
                {
                    ls.hide();
                }
            }
        }

        private void ToolStripButton3_Click(object sender, EventArgs e)
        {
            LoadingSpinner ls = new LoadingSpinner(this,AppMessages.messages["project_save"]);
            try
            {
                ls.show();
                string dPath = ProjectConfig.projectPath.Replace("~temp_", "");
                ProjectLoader.saveOnly(dPath, ProjectConfig.projectPath);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            finally
            {
                ls.hide();
            }
        }

        // Show reports
        private void ToolStripButton22_Click(object sender, EventArgs e)
        {
            ReportWindow rpw = new ReportWindow();
            rpw.Show();
        }

        // Plan outline
        private void ToolStripButton5_Click(object sender, EventArgs e)
        {
            OutlineWindow ouw = new OutlineWindow();
            ouw.Show();
        }

        //Review Plan
        private void ToolStripButton20_Click(object sender, EventArgs e)
        {
            ReviewWindow rvw = new ReviewWindow();
            rvw.Show();
        }
    }
}
