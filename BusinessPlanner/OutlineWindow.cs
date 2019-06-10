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
    public partial class OutlineWindow : Form
    {
        List<DocumentItem> documentList = new List<DocumentItem>();
        public Dictionary<string,string> report_files = new Dictionary<string,string>();
        public OutlineWindow()
        {
            InitializeComponent();
            setTreeNodes();
            treeView1.ExpandAll();
        }

        public void setTreeNodes()
        {
            PopulateTreeView(0, null);
        }

        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {
            int total_files = Directory.GetFiles(ProjectConfig.projectPath).Length;
            List<TreeViewItem> filteredItems = null;
            if (ProjectConfig.projectSettings["PlanType"].ToString() == "Standard Plan")
            {
                filteredItems = StandardConfig.getProjectNodes().Where(item => item.ParentID == parentId).ToList();
            }
            else if (ProjectConfig.projectSettings["PlanType"].ToString() == "Quick Plan")
            {
                filteredItems = QuickConfig.getProjectNodes().Where(item => item.ParentID == parentId).ToList();
            }
            else if (ProjectConfig.projectSettings["PlanType"].ToString() == "Financial Plan")
            {
                filteredItems = FinancialConfig.getProjectNodes().Where(item => item.ParentID == parentId).ToList();
            }
            else
            {
                MessageBox.Show("We encountered some config issues");
            }

            TreeNode childNode;
            foreach (var i in filteredItems)
            {
                if (parentNode == null)
                    childNode = this.treeView1.Nodes.Add(i.Text);
                else
                    childNode = parentNode.Nodes.Add(i.Text);

                PopulateTreeView(i.ID, childNode);
            }
        }

    }
}
