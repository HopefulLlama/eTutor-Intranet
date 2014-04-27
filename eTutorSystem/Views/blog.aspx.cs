using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eTutorSystem.Model;
using eTutorSystem.Controller_Model;

namespace eTutorSystem.Views
{
    public partial class blog : System.Web.UI.Page, IBlogInterface
    {
        blog_controller controller = blog_controller.ControllerInstance;

        UserDetails _user;
        List<BlogPosts> allPostsByUser;
        private Controller_Model.Model model = Controller_Model.Model.Instance;

        protected void Page_Load(object sender, EventArgs e)
        {
            controller.BlogView = this;
            controller.loadBlogView();
            checkUser();
            populateTable();
        }

        public string welcome
        {
            set { this.welcome_lbl.Text = value; }
        }

        public string studentID
        {
            get { return null; }
            set { this.studentID = value; }
        }

        private void checkUser()
        {
            _user = (UserDetails)Session["User"];
            if (Request.QueryString["blogOwner"] != null)
            {
                string blogOwner = Request.QueryString["blogOwner"];
                if (_user != null)
                {
                    if (_user.UserType == 1)
                    {
                        if (!blogOwner.Equals(_user.UserID))
                        {
                            Response.Redirect("student_area.aspx", true);
                        }
                    }
                    else
                    {
                        Label author = new Label();
                        UserDetails blogAuthor = model.selectUserDetailsById(Request.QueryString["blogOwner"]);
                        author.Text = "Blog Author: " + blogAuthor.FirstName + " " + blogAuthor.Surname;
                        author.CssClass = "standardLabel";
                        authorPanel.Controls.Add(author);
                        newPostLinkPanel.Visible = false;
                        newPostFormPanel.Visible = false;
                    }
                }
            }
            else
            {
                if (_user.UserType == 1)
                {
                    Response.Redirect("blog.aspx?blogOwner=" + _user.UserID, true);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        general_functions.Instance.loadTuteeDropDown("blog");
                    }
                    selectTuteePanel.Visible = true;
                    blogHistoryLinkPanel.Visible = false;
                    blogHistoryPanel.Visible = false;
                    newPostLinkPanel.Visible = false;
                    newPostFormPanel.Visible = false;
                }
            }
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
        
        protected void selectTuteeBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("blog.aspx?blogOwner=" + tuteeDdl.SelectedValue);
        }

        private void populateTable()
        {
            allPostsByUser = BlogPosts.getAllBlogPostsByUserID(Request.QueryString["blogOwner"]);
            int i = 0;
            foreach (BlogPosts bp in allPostsByUser)
            {
                TableRow row = new TableRow();
                TableCell cell = new TableCell();
                cell.Text = "" + bp.Date.ToString().Substring(0, 10);
                cell.Width = new Unit(50);
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "" + bp.Time;
                cell.Width = new Unit(50);
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "" + bp.PostContent;
                cell.Width = new Unit(400);
                row.Cells.Add(cell);

                // Set Row colours
                if (i%2 == 0)
                {
                    row.BackColor = Color.FromArgb(161, 178, 195);
                }
                else
                {
                    row.BackColor = Color.WhiteSmoke;
                }
                i++;

                blogHistoryTable.Rows.Add(row);
            }
            blogHistoryTable.GridLines = GridLines.Both;
        }

        protected void blogHistoryLinkClicked(object sender, EventArgs e)
        {
            if (blogHistoryPanel.Visible == true)
            {
                blogHistoryPanel.Visible = false;
                blogHistoryLink.Text = "Blog History - Hidden";
            }
            else
            {
                blogHistoryPanel.Visible = true;
                blogHistoryLink.Text = "Blog History - Visible";
            }
        }

        protected void newPostLinkButtonClicked(object sender, EventArgs e)
        {
            if (newPostFormPanel.Visible == true)
            {
                newPostFormPanel.Visible = false;
                newPostLinkButton.Text = "New Post Form - Hidden";
            }
            else
            {
                newPostFormPanel.Visible = true;
                newPostLinkButton.Text = "New Post Form - Visible";
            }
        }

        protected void submitNewBlogPost(object sender, EventArgs e)
        {
            if (_user.UserID == Request.QueryString["blogOwner"])
            {
                if (newPostTextbox.Text != "")
                {
                    DateTime date = DateTime.Now.Date;
                    TimeSpan time = DateTime.Now.TimeOfDay;

                    string postContent = Server.HtmlEncode(newPostTextbox.Text);
                    BlogPosts newPost = new BlogPosts(_user.UserID, date, time, postContent);
                    if (newPost.insertToDatabase())
                    {
                        Response.Write("<SCRIPT LANGUAGE=\"\"JavaScript\"\">alert(\"Blog post uploaded!\")</SCRIPT>");
                        if (_user.SupervisorID != "")
                        {
                            UserDetails supervisor = model.selectUserDetailsById(_user.SupervisorID);
                            string emailSubject = "New blog update!";
                            string emailMessage = "User " + _user.UserID + " has created a new post for their blog. Please log in to view the new post.";
                            general_functions.Instance.email(supervisor.EmailAddress, emailMessage, emailSubject);
                        }
                        Response.Redirect("../Views/blog.aspx?blogOwner=" + _user.UserID);
                    }
                    else
                    {
                        Response.Write("<SCRIPT LANGUAGE=\"\"JavaScript\"\">alert(\"Upload of blog post failed. Please try again.\")</SCRIPT>");
                    }
                    populateTable();
                }
                else
                {
                    Response.Write("<SCRIPT LANGUAGE=\"\"JavaScript\"\">alert(\"Blog entry cannot be empty.\")</SCRIPT>");
                }
            }
            else
            {
                Response.Write("<SCRIPT LANGUAGE=\"\"JavaScript\"\">alert(\"Only the blog owner may upload a new blog entry.\")</SCRIPT>");
            }
        }

        protected void logout_lkbtn_Click(object sender, EventArgs e)
        {
            controller.logout("blog");
        }
    }
}