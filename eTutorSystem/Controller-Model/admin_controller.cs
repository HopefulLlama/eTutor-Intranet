using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eTutorSystem.Model;

namespace eTutorSystem.Controller_Model
{
    public class admin_controller
    {
        #region Init

            // Class Declaration ans Instantiation
            private static admin_controller _instance;
            private IadminFormInterface _adminView;
            private Model model = Model.Instance;
            
            // Private constructor
            private admin_controller() { }

            // Controller Instance
            public static admin_controller ControllerInstance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = new admin_controller();
                    }
                    return _instance;
                }
            }

            // Set View Interfaces
            public IadminFormInterface AdminView
            {
                get { return this._adminView; }
                set 
                { 
                    this._adminView = (IadminFormInterface)value;
                    general_functions.Instance.AdminView = value;
                }
            }

        #endregion

        #region Admin Methods
        
            // Load Initial Admin View
            public void loadAdminView()
            {
                general_functions.Instance.setWelcomeMessage("admin");
                displayStudents();
            }

            // Display Students
            public void displayStudents()
            {
                List<UserDetails> _students = model.selectAllStudents();
                int _numTutees = _students.Count;

                if (_numTutees > 0)
                {
                    int _counter = 2;
                    Hashtable _data = new Hashtable();
                    string[] _titles = { string.Empty, "Student", "Supervisor", "Messages", "Last Interaction" };
                    string[] _subtitles = { " - ", "Firstname", "Surname", string.Empty, "Past Week Count", "Weekly Avg.", string.Empty};
                    _data.Add(0, _titles);
                    _data.Add(1, _subtitles);

                    foreach (UserDetails _student in _students)
                    {
                        int messageCount = MessageDetails.getMessageCountOverPastWeek(_student.UserID);
                        decimal weeklyAverage = MessageDetails.getWeeklyAvgMessageCountOverPastFourWeeks(_student.UserID);
                        string lastInteraction = _student.getLastInteraction();
                        string[] _tempArray = { _student.UserID, _student.FirstName, _student.Surname, _student.SupervisorID, messageCount.ToString(), weeklyAverage.ToString(), lastInteraction };
                        _data.Add(_counter, _tempArray);
                        _counter++;
                    }
                    AdminView.numStudents = "Number of students: " + _numTutees;
                    AdminView.setStudentsTable(_data);
                }
                else
                {
                    AdminView.noStudents = "There are no students?!";
                }
            }

            // Submit Message
            public void processSupervisorChange(string _tutorID, List<string> _studentsList)
            {
                if (_tutorID != string.Empty && _studentsList != null)
                {
                    AdminView.error = string.Empty;
                    AdminView.error = "Please be patient whilst the system sends the appropriate notification emails.";

                    foreach (string _student in _studentsList)
                    {
                        UserDetails _studentM = model.selectUserDetailsById(_student);
                        model.updateStudent(_tutorID, _studentM.UserID);
                        general_functions.Instance.email(_studentM.EmailAddress, "You have been assigned a new personal tutor: (" + _tutorID + ").", "eTutor:- Assigned to new tutor");
                        displayStudents();

                        AdminView.error = "Tutor assigning successful!";
                    }
                }
                else
                {
                    AdminView.error = "Please select a student to assign.";
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