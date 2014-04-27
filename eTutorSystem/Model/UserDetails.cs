using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace eTutorSystem.Model
{
    public class UserDetails
    {
        
        protected string userID, firstName, surname, emailAddress, password, supervisorID;
        protected int userType, programmeID;

        public int ProgrammeID
        {
            get { return programmeID; }
            set { programmeID = value; }
        }

        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public string SupervisorID
        {
            get { return this.supervisorID; }
            set { this.supervisorID = (string)value; }
        }

        public string EmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
         
        public int UserType
        {
            get { return userType; }
            set { userType = value; }
        }

        public string Fullname 
        {
            get { return this.firstName + " " + this.surname; }
        }

        public UserDetails(string userID, string firstName, string surname, string emailAddress, string password, int userType, string supervisorID, int programmeID)
        {
            this.userID = userID;
            this.firstName = firstName;
            this.surname = surname;
            this.emailAddress = emailAddress;
            this.password = password;
            this.userType = userType;
            this.SupervisorID = supervisorID;
            this.programmeID = programmeID;
        }

        public UserDetails()
        {

        }

        public string getLastInteraction()
        {
            return MessageDetails.getLatestSentMessageDateOfUser(userID);
        }

        public static UserDetails getUserById(string id){
            UserDetails user = null;
            try
            {
                string command = "SELECT * FROM UserDetails WHERE UserID = '" + id + "'";
                SqlCommand sqlCommand = new SqlCommand(command, DBConnection.getInstance().Conn);

                DBConnection.getInstance().Conn.Open();
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string userID = reader.GetString(reader.GetOrdinal("UserID"));
                        string firstName= reader.GetString(reader.GetOrdinal("FirstName"));
                        string surname = reader.GetString(reader.GetOrdinal("Surname"));
                        string emailAddress = reader.GetString(reader.GetOrdinal("EmailAddress"));
                        string password = reader.GetString(reader.GetOrdinal("Password"));
                        int userType = reader.GetInt32(reader.GetOrdinal("UserType"));
                        string supervisorID = reader.IsDBNull(reader.GetOrdinal("SupervisorID")) ? " " : reader.GetString(reader.GetOrdinal("SupervisorID")).ToString();
                        int programmeID = reader.IsDBNull(reader.GetOrdinal("ProgrammeID")) ? 0 : reader.GetInt32(reader.GetOrdinal("ProgrammeID"));
                        user = new UserDetails(userID, firstName, surname, emailAddress, password, userType, supervisorID, programmeID);
                    }
                }
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
            return user;
        }

    }
}