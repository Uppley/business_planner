using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessPlanner.Utility
{
    class TableGenerator
    {
        public void Generate(DataGridView dgv,string document)
        {
            StringBuilder tableRtf = new StringBuilder();
            tableRtf.Append(@"{\rtf1 ");
            int ra = 0;
            for (int i = -1; i < dgv.Rows.Count-1; i++)
            {
                tableRtf.Append(@"{\trowd\trleft0\trgaph-0\trbrdrt\brdrnone\trbrdrb\brdrnone\trbrdrr\brdrnone\trbrdrl\brdrnone\trbrdrv\brdrnone\trbrdrh\brdrnone\trautofit1\trpaddl10\trpaddr10\trpaddb10\trpaddt10\trpaddfl3\trpaddfr3\trpaddft3\trpaddfb3\trql\ltrrow");

                
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    tableRtf.Append(@"\clvertalt\clbrdrt\brdrnone\clbrdrl\brdrnone\clbrdrb\brdrnone\clbrdrr\brdrnone\cellx0");
                }

                tableRtf.Append("{");
                
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    if (ra == 0)
                        tableRtf.Append(@"{\fs22\f3\b\intbl {\ul\ltrch " + dgv.Columns[j].HeaderText + @"}\li0\ri0\sa0\sb0\fi0\ql\sl15\slmult0\cell}");
                    else
                        tableRtf.Append(@"{\fs22\f3\intbl {\ltrch " + dgv.Rows[i].Cells[j].Value.ToString() + @"}\li0\ri0\sa0\sb0\fi0\ql\sl15\slmult0\cell}");
                }

                tableRtf.Append("}");
                tableRtf.Append(@"{\trowd\trleft0\trgaph-0\trbrdrt\brdrnone\trbrdrb\brdrnone\trbrdrr\brdrnone\trbrdrl\brdrnone\trbrdrv\brdrnone\trbrdrh\brdrnone\trftsWidth1\trftsWidthB3\trpaddl10\trpaddr10\trpaddb10\trpaddt10\trpaddfl3\trpaddfr3\trpaddft3\trpaddfb3\trql\ltrrow");


                tableRtf.Append(@"\row}}");
                ra++;
            }

            tableRtf.Append(@"\pard");
            tableRtf.Append(@"}");
            RichTextBox rtb = new RichTextBox();
            rtb.LoadFile(ProjectConfig.projectPath + "\\" + document);
            rtb.Text = "";
            rtb.SelectedRtf = tableRtf.ToString();
            rtb.SaveFile(ProjectConfig.projectPath + "\\" + document);
            rtb.Dispose();
        }

        public void GenerateMultipleTable(DataGridView[] dgv, string document,string[] names)
        {
            
            StringBuilder tableRtf = new StringBuilder();
            tableRtf.Append(@"{\rtf1\ansi\deff0");
            for (int d=0;d<dgv.Length;d++)
            {
                tableRtf.Append(@"\pard\fs30\b " + names[d]+@"\b0");
                tableRtf.Append(@"\line\line");
                int ra = 0;
                for (int i = -1; i < dgv[d].Rows.Count - 1; i++)
                {
                    tableRtf.Append(@"{\trowd\trleft0\trgaph-0\trbrdrt\brdrnone\trbrdrb\brdrnone\trbrdrr\brdrnone\trbrdrl\brdrnone\trbrdrv\brdrnone\trbrdrh\brdrnone\trftsWidth1\trftsWidthB3\trpaddl10\trpaddr10\trpaddb10\trpaddt10\trpaddfl3\trpaddfr3\trpaddft3\trpaddfb3\trql\ltrrow");


                    for (int j = 0; j < dgv[d].Columns.Count; j++)
                    {
                        tableRtf.Append(@"\clvertalt\clbrdrt\brdrnone\clbrdrl\brdrnone\clbrdrb\brdrnone\clbrdrr\brdrnone\cellx0");
                    }

                    tableRtf.Append("{");

                    for (int j = 0; j < dgv[d].Columns.Count; j++)
                    {
                        if (ra == 0)
                            tableRtf.Append(@"{\fs22\f3\b\intbl {\ul\ltrch " + dgv[d].Columns[j].HeaderText + @"}\li0\ri0\sa0\sb0\fi0\ql\sl15\slmult0\cell}");
                        else
                            tableRtf.Append(@"{\fs22\f3\intbl {\ltrch " + dgv[d].Rows[i].Cells[j].Value.ToString() + @"}\li0\ri0\sa0\sb0\fi0\ql\sl15\slmult0\cell}");
                    }

                    tableRtf.Append("}");
                    tableRtf.Append(@"{\trowd\trleft0\trgaph-0\trbrdrt\brdrnone\trbrdrb\brdrnone\trbrdrr\brdrnone\trbrdrl\brdrnone\trbrdrv\brdrnone\trbrdrh\brdrnone\trftsWidth1\trftsWidthB3\trpaddl10\trpaddr10\trpaddb10\trpaddt10\trpaddfl3\trpaddfr3\trpaddft3\trpaddfb3\trql\ltrrow");


                    tableRtf.Append(@"\row}}");
                    ra++;
                }

                tableRtf.Append(@"\pard");

                tableRtf.Append(@"\line");
                tableRtf.Append(@"\line");

            }
            tableRtf.Append(@"}");

            RichTextBox rtb = new RichTextBox();
            rtb.LoadFile(ProjectConfig.projectPath + "\\" + document);
            rtb.Text = "";
            rtb.SelectedRtf = tableRtf.ToString();
            rtb.SaveFile(ProjectConfig.projectPath + "\\" + document);
            rtb.Dispose();
        }
    }
}
