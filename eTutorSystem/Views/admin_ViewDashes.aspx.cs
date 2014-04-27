using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eTutorSystem.Controller_Model;
using eTutorSystem.Model;

namespace eTutorSystem.Views
{
    public partial class admin_ViewDashes : System.Web.UI.Page, IdashesInterface
    {
        adminDashes_controller controller = adminDashes_controller.ControllerInstance;

        protected void Page_Load(object sender, EventArgs e)
        {
            UserDetails _user = (UserDetails)Session["User"];
            if (_user != null)
            {
                if (_user.UserType == 1)
                {
                    HttpContext.Current.Response.Redirect("student_area.aspx");
                }
                else if (_user.UserType == 2)
                {
                    HttpContext.Current.Response.Redirect("tutor_area.aspx");
                }
                else if (_user.UserType == 3)
                {
                    controller.DashesView = this;
                    if (!IsPostBack)
                    {
                        controller.loadDahsesView();
                    }
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("login.aspx");
            }
        }

        public string welcome
        {
            set { this.welcome_lbl.Text = value; }
        }

        public int option
        {
            get { return ((studentRdBtn.Checked == true) ? 0 : 1); }
        }

        public List<ListItem> optionsDropDownBox
        {
            set
            {
                this.usersDdb.DataSource = value;
                this.usersDdb.DataTextField = "Text";
                this.usersDdb.DataValueField = "Value";
                this.usersDdb.DataBind();
            }
        }

        public string selectedUserID
        {
            get { return this.usersDdb.SelectedValue.ToString(); }
        }

        protected void logout_lkbtn_Click(object sender, EventArgs e)
        {
            controller.logout("admin");
        }

        protected void studentRdBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (studentRdBtn.Checked == true)
            {
                controller.loadStudentTutorDropDownBox();
            }
        }

        protected void tutorrdBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (tutorrdBtn.Checked == true)
            {
                controller.loadStudentTutorDropDownBox();
            }
        }

        protected void submitMessageBtn_Click(object sender, EventArgs e)
        {
            controller.submitDash();
        }

    }
}