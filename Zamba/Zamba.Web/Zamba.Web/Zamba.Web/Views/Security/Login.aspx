<%@ Page Language="C#" AutoEventWireup="true" Inherits="Login" EnableEventValidation="false" CodeBehind="Login.aspx.cs" ValidateRequest ="true" %>

<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9; IE=8; IE=7; IE=EDGE,chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">

    <title title="Zamba.BPM"></title>
    <link id="lnkWebIcon" rel="shortcut icon" runat="server" type="image/x-icon" />

    <link href="../../Content/Login/animate/animate.css" rel="stylesheet" />
    <link href="../../Content/Login/animsition/css/animsition.min.css" rel="stylesheet" />
    <link href="../../Content/Login/select2/select2.min.css" rel="stylesheet" />

    <link href="../../Content/Login/fonts/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../Content/Login/fonts/iconic/css/material-design-iconic-font.min.css" rel="stylesheet" />

    <link href="../../Content/Login/css/util.css" rel="stylesheet" />
    <link href="../../Content/Login/css/main.css" rel="stylesheet" />
    <link href="../../Content/toastr.css" rel="stylesheet" />

    <asp:PlaceHolder runat="server">
        <%: Styles.Render("~/bundles/Styles/jquery")%>
        <%: Styles.Render("~/bundles/Styles/bootstrap")%>
        <%: Scripts.Render("~/bundles/jqueryCore") %>        
    </asp:PlaceHolder>

</head>

