using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPlanner.Utility
{
    public class StandardConfig
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
            new TreeViewItem() { ParentID = 5, ID = 9, Text = "Company Expenditure" },
            new TreeViewItem() { ParentID = 0, ID = 10, Text = "Product Selling" },
            new TreeViewItem() { ParentID = 10, ID = 11, Text = "Product Summary" },
            new TreeViewItem() { ParentID = 0, ID = 12, Text = "Market Analysis" },
            new TreeViewItem() { ParentID = 12, ID = 13, Text = "Market Summary" },
            new TreeViewItem() { ParentID = 12, ID = 13, Text = "Strengths" },
            new TreeViewItem() { ParentID = 12, ID = 13, Text = "Weaknesses" },
            new TreeViewItem() { ParentID = 12, ID = 13, Text = "Opportunities" },
            new TreeViewItem() { ParentID = 12, ID = 13, Text = "Threats" },
            new TreeViewItem() { ParentID = 12, ID = 14, Text = "Analysis Table" },
            new TreeViewItem() { ParentID = 12, ID = 15, Text = "Market Segmentation" },
            new TreeViewItem() { ParentID = 12, ID = 17, Text = "Website Strategy" },
            new TreeViewItem() { ParentID = 12, ID = 18, Text = "Developments Requirements" },
            new TreeViewItem() { ParentID = 0, ID = 19, Text = "Line Of Business" },
            new TreeViewItem() { ParentID = 19, ID = 20, Text = "Business Analysis" },
            new TreeViewItem() { ParentID = 19, ID = 21, Text = "Competitive Study" },
            new TreeViewItem() { ParentID = 0, ID = 22, Text = "Sales Statistics" },
            new TreeViewItem() { ParentID = 22, ID = 23, Text = "Sales Strategy" },
            new TreeViewItem() { ParentID = 22, ID = 24, Text = "Sales Forecast Table" },
            new TreeViewItem() { ParentID = 22, ID = 25, Text = "Explain Sales Forecast" },
            new TreeViewItem() { ParentID = 0, ID = 26, Text = "Marketing Plan" },
            new TreeViewItem() { ParentID = 26, ID = 27, Text = "Strategy Summary" },
            new TreeViewItem() { ParentID = 26, ID = 28, Text = "Competitive Edge" },
            new TreeViewItem() { ParentID = 0, ID = 29, Text = "Financial Plan" },
            new TreeViewItem() { ParentID = 29, ID = 30, Text = "Financial Summary" },
            new TreeViewItem() { ParentID = 29, ID = 31, Text = "Financial Statement" },
            new TreeViewItem() { ParentID = 0, ID = 32, Text = "Finish" },
            new TreeViewItem() { ParentID = 32, ID = 33, Text = "Executive Summary" }
        };
        
        public static int getId()
        {

            return StandardConfig.treeViewList[StandardConfig.treeViewList.Count - 1].ID;
        }

        public static List<TreeViewItem> getAllNodes()
        {
            return StandardConfig.treeViewList;
        }

        public static List<TreeViewItem> getProjectNodes()
        {
            List<TreeViewItem> li = new List<TreeViewItem>();
            foreach(var s in StandardConfig.treeViewList)
            {
                
                if (s.ParentID == 0)
                {
                    li.Add(s);
                }
                else
                {
                    foreach(var d in StandardDocument.DocumentList)
                    {
                        if(d.ItemName==s.Text && File.Exists(ProjectConfig.projectPath+"//"+d.DocumentName))
                        {
                            li.Add(s);
                        }
                    }
                }
            }
            return li;
        }
    }
}
