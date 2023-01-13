<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Views_Aysa_GeneralReportStandAlone" EnableEventValidation="false" Codebehind="GeneralReportStandAlone.aspx.cs" %>

<%@ Import Namespace="System.Web.Optimization" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
    <%: Scripts.Render("~/bundles/jqueryCore") %>
    <%: Scripts.Render("~/bundles/jqueryAddIns") %>
    <%: Scripts.Render("~/bundles/jqueryval") %>
    <%: Scripts.Render("~/bundles/bootstrap") %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Reporte General</title>


    <style type="text/css">
        .filterDiv {
            margin: 20px 0px 10px 0px;
        }

        .DisplayNone {
            display: none;
        }

        .loading {
            background-color: Black;
            display: none;
            position: absolute;
            white-space: nowrap;
            z-index: 998;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            font-weight: bold;
            padding: 5px;
            -moz-border-radius: 5px;
            filter: alpha(opacity=30);
            opacity: 0.3;
        }

        .white {
            color: White !important;
        }
    </style>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function GetSelectedItems() {
                var IdIndustria = $find("RadGrid1_ctl00").get_selectedItems()[0].get_cell("ID_Industria").innerText;
                document.getElementById("hdnIdIndustria").value = IdIndustria;
            }

            function LoadID() {
                ShowLoadingAnimation();
                GetSelectedItems();
            }

            //Obtiene el ID de una industria, lo guarda en un hdn y hace postback para abrir la tarea
            function OnRowDblClick(rowIndex) {
                var IdIndustria = $find("RadGrid1_ctl00").get_selectedItems()[0].get_cell("ID_Industria").innerText;
                document.getElementById("hdnIdIndustria").value = IdIndustria;
                document.forms[0].__EVENTTARGET.value = 'Button1';
                document.forms[0].submit();
            }

            //Evalua si alguien abrio la ventana y si fue zamba, si es asi abre la tarea y selecciona la ventana
            //de Zamba.
            function OpenTaskInOpener(url, taskID, taskName) {
                var virtualDirectory = document.location.pathname.split("/")[1].toLowerCase();
                var domain = document.domain.toLowerCase();
                var opener = window.opener;

                try {
                    if (opener != null) {

                        if (opener.document.location.href.toLowerCase().indexOf("main/default.aspx") == -1) {
                            alert("El cliente Zamba Web no se encuentra logueado \r Por favor ingrese en su cliente web.  \rUna vez hecho esto reabra el reporte.");
                            return;
                        }

                        if (opener.document.location.href.toLowerCase().indexOf(virtualDirectory) != -1 ||
                            opener.document.location.href.toLowerCase().indexOf(domain) != -1) {

                            var anchor = document.getElementById("taskAnchor");
                            anchor.href = url;

                            opener.SwitchDocTaskForResults(anchor, taskID, taskName);
                            opener.SelectTaskFromModal();
                            opener.focus();
                        }
                    }
                    else {
                        alert("Para obtener la funcionalidad de apertura de tareas debe tener abierto el cliente de Zamba web y estar logueado en él.  \rUna vez hecho esto reabra el reporte.");
                    }
                }
                catch (ex) {
                    alert("Para obtener la funcionalidad de apertura de tareas debe tener abierto el cliente de Zamba web y estar logueado en él.  \rUna vez hecho esto reabra el reporte.");
                }
            }

            $(document).ready(function () {
                ShowLoadingAnimation();
            });

            $(window).on("load",function () {
                hideLoading();
            });

            function ShowLoadingAnimation() {
                $(document).scrollTop(0);
                $("#loading").fadeIn("slow", function () {
                    $("#loading").css("filter", "alpha(opacity=30)");
                    $("#loading").css("opacity", "0.3");
                });

                var html = jQuery('html');
                html.data('previous-overflow', html.css('overflow'));
                html.css('overflow', 'hidden');
            }

            function hideLoading() {
                $("#loading").fadeOut("slow", function () {
                    var html = jQuery('html');
                    html.css('overflow', html.data('previous-overflow'));
                });
            }

            function hideLoadingObs() {
                var doc = document;

                if (doc.readyState == "interactive " || doc.readyState == "complete") {
                    hideLoading();
                }
                else {
                    setTimeout('hideLoadingObs();', 250);
                }
            }


            function ResizeGrid() {
                var radGrid0 = $find('<%= RadGrid1.ClientID %>'); //outer RadGrid

                if (radGrid0 != null) {
                    var headerHeight = $("#reportHeader").height();
                    var docHeight = $(window).height();
                    var grd = radGrid0._gridDataDiv;
                    var FirstHeight = 0;
                    if ($(grd.firstElementChild)) {
                        FirstHeight = $(grd.firstElementChild).height();
                    }
                    if (headerHeight + FirstHeight > docHeight) {
                        grd.style.height = (docHeight - headerHeight - 30) + "px";
                    }
                }
            }

            function FilterCreated(a) {
                if (a._element) {
                    $(a._element).find('[type="text"]').each(function () {
                        $(this).keypress(function (evt) {
                            if (evt.which == 13) {
                                evt.preventDefault();
                                return false;
                            }
                        });
                    });
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
            <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1">
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
                <ClientEvents OnRequestStart="onRequestStart" />
                <ClientEvents OnResponseEnd="onRequestEnd" />
            </telerik:RadAjaxManager>
            <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1">
                <strong>Procesando...
                <img src="../../Content/Images/loading.gif" alt="" />
                </strong>
            </telerik:RadAjaxLoadingPanel>
            <div id="reportHeader">
                <div class="module" id="module" style="height: auto; width: 92%; color: White">
                    <script type="text/javascript">
                        function onRequestStart(sender, args) {
                            ShowLoadingAnimation();
                        }


                        function onRequestEnd(sender, args) {
                            hideLoading();
                        }

                        function button2_onclick() {

                        }

                        function button2_onclick() {

                        }
                    </script>
                    <div style="font-size: small; color: White">
                        <table>
                            <tr>
                                <td style="color: White; font-size: medium">Entidades:
                                </td>
                                <asp:Label ID="lblAviso" Text="Seleccione las entidades y apliquelas para poder continuar"
                                    ForeColor="Red" runat="server"></asp:Label>
                            </tr>
                            <tr>
                                <td style="width: 100%">
                                    <asp:Panel ID="panelEntidades" runat="server">
                                        <asp:CheckBox Text="Industrias" ID="chk1027" runat="server" AutoPostBack="false" />
                                        <asp:CheckBox Text="Micro - Macro" ID="chk1040" runat="server" AutoPostBack="false" />
                                        <asp:CheckBox Text="FHV" ID="chk1045" runat="server" AutoPostBack="false" />
                                        <asp:CheckBox Text="Conexiones a agua" ID="chk1038" runat="server" AutoPostBack="false" />
                                        <asp:CheckBox Text="Descargas" ID="chk1043" runat="server" AutoPostBack="false" />
                                        <asp:CheckBox Text="CTM" ID="chk1044" runat="server" AutoPostBack="false" />
                                        <asp:CheckBox Text="Inspecciones" ID="chk1028" runat="server" AutoPostBack="false" />
                                        <asp:CheckBox Text="Intimaciones" ID="chk11074" runat="server" AutoPostBack="false" />
                                        <asp:CheckBox Text="Denuncias" ID="chk11075" runat="server" AutoPostBack="false" />
                                        <asp:CheckBox Text="Cortes" ID="chk11077" runat="server" AutoPostBack="false" />
                                        <asp:CheckBox Text="Notas" ID="chk11076" runat="server" AutoPostBack="false" />
                                    </asp:Panel>
                                </td>
                                <td style="width: 100%">
                                    <asp:Button runat="server" ID="btnApplyEntities" Text="Aplicar Entidades" OnClick="btnApplyEntities_Click"
                                        OnClientClick="ShowLoadingAnimation();" />
                                </td>
                                <td style="width: 100%">&nbsp;
                                </td>
                            </tr>
                        </table>
                        <input type="hidden" id="hdnIdIndustria" runat="server" />
                    </div>
                    <asp:Panel runat="server" ID="panelAcciones" Visible="false">
                        <asp:Button ID="Button1" runat="server" Text="Abrir Tarea" OnClick="Button1_Click"
                            OnClientClick="LoadID();" />
                        &nbsp;
                    <asp:Button ID="Button2" runat="server" Text="Exportar a Excel" OnClick="Button2_Click"
                        OnClientClick="ShowLoadingAnimation();setTimeout('hideLoadingObs();',2500);" />
                        <asp:Button ID="Button3" runat="server" Text="Exportar a CSV" OnClick="Button3_Click"
                            OnClientClick="ShowLoadingAnimation();setTimeout('hideLoadingObs();',2500);" />
                        <asp:CheckBox ID="CheckBox2" Text="Ignorar filtrado(exportar todos los registros)"
                            Checked="false" runat="server"></asp:CheckBox>

                    </asp:Panel>
                </div>

                <div>
                    <telerik:RadNumericTextBox ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true"
                        Label="Maximo de resultados" labelwidth="120px" LabelCssClass="white" runat="server" ID="txtMaxResults" Width="300px">
                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                    </telerik:RadNumericTextBox>
                </div>
                <%--<div style="font-size: small">
            <asp:Panel ID="PanelExportación" runat="server" Visible="false">
                <asp:Label ID="Label1" Text="Opciones de Exportacion:" runat="server" ForeColor="White" />
                <asp:CheckBox ID="CheckBox1" Text="Exportar registros unicamente" runat="server"
                    Checked="true" Visible="true" ForeColor="White"></asp:CheckBox>
                <asp:CheckBox ID="CheckBox3" Text="Abrir en una nueva ventana" runat="server" Checked="true"
                    Visible="true" ForeColor="White"></asp:CheckBox>
            </asp:Panel>
        </div>--%>
                <div class="filterDiv" id="divFilter">
                    <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="RadGrid1" ShowApplyButton="False"
                        OnPreRender="RadFilter1_PreRender" AddExpressionToolTip="Agregar Filtro" AddGroupToolTip="Agregar Grupo"
                        ApplyButtonText="Aplicar Filtro" Localization-FilterFunctionBetween="Entre" ocalization-FilterFunctionContains="Contiene"
                        Localization-FilterFunctionDoesNotContain="No Contiene" Localization-FilterFunctionEndsWith="Termina"
                        Localization-FilterFunctionEqualTo="=" Localization-FilterFunctionGreaterThan=">"
                        Localization-FilterFunctionGreaterThanOrEqualTo=">" Localization-FilterFunctionIsEmpty="Vacio"
                        Localization-FilterFunctionIsNull="Nulo" Localization-FilterFunctionLessThan="<"
                        Localization-FilterFunctionLessThanOrEqualTo="=<" Localization-FilterFunctionNotBetween="No Entre"
                        Localization-FilterFunctionNotEqualTo="<>" Localization-FilterFunctionNotIsEmpty="No Vacio"
                        Localization-FilterFunctionNotIsNull="No Nulo" Localization-FilterFunctionStartsWith="Empieza"
                        Localization-GroupOperationAnd="Y" Localization-GroupOperationOr="O" Localization-GroupOperationNotAnd="Y No"
                        Localization-GroupOperationNotOr="O No" CssClass="RadFilter RadFilter_Default "
                        Skin="Windows7" Visible="false" OnApplyExpressions="ApplyExpressions">
                        <Localization GroupOperationAnd="Y" GroupOperationOr="O" GroupOperationNotAnd="Y No"
                            GroupOperationNotOr="O No" FilterFunctionContains="Contiene" FilterFunctionDoesNotContain="No Contiene"
                            FilterFunctionStartsWith="Empieza" FilterFunctionEndsWith="Termina" FilterFunctionEqualTo="="
                            FilterFunctionNotEqualTo="&lt;&gt;" FilterFunctionGreaterThan="&gt;" FilterFunctionLessThan="&lt;"
                            FilterFunctionGreaterThanOrEqualTo="=&gt;" FilterFunctionLessThanOrEqualTo="=&lt;"
                            FilterFunctionBetween="Entre" FilterFunctionNotBetween="No Entre" FilterFunctionIsEmpty="Vacio"
                            FilterFunctionNotIsEmpty="No Vacio" FilterFunctionIsNull="Nulo" FilterFunctionNotIsNull="No Nulo"></Localization>
                        <ClientSettings ClientEvents-OnFilterCreated="FilterCreated"></ClientSettings>
                    </telerik:RadFilter>
                </div>
            </div>
            <telerik:RadGrid runat="server" ID="RadGrid1" DataSourceID="SqlDataSource2" AllowPaging="True"
                AllowSorting="True" AllowFilteringByColumn="True" Width="100%" ClientSettings-Resizing-AllowResizeToFit="true"
                ClientSettings-Resizing-ResizeGridOnColumnResize="false" ClientSettings-Scrolling-AllowScroll="true"
                ClientSettings-Scrolling-UseStaticHeaders="false" ClientSettings-Selecting-AllowRowSelect="true"
                GroupingSettings-CaseSensitive="false" ShowFooter="true" ShowHeader="true" ClientSettings-Resizing-AllowRowResize="false"
                CommandItemStyle-Wrap="false" ActiveItemStyle-Wrap="true" AlternatingItemStyle-Wrap="false"
                EditItemStyle-Wrap="false" Font-Size="Small" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                OnItemCommand="RadGrid1_ItemCommand" OnLoad="RadGrid1_Load" OnNeedDataSource="RadGrid1_NeedDataSource"
                CellSpacing="0" GridLines="None" Skin="Windows7" PageSize="50"
                Visible="false">
                <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
                </HeaderContextMenu>
                <ExportSettings HideStructureColumns="true" />
                <AlternatingItemStyle Wrap="False"></AlternatingItemStyle>
                <ItemStyle Wrap="False"></ItemStyle>
                <MasterTableView IsFilterItemExpanded="false" CommandItemDisplay="Top" DataKeyNames="ID_Industria">
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
                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="True"
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
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="SELECT WFStep.Name, WFStepStates.Description, 
DOC_I1027.I1181, DOC_I1027.I1183, DOC_I1027.I1185, DOC_I1027.I1189, DOC_I1027.I1187, DOC_I1027.I1188, DOC_I1027.I1190, DOC_I1027.I1191, 
DOC_I1027.I1192, DOC_I1027.I1193, DOC_I1040.I1217, DOC_I1040.I1241, DOC_I1040.I1253, DOC_I1040.I1240, DOC_I1040.I1243, DOC_I1040.I1244, 
DOC_I1040.I1268 FROM DOC_I1027 INNER JOIN DOC_I1040 ON DOC_I1027.I1217 = DOC_I1040.I1217 
             INNER JOIN
                      WFDocument ON DOC_I1027.DOC_ID = WFDocument.Doc_ID INNER JOIN
                      WFStep ON WFDocument.step_Id = WFStep.step_Id INNER JOIN
                      WFStepStates ON WFDocument.Do_State_ID = WFStepStates.Doc_State_ID"></asp:SqlDataSource>
        </div>
        <div id="loading" class="loading">
            <table style="width: 100%; height: 100%">
                <tr style="height: 50%">
                    <td></td>
                </tr>
                <tr style="height: 1%">
                    <td style="text-align: center">
                        <img src="../../Content/images/Loader.gif" style="top: 50%" alt="Cargando..." />
                    </td>
                </tr>
                <tr style="height: 49%">
                    <td></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
