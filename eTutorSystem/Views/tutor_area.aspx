<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Tutor.Master" AutoEventWireup="true" CodeBehind="tutor_area.aspx.cs" Inherits="eTutorSystem.Views.tutor_area" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
UoG Tutor Area
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="welcome" runat="server">
    <asp:Label ID="welcome_lbl" runat="server"></asp:Label>
    &nbsp;
    <asp:LinkButton ID="logout_lkbtn" runat="server" CssClass="logout_lbl" OnClick="logout_lkbtn_Click">Logout?</asp:LinkButton>
&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <h1>Dashboard</h1>
    <asp:Label ID="tuteesLbl" runat="server" CssClass="standardLabel"></asp:Label>
     <br />
<asp:LinkButton ID="selectLnkBtn" runat="server" CssClass="standardLinkButton" OnClick="selectLnkBtn_Click">Select-All</asp:LinkButton>
&nbsp;<asp:Label ID="Label4" runat="server" CssClass="standardLabel" Text="|"></asp:Label>
&nbsp;<asp:LinkButton ID="deselectLnkBtn" runat="server" CssClass="standardLinkButton" OnClick="deselectLnkBtn_Click">Deselect-All</asp:LinkButton>
<br />
<asp:LinkButton ID="messageLnkBtn" runat="server" CssClass="standardLinkButton" OnClick="messageLnkBtn_Click">SEND MESSAGE</asp:LinkButton>
&nbsp;<asp:Label ID="Label1" runat="server" CssClass="titleLabel" Text="|"></asp:Label>
&nbsp;<asp:LinkButton ID="meetingLnkBtn" runat="server" CssClass="standardLinkButton" OnClick="meetingLnkBtn_Click">ARRANGE MEETING</asp:LinkButton>
&nbsp;<asp:Label ID="Label2" runat="server" CssClass="titleLabel" Text="|"></asp:Label>
&nbsp;<asp:LinkButton ID="uploadsLnkBtn" runat="server" CssClass="standardLinkButton" OnClick="uploadsLnkBtn_Click">VIEW UPLOADS</asp:LinkButton>
&nbsp;<asp:Label ID="Label3" runat="server" CssClass="titleLabel" Text="|"></asp:Label>
&nbsp;<asp:LinkButton ID="blogLnkBtn" runat="server" CssClass="standardLinkButton" OnClick="blogLnkBtn_Click">VIEW BLOG</asp:LinkButton>
<br />
    <asp:Panel ID="searchLinkPanel" runat="server" style="margin-left:10%; text-align: left;" BackColor="#EEEEEE" BorderColor="#999999" BorderStyle="Solid" BorderWidth="2px" Width="80%">
         &nbsp;
         <asp:LinkButton ID="searchLinkButton" runat="server" CssClass="standardLinkButton" OnClick="searchLinkButtonClicked">Search Form - Hidden</asp:LinkButton>
    </asp:Panel>
    <asp:Panel ID="searchFormPanel" runat="server" style="margin-left:10%; border-top:none; text-align: center;" Width="80%" BackColor="#EEEEEE" BorderColor="#999999" BorderStyle="Dashed" BorderWidth="2px" Visible="false">
        <br />
        <asp:Label ID="searchLabel" runat="server" CssClass="standardLabel">Search</asp:Label><br />
        <br />
        <asp:Label ID="firstNameLabel" runat="server" CssClass="standardLabel">First Name: </asp:Label><asp:TextBox ID="firstNameTextbox" runat="server" TextMode="SingleLine" Placeholder="John"></asp:TextBox><br />
        <asp:Label ID="surnameLabel" runat="server" CssClass="standardLabel">Surname: </asp:Label><asp:TextBox ID="surnameTextbox" runat="server" TextMode="SingleLine" Placeholder="Doe"></asp:TextBox><br />    
        <asp:Label ID="programmeLabel" runat="server" CssClass="standardLabel">Programme: </asp:Label><asp:DropDownList ID="programmeDropdown" runat="server" /><br />
        <br />
        <asp:Label ID="orderLabel" runat="server" CssClass="standardLabel">Order By: </asp:Label><asp:DropDownList ID="orderDropdown" runat="server">
            <asp:ListItem Value="0" Text="" />
            <asp:ListItem Value="1" Text="First Name - Ascending" />
            <asp:ListItem Value="2" Text="First Name - Descending" />
            <asp:ListItem Value="3" Text="Surname - Ascending" />
            <asp:ListItem Value="4" Text="Surname - Descending" />
        </asp:DropDownList><br />
        <asp:Button ID="searchSubmit" runat="server" Text="Submit!" OnClick="submitSearch"/><asp:Button ID="resetButton" runat="server" Text="Reset View" OnClick="submitReset"/><br />
        <br />
    </asp:Panel>
    <br />
<asp:Label ID="errorLbl" runat="server" CssClass="errorLabel" Visible="False"></asp:Label>
&nbsp;<asp:Panel ID="tuteesPanel" runat="server">
        <br />
        <asp:Table ID="tuteesTbl" runat="server" CssClass="table" Font-Names="lato" Font-Size="14pt" ForeColor="#1F4F89">
        </asp:Table>
        <br />
    </asp:Panel>
    </asp:Content>