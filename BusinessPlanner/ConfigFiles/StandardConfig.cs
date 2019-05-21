using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPlanner.Utility
{
    public class StandardConfig
    {
        public static List<TreeViewItem> treeViewList = new List<TreeViewItem>() {
            new TreeViewItem() {ParentID=0,ID=1,Text="Home"},
            new TreeViewItem() { ParentID = 0, ID = 2, Text = "Kick Start" },
            new TreeViewItem() { ParentID = 2, ID = 3, Text = "Objectives" },
             new TreeViewItem() { ParentID = 2, ID =4, Text = "Mission" },
            new TreeViewItem() { ParentID = 0, ID = 5, Text = "Company Insight" },
            new TreeViewItem() { ParentID = 5, ID = 6, Text = "Company Summary" },
            new TreeViewItem() { ParentID = 5, ID = 7, Text = "Ownership" },
            new TreeViewItem() { ParentID = 5, ID = 8, Text = "Start Up Table" },
            new TreeViewItem() { ParentID = 0, ID = 9, Text = "Product Selling" },
            new TreeViewItem() { ParentID = 9, ID = 10, Text = "Product Summary" },
            new TreeViewItem() { ParentID = 0, ID = 11, Text = "Market Analysis" },
            new TreeViewItem() { ParentID = 11, ID = 12, Text = "Market Summary" },
            new TreeViewItem() { ParentID = 11, ID = 13, Text = "Analysis Table" },
            new TreeViewItem() { ParentID = 11, ID = 14, Text = "Market Segmentation" },
            new TreeViewItem() { ParentID = 0, ID = 15, Text = "Line Of Business" },
            new TreeViewItem() { ParentID = 15, ID = 16, Text = "Business Analysis" },
            new TreeViewItem() { ParentID = 15, ID = 17, Text = "Competitive Study" },
            new TreeViewItem() { ParentID = 0, ID = 18, Text = "Sales Forecast" },
            new TreeViewItem() { ParentID = 18, ID = 19, Text = "Sales Strategy" },
            new TreeViewItem() { ParentID = 18, ID = 20, Text = "Sales Forecast Table" },
            new TreeViewItem() { ParentID = 18, ID = 21, Text = "Explain Sales Forecast" },
            new TreeViewItem() { ParentID = 0, ID = 22, Text = "Marketing Plan" },
            new TreeViewItem() { ParentID = 22, ID = 23, Text = "Strategy Summary" },
            new TreeViewItem() { ParentID = 22, ID = 24, Text = "Competitive Edge" },
            new TreeViewItem() { ParentID = 0, ID = 25, Text = "Financial Plan" },
            new TreeViewItem() { ParentID = 25, ID = 26, Text = "Financial Summary" },
            new TreeViewItem() { ParentID = 25, ID = 27, Text = "Investment Requirement" },
            new TreeViewItem() { ParentID = 0, ID = 28, Text = "Finish" },
            new TreeViewItem() { ParentID = 28, ID = 29, Text = "Executive Summary" }
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
                        if(d.ItemName==s.Text)
                        {
                            li.Add(s);
                        }
                    }
                }
            }
            return li;
        }

        public static void updateNodes(int pid,int id,string t)
        {
            StandardConfig.treeViewList.Add(new TreeViewItem() { ParentID = pid, ID = id, Text = t });
        }
    }
}
