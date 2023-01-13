<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogOut.aspx.cs" Inherits="WebLogin.LogOutMessagePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lblLogOut" runat="server" Text="Se ha desconectado correctamente"></asp:Label>
    
        <br />
        <br />
        <asp:Button ID="btnLogIn" runat="server" onclick="btnLogIn_Click" Text="Conectarse" 
            Width="100px" />
    
    </div>
    </form>
</body>
</html>
