using BusinessPlanner.Partials;
using BusinessPlanner.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessPlanner
{
    public partial class Dashboard : Form
    {
        List<int> findPosition = new List<int>();
        int findLength = 0;
        string file_loaded;
        public string richtext { get; set; }
        public Dashboard(String name)
        {
            InitializeComponent();
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
            this.setContentInitial(file_loaded);
            richTextBox1.SelectionFont = new Font("Arial", 12, richTextBox1.SelectionFont.Style);
            richTextBox1.ZoomFactor = 1.0f;
            toolStripComboBox2.SelectedIndex = 2;
        }

        private void setContentInitial(String name)
        {
                
                _LoadingDialog ld = new _LoadingDialog(AppMessages.messages["loading"]);
                try
                {
                    ld.Show();
                    Application.DoEvents();
                    label9.Text = DocumentRecord.DocumentList.Find(item => item.DocumentName == name).ItemName;
                    richTextBox1.LoadFile(Path.Combine(ProjectConfig.projectPath,name));
                }
                catch(Exception e)
                {
                    MessageBox.Show("Exception: " + e.Message);
                }
                finally
                {
                    ld.Close();
                }

        }



        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            
        }


        private void btnCut_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
           richTextBox1.Paste();
        }

        private void btnBold_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, richTextBox1.SelectionFont.Size, ToggleFontStyle(richTextBox1.SelectionFont.Style, FontStyle.Bold));
            }
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, richTextBox1.SelectionFont.Size, ToggleFontStyle(richTextBox1.SelectionFont.Style, FontStyle.Italic));
            }
        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, richTextBox1.SelectionFont.Size, ToggleFontStyle(richTextBox1.SelectionFont.Style, FontStyle.Underline));
            }
        }

      

        private void btnToggleBullets_Click(object sender, EventArgs e)
        {
            
                richTextBox1.SelectionBullet = !richTextBox1.SelectionBullet;
            
        }

        private void btnDecreaseIndentation_Click(object sender, EventArgs e)
        {
            
                richTextBox1.SelectionIndent -= 10;
            
        }

        private void btnIncreaseIndentation_Click(object sender, EventArgs e)
        {
            
                richTextBox1.SelectionIndent += 10;
            
        }

        private void btnAlignLeft_Click(object sender, EventArgs e)
        {
            
                richTextBox1.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            
        }

        private void btnAlignCenter_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
        }

        private void btnAlignRight_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Right;
        }

        //endregion

        

        

        private FontStyle ToggleFontStyle(FontStyle item, FontStyle toggle)
        {
            return item ^ toggle;
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            richTextBox1.SaveFile(Path.Combine(ProjectConfig.projectPath, file_loaded));
            label2.Text = "Saved";
        }

        private void btnGemBoxCut_Click_1(object sender, EventArgs e)
        {
            
            richTextBox1.SelectedRtf = string.Empty;
        }

        private void btnGemBoxCopy_Click_1(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void btnGemBoxPastePrepend_Click_1(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }



        private void btnUndo_Click_1(object sender, EventArgs e)
        {
            if (richTextBox1.CanUndo)
                richTextBox1.Undo();
        }

        private void btnRedo_Click_1(object sender, EventArgs e)
        {
            if (richTextBox1.CanRedo)
                richTextBox1.Redo();
        }

        private void toggleFont_Click(object sender, EventArgs e)
        {
            //this.ToggleFontStyle();
        }

       

        private void richTextBox1_TextChanged(object sender, KeyEventArgs e)
        {
            label2.Text = "Editing";
        }

        private void ToolStripButton13_Click(object sender, EventArgs e)
        {

            OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.Filter = "Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif";
            if (opnfd.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(opnfd.FileName);
                Clipboard.SetImage(img);
                richTextBox1.Paste();
                richTextBox1.Focus();
            }
        }

        private void ToolStripButton15_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = false;
            MyDialog.ShowHelp = true;
            MyDialog.Color = richTextBox1.ForeColor;

            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                if (richTextBox1.SelectedText != null)
                {
                    richTextBox1.SelectionColor = MyDialog.Color;
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
                richTextBox1.SelectedRtf = tableRtf.ToString();
            }
            
        }

        private void ToolStripButton3_Click(object sender, EventArgs e)
        {
            
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
                richTextBox1.SelectedRtf = st.ToString();
            }
        }

        private void ToolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
                richTextBox1.SelectionFont = new Font(toolStripComboBox1.SelectedItem.ToString(), 12, richTextBox1.SelectionFont.Style);
        }

        private void RichTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            
            if (richTextBox1.SelectionFont != null)
            {
                boldBt.Checked = false;
                italicBt.Checked = false;
                underBt.Checked = false;
                toolStripComboBox1.SelectedItem = richTextBox1.SelectionFont.Name;
                fontSizeCmb.SelectedItem = Int32.TryParse(richTextBox1.SelectionFont.Size.ToString(), out int res) ?  res : 12;
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
            
            richTextBox1.ZoomFactor = zf;
        }

        private void FontSizeCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, float.Parse(fontSizeCmb.SelectedItem.ToString()), richTextBox1.SelectionFont.Style);
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = false;
            MyDialog.ShowHelp = true;
            MyDialog.Color = richTextBox1.BackColor;

            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                if (richTextBox1.SelectedText != null)
                {
                    richTextBox1.SelectionBackColor = MyDialog.Color;
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
                    while (startIndex < richTextBox1.TextLength)
                    {
                        int wordStartIndex = richTextBox1.Find(hpl.searchTerm, startIndex, RichTextBoxFinds.None);
                        if (wordStartIndex != -1)
                        {
                            richTextBox1.SelectionStart = wordStartIndex;
                            richTextBox1.SelectionLength = hpl.searchTerm.Length;
                            richTextBox1.SelectionBackColor = Color.Yellow;
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
                    richTextBox1.SelectionStart = c;
                    richTextBox1.SelectionLength = this.findLength;
                    richTextBox1.SelectionBackColor = Color.White;
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
            label2.Text = "Editing";
        }
    }
}
