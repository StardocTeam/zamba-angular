<%@ Page language="c#" Inherits="testScan.WebForm1" CodeFile="Default.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<script language="javascript" type="text/javascript">
		function runApp()
		{
		var browser=navigator.appVersion;
		if (browser.indexOf("Windows NT 5.2")>-1)
		{
		runAppXP();
		}
		else if (browser.indexOf("Windows NT 5.1")>-1)
		{
		runAppXP();
		}
		else
		{
		window.open('file:///d:/TWAINDEMO/testscan/TZTwain.exe',1);
		}
		}
		
function runAppXP() 
{ 
var shell = new ActiveXObject("WScript.shell");
shell.run("d:\\TWAINDEMO\\testscan\\tztwain.exe", 1, true); 
}
</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr colspan="4">
					<INPUT onclick="runApp()" type="button" value="Scan">&nbsp;
					<asp:hyperlink id="HyperLink1" runat="server" NavigateUrl="TZTwain.exe">Download</asp:hyperlink>
					<!--<input type="button" name="button2" value="Wia" onClick="runWia()">--></tr>
				<tr>
					<td><asp:image id="Image1" runat="server" EnableViewState="False" Width="300px" Height="208px"
							ImageUrl="upload/j2.jpg"></asp:image></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
