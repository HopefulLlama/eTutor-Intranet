using eTutorSystem.Controller_Model;
using eTutorSystem.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eTutorSystem.Views
{
    public partial class downloadFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["DocumentID"] != null)
            {
                UserDetails _user = (UserDetails)Session["User"];
                if (_user != null)
                {
                    if (_user.UserType == 1 || _user.UserType == 2)
                    {
                        string _documentid = Request.QueryString["DocumentID"];
                        UserDocument _doc = upload_controller.ControllerInstance.getDocumentData(_documentid);
                        
                        string _appDomain = HttpRuntime.AppDomainAppPath.Replace("\\", "/");
                        string _fileLocation = _doc.UploadPath;
                        FileInfo file = new FileInfo(_appDomain + _fileLocation.Remove(0, 2));

                        string[] _fileLocationArray = _fileLocation.Split('/');
                        string[] _filenameArray = _fileLocationArray[4].Split('.');
                        string _fileType = _filenameArray[1];
                        string _contentType = string.Empty;

                        if(_fileType.Equals("pdf"))
                        {
                            _contentType = "application/pdf";
                        }
                        else if(_fileType.Equals("doc") || _fileType.Equals("docx"))
                        {
                            _contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                        }
                        HttpContext.Current.Response.ContentType = _contentType;
                        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                        HttpContext.Current.Response.TransmitFile(file.FullName);
                        HttpContext.Current.Response.End();

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
            else
            {
 
            }

           

        }
    }
}