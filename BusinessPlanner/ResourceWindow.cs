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
    public partial class ResourceWindow : Form
    {
        public ResourceWindow()
        {
            InitializeComponent();
        }

        private void OpenLinks_Click(object sender, EventArgs e)
        {
            ShowLinkWindow slw = new ShowLinkWindow();
            slw.Show();
        }

        private void OpenDocuments_Click(object sender, EventArgs e)
        {
            ShowDocumentWindow sdw = new ShowDocumentWindow();
            sdw.Show();
        }

        private void OpenVideos_Click(object sender, EventArgs e)
        {
            ShowVideoWindow svw = new ShowVideoWindow();
            svw.Show();
        }
    }
}
