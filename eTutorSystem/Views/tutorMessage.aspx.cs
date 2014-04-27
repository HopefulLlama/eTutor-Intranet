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

namespace eTutorSystem.Views
{
    public partial class tutorMessage : System.Web.UI.Page, IMessageInterface
    {
        message_controller controller = message_controller.ControllerInstance;
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
                    controller.MessageView = this;

                    if (Request.QueryString["StudentID"] != null)
                    {
                        string _parameters = Request.QueryString["StudentID"].ToString();
                        string[] _parametersArray = _parameters.Split('-');
                        if (_parametersArray.Length > 1)
                        {
                            //group message
                            displayHeaderPanel.Visible = false;
                            messageDisplayPanel.Visible = false;
                            if (!Page.IsPostBack)
                            {
                                messagePanel.Visible = true;
                                newMessageLnkLbl.Text = "New Message - Visible";
                            }
                            general_functions.Instance.setWelcomeMessage("message");
                        }
                        else
                        {
                            studentID = _parametersArray[0];
                            controller.loadMessageView();
                        }
                    }
                    else 
                    {
                        selectTuteePanel.Visible = true;
                        messageDisplayPanel.Visible = false;
                        displayHeaderPanel.Visible = false;
                        messagePanel.Visible = false;
                        messageHeaderPanel.Visible = false;
                        if (!Page.IsPostBack)
                        {
                            controller.loadMessageNullView();
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

        public string subject
        {
            get { return this.subjectTxtBx.Text; }
            set { this.subjectTxtBx.Text = value; }
        }

        public string messageContent
        {
            get { return this.messageTxtBx.Text; }
            set { this.messageTxtBx.Text = value; }
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

        public string noMessages
        {
            set
            {
                noMessagesLbl.Text = value;
                noMessagesLbl.Visible = true;
            }
        }

        public void resetMessageView()
        {
            messageTbl.Rows.Clear();
            noMessages = string.Empty;
            subject = string.Empty;
            messageContent = string.Empty;
            error = string.Empty;
            messagePanel.Visible = false;
            newMessageLnkLbl.Text = "New Message - Hidden";
            messageDisplayPanel.Visible = true;
            messagesLnkBtn.Text = "Messages - Visible";
        }

        public void resetGroupMessageView()
        {
            subject = string.Empty;
            messageContent = string.Empty;
        }


        public void setMessageTable(Hashtable _data, string _senderid)
        {
            for (int i = 0; i < _data.Count; i++)
            {
                Color _tempColor = new Color();
                TableRow _tempRow = new TableRow();
                string[] _messageData = (string[])_data[i];

                for (int j = 0; j < _messageData.Length; j++)
                {
                    // Set cell contents
                    TableCell _tempCell = new TableCell();
                    _tempCell.Text = _messageData[j];
                    // Add cell to row
                    _tempRow.Controls.Add(_tempCell);

                    // Set cell width
                    if (j == (_messageData.Length - 1))
                    {
                        _tempCell.Width = new Unit(400, UnitType.Pixel);
                    }
                    else
                    {
                        _tempCell.Width = new Unit(50, UnitType.Pixel);
                    }

                    // Set row colour
                    if (_messageData[2].Equals(_senderid))
                    {
                        _tempColor = Color.Wheat;
                    }
                    else
                    {
                        _tempColor = Color.WhiteSmoke;
                    }

                }
                // Set Row colours
                if (i == 0)
                {
                    _tempRow.BackColor = Color.FromArgb(161, 178, 195);
                }
                else
                {
                    _tempRow.BackColor = _tempColor;
                }
                // Add row to table
                messageTbl.Controls.Add(_tempRow);
            }
            messageTbl.GridLines = GridLines.Both;
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

        protected void logout_lkbtn_Click(object sender, EventArgs e)
        {
            controller.logout("message");
        }

        protected void messagesLnkBtn_Click(object sender, EventArgs e)
        {
            if (messageDisplayPanel.Visible == true)
            {
                messageDisplayPanel.Visible = false;
                messagesLnkBtn.Text = "Messages - Hidden";
            }
            else
            {
                messageDisplayPanel.Visible = true;
                messagesLnkBtn.Text = "Messages - Visible";
            }
        }

        protected void newMessageLnkLbl_Click(object sender, EventArgs e)
        {
            if (messagePanel.Visible == true)
            {
                messagePanel.Visible = false;
                newMessageLnkLbl.Text = "New Message - Hidden";
            }
            else
            {
                messagePanel.Visible = true;
                newMessageLnkLbl.Text = "New Message - Visible";
            }
        }

        protected void submitMessageBtn_Click(object sender, EventArgs e)
        {
            if (subject != "" && messageContent != "")
            {
                string _parameters = Request.QueryString["StudentID"].ToString();
                string[] _parametersArray = _parameters.Split('-');
                List<string> _tutees = new List<string>();
                foreach (string _s in _parametersArray)
                {
                    _tutees.Add(_s);
                }
                controller.submitMessage(Server.HtmlEncode(subject), Server.HtmlEncode(messageContent), _tutees);
            }
            else
            {
                Response.Write("<SCRIPT LANGUAGE=\"\"JavaScript\"\">alert(\"Message content or subject cannot be null.\")</SCRIPT>");
            }
        }

        protected void selectTuteeBtn_Click(object sender, EventArgs e)
        {
            studentID = tuteeDdl.SelectedValue;
            Response.Redirect("tutorMessage.aspx?StudentID=" + studentID);
        }

    }
}