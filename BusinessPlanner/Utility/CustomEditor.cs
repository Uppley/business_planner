using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessPlanner.Utility
{
    public static class CustomEditor
    {
        public static void Cut(RichTextBox rtb)
        {
            rtb.Cut();
        }

        public static void Copy(RichTextBox rtb)
        {
            rtb.Copy();
        }

        public static void Paste(RichTextBox rtb)
        {
            rtb.Paste();
        }

        public static void makeBold(RichTextBox rtb)
        {
            if (rtb.SelectionFont != null)
            {
                rtb.SelectionFont = new Font(rtb.SelectionFont.FontFamily, rtb.SelectionFont.Size, ToggleFontStyle(rtb.SelectionFont.Style, FontStyle.Bold));
            }
        }

        public static void makeItalic(RichTextBox rtb)
        {
            if (rtb.SelectionFont != null)
            {
                rtb.SelectionFont = new Font(rtb.SelectionFont.FontFamily, rtb.SelectionFont.Size, ToggleFontStyle(rtb.SelectionFont.Style, FontStyle.Italic));
            }
        }

        public static void makeUnderline(RichTextBox rtb)
        {
            if (rtb.SelectionFont != null)
            {
                rtb.SelectionFont = new Font(rtb.SelectionFont.FontFamily, rtb.SelectionFont.Size, ToggleFontStyle(rtb.SelectionFont.Style, FontStyle.Underline));
            }
        }

        public static void toggleBullets(RichTextBox rtb)
        {
            rtb.SelectionBullet = !rtb.SelectionBullet;
        }

        public static void decIndent(RichTextBox rtb)
        {
            rtb.SelectionIndent -= 10;
        }

        public static void incIndent(RichTextBox rtb)
        {
            rtb.SelectionIndent += 10;
        }

        public static void alignLeft(RichTextBox rtb)
        {
            rtb.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
        }

        public static void alignCenter(RichTextBox rtb)
        {
            rtb.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
        }

        public static void alignRight(RichTextBox rtb)
        {
            rtb.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Right;
        }

        public static void undo(RichTextBox rtb)
        {
            if (rtb.CanUndo)
                rtb.Undo();
        }

        public static void redo(RichTextBox rtb)
        {
            if (rtb.CanRedo)
                rtb.Redo();
        }

        public static void zoomIn(RichTextBox rtb)
        {
            rtb.ZoomFactor = 1.5f;
        }

        public static void zoomOut(RichTextBox rtb)
        {
            rtb.ZoomFactor = 0.5f;
        }


        public static void incFont(RichTextBox rtb)
        {
            if (rtb.SelectionFont != null)
            {
                float fz = rtb.SelectionFont.Size;
                fz += 2;
                if(fz > 100)
                    rtb.SelectionFont = new Font(rtb.SelectionFont.FontFamily,fz , rtb.SelectionFont.Style);
            }
                
        }

        public static void decFont(RichTextBox rtb)
        {
            if (rtb.SelectionFont != null)
            {
                float fz = rtb.SelectionFont.Size;
                fz -= 2;
                if(fz > 0)
                    rtb.SelectionFont = new Font(rtb.SelectionFont.FontFamily, fz, rtb.SelectionFont.Style);
            }
        }

        public static void selectAll(RichTextBox rtb)
        {
            rtb.SelectAll();
        }

        public static void deselectAll(RichTextBox rtb)
        {
            rtb.DeselectAll();
        }

        private static FontStyle ToggleFontStyle(FontStyle style, FontStyle toggle)
        {
            return style ^ toggle;
        }
    }
}
