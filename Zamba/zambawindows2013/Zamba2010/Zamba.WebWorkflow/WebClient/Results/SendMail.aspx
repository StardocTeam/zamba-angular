<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendMail.aspx.cs" Inherits="DocViewer"
    Title="Zamba Web Visualizador de Documentos" %>

<%@ Register Src="~/Controls/Notifications/WCSendMail.ascx" TagName="WCSendMail"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Envia Email</title>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:WCSendMail ID="WCSendMail" runat="server" />
    </form>
</body>
</html>
