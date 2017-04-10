using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using MySql.Data.MySqlClient;
namespace SATSystem
{
    public class DBConnect
    {
        private MySqlConnection connect;
        private string Server;
        private string Database;
        private string UserId;
        private string Password;

        public DBConnect()
        {
            string LocalHost = "127.0.0.1";
            string User = "root";
            string Password = "Sb636027";
            Initialize(LocalHost, User, Password);
        }
        public DBConnect(string strServer, string strUserId, string strPassword)
        {
            Initialize(strServer, strUserId, strPassword);
        }

        private void Initialize(string strServer, string strUserId, string strPassword)
        {
            //Server = "tcp://127.0.0.1:3306";
            Server = strServer;
            Database = "SAT";
            UserId = strUserId;
            Password = strPassword;
            string connectionString = "SERVER=" + Server + "; Port=3306; DATABASE=" + Database + ";" + "UID=" + UserId + ";" + "PASSWORD=" + Password + ";";
            connect = new MySqlConnection(connectionString);
        }

        public bool OpenConnection()
        {
            try
            {
                connect.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0: MessageBox.Show("Cannot connect to server. Contact Administrator."); break;
                    case 1045: MessageBox.Show("Invalid username/password, Please try again."); break;
                }
                return false;
            }
        }

        public bool CloseConnection()
        {
            try
            {
                connect.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool ValidateAdmin(string strUserId, string strPassword)
        {
            string query = "SELECT AdminUserID, AdminPassCode FROM SATAdmin WHERE AdminUserID='" + strUserId + "';";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connect);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read() && strPassword.Equals(dataReader["AdminPassCode"]))
                {
                    dataReader.Close();
                    this.CloseConnection();
                    return true;
                }
                else
                {
                    dataReader.Close();
                    this.CloseConnection();
                    return false;
                }
            }            
            this.CloseConnection();
            return false;
        }

        public bool UpdateRating(int Rating)
        {
            int Status = 0;
            string InsertQuery = "UPDATE SATRating SET Rating=" + Rating + ";";

            if (this.OpenConnection() == true)
            {
                MySqlCommand CmdInsert = new MySqlCommand(InsertQuery, connect);
                Status = CmdInsert.ExecuteNonQuery();
                this.CloseConnection();
                if (Status < 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public string GetRating()
        {
            string Rating = "";
            string query = "select Rating from SATRating;";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connect);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read() && (dataReader["Rating"]).Equals(""))
                {
                    dataReader.Close();
                    this.CloseConnection();
                    return "Error";
                }
                Rating = (dataReader["Rating"]).ToString();                
                this.CloseConnection();                
            }
            return Rating;
        }
        public bool InsertStudent(string RollNumber,
                                    string Name,
                                    string Age,
                                    string Gender,
                                    string Class,
                                    string Dept,
                                    string strAcademicyear,
                                    string FatherName,
                                    string FatherOcc,
                                    string FatherContact,
                                    string MotherName,
                                    string MotherOcc,
                                    string MotherContact,
                                    string Address1,
                                    string Address2,
                                    string City,
                                    string RFID)
        {
            int Status = 0;
            string InsertQuery = "INSERT INTO SATPupil (PupilRollNumber, PupilName, PupilAge, PupilGender, PupilClass, PupilDepartment, PupilAcademicYear," + 
            "PupilFatherName, PupilFatherContactNumber, PupilFatherOccupation, PupilMotherName, PupilMotherContactNumber, PupilMotherOccupation, "+
            "PupilAddress1, PupilAddress2, PupilCity, PupilRFID) VALUES ('" + RollNumber + "', '" + Name + "', '" + Age + "', '" + Gender + "', '" +
            Class + "', '" + Dept + "', '" + strAcademicyear + "', '" + FatherName + "', '" + FatherOcc + "', '" + FatherContact + "', '" + MotherName + "', '" + 
            MotherOcc + "', '" + MotherContact + "', '" + Address1 + "', '" + Address2 + "', '" + City + "', '" + RFID + "');";

            if (this.OpenConnection() == true)
            {
                MySqlCommand CmdInsert = new MySqlCommand(InsertQuery, connect);
                Status = CmdInsert.ExecuteNonQuery();
                this.CloseConnection();
                if (Status < 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public string InsertAttendance(string Date, string RFID)
        {
            string PupilID = "";
            string PupilName = "";
            string query = "SELECT PupilID, PupilName FROM SATPupil WHERE PupilRFID='" + RFID + "';";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connect);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read() && (dataReader["PupilID"]).Equals(""))
                {
                    dataReader.Close();
                    this.CloseConnection();
                    return "Error";
                }
                PupilID = (dataReader["PupilID"]).ToString();
                PupilName = (dataReader["PupilName"]).ToString();
                this.CloseConnection();
            }
            try
            {
                if (this.OpenConnection() == true)
                {
                    int Status = 0;
                    string InsertQuery = "INSERT INTO SATAttendance(AttendanceDate, PupilID) VALUES ('" + Date + "', '" + PupilID + "');";
                    MySqlCommand CmdInsert = new MySqlCommand(InsertQuery, connect);
                    Status = CmdInsert.ExecuteNonQuery();
                    this.CloseConnection();
                    if (Status < 0)
                    {
                        return "Error";
                    }
                    else
                    {
                        return PupilName;
                    }

                }
            }catch(Exception e)
            {
                return "ErrorDuplicate";
            }
            return "Error";
        }


        public List<string>[] SelectStudentByDate(string strDate)
        {
            string query = "SELECT A.PupilRollNumber, A.PupilName, A.PupilClass, A.PupilDepartment, CASE WHEN B.PupilID IS NULL THEN 'Absent' ELSE 'Present' END as attendance_status " +
                "FROM SATPupil A LEFT JOIN SATAttendance B on A.PupilID=B.PupilID WHERE " +
                "B.AttendanceDate = '" + strDate + "' ORDER BY B.PupilID ASC;";
            List<string>[] list = new List<string>[5];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
            list[4] = new List<string>();

            if (this.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, connect);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        list[0].Add(dataReader["PupilRollNumber"] + "");
                        list[1].Add(dataReader["PupilName"] + "");
                        list[2].Add(dataReader["PupilClass"] + "");
                        list[3].Add(dataReader["PupilDepartment"] + "");
                        list[4].Add(dataReader["attendance_status"] + "");
                    }

                    dataReader.Close();
                    this.CloseConnection();
                }
                catch (Exception e)
                {

                }
                return list;
            }
            else
            {
                return list;
            }
        }

        public string[] SelectStudentByRFID(string strRFID)
        {
            string query = "SELECT PupilID, PupilRollNumber, PupilName, PupilAge, PupilGender, PupilClass, PupilDepartment, PupilAcademicYear, " +
                "PupilFatherName, PupilFatherContactNumber, PupilFatherOccupation, PupilMotherName, PupilMotherContactNumber, PupilMotherOccupation, " +
                "PupilAddress1, PupilAddress2, PupilCity FROM SATPupil WHERE PupilRFID = '" + strRFID + "' ;";
            string[] list = new string[17];
            for (int i = 0; i < 17; i++)
            {
                list[i] = "";
            }
            if (this.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, connect);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        list[0] = dataReader["PupilRollNumber"].ToString();
                        list[1] = dataReader["PupilName"].ToString();
                        list[2] = dataReader["PupilAge"].ToString();
                        list[3] = dataReader["PupilGender"].ToString();
                        list[4] = dataReader["PupilGender"].ToString();
                        list[5] = dataReader["PupilDepartment"].ToString();
                        list[6] = dataReader["PupilAcademicYear"].ToString();
                        list[7] = dataReader["PupilFatherName"].ToString();
                        list[8] = dataReader["PupilFatherContactNumber"].ToString();
                        list[9] = dataReader["PupilFatherOccupation"].ToString();
                        list[10] = dataReader["PupilMotherName"].ToString();
                        list[11] = dataReader["PupilMotherContactNumber"].ToString();
                        list[12] = dataReader["PupilMotherOccupation"].ToString();
                        list[13] = dataReader["PupilAddress1"].ToString();
                        list[14] = dataReader["PupilAddress2"].ToString();
                        list[15] = dataReader["PupilCity"].ToString();
                        list[16] = dataReader["PupilID"].ToString();
                    }

                    dataReader.Close();
                    this.CloseConnection();
                }
                catch (Exception e)
                {

                }
                return list;
            }
            else
            {
                return list;
            }
        }

        public List<string>[] SelectAllStudents()
        {
            string query = "SELECT PupilRollNumber, PupilName, PupilClass, PupilDepartment FROM SATPupil ORDER BY PupilRollNumber ASC;";
            List<string>[] list = new List<string>[5];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
            list[4] = new List<string>();

            if (this.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, connect);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        list[0].Add(dataReader["PupilRollNumber"] + "");
                        list[1].Add(dataReader["PupilName"] + "");
                        list[2].Add(dataReader["PupilClass"] + "");
                        list[3].Add(dataReader["PupilDepartment"] + "");
                        list[4].Add("Absent");
                    }

                    dataReader.Close();
                    this.CloseConnection();
                }
                catch (Exception e)
                {

                }
                return list;
            }
            else
            {
                return list;
            }
        }

        public bool DeleteStudentByRFID(string strRFID, string strPupilID)
        {
            string query1 = "DELETE FROM SATAttendance WHERE PupilID=" + strPupilID + ";";
            string query2 = "DELETE FROM SATPupil WHERE PupilRFID ='" + strRFID + "';";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd1 = new MySqlCommand(query1, connect);
                cmd1.ExecuteNonQuery();
                this.CloseConnection();
            }
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd2 = new MySqlCommand(query2, connect);
                cmd2.ExecuteNonQuery();
                this.CloseConnection();
                return true;
            }
            return false;
        }
    }
}
