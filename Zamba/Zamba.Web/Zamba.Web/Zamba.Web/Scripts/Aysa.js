

var itemToScroll = null; 
function ValidateIndustryBeforeSave(evt) {
    var isValid = true;
    //Recorro elementos del fieldset validando si se completaron
    $('.GeneralFormControl').each(function(i, item) {
        if (!$(item).valid()) {
            isValid = false;
            $('#TAB').tabber.tabShow(0);
            evt.preventDefault();
			
            if(itemToScroll === null)
						itemToScroll = $(item);
        }
    });

    $('.ComercialDataFormControl').each(function(i, item) {
        if (!$(item).hasClass('disable') && !$(item).valid()) {
            isValid = false;
            $('#TAB').tabber.tabShow(6);
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
        $("#hdnRuleId").name = "zamba_save";
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
        $('#TAB').tabber.tabShow(1);
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

//Funcion que eval�a si una tecla pulsada pertenece al tipo dado
function KeyIsValid(e, TypeToValidate, Obj) {

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

//Funci�n que evalua si habilitar o no un control en base a uno o dos valores
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

//Funci�n que evalua si habilitar o no un control en base a uno o dos valores(evalua por distinto)
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
    if ($('#aspnetForm').length && !$('#aspnetForm').valid()) {
        evt.preventDefault();
		setTimeout("parent.hideLoading();", 500);
    }
    else {
        $("#hdnRuleId").name = "zamba_save";
    }
}

function ValidateBeforeSaveInsert() {
    ConvertToUpperForm();
    if (!$('#aspnetForm').valid()){
		setTimeout("parent.hideLoading();", 500);
		return false;
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
		
//Funci�n que evalua si hacer requerido o no un control en base a uno o dos valores
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

function SeparateGridInspecciones() {
    var tableRows = $("#zamba_associated_documents_1028")[0].rows;
    if (tableRows.length > 0) {
        var headerTable = tableRows[0].cloneNode(1);
        var RowsBody = new Array();
        var i;

        for (i = 1; i < tableRows.length; i++) {
            if (tableRows[i].cells[2].innerHTML == "Realizada")
                RowsBody.push(tableRows[i]);
        }

        if (RowsBody.length > 0) {
            $("#tableRealizadas").append(headerTable);
            for (i = 0; i < RowsBody.length; i++)
                $("#tableRealizadas").append(RowsBody[i]);
        } 
    }
}