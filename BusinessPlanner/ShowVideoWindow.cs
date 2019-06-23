using BP.Resource;
using BP.Resource.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessPlanner
{
    public partial class ShowVideoWindow : Form
    {
        List<VideoItem> resourceVideos = new List<VideoItem>();
        public ShowVideoWindow()
        {
            InitializeComponent();
            ResourceVideo resourceVideo = new ResourceVideo();
            resourceVideos = resourceVideo.videoItems;
            setUpView();
        }

        private void setUpView()
        {
            int columns = 1;
            int rows = resourceVideos.Count() % columns == 0 ? resourceVideos.Count() / columns : (resourceVideos.Count() / columns) + 1;
            //Button[] buttons = new Button[resourceVideos.Count()];
            TableLayoutPanel tbl = new TableLayoutPanel();
            tbl.RowCount = rows;
            tbl.AutoScroll = true;
            tbl.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
            tbl.ColumnCount = columns;
            tbl.Dock = DockStyle.Fill;
            for (int i = 0; i < tbl.RowCount; i++)
            {
                tbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 400));
            }
            for (int i = 0; i < tbl.ColumnCount; i++)
            {
                tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            }
            TableLayoutPanel[] panels = new TableLayoutPanel[resourceVideos.Count];
            
            Label[] labels = new Label[resourceVideos.Count];
            WebBrowser[] wb = new WebBrowser[resourceVideos.Count];
            for (var i = 0; i < panels.Length; i++)
            {
                panels[i] = new TableLayoutPanel();
                panels[i].Dock = DockStyle.Fill;
                panels[i].RowCount = 2;
                panels[i].GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
                panels[i].ColumnCount = 1;
                labels[i] = new Label();
                labels[i].Size = new Size(600, 30);
                labels[i].Text = resourceVideos[i].Title;
                labels[i].Margin = new Padding(5);
                labels[i].Font = new Font("Arial", 12,FontStyle.Bold);
                wb[i] = new WebBrowser();
                wb[i].Size = new Size(600, 350);
                
                wb[i].DocumentText = String.Format("<html><head>" +
                    "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge\"/>" +
                    "</head><body>" +
                    "<iframe width=\"100%\" height=\"320\" src=\"https://www.youtube.com/embed/{0}?autoplay=0\"" +
                    "frameborder = \"0\" ; encrypted-media\" allowfullscreen></iframe>" +
                    "</body></html>", resourceVideos[i].ID);
                panels[i].Controls.Add(labels[i],0,0);
                panels[i].Controls.Add(wb[i],0,1);
                tbl.Controls.Add(panels[i]);
            }

            panel2.Controls.Add(tbl);
        }

        
    }
}
