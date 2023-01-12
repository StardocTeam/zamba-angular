<%@ Control Language="C#" AutoEventWireup="true" Inherits="Views_UC_Grid_ucHistoryGrid" CodeBehind="ucHistoryGrid.ascx.cs" %>

<link href="../../../Content/Styles/normalize.min.css" rel="stylesheet" />

<%: Scripts.Render("~/bundles/jqueryAddIns") %>
<!-- Referencias Genericas -->
<!-- CSS -->
<link href="../../../Content/partialSearchIndexs.css?v=248" rel="stylesheet" />

<link href="../../../Scripts/KendoUI/styles/kendo.common.min.css" rel="stylesheet" />
<link href="../../../Scripts/KendoUI/styles/kendo.default.min.css" rel="stylesheet" />
<link href="../../../Scripts/KendoUI/styles/kendo.default.mobile.min.css" rel="stylesheet" />

<link rel="stylesheet" type="text/css" href="../../../Content/bootstrap.min.css">
<!-- JS -->
<script src="../../../Scripts/jquery-3.1.1.min.js"></script>

<script src="../../../Scripts/angular.min.js"></script>
<script src="../../../Scripts/angular-messages.js"></script>
<script src="../../../Scripts/angular-xeditable-0.8.1/js/xeditable.js"></script>
<script src="../../../Scripts/angular-sanitize.min.js"></script>
<script src="../../../Scripts/angular-animate.min.js"></script>

<script src="../../../Scripts/ng-embed/ng-embed.min.js"></script>

<script src="../../../Scripts/sweetalert.min.js"></script>

<script src="../../../Scripts/KendoUI/js/kendo.all.min.js"></script>

<script src="../../../Scripts/jq_datepicker.js"></script>

<script src="../../../Scripts/Zamba.Fn.min.js?v=248"></script>

<script src="../../../Scripts/app/search/Zamba.Search.Common.js?v=248"></script>

<script src="../../../Scripts/bootstrap.min.js"></script>

<script src="../../../Scripts/moment.js"></script>

<script src="../../../Scripts/Validations/Fields/fields.js?v=248"></script>

<%: Scripts.Render("~/bundles/ZScripts") %>
<%: Scripts.Render("~/bundles/Scripts/masterblankScripts") %>
<%: Scripts.Render("~/bundles/kendo") %>
<%: Styles.Render("~/bundles/Styles/masterblankStyles") %>
<%: Styles.Render("~/bundles/Styles/kendo")%>

<link rel="stylesheet" href="../../../Content/styles/tabber.css" type="text/css" />

<script src="../../../scripts/zamba.tabs.js?v=248" type="text/javascript"></script>

<script src="../../../scripts/tabber.js" type="text/javascript"></script>



 <script src="../../app/zapp.js?v=248"></script>
    <script src="../../app/i18n/i18n.js?v=248"></script>
    <script src="../../Scripts/bower-angular-translate-2.9.0.1/angular-translate.js" ></script>
        <!--locale translate-->



    <script src="../../GlobalSearch/services/angular-local-storage.min.js"></script>

<!--Referencias TimeLine-->
<script src="../../../app/timeLine/timeLineController.js?v=248"></script>


<script src="../../../app/timeLineNews/timeLineNewsController.js?v=248"></script>


<script src="../../../app/timeLineHorizontal/timeLineHorizontal.js?v=248"></script>

<!-- Referencias Observaciones -->
<script src="../../../app/Observaciones/observacionesController.js?v=248"></script>

<!-- Referencias Observaciones -->
<script src="../../../app/ObservacionesV2/observacionesNewController.js"></script>

<script src="../../../Scripts/JsBarcode.all.min.js"></script>
<!-- Referencias Grilla de Asociados -->
<!-- CSS -->
<link href="../../../app/Grid/CSS/GridDirective.css?v=248" rel="stylesheet" />
<link href="../../../app/Grid/CSS/GridKendo.css?v=248" rel="stylesheet" />
<link href="../../../app/Grid/CSS/GridThumb.css?v=248" rel="stylesheet" />
<!-- JS -->
<script src="../../../app/Grid/JS/ui-bootstrap-tpls-1.2.4.js"></script>

<script src="../../../app/Tasks/Controller/TaskController.js?v=248"></script>

<script src="../../../app/Grid/Controller/GridController.js?v=248"></script>

