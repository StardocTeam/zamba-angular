<%@ Page Language="C#" AutoEventWireup="true" Inherits="Views_CustomErrorPages_Error" Codebehind="Error.aspx.cs" %>

<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html>
<head runat="server">
  <%--  <link rel="Stylesheet" type="text/css" href="Content/Styles/ZambaUIWeb.css" />
    <link rel="Stylesheet" type="text/css" href="Content/Styles/ZambaUIWebTables.css" />
    --%>

 <%--   <%: Scripts.Render("~/bundles/jqueryCore") %>
    <%: Scripts.Render("~/bundles/jqueryAddIns") %>
    <%: Scripts.Render("~/bundles/jqueryval") %>
    <%: Scripts.Render("~/bundles/bootstrap") %>
    <%: Scripts.Render("~/bundles/ZScripts") %>--%>

    <%--<script src="../../scripts/jq_datepicker.js" type="text/javascript"></script>
    <script src="../../scripts/Zamba.js" type="text/javascript"></script>
    <script src="../../scripts/Zamba.Validations.js" type="text/javascript"></script>--%>
            <script>
                function redirect() {
                    var queryString = localStorage.queryStringAuthorization;
                    if (queryString == "undefined")
                        queryString = "";
                    location.href = "../../globalsearch/search/search.html?" + queryString;
                }
        </script> 
    <script type="text/javascript">
        function pageLoad() {
        }

        //$(document).ready(function () {
        //    if (parent != this)
        //        parent.RedirectToErrorPage(document.location);
        //});
    </script>

    <title>Zamba Software</title>
</head>
<body>
   
    <img  style="margin-left:45%; width:200px; height:200px;" src="../../Content/Images/error400y500.png" />

    <h4 style="margin-left:50.5%;">Error</h4>

    <form id="form1" runat="server">

        <div align="center" style="margin-left:5%;">
            <div align="center">
                <div class="cabecera-wrapper">
                    <div class="cabecera-login">
                    </div>
                </div>

             
             
                  <br />

               
                <table style="width: 100%; text-decoration-color:aqua;">

                    <tr>
                        <td align="center">
                            <div id="MasterHeader">
                                <div style="color: #000000">

                      
                                    <h4>Ha ocurrido un error en la aplicacion, por favor intente nuevamente o contactese con el Administrador del sistema.</h4>
                                
                                    <hr style="margin-top:18%;" />
                                    <a id="btnHome" href="#" onclick="redirect();">Volver a la pagina principal.</a>
                                </div>
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
                                    Copyright©  <% =DateTime.Now.ToString("yyyy") %>  Stardoc Argentina - Todos los derechos reservados. <%--Resolución Optima 1024 x 768
                        <br />
                                    Se recomienda utilizar Internet Explorer 7.0 o superior para obtener mayor compatibilidad--%>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>


            </div>
    </form>





</body>
</html>
