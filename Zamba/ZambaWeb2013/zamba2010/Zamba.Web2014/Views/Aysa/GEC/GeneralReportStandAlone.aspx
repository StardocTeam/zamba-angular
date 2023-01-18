<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GeneralReportStandAlone.aspx.cs"
    Inherits="Views.Aysa.GEC.GeneralReportStandAlone" EnableEventValidation="false" %>
<!DOCTYPE html>
<html >
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
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function GetSelectedItems() 
            {
                var IdIndustria = $find("RadGrid1_ctl00").get_selectedItems()[0].get_cell("IDZamba").innerText;
                $("#hdnIdDocument").value = IdIndustria;
            }

            //Obtiene el ID de una industria, lo guarda en un hdn y hace postback para abrir la tarea
            function OnRowDblClick(rowIndex) {
                var IdIndustria = $find("RadGrid1_ctl00").get_selectedItems()[0].get_cell("IDZamba").innerText;
                $("#hdnIdDocument").value = IdIndustria;
                document.forms[0].__EVENTTARGET.value = 'Button1';
                document.forms[0].submit();
            }

            //Evalua si alguien abrio la ventana y si fue zamba, si es asi abre la tarea y selecciona la ventana
            //de Zamba.
            function OpenTaskInOpener(url, taskID, taskName) {
                if (window.opener != null && (window.opener.document.location.href.indexOf('Zamba') != -1 ||
                    window.opener.document.location.href.indexOf('zamba') != -1)) {

                    var anchor = $("#taskAnchor");
                    anchor.href = url;

                    window.opener.SwitchDocTaskForResults(anchor, taskID, taskName);
                    window.opener.SelectTaskFromModal();
                    window.opener.focus();
                }
            } 
        </script>
    </telerik:RadCodeBlock>
</head>
<body class="BODY">
    <form id="form1" runat="server">
        <a id="taskAnchor" style="display: none"></a>
    <div>
        <asp:ScriptManager ID="RadScriptManager1" runat="server" ScriptMode="Release">
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
        <div class="module" style="height: auto; width: 92%; color: White">
            <script type="text/javascript">
                function onRequestStart(sender, args) {
                    if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0 ||
                    args.get_eventTarget().indexOf("ExportToWordButton") >= 0 ||
                    args.get_eventTarget().indexOf("ExportToCsvButton") >= 0) {
                        args.set_enableAjax(false);
                    }
                }
            </script>
            <div style="font-size: small; color: White">
                <table>
                    <tr>
                        <td style="color: White; font-size: medium">
                            Entidades:
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <asp:UpdatePanel runat="server" ID="up">
                                <ContentTemplate>
                                    <asp:DropDownList runat="server" ID="cbEntidades" AutoPostBack="true" OnSelectedIndexChanged="cbEntidadesChange">
                                        <Items>
                                            <asp:ListItem Text="Documentos Gec" Value="11059"></asp:ListItem>
                                            <asp:ListItem Text="Documentacion Anexa" Value="11062"></asp:ListItem>
                                            <asp:ListItem Text="Documentos, Manuales y Procedimientos" Value="11080"></asp:ListItem>
                                        </Items>
                                    </asp:DropDownList>
                                    <input type="hidden" id="hdnIds" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 100%">
                            <asp:Button runat="server" ID="btnApplyEntities" Text="Aplicar Entidades" OnClick="BtnApplyEntitiesClick" />
                        </td>
                        <td style="width: 100%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <input type="hidden" id="hdnIdDocument" runat="server" />
            </div>
            <div>
                <asp:Button ID="Button1" runat="server" Text="Abrir Tarea" OnClientClick="GetSelectedItems();" OnClick="Button1Click" />
                &nbsp;
                <asp:Button ID="Button2" runat="server" Text="Exportar a Excel" OnClick="Button2Click" />
                <asp:Button ID="Button3" runat="server" Text="Exportar a CSV" OnClick="Button3Click" />
                <asp:CheckBox ID="CheckBox2" Text="Ignorar paginado(exportar todas las paginas)"
                    runat="server"></asp:CheckBox>
            </div>
            <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel1">
                <div class="filterDiv">
                    <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="RadGrid1" ShowApplyButton="False"
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
                        Skin="Windows7" >
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
                    AllowSorting="True" AllowFilteringByColumn="True" Width="98%" ClientSettings-Resizing-AllowResizeToFit="false"
                    ClientSettings-Resizing-ResizeGridOnColumnResize="false" ClientSettings-Scrolling-AllowScroll="false"
                    ClientSettings-Selecting-AllowRowSelect="true" GroupingSettings-CaseSensitive="false"
                    ClientSettings-Resizing-AllowRowResize="false" CommandItemStyle-Wrap="false"
                    ActiveItemStyle-Wrap="true" AlternatingItemStyle-Wrap="false" EditItemStyle-Wrap="false"
                    Font-Size="Small" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" OnItemCommand="RadGrid1ItemCommand"
                    OnLoad="RadGrid1Load" OnNeedDataSource="RadGrid1NeedDataSource" CellSpacing="0"
                    GridLines="None" ShowGroupPanel="True" Skin="Windows7" PageSize="50">
                    <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
                    </HeaderContextMenu>
                    <ExportSettings HideStructureColumns="true" />
                    <AlternatingItemStyle Wrap="False"></AlternatingItemStyle>
                    <ItemStyle Wrap="False"></ItemStyle>
                    <MasterTableView IsFilterItemExpanded="false" CommandItemDisplay="Top" >
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                        </ExpandCollapseColumn>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                        <CommandItemTemplate>
                            <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1ButtonClick">
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
                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="True" AllowDragToGroup="True"
                        ReorderColumnsOnClient="True">
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnRowDblClick="OnRowDblClick" />
                    </ClientSettings>
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                    <EditItemStyle Wrap="False"></EditItemStyle>
                    <ActiveItemStyle Wrap="True"></ActiveItemStyle>
                    <CommandItemStyle Wrap="False"></CommandItemStyle>
                </telerik:RadGrid>
            </telerik:RadAjaxPanel>
            <div style="font-size: small; display: none">
                Opciones de Exportacion:
                <asp:CheckBox ID="CheckBox1" Text="Exportar registros unicamente" runat="server"
                    Checked="true" Visible="false"></asp:CheckBox>
                <asp:CheckBox ID="CheckBox3" Text="Abrir en una nueva ventana" runat="server" Checked="true"
                    Visible="false"></asp:CheckBox>
            </div>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="">
            </asp:SqlDataSource>
        </div>
    </div>
    </form>
</body>
</html>
