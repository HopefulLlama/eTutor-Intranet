using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI.WebControls;
using eTutorSystem.Model;

namespace eTutorSystem.Controller_Model
{
    public class general_functions
    {
        #region Init

            // Class Declaration ans Instantiation
            private static general_functions _instance;
            private IstudentFormInterface _studentView;
            private ItutorFormInterface _tutorView;
            private IadminFormInterface _adminView;
            private IUploadInterface _uploadView;
            private IMessageInterface _messageView;
            private IBlogInterface _blogView;
            private IdashesInterface _dashesView;
            private IMeetingInterface _meetingView;
            private Model model = Model.Instance;

            // Private constructor
            private general_functions() { }

            // Controller Instance
            public static general_functions Instance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = new general_functions();
                    }
                    return _instance;
                }
            }

            // Set View Interfaces
            public IstudentFormInterface StudentView
            {
                get { return this._studentView; }
                set { this._studentView = (IstudentFormInterface)value; }
            }

            public ItutorFormInterface TutorView
            {
                get { return this._tutorView; }
                set { this._tutorView = (ItutorFormInterface)value; }
            }

            public IadminFormInterface AdminView
            {
                get { return this._adminView; }
                set { this._adminView = (IadminFormInterface)value; }
            }
            
            public IUploadInterface UploadView
            {
                get { return this._uploadView; }
                set { this._uploadView = (IUploadInterface)value; }
            }

            public IMessageInterface MessageView
            {
                get { return this._messageView; }
                set { this._messageView = (IMessageInterface)value; }
            }

            public IBlogInterface BlogView
            {
                get { return this._blogView; }
                set { this._blogView = (IBlogInterface)value; }
            }

            public IdashesInterface DashesView
            {
                get { return this._dashesView; }
                set { this._dashesView = (IdashesInterface)value; }
            }

            public IMeetingInterface MeetingView
            {
                get { return this._meetingView; }
                set { this._meetingView = (IMeetingInterface)value; }
            }

         #endregion
         
        // Logout 
        public void logout(string _page)
            {
                // Reset relevant page load content
                if (_page.Equals("student"))
                {
                    StudentView.welcome = string.Empty;
                }
                else if (_page.Equals("tutor"))
                {
                    TutorView.welcome = string.Empty;
                }
                else if (_page.Equals("admin"))
                {
                    AdminView.welcome = string.Empty;
                }
                else if (_page.Equals("upload"))
                {
                    UploadView.welcome = string.Empty;
                }
                else if (_page.Equals("studentMeeting"))
                {
                    StudentView.welcome = string.Empty;
                }
                else if (_page.Equals("tutorMeeting"))
                {
                    TutorView.welcome = string.Empty;
                }
                else if (_page.Equals("message"))
                {
                    MessageView.welcome = string.Empty;
                }
                else if (_page.Equals("blog"))
                {
                    BlogView.welcome = string.Empty;
                }
                else if (_page.Equals("dash"))
                {
                    DashesView.welcome = string.Empty;
                }
                else if (_page.Equals("meeting"))
                {
                    MeetingView.welcome = string.Empty;
                }

                // Remove user session variables
                HttpContext.Current.Session.Remove("User");
                // Redirect user to login page
                HttpContext.Current.Response.Redirect("login.aspx", true);
            }

        // Load welcome message 
        public void setWelcomeMessage(string page)
        {
            UserDetails _user = (UserDetails)HttpContext.Current.Session["User"];
            string welcomeStr = "Welcome " + _user.UserID + "!";

            if (page.Equals("student"))
            {
                StudentView.welcome = welcomeStr;
            }
            else if (page.Equals("tutor"))
            {
                TutorView.welcome = welcomeStr;
            }
            else if (page.Equals("admin"))
            {
                AdminView.welcome = welcomeStr;
            }
            else if (page.Equals("upload"))
            {
                UploadView.welcome = welcomeStr;
            }
            else if (page.Equals("message"))
            {
                MessageView.welcome = welcomeStr;
            }
            else if (page.Equals("blog"))
            {
                BlogView.welcome = welcomeStr;
            }
            else if (page.Equals("dash"))
            {
                DashesView.welcome = welcomeStr;
            }
            else if (page.Equals("meeting"))
            {
                MeetingView.welcome = welcomeStr;
            }
        }

        // Email
        public void email(string _userEmail, string _message, string _subject)
        {
            string _disclaimer = "\n\nPLEASE NOTE:\nTHIS IS NOT A REAL EMAIL FROM THE UNIVERSITY OF GREENWICH. THIS IS A TEST EMAIL SENT FROM AN ETUTOR COURSEWORK IMPLEMENTATION.\nIF YOU HAVE RECIEVED THIS BY MISTAKE, PLEASE DISREGARD IT.\nREGARDS - ELITE FALCONS.";
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(_userEmail);
            mail.To.Add(_userEmail);
            mail.Body = _message + _disclaimer;
            mail.Subject = _subject;
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gre.ac.uk";
            client.Send(mail);
        }

        // Meeting Email
        public void email(string _recieverEmail, string _senderEmail, string _message, string _subject)
        {
            string _disclaimer = "\n\nPLEASE NOTE:\nTHIS IS NOT A REAL EMAIL FROM THE UNIVERSITY OF GREENWICH. THIS IS A TEST EMAIL SENT FROM AN ETUTOR COURSEWORK IMPLEMENTATION.\nIF YOU HAVE RECIEVED THIS BY MISTAKE, PLEASE DISREGARD IT.\nREGARDS - ELITE FALCONS.";
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(_senderEmail);
            mail.To.Add(_recieverEmail);
            mail.Body = _message + _disclaimer;
            mail.Subject = _subject;
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gre.ac.uk";
            client.Send(mail);
        }

       

        // Load Tutee Drop Down List
        public void loadTuteeDropDown(string _viewType)
        {
            UserDetails _user = (UserDetails)HttpContext.Current.Session["User"];
            List<UserDetails> _tutees = model.selectTutees(_user.UserID);

            List<ListItem> _tuteeList = new List<ListItem>();
            if (_tutees.Count != 0)
            {
                foreach (UserDetails _tutee in _tutees)
                {
                    _tuteeList.Add(new ListItem(_tutee.Fullname, _tutee.UserID));
                }

                if (_viewType.Equals("message"))
                {
                    MessageView.tuteeDropDown = _tuteeList;
                }
                else if (_viewType.Equals("upload"))
                {
                    UploadView.tuteeDropDown = _tuteeList;
                }
                else if (_viewType.Equals("blog"))
                {
                    BlogView.tuteeDropDown = _tuteeList;
                }
            }
        }

        // Load Student Drop Down List
        public void loadStudentDropDown(string _viewType)
        {
            List<UserDetails> _students = model.selectAllStudents();

            List<ListItem> _studentsList = new List<ListItem>();
            if (_students.Count != 0)
            {
                foreach (UserDetails _tutee in _students)
                {
                    _studentsList.Add(new ListItem(_tutee.Fullname, _tutee.UserID));
                }

                if (_viewType.Equals("dash"))
                {
                    DashesView.optionsDropDownBox = _studentsList;
                }
                
            }
        }

        // Load Tutee Drop Down List
        public void loadTutorDropDown(string _viewType)
        {
            List<UserDetails> _tutors = model.selectTutors();

            List<ListItem> _tutorList = new List<ListItem>();
            if (_tutors.Count != 0)
            {
                foreach (UserDetails _tutor in _tutors)
                {
                    _tutorList.Add(new ListItem(_tutor.Fullname, _tutor.UserID));
                }

                if (_viewType.Equals("admin"))
                {
                    AdminView.tutorDropDown = _tutorList;
                }
                else if (_viewType.Equals("dash"))
                {
                    DashesView.optionsDropDownBox = _tutorList;
                }
                
            }
        }

        // Recursive Name Function
        public List<string> generateFileNameRecursively(string _filename, string _prePath, int num, List<string> _list)
        {
            string _path = _prePath + "/" + _filename;
            Boolean _fileExists = File.Exists(_path);
            int i = num;
            if (_fileExists == true)
            {
                string[] _filenameDreakDown = _filename.Split('.');
                string _a = _filenameDreakDown[0];
                string _b = _filenameDreakDown[1];

                int _previous = 0;
                string _last = _a.Substring(_a.Length - 1, 1);
                Boolean _result = int.TryParse(_last, out _previous);
                if (_result == true)
                {
                    if (_previous == (i - 1))
                    {
                        _a = _a.Substring(0, _a.Length - 1);
                    }
                }

                _filename = _a + i.ToString() + "." + _b;
                i++;
                _list.Add(_filename);
                generateFileNameRecursively(_filename, _prePath, i, _list);
            }
            else
            {
                _list.Add(_filename);
            }
            return _list;
        }

    }
}