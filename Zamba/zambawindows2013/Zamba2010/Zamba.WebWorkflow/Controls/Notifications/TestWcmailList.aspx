<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestWcmailList.aspx.cs" Inherits="Controls_Notifications_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="ZControls" TagName="maillist" Src="~/Controls/Notifications/WCMailList.ascx"  %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ZControls:maillist runat="server" id="mails"/>
    </div>
    </form>
</body>
</html>
