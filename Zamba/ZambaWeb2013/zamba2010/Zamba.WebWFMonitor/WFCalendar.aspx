<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WFCalendar.aspx.vb" Inherits="_Default" %>
<%--<%@ Register TagPrefix="daypilot" Namespace="DayPilot.Web.Ui" Assembly="DayPilot" %>--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DayPilot Calendar Demo</title>
		<style>
H1 { FONT-FAMILY: Tahoma }
H2 { FONT-FAMILY: Tahoma }
P { FONT-SIZE: 10pt; FONT-FAMILY: Tahoma }
LI { FONT-SIZE: 10pt; FONT-FAMILY: Tahoma }
</style>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table cellspacing="0" cellpadding="0" border="0" style="width: 720px">
				<tr>
					<td valign="top" style="width: 227px">
						<P>
							<asp:Calendar id="Calendar1" runat="server" CellPadding="4" BorderColor="#999999" Font-Names="Verdana"
								Font-Size="8pt" Height="180px" ForeColor="Black" DayNameFormat="FirstLetter" BackColor="White"
								Width="200px" >
								<TodayDayStyle ForeColor="Black" BackColor="#CCCCCC"></TodayDayStyle>
								<SelectorStyle BackColor="#CCCCCC"></SelectorStyle>
								<NextPrevStyle VerticalAlign="Bottom"></NextPrevStyle>
								<DayHeaderStyle Font-Size="7pt" Font-Bold="True" BackColor="#CCCCCC"></DayHeaderStyle>
								<SelectedDayStyle Font-Bold="True" ForeColor="White" BackColor="#666666"></SelectedDayStyle>
								<TitleStyle Font-Bold="True" BorderColor="Black" BackColor="#999999"></TitleStyle>
								<WeekendDayStyle BackColor="#FFFFCC"></WeekendDayStyle>
								<OtherMonthDayStyle ForeColor="#808080"></OtherMonthDayStyle>
							</asp:Calendar></P>
						<P>
                            &nbsp;</P>
						
					</td>
                    <td style="width: 350px" valign="top">
                    </td>
                    <td style="width: 48px" valign="top">
                    </td>
					<td valign="top" style="width: 489px">
						<P>
<%--							<DayPilot:DayPilotCalendar id=DayPilotCalendar1 runat="server" Width="500" NonBusinessHours="AlwaysVisible" JavaScriptEventAction="alert('Event Id: {0}');" PkColumnName="id" NameColumnName="name" EndColumnName="end" BeginColumnName="start" DataSource="<%# getData %>" HourHeight="30">
							</DayPilot:DayPilotCalendar></P>
--%>					</td>
				</tr>
			</table>
			<P>&nbsp;</P>
		</form>
	</body>
</HTML>