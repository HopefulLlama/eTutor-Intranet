using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eTutorSystem.Controller_Model
{
    public class adminDashes_controller
    {
        #region Init

            // Class Declaration ans Instantiation
            private static adminDashes_controller _instance;
            private IdashesInterface _dashesView;
            private Model model = Model.Instance;
            
            // Private constructor
            private adminDashes_controller() { }

            // Controller Instance
            public static adminDashes_controller ControllerInstance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = new adminDashes_controller();
                    }
                    return _instance;
                }
            }

            // Set View Interfaces
            public IdashesInterface DashesView
            {
                get { return this._dashesView; }
                set 
                { 
                    this._dashesView = (IdashesInterface)value;
                    general_functions.Instance.DashesView = value;
                }
            }

        #endregion

        #region Admin Methods
        
            // Load Dashes View
            public void loadDahsesView()
            {
                general_functions.Instance.setWelcomeMessage("dash");
                general_functions.Instance.loadStudentDropDown("dash");
            }
            
            // Load Student/ Tutor Combo Box
            public void loadStudentTutorDropDownBox()
            {
                if (DashesView.option == 0)
                {
                    general_functions.Instance.loadStudentDropDown("dash");
                }
                else
                {
                    general_functions.Instance.loadTutorDropDown("dash");
                }
            }

            // Process Dash View request
            public void submitDash()
            {
                if (DashesView.option == 0)
                {
                    HttpContext.Current.Response.Redirect("../Views/student_area.aspx?UID=" + DashesView.selectedUserID);
                }
                else if (DashesView.option == 1)
                {
                    HttpContext.Current.Response.Redirect("../Views/tutor_area.aspx?UID=" + DashesView.selectedUserID);
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