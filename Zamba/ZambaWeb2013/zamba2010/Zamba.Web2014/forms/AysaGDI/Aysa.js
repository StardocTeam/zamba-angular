//Ezequiel
$(document).ready(
function () {
    // $("form").after('<div id="dynamic_filter" style="display:none;"></div>');
    //    $("select").each(
    //    function () {
    //        if (this.id.toLowerCase().startsWith("zamba_index")) {
    //          var btnid = this.id.toString().split('_')[this.id.toString().split('_').length - 1];
    //           var $btnfilter = $('<input id="table_filter_ ' + btnid + '" type="button" value="Filtrar" class="submit" style=" margin-left:10px;" onclick="CreateTable(this)"/>');
    //           $("#" + this.id).after($btnfilter);
    //       }
    //   }
    //);
    AddFilter('zamba_index_1185',false);
    AddFilter('zamba_index_1189',false);
    AddFilter('zamba_index_1223',false);
}
);


//Funcion que agrega un filtro al control
//indexid = id del tag que contiene el indice a aplicar el control
// codecolumn = true/false si se quiere visualizar o no la columna de codigo
function AddFilter(indexid,codecolumn) {
    var btnid = indexid.toString().split('_')[indexid.toString().split('_').length - 1];
    var $btnfilter = $('<img id="table_filter_ ' + btnid + '" onclick="CreateTable(this,' + codecolumn + ');" style="height: 16px; margin-left:10px;" alt="Filtrar" src="../../forms/images/UI_blue/search.gif"/>');
    $("#" + indexid).after($btnfilter);
}

function CreateTable(obj, codecolumn) {
	parent.ShowLoadingAnimation();
	
    //Creando el nombre del select
    var arrayObj = obj.id.toString().split('_');
    var cBox = "zamba_index_" + arrayObj[arrayObj.length - 1].trim();
    $('#dynamic_filter').data("indexid", cBox);

    var table = LoadTable($('#dynamic_filter').data("indexid"), codecolumn);

    //Limpiando la tabla
    $('#dynamic_filter').html("");

    //Agregando los botones
    var html = table;
    html += '<center><input id="btnAceptDynamic" type="button" value="Aceptar" onclick="Accept()" />';
    html += '<input id="btnCancelDynamic" type="button" value="Cancelar" style=" margin-left:10px;" onclick="Cancel()" /></center>';

    //Agregando la tabla creada dinamicamente
    $('#dynamic_filter').html(html);


    $("#example tbody").click(function (event) { $(oTable.fnSettings().aoData).each(function () { $(this.nTr).removeClass('row_selected'); }); $(event.target.parentNode).addClass('row_selected'); });

    //Agregando el evento click a cada fila
    $('#example tr').click(function () {
        if ($(this).hasClass('row_selected'))
            $(this).removeClass('row_selected');
        else
            $(this).addClass('row_selected');
    });

    /* Add a click handler for the delete row */
    $('#delete').click(function () {
        var anSelected = fnGetSelected(oTable);
        oTable.fnDeleteRow(anSelected[0]);
    });

    /* Init the table */
    var oTable = $('#example').dataTable({ "bPaginate": false, "bLengthChange": false, "bInfo": false, "bAutoWidth": false, "bSort": false });

    //Abriendo el dialgo modal
    $("#dynamic_filter").dialog({ autoOpen: false, modal: true, position: 'center', closeOnEscape: true, draggable: true, resizable: false, open: function (event, ui) { $(".ui-dialog-titlebar-close").hide(); }, height: 600, width: 600, position:'top' });
    $("#dynamic_filter").dialog("open");

	setTimeout("parent.hideLoading();", 500);
}

function Cancel() {
    $("#dynamic_filter").dialog("close");
}

function Accept() {
    var rows = $('#example tbody tr');

    var value = "";
    for (var i = 0; i < rows.length; i++) {
        if ($(rows[i]).hasClass('row_selected')) {
            value = rows[i];
            value = $(value).find("td")[0].innerHTML;
        }
    }
    if (value == "") {
        alert("Por favor seleccione una fila");
    }
    else {
        $('#' + $('#dynamic_filter').data("indexid") + ' option').each(
                     function () {
                         if ($(this).val().toString().trim() == value.toString().trim()) {
                             $(this).attr("selected", "selected");
                             $(this).parent().change();
                             $(this).parent().valid();
                         }
                     });
        Cancel();
    }
}



/*
* I don't actually use this here, but it is provided as it might be useful and demonstrates
* getting the TR nodes from DataTables
*/
function fnGetSelected(oTableLocal) {
    var aReturn = new Array();
    var aTrs = oTableLocal.fnGetNodes();

    for (var i = 0; i < aTrs.length; i++) {
        if ($(aTrs[i]).hasClass('row_selected')) {
            aReturn.push(aTrs[i]);
        }
    }
    return aReturn;
}

function LoadTable(cbox, codecolumn) {
    var t = "";
    var select = $("#" + cbox + " option");
    t = '<div style="height: 250px; overflow:scroll"><table cellpadding="0" cellspacing="0" border="0" class="display" id="example">';

    //Cabezera
    t += "<thead>";
    t += "<tr>";
    if (codecolumn)
        t += "<th>Codigo</th>";
    else
        t += '<th style="display:none;">Codigo</th>';
    t += "<th>Descripcion</th>";
    t += "</tr>";
    t += "</thead>";

    //Cuerpo
    t += "<tbody>";
    for (var i = 0; i < select.length; i++) {
        t += '<tr class="gradeA">';
        if ( codecolumn)
            t += "<td>";
        else
            t += '<td style="display:none;">';
        t += select[i].value;
        t += "</td>";
        t += "<td>";
        t += select[i].text;
        t += "</td>";
        t += "</tr>";
    }
    t += "</tbody>";

    //Final
    t += "<tfoot>";
    t += "<tr>";
    t += "</tr>";
    t += "</tfoot>";


    t += "</table></div>";

    return t;
}

var itemToScroll = null; 
function ValidateIndustryBeforeSave(evt) {
    var isValid = true;
    //Recorro elementos del fieldset validando si se completaron
    $('.GeneralFormControl').each(function(i, item) {
        if (!$(item).valid()) {
            isValid = false;
            document.getElementById('TAB').tabber.tabShow(0);
            evt.preventDefault();
			
            if(itemToScroll === null)
						itemToScroll = $(item);
        }
    });

    $('.ComercialDataFormControl').each(function(i, item) {
        if (!$(item).hasClass('disable') && !$(item).valid()) {
            isValid = false;
            document.getElementById('TAB').tabber.tabShow(6);
            evt.preventDefault();
			
			if(itemToScroll === null)
						itemToScroll = $(item);
        }
    });
	
	if(itemToScroll != null){
		$.scrollTo(itemToScroll,600);
		setTimeout("itemToScroll.focus();itemToScroll=null;",600);
	}

    if (isValid)
        document.getElementById("hdnRuleId").name = "zamba_save";
    else {
        setTimeout("parent.hideLoading();", 1000);
    }
}



function ValidacionPersonalizada(evt) {
    var isValid = true;
    //Recorro elementos del fieldset validando si se completaron
    $('.GeneralFormControl').each(function (i, item) {
        if (!$(item).valid()) {
            isValid = false;
        }
    });


    if (!isValid) {
        evt.preventDefault();
    }
    else {
        document.getElementById('TAB').tabber.tabShow(1);
        setTimeout("parent.hideLoading();", 500);
    }
}


function ValidateLength(element, RequiredLength) {
    var Str = element.value;
    $(element).valid();
    if (Str.length < RequiredLength)
        return true;
    return false;
}

//Funcion que evalúa si una tecla pulsada pertenece al tipo dado
function KeyIsValid2(e, TypeToValidate, Obj) {

    var key = e.charCode || e.keyCode || 0;
    var isValid = false;

    switch (TypeToValidate) {
        case "numeric":
            if (key >= 48 && key <= 57)
                isValid = true;
            break;
        case "date":
            if ((key >= 48 && key <= 57) || key == 47)
                isValid = true;
            break;
        case "2decimal":
            var currentValue = $("#" + Obj).val();
            var commaPos = currentValue.indexOf(',');
            var caretPos = $("#" + Obj).caret().start;

            if (key == 44)
                isValid = (commaPos == -1 && caretPos != 0);
            else {
                if ((key >= 48 && key <= 57)) {
                    if (commaPos == -1) {
                        isValid = (currentValue.length < 6);
                    }
                    else {
                        if (caretPos > commaPos) {
                            isValid = (currentValue.length - 3 < commaPos);
                        }
                        else
                            isValid = (commaPos < 6);
                    }
                }
            }
            break;
		case "5decimal":
            var currentValue = $("#" + Obj).val();
            var commaPos = currentValue.indexOf(',');
            var caretPos = $("#" + Obj).caret().start;
            if (key == 44)
                isValid = (commaPos == -1 && caretPos != 0);
            else {
                if ((key >= 48 && key <= 57)) {
                    if (commaPos == -1) {
                        isValid = (currentValue.length < 6);
                    }
                    else {
                        if (caretPos > commaPos) {
                            isValid = (currentValue.length - 6 < commaPos);
                        }
                        else
                            isValid = (commaPos < 6);
                    }
                }
            }
            break;
    }

    return isValid;
}

//Función que evalua si habilitar o no un control en base a uno o dos valores
function ConditionalEnable(ObjSource, ObjDestiny, Value, Value2) {
    if ($("#" + ObjSource).val() == Value || (Value2 != undefined && $("#" + ObjSource).val() == Value2)) {

        //Quita la clase que ignora el validate y valida el control nuevamente
        $("#" + ObjDestiny).removeClass("disable");
        $("#" + ObjDestiny).valid();
        //Habilita el datepicker
        $("#" + ObjDestiny).datepicker("enable");
        //Muestra el asterisco para marcar que es un campo requerido.
        $("#lbl_" + ObjDestiny).show();
        $("#" + ObjDestiny).attr('disabled', '');

        if ($("#" + ObjDestiny).datepicker("option", "disabled") === undefined) {
            $("#" + ObjDestiny).attr('disabled', 'disabled');
        }
    }
    else {
        $("#" + ObjDestiny).attr('disabled', 'disabled');
        $("#" + ObjDestiny).val("");
        //Agrega la clase que ignora el validate y valida el control nuevamente
        $("#" + ObjDestiny).addClass("disable");
        $("#" + ObjDestiny).valid();
        //Oculta el asterisco para marcar que es un campo requerido.
        $("#lbl_" + ObjDestiny).hide();
        //deshabilita el datepicker
        $("#" + ObjDestiny).datepicker("disable");
    }
}

//Función que evalua si habilitar o no un control en base a uno o dos valores(evalua por distinto)
function ConditionalEnableFalse(ObjSource, ObjDestiny, Value, Value2) {
    if ($("#" + ObjSource).val() != Value && (Value2 != undefined && $("#" + ObjSource).val() != Value2)) {

        //Quita la clase que ignora el validate y valida el control nuevamente
        $("#" + ObjDestiny).removeClass("disable");
        $("#" + ObjDestiny).valid();
        //Habilita el datepicker
        $("#" + ObjDestiny).datepicker("enable");
        //Muestra el asterisco para marcar que es un campo requerido.
        $("#lbl_" + ObjDestiny).show();
        $("#" + ObjDestiny).attr('disabled', '');

        if ($("#" + ObjDestiny).datepicker("option", "disabled") === undefined) {
            $("#" + ObjDestiny).attr('disabled', 'disabled');
        }
    }
    else {
        $("#" + ObjDestiny).attr('disabled', 'disabled');
        $("#" + ObjDestiny).val("");
        //Agrega la clase que ignora el validate y valida el control nuevamente
        $("#" + ObjDestiny).addClass("disable");
        $("#" + ObjDestiny).valid();
        //Oculta el asterisco para marcar que es un campo requerido.
        $("#lbl_" + ObjDestiny).hide();
        //deshabilita el datepicker
        $("#" + ObjDestiny).datepicker("disable");
    }
}

function LoadLoadingRuleButtons() {
    var buttons = $("input");
    buttons.each(function() {
        if (this.id.indexOf("zamba_rule") != -1)
            $(this).click(ShowLoadingAnimation);
    });
}

function ValidateBeforeSave(evt) {
    ConvertToUpperForm();
    if (!$('#aspnetForm').valid()) {
        evt.preventDefault();
		setTimeout("parent.hideLoading();", 500);
    }
    else {
        document.getElementById("hdnRuleId").name = "zamba_save";
    }
}

function ValidateBeforeSaveInsert() {
    ConvertToUpperForm();
    if ($('#aspnetForm').valid())
        __doPostBack('ctl00$ContentPlaceHolder$InsertControl$btn_insertar', '');
	else{
		setTimeout("parent.hideLoading();", 500);
	}
}


        function ConvertToUpperForm() {
            $('.ConvertToUpper').each(function(i, item) {
                var cadena = $(item).val();
                cadena = cadena.toUpperCase();
                $(item).val(cadena);
            });
        }
        
        function ReadOnlyform() {
        if ($("#zamba_index_1269").val() == 2) {
            $('.ZambaRule').each(function(i, item) {
                $(item).hide();
            });
            }
        }
		
//Función que evalua si hacer requerido o no un control en base a uno o dos valores
function ConditionalRequire(ObjSource, ObjDestiny, Value, Value2) {
	if ($("#" + ObjSource).val() == Value || (Value2 != undefined && $("#" + ObjSource).val() == Value2)) {
		$("#" + ObjDestiny).valid();
		$("#lbl_" + ObjDestiny).show();
		$("#" + ObjDestiny).addClass("required");
	}
	else {
		$("#" + ObjDestiny).removeClass("required");
		$("#" + ObjDestiny).valid();
		$("#lbl_" + ObjDestiny).hide();
	}
}
