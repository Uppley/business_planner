using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessPlanner.Utility
{
    class ProjectLoader
    {
        public static void load(string dPath,string tempPath)
        {
            try
            {
                if (Directory.Exists(tempPath))
                    Directory.Delete(tempPath,true);
                DirectoryInfo di = Directory.CreateDirectory(tempPath);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                if (File.Exists(dPath))
                {
                    ZipFile.ExtractToDirectory(dPath, tempPath);
                    ProjectConfig.projectPath = tempPath;
                    ProjectConfig.loadSettings();
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {

            }
        }

        public static void save(string dPath,string tempPath)
        {
            try
            {
                if (File.Exists(dPath + ProjectConfig.projectExtension))
                    File.Delete(dPath + ProjectConfig.projectExtension);
                ZipFile.CreateFromDirectory(tempPath,dPath+ProjectConfig.projectExtension);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                if (Directory.Exists(tempPath))
                {
                    Directory.Delete(tempPath, true);
                }
            }
        }

        public static void saveOnly(string dPath, string tempPath)
        {
            try
            {
                if (File.Exists(dPath + ProjectConfig.projectExtension))
                    File.Delete(dPath + ProjectConfig.projectExtension);
                ZipFile.CreateFromDirectory(tempPath, dPath + ProjectConfig.projectExtension);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }

        public static void saveAsOnly(string dPath, string tempPath)
        {
            try
            {
                if (File.Exists(dPath))
                    File.Delete(dPath);
                ZipFile.CreateFromDirectory(tempPath, dPath);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
}
