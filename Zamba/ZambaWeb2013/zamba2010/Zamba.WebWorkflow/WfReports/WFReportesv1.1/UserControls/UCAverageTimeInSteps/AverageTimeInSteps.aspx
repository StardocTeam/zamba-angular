<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AverageTimeInSteps.aspx.cs" Inherits="UserControls_UCAverageTimeInSteps_AverageTimeInSteps" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/WfReports/WFReportesv1.1/UserControls/UCAverageTimeInSteps/UCAverageTimeInSteps.ascx"  
    TagPrefix="MyUcs" TagName="UCAverageTimeInSteps" %>
    
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body style="margin:0px 0px 0px 0px">
    <form id="form1" runat="server" style="margin:0px 0px 0px 0px">
    <div style="margin:0px 0px 0px 0px">
    <MyUcs:UCAverageTimeInSteps runat="server" ID="UCAverageTimeInSteps" />
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    </form>
</body>
</html>