<body id="loginBody" runat="server" class="bg noscroll">

    <script>
        function ExecutePostLoginActions() {
        }

        function CloseModalLogin() {
            parent.$("#modalLogin").modal("hide");

        }

        

    </script>
    <div class="limiter">
        <div class="container-login100">
            <div class="wrap-login100" id="loginContainer">
                <form id="form2" runat="server" role="form" defaultbutton="btnLogin" class="login100-form validate-form">

                    <asp:HiddenField ID="hdnUserId" runat="server" />
                    <asp:HiddenField ID="hdnConnectionId" runat="server" />
                    <asp:HiddenField ID="hdnComputer" runat="server" />
                    <asp:HiddenField ID="hdnthisdomian" runat="server" />
                    <asp:HiddenField ID="hdnLocation" runat="server" />

                    <div class="container">
                        <img id="clientlogo" class="img-responsive" width="295" height="190">
                    </div>


                    <div class="row">
                        <div id="controlMensaje" class="controlMensaje">
                            <asp:Label ID="lblMensajeLogin" Font-Bold="false" Visible="true" runat="server" Font-Size="Small" Text="" />
                        </div>
                    </div>

                    <div id="pnlZambaLogin" runat="server">
                        <%--input y label username--%>
                        <div class="wrap-input100 validate-input" data-validate="Valid email is: a@b.c">
                            <input runat="server" class="input100 login-font-size-25-mobile" type="text" name="email" id="txtUserName" autocomplete="off" placeholder="Usuario">
                            <span class="focus-input100"></span>

                        </div>

                        <%--input y label password--%>
                        <div class="wrap-input100 validate-input" data-validate="Enter password">
                            <%--<span class="btn-show-pass"><i class="zmdi zmdi-eye"></i></span>--%>
                            <input class="input100 login-font-size-50-mobile" runat="server" id="txtPassword" type="password" name="pass" placeholder="Clave" autocomplete="off">
                            <span class="focus-input100"></span>

                        </div>
                    </div>




                    <%--ingreso--%>
                    <div class="container-login100-form-btn">
                        <div class="wrap-login100-form-btn">
                            <div class="login100-form-bgbtn"></div>
                            <asp:LinkButton runat="server" class="login100-form-btn" OnClick="btnLoginZamba_Click" ID="btnLogin" OnClientClick="return showSpinnerLogin();">
                                    Ingresar
                            </asp:LinkButton>
                        </div>
                    </div>

                    <%-- Etiqueta error login --%>
                    <h5 align="center">
                        <label align="center" id="lblError" runat="Server" style="color: red;"></label>
                    </h5>
                    <div id="spinner-box-container">
                        <img id="z-img-spinner">
                    </div>
                    <div class="p-t-30">
                        <div class="login-font-size-30-mobile" style="font-size: x-small;">Zamba Web Ver. <% =ConfigurationManager.AppSettings["ZambaVersion"].ToString() %></div>
                        <div class="login-font-size-30-mobile" style="font-size: x-small;">Copyright© <% =DateTime.Now.ToString("yyyy") %> Stardoc Argentina - Todos los derechos reservados.</div>
                    </div>


                    <div style="display: none">
                        <%-- Imagen de Encabezado --%>
                        <div class="row bannerHeader">
                            <%--   <div class=" hidden-xs  col-md-10  cabecera-login">
                                </div>--%>

                            <%--<div class="col-xs-offset-0 col-xs-12 col-md-12 col-md-2 cabecera-login2"></div>--%>
                        </div>


                        <%--                            <div class="row">
                                <div id="controlMensaje" class="controlMensaje">
                                    <asp:Label ID="lblMensajeLogin" Font-Bold="false" Visible="true" runat="server" Font-Size="Small" Text="" />
                                </div>
                            </div>--%>

                        <%--Form login --%>
                        <div id="">
                            <div class="row">
                                <div class=" col-xs-12 col-md-offset-4 col-md-4">
                                    <%--input y label username--%>
                                    <%--<div class="form-group">
                                            <label class="login-font-size-50-mobile" for="txtUserName" runat="server">Usuario: </label>
                                            <input type="text" runat="server" class="form-control login-font-size-25-mobile " id="txtUserName" placeholder="Usuario" />
                                            <asp:Label runat="server" Font-Bold="True" ForeColor="Red" Text=" (Requerido)" ID="RequiredFieldValidator1"
                                                Visible="False" AssociatedControlID="txtUserName"></asp:Label>
                                        </div>--%>
                                    <%--input y label password--%>
                                </div>
                                <div class=" col-xs-12 col-md-offset-4 col-md-4 ">
                                    <%--<div class="form-group">
                                            <label for="txtPassword" class="login-font-size-50-mobile" runat="server" id="lblPassword">Clave: </label>
                                            <input runat="server" class="form-control login-font-size-25-mobile" id="txtPassword" autocomplete="off" type="password" placeholder="Clave" />
                                            <asp:Label ForeColor="Red" Font-Bold="True" runat="server" Text=" (Requerido)" ID="RequiredFieldValidator2"
                                                Visible="False" AssociatedControlID="txtPassword"></asp:Label>
                                        </div>--%>
                                </div>
                            </div>
                        </div>


                        <%-- Boton iniciar sesion --%>
                        <%--                 <div class="row">
                                <div class=" col-xs-9 col-md-offset-4 col-md-4 nlZambaLogin">
                                    <asp:LinkButton runat="server" CssClass="login-font-size-30-mobile pull-right btn btn-primary boton-iniciar-sesion2" OnClick="btnLoginZamba_Click" ID="btnLogin">
                            Ingresar <span class="glyphicon glyphicon-log-in"></span>
                                    </asp:LinkButton>
                                </div>

                            </div>--%>

                        <%-- Etiqueta error login --%>
                        <%--                            <div class="row">
                                <div class="form-group col-md-5 col-md-offset-6 col-xs-10">
                                    <label id="lblError" runat="Server" style="color: red;"></label>
                                </div>
                            </div>--%>

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

                </form>
            </div>
        </div>

    </div>

    <asp:Label runat="server" Font-Bold="True" ForeColor="Red" Text="" ID="RequiredFieldValidator1" Visible="False" AssociatedControlID="txtUserName"></asp:Label>
    <asp:Label ForeColor="Red" Font-Bold="True" runat="server" Text="" ID="RequiredFieldValidator2" Visible="False" AssociatedControlID="txtPassword"></asp:Label>

    <style>
        .noscroll {
            overflow-x: scroll;
            overflow-x: hidden;
            overflow-y: scroll;
            overflow-y: hidden;
        }

        .manual:hover {
            background-color: white !important;
            color: black !important;
        }

        .manual:focus {
            background-color: white !important;
            color: black !important;
            text-decoration: underline !important;
        }

        a:hover, a:focus {
            color: #ffffff !important;
            text-decoration: none !important;
            text-decoration: underline !important;
        }
    </style>

    <%: Scripts.Render("~/bundles/modernizr") %>
    <%: Scripts.Render("~/bundles/bootstrap") %>
    <%: Scripts.Render("~/bundles/Login") %>

    <script type="text/javascript"> 
        var thisDomain;
        function popupsLocked() {
            if (localStorage.getItem("LockedPopup") == 'true')
                return true;
            if (isInternetExplorer())
                return false;
            var ret = false;
            var testNavigator = window.open('https://www.google.com', '_blank');
            if (testNavigator == undefined || testNavigator == null)
                ret = true;
            else {
                testNavigator.close();
                ret = false;
            }
            localStorage.setItem("LockedPopup", ret)
            return ret;
        }

        function showSpinnerLogin() {
            $('#spinner-box-container').css('display', 'flex');
            $('#lblError').css('display', 'none');
            $('#btnLogin').css('display', 'none');
            return true;
        }
        
        function isFireFox() {
            return (
                navigator.appVersion.indexOf('Mozilla/') > 0 ||
                navigator.userAgent.toString().indexOf('Firefox/') > 0)
        }
        function isInternetExplorer() {
            return (navigator.userAgent.indexOf('MSIE') !== -1 ||
                navigator.appVersion.indexOf('Trident/') > 0 ||
                navigator.userAgent.toString().indexOf('Edge/') > 0)
        }

        
            if (thisDomain == null || thisDomain == undefined) {
                thisDomain = window.location.protocol + "//" + window.location.host  + "/" + window.location.pathname.split('/')[1];
            }
            if (window.domainName == undefined) {
                $("#clientlogo").attr("src", thisDomain + "/content/images/logozamba.jpg");
            } else {
                $("#clientlogo").attr("src", window.location.origin + window.domainName + "/App_Themes/<a href='../../Test/MailTo/'>../../Test/MailTo/</a>Provincia/Images/LoginMain.jpg");
            }
        function GetPathHelp(){
            var ret = "";
            if (window.domainName == undefined) {
                ret = thisDomain + "/Views/Security/Help/";
            } else {
                ret = window.location.origin + window.domainName + "/Views/Security/Help/";
            }
            return ret;
        }






        function showManualUnlockedPopupsForFireFox() {
            var mensaje = "Su navegador tiene bloqueadas las ventanas emergentes. <br>Lea el <a class='manual' download='" + GetPathHelp() + "PopupsUnlockedFireFox.pdf' href='" + GetPathHelp() + "PopupsUnlockedFireFox.pdf'>manual de instrucciones</a> para solucionar el problema."
            $($("#lblError")[0]).html(mensaje);
        }

        function showManualUnlockedPopupsForChromeAndEdge() {
            var mensaje = "Su navegador tiene bloqueadas las ventanas emergentes. <br>Lea el <a class='manual' download='" + GetPathHelp() + "PopupsUnlockedChromeEdge.pdf' href='" + GetPathHelp() + "PopupsUnlockedChromeEdge.pdf'>manual de instrucciones</a> para solucionar el problema."
            $($("#lblError")[0]).html(mensaje);
        }
        function showManualUnlockedPopupsForInternetExplorer() {
            var mensaje = "Su navegador tiene bloqueadas las ventanas emergentes. <br>Lea el <a class='manual' download='" + GetPathHelp() + "PopupsUnlockedIE11.pdf' href='" + GetPathHelp() + "PopupsUnlockedIE11.pdf'>manual de instrucciones</a> para solucionar el problema."
            $($("#lblError")[0]).html(mensaje);
        }
        function hideSpinnerLogin() {
            $('#spinner-box-container').css('display', 'none');
            $('#btnLogin').css('display', 'flex');
        }
        getPhoto();
        setSpinnerImage();

        function GetURLHelper() {
            return '<%=Zamba.Core.ZOptBusiness.GetValueOrDefault("URLHelper","http://www.zamba.com.ar/zambaHelp/viewer/") %>';
        };

        function getRestApiUrl() {
            return '<%=ConfigurationManager.AppSettings["RestApiUrl"] %>';
        };

        function getThisDomain() {
            return '<%=ConfigurationManager.AppSettings["ThisDomain"] %>';
        };

        var domainName = getThisDomain();
        var urlLocation = location.origin.trim();
        document.getElementById("hdnthisdomian").setAttribute("value", domainName);
        document.getElementById("hdnLocation").setAttribute("value", urlLocation);
        var dominio = document.getElementById("hdnthisdomian").value;
        window.onload = new function () {
            if (isInternetExplorer()) {
                showManualUnlockedPopupsForInternetExplorer();
            }
            else if (isFireFox()) {
                if (popupsLocked()) {
                    showManualUnlockedPopupsForFireFox()
                }
            }
            else {
                if (popupsLocked()) {
                    showManualUnlockedPopupsForChromeAndEdge();
                    return false;
                }
            }
        };

        function validationError() {
            toastr.warning("No se ha ingresado un usuario");
        }



    </script>

</body>
</html>
