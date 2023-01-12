<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogIn.aspx.cs" Inherits="LogIn" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Zamba Software</title>
    
</head>
<body style="width: 20px">
    <form id="form1" runat="server">
        <table cellspacing="15" width="500">
            <tr>
                <td>
                    <asp:Label Text="Usuario:" runat="server" ID="lbUserName" CssClass="Label" />
                </td>
                <td>
                    <asp:TextBox ID="txtUserName" runat="server" CssClass="TextBox" />
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtUserName"
                        Display="Dynamic" ErrorMessage="* Falta completar este campo." runat="server" CssClass="RequiredFieldValidator"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbUserPass" runat="server" Text="Contraseña:" CssClass="Label" />
                </td>
                <td>
                    <asp:TextBox ID="txtUserPass" TextMode="Password" runat="server" CssClass="TextBox" />
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtUserPass"
                        ErrorMessage="* Falta completar este campo." runat="server"  CssClass="RequiredFieldValidator"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbRemindMe" runat="server" Text="Recordarme?" CssClass="Label"/>
                </td>
                <td>
                    <asp:CheckBox ID="Persist" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <asp:Button ID="Submit1" OnClick="Logon_Click" Text="Ingresar" runat="server" CssClass="Button" />
                </td>
            </tr>
            <asp:Label ID="Msg" ForeColor="red" Font-Size="Small" runat="server" CssClass="Label"/></table>
    </form>
</body>
</html>
