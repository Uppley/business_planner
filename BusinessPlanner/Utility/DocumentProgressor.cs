using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPlanner.Utility
{
    class DocumentProgressor
    {
        private Dictionary<string,int> filedata;
        BPSettings bp;
        public DocumentProgressor()
        {
            bp = new BPSettings();
            var data = ProjectConfig.projectSettings["progress"];
            filedata = JObject.FromObject(data).ToObject<Dictionary<string, int>>();
        }
        public int totalSteps()
        {
            return filedata.Count;
        }

        public int completedSteps()
        {
            var ctp = filedata.Where(x => x.Value == 1);
            return ctp.Count();
        }

        public void updateProgress(string fname,int i)
        {
            filedata[fname] = i;
            ProjectConfig.projectSettings["progress"] = filedata;
        }

        public Dictionary<string,int> fetchAllSteps()
        {
            return filedata;
        }
    }
}
