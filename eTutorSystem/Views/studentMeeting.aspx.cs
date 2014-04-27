using eTutorSystem.Controller_Model;
using eTutorSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

namespace eTutorSystem.Views
{
    public partial class studentMeeting : System.Web.UI.Page, IMeetingInterface
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
            controller.MeetingView = this;
            populateTable();
            general_functions.Instance.MeetingView = this;
            Meeting_Controller.ControllerInstance.MeetingView = this;
            general_functions.Instance.setWelcomeMessage("meeting");

            populateDropDownList();
             UserDetails studentCheck = new UserDetails();
            List<MeetingDetails> meetingDetailsLists = new List<MeetingDetails>();
            studentCheck = (UserDetails)Session["User"];

            if ((Session["StudentMeetingDetailsLists"] != null)
                && (HttpContext.Current.Request.QueryString["ButtonID"] != null)
                && (HttpContext.Current.Request.QueryString["type"] != null))
            {
                meetingDetailsLists = (List<MeetingDetails>)Session["StudentMeetingDetailsLists"];

                foreach (MeetingDetails meetingItem in meetingDetailsLists)
                {
                    if (meetingItem.MeetingID == int.Parse(HttpContext.Current.Request.QueryString["ButtonID"])
                        && studentCheck.UserID == meetingItem.StudentID)
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
                appointmentPanel.Visible = true;
                Session["StudentMeetingDetailsLists"] = null;
                populateDropDownList();
                populateTable();
            }
        }
        public void populateDropDownList()
        {
           for (int i = 0; i < 24; i++)
            {
                HourDDL.Items.Add(i.ToString());
            }

            for (int j = 0; j < 60; j++)
            {
                MinuteDDL.Items.Add(j.ToString());
            }
        }

