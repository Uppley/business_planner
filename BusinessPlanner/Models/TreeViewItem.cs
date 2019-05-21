using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPlanner.Utility
{
    public class TreeViewItem
    {
        public int ID { get; set; }
        public int ParentID { get; set; }
        public string Text { get; set; }
    }
}
