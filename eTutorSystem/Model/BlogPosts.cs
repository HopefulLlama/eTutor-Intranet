using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;

namespace eTutorSystem.Model
{
    public class BlogPosts
    {
        protected long _postID;
        protected string _userID, _postContent;
        protected DateTime _date;
        protected TimeSpan _time;
        
        public long PostID
        {
            get { return _postID; }
            set { _postID = value; }
        }
        
        public string UserID
        {
            get { return _userID; }
            set { _userID = value; }
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
        
        public string PostContent
        {
            get { return _postContent; }
            set { _postContent = value; }
        }

        public BlogPosts(long postID, string userID, DateTime date, TimeSpan time, string postContent) 
        {
            this._postID = postID;
            this._userID = userID;
            this._date = date;
            this._time = time;
            this._postContent = postContent;
        }
       
        public BlogPosts(string userID, DateTime date, TimeSpan time, string postContent)
        {
            this._userID = userID;
            this._date = date;
            this._time = time;
            this._postContent = postContent;
        }

        public BlogPosts()
        {

        }

        public static List<BlogPosts> getAllBlogPostsByUserID(string id)
        {
            try
            {
                List<BlogPosts> allPostsByUser = new List<BlogPosts>();
                string command = "SELECT * FROM BlogPosts WHERE UserID = '" + id + "' ORDER BY Date DESC, Time DESC ";
                SqlCommand sqlCommand = new SqlCommand(command, DBConnection.getInstance().Conn);

                DBConnection.getInstance().Conn.Open();
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int postID = reader.GetInt32(reader.GetOrdinal("PostID"));
                        string userID = reader.GetString(reader.GetOrdinal("UserID"));
                        DateTime date = reader.GetDateTime(reader.GetOrdinal("Date"));
                        TimeSpan time = reader.GetTimeSpan(reader.GetOrdinal("Time"));
                        string postContent = reader.GetString(reader.GetOrdinal("PostContent"));
                        BlogPosts bp = new BlogPosts(postID, userID, date, time, postContent);
                        allPostsByUser.Add(bp);
                    }
                }
                return allPostsByUser;
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
            string date = this._date.Date.ToString();
            string time = this._time.ToString().Substring(0, this._time.ToString().LastIndexOf("."));

            string command = "INSERT INTO BlogPosts(UserID, Date, Time, PostContent) VALUES (@UserID, @Date, @Time, @PostContent);";
            SqlCommand sqlCommand = new SqlCommand(command, DBConnection.getInstance().Conn);

            sqlCommand.Parameters.Add("@UserID", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@Date", SqlDbType.Date);
            sqlCommand.Parameters.Add("@Time", SqlDbType.Time);
            sqlCommand.Parameters.Add("@PostContent", SqlDbType.VarChar);

            sqlCommand.Parameters["@UserID"].Value = this._userID;
            sqlCommand.Parameters["@Date"].Value = date;
            sqlCommand.Parameters["@Time"].Value = time;
            sqlCommand.Parameters["@PostContent"].Value = this._postContent;

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