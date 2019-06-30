using BusinessPlanner.Partials;
using BusinessPlanner.Utility;
using NHunspellComponent.Spelling;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessPlanner.Models;
using BP.Instructions;

namespace BusinessPlanner
{
    public partial class Dashboard : Form
    {
        public List<int> findPosition = new List<int>();
        
        public int findLength = 0;
        string file_loaded;
        string section_name;
        SectionInstructions secIns;
        public string richtext { get; set; }
        MainWindow mw;
        DocumentProgressor dgp;

        public Dashboard(MainWindow maw,String name)
        {
            InitializeComponent();
            mw = maw;
            file_loaded = name;
            ToolStripManager.Renderer = new Office2007Renderer.Office2007Renderer();
            toolStrip2.Renderer = new Office2007Renderer.Office2007Renderer();
            toolStrip3.Renderer = new Office2007Renderer.Office2007Renderer();
            findPosition.Clear();
            foreach (FontFamily font in System.Drawing.FontFamily.Families)
            {
                toolStripComboBox1.Items.Add(font.Name.ToString());
            }
            for(int i = 5;i<=90;i++)
            {
                fontSizeCmb.Items.Add(i);
            }
            toolStripComboBox1.SelectedItem = "Arial";
            fontSizeCmb.SelectedItem = 12;
            section_name = setDocumentList().Find(item => item.DocumentName == name).ItemName;
            secIns = new SectionInstructions();
            this.setContentInitial(file_loaded);
            this.richTextBox1.SelectionFont = new Font("Arial", 12, this.richTextBox1.SelectionFont.Style);
            this.richTextBox1.ZoomFactor = 1.0f;
            toolStripComboBox2.SelectedIndex = 2;
            dgp = new DocumentProgressor();
            if (WorkProgress.workItems.Exists(x => x.filename == file_loaded))
            {
                label2.Text = "Unsaved";
            }

        }

        private List<DocumentItem> setDocumentList()
        {
            List<DocumentItem> documentList = null;

            if (ProjectConfig.projectSettings["PlanType"].ToString() == "Standard Plan")
            {
                documentList = StandardDocument.DocumentList;
            }
            else if (ProjectConfig.projectSettings["PlanType"].ToString() == "Quick Plan")
            {
                documentList = QuickDocument.DocumentList;
            }
            else if (ProjectConfig.projectSettings["PlanType"].ToString() == "Financial Plan")
            {
                documentList = FinancialDocument.DocumentList;
            }
            return documentList;
        }

        private void setContentInitial(String name)
        {
            try
            {
                label9.Text = section_name;
                instruction_box.Text = secIns.instructionItems.FirstOrDefault(x => x.section == section_name).instruction;
                if(WorkProgress.workItems.Exists(x=>x.filename==name))
                {
                    int index = WorkProgress.workItems.FindIndex(x => x.filename == name);
                    //Debug.WriteLine(WorkProgress.workItems[index].filename);
                    this.richTextBox1.Rtf = WorkProgress.workItems[index].data;
                    WorkProgress.workItems[index].data = this.richTextBox1.Rtf;
                }
                else
                {
                    this.richTextBox1.LoadFile(Path.Combine(ProjectConfig.projectPath, name));
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Exception: " + e.Message);
            }
            finally
            {
                
            }

        }


        private void btnPaste_Click(object sender, EventArgs e)
        {
            CustomEditor.Paste(this.richTextBox1);
        }

        private void btnBold_Click(object sender, EventArgs e)
        {
            CustomEditor.makeBold(this.richTextBox1);
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            CustomEditor.makeItalic(this.richTextBox1);
        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            CustomEditor.makeUnderline(this.richTextBox1);
        }

        private void btnToggleBullets_Click(object sender, EventArgs e)
        {
            CustomEditor.toggleBullets(this.richTextBox1);
        }

        private void btnDecreaseIndentation_Click(object sender, EventArgs e)
        {
            CustomEditor.decIndent(this.richTextBox1);
        }

        private void btnIncreaseIndentation_Click(object sender, EventArgs e)
        {
            CustomEditor.incIndent(this.richTextBox1);
        }

        private void btnAlignLeft_Click(object sender, EventArgs e)
        {
            CustomEditor.alignLeft(this.richTextBox1);
        }

        private void btnAlignCenter_Click(object sender, EventArgs e)
        {
            CustomEditor.alignCenter(this.richTextBox1);
        }

        private void btnAlignRight_Click(object sender, EventArgs e)
        {
            CustomEditor.alignRight(this.richTextBox1);
        }


        private void btnSave_Click_1(object sender, EventArgs e)
        {
            saveDocument();
        }


        private void btnGemBoxCut_Click_1(object sender, EventArgs e)
        {

            CustomEditor.Cut(this.richTextBox1);
        }

        private void btnGemBoxCopy_Click_1(object sender, EventArgs e)
        {
            CustomEditor.Copy(this.richTextBox1);
        }

        private void btnUndo_Click_1(object sender, EventArgs e)
        {
            CustomEditor.undo(this.richTextBox1);
        }

        private void btnRedo_Click_1(object sender, EventArgs e)
        {
            CustomEditor.redo(this.richTextBox1);
        }

        private void ToolStripButton13_Click(object sender, EventArgs e)
        {

            OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif;)|*.jpg;*.jpeg;*.png;*.gif;";
            if (opnfd.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(opnfd.FileName);
                Clipboard.SetImage(img);
                this.richTextBox1.Paste();
                this.richTextBox1.Focus();
            }
        }

