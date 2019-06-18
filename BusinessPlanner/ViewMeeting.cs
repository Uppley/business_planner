using BusinessPlanner.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessPlanner
{
    public partial class viewMeeting : Form
    {
        SQLiteConnection conn;
        public viewMeeting()
        {
            InitializeComponent();
            try
            {
                conn = DatabaseReader.CreateConnection();
                SQLiteDataReader sdr = DatabaseReader.ReadData("MeetingTable", conn);
                DataTable dt = new DataTable();
                dt.Columns.Add("Id");
                dt.Columns.Add("Name");
                dt.Columns.Add("Email");
                dt.Columns.Add("Subject");
                dt.Columns.Add("Body");
                dt.Columns.Add("Location");
                dt.Columns.Add("Date");
                dt.Columns.Add("Duration");
                while (sdr.Read())
                {
                    DataRow row = dt.NewRow();
                    row["Id"] = sdr["Id"];
                    row["Name"] = sdr["Name"];
                    row["Email"] = sdr["Email"];
                    row["Subject"] = sdr["Subject"];
                    row["Location"] = sdr["Location"];
                    row["Body"] = sdr["Body"];
                    row["Date"] = sdr["Date"];
                    row["Duration"] = sdr["Duration"];
                    dt.Rows.Add(row);
                }
                sdr.Close();
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].ReadOnly = true;
            }
            catch(Exception e)
            {
                MessageBox.Show("Unable to fetch data !");
            }
            finally
            {
                DatabaseReader.Close(conn);
                conn = null;
            }
        }

        public viewMeeting(string date)
        {
            InitializeComponent();
            try
            {
                conn = DatabaseReader.CreateConnection();
                SQLiteDataReader sdr = DatabaseReader.getTodayMeeting(date,conn);
                DataTable dt = new DataTable();
                dt.Columns.Add("Id");
                dt.Columns.Add("Name");
                dt.Columns.Add("Email");
                dt.Columns.Add("Subject");
                dt.Columns.Add("Body");
                dt.Columns.Add("Location");
                dt.Columns.Add("Date");
                dt.Columns.Add("Duration");
                while (sdr.Read())
                {
                    DataRow row = dt.NewRow();
                    row["Id"] = sdr["Id"];
                    row["Name"] = sdr["Name"];
                    row["Email"] = sdr["Email"];
                    row["Subject"] = sdr["Subject"];
                    row["Location"] = sdr["Location"];
                    row["Body"] = sdr["Body"];
                    row["Date"] = sdr["Date"];
                    row["Duration"] = sdr["Duration"];
                    dt.Rows.Add(row);
                }
                sdr.Close();
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].ReadOnly = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                DatabaseReader.Close(conn);
                conn = null;
            }
        }



        private void DataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value);
            try
            {
                conn = DatabaseReader.CreateConnection();
                if(DatabaseReader.RemoveData("MeetingTable",id,conn))
                    MessageBox.Show("Row deleted successfully !");
                else
                    MessageBox.Show("Unable to delete data !");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DatabaseReader.Close(conn);
                conn = null;
            }
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            Dictionary<string, string> data = new Dictionary<string, string>();
            int id = Convert.ToInt32(row.Cells[0].Value);
            data["name"] = row.Cells[1].Value.ToString();
            data["email"] = row.Cells[2].Value.ToString();
            data["subject"] = row.Cells[3].Value.ToString();
            data["body"] = row.Cells[4].Value.ToString();
            data["location"] = row.Cells[5].Value.ToString();
            data["date"] = row.Cells[6].Value.ToString();
            data["duration"] = row.Cells[7].Value.ToString();

            try
            {
                conn = DatabaseReader.CreateConnection();
                if (DatabaseReader.UpdateMeetingData(id, data, conn))
                    MessageBox.Show("Record updated successfully !");
                else
                    MessageBox.Show("Unable to update record !");
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
    }
}
