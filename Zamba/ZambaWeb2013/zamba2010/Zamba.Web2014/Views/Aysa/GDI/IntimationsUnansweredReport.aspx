﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IntimationsUnansweredReport.aspx.cs"
    Inherits="Views_Aysa_GDI_IntimationsUnansweredReport" UICulture="Auto" Culture="Auto" %>

<%@ Import Namespace="System.Web.Optimization" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<%: Scripts.Render("~/bundles/jqueryCore") %>
<%: Scripts.Render("~/bundles/jqueryAddIns") %>
<%: Scripts.Render("~/bundles/jqueryval") %>
<%: Scripts.Render("~/bundles/bootstrap") %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>Reporte de Intimaciones sin respuesta</title>

    <style type="text/css">
        .filterDiv {
            margin: 20px 0px 10px 0px;
        }

        .DisplayNone {
            display: none;
        }
    </style>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function GetSelectedItems() {
            }

            //Obtiene el ID de una industria, lo guarda en un hdn y hace postback para abrir la tarea
            function OnRowDblClick(rowIndex) {

                document.forms[0].__EVENTTARGET.value = 'Button1';
                document.forms[0].submit();
            }

            //Evalua si alguien abrio la ventana y si fue zamba, si es asi abre la tarea y selecciona la ventana
            //de Zamba.
            function OpenTaskInOpener(url, taskID, taskName) {
                var virtualDirectory = document.location.pathname.split("/")[1];
                var domain = document.domain;
                if (parent != null && (parent.document.location.href.indexOf(virtualDirectory) != -1 ||
                    parent.document.location.href.indexOf(domain) != -1)) {

                    var anchor = document.getElementById("taskAnchor");
                    anchor.href = url;

                    parent.SwitchDocTaskForResults(anchor, taskID, taskName);
                    parent.SelectTaskFromModal();
                    parent.focus();
                    parent.closeIntimationsUnansweredReport();
                }
            }
            function Exit() {
                parent.closeIntimationsUnansweredReport();
            }

            function ResizeGrid() {
                var radGrid0 = $find('<%= RadGrid1.ClientID %>'); //outer RadGrid

                if (radGrid0 != null) {
                    var headerHeight = $("#reportHeader").height();
                    var docHeight = 550;
                    var grd = radGrid0._gridDataDiv;
                    if (radGrid0._groupPanel) {
                        var grpPanelHeight = $(radGrid0._groupPanel._element).height();

                        if (headerHeight + $(grd).height() + grpPanelHeight + 30 > docHeight) {
                            grd.style.height = (docHeight - headerHeight - grpPanelHeight - 30) + "px";
                        }
                    }
                }
            }
        </script>
    </telerik:RadCodeBlock>
