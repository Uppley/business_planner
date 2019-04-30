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
        public static string projectFile { get; set; }
        public static string projectBase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"BP Plans");

        public static List<string> projectList()
        {
            if(Directory.Exists(ProjectConfig.projectBase))
            {
                string[] projects = Directory.GetDirectories(ProjectConfig.projectBase);
                return projects.ToList();
            }
            else
            {
                Directory.CreateDirectory(ProjectConfig.projectBase);
                return new List<string>();
            }
            
        }
    }
}
