<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GeneralReportStandAlone.aspx.cs" 
    Inherits="Views_Aysa_GeneralReportStandAlone" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
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
    </style>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function GetSelectedItems() {
                var IdIndustria = $find("RadGrid1_ctl00").get_selectedItems()[0].get_cell("ID Industria").innerText;
                $("#hdnIdIndustria").value = IdIndustria;
            }

            //Obtiene el ID de una industria, lo guarda en un hdn y hace postback para abrir la tarea
            function OnRowDblClick(rowIndex) {
                var IdIndustria = $find("RadGrid1_ctl00").get_selectedItems()[0].get_cell("ID Industria").innerText;
                $("#hdnIdIndustria").value = IdIndustria;
                document.forms[0].__EVENTTARGET.value = 'Button1';
                document.forms[0].submit();
            } 
            
            //Evalua si alguien abrio la ventana y si fue zamba, si es asi abre la tarea y selecciona la ventana
            //de Zamba.
            function OpenTaskInOpener(url,taskID,taskName){
                if(window.opener != null && (window.opener.document.location.href.indexOf('Zamba') != -1 ||
                    window.opener.document.location.href.indexOf('zamba') != -1)){
                    
                    window.opener.OpenTask("../WF/TaskViewer.aspx?taskid=" + taskID,taskID,taskName);
                    window.opener.SelectTaskFromModal();
                    window.opener.focus();
                }
            } 
        </script>

    </telerik:RadCodeBlock>
</head>
<body class="BODY">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="RadScriptManager1" runat="server" ScriptMode="Release">
		    <Scripts>
		    </Scripts>
	    </asp:ScriptManager>
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
        <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" >
            <strong>  Procesando... <img src="../../Content/Images/loading.gif" alt="" /> </strong>
        </telerik:RadAjaxLoadingPanel>                
        
        <div class="module" style="height: auto; width:92%; color:White" >
     
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

function button2_onclick() {

}

    </script>

<div style="font-size: small; color:White">
<table>
<tr>
<td  style="color:White; font-size: medium">Entidades:</td>
</tr>
<tr>
<td style="width:100%">
<asp:UpdatePanel runat="server" ID="up" ><ContentTemplate>

<asp:CheckBox Text="Cliente" ID="chk1024" runat="server"  AutoPostBack="true" 
        oncheckedchanged="chk1027_CheckedChanged" />
&nbsp;
<asp:CheckBox Text="Solicitud de Crédito" ID="chk1023" runat="server"  AutoPostBack="true" 
        oncheckedchanged="chk1040_CheckedChanged" />
&nbsp;
<asp:CheckBox Text="Documentacion Adiciona" ID="chk1025" runat="server"  AutoPostBack="true" 
        oncheckedchanged="chk1045_CheckedChanged" />
&nbsp;
<asp:CheckBox Text="Antecedentes Comerciales" ID="chk1029" runat="server"  
        AutoPostBack="true" oncheckedchanged="chk1038_CheckedChanged" />
&nbsp;

<input type="hidden" id="hdnIds" runat="server" />

</ContentTemplate></asp:UpdatePanel>  

</td>

<td style="width:100%">
<asp:Button runat="server" ID="btnApplyEntities" Text="Aplicar Entidades" 
        onclick="btnApplyEntities_Click" />

</td >
<td style="width:100%">
&nbsp;
</td>
</tr>
</table>

<input type="hidden" id="hdnIdIndustria" runat="server" />


</div>
<div>
            <asp:Button ID="Button1" runat="server" Text="Abrir Tarea" onclick="Button1_Click" />
            &nbsp;
            <asp:Button ID="Button2" runat="server" Text="Exportar a Excel" onclick="Button2_Click" />
            <asp:Button ID="Button3" runat="server" Text="Exportar a CSV" onclick="Button3_Click" />
          <%--  <asp:Button ID="Button4" runat="server" Text="Exportar a Word" onclick="Button4_Click" />
            <asp:Button ID="Button5" runat="server" Text="Exportar a PDF" onclick="Button5_Click" />
          --%>  <asp:CheckBox ID="CheckBox2" Text="Ignorar paginado(exportar todas las paginas)" runat="server">
            </asp:CheckBox>
</div>
        
<div class="filterDiv">                    
            <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="RadGrid1" 
                ShowApplyButton="False" OnPreRender="RadFilter1_PreRender"
 AddExpressionToolTip="Agregar Filtro" AddGroupToolTip="Agregar Grupo" 
                ApplyButtonText="Aplicar Filtro"  Localization-FilterFunctionBetween="Entre" 
                Localization-FilterFunctionContains="Contiene" 
                Localization-FilterFunctionDoesNotContain="No Contiene" 
                Localization-FilterFunctionEndsWith="Termina" 
                Localization-FilterFunctionEqualTo="=" 
                Localization-FilterFunctionGreaterThan=">" 
                Localization-FilterFunctionGreaterThanOrEqualTo=">" 
                Localization-FilterFunctionIsEmpty="Vacio" 
                Localization-FilterFunctionIsNull="Nulo" 
                Localization-FilterFunctionLessThan="<" 
                Localization-FilterFunctionLessThanOrEqualTo="=<" 
                Localization-FilterFunctionNotBetween="No Entre" 
                Localization-FilterFunctionNotEqualTo="<>" 
                Localization-FilterFunctionNotIsEmpty="No Vacio" 
                Localization-FilterFunctionNotIsNull="No Nulo" 
                Localization-FilterFunctionStartsWith="Empieza" 
                Localization-GroupOperationAnd="Y" Localization-GroupOperationOr="O" 
                Localization-GroupOperationNotAnd="Y No" 
                Localization-GroupOperationNotOr="O No" CssClass="RadFilter RadFilter_Default " 
                Skin="Windows7"  >
