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
    public partial class SplashScreen : Form
    {
        Timer tmr;
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void SplashScreen_Shown(object sender, EventArgs e)
        {
            tmr = new Timer();
            tmr.Interval = 3000;
            tmr.Start();
            tmr.Tick += tmr_Tick;
        }

        void tmr_Tick(object sender, EventArgs e)

        {
            tmr.Stop();
            MainWindow mf = new MainWindow();
            mf.Show();
            this.Hide();
        }
    }
}
