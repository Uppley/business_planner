﻿using System;
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
    public partial class SStep7Dialog : Form
    {
        private String mData { get; set; }

        public SStep7Dialog()
        {
            InitializeComponent();
            if (AppUtilities.mainData.ContainsKey("sellType"))
            {
                var cb = this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Text == AppUtilities.mainData["sellType"].ToString());
                cb.Checked = true;
            }
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            SStep6Dialog st6 = new SStep6Dialog();
            this.Hide();
            st6.ShowDialog();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var cb = this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            this.mData = cb.Text;
            AppUtilities.CreateOrUpdateDict("sellType", this.mData);
            SStep8Dialog st8 = new SStep8Dialog();
            this.Hide();
            st8.ShowDialog();
        }

        private void SStep7Dialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
