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
        //string rtfText = @"{\rtf1\ansi\ansicpg1252\deff0\nouicompat\deflang14346{\fonttbl{\f0\fnil\fcharset0 Calibri;}} {\*\generator Riched20 10.0.10586}\viewkind4\uc1 \pard\sa200\sl276\slmult1\f0\fs22\lang10 Enter Text Here.\par }";

        public DocumentCreator()
        {
            planType = AppUtilities.mainData["step1"].ToString();
            if (planType == "Quick Plan")
                documentList = QuickDocument.DocumentList;
            else if (planType == "Standard Plan")
                documentList = StandardDocument.DocumentList;
            else if (planType == "Financial Plan")
                documentList = FinancialDocument.DocumentList;
        }

        private string getCurrency(object data)
        {
            string c = "AED";
            if(int.Parse(data.ToString())==0)
            {
                c = "AED";
            }else if(int.Parse(data.ToString()) == 1)
            {
                c = "USD";
            }
            else if(int.Parse(data.ToString()) == 2)
            {
                c = "EURO";
            }
            return c;
        }
        
        private int isSWOT(object data)
        {
            int i = 0;
            var dt = (Dictionary<string,Boolean>)data;
            if(dt["swot"])
            {
                i = 1;
            }
            return i;
        }

        private string getSellType()
        {
            string t = "";
            if (AppUtilities.mainData["sellType"].ToString() == "I sell services.")
                t = "Service Summary";
            else if (AppUtilities.mainData["sellType"].ToString() == "I sell products.")
                t = "Product Summary";
            else
                t = "Product & Service Summary";
            return t;

        }
        private int isWeb(object data)
        {
            int i = 0;
            var dt = (Dictionary<string, Boolean>)data;
            if (dt["web"])
            {
                i = 1;
            }
            return i;
        }
        public string createQuickPackage()
        {
            string projectTitle = AppUtilities.mainData["step3"].ToString();
            projectPath = Path.Combine(ProjectConfig.projectBase,projectTitle+ProjectConfig.projectExtension);
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["is_startup"] = AppUtilities.mainData["step2"].ToString() == "This business plan is for a startup." ? "yes" : "no";
            tempPath = Path.Combine(ProjectConfig.projectBase, "~temp_"+projectTitle);
            try
            {
                if (Directory.Exists(tempPath))
                    Directory.Delete(tempPath,true);
                DirectoryInfo di = Directory.CreateDirectory(tempPath);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                Dictionary<string, int> progress = new Dictionary<string, int>();
                BPSettings bp = new BPSettings();
                bp.AddSetting("Title", projectTitle);
                bp.AddSetting("PlanType", planType);
                bp.AddSetting("StartUp", data["is_startup"]);
                bp.AddSetting("SWOT", isSWOT(AppUtilities.mainData["step4"]).ToString());
                bp.AddSetting("Website", isWeb(AppUtilities.mainData["step4"]).ToString());
                foreach (var d in documentList)
                {
                    if (d.Ftype == "rtf")
                    {
                        
                        if (isSWOT(AppUtilities.mainData["step4"]) != 1 && (d.ItemName == "Strengths" || d.ItemName == "Weaknesses" || d.ItemName == "Opportunities" || d.ItemName == "Threats"))
                            continue;
                        if (isWeb(AppUtilities.mainData["step4"]) != 1 && (d.ItemName == "Website Strategy" || d.ItemName == "Developments Requirements"))
                            continue;
                        if (data["is_startup"].ToString() != "yes" && d.ItemName == "Start Up Investment")
                            continue;
                        if ((d.ItemName == "Product Summary" || d.ItemName == "Service Summary" || d.ItemName == "Product & Service Summary") && !getSellType().Equals(d.ItemName))
                            continue;
                        rtb.Rtf = "";
                        progress.Add(d.DocumentName, 0);
                        rtb.SaveFile(Path.Combine(tempPath, d.DocumentName));
                        Debug.WriteLine("Fcrea "+d.DocumentName);
                    }
                    else if(d.Ftype == "xls")
                    {
                        ExcelReader excelReader = new ExcelReader();
                        excelReader.createExcelFile(Path.Combine(tempPath, "data.xls"),data);
                    }
                }
                bp.AddSetting("progress", progress);
                bp.SaveSetting(Path.Combine(tempPath, "settings.json"));
                
                ZipFile.CreateFromDirectory(tempPath, projectPath);
                Debug.WriteLine("Project setup complete !");
            }
            catch(Exception e)
            {
                Debug.WriteLine("Erro foun "+e.Message);
                
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

        public string createStandardPackage()
        {
            string projectTitle = AppUtilities.mainData["step5"].ToString();
            projectPath = Path.Combine(ProjectConfig.projectBase, projectTitle + ProjectConfig.projectExtension);
            string startDate = AppUtilities.mainData["step4"].ToString();
            DateTime oDate = DateTime.Parse(startDate);
            string plan_type = AppUtilities.mainData["step8"].ToString();
            int plan_duration = 6;
            if (plan_type != "I would like a standard-term business plan.")
            {
                plan_duration = 12;
            }
            List<string> months = new List<string>();
            months.Add(oDate.ToString("MMMM", CultureInfo.CreateSpecificCulture("en")));
            for (int i = 1; i < plan_duration; i++)
            {
                months.Add(oDate.AddMonths(i).ToString("MMM", CultureInfo.InvariantCulture));
            }
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["plan_duration"] = plan_duration;
            data["months"] = months;
            data["currency"] = getCurrency(AppUtilities.mainData["step2"]);
            data["is_startup"] = AppUtilities.mainData["step3"].ToString() == "This business plan is for a startup." ? "yes" : "no";
            tempPath = Path.Combine(ProjectConfig.projectBase, "~temp_" + projectTitle);
            try
            {
                if (Directory.Exists(tempPath))
                    Directory.Delete(tempPath, true);
                DirectoryInfo di = Directory.CreateDirectory(tempPath);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                Dictionary<string, int> progress = new Dictionary<string, int>();
                BPSettings bp = new BPSettings();
                bp.AddSetting("Title", projectTitle);
                bp.AddSetting("PlanType", planType);
                bp.AddSetting("Currency", getCurrency(AppUtilities.mainData["step2"]));
                bp.AddSetting("StartUp", data["is_startup"]);
                bp.AddSetting("Duration", plan_duration);
                bp.AddSetting("SWOT", isSWOT(AppUtilities.mainData["step6"]).ToString());
                bp.AddSetting("Website", isWeb(AppUtilities.mainData["step6"]).ToString());
                foreach (var d in documentList)
                {
                    if (d.Ftype == "rtf")
                    {
                        if (isSWOT(AppUtilities.mainData["step6"]) != 1 && (d.ItemName == "Strengths" || d.ItemName == "Weaknesses" || d.ItemName == "Opportunities" || d.ItemName == "Threats"))
                            continue;
                        if (isWeb(AppUtilities.mainData["step6"]) != 1 && (d.ItemName == "Website Strategy" || d.ItemName == "Developments Requirements"))
                            continue;
                        if (data["is_startup"].ToString() != "yes" && d.ItemName == "Start Up Investment")
                            continue;
                        if ((d.ItemName== "Product Summary" || d.ItemName == "Service Summary" || d.ItemName == "Product & Service Summary") && !getSellType().Equals(d.ItemName))
                            continue;
                        rtb.Rtf = "";
                        progress.Add(d.DocumentName, 0);
                        rtb.SaveFile(Path.Combine(tempPath, d.DocumentName));

                    }
                    else if (d.Ftype == "xls")
                    {
                        ExcelReader excelReader = new ExcelReader();
                        excelReader.createExcelFile(Path.Combine(tempPath, "data.xls"), data);
                    }
                }
                bp.AddSetting("progress", progress);
                bp.SaveSetting(Path.Combine(tempPath, "settings.json"));
                ZipFile.CreateFromDirectory(tempPath, projectPath);

                Debug.WriteLine("Project setup complete !");

            }
            catch (Exception e)
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

        public string createFinancialPackage()
        {
            string projectTitle = AppUtilities.mainData["step5"].ToString();
            projectPath = Path.Combine(ProjectConfig.projectBase, projectTitle + ProjectConfig.projectExtension);
            string startDate = AppUtilities.mainData["step4"].ToString();
            DateTime oDate = DateTime.Parse(startDate);
            string plan_type = AppUtilities.mainData["step7"].ToString();
            int plan_duration = 6;
            if (plan_type != "I would like a standard-term business plan.")
            {
                plan_duration = 12;
            }
            List<string> months = new List<string>();
            months.Add(oDate.ToString("MMMM", CultureInfo.CreateSpecificCulture("en")));
            for (int i = 1; i < plan_duration; i++)
            {
                months.Add(oDate.AddMonths(i).ToString("MMM", CultureInfo.InvariantCulture));
            }
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["plan_duration"] = plan_duration;
            data["months"] = months;
            data["currency"] = getCurrency(AppUtilities.mainData["step2"]);
            data["is_startup"] = AppUtilities.mainData["step3"].ToString() == "This business plan is for a startup." ? "yes" : "no";
            tempPath = Path.Combine(ProjectConfig.projectBase, "~temp_" + projectTitle);
            try
            {
                if (Directory.Exists(tempPath))
                    Directory.Delete(tempPath, true);
                DirectoryInfo di = Directory.CreateDirectory(tempPath);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                Dictionary<string, int> progress = new Dictionary<string, int>();
                BPSettings bp = new BPSettings();
                bp.AddSetting("Title", projectTitle);
                bp.AddSetting("PlanType", planType);
                bp.AddSetting("Currency", getCurrency(AppUtilities.mainData["step2"]));
                bp.AddSetting("StartUp", data["is_startup"]);
                bp.AddSetting("Duration", plan_duration);
                foreach (var d in documentList)
                {
                    if (d.Ftype == "rtf")
                    {
                        
                        if (data["is_startup"].ToString() != "yes" && d.ItemName == "Start Up Investment")
                            continue;
                        if ((d.ItemName == "Product Summary" || d.ItemName == "Service Summary" || d.ItemName == "Product & Service Summary") && !getSellType().Equals(d.ItemName))
                            continue;
                        rtb.Rtf = "";
                        progress.Add(d.DocumentName, 0);
                        rtb.SaveFile(Path.Combine(tempPath, d.DocumentName));

                    }
                    else if (d.Ftype == "xls")
                    {
                        ExcelReader excelReader = new ExcelReader();
                        excelReader.createExcelFile(Path.Combine(tempPath, "data.xls"), data);
                    }
                }
                bp.AddSetting("progress", progress);
                bp.SaveSetting(Path.Combine(tempPath, "settings.json"));
                ZipFile.CreateFromDirectory(tempPath, projectPath);

                Debug.WriteLine("Project setup complete !");

            }
            catch (Exception e)
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
