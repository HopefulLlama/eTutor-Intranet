using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI.WebControls;
using eTutorSystem.Model;

namespace eTutorSystem.Controller_Model
{
    public class upload_controller
    {
        #region Init

            // Class Declaration ans Instantiation
            private static upload_controller _instance;
            private IUploadInterface _uploadView;
            private ItutorFormInterface _tutorView;
            private Model model = Model.Instance;
            
            // Private constructor
            private upload_controller() { }

            // Controller Instance
            public static upload_controller ControllerInstance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = new upload_controller();
                    }
                    return _instance;
                }
            }

            // Set View Interfaces
            public IUploadInterface UploadView
            {
                get { return this._uploadView; }
                set 
                { 
                    this._uploadView = (IUploadInterface)value;
                    general_functions.Instance.UploadView = value;
                }
            }

            public ItutorFormInterface TutorView
            {
                get { return this._tutorView; }
                set { this._tutorView = (ItutorFormInterface)value; }
            }

        #endregion

        #region Upload Methods

            // Load initial Tutor File View 
            public void loadUploadsView()
            {
                general_functions.Instance.setWelcomeMessage("upload");
                displayUploads();
            }

            // Load Upload Initial View If Student ID = null
            public void loadUploadNullView()
            {
                general_functions.Instance.setWelcomeMessage("upload");
                general_functions.Instance.loadTuteeDropDown("upload");
            }

            // Process Upload Request
            public void processUploadRequest(List<string> _tutees)
            {
                if (_tutees.Count == 1)
                {
                    string _builder = string.Empty;
                    foreach (string _tutee in _tutees)
                    {
                        _builder += _tutee;
                    }
                    HttpContext.Current.Response.Redirect("tutorUpload.aspx?StudentId=" + _builder);
                }
                else
                {
                    TutorView.error = "Please Ensure you have selected JUST ONE tutee from the list...";
                }
            }

            // Process Upload
            public void processUpload()
            {
                FileUpload _fileUpload = UploadView.fileUpload;
                if (_fileUpload.HasFile)
                {
                    try
                    {
                        if (_fileUpload.PostedFile.ContentType == "application/pdf" ||
                            _fileUpload.PostedFile.ContentType == "application/doc" ||
                            _fileUpload.PostedFile.ContentType == "application/docx" ||
                            _fileUpload.PostedFile.ContentType == "application/vnd.msword" ||
                            _fileUpload.PostedFile.ContentType == "application/vnd.ms-word" ||
                            _fileUpload.PostedFile.ContentType == "application/winword" ||
                            _fileUpload.PostedFile.ContentType == "application/word" ||
                            _fileUpload.PostedFile.ContentType == "application/msword" ||
                            _fileUpload.PostedFile.ContentType == "application/x-msw6" ||
                            _fileUpload.PostedFile.ContentType == "application/x-msword" ||
                            _fileUpload.PostedFile.ContentType == "application/pdf" ||
                            _fileUpload.PostedFile.ContentType == "application/x-pdf" ||
                            _fileUpload.PostedFile.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" ||
                            _fileUpload.PostedFile.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.template")
                        {
                            if (_fileUpload.PostedFile.ContentLength < 2.097e+7)
                            {
                                UploadView.error = string.Empty;
                                UserDetails _user = (UserDetails)HttpContext.Current.Session["User"];
                                UserDetails _tutor = model.selectUserDetailsById(_user.SupervisorID);
                                string _filename = Path.GetFileName(_fileUpload.FileName);

                                string _appDomain = HttpRuntime.AppDomainAppPath.Replace("\\", "/");
                                string _userUploadFolder = _appDomain + "Storage/Files/" + _user.UserID;
                                if (!Directory.Exists(_userUploadFolder))
                                {
                                    Directory.CreateDirectory(_userUploadFolder);
                                }

                                List<string> _fileNamesRecursive = general_functions.Instance.generateFileNameRecursively(_filename, _userUploadFolder, 1, new List<string>());

                                string _path = "Storage/Files/" + _user.UserID + "/" + _fileNamesRecursive[(_fileNamesRecursive.Count - 1)];
                                string _date = DateTime.Now.ToString("yyy-MM-dd");
                                string _time = DateTime.Now.ToString("HH:mm:ss");
                                byte[] data = _fileUpload.FileBytes;

                                using (WebClient client = new WebClient())
                                {
                                    client.Credentials = new NetworkCredential("lj048", "Garentee12"); // Jon, place your account password here
                                    model.insertUploadFileDetails(_user.UserID, _date, _time, ("~/" + _path));
                                    client.UploadData(@"ftp://cms-stu-iis.gre.ac.uk/eTutorSystem/eTutorSystem/" + _path, data); // Jon, i think your ftp path might be: ftp://cms-stu-iis.gre.ac.uk/eTutorSystem/eTutorSystem/   but double check before you use it

                                    general_functions.Instance.email(_tutor.EmailAddress, "Your tutee (" + _user.UserID + ") has uploaded a file.\n Please Log into the eTutor system to view it.\n- eTutor System", "eTutor:- One of your tutees has uploaded a file!");
                                    UploadView.resetUploadView();
                                    displayUploads();
                                }
                               
                            }
                            else
                            {
                                UploadView.error = "Upload status: The file has to be less than 20MB!";
                            }
                        }
                        else
                        {
                            UploadView.error = "Upload status: Only PDF, DOCX & DOC files are accepted!";
                        }
                    }
                    catch (Exception ex)
                    {
                        UploadView.error = "Upload status: The file could not be uploaded. The following error occured:\n" + ex.Message;
                    }
                }
            }

            // Process Blog
            public void processBlogRequest(List<string> _tutees)
            {
                if (_tutees.Count == 1)
                {
                    foreach (string tutee in _tutees)
                    {
                        HttpContext.Current.Response.Redirect("blog.aspx?blogOwner=" + tutee);
                    }
                }
                else
                {
                    TutorView.error = "Please Ensure you have selected JUST ONE tutee from the list...";
                }
            }

            // Display student uploads
            public void displayUploads()
            {
                UserDetails _user = (UserDetails)HttpContext.Current.Session["User"];
                List<UserDocument> _uploadsList = new List<UserDocument>();

                if (_user.UserType == 1)
                {
                    _uploadsList = model.selectUploadFiles(_user.UserID);
                }
                else if (_user.UserType == 2)
                {
                    string _studentID = UploadView.studentID;
                    _uploadsList = model.selectUploadFiles(_studentID);
                }

                Hashtable _formattedMessageData = new Hashtable();
                int _counter = 1;

                if (_uploadsList.Count > 0)
                {
                    string _sender = string.Empty;
                    string[] _headers = { "Date", "Time", "File", "Comment" };
                    _formattedMessageData.Add(0, _headers);
                    foreach (UserDocument _doc in _uploadsList)
                    {
                        string[] _fileUploadSegments = _doc.UploadPath.Split('/');
                        string _file = _fileUploadSegments[4];
                        DocumentComment _comment = model.selectComment(_doc.DocumentID.ToString());
                        string _commentStr = (_comment == null) ? "n/a" : _comment.DocumentComments;
                        string[] _tempArray = { _doc.DocumentID.ToString(), _doc.Date.ToShortDateString(), _doc.Time.ToShortTimeString(), _file, _commentStr };
                        _formattedMessageData.Add(_counter, _tempArray);
                        _counter++;
                    }
                    UploadView.setUploadTable(_formattedMessageData);
                }
                else
                {
                    UploadView.noMessages = "No Upload History Found.";
                }
            }

            // Get Document Data
            public UserDocument getDocumentData(string _docid)
            {
                return model.selectUploadFileByDocumentId(_docid);
            }

            // Insert Comment
            public void submitComment(string _documentid, string _comment, string _studentid)
            {
                if (_documentid != string.Empty && _comment != string.Empty)
                {
                    UserDetails _user = (UserDetails)HttpContext.Current.Session["User"];
                    model.insertComment(_documentid, _user.UserID, _comment);
                    UserDetails _student = model.selectUserDetailsById(_studentid);
                    general_functions.Instance.email(_student.EmailAddress, "You have received feedback from your tutor (" + _user.UserID + ").\n Please Log into the eTutor system to view it.\n- eTutor System", "eTutor:- You have received feedback!");
                    UploadView.resetUploadView();
                    displayUploads();
                }
                else
                {
                    UploadView.error = "Please ensure you have entered a comment before submitting";
                }
            }

            // Logout
            public void logout(string _page)
            {
                general_functions.Instance.logout(_page);
            }

        #endregion
    }
}