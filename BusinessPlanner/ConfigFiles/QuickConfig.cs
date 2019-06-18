using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPlanner.Utility
{
    class QuickConfig
    {
        public static List<TreeViewItem> treeViewList = new List<TreeViewItem>() {
            new TreeViewItem() { ParentID = 0, ID = 1, Text = "Home"},
            new TreeViewItem() { ParentID = 0, ID = 2, Text = "Kick Start" },
            new TreeViewItem() { ParentID = 2, ID = 3, Text = "Objectives" },
            new TreeViewItem() { ParentID = 2, ID = 4, Text = "Mission" },
            new TreeViewItem() { ParentID = 0, ID = 5, Text = "Company Insight" },
            new TreeViewItem() { ParentID = 5, ID = 6, Text = "Company Summary" },
            new TreeViewItem() { ParentID = 5, ID = 7, Text = "Ownership" },
            new TreeViewItem() { ParentID = 5, ID = 8, Text = "Start Up Investment" },
            new TreeViewItem() { ParentID = 0, ID = 10, Text = "What You Sell" },
            new TreeViewItem() { ParentID = 10, ID = 11, Text = "Product Summary" },
            new TreeViewItem() { ParentID = 10, ID = 12, Text = "Service Summary" },
            new TreeViewItem() { ParentID = 10, ID = 13, Text = "Product & Service Summary" },
            new TreeViewItem() { ParentID = 0, ID = 14, Text = "Market Analysis" },
            new TreeViewItem() { ParentID = 14, ID = 15, Text = "Market Summary" },
            new TreeViewItem() { ParentID = 14, ID = 16, Text = "Strengths" },
            new TreeViewItem() { ParentID = 14, ID = 17, Text = "Weaknesses" },
            new TreeViewItem() { ParentID = 14, ID = 18, Text = "Opportunities" },
            new TreeViewItem() { ParentID = 14, ID = 19, Text = "Threats" },
            new TreeViewItem() { ParentID = 14, ID = 20, Text = "Analysis Table" },
            new TreeViewItem() { ParentID = 14, ID = 21, Text = "Market Segmentation" },
            new TreeViewItem() { ParentID = 14, ID = 22, Text = "Website Strategy" },
            new TreeViewItem() { ParentID = 14, ID = 23, Text = "Developments Requirements" },
            new TreeViewItem() { ParentID = 0, ID = 24, Text = "Line Of Business" },
            new TreeViewItem() { ParentID = 24, ID = 25, Text = "Business Analysis" },
            new TreeViewItem() { ParentID = 24, ID = 26, Text = "Competitive Study" },
            new TreeViewItem() { ParentID = 0, ID = 27, Text = "Sales Statistics" },
            new TreeViewItem() { ParentID = 27, ID = 28, Text = "Sales Strategy" },
            new TreeViewItem() { ParentID = 0, ID = 29, Text = "Marketing Plan" },
            new TreeViewItem() { ParentID = 29, ID = 30, Text = "Strategy Summary" },
            new TreeViewItem() { ParentID = 29, ID = 31, Text = "Competitive Edge" },
            new TreeViewItem() { ParentID = 0, ID = 32, Text = "Finish" },
            new TreeViewItem() { ParentID = 32, ID = 33, Text = "Executive Summary" }
        };

        public static int getId()
        {

            return QuickConfig.treeViewList[QuickConfig.treeViewList.Count - 1].ID;
        }

        public static List<TreeViewItem> getAllNodes()
        {
            return QuickConfig.treeViewList;
        }

        public static List<TreeViewItem> getProjectNodes()
        {
            List<TreeViewItem> li = new List<TreeViewItem>();
            foreach (var s in QuickConfig.treeViewList)
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
            QuickConfig.treeViewList.Add(new TreeViewItem() { ParentID = pid, ID = id, Text = t });
        }
    }
}
