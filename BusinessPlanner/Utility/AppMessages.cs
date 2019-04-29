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
            {"loading","Loading...Please Wait !" },
            {"exporting","Exporting...Please Wait !" },
            {"export_success","File exported successfully !" },
        };
        
    }
}
