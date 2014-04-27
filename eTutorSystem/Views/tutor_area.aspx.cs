using eTutorSystem.Controller_Model;
using eTutorSystem.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

namespace eTutorSystem.Views
{
    public partial class tutor_area : System.Web.UI.Page, ItutorFormInterface
    {
        tutor_controller controller = tutor_controller.ControllerInstance;

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
                    controller.TutorView = this;
                    #region Sorting out search crap
                    string firstName = "";
                    string surname = "";
                    int programmeID = 0;
                    int orderType = 0;
                    if (Request.QueryString["firstName"] != null)
                    {
                        firstName = Request.QueryString["firstName"];
                    }
                    if (Request.QueryString["surname"] != null){
                        surname = Request.QueryString["surname"];
                    }
                    if (Request.QueryString["programmeID"] != null)
                    {
                        int.TryParse(Request.QueryString["programmeID"], out programmeID);
                    }
                    if (Request.QueryString["orderType"] != null)
                    {
                        int.TryParse(Request.QueryString["orderType"], out orderType);
                    }
                    #endregion
                    controller.loadTutorView(firstName, surname, programmeID, orderType);
                    if (!Page.IsPostBack)
                    {
                        List<ProgrammeDetails> allProgrammes = new List<ProgrammeDetails>();
                        allProgrammes.Add(new ProgrammeDetails(0, ""));
                        allProgrammes.AddRange(ProgrammeDetails.getAllProgrammeDetails());

                        programmeDropdown.DataTextField = "Name";
                        programmeDropdown.DataValueField = "ProgrammeID";
                        programmeDropdown.DataSource = allProgrammes;
                        programmeDropdown.DataBind();
                    }
                }
                else if (_user.UserType == 3)
                {
                    if (Request.QueryString["UID"] != null)
                    {
                        controller.TutorView = this;
                        controller.loadTutorViewForAdmin();
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
            dash.HRef = "../Views/tutor_area.aspx?UID=" + studentToView;
            message.HRef = "../Views/tutor_area.aspx?UID=" + studentToView;
            meet.NavigateUrl = "../Views/tutor_area.aspx?UID=" + studentToView;
            upload.HRef = "../Views/tutor_area.aspx?UID=" + studentToView;
            blogLink.NavigateUrl = "../Views/tutor_area.aspx?UID=" + studentToView;
        }

        public string noTutees
        {
            set
            {
                this.tuteesPanel.Controls.Clear();
                Label l1 = new Label();
                l1.Text = value;
                this.tuteesPanel.Controls.Add(l1);
            }
        }

        public string numTutees
        {
            set { this.tuteesLbl.Text = value; }
        }

        public string error
        {
            set
            {
                this.errorLbl.Text = value;
                this.errorLbl.Visible = true;
            }
        }

        public void setTuteesTable(Hashtable _data)
        {
            for (int i = 0; i < _data.Count; i++)
            {
                TableRow _tempRow = new TableRow();
                string[] _messageData = (string[])_data[i];

                for (int j = 0; j < _messageData.Length; j++)
                {
                    // Set cell contents
                    TableCell _tempCell = new TableCell();

                    if (i >= 2 && j == 0)
                    {
                        CheckBox _ck = new CheckBox();
                        _ck.ID = _messageData[0];
                        _tempCell.Controls.Add(_ck);
                    }
                    else
                    {
                        _tempCell.Text = _messageData[j];
                    }

                    // Add cell to row
                    _tempRow.Controls.Add(_tempCell);

                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            _tempCell.ColumnSpan = 3;
                        }
                        else if (j == 1)
                        {
                            _tempCell.ColumnSpan = 3;
                        }
                        else if (j == 2)
                        {
                            _tempCell.ColumnSpan = 5;
                        }
                        else if (j == 3)
                        {
                            _tempCell.ColumnSpan = 2;
                        }
                        else if (j == 4)
                        {
                            _tempCell.ColumnSpan = 2;
                        }
                    }
                    else
                    {
                        _tempCell.ColumnSpan = 1;
                    }

                }
                // Add row to table
                tuteesTbl.Controls.Add(_tempRow);
            }
            tuteesTbl.GridLines = GridLines.Both;
        }

        protected void logout_lkbtn_Click(object sender, EventArgs e)
        {
            controller.logout("tutor");
        }
              
        protected void selectLnkBtn_Click(object sender, EventArgs e)
        {
            int _counter = 0;
            foreach (TableRow row in tuteesTbl.Rows)
            {
                if (_counter == 0 || _counter == 1)
                {
                    _counter++;
                }
                else
                {
                    var _ck = (CheckBox)row.Cells[0].Controls[0];
                    if (_ck.Checked == false)
                    {
                        _ck.Checked = true;
                    }
                    _counter++;
                }
            }
        }

        protected void deselectLnkBtn_Click(object sender, EventArgs e)
        {
            int _counter = 0;
            foreach (TableRow row in tuteesTbl.Rows)
            {
                if (_counter == 0 || _counter == 1)
                {
                    _counter++;
                }
                else
                {
                    var _ck = (CheckBox)row.Cells[0].Controls[0];
                    if (_ck.Checked == true)
                    {
                        _ck.Checked = false;
                    }
                    _counter++;
                }
            }
        }

        private List<string> gatherSelectedTutees()
        {
            int _counter = 0;
            List<string> _tuteeList = new List<string>();
            foreach (TableRow row in tuteesTbl.Rows)
            {
                if (_counter == 0 || _counter == 1)
                {
                    _counter++;
                }
                else
                {
                    var _ck = (CheckBox)row.Cells[0].Controls[0];
                    if (_ck.Checked)
                    {
                        _tuteeList.Add(_ck.ID);
                    }
                    _counter++;
                }
            }
            return _tuteeList;
        }

        protected void messageLnkBtn_Click(object sender, EventArgs e)
        {
            message_controller.ControllerInstance.processMessageRequest(gatherSelectedTutees());
        }

        protected void meetingLnkBtn_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Redirect("tutorMeeting.aspx");
        }

        protected void uploadsLnkBtn_Click(object sender, EventArgs e)
        {
            upload_controller.ControllerInstance.processUploadRequest(gatherSelectedTutees());
        }

        protected void blogLnkBtn_Click(object sender, EventArgs e)
        {
            blog_controller.ControllerInstance.processBlogRequest(gatherSelectedTutees());
        }

        protected void searchLinkButtonClicked(object sender, EventArgs e)
        {
            if (searchFormPanel.Visible == false)
            {
                searchFormPanel.Visible = true;
                searchLinkButton.Text = "Search Form - Visible";
            }
            else
            {
                searchFormPanel.Visible = false;
                searchLinkButton.Text = "Search Form - Hidden";
            }
        }

        protected void submitSearch(object sender, EventArgs e)
        {
            StringBuilder url = new StringBuilder();
            url.Append("./tutor_area.aspx");
            if (firstNameTextbox.Text != "" || surnameTextbox.Text != "" || programmeDropdown.SelectedValue != "0" || orderDropdown.SelectedValue != "0")
            {
                url.Append("?");
                int counter = 0;
                if (firstNameTextbox.Text != "")
                {
                    url.Append("firstName=" + firstNameTextbox.Text);
                    counter++;
                }
                if (surnameTextbox.Text != "")
                {
                    if(counter > 0)
                    {
                        url.Append("&");
                    }
                    url.Append("surname=" + surnameTextbox.Text);
                    counter++;
                }
                if (programmeDropdown.SelectedValue != "0")
                {
                    if(counter > 0)
                    {
                        url.Append("&");
                    }
                    url.Append("programmeID=" + programmeDropdown.SelectedValue);
                    counter++;
                }
                if (orderDropdown.SelectedValue != "0")
                {
                    if (counter > 0)
                    {
                        url.Append("&");
                    }
                    url.Append("orderType=" + orderDropdown.SelectedValue);
                    counter++;
                }
            }
                
            Response.Redirect(url.ToString());
        }


        protected void submitReset(object sender, EventArgs e)
        {
            StringBuilder url = new StringBuilder();
            url.Append("./tutor_area.aspx");
            Response.Redirect(url.ToString());
        }
    
    }
}