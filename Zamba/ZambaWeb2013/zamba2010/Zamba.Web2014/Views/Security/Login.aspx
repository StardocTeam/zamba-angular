<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" EnableEventValidation="false" %>

<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9; IE=8; IE=7; IE=EDGE,chrome=1" />
    <meta charset="UTF-8" />
    <title title="Zamba Web"></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <link rel="apple-touch-icon-precomposed" href="img/icon.png" />
    <link rel="apple-touch-icon" href="img/icon.png" />
    <link rel="apple-touch-startup-image" href="img/splash.png" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <link rel="apple-touch-icon" sizes="57x57" href="apple-icon-57x57.png" />
    <link rel="apple-touch-icon" sizes="72x72" href="apple-icon-72x72.png" />
    <link rel="apple-touch-icon" sizes="114x114" href="apple-icon-114x114.png" />
    <link rel="apple-touch-icon" sizes="144x144" href="apple-icon-144x144.png" />
    <link rel="apple-touch-icon-precomposed" sizes="57x57" href="apple-icon-57x57-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="apple-icon-72x72-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="apple-icon-114x114-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="apple-icon-144x144-precomposed.png" />

    <!-- Startup splash screen images -->
    <!-- iPhone 320x460 -->
    <link href="<?php echo get_stylesheet_directory_uri() . '/images/startup-iphone.png'; ?>" media="(device-width: 320px) and (device-height: 480px) and (-webkit-device-pixel-ratio: 1)" rel="apple-touch-startup-image">
    <!-- iPhone Retina 640x920 -->
    <link rel="apple-touch-startup-image" href="<?php echo get_stylesheet_directory_uri() . '/images/startup-iphone-retina.png'; ?>" media="(device-width: 320px) and (device-height: 480px) and (-webkit-device-pixel-ratio: 2)">
    <!-- iPhone 5 640x1096 -->
    <link rel="apple-touch-startup-image" href="<?php echo get_stylesheet_directory_uri() . '/images/startup-iphone5.png'; ?>" media="(device-width: 320px) and (device-height: 568px) and (-webkit-device-pixel-ratio: 2)">
    <!-- iPad Portrait 768x1004 -->
    <link href="<?php echo get_stylesheet_directory_uri() . '/images/startup-ipad-portrait.png'; ?>" media="(device-width: 768px) and (device-height: 1024px) and (orientation: portrait) and (-webkit-device-pixel-ratio: 1)" rel="apple-touch-startup-image">
    <!-- iPad Landscape 748x1024 -->
    <link href="<?php echo get_stylesheet_directory_uri() . '/images/startup-ipad-landscape.png'; ?>" media="(device-width: 768px) and (device-height: 1024px) and (orientation: landscape) and (-webkit-device-pixel-ratio: 1)" rel="apple-touch-startup-image">
    <!-- iPad Retina Portrait 1536x2008 -->
    <link rel="apple-touch-startup-image" href="<?php echo get_stylesheet_directory_uri() . '/images/startup-ipad-retina-portrait.png'; ?>" media="(device-width: 768px) and (device-height: 1024px) and (orientation: portrait) and (-webkit-device-pixel-ratio: 2)">
    <!-- iPad Retina Landscape 1496x2048 -->
    <link rel="apple-touch-startup-image" href="<?php echo get_stylesheet_directory_uri() . '/images/startup-ipad-retina-landscape.png'; ?>" media="(device-width: 768px) and (device-height: 1024px) and (orientation: landscape) and (-webkit-device-pixel-ratio: 2)">

    <link id="lnkWebIcon" rel="shortcut icon" runat="server" type="image/x-icon" />
    <link rel="Stylesheet" type="text/css" href="../../Content/Styles/ZambaUIWeb.css" />

    <asp:PlaceHolder runat="server">
        <%: Styles.Render("~/bundles/Styles/jquery")%>
        <%: Styles.Render("~/Content/css")%>
        <%: Styles.Render("~/bundles/Styles/bootstrap")%>
        <%: Styles.Render("~/Content/bootstrap-responsive.css")%>
        <%: GetJqueryCoreScript()%>
        <%: Scripts.Render( "~/bundles/modernizr") %>
        <%: Scripts.Render("~/bundles/bootstrap") %>
    </asp:PlaceHolder>
</head>

