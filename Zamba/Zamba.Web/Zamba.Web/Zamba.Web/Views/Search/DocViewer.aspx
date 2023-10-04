<%@ Page Language="C#" AutoEventWireup="true" Inherits="DocViewer" MasterPageFile="~/MasterBlankPage.master" EnableSessionState="True" ValidateRequest="false" CodeBehind="DocViewer.aspx.cs" %>

<%@ Register Src="~/Views/UC/Common/ZDeleteButton.ascx" TagName="UCZDeleteButton" TagPrefix="ZDeleteButton" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Reference Control="~/Views/UC/Viewers/FormBrowser.ascx" %>
<%@ Reference Control="~/Views/UC/Viewers/DocViewer.ascx" %>

<asp:Content ID="contentDocViewer" ContentPlaceHolderID="ContentPlaceHolder" runat="server" style="overflow: hidden">





    <link href="../../Content/Styles/normalize.min.css" rel="stylesheet" />


    <%: Scripts.Render("~/bundles/jqueryAddIns") %>
    <script src="../../Scripts/Zamba.js?v=263"></script>
    <script src="../../Scripts/FormBrowser/Zamba.FormBrowser.js?v=261"></script>
    <script src="../../Scripts/Zamba.associated.js?v=261"></script>
    <link rel="stylesheet" type="text/css" href="../../Content/css/ZClass.css" />

    <!-- Referencias Genericas -->
    <!-- CSS -->
    <link href="../../Content/partialSearchIndexs.css?v=261" rel="stylesheet" />

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/2.4.0/jszip.min.js"></script>

    <link href="../../Scripts/KendoUI/styles/kendo.common.min.css" rel="stylesheet" />
    <link href="../../Scripts/KendoUI/styles/kendo.default.min.css" rel="stylesheet" />
    <link href="../../Scripts/KendoUI/styles/kendo.default.mobile.min.css" rel="stylesheet" />

    <link rel="stylesheet" type="text/css" href="../../Content/bootstrap.min.css">

    <!-- JS -->
    <script src="../../Scripts/jquery-3.1.1.min.js"></script>


    <script src="../../Scripts/angular.min.js"></script>
    <script src="../../Scripts/angular-messages.js"></script>
    <script src="../../Scripts/angular-xeditable-0.8.1/js/xeditable.min.js"></script>
    <script src="../../Scripts/angular-sanitize.min.js"></script>
    <script src="../../Scripts/angular-animate.min.js"></script>

    <script src="../../Scripts/ng-embed/ng-embed.min.js"></script>

    <script src="../../Scripts/sweetalert.min.js"></script>

    <script src="../../Scripts/KendoUI/js/kendo.all.min.js"></script>

    <script src="../../Scripts/jq_datepicker.js"></script>

    <script src="../../Scripts/Zamba.Fn.min.js?v=261"></script>

    <script src="../../Scripts/app/search/Zamba.Search.Common.js?v=261"></script>

    <script src="../../Scripts/bootstrap.min.js"></script>




    <script src="../../Scripts/Validations/Fields/fields.js?v=261"></script>

    <%: Scripts.Render("~/bundles/ZScripts") %>
    <%: Scripts.Render("~/bundles/Scripts/masterblankScripts") %>
    <%: Scripts.Render("~/bundles/kendo") %>
    <%: Styles.Render("~/bundles/Styles/masterblankStyles") %>
    <%: Styles.Render("~/bundles/Styles/kendo")%>

    <link href="../../Content/zmaterial.css?v=261" rel="stylesheet" />

    <link rel="stylesheet" href="../../Content/styles/tabber.css" type="text/css" />

    <script src="../../scripts/zamba.tabs.js?v=261" type="text/javascript"></script>

    <script src="../../scripts/tabber.js" type="text/javascript"></script>
    <%--    <script src="../../Scripts/Lista.js?v=261"></script>--%>



    <script src="../../app/zapp.js?v=261"></script>
    <script src="../../app/i18n/i18n.js?v=261"></script>
    <!--locale translate-->
    <script src="../../Scripts/bower-angular-translate-2.9.0.1/angular-translate.js"></script>

    <script src="../../GlobalSearch/services/angular-local-storage.min.js"></script>
  
    <!--Referencias TimeLine-->
    <script src="../../app/timeLine/timeLineController.js?v=261"></script>
    <script src="../../app/timeLine/timelineservices.js?v=261"></script>

    <script src="../../app/timeLineNews/timeLineNewsController.js?v=261"></script>
    <script src="../../app/timeLineNews/timelineNewsservices.js?v=261"></script>
    <script src="../../app/timeLineHorizontal/timeLineHorizontal.js"></script>
    <script src="../../app/timeLineHorizontal/TimeLineHorizontalService.js"></script>

    <!-- Referencias Observaciones -->
    <script src="../../app/Observaciones/observacionesController.js?v=261"></script>
    <script src="../../app/Observaciones/observacionesservices.js?v=261"></script>
    <!-- Referencias busqueda SLST para formulario -->
    <script src="../../app/FormSearchSLST/FormSearchSLSTService.js"></script>
    <script src="../../app/FormSearchSLST/FormSearchSLSTController.js"></script>

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

    <script src="../../app/user/Service/UserService.js?v=261"></script>
    <script src="../../app/DocumentViewer/DocumentViewerController.js?v=261"></script>
    <script src="../../app/DocumentViewer/DocumentViewerService.js?v=261"></script>

    <script src="../../app/GenericInputFromSlst/genericInputFromSlstController.js?v=261"></script>
    <script src="../../app/GenericInputFromSlst/genericInputFromSlstServices.js?v=261"></script>

    <script src="../../GlobalSearch/services/authService.js?v=261"></script>

    <asp:HiddenField runat="server" ID="hdnResultId"></asp:HiddenField>
    <asp:HiddenField runat="server" ID="hdnDocTypeId"></asp:HiddenField>

    <div id="Header" style="margin-top: 5px;">
        <%--        <a class="navbar-brand navbar-light bg-ligh navbar-fixed-top tasklogo" style="height: 37px; width: 100%;" href="#"></a>--%>
        <%--        <asp:UpdatePanel runat="server" ID="updHeader">
            <ContentTemplate>
                <ZDeleteButton:UCZDeleteButton ID="deleteCtrl" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
    <div id="openModalIF" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="position: -ms-page; margin-top: 20px;">
        <div class="modal-dialog">
            <div class="modal-content" id="openModalIFContent" style="width: 98%; height: 92%;">
                <div class="modal-header bgdColorHeader">
                    <button type="button" class="close" data-dismiss="modal" id="closeModalIF"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <%--<button type="button" class="close" onclick="OpenModalIF.fullscreen(this);"><span aria-hidden="true">&#9633;</span></button>--%>
                    <h5 class="modal-title titleColor" id="modalFormTitle"></h5>
                </div>
                <div class="modal-body" id="modalFormHome">
                    <div id="modalDivBody"></div>
                    <iframe id="modalIframe" runat="server" style="width: 98%; height: 92%;" frameborder="0" allowtransparency="true"></iframe>
                </div>
            </div>
        </div>
    </div>
    <div id="tbDoc" class="" style="width: 100%; height: 100%">
        <asp:Panel runat="server" ID="pnlViewer" Style="width: 100%; height: 100%; overflow: visible"></asp:Panel>
    </div>
    <div onclick="this.style.display='none'"
        onmouseout="this.style.opacity=.8"
        onmouseover="this.style.opacity=1"
        id="popupslocked"
        class="popupslocked-div">
        <a class="popupslocked-a"
            id="linkDownloadManual"
            href="#" download="">¡ALERTA! Su navegador tiene las ventanas emergentes bloqueadas impidiendo que Zamba funcione correctamente. Haga click aquí para leer el manual de instrucciones y solucionar el problema.
        </a>
    </div>
    <script>
        var docDivClientID = '<%=pnlViewer.FindControl("pnlViewer").ClientID%>';
        
        function closeTask() {
            RefreshParentDataFromChildWindow();
            window.close();
        }
        // Felipe; Este codigo es para poner el titulo de la pagina, se toma del nombre de la tarea, solicitud de  MARSH
        $(document).ready(function () {
            var UrlParams;
            var flag;

            if (flag = (parent.name != "TAGGESTION")) {
                UrlParams = getUrlParametersFromIframe();
            } else {
                UrlParams = parent.getUrlParametersFromIframe();
            }

            if ($("#ctl00_ContentPlaceHolder_ctl01_docTB_lbltitulodocumento")[0] != undefined) {
                var nameTitle = $("#ctl00_ContentPlaceHolder_ctl01_docTB_lbltitulodocumento")[0].innerHTML;
                if (nameTitle != "" & nameTitle != null) {
                    window.document.title = nameTitle;
                }
            }

            $("body").css("overflow", "hidden");
            document.getElementById("Toolbar").style.marginTop = "0";

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
        });

        function getUrlParametersFromIframe() {
            var Url = $("#iframeDoShowForm").attr("src");
            return getUrlParameters(Url);
        }

        function HideBtnsOnPreviewMode() {
            $("#ctl00_ContentPlaceHolder_ucTaskHeader_UACCell").hide();

            $("#ctl00_ContentPlaceHolder_ctl00_docTB_btnEmail").hide();
            $("#ctl00_ContentPlaceHolder_ctl00_docTB_btnAttach").hide();
            $("#ctl00_ContentPlaceHolder_ctl00_docTB_BtnShowIndexs").hide();

            $("#liMails").hide();
            $("#liHistory").hide();
            $("#liAsociated").hide();
            $("#BtnFinalizar").hide();
            $("#BtnIniciar").hide();
            $("#liCerrar").hide();
        }
    </script>
    <style>
        #Toolbar {
            min-height: 50px !important;
            margin-top: 0px;
        }

        #navbarSupportedContent {
            margin-top: 17px !important;
        }

        .bgdColorHeader{
            background-color: #3175b0;
        }

        .titleColor{
            color: white;
        }
    </style>
</asp:Content>
