<%@ Page Language="C#" AutoEventWireup="true" Inherits="Login" EnableEventValidation="false" Async="false" CodeBehind="Login.aspx.cs" %>

<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9; IE=8; IE=7; IE=EDGE,chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
                         <p  id="version" style="display: none"> V.- 1.0</p>
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
    </body>
    </html>




        <%: Scripts.Render( "~/bundles/particles") %>
        <%: Scripts.Render( "~/bundles/modernizr") %>
        <%: Scripts.Render("~/bundles/bootstrap") %>


        <script type="text/javascript">


      
  
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


try     {
         
  $(".particles-js-canvas-el").css("position", "fixed");
 } catch (e) {

    var ZambaWebRestApiURL = location.origin.trim() + getValueFromWebConfig("RestApiUrl");// + "/api";
    var URLLoginByGUID = ZambaWebRestApiURL + '/api/Account/LoginByGuid';

    $(document).ready(function () {
        CheckIfAuthenticated();
    });

    function CheckIfAuthenticated() {
        if (localStorage != undefined)
            if (localStorage.authorizationData != undefined) {
                var data = JSON.parse(localStorage.authorizationData);
                var userid = data.UserId;
                var token = data.token;
                $.ajax({
                    url: ZambaWebRestApiURL + '/api/Account/CheckToken?UserId=' + userid + '&Token=' + token,
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    success: function (response) {
                        if (response == true) {
                            var url = getUrlParameters().returnurl;
                            if (url == undefined)
                                url = "/" + location.pathname.split('/')[1] + "/globalsearch/search/search.html?"
                            if (url.substring(0, 1) == "/")
                                url = location.origin.trim() + url;
                            var splitUrl = url.split('?');
                            if (splitUrl.length == 2) {
                                var queryString = "";
                                var params = splitUrl[1].split('&');
                                // Debo recuperar el querystring sin informacion de userid y token
                                if (params[0] != "") {
                                    params.forEach(function (param) {
                                        var key = param.split('=')[0];
                                        var value = param.split('=')[1];
                                        if (key != 'userid' && key != "token") {
                                            queryString += key + "=" + value + "&";
                                        }
                                    }
                                    );
                                }
                                queryString += "userid=" + userid + "&token=" + token.substring(0,180);
                                url = splitUrl[0] + "?" + queryString
                                location.href = url;
                            }}
                    },
                    error: function (error) {
                        ret = error
                    }
                });
            }
    }

    function LoginByGuid(userid, guid, redirectUrl) {
        var Params = {
            userid: userid,
            guid: guid
        };
        var ret;
        $.ajax({
            url: URLLoginByGUID + "?userid=" + userid + "&guid=" + guid,
            type: "POST",
            //data: Params,
            contentType: "application/json; charset=utf-8",
            async: false,
            success: function (response) {
                if (JSON.parse(response).token == undefined || JSON.parse(response).token == null)
                    ret = error;
                else {
                    var tokenInfo = JSON.parse(response)
                    if (localStorage) {
                        localStorage.setItem('authorizationData', JSON.stringify({
                            token: tokenInfo.token,
                            userName: tokenInfo.userName,
                            refreshToken: tokenInfo.refreshToken,
                            useRefreshTokens: tokenInfo.useRefreshTokens,
                            generateDate: new Date(),
                            UserId: tokenInfo.userid
                        }));
                    }
                }                
                window.location.href = redirectUrl + "userid=" + tokenInfo.userid + "&token=" + tokenInfo.token.substring(0, 180);
            },
            error: function (error) {
                ret = error
            }
        });
        return ret;
    }
    function getUrlParameters() {
        var pairs = window.location.search.substring(1).split(/[&?]/);
        var res = {}, i, pair;
        for (i = 0; i < pairs.length; i++) {
            pair = pairs[i].toLowerCase().split('=');
            if (pair[1])
                res[decodeURIComponent(pair[0])] = decodeURIComponent(pair[1]);
        }
        return res;
    }
    function getValueFromWebConfig(key) {


        var url = "Services/ViewsService.asmx/getValueFromWebConfig?key=" + key;
        if (location.pathname.toLowerCase().split("/").indexOf('login.aspx') > 1) {
            url = "../../Services/ViewsService.asmx/getValueFromWebConfig?key=" + key;
        }

        $.ajax({
            "async": false,
            "crossDomain": true,
            "url": url,
            "method": "GET",
            "headers": {
                "cache-control": "no-cache"
            },
            "success": function (response) {
                if (response.childNodes[0].innerHTML == undefined) {
                    pathName = response.childNodes[0].textContent;
                } else {
                    pathName = response.childNodes[0].innerHTML;
                }

            },
            "error": function (data, status, headers, config) {
                console.log(data);
            }
        });
        return pathName;
    }
</script>
