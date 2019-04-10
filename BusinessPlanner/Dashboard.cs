using GemBox.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessPlanner
{
    public partial class Dashboard : Form
    {
        public Dashboard(String name)
        {
            InitializeComponent();
            ToolStripManager.Renderer = new Office2007Renderer.Office2007Renderer();
            toolStrip2.Renderer = new Office2007Renderer.Office2007Renderer();
            toolStrip3.Renderer = new Office2007Renderer.Office2007Renderer();
            
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
            this.setContentInitial(name);
        }

        private void setContentInitial(String name)
        {
            if (name == "objectives")
            {
                using (var stream = new MemoryStream())
                {
                    DocumentModel.Load("Document.docx").Save(stream, SaveOptions.RtfDefault);
                    stream.Position = 0;
                    this.richTextBox1.LoadFile(stream, RichTextBoxStreamType.RichText);
                }
            }
            else
            {
                this.richTextBox1.Text = "";
            }
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            using (var stream = new MemoryStream())
            {
                DocumentModel.Load("Document.docx").Save(stream, SaveOptions.RtfDefault);
                stream.Position = 0;
                this.richTextBox1.LoadFile(stream, RichTextBoxStreamType.RichText);

            }
        }


        private void btnCut_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Cut();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Copy();
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Paste();
        }

        private void btnBold_Click(object sender, EventArgs e)
        {
            this.richTextBox1.SelectionFont = new Font(this.richTextBox1.SelectionFont.FontFamily, this.richTextBox1.SelectionFont.Size, this.ToggleFontStyle(this.richTextBox1.SelectionFont.Style, FontStyle.Bold));
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            this.richTextBox1.SelectionFont = new Font(this.richTextBox1.SelectionFont.FontFamily, this.richTextBox1.SelectionFont.Size, this.ToggleFontStyle(this.richTextBox1.SelectionFont.Style, FontStyle.Italic));
        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            this.richTextBox1.SelectionFont = new Font(this.richTextBox1.SelectionFont.FontFamily, this.richTextBox1.SelectionFont.Size, this.ToggleFontStyle(this.richTextBox1.SelectionFont.Style, FontStyle.Underline));
        }

        private void btnIncreaseFontSize_Click(object sender, EventArgs e)
        {
            this.richTextBox1.SelectionFont = new Font(this.richTextBox1.SelectionFont.FontFamily, this.richTextBox1.SelectionFont.Size + 1, this.richTextBox1.SelectionFont.Style);
        }

        private void btnDecreaseFontSize_Click(object sender, EventArgs e)
        {
            this.richTextBox1.SelectionFont = new Font(this.richTextBox1.SelectionFont.FontFamily, this.richTextBox1.SelectionFont.Size - 1, this.richTextBox1.SelectionFont.Style);
        }

        private void btnToggleBullets_Click(object sender, EventArgs e)
        {
            this.richTextBox1.SelectionBullet = !this.richTextBox1.SelectionBullet;
        }

        private void btnDecreaseIndentation_Click(object sender, EventArgs e)
        {
            this.richTextBox1.SelectionIndent -= 10;
        }

        private void btnIncreaseIndentation_Click(object sender, EventArgs e)
        {
            this.richTextBox1.SelectionIndent += 10;
        }

        private void btnAlignLeft_Click(object sender, EventArgs e)
        {
            this.richTextBox1.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
        }

        private void btnAlignCenter_Click(object sender, EventArgs e)
        {
            this.richTextBox1.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
        }

        private void btnAlignRight_Click(object sender, EventArgs e)
        {
            this.richTextBox1.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Right;
        }

        //endregion

        private void DoGemBoxCopy()
        {
            using (var stream = new MemoryStream())
            {
                // Save RichTextBox selection to RTF stream.
                var writer = new StreamWriter(stream);
                writer.Write(this.richTextBox1.SelectedRtf);
                writer.Flush();

                stream.Position = 0;

                // Save RTF stream to clipboard.
                DocumentModel.Load(stream, LoadOptions.RtfDefault).Content.SaveToClipboard();
            }
        }

        private void DoGemBoxPaste(bool prepend)
        {
            using (var stream = new MemoryStream())
            {
                // Save RichTextBox content to RTF stream.
                var writer = new StreamWriter(stream);
                writer.Write(this.richTextBox1.SelectedRtf);
                writer.Flush();

                stream.Position = 0;

                // Load document from RTF stream and prepend or append clipboard content to it.
                var document = DocumentModel.Load(stream, LoadOptions.RtfDefault);
                var content = document.Content;
                (prepend ? content.Start : content.End).LoadFromClipboard();

                stream.Position = 0;

                // Save document to RTF stream.
                document.Save(stream, SaveOptions.RtfDefault);

                stream.Position = 0;

                // Load RTF stream into RichTextBox.
                var reader = new StreamReader(stream);
                this.richTextBox1.SelectedRtf = reader.ReadToEnd();
            }
        }

        private FontStyle ToggleFontStyle(FontStyle item, FontStyle toggle)
        {
            return item ^ toggle;
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            using (var stream = new MemoryStream())
            {
                this.richTextBox1.SaveFile(stream, RichTextBoxStreamType.RichText);
                stream.Position = 0;
                DocumentModel.Load(stream, LoadOptions.RtfDefault).Save("Document.docx");
                this.label2.Text = "Saved";
            }
        }

        private void btnGemBoxCut_Click_1(object sender, EventArgs e)
        {
            this.DoGemBoxCopy();
            this.richTextBox1.SelectedRtf = string.Empty;
        }

        private void btnGemBoxCopy_Click_1(object sender, EventArgs e)
        {
            this.DoGemBoxCopy();
        }

        private void btnGemBoxPastePrepend_Click_1(object sender, EventArgs e)
        {
            this.richTextBox1.Paste();
        }



        private void btnUndo_Click_1(object sender, EventArgs e)
        {
            if (this.richTextBox1.CanUndo)
                this.richTextBox1.Undo();
        }

        private void btnRedo_Click_1(object sender, EventArgs e)
        {
            if (this.richTextBox1.CanRedo)
                this.richTextBox1.Redo();
        }

        private void toggleFont_Click(object sender, EventArgs e)
        {
            //this.ToggleFontStyle();
        }

       

        private void richTextBox1_TextChanged(object sender, KeyEventArgs e)
        {
            this.label2.Text = "Editing";
        }
    }
}
