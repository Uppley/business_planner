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
using System.Drawing.Printing;
using RichTextBoxPrintCtrl;
using BusinessPlanner.Partials;
using BP.CurrencyFetcher;
using System.Configuration;
using Font = System.Drawing.Font;

namespace BusinessPlanner
{
    public partial class MainWindow : Form
    {
        Home home = new Home();
        Dashboard dashboard;
        String project_name;
        List<DocumentItem> documentList = new List<DocumentItem>();
        int checkprint;
        int splitDist=355;
        int meeting_count;
        public MainWindow()
        {
            InitializeComponent();
            ToolStripManager.Renderer = new Office2007Renderer.Office2007Renderer();
            toolBar.Renderer = new Office2007Renderer.Office2007Renderer();
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
            currencyHandler();
            if(dashboard == null)
            {
                activateEditMenus(0);
            }
            //splitContainer1.SplitterDistance = splitDist;
            meeting_count = DatabaseReader.getMeetingCount();
            button1.Text =  meeting_count==1 ? meeting_count+" Meeting Today" : meeting_count + " Meetings Today";
            if ( meeting_count > 0)
            {
                button1.ImageIndex = 1;
                button1.Enabled = true;
            }
            else
            {
                button1.ImageIndex = 0;
                button1.Enabled = false;
            }
        }

        private void currencyHandler()
        {
            if (currency.Text != "N.A.")
            {
                fromCurr.Text = "1 " + currency.Text + " = ";
                getCurrencyRateAsync();
            }
        }

        private async void getCurrencyRateAsync()
        {
            CurrencyFetcher currencyFetcher = new CurrencyFetcher();
            string res = await currencyFetcher.GetExchangeRate(Properties.Settings.Default.currency_api, currency.Text, "USD");
            if(res=="error")
            {
                toCurr.Text = "undefined";
            }
            else
            {
                toCurr.Text = res+" USD";
            }
        }

