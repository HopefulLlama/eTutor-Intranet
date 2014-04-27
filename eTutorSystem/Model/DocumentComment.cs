using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace eTutorSystem.Model
{
    public class DocumentComment
    {
        protected long _documentID;
        protected string _userID, _documentComments;
        protected DateTime _commentDateTime;

        public long DocumentID
        {
          get { return _documentID; }
          set { _documentID = value; }
        }
        
        public string UserID
        {
          get { return _userID; }
          set { _userID = value; }
        }
        
        public DateTime CommentDateTime
        {
          get { return _commentDateTime; }
          set { _commentDateTime = value; }
        }
        
        public string DocumentComments
        {
          get { return _documentComments; }
          set { _documentComments = value; }
        }

        DocumentComment(DateTime commentDateTime, long documentID, string userID, string documentComments)
        {
            this._commentDateTime = commentDateTime;
            this._documentID = documentID;
            this._userID = userID;
            this._documentComments = documentComments; 
        }

        public DocumentComment(long documentID, string userID, string documentComments)
        {
            this._documentID = documentID;
            this._userID = userID;
            this._documentComments = documentComments;  
        }

        DocumentComment()
        {

        }
    }
}