</head>
<body class="BODY">
    <form id="form1" runat="server">
        <a id="taskAnchor" style="display: none"></a>
        <div>
            <asp:ScriptManager ID="RadScriptManager1" runat="server">
                <Scripts>
                </Scripts>
            </asp:ScriptManager>
            <script type="text/javascript">
                window.onresize = ResizeGrid;
                Sys.Application.add_load(ResizeGrid);
            </script>
            <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="RadFilter1">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadFilter1" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                    <telerik:AjaxSetting AjaxControlID="RadGrid1">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>
            <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1">
                <strong>Procesando...
                <img src="../../../Content/Images/loading.gif" alt="" />
                </strong>
            </telerik:RadAjaxLoadingPanel>
            <div id="reportHeader">
                <div class="module" style="width: 92%; color: White">
                    <script type="text/javascript">
                        function onRequestStart(sender, args) {

                            if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0 ||
                        args.get_eventTarget().indexOf("ExportToWordButton") >= 0 ||
                        args.get_eventTarget().indexOf("ExportToCsvButton") >= 0) {
                                args.set_enableAjax(false);
                            }
                        }
                        function button2_onclick() {

                        }

                    </script>
                    <div>
                        <asp:Button ID="Button1" runat="server" Text="Abrir Tarea" OnClick="Button1_Click" />
                        <asp:Button ID="Button2" runat="server" Text="Exportar a Excel" OnClick="Button2_Click" />
                        <asp:Button ID="Button3" runat="server" Text="Exportar a CSV" OnClick="Button3_Click" />
                        <asp:CheckBox ID="CheckBox2" Text="Ignorar filtrado(exportar todos los registros)"
                            Checked="true" runat="server"></asp:CheckBox>
                    </div>
                    <%-- <div style="text-align: right">
                    <input id="BtnExit" type="button" value=" Salir " onclick="Exit();" />
                </div>--%>
                    <div class="filterDiv">
                        <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="RadGrid1" ShowApplyButton="False"
                            OnPreRender="RadFilter1_PreRender" AddExpressionToolTip="Agregar Filtro" AddGroupToolTip="Agregar Grupo"
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
                            Skin="Windows7">
                            <Localization GroupOperationAnd="Y" GroupOperationOr="O" GroupOperationNotAnd="Y No"
                                GroupOperationNotOr="O No" FilterFunctionContains="Contiene" FilterFunctionDoesNotContain="No Contiene"
                                FilterFunctionStartsWith="Empieza" FilterFunctionEndsWith="Termina" FilterFunctionEqualTo="="
                                FilterFunctionNotEqualTo="&lt;&gt;" FilterFunctionGreaterThan="&gt;" FilterFunctionLessThan="&lt;"
                                FilterFunctionGreaterThanOrEqualTo="=&gt;" FilterFunctionLessThanOrEqualTo="=&lt;"
                                FilterFunctionBetween="Entre" FilterFunctionNotBetween="No Entre" FilterFunctionIsEmpty="Vacio"
                                FilterFunctionNotIsEmpty="No Vacio" FilterFunctionIsNull="Nulo" FilterFunctionNotIsNull="No Nulo"></Localization>
                        </telerik:RadFilter>
                    </div>
                </div>
            </div>
            <div>
                <telerik:RadGrid runat="server" ID="RadGrid1" DataSourceID="SqlDataSource2" AllowPaging="True"
                    AllowSorting="True" AllowFilteringByColumn="True" Width="100%" ClientSettings-Resizing-AllowResizeToFit="true"
                    ClientSettings-Resizing-ResizeGridOnColumnResize="false" ClientSettings-Scrolling-AllowScroll="true"
                    ClientSettings-Selecting-AllowRowSelect="true" GroupingSettings-CaseSensitive="false"
                    ClientSettings-Resizing-AllowRowResize="false" CommandItemStyle-Wrap="false"
                    ActiveItemStyle-Wrap="true" AlternatingItemStyle-Wrap="false" EditItemStyle-Wrap="false"
                    Font-Size="Small" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" OnItemCommand="RadGrid1_ItemCommand"
                    OnLoad="RadGrid1_Load" OnNeedDataSource="RadGrid1_NeedDataSource" CellSpacing="0"
                    GridLines="None" ShowGroupPanel="True" Skin="Windows7" PageSize="50">
                    <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
                    </HeaderContextMenu>
                    <ExportSettings HideStructureColumns="true" />
                    <AlternatingItemStyle Wrap="False"></AlternatingItemStyle>
                    <ItemStyle Wrap="False"></ItemStyle>
                    <PagerStyle Mode="NumericPages"></PagerStyle>
                    <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Auto" IsFilterItemExpanded="false"
                        CommandItemDisplay="Top" UseAllDataFields="true">
                        <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldAlias="Alertas" FieldName="Alerta"></telerik:GridGroupByField>
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="Alerta"></telerik:GridGroupByField>
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                        </ExpandCollapseColumn>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                        <CommandItemTemplate>
                            <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick">
                                <Items>
                                    <telerik:RadToolBarButton Text="Aplicar Filtro" CommandName="FilterRadGrid" ImageUrl="<%#GetFilterIcon() %>"
                                        ImagePosition="Left" />
                                </Items>
                            </telerik:RadToolBar>
                        </CommandItemTemplate>
                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                            ShowExportToCsvButton="true" />
                    </MasterTableView>
                    <HeaderStyle Wrap="False"></HeaderStyle>
                    <ClientSettings AllowGroupExpandCollapse="true" ReorderColumnsOnClient="true" AllowDragToGroup="true"
                        AllowColumnsReorder="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="OnRowDblClick" />
                    </ClientSettings>
                    <GroupingSettings ShowUnGroupButton="false"></GroupingSettings>
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                    <EditItemStyle Wrap="False"></EditItemStyle>
                    <ActiveItemStyle Wrap="True"></ActiveItemStyle>
                    <CommandItemStyle Wrap="False"></CommandItemStyle>
                </telerik:RadGrid>
                <%--     <div style="font-size: small">
                <asp:Label Text="Opciones de Exportacion:" ForeColor="White" runat="server" ></asp:Label>
                <asp:CheckBox ID="CheckBox1" Text="Exportar registros unicamente" runat="server"
                    Checked="true" Visible="true"></asp:CheckBox>
                <asp:CheckBox ID="CheckBox3" Text="Abrir en una nueva ventana" runat="server" Checked="true"
                    Visible="true"></asp:CheckBox>
            </div>--%>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand=""></asp:SqlDataSource>
            </div>
        </div>
    </form>
</body>
</html>