        private void activateEditMenus(int v)
        {
            ToolStripItemCollection tsic = ((ToolStripMenuItem)editToolStripMenuItem).DropDownItems;

            foreach (ToolStripItem tsi in tsic)
            {
                if(v==1)
                {
                    tsi.Enabled = true;
                }
                else
                {
                    tsi.Enabled = false;
                }
            }
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
                activateEditMenus(0);
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
                            activateEditMenus(0);
                            frw.TopLevel = false;
                            panel1.Controls.Clear();
                            panel1.Controls.Add(frw);
                            frw.Show();
                            
                        }
                        else if(this.treeView1.SelectedNode.Text == "Start Up Investment")
                        {
                            StartUpWindow stw = new StartUpWindow(this);
                            activateEditMenus(0);
                            stw.TopLevel = false;
                            panel1.Controls.Clear();
                            panel1.Controls.Add(stw);
                            stw.Show();
                            
                        }
                        else if (this.treeView1.SelectedNode.Text == "Company Expenditure")
                        {
                            ExpenditureWindow stw = new ExpenditureWindow(this);
                            activateEditMenus(0);
                            stw.TopLevel = false;
                            panel1.Controls.Clear();
                            panel1.Controls.Add(stw);
                            stw.Show();
                            
                        }
                        else if (this.treeView1.SelectedNode.Text == "Analysis Table")
                        {
                            AnalysisWindow stw = new AnalysisWindow(this);
                            activateEditMenus(0);
                            stw.TopLevel = false;
                            panel1.Controls.Clear();
                            panel1.Controls.Add(stw);
                            stw.Show();
                            
                        }
                        else if(this.treeView1.SelectedNode.Text== "Financial Statement")
                        {
                            FinancialWindow fiw = new FinancialWindow(this);
                            activateEditMenus(0);
                            fiw.TopLevel = false;
                            panel1.Controls.Clear();
                            panel1.Controls.Add(fiw);
                            fiw.Show();
                        }
                        else
                        {
                            dashboard = new Dashboard(this, ProjectConfig.projectFile);
                            activateEditMenus(1);
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
                    activateEditMenus(0);
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
            Clipboard.Clear();
            string mssg = "";
            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog1.DefaultExt = "docx";
            saveFileDialog1.Filter = "docx files (*.docx)|*.docx|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
                RichTextBox rtb1 = new RichTextBox();
                RichTextBox rtb2 = new RichTextBox();
                int seq = 1;
                List<DocumentItem> allFiles = documentList.FindAll(x=>x.Ftype=="rtf" && File.Exists(ProjectConfig.projectPath + "//" + x.DocumentName)).OrderBy(x => x.Seq).ToList();
                foreach(DocumentItem doc in allFiles)
                {
                    rtb1.LoadFile(ProjectConfig.projectPath+"\\"+doc.DocumentName);
                    rtb1.SelectAll();
                    rtb1.Copy();
                    rtb2.SelectionFont = new System.Drawing.Font("Arial", 18, FontStyle.Bold |FontStyle.Underline);
                    rtb2.AppendText("\n\n"+seq+". "+doc.ItemName+"\n\n");
                    rtb2.SelectionFont = new System.Drawing.Font(rtb1.Font, FontStyle.Regular);
                    rtb2.Paste();
                    Clipboard.Clear();
                    seq += 1;
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
                    document.setContent();
                    
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
            Clipboard.Clear();
            saveFileDialog2.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog2.DefaultExt = "pdf";
            saveFileDialog2.Filter = "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
            if (saveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                
                RichTextBox rtb1 = new RichTextBox();
                RichTextBox rtb2 = new RichTextBox();
                int seq = 1;
                List<DocumentItem> allFiles = documentList.FindAll(x => x.Ftype == "rtf" && File.Exists(ProjectConfig.projectPath + "//" + x.DocumentName)).OrderBy(x => x.Seq).ToList();
                foreach (DocumentItem doc in allFiles)
                {
                    rtb1.LoadFile(ProjectConfig.projectPath + "\\" + doc.DocumentName);
                    rtb1.SelectAll();
                    rtb1.Copy();
                    rtb2.SelectionFont = new System.Drawing.Font("Arial", 18, FontStyle.Bold | FontStyle.Underline);
                    rtb2.AppendText("\n\n" + seq + ". " + doc.ItemName + "\n\n");
                    rtb2.SelectionFont = new System.Drawing.Font(rtb1.Font, FontStyle.Regular);
                    rtb2.Paste();
                    Clipboard.Clear();
                    seq += 1;
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
                    document.setContent();
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

        //Print preview
        private void ToolStripButton2_Click(object sender, EventArgs e)
        {

            printPreviewDialog1.Document = printDocument1;
            
            printPreviewDialog1.ShowDialog();
            
        }

        private void ToolStripButton4_Click(object sender, EventArgs e)
        {
            printDocument();
        }

        private void PrintDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            checkprint = 0;
        }

        private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Clipboard.Clear();
            RichTextBox rtb1 = new RichTextBox();
            RichTextBoxPrintCtrl.RichTextBoxPrintCtrl.RichTextBoxPrintCtrl rtb2 = new RichTextBoxPrintCtrl.RichTextBoxPrintCtrl.RichTextBoxPrintCtrl();
            int seq = 1;
            List<DocumentItem> allFiles = documentList.FindAll(z => z.Ftype == "rtf" && File.Exists(ProjectConfig.projectPath + "//" + z.DocumentName)).OrderBy(z => z.Seq).ToList();
            foreach (DocumentItem doc in allFiles)
            {
                rtb1.LoadFile(ProjectConfig.projectPath + "\\" + doc.DocumentName);
                rtb1.SelectAll();
                rtb1.Copy();
                rtb2.SelectionFont = new System.Drawing.Font("Arial", 18, FontStyle.Bold | FontStyle.Underline);
                rtb2.AppendText("\n\n" + seq + ". " + doc.ItemName + "\n\n");
                rtb2.SelectionFont = new System.Drawing.Font(rtb1.Font, FontStyle.Regular);
                rtb2.Paste();
                Clipboard.Clear();
                seq += 1;
            }
            checkprint = rtb2.Print(checkprint, rtb2.TextLength, e);

            if (checkprint < rtb2.TextLength)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
        }

        private void ShowToolbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var temp = (ToolStripMenuItem)sender;
            if(temp.CheckState==CheckState.Checked)
            {
                temp.CheckState = CheckState.Unchecked;
                toolStripContainer1.Hide();
                this.Refresh();
            }
            else
            {
                temp.CheckState = CheckState.Checked;
                toolStripContainer1.Show();
                this.Refresh();
            }
        }

        private void ShowTasksMenu_Click(object sender, EventArgs e)
        {
            
            var temp = (ToolStripMenuItem)sender;
            if (temp.CheckState == CheckState.Checked)
            {
                temp.CheckState = CheckState.Unchecked;
                splitContainer1.Panel1.Hide();
                splitContainer1.SplitterDistance = 0;
                
            }
            else
            {
                temp.CheckState = CheckState.Checked;
                splitContainer1.SplitterDistance = splitDist;
                splitContainer1.Panel1.Show();
            }
        }

        private void ReportsMenu_Click(object sender, EventArgs e)
        {
            ReportWindow rep = new ReportWindow();
            rep.Show();
        }

        private void OutlineMenu_Click(object sender, EventArgs e)
        {
            OutlineWindow ouw = new OutlineWindow();
            ouw.Show();
        }

        private void PlanReviewMenu_Click(object sender, EventArgs e)
        {
            ReviewWindow rew = new ReviewWindow();
            rew.Show();
        }

        private void CurrencyRateMenu_Click(object sender, EventArgs e)
        {
            var temp = (ToolStripMenuItem)sender;
            if (temp.CheckState == CheckState.Checked)
            {
                temp.CheckState = CheckState.Unchecked;
                currencyPanel.Hide();

            }
            else
            {
                temp.CheckState = CheckState.Checked;
                currencyPanel.Show();
            }
        }

        private void ModifyProjectNameMenu_Click(object sender, EventArgs e)
        {
            _ProjectNameDialog pnd = new _ProjectNameDialog();
            pnd.oldName = label1.Text;
            DialogResult pnd_data = pnd.ShowDialog(this);
            if (pnd_data == DialogResult.OK)
            {
                
                label1.Text = pnd.newName.ToUpper();
                
            }
        }

        //undo
        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {

            CustomEditor.undo(dashboard.richTextBox1);
        }

        //redo
        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            CustomEditor.redo(dashboard.richTextBox1);
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomEditor.Cut(dashboard.richTextBox1);
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomEditor.Copy(dashboard.richTextBox1);
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomEditor.Paste(dashboard.richTextBox1);
        }


        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomEditor.selectAll(dashboard.richTextBox1);
        }

