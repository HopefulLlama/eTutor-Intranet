<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="admin_ViewDashes.aspx.cs" Inherits="eTutorSystem.Views.admin_ViewDashes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    UoG - Dashboards
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="welcome" runat="server">
    <asp:Label ID="welcome_lbl" runat="server"></asp:Label>
    <asp:LinkButton ID="logout_lkbtn" runat="server" CssClass="logout_lbl" OnClick="logout_lkbtn_Click">Logout?</asp:LinkButton>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="server">
    <h1>View student & tutor dashboards</h1>

     <asp:Panel ID="messagePanel" runat="server" style="margin-left:10%; text-align: center; overflow-x:hidden;" BackColor="#EEEEEE"  BorderColor="#999999" BorderStyle="Dashed" BorderWidth="2px" Width="80%">
         <br />
         <asp:Label ID="Label1" runat="server" CssClass="standardLabel" Text="Who's dash would you like to view?"></asp:Label>
         <br />
         <asp:RadioButton ID="studentRdBtn" runat="server" CssClass="standardLabel" Text="Student" Checked="True" GroupName="rdGroup" OnCheckedChanged="studentRdBtn_CheckedChanged" AutoPostBack="true"/>
         &nbsp;&nbsp;
         <asp:RadioButton ID="tutorrdBtn" runat="server" CssClass="standardLabel" Text="Tutor" GroupName="rdGroup" OnCheckedChanged="tutorrdBtn_CheckedChanged" AutoPostBack="true"/>
         <br />
         <br />
         <asp:Label ID="Label2" runat="server" CssClass="standardLabel" Text="Student/ Tutor:"></asp:Label>
         <br />
         <asp:DropDownList ID="usersDdb" runat="server" Width="300px">
         </asp:DropDownList>
         <br />
         <br />
         <asp:Button ID="submitMessageBtn" runat="server" Text="Submit" Width="200px" OnClick="submitMessageBtn_Click" OnClientClick="target ='_blank';" />
         <br />
         <br />
     </asp:Panel>
    <br />

</asp:Content>
