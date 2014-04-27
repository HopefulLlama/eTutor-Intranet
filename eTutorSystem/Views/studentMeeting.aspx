<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Student.Master" AutoEventWireup="true" CodeBehind="studentMeeting.aspx.cs" Inherits="eTutorSystem.Views.studentMeeting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    UoG Student - Meeting
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="welcome" runat="server">
    
    <style type="text/css">
        .calenderStructure {
        display: inline-block;
border-width: 0px;
border-style: solid;
border-color: black;
        }
    </style>
    <asp:Label ID="welcome_lbl" runat="server"></asp:Label>
    &nbsp;
    <asp:LinkButton ID="logout_lkbtn" runat="server" CssClass="logout_lbl" OnClick="logout_lkbtn_Click">Logout?</asp:LinkButton>
&nbsp;
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="server">
   <h1>Meetings</h1>


        <asp:Panel ID="appointmentPanel" runat="server" Style="margin-left: 10%; border-top: none; text-align: center; overflow-x: hidden;" BackColor="#EEEEEE" BorderColor="#999999" BorderStyle="Dashed" BorderWidth="2px" Width="80%" Visible="False">
             <p>
    <br />
    <asp:Label ID="Label1" runat="server" Text="Please Select a Meeting Type"></asp:Label>
&nbsp;</p>
    <p>
        <asp:DropDownList ID="TypeDDL" runat="server" style="margin-left: 0px" Width="146px">
            <asp:ListItem>Physical</asp:ListItem>
            <asp:ListItem>Virtual</asp:ListItem>
        </asp:DropDownList>
    </p>
    <p>
        <asp:Label ID="Label2" runat="server" Text="Please Select Date"></asp:Label>
    </p>
    
        <asp:Calendar CssClass="calenderStructure" ID="Calendar" runat="server" Width="161px">
            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt" />
            <DayStyle Width="14%" />
            <NextPrevStyle Font-Size="8pt" ForeColor="White" />
            <OtherMonthDayStyle ForeColor="#999999" Width="14%" />
            <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
            <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
            <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" />
            <TodayDayStyle BackColor="#CCCC99" />
        </asp:Calendar>
    
    <p>
        <asp:Label ID="Label3" runat="server" Text="Time"></asp:Label>
</p>
<p>
        <asp:DropDownList ID="HourDDL" runat="server">
        </asp:DropDownList>
&nbsp;<asp:Label ID="Label5" runat="server" Text=" : "></asp:Label>
&nbsp;<asp:DropDownList ID="MinuteDDL" runat="server">
        </asp:DropDownList>
</p>
<p>
        <asp:Label ID="Label4" runat="server" Text="Enter Meeting Location"></asp:Label>
</p>
<p>
        <asp:TextBox ID="LocationTB" runat="server" Width="137px"></asp:TextBox>
</p>
             <p>

             </p>
<p>
        <asp:Button ID="SendMailBttn" runat="server" Text="Send Meeting Request" OnClick="SendMailBttn_Click" />
</p>
<br />
    </asp:Panel>
    <br />
        <asp:Panel ID="meetingHistoryPanel" runat="server" style="margin-left:10%; border-top:none; text-align: center;" Width="80%" BackColor="#EEEEEE" BorderColor="#999999" BorderStyle="Dashed" BorderWidth="2px">
        <br />
        <asp:Table id="meetingHistoryTable" runat="server"  CssClass="table" Font-Names="lato" Font-Size="14pt" ForeColor="#1F4F89">
            <asp:TableHeaderRow><asp:TableHeaderCell>MeetingID</asp:TableHeaderCell><asp:TableHeaderCell>Tutor</asp:TableHeaderCell><asp:TableHeaderCell>Date</asp:TableHeaderCell><asp:TableHeaderCell>Time</asp:TableHeaderCell><asp:TableHeaderCell>Type</asp:TableHeaderCell><asp:TableHeaderCell>Location</asp:TableHeaderCell><asp:TableHeaderCell>Student Status</asp:TableHeaderCell><asp:TableHeaderCell>Tutor Status</asp:TableHeaderCell></asp:TableHeaderRow>
        </asp:Table>
        <br />
    </asp:Panel>
</asp:Content>
