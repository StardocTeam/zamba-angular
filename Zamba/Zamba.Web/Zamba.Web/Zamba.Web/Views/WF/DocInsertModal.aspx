<%@ Page Language="C#" AutoEventWireup="true" Inherits="Views_WF_DocInsertModal" MasterPageFile="~/MasterBlankpage.Master" EnableViewState="false" CodeBehind="DocInsertModal.aspx.cs" %>


<%@ Register Src="~/Views/UC/WF/UCWFExecution.ascx" TagName="UCWFExecution" TagPrefix="UC6" %>

<%@ Register Src="~/Views/UC/Viewers/FormBrowser.ascx" TagName="FormBrowser" TagPrefix="uc1" %>

<asp:Content ID="Content5" runat="server" ContentPlaceHolderID="ContentPlaceHolder">
<link id="lnkWebIcon" rel="shortcut icon" runat="server" type="image/x-icon" />
   
    <link href="../../Content/Styles/normalize.min.css" rel="stylesheet" />


    <%: Scripts.Render("~/bundles/jqueryAddIns") %>
    <script src="../../Scripts/Zamba.js?v=257"></script>
    
    <script src="../../Scripts/Validations/Fields/fields.js?v=248"></script>

    <%: Scripts.Render("~/bundles/ZScripts") %>

    <script src="../../Scripts/FormBrowser/Zamba.FormBrowser.js?v=248"></script>

    <%--<script src="../../Scripts/Push Notification/OneSignalSDK.js"></script>--%>
    <%--<script src="https://cdn.onesignal.com/sdks/OneSignalSDK.js" async=""></script>--%>
    <%--<script src="../../Scripts/Push Notification/PushNotification.js"></script>--%>



    <!-- Referencias Genericas -->
    <!-- CSS -->
    <link href="../../Content/partialSearchIndexs.css?v=248" rel="stylesheet" />

    <link href="../../Scripts/KendoUI/styles/kendo.common.min.css" rel="stylesheet" />
    <link href="../../Scripts/KendoUI/styles/kendo.default.min.css" rel="stylesheet" />
    <link href="../../Scripts/KendoUI/styles/kendo.default.mobile.min.css" rel="stylesheet" />


    <link rel="stylesheet" type="text/css" href="../../Content/bootstrap.min.css">

    <script src="../../Scripts/jquery-3.1.1.min.js"></script>
   

    <script src="../../Scripts/angular.min.js"></script>
    <script src="../../Scripts/angular-messages.js"></script>
    <script src="../../Scripts/angular-xeditable-0.8.1/js/xeditable.js"></script>
    <script src="../../Scripts/angular-sanitize.min.js"></script>
    <script src="../../Scripts/angular-animate.min.js"></script>

    <script src="../../Scripts/ng-embed/ng-embed.min.js"></script>

    <script src="../../Scripts/sweetalert.min.js"></script>

    <script src="../../Scripts/KendoUI/js/kendo.all.min.js"></script>

    <script src="../../Scripts/jq_datepicker.js"></script>

    <script src="../../Scripts/Zamba.Fn.min.js?v=248"></script>

    <script src="../../Scripts/app/search/Zamba.Search.Common.js?v=248"></script>

    <script src="../../Scripts/bootstrap.min.js"></script>




    <script src="../../Scripts/Validations/Fields/fields.js?v=248"></script>

    <%: Scripts.Render("~/bundles/ZScripts") %>
    <%: Scripts.Render("~/bundles/Scripts/masterblankScripts") %>
    <%: Scripts.Render("~/bundles/kendo") %>
    <%: Styles.Render("~/bundles/Styles/masterblankStyles") %>
    <%: Styles.Render("~/bundles/Styles/kendo")%>

    <link rel="stylesheet" href="../../Content/styles/tabber.css" type="text/css" />

    <script src="../../scripts/zamba.tabs.js?v=248" type="text/javascript"></script>

    <script src="../../scripts/tabber.js" type="text/javascript"></script>



     <script src="../../app/zapp.js?v=248"></script>
    <script src="../../app/i18n/i18n.js?v=248"></script>
    <script src="../../Scripts/bower-angular-translate-2.9.0.1/angular-translate.js" ></script>
        <!--locale translate-->



    <script src="../../GlobalSearch/services/angular-local-storage.min.js"></script>

    <!--Referencias TimeLine-->
    <script src="../../app/timeLine/timeLineController.js?v=248"></script>
    <script src="../../app/timeLine/timelineservices.js?v=248"></script>

    <script src="../../app/timeLineNews/timeLineNewsController.js?v=248"></script>
    <script src="../../app/timeLineNews/timelineNewsservices.js?v=248"></script>

    <script src="../../app/timeLineHorizontal/timeLineHorizontal.js?v=248"></script>
    <script src="../../app/timeLineHorizontal/TimeLineHorizontalService.js?v=248"></script>

    <!-- Referencias busqueda SLST para formulario -->
    <script src="../../app/FormSearchSLST/FormSearchSLSTService.js"></script>
    <script src="../../app/FormSearchSLST/FormSearchSLSTController.js"></script>

    <!-- Referencias Observaciones -->
    <script src="../../app/Observaciones/observacionesController.js?v=248"></script>
    <script src="../../app/Observaciones/observacionesservices.js?v=248"></script>

    <!-- Referencias Observaciones -->
    <script src="../../app/ObservacionesV2/observacionesNewController.js"></script>
    <script src="../../app/ObservacionesV2/observacionesNewServices.js"></script>

    <script src="../../Scripts/JsBarcode.all.min.js"></script>
    <!-- Referencias Grilla de Asociados -->
    <!-- CSS -->
    <link href="../../app/Grid/CSS/GridDirective.css?v=248" rel="stylesheet" />
    <link href="../../app/Grid/CSS/GridKendo.css?v=248" rel="stylesheet" />
    <link href="../../app/Grid/CSS/GridThumb.css?v=248" rel="stylesheet" />
    <!-- JS -->
    <script src="../../app/Grid/JS/ui-bootstrap-tpls-1.2.4.js"></script>

    <script src="../../app/Tasks/Controller/TaskController.js?v=248"></script>
    <script src="../../app/Tasks/Service/TaskService.js?v=248"></script>

    <script src="../../app/Grid/Controller/GridController.js?v=248"></script>
    <script src="../../app/Grid/Service/GridService.js?v=248"></script>

    <script src="../../DropFiles/dropfiles.js?v=248" type="text/javascript"></script>
    <script src="../../scripts/dropzone.js?v=248" type="text/javascript"></script>

    <script src="../../app/ruleExecition/RuleExecutionService.js?v=248"></script>

    <script src="../../Scripts/angular-filter/angular-filter.min.js"></script>

    <script src="../../app/app-views/controllers/forumctrl.js?v=248"></script>
    <script src="../../app/app-views/services/forumservice.js?v=248"></script>

    <link href="../../app/fullscreen/fullscreen.css" rel="stylesheet" />
    <script src="../../app/fullscreen/fullscreen.js"></script>

     <script src="../../app/user/Service/UserService.js?v=248"></script>
    <script src="../../app/DocumentViewer/DocumentViewerController.js?v=248"></script>
    <script src="../../app/DocumentViewer/DocumentViewerService.js?v=248"></script>
    <script src="../../Scripts/zamba.associated.js?v=248"></script>

    <script src="../../app/DocInsertModal/docInsertModalController.js?v=248"></script>
    <script src="../../app/DocInsertModal/docInsertModalServices.js?v=248"></script>


    <script src="../../GlobalSearch/services/authService.js?v=257"></script>

   <%-- <script src="../../Scripts/sweetalert2/sweetalert2.all.js"></script>
    <script src="../../Scripts/sweetalert2/sweetalert2.js"></script>
    <script src="../../Scripts/sweetalert2/sweetalert2.all.min.js"></script>--%>

    <link href="../../Content/zmaterial.css?v=248" rel="stylesheet" />
    <%--  <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">--%>
    <link href="../../Content/jquery-ui.min.css" rel="stylesheet" />
    <script src="../../Scripts/GenericCalendar.js"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                var $j = $.noConflict(true);
                setPhotoValueConfig();
                addcalendar();
                AutoCompleteClendarOff();
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
                var thisDomain = window.location.protocol + "//" + window.location.host  + "/" + window.location.pathname.split('/')[1];
                var baseUrl = location.origin.trim() + thisDomain + '/globalsearch/search/search.html?';
                var urlHome = baseUrl + "t=" + authDataJSON.token + "&user=" + authDataJSON.UserId + "#tabHome"
                window.open(urlHome, '_blank');
                //window.close();
            }


           
        </script>

    <div>

        <div id="appIdControllerDocInsert">

            <nav id="MasterHeaderDocInsert" class="navbar navbar-toggleable-sm navbar-fixed-top navbar-Main z-sidenav-header-insert-modal">
                <div class="container-fluid">

                    <div class="navbar-header" style="padding-top: 2px">
                        <!-- Item Inicio -->
                      <%--  <button id="liHome" class="btn btn-navbar " style="background-color: transparent" onclick="navigateToHome();">--%>
                            <a style="color: white; background-color: transparent; text-decoration:none" class="disable" id="btntabhome">
                                <img src="../../Content/Images/Icons/insert.png" style=" width: 27px; margin-left: 11px; vertical-align:bottom; margin-top: 10px;" class="ng-scope">
                                <span  style="vertical-align: text-bottom;font-size:14px;font-family:Helvetica, sans-serif;font-weight:bold" id="TabName" ng-cloak ng-controller="docInsertModalController" >{{DocInsertModalName}}</span>
                            </a>
                        <%--</button>--%>
                    </div>
                </div>
            </nav>
        </div>

    </div>

        <style>
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


            .z-sidenav-header-insert-modal {
                background-color: #337ab7 !important;
                color: white;
            }
        </style>
    <div style="align-content: center; margin-top: 120px" id="Container">
        <div>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
        <div id="divFormContainer">
        <UC6:UCWFExecution ID="UC_WFExecution" runat="server" height="200" width="200" />
            <uc1:FormBrowser ID="FormBrowser" runat="server" />
        </div>
    </div>
</asp:Content>
