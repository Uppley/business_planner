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

namespace BusinessPlanner
{
    public partial class MainWindow : Form
    {
        Dashboard dashboard;
        Home home = new Home();
        //string[] documents;
        public MainWindow()
        {
            InitializeComponent();
            ToolStripManager.Renderer = new Office2007Renderer.Office2007Renderer();
            toolStrip1.Renderer = new Office2007Renderer.Office2007Renderer();
            this.home.TopLevel = false;
            panel1.Controls.Clear();
            panel1.Controls.Add(this.home);
            this.home.Show();
            Utilities.mainForm = this;
           // documents = extractDocuments();
            if(ProjectConfig.projectPath != null)
            {
                string[] pn = ProjectConfig.projectPath.Split(new char[] { '\\' });
                string pname = pn[pn.Count() - 1];
                pname = pname.Replace("temp_", "");
                label1.Text = pname;
            }
            setTreeNodes();
        }

        private string[] extractDocuments()
        {
            List<string> filenames= new List<String>();
            foreach(var s in Directory.GetFiles(ProjectConfig.projectPath))
            {
                string[] fnames = s.Split(new char[] { '\\' });
                string fname = fnames[fnames.Count() - 1];
                Debug.WriteLine(fname);
                filenames.Add(fname);
            }
           
            return filenames.ToArray();
        }

        public void setTreeNodes()
        {
            PopulateTreeView(0, null);
        }

        public void updateTreeNodes()
        {
            treeView1.Nodes.Clear();
            PopulateTreeView(0, null);
        }

        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {
            
            var filteredItems = AppConfig.getProjectNodes().Where(item =>item.ParentID == parentId);
            TreeNode childNode;
            foreach (var i in filteredItems.ToList())
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
                    ProjectConfig.projectFile = DocumentRecord.DocumentList.Find(item => item.ItemName == this.treeView1.SelectedNode.Text).DocumentName;
                    this.dashboard = new Dashboard(ProjectConfig.projectFile);
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
                    lb.ForeColor = Color.RoyalBlue;
                    lb.Font = new System.Drawing.Font(FontFamily.GenericSansSerif,35.0F, FontStyle.Bold);
                    lb.Left = 50;
                    lb.Top = 50;
                    Label lb1 = new Label();
                    lb1.Left = 50;
                    lb1.Top = 120;
                    lb1.Text = "";
                    lb1.BorderStyle = BorderStyle.Fixed3D;
                    lb1.AutoSize = false;
                    lb1.Width = 1190;
                    lb1.Height = 2;
                    lb1.BackColor = Color.Red;
                    Label lb2 = new Label();
                    lb2.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer cursus dui id eros condimentum feugiat. Praesent et mauris nec nibh convallis aliquet in id magna. Quisque id dui vitae ipsum malesuada vulputate. Ut eu pulvinar enim. Duis dolor sapien, rutrum eget ultrices convallis, aliquam eu ex. Vestibulum eget erat quis purus pulvinar ultricies ac vel ipsum. Suspendisse nibh erat, consequat vitae consectetur sed, vehicula vel quam. Vivamus bibendum arcu tortor, sit amet porta tellus sollicitudin eu. Sed pharetra a ante sed tincidunt. In accumsan velit mauris, vitae placerat leo gravida nec. Cras condimentum massa et faucibus posuere.\n\n\nSuspendisse tempus fringilla lectus ac feugiat. Quisque eget elementum lorem. In euismod finibus dui et mollis. Sed auctor tincidunt urna vel dignissim. Vivamus sed leo nec mauris porta interdum nec et nibh.";
                    lb2.Font = new System.Drawing.Font(FontFamily.GenericSerif, 12.0F, FontStyle.Regular);
                    lb2.Top = 150;
                    lb2.Left = 60;
                    lb2.Width = 1200;
                    lb2.Height = 700;
                    panel1.Controls.Add(lb);
                    panel1.Controls.Add(lb1);
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
            _LoadingDialog ld = new _LoadingDialog(AppMessages.messages["project_save"]);
            try
            {
                ld.Show();
                Application.DoEvents();
                string dPath = ProjectConfig.projectPath.Replace("temp_", "");
                DocumentLoader.save(dPath, ProjectConfig.projectPath);
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message);
            }
            finally
            {
                ld.Close();
            }
        }

