<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Student.Master" AutoEventWireup="true" CodeBehind="student_area.aspx.cs" Inherits="eTutorSystem.Views.student_area" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    Uog Student Area
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="welcome" runat="server">
    <asp:Label ID="welcome_lbl" runat="server"></asp:Label>
    &nbsp;
    <asp:LinkButton ID="logout_lkbtn" runat="server" CssClass="logout_lbl" OnClick="logout_lkbtn_Click">Logout?</asp:LinkButton>
&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <h1>Dashboard</h1>
    <asp:Label ID="tutorLbl" runat="server" CssClass="standardLabel"></asp:Label>
    <br />
    <asp:Label ID="tutorEmailLbl" runat="server" CssClass="standardLabel"></asp:Label>
    <br />
    <asp:Panel ID="infoPanel" runat="server">
        <br />
        <asp:Table ID="infoTbl" runat="server" CssClass="table" Font-Names="lato" Font-Size="14pt" ForeColor="#1F4F89">
        </asp:Table>
        <br />
    </asp:Panel>
    </asp:Content>
