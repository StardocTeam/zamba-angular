<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AsignedTasksCount.aspx.cs" Inherits="UserControls_UCAsignedTasksCount_AsignedTasksCount" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%--<%@ Register Src="~/UserControls/UCAsignedTasksCount/UCAsignedTasksCount.ascx" TagPrefix="MyUcs"--%>
<%@ Register Src="~/WfReports/UserControls/UCAsignedTasksCount/UCAsignedTasksCount.ascx" TagPrefix="MyUcs"
    TagName="UCAsignedTasksCount" %>
    
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body style="margin:0px 0px 0px 0px">
    <form id="form1" runat="server" style="margin:0px 0px 0px 0px">
    <div style="margin:0px 0px 0px 0px">
    <MyUcs:UCAsignedTasksCount runat="server" ID="UCAsignedTasksCount" />
    </div>
    </form>
</body>
</html>
