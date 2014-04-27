<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Tutor.Master" AutoEventWireup="true" CodeBehind="tutorMeeting.aspx.cs" Inherits="eTutorSystem.tutorMeeting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    UoG Tutor - Meeting
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="welcome" runat="server">
    <style type="text/css">
        .calenderStructure {
            display: inline-block;
            border-width: 0px;
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
<br />
    <br />
    <asp:Panel ID="TuteesSelectionPanel" runat="server" Style="margin-left: 10%; text-align: center;" BackColor="#EEEEEE" BorderColor="#999999" BorderWidth="2px" Width="80%" Visible="False">
        <asp:Label ID="Label2" runat="server" CssClass="standardLabel" Text="Select Tutee:"></asp:Label>
        <br />
        <asp:DropDownList ID="tuteeDdl" runat="server" Width="300px">
        </asp:DropDownList>
        <br />
        <asp:Button ID="selectTuteeBtn" runat="server" Text="Continue" OnClick="selectTuteeBtn_Click" CausesValidation="False" />

    </asp:Panel>
    &nbsp;<br />
    <asp:Panel ID="appointmentPanel" runat="server" Style="margin-left: 10%; border-top: none; text-align: center; overflow-x: hidden;" BackColor="#EEEEEE" BorderColor="#999999" BorderStyle="Dashed" BorderWidth="2px" Width="80%" Visible="False">
        <br />
        <asp:Label ID="Label5" runat="server" CssClass="standardLabel" Text="Select Meeting Type:"></asp:Label>
        <br />
        <br />
        <asp:DropDownList ID="DropDownList2" runat="server" Style="margin-left: 0px" Width="151px">
            <asp:ListItem>Physical</asp:ListItem>
            <asp:ListItem Value="Virtual">Virtual</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" CssClass="standardLabel" Text="Please Select Date :"></asp:Label>
        <br />
        <br />
        <br />
        <asp:Calendar CssClass="calenderStructure" ID="Calendar1" runat="server" DayNameFormat="Full">
            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt" />
            <DayStyle Width="14%" />
            <NextPrevStyle Font-Size="8pt" ForeColor="White" />
            <OtherMonthDayStyle ForeColor="#999999" Width="14%" Font-Italic="False" />
            <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
            <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
            <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" />
            <TodayDayStyle BackColor="#CCCC99" />
        </asp:Calendar>

        <br />
        <br />
        <asp:Label ID="Label3" runat="server" CssClass="standardLabel" Text="Please Select Time :"></asp:Label>
        <br />
        <asp:DropDownList ID="DropDownList1" runat="server" Style="margin-left: 0px" Width="88px">
        </asp:DropDownList>
        &nbsp;&nbsp;<asp:Label ID="Label6" runat="server" Font-Bold="True" Text=" : "></asp:Label>
&nbsp;
         <asp:DropDownList ID="DropDownList3" runat="server" Width="83px">
         </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" CssClass="standardLabel" Text="Please Select Location :"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Send Request" Width="168px" OnClick="Button1_Click" />
        <br />
        <br />
    </asp:Panel>
    <br />
    <asp:Panel ID="meetingHistoryPanel" runat="server" style="margin-left:10%; border-top:none; text-align: center;" Width="80%" BackColor="#EEEEEE" BorderColor="#999999" BorderStyle="Dashed" BorderWidth="2px">
        <br />
        <asp:Table id="meetingHistoryTable" runat="server"  CssClass="table" Font-Names="lato" Font-Size="14pt" ForeColor="#1F4F89">
            <asp:TableHeaderRow><asp:TableHeaderCell>MeetingID</asp:TableHeaderCell><asp:TableHeaderCell>Tutee</asp:TableHeaderCell><asp:TableHeaderCell>Date</asp:TableHeaderCell><asp:TableHeaderCell>Time</asp:TableHeaderCell><asp:TableHeaderCell>Type</asp:TableHeaderCell><asp:TableHeaderCell>Location</asp:TableHeaderCell><asp:TableHeaderCell>Student Status</asp:TableHeaderCell><asp:TableHeaderCell>Tutor Status</asp:TableHeaderCell></asp:TableHeaderRow>
        </asp:Table>
        <br />
    </asp:Panel>
</asp:Content>
