using eTutorSystem.Controller_Model;
using eTutorSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Drawing;

namespace eTutorSystem.Views
{
    public partial class admin_area : System.Web.UI.Page, IadminFormInterface
    {
        private admin_controller controller = admin_controller.ControllerInstance;
        private string _tutorid = string.Empty;

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
                    controller.AdminView = this;
                    if (!IsPostBack) { general_functions.Instance.loadTutorDropDown("admin"); }
                    controller.loadAdminView();
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("login.aspx");
            }

        }

        public string tutorID
        {
            get { return this._tutorid; }
            set { this._tutorid = value; }
        }

        public string welcome
        {
            set { this.welcome_lbl.Text = value; }
        }

        protected void logout_lkbtn_Click(object sender, EventArgs e)
        {
            controller.logout("admin");
        }

        public string noStudents
        {
            set
            {
                this.studentsPanel.Controls.Clear();
                Label l1 = new Label();
                l1.Text = value;
                this.studentsPanel.Controls.Add(l1);
            }
        }

        public string numStudents
        {
            set { this.studentsLbl.Text = value; }
        }

        public string error
        {
            set
            {
                this.errorLbl.Text = value;
                this.errorLbl.Visible = true;
            }
        }

        public void setStudentsTable(Hashtable _data)
        {
            this.studentsTbl.Rows.Clear();
            for (int rowCounter = 0; rowCounter < _data.Count; rowCounter++)
            {
                TableRow _tempRow = new TableRow();
                string[] _messageData = (string[])_data[rowCounter];
                
                for (int columnCounter = 0; columnCounter < _messageData.Length; columnCounter++)
                {
                    // Set cell contents
                    TableCell _tempCell = new TableCell();

                    if (rowCounter >= 2 && columnCounter == 0)
                    {
                        CheckBox _ck = new CheckBox();
                        _ck.ID = _messageData[0];
                        _tempCell.Controls.Add(_ck);
                    }
                    else
                    {
                        _tempCell.Text = _messageData[columnCounter];
                    }

                    // Add cell to row
                    _tempRow.Controls.Add(_tempCell);

                    if (rowCounter == 0)
                    {
                        if (columnCounter == 0)
                        {
                            _tempCell.ColumnSpan = 1;
                        }
                        else if (columnCounter == 1)
                        {
                            _tempCell.ColumnSpan = 2;
                        }
                        else if (columnCounter == 2)
                        {
                            _tempCell.ColumnSpan = 1;
                        }
                        else if (columnCounter == 3)
                        {
                            _tempCell.ColumnSpan = 2;
                        }
                        else if (columnCounter == 4)
                        {
                            _tempCell.ColumnSpan = 1;
                        }
                    }
                    else
                    {
                        _tempCell.ColumnSpan = 1;
                    }
                }
                // Add row to table
                if (rowCounter >= 2)
                {
                    Color red = Color.OrangeRed;
                    Color orange = Color.Salmon;
                    Color yellow = Color.Wheat;
                    if(_tempRow.Cells[3].Text == "")
                    {
                        _tempRow.BackColor = red;
                    }
                    else if (_tempRow.Cells[6].Text == "")
                    {
                        _tempRow.BackColor = orange;
                    }
                    else
                    {
                        DateTime date = DateTime.Parse(_tempRow.Cells[6].Text);
                        if ((DateTime.Now - date).Days > 28)
                        {
                            _tempRow.BackColor = orange;
                        }
                        else if ((DateTime.Now - date).Days > 7)
                        {
                            _tempRow.BackColor = yellow;
                        }
                    }
                }
                studentsTbl.Controls.Add(_tempRow);
            }
            studentsTbl.GridLines = GridLines.Both;
        }

        public List<ListItem> tutorDropDown
        {
            set
            {
                this.tutorDdl.DataSource = value;
                this.tutorDdl.DataTextField = "Text";
                this.tutorDdl.DataValueField = "Value";
                this.tutorDdl.DataBind();
            }
        }

        protected void selectTutorBtn_Click(object sender, EventArgs e)
        {
            int _counter = 0;
            List<string> _studentList = new List<string>();
            foreach (TableRow row in studentsTbl.Rows)
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
                        tutorID = tutorDdl.SelectedValue;
                        _studentList.Add(_ck.ID);
                    }
                    _counter++;
                }
            }
            controller.processSupervisorChange(tutorID, _studentList);
        }

    }
}