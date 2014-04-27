using eTutorSystem.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace eTutorSystem.Controller_Model
{
    public class Model
    {
        #region Model Init

            // Class Variables Declarations
            private static Model _instance;
            private SqlConnection _connection; // private sql connection variable

            // Class Constructor
            private Model() { }

            // Model Instance
            public static Model Instance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = new Model();
                    }
                    return _instance;
                }
            }

            // Connection String Encapsulation 
            private SqlConnection conn
            {
                get { return this._connection; }
                set { this._connection = value; }
            }

            // Check Server is active
            public bool IsServerConnected()
            {
                using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        return true;
                    }
                    catch (SqlException)
                    {
                        return false;
                    }
                    finally
                    {
                        // not really necessary
                        conn.Close();
                    }
                }
            }

        #endregion

        #region User Database Communication Methods

            // Get the User Type By User ID
            public int getUserType(string _userid)
            {
                int _type = 0;
                string selectUserQuery = "SELECT UserType FROM UserDetails WHERE UserID=@usr";
                conn = new SqlConnection(DBConnection.ConnectionString); // Set connection string

                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = selectUserQuery;
                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@usr", _userid);
                            SqlDataReader rdr = cmd.ExecuteReader();

                            while (rdr.Read())
                            {
                               _type = int.Parse(rdr["UserType"].ToString());
                            }
                            return _type;

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
            
            // Check user exists
            public Boolean checkUserExists(string _username, string _password)
            {
                Boolean _exists = false;
                int _returnInt = 0;
                string selectUserQuery = "SELECT COUNT(UserType) AS ex FROM UserDetails WHERE UserID=@usr AND Password=@pswd";
                conn = new SqlConnection(DBConnection.ConnectionString); // Set connection string
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = selectUserQuery;
                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@usr", _username);
                            cmd.Parameters.AddWithValue("@pswd", _password);
                            SqlDataReader rdr = cmd.ExecuteReader();

                            while (rdr.Read())
                            {
                                _returnInt = int.Parse(rdr["ex"].ToString());
                            }

                            if (_returnInt == 1)
                            {
                                _exists = true;
                            }
                            else 
                            {
                                _exists = false;
                            }
                            return _exists;

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e);
                            return false;
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }

                }
            }

            // Select User Details By Id
            public UserDetails selectUserDetailsById(string _userID)
            {
                UserDetails _user = null;
                string selectUserQuery = "SELECT * FROM UserDetails WHERE UserID=@usr";
                conn = new SqlConnection(DBConnection.ConnectionString); // Set connection string
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = selectUserQuery;
                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@usr", _userID);
                            SqlDataReader rdr = cmd.ExecuteReader();

                            while (rdr.Read())
                            {
                                _user = constructUserDetailsFromReader(rdr);
                            }
                            return _user;

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e);
                            return null;
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }

                }
            }
        
            // Select tutees by search criteria
            // Select User Details By Id
            public List<UserDetails> selectTuteesBySearchCriteria(string _tutorid, string firstName, string surname, int programmeID, int orderType)
            {
                List<UserDetails> users = new List<UserDetails>();
                string selectUserQuery = "SELECT * FROM UserDetails WHERE SupervisorID = @tutor  ORDER BY Surname";
                conn = new SqlConnection(DBConnection.ConnectionString); // Set connection string
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        try
                        {
                            #region Checking for parameters and adjusting WHERE clause
                            if (firstName != "")
                            {
                                selectUserQuery += "AND Firstname LIKE @firstname ";
                            }
                            if (surname != "")
                            {
                                selectUserQuery += "AND Surname LIKE @surname ";
                            }
                            if (programmeID > 0)
                            {
                                selectUserQuery += "AND ProgrammeID = @programmeID ";
                            }

                            if (orderType > 0)
                            {
                                switch (orderType)
                                {
                                    case 1:
                                        selectUserQuery += "ORDER BY Firstname ASC ";
                                        break;
                                    case 2:
                                        selectUserQuery += "ORDER BY Firstname DESC ";
                                        break;
                                    case 3:
                                        selectUserQuery += "ORDER BY Surname ASC ";
                                        break;
                                    case 4:
                                        selectUserQuery += "ORDER BY Surname DESC ";
                                        break;
                                }
                            }
                            #endregion
                            cmd.CommandText = selectUserQuery;
                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@tutor", _tutorid);
                            #region Adding values to parameters
                            if (firstName != "")
                            {
                                cmd.Parameters.AddWithValue("@firstname", "%"+firstName+"%");
                            }
                            if (surname != "")
                            {
                                cmd.Parameters.AddWithValue("@surname", "%" + surname + "%");
                            }
                            if (programmeID > 0)
                            {
                                cmd.Parameters.AddWithValue("@programmeID", programmeID);
                            }
                            #endregion
                            SqlDataReader rdr = cmd.ExecuteReader();

                            while (rdr.Read())
                            {
                                users.Add(constructUserDetailsFromReader(rdr));
                            }
                            return users;

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e);
                            return null;
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }

                }
            }
            
            // Select Students By Supervisor ID
            public List<UserDetails> selectTutees(string _tutorid)
            {
                List<UserDetails> _tuteeList = new List<UserDetails>();
                string selectUserQuery = "SELECT * FROM UserDetails WHERE SupervisorID = @tutor  ORDER BY Surname;";
                conn = new SqlConnection(DBConnection.ConnectionString);
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = selectUserQuery;
                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@tutor", _tutorid);
                            SqlDataReader rdr = cmd.ExecuteReader();

                            while (rdr.Read())
                            {
                                _tuteeList.Add(constructUserDetailsFromReader(rdr));
                            }
                            return _tuteeList;

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e);
                            return null;
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }

                }
            }

            // Select Students
            public List<UserDetails> selectAllStudents()
            {
                List<UserDetails> _studentList = new List<UserDetails>();
                String selectUserQuery = "SELECT * FROM UserDetails WHERE UserType = 1 ORDER BY Surname;";
                conn = new SqlConnection(DBConnection.ConnectionString);
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = selectUserQuery;
                            cmd.Prepare();
                            SqlDataReader rdr = cmd.ExecuteReader();

                            while (rdr.Read())
                            {
                                _studentList.Add(constructUserDetailsFromReader(rdr));
                            }
                            return _studentList;

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e);
                            return null;
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }

                }
            }

            // Select Tutors
            public List<UserDetails> selectTutors()
            {
                List<UserDetails> _tutorList = new List<UserDetails>();
                String selectUserQuery = "SELECT * FROM UserDetails WHERE UserType = 2  ORDER BY Surname;";
                conn = new SqlConnection(DBConnection.ConnectionString);
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = selectUserQuery;
                            cmd.Prepare();
                            SqlDataReader rdr = cmd.ExecuteReader();

                            while (rdr.Read())
                            {
                                _tutorList.Add(constructUserDetailsFromReader(rdr));
                            }
                            return _tutorList;

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e);
                            return null;
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }

                }
            }

            // Insert message
            public void updateStudent(string _supervisorID, string _userID)
            {
                string insertMessageQuery = "UPDATE UserDetails SET SupervisorID = @SupervisorID WHERE (UserID = @UserID);";

                conn = new SqlConnection(DBConnection.ConnectionString);
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = insertMessageQuery;
                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@SupervisorID", _supervisorID);
                            cmd.Parameters.AddWithValue("@UserID", _userID);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e);
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }

            }

            private UserDetails constructUserDetailsFromReader(SqlDataReader rdr)
            {
                UserDetails ud = null;
                string _userid = rdr["UserID"].ToString();
                string _fname = rdr["Firstname"].ToString();
                string _sname = rdr["Surname"].ToString();
                string _email = rdr["EmailAddress"].ToString();
                string _pass = rdr["Password"].ToString();
                int _type = int.Parse(rdr["UserType"].ToString());
                string _supervisor = rdr["SupervisorID"].ToString();
                string _programme = rdr["ProgrammeID"].ToString();
                int _programmeID = 0, result;
                if (int.TryParse(_programme, out result))
                {
                    _programmeID = result;
                }
                ud = new UserDetails(_userid, _fname, _sname, _email, _pass, _type, _supervisor, _programmeID);
                return ud;
            }
        #endregion

        #region User Messaging & Upload Communication Methods
        
            /***** MESSAGE RELATED QUERY METHODS *****/

            // Count Number of Messages
            public int countMessages(string _studentid, string _recipientid)
            {
                int _numMessages = 0;
                string selectUserQuery = "SELECT COUNT(MessageID) AS num FROM MessageDetails WHERE SenderID=@sender AND Recipientid=@recipient";
                conn = new SqlConnection(DBConnection.ConnectionString); // Set connection string
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = selectUserQuery;
                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@sender", _studentid);
                            cmd.Parameters.AddWithValue("@recipient", _recipientid);
                            SqlDataReader rdr = cmd.ExecuteReader();

                            while (rdr.Read())
                            {
                                _numMessages = int.Parse(rdr["num"].ToString());
                            }
                            return _numMessages;

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

            // Get last sent message date
            public DateTime getDateOfLastMessageSent(string _tutorid, string _studentid)
            {
                DateTime _date = new DateTime();
                string selectUserQuery =    "SELECT TOP 1 * FROM MessageDetails " +
                                            "WHERE SenderID=@tutor AND RecipientID=@student OR SenderID=@student AND RecipientID=@tutor " +
                                            "ORDER BY Date DESC, Time DESc;";
                conn = new SqlConnection(DBConnection.ConnectionString); // Set connection string
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = selectUserQuery;
                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@student", _studentid);
                            cmd.Parameters.AddWithValue("@tutor", _tutorid);
                            SqlDataReader rdr = cmd.ExecuteReader();

                            while (rdr.Read())
                            {
                                _date = DateTime.Parse(rdr["Date"].ToString());
                            }
                            return _date;

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e);
                            return DateTime.Parse("");
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }

                }
            }

            // Select all messages between a student and tutor
            public List<MessageDetails> selectMessages(string _studentid, string _tutorid)
            {
                MessageDetails _message = null;
                List<MessageDetails> _messageList = new List<MessageDetails>();
                string selectMessageQuery =     "SELECT * FROM MessageDetails WHERE SenderID = @student AND RecipientID = @tutor " +
                                                "UNION " +
                                                "SELECT * FROM MessageDetails WHERE SenderID = @tutor AND RecipientID = @student " +
                                                "ORDER BY Date DESC, Time DESC;";
                conn = new SqlConnection(DBConnection.ConnectionString);
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = selectMessageQuery;
                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@student", _studentid);
                            cmd.Parameters.AddWithValue("@tutor", _tutorid);
                            SqlDataReader rdr = cmd.ExecuteReader();

                            while (rdr.Read())
                            {
                                long _messageid = long.Parse(rdr["MessageID"].ToString());
                                string _senderid = rdr["SenderID"].ToString();
                                string _recipientid = rdr["RecipientID"].ToString();
                                DateTime _date = DateTime.Parse(rdr["Date"].ToString());
                                DateTime _time = DateTime.Parse(rdr["Time"].ToString());
                                string _subject = rdr["Subject"].ToString();
                                string _messageContent = rdr["MessageContent"].ToString();
                                _message = new MessageDetails(_messageid, _senderid, _recipientid, _date, _time, _subject, _messageContent);
                                _messageList.Add(_message);
                            }
                            return _messageList;

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e);
                            return null;
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }

                }
            }
        
            // Insert message
            public void insertMessage(string _subject, string _message, string _senderid, string _recipientid, string _date, string _time)
            {
                string insertMessageQuery = "INSERT INTO MessageDetails (SenderID, RecipientID, Date, Time, Subject, MessageContent) VALUES (@sender, @recipient, @date, @time, @subject, @message);";

                conn = new SqlConnection(DBConnection.ConnectionString);
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = insertMessageQuery;
                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@subject", _subject);
                            cmd.Parameters.AddWithValue("@message", _message);
                            cmd.Parameters.AddWithValue("@sender", _senderid);
                            cmd.Parameters.AddWithValue("@recipient", _recipientid);
                            cmd.Parameters.AddWithValue("@date", _date);
                            cmd.Parameters.AddWithValue("@time", _time);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e);
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }

            }

            /***** UPLOAD RELATED QUERY METHODS *****/
        
            // Count Number of files uploaded by a student
            public int countFilesUploadedByStudent(string _studentid)
            {
                int _numFiles = 0;
                string selectUserQuery = "SELECT COUNT(DocumentID) AS doc FROM UserDocument WHERE UserID=@student";
                conn = new SqlConnection(DBConnection.ConnectionString); // Set connection string
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = selectUserQuery;
                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@student", _studentid);
                            SqlDataReader rdr = cmd.ExecuteReader();

                            while (rdr.Read())
                            {
                                _numFiles = int.Parse(rdr["doc"].ToString());
                            }
                            return _numFiles;

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
    
            // Insert Upload File Data
            public void insertUploadFileDetails(string _userid, string _date, string _time, string _path)
            {
                string insertMessageQuery = "INSERT INTO UserDocument (UserID, Date, Time, UploadPath) VALUES (@user, @date, @time, @doc);";

                conn = new SqlConnection(DBConnection.ConnectionString);
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = insertMessageQuery;
                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@user", _userid);
                            cmd.Parameters.AddWithValue("@date", _date);
                            cmd.Parameters.AddWithValue("@time", _time);
                            cmd.Parameters.AddWithValue("@doc", _path);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e);
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }

            // Select Inserted File Data
            public List<UserDocument> selectUploadFiles(string _studentid)
            {
                UserDocument _document = null;
                List<UserDocument> _documentList = new List<UserDocument>();
                string selectDocumentQuery = "SELECT * FROM UserDocument WHERE UserID=@student ORDER BY Date DESC, Time DESC;";
                conn = new SqlConnection(DBConnection.ConnectionString);
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = selectDocumentQuery;
                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@student", _studentid);
                            SqlDataReader rdr = cmd.ExecuteReader();

                            while (rdr.Read())
                            {
                                long _documentid = long.Parse(rdr["DocumentID"].ToString());
                                string _userid = rdr["UserID"].ToString();
                                DateTime _date = DateTime.Parse(rdr["Date"].ToString());
                                DateTime _time = DateTime.Parse(rdr["Time"].ToString());
                                string _uploadPath = rdr["UploadPath"].ToString();
                                _document = new UserDocument(_documentid, _userid, _date, _time, _uploadPath);
                                _documentList.Add(_document);
                            }
                            return _documentList;

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e);
                            return null;
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }

                }
            }

            // Select INdividual Inserted File Data
            public UserDocument selectUploadFileByDocumentId(string _documentid)
            {
                UserDocument _document = null;
                string selectDocumentQuery = "SELECT * FROM UserDocument WHERE DocumentID=@doc";
                conn = new SqlConnection(DBConnection.ConnectionString);
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = selectDocumentQuery;
                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@doc", _documentid);
                            SqlDataReader rdr = cmd.ExecuteReader();

                            while (rdr.Read())
                            {
                                long _docid = long.Parse(rdr["DocumentID"].ToString());
                                string _userid = rdr["UserID"].ToString();
                                DateTime _date = DateTime.Parse(rdr["Date"].ToString());
                                DateTime _time = DateTime.Parse(rdr["Time"].ToString());
                                string _uploadPath = rdr["UploadPath"].ToString();
                                _document = new UserDocument(_docid, _userid, _date, _time, _uploadPath);
                            }
                            return _document;

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e);
                            return null;
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }

                }
            }

            // Select Uploaded File Comment
            public DocumentComment selectComment(string _documentID)
            {
                DocumentComment _comment = null;
                string selectCommentQuery = "SELECT * FROM DocumentComment WHERE DocumentID=@doc;";
                conn = new SqlConnection(DBConnection.ConnectionString); // Set connection string
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = selectCommentQuery;
                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@doc", _documentID);
                            SqlDataReader rdr = cmd.ExecuteReader();

                            while (rdr.Read())
                            {
                                _comment = new DocumentComment(long.Parse(rdr["DocumentID"].ToString()), rdr["UserID"].ToString(), rdr["DocumentComments"].ToString());
                            }
                            return _comment;

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e);
                            return null;
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }

                }
            }

            // Insert Comment
            public void insertComment(string _documentid, string _tutorid, string _comment)
            {
                string insertCommentQuery = "INSERT INTO DocumentComment (DocumentID, UserID, DocumentComments) VALUES (@doc, @tutor, @comment);";

                conn = new SqlConnection(DBConnection.ConnectionString);
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = insertCommentQuery;
                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@doc", _documentid);
                            cmd.Parameters.AddWithValue("@tutor", _tutorid);
                            cmd.Parameters.AddWithValue("@comment", _comment);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e);
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }

            // Check if Comment Exists
            public Boolean commentExists(string _documentid)
            {
                Boolean _exists = false;
                string selectCommentQuery = "SELECT * FROM DocumentComment WHERE DocumentID=@doc;";
                conn = new SqlConnection(DBConnection.ConnectionString); // Set connection string
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = selectCommentQuery;
                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@doc", _documentid);
                            SqlDataReader rdr = cmd.ExecuteReader();

                            DocumentComment _temp = null;
                            while (rdr.Read())
                            {
                                _temp = new DocumentComment(long.Parse(rdr["DocumentID"].ToString()), rdr["UserID"].ToString(), rdr["DocumentComments"].ToString());
                            }

                            if (_temp != null)
                            {
                                _exists = true;
                            }

                            return _exists;

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e);
                            return _exists;
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }

                }
            }

        #endregion

        #region Blog Posts Communication Methods
        
            // Count Blog Posts
            public int countBlogPosts(string _userID)
            {
                int _numMessages = 0;
                string selectUserQuery = "SELECT COUNT(PostID) AS num FROM BlogPosts WHERE UserID=@UserID";
                conn = new SqlConnection(DBConnection.ConnectionString); // Set connection string
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = selectUserQuery;
                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@UserID", _userID);
                            SqlDataReader rdr = cmd.ExecuteReader();

                            while (rdr.Read())
                            {
                                _numMessages = int.Parse(rdr["num"].ToString());
                            }
                            return _numMessages;

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

            // Get last sent message date
            public DateTime getDateOfLastBlogPost(string _userID)
            {
                DateTime _date = new DateTime();
                string selectUserQuery = "SELECT TOP 1 * FROM BlogPosts " +
                                            "WHERE UserID = @UserID " +
                                            "ORDER BY Date DESC, Time DESC;";
                conn = new SqlConnection(DBConnection.ConnectionString); // Set connection string
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = selectUserQuery;
                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@UserID", _userID);
                            SqlDataReader rdr = cmd.ExecuteReader();

                            while (rdr.Read())
                            {
                                _date = DateTime.Parse(rdr["Date"].ToString());
                            }
                            return _date;

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e);
                            return DateTime.Parse("");
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }

                }
            }
        
        #endregion 
    }
}