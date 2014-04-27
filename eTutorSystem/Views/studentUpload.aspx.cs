using eTutorSystem.Controller_Model;
using eTutorSystem.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eTutorSystem.Views
{
    public partial class studentUpload : System.Web.UI.Page, IUploadInterface
    {
        upload_controller controller = upload_controller.ControllerInstance;

        protected void Page_Load(object sender, EventArgs e)
        {
            UserDetails _user = (UserDetails)Session["User"];
            if (_user != null)
            {
                if (_user.UserType == 1)
                {
                    controller.UploadView = this;
                    controller.loadUploadsView();
                }
                else if (_user.UserType == 2)
                {
                    HttpContext.Current.Response.Redirect("tutor_area.aspx");
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

        public string noMessages
        {
            set
            {
                noUploadsLbl.Text = value;
                noUploadsLbl.Visible = true;
            }
        }

        // not needeed in this context - inherited
        public string studentID
        {
            get { return null; }
            set { this.studentID = null; }
        }

        // not needed in this context - inherited
        public List<ListItem> tuteeDropDown
        {
            set { this.tuteeDropDown = null; }
        }

        public FileUpload fileUpload 
        {
            get { return this.fileUploadControl; } 
        }

        public void resetUploadView()
        {
            uploadsTbl.Rows.Clear();
            noMessages = string.Empty;
            error = string.Empty;
            newUploadPanel.Visible = false;
            newUploadLnkLbl.Text = "New Upload - Hidden";
            viewUploadsDisplayPanel.Visible = true;
            viewUploadsLnkBtn.Text = "Uploads - Visible";
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
                        _lnkBtn.OnClientClick = "window.open('../Views/downloadFile.aspx?DocumentID="+_messageData[0]+"')";
                       // _lnkBtn.PostBackUrl = "../Views/downloadFile.aspx?DocumentID=" + _messageData[0];
                        _tempCell.Controls.Add(_lnkBtn);
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


        protected void newMessageLnkLbl_Click(object sender, EventArgs e)
        {
            if (newUploadPanel.Visible == true)
            {
                newUploadPanel.Visible = false;
                newUploadLnkLbl.Text = "New Upload - Hidden";
            }
            else
            {
                newUploadPanel.Visible = true;
                newUploadLnkLbl.Text = "New Upload - Visible";
            }
        }

        protected void messagesLnkBtn_Click(object sender, EventArgs e)
        {
            if (viewUploadsDisplayPanel.Visible == true)
            {
                viewUploadsDisplayPanel.Visible = false;
                viewUploadsLnkBtn.Text = "Uploads - Hidden";
            }
            else
            {
                viewUploadsDisplayPanel.Visible = true;
                viewUploadsLnkBtn.Text = "Uploads - Visible";
            }
        }

        protected void logout_lkbtn_Click(object sender, EventArgs e)
        {
            controller.logout("upload");
        }

        protected void submitMessageBtn_Click(object sender, EventArgs e)
        {
            controller.processUpload();
        }
    }
}