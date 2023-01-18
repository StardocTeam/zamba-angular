<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="500.aspx.cs" Inherits="Zamba.Web.Views.CustomErrorPages._500" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

   <img  style="margin-left:45%; width:200px; height:200px;" src="../../Content/Images/error400y500.png" />

    
    <h4 style="margin-left:49.5%;">Error 500 </h4>

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

                                        <h4>Se produjo un error al tratar de cargar su solicitud.         </h4>
                                    <h4>Causas probables.         </h4>
                                    <h4>Internal server error         </h4>
                                  
                                    <h4>Por favor pruebe solucionarlo actualizando la página actual.         </h4>
                                    <br />
                                    <hr style="margin-top:5%;" />
                                    <%--<a id="btnHome" href="../../">Volver a la pagina principal.</a>--%>
                                    <a id="btnHome" href="../../globalsearch/search/search.html">Volver a la pagina principal.</a>
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
