<%@ Page Language="C#" AutoEventWireup="true" Inherits="Views_Insert_Insert"
    UICulture="Auto" Culture="Auto" EnableViewState="true" EnableEventValidation="false" CodeBehind="Insert.aspx.cs" %>



<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/Views/UC/Upload/ucUploadFile.ascx" TagName="ucUploadFile" TagPrefix="uc1" %>
<%@ Register Src="~/Views/UC/Upload/ucDocTypes.ascx" TagName="ucDocTypes" TagPrefix="uc2" %>
<%@ Register Src="~/Views/UC/Index/ucDocTypeIndexsForInsert.ascx" TagName="ucDocTypesIndexs" TagPrefix="uc3" %>
<%@ Register Src="~/Views/UC/WF/UCWFExecution.ascx" TagName="UCWFExecution" TagPrefix="UC6" %>


<html>
<head id="Head2" runat="server">

    <asp:PlaceHolder runat="server">

        <link href="../../Content/Styles/normalize.min.css" rel="stylesheet" />

        <script src="../../Scripts/jquery-3.1.1.min.js"></script>
        <script src="../../forms/Scripts/jquery.validate.min.js"></script>

        <%: Scripts.Render("~/bundles/jqueryAddIns") %>
        <script src="../../Scripts/Zamba.js?v=257"></script>
        <link rel="stylesheet" type="text/css" href="../../Content/css/ZClass.css" />
        <link rel="stylesheet" type="text/css" href="../../Content/bootstrap.min.css">
        <%--     <link rel="stylesheet" type="text/css" href="../../Content/site.css"/>--%>
        <!-- JS -->

        <%--        <script src="../../Scripts/jquery-1.12.4.js"></script>--%>
        <script src="../../Scripts/jquery-1.9.1.js"></script>
        <script src="../../Scripts/jquery-ui.js"></script>
        <link href="../../Content/themes/base/jquery-ui.css" rel="stylesheet" />


        <%: Styles.Render("~/bundles/Styles/masterblankStyles")%>
        <%: Styles.Render("~/bundles/Styles/ZStyles")%>
        <link href="../../Content/site.css" rel="stylesheet" />
        <%: Styles.Render( "~/bundles/Styles/bootstrap")%>
        <%: Scripts.Render("~/bundles/jqueryAddIns") %>
        <%: Scripts.Render( "~/bundles/modernizr") %>
        <%: Scripts.Render("~/bundles/jqueryAddIns") %>
        <%: Scripts.Render("~/bundles/ZScripts") %>
        <%: Scripts.Render("~/bundles/bootstrap") %>
        <%: Scripts.Render("~/scripts/dropzone.js") %>
        <%: Scripts.Render("~/bundles/bootbox") %>
        <link href="../../Content/dropzone.min.css" rel="stylesheet" />
        <link href="../../Content/partialSearchIndexs.css?v=248" rel="stylesheet" />
        <script src="../../Scripts/sweetalert.min.js"></script>
        <script src="../../Scripts/ListaV2.js?v=248"></script>



        <script src="../../Scripts/angular.min.js"></script>
        <script src="../../Scripts/angular-messages.js"></script>
        <script src="../../Scripts/angular-xeditable-0.8.1/js/xeditable.js"></script>
        <script src="../../Scripts/angular-sanitize.min.js"></script>
        <script src="../../Scripts/angular-animate.min.js"></script>
        <script src="../../Scripts/ng-embed/ng-embed.min.js"></script>

        <script src="../../Scripts/angular-filter/angular-filter.min.js"></script>

        <script src="../../app/Grid/JS/ui-bootstrap-tpls-1.2.4.js"></script>

        <script src="../../app/zapp.js?v=248"></script>
        <script src="../../app/i18n/i18n.js?v=248"></script>
        <script src="../../Scripts/bower-angular-translate-2.9.0.1/angular-translate.js"></script>
        <!--locale translate-->



        <script src="../../GlobalSearch/services/angular-local-storage.min.js"></script>
        <script src="../../GlobalSearch/services/authService.js?v=257"></script>
        <script src="../../app/user/Controller/UserController.js?v=248"></script>
        <script src="../../app/user/Service/UserService.js?v=248"></script>

        <script src="../../GlobalSearch/services/angular-local-storage.min.js"></script>

        <script src="../../app/Tasks/Controller/TaskController.js?v=242"></script>
        <script src="../../app/Tasks/Service/TaskService.js?v=242"></script>


        <script src="../../app/insert/insertcontroller.js?v=248"></script>
        <script src="../../app/insert/insertservices.js?v=248"></script>

        <link href="../../Scripts/angular-material.min.css" rel="stylesheet" />
        <script src="../../Scripts/angular-material.min.js"></script>

        <link href="../../Content/zmaterial.css" rel="stylesheet" />
        <script src="../../Scripts/zamba.associated.js?v=248"></script>
        <%-- Header --%>
        <script src="../../Scripts/toastr.min.js"></script>
        <link href="../../Content/toastr.css" rel="stylesheet" />
        <link rel="stylesheet" type="text/css" href="../../Content/font-awesome.min.css" />

        <script src="../../app/i18n/i18n.js?v=248"></script>
        <script src="../../Scripts/bower-angular-translate-2.9.0.1/angular-translate.js"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                setPhotoValueConfig();
            });

            function setPhotoValueConfig() {

                var photoValue = getValueFromWebConfig("WebClient");

                if (photoValue == "Provincia") {
                    $(".clientlogo2").prop("src", "../../App_Themes/Provincia/Images/logo-header-blanco.png");
                    if (!$("#lnkWebIcon")[0] == undefined)
                        $("#lnkWebIcon")[0].attributes[3].nodeValue = "../../App_Themes/Provincia/Images/WebIcon.png";
                    $(".clientlogo2").css("max-height", "40px !important");
                    $(".clientlogo2").css("width", "112px")
                    $(".clientlogo2").css("margin-left", "-12px");
                    $(".clientlogo2").css("vertical-align", "-webkit-baseline-middle");
                }
                if (photoValue == "Boston") {
                    try {

                        $(".clientlogo2").prop("src", "../../App_Themes/Boston/Images/Logo.jpg");
                        if (!$("#lnkWebIcon")[0] == undefined)
                            $("#lnkWebIcon")[0].attributes[3].nodeValue = "../../App_Themes/Boston/Images/WebIcon.jpg";
                        $(".clientlogo2").css("margin-top", "-3px");
                        $(".clientlogo2").css("height", "50px");
                        $(".clientlogo2").css("margin-left", "-15px");
                        $(".clientlogo2").css("width", "126px");

                        $(".clientlogo2").css("vertical-align", "-webkit-baseline-middle");
                        $(".btn.btn-navbar").css("margin-top", "5px");
                        $("#dropCabezera").css("margin-top", "5px");
                        $(".img-circle.ng-show").css("margin-top", "2px");
                    } catch (e) {

                    }

                }
                var FechaVersion = getValueFromWebConfig("FechaVersion");

                if (FechaVersion != undefined && FechaVersion != null) {
                    if ($("#FechaIdVersion")[0] != undefined)
                        $("#FechaIdVersion")[0].innerText = "Fecha del compilado: " + FechaVersion;
                }

                ////////////////////////////////////////////////////////////////////////

            }


            $(window).on("ready", function () {
                if (getUrlParameters().embedded != undefined) {
                    $('#zmodal-header').hide();
                }
                $(document).find("body").css("overflow", "auto");
                if ($("#divFormContainer").length) {
                    $("#divFormContainer").bind("resize", function (event) {
                        ResizeFormBrowser();
                    });
                }

                setTimeout("ResizeFormBrowser();", 50);
            });

            function ResizeFormBrowser() {
                if (parent !== document) {
                    $(document).height($("#Container").height());
                    parent.ResizeTBIframe($("#Container").width() + 2, $("#Container").height() + 20);
                }
            }

            function Cerrar() {
                parent.CloseInsert();
            }

            //Se ocultan los botones para que no sigan apareciendo mientras carga la pagina
            $("#zamba_save").click(function () {
                $(this).css("display", "none");
                $("#zamba_cancel").css("display", "none");
            });

            $("#zamba_cancel").click(function () {
                $(this).css("display", "none");
                $("#zamba_save").css("display", "none");
            });

            function navigateToHome() {
                var authData = localStorage.getItem('authorizationData');
                var authDataJSON = JSON.parse(authData);
                authDataJSON.UserId;
                authDataJSON.token;
                var thisDomain = window.location.protocol + "//" + window.location.host + "/" + window.location.pathname.split('/')[1];
                var baseUrl = thisDomain + '/globalsearch/search/search.html?';
                var urlHome = baseUrl + "t=" + authDataJSON.token + "&user=" + authDataJSON.UserId + "#tabHome"
                window.open(urlHome);
                window.close();
            }

        </script>

        <style>
            /* latin */
            @font-face {
                font-family: 'Libre Barcode 39';
                font-style: normal;
                font-weight: 400;
                src: local('Libre Barcode 39 Regular'), local('LibreBarcode39-Regular'), url(https://fonts.gstatic.com/s/librebarcode39/v8/-nFnOHM08vwC6h8Li1eQnP_AHzI2G_Bx0g.woff2) format('woff2');
                unicode-range: U+0000-00FF, U+0131, U+0152-0153, U+02BB-02BC, U+02C6, U+02DA, U+02DC, U+2000-206F, U+2074, U+20AC, U+2122, U+2191, U+2193, U+2212, U+2215, U+FEFF, U+FFFD;
            }

            @font-face {
                font-family: 'IDAutomationHC39M';
                src: local('IDAutomationHC39M'), url(../../fonts/IDAutomationHC39M.woff);
            }

            .codebar39 {
                font-family: 'Libre Barcode 39', cursive;
                font-size: 48px;
            }

            @media print {

                /* ESTILOS SOLO PARA LA IMPRESION */
                body {
                    writing-mode: vertical-rl;
                    -webkit-print-color-adjust: exact;
                }

                .ng-scope > zamba-associated > nav {
                    display: none;
                }

                #printer {
                    display: none;
                }

                @page {
                    size: auto;
                    /* auto es el valor inicial */
                    margin: 0;
                    margin-left: 25px;
                    margin-top: 2px;
                    margin-bottom: auto;
                }

                .noprint,
                .noprint * {
                    display: none !important;
                }

                .AllTop {
                    position: relative;
                    margin-top: -75px;
                }

                .insertAttributesPanel {
                    height: 100% !important;
                    overflow-y: hidden !important;
                }
            }

            .texto-vertical-2 {
                writing-mode: vertical-lr;
                transform: rotate(180deg);
            }

            /*menu usuario*/
            .UserMenuItem:hover {
                color: #fafafa !important;
                text-decoration: none;
                background-color: #1984c8 !important;
            }

            .nav > li > a:focus, .nav > li > a:hover, .nav .open > a {
                background-color: #0b4376 !important;
            }
            /*PANEL USUARIO*/
            .tablediv {
                max-width: 95% !important;
                margin-bottom: 0px !important;
            }

            .multiselect.dropdown-toggle.btn.btn-default {
                width: 300px;
                white-space: normal;
            }

            .multiselect-container.dropdown-menu {
                min-width: 250px !important;
                MAX-HEIGHT: 213PX !important;
                OVERFLOW: auto !important;
            }
            /* Tabs panel */
            .tabbable-panel {
                border: 1px solid #eee;
                padding: 10px;
            }

            /* Default mode */
            .tabbable-line > .nav-tabs {
                border: none;
                margin: 0px;
            }

                .tabbable-line > .nav-tabs > li {
                    margin-right: 2px;
                }

                    .tabbable-line > .nav-tabs > li > a {
                        border: 0;
                        margin-right: 0;
                        color: #737373;
                    }

                        .tabbable-line > .nav-tabs > li > a > i {
                            color: #a6a6a6;
                        }

                    .tabbable-line > .nav-tabs > li.open, .tabbable-line > .nav-tabs > li:hover {
                        border-bottom: 4px solid #fbcdcf;
                    }

                        .tabbable-line > .nav-tabs > li.open > a, .tabbable-line > .nav-tabs > li:hover > a {
                            border: 0;
                            background: none !important;
                            color: #333333;
                        }

                            .tabbable-line > .nav-tabs > li.open > a > i, .tabbable-line > .nav-tabs > li:hover > a > i {
                                color: #a6a6a6;
                            }

                        .tabbable-line > .nav-tabs > li.open .dropdown-menu, .tabbable-line > .nav-tabs > li:hover .dropdown-menu {
                            margin-top: 0px;
                        }

                    .tabbable-line > .nav-tabs > li.active {
                        border-bottom: 4px solid #f3565d;
                        position: relative;
                    }

                        .tabbable-line > .nav-tabs > li.active > a {
                            border: 0;
                            color: #333333;
                        }

                            .tabbable-line > .nav-tabs > li.active > a > i {
                                color: #404040;
                            }

            .tabbable-line > .tab-content {
                margin-top: -3px;
                background-color: #fff;
                border: 0;
                border-top: 1px solid #eee;
                padding: 15px 0;
            }

            .portlet .tabbable-line > .tab-content {
                padding-bottom: 0;
            }

            .accordionConfig {
                background-color: #eee;
                color: #444;
                cursor: pointer;
                padding: 15px;
                width: 98%;
                border: none;
                text-align: left;
                outline: none;
                font-size: 15px;
                transition: 0.4s;
                margin-left: 1%;
            }

            /*.active, .accordionConfig:hover {
                        background-color: #ccc;
                    }*/

            .panelConfig {
                padding: 0 18px;
                display: none;
                background-color: white;
                overflow: hidden;
            }

            .dropdown-menu {
                cursor: pointer;
                /*width: 100%;*/
                margin-top: 11px;
                width: 354px;
                border-radius: 10px !important;
            }

                .dropdown-menu li a {
                    color: #337ab7;
                }



            .Selected {
                background-color: #337ab7 !important;
                color: white !important;
            }

            #UserNameLink {
                overflow: hidden;
                text-overflow: ellipsis;
                width: 80px;
                /*display: inline-block;*/
                line-height: 11px;
            }
            /*.k-grid-content.k-auto-scrollable {
                    max-height: 311px;
                }*/
            .LoadMoreResults {
                margin-top: -4px;
            }








            #tabInsertWrapper > .nav > li > a {
                padding-bottom: 5px;
            }

            #tabInsertWrapper > .nav {
                background-color: white !important;
            }

            #tabInsertWrapper > ul > li > a:hover, #tabinsert > .nav > li > a:focus {
                text-decoration: none !important;
                background-color: #31b0d5 !important;
                color: white !important;
            }

            #tabInsertWrapper > ul > li.active > a {
                text-decoration: none !important;
                background-color: #31b0d5 !important;
                color: white !important;
            }

            .MasterHeaderClass {
                display: none;
            }


            .DropPanelDisplay {
                display: none;
            }


            #ucDocTypesIndexs_ctl20_container {
                left: auto !important;
                top: 367px !important;
                position: fixed !important;
            }

            .swal-modal {
                margin-top: -230px !important;
            }
        </style>


    </asp:PlaceHolder>
    <title>Insertar documentos</title>
