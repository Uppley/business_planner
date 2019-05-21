using BusinessPlanner.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPlanner.Utility
{
    class BPSettings
    {
        private List<Data> _data = new List<Data>();
        public void AddSetting(string prop,string val)
        {
            _data.Add(new Data() {property=prop,value=val});
        }

        public List<Data> ReadSettings(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                List<Data> items = JsonConvert.DeserializeObject<List<Data>>(json);
                return items; 
            }
        }

        public void SaveSetting(string path)
        {
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, _data);
            }
        }
    }
}
