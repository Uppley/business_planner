using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPlanner.Utility
{
    class FinancialConfig
    {
        public static List<TreeViewItem> treeViewList = new List<TreeViewItem>() {
            new TreeViewItem() {ParentID=0,ID=1,Text="Home"},
           
            new TreeViewItem() { ParentID = 0, ID = 1, Text = "Company Insight" },
            new TreeViewItem() { ParentID = 1, ID = 2, Text = "Company Summary" },
            new TreeViewItem() { ParentID = 1, ID = 3, Text = "Start Up Table" },
            new TreeViewItem() { ParentID = 0, ID = 4, Text = "Product Selling" },
            new TreeViewItem() { ParentID = 4, ID = 5, Text = "Product Summary" },
            new TreeViewItem() { ParentID = 0, ID = 6, Text = "Sales Forecast" },
            new TreeViewItem() { ParentID = 6, ID = 7, Text = "Sales Strategy" },
            new TreeViewItem() { ParentID = 6, ID = 8, Text = "Sales Forecast Table" },
            new TreeViewItem() { ParentID = 6, ID = 9, Text = "Explain Sales Forecast" },
            new TreeViewItem() { ParentID = 0, ID = 10, Text = "Financial Plan" },
            new TreeViewItem() { ParentID = 10, ID = 11, Text = "Financial Summary" },
            new TreeViewItem() { ParentID = 10, ID = 12, Text = "Investment Requirement" },
            new TreeViewItem() { ParentID = 0, ID = 13, Text = "Finish" },
            new TreeViewItem() { ParentID = 13, ID = 14, Text = "Executive Summary" }
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
                    foreach (var d in FinancialDocument.DocumentList)
                    {
                        if (d.ItemName == s.Text)
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
