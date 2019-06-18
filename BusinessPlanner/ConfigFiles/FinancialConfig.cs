using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPlanner.Utility
{
    class FinancialConfig
    {
        public static List<TreeViewItem> treeViewList = new List<TreeViewItem>() {
            new TreeViewItem() {ParentID = 0, ID = 1,Text="Home"},
            new TreeViewItem() { ParentID = 0, ID = 2, Text = "Company Insight" },
            new TreeViewItem() { ParentID = 2, ID = 3, Text = "Company Summary" },
            new TreeViewItem() { ParentID = 2, ID = 4, Text = "Start Up Investment" },
            new TreeViewItem() { ParentID = 2, ID = 5, Text = "Company Expenditure" },
            new TreeViewItem() { ParentID = 0, ID = 6, Text = "What You Sell" },
            new TreeViewItem() { ParentID = 6, ID = 7, Text = "Product Summary" },
            new TreeViewItem() { ParentID = 6, ID = 8, Text = "Service Summary" },
            new TreeViewItem() { ParentID = 6, ID = 9, Text = "Product & Service Summary" },
            new TreeViewItem() { ParentID = 0, ID = 10, Text = "Sales Statistics" },
            new TreeViewItem() { ParentID = 10, ID = 11, Text = "Sales Strategy" },
            new TreeViewItem() { ParentID = 10, ID = 12, Text = "Sales Forecast Table" },
            new TreeViewItem() { ParentID = 10, ID = 13, Text = "Explain Sales Forecast" },
            new TreeViewItem() { ParentID = 0, ID = 14, Text = "Financial Plan" },
            new TreeViewItem() { ParentID = 14, ID = 15, Text = "Financial Summary" },
            new TreeViewItem() { ParentID = 14, ID = 16, Text = "Financial Statement" },
            new TreeViewItem() { ParentID = 0, ID = 17, Text = "Finish" },
            new TreeViewItem() { ParentID = 17, ID = 18, Text = "Executive Summary" }
        };

        public static int getId()
        {

            return FinancialConfig.treeViewList[FinancialConfig.treeViewList.Count - 1].ID;
        }

        public static List<TreeViewItem> getAllNodes()
        {
            return FinancialConfig.treeViewList;
        }

        public static List<TreeViewItem> getProjectNodes()
        {
            List<TreeViewItem> li = new List<TreeViewItem>();
            foreach (var s in FinancialConfig.treeViewList)
            {
                if (s.ParentID == 0)
                {
                    li.Add(s);
                }
                else
                {
                    foreach (var d in StandardDocument.DocumentList)
                    {
                        if (d.ItemName == s.Text && File.Exists(ProjectConfig.projectPath + "//" + d.DocumentName))
                        {
                            li.Add(s);
                        }
                    }
                }
            }
            return li;
        }

        public static void updateNodes(int pid, int id, string t)
        {
            FinancialConfig.treeViewList.Add(new TreeViewItem() { ParentID = pid, ID = id, Text = t });
        }
    }
}
