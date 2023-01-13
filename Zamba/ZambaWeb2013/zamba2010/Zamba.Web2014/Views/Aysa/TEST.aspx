<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TEST.aspx.cs" Inherits="Views_Aysa_TEST" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
       
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

            $(window).load(function () {
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
<body>
    
    <form id="form1" runat="server">
        
        
        <p style="width: 100%">
        <asp:Button runat="server" ID="btnApplyEntities" Text="Aplicar Entidades" Onclick=" btnApplyEntities_Click" />
         </p>


        <asp:Panel ID="panelEntidades" runat="server">
        </asp:Panel>
        <input type="hidden" id="hdnIdIndustria" runat="server" />


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

         <asp:Label ID="lblAviso" Text="Seleccione las entidades y apliquelas para poder continuar"
                                ForeColor="Red" runat="server"></asp:Label>

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
                        FilterFunctionNotIsEmpty="No Vacio" FilterFunctionIsNull="Nulo" FilterFunctionNotIsNull="No Nulo">
                    </Localization>
                    <ClientSettings ClientEvents-OnFilterCreated="FilterCreated"></ClientSettings>
                </telerik:RadFilter>
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
                      WFStepStates ON WFDocument.Do_State_ID = WFStepStates.Doc_State_ID">
        </asp:SqlDataSource>


    </form>


   
</body>
</html>
