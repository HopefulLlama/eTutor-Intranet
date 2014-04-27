using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace eTutorSystem.Controller_Model
{
    // Login Web Form Interface
    public interface IloginFormInterface
    {
        string username { get; set; }
        string password { get; set; }
        string usernameError { set; }
        string passwordError { set; }
        string status { set; }
    }

    // Student Area Interface
    public interface IstudentFormInterface
    {
        string welcome { set; }
        Boolean welcomeEnabled { set; }
        Boolean logoutVisible { set; }
        string studentToView { get; }
        void nullAnchors();
        string tutor { get; set; }
        string tutorEmail { get; set; }
        void setInfoTable(Hashtable _data);
    }

    // Tutor Area Interface
    public interface ItutorFormInterface
    {
        string welcome { set; }
        Boolean welcomeEnabled { set; }
        Boolean logoutVisible { set; }
        string studentToView { get; }
        void nullAnchors();
        string noTutees { set; }
        string numTutees { set; }
        string error { set; }
        void setTuteesTable(Hashtable _data);
    }

    // Admin Area Interface
    public interface IadminFormInterface
    {
        string welcome { set; }
        string noStudents { set; }
        string numStudents { set; }
        string error { set; }
        void setStudentsTable(Hashtable _data);
        List<ListItem> tutorDropDown { set; }
    }

    // Message Interface
    public interface IMessageInterface
    {
        string welcome { set; }
        string subject { get; set; }
        string messageContent { get; set; }
        string error { get; set; }
        string studentID { get; }
        string noMessages { set; }
        void resetMessageView();
        void resetGroupMessageView();
        void setMessageTable(Hashtable _data, string _senderid);
        List<ListItem> tuteeDropDown { set; }
    }

    // Tutor View Files Interface
    public interface IUploadInterface
    {
        string welcome { set; }
        string error { set; }
        string noMessages { set; }
        string studentID { get; set; }
        FileUpload fileUpload { get; }
        void resetUploadView();
        void setUploadTable(Hashtable _data);
        List<ListItem> tuteeDropDown { set; }
    }

    // Blog Interface
    public interface IBlogInterface
    {
        string welcome { set; }
        string studentID { get; set; }
        List<ListItem> tuteeDropDown { set; }
    }

    // Meeting Interface
    public interface IMeetingInterface
    {
        string welcome { set; }
        string studentID { get; set; }
    }   

    // Dashes Interfaces
    public interface IdashesInterface
    {
        string welcome { set; }
        int option { get; }
        List<ListItem> optionsDropDownBox { set; }
        string selectedUserID { get; }
    }
}