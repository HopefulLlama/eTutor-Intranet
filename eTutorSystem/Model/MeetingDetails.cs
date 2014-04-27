using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;

namespace eTutorSystem.Model
{
    public class MeetingDetails
    {

        protected long _meetingID;
        protected string _studentID, _tutorID, _type, _location;
        protected DateTime _date;
        protected TimeSpan _time;
        string _studentStatus;
        string _tutorStatus;
        string _studentName;
        string _tutorName;

        public string StudentName
        {
            get { return _studentName; }
            set { _studentName = value; }
        }

        public string TutorName
        {
            get { return _tutorName; }
            set { _tutorName = value; }
        }

        public string StudentStatus
        {
            get { return _studentStatus; }
            set { _studentStatus = value; }
        }


        public string TutorStatus
        {
            get { return _tutorStatus; }
            set { _tutorStatus = value; }
        }

        public long MeetingID
        {
            get { return _meetingID; }
            set { _meetingID = value; }
        }

        public string StudentID
        {
            get { return _studentID; }
            set { _studentID = value; }
        }

        public string TutorID
        {
            get { return _tutorID; }
            set { _tutorID = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public TimeSpan Time
        {
            get { return _time; }
            set { _time = value; }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public MeetingDetails(long meetingID, string studentID, string tutorID, DateTime date, TimeSpan time, string type, string location, string studentStatus, string tutorStatus, string studentName, string tutorName)
        {

            this._meetingID = meetingID;
            this._studentID = studentID;
            this._tutorID = tutorID;
            this._date = date;
            this._time = time;
            this._type = type;
            this._location = location;
            this._studentName = studentName;
            this._tutorName = tutorName;
            this._studentStatus = studentStatus;
            this._tutorStatus = tutorStatus;
        }

        public MeetingDetails(long meetingID, string studentID, string tutorID, DateTime date, TimeSpan time, string type, string location, string studentStatus, string tutorStatus)
        {

            this._meetingID = meetingID;
            this._studentID = studentID;
            this._tutorID = tutorID;
            this._date = date;
            this._time = time;
            this._type = type;
            this._location = location;
            this._studentStatus = studentStatus;
            this._tutorStatus = tutorStatus;
        }


        public MeetingDetails(string studentID, string tutorID, DateTime date, TimeSpan time, string type, string location, string studentStatus, string tutorStatus)
        {
            this._studentID = studentID;
            this._tutorID = tutorID;
            this._date = date;
            this._time = time;
            this._type = type;
            this._location = location;
            this._studentStatus = studentStatus;
            this._tutorStatus = tutorStatus;
        }

        public MeetingDetails()
        {

        }

        public void selectMeetingDetailsByMeetingID()
        {
            try
            {
                string command = "SELECT * FROM MeetingDetails WHERE MeetingID = '" + _meetingID + "'";
                SqlCommand sqlCommand = new SqlCommand(command, DBConnection.getInstance().Conn);

                DBConnection.getInstance().Conn.Open();
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        this._meetingID = reader.GetInt32(reader.GetOrdinal("MeetingID"));
                        this._studentID = reader.GetString(reader.GetOrdinal("StudentID"));
                        this._tutorID = reader.GetString(reader.GetOrdinal("TutorID"));
                        this._date = reader.GetDateTime(reader.GetOrdinal("Date"));
                        this._time = reader.GetTimeSpan(reader.GetOrdinal("Time"));
                        this._type = reader.GetString(reader.GetOrdinal("Type"));
                        this._location = reader.GetString(reader.GetOrdinal("Location"));
                        this._studentStatus = reader.GetString(reader.GetOrdinal("StudentStatus"));
                        this._tutorStatus = reader.GetString(reader.GetOrdinal("TutorStatus"));
                    }
                }
            }

            catch (ArgumentOutOfRangeException aoorex)
            {
            }
            catch (Exception ex)
            {

            }
            finally
            {
                DBConnection.getInstance().Conn.Close();
            }
        }

        //public static List<MeetingDetails> getAllMeetingsByStudentID(string id)
        //{
        //    try
        //    {
        //        List<MeetingDetails> allMeetings = new List<MeetingDetails>();
        //        string command = "SELECT * FROM MeetingDetails WHERE StudentID = '" + id + "'";
        //        SqlCommand sqlCommand = new SqlCommand(command, DBConnection.getInstance().Conn);

        //        DBConnection.getInstance().Conn.Open();
        //        using (SqlDataReader reader = sqlCommand.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                long _meetingID = reader.GetInt32(reader.GetOrdinal("MeetingID"));
        //                string _studentID = reader.GetString(reader.GetOrdinal("StudentID"));
        //                string _tutorID = reader.GetString(reader.GetOrdinal("TutorID"));
        //                DateTime _date = reader.GetDateTime(reader.GetOrdinal("Date"));
        //                TimeSpan _time = reader.GetTimeSpan(reader.GetOrdinal("Time")); ;
        //                string _type = reader.GetString(reader.GetOrdinal("Type"));
        //                string _location = reader.GetString(reader.GetOrdinal("Location"));
        //                string _StudentStatus = reader.GetString(reader.GetOrdinal("StudentStatus"));
        //                string _TutorStatus = reader.GetString(reader.GetOrdinal("TutorStatus"));

        //                MeetingDetails md = new MeetingDetails(_meetingID, _studentID,
        //                    _tutorID, _date, _time, _type, _location, _StudentStatus, _TutorStatus);
        //                allMeetings.Add(md);
        //            }
        //        }
        //        return allMeetings;
        //    }

        //    catch (ArgumentOutOfRangeException aoorex)
        //    {
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //    finally
        //    {
        //        DBConnection.getInstance().Conn.Close();
        //    }
        //}

        public static List<MeetingDetails> getAllMeetingsByTutorID(string id)
        {
            try
            {
                List<MeetingDetails> allMeetings = new List<MeetingDetails>();
                string command =
            "SELECT MeetingDetails.MeetingID, MeetingDetails.StudentID, { fn CONCAT({ fn CONCAT(UserDetails.Firstname, ', ') }, dbo.UserDetails.Surname) } AS 'Student', " +
            "MeetingDetails.TutorID, { fn CONCAT({ fn CONCAT(UserDetails_1.Firstname, ', ') }, UserDetails_1.Surname) } AS Tutor, dbo.MeetingDetails.Date, " +
            "MeetingDetails.Time, MeetingDetails.Type, MeetingDetails.Location, MeetingDetails.StudentStatus, MeetingDetails.TutorStatus " +
            "FROM MeetingDetails INNER JOIN " +
            "UserDetails ON MeetingDetails.StudentID = UserDetails.UserID INNER JOIN " +
            "UserDetails AS UserDetails_1 ON MeetingDetails.TutorID = UserDetails_1.UserID " +
            "WHERE MeetingDetails.TutorID='" + id + "' ORDER BY Date DESC, Time DESC";

                SqlCommand sqlCommand = new SqlCommand(command, DBConnection.getInstance().Conn);

                DBConnection.getInstance().Conn.Open();
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        long _meetingID = reader.GetInt32(reader.GetOrdinal("MeetingID"));
                        string _studentID = reader.GetString(reader.GetOrdinal("StudentID"));
                        string _tutorID = reader.GetString(reader.GetOrdinal("TutorID"));
                        DateTime _date = reader.GetDateTime(reader.GetOrdinal("Date"));
                        TimeSpan _time = reader.GetTimeSpan(reader.GetOrdinal("Time")); ;
                        string _type = reader.GetString(reader.GetOrdinal("Type"));
                        string _location = reader.GetString(reader.GetOrdinal("Location"));
                        string _StudentStatus = reader.GetString(reader.GetOrdinal("StudentStatus"));
                        string _TutorStatus = reader.GetString(reader.GetOrdinal("TutorStatus"));
                        string _studentName = reader.GetString(reader.GetOrdinal("Student"));
                        string _tutorName = reader.GetString(reader.GetOrdinal("Tutor"));

                        MeetingDetails md = new MeetingDetails(_meetingID, _studentID,
                            _tutorID, _date, _time, _type, _location, _StudentStatus, _TutorStatus, _studentName, _tutorName);
                        allMeetings.Add(md);
                    }
                }
                return allMeetings;
            }
            catch (ArgumentOutOfRangeException aoorex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                DBConnection.getInstance().Conn.Close();
            }
        }

