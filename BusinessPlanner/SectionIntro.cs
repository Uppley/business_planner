﻿using BP.Data;
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
    public partial class SectionIntro : Form
    {
        public SectionIntro(string name)
        {
            InitializeComponent();
            BPData bpd = new BPData();
            var data = bpd.nodeItems.Find(x => x.name == name);
            if(data != null)
            {
                label1.Text = data.name;
                label2.Text = data.description;
            }
            
        }
    }
}
