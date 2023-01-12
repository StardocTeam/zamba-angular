<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Views_CustomErrorPages_Error" %>

<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html>
<head runat="server">

    <link rel="Stylesheet" type="text/css" href="Content/Styles/ZambaUIWeb.css" />
    <link rel="Stylesheet" type="text/css" href="Content/Styles/ZambaUIWebTables.css" />

    <%: Scripts.Render("~/bundles/jqueryCore") %>
    <%: Scripts.Render("~/bundles/jqueryAddIns") %>
    <%: Scripts.Render("~/bundles/jqueryval") %>
    <%: Scripts.Render("~/bundles/bootstrap") %>
    <script src="../../scripts/jq_datepicker.js" type="text/javascript"></script>
    <script src="../../scripts/Zamba.js" type="text/javascript"></script>
    <script src="../../scripts/Zamba.Validations.js" type="text/javascript"></script>

    <script type="text/javascript">
        function pageLoad() {
        }

        $(document).ready(function () {
            if (parent != this)
                parent.RedirectToErrorPage(document.location);
        });
    </script>

    <title>Untitled Page</title>

</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <div class="cabecera-wrapper">
                <div class="cabecera-login">
                </div>
            </div>

            <br />
            <br />
            <table>
                <tr>
                    <td align="center">
                        <div style="color: white">
                            <h4>Ha ocurrido un error en la aplicacion, por favor intente nuevamente o contactese con el Administrador del sistema.         </h4>

                            <a id="btnHome" href="../../">Volver a la pagina principal.       </a>


                        </div>
                    </td>
                </tr>
                <tr style="height: 40%">
                    <td>
                        <div style="padding-top: 20px; padding-right: 40px; padding-bottom: 30px; padding-left: 30px">
                            <div id="divInicioSesion">
                                &nbsp;
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="FooterLogIn" style="height: 20pt;">
                            <div>
                                Copyright© 2010 Stardoc Argentina - Todos los derechos reservados. Resolución Optima 1024 x 768
                        <br />
                                Se recomienda utilizar Internet Explorer 7.0 o superior para obtener mayor compatibilidad
                            </div>
                        </div>
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
