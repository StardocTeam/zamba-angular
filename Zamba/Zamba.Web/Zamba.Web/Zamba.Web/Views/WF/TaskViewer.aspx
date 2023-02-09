<%@ Page Language="C#" ValidateRequest="true" AutoEventWireup="true" Inherits="TaskViewer" MasterPageFile="~/MasterBlankpage.Master" EnableViewState="false" EnableEventValidation="false" CodeBehind="TaskViewer.aspx.cs" %>

<%@ Register Src="~/Views/UC/WF/UCWFExecution.ascx" TagName="UCWFExecution" TagPrefix="UC6" %>
<%@ Register Src="~/Views/UC/Task/TaskHeader.ascx" TagName="UCTaskHeader" TagPrefix="UC3" %>
<%@ Register Src="~/Views/UC/Task/TaskDetail.ascx" TagName="UCTaskDetail" TagPrefix="UC5" %>



<asp:Content ID="Content3" ContentPlaceHolderID="header_js" runat="Server">
</asp:Content>

<asp:Content ID="Content5" runat="server" ContentPlaceHolderID="ContentPlaceHolder">

    <style type="text/css">
        .modal-headerDoShowForm.gj-draggable {
            background-color: #337ab7;
            color: white;
            padding-top: 5px;
            padding-bottom: 5px;
        }

        .doShowFormIframe {
            background-color: #337ab7;
            color: white;
            padding-top: 5px;
            padding-bottom: 5px;
        }

        .doShowFormDialog {
            padding: 0px !important;
        }

        #modalDoShowForm {
            margin: 0px;
            display: inline-table;
            z-index: 9999;
        }

        #modalDoShowFormHome {
            padding: 0px;
        }

        #modalDoShowFormContent {
            padding-top: 5px;
            padding-right: 5px;
            padding-bottom: 5px;
            padding-left: 5px;
        }

        #modalDoShowForm.modal-dialog {
            margin-bottom: 0px;
            margin-top: 0px;
        }

        #iframeDoShowForm {
            width: 100%;
            height: 100%;
        }

        .scrollBarShow {
            overflow: auto !important;
        }

        .modalDialogStyle {
            height: 90% !important;
            width: 96% !important;
        }

        .modal-headerDoShowForm#tittle {
            width: 95%;
        }

        #closeModalDoShowForm {
            margin-left: 5px;
            margin-right: 5px;
            opacity: 1 !important;
            color: #ffffff;
        }
    </style>

    <link href="../../Content/Styles/normalize.min.css" rel="stylesheet" />


    <%: Scripts.Render("~/bundles/jqueryAddIns") %>
    <script src="../../Scripts/Zamba.js?v=169"></script>
    <script src="../../Scripts/Zamba.Associated.js?v=227"></script>
    <%--<script src="../../Scripts/Push Notification/OneSignalSDK.js"></script>--%>
    <%--<script src="https://cdn.onesignal.com/sdks/OneSignalSDK.js" async=""></script>--%>
    <%--<script src="../../Scripts/Push Notification/PushNotification.js"></script>--%>



    <!-- Referencias Genericas -->
    <!-- CSS -->
    <link href="../../Content/partialSearchIndexs.css?v=168" rel="stylesheet" />

    <link href="../../Scripts/KendoUI/styles/kendo.common.min.css" rel="stylesheet" />
    <link href="../../Scripts/KendoUI/styles/kendo.default.min.css" rel="stylesheet" />
    <link href="../../Scripts/KendoUI/styles/kendo.default.mobile.min.css" rel="stylesheet" />


    <link rel="stylesheet" type="text/css" href="../../Content/bootstrap.min.css">
    <%--     <link rel="stylesheet" type="text/css" href="../../Content/site.css"/>--%>
    <!-- JS -->
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

    <script src="../../Scripts/Zamba.Fn.min.js?v=168"></script>

    <script src="../../Scripts/app/search/Zamba.Search.Common.js?v=168"></script>

    <script src="../../Scripts/bootstrap.min.js"></script>




    <script src="../../Scripts/Validations/Fields/fields.js?v=168"></script>

    <%: Scripts.Render("~/bundles/ZScripts") %>
    <%: Scripts.Render("~/bundles/Scripts/masterblankScripts") %>
    <%: Scripts.Render("~/bundles/kendo") %>
    <%: Styles.Render("~/bundles/Styles/masterblankStyles") %>
    <%: Styles.Render("~/bundles/Styles/kendo")%>

    <link rel="stylesheet" href="../../Content/styles/tabber.css" type="text/css" />

    <script src="../../scripts/zamba.tabs.js?v=168" type="text/javascript"></script>

    <script src="../../scripts/tabber.js" type="text/javascript"></script>


    <!-- Servicios de auntenticacion (authService.js y authcontroller.js) -->

    <script src="../../app/zapp.js?v=110"></script>
    <script src="../../GlobalSearch/services/authService.js"></script>
    <script src="../../GlobalSearch/search/authcontroller.js?v=133"></script>

    <script src="../../GlobalSearch/services/angular-local-storage.min.js"></script>

    <!--Referencias TimeLine-->
    <script src="../../app/timeLine/timeLineController.js?v=168"></script>
    <script src="../../app/timeLine/timelineservices.js?v=168"></script>

    <script src="../../app/timeLineNews/timeLineNewsController.js?v=168"></script>
    <script src="../../app/timeLineNews/timelineNewsservices.js?v=168"></script>

    <script src="../../app/timeLineHorizontal/timeLineHorizontal.js?v=168"></script>
    <script src="../../app/timeLineHorizontal/TimeLineHorizontalService.js?v=168"></script>

    <!-- Referencias Observaciones -->
    <script src="../../app/Observaciones/observacionesController.js?v=168"></script>
    <script src="../../app/Observaciones/observacionesservices.js?v=168"></script>

    <!-- Referencias Observaciones -->
    <script src="../../app/ObservacionesV2/observacionesNewController.js"></script>
    <script src="../../app/ObservacionesV2/observacionesNewServices.js"></script>

    <script src="../../Scripts/JsBarcode.all.min.js"></script>
    <!-- Referencias Grilla de Asociados -->
    <!-- CSS -->
    <link href="../../app/Grid/CSS/GridDirective.css?v=168" rel="stylesheet" />
    <link href="../../app/Grid/CSS/GridKendo.css?v=168" rel="stylesheet" />
    <link href="../../app/Grid/CSS/GridThumb.css?v=168" rel="stylesheet" />
    <!-- JS -->
    <script src="../../app/Grid/JS/ui-bootstrap-tpls-1.2.4.js"></script>

    <script src="../../app/Tasks/Controller/TaskController.js?v=168"></script>
    <script src="../../app/Tasks/Service/TaskService.js?v=168"></script>

    <script src="../../app/Grid/Controller/GridController.js?v=168"></script>
    <script src="../../app/Grid/Service/GridService.js?v=168"></script>

    <script src="../../DropFiles/dropfiles.js?v=168" type="text/javascript"></script>
    <script src="../../scripts/dropzone.js?v=168" type="text/javascript"></script>

    <script src="../../app/ruleExecition/RuleExecutionService.js?v=168"></script>

    <script src="../../Scripts/angular-filter/angular-filter.min.js"></script>

    <script src="../../app/app-views/controllers/forumctrl.js?v=168"></script>
    <script src="../../app/app-views/services/forumservice.js?v=168"></script>

    <link href="../../app/fullscreen/fullscreen.css" rel="stylesheet" />
    <script src="../../app/fullscreen/fullscreen.js"></script>


    <script src="../../app/DocumentViewer/DocumentViewerController.js?v=168"></script>
    <script src="../../app/DocumentViewer/DocumentViewerService.js?v=168"></script>


    <script>
        try {

            window.addEventListener('beforeunload', function (event) {
                try {

                    if (window.opener && !window.opener.closed) {
                        if (window.opener.document.getElementById('hdnEnableOpenerRefresh') != null)
                            if (window.opener.document.getElementById('hdnEnableOpenerRefresh').value != undefined)
                                if (window.opener.document.getElementById('hdnEnableOpenerRefresh').value == "true")

                                    window.opener.location.reload();
                    }

                } catch (e) {

                }
            });
        } catch (e) {

        }

    </script>

    <%--calednar--%>

    <%-- <link href='../../Scripts/calendar/fullcalendar.min.css' rel='stylesheet' />
    <link href='../../Scripts/calendar/fullcalendar.print.min.css' rel='stylesheet' media='print' />

    <script src='../../Scripts/fullcalendar.min.js'></script>
    <script src='../../Scripts/moment.js'></script>

    <script src="../../LineCalendar/LineCalendarController.js"></script>
    <script src="../../LineCalendar/LineCalendarService.js"></script>--%>


    <%--    CHAT--%>

    <link href="../../Content/CSS/TaskViewer.css?v=168" rel="stylesheet" />
    <%--  --%>

    <link href="../../Scripts/chosen_v1.8.7/chosen.css" rel="stylesheet" />
    <script src="../../Scripts/chosen_v1.8.7/chosen.jquery.js"></script>

    <div id="taskController" ng-controller="TaskController">
        <asp:HiddenField ID="hdnTaskID" runat="server" />
        <asp:HiddenField ID="hdnUserId" runat="server" />
        <asp:HiddenField ID="hdnPushNotification_player_id" runat="server" />
        <asp:HiddenField ID="hdnPushNotification_app_id" runat="server" />
        <asp:HiddenField ID="hdnConnectionId" runat="server" />
        <asp:HiddenField ID="hdnToken" runat="server"/>
        <input id="hdnDummyTabIndex" type="submit" style="position: absolute; top: -20px" />

        <UC6:UCWFExecution ID="UC_WFExecution" runat="server" height="200" width="200" />
        <iframe id="printFrame" style="display: none;"></iframe>
        <div class="" id="rowTaskHeader">
            <UC3:UCTaskHeader runat="server" ID="ucTaskHeader" />
        </div>
        <div id="rowTaskDetail">
            <UC5:UCTaskDetail runat="server" ID="ucTaskDetail" />
        </div>

        <div id="openModalIF" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="position: -ms-page; margin-top: 20px;">
            <div class="modal-dialog"">
                <div class="modal-content" id="openModalIFContent" style="width: 98%; height: 87%; left: 1%;">

                    <div class="modal-header headerModalColor">
                        <button type="button" class="close" data-dismiss="modal" id="closeModalIF"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
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

        <div id="modalDoShowForm" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="position: -ms-page; margin-top: 20px;">
            <div class="modal-dialog" style="margin: 0px;">
                <div class="modal-content" id="modalDoShowFormContent" style="width: 100%; height: 100%; left: 1%;">

                    <div class="modal-headerDoShowForm headerModalColorDoShowForm">
                                                <table style="width: 100%">
                            <tr>
                                <th colspan="2"><label id="tittle"></label></th>
                                <th></th>
                            </tr>
                        </table>

                    </div>

                    <div class="modal-body" id="modalDoShowFormHome">
                        <div id="modalDivBodyDoShowForm"></div>
                        <iframe id="iframeDoShowForm" style="width: 100%; height: 100%;" frameborder="0" allowtransparency="true"></iframe>
                    </div>

                </div>
            </div>
        </div>

    </div>

    
    <script type="text/javascript">
        $(document).ready(function () {
            var UrlParams = parent.getUrlParametersFromIframe();
            if (UrlParams.hasOwnProperty("modalmode")) {
                if (JSON.parse(UrlParams.modalmode)) {
                    $("#rowTaskHeader").hide();
                    $("#Toolbar-div").hide();
                    $(".Body-Master-Blanck").css("padding-top", "0px");
                    $("#modalDoShowForm").css("display", "none");
                    parent.showVerticalScrollBar();
                } else {
                    $("#rowTaskHeader").show();
                    $("#Toolbar-div").show();
                    $(".Body-Master-Blanck").css("padding-top", "99px");
                    $("#modalDoShowForm").css("display", "inline-table");
                    parent.hideVerticalScrollBar();
                }
            } else {
                $("#modalDoShowForm").css("display", "none");
                parent.showVerticalScrollBar();
            }

            $("#closeModalDoShowForm").on("click", function () {
                $("#modalDoShowForm").hide();
                $(".modal-backdrop.fade.in").hide();
                $(".ui-widget-overlay.ui-front").hide();

                $.ajax({
                    type: "GET",
                    url: "../../Services/TaskService.asmx/CleanSessionVariable?varName=CurrentFormID",
                    async: false,
                    success: function (response) {
                        console.log(response);
                    },
                    error: function (request, status, error) {
                        console.log(error);
                    }
                });
            });
        });

        window.onload = function () {
            document.querySelector(".modal-headerDoShowForm").querySelector("#tittle").innerHTML = document.querySelector("#ctl00_ContentPlaceHolder_ucTaskDetail_ctl00_docTB_lbltitulodocumento").innerHTML;
        }

        //Para quitar backDrop del modal
        $(".ui-widget-overlay.ui-front").each(function (i, elem) {
            $(elem).hide();
        });

        //Para quitar backDrop del modal (marsh 4.5)
        $(".modal-backdrop.fade.in").each(function (i, elem) {
            $(elem).hide();
        });

        function showVerticalScrollBar() {
            var BodyMasterBlanck = $(".Body-Master-Blanck");
            BodyMasterBlanck.addClass("scrollBarShow");
        }

        function hideVerticalScrollBar() {
            var BodyMasterBlanck = $(".Body-Master-Blanck");
            BodyMasterBlanck.removeClass("scrollBarShow");
        }

        function getUrlParameters(url) {
            if (url != undefined) {
                var pairs = url.split("?")[1].split("&");
                var res = {}, i, pair;
                for (i = 0; i < pairs.length; i++) {
                    pair = pairs[i].toLowerCase().split('=');
                    if (pair[1])
                        res[decodeURIComponent(pair[0])] = decodeURIComponent(pair[1]);
                }
                return res;
            } else {
                var pairs = window.location.search.substring(1).split(/[&?]/);
                var res = {}, i, pair;
                for (i = 0; i < pairs.length; i++) {
                    pair = pairs[i].toLowerCase().split('=');
                    if (pair[1])
                        res[decodeURIComponent(pair[0])] = decodeURIComponent(pair[1]);
                }
                return res;
            }
        }

        function getUrlParametersFromIframe() {
            var Url = $("#iframeDoShowForm").attr("src");
            return getUrlParameters(Url);
        }


        $('#rowTaskDetail, #rowTaskHeader').on('click', function () {
            CheckUserTimeOut();
        });

        var push_notification_player_id = document.getElementById('<%= hdnPushNotification_player_id.ClientID%>').value;
        var push_notification_app_id = document.getElementById('<%= hdnPushNotification_app_id.ClientID%>').value;

        $(document).ready(function () {

            $("#hdnDummyTabIndex").focus();
            $("#hdnDummyTabIndex").submit(function (evt) {
                evt.preventDefault();
            });

            $("#hdnDummyTabIndex").hide();

            keepSessionAlive();
        });

        function RefreshLocation() {
            document.location = document.location;
        }

        function getToolBTaskH() {
            return $("#rowTaskHeader").height();
        }

    </script>

    <script type="text/javascript">

        var thisDomain = location.origin.trim() + getValueFromWebConfig("ThisDomain");
        var ZambaWebRestApiURL = location.origin.trim() + getValueFromWebConfig("RestApiUrl") + "/api";
        var zambaApplication = "Zamba";
        var URLServer = thisDomain + "/ZambaChat/";
        var urlGlobalSearch = thisDomain + "/Views/Search/";
        var URLServer = thisDomain + "/ZambaChat/";
        var ZCollLnk ='<%=Zamba.Core.ZOptBusiness.GetValueOrDefault("ZCollLnk","https://localhost/zamba.web/zamba.collaboration/") %>';
        var zCollServer = '<%=Zamba.Core.ZOptBusiness.GetValueOrDefault("zCollServer","http://localhost/zamba.web/zambacollaborationserver/") %>';

    </script>

</asp:Content>
