using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eTutorSystem.Model;

namespace eTutorSystem.Controller_Model
{
    public class student_controller
    {
        #region Init

            // Class Declaration ans Instantiation
            private static student_controller _instance;
            private IstudentFormInterface _studentView;
            private Model model = Model.Instance;
            
            // Private constructor
            private student_controller() { }

            // Controller Instance
            public static student_controller ControllerInstance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = new student_controller();
                    }
                    return _instance;
                }
            }

            // Set View Interfaces
            public IstudentFormInterface StudentView
            {
                get { return this._studentView; }
                set 
                { 
                    this._studentView = (IstudentFormInterface)value;
                    general_functions.Instance.StudentView = value;
                }
            }

        #endregion

        #region Student Methods

            // Load Initial Student View
            public void loadStudentView()
            {
                general_functions.Instance.setWelcomeMessage("student");
                studentPersonalTutor(0);
                displayStudentDashInfo(0);
            }

            // Load Initial Student View for admin
            public void loadStudentViewForAdmin()
            {
                StudentView.welcomeEnabled = false;
                StudentView.logoutVisible = false;
                StudentView.nullAnchors();
                studentPersonalTutor(1);
                displayStudentDashInfo(1);
            }

            // Set Student's Tutor
            public void studentPersonalTutor(int _type)
            {
                UserDetails _user = null;
                if (_type == 0) // 0=loggedin user; 1=student chosen
                {
                    _user = (UserDetails)HttpContext.Current.Session["User"];
                }
                else
                {
                    _user = model.selectUserDetailsById(StudentView.studentToView);
                }
                string _tutorName = string.Empty;
                string _tutorEmail = string.Empty;

                if (_user.SupervisorID != "")
                {
                    UserDetails _tutor = model.selectUserDetailsById(_user.SupervisorID.ToString());
                    _tutorName = _tutor.Fullname;
                    _tutorEmail = _tutor.EmailAddress;
                }
                else
                {
                    _tutorName = " -- UNASSIGNED --";
                    _tutorEmail = " -- UNASSIGNED --";
                }
                StudentView.tutor = "You personal tutor is: " + _tutorName;
                StudentView.tutorEmail = "You personal tutor's email: " + _tutorEmail;
            }

            // Display Students dash Info
            public void displayStudentDashInfo(int _type)
            {
                UserDetails _user = null;
                if (_type == 0) // 0=loggedin user; 1=student chosen
                {
                    _user = (UserDetails)HttpContext.Current.Session["User"];
                }
                else
                {
                    _user = model.selectUserDetailsById(StudentView.studentToView);
                }

                Hashtable _data = new Hashtable();
                string[] _titles = { "Messages", "Meetings", "Blogs" };
                string[] _subs = { "Received", "Sent", "Requests", "Real", "Virtual", "Cancelled" };
                _data.Add(0, _titles);
                _data.Add(1, _subs);

                int _numMessagesFromStudent = model.countMessages(_user.UserID, _user.SupervisorID);
                int _numMessagesFromTutor = model.countMessages(_user.SupervisorID, _user.UserID);
                int _numBlogPosts = model.countBlogPosts(_user.UserID);
                List<MeetingDetails> meetings = MeetingDetails.getAllMeetingsByStudentID(_user.UserID);
                int _numMeetings = meetings.Count;
                int _numAccepted = 0;
                int _numDeclined = 0;
                int _numCancelled = 0;
                foreach (MeetingDetails m in meetings)
                {
                    if (m.TutorStatus == "Accepted")
                    {
                        _numAccepted++;
                    } else if (m.TutorStatus == "Declined")
                    {
                        _numDeclined++;
                    } else if (m.TutorStatus == "Cancelled")
                    {
                        _numCancelled++;
                    } 

                }
                string[] _tempArray = { _numMessagesFromTutor.ToString(), _numMessagesFromStudent.ToString(), _numMeetings.ToString(), _numAccepted.ToString(), _numDeclined.ToString(), _numCancelled.ToString(), _numBlogPosts.ToString() };
                _data.Add(2, _tempArray);

                StudentView.setInfoTable(_data);
            }
        
            // Logout
            public void logout(string _page)
            {
                general_functions.Instance.logout(_page);
            }

        #endregion

    }
}