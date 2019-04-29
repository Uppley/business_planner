using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessPlanner
{
    class Utilities
    {
        public static Dictionary<string, object> mainData = new Dictionary<string, object>();

        public static Form mainForm { get; set; }

        public static Dictionary<string, object> CreateOrUpdateDict(string key, object data)
        {
            if (!Utilities.mainData.ContainsKey(key))
            {
                Utilities.mainData.Add(key, data);
            }
            else
            {
                Utilities.mainData[key] = data;
            }
            return Utilities.mainData;
        }
    }
}
