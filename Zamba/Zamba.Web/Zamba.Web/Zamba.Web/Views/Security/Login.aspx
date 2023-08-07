<%@ Page Language="C#" AutoEventWireup="true" Inherits="Login" EnableEventValidation="false" Async="false" CodeBehind="Login.aspx.cs" %>

<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9; IE=8; IE=7; IE=EDGE,chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <meta property="AntiForgeryToken" value="#AntiForgeryToken" />

    <title title="Zamba Web"></title>
    <link id="lnkWebIcon" rel="shortcut icon" runat="server" type="image/x-icon" />
    <link rel="stylesheet" type="text/css" href="../../Content/styles/normalize.css" />
    <link rel="stylesheet" type="text/css" href="../../Content/font-awesome.min.css" />
    <link href="../../Content/partialSearchIndexs.css" rel="stylesheet" type="text/css" />

    <asp:PlaceHolder runat="server">
        <%: Styles.Render("~/bundles/Styles/jquery")%>
        <%: Styles.Render("~/bundles/Styles/bootstrap")%>
        <%: Scripts.Render("~/bundles/jqueryCore") %>        
    </asp:PlaceHolder>
</head>

<body id="loginBody" runat="server">
    <script src="Login.js"></script>

    <div class="hidden-xs" id="particles-js"></div>
    <form id="form2" runat="server" role="form" defaultbutton="btnLogin">
        <asp:HiddenField ID="hdnUserId" runat="server" />
        <asp:HiddenField ID="hdnConnectionId" runat="server" />
        <asp:HiddenField ID="hdnComputer" runat="server" />
        <asp:HiddenField ID="hdnthisdomian" runat="server" />
        <asp:HiddenField ID="hdnLocation" runat="server" />


        <div class="container-fluid">
            <div id="MasterHeader" class="navbar navbar-static-top">
                <div class="container clientPhrase" id="header-Cabezera-Master">
                    <a class="navbar-brand" id="tblCabecera">
                        <span class="clientlogologin" style="border: none;"></span>
                    </a>

                    <div class="slogan"></div>
                    <div class="collapse navbar-collapse navHeaderCollapse ">
                        <ul class="nav navbar-nav navbar-right">
                            <li>
                                <div id="dynamicButtonContainer" runat="server" style=""></div>
                            </li>

                            <li></li>
                        </ul>
                    </div>
                </div>
            </div>

            <%-- Imagen de Encabezado --%>
            <div class="row bannerHeader">
                <div class=" hidden-xs  col-md-10  cabecera-login">
                </div>

                <%--<div class="col-xs-offset-0 col-xs-12 col-md-12 col-md-2 cabecera-login2"></div>--%>
            </div>


            <div class="row">
                <div id="controlMensaje" class="controlMensaje">
                    <asp:Label ID="lblMensajeLogin" Font-Bold="false" Visible="true" runat="server" Font-Size="Small" Text="" />
                </div>
            </div>

            <%--Form login --%>
            <div id="pnlZambaLogin" runat="server">
                <div class="row">
                    <div class=" col-xs-12 col-md-offset-4 col-md-4">
                        <%--input y label username--%>
                        <div class="form-group">
                            <label class="login-font-size-50-mobile" for="txtUserName" runat="server">Usuario: </label>
                            <input type="text" runat="server" class="form-control login-font-size-25-mobile " id="txtUserName" placeholder="Usuario" />
                            <asp:Label runat="server" Font-Bold="True" ForeColor="Red" Text=" (Requerido)" ID="RequiredFieldValidator1"
                                Visible="False" AssociatedControlID="txtUserName"></asp:Label>
                        </div>
                        <%--input y label password--%>
                    </div>
                    <div class=" col-xs-12 col-md-offset-4 col-md-4 ">
                        <div class="form-group">
                            <label for="txtPassword" class="login-font-size-50-mobile" runat="server" id="lblPassword">Clave: </label>
                            <input runat="server" class="form-control login-font-size-25-mobile" id="txtPassword" autocomplete="off" type="password" placeholder="Clave" />
                            <asp:Label ForeColor="Red" Font-Bold="True" runat="server" Text=" (Requerido)" ID="RequiredFieldValidator2"
                                Visible="False" AssociatedControlID="txtPassword"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>

            <%-- Boton iniciar sesion --%>
            <div class="row">
                <div class=" col-xs-9 col-md-offset-4 col-md-4 nlZambaLogin">
                    <asp:LinkButton runat="server" CssClass="login-font-size-30-mobile pull-right btn btn-primary boton-iniciar-sesion2" OnClick="btnLoginZamba_Click" ID="btnLogin">
                            Ingresar <span class="glyphicon glyphicon-log-in"></span>
                    </asp:LinkButton>
                    <a runat="server" id="btnLoginWithOkta" href="OktaAuthentication.html">Ingresar con Okta Authentication</a>
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
                    <asp:LinkButton runat="server" CssClass="login-font-size-30-mobile col-xs-offset-5 btn btn-primary" OnClick="btnLoginWindows_Click" ID="btnLoginWindows">
                            Ingresar <span class="glyphicon glyphicon-log-in"></span>
                    </asp:LinkButton>
                </asp:Panel>
            </div>

            <div class="row hidden-xs">
                <div class="FooterLogIn">
                    <div class="divSlogan" style="display: none">
                        <p id="version" style="display: none">V.- 1.0</p>
                    </div>
                    &nbsp;
                </div>
                <div>
                    <%-- <img class="pwbyzamba" src="../../Content/Images/pwbyzamba.png" alt="" />--%>
                    <div class="hidden-xs ">
                        <div class="legal">
                            <div class="login-font-size-30-mobile">Zamba Web Ver. <% =ConfigurationManager.AppSettings["ZambaVersion"].ToString() %></div>
                            <div class="login-font-size-30-mobile">Copyright© <% =DateTime.Now.ToString("yyyy") %> Stardoc Argentina - Todos los derechos reservados.</div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>

        </div>
        <%: Scripts.Render( "~/bundles/particles") %>
        <%: Scripts.Render( "~/bundles/modernizr") %>
        <%: Scripts.Render("~/bundles/bootstrap") %>
    </form>
</body>
</html>
