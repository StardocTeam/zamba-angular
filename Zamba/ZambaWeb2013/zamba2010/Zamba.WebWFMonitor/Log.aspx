<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Log.aspx.vb" Inherits="Log" %>

<%@ Register Src="~/UserControls/Login.ascx" TagName="Login" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Log de Usuario</title>
    <link rel="Stylesheet" type="text/css" href="App_Themes/Standard/css-content.css" />
</head>
<body>
    <div class="centrar">
        <form id="form1" runat="server">
            <uc1:Login ID="Login1" runat="server" />
        </form>
    </div>
</body>
</html>
