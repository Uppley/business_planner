using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessPlanner.Utility
{
    class DocumentCreator
    {
        public readonly List<DocumentItem>documentList;
        private string projectPath;
        private string tempPath;
        private string planType;
        RichTextBox rtb = new RichTextBox();
        string rtfText = @"{\rtf1\ansi\ansicpg1252\deff0\nouicompat\deflang14346{\fonttbl{\f0\fnil\fcharset0 Calibri;}} {\*\generator Riched20 10.0.10586}\viewkind4\uc1 \pard\sa200\sl276\slmult1\f0\fs22\lang10 Enter Text Here.\par }";

        public DocumentCreator()
        {
            planType = AppUtilities.mainData["step1"].ToString();
            if (planType == "Plan As You Go")
                documentList = PlanAsDocument.DocumentList;
            else if (planType == "Standard Plan: Multiple Topics and Linked Financial Tables")
                documentList = StandardDocument.DocumentList;
            else if (planType == "Financial Plan: Topics Related To Financial Domain Only")
                documentList = FinancialDocument.DocumentList;
        }

        public string getCurrency()
        {
            string c = "AED";
            if(int.Parse(AppUtilities.mainData["step2"].ToString())==0)
            {
                c = "AED";
            }else if(int.Parse(AppUtilities.mainData["step2"].ToString()) == 1)
            {
                c = "USD";
            }
            else if(int.Parse(AppUtilities.mainData["step2"].ToString()) == 2)
            {
                c = "EURO";
            }
            return c;
        }
        public string createPackage()
        {
            string projectTitle = AppUtilities.mainData["step5"].ToString();
            projectPath = Path.Combine(ProjectConfig.projectBase,projectTitle+ProjectConfig.projectExtension);
            string startDate = AppUtilities.mainData["step4"].ToString();
            DateTime oDate = DateTime.Parse(startDate);
            string plan_type = AppUtilities.mainData["step8"].ToString();
            int plan_duration = 6;
            if(plan_type != "I would like a standard-term business plan.")
            {
                plan_duration = 12;
            }
            List<string> months = new List<string>();
            months.Add(oDate.ToString("MMMM", CultureInfo.CreateSpecificCulture("en")));
            for(int i=1;i < plan_duration;i++)
            {
                months.Add(oDate.AddMonths(i).ToString("MMM", CultureInfo.InvariantCulture));
            }
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["plan_duration"] = plan_duration;
            data["months"] = months;
            data["currency"] = getCurrency();
            data["is_startup"] = AppUtilities.mainData["step3"].ToString() == "This business plan is for a startup." ? "yes" : "no";
            tempPath = Path.Combine(ProjectConfig.projectBase, "~temp_"+projectTitle);
            try
            {
                if (Directory.Exists(tempPath))
                    Directory.Delete(tempPath,true);
                DirectoryInfo di = Directory.CreateDirectory(tempPath);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                foreach (var d in documentList)
                {
                    if (d.Ftype == "rtf")
                    {
                        rtb.Rtf = rtfText;
                        rtb.SaveFile(Path.Combine(tempPath, d.DocumentName));
                    }
                    else if(d.Ftype == "xls")
                    {
                        ExcelReader excelReader = new ExcelReader();
                        excelReader.createExcelFile(Path.Combine(tempPath, "data.xls"),data);
                    }
                }
                BPSettings bp = new BPSettings();
                bp.AddSetting("Title", projectTitle);
                bp.AddSetting("PlanType", planType);
                bp.AddSetting("Currency", getCurrency());
                bp.SaveSetting(Path.Combine(tempPath, "settings.json"));
                ZipFile.CreateFromDirectory(tempPath, projectPath);

                Debug.WriteLine("Project setup complete !");
                
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                
            }
            finally
            {
                if (Directory.Exists(tempPath))
                {
                    Directory.Delete(tempPath, true);
                }
                
            }

            return projectPath;
        }
    }
}