<script src="../../../DropFiles/dropfiles.js?v=248" type="text/javascript"></script>
<script src="../../../scripts/dropzone.js?v=248" type="text/javascript"></script>

<script src="../../../Scripts/angular-filter/angular-filter.min.js"></script>

<script src="../../../app/app-views/controllers/forumctrl.js?v=248"></script>

<link href="../../../app/fullscreen/fullscreen.css" rel="stylesheet" />
<script src="../../../app/fullscreen/fullscreen.js"></script>

 <script src="../../app/user/Service/UserService.js?v=248"></script>
<script src="../../../app/DocumentViewer/DocumentViewerController.js?v=248"></script>
<script src="../../../app/DocumentViewer/DocumentViewerService.js?v=248"></script>

<link href="../../../Content/zmaterial.css?v=248" rel="stylesheet" />

<%--    CHAT--%>
<link href="../../../Content/CSS/TaskViewer.css?v=248" rel="stylesheet" />

<link href="../../../Scripts/chosen_v1.8.7/chosen.css" rel="stylesheet" />
<script src="../../../Scripts/chosen_v1.8.7/chosen.jquery.js"></script>

<link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css">

<style>

    #id01{
        padding-top: 80px !important;
    }

</style>

<script>
    function testViewMail() {
        document.getElementById('id01').style.display = 'block';
    }
</script>

 <script src="../../app/zapp.js?v=248"></script>
    <script src="../../app/i18n/i18n.js?v=248"></script>
    <script src="../../Scripts/bower-angular-translate-2.9.0.1/angular-translate.js" ></script>
        <!--locale translate-->



    <script src="../../GlobalSearch/services/angular-local-storage.min.js"></script>
<script src="../../../Scripts/sweetalert2/sweetalert2.all.js"></script>
<script src="../../../Scripts/sweetalert2/sweetalert2.js"></script>
<script src="../../../Scripts/sweetalert2/sweetalert2.all.min.js"></script>
<script src="../../../Scripts/Zamba.js?v=257"></script>
<script src="../../../app/DocumentViewer/DocumentViewerController.js"></script>
<script src="../../../app/DocumentViewer/DocumentViewerService.js"></script>
<asp:HiddenField ID="hdnAction" runat="server" />
<asp:HiddenField ID="hdnTarget" runat="server" />
<asp:Label runat="server" ID="lblMessage" Font-Size="14pt" Visible="false" ForeColor="Black"></asp:Label>
<asp:BoundField DataField="YourDateField" HeaderText="SomeHeader" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />

<div class="w3-container">
    <div id="id01" class="w3-modal">
        <div class="w3-modal-content" style="width: 80%">
            <div class="w3-container">
                <span onclick="document.getElementById('id01').style.display='none'" class="w3-button w3-display-topright" >&times;</span>
                <div ng-app="app" id="modalViewerMail" ng-controller="DocumentViewerController" >
                    <zamba-document-viewer height="mailHistory"></zamba-document-viewer>
                </div>
            </div>
        </div>
    </div>
</div>

<div style="margin-left:30px; margin-bottom:30px"> 
    <asp:GridView ID="grdHistory" runat="server" Width="100%"
        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" AllowPaging="true" OnPageIndexChanging="pageChangeEvent" HeaderStyle-HorizontalAlign="Center">
        <RowStyle CssClass="RowStyle" Wrap="false" />
        <EmptyDataRowStyle CssClass="EmptyRowStyle" Wrap="false" />
        <PagerStyle CssClass="PagerStyle" />
        <SelectedRowStyle CssClass="SelectedRowStyle" Wrap="false" />
        <HeaderStyle CssClass="HeaderStyle" Wrap="false" HorizontalAlign="Center" />
        <EditRowStyle CssClass="EditRowStyle" Wrap="false" />
        <AlternatingRowStyle CssClass="AltRowStyle" Wrap="false" />
    </asp:GridView>
</div>
<div id="divIframe" runat="server" style="padding-top: 10px">
    <iframe id="formBrowser" runat="server" frameborder="1" style="border: 1px; overflow-y: auto; display: none; height: 400px; width: 98%"
        scrolling="yes"></iframe>
    <asp:Label ID="lblAttachError" runat="server" Text="Error al visualizar el adjunto"
        Visible="False" ForeColor="Red"></asp:Label>
</div>


