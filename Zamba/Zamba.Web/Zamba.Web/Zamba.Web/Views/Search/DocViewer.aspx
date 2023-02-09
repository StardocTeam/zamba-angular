<%@ Page Language="C#" AutoEventWireup="true" Inherits="DocViewer" MasterPageFile="~/MasterBlankPage.master" EnableSessionState="True" ValidateRequest="true" CodeBehind="DocViewer.aspx.cs" %>

<%@ Register Src="~/Views/UC/Common/ZDeleteButton.ascx" TagName="UCZDeleteButton" TagPrefix="ZDeleteButton" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Reference Control="~/Views/UC/Viewers/FormBrowser.ascx" %>
<%@ Reference Control="~/Views/UC/Viewers/DocViewer.ascx" %>

<asp:Content ID="contentDocViewer" ContentPlaceHolderID="ContentPlaceHolder" runat="server" style="overflow: hidden">

     
    
    
    
    <link href="../../Content/Styles/normalize.min.css" rel="stylesheet" />


    <%: Scripts.Render("~/bundles/jqueryAddIns") %>
    <script src="../../Scripts/Zamba.js?v=169"></script>
    <script src="../../Scripts/Zamba.Associated.js?v=227"></script>

    <!-- Referencias Genericas -->
    <!-- CSS -->
    <link href="../../Content/partialSearchIndexs.css?v=168" rel="stylesheet" />

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

    <script src="../../Scripts/Zamba.Fn.min.js?v=168"></script>

    <script src="../../Scripts/app/search/Zamba.Search.Common.js?v=168"></script>

    <script src="../../Scripts/bootstrap.min.js"></script>



    <script src="../../app/zapp.js?v=168"></script>
    <script src="../../Scripts/Validations/Fields/fields.js"></script>
    <script src="../../GlobalSearch/services/authService.js"></script>
    <script src="../../GlobalSearch/search/authcontroller.js?v=133"></script>

    <%: Scripts.Render("~/bundles/ZScripts") %>
    <%: Scripts.Render("~/bundles/Scripts/masterblankScripts") %>
    <%: Scripts.Render("~/bundles/kendo") %>
    <%: Styles.Render("~/bundles/Styles/masterblankStyles") %>
    <%: Styles.Render("~/bundles/Styles/kendo")%>

    <link rel="stylesheet" href="../../Content/styles/tabber.css" type="text/css" />

    <script src="../../scripts/zamba.tabs.js?v=168" type="text/javascript"></script>

    <script src="../../scripts/tabber.js" type="text/javascript"></script>
    <%--    <script src="../../Scripts/Lista.js?v=168"></script>--%>



    

    <script src="../../GlobalSearch/services/angular-local-storage.min.js"></script>
    <%--<script src="../../GlobalSearch/services/authInterceptorService.js"></script>
    <script src="../../GlobalSearch/services/authService.js?v=168"></script>--%>

    <!--Referencias TimeLine-->
    <script src="../../app/timeLine/timeLineController.js?v=168"></script>
    <script src="../../app/timeLine/timelineservices.js?v=168"></script>

    <script src="../../app/timeLineNews/timeLineNewsController.js?v=168"></script>
    <script src="../../app/timeLineNews/timelineNewsservices.js?v=168"></script>
    <script src="../../app/timeLineHorizontal/timeLineHorizontal.js"></script>
    <script src="../../app/timeLineHorizontal/TimeLineHorizontalService.js"></script>

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

    
    <script src="../../app/DocumentViewer/DocumentViewerController.js?v=168"></script>
    <script src="../../app/DocumentViewer/DocumentViewerService.js?v=168"></script>

    <%--calednar--%>

    <%-- <link href='../../Scripts/calendar/fullcalendar.min.css' rel='stylesheet' />
    <link href='../../Scripts/calendar/fullcalendar.print.min.css' rel='stylesheet' media='print' />

    <script src='../../Scripts/fullcalendar.min.js'></script>
    <script src='../../Scripts/moment.js'></script>

    <script src="../../LineCalendar/LineCalendarController.js"></script>
    <script src="../../LineCalendar/LineCalendarService.js"></script>--%>

        <asp:HiddenField ID="hdnToken" runat="server"/>
        <asp:HiddenField runat="server" ID="hdnResultId"></asp:HiddenField>
        <asp:HiddenField runat="server" ID="hdnDocTypeId"></asp:HiddenField>
    


        <div id="Header" style="margin-top: 5px;">
             <a class="navbar-brand navbar-light bg-ligh navbar-fixed-top tasklogo" style="height:37px; width:100%;" href="#"></a>
            <asp:UpdatePanel runat="server" ID="updHeader">
                <ContentTemplate>
                    <%--                <button type="submit" id="BtnClose" runat="server" class="btn btn-danger btn-xs" style="margin-left:8px" tooltip="Cerrar"
                    onclick="closeTask()" tabindex="-1">
                    Cerrar             
                </button>--%>
                    <ZDeleteButton:UCZDeleteButton ID="deleteCtrl" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div id="tbDoc" class="" style="width: 100%; height: 100%">
            <asp:Panel runat="server" ID="pnlViewer" style="width: 100%; height: 100%; overflow: visible"></asp:Panel>
        </div>
 
    <script>
        function closeTask() {
            RefreshParentDataFromChildWindow();
            window.close();
        }
        // Felipe; Este codigo es para poner el titulo de la pagina, se toma del nombre de la tarea, solicitud de  MARSH
        $(document).ready(function () {
            if ($("#ctl00_ContentPlaceHolder_ctl01_docTB_lbltitulodocumento")[0] != undefined) {
                var nameTitle = $("#ctl00_ContentPlaceHolder_ctl01_docTB_lbltitulodocumento")[0].innerHTML;
                if (nameTitle != "" & nameTitle != null) {
                    window.document.title = nameTitle;
                }
            }
        });
        //$("#BtnClose").click(function(){
        //        window.close();
        //    });
    </script>
</asp:Content>
