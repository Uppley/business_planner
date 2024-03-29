﻿using System;
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
    public partial class SStep6Dialog : Form
    {
        public Dictionary<string, Boolean> mData;
        public SStep6Dialog()
        {
            InitializeComponent();
            mData = new Dictionary<string, bool>();
            if (AppUtilities.mainData.ContainsKey("step6"))
            {
                Dictionary<string, Boolean> data = new Dictionary<string, bool>();
                data = (Dictionary<string, Boolean>)AppUtilities.mainData["step6"];
                checkBox1.Checked = data["swot"];
                checkBox2.Checked = data["web"];
            }
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            SStep5Dialog st5 = new SStep5Dialog();
            this.Hide();
            st5.ShowDialog();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.mData["swot"] = checkBox1.Checked;
            this.mData["web"] = checkBox2.Checked;
            AppUtilities.CreateOrUpdateDict("step6",this.mData);
            Debug.WriteLine(AppUtilities.mainData);
            SStep7Dialog st7 = new SStep7Dialog();
            this.Hide();
            st7.ShowDialog();
        }


        private void SStep6Dialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
