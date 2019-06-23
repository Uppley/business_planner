using BP.Resource;
using BP.Resource.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessPlanner
{
    public partial class ShowLinkWindow : Form
    {
        List<LinkItem> linkItems;
        public ShowLinkWindow()
        {
            InitializeComponent();
            ResourceLink resourceLinks = new ResourceLink();
            linkItems = resourceLinks.linkItems;
            setUpView();
        }

        private void setUpView()
        {
            var linkgroupList = linkItems.GroupBy(g => g.GroupName).Select(i => i.ToList()).ToList();
            int j = 0;
            foreach (var gb in tableLayoutPanel2.Controls.OfType<GroupBox>())
            {
                gb.Text = linkgroupList[j][0].GroupName;
                var i = 0;
                foreach(var lb in gb.Controls.OfType<LinkLabel>())
                {
                    lb.Text = linkgroupList[j][i].LinkName;
                    lb.Click += goToHyperlink;
                    i += 1;
                }

                j += 1;
            }           
            
        }

        private void goToHyperlink(object sender, EventArgs e)
        {
            var lb = (LinkLabel)sender;
            string hp = linkItems.FindAll(x => x.LinkName == lb.Text)[0].Link;
            ProcessStartInfo startInfo = new ProcessStartInfo(hp);
            Process.Start(startInfo);
        }
    }
}