        private void ToolStripButton15_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = false;
            MyDialog.ShowHelp = true;
            MyDialog.Color = this.richTextBox1.ForeColor;

            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                if (this.richTextBox1.SelectedText != null)
                {
                    this.richTextBox1.SelectionColor = MyDialog.Color;
                }
            }
        }

        private void ToolStripButton14_Click(object sender, EventArgs e)
        {
            _TableInputForm tbl = new _TableInputForm();
            DialogResult table_dimensions = tbl.ShowDialog(this);
            if(table_dimensions == DialogResult.OK && tbl.col_width > 0)
            {
                int col_width = Decimal.ToInt32(tbl.col_width)*1000;
                int rows = Decimal.ToInt32(tbl.rows);
                int columns = Decimal.ToInt32(tbl.columns);
                StringBuilder tableRtf = new StringBuilder();
                tableRtf.Append(@"{\rtf1 ");
                for (int i = 0; i < rows; i++)
                {
                    tableRtf.Append(@"\trowd");
                    for (int j = 1; j <= columns; j++)
                    {
                        string col = (col_width * j).ToString();
                        tableRtf.Append(@"\cellx"+col);
                    }
                    tableRtf.Append(@"\intbl \cell \row");
                }
                
                tableRtf.Append(@"\pard");
                tableRtf.Append(@"}");
                this.richTextBox1.SelectedRtf = tableRtf.ToString();
            }
            
        }

        private void ToolStripButton4_Click(object sender, EventArgs e)
        {
            
            _HyperlinkInputForm hpl = new _HyperlinkInputForm();
            DialogResult hpl_data = hpl.ShowDialog(this);
            if (hpl_data == DialogResult.OK)
            {

                StringBuilder st = new StringBuilder();
                st.Append(@"{\rtf1");
                st.Append(@"{\colortbl;\red0\green0\blue238;}");
                st.Append(@"{\field{\*\fldinst {HYPERLINK "+ hpl.linkUrl +"}}");
                st.Append(@"{\fldrslt{\cf1\ul " + hpl.linkText+"}}}");
                st.Append(@"}");
                this.richTextBox1.SelectedRtf = st.ToString();
            }
        }

        private void ToolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.richTextBox1.SelectionFont != null)
                this.richTextBox1.SelectionFont = new Font(toolStripComboBox1.SelectedItem.ToString(), 12, this.richTextBox1.SelectionFont.Style);
        }

        private void RichTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            
            if (this.richTextBox1.SelectionFont != null)
            {
                boldBt.Checked = false;
                italicBt.Checked = false;
                underBt.Checked = false;
                toolStripComboBox1.SelectedItem = this.richTextBox1.SelectionFont.Name;
                fontSizeCmb.SelectedItem = Int32.TryParse(this.richTextBox1.SelectionFont.Size.ToString(), out int res) ?  res : 12;
            }
         }

        private void ToolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripComboBox cmb = (ToolStripComboBox)sender;
            int i = toolStripComboBox2.SelectedIndex;
            float zf = 1.0f;
            if (i == 0)
            {
                zf = 0.5f;
            }
            else if (i == 1)
            {
                zf = 0.75f;
            }
            else if(i == 3)
            {
                zf = 1.25f;
            }
            else if(i == 4)
            {
                zf = 1.5f;
            }

            this.richTextBox1.ZoomFactor = zf;
        }

        private void FontSizeCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.richTextBox1.SelectionFont != null)
                this.richTextBox1.SelectionFont = new Font(this.richTextBox1.SelectionFont.FontFamily, float.Parse(fontSizeCmb.SelectedItem.ToString()), this.richTextBox1.SelectionFont.Style);
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = false;
            MyDialog.ShowHelp = true;
            MyDialog.Color = this.richTextBox1.BackColor;

            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                if (this.richTextBox1.SelectedText != null)
                {
                    this.richTextBox1.SelectionBackColor = MyDialog.Color;
                }
            }
        }

        private void ToolStripButton16_Click(object sender, EventArgs e)
        {
            _FindDialog hpl = new _FindDialog();
            DialogResult hpl_data = hpl.ShowDialog(this);
            if (hpl_data == DialogResult.OK)
            {
                if (hpl.searchTerm.Length > 0)
                {
                    this.findLength = hpl.searchTerm.Length;
                    int startIndex = 0;
                    while (startIndex < this.richTextBox1.TextLength)
                    {
                        int wordStartIndex = this.richTextBox1.Find(hpl.searchTerm, startIndex, RichTextBoxFinds.None);
                        if (wordStartIndex != -1)
                        {
                            this.richTextBox1.SelectionStart = wordStartIndex;
                            this.richTextBox1.SelectionLength = hpl.searchTerm.Length;
                            this.richTextBox1.SelectionBackColor = Color.Yellow;
                            this.findPosition.Add(wordStartIndex);
                        }
                        else
                            break;
                        startIndex += wordStartIndex + hpl.searchTerm.Length;
                    }

                }
            }
        }

        private void RichTextBox1_MouseClick(object sender, MouseEventArgs e)
        {

            if (this.findPosition.Count() > 0 && this.findLength > 0)
            {
                foreach (int c in this.findPosition)
                {
                    this.richTextBox1.SelectionStart = c;
                    this.richTextBox1.SelectionLength = this.findLength;
                    this.richTextBox1.SelectionBackColor = Color.White;
                }

                this.findPosition.Clear();
                this.findLength = 0;
            }
        }

        private void NextEgBt_MouseHover(object sender, EventArgs e)
        {
            nextEgBt.ImageIndex = 1;
        }

        private void NextEgBt_MouseLeave(object sender, EventArgs e)
        {
            nextEgBt.ImageIndex = 0;
        }

        private void PrevEgBt_MouseHover(object sender, EventArgs e)
        {
            prevEgBt.ImageIndex = 1;
        }

        private void PrevEgBt_MouseLeave(object sender, EventArgs e)
        {
            prevEgBt.ImageIndex = 0;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.richTextBox1.CanUndo)
                btnUndo.Enabled = true;
            else
                btnUndo.Enabled = false;
            if (this.richTextBox1.CanRedo)
                btnRedo.Enabled = true;
            else
                btnRedo.Enabled = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(label4.Text);
            this.richTextBox1.Paste();
        }

        public void saveDocument()
        {
            this.richTextBox1.SaveFile(Path.Combine(ProjectConfig.projectPath, file_loaded));
            label2.Text = "Saved";
            Dashboard_changeProgress(file_loaded);
            if (WorkProgress.workItems.Exists(x => x.filename == file_loaded))
            {
                int index = WorkProgress.workItems.FindIndex(x => x.filename == file_loaded);
                WorkProgress.workItems.RemoveAt(index);
            }
            string child_node = setDocumentList().Find(x => x.DocumentName == file_loaded).ItemName;
            TreeNode node = mw.treeView1.Nodes.Find(child_node, true)[0];
            node.Text = node.Name;
            TreeNode parent = node.Parent;
            TreeNodeCollection child_nodes = parent.Nodes;
            foreach(TreeNode p in child_nodes)
            {
                string cn = setDocumentList().Find(x => x.ItemName == p.Name).DocumentName;
                if(WorkProgress.workItems.Exists(x=>x.filename==cn))
                {
                    break;
                }
                else
                {
                    parent.Text = parent.Name;
                }
            }
            
        }

        private void Dashboard_changeProgress(string mssg)
        {
            Label l = mw.Controls.Find("label4", true)[0] as Label;
            ProgressBar pbar = mw.Controls.Find("progressBar1", true)[0] as ProgressBar;
            dgp.updateProgress(file_loaded, richTextBox1.TextLength > 0 ? 1 : 0);
            l.Text = dgp.completedSteps().ToString() + " /";
            pbar.Value = dgp.completedSteps();
            l.Refresh();
        }

        private void RichTextBox1_Leave(object sender, EventArgs e)
        {
            label2.Text = "Unsaved";
            if (WorkProgress.workItems.Exists(x => x.filename == file_loaded))
            {
                int index = WorkProgress.workItems.FindIndex(x => x.filename == file_loaded);
                WorkProgress.workItems[index].data = this.richTextBox1.Rtf;
            }
            else
            {
                WorkProgress.workItems.Add(new WorkItem { filename = file_loaded, data = this.richTextBox1.Rtf });
            }
            string child_node = setDocumentList().Find(x => x.DocumentName == file_loaded).ItemName;
            TreeNode node = mw.treeView1.Nodes.Find(child_node, true)[0];
            node.Text = node.Name + "*";
            node.Parent.Text = node.Parent.Name + "*";
        }
    }
}
