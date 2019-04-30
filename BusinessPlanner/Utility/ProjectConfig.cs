using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPlanner.Utility
{
    class ProjectConfig
    {
        public static string projectPath { get; set; }
        public static string projectExtension = ".bpx";
        public static string projectFile { get; set; }
        public static string projectBase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"BP Plans");

        public static List<string> projectList()
        {
            if(Directory.Exists(ProjectConfig.projectBase))
            {
                DirectoryInfo di = new DirectoryInfo(ProjectConfig.projectBase);
                return di.GetFiles("*"+ProjectConfig.projectExtension).OrderByDescending(f => f.LastWriteTime).Select(a => a.Name).ToList();
                
            }
            else
            {
                Directory.CreateDirectory(ProjectConfig.projectBase);
                return new List<string>();
            }
            
        }
    }
}
