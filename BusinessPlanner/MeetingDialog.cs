using BusinessPlanner.Utility;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
//using Outlook = Microsoft.Office.Interop.Outlook;

namespace BusinessPlanner.Partials
{
    public partial class MeetingDialog : Form
    {
        //Outlook.Application Oapp;
        SQLiteConnection conn;
        public MeetingDialog()
        {
            InitializeComponent();
            //Oapp = new Outlook.Application();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                /*Outlook.AppointmentItem appt = Oapp.CreateItem(Outlook.OlItemType.olAppointmentItem) as Outlook.AppointmentItem;
                appt.Subject = textBox3.Text;
                appt.MeetingStatus = Outlook.OlMeetingStatus.olMeeting;
                appt.Location = textBox2.Text;
                appt.Start = dateTimePicker1.Value;
                appt.Duration = int.Parse(comboBox1.Text);
                appt.Body = richTextBox1.Text;
                Outlook.Recipient recipRequired = appt.Recipients.Add(textBox1.Text);
                recipRequired.Type = (int)Outlook.OlMeetingRecipientType.olRequired;
                appt.Recipients.ResolveAll();
                appt.Display(false);
                this.Close();*/

                Dictionary<string, string> data = new Dictionary<string, string>();
                data["name"] = textBox1.Text;
                data["email"] = textBox2.Text;
                data["subject"] = textBox3.Text;
                data["body"] = richTextBox1.Text;
                data["location"] = textBox4.Text;
                data["date"] = dateTimePicker1.Value.Date.ToString();
                data["duration"] = comboBox1.SelectedItem.ToString();

                conn = DatabaseReader.CreateConnection();
                DatabaseReader.CreateMeetingTable(conn);
                if(DatabaseReader.InsertMeetingData(data, conn))
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
