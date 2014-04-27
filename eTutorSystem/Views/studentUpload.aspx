<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Student.Master" AutoEventWireup="true" CodeBehind="studentUpload.aspx.cs" Inherits="eTutorSystem.Views.studentUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    UoG Student - Uploads
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="welcome" runat="server">
     <asp:Label ID="welcome_lbl" runat="server"></asp:Label>
    &nbsp;
    <asp:LinkButton ID="logout_lkbtn" runat="server" CssClass="logout_lbl" OnClick="logout_lkbtn_Click">Logout?</asp:LinkButton>
    &nbsp;
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="server">
    <h1>&nbsp;Uploads</h1>
    <asp:Panel ID="Panel1" runat="server" style="margin-left:10%; text-align: left;" BackColor="#EEEEEE" BorderColor="#999999" BorderStyle="Solid" BorderWidth="2px" Width="80%">
         &nbsp;<asp:LinkButton ID="newUploadLnkLbl" runat="server" OnClick="newMessageLnkLbl_Click" style="text-align: left" CssClass="standardLinkButton">New Upload - Hidden</asp:LinkButton>
     </asp:Panel>
     <asp:Panel ID="newUploadPanel" runat="server" style="margin-left:10%; border-top:none; text-align: center; overflow-x:hidden;" BackColor="#EEEEEE"  BorderColor="#999999" BorderStyle="Dashed" BorderWidth="2px" Width="80%" Visible="False">
         <br />
         <asp:Label ID="Label1" runat="server" CssClass="standardLabel" Text="Upload Document:"></asp:Label>
         <br />
         <asp:FileUpload ID="fileUploadControl" runat="server" />
         <br />
         <br />
         <asp:Button ID="submitMessageBtn" runat="server" Text="Submit" OnClick="submitMessageBtn_Click" />
         <br />
         <asp:Label ID="errorLbl" runat="server" CssClass="errorLabel" Visible="False"></asp:Label>
         <br />
     </asp:Panel>
    &nbsp;
     <asp:Panel ID="Panel2" runat="server" style="margin-left:10%; text-align: left;" BackColor="#EEEEEE" BorderColor="#999999" BorderStyle="Solid" BorderWidth="2px" Width="80%">
         &nbsp;
         <asp:LinkButton ID="viewUploadsLnkBtn" runat="server" CssClass="standardLinkButton" OnClick="messagesLnkBtn_Click">Uploads - Visible</asp:LinkButton>
     </asp:Panel>
    <asp:Panel ID="viewUploadsDisplayPanel" runat="server" style="margin-left:10%; border-top:none; text-align: center;" Width="80%" BackColor="#EEEEEE" BorderColor="#999999" BorderStyle="Dashed" BorderWidth="2px">
    <br />
    <asp:Table ID="uploadsTbl" runat="server" CssClass="table" Font-Names="lato" Font-Size="14pt" ForeColor="#1F4F89">
    </asp:Table>
    <asp:Label ID="noUploadsLbl" runat="server" Visible="False"></asp:Label>
    <br />
</asp:Panel>
     &nbsp;
</asp:Content>
