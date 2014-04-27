using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eTutorSystem.Controller_Model
{
    public class Meeting_Controller
    {
        // Class Declaration ans Instantiation
        private static Meeting_Controller _instance;
        private IMeetingInterface _meetingView;

        // Controller Instance
        public static Meeting_Controller ControllerInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Meeting_Controller();
                }
                return _instance;
            }
        }

        
        public IMeetingInterface MeetingView
        {
            get { return _meetingView; }
            set { 
                _meetingView = value; 
                general_functions.Instance.MeetingView = value;
            }
        }


        // Logout
        public void logout(string _page)
        {
            general_functions.Instance.logout(_page);
        }
    }
}