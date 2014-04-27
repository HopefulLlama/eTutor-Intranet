<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="admin_area.aspx.cs" Inherits="eTutorSystem.Views.admin_area" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    UoG Admin Dashboard
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="welcome" runat="server">
    <asp:Label ID="welcome_lbl" runat="server"></asp:Label>
    &nbsp;
    <asp:LinkButton ID="logout_lkbtn" runat="server" CssClass="logout_lbl" OnClick="logout_lkbtn_Click">Logout?</asp:LinkButton>
&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <h1>Admin Area - Allocation</h1>
    &nbsp;&nbsp;&nbsp;<asp:Label ID="studentsLbl" runat="server" CssClass="standardLabel"></asp:Label>
     <br />
    <p style="text-align: center">
                <asp:Label ID="Label1" runat="server" CssClass="standardLabel" Text="Tutor List "></asp:Label>
         <br />
    <asp:DropDownList ID="tutorDdl" runat="server" Width="300px">
        </asp:DropDownList>
        <br />
        <asp:Button ID="selectTutorBtn" runat="server" Text="Submit" OnClick="selectTutorBtn_Click" /><br />
        <br />
        <asp:Label runat="server" CssClass="standardLabel">Colour code</asp:Label>
        <asp:Table runat="server" CssClass="table" Font-Names="lato" Font-Size="14pt" ForeColor="#1F4F89" GridLines="Both">
            <asp:TableRow>
                <asp:TableCell BackColor="OrangeRed">Student w/o Tutor</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell BackColor="Salmon">Student w/ more than 28 days of no interaction</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell BackColor="Wheat">Student w/ more than 7 days of no interaction</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        </p>
    <asp:Label ID="errorLbl" runat="server" CssClass="errorLabel" Visible="False"></asp:Label>
&nbsp;<asp:Panel ID="studentsPanel" runat="server">
        <br />
        <asp:Table ID="studentsTbl" runat="server" CssClass="table" Font-Names="lato" Font-Size="14pt" ForeColor="#1F4F89">
        </asp:Table>
        <br />
    </asp:Panel>
</asp:Content>