<body id="loginBody" runat="server">
    <form id="form2" runat="server" role="form" defaultbutton="btnLogin">
        <asp:HiddenField ID="hdnUserId" runat="server" />
        <asp:HiddenField ID="hdnConnectionId" runat="server" />
        <asp:HiddenField ID="hdnComputer" runat="server" />

          <div class="container-fluid">
            <div id="MasterHeader" class="navbar navbar-static-top">
                <div class="container clientPhrase" id="header-Cabezera-Master">
                    <%--<button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navHeaderCollapse">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>--%>

                     <a class="navbar-brand" id="tblCabecera">
                        <span class="clientlogologin" style="border: none;"></span>
                    </a>

                    <div class="slogan"></div>
                    <div class="collapse navbar-collapse navHeaderCollapse ">
                        <ul class="nav navbar-nav navbar-right">
                            <li>
                                <div id="dynamicButtonContainer" runat="server" style=""></div>
                            </li>
                            <li>
                                <% if (Page.Theme.ToLower() == "aysadal")
                                   { %>
                                <script type="text/javascript">
                                    function ShowTabProductCatalog() {
                                        WindowRef = window.open("../Aysa/DAL/ProductCatalog.aspx");
                                        if (WindowRef != null) {
                                            WindowRef.opener = self;
                                        }
                                        WindowRef.focus();
                                    }
                                </script>
                                <input id="btnProductCatalogLogin" type="button" value="CATALOGO DE PRODUCTO" onclick="ShowTabProductCatalog();" />
                                <% } %>
                            </li>
                            <li></li>
                        </ul>
                    </div>
                </div>
            </div>

        <%-- Imagen de Encabezado --%>
        <div class="row bannerHeader">
            <div class="hidden-sm hidden-xs col-md-offset-0 col-md-10 cabecera-login "></div>
            <div class="col-xs-offset-0 col-xs-12 col-sm-12 col-md-2 cabecera-login2"></div>
        </div>


        <div id="divPwbyzambaCenter" class="row text-center">
            <img class="pwbyzambaCenter" src="../../Content/Images/pwbyzamba.png" alt="" />
        </div>

        <div class="row">
            <div id="controlMensaje" class="controlMensaje">
                <asp:Label ID="lblMensajeLogin" Font-Bold="false" Visible="true" runat="server" Font-Size="Small" Text="" />
            </div>
        </div>
                    </div>

        <%--Form login --%>
        <div id="pnlZambaLogin" runat="server">
            <div class="row">
                <div class="col-xs-offset-2 col-xs-8 col-sm-offset-4 col-sm-4 col-md-offset-8 col-md-3  pnlZambaLogin">
                    <%--input y label username--%>
                    <div class="form-group">
                        <label for="txtUserName" runat="server">Usuario: </label>
                        <input type="text" runat="server" class="form-control " id="txtUserName" placeholder="Ingrese Nombre de Usuario" />
                        <asp:Label runat="server" Font-Bold="True" ForeColor="Red" Text=" (Requerido)" ID="RequiredFieldValidator1"
                            Visible="False" AssociatedControlID="txtUserName"></asp:Label>
                    </div>
                    <%--input y label password--%>
                    <div class="form-group">
                        <label for="txtPassword" runat="server" id="lblPassword">Clave: </label>
                        <input runat="server" class="form-control" id="txtPassword" autocomplete="off" type="password" placeholder="Ingrese su clave" />
                        <asp:Label ForeColor="Red" Font-Bold="True" runat="server" Text=" (Requerido)" ID="RequiredFieldValidator2"
                            Visible="False" AssociatedControlID="txtPassword"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

        <%-- Boton iniciar sesion --%>
        <div class="row">
            <div class="col-xs-offset-2 col-xs-8 col-sm-offset-4 col-sm-4 col-md-offset-8 col-md-3 nlZambaLogin">
                <asp:LinkButton runat="server" Class="pull-right btn btn-primary boton-iniciar-sesion2" OnClick="btnLoginZamba_Click" ID="btnLogin">
                            Ingresar <span class="glyphicon glyphicon-log-in"></span>
                </asp:LinkButton>
            </div>
        </div>

        <%-- Etiqueta error login --%>
        <div class="row">
            <div class="form-group col-md-5 col-md-offset-6 col-xs-10">
                <label id="lblError" runat="Server" style="color: red;"></label>
            </div>
        </div>

        <div class="row">
            <asp:Panel ID="pnlWindowsLogin" runat="server">
                <asp:Table ID="tblBtnIngresar" Width="25%" runat="server">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center">
                            <asp:Button ID="btnLoginWindows" OnClick="btnLoginWindows_Click" Text="Ingresar"
                                Font-Size="Medium" BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid"
                                BorderWidth="1px" Font-Names="Verdana" ForeColor="#284775" runat="server" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center">
                            <asp:CheckBox ID="chkZambaLogIn" Visible="true" Text="Ingresar con otro usuario"
                                runat="server" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:Panel>
        </div>
        <div class="Container-fluid">
            <div class="row">
                <div class="col-md-12 col-md-offset-0 ">
                    <div id="divInicioSesion">
                        &nbsp;
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="FooterLogIn">                
                <div class="divSlogan" style="display: none"</div>
                    &nbsp;
                </div>
                   <div>
                     <img class="pwbyzamba" src="../../Content/Images/pwbyzamba.png" alt="" />
                    <div class="hidden-xs hidden-sm"><div class ="legal">
                     <div>Zamba Web Ver. <% =HttpContext.Current.Application["ZambaVersion"] %></div> 
                     <div>Copyright© 2016 Stardoc Argentina - Todos los derechos reservados.</div>
                        </div>
                    <br />
                </div>
            </div>
        </div>

    </form>

    <script>
        $(window).load(function () {
            parent.ResizeCurrentTab($(document).height(), $(document).context.location.href);
            if (parent.hideLoading != null) {
                parent.hideLoading();
            }

            if (parent != this) {
                parent.RedirectToLogin();
            }
        });

        <%-- creo que se agregan para salvar errores --%>
        function hideLoading() { }
        function ResizeCurrentTab() { }
    </script>
</body>
</html>
