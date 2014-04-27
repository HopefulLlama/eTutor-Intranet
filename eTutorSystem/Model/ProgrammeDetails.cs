using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace eTutorSystem.Model
{
    public class ProgrammeDetails
    {
        protected int _programmeID;
        protected string _name;

        public int ProgrammeID
        {
            get { return _programmeID; }
            set { _programmeID = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public ProgrammeDetails(int programmeID, string name)
        {
            this._programmeID = programmeID;
            this._name = name;
        }

        public ProgrammeDetails()
        {

        }


        public static List<ProgrammeDetails> getAllProgrammeDetails()
        {
            try
            {
                List<ProgrammeDetails> allProgrammeDetails = new List<ProgrammeDetails>();
                string command = "SELECT * FROM ProgrammeDetails;";
                SqlCommand sqlCommand = new SqlCommand(command, DBConnection.getInstance().Conn);

                DBConnection.getInstance().Conn.Open();
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int programmeID = reader.GetInt32(reader.GetOrdinal("ProgrammeID"));
                        string programmeName = reader.GetString(reader.GetOrdinal("Name"));
                        
                        ProgrammeDetails pd = new ProgrammeDetails(programmeID, programmeName);
                        allProgrammeDetails.Add(pd);
                    }
                }
                return allProgrammeDetails;
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
    }
}