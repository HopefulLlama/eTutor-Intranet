using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eTutorSystem.Controller_Model
{
    public class blog_controller
    {
        #region Init

            // Class Declaration ans Instantiation
            private static blog_controller _instance;
            private IBlogInterface _blogView;
            private ItutorFormInterface _tutorView;
            private Model model = Model.Instance;
            
            // Private constructor
            private blog_controller() { }

            // Controller Instance
            public static blog_controller ControllerInstance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = new blog_controller();
                    }
                    return _instance;
                }
            }

            // Set View Interfaces
            public IBlogInterface BlogView
            {
                get { return this._blogView; }
                set 
                { 
                    this._blogView = (IBlogInterface)value;
                    general_functions.Instance.BlogView = value;
                }
            }

            public ItutorFormInterface TutorView
            {
                get { return this._tutorView; }
                set { this._tutorView = (ItutorFormInterface)value; }
            }

        #endregion

        #region Blog Methods

            // Load Bload Initial View
            public void loadBlogView()
            {
                general_functions.Instance.setWelcomeMessage("blog");
            }

            // Process Blog
            public void processBlogRequest(List<string> _tutees)
            {
                if (_tutees.Count == 1)
                {
                    foreach (string tutee in _tutees)
                    {
                        HttpContext.Current.Response.Redirect("blog.aspx?blogOwner=" + tutee);
                    }
                }
                else
                {
                    TutorView.error = "Please Ensure you have selected JUST ONE tutee from the list...";
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