<Localization GroupOperationAnd="Y" GroupOperationOr="O" GroupOperationNotAnd="Y No" GroupOperationNotOr="O No" FilterFunctionContains="Contiene" FilterFunctionDoesNotContain="No Contiene" FilterFunctionStartsWith="Empieza" FilterFunctionEndsWith="Termina" FilterFunctionEqualTo="=" FilterFunctionNotEqualTo="&lt;&gt;" FilterFunctionGreaterThan="&gt;" FilterFunctionLessThan="&lt;" FilterFunctionGreaterThanOrEqualTo="=&gt;" FilterFunctionLessThanOrEqualTo="=&lt;" FilterFunctionBetween="Entre" FilterFunctionNotBetween="No Entre" FilterFunctionIsEmpty="Vacio" FilterFunctionNotIsEmpty="No Vacio" FilterFunctionIsNull="Nulo" FilterFunctionNotIsNull="No Nulo"></Localization>
            </telerik:RadFilter>
        </div>
        <telerik:RadGrid runat="server" ID="RadGrid1" DataSourceID="SqlDataSource2" 
            AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="True"   Width="98%"
             ClientSettings-Resizing-AllowResizeToFit="false" 
                ClientSettings-Resizing-ResizeGridOnColumnResize="false" ClientSettings-Scrolling-AllowScroll="false"
              ClientSettings-Selecting-AllowRowSelect="true"  GroupingSettings-CaseSensitive="false"
                ClientSettings-Resizing-AllowRowResize="false" CommandItemStyle-Wrap="false" 
                ActiveItemStyle-Wrap="true" AlternatingItemStyle-Wrap="false" 
                EditItemStyle-Wrap="false" Font-Size="Small" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" 
               OnItemCommand="RadGrid1_ItemCommand" onload="RadGrid1_Load" 
            onneeddatasource="RadGrid1_NeedDataSource" CellSpacing="0" GridLines="None" 
                ShowGroupPanel="True" Skin="Windows7" PageSize="50">
<HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default"></HeaderContextMenu>

            <ExportSettings HideStructureColumns="true" />

<AlternatingItemStyle Wrap="False"></AlternatingItemStyle>

<ItemStyle Wrap="False"></ItemStyle>

 <MasterTableView IsFilterItemExpanded="false" CommandItemDisplay="Top" DataKeyNames="Nro Cliente">

<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>
                <CommandItemTemplate>
                    <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick">
                        <Items>
                            <telerik:RadToolBarButton Text="Aplicar Filtro" CommandName="FilterRadGrid" ImageUrl="<%#GetFilterIcon() %>"
                                ImagePosition="Left" />
                              <%--  <telerik:RadToolBarButton Text="Exportar a Excel" CommandName="ExportToExcel"  ImageUrl="<%#GetExcelIcon() %>"
                                ImagePosition="Left" />
                                <telerik:RadToolBarButton Text="Exportar a CSV" CommandName="ExportToCSV"  ImageUrl="<%#GetCSVIcon() %>"
                                ImagePosition="Left" />
                                <telerik:RadToolBarButton Text="Exportar a Word" CommandName="ExportToWord"  ImageUrl="<%#GetWordIcon() %>"
                                ImagePosition="Left" />
                                <telerik:RadToolBarButton Text="Exportar a PDF" CommandName="ExportToPDF"  ImageUrl="<%#GetPDFIcon() %>"
                                ImagePosition="Left" />
                              --%> 
                                

                        </Items>
                    </telerik:RadToolBar>
                </CommandItemTemplate>

  <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                ShowExportToCsvButton="true" />

            </MasterTableView>

<HeaderStyle Wrap="False"></HeaderStyle>

                  <ClientSettings EnableRowHoverStyle="true" allowcolumnsreorder="True" 
                allowdragtogroup="True" reordercolumnsonclient="True" >
            <Selecting AllowRowSelect="True" />

        <ClientEvents OnRowDblClick="OnRowDblClick" /> 

    </ClientSettings>   


<FilterMenu EnableImageSprites="False"></FilterMenu>

<EditItemStyle Wrap="False"></EditItemStyle>

<ActiveItemStyle Wrap="True"></ActiveItemStyle>

<CommandItemStyle Wrap="False"></CommandItemStyle>


        </telerik:RadGrid>
<div style="font-size:small">
        Opciones de Exportacion:
           <asp:CheckBox ID="CheckBox1" Text="Exportar registros unicamente" runat="server" Checked="true" Visible="false"></asp:CheckBox>
            <asp:CheckBox ID="CheckBox3" Text="Abrir en una nueva ventana" runat="server" Checked="true" Visible="false">
            </asp:CheckBox>
</div>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            SelectCommand="Select Doc_I1023.I64,Doc_I1023.I1179,Doc_I1023.I1180 from Doc_I1023">
        </asp:SqlDataSource>
</div>  
  
     </div>

    </form>
</body>
</html>