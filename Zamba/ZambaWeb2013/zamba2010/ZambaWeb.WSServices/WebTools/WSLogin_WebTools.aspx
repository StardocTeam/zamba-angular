<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login_WebTools.aspx.cs" Inherits="ZambaWeb.WSServices.WebTools.Login_WebTools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Zamba Administrative Tools</title>
    <script src="../../scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../../scripts/jquery-ui-1.8.6.min.js" type="text/javascript"></script>
    <script src="../../scripts/jquery.layout-latest.js" type="text/javascript"></script>
    <script type="text/javascript">
      
         $(document).ready(function() {
            $("#dvTools").css("display", "none");
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
      <div id="ValidateUser_Div">
        <table>
            <tr>            
                <td><span>Usuario: </span></td>
                <td><asp:TextBox ID="userName" runat="server" Width="141px"></asp:TextBox></td>
            </tr>
            <tr>
                <td><span>Clave: </span></td>
                <td><asp:TextBox TextMode="password" ID="UserPass" runat="server"></asp:TextBox></td>
            </tr>
        </table>
        <asp:label id="lblMsg" runat="server"></asp:label><br /><br />
        <asp:Button Text="Aceptar" id="btnconfirm" runat="server" onclick="btnconfirm_Click" />
    </div>
    <br />
    <div id="dvTools">
        <a href="../../WebTools/WSZambaAdmin.aspx">Zamba Query</a><br />        
        <a href="../../WebTools/WSAdminTool.aspx">Admin Tool</a>
    </div>
    </form>
</body>
</html>
