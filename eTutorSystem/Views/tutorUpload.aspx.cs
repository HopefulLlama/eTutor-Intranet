using eTutorSystem.Controller_Model;
using eTutorSystem.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eTutorSystem.Views
{
    public partial class tutorViewFiles : System.Web.UI.Page, IUploadInterface
    {
        upload_controller controller = upload_controller.ControllerInstance;
        string _studentid = string.Empty;

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
                    controller.UploadView = this;

                    if (Request.QueryString["StudentID"] != null)
                    {
                        selectTuteePanel.Visible = false;
                        studentID = Request.QueryString["StudentID"].ToString();
                        controller.loadUploadsView();
                    }
                    else
                    {
                        selectTuteePanel.Visible = true;
                        viewUploadsDisplayPanel.Visible = false;
                        uploadsHeaderPanel.Visible = false;
                        
                        if (!Page.IsPostBack)
                        {
                            controller.loadUploadNullView();
                        }
                    }

                }
                else if (_user.UserType == 3)
                {
                    HttpContext.Current.Response.Redirect("admin_area.aspx");
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

        public string error
        {
            get { return this.errorLbl.Text; }
            set
            {
                this.errorLbl.Text = value;
                this.errorLbl.Visible = true;
            }
        }

        public string studentID
        {
            get { return this._studentid; }
            set { this._studentid = value; }
        }

        public string documentID
        {
            get
            {
                if (ViewState["docID"] != null)
                {
                    return ViewState["docID"].ToString();
                }
                else
                {
                    return null;
                }
            }
            set { ViewState["docID"] = value; }
        }

        public string comment
        {
            get { return this.commentTxtBx.Text; }
            set { this.commentTxtBx.Text = value; }
        }

        public string noMessages
        {
            set
            {
                noUploadsLbl.Text = value;
                noUploadsLbl.Visible = true;
            }
        }

        // Not needed in this context
        public FileUpload fileUpload
        {
            get { return null; }
        }

        public List<ListItem> tuteeDropDown
        {
            set
            {
                this.tuteeDdl.DataSource = value;
                this.tuteeDdl.DataTextField = "Text";
                this.tuteeDdl.DataValueField = "Value";
                this.tuteeDdl.DataBind();
            }
        }

        public void resetUploadView()
        {
            uploadsTbl.Rows.Clear();
            noMessages = string.Empty;
            error = string.Empty;
            documentID = null;
            comment = string.Empty;
            commentPanel.Visible = false;
        }

        public void setUploadTable(Hashtable _data)
        {
            for (int i = 0; i < _data.Count; i++)
            {
                TableRow _tempRow = new TableRow();
                string[] _messageData = (string[])_data[i];

                for (int j = 0; j < _messageData.Length; j++)
                {
                    // Set cell contents
                    TableCell _tempCell = new TableCell();

                    if (i != 0 && j == 3)
                    {
                        LinkButton _lnkBtn = new LinkButton();
                        _lnkBtn.Text = _messageData[j];
                        _lnkBtn.PostBackUrl = "../Views/downloadFile.aspx?DocumentID=" + _messageData[0];
                        _tempCell.Controls.Add(_lnkBtn);
                    }
                    else if (j == 4)
                    {
                        if (_messageData[j].Equals("n/a"))
                        {
                            LinkButton _lnkBtn = new LinkButton();
                            _lnkBtn.Text = "--- Add Comment ---";
                            _lnkBtn.Click += new EventHandler(commentsLnkBtn_Click);
                            _lnkBtn.ID = _messageData[0];
                            _tempCell.Controls.Add(_lnkBtn);
                        }
                        else
                        {
                            _tempCell.Text = _messageData[j];
                        }
                        
                    }
                    else
                    {
                        _tempCell.Text = _messageData[j];
                    }

                    // Add cell to row
                    if (i == 0 && j == 0)
                    {
                        _tempRow.Controls.Add(_tempCell);
                    }
                    else if (j != 0)
                    {
                        _tempRow.Controls.Add(_tempCell);
                    }

                    // Set cell width
                    if (j == (_messageData.Length - 1))
                    {
                        _tempCell.Width = new Unit(400, UnitType.Pixel);
                    }
                    else
                    {
                        _tempCell.Width = new Unit(50, UnitType.Pixel);
                    }
                }

                // Add row to table
                uploadsTbl.Controls.Add(_tempRow);
            }
            uploadsTbl.GridLines = GridLines.Both;
        }

        protected void logout_lkbtn_Click(object sender, EventArgs e)
        {
            controller.logout("upload");
        }

        protected void selectTuteeBtn_Click(object sender, EventArgs e)
        {
            studentID = tuteeDdl.SelectedValue;
            Debug.WriteLine(studentID);
            HttpContext.Current.Response.Redirect("tutorUpload.aspx?StudentID=" + studentID);
        }

        protected void uploadsLnkBtn_Click(object sender, EventArgs e)
        {
            if (viewUploadsDisplayPanel.Visible == true)
            {
                viewUploadsDisplayPanel.Visible = false;
                viewUploadsLnkBtn.Text = "Messages - Hidden";
            }
            else
            {
                viewUploadsDisplayPanel.Visible = true;
                viewUploadsLnkBtn.Text = "Messages - Visible";
            }
        }

        protected void commentsLnkBtn_Click(object sender, EventArgs e)
        {
            if (commentPanel.Visible == true)
            {
                commentPanel.Visible = false;
            }
            else
            {
                commentPanel.Visible = true;
            }

            LinkButton _lnkBtn = (LinkButton)sender;
            documentID = _lnkBtn.ID;
        }

        protected void submitCommentBtn_Click(object sender, EventArgs e)
        {
            if (comment != "")
            {
                controller.submitComment(documentID, Server.HtmlEncode(comment), studentID);
            }
            else
            {
                Response.Write("<SCRIPT LANGUAGE=\"\"JavaScript\"\">alert(\"Comment cannot be null.\")</SCRIPT>");
            }
        }

    }
}