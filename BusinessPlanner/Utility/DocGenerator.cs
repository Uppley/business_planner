using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPlanner.Utility
{
    class DocGenerator
    {
        private object missing;
        private object Visible;
        private object start1;
        private object end1;
        private ApplicationClass WordApp;
        private Document adoc;

        public void open()
        {
            missing = System.Reflection.Missing.Value;
            Visible = true;
            start1 = 0;
            end1 = 0;
            WordApp = new ApplicationClass();
            adoc = WordApp.Documents.Add(ref missing, ref missing, ref missing, ref missing);
        }

        public void getHeader(string data)
        {
            foreach (Section section in this.adoc.Sections)
            {
                    
                Range headerRange = section.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;   
                headerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                headerRange.Font.ColorIndex = WdColorIndex.wdDarkRed;
                headerRange.Font.Size = 10;
                headerRange.Text = data+" Plan";
                headerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                headerRange.InlineShapes.AddHorizontalLineStandard();
            }
        }

        public void getFooterWithPageNumber()
        {
            foreach (Section wordSection in this.adoc.Sections)
            {
                Range footerRange = wordSection.Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                footerRange.InlineShapes.AddHorizontalLineStandard();
                footerRange.Font.ColorIndex = WdColorIndex.wdDarkRed;
                footerRange.Font.Size = 10;
                footerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                adoc.Sections[1].Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].PageNumbers.Add();
            }
        }

        public void generateCoverPage(string data)
        {
            WordApp.Selection.Font.Bold = 1;
            WordApp.Selection.Font.Underline = WdUnderline.wdUnderlineSingle;
            WordApp.Selection.Font.AllCaps = 1;
            WordApp.Selection.Font.Size = 50;
            WordApp.Selection.ParagraphFormat.SpaceBefore = 150;
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            WordApp.Selection.TypeText(data);
            WordApp.Selection.TypeParagraph();
            this.clearFormatting();
            WordApp.Selection.Font.Bold = 1;
            WordApp.Selection.Font.Underline = WdUnderline.wdUnderlineSingle;
            WordApp.Selection.Font.Size = 25;
            WordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            WordApp.Selection.TypeText("Business Plan");
            WordApp.Selection.InsertBreak(WdBreakType.wdPageBreak);
            this.clearFormatting();
        }

        public void getContent()
        {
            this.WordApp.Selection.Paste();
        }

        public void saveAsWord(object data)
        {
            this.adoc.SaveAs(ref data, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
        }

        public void saveAsPdf(object data)
        {
            object fileformat = WdSaveFormat.wdFormatPDF;
            adoc.SaveAs2(ref data, ref fileformat, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
            object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
            ((_Document)this.adoc).Close(ref saveChanges, ref missing, ref missing);
        }

        public void clearFormatting()
        {
            this.WordApp.Selection.ClearFormatting();
        }

        public void close()
        {
            this.WordApp.Quit();
            this.adoc = null;
        }
    }
}
