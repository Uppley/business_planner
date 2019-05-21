using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPlanner.Utility
{
    class PlanAsConfig
    {
        public static List<TreeViewItem> treeViewList = new List<TreeViewItem>() {
            new TreeViewItem() {ParentID=0,ID=1,Text="Home"},
            new TreeViewItem() { ParentID = 0, ID = 2, Text = "Kick Start" },
            new TreeViewItem() { ParentID = 2, ID = 3, Text = "Objectives" },
            new TreeViewItem() { ParentID = 0, ID = 4, Text = "Company Insight" },
            new TreeViewItem() { ParentID = 4, ID = 5, Text = "Company Summary" },
            new TreeViewItem() { ParentID = 4, ID = 6, Text = "Ownership" },
            new TreeViewItem() { ParentID = 4, ID = 7, Text = "Start Up Table" },
            new TreeViewItem() { ParentID = 0, ID = 8, Text = "Product Selling" },
            new TreeViewItem() { ParentID = 8, ID = 9, Text = "Product Summary" },
            new TreeViewItem() { ParentID = 0, ID = 10, Text = "Market Analysis" },
            new TreeViewItem() { ParentID = 10, ID = 11, Text = "Market Summary" },
            new TreeViewItem() { ParentID = 10, ID = 12, Text = "Analysis Table" },
            new TreeViewItem() { ParentID = 10, ID = 13, Text = "Market Segmentation" },
            new TreeViewItem() { ParentID = 0, ID = 14, Text = "Line Of Business" },
            new TreeViewItem() { ParentID = 14, ID = 15, Text = "Business Analysis" },
            new TreeViewItem() { ParentID = 14, ID = 16, Text = "Competitive Study" },
            new TreeViewItem() { ParentID = 0, ID = 17, Text = "Sales Forecast" },
            new TreeViewItem() { ParentID = 17, ID = 18, Text = "Sales Strategy" },
            new TreeViewItem() { ParentID = 17, ID = 19, Text = "Sales Forecast Table" },
            new TreeViewItem() { ParentID = 17, ID = 20, Text = "Explain Sales Forecast" },
            new TreeViewItem() { ParentID = 0, ID = 21, Text = "Marketing Plan" },
            new TreeViewItem() { ParentID = 21, ID = 22, Text = "Strategy Summary" },
            new TreeViewItem() { ParentID = 21, ID = 23, Text = "Competitive Edge" },
            new TreeViewItem() { ParentID = 0, ID = 24, Text = "Financial Plan" },
            new TreeViewItem() { ParentID = 24, ID = 25, Text = "Financial Summary" },
            new TreeViewItem() { ParentID = 24, ID = 26, Text = "Investment Requirement" },
            new TreeViewItem() { ParentID = 0, ID = 27, Text = "Finish" },
            new TreeViewItem() { ParentID = 27, ID = 28, Text = "Executive Summary" }
        };

        public static int getId()
        {

            return PlanAsConfig.treeViewList[PlanAsConfig.treeViewList.Count - 1].ID;
        }

        public static List<TreeViewItem> getAllNodes()
        {
            return PlanAsConfig.treeViewList;
        }

        public static List<TreeViewItem> getProjectNodes()
        {
            List<TreeViewItem> li = new List<TreeViewItem>();
            foreach (var s in PlanAsConfig.treeViewList)
            {
                if (s.ParentID == 0)
                {
                    li.Add(s);
                }
                else
                {
                    foreach (var d in PlanAsDocument.DocumentList)
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
            PlanAsConfig.treeViewList.Add(new TreeViewItem() { ParentID = pid, ID = id, Text = t });
        }
    }
}