        private void DeselectAll_Click(object sender, EventArgs e)
        {
            CustomEditor.deselectAll(dashboard.richTextBox1);
        }

        private void FindToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findWord();
            
        }

        

        private void ReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ReplaceDialog hpl = new _ReplaceDialog();
            DialogResult hpl_data = hpl.ShowDialog(this);
            if (hpl_data == DialogResult.OK)
            {
                dashboard.richTextBox1.Text = dashboard.richTextBox1.Text.Replace(hpl.searchTerm, hpl.newTerm);
            }
        }

        private void ModifyCurrency_Click(object sender, EventArgs e)
        {
            _CurrencyDialog cnd = new _CurrencyDialog();
            cnd.oldName = currency.Text;
            DialogResult pnd_data = cnd.ShowDialog(this);
            if (pnd_data == DialogResult.OK)
            {

                currency.Text = cnd.newName.ToUpper();
                currencyHandler();
            }
        }

        private void AddNewEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MeetingDialog med = new MeetingDialog();
            med.Show();
        }

        private void AddNewContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ContactDialog cod = new ContactDialog();
            cod.Show();
        }

        private void ViewContactsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewContact vco = new viewContact();
            vco.Show();
        }

        private void ViewEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewMeeting vem = new viewMeeting();
            vem.Show();
        }

        private void SpellCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var temp = (ToolStripMenuItem)sender;
            if (temp.CheckState == CheckState.Checked)
            {
                temp.CheckState = CheckState.Unchecked;
                dashboard.Dispose();
                dashboard = new Dashboard(this, ProjectConfig.projectFile);
                activateEditMenus(1);
                dashboard.TopLevel = false;
                panel1.Controls.Clear();
                panel1.Controls.Add(dashboard);
                dashboard.Show();
                dashboard.spellingWorker1.IsEditorSpellingEnabled = false;
                dashboard.spellingWorker1.Editor.IsSpellingEnabled = false;

            }
            else
            {
                temp.CheckState = CheckState.Checked;
                dashboard.Dispose();
                dashboard = new Dashboard(this, ProjectConfig.projectFile);
                activateEditMenus(1);
                dashboard.TopLevel = false;
                panel1.Controls.Clear();
                panel1.Controls.Add(dashboard);
                dashboard.Show();
                dashboard.spellingWorker1.IsEditorSpellingEnabled = true;
                dashboard.spellingWorker1.Editor.IsSpellingEnabled = true;
            }
        }

        private void FAQToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ResourceWindow rew = new ResourceWindow();
            rew.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            viewMeeting vem = new viewMeeting(DateTime.Now.Date.ToString());
            vem.Show();
        }

        private void ToolStripButton19_Click(object sender, EventArgs e)
        {
            ResourceWindow rsw = new ResourceWindow();
            rsw.Show();
        }

        private void findWord()
        {
            _FindDialog hpl = new _FindDialog();
            DialogResult hpl_data = hpl.ShowDialog(this);
            if (hpl_data == DialogResult.OK)
            {
                if (hpl.searchTerm.Length > 0)
                {
                    dashboard.findLength = hpl.searchTerm.Length;
                    int startIndex = 0;
                    while (startIndex < dashboard.richTextBox1.TextLength)
                    {
                        int wordStartIndex = dashboard.richTextBox1.Find(hpl.searchTerm, startIndex, RichTextBoxFinds.None);
                        if (wordStartIndex != -1)
                        {
                            dashboard.richTextBox1.SelectionStart = wordStartIndex;
                            dashboard.richTextBox1.SelectionLength = hpl.searchTerm.Length;
                            dashboard.richTextBox1.SelectionBackColor = Color.Yellow;
                            dashboard.findPosition.Add(wordStartIndex);
                        }
                        else
                            break;
                        startIndex += wordStartIndex + hpl.searchTerm.Length;
                    }

                }
            }
        }

        private void printDocument()
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control)
            {
                var kp = e.KeyCode;
                switch (kp)
                {
                    case Keys.Z:
                        CustomEditor.undo(dashboard.richTextBox1);
                        break;
                    case Keys.Y:
                        CustomEditor.redo(dashboard.richTextBox1);
                        break;
                    case Keys.D:
                        CustomEditor.deselectAll(dashboard.richTextBox1);
                        break;
                    case Keys.S:
                        dashboard.saveDocument();
                        break;
                    case Keys.B:
                        CustomEditor.makeBold(dashboard.richTextBox1);
                        break;
                    case Keys.I:
                        CustomEditor.makeItalic(dashboard.richTextBox1);
                        break;
                    case Keys.U:
                        CustomEditor.makeUnderline(dashboard.richTextBox1);
                        break;
                    case Keys.P:
                        printDocument();
                        break;
                    case Keys.N:
                        this.CloseForNew();
                        break;
                    case Keys.Oemplus:
                        CustomEditor.zoomIn(dashboard.richTextBox1);
                        break;
                    case Keys.OemMinus:
                        CustomEditor.zoomOut(dashboard.richTextBox1);
                        break;
                    case Keys.F:
                        findWord();
                        break;
                    case Keys.A:
                        CustomEditor.selectAll(dashboard.richTextBox1);
                        break;
                    case Keys.D0:
                        dashboard.richTextBox1.ZoomFactor = 1.0f;
                        break;
                    case Keys.NumPad0:
                        dashboard.richTextBox1.ZoomFactor = 1.0f;
                        break;
                    default:
                        break;
                }
            }
            
            
        }

        
    }
}
