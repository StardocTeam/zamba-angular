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
//    AddFilter('zamba_index_1185',false);
//    AddFilter('zamba_index_1189',false);
//    AddFilter('zamba_index_1223',false);
}
);

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
        default:
            if (TypeToValidate.indexOf("decimal") < 0)
                break;

            var arrayOfConfiguration = TypeToValidate.split('_');
            if (arrayOfConfiguration.length < 2)
                break;

            var decimalCount = arrayOfConfiguration[1];
            if (!decimalCount)
                break;

            var integerCount = arrayOfConfiguration[2];
            if (!integerCount)
                break;

            var currentValue = $(Obj).val();
            var commaPos = currentValue.indexOf(',');
            var caretPos = $(Obj).caret().start;

            if (key == 44)
                isValid = (commaPos == -1 && caretPos != 0);
            else {
                if ((key >= 48 && key <= 57)) {
                    if (commaPos == -1) {
                        isValid = (currentValue.length < integerCount);
                    }
                    else {
                        if (caretPos > commaPos) {
                            isValid = (currentValue.length - decimalCount < commaPos + 1);
                        }
                        else
                            isValid = (commaPos < integerCount);
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
        $("#hdnRuleId").name = "zamba_save";
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
		//$("#" + ObjDestiny).addClass("required");
		$("#" + ObjDestiny).rules('add', 'required');
	}
	else {
	    //$("#" + ObjDestiny).removeClass("required");
	    $("#" + ObjDestiny).rules('remove', 'required');
		$("#" + ObjDestiny).valid();
		$("#lbl_" + ObjDestiny).hide();
	}
}
