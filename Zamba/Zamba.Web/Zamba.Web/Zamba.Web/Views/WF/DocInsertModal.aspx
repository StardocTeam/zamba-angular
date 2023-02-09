<%@ Page Language="C#" AutoEventWireup="false" Inherits="Views_WF_DocInsertModal" MasterPageFile="~/MasterBlankpage.Master" EnableViewState="false" CodeBehind="DocInsertModal.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterBlankPage.master" %>

<%@ Register Src="~/Views/UC/Viewers/FormBrowser.ascx" TagName="FormBrowser" TagPrefix="uc1" %>

<asp:Content ID="Content5" runat="server" ContentPlaceHolderID="ContentPlaceHolder">
    <link href="../../Content/Styles/normalize.min.css" rel="stylesheet" />

    <%: Scripts.Render("~/bundles/jqueryAddIns") %>
    <script src="../../Scripts/Zamba.js?v=169"></script>

    <!-- Referencias Genericas -->
    <!-- CSS -->
    <link href="../../Content/partialSearchIndexs.css?v=168" rel="stylesheet" />
    <link href="../../Scripts/KendoUI/styles/kendo.common.min.css" rel="stylesheet" />
    <link href="../../Scripts/KendoUI/styles/kendo.default.min.css" rel="stylesheet" />
    <link href="../../Scripts/KendoUI/styles/kendo.default.mobile.min.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../../Content/bootstrap.min.css">
    <link rel="stylesheet" href="../../Content/styles/tabber.css" type="text/css" />
    <link href="../../app/Grid/CSS/GridDirective.css?v=168" rel="stylesheet" />
    <link href="../../app/Grid/CSS/GridKendo.css?v=168" rel="stylesheet" />
    <link href="../../app/Grid/CSS/GridThumb.css?v=168" rel="stylesheet" />
    <link href="../../app/fullscreen/fullscreen.css" rel="stylesheet" />

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

    <script src="../../scripts/zamba.tabs.js?v=168" type="text/javascript"></script>
    <script src="../../scripts/tabber.js" type="text/javascript"></script>
    <script src="../../app/zapp.js?v=168"></script>
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
    
    <script src="../../app/fullscreen/fullscreen.js"></script>
    <script src="../../app/DocumentViewer/DocumentViewerController.js?v=168"></script>
    <script src="../../app/DocumentViewer/DocumentViewerService.js?v=168"></script>
    <script src="../../Scripts/Zamba.Associated.js?v=227"></script>

    <div style="align-content: center" id="Container">
        <div>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
        <div id="divFormContainer">
            <uc1:FormBrowser ID="FormBrowser" runat="server" />
        </div>
    </div>

    <script type="text/javascript">

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

    </script>

</asp:Content>
