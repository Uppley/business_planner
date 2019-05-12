using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        RichTextBox rtb = new RichTextBox();
        string rtfText = @"{\rtf1\ansi\ansicpg1252\deff0\nouicompat\deflang14346{\fonttbl{\f0\fnil\fcharset0 Calibri;}} {\*\generator Riched20 10.0.10586}\viewkind4\uc1 \pard\sa200\sl276\slmult1\f0\fs22\lang10 Enter Text Here.\par }";

        public DocumentCreator()
        {
            documentList = DocumentRecord.DocumentList;
        }
        public string createPackage()
        {
            projectPath = Path.Combine(ProjectConfig.projectBase,Utilities.mainData["step5"].ToString()+ProjectConfig.projectExtension);
            tempPath = Path.Combine(ProjectConfig.projectBase, "~temp_"+Utilities.mainData["step5"].ToString());
            try
            {
                if (Directory.Exists(tempPath))
                    Directory.Delete(tempPath,true);
                DirectoryInfo di = Directory.CreateDirectory(tempPath);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                foreach (var d in documentList)
                {
                    
                        rtb.Rtf = rtfText;
                        rtb.SaveFile(Path.Combine(tempPath, d.DocumentName));
                }
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