        private void ToolStripButton21_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(this.home);
            this.home.Show();
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Step1Dialog st = new Step1Dialog();
            st.ShowDialog();
        }

        private void MSWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog1.DefaultExt = "docx";
            saveFileDialog1.Filter = "docx files (*.docx)|*.docx|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                RichTextBox rtb = new RichTextBox();
                rtb.LoadFile("Document1.rtf");
                object missing = System.Reflection.Missing.Value;
                object Visible = true;
                object start1 = 0;
                object end1 = 0;
                ApplicationClass WordApp = new ApplicationClass();
                Document adoc = WordApp.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                Microsoft.Office.Interop.Word.Range rng = adoc.Range(ref start1, ref missing);
                _LoadingDialog ld = new _LoadingDialog(AppMessages.messages["exporting"]);
                try
                {
                    ld.Show();
                    System.Windows.Forms.Application.DoEvents();
                    Clipboard.SetText(rtb.Rtf, TextDataFormat.Rtf);
                    WordApp.Selection.Paste();
                    object filename = saveFileDialog1.FileName;
                    adoc.SaveAs(ref filename, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                    ld.Close();
                    MessageBox.Show(AppMessages.messages["export_success"]);
                }
                catch (Exception ex)
                {
                    ld.Close();
                    MessageBox.Show(ex.Message);
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
                RichTextBox rtb = new RichTextBox();
                rtb.LoadFile("Document1.rtf");
                object missing = System.Reflection.Missing.Value;
                object Visible = true;
                object start1 = 0;
                object end1 = 0;
                ApplicationClass WordApp = new ApplicationClass();
                Document adoc = WordApp.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                Microsoft.Office.Interop.Word.Range rng = adoc.Range(ref start1, ref missing);
                _LoadingDialog ld = new _LoadingDialog(AppMessages.messages["exporting"]);
                try
                {
                    ld.Show();
                    System.Windows.Forms.Application.DoEvents();
                    Clipboard.SetText(rtb.Rtf, TextDataFormat.Rtf);
                    WordApp.Selection.Paste();
                    object filename = saveFileDialog2.FileName;
                    object fileformat = WdSaveFormat.wdFormatPDF;
                    adoc.SaveAs(ref filename, ref fileformat, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                    ld.Close();
                    MessageBox.Show(AppMessages.messages["export_success"]);
                }
                catch (Exception ex)
                {
                    ld.Close();
                    MessageBox.Show(ex.Message);
                }
                
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.Filter = "BUPX Files (*.bupx;)|*.bupx;";
            string dPath = ProjectConfig.projectPath.Replace("temp_", "")+ProjectConfig.projectExtension;
            if (opnfd.ShowDialog() == DialogResult.OK)
            {
                if (opnfd.FileName.Equals(dPath))
                {
                    MessageBox.Show(AppMessages.messages["project_active"]);
                }
                else
                {
                    _LoadingDialog ld = new _LoadingDialog(AppMessages.messages["project_loading"]);
                    try
                    {
                        ld.Show();
                        Application.DoEvents();
                        string proPath = opnfd.FileName;
                        string tempPath = Path.Combine(Path.GetDirectoryName(opnfd.FileName), "temp_" + opnfd.SafeFileName.Replace(ProjectConfig.projectExtension, ""));
                        this.Close();
                        DocumentLoader.load(proPath, tempPath);
                        MainWindow mf = new MainWindow();
                        mf.Show();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Exception: " + ex.Message);
                    }
                    finally
                    {
                        ld.Close();
                    }
                }
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _LoadingDialog ld = new _LoadingDialog(AppMessages.messages["project_save"]);
            try
            {
                ld.Show();
                Application.DoEvents();
                string dPath = ProjectConfig.projectPath.Replace("temp_", "");
                DocumentLoader.saveOnly(dPath, ProjectConfig.projectPath);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            finally
            {
                ld.Close();
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
                _LoadingDialog ld = new _LoadingDialog(AppMessages.messages["project_save"]);
                try
                {
                    ld.Show();
                    Application.DoEvents();
                    string dPath = saveFileDialog4.FileName;
                    DocumentLoader.saveAsOnly(dPath, ProjectConfig.projectPath);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception: " + ex.Message);
                }
                finally
                {
                    ld.Close();
                }
            }
        }

        private void ToolStripButton3_Click(object sender, EventArgs e)
        {
            _LoadingDialog ld = new _LoadingDialog(AppMessages.messages["project_save"]);
            try
            {
                ld.Show();
                Application.DoEvents();
                string dPath = ProjectConfig.projectPath.Replace("temp_", "");
                DocumentLoader.saveOnly(dPath, ProjectConfig.projectPath);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            finally
            {
                ld.Close();
            }
        }

        /*private void TreeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            string minusPath = Application.StartupPath + Path.DirectorySeparatorChar + @"Images\minus.png";
            string plusPath = Application.StartupPath + Path.DirectorySeparatorChar + @"Images\plus.png";
            string nodePath = Application.StartupPath + Path.DirectorySeparatorChar + @"Images\node_img.png";
            Rectangle nodeRect = e.Node.Bounds;
            
            
            Point ptExpand = new Point(nodeRect.Location.X - 20, nodeRect.Location.Y + 2);
            Image expandImg = null;
            if (e.Node.IsExpanded || e.Node.Nodes.Count < 1)
                expandImg = Image.FromFile(minusPath);
            else
                expandImg = Image.FromFile(plusPath);
            Graphics g = Graphics.FromImage(expandImg);
            IntPtr imgPtr = g.GetHdc();
            g.ReleaseHdc();
            e.Graphics.DrawImage(expandImg, ptExpand);

            
            Point ptNodeIcon = new Point(nodeRect.Location.X - 4, nodeRect.Location.Y + 2);
            Image nodeImg = Image.FromFile(nodePath);
            g = Graphics.FromImage(nodeImg);
            imgPtr = g.GetHdc();
            g.ReleaseHdc();
            e.Graphics.DrawImage(nodeImg, ptNodeIcon);

            
            System.Drawing.Font nodeFont = e.Node.NodeFont;
            if (nodeFont == null)
                nodeFont = ((TreeView)sender).Font;
            Brush textBrush = SystemBrushes.WindowText;
            //to highlight the text when selected
            if ((e.State & TreeNodeStates.Focused) != 0)
                textBrush = SystemBrushes.HotTrack;
            //Inflate to not be cut
            Rectangle textRect = nodeRect;
            //need to extend node rect
            textRect.Width += 40;
            e.Graphics.DrawString(e.Node.Text, nodeFont, textBrush,
                Rectangle.Inflate(textRect, -12, 0));
        }*/
    }
}
