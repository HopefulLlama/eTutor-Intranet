using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eTutorSystem.Model;

namespace eTutorSystem.Controller_Model
{
    public class tutor_controller
    {
        #region Init

            // Class Declaration ans Instantiation
            private static tutor_controller _instance;
            private ItutorFormInterface _tutorView;
            private Model model = Model.Instance;
            
            // Private constructor
            private tutor_controller() { }

            // Controller Instance
            public static tutor_controller ControllerInstance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = new tutor_controller();
                    }
                    return _instance;
                }
            }

            // Set View Interfaces
            public ItutorFormInterface TutorView
            {
                get { return this._tutorView; }
                set 
                { 
                    this._tutorView = (ItutorFormInterface)value;
                    blog_controller.ControllerInstance.TutorView = value;
                    message_controller.ControllerInstance.TutorView = value;
                    upload_controller.ControllerInstance.TutorView = value;
                    general_functions.Instance.TutorView = value;
                }
            }

        #endregion

        #region Tutor Methods

            // Load Initial Tutor tutees list view
            public void loadTutorView(string firstName, string surname, int programmeID, int orderType)
            {
                general_functions.Instance.setWelcomeMessage("tutor");
                displayTutees(0, firstName, surname, programmeID, orderType);
            }

            // Load Initial Tutor tutees list view For Admin
            public void loadTutorViewForAdmin()
            {
                TutorView.welcomeEnabled = false;
                TutorView.logoutVisible = false;
                TutorView.nullAnchors();
                displayTutees(1, "", "", 0, 0);
            }

            // Display Tutees
            public void displayTutees(int _type, string firstName, string surname, int programmeID, int orderType)
            {
                UserDetails _user = null;
                if (_type == 0) // 0=loggedin user; 1=student chosen
                {
                    _user = (UserDetails)HttpContext.Current.Session["User"];
                }
                else
                {
                    _user = model.selectUserDetailsById(TutorView.studentToView);
                }
                List<UserDetails> _tutees = new List<UserDetails>();
                if (firstName != "" || surname != "" || programmeID > 0 || orderType > 0)
                {
                    _tutees = model.selectTuteesBySearchCriteria(_user.UserID, firstName, surname, programmeID, orderType);
                }
                else
                {
                    _tutees = model.selectTutees(_user.UserID);
                }
                int _numTutees = _tutees.Count;

                if (_numTutees > 0)
                {
                    int _counter = 2;
                    Hashtable _data = new Hashtable();
                    string[] _titles = { "Student", "Messages", "Meetings", "Uploads", "Blogs" };
                    string[] _subs = { " - ", "Firstname", "Surname", "Received", "Sent", "Last", "Requests", "Real", "Virtual", "Cancelled", "Last", "Number", "Comments", "Entries", "Last" };
                    _data.Add(0, _titles);
                    _data.Add(1, _subs);

                    foreach (UserDetails _tutee in _tutees)
                    {
                        int _numMessagesFromStudent = model.countMessages(_tutee.UserID, _user.UserID);
                        int _numMessagesFromTutor = model.countMessages(_user.UserID, _tutee.UserID);
                        DateTime _lastMessageDate = model.getDateOfLastMessageSent(_user.UserID, _tutee.UserID);
                        string _lastMessageStr = (_lastMessageDate.ToShortDateString().Equals("01/01/0001")) ? "n/a" : _lastMessageDate.ToShortDateString();
                        int _numFilesUploadedByStudent = model.countFilesUploadedByStudent(_tutee.UserID);
                        List<UserDocument> _documentList = model.selectUploadFiles(_tutee.UserID);
                        int _numberComments = 0;
                        int _blogPosts = model.countBlogPosts(_tutee.UserID);
                        DateTime _lastBlogDate = model.getDateOfLastBlogPost(_tutee.UserID);
                        string _lastBlogStr = (_lastBlogDate.ToShortDateString().Equals("01/01/0001")) ? "n/a" : _lastBlogDate.ToShortDateString();

                        foreach (UserDocument _doc in _documentList)
                        {
                            if (model.commentExists(_doc.DocumentID.ToString()) == true)
                            {
                                _numberComments++;
                            }
                        }
                        List<MeetingDetails> meetings = MeetingDetails.getAllMeetingsByStudentID(_tutee.UserID);
                        int _numMeetings = meetings.Count;
                        int _numAccepted = 0;
                        int _numDeclined = 0;
                        int _numCancelled = 0;
                        DateTime last = DateTime.Parse("01/01/0001");
                        string date = "";
                        foreach (MeetingDetails m in meetings)
                        {
                            if (m.StudentStatus == "Accepted")
                            {
                                _numAccepted++;
                            }
                            else if (m.StudentStatus == "Declined")
                            {
                                _numDeclined++;
                            }
                            else if (m.StudentStatus == "Cancelled")
                            {
                                _numCancelled++;
                            }
                            if (m.Date > last)
                            {
                                last = m.Date;
                            }
                        }
                        if (last == DateTime.Parse("01/01/0001"))
                        {
                            date = "n/a";
                        }
                        else
                        {
                            date = last.ToString();
                        }


                        string[] _tempArray = { _tutee.UserID, _tutee.FirstName, _tutee.Surname, _numMessagesFromStudent.ToString(), _numMessagesFromTutor.ToString(), _lastMessageStr, _numMeetings.ToString(), _numAccepted.ToString(), _numDeclined.ToString(), _numCancelled.ToString(), date, _numFilesUploadedByStudent.ToString(), _numberComments.ToString(), _blogPosts.ToString(), _lastBlogStr };
                        _data.Add(_counter, _tempArray);
                        _counter++;
                    }
                    TutorView.numTutees = "Number of Tutees: " + _numTutees;
                    TutorView.setTuteesTable(_data);
                }
                else
                {
                    TutorView.noTutees = "No tutees found. This may be because your search criteria returned no results, or you have no students allocated to you.";
                }
            }
                  
            // Logout
            public void logout(string _page)
            {
                general_functions.Instance.logout(_page);
            }

        #endregion
    }
}