</head>

<body ng-app="app" ng-controller="insertController" ng-cloak>
    <div id="taskController" ng-controller="TaskController">
        <div id="appIdController" data-ng-controller="appController" ng-cloak>
            <div id="zmodal-header">
                <nav id="MasterHeader" class="navbar navbar-toggleable-sm navbar-fixed-top navbar-Main MasterHeaderClass">
                    <div class="container-fluid">

                        <div class="navbar-header" style="padding-top: 2px">
                            <button class="navbar-toggle collapsed navbar-toggler-right" type="button" data-toggle="collapse" data-target="#myNavigation" aria-controls="myNavigation" aria-expanded="false" aria-label="Toggle navigation">
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                            </button>

                            <img class="hidden-xs  clientlogo2 " style="margin-top: 0"></img>


                            <!-- Item Inicio -->
                            <button id="liHome" class="btn btn-navbar " style="background-color: transparent" onclick="navigateToHome();window.close();">
                                <a style="color: white; background-color: transparent" class="disable" id="btntabhome">
                                    <span class=" fa fa-home" style="margin-right: 5px"></span><span class="hidden-xs">Inicio</span>
                                </a>
                            </button>

                        </div>

                        <div class="navbar-collapse " id="myNavigation" style="color: white">

                            <ul class="nav navbar-nav navbar-right pull-right">
                                <li class="dropdown" id="tblUsuario">
                                    <a href="#" id="UsuarioDrop" class="dropdown-toggle" data-toggle="dropdown" style="margin-top: 5px; padding-top: 0; padding-bottom: 0; color: white; width: 208px;">
                                        <!--<span class="glyphicon glyphicon-user icon-color " style="padding-right: 5px;"></span>-->
                                        <img ng-cloak ng-show="thumphoto != '' " ng-src="{{'data:image/png;base64,' + thumphoto}}" class="img-circle ng-show" style="width: 37px; height: 33px; margin-left: -5px; margin-right: 10px;">
                                        <img ng-cloak ng-show="thumphoto == '' " src="../../Content/Images/icons/iconoObservacion2.jpg" alt="User Avatar" class="img-circle ng-show" style="width: 37px; height: 33px; margin-top: -5px; margin-left: -5px; margin-right: 10px;" />
                                        <span id="UserNameLink" title="{{CurrentUserName}}">{{CurrentApellido}}  {{CurrentUserName}}</span>
                                        <span><i class="caret"></i></span>
                                    </a>
                                    <ul class="dropdown-menu" id="dropdown-user">

                                        <div>
                                            <div>
                                                <img ng-cloak ng-show="thumphoto != '' " ng-src="{{'data:image/png;base64,' + thumphoto}}" class="img-circle ng-show" style="width: 115px; height: 99px; margin-left: 35%;">
                                                <img ng-cloak ng-show="thumphoto == '' " src="../../Content/Images/icons/iconoObservacion2.jpg" alt="User Avatar" class="img-circle ng-show" style="width: 115px; height: 99px; margin-left: 35%;" />
                                            </div>
                                            <div style="margin-top: 7px;">
                                                <p id="UserNameLink" class="font-weight-light" title="{{CurrentUserName}}  {{CurrentApellido}}" style="color: #999; display: block; width: 100% !important; text-align: center;">{{CurrentApellido}}  {{CurrentUserName}}</p>
                                                <p id="UserNameLink" class="font-weight-light" title="{{CurrentUsuario}}" style="color: #999; display: block; width: 100% !important; text-align: center;">{{CurrentUsuario}}</p>
                                                <p id="UserNameLink" class="font-weight-light" title="{{CurrentTelefono}}" style="color: #999; display: block; width: 100% !important; text-align: center;">Numero/extensión: {{CurrentTelefono}}</p>
                                                <p id="UserNameLink" class="font-weight-light" title="{{CurrentPuesto}}" style="color: #999; display: block; width: 100% !important; text-align: center;">{{CurrentPuesto}}</p>
                                            </div>
                                        </div>

                                        <div class="list-group" style="margin-top: 50px; margin-bottom: 0px;" ng-controller="UserController">
                                            <%--<a class=" UserMenuItem list-group-item list-group-item-action" data-toggle="modal" data-target="#ModalChangePassword" ng-if="getUserRight(191)">Cambiar Clave</a>--%>
                                            <a class="list-group-item list-group-item-action" ng-click="Logout()" style="text-decoration: none;" id="logOutSite">Cerrar Sesión</a>
                                            <%--<a class="UserMenuItem list-group-item list-group-item-action" onclick="AyudaSearch()" style="text-decoration: none;" id="Ayuda">Ayuda</a>--%>
                                            <%--<a class="UserMenuItem list-group-item list-group-item-action" onclick="CleanCache()" style="text-decoration: none; display: none" id="cleancache">Actualizar</a>--%>
                                            <a class="UserMenuItem list-group-item list-group-item-action " ng-click="clearAllCacheWithoutReload();Logout()" style="text-decoration: none;" id="cleanallcache">Limpiar Cache</a>
                                            <%--<a class="UserMenuItem list-group-item list-group-item-action" data-toggle="modal" data-target="#ModalCenterVersion" onclick="" style="text-decoration: none;" id="VersionZamba">Versión</a>--%>
                                        </div>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                    </div>
                </nav>
            </div>

        </div>


        <input type="hidden" id="insertMode" value="insert" />
        <!--Iframe Modal para la Ejecucion de Reglas. Dentro Carga el y que tiene el Switch para Executar cada Regla.-->
        <%--<div class="modal fade" id="openModalIFWF" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="overflow-y: hidden;">
        <div class="modal-dialog">
            <div class="modal-content" id="openModalIFContentWF" style="padding: 0">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" id="closeModalIFWF"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>

                    <h4 class="modal-title" id="modalFormTitleWF"></h4>
                </div>
                <div class="modal-body" id="modalFormHomeWF" style="padding: 0; height: 350px">
                    <div id="EntryRulesContent" style="z-index: 997; top: 0; right: 120px;">
                        <iframe id="WFExecForEntryRulesFrame" src="~/Views/WF/WFExecutionForEntryRules.aspx" style="height: 700px;" runat="server"></iframe>
                        <%--                            <iframe id="printFrame" src="" style="display:none;"></iframe>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
        <div id="openModalIF" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="position: -ms-page; margin-top: 20px;">
            <div class="modal-dialog">
                <div class="modal-content" id="openModalIFContent" style="width: 100%; height: 100%;">
                    <div class="modal-header">
                        <button type="button" class="close" onclick="closeModalIF()" id="closeModalIF"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                        <%--<button type="button" class="close" onclick="OpenModalIF.fullscreen(this);"><span aria-hidden="true">&#9633;</span></button>--%>
                        <h5 class="modal-title" id="modalFormTitle"></h5>
                    </div>
                    <div class="modal-body" id="modalFormHome">
                        <div id="modalDivBody"></div>
                        <iframe id="modalIframe" runat="server" style="width: 100%; height: 100%;" frameborder="0" allowtransparency="true"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <div id="InsertIframeBody" class="col-lg-12 body-Frm-Views-Insert">

            <div class="col-lg-12">
                <form id="aspnetForm" runat="server" class="form" enctype="multipart/form-data">
                    <UC6:UCWFExecution ID="UC_WFExecution" runat="server" height="200" width="200" />
                    <asp:HiddenField ID="hdnUserId" runat="server" />
                    <asp:HiddenField ID="hdnConnectionId" runat="server" />
                    <div id="dialoginfoetapa" style="display: none" title="Zamba Software">
                        <asp:Label ID="lblpopup" runat="server" Text="Contendra el texto de la etapa"></asp:Label>
                    </div>

                    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" ScriptMode="Release" AsyncPostBackTimeout="360000">
                    </asp:ScriptManager>


                    <asp:Panel runat="server" ID="pnlDatos">
                        <div class="row div-Frm-Views-Insert" style="overflow: visible; border-radius: 4px; width: auto; height: auto;">
                            <div class="col-md-6 col-xs-12" id="test">                              

                                <div class="noprint">
                                    <button id="InsertOptionBtn" title="Pestaña 'Archivos'" type="button" class="btnBlueMaterialInBoostrap" onclick="InsertOptionClick();" ng-click="insertMode = true">Insertar archivos</button>
                                    <button id="BarcodeOptionBtn" title="Pestaña 'Caratulas'" type="button" class="btnWhiteMaterialInBoostrap" onclick="BarcodeOptionClick();" ng-click="insertMode = false">Generar caratulas</button>
                                    <%--<a id="BtnClearIndex" class="btn btn-sm btn-outline-primary ng-scope" onclick="clearFilter()">
                                    <span class="glyphicon glyphicon-trash"></span><span class="hidden-xs" style="padding-left: 5px;">Limpiar</span>
                                      
                                </a>--%>
                                    <button id="BtnClearIndex" title="Vaciar campos" type="button" class="btn-circleMaterialInBoostrap" onclick="clearFilter()"><i class="fa fa-trash" aria-hidden="true"></i></button>
                                </div>

                             

                                <div id="BarcodePanel" ng-show="insertMode == false">
                                    <div class="form-group row">
                                        <div class="col-xs-6" style="margin-top: 45px">
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <div class="input-group">
                                                        <!--Nro Guia-->
                                                        <span class="input-group-addon" style="font-weight: bold !important; border: none; background-color: transparent; box-shadow: none">Caratula:</span>
                                                        <input type="text" id="nroBC" ng-model="barcodeId" size="1" class="form-control input-sm" disabled style="font-weight: 600 !important; background-color: white !important; border: none; background-color: transparent; box-shadow: none" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-11 col-xs-offset-1">
                                                    <span runat="server" id="currentUserName"></span>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-11 col-xs-offset-1">
                                                    <span id="currentTime"></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-6 pull-right">
                                            <input id="nroBC39" ng-model="barcodeId" class="form-control input-sm dataType length" style="font-family: 'IDAutomationHC39M'; font-size: 15pt; text-align: center; height: 75px; background: none; border: none; box-shadow: none;"
                                                readonly="readonly">
                                        </div>
                                    </div>

                                </div>


                                <div>
                                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <uc2:ucDocTypes runat="server" ID="ucDocTypes" EnableViewState="false" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div style="padding-top: 10px; overflow-x: hidden; position: relative; left: 5px;">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <uc3:ucDocTypesIndexs runat="server" ID="ucDocTypesIndexs" EnableViewState="false" />

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <br />
                            </div>
                            <div class="col-md-6 col-xs-12 ">
                                <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="DropPanel" class="DropPanelDisplay">
                                    <ContentTemplate>

                                        <div>
                                            <div>
                                                <asp:LinkButton runat="server" title="Subir archivos" Text="Insertar" ID="lnkInsertar" OnClientClick="SetInserting('Insertando archivos...');" OnClick="lnkInsertar_clic" ng-show="insertMode == true"
                                                    CssClass="btnBlueMaterialInBoostrap BtnInsertdisable" data-loading-text="<div style='display:inline-block' class='loader'></div><div style='display:inline-block; margin-left:10px'> Procesando </div> " />
                                            </div>
                                            <div style="overflow: auto">
                                                <uc1:ucUploadFile runat="server" ID="ucUploadFile" EnableViewState="false" />
                                                <asp:Label ID="lblMsj" runat="server" Visible="false" CssClass="error" Style="padding: 10px 0px 10px 0px"></asp:Label>
                                            </div>

                                        </div>



                                    </ContentTemplate>

                                </asp:UpdatePanel>

                               

                                <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="NavPanel">
                                    <ContentTemplate>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="nav nav-bar noprint">


                                    <asp:LinkButton runat="server" Text="Replicar" ID="lnkReplicar" OnClick="lnkReplicar_clic" ng-show="insertMode == true " disable
                                        CssClass="btn btn-primary btn-sm" data-loading-text="<div style='display:inline-block' class='loader'></div><div style='display:inline-block; margin-left:10px'> Procesando </div> " />

                                    <button type="button" id="lnkGenerarCaratula" title="Generar una caratula" data-ng-click="generateBC();" data-ng-show="insertMode == false"
                                        class="btngreenMaterialInBoostrap" data-loading-text="<div style='display:inline-block' class='loader'></div><div style='display:inline-block; margin-left:10px'> Procesando </div> ">
                                        Generar Caratula</button>

                                    <button type="button" id="lnkReimprimirCaratula" title="Reimprimir caratula" data-ng-click="PrintBC();" data-ng-show="insertMode == false && barcodeId > 0"
                                        class="btn btn-primary btn-sm" data-loading-text="<div style='display:inline-block' class='loader'></div><div style='display:inline-block; margin-left:10px'> Procesando </div> ">
                                        Reimprimir Caratula</button>

                                    <button type="button" id="lnkReplicarCaratula" title="Replicar caratula" data-ng-click="ReplicarBC();" data-ng-show="insertMode == false && barcodeId > 0"
                                        class="btn btn-primary btn-sm" data-loading-text="<div style='display:inline-block' class='loader'></div><div style='display:inline-block; margin-left:10px'> Procesando </div> ">
                                        Replicar Caratula</button>

                                    <asp:LinkButton ID="lnkRefresh" CssClass="btn btn-success btn-sm" OnClick="lnkRefresh_click" Text="Refrescar" runat="server">Cancelar</asp:LinkButton>


                                    <%--                                <button type="button" id="lnkhistory" data-ng-click="showHistory()" data-ng-show="insertMode == false" data-ng-controller="historycontroller"
                                    class="btn btn-primary btn-sm">
                                    <i class="fa fa-clock"></i>
                                </button>--%>



                                    <asp:LinkButton runat="server" Text="Cancelar" ID="lnkCancelar" OnClientClick="CloseInsert();" Visible="false"
                                        CssClass="btn btn-primary btn-sm" OnClick="lnkCancel_clic" />
                                </div>
                                <div class="row">
                                    <span id="insertInfo" class="insertInfo"></span>
                                    <asp:Label ID="lblInsertado" runat="server" Visible="false" Style="top: 20px; left: 120px; position: relative;"
                                        Font-Size="Medium" Font-Bold="true" Text="El documento ha sido insertado con exito"></asp:Label>
                                </div>

                            </div>
                        </div>
                    </asp:Panel>
                </form>
            </div>

        </div>

    </div>
    <style>
        .loader {
            border: 16px solid #f3f3f3;
            border-radius: 50%;
            border-top: 16px solid #3498db;
            width: 30px;
            height: 30px;
            -webkit-animation: spin 2s linear infinite; /* Safari */
            animation: spin 2s linear infinite;
        }

        /* Safari */
        @-webkit-keyframes spin {
            0% {
                -webkit-transform: rotate(0deg);
            }

            100% {
                -webkit-transform: rotate(360deg);
            }
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }
    </style>


    <script type="text/javascript">



        $(document).ready(function () {

            $(".insertAttribute.isRequired").focusout(function () {
                RemoveClaseBtnInsert();
            });

            $('select[class~="isRequired"]').click(function () {
                RemoveClaseBtnInsert();
            });


            $("#nroBC39").val("(" + $("#nroBC").val() + ")");
            display_ct();
            var scope = angular.element($("#InsertIframeBody")).scope();
            if ($("#insertMode").val() == "BC") {
                scope.insertMode = false;
            }
            else {
                scope.insertMode = true;
            }
        });


        function RemoveClaseBtnInsert() {
            var evaluator = false;

            if ($(".isRequired").length > 0) {
                for (var i = 0; i < $(".isRequired").length; i++) {

                    if ($($(".isRequired")[i]).val() != "" && $($(".isRequired")[i]).val() != undefined) {
                        evaluator = true;
                    } else {
                        evaluator = false;
                        break;
                    }
                }
            } else {
                evaluator = true;
            }


            if (evaluator && $("span[data-dz-name]").length >= 1) {
                $("#lnkInsertar").removeClass("BtnInsertdisable")
            } else if ($("#lnkInsertar").hasClass("BtnInsertdisable") != true) {
                $("#lnkInsertar").addClass("BtnInsertdisable")
            }

        }

        function AddClaseBtnInsert() {
            $("#lnkInsertar").addClass("BtnInsertdisable")
        }



        function RemoveClassDropPanelDisplay() {
            $("#DropPanel").removeClass("DropPanelDisplay")
        }



        function InsertOptionClick() {
            $("#BarcodeOptionBtn").removeClass('btn-primary');
            $("#BarcodeOptionBtn").addClass('btn-default');
            $("#InsertOptionBtn").removeClass('btn-default');
            $("#InsertOptionBtn").addClass('btn-primary');
            $("#insertMode").val("insert");
            $("#DropPanel").show();
        };

        function BarcodeOptionClick() {
            $("#InsertOptionBtn").removeClass('btn-primary');
            $("#InsertOptionBtn").addClass('btn-default');
            $("#BarcodeOptionBtn").removeClass('btn-default');
            $("#BarcodeOptionBtn").addClass('btn-primary');
            $("#insertMode").val("BC");
            $("#DropPanel").hide();
        };

        function display_c() {
            var refresh = 1000; // Refresh rate in milli seconds
            mytime = setTimeout('display_ct()', refresh)
        };

        function display_ct() {
            var x = new Date()
            document.getElementById('currentTime').innerHTML = x.toLocaleString();
            display_c();
        };

        function generateBC() {

        };

        function EnableInsert() {
            var scope = angular.element($("#InsertIframeBody")).scope();
            scope.FileAdded = true;
        };

        function SetInserting(text) {
            $("#lnkInsertar")[0].innerHTML = "Insertando Archivo....";
            $("#insertInfo").innerHTML = text;

        };

        function SetInsertOK() {
            $("#insertInfo").innerHTML = "Archivos Insertados.";

        };

        function clearFilter() {
            var countIndex = $(".insertAttribute").length;
            if (countIndex > 0) {
                for (var i = 0; i < countIndex; i++) {
                    if ($($(".insertAttribute")[i].nodeName).selector == "SELECT") {
                        $($(".insertAttribute")[i]).val("Seleccionar");
                    }
                    if ($($(".insertAttribute")[i].nodeName).selector == "INPUT" || $($(".insertAttribute")[i].nodeName).selector == "TEXTAREA") {
                        $($(".insertAttribute")[i]).val("");
                    }
                }
            }
        }

    </script>

