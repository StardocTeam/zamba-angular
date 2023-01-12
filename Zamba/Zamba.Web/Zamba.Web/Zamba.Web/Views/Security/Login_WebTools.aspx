<%@ Page Language="C#" AutoEventWireup="true" Inherits="Views_Security_Login_WebTools" Codebehind="Login_WebTools.aspx.cs" %>

<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9; IE=8; IE=7; IE=EDGE,chrome=1" />

    <title>Zamba Administrative Tools</title>
    

     <%:Scripts.Render("~/bundles/jqueryCore") %>
 
    <%: Scripts.Render("~/bundles/jqueryAddIns") %>
    <%: Scripts.Render("~/bundles/bootstrap") %>

    <script type="text/javascript">

        $(document).ready(function () {
            $("#dvTools").css("display", "none");
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="ValidateUser_Div">
            <table>
                <tr>
                    <td><span style="color: white">Usuario: </span></td>
                    <td>
                        <asp:TextBox ID="userName" runat="server" Width="141px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><span style="color: white">Clave: </span></td>
                    <td>
                        <asp:TextBox TextMode="password" ID="UserPass" runat="server"></asp:TextBox></td>
                </tr>
            </table>
            <asp:Label ID="lblMsg" runat="server"></asp:Label><br />
            <br />
            <asp:Button Text="Aceptar" ID="btnconfirm" runat="server" OnClick="btnconfirm_Click" />
        </div>
        <br />
        <div id="dvTools">
            <a href="../../Views/Tools/ZambaAdmin.aspx" style="color: white">Zamba Query</a><br />
            <a href="../../Views/Tools/AdminTool.aspx" style="color: white">Admin Tool</a><br />
            <a href="../../Views/Tools/AdminCleaner.aspx" style="color: white">Admin Cleaner</a><br />
            <a href="../../Views/Tools/WSTestPage.aspx" style="color: white">Pruebas de WS Result</a>
        </div>
    </form>
</body>
</html>