        System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(DBConnection.ConnectionString);
        DataTable dtSupervisor = new DataTable();
        string Query()
        {
            dtSupervisor.Rows.Clear();
            UserDetails _user = (UserDetails)Session["User"];
            System.Data.SqlClient.SqlCommand cm = new System.Data.SqlClient.SqlCommand("SELECT UserID, Firstname, Surname, EmailAddress FROM [Elite Falcons].[dbo].[UserDetails] where UserID in (SELECT SupervisorID FROM [Elite Falcons].[dbo].[UserDetails] where UserID =@ID)", cn);
            cm.Parameters.AddWithValue("@ID", _user.UserID);
            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cm);
            da.Fill(dtSupervisor);
           return dtSupervisor.Rows[0]["EmailAddress"].ToString();

        }

           protected void SendMailBttn_Click(object sender, EventArgs e)
        {
            string _recieverEmail = "";
            string _senderEmail = "";
            string _message = "";
            string _subject = "";
            string calendarValue = DateTime.Now.ToShortDateString();

            if (Calendar.SelectedDate.ToShortDateString() == "01/01/0001")
            {
                calendarValue = DateTime.Now.AddDays(1).ToShortDateString();
            }
            else
            {
                calendarValue = Calendar.SelectedDate.ToShortDateString();
            }

            UserDetails _user = (UserDetails)Session["User"];

            UserDetails tutorDetails = new UserDetails();
            MeetingDetails meetingInfo = new MeetingDetails();
            meetingInfo.selectMeetingDetailsByMeetingID();
            tutorDetails = UserDetails.getUserById(_user.SupervisorID);

            _recieverEmail = tutorDetails.EmailAddress.ToString().Trim();
            _senderEmail = _user.EmailAddress.ToString();


            _subject = "Meeting Request";
            _message = "Hello " + tutorDetails.FirstName.ToString().Trim() + " " + tutorDetails.Surname.ToString().Trim() + ", ";
            _message += Environment.NewLine;
            _message += "You have been requested to attend the following meeting at the following location by " + _user.Fullname + ", please respond, thank you.";
            _message += "Location: " + Environment.NewLine;
            _message += LocationTB.Text + Environment.NewLine;
            _message += Environment.NewLine + "Date and Time:";
            _message += Environment.NewLine + calendarValue + " " + HourDDL.SelectedItem + ":" + MinuteDDL.SelectedItem;
            _message += Environment.NewLine + Environment.NewLine;
            _message += "Kind Regards, " + Environment.NewLine + _user.Fullname.ToString();
            general_functions.Instance.email(_recieverEmail, _senderEmail, _message, _subject);
            DateTime dtt = Convert.ToDateTime((HourDDL.SelectedItem.ToString() + ":" + MinuteDDL.SelectedItem.ToString() + ":" + "00").ToString());
            TimeSpan TS = new TimeSpan(dtt.Hour, dtt.Minute, dtt.Second);



            MeetingDetails studentMeetingDetails = new MeetingDetails(_user.UserID,
                tutorDetails.UserID,
                Convert.ToDateTime(calendarValue),
                TS,
                TypeDDL.SelectedItem.ToString(),
                LocationTB.Text,
                "Accepted",
                "-");

            studentMeetingDetails.insertToDatabase();

            populateDropDownList();
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
            
             UserDetails tutorDetails = new UserDetails();
            MeetingDetails meetingInfo = new MeetingDetails();
            meetingInfo.MeetingID = long.Parse(MeetingID);
            meetingInfo.selectMeetingDetailsByMeetingID();

            tutorDetails= UserDetails.getUserById(meetingInfo.TutorID.ToString());

            _recieverEmail = tutorDetails.EmailAddress.ToString().Trim();
            _senderEmail = _user.EmailAddress.ToString();
            _subject = "Meeting Update";
            _message = "Hello " + tutorDetails.FirstName + " "+tutorDetails.Surname;
            _message += Environment.NewLine;
            _message += "There is a meeting update please login to the eTutor System to view the meeting details, thank you.";
            _message += Environment.NewLine + Environment.NewLine;
            _message += "Kind Regards, " + Environment.NewLine + _user.Fullname.ToString();
            general_functions.Instance.email(_recieverEmail, _senderEmail, _message, _subject);
            

            if(updateType == "Accepted")
            {
                meetingInfo.StudentStatus = "Accepted";
            }
            else if(updateType == "Declined")
            {
                meetingInfo.StudentStatus = "Declined";
            }

            meetingInfo.updateStudentToDatabase();

            populateDropDownList();
            appointmentPanel.Visible = true;
            populateTable();  
        }

        private void populateTable()
        {
            UserDetails _user = (UserDetails)Session["User"];
            meetingHistoryTable.Rows.Clear();
            List<MeetingDetails> allMeetingsByStudentID;
            allMeetingsByStudentID = MeetingDetails.getAllMeetingsByStudentID(_user.UserID);
            int i = 0;
            foreach (MeetingDetails md in allMeetingsByStudentID)
            {
                TableRow row = new TableRow();
                TableCell cell = new TableCell();

                cell.Text = "" + md.MeetingID.ToString().Trim();
                cell.Width = new Unit(50);
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "" + md.TutorName.ToString().Trim();
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

                if (md.StudentStatus.ToString().Trim() == "-")
                {
                    cell = new TableCell();
                    cell.Text = "" + md.TutorStatus.ToString().Trim();
                    cell.Width = new Unit(50);
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Controls.Add(createLinkButton(md.MeetingID.ToString(), "AcceptDecline"));
                    cell.Controls.Add(new Literal() { ID = md.MeetingID.ToString() + "br", Text = "<br/>" });
                    cell.Controls.Add(createLinkButton(md.MeetingID.ToString(), "DeclineAccept"));
                    cell.Width = new Unit(50);
                    row.Cells.Add(cell);
                }
                else if (md.StudentStatus.ToString().Trim() == "Accepted")
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
                else if (md.StudentStatus.ToString().Trim() == "Declined")
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
            Session["StudentMeetingDetailsLists"] = allMeetingsByStudentID;
        }

        private LinkButton createLinkButton(string buttonID, string type)
        {
            LinkButton lnkButton = new LinkButton();


            if (type == "Decline")
            {
                lnkButton.ID = type + buttonID;
                lnkButton.Text = "Decline";
                lnkButton.PostBackUrl = "studentMeeting.aspx?type=" + type + "&ButtonID=" + buttonID;
                lnkButton.CssClass = "textcolor";
                lnkButton.CausesValidation = false;
            }
            else if (type == "Accept")
            {
                lnkButton.ID = type + buttonID;
                lnkButton.Text = "Accept";
                lnkButton.PostBackUrl = "studentMeeting.aspx?type=" + type + "&ButtonID=" + buttonID;
                lnkButton.CssClass = "textcolor";
                lnkButton.CausesValidation = false;
            }
            else if(type== "AcceptDecline")
            {
                lnkButton.ID = type + buttonID;
                lnkButton.Text = "Accept";
                lnkButton.PostBackUrl = "studentMeeting.aspx?type=AcceptDecline&ButtonID=" + buttonID;
                lnkButton.CssClass = "textcolor";
                lnkButton.CausesValidation = false;
            }
            else if (type == "DeclineAccept")
            {
                lnkButton.ID = type + buttonID;
                lnkButton.Text = "Decline";
                lnkButton.PostBackUrl = "studentMeeting.aspx?type=DeclineAccept&ButtonID=" + buttonID;
                lnkButton.CssClass = "textcolor";
                lnkButton.CausesValidation = false;
            }

            return lnkButton;
        }

        protected void logout_lkbtn_Click(object sender, EventArgs e)
        {
            controller.logout("studentMeeting");
        }

    }
}