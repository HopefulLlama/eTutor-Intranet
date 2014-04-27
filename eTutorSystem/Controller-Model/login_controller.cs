using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eTutorSystem.Model;

namespace eTutorSystem.Controller_Model
{
    public class login_controller
    {
        #region Init

            // Class Declaration ans Instantiation
            private static login_controller _instance;
            private IloginFormInterface _loginView;
            private Model model = Model.Instance;

            // Private constructor
            private login_controller() { }

            // Controller Instance
            public static login_controller ControllerInstance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = new login_controller();
                    }
                    return _instance;
                }
            }

            // Set View Interfaces
            public IloginFormInterface LoginView
            {
                get { return this._loginView; }
                set { this._loginView = (IloginFormInterface)value; }
            }

        #endregion

        #region Login Methods

            // Login method
            public void login()
            {
                // Get username & password from login form
                string _username = LoginView.username;
                string _password = LoginView.password;

                // Check if username field is empty
                if (_username != string.Empty)
                {
                    // Reset errors
                    LoginView.usernameError = "";
                    LoginView.passwordError = "";
                    // Check if password field is empty
                    if (_password != string.Empty)
                    {
                        // Reset error
                        LoginView.passwordError = "";

                        //Start the login authentication method
                        if (model.IsServerConnected() == true)
                        {
                            if (model.checkUserExists(_username, _password) == true)
                            {
                                UserDetails _user = model.selectUserDetailsById(_username);
                                if (_user != null)
                                {
                                    setUserSessionVariables(_user); // add user type
                                    accountTypeRedirect(_user.UserType);
                                }
                            }
                            else
                            {
                                LoginView.passwordError = "That User does not exist.";
                            }
                        }
                        else
                        {
                            LoginView.passwordError = "We are experiencing Database connection issues. Please Try Again Later.";
                        }
                    }
                    else
                    {
                        LoginView.passwordError = "Enter your password.";
                    }
                }
                else
                {
                    LoginView.usernameError = "Enter your username.";
                }

            }

            // Redirect based on user account type
            private void accountTypeRedirect(int _accountType)
            {
                if (_accountType == 1)
                {
                    HttpContext.Current.Response.Redirect("student_area.aspx");
                }
                else if (_accountType == 2)
                {
                    HttpContext.Current.Response.Redirect("tutor_area.aspx");
                }
                else if (_accountType == 3)
                {
                    HttpContext.Current.Response.Redirect("admin_area.aspx");
                }
            }

            // Set User Session Variables method
            private void setUserSessionVariables(UserDetails User)
        {
            HttpContext.Current.Session["User"] = User;
        }

        #endregion

    }
}