</body>
</html>
<script type="text/javascript">
    function OpenTask(url, taskid, taskname) {
        window.opener.OpenTask(url, taskid, taskname);
        closeSelf();
    }

    function InsertSucces() {



    }

    function closeSelf() {
        window.close();
    }
    function maxlength(tb, max) {
        return (tb.value.length < max);
    }

    function FixAndPosition(objDlg) {
        objDlg.css("top", "100px");
        objDlg.css("position", "absolute");
        var zIndexMax = getMaxZ();
        $(objDlg).css({ "z-index": Math.round(zIndexMax) });
    }

      <%--      function OpentInsertingDialog() {
                $("#<%=pnlDatos.ClientID%>").hide();
                $("#divInserting").show();
                window.setInterval(function () { $("#btnForceClose").show(); }, 10000);

                if (parent != this)
                    parent.ShowLoadingAnimation();
            }

            function CloseInsertingDialog() {
                t = setTimeout("hideLoading();", 500);
            }--%>

         <%--   function hideLoading() {
                $("#divInserting").hide();
                $("#<%=pnlDatos.ClientID%>").show();
            $("#btnForceClose").hide();--%>

            //if (parent != this)
            //    parent.hideLoading();
            //}



            //function ModifyHeight() {
            //    var height = getHeightScreen();

        <%-- Height es 0 cuando viene del thick box, por ende le mando el alto justo y lo calculo por el 60% --%>
            //if (height < 1) {
            //    height = 572;
            //    height = height * 0.6;
            //} else {

            //    height = height * 0.5;
            //}

            //$("#divAttributes").height(height);
            //}
    <%-- Se coloco para esconder los datapicker ya que se superponian --%>
    $(document).ready(function () {
        setTimeout(function () {
            $("input").click(function () {
                $("#ui-datepicker-div").remove();
            })


        }, 100);
    });

    $('.btn').on('click', function () {
        var $this = $(this);
        $this.button('loading');
        setTimeout(function () {
            $this.button('reset');
        }, 3000);

    });
</script>


