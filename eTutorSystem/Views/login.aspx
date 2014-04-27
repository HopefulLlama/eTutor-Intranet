<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Login.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="eTutorSystem.Views.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    Uog eTutor System
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <h1>Sign In</h1>
    <br />
        <div class="login_top">
            &nbsp;<asp:TextBox ID="username_txtbx" runat="server" Style="text-align: justify" Width="200px" placeholder="Username"></asp:TextBox>
            &nbsp;<br />
            <asp:Label ID="username_error_lbl" runat="server" CssClass="errorLabel"></asp:Label>
        </div>
        <br />
        <div class="login_middle">
            <asp:TextBox ID="password_txtbx" runat="server" Width="200px" TextMode="Password" placeholder="Password"></asp:TextBox>
            &nbsp;<br />
            <asp:Label ID="password_error_lbl" runat="server" CssClass="errorLabel"></asp:Label>
            &nbsp;<br />
                    <asp:Label ID="statusLbl" runat="server" CssClass="errorLabel"></asp:Label>
                    </div>
        <br />
        <div class="login_bottom">
            <asp:Button ID="login_btn" runat="server" Style="text-align: center" Text="Sign in" Width="200px" OnClick="login_btn_Click" />

        </div>
    
    <br />
    <br />
</asp:Content>
