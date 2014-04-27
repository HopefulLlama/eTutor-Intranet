using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace eTutorSystem.Model
{
    public class UserDocument
    {   
        protected long documentID;
        protected string userID, uploadPath;
        protected DateTime date, time;
        
        public long DocumentID
        {
          get { return documentID; }
          set { documentID = value; }
        }
        
        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        
        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }
        
        public string UploadPath
        {
          get { return uploadPath; }
          set { uploadPath = value; }
        }
        
        public UserDocument(long documentID, string userID, DateTime date, DateTime time, string uploadpath) 
        {
            this.documentID = documentID;
            this.userID = userID;
            this.date = date;
            this.time = time;
            this.uploadPath = uploadpath;
        }
        
        UserDocument(string userID, DateTime date, DateTime time, string uploadpath) 
        {
            this.userID = userID;
            this.date = date;
            this.time = time;
            this.uploadPath = uploadpath;
        }
        
        UserDocument()
        {
        }

    }
}