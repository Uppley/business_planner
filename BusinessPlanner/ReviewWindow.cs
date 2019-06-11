using BusinessPlanner.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessPlanner
{
    public partial class ReviewWindow : Form
    {
        
        public Dictionary<string, string> report_files = new Dictionary<string, string>();
        public ReviewWindow()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView2.View = View.Details;
            listView1.ForeColor = Color.Red;
            listView2.ForeColor = Color.DarkGreen;
            DocumentProgressor dcp = new DocumentProgressor();
            var allSteps = dcp.fetchAllSteps();
            populateIncomplete(allSteps.Where(x => x.Value == 0));
            populateComplete(allSteps.Where(x => x.Value == 1));
        }

        private void populateComplete(IEnumerable<KeyValuePair<string, int>> data)
        {
            string nodeName = "None";
            string parentName = "None";
            foreach (var d in data)
            {
                if (ProjectConfig.projectSettings["PlanType"].ToString() == "Standard Plan")
                {
                    nodeName = StandardDocument.DocumentList.Find(x => x.DocumentName == d.Key).ItemName;
                    int pid = StandardConfig.treeViewList.Find(x => x.Text == nodeName).ParentID;
                    parentName = StandardConfig.treeViewList.Find(x => x.ID == pid).Text;
                }
                else if (ProjectConfig.projectSettings["PlanType"].ToString() == "Quick Plan")
                {
                    nodeName = QuickDocument.DocumentList.Find(x => x.DocumentName == d.Key).ItemName;
                    int pid = QuickConfig.treeViewList.Find(x => x.Text == nodeName).ParentID;
                    parentName = QuickConfig.treeViewList.Find(x => x.ID == pid).Text;
                }
                else if (ProjectConfig.projectSettings["PlanType"].ToString() == "Financial Plan")
                {
                    nodeName = FinancialDocument.DocumentList.Find(x => x.DocumentName == d.Key).ItemName;
                    int pid = FinancialConfig.treeViewList.Find(x => x.Text == nodeName).ParentID;
                    parentName = FinancialConfig.treeViewList.Find(x => x.ID == pid).Text;
                }
                listView2.Items.Add(new ListViewItem(new string[] { nodeName, parentName }));
            }
        }

        private void populateIncomplete(IEnumerable<KeyValuePair<string, int>> data)
        {
            string nodeName = "None";
            string parentName = "None";
            foreach (var d in data)
            {
                if (ProjectConfig.projectSettings["PlanType"].ToString() == "Standard Plan")
                {
                    nodeName = StandardDocument.DocumentList.Find(x => x.DocumentName == d.Key).ItemName;
                    int pid = StandardConfig.treeViewList.Find(x => x.Text == nodeName).ParentID;
                    parentName = StandardConfig.treeViewList.Find(x => x.ID == pid).Text;
                }
                else if (ProjectConfig.projectSettings["PlanType"].ToString() == "Quick Plan")
                {
                    nodeName = QuickDocument.DocumentList.Find(x => x.DocumentName == d.Key).ItemName;
                    int pid = QuickConfig.treeViewList.Find(x => x.Text == nodeName).ParentID;
                    parentName = QuickConfig.treeViewList.Find(x => x.ID == pid).Text;
                }
                else if (ProjectConfig.projectSettings["PlanType"].ToString() == "Financial Plan")
                {
                    nodeName = FinancialDocument.DocumentList.Find(x => x.DocumentName == d.Key).ItemName;
                    int pid = FinancialConfig.treeViewList.Find(x => x.Text == nodeName).ParentID;
                    parentName = FinancialConfig.treeViewList.Find(x => x.ID == pid).Text;
                }
                listView1.Items.Add(new ListViewItem(new string[] { nodeName, parentName }));
            }
        }
    }
}
