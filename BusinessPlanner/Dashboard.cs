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
        string filepath = "test.rtf";
        string[] fileContent;
        StringBuilder contentBuilder;
        public Dashboard(String name)
        {
            InitializeComponent();
            ToolStripManager.Renderer = new Office2007Renderer.Office2007Renderer();
            toolStrip2.Renderer = new Office2007Renderer.Office2007Renderer();
            toolStrip3.Renderer = new Office2007Renderer.Office2007Renderer();
            
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
            //contentBuilder = new StringBuilder();
            //fileContent = File.ReadAllLines(filepath);
            this.setContentInitial(name);
            richTextBox1.SelectionFont = new Font("Arial", 12, richTextBox1.SelectionFont.Style);
            richTextBox1.ZoomFactor = 1.0f;
            toolStripComboBox2.SelectedIndex = 2;
        }

        private void setContentInitial(String name)
        {
            if (name == "objectives")
            {
                /*int indx_start = Array.FindIndex(this.fileContent,str => str.Contains(@"{\pard \fs60 Objectives\par}"));
                int indx_end = Array.FindIndex(this.fileContent, str => str.Contains(@"{\pard \fs15 end_objectives\par}"));
                
                contentBuilder.AppendLine(@"{\rtf1");
                for (int i=indx_start+1;i<indx_end;i++)
                {
                    contentBuilder.AppendLine(this.fileContent[i]);

                }
                
                contentBuilder.AppendLine(@"}");
                
                richTextBox1.Rtf = contentBuilder.ToString();*/
                _LoadingDialog ld = new _LoadingDialog();
                try
                {
                    ld.Show();
                    Application.DoEvents();
                    richTextBox1.LoadFile("Document1.rtf");
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
            else
            {
               richTextBox1.Text = "";
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
            //string data = richTextBox1.Rtf;
            //int pFrom = data.IndexOf(@"{\rtf1") + @"{\rtf1".Length;
            //int pTo = data.LastIndexOf(@"}");
           // String result = data.Substring(pFrom, pTo - pFrom);
           // MessageBox.Show(result);
            richTextBox1.SaveFile("Document1.rtf");
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
            if(richTextBox1.SelectionFont != null)
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
    }
}
