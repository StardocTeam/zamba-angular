<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportBuilder.ascx.cs" Inherits="Web.ReportBuilder" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script type="text/javascript">
    $(document).ready(function () {
        if (!window.opener) {
            $("#gridContainer").height(300);
            $("#gridContainer").width(474);
        }
    });

    function showError() 
    {
        var ReportName = $("#ReporTitle").html();
        parent.showError('<%=this.ReportName %>');
    }
</script>
<div class="design">
    <span id="ReporTitle" class="title2"><%=this.ReportName %></span>
</div>
<asp:CheckBox ID="CheckBox2" Text="Ignorar paginado en exportacion" runat="server">
            </asp:CheckBox>
<input type="button" id="btnFullScreen" value="Pantalla completa" onclick="FullSizeReport(document.location);" />
<div id="gridContainer" style="overflow:auto">
    <telerik:RadGrid runat="server" ID="RadGrid1" AllowSorting="True"
        AllowFilteringByColumn="True" Width="98%" ClientSettings-Resizing-AllowResizeToFit="false"
        GroupingEnabled="true" BorderStyle="None" CellSpacing="0" GridLines="None" AllowPaging="true"
        ShowGroupPanel="True" Skin="Windows7" PageSize="20" OnItemCommand="RadGrid1_ItemCommand">
        <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
        <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
        </ClientSettings>
        <GroupingSettings ShowUnGroupButton="true" />
        <ExportSettings IgnorePaging="true" OpenInNewWindow="true" HideStructureColumns="true">
            <Pdf PageHeight="210mm" PageWidth="297mm" DefaultFontFamily="Arial Unicode MS"
                PageBottomMargin="20mm" PageTopMargin="20mm" PageLeftMargin="20mm" PageRightMargin="20mm" />
        </ExportSettings>
        <MasterTableView Width="100%" CommandItemDisplay="Top">
            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
            <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToCsvButton="false" ShowExportToPdfButton="true"
                ExportToExcelText="Exportar a Excel" ExportToPdfText="Exportar a PDF" ExportToWordText="Exportar a Word"/>
        </MasterTableView>
    </telerik:RadGrid>
</div>
  