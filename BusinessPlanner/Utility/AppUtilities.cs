using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessPlanner
{
    class AppUtilities
    {
        public static Dictionary<string, object> mainData = new Dictionary<string, object>();

        public static Form mainForm { get; set; }

        public static Dictionary<string, object> CreateOrUpdateDict(string key, object data)
        {
            if (!AppUtilities.mainData.ContainsKey(key))
            {
                AppUtilities.mainData.Add(key, data);
            }
            else
            {
                AppUtilities.mainData[key] = data;
            }
            return AppUtilities.mainData;
        }

        public static bool IsEmailValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
