using eTutorSystem.Controller_Model;
using eTutorSystem.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.Security.Policy;

namespace eTutorSystem.Views
{
    public partial class student_area : System.Web.UI.Page, IstudentFormInterface
    {
        student_controller controller = student_controller.ControllerInstance;

        protected void Page_Load(object sender, EventArgs e)
        {
            UserDetails _user = (UserDetails)Session["User"];
            if (_user != null)
            {
                if (_user.UserType == 1)
                {
                    controller.StudentView = this;
                    controller.loadStudentView();
                    
                }
                else if (_user.UserType == 2)
                {
                    HttpContext.Current.Response.Redirect("tutor_area.aspx");
                }
                else if (_user.UserType == 3)
                {
                    if (Request.QueryString["UID"] != null)
                    {
                        controller.StudentView = this;
                        controller.loadStudentViewForAdmin();
                        HttpContext.Current.Response.Write("<Script language='javaScript'>window.opener.location.href = window.opener.location; </Script>");
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("admin_area.aspx");
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

        public Boolean welcomeEnabled
        {
            set { this.welcome_lbl.Enabled = value; }
        }

        public Boolean logoutVisible
        {
            set { this.logout_lkbtn.Visible = value; }
        }

        public string studentToView
        {
            get { return Request.QueryString["UID"].ToString(); }
        }

        public void nullAnchors()
        {
            HtmlAnchor dash = (HtmlAnchor)(this.Master).FindControl("dash");
            HtmlAnchor message = (HtmlAnchor)(this.Master).FindControl("message");
            HyperLink meet = (HyperLink)(this.Master).FindControl("meet");
            HtmlAnchor upload = (HtmlAnchor)(this.Master).FindControl("upload");
            HyperLink blogLink = (HyperLink)(this.Master).FindControl("blogLink");
            dash.HRef = "../Views/student_area.aspx?UID=" + studentToView;
            message.HRef = "../Views/student_area.aspx?UID=" + studentToView;
            meet.NavigateUrl = "../Views/student_area.aspx?UID=" + studentToView;
            upload.HRef = "../Views/student_area.aspx?UID=" + studentToView;
            blogLink.NavigateUrl = "../Views/student_area.aspx?UID=" + studentToView; 
        }

        public string tutor
        {
            get { return this.tutorLbl.Text; }
            set { this.tutorLbl.Text = value; }
        }

        public string tutorEmail
        {
            get { return this.tutorEmailLbl.Text; }
            set { this.tutorEmailLbl.Text = value; }
        }

        public void setInfoTable(Hashtable _data)
        {
            for (int i = 0; i < _data.Count; i++)
            {
                TableRow _tempRow = new TableRow();
                string[] _messageData = (string[])_data[i];

                for (int j = 0; j < _messageData.Length; j++)
                {
                    // Set cell contents
                    TableCell _tempCell = new TableCell();
                    _tempCell.Text = _messageData[j];

                    // Add cell to row
                    _tempRow.Controls.Add(_tempCell);
                    

                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            _tempCell.ColumnSpan = 2;
                        }
                        else if (j == 1)
                        {
                            _tempCell.ColumnSpan = 4;
                        }
                        else if (j == 2)
                        {
                            _tempCell.ColumnSpan = 1;
                            _tempCell.RowSpan = 2;
                        }
                    }
                    else
                    {
                        _tempCell.ColumnSpan = 1;
                    }

                }
                // Add row to table
                infoTbl.Controls.Add(_tempRow);
            }
            infoTbl.GridLines = GridLines.Both;
        }

        protected void logout_lkbtn_Click(object sender, EventArgs e)
        {
            controller.logout("student");
        }

    }
}