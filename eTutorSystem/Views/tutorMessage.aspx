<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Tutor.Master" AutoEventWireup="true" CodeBehind="tutorMessage.aspx.cs" Inherits="eTutorSystem.Views.tutorMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     UoG Tutor - Message</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="welcome" runat="server">
     <asp:Label ID="welcome_lbl" runat="server"></asp:Label>
    &nbsp;
    <asp:LinkButton ID="logout_lkbtn" runat="server" CssClass="logout_lbl" OnClick="logout_lkbtn_Click">Logout?</asp:LinkButton>
    &nbsp;
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="server">
    <h1>Communication with your tutee</h1>
    <asp:Panel ID="selectTuteePanel" runat="server" style="margin-left:10%; text-align: center;" BackColor="#EEEEEE" BorderColor="#999999" BorderStyle="Solid" BorderWidth="2px" Width="80%" Visible="False">
        <asp:Label ID="Label2" runat="server" CssClass="standardLabel" Text="Select Tutee:"></asp:Label>
        <br />
        <asp:DropDownList ID="tuteeDdl" runat="server" Width="300px">
        </asp:DropDownList>
        <br />
        <asp:Button ID="selectTuteeBtn" runat="server" Text="Continue" OnClick="selectTuteeBtn_Click" />
        
     </asp:Panel>
    <br />
    <asp:Panel ID="messageHeaderPanel" runat="server" style="margin-left:10%; text-align: left;" BackColor="#EEEEEE" BorderColor="#999999" BorderStyle="Solid" BorderWidth="2px" Width="80%">
         &nbsp;<asp:LinkButton ID="newMessageLnkLbl" runat="server" OnClick="newMessageLnkLbl_Click" style="text-align: left" CssClass="standardLinkButton">New Message - Hidden</asp:LinkButton>
     </asp:Panel>
     <asp:Panel ID="messagePanel" runat="server" style="margin-left:10%; border-top:none; text-align: center; overflow-x:hidden;" BackColor="#EEEEEE"  BorderColor="#999999" BorderStyle="Dashed" BorderWidth="2px" Width="80%" Visible="False">
         <br />
         <asp:Label ID="Label1" runat="server" CssClass="standardLabel" Text="Your Message:"></asp:Label>
         <br />
         <asp:TextBox ID="subjectTxtBx" runat="server" Width="400px" placeholder="Subject"></asp:TextBox>
         <br />
         <br />
         <asp:TextBox ID="messageTxtBx" style="max-width:400px; min-width:400px" runat="server" MaxLength="1000" Rows="5" TextMode="MultiLine" Width="400px" placeholder="Message"></asp:TextBox>
         <br />
         <asp:Button ID="submitMessageBtn" runat="server" Text="Submit" OnClick="submitMessageBtn_Click" />
         <br />
         <asp:Label ID="errorLbl" runat="server" CssClass="errorLabel" Visible="False"></asp:Label>
         <br />
     </asp:Panel>
    &nbsp;
     <asp:Panel ID="displayHeaderPanel" runat="server" style="margin-left:10%; text-align: left;" BackColor="#EEEEEE" BorderColor="#999999" BorderStyle="Solid" BorderWidth="2px" Width="80%">
         &nbsp;
         <asp:LinkButton ID="messagesLnkBtn" runat="server" CssClass="standardLinkButton" OnClick="messagesLnkBtn_Click">Messages - Visible</asp:LinkButton>
     </asp:Panel>
<asp:Panel ID="messageDisplayPanel" runat="server" style="margin-left:10%; border-top:none; text-align: center;" Width="80%" BackColor="#EEEEEE" BorderColor="#999999" BorderStyle="Dashed" BorderWidth="2px">
     <br />
    <asp:Table ID="messageTbl" runat="server" CssClass="table" Font-Names="lato" Font-Size="14pt" ForeColor="#1F4F89">
    </asp:Table>
     <asp:Label ID="noMessagesLbl" runat="server" Visible="False"></asp:Label>
    <br />
</asp:Panel>
     &nbsp;
    
</asp:Content>
