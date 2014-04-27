using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace eTutorSystem.Model
{
    public class StudentProgramme
    {        
        protected string _studentID, _programmeID, _personalTutorID;

        public string StudentID
        {
            get { return _studentID; }
            set { _studentID = value; }
        }

        public string ProgrammeID
        {
            get { return _programmeID; }
            set { _programmeID = value; }
        }

        public string PersonalTutorID
        {
            get { return _personalTutorID; }
            set { _personalTutorID = value; }
        }

        public StudentProgramme(string studentID, string programmeID, string personalTutorID)
        {
            this._studentID = studentID;
            this._programmeID = programmeID;
            this._personalTutorID = personalTutorID;

        }

        public StudentProgramme()
        {

        }

    }
}