using eTutorSystem.Controller_Model;
using eTutorSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eTutorSystem
{
    public partial class tutorMeeting : System.Web.UI.Page, IMeetingInterface
    {

        Meeting_Controller controller = Meeting_Controller.ControllerInstance;

        public string welcome
        {
            set { this.welcome_lbl.Text = value; }
        }

        public string studentID
        {
            get { return null; }
            set { this.studentID = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            UserDetails tutorCheck = new UserDetails();
            populateTable();
            List<MeetingDetails> meetingDetailsLists = new List<MeetingDetails>();
            tutorCheck = (UserDetails)Session["User"];
            general_functions.Instance.MeetingView = this;
            Meeting_Controller.ControllerInstance.MeetingView = this;
            general_functions.Instance.setWelcomeMessage("meeting");         

            if ((Session["meetingDetailsLists"] != null)
                && (HttpContext.Current.Request.QueryString["ButtonID"] != null)
                && (HttpContext.Current.Request.QueryString["type"] != null))
            {
                meetingDetailsLists = (List<MeetingDetails>)Session["meetingDetailsLists"];

                foreach (MeetingDetails meetingItem in meetingDetailsLists)
                {
                    if (meetingItem.MeetingID == int.Parse(HttpContext.Current.Request.QueryString["ButtonID"])
                        && tutorCheck.UserID == meetingItem.TutorID)
                    {
                        if (HttpContext.Current.Request.QueryString["type"] == "Accept")
                        {
                            string MeetingID = meetingItem.MeetingID.ToString();
                            string updateType = "Accepted";
                            updateMeeting(MeetingID, updateType);
                            break;
                        }
                        if (HttpContext.Current.Request.QueryString["type"] == "Decline")
                        {
                            string MeetingID = meetingItem.MeetingID.ToString();
                            string updateType = "Declined";
                            updateMeeting(MeetingID, updateType);
                            break;
                        }
                        if (HttpContext.Current.Request.QueryString["type"] == "AcceptDecline")
                        {
                            string MeetingID = meetingItem.MeetingID.ToString();
                            string updateType = "Accepted";
                            updateMeeting(MeetingID, updateType);
                            break;
                        }
                        if (HttpContext.Current.Request.QueryString["type"] == "DeclineAccept")
                        {
                            string MeetingID = meetingItem.MeetingID.ToString();
                            string updateType = "Declined";
                            updateMeeting(MeetingID, updateType);
                            break;
                        }

                        else
                        {
                            break;
                        }
                    }
                }
            }

            if (!IsPostBack)
            {
                Session["meetingDetailsLists"] = null;
                TuteesSelectionPanel.Visible = true;
                appointmentPanel.Visible = false;
                populateDropDownList();
                populateStudentList();
                populateTable();
            }
        }
        System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(DBConnection.ConnectionString);
        DataTable dtstudents = new DataTable();
        
        private void Query()
        {
            dtstudents.Rows.Clear();
            UserDetails _user = (UserDetails)Session["User"];
            System.Data.SqlClient.SqlCommand cm = new System.Data.SqlClient.SqlCommand("SELECT UserID,Firstname+' '+Surname as Name,EmailAddress,UserType,SupervisorID  FROM [Elite Falcons].[dbo].[UserDetails] where SupervisorID =@ID", cn);
            cm.Parameters.AddWithValue("@ID", _user.UserID);
            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cm);
            da.Fill(dtstudents);
            tuteeDdl.DataValueField = "UserID";
            tuteeDdl.DataTextField = "Name";
            tuteeDdl.DataSource = dtstudents;
            tuteeDdl.DataBind();

        }


        private void populateTable()
        {
            UserDetails _user = (UserDetails)Session["User"];
            meetingHistoryTable.Rows.Clear();
            List<MeetingDetails> allMeetingsByTutorID;
            allMeetingsByTutorID = MeetingDetails.getAllMeetingsByTutorID(_user.UserID);
            int i = 0;
            foreach (MeetingDetails md in allMeetingsByTutorID)
            {
                TableRow row = new TableRow();
                TableCell cell = new TableCell();

                cell.Text = "" + md.MeetingID.ToString().Trim();
                cell.Width = new Unit(50);
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "" + md.StudentName.ToString().Trim();
                cell.Width = new Unit(50);
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "" + md.Date.ToString().Substring(0, 10);
                cell.Width = new Unit(50);
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "" + md.Time;
                cell.Width = new Unit(50);
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "" + md.Type.ToString().Trim();
                cell.Width = new Unit(400);
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "" + md.Location.ToString().Trim();
                cell.Width = new Unit(50);
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "" + md.StudentStatus.ToString().Trim();
                cell.Width = new Unit(50);
                row.Cells.Add(cell);

                if(md.TutorStatus.ToString().Trim() == "-")
                {
                    cell = new TableCell();
                    cell.Text = "" + md.TutorStatus.ToString().Trim();
                    cell.Width = new Unit(50);
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Controls.Add(createLinkButton(md.MeetingID.ToString(), "AcceptDecline"));
                    cell.Controls.Add(new Literal() { ID=md.MeetingID.ToString() +"br", Text="<br/>" } );
                    cell.Controls.Add(createLinkButton(md.MeetingID.ToString(), "DeclineAccept"));
                    cell.Width = new Unit(50);
                    row.Cells.Add(cell);
                }
                else if (md.TutorStatus.ToString().Trim() == "Accepted")
                {
                    cell = new TableCell();
                    cell.Text = "" + md.TutorStatus.ToString().Trim();
                    cell.Width = new Unit(50);
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Controls.Add(createLinkButton(md.MeetingID.ToString(), "Decline"));
                    cell.Width = new Unit(50);
                    row.Cells.Add(cell);
                }
                else if (md.TutorStatus.ToString().Trim() == "Declined")
                {
                    cell = new TableCell();
                    cell.Text = "" + md.TutorStatus.ToString().Trim();
                    cell.Width = new Unit(50);
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Controls.Add(createLinkButton(md.MeetingID.ToString(), "Accept"));
                    cell.Width = new Unit(50);
                    row.Cells.Add(cell);
                }

                // Set Row colours
                if (i % 2 == 0)
                {
                    row.BackColor = Color.FromArgb(161, 178, 195);
                }
                else
                {
                    row.BackColor = Color.WhiteSmoke;
                }
                i++;

                meetingHistoryTable.Rows.Add(row);
            }
            meetingHistoryTable.GridLines = GridLines.Both;
            Session["meetingDetailsLists"] = allMeetingsByTutorID;
        }

        private LinkButton createLinkButton(string buttonID, string type) 
        {
            LinkButton lnkButton = new LinkButton();
            
            
            if(type == "Decline")
            {
                lnkButton.ID = type + buttonID;
                lnkButton.Text = "Decline";
                lnkButton.PostBackUrl = "tutorMeeting.aspx?type=" + type + "&ButtonID=" + buttonID;
                lnkButton.CssClass = "textcolor";
                lnkButton.CausesValidation = false;
            }
            else if(type == "Accept")
            {
                lnkButton.ID = type + buttonID;
                lnkButton.Text = "Accept";
                lnkButton.PostBackUrl = "tutorMeeting.aspx?type=" + type + "&ButtonID=" + buttonID;
                lnkButton.CssClass = "textcolor";
                lnkButton.CausesValidation = false;
            }

            else if (type == "AcceptDecline")
            {
                lnkButton.ID = type + buttonID;
                lnkButton.Text = "Accept";
                lnkButton.PostBackUrl = "tutorMeeting.aspx?type=AcceptDecline&ButtonID=" + buttonID;
                lnkButton.CssClass = "textcolor";
                lnkButton.CausesValidation = false;
            }

            else if (type == "DeclineAccept")
            {
                lnkButton.ID = type + buttonID;
                lnkButton.Text = "Decline";
                lnkButton.PostBackUrl = "tutorMeeting.aspx?type=DeclineAccept&ButtonID=" + buttonID;
                lnkButton.CssClass = "textcolor";
                lnkButton.CausesValidation = false;
            }


            return lnkButton;
        }

        public void populateStudentList()
        {
            Query();
            TuteesSelectionPanel.Visible = true;
        }

        public void populateDropDownList()
        {
            TuteesSelectionPanel.Visible = true;
            //HourAndMinutes 
            for (int i = 0; i < 24; i++)
            {
                DropDownList1.Items.Add(i.ToString());
            }

            for (int j = 0; j < 60; j++)
            {
                DropDownList3.Items.Add(j.ToString());
            }
        }


        protected void selectTuteeBtn_Click(object sender, EventArgs e)
        {
            populateTable();
            TuteesSelectionPanel.Visible = false;
            Session["selectedStudent"] = null;
            Session["selectedStudent"] = tuteeDdl.SelectedValue.ToString().Trim();
            populateDropDownList();
            appointmentPanel.Visible = true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string _recieverEmail = "";
            string _senderEmail = "";
            string _message = "";
            string _subject = "";
           string calendarValue = DateTime.Now.ToShortDateString();

           if (Calendar1.SelectedDate.ToShortDateString() == "01/01/0001")
           {
               calendarValue = DateTime.Now.AddDays(1).ToShortDateString();
           }
           else
           {
               calendarValue = Calendar1.SelectedDate.ToShortDateString();
           }

            UserDetails _user = (UserDetails)Session["User"];

            UserDetails studentDetails = new UserDetails();
            MeetingDetails meetingInfo = new MeetingDetails();
            meetingInfo.selectMeetingDetailsByMeetingID();

            studentDetails = UserDetails.getUserById(tuteeDdl.SelectedValue);

            _recieverEmail = studentDetails.EmailAddress.ToString().Trim();
            _senderEmail = _user.EmailAddress.ToString();


            _subject = "Meeting Request";
            _message = "Hello " + studentDetails.FirstName.ToString().Trim() + " "+ studentDetails.Surname.ToString().Trim() +", ";
            _message += Environment.NewLine;
            _message += "You have been requested to attend the following meeting at the following location by " + _user.Fullname + ", please respond, thank you.";
            _message += "Location: " + Environment.NewLine;
            _message += TextBox1.Text + Environment.NewLine;
            _message += Environment.NewLine + "Date and Time:";
            _message += Environment.NewLine + calendarValue + " " + DropDownList1.SelectedItem + ":" + DropDownList3.SelectedItem;
            _message += Environment.NewLine + Environment.NewLine;
            _message += "Kind Regards, " + Environment.NewLine + _user.Fullname.ToString();
            general_functions.Instance.email(_recieverEmail, _senderEmail, _message, _subject);
            DateTime dtt = Convert.ToDateTime((DropDownList1.SelectedItem.ToString() + ":" + DropDownList3.SelectedItem.ToString() +":" + "00").ToString());
            TimeSpan TS = new TimeSpan(dtt.Hour, dtt.Minute, dtt.Second);
            string selectedTutee = (String) Session["selectedStudent"];

            

            MeetingDetails tutorMeetingDetails = new MeetingDetails(selectedTutee,
                _user.UserID,
                Convert.ToDateTime(calendarValue),
                TS,
                DropDownList2.SelectedItem.ToString(),
                TextBox1.Text,
                "-",
                "Accepted");
            tutorMeetingDetails.insertToDatabase();

            populateDropDownList();
            TuteesSelectionPanel.Visible = false;
            appointmentPanel.Visible = true;
            populateTable();
        }

        private void updateMeeting(string MeetingID, string updateType)
        {
            string _recieverEmail = "";
            string _senderEmail = "";
            string _message = "";
            string _subject = "";
           
            UserDetails _user = (UserDetails)Session["User"];
            
             UserDetails studentDetails = new UserDetails();
            MeetingDetails meetingInfo = new MeetingDetails();
            meetingInfo.MeetingID = long.Parse(MeetingID);
            meetingInfo.selectMeetingDetailsByMeetingID();

            studentDetails= UserDetails.getUserById(meetingInfo.StudentID.ToString());

            _recieverEmail = studentDetails.EmailAddress.ToString().Trim();
            _senderEmail = _user.EmailAddress.ToString();
            _subject = "Meeting Update";
            _message = "Hello " + studentDetails.FirstName + " "+studentDetails.Surname;
            _message += Environment.NewLine;
            _message += "There is a meeting update please login to the eTutor System to view the meeting details, thank you.";
            _message += Environment.NewLine + Environment.NewLine;
            _message += "Kind Regards, " + Environment.NewLine + _user.Fullname.ToString();
            general_functions.Instance.email(_recieverEmail, _senderEmail, _message, _subject);
            
            DateTime dtt = Convert.ToDateTime((DropDownList1.SelectedItem.ToString() + ":" + DropDownList3.SelectedItem.ToString() +":" + "00").ToString());
            TimeSpan TS = new TimeSpan(dtt.Hour, dtt.Minute, dtt.Second);

            if(updateType == "Accepted")
            {
                meetingInfo.TutorStatus = "Accepted";
            }
            else if(updateType == "Declined")
            {
                meetingInfo.TutorStatus = "Declined";
            }

            
            meetingInfo.updateToDatabase();

            populateDropDownList();
            TuteesSelectionPanel.Visible = false;
            appointmentPanel.Visible = true;
            populateTable();
        }

        protected void logout_lkbtn_Click(object sender, EventArgs e)
        {
            controller.logout("tutorMeeting");
        }
    }
}