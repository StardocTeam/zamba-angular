<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCDoShowTable.ascx.cs"
    Inherits="Views_UC_WF_Rules_UCDoShowTable" %>
<%@ Import Namespace="System.Web.Optimization" %>



<link rel="Stylesheet" type="text/css" href="../../Content/Styles/GridThemes/GridViewGray.css" />	
<script type="text/javascript" src="../../Scripts/jquery.quicksearch.js"></script>
<script type="text/javascript">

    $(document).ready(function () {
        
        InitializeTable();
        $('input#search_dgValue').quicksearch('table#ShowDoShowTable_dgValue tbody tr ');
        FormatTable();
    });

	function InitializeTable() {
	    var grid = $("#ctl00_ContentPlaceHolder_UC_WFExecution_ShowDoShowTable_dgValue");

	    //Por cada GridView que se encuentre modificar el código HTML generado para agregar el THEAD.
	    if (grid.find("tbody > tr > th").length > 0) {
	        grid.find("tbody").before("<thead><tr></tr></thead>");
	        grid.find("thead:first tr").append(grid.find("th"));
	        //grid.find("tbody tr:first").remove();
	    }

        //Cuando no hay datos se acondiciona la apariencia
	    if (grid.find("tr").length == 1) {
	        $("#GridContainer").css("overflow", "hidden");
	        $("#GridContainer").css("height", "50px");
	        $('#<%=_btnok.ClientID %>').attr("disabled", "disabled");
	    }
	    
	}

    //Esta función evalúa si está la opción de checkeo múltiple activa, sino lo esta, hace que los checkbox de seleccion
    //se comporten como radiobuton
	function MultipleCheckFuncionality(chkObject) {
		//Al seleccionar un item se habilita el botón OK
		$('#<%=_btnok.ClientID %>').removeAttr("disabled");
	    var selectedChecksIndexes = "";
		var isMultipleCheck = $("#<%=hdnMultipleCheck.ClientID %>").val().toLowerCase();
        
        if (isMultipleCheck == "false") {
            //Obtenemos la tabla del gridView
            var tblControl = $("#<%=dgValue.ClientID %>");
            //Extraemos todos los elementos del tag input.
            var lstControls = tblControl.getElementsByTagName("input");
            var max = lstControls.length;
            var i = 0;
            //Descheckeamos todos los checkbox y agregamos los indices de los checkeados en el hdnChecks
            for (i = 0; i < max; i++) {
                if (lstControls[i].id.indexOf("chkSelected", 0) != -1) {
                    if (lstControls[i].checked) {
                        selectedChecksIndexes = i;
                    }
                    lstControls[i].checked = false;
                }
            }
            //Checkeamos el control clickeado
            chkObject.checked = true;                   
        }
        else
        {
            var anyChecked = false;
            //Obtenemos la tabla del gridView
            //var tblControl = $("#<%=dgValue.ClientID %>");
            var tblControl =  document.getElementById("<%=dgValue.ClientID %>")
            //Extraemos todos los elementos del tag input.
            var lstControls = tblControl.getElementsByTagName("input");
            var max = lstControls.length;
            var i = 0;
            //Agregamos los indices de los checks checkeados en el hdnChecks
            for (i = 0; i < max; i++) {
                if (lstControls[i].id.indexOf("chkSelected", 0) != -1) {
                    if (lstControls[i].checked) {
                        anyChecked = true;
                        selectedChecksIndexes += (selectedChecksIndexes !== "" ? "," : "") + i;
                    }
                }
            }

            if (anyChecked) {
                $('#<%=_btnok.ClientID %>').removeAttr("disabled");
            }
            else {
                $('#<%=_btnok.ClientID %>').attr("disabled", "disabled");
            }
        }

        getHdnChecks().val(selectedChecksIndexes);    
        return true;
    }

    function FormatTable() {
        var div = $("#<%=GridContainer.ClientID %> div:first");
        var table = $("#<%=GridContainer.ClientID %> table:first");
        table.appendTo("#<%=GridContainer.ClientID %>");
        div.remove();
    }
</script>

<style type="text/css">
    .notFixed
    {
        table-layout: auto !important;
    }
</style>

   <asp:HiddenField ID="hdnMultipleCheck" runat="server" />
   <div style="text-align:left">
     
                Buscar
                <input  type="text" id="search_dgValue" class="form-control"/>
      
    </div>
    <div id="GridContainer" runat="server" style="overflow: auto; height:440px; width:770px; padding-bottom:15px">
        <asp:GridView ID="dgValue" runat="server" AllowPaging="false"
            AllowSorting="false" GridLines="None"
            EmptyDataText="No hay registros para visualizar"
            CssClass="mGrid notFixed"
            PagerStyle-CssClass="pgr"
            AlternatingRowStyle-CssClass="alt"            
            >
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelected" Enabled="true" runat="server"  OnClick="MultipleCheckFuncionality(this);" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
   
    </div>
     <div style="text-align:center ;margin-top:10px;">
    <asp:Button ID="_btnok" Text="Ok" runat="server" UseSubmitBehavior="false" OnClick="_btnOk_Click" Width="97px" disabled="true" class="btn btn-primary btn-xs"/>
    <asp:Button ID="_btnCancel" Text="Cancel" runat="server" Width="102px" UseSubmitBehavior="false" Class="btn btn-primary btn-xs"
        OnClick="_btnCancel_Click" />
    </div>
