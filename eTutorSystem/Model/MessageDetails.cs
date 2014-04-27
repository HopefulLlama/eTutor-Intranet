using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace eTutorSystem.Model
{
    public class MessageDetails
    {
        protected long _messageID;
        protected string _senderID, _recipientID, _subject, _messageContent;
        protected DateTime _date, _time;

        public long MessageID
        {
            get { return _messageID; }
            set { _messageID = value; }
        }

        public string SenderID
        {
            get { return _senderID; }
            set { _senderID = value; }
        }

        public string RecipientID
        {
            get { return _recipientID; }
            set { _recipientID = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }

        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        public string MessageContent
        {
            get { return _messageContent; }
            set { _messageContent = value; }
        }

        public MessageDetails(long messageID, string senderID, string recipientID, DateTime date, DateTime time, string subject, string messageContent)
        {
            this._messageID = messageID;
            this._senderID = senderID;
            this._recipientID = recipientID;
            this._date = date;
            this._time = time;
            this._subject = subject;
            this._messageContent = messageContent;
        }

        MessageDetails(string senderID, string recipientID, DateTime date, DateTime time, string subject, string messageContent)
        {
            this._senderID = senderID;
            this._recipientID = recipientID;
            this._date = date;
            this._time = time;
            this._subject = subject;
            this._messageContent = messageContent;
        }

        MessageDetails()
        {

        }

        public static int getMessageCountOverPastWeek(string userID)
        {
            int messageCount = 0;
            string selectCommand = "SELECT COUNT(*) AS numberOfMessages FROM MessageDetails WHERE MessageDetails.SenderID = '" + userID + "' AND MessageDetails.Date > DATEADD(day, -7, GETDATE())";
            SqlConnection conn = new SqlConnection(DBConnection.ConnectionString);
            using (conn)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    try
                    {
                        cmd.CommandText = selectCommand;
                        cmd.Prepare();
                        SqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            messageCount = rdr.GetInt32(rdr.GetOrdinal("numberOfMessages"));
                        }
                        return messageCount;

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: " + e);
                        return 0;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }

            }
        }

        public static decimal getWeeklyAvgMessageCountOverPastFourWeeks(string userID)
        {
            decimal messageCount = 0;
            string selectCommand = "SELECT CONVERT(DECIMAL(10,2), CONVERT(DECIMAL(10,2), COUNT(*))/4) AS avgWeeklyMessages FROM MessageDetails WHERE MessageDetails.SenderID = '" + userID + "' AND MessageDetails.Date > DATEADD(day, -28, GETDATE())";
            SqlConnection conn = new SqlConnection(DBConnection.ConnectionString);
            using (conn)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    try
                    {
                        cmd.CommandText = selectCommand;
                        cmd.Prepare();
                        SqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            messageCount = rdr.GetDecimal(rdr.GetOrdinal("avgWeeklyMessages"));
                        }
                        return messageCount;

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: " + e);
                        return 0;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public static string getLatestSentMessageDateOfUser(string userID)
        {
            string dateTime = "";
            string selectCommand = "SELECT TOP 1 Date, Time " +
                "FROM MessageDetails " +
                "WHERE SenderID = '" + userID + "' " +
                "ORDER BY Date DESC, Time DESC; ";
            SqlConnection conn = new SqlConnection(DBConnection.ConnectionString);
            using (conn)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    try
                    {
                        cmd.CommandText = selectCommand;
                        cmd.Prepare();
                        SqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            DateTime date = rdr.GetDateTime(rdr.GetOrdinal("Date"));
                            TimeSpan time = rdr.GetTimeSpan(rdr.GetOrdinal("Time"));

                            dateTime = date.ToString("dd/MM/yyyy") + " ";
                            dateTime += time.ToString("HH:mm");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: " + e);
                    }
                    finally
                    {
                        conn.Close();
                    }
                    return dateTime;
                }

            }
        }

    }
}