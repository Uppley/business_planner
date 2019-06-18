using BusinessPlanner.Utility;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
//using Outlook = Microsoft.Office.Interop.Outlook;

namespace BusinessPlanner.Partials
{
    public partial class ContactDialog : Form
    {
        SQLiteConnection conn;
        public ContactDialog()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, string> data = new Dictionary<string, string>();
                data["name"] = textBox1.Text;
                data["email"] = textBox2.Text;
                data["contact"] = textBox3.Text;
                data["address"] = richTextBox1.Text;
                data["company"] = textBox4.Text;
                

                conn = DatabaseReader.CreateConnection();
                DatabaseReader.CreateContactTable(conn);
                if(DatabaseReader.InsertContactData(data, conn))
                {
                    MessageBox.Show("Record added successfully !");
                    
                }
                else
                {
                    MessageBox.Show("Unable to add record !");
                }
                
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DatabaseReader.Close(conn);
                conn = null;
            }
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
