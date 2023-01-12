<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Views.Reports.Reports" EnableEventValidation="false" Codebehind="Reports.aspx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Zamba.Reportes.Personalizados</title>
    <style type="text/css">
        .filterDiv
        {
            margin: 20px 0px 10px 0px;
        }
        
        .DisplayNone
        {
            display: none;
        }
        
        .rtbIcon
        {
            display:none;   
         }
    </style>
</head>
<body class="BODY">
    <form id="form1" runat="server">
        <a id="taskAnchor" style="display: none"></a>
    <div>
        <asp:ScriptManager ID="RadScriptManager1" runat="server"  ScriptMode="Release" >
            <Scripts>
            </Scripts>
        </asp:ScriptManager>
       <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1">
            <strong>Procesando...
                <img src="../../../Content/Images/loading.gif" alt="" />
            </strong>
        </telerik:RadAjaxLoadingPanel>
        <div class="module" id="module" style="height: auto; width: 100%; color: White">
            <div>
                <asp:Button ID="Button2" runat="server" Text="Exportar a Excel" OnClick="Button2Click" />
                <asp:Button ID="Button3" runat="server" Text="Exportar a CSV" OnClick="Button3Click" />
                <asp:CheckBox ID="CheckBox2" Text="Ignorar paginado(exportar todas las paginas)"
                    runat="server"></asp:CheckBox>
            </div>
            <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel1">
                <div class="filterDiv">
                    <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="RadGrid1" ShowApplyButton="True"
                        OnPreRender="RadFilter1PreRender" AddExpressionToolTip="Agregar Filtro" AddGroupToolTip="Agregar Grupo"
                        ApplyButtonText="Aplicar Filtro" Localization-FilterFunctionBetween="Entre" Localization-FilterFunctionContains="Contiene"
                        Localization-FilterFunctionDoesNotContain="No Contiene" Localization-FilterFunctionEndsWith="Termina"
                        Localization-FilterFunctionEqualTo="=" Localization-FilterFunctionGreaterThan=">"
                        Localization-FilterFunctionGreaterThanOrEqualTo=">" Localization-FilterFunctionIsEmpty="Vacio"
                        Localization-FilterFunctionIsNull="Nulo" Localization-FilterFunctionLessThan="<"
                        Localization-FilterFunctionLessThanOrEqualTo="=<" Localization-FilterFunctionNotBetween="No Entre"
                        Localization-FilterFunctionNotEqualTo="<>" Localization-FilterFunctionNotIsEmpty="No Vacio"
                        Localization-FilterFunctionNotIsNull="No Nulo" Localization-FilterFunctionStartsWith="Empieza"
                        Localization-GroupOperationAnd="Y" Localization-GroupOperationOr="O" Localization-GroupOperationNotAnd="Y No"
                        Localization-GroupOperationNotOr="O No" CssClass="RadFilter RadFilter_Default "
                        Skin="Windows7" OnApplyExpressions="ApplyExpressions">
                        <Localization GroupOperationAnd="Y" GroupOperationOr="O" GroupOperationNotAnd="Y No"
                            GroupOperationNotOr="O No" FilterFunctionContains="Contiene" FilterFunctionDoesNotContain="No Contiene"
                            FilterFunctionStartsWith="Empieza" FilterFunctionEndsWith="Termina" FilterFunctionEqualTo="="
                            FilterFunctionNotEqualTo="&lt;&gt;" FilterFunctionGreaterThan="&gt;" FilterFunctionLessThan="&lt;"
                            FilterFunctionGreaterThanOrEqualTo="=&gt;" FilterFunctionLessThanOrEqualTo="=&lt;"
                            FilterFunctionBetween="Entre" FilterFunctionNotBetween="No Entre" FilterFunctionIsEmpty="Vacio"
                            FilterFunctionNotIsEmpty="No Vacio" FilterFunctionIsNull="Nulo" FilterFunctionNotIsNull="No Nulo">
                        </Localization>
                    </telerik:RadFilter>
                </div>
                <telerik:RadGrid runat="server" ID="RadGrid1" DataSourceID="SqlDataSource2" AllowPaging="True"
                    AllowSorting="True" AllowFilteringByColumn="True" Width="100%"
                    ClientSettings-Selecting-AllowRowSelect="true" GroupingSettings-CaseSensitive="false"
                    ClientSettings-Resizing-AllowRowResize="false" CommandItemStyle-Wrap="false"
                    ActiveItemStyle-Wrap="true" AlternatingItemStyle-Wrap="false" EditItemStyle-Wrap="false"
                    Font-Size="Small" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" OnItemCommand="RadGrid1ItemCommand"
                    OnLoad="RadGrid1Load" OnNeedDataSource="RadGrid1NeedDataSource" CellSpacing="0"
                    GridLines="None" ShowGroupPanel="True" Skin="Windows7" PageSize="15" 
                    ExportSettings-ExportOnlyData="true" ExportSettings-OpenInNewWindow="true" 
                    ExportSettings-Excel-Format="ExcelML">
                    <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
                    </HeaderContextMenu>
                    <ExportSettings HideStructureColumns="true" />
                    <AlternatingItemStyle Wrap="False"></AlternatingItemStyle>
                    <ItemStyle Wrap="False"></ItemStyle>
                     <MasterTableView IsFilterItemExpanded="false" CommandItemDisplay="Top">
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
                        </EditFormSettings>
                        <CommandItemTemplate>
                            <telerik:RadToolBar runat="server" ID="RadToolBar1" Visible="false">
                            </telerik:RadToolBar>
                        </CommandItemTemplate>
                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true" ShowExportToCsvButton="true" />
                    </MasterTableView>
                    <HeaderStyle Wrap="False"></HeaderStyle>
                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="True" AllowDragToGroup="True"
                        ReorderColumnsOnClient="True">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                    <EditItemStyle Wrap="False"></EditItemStyle>
                    <ActiveItemStyle Wrap="True"></ActiveItemStyle>
                    <CommandItemStyle Wrap="False"></CommandItemStyle>
                </telerik:RadGrid>
            </telerik:RadAjaxPanel>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="">
            </asp:SqlDataSource>
        </div>
    </div>
    </form>
</body>
</html>