        public static List<MeetingDetails> getAllMeetingsByStudentID(string id)
        {
            try
            {
                List<MeetingDetails> allMeetings = new List<MeetingDetails>();
                string command =
            "SELECT MeetingDetails.MeetingID, MeetingDetails.StudentID, { fn CONCAT({ fn CONCAT(UserDetails.Firstname, ', ') }, dbo.UserDetails.Surname) } AS 'Student', " +
            "MeetingDetails.TutorID, { fn CONCAT({ fn CONCAT(UserDetails_1.Firstname, ', ') }, UserDetails_1.Surname) } AS Tutor, dbo.MeetingDetails.Date, " +
            "MeetingDetails.Time, MeetingDetails.Type, MeetingDetails.Location, MeetingDetails.StudentStatus, MeetingDetails.TutorStatus " +
            "FROM MeetingDetails INNER JOIN " +
            "UserDetails ON MeetingDetails.StudentID = UserDetails.UserID INNER JOIN " +
            "UserDetails AS UserDetails_1 ON MeetingDetails.TutorID = UserDetails_1.UserID " +
            "WHERE MeetingDetails.StudentID='" + id + "' ORDER BY Date DESC, Time DESC";

                SqlCommand sqlCommand = new SqlCommand(command, DBConnection.getInstance().Conn);

                DBConnection.getInstance().Conn.Open();
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        long _meetingID = reader.GetInt32(reader.GetOrdinal("MeetingID"));
                        string _studentID = reader.GetString(reader.GetOrdinal("StudentID"));
                        string _tutorID = reader.GetString(reader.GetOrdinal("TutorID"));
                        DateTime _date = reader.GetDateTime(reader.GetOrdinal("Date"));
                        TimeSpan _time = reader.GetTimeSpan(reader.GetOrdinal("Time")); ;
                        string _type = reader.GetString(reader.GetOrdinal("Type"));
                        string _location = reader.GetString(reader.GetOrdinal("Location"));
                        string _StudentStatus = reader.GetString(reader.GetOrdinal("StudentStatus"));
                        string _TutorStatus = reader.GetString(reader.GetOrdinal("TutorStatus"));
                        string _studentName = reader.GetString(reader.GetOrdinal("Student"));
                        string _tutorName = reader.GetString(reader.GetOrdinal("Tutor"));

                        MeetingDetails md = new MeetingDetails(_meetingID, _studentID,
                            _tutorID, _date, _time, _type, _location, _StudentStatus, _TutorStatus, _studentName, _tutorName);
                        allMeetings.Add(md);
                    }
                }
                return allMeetings;
            }
            catch (ArgumentOutOfRangeException aoorex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                DBConnection.getInstance().Conn.Close();
            }
        }

        public Boolean insertToDatabase()
        {
            string command = "INSERT INTO MeetingDetails(StudentID, TutorID, Date, Time, Type, Location, StudentStatus, TutorStatus) VALUES (@StudentID, @TutorID, @Date, @Time, @Type, @Location, @StudentStatus, @TutorStatus);";
            SqlCommand sqlCommand = new SqlCommand(command, DBConnection.getInstance().Conn);

            sqlCommand.Parameters.Add("@StudentID", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@TutorID", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@Date", SqlDbType.Date);
            sqlCommand.Parameters.Add("@Time", SqlDbType.Time);
            sqlCommand.Parameters.Add("@Type", SqlDbType.Char);
            sqlCommand.Parameters.Add("@Location", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@StudentStatus", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@TutorStatus", SqlDbType.VarChar);


            sqlCommand.Parameters["@StudentID"].Value = this._studentID;
            sqlCommand.Parameters["@TutorID"].Value = this._tutorID;
            sqlCommand.Parameters["@Date"].Value = this._date;
            sqlCommand.Parameters["@Time"].Value = this._time;
            sqlCommand.Parameters["@Type"].Value = this._type;
            sqlCommand.Parameters["@Location"].Value = this._location;
            sqlCommand.Parameters["@StudentStatus"].Value = this._studentStatus;
            sqlCommand.Parameters["@TutorStatus"].Value = this._tutorStatus;


            try
            {
                DBConnection.getInstance().Conn.Open();
                int affectedRows = sqlCommand.ExecuteNonQuery();
                if (affectedRows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                DBConnection.getInstance().Conn.Close();
            }
        }

        public Boolean updateToDatabase()
        {
            string command = "update MeetingDetails SET TutorStatus=@tutorStatus where MeetingID=@meetingID;";
            SqlCommand sqlCommand = new SqlCommand(command, DBConnection.getInstance().Conn);

            sqlCommand.Parameters.Add("@tutorStatus", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@meetingID", SqlDbType.Int);

            sqlCommand.Parameters["@tutorStatus"].Value = this._tutorStatus;
            sqlCommand.Parameters["@meetingID"].Value = this._meetingID;

            try
            {
                DBConnection.getInstance().Conn.Open();
                int affectedRows = sqlCommand.ExecuteNonQuery();
                if (affectedRows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                DBConnection.getInstance().Conn.Close();
            }
        }


        public Boolean updateStudentToDatabase()
        {
            string command = "update MeetingDetails SET StudentStatus=@studentStatus where MeetingID=@meetingID;";
            SqlCommand sqlCommand = new SqlCommand(command, DBConnection.getInstance().Conn);

            sqlCommand.Parameters.Add("@studentStatus", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@meetingID", SqlDbType.Int);

            sqlCommand.Parameters["@studentStatus"].Value = this._studentStatus;
            sqlCommand.Parameters["@meetingID"].Value = this._meetingID;

            try
            {
                DBConnection.getInstance().Conn.Open();
                int affectedRows = sqlCommand.ExecuteNonQuery();
                if (affectedRows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                DBConnection.getInstance().Conn.Close();
            }
        }


    }

}