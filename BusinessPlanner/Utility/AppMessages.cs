using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPlanner
{
    static class AppMessages
    {
        public static Dictionary<string, string> messages = new Dictionary<string, string>
        {
            {"exit_head","Exit Business Planner" },
            {"exit_body","Are you sure to quit Business Planner ?\nIf you are sure then click the Yes button else click No." },
            {"new_head","New Plan Confirmation" },
            {"new_body","Creating new plan requires the existing window to be closed.\nDo you want to close current operation ?" },
            {"loading","Loading...Please Wait !" },
            {"exporting","Exporting...Please Wait !" },
            {"export_success","File exported successfully !" },
            {"document_created","Creating documents...Please Wait !" },
            {"project_loading","Project loading...Please Wait !" },
            {"project_save","Saving Project...Please Wait !" },
            {"project_active","Error loading !\nThis project is already active." },
            {"data_save","Saving your data" },
            {"data_load","Loading your data" },
        };
        
    }
}
