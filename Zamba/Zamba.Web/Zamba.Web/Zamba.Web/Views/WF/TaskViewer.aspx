<%@ Page Language="C#" AutoEventWireup="true" Inherits="TaskViewer" MasterPageFile="~/MasterBlankpage.Master" EnableViewState="false" EnableEventValidation="false" CodeBehind="TaskViewer.aspx.cs" %>

<%@ Register Src="~/Views/UC/WF/UCWFExecution.ascx" TagName="UCWFExecution" TagPrefix="UC6" %>
<%@ Register Src="~/Views/UC/Task/TaskDetail.ascx" TagName="UCTaskDetail" TagPrefix="UC5" %>
<%@ Register Src="~/Views/UC/Task/TaskHeader.ascx" TagName="UCTaskHeader" TagPrefix="UC3" %>


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
            /*height: 400px;*/
            padding: 0px;
        }

        #modalDoShowFormContent {
            /*width: 600px;*/
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
        .modal-backdrop.in {
            opacity: 0 !important;
        }
    </style>


    <link href="../../Content/Styles/normalize.min.css" rel="stylesheet" />
    <%: Scripts.Render("~/bundles/jqueryAddIns") %>
    <script src="../../Scripts/zamba.js?v=263"></script>
    <script src="../../Scripts/FormBrowser/Zamba.FormBrowser.js?v=261"></script>
    <script src="../../Scripts/Zamba.associated.js?v=261"></script>
    <%--<script src="../../Scripts/Push Notification/OneSignalSDK.js"></script>--%>
    <%--<script src="https://cdn.onesignal.com/sdks/OneSignalSDK.js" async=""></script>--%>
    <%--<script src="../../Scripts/Push Notification/PushNotification.js"></script>--%>

    <link rel="stylesheet" type="text/css" href="../../Content/bootstrap.min.css">
    <script src="../../Scripts/bootstrap.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../../Content/css/ZClass.css" />
    <!-- Referencias Genericas -->
    <!-- CSS -->
    <link href="../../Content/partialSearchIndexs.css?v=261" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/2.4.0/jszip.min.js"></script>

    <link href="../../Scripts/KendoUI/styles/kendo.common.min.css" rel="stylesheet" />
    <link href="../../Scripts/KendoUI/styles/kendo.default.min.css" rel="stylesheet" />
    <link href="../../Scripts/KendoUI/styles/kendo.default.mobile.min.css" rel="stylesheet" />


    <%--     <link rel="stylesheet" type="text/css" href="../../Content/site.css"/>--%>
    <!-- JS -->
    <%--    <script src="../../Scripts/jquery-3.1.1.min.js"></script>--%>


    <script src="../../Scripts/angular.min.js"></script>
    <script src="../../Scripts/angular-messages.js"></script>
    <script src="../../Scripts/angular-xeditable-0.8.1/js/xeditable.js"></script>
    <script src="../../Scripts/angular-sanitize.min.js"></script>
    <script src="../../Scripts/angular-animate.min.js"></script>

    <script src="../../Scripts/ng-embed/ng-embed.min.js"></script>

    <script src="../../Scripts/sweetalert.min.js"></script>

    <script src="../../Scripts/KendoUI/js/kendo.all.min.js"></script>

    <script src="../../Scripts/jq_datepicker.js"></script>

    <script src="../../Scripts/Zamba.Fn.min.js?v=261"></script>

    <script src="../../Scripts/app/search/Zamba.Search.Common.js?v=261"></script>

    <script src="../../Scripts/Validations/Fields/fields.js?v=261"></script>

    <%: Scripts.Render("~/bundles/ZScripts") %>
    <%: Scripts.Render("~/bundles/Scripts/masterblankScripts") %>
    <%: Scripts.Render("~/bundles/kendo") %>
    <%: Styles.Render("~/bundles/Styles/masterblankStyles") %>
    <%: Styles.Render("~/bundles/Styles/kendo")%>
    <%: Styles.Render("~/bundles/Styles/fonts/glyphs")%>

    


    <link rel="stylesheet" href="../../Content/styles/tabber.css" type="text/css" />

    <script src="../../scripts/zamba.tabs.js?v=261" type="text/javascript"></script>

    <script src="../../scripts/tabber.js" type="text/javascript"></script>

    <script src="../../app/zapp.js?v=261"></script>
    <script src="../../app/i18n/i18n.js?v=261"></script>
    <script src="../../Scripts/bower-angular-translate-2.9.0.1/angular-translate.js"></script>


    
    <script src="../../app/FormSearchSLST/FormSearchSLSTService.js"></script>
    <script src="../../app/FormSearchSLST/FormSearchSLSTController.js"></script>

    <!--locale translate-->



    <script src="../../GlobalSearch/services/angular-local-storage.min.js"></script>

    <!--Referencias TimeLine-->
    <script src="../../app/timeLine/timeLineController.js?v=261"></script>
    <script src="../../app/timeLine/timelineservices.js?v=261"></script>

    <script src="../../app/timeLineNews/timeLineNewsController.js?v=261"></script>
    <script src="../../app/timeLineNews/timelineNewsservices.js?v=261"></script>

    <script src="../../app/timeLineHorizontal/timeLineHorizontal.js?v=261"></script>
    <script src="../../app/timeLineHorizontal/TimeLineHorizontalService.js?v=261"></script>

    <!-- Referencias Observaciones -->
    <script src="../../app/Observaciones/observacionesController.js?v=261"></script>
    <script src="../../app/Observaciones/observacionesservices.js?v=261"></script>

    <!-- Referencias Observaciones -->
    <script src="../../app/ObservacionesV2/observacionesNewController.js"></script>
    <script src="../../app/ObservacionesV2/observacionesNewServices.js"></script>

    <script src="../../Scripts/JsBarcode.all.min.js"></script>
    <!-- Referencias Grilla de Asociados -->
    <!-- CSS -->
    <link href="../../app/Grid/CSS/GridDirective.css?v=261" rel="stylesheet" />
    <link href="../../app/Grid/CSS/GridKendo.css?v=261" rel="stylesheet" />
    <link href="../../app/Grid/CSS/GridThumb.css?v=261" rel="stylesheet" />
    <!-- JS -->
    <script src="../../app/Grid/JS/ui-bootstrap-tpls-1.2.4.js"></script>

    <script src="../../app/Tasks/Controller/TaskController.js?v=261"></script>
    <script src="../../app/Tasks/Service/TaskService.js?v=261"></script>

    <script src="../../app/Grid/Controller/GridController.js?v=261"></script>
    <script src="../../app/Grid/Service/GridService.js?v=261"></script>

    <script src="../../DropFiles/dropfiles.js?v=261" type="text/javascript"></script>
    <script src="../../scripts/dropzone.js?v=261" type="text/javascript"></script>

    <script src="../../app/ruleExecition/RuleExecutionService.js?v=261"></script>

    <script src="../../Scripts/angular-filter/angular-filter.min.js"></script>

    <script src="../../app/app-views/controllers/forumctrl.js?v=261"></script>
    <script src="../../app/app-views/services/forumservice.js?v=261"></script>

    <link href="../../app/fullscreen/fullscreen.css" rel="stylesheet" />
    <script src="../../app/fullscreen/fullscreen.js"></script>

    <script src="../../app/user/Service/UserService.js?v=261"></script>
    <script src="../../app/DocumentViewer/DocumentViewerController.js?v=261"></script>
    <script src="../../app/DocumentViewer/DocumentViewerService.js?v=261"></script>


    <script src="../../app/GenericInputFromSlst/genericInputFromSlstController.js?v=261"></script>
    <script src="../../app/GenericInputFromSlst/genericInputFromSlstServices.js?v=261"></script>
    <%-- <script src="../../Scripts/sweetalert2/sweetalert2.all.js"></script>
    <script src="../../Scripts/sweetalert2/sweetalert2.js"></script>
    <script src="../../Scripts/sweetalert2/sweetalert2.all.min.js"></script>--%>
    <%--<script src="../../app/Login/loginController.js"></script>
    <script src="../../app/Login/loginServices.js"></script>--%>
    <link href="../../Content/zmaterial.css?v=261" rel="stylesheet" />

    <link href="../../Content/CSS/TaskViewer.css?v=261" rel="stylesheet" />

    <link href="../../Content/CSS/TaskViewer.css?v=261" rel="stylesheet" />

    <link href="../../Scripts/chosen_v1.8.7/chosen.css" rel="stylesheet" />
    <script src="../../Scripts/chosen_v1.8.7/chosen.jquery.js"></script>
    <script src="../../Scripts/GenericCalendar.js"></script>

    <script src="../../GlobalSearch/services/authService.js?v=261"></script>

    <script src="https://unpkg.com/gijgo@1.9.13/js/gijgo.min.js" type="text/javascript"></script>
    <link href="https://unpkg.com/gijgo@1.9.13/css/gijgo.min.css" rel="stylesheet" type="text/css" />    

    <div id="taskController" ng-controller="TaskController">
        <asp:HiddenField ID="hdnTaskID" runat="server" />
        <asp:HiddenField ID="hdnUserId" runat="server" />
        <asp:HiddenField ID="hdnPushNotification_player_id" runat="server" />
        <asp:HiddenField ID="hdnPushNotification_app_id" runat="server" />
        <asp:HiddenField ID="hdnConnectionId" runat="server" />
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
            <div class="modal-dialog modalDialogStyle">
                <div class="modal-content" id="openModalIFContent" style="width: 98%; height: 87%; left: 1%;">

                    <div class="modal-header headerModalColor">
                        <button type="button" class="close"  onclick="closeModalIFs()" id="closeModalIF"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
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
                                <th colspan="2">
                                    <label id="tittle"></label>
                                </th>
                                <th>
                                    <button type="button" class="close" data-dismiss="modal" id="closeModalDoShowForm"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button></th>
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
    <div id="exportPDFloading" class="modal" role="dialog">
        <div class="modal-dialog modal-dialog-centered" style="width: 300px;">
            <div class="modal-content" id="exportPDFContent" style="margin-top: 330px; text-align: center">
                <div class="modal-body">
                    <h4 class="modal-title" style="text-align: center">
                        Exportando PDF...
                        <i class="fa fa-circle-o-notch fa-spin" style="font-size:18px;"></i>
                    </h4>
                </div>
            </div>
        </div>
    </div>    


    <%-- <div id="angularLogin" ng-controller="loginController">
        <generic-login></generic-login>
    </div>--%>

    <script type="text/javascript">

        $(document).ready(function () {
            var UrlParams;
            var flag;
            if (!isDashBoardRRHH) {
                if (flag = (parent.name != "TAGGESTION")) {
                    UrlParams = getUrlParametersFromIframe();
                } else {
                    UrlParams = parent.getUrlParametersFromIframe();
                }
            }
           
            if (UrlParams.hasOwnProperty("modalmode")) {
                var isTrueSet = (UrlParams.modalmode === 'true');

                if (isTrueSet) {
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
                flag ? showVerticalScrollBar() : parent.showVerticalScrollBar();
            }

            $("#closeModalDoShowForm").on("click", function () {
                parent.window.location.reload();

                //TO DO: Correccion del doshowform que muestra el formulario tanto en el modla como atras.
                //$("#modalDoShowForm").hide();
                //$(".modal-backdrop.fade.in").hide();
                //$(".ui-widget-overlay.ui-front").hide();


                //$.ajax({
                //    type: "GET",
                //    url: "../../Services/TaskService.asmx/CleanSessionVariable?varName=CurrentFormID",
                //    contentType: "application/json; charset=utf-8",
                //    async: false,
                //    success: function (response) {
                //        console.log(response);

                //    },
                //    error: function (request, status, error) {
                //        console.log(error);
                //    }
                //});
            });

            if (UrlParams.hasOwnProperty("previewmode")) {
                if (UrlParams.previewmode != undefined) {
                    var isTrueSet = (UrlParams.previewmode === 'true');

                    if (isTrueSet) 
                        HideBtnsOnPreviewMode();                    
                }
            } else {
                $("#modalDoShowForm").css("display", "none");
                flag ? showVerticalScrollBar() : parent.showVerticalScrollBar();
            }

            $("#BarcodePanel").hide();
            checkDisabledRules();
        });

        function checkDisabledRules() {
            var taskId = getUrlParameters(window.location.href).taskid;

            var DisabledRulesByTask = sessionStorage.getItem("DisabledRules-" + taskId);


            if (DisabledRulesByTask != undefined) {

                DisabledRulesByTask = DisabledRulesByTask.replaceAll('}"', '}').replaceAll('"{', '{');
                DisabledRulesByTask = JSON.parse(DisabledRulesByTask);

                DisabledRulesByTask.forEach(function (item) {
                    EnableDisableRule(item.currentRuleId, item.IsEnabled);
                });
            }
        }

        window.onload = function () {
            try {
            document.querySelector(".modal-headerDoShowForm").querySelector("#tittle").innerHTML = document.querySelector("#ctl00_ContentPlaceHolder_ucTaskDetail_ctl00_docTB_lbltitulodocumento").innerHTML;
            } catch (e) {
                console.error(e);
            }
        }

        //Para quitar backDrop del modal
        $(".ui-widget-overlay.ui-front").each(function (i, elem) {
            $(elem).hide();
        });

        //Para quitar backDrop del modal (marsh 4.5)
        $(".modal-backdrop.fade.in").each(function (i, elem) {
            $(elem).hide();
        });

        $('#rowTaskDetail, #rowTaskHeader').on('click', function () {
            //Este codigo se comenta ya que queda inabilitado por servicio q verifica el login automatico
           /* CheckUserTimeOut();*/
        });

        var push_notification_player_id = document.getElementById('<%= hdnPushNotification_player_id.ClientID%>').value;
        var push_notification_app_id = document.getElementById('<%= hdnPushNotification_app_id.ClientID%>').value;

        $(document).ready(function () {
            localStorage.setItem("PreviewTaskChanged", false);
            $("#hdnDummyTabIndex").focus();
            $("#hdnDummyTabIndex").submit(function (evt) {
                evt.preventDefault();
            });

            $("#hdnDummyTabIndex").hide();

            $(".input-group").find("[name*='zamba_index']").bind("change", function () {
                localStorage.setItem("PreviewTaskChanged", true);
            });

            keepSessionAlive();
            AutoCompleteClendarOff();
            clearTabSelectedOnLocalStorage();
            focusTab();

        });

        function showVerticalScrollBar() {
            var BodyMasterBlanck = $(".Body-Master-Blanck");
            BodyMasterBlanck.addClass("scrollBarShow");
        }

        function hideVerticalScrollBar() {
            var BodyMasterBlanck = $(".Body-Master-Blanck");
            BodyMasterBlanck.removeClass("scrollBarShow");
        }

        function RefreshLocation() {
            document.location = document.location;
        }

        function getToolBTaskH() {
            return $("#rowTaskHeader").height();
        }

        function setCurrentForm(formId) {
            $("#ctl00_ContentPlaceHolder_ucTaskDetail_HiddenCurrentFormID").val(formId);
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


        function focusTab() {
            var docId = GetDOCID();
            var docTypeId = GetDocTypeId();
            var varNameLocalStorage = "TaskViewer-LastTabSelected" + docId.toString() + "-" + docTypeId.toString();

            var LastTabSelected = JSON.parse(localStorage.getItem(varNameLocalStorage));

            var selectedTab = $("li").filter(function () {
        //        return $(this).text() == LastTabSelected.selectedTab;
            });

            $(selectedTab[0]).find('a').click();
        }

        function clearTabSelectedOnLocalStorage() {
            for (var i = 0, len = localStorage.length; i < len; i++) {
                var key = localStorage.key(i);

                if (key.startsWith("TaskViewer-LastTabSelected")) {
                    var value = JSON.parse(localStorage[key]);

                    if (value.docId == getElementFromQueryString("docid") && value.docTypeId == getElementFromQueryString("DocType")) {

                        var expirationDateTime = moment(value.expirationDateTime);
                        if (expirationDateTime <= Date.now()){
                            localStorage.removeItem(key);
                        }
                    }
                }
            }
        }

        function setTabSelectedOnLocalStorage() {
            var selectedTab = $($($(".nav-tabs")[0]).find("li.active")[0]).find("a")[0].innerText;
            var expirationDateTime = moment().add(1, 'd').format("YYYY-MM-DD hh:mm:ss");
            var docId = GetDOCID();
            var docTypeId = GetDocTypeId();
            var varNameLocalStorage = "TaskViewer-LastTabSelected" + docId.toString() + "-" + docTypeId.toString();

            var LastTabSelected = {
                selectedTab: selectedTab,
                expirationDateTime: expirationDateTime,
                docId: docId,
                docTypeId: docTypeId
            }

            var JSONLastTabSelected = JSON.stringify(LastTabSelected);
            localStorage.setItem(varNameLocalStorage, JSONLastTabSelected);
        }

        function HideBtnsOnPreviewMode() {
            $("#ctl00_ContentPlaceHolder_ucTaskDetail_ctl00_docTB_btnEmail").hide();
            $("#ctl00_ContentPlaceHolder_ucTaskDetail_ctl00_docTB_btnAttach").hide();
            $("#ctl00_ContentPlaceHolder_ucTaskDetail_ctl00_docTB_BtnShowIndexs").hide();
            $("#ctl00_ContentPlaceHolder_ucTaskHeader_UACCell").hide();
            $("#liMails").hide();
            $("#liHistory").hide();
            $("#liAsociated").hide();
            $("#BtnFinalizar").hide();
            $("#BtnIniciar").hide();
            $("#liCerrar").hide();

        }

        $(".nav-tabs").click(function () {
            setTimeout(setTabSelectedOnLocalStorage, 1000);
        });
        function thumbZoom(t) {
            
        }



    </script>

    <script type="text/javascript">

        var thisDomain = window.location.protocol + "//" + window.location.host + "/" + window.location.pathname.split('/')[1]; var ZambaWebRestApiURL = location.origin.trim() + getValueFromWebConfig("RestApiUrl") + "/api";
        var zambaApplication = "Zamba";
        var URLServer = thisDomain + "/ZambaChat/";
        var urlGlobalSearch = thisDomain + "/Views/Search/";
        var URLServer = thisDomain + "/ZambaChat/";
        var ZCollLnk ='<%=Zamba.Core.ZOptBusiness.GetValueOrDefault("ZCollLnk","https://localhost/zamba.web/zamba.collaboration/") %>';
        var zCollServer = '<%=Zamba.Core.ZOptBusiness.GetValueOrDefault("zCollServer","http://localhost/zamba.web/zambacollaborationserver/") %>';

    </script>



</asp:Content>


