<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Student.Master" AutoEventWireup="true" CodeBehind="blog.aspx.cs" Inherits="eTutorSystem.Views.blog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    Uog Blog
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="welcome" runat="server">
    <asp:Label ID="welcome_lbl" runat="server"></asp:Label>
    &nbsp;
    <asp:LinkButton ID="logout_lkbtn" runat="server" CssClass="logout_lbl" OnClick="logout_lkbtn_Click">Logout?</asp:LinkButton>
&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <h1>Blog</h1>
    <br />
    <asp:Panel ID="selectTuteePanel" runat="server" style="margin-left:10%; text-align: center;" BackColor="#EEEEEE" BorderColor="#999999" BorderStyle="Solid" BorderWidth="2px" Width="80%" Visible="False">
        <asp:Label ID="Label2" runat="server" CssClass="standardLabel" Text="Select Tutee:"></asp:Label>
        <br />
        <asp:DropDownList ID="tuteeDdl" runat="server" Width="300px">
        </asp:DropDownList>
        <br />
        <asp:Button ID="selectTuteeBtn" runat="server" Text="Continue" OnClick="selectTuteeBtn_Click" />
     </asp:Panel>
    <asp:Panel ID="newPostLinkPanel" runat="server" style="margin-left:10%; text-align: left;" BackColor="#EEEEEE" BorderColor="#999999" BorderStyle="Solid" BorderWidth="2px" Width="80%">
         &nbsp;
         <asp:LinkButton ID="newPostLinkButton" runat="server" CssClass="standardLinkButton" OnClick="newPostLinkButtonClicked">New Post Form - Hidden</asp:LinkButton>
    </asp:Panel>
    <asp:Panel ID="newPostFormPanel" runat="server" style="margin-left:10%; border-top:none; text-align: center;" Width="80%" BackColor="#EEEEEE" BorderColor="#999999" BorderStyle="Dashed" BorderWidth="2px" Visible="false">
        <br />
            <asp:TextBox ID="newPostTextbox" runat="server" TextMode="MultiLine" Rows="5" MaxLength="300" Width="800px" Placeholder="Today, in regards to my project, I have achieved..."></asp:TextBox><br />
            <asp:Button ID="newPostSubmit" runat="server" Text="Submit!" OnClick="submitNewBlogPost"/><br />
        <br />
    </asp:Panel>
    <br />
    <asp:Panel ID="authorPanel" runat="server" />
    <br />
    <asp:Panel ID="blogHistoryLinkPanel" runat="server" style="margin-left:10%; text-align: left;" BackColor="#EEEEEE" BorderColor="#999999" BorderStyle="Solid" BorderWidth="2px" Width="80%">
         &nbsp;
         <asp:LinkButton ID="blogHistoryLink" runat="server" CssClass="standardLinkButton" OnClick="blogHistoryLinkClicked">Blog History - Visible</asp:LinkButton>
     </asp:Panel>
    <asp:Panel ID="blogHistoryPanel" runat="server" style="margin-left:10%; border-top:none; text-align: center;" Width="80%" BackColor="#EEEEEE" BorderColor="#999999" BorderStyle="Dashed" BorderWidth="2px">
        <br />
        <asp:Table id="blogHistoryTable" runat="server" CssClass="table" Font-Names="lato" Font-Size="14pt" ForeColor="#1F4F89">
            <asp:TableHeaderRow><asp:TableHeaderCell>Date</asp:TableHeaderCell><asp:TableHeaderCell>Time</asp:TableHeaderCell><asp:TableHeaderCell>Post</asp:TableHeaderCell></asp:TableHeaderRow>
        </asp:Table>
        <br />
    </asp:Panel>
    <br />
</asp:Content>
