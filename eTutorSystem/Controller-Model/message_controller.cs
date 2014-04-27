using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eTutorSystem.Model;

namespace eTutorSystem.Controller_Model
{
    public class message_controller
    {
        #region Init

            // Class Declaration ans Instantiation
            private static message_controller _instance;
            private IMessageInterface _messageView;
            private ItutorFormInterface _tutorView;
            private Model model = Model.Instance;
            
            // Private constructor
            private message_controller() { }

            // Controller Instance
            public static message_controller ControllerInstance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = new message_controller();
                    }
                    return _instance;
                }
            }

            // Set View Interfaces
            public IMessageInterface MessageView
            {
                get { return this._messageView; }
                set 
                { 
                    this._messageView = (IMessageInterface)value;
                    general_functions.Instance.MessageView = value;
                }
            }

            // Set View Interfaces
            public ItutorFormInterface TutorView
            {
                get { return this._tutorView; }
                set { this._tutorView = (ItutorFormInterface)value; }
            }

        #endregion

        #region Message Methods

            // Select Messages between student and tutor
            public void displayMessages()
            {
                UserDetails _user = (UserDetails)HttpContext.Current.Session["User"];
                List<MessageDetails> _messageList = new List<MessageDetails>();

                if (_user.UserType == 1)
                {
                    _messageList = model.selectMessages(_user.UserID, _user.SupervisorID);
                }
                else if (_user.UserType == 2)
                {
                    string _studentID = MessageView.studentID;
                    _messageList = model.selectMessages(_studentID, _user.UserID);
                }

                Hashtable _formattedMessageData = new Hashtable();
                int _counter = 1;

                if (_messageList.Count > 0)
                {
                    string _sender = string.Empty;
                    string[] _headers = { "Date", "Time", "From", "Message" };
                    _formattedMessageData.Add(0, _headers);
                    foreach (MessageDetails _message in _messageList)
                    {
                        string[] _tempArray = { _message.Date.ToShortDateString(), _message.Time.ToShortTimeString(), _message.SenderID, _message.MessageContent };
                        _sender = _message.SenderID;
                        _formattedMessageData.Add(_counter, _tempArray);
                        _counter++;
                    }
                    MessageView.setMessageTable(_formattedMessageData, _sender);
                }
                else
                {
                    MessageView.noMessages = "No Message History Found.";
                }
            }

            // Submit Message
            public void submitMessage(string _subject, string _message, List<string> _tutees)
            {
                if (_subject != string.Empty && _subject != null && _message != string.Empty && _message != null)
                {
                    UserDetails _user = (UserDetails)HttpContext.Current.Session["User"];
                    string _date = DateTime.Now.ToString("yyy-MM-dd");
                    string _time = DateTime.Now.ToString("hh:mm:ss");
                    MessageView.error = string.Empty;
                    MessageView.error = "Please be patient whilst the system sends the appropriate notification emails.";

                    if (_user.UserType == 1)
                    {
                        UserDetails _tutor = model.selectUserDetailsById(_user.SupervisorID);
                        model.insertMessage(_subject, _message, _user.UserID, _tutor.UserID, _date, _time);
                        general_functions.Instance.email(_tutor.EmailAddress, "You have received a message from one of your tutees (" + _user.UserID + ").\n Please Log into the eTutor system to view it.\n- eTutor System", "eTutor:- You have received a message!");
                        MessageView.resetMessageView();
                        loadMessageView();
                    }
                    else if (_user.UserType == 2)
                    {
                        foreach (string _tutee in _tutees)
                        {
                            UserDetails _student = model.selectUserDetailsById(_tutee);
                            model.insertMessage(_subject, _message, _user.UserID, _student.UserID, _date, _time);
                            general_functions.Instance.email(_student.EmailAddress, "You have received a message from your tutor (" + _user.UserID + ").\n Please Log into the eTutor system to view it.\n\n- eTutor System", "eTutor:- You have received a message!");
                        }

                        if (_tutees.Count == 1)
                        {
                            MessageView.resetMessageView();
                            loadMessageView();
                        }
                        else
                        {
                            MessageView.resetGroupMessageView();
                            MessageView.error = "Group Message Sent Successfully!";
                        }
                    }
                }
                else
                {
                    MessageView.error = "Please ensure the 'Subject' and 'Message' fields are completed before submitting your message.";
                }

            }

            // Load Messaage Initail View
            public void loadMessageView()
            {
                general_functions.Instance.setWelcomeMessage("message");
                displayMessages();
            }

            // Load Message Initial View If Student ID = null
            public void loadMessageNullView()
            {
                general_functions.Instance.setWelcomeMessage("message");
                general_functions.Instance.loadTuteeDropDown("message");
            }

            // Process Message Request
            public void processMessageRequest(List<string> _tutees)
            {
                if (_tutees.Count != 0)
                {
                    string _builder = string.Empty;
                    string _lastItem = _tutees[(_tutees.Count - 1)];
                    foreach (string _tutee in _tutees)
                    {
                        if (_tutee == _lastItem)
                        {
                            _builder += _tutee;
                        }
                        else
                        {
                            _builder += _tutee + "-";
                        }
                    }
                    HttpContext.Current.Response.Redirect("tutorMessage.aspx?StudentId=" + _builder);
                }
                else
                {
                    TutorView.error = "Please Ensure you have selected at least one tutee from the list...";
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