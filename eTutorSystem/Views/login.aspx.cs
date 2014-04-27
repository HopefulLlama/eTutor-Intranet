using eTutorSystem.Controller_Model;
using eTutorSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eTutorSystem.Views
{
    public partial class login : System.Web.UI.Page, IloginFormInterface
    {
        login_controller controller = login_controller.ControllerInstance;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            UserDetails _user = (UserDetails)Session["User"];
            if (_user != null)
            {
                if (_user.UserType == 1)
                {
                    HttpContext.Current.Response.Redirect("student_area.aspx", true);
                }
                else if (_user.UserType == 2)
                {
                    HttpContext.Current.Response.Redirect("tutor_area.aspx", true);
                }
                else if (_user.UserType == 3)
                {
                    HttpContext.Current.Response.Redirect("admin_area.aspx", true);
                }
            }
            else
            {
                controller.LoginView = this;
            }
        }

        protected void login_btn_Click(object sender, EventArgs e)
        {
            controller.login(); // call login method in controller
        }
        
        public string username
        {
            get { return this.username_txtbx.Text; }
            set { this.username_txtbx.Text = value; }
        }

        public string password 
        {
            get { return this.password_txtbx.Text; }
            set { this.password_txtbx.Text = value; }
        }

        public string usernameError 
        {
            set { this.username_error_lbl.Text = value; }
        }

        public string passwordError 
        {
            set { this.password_error_lbl.Text = value; }
        }

        public string status
        {
            set { this.statusLbl.Text = value; }
        }

    }
}