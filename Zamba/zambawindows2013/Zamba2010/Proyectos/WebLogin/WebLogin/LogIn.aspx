<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="WebLogin._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .style1
        {
            width: 87px;
        }
        .style2
        {
            width: 151px;
        }
        .style4
        {
            width: 250px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <table>
    <tr>
    <td class="style1">
    
                                        <asp:Label ID="UserNameLabel" runat="server" 
            AssociatedControlID="txtUserName">Usuario:</asp:Label>
    
    </td>
    <td class="style2">
    
                                        <asp:TextBox ID="txtUserName" runat="server" 
            Font-Size="0.8em" MaxLength="15" Width="150px"></asp:TextBox >
                                        </td>
                                        <td>
                                        *
                                        </td>
    </tr>
    <tr>
    <td class="style1">
    
                                        <asp:Label ID="PasswordLabel" runat="server" 
            AssociatedControlID="txtPassword">Contraseña:</asp:Label>
    
    </td>
    <td class="style2">
    
                                        <asp:TextBox ID="txtPassword" runat="server" 
            Font-Size="0.8em" TextMode="Password" Width="150px"></asp:TextBox >
                                        </td>
                                                                                <td>
                                        *
                                        </td>
    </tr>
    </table>
    
    <table>
    <tr>
    <td class="style4">
        <br />
        <asp:Button ID="btnLogIn" runat="server" onclick="btnLogIn_Click" Text="Conectarse" 
            Width="115px" />
    
    &nbsp;
    
    &nbsp;<asp:Button ID="btnLogOut" runat="server" onclick="btnLogOut_Click" 
            Text="Desconectarse" Width="115px" />
    </td>
    </tr>
    </table>
    <table>
        <tr>
    <td class="style4">
                                        <asp:Label ID="lblMessages" runat="server"></asp:Label>
    </td>
    </tr>
    </table>                                                    
    </form>
</body>
</html>
