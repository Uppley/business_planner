using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessPlanner.Utility
{
    class DatabaseReader
    {
        public static SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;
            sqlite_conn = new SQLiteConnection("Data Source="+ProjectConfig.projectPath+"//user_config.db; Version = 3; Compress = True;");
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return sqlite_conn;
        }

        public static void CreateMeetingTable(SQLiteConnection conn)
        {

            SQLiteCommand sqlite_cmd;
            string Createsql = "CREATE TABLE IF NOT EXISTS MeetingTable(Id INTEGER PRIMARY KEY,Name VARCHAR(20) NOT NULL, Email VARCHAR(20) NOT NULL,Subject VARCHAR(80) NOT NULL,Body TEXT,Location VARCHAR(50) NOT NULL,Date TEXT NOT NULL,Duration Varchar(20))";
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();
        }

        public static void CreateContactTable(SQLiteConnection conn)
        {

            SQLiteCommand sqlite_cmd;
            string Createsql = "CREATE TABLE IF NOT EXISTS ContactTable(Id INTEGER PRIMARY KEY, Name VARCHAR(20) NOT NULL, Email VARCHAR(20) NOT NULL, Contact VARCHAR(20) NOT NULL, Address TEXT, Company VARCHAR(30))";
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();
        }

        public static bool InsertMeetingData(Dictionary<string,string>data,SQLiteConnection conn)
        {
            if(DatabaseReader.validateMeetingData(data))
            {
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = "INSERT INTO MeetingTable(Name,Email,Subject,Body,Location,Date,Duration) VALUES('" + data["name"] + "','" + data["email"] + "','" + data["subject"] + "','" + data["body"] + "','" + data["location"] + "','" + data["date"] + "','" + data["duration"] + "'); ";
                if (sqlite_cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }

        public static bool InsertContactData(Dictionary<string, string> data, SQLiteConnection conn)
        {
            if(validateContactData(data))
            {
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = "INSERT INTO ContactTable(Name,Email,Contact,Address,Company) VALUES('" + data["name"] + "','" + data["email"] + "','" + data["contact"] + "','" + data["address"] + "','" + data["company"] + "'); ";
                if (sqlite_cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }

        public static SQLiteDataReader ReadData(string table,SQLiteConnection conn)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM "+table;
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            return sqlite_datareader;
        }

        public static SQLiteDataReader getTodayMeeting(string date,SQLiteConnection conn)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM MeetingTable where Date = '" + date + "';";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            return sqlite_datareader;
        }

        public static int getMeetingCount()
        {
            int output = 0;
            SQLiteConnection conn=DatabaseReader.CreateConnection();
            try
            {
                SQLiteDataReader sqlite_datareader;
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = "select count(id) from MeetingTable where Date = '" + DateTime.Now.Date.ToString() + "';";
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    output = Convert.ToInt32(sqlite_datareader[0]);
                    sqlite_datareader.Close();
                }
                
                return output;
            }
            catch(Exception e)
            {
                
                return output;
            }
            finally
            {
                DatabaseReader.Close(conn);
            }
            
            
        }


        public static bool UpdateMeetingData(int id,Dictionary<string, string> data, SQLiteConnection conn)
        {
            if (DatabaseReader.validateMeetingData(data))
            {
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = "UPDATE  MeetingTable SET Name='" + data["name"] + "',Email='" + data["email"] + "',Subject='" + data["subject"] + "',Body='" + data["body"] + "',Location='" + data["location"] + "',Date='" + data["date"] + "',Duration='" + data["duration"] + "' where Id=" + id + "; ";
                if (sqlite_cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

       

        public static bool UpdateContactData(int id,Dictionary<string, string> data, SQLiteConnection conn)
        {
            if(DatabaseReader.validateContactData(data))
            {
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = "UPDATE  ContactTable SET Name='" + data["name"] + "',Email='" + data["email"] + "',Contact='" + data["contact"] + "',Address='" + data["address"] + "',Company='" + data["company"] + "where Id=" + id + "; ";
                if (sqlite_cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }

        public static bool RemoveData(string table,int id, SQLiteConnection conn)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "DELETE FROM "+table+" WHERE Id="+id+"; ";
            if (sqlite_cmd.ExecuteNonQuery() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void Close(SQLiteConnection conn)
        {
            conn.Close();
        }

        private static bool validateMeetingData(Dictionary<string,string> data)
        {
            
            if (data["name"]!="" && data["email"]!="" && data["subject"]!="" && data["location"]!="" && Convert.ToDateTime(data["date"]).Date > DateTime.Now.Date && AppUtilities.IsEmailValid(data["email"]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool validateContactData(Dictionary<string, string> data)
        {

            if (data["name"] != "" && data["email"] != "" && data["contact"] != "" && AppUtilities.IsEmailValid(data["email"]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
