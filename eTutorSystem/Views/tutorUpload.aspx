<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Tutor.Master" AutoEventWireup="true" CodeBehind="tutorUpload.aspx.cs" Inherits="eTutorSystem.Views.tutorViewFiles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    UoG Tutor - Uploads
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="welcome" runat="server">
    <asp:Label ID="welcome_lbl" runat="server"></asp:Label>
    &nbsp;
    <asp:LinkButton ID="logout_lkbtn" runat="server" CssClass="logout_lbl" OnClick="logout_lkbtn_Click">Logout?</asp:LinkButton>
    &nbsp;
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="server">
    <h1>Tutee's uploaded files</h1>
    <asp:Panel ID="selectTuteePanel" runat="server" style="margin-left:10%; text-align: center;" BackColor="#EEEEEE" BorderColor="#999999" BorderStyle="Solid" BorderWidth="2px" Width="80%" Visible="False">
        <asp:Label ID="Label2" runat="server" CssClass="standardLabel" Text="Select Tutee:"></asp:Label>
        <br />
        <asp:DropDownList ID="tuteeDdl" runat="server" Width="300px">
        </asp:DropDownList>
        <br />
        <asp:Button ID="selectTuteeBtn" runat="server" Text="Continue" OnClick="selectTuteeBtn_Click" />
     </asp:Panel>
    <br />
    <asp:Panel ID="commentPanel" runat="server" style="margin-left:10%; text-align: center;" BackColor="#EEEEEE" BorderColor="#999999" BorderStyle="Solid" BorderWidth="2px" Width="80%" Visible="False">
        <asp:Label ID="Label1" runat="server" CssClass="standardLabel" Text="Your Comment"></asp:Label>
        <br />
        <asp:TextBox ID="commentTxtBx" style="max-width:400px; min-width:400px" runat="server" MaxLength="1000" Rows="5" TextMode="MultiLine" Width="400px" placeholder="Comment"></asp:TextBox>
        <br />
        <asp:Button ID="submitCommentBtn" runat="server" Text="Submit" OnClick="submitCommentBtn_Click" />
        <br />
        <asp:Label ID="errorLbl" runat="server" CssClass="errorLabel" Visible="False"></asp:Label>
     </asp:Panel>
    <br />
     <asp:Panel ID="uploadsHeaderPanel" runat="server" style="margin-left:10%; text-align: left;" BackColor="#EEEEEE" BorderColor="#999999" BorderStyle="Solid" BorderWidth="2px" Width="80%">
         &nbsp;
         <asp:LinkButton ID="viewUploadsLnkBtn" runat="server" CssClass="standardLinkButton" OnClick="uploadsLnkBtn_Click">Uploads - Visible</asp:LinkButton>
     </asp:Panel>
    <asp:Panel ID="viewUploadsDisplayPanel" runat="server" style="margin-left:10%; border-top:none; text-align: center;" Width="80%" BackColor="#EEEEEE" BorderColor="#999999" BorderStyle="Dashed" BorderWidth="2px">
    <br />
    <asp:Table ID="uploadsTbl" runat="server" CssClass="table" Font-Names="lato" Font-Size="14pt" ForeColor="#1F4F89">
    </asp:Table>
    <asp:Label ID="noUploadsLbl" runat="server" Visible="False"></asp:Label>
    <br />
    </asp:Panel>
    <br />
    </asp:Content>
