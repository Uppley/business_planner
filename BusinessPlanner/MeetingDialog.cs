using System;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace BusinessPlanner.Partials
{
    public partial class MeetingDialog : Form
    {
        Outlook.Application Oapp;
        public MeetingDialog()
        {
            InitializeComponent();
            Oapp = new Outlook.Application();
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

            }catch(Exception ex)
            {
                MessageBox.Show("We encountered some issue with Outlook.");
            }
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
