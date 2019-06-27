using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using OfficeOpenXml;
using Bold = DocumentFormat.OpenXml.Wordprocessing.Bold;
using Break = DocumentFormat.OpenXml.Wordprocessing.Break;
using FontSize = DocumentFormat.OpenXml.Wordprocessing.FontSize;
using NonVisualGraphicFrameDrawingProperties =
    DocumentFormat.OpenXml.Drawing.Wordprocessing.NonVisualGraphicFrameDrawingProperties;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;
using ParagraphProperties = DocumentFormat.OpenXml.Wordprocessing.ParagraphProperties;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using RunProperties = DocumentFormat.OpenXml.Wordprocessing.RunProperties;
using TabStop = DocumentFormat.OpenXml.Wordprocessing.TabStop;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;

namespace BusinessPlanner.Utility
{
    public class ExportGenerator
    {
        string altChunkId = "AltChunkId5";
        WordprocessingDocument wordDocument;
        MainDocumentPart mainPart;
        AlternativeFormatImportPart chunk;
        Body body;
        public void open(string fname)
        {
            wordDocument = WordprocessingDocument.Create(fname, WordprocessingDocumentType.Document, true);
            mainPart = wordDocument.AddMainDocumentPart();
            mainPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document();
            body = mainPart.Document.AppendChild(new Body());
            chunk = mainPart.AddAlternativeFormatImportPart(AlternativeFormatImportPartType.Rtf, altChunkId);
        }

        

        /**public void getFooterWithPageNumber()
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
        }**/

        public void generateCoverPage(string data)
        {
            body.Append(new Paragraph(new Run(new Break())));
            body.Append(new Paragraph(new Run(new Break())));
            body.Append(new Paragraph(new Run(new Break())));
            body.Append(new Paragraph(new Run(new Break())));

            //heading
            Paragraph heading = new Paragraph();
            ParagraphProperties hp = new ParagraphProperties();
            hp.Justification = new Justification() { Val = JustificationValues.Center };
            heading.Append(hp);
            Run r = new Run();
            RunProperties runProperties = r.AppendChild(new RunProperties());
            Bold bold = new Bold();
            bold.Val = OnOffValue.FromBoolean(true);
            FontSize size = new FontSize();
            size.Val = "60";
            runProperties.AppendChild(bold);
            runProperties.AppendChild(size);
            Text t = new Text(data);
            r.Append(t);
            heading.Append(r);
            body.Append(heading);

            //subheading
            Paragraph subheading = new Paragraph();
            ParagraphProperties shp = new ParagraphProperties();
            shp.Justification = new Justification() { Val = JustificationValues.Center };
            subheading.Append(shp);
            Run rs = new Run();
            RunProperties rhProperties = rs.AppendChild(new RunProperties());
            Bold bs = new Bold();
            bs.Val = OnOffValue.FromBoolean(true);
            FontSize sh = new FontSize();
            sh.Val = "40";
            rhProperties.AppendChild(bs);
            rhProperties.AppendChild(sh);
            Text subhead = new Text("BUSINESS PLAN");
            rs.Append(subhead);
            subheading.Append(rs);
            body.Append(subheading);
            addPageBreak();
        }

        public void addPageBreak()
        {
            body.Append(new Paragraph(new Run(new Break() { Type = BreakValues.Page })));
        }


        public void setContent(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                chunk.FeedData(ms);
            }
            AltChunk altChunk = new AltChunk();
            altChunk.Id = altChunkId;

            // Embed AltChunk after the last paragraph.
            body.InsertAfter(altChunk, body.Elements<Paragraph>().Last());
        }

        public void saveDocument()
        {
            mainPart.Document.Save();
        }

        public void Close()
        {
            wordDocument.Close();
        }

       /** public void saveAsPdf(object data)
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
        }**/
    }
}
