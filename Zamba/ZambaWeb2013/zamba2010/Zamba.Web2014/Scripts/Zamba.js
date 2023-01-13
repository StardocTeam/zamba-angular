function GTUID() {
    var e = $('#ctl00_hdnUserId');
    if (e.length == 0) e = $('#ctl00_ContentPlaceHolder_hdnUserId');
    if (e.length == 0) e = $('#hdnUserId');
    if (e.length == 0) e = $('hdnUserId');

    var userID = $(e).val();

    return userID;
}

$(function () {
    $(".fg-button:not(.ui-state-disabled)").hover
    (
        function () {
            $(this).addClass("ui-state-hover");
        },
        function () {
            $(this).removeClass("ui-state-hover");
        }
    )
    .mousedown
    (
        function () {
            $(this).parents('.fg-buttonset-single:first').find(".fg-button.ui-state-active").removeClass("ui-state-active");
            if ($(this).is('.ui-state-active.fg-button-toggleable, .fg-buttonset-multi .ui-state-active')) {
                $(this).removeClass("ui-state-active");
            }
            else {
                $(this).addClass("ui-state-active");
            }
        }
    )
    .mouseup
    (
        function () {
            if (!$(this).is('.fg-button-toggleable, .fg-buttonset-single .fg-button, .fg-buttonset-multi .fg-button')) {
                $(this).removeClass("ui-state-active");
            }
        }
     );
});

var previousWidth;

function makeCalendar(id) {
    $(function () {
        if (id.indexOf("completarindice") != -1) {
            if ($('#' + id).lenght > 0) $('#' + id).datepicker({
                changeMonth: true,
                changeYear: true,
                showOn: 'button',
                buttonText: 'Abrir calendario',
                buttonImage: '../../Content/Images/calendar.png',
                buttonImageOnly: true,
                duration: "",
                onClose: function () {
                    //Restauramos el width original
                    $("#DivIndices").animate({ width: previousWidth }, "fast");
                    $("#separator").animate({ width: previousWidth }, "fast");

                    //Acomodamos el documento a lo restaurado.
                    var a = $(window).width() - previousWidth - 5;
                    $("#separator").next().animate({ width: a }, "fast");
                },
                beforeShow: function (input, inst) {
                    //Guardamos el width anterior.  
                    previousWidth = $("#DivIndices").width();
                    //Saco el ancho de los labels
                    var labelWidth = $(input).parent().prev().width();

                    var calcultateWidth = $(inst.dpDiv).width() + labelWidth + 20;

                    $("#DivIndices").animate({ width: calcultateWidth }, "slow");
                    $("#separator").animate({ width: calcultateWidth }, "slow");

                    //Acomodamos el documento, con la diferencia de pantalla.
                    var a = $(window).width() - calcultateWidth - 5;
                    $("#separator").next().animate({ width: a }, "slow");
                }
            });
        }
        else {
            if ($('#' + id).lenght > 0) $('#' + id).datepicker({
                changeMonth: true,
                changeYear: true,
                showOn: 'button',
                buttonText: 'Abrir calendario',
                buttonImage: '../../Content/Images/calendar.png',
                buttonImageOnly: true,
                duration: "",
                dateFormat: "dd-mm-yyyy"
            });
        }
    });
}

function AddCalendar(id, limit) {
    if (id.indexOf("completarindice") != -1) {
        if ($('#' + id).length > 0) $('#' + id).datepicker({
            changeMonth: true,
            changeYear: true,
            showOn: 'button',
            buttonText: 'Abrir calendario',
            buttonImage: '../../Content/Images/calendar.png',
            buttonImageOnly: true,
            duration: "",
            onClose: function () {
                //Restauramos el width original
                $("#DivIndices").animate({ width: previousWidth }, "fast");
                $("#separator").animate({ width: previousWidth }, "fast");

                //Acomodamos el documento a lo restaurado.
                var a = $(window).width() - previousWidth - 5;
                $("#separator").next().animate({ width: a }, "fast");
            },
            beforeShow: function (input, inst) {
                //Guardamos el width anterior.  
                previousWidth = $("#DivIndices").width();
                //Saco el ancho de los labels
                var labelWidth = $(input).parent().prev().width();

                var calcultateWidth = $(inst.dpDiv).width() + labelWidth + 20;

                $("#DivIndices").animate({ width: calcultateWidth }, "slow");
                $("#separator").animate({ width: calcultateWidth }, "slow");

                //Acomodamos el documento, con la diferencia de pantalla.
                var a = $(window).width() - calcultateWidth - 5;
                $("#separator").next().animate({ width: a }, "slow");
            }
        });
    }
    else {
        $(function () {
            if ($('#' + id).length > 0) {
                $('#' + id).datepicker({
                    changeMonth: true,
                    changeYear: true,
                    showOn: 'button',
                    buttonText: 'Abrir calendario',
                    buttonImage: '../../Content/Images/calendar.png',
                    buttonImageOnly: true,
                    duration: "",
                    dateFormat: 'dd/mm/yy',
                    constrainInput: true
                });
            }
        });
    }
}

function windowHeight() {
    var myHeight = 0;
    if (typeof (window.innerWidth) == 'number') {
        myHeight = window.innerHeight;
    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        myHeight = document.documentElement.clientHeight;
    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        myHeight = document.body.clientHeight;
    }
    return myHeight;
}

function windowWidth() {
    var myWidth = 0;
    if (typeof (window.innerWidth) == 'number') {
        myWidth = window.innerWidth;
    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        myWidth = document.documentElement.clientWidth;
    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        myWidth = document.body.clientWidth;
    }
    return myWidth;
}

function ajustarselects() {
    $('select').each(function () {
        if (!$(this).hasClass("notExpand")) {
            if ($(this).attr('multiple') == false) {
                $(this).mousedown(function () {
                    if ($(this).css("width") != "auto") {
                        var width = $(this).width();
                        $(this).data("origWidth", $(this).css("width")).css("width", "auto");

                        // If the width is now less than before then undo 
                        if ($(this).width() < width) {
                            $(this).unbind('mousedown');
                            $(this).css("width", $(this).data("origWidth"));
                        }
                    }
                })

                // Handle blur if the user does not change the value 
                .blur(function () {
                    $(this).css("width", $(this).data("origWidth"));
                })

                // Handle change of the user does change the value 
                .change(function () {
                    $(this).css("width", $(this).data("origWidth"));
                });
            }
        }
    });
}

function expandDiv(div, difwidth, difheight) {
    var myWidth = 0, myHeight = 0;
    if (typeof (window.innerWidth) == 'number') {
        //Non-IE
        myWidth = window.innerWidth;
        myHeight = window.innerHeight;
    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        //IE 6+ in 'standards compliant mode'
        myWidth = document.documentElement.clientWidth;
        myHeight = document.documentElement.clientHeight;
    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        //IE 4 compatible
        myWidth = document.body.clientWidth;
        myHeight = document.body.clientHeight;
    }
    myWidth = myWidth - difwidth;
    myHeight = myHeight - difheight
    setInterval("doExpand('" + div + "'," + myWidth + "," + myHeight + ")", 500);
}

function doExpand(div, w, h) {
    $("#" + div).width(w);
    $("#" + div).height(h);
}

function modifyTextBoxSize(tb) {
    if (tb.rows == 5) {
        tb.rows = 1;
    }
    else {
        tb.rows = 5;
    }
}

function maxLenght(tb, max) {
    return (tb.value.length < max);
}

function getHeightScreen() {
    //Obtiene la altura que deben tener los contenedores principales
    var masterHeader = $("#MasterHeader").css("display") == "none" ? 0 : $("#MasterHeader").height();
    var barraCabecera = $("#barra-Cabezera").css("display") == "none" ? 0 : $("#barra-Cabezera").innerHeight();
    var headersHeight = masterHeader + barraCabecera;
    var currentheight = document.body.clientHeight - headersHeight;
    if (currentheight > 500) {
        return currentheight;
    }
    else {
        return 500;
    }
}

function getMTHeight() {
    var mt = $("#mainMenu");
    if (mt) {
        return $(mt).height();
    }
    else {
        return 0;
    }
}

function getTBHeight() {
    var mt = $("#TasksDivUL");
    if (mt) {
        return $(mt).height();
    }
    else {
        return 0;
    }
}

function GetDocumentAvailableHeight() {
    var mainMenuHeight = (typeof parent.getMainMenuHeightFromParent == "function") ? parent.getMainMenuHeightFromParent() : 0;
    var tbh = (typeof parent.getTBH == "function") ? parent.getTBH() : 0;

    var gtHeightScreen = getHeightScreen();
    var availableHeight = (gtHeightScreen == 0) ? parent.GetTaskAvailableHeight() : gtHeightScreen;

    var tToolBTaskH = (getToolBTaskH() == 0) ? 30 : getToolBTaskH();
    var tTabDetH = (getTabDetH() == 0) ? 30 : getTabDetH();

    return availableHeight - mainMenuHeight - tbh - tToolBTaskH - tTabDetH;
}

function ValidateLength(element, RequiredLength) {
    var Str = element.value;
    $(element).valid();
    if (Str.length < RequiredLength)
        return true;
    return false;
}

function KeyIsValid(e, TypeToValidate, Obj) {
    if (e.keyCode > 0) return true;

    var key = e.charCode || 0;
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

function SelectErrorTab(control) {
    if (!tabErrorSelected) {
        var currTabber;
        var i;

        //por cada tabber
        $(".tabberlive").each(function () {
            i = 0;
            currTabber = this;

            //por cada tab
            $(this).find(".tabbertab").each(function () {
                //Si la tab contiene al elemento a resaltar, selecciono la tab
                if ($(this).find("#" + control.id).length > 0) {
                    currTabber.tabber.tabShow(i);
                    i = 0;
                    return;
                }

                //Si el tab no conteien al elemento y es la tad del tabber que estamos recorriendo incremento el contador.
                if ($(this).parent()[0] != null && (currTabber.uniqueID == $(this).parent()[0].uniqueID)) {
                    i++;
                }
            });
        });
        tabErrorSelected = true;
    }
}

var validatorExtended = false;
var tabErrorSelected = false;
var lastErrorElement;

function SetValidationsAction(SubmitButtonId) {
    if (jQuery.data(document.forms[0], "validator") == null) {
        $('#aspnetForm').validate({
            onsubmit: false
        });
    }
    //Obtenemos la funcion de hightlight
    var validator = jQuery.data(document.forms[0], "validator");
    if (validator == undefined) return;

    var validatorHightlight = validator.settings.highlight;
    validator.settings.highlight = null;
    var i;

    validator.settings.highlight = function (a, b, d) {
        validatorHightlight(a, b, d);

        if (lastErrorElement != a.uniqueID || a[0] == undefined) {
            lastErrorElement = a.uniqueID;
            tabErrorSelected = false;
        }

        SelectErrorTab(a);
    };

    SetFieldsValidations();

    if (!validatorExtended) {
        validatorExtended = ExtendValidator();
    }

    if (SubmitButtonId) {
        var element = document.getElementById(SubmitButtonId);
        if (element) {
            var eClick = element.onclick;
            element.onclick = null;

            $(element).click(function (evt) {
                if ($(this).attr("id").indexOf("WFExecution") > -1) {

                    if (DoRuleValidation(this, evt)) {
                        ShowLoadingAnimation();
                        if (eClick)
                            eClick();
                    }
                    else {
                        return false;
                    }
                }
                else {
                    if (DoGeneralValidation(this, evt)) {
                        if (eClick)
                            eClick();
                    }
                    else {
                        return false;
                    }
                }
            });
        }
    }
}

function DoGeneralValidation(objBtn, evt) {
    if (!$("#aspnetForm").valid()) {
        evt.stopPropagation();
        evt.preventDefault();
        setTimeout("parent.hideLoading();", 1000);

        var msgDialog = $("#divValidationFail");

        if (msgDialog) {
            $(msgDialog).dialog({ autoOpen: false, modal: true, position: 'center', closeOnEscape: false, draggable: false, resizable: false, open: function (event, ui) { $(".ui-dialog-titlebar-close").hide(); } });
            $(msgDialog).dialog('open');
        }

        return false;
    }
    else {
        return true;
    }
}

function DoRuleValidation(objBtn, evt) {
    var isValid = true;
    var divContainter = $('[id$="pnlUcRules"]');
    divContainter.find(".RuleField").each(function () {
        if (!$(this).valid()) {
            isValid = false;
            return false;
        }
    });

    if (!isValid) {
        setTimeout("parent.hideLoading();", 1000);
        var msgDialog = $("#divValidationFail");
        if (msgDialog) {
            $(msgDialog).dialog({ autoOpen: false, modal: true, position: 'center', closeOnEscape: false, draggable: false, resizable: false, open: function (event, ui) { $(".ui-dialog-titlebar-close").hide(); } });
            $(msgDialog).dialog('open');
        }
        return false;
    }
    else {
        return true;
    }
}

function SetFieldsValidations() {
    SetValidateForLength();
    SetValidateForDataType();
    SetValidateForRequired();
    SetDefaultValues();
    SetHierarchicalFunctionally();
    SetMinValuesValidations();
    SetMaxValuesValidations();
}

function SetValidateForLength() {
    $(".length").each(function () {
        if (!$(this).hasClass("readonly")) {
            var length = $(this).attr("length");
            if (length) {
                $(this).change(function (evt) {
                    if (!ValidateLength(this, length))
                        evt.preventDefault();
                });

                $(this).keypress(function (evt) {
                    if (!ValidateLength(this, length))
                        evt.preventDefault();
                });
                $(this).rules("add", {
                    maxlength: length,
                    messages: {
                        maxlength: jQuery.validator.format("Se ha exedido largo de " + length + " caracteres.")
                    }
                });
            }
        }
    });
}

$.validator.methods.number = function (value, element) {
    return this.optional(element) || /\d{1,3}(\.\d{3})*(\.\d\d)?|\.\d\d/.test(value);
}

function SetValidateForDataType() {
    $(".dataType").each(function () {
        if (!$(this).hasClass("readonly")) {
            var dataType = $(this).attr("dataType");
            if (dataType) {
                $(this).change(function (evt) {
                    if (!KeyIsValid(evt, dataType, this))
                        evt.preventDefault();
                });

                $(this).keypress(function (evt) {
                    if (!KeyIsValid(evt, dataType, this))
                        evt.preventDefault();
                });


                switch (dataType) {
                    case "numeric":
                        $(this).rules("add", {
                            digits: true,
                            messages: {
                                digits: jQuery.validator.format("Solo se permite un n&uacute;mero entero.")
                            }
                        });
                        break;
                    case "date":
                        $(this).rules("add", {
                            dateAR: true,
                            messages: {
                                dateAR: jQuery.validator.format("La fecha debe estar en formato:<br/> dia/mes/año.")
                            }
                        });
                        MakeDateIndexs(this);
                        break;
                    default:
                        $(this).rules("add", {
                            number: true,
                            messages: {
                                number: jQuery.validator.format("Solo se permiten n&uacute;meros.")
                            }
                        });
                        break;
                }
            }
        }
    });
}

function SetMinValuesValidations() {
    $(".haveMinValue").each(function () {
        if (!$(this).hasClass("readonly")) {
            var minValue = $(this).attr("ZMinValue");
            var dataType = $(this).attr("dataType");
            var aceptEquals = minValue.startsWith("=");
            minValue = minValue.replace("=", "");

            if (minValue) {
                //Si existe la funcion para reemplazar con el dia de hoy

                if (minValue == "ZGetDate") {
                    minValue = getLocaleShortDateString(new Date());
                }

                //Si es menor o igual
                if (aceptEquals) {
                    $(this).rules("add", {
                        lessEqualThan: minValue,
                        messages: {
                            lessEqualThan: "El valor debe ser mayor o igual que: " + minValue
                        }
                    });
                }
                else {
                    $(this).rules("add", {
                        lessThan: minValue,
                        messages: {
                            lessThan: "El valor debe ser mayor que: " + minValue
                        }
                    });
                }

                if (dataType == "date") {
                    var minDate;
                    if (aceptEquals)
                        minDate = CreateDate(Number(minValue.split("/")[0]), minValue.split("/")[1] - 1, minValue.split("/")[2]);
                    else
                        minDate = CreateDate(Number(minValue.split("/")[0]) + 1, minValue.split("/")[1] - 1, minValue.split("/")[2]);

                    $(this).datepicker("option", "minDate", minDate);
                }
            }
        }
    });
}

function Hidden(objID) {

    var obj = document.getElementById(objID);
    var controlType = obj.nodeName.toLowerCase();

    if (obj) {
        switch (controlType) {
            case "input":
                switch (obj.type.toLowerCase()) {
                    case "checkbox":
                        $(obj).css("display", "none");
                        break;
                    case "text":
                        $(obj).parent().css("display", "none");
                        $(obj).parent().prev().css("display", "none");
                        break;
                    default:
                        $(obj).css("display", "none");
                }
                break;
            case "textarea":
            case "select":
                $(obj).css("display", "none");
                break;
        }
    }
}

function Visible(objID) {
    var obj = document.getElementById(objID);
    var controlType = obj.nodeName.toLowerCase();

    if (obj) {
        switch (controlType) {
            case "input":
                switch (obj.type.toLowerCase()) {
                    case "checkbox":
                        $(obj).css("display", "block");
                        break;
                    case "text":
                        $(obj).parent().css("display", "block");
                        $(obj).parent().prev().css("display", "block");
                        break;
                    default:
                        $(obj).css("display", "block");
                }
                break;
            case "textarea":
                $(obj).css("display", "block");
                break;
            case "select":
                $(obj).css("display", "block");
        }
    }
}

function SetMaxValuesValidations() {
    $(".haveMaxValue").each(function () {
        if (!$(this).hasClass("readonly")) {
            var maxValue = $(this).attr("ZMaxValue");
            var dataType = $(this).attr("dataType");
            var aceptEquals = maxValue.startsWith("=");
            maxValue = maxValue.replace("=", "");

            if (maxValue) {
                if (maxValue == "ZGetDate") {
                    maxValue = getLocaleShortDateString(new Date());
                }

                if (aceptEquals) {
                    $(this).rules("add", {
                        greaterEqualThan: maxValue,
                        messages: {
                            greaterEqualThan: "El valor debe ser menor o igual que: " + maxValue
                        }
                    });
                }
                else {
                    $(this).rules("add", {
                        greaterThan: maxValue,
                        messages: {
                            greaterThan: "El valor debe ser menor que: " + maxValue
                        }
                    });
                }

                if (dataType == "date") {
                    var maxDate;
                    if (aceptEquals)
                        maxDate = CreateDate(Number(maxValue.split("/")[0]), maxValue.split("/")[1] - 1, maxValue.split("/")[2]);
                    else
                        maxDate = CreateDate(Number(maxValue.split("/")[0]) - 1, maxValue.split("/")[1] - 1, maxValue.split("/")[2]);

                    $(this).datepicker("option", "maxDate", maxDate);
                }
            }
        }
    });
}

function CreateDate(day, month, year) {
    var d = new Date();

    d.setFullYear(year);
    d.setMonth(month);
    d.setDate(Number(day));

    return d;
}

function SetValidateForRequired() {
    $(".isRequired").each(function () {
        MakeRequired(this.id);
    });
}

function MakeRequired(idObj) {
    var obj = document.getElementById(idObj);

    if (obj) {
        var indexName = $(obj).attr("indexName");
        if (indexName) {
            $(obj).rules("add", {
                required: true,
                messages: {
                    required: jQuery.validator.format("El campo " + indexName + " es requerido.")
                }
            });
        }
        else {
            $(obj).rules("add", {
                required: true,
                messages: {
                    required: jQuery.validator.format("Por favor complete este campo.")
                }
            });
        }
    }
}

function MakeNonRequired(idObj) {
    //Para hacer no requerido se remueve la regla de requerido
    $("#" + idObj).rules("remove", "required");
}

function Enable(objID) {
    var obj = document.getElementById(objID);
    var controlType = obj.nodeName.toLowerCase();

    if (obj) {
        switch (controlType) {
            case "input":
                switch (obj.type.toLowerCase()) {
                    case "checkbox":
                        $(obj).removeAttr("disabled");
                        $(obj).removeClass("ReadOnly");
                        break;
                    default:
                        $(obj).removeAttr("readOnly");
                        $(obj).removeClass("ReadOnly");
                }
                break;
            case "textarea":
                $(obj).removeAttr("readOnly");
                $(obj).removeClass("ReadOnly");
                break;
            case "select":
                $(obj).removeAttr("disabled");
                break;
        }
    }
}

function Disable(objID) {
    var obj = document.getElementById(objID);
    var controlType = obj.nodeName.toLowerCase();

    if (obj) {
        switch (controlType) {
            case "input":
                switch (obj.type.toLowerCase()) {
                    case "checkbox":
                        $(obj).attr("disabled", "disabled");
                        $(obj).addClass("ReadOnly");
                        break;
                    default:
                        $(obj).attr("readOnly", "readOnly");
                        $(obj).addClass("ReadOnly");
                        $(obj).val('');
                }
                break;
            case "textarea":
                $(obj).attr("readOnly", "readOnly");
                $(obj).addClass("ReadOnly");
                $(obj).val('');
                break;
            case "select":
                $(obj).attr("disabled", "disabled");
                break;
        }
    }
}

function SetDefaultValues() {
    $(".haveDefaultValue").each(function () {
        var DefaultValue = $(this).attr("DefaultValue");
        if ($(this).val() == '')
            $(this).val(DefaultValue);
    });
}

function SetHierarchicalFunctionally() {
    $(".HierarchicalIndex").each(function () {
        $(this).change(function () {
            var childIndexId = $(this).attr("ChildIndexId");
            var parentIndexId = $(this).attr('id').split('_')[2];

            //Se obtienen los hijos
            var childIndexSplitted = childIndexId.split('|');

            if (childIndexSplitted.length > 1) {
                for (var i = 0; i < childIndexSplitted.length; i++) {
                    if (childIndexSplitted[i] && childIndexSplitted[i] != '') {
                        if (typeof $(this).val() == "string") {
                            GetHierarchyOptions(childIndexSplitted[i], parentIndexId, $(this).val());
                        }
                        else {
                            GetHierarchyOptions(childIndexSplitted[i], parentIndexId, $(this).val()[0]);
                        }
                    }
                }
            }
            else {
                if (typeof $(this).val() == "string") {
                    GetHierarchyOptions(childIndexSplitted[0], parentIndexId, $(this).val());
                }
                else {
                    GetHierarchyOptions(childIndexSplitted[0], parentIndexId, $(this).val()[0]);
                }
            }
        });
    });
}

function GetHierarchyOptions(IndexId, ParentIndexId, ParentValue, SenderID) {
    var userId;

    var IndexIdTag = $("#zamba_index_" + IndexId);
    if (IndexIdTag) {


        userId = GTUID();


        ScriptWebServices.IndexsService.set_defaultSucceededCallback(IndexsCallback);
        ScriptWebServices.IndexsService.set_defaultFailedCallback(IndexsOnError);

        if (SenderID) {
            ScriptWebServices.IndexsService.GetHierarchyOptionsWidthID(IndexId, ParentIndexId, ParentValue, userId, SenderID);
        }
        else {
            ScriptWebServices.IndexsService.GetHierarchyOptions(IndexId, ParentIndexId, ParentValue, userId);
        }
        return true;
    }
    return true;
}

//Agrega una condicion dinamica.
function InjectCondition(idSource, idTarget, action, rollback, value, comparator) {
    var comparation;
    var comparateValue;
    //Valor a comparar
    var comparateSource = "this.value";

    //Si el valor viene con <Attribute> usar el valor del atributo en el form.
    if (value.indexOf("<Attribute>") > -1) {
        var attrID = "zamba_index_" + value.replace("<Attribute>(", "").replace(")", "");
        comparateValue = "$('#" + attrID + "').val().toLowerCase()";
    }
    else
        comparateValue = "'" + value.toLowerCase() + "'";

    //Si el comparador es comun, contruye la comparacion normalmente, sino le agrega su funcion
    switch (comparator) {
        case "==": case "!=": case "<": case ">": case "<=": case ">=":
            comparation = comparateSource + comparator + comparateValue;
            break;
        case "starts":
            comparation = comparateSource + ".startsWith(" + comparateValue + ")";
            break;
        case "ends":
            comparation = comparateSource + ".endsWith(" + comparateValue + ")";
            break;
        case "in":
            comparation = comparateSource + ".contains(" + comparateValue + ")";
            break;
        case "into":
            comparation = comparateSource + ".into(" + comparateValue + ")";
            break;
        case "notInto":
            comparation = comparateSource + ".notInto(" + comparateValue + ")";
            break;
    }

    var objSource = document.getElementById(idSource);

    //Si el campo se encuentra
    if (objSource) {
        //Si el input se puede ingresar datos tecleando, agregar la condicion
        if (objSource.nodeName.toLowerCase() == "input" ||
            objSource.nodeName.toLowerCase() == "textarea") {

            $(objSource).keyup(function () {
                //Evalua la comparacion
                if (eval(comparation)) {
                    //Ejecuta la accion
                    eval(action + "('" + idTarget + "')");
                }
                else {
                    //Ejecuta el rollback
                    eval(rollback + "('" + idTarget + "')");
                }
            });
        }

        $(objSource).change(function () {
            if (eval(comparation)) {
                eval(action + "('" + idTarget + "')");
            }
            else {
                eval(rollback + "('" + idTarget + "')");
            }
        });

        $(window).load(function () {
            $("#" + idSource).change();
        });
    }
}

function SetListFilters() {
    //Obtiene todos los tags select
    $('select:not(:disabled)').each(function () {
        //Verifica que lo que encontro sea un índice
        if ($(this).attr('id').toLowerCase().indexOf('zamba_index_') > -1 && !$(this).hasClass("readonly") && $(this).css('display') != 'none') {
            //Agrega la lupa para filtrar y buscar valores
            AddFilter($(this).attr('id'), false);
        }
    });
}

function IndexsCallback(result) {
    var idReturned = result.toString().split('|')[0];
    var childIndexId;

    if (isNaN(idReturned))
        childIndexId = idReturned;
    else
        childIndexId = "zamba_index_" + result.toString().split('|')[0];

    var indexOptions = result.toString().split('|')[1];
    var childIndexSelect = document.getElementById(childIndexId);

    if (childIndexSelect && childIndexSelect.nodeName && childIndexSelect.nodeName.toLowerCase() != "input") {
        $('#' + childIndexId).html(indexOptions);
        $('#' + childIndexId).change();
    }
}

function IndexsOnError(result) {
    toastr.error("Error: " + result.get_message());
}

function ExecuteFormReadyActions() {

    var btnzamba_showOriginal = $("#zamba_showOriginal")
    if (btnzamba_showOriginal != null) {
        $("#zamba_showOriginal").click(function () {
            ShowLoadingAnimation();
        });
    };
    $("#zamba_save").click(function () {
        ShowLoadingAnimation();
    });

    //Obtiene los textbox de los formularios, y les agrega en el foco la seleccion del texto.
    //Con esto se deberia solucionar el problema del "bloqueo de los textbox".
    FixFocusError();

    $('#aspnetForm').validate({
        onsubmit: false
    });

    SetValidationsAction("zamba_save");

    ajustarselects();
    SetListFilters();

    $("input[id^=zamba_rule_]").each(function () {
        if ($(this).hasClass("DontSave")) {
            $(this).click(function () {
                ShowLoadingAnimation();
                $("#hdnRuleActionType").val("DontSave");
                document.ExecuteValidations = false;
            });
        }
        else {
            $(this).click(function () {
                ShowLoadingAnimation();
                $("#hdnRuleActionType").val("Save");
                document.ExecuteValidations = true;
            });
            SetValidationsAction(this.id);
        }
    });
    //Inicializar los combos dependientes de zvar
    $(".zvarDropDown").zvarDropDown();

    //Inicializar los tablas dependientes de zvar o ajax
    $(".zAjaxTable").zAjaxTable();

    //Inicializar los autocomplete
    $(".zAutocomplete").zAutocomplete();
}

document.ExecuteValidations = true;

//Calendars
function MakeDateIndexs(item) {
    if (!$(item).hasClass("ReadOnly") && !$(item).hasClass("hasDatepicker")) AddCalendar(item.id);
}

function zamba_save_onclick() {
    //$("#hdnRuleId").name = "zamba_save";
}

function zamba_cancel_onclick() {
    //$("#hdnRuleId").name = "zamba_save";
}

// Se encarga de corregir los problemas de foco al hacer postback que pasa en formularios.
// Los input de tipo text y los textarea deben implementar la clase readOnly y tener el
// atributo readonly="readonly" para que funcione. No se debe usar el disabled.
function FixFocusError() {
    //Obtiene los textbox de los formularios, y les agrega en el foco la seleccion del texto.
    //Con esto se deberia solucionar el problema del "bloqueo de los textbox".
    $("input[type=text]").focus(function () {
        if (!$(this).hasClass("hasDatepicker")) {
            $(this).select();
            if (typeof ($(this).caret) != "undefined") {
                //Esto es para intentar solucionar nuevamente el error de foco
                $(this).caret().start = 0;
                $(this).caret().end = 0;
            }
        }
    });

    //Obtiene los textbox de los formularios, y les agrega en el foco la seleccion del texto.
    //Con esto se deberia solucionar el problema del "bloqueo de los textbox".
    $("textarea").focus(function () {
        $(this).select();
        if (typeof ($(this).caret) != "undefined") {
            //Esto es para intentar solucionar nuevamente el error de foco
            $(this).caret().start = 0;
            $(this).caret().end = 0;
        }
    });
}

function SetIndexPnlVisibility(obj, divDoc, panel2Indices) {
    var divIndice = $("#DivIndices");
    var btnOcultar = $("#btnOcultar");
    var btnVisualizar = $("#btnVisualizar");
    var tableIndice = $("#ctl00_ContentPlaceHolder_ucTaskDetail_ctl00_completarindice_tblIndices").width();
    var a;
    var scrollDifference;
    if ($(obj).hasClass("Expand")) {
        $(panel2Indices).css("overflow", "auto");
        scrollDifference = $(divIndice)[0].scrollWidth - tableIndice;
        //Muestra el panel de índices
        $(divIndice).animate({ left: 0 }, "slow");

        //Calcula el espacio necesario para desplazar el documento
        a = $(divDoc).width() - $(divIndice).width();

        //Hace los desplazamientos con animacion       
        $("#separator").animate({ width: $(divIndice).width() }, "slow");
        $(divDoc).animate({ width: a + 16 + scrollDifference }, "slow");

        $(obj).addClass("Collapse");
        $(obj).removeClass("Expand");
    }
    else {
        //Oculta el panel de índices
        $(panel2Indices).css("overflow", "hidden");
        scrollDifference = $(divIndice)[0].scrollWidth - tableIndice;
        a = 0 - $(divIndice).width() + 16 + scrollDifference;

        //Hace la animacion
        $(divIndice).animate({ left: a }, "slow");

        //Acomoda el documento
        $("#separator").animate({ width: 16 }, "slow");
        a = $(divDoc).width() + $(divIndice).width() - 16 + scrollDifference;
        $(divDoc).animate({ width: a }, "slow");

        $(obj).addClass("Expand");
        $(obj).removeClass("Collapse");

    }
}

//Funcion que agrega un filtro al control
//indexid = id del tag que contiene el indice a aplicar el control
// codecolumn = true/false si se quiere visualizar o no la columna de codigo
function AddFilter(indexid, codecolumn) {

    var btnid = indexid.toString().split('_')[indexid.toString().split('_').length - 1];
    var $btnfilter = $('<img id="table_filter_' + btnid + '" onclick="CreateTable(this,' + codecolumn + ');" style="height: 16px; margin-left:10px;" alt="Filtrar" src="../../Content/Images/UI_blue/search.gif"/>');
    $("#" + indexid).after($btnfilter);
}
function CreateTable(obj, codecolumn) {
   // return;
    parent.ShowLoadingAnimation();

    var winHeight = $(window).height();

    //Creando el nombre del select
    var arrayObj = obj.id.toString().split('_');
    var cBox = "zamba_index_" + arrayObj[arrayObj.length - 1].trim();
    $('#dynamic_filter').data("indexid", cBox);

    //var table = LoadTable($('#dynamic_filter').data("indexid"), codecolumn);
    var table = LoadTable3(cBox, codecolumn);

    //Limpiando la tabla
    $('#dynamic_filter').html("");

    var initModal = '<div class="modal fade" id="openModalSearch" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">' +
    '<div class="modal-dialog">' +
    ' <div class="modal-content" id="openModalIFSearch">' +
    ' <div class="modal-header">' +
    '<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>' +
    '<h4 class="modal-title" id="modalFormTitleSearch"></h4>' +
    '</div>' +
    '<div class="modal-body" id="">' +
    '<div id="modalDivBodySearch">';
    var endModal = '</div> </div> </div> </div>';

    //Agregando los botones
    var html = initModal + table;

    html += '<center><input id="btnAceptDynamic" type="button" value="Aceptar" onclick="Accept()" />';
    html += '<input id="btnCancelDynamic" type="button" value="Cancelar" style=" margin-left:10px;" onclick="Cancel()" /></center>';
    html += endModal;
  
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

    //Calculando heights
    var popupHeight = selectHeight >= winHeight ? winHeight - 45 : selectHeight + 45;
    var tableHeight = selectHeight > popupHeight - 70 ? popupHeight - 70 : selectHeight;

    if (tableHeight <= 0) {
        tableHeight = 30;
    }
    selectHeight = selectHeight > 1400 && selectHeight / 10;

    if (popupHeight > $(window).width() - 600) {
        $("#dynamic_filter").css('height', selectHeight - 77 + 'px');
        $("#exampleContainer").css('height', selectHeight - 160 + 'px');
        popupHeight = tableHeight - 130;
    }
    else {
        popupHeight = tableHeight - 130;
    }

    //Abriendo el dialgo modal
    $("#example_wrapper").height(tableHeight);
    if (selectHeight > winHeight - 70) {
        tableHeight = winHeight - 100;

        $("#exampleContainer").height(tableHeight);
    }
    $("#dynamic_filter").css('height', '');
    $("#openModalSearch").modal();

    //$("#dynamic_filter").dialog({ autoOpen: false, modal: true, position: 'center', closeOnEscape: true, draggable: false, resizable: false, open: function (event, ui) { $(".ui-dialog-titlebar-close").hide(); }, height: popupHeight, width: 600, position: 'top' });
    //$("#dynamic_filter").dialog("open");

    setTimeout("parent.hideLoading();", 500);
}
function Cancel() {
    $("#openModalSearch").modal('hide');
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
        toastr.error("Por favor seleccione una fila");
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

var selectHeight;

function LoadTable3(cbox, codecolumn) {
    selectHeight = 0;
    var t = "";
    var select = $("#" + cbox + " option");
    t = '<div id="exampleContainer" style="overflow:auto;"><table cellpadding="0" cellspacing="0" border="0" class="display" id="example" style="height:60px;">';

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
        if (codecolumn) {
            t += "<td>";
        } else {
            selectHeight = selectHeight + 20;
            t += '<td style="display:none;">';
            t += select[i].value;
            t += "</td>";
            t += "<td>";
            t += select[i].text;
            t += "</td>";
            t += "</tr>";
        }
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

// 07/08/2012 Javier Se modifica funcion para que tambien acepte una url.
function getParameterByName(name, url) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results;

    if (url != undefined)
        results = regex.exec(url);
    else
        results = regex.exec(window.location.search);

    if (results == null)
        return "";
    else
        return decodeURIComponent(results[1].replace(/\+/g, " "));
}

//-----------------------------------------------------------------------------------------------------//
//Variables para abir documento
var urlToOpen = '';
var docNameToOpen = '';
var lsTabs = []; //Lista de tareas o documentos para mostrar

function AddDocTaskToOpenList(taskID, docID, docTypeId, asDoc, docName, url, userID) {

    try {
        if (!taskID || taskID == '')
            taskID = 0;
        else
            taskID = parseInt(taskID);

        if (!docID || docID == '')
            docID = 0;
        else
            docID = parseInt(docID);

        if (!userID || userID == '')
            userID = '';
        else
            userID = parseInt(userID);

    } catch (e) {
        toastr.error("Error al abrir el documento. Mensaje: " + e);
    }

    var newTab = {
        taskID: taskID,
        docID: docID,
        docTypeId: docTypeId,
        asDoc: asDoc,
        docName: docName,
        url: url,
        userID: userID
    };
    lsTabs.push(newTab);

}


function OpenPendingTabs(result) {
    if (result != undefined) {
        //No es la primera ejecucion
        GetIDSuccess(result);
        lsTabs.shift();
    }

    if (lsTabs.length == 0) { return false; } // No hay tareas para abrir

    var tasks = lsTabs;

    $('.modal-title').html('Abrir ' + ((tasks.length >= 2) ? (tasks.length) + ' tareas' : 'tarea'));
    //Div modal bootstrap
    $('#openTasks').on('show.bs.modal', function (e) {

        var task = '<strong>';
        for (var i = 0; i <= tasks.length - 1; i++)
            task += '<li>' + tasks[i].docName + '</li></br>';
        task += '</strong>';
        $('.tareasDiv').html(task);

        if (tasks.length >= 2) {
            $("#subTitleModal").html('Se cerraron inesperadamente las siguientes tareas:');
            $("#questionModal").html(" ¿Desea volver a abrirlas?");
        }
        else {
            $("#subTitleModal").html('Se cerró inesperadamente la siguiente tarea:');
            $("#questionModal").html(" ¿Desea volver a abrirla?");
        }

        //Ok boton modal bootstrap
        $(this).find('.btn-ok').click(function (e) {
            try {
                $('#openTasks').modal('hide');
                //Seteo los callbacks del servicio
                ScriptWebServices.TasksService.set_defaultSucceededCallback(OpenPendingTabs);
                ScriptWebServices.TasksService.set_defaultFailedCallback(GetIDError);
                $('#NoTaskDiv').css('display', 'none');

            } catch (e) { }

            var newTab = lsTabs[0];

            urlToOpen = newTab.url;
            ScriptWebServices.TasksService.GetTabID(newTab.taskID, newTab.docID, newTab.docTypeId, newTab.asDoc, newTab.userID);
        });
    });

    $('#openTasks').modal();
    return false;
}


function OpenDocTask2(taskID, docID, docTypeId, asDoc, docName, url, userID) {
    ShowLoadingAnimation();

    try {
        ScriptWebServices.TasksService.set_defaultSucceededCallback(GetIDSuccess);
        ScriptWebServices.TasksService.set_defaultFailedCallback(GetIDError);
    } catch (e) { }

    //Completo las variables para abrir doc
    urlToOpen = url;
    docNameToOpen = docName;

    try {
        if (!taskID || taskID == '')
            taskID = 0;
        else
            taskID = parseInt(taskID);

        if (!docID || docID == '')
            docID = 0;
        else
            docID = parseInt(docID);

        if (!userID || userID == '')
            userID = '';
        else
            userID = parseInt(userID);

    } catch (e) {
        toastr.error("Error al abrir el documento. Mensaje: " + e);
    }

    try {
        $('#NoTaskDiv').css('display', 'none');
        ScriptWebServices.TasksService.GetTabID(taskID, docID, docTypeId, asDoc, userID);
    } catch (e) { }

    return false;
}


//Nuevo metodo para apertura de tareas en pestña, en base al id devuelto del WS
function GetIDSuccess(result) {
    var tabID = result.split('|')[0];
    var tabName = result.split('|')[1];

    if (tabName == "" || tabID == "") {
        toastr.error('El documento es inexistente o no se tiene permiso para acceder al mismo');
        hideLoading();
    }
    else {
        var divTasks;
        var mainTabber;

        //Verifica si el codigo es ejecutado desde la web en general o desde dentro de una tarea
        if ($('#TasksDiv').length) {
            divTasks = $('#TasksDiv');
            mainTabber = $('#MainTabber');
        } else {
            divTasks = window.parent.$('#TasksDiv');
            mainTabber = window.parent.$('#MainTabber');
        }

        if (document.getElementById(tabID) == null) {
            var masterHeader;
            var barraCabecera;
            var mainmenu;

            if ($("#MasterHeader").length) {
                masterHeader = $("#MasterHeader").css("display") == "none" ? 0 : $("#MasterHeader").height();
                barraCabecera = $("#barra-Cabezera").css("display") == "none" ? 0 : $("#barra-Cabezera").innerHeight();
                mainmenu = $("#mainMenu").css("display") == "none" ? 0 : $("#mainMenu").innerHeight();
            } else {
                masterHeader = window.parent.$("#MasterHeader").css("display") == "none" ? 0 : window.parent.$("#MasterHeader").height();
                barraCabecera = window.parent.$("#barra-Cabezera").css("display") == "none" ? 0 : window.parent.$("#barra-Cabezera").innerHeight();
                mainmenu = window.parent.$("#mainMenu").css("display") == "none" ? 0 : window.parent.$("#mainMenu").innerHeight();
            }

            var divtag = '<div id="' + tabID + '" class="task-tab-div"><iframe id="IFTaskContent" class="IFTaskContent" frameborder="0" src="' + urlToOpen + '"/><div/>';
            $(divtag).appendTo(divTasks);
            divTasks.zTabs("add", tabID, tabName);

            parent.ShowLoadingAnimation();
        }
        else {
            hideLoading();
        }

        divTasks.zTabs('select', tabID);
        mainTabber.zTabs("select", 'tabtasks');
    }

    urlToOpen = '';
    docNameToOpen = '';
}

//Error en WS
function GetIDError(result) {
    toastr.error("Error al abrir el documento. Mensaje: " + result.get_message());
}

function getLocaleShortDateString(d) {
    var f = { "ar-SA": "dd/MM/yy", "bg-BG": "dd.M.yyyy", "ca-ES": "dd/MM/yyyy", "zh-TW": "yyyy/M/d", "cs-CZ": "d.M.yyyy", "da-DK": "dd-MM-yyyy", "de-DE": "dd.MM.yyyy", "el-GR": "d/M/yyyy", "en-US": "M/d/yyyy", "fi-FI": "d.M.yyyy", "fr-FR": "dd/MM/yyyy", "he-IL": "dd/MM/yyyy", "hu-HU": "yyyy. MM. dd.", "is-IS": "d.M.yyyy", "it-IT": "dd/MM/yyyy", "ja-JP": "yyyy/MM/dd", "ko-KR": "yyyy-MM-dd", "nl-NL": "d-M-yyyy", "nb-NO": "dd.MM.yyyy", "pl-PL": "yyyy-MM-dd", "pt-BR": "d/M/yyyy", "ro-RO": "dd.MM.yyyy", "ru-RU": "dd.MM.yyyy", "hr-HR": "d.M.yyyy", "sk-SK": "d. M. yyyy", "sq-AL": "yyyy-MM-dd", "sv-SE": "yyyy-MM-dd", "th-TH": "d/M/yyyy", "tr-TR": "dd.MM.yyyy", "ur-PK": "dd/MM/yyyy", "id-ID": "dd/MM/yyyy", "uk-UA": "dd.MM.yyyy", "be-BY": "dd.MM.yyyy", "sl-SI": "d.M.yyyy", "et-EE": "d.MM.yyyy", "lv-LV": "yyyy.MM.dd.", "lt-LT": "yyyy.MM.dd", "fa-IR": "MM/dd/yyyy", "vi-VN": "dd/MM/yyyy", "hy-AM": "dd.MM.yyyy", "az-Latn-AZ": "dd.MM.yyyy", "eu-ES": "yyyy/MM/dd", "mk-MK": "dd.MM.yyyy", "af-ZA": "yyyy/MM/dd", "ka-GE": "dd.MM.yyyy", "fo-FO": "dd-MM-yyyy", "hi-IN": "dd-MM-yyyy", "ms-MY": "dd/MM/yyyy", "kk-KZ": "dd.MM.yyyy", "ky-KG": "dd.MM.yy", "sw-KE": "M/d/yyyy", "uz-Latn-UZ": "dd/MM yyyy", "tt-RU": "dd.MM.yyyy", "pa-IN": "dd-MM-yy", "gu-IN": "dd-MM-yy", "ta-IN": "dd-MM-yyyy", "te-IN": "dd-MM-yy", "kn-IN": "dd-MM-yy", "mr-IN": "dd-MM-yyyy", "sa-IN": "dd-MM-yyyy", "mn-MN": "yy.MM.dd", "gl-ES": "dd/MM/yy", "kok-IN": "dd-MM-yyyy", "syr-SY": "dd/MM/yyyy", "dv-MV": "dd/MM/yy", "ar-IQ": "dd/MM/yyyy", "zh-CN": "yyyy/M/d", "de-CH": "dd.MM.yyyy", "en-GB": "dd/MM/yyyy", "es-MX": "dd/MM/yyyy", "fr-BE": "d/MM/yyyy", "it-CH": "dd.MM.yyyy", "nl-BE": "d/MM/yyyy", "nn-NO": "dd.MM.yyyy", "pt-PT": "dd-MM-yyyy", "sr-Latn-CS": "d.M.yyyy", "sv-FI": "d.M.yyyy", "az-Cyrl-AZ": "dd.MM.yyyy", "ms-BN": "dd/MM/yyyy", "uz-Cyrl-UZ": "dd.MM.yyyy", "ar-EG": "dd/MM/yyyy", "zh-HK": "d/M/yyyy", "de-AT": "dd.MM.yyyy", "en-AU": "d/MM/yyyy", "es-ES": "dd/MM/yyyy", "fr-CA": "yyyy-MM-dd", "sr-Cyrl-CS": "d.M.yyyy", "ar-LY": "dd/MM/yyyy", "zh-SG": "d/M/yyyy", "de-LU": "dd.MM.yyyy", "en-CA": "dd/MM/yyyy", "es-GT": "dd/MM/yyyy", "fr-CH": "dd.MM.yyyy", "ar-DZ": "dd-MM-yyyy", "zh-MO": "d/M/yyyy", "de-LI": "dd.MM.yyyy", "en-NZ": "d/MM/yyyy", "es-CR": "dd/MM/yyyy", "fr-LU": "dd/MM/yyyy", "ar-MA": "dd-MM-yyyy", "en-IE": "dd/MM/yyyy", "es-PA": "MM/dd/yyyy", "fr-MC": "dd/MM/yyyy", "ar-TN": "dd-MM-yyyy", "en-ZA": "yyyy/MM/dd", "es-DO": "dd/MM/yyyy", "ar-OM": "dd/MM/yyyy", "en-JM": "dd/MM/yyyy", "es-VE": "dd/MM/yyyy", "ar-YE": "dd/MM/yyyy", "en-029": "MM/dd/yyyy", "es-CO": "dd/MM/yyyy", "ar-SY": "dd/MM/yyyy", "en-BZ": "dd/MM/yyyy", "es-PE": "dd/MM/yyyy", "ar-JO": "dd/MM/yyyy", "en-TT": "dd/MM/yyyy", "es-AR": "dd/MM/yyyy", "ar-LB": "dd/MM/yyyy", "en-ZW": "M/d/yyyy", "es-EC": "dd/MM/yyyy", "ar-KW": "dd/MM/yyyy", "en-PH": "M/d/yyyy", "es-CL": "dd-MM-yyyy", "ar-AE": "dd/MM/yyyy", "es-UY": "dd/MM/yyyy", "ar-BH": "dd/MM/yyyy", "es-PY": "dd/MM/yyyy", "ar-QA": "dd/MM/yyyy", "es-BO": "dd/MM/yyyy", "es-SV": "dd/MM/yyyy", "es-HN": "dd/MM/yyyy", "es-NI": "dd/MM/yyyy", "es-PR": "dd/MM/yyyy", "am-ET": "d/M/yyyy", "tzm-Latn-DZ": "dd-MM-yyyy", "iu-Latn-CA": "d/MM/yyyy", "sma-NO": "dd.MM.yyyy", "mn-Mong-CN": "yyyy/M/d", "gd-GB": "dd/MM/yyyy", "en-MY": "d/M/yyyy", "prs-AF": "dd/MM/yy", "bn-BD": "dd-MM-yy", "wo-SN": "dd/MM/yyyy", "rw-RW": "M/d/yyyy", "qut-GT": "dd/MM/yyyy", "sah-RU": "MM.dd.yyyy", "gsw-FR": "dd/MM/yyyy", "co-FR": "dd/MM/yyyy", "oc-FR": "dd/MM/yyyy", "mi-NZ": "dd/MM/yyyy", "ga-IE": "dd/MM/yyyy", "se-SE": "yyyy-MM-dd", "br-FR": "dd/MM/yyyy", "smn-FI": "d.M.yyyy", "moh-CA": "M/d/yyyy", "arn-CL": "dd-MM-yyyy", "ii-CN": "yyyy/M/d", "dsb-DE": "d. M. yyyy", "ig-NG": "d/M/yyyy", "kl-GL": "dd-MM-yyyy", "lb-LU": "dd/MM/yyyy", "ba-RU": "dd.MM.yy", "nso-ZA": "yyyy/MM/dd", "quz-BO": "dd/MM/yyyy", "yo-NG": "d/M/yyyy", "ha-Latn-NG": "d/M/yyyy", "fil-PH": "M/d/yyyy", "ps-AF": "dd/MM/yy", "fy-NL": "d-M-yyyy", "ne-NP": "M/d/yyyy", "se-NO": "dd.MM.yyyy", "iu-Cans-CA": "d/M/yyyy", "sr-Latn-RS": "d.M.yyyy", "si-LK": "yyyy-MM-dd", "sr-Cyrl-RS": "d.M.yyyy", "lo-LA": "dd/MM/yyyy", "km-KH": "yyyy-MM-dd", "cy-GB": "dd/MM/yyyy", "bo-CN": "yyyy/M/d", "sms-FI": "d.M.yyyy", "as-IN": "dd-MM-yyyy", "ml-IN": "dd-MM-yy", "en-IN": "dd-MM-yyyy", "or-IN": "dd-MM-yy", "bn-IN": "dd-MM-yy", "tk-TM": "dd.MM.yy", "bs-Latn-BA": "d.M.yyyy", "mt-MT": "dd/MM/yyyy", "sr-Cyrl-ME": "d.M.yyyy", "se-FI": "d.M.yyyy", "zu-ZA": "yyyy/MM/dd", "xh-ZA": "yyyy/MM/dd", "tn-ZA": "yyyy/MM/dd", "hsb-DE": "d. M. yyyy", "bs-Cyrl-BA": "d.M.yyyy", "tg-Cyrl-TJ": "dd.MM.yy", "sr-Latn-BA": "d.M.yyyy", "smj-NO": "dd.MM.yyyy", "rm-CH": "dd/MM/yyyy", "smj-SE": "yyyy-MM-dd", "quz-EC": "dd/MM/yyyy", "quz-PE": "dd/MM/yyyy", "hr-BA": "d.M.yyyy.", "sr-Latn-ME": "d.M.yyyy", "sma-SE": "yyyy-MM-dd", "en-SG": "d/M/yyyy", "ug-CN": "yyyy-M-d", "sr-Cyrl-BA": "d.M.yyyy", "es-US": "M/d/yyyy" };

    var l = navigator.language ? navigator.language : navigator['userLanguage'], y = d.getFullYear(), m = d.getMonth() + 1, d = d.getDate();
    f = (l in f) ? f[l] : "dd/MM/yyyy";
    function z(s) { s = '' + s; return s.length > 1 ? s : '0' + s; }
    f = f.replace(/yyyy/, y); f = f.replace(/yy/, String(y).substr(2));
    f = f.replace(/MM/, z(m)); f = f.replace(/M/, m);
    f = f.replace(/dd/, z(d)); f = f.replace(/d/, d);
    return f;
}

function parseDate(input) {
    var parts = input.match(/(\d+)/g);
    if (parts == null)
        return null;

    var days = parts[0];
    var month = parts[1] - 1;
    var year = parts[2];

    // new Date(year, month [, date [, hours[, minutes[, seconds[, ms]]]]])
    var d = new Date(year, month, days);

    //Si los dias,meses y años de entrada coinciden con los de salida, entonces la fecha es valida
    if (d && d.getMonth() == month && d.getDate() == days && d.getFullYear() == year) {
        return d;
    }
    else
        return null;
}

function SetLoadingAnimationObserver() {
    if (document.firstLoad == true) {
        var allOfThem = document.getElementsByTagName('*');
        var i = 0;
        if (document.isLoading == true) {
            var stopLoadingAnimation = true;
            var currElement;
            while (allOfThem[i] != null && stopLoadingAnimation == false) {
                currElement = allOfThem[i];
                if (currElement.readyState != "interactive " && currElement.readyState != "complete") {
                    stopLoadingAnimation = false;
                }
                i++;
            }
            if (stopLoadingAnimation == true) {
                document.firstLoad = false;
                hideLoading();
            }
            else {
                setTimeout("SetLoadingAnimationObserver();", 1000);
            }
        }
        else {
            document.firstLoad = false;
            hideLoading();
        }
    }
}

var ZDoc = document;
function StartObjectLoadingObserverById(objEjementId) {
    var element = ZDoc.getElementById(objEjementId);
    StartObjectLoadingObserver(element);
}
function StartObjectLoadingObserver(objEjement) {
    if (objEjement == undefined || objEjement.readyState == undefined)
        AddReadyState(objEjement);
    else
        return;

    if (!ZDoc.arrLoadingElements) {
        ZDoc.arrLoadingElements = new Array();
    }
    ZDoc.arrLoadingElements.push(objEjement);

    if (!ZDoc.pendingObjectsObserverStarted) {
        StartPendingObjectsObserver();
    }
}
function AddReadyState(obj) {

    if (obj != null && obj != undefined) {

    obj.readyState = "loading";
    $(obj).load(function () {
        this.readyState = "complete";
    });
}
}
function StartPendingObjectsObserver() {
    ZDoc.pendingObjectsObserverStarted = true;
    var pendingObjects = ZDoc.arrLoadingElements;

    if (pendingObjects) {
        var iterator = pendingObjects.length - 1;
        var itemDeleted;

        while (iterator > -1) {
            itemDeleted = false;

            if (!pendingObjects[iterator]) {
                document.arrLoadingElements.splice(iterator, 1);
            }
            else {
                if ((pendingObjects[iterator].readyState != "interactive " && pendingObjects[iterator].readyState != "complete") && pendingObjects[iterator].id != 'ctl00_WFExecForEntryRulesFrame') {
                    ShowLoadingAnimation();
                    setTimeout("StartPendingObjectsObserver()", 1000);
                    return;
                }
                else {
                    document.arrLoadingElements.splice(iterator, 1);
                    itemDeleted = true;
                }
            }

            iterator--;
        }

        if (document.arrLoadingElements.length == 0) {
            ZDoc.pendingObjectsObserverStarted = false;
            hideLoading();
        }
    }
}

function ShowDeleteConfirmation() {
    ShowLoadingAnimation();
    getBtnDelete().hide("2000");
    $("#divDeleteConfirmation").show("3000");
    hideLoading();
}

function HideDeleteConfirmation() {
    ShowLoadingAnimation();
    $("#divDeleteConfirmation").hide("2000");
    getBtnDelete().show("3000");
    hideLoading();
}

function sortTable(objTable, colId, ascending) {
    var tbl = objTable.tBodies[0];
    if (tbl == undefined) return;

    var store = [];
    for (var i = 1, len = tbl.rows.length; i < len; i++) {
        var row = tbl.rows[i];
        var sortnr = (row.cells[0].textContent || row.cells[colId].innerText);
        if (sortnr != "" && sortnr != null)
            sortnr = parseDate(sortnr);
        else {
            if (ascending) {
                sortnr = new Date(8640000000000000);
            }
            else {
                sortnr = new Date(-8640000000000000);
            }
        }
        if (!isNaN(sortnr)) store.push([sortnr, row]);
    }
    if (ascending)
        store.sort(function (x, y) {
            return x[0] - y[0];
        });
    else
        store.sort(function (x, y) {
            return y[0] - x[0];
        });

    for (var i = 0, len = store.length; i < len; i++) {
        tbl.appendChild(store[i][1]);
    }
    store = null;
}

//---------------------------------------------------------------------------
//
//  Bloque utilizado para agregar funcionalidad a javascript y jQuery de forma nativa
//  Nota: Evaluar si conviene mover estas funcionalidades "nativas" a un js nuevo.
//
//-----------------------------------------------------------------------------------------------------
(function () {
    jQuery.fn.hasClass = function (a) {
        var Aa = /[\n\t]/g, ca = /\s+/, Za = /\r/g, $a = /href|src|style/;
        a = " " + a + " ";
        for (var b = 0, d = this.length; b < d; b++)
            if ((" " + this[b].className + " ").toLowerCase().replace(Aa, " ").indexOf(a.toLowerCase()) > -1)
                return true;
        return false
    }

    if (typeof String.prototype.startsWith != 'function') {
        String.prototype.startsWith = function (str) {
            return this.slice(0, str.length) == str;
        };
    }

    if (typeof String.prototype.endsWith != 'function') {
        String.prototype.endsWith = function (str) {
            return this.slice(-str.length) == str;
        };
    }

    if (typeof String.prototype.contains != 'function') {
        String.prototype.contains = function (str) {
            return this.indexOf(str) > -1;
        };
    }

    if (typeof String.prototype.into != 'function') {
        String.prototype.into = function (str) {
            var returnValue = false;
            var splitedValues = str.split('|');

            if (splitedValues.length > 0) {
                var max = splitedValues.length;
                for (var i = 0; i < max; i++) {
                    if (this == splitedValues[i])
                        return true;
                }
            }

            return returnValue;
        };
    }

    if (typeof String.prototype.notInto != 'function') {
        String.prototype.notInto = function (str) {
            var returnValue = true;
            var splitedValues = str.split('|');

            if (splitedValues.length > 0) {
                var max = splitedValues.length;
                for (var i = 0; i < max; i++) {
                    if (this == splitedValues[i])
                        return false;
                }
            }

            return returnValue;
        };

        if (typeof String.prototype.textWidth != 'function') {
            String.prototype.textWidth = function (font) {
                var f = font || '12px arial',
              o = $('<div>' + this + '</div>')
                    .css({ 'position': 'absolute', 'float': 'left', 'white-space': 'nowrap', 'visibility': 'hidden', 'font': f })
                    .appendTo($('body')),
              w = o.width();

                o.remove();

                return w;
            };
        }
    }
})();


function SetEditableColumns() {
    $(".editable").each(function () {
        var cols = $(this).attr("editablecolumns");

        /* Create input box and wrap in td and append to tr */
        $("<td>").append($("<input>", {
            type: "text",
            val: $("<td>").val(),
            name: "title",
            "class": "text",
            "css": {
                "width": "50px"
            }
        }));


    });
}



//Funciones para Formularios con Grillas, para Ejecutar Reglas desde cada Row de la grilla o para obtener los valores de todos los rows de la grilla y ejecutar una regla
function SetRuleIdAndZvar(sender, RuleId, ZVars) {

    $("#hdnRuleId").name = RuleId;
    $("#hdnRuleId").value = ZVars;

}


var ZVars;
var currentTag;

function SetRuleIdAndGrid(sender, RuleId, currentzvars, targetTableId) {
    count = 0;
    ZVars = currentzvars;

    var tbody;

    if (targetTableId == null || targetTableId == '')
        tbody = $(sender).closest('table').children('tbody')[0];
    else
        tbody = $('#' + targetTableId).children('tbody')[0];

    var rows = $('tr', tbody).each(function (e) {
        porcadatr(this, ZVars);
    });

    zvargridresult = zvargridresult + ')';

    // a futuro se deberia consumir como un servicio rest con json para la ejecucion en background de la regla y poder pasarle la coleccion como objeto.
    $("#hdnRuleId").name = RuleId;
    $("#hdnRuleId").value = ZVars.split("[")[0].replace('zvar(', '') + "=" + zvargridresult;

}

var count = 0;
var zvargridresult = 'totable(';

function porcadatr(sender, Zvars) {
    currentTag = sender;
    if (count > 0) {
        var zvarslist = Zvars.split(']').join('').split(')').join('').split("[");
        zvarslist.splice(0, 1);
        $(zvarslist).each(function () {

            var varvalue;

            if ($(currentTag).find('td:eq(' + this + ')').children('input').val() != null) {
                varvalue = $(currentTag).find('td:eq(' + this + ')').children('input').val();
            }
            else {
                varvalue = $(currentTag).find('td:eq(' + this + ')').text();
            }

            zvargridresult = zvargridresult + varvalue + "|";

        });
    }
    count++;
    zvargridresult = zvargridresult + ";";
}


function SetRuleIdAndGridWithNonCeros(sender, RuleId, currentzvars, targetTableId) {
    count = 0;
    ZVars = currentzvars;

    var tbody;

    if (targetTableId == null || targetTableId == '')
        tbody = $(sender).closest('table').children('tbody')[0];
    else
        tbody = $('#' + targetTableId).children('tbody')[0];

    var rows = $('tr', tbody).each(function (e) {
        porcadatrWithNonCeros(this, ZVars);
    });

    zvargridresult = zvargridresult + ')';

    // a futuro se deberia consumir como un servicio rest con json para la ejecucion en background de la regla y poder pasarle la coleccion como objeto.
    $("#hdnRuleId").name = RuleId;
    $("#hdnRuleId").value = ZVars.split("[")[0].replace('zvar(', '') + "=" + zvargridresult;

}


function porcadatrWithNonCeros(sender, Zvars) {
    currentTag = sender;
    if (count > 0) {

        var zvargridresulttemp = '';

        var zvarslist = Zvars.split(']').join('').split(')').join('').split("[");
        zvarslist.splice(0, 1);
        $(zvarslist).each(function () {

            var varvalue;

            if ($(currentTag).find('td:eq(' + this + ')').children('input').val() != null) {
                varvalue = $(currentTag).find('td:eq(' + this + ')').children('input').val();
            }
            else {
                varvalue = $(currentTag).find('td:eq(' + this + ')').text();
            }

            if (this == 5 && varvalue == '0') {
                zvargridresulttemp = '';
            }
            else {
                zvargridresulttemp = zvargridresulttemp + varvalue + "|";
            }

        });

        zvargridresult = zvargridresult + zvargridresulttemp;
    }
    count++;
    if (zvargridresulttemp != '') zvargridresult = zvargridresult + ";";
}

function SetRuleId(sender) {
    $("#hdnRuleId").name = sender.id;
    frmMain.submit();
}


function SetAsocId(sender) {
    $("#hdnAsocId").name = sender.id;
    frmMain.submit();
}


//Inicia una nueva ejecucion de reglas
function SetNewGeneralExecution() {
    //ShowGeneralRuleContainer();
    //ResizeGeneralRuleIframe();

    var WFIframe = $("#ctl00_WFExecForEntryRulesFrame");
    var CurrTask = WFIframe.contents().find("#hdnCurrTaskID").val();
    WFIframe[0].contentWindow.location = "../WF/WFExecutionForEntryRules.aspx";

    if (CurrTask != "" && CurrTask == -1 && WFIframe != null) {
        if (WFIframe[0] != null) {
            WFIframe[0].contentWindow.location = "about:blank";
            WFIframe[0].contentWindow.location = "../WF/WFExecutionForEntryRules.aspx";
            //StartObjectLoadingObserver()
            //  ShowIFrameModal(WFIframe[0]);
            //   ShowIFrameModal(WFIframe[0]," ",600,400);
            // ShowIFrameModal(" ", WFIframe[0].contentWindow.location, 600, 400);
        }
        else {
            if (WFIframe.context != null) {
                WFIframe.context.location = "about:blank";
                WFIframe.context.location = "../WF/WFExecutionForEntryRules.aspx";
            }
        }
    }
}

function ShowGeneralRuleContainer() {
    //Se muestra el contenedor de reglas
    var rulesContent = $("#EntryRulesContent");
    rulesContent.css("width", "600");
    rulesContent.css("height", "50%");
    rulesContent.css("top", "-1000");
    rulesContent.css("left", (window.screen.width / 2) - (rulesContent.width() / 2));
    rulesContent.show();
}

function ResizeGeneralRuleIframe() {
    var WFIframe = $("#ctl00_WFExecForEntryRulesFrame");
    WFIframe.css("width", $("#EntryRulesContent").width());
    WFIframe.css("height", $("#EntryRulesContent").height());
}

//Llamada asincrona al metodo para setear una nueva ejecucion de reglas
function RuleButtonClick(ruleId) {
    ShowLoadingAnimation();
    $.ajax({
        type: "POST",
        url: document.config.urlBase + "/Services/TasksService.asmx/SetNewRuleExecution",
        data: "{ ruleId: " + ruleId + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            SetNewGeneralExecution();
            hideLoading();
        },
        error: function (request, status, err) {
            toastr.error("Error en la ejecucion de reglas \r" + request.responseText);
            hideLoading();
        }
    });
}

function GetPipedSeparateValues(idsArray) {
    var values = '';
    for (var i = 0; i < idsArray.length; i++) {
        if (idsArray[i] != null && idsArray[i] != '') {
            if (i > 0)
                values += '|';
            values += $('#' + idsArray[i]).val();
        }
    }

    return values;
}


function clearindexcache(indexid) {
    $.ajax({
        type: 'POST',
        url: '../../Services/IndexsService.asmx/ClearIndexCache',
        data: "{'IndexId':" + indexid + "}",
        contentType: 'application/json; utf-8',
        dataType: 'json',
        success: function (data) {
            if (data.d != null) {
                toastr.error(data.d);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }

    });


}

function keepSessionAlive() {
    $.ajax({
        type: "POST",
        url: document.config.urlBase + "/Services/TasksService.asmx/KeepSessionAlive",
        data: '',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (document.config.showSessionRefreshLog) {
                var now = new Date();
                $("#refresh").html("<span>" + now + "</span>");
            }
        },
        error: function (a, b) {
            if (document.config.showSessionRefreshLog) {
                $("#refresh").html("<span>Error" + b + "</span>");
            }
        },
        dataType: "json"
    });

    setTimeout("keepSessionAlive()", document.config.sessionTimeOut * 60000 - 30000); //sessionTimeOut is minutes
}

function getAvailableWidth() {
    var screenWidth = $(window).width();

    if (screenWidth == 0)
        screenWidth = $(parent.$("#mainMenu")).width();

    return screenWidth;
}

function addEvent(id, eventName, handler) {
    var elementId = getSelectorId(id);

    $(elementId).on(eventName, handler);
}

function getSelectorId(id) {
    var selectorId;

    if (id.indexOf('#') > -1)
        selectorId = id;
    else
        selectorId = '#' + id;

    return selectorId;
}

//Scripts de la MasterPage

var UseTaskTab = false;

function ResizeCurrentTab(height, url) {
    if (height > 0) {
        if (url.toLowerCase().indexOf("taskviewer") != -1 || url.toLowerCase().indexOf("docviewer") != -1) {
            if ($('#TasksDivUL').find(".ui-tabs-selected").find("a")[0] != undefined) {
                idTabSelected = $('#TasksDivUL').find(".ui-tabs-selected").find("a")[0].href;
                $(idTabSelected).height(getHeightScreen() - getTBH());
            }
        }
    }
}

function GetTaskAvailableHeight() {
    return getHeightScreen() - getTBH();
}

function ExtractIdFromUrl(url, typeId) {
    var start;
    var end;
    if (typeId == "doc") {
        start = url.indexOf('docid', 0) + 6;
        end = url.indexOf('&', start);
    }
    else {
        start = url.indexOf('taskid', 0) + 7;
        end = url.indexOf('&', start);
    }
    return url.substr(start, end - start);
}

function loadCliksInRows() {
    if ($('.AltRowStyle').find('a') != null) {
        $('.AltRowStyle').click(function () {
            if ($(this).find('a')[0])
                window.open($(this).find('a')[0], '_blank', '', false);
            return false;
        });
        $('.AltRowStyle').find('a').click(function () {
            window.open($(this).attr('href'), '_blank', '', false);
            return false;
        });
    }
    if ($('.RowStyle').find('a') != null) {
        $('.RowStyle').click(function () {
            if ($(this).find('a')[0])
                window.open($(this).find('a')[0], '_blank', '', false);
            return false;
        });
        $('.RowStyle').find('a').click(function () {
            window.open($(this).attr('href'), '_blank', '', false); return false;
        });
    }

    if ($('.AltRowStyleTasks').find('a') != null) {
        $('.AltRowStyleTasks').click(function () {
            if ($(this).find('a')[0]) {
                var name = $(this).find(">td")[1].innerText;
                SwitchDocTaskForResults($(this).find('a')[0], true, name);

                return false;
            }
        });
        $('.AltRowStyleTasks').find('a').click(function () {
            var name = $(this).parent().parent().find("td")[1].innerText;
            SwitchDocTaskForResults($(this), true, name);

            return false;
        });
    }
    if ($('.RowStyleTasks').find('a') != null) {
        $('.RowStyleTasks').click(function () {
            if ($(this).find('a')[0]) {
                var name = $(this).find(">td")[1].innerText;
                SwitchDocTaskForResults($(this).find('a')[0], true, name);

                return false;
            }
        });
        $('.RowStyleTasks').find('a').click(function () {
            var name = $(this).parent().parent().find("td")[1].innerText;
            SwitchDocTaskForResults($(this), true, name);

            return false;
        });
    }
    if ($('.FormRowStyle').find('a') != null) {
        $('.FormRowStyle').click(function () {
            if ($(this).find('a')[0]) {
                var name = $(this).find(">td")[1].innerText;
                SwitchDocTaskForResults($(this).find('a')[0], true, name);
                return false;
            }
        });
        $('.FormRowStyle').find('a').click(function () {
            var name = $(this).parent().parent().find("td")[1].innerText;
            SwitchDocTaskForResults($(this), true, name);
            return false;
        });
    }
    if ($('.RowStyleResults').find('a') != null) {
        $('.RowStyleResults').click(function () {
            if ($(this).find('a')[0]) {
                var name = $(this).find(">td")[1].innerText;
                SwitchDocTaskForResults($(this).find('a')[0], true, name);

                return false;
            }
        });
        $('.RowStyleResults').find('a').click(function () {
            var name = $(this).parent().parent().find("td")[1].innerText;
            SwitchDocTaskForResults($(this), true, name);
            return false;
        });
    }
    if ($('.AltRowStyleResults').find('a') != null) {
        $('.AltRowStyleResults').click(function () {
            if ($(this).find('a')[0]) {
                var name = $(this).find(">td")[1].innerText;
                SwitchDocTaskForResults($(this).find('a')[0], true, name);
                return false;
            }
        });
        $('.AltRowStyleResults').find('a').click(function () {
            var name = $(this).parent().parent().find("td")[1].innerText;
            SwitchDocTaskForResults($(this), true, name);
            return false;
        });
    }
}


function SwitchDocTaskForResults(anchor, asTask, name) {
    var url = anchor.href;
    if (url === undefined) {
        url = anchor[0].href;
    }


    if (url == null || url == "") {
        toastr.error("Error al abrir una tarea, ID no especificado");
        return;
    }

    url = url.toLowerCase();

    //Se obtiene de la url el docID
    var id = getParameterByName("docid", url);

    var userID;
    userID = GTUID();


    if (id != "") {
        var docTypeId = getParameterByName("doctypeid", url);

        if (docTypeId == "")
            docTypeId = getParameterByName("doctype", url);

        //Si lo tiene lo envia para abrirlo
        OpenDocTask2(0, id, docTypeId, !asTask, name, url, userID);

    }
    else {
        //Sino busca task id
        id = getParameterByName("taskid", url);

        if (id != "") {
            OpenDocTask2(id, 0, -1, false, name, url, userID);

        }
        else {
            toastr.error("Error al abrir una tarea, ID no especificado");
        }
    }
}

function OpenWindow(url) {
    window.open(url);

    var tabcount = $('#TasksDivUL').find("li").length;
    if (tabcount > 0)
        RefreshCurrentTab();
}
function MakeSearch() {
    parent.ViewResults();
}

function OpenDocTaskfronMaster2(n, id, docTypeId, asTask, name, url, userID) {
    OpenDocTask2(n, id, docTypeId, asTask, name, url, userID);
}

function ViewResults() {
    $('#MainTabber').zTabs("select", '#tabresults');
}

function ReloadFrame() {
    var iframe = document.frames ? document.frames["ctl00_ContentPlaceHolder1_formBrowser"] : $("#ctl00_ContentPlaceHolder1_formBrowser");
    var ifWin = iframe.contentWindow || iframe;
    var url = ifWin.location;
    ifWin.location.reload();
    ifWin.location = url;

}

var unloadCount = 1;
var showedDialog = false;
var unloadFromInsert = false;

function RefreshGrid(tabName) {
    var WFIframe;
    switch (tabName) {
        case "tareas":
            WFIframe = $("#ctl00_ContentPlaceHolder_wfiframe");
            break;
    }
}

//Refresca el tab actual
function RefreshCurrentTab() {
    ShowLoadingAnimation();
    //obtengo el objeto que contiene el iframe
    var id = $('#TasksDiv').zTabs("activeAnchor").attr("href");
    id = id.substr(id.indexOf("#"));
    //obtengo el Iframe
    id = $(id).find(":first-child")[0];
    var Iframe = $(id);
    if (Iframe != null && Iframe[0] != null) {
        Iframe[0].contentWindow.location.href = Iframe[0].contentWindow.location.href;
    }
    else {
        if (Iframe.context != null) {
            Iframe[0].contentWindow.location.href = Iframe[0].contentWindow.location.href;
        }
    }
}

function RefreshTab(tabid) {
    var id = $(tabid).find(":first-child")[0];
    var Iframe = $(id);
    if (Iframe != null && Iframe[0] != null) {
        Iframe[0].contentWindow.location.href = Iframe[0].contentWindow.location.href;
        //Iframe[0].contentWindow.location.reload(true);
    }
}

function RefreshTask(taskid, docid) {
    var doc = document;

    ShowLoadingAnimation();

    if (doc.getElementById("T" + taskid)) {
        RefreshTab("#T" + taskid);
    }
    else {
        if (doc.getElementById("D" + taskid)) {
            RefreshTab("#D" + taskid);
        }
        else {
            if (doc.getElementById("T" + docid)) {
                RefreshTab("#T" + docid);
            }
            else {
                if (doc.getElementById("D" + docid)) {
                    RefreshTab("#D" + docid);
                }
            }
        }
    }
}

function CloseTask(taskid, isCloseFromButton) {
    var id;
    if (isCloseFromButton) {
        id = $('#TasksDiv').zTabs("activeAnchor").attr("href");
        id = id.substr(id.indexOf("#"));
    }
    else
        id = '#T' + taskid;

    $('#TasksDiv').zTabs("remove", id);


    if ($('#TasksDiv').zTabs("length") == 0) {
        $('#MainTabber').zTabs("select", '#tabtasklist');
        $('#NoTaskDiv').css('display', 'block');

        document.getElementById('ctl00_ContentPlaceHolder_Arbol_RefreshClick').click();
        hideLoading();

    }
    //__doPostBack('ctl00$ContentPlaceHolder$Arbol$BtnRefresh','')
}
function CheckAnyTaskOpen() {
    return ($('#TasksDiv').tabs("length") == 0);
}
function InsertOpen() {
    var destinationURL = "/Views/Insert/Insert.aspx";
    var newwindow = window.open(destinationURL, this.target, 'width=630,height=550,left=' + (screen.width - 600) / 2 + ',top=' + (screen.height - 580) / 2 + ',directories=no,status=no,menubar=no,toolbar=no,location=no,resizable=yes');
}
function InsertForm(formid, modal) {
    if (!modal) {
        location.href = '/Views/WF/DocInsert.aspx?formid=' + formid;
    }
    else {
        ShowIFrameModal("Insertar documentos", "../WF/DocInsertModal.aspx?formid=" + formid + "&modal=true", 560, 580);
        StartObjectLoadingObserverById("IFDialogContent");
    }
}
function AsociateForm(formid, docid, doctypeid, taskid, continueWithCurrentTasks, dontOpenTaskAfterInsert, fillCommonAttributes, haveSpecificAtt) {
    var url = "../WF/DocInsertModal.aspx?formid=" + formid + "&DocID=" + docid + "&DocTypeID=" + doctypeid + "&CallTaskID=" +
         taskid + "&ContinueWithCurrentTasks=" + continueWithCurrentTasks + "&DontOpenTaskAfterInsert=" + dontOpenTaskAfterInsert + "&FillCommonAttributes=" +
         fillCommonAttributes + "&haveSpecificAtt=" + haveSpecificAtt;

    ShowIFrameModal("Insertar documentos", url, 800, 600);//550, 950
    StartObjectLoadingObserverById("IFDialogContent");
}

function ShowInsertAsociated(url) {
    //BlockWebPageInteraction();
    ShowIFrameModal("Insertar documentos", url, 800, 600);
    //StartObjectLoadingObserverById("IFDialogContent");
}

function CloseInsert() {
    
    if ($("#openModalIF").length == 0) {
        parent.UnblockWebPageInteraction();
        parent.$("#openModalIF").modal("hide");
    }
    else {
    UnblockWebPageInteraction();
    $("#openModalIF").modal("hide");
}
}

$("#openModalIF").on('hide.bs.modal', function (e) {
    $("#modalIframe").attr("src", "");
})

function FixAndPosition(objDlg) {
    objDlg.css("top", "100px");
    objDlg.css("position", "absolute");

    var zIndexMax = getMaxZ();
    $(objDlg).css({ "z-index": Math.round(zIndexMax) });
}
function OpenHome() {
    window.open($("#ctl00_hdnLink").val(), $("#ctl00_hdnTarget").val(), "", false);
}

function ShowDivModal(title, div, width, height) {
    if (div.width() != undefined)
        width = div.width();
    if (div.height() != undefined)
        height = div.height() + 100;

    if ($("#modalIframe").length >= 1) {
        if (title == "") {
            $("#openModalIF >div .modal-header").css("padding", 0);
            $("#openModalIF >div .modal-header >button").css("margin-right", 10);
        }
        $("#modalIframe").attr("src", "about:blank");
        $("#modalIframe").css("display", "none");
        $("#modalFormTitle").html(title);
        $("#modalDivBody").html(div.html());
        $("#openModalIFContent").css("width", width);
        $("#openModalIFContent").css("height", height);
        $("#modalDivBody").css("display", "block");
        $("#openModalIF").modal();
    }
    else {
        if (title == "") {
            $("#openModalIF >div .modal-header", window.parent.document).css("padding", 0);
            $("#openModalIF >div .modal-header >button", window.parent.document).css("margin-right", 10);
        }
        var $modalIframe = $("#modalIframe", window.parent.document);
        if ($modalIframe.length == 0)
            $modalIframe = $("#modalFormHome", window.parent.document).children("iframe");

        $modalIframe.attr("src", "about:blank");
        $modalIframe.css("display", "none");
        $("#modalFormTitle", window.parent.document).html(title);
        $("#modalDivBody", window.parent.document).html(div.html());
        $("#openModalIFContent", window.parent.document).css("width", width);
        $("#openModalIFContent", window.parent.document).css("height", height);
        $("#modalDivBody", window.parent.document).css("display", "block");
        if (typeof $().modal != 'function')
            reloadBootstrap();

        $("#openModalIF", window.parent.document).modal();
    }
}

function ShowIFrameModal(title, src, width, height) {

    if (title == '') {
        $("#modalFormTitle").css("height", (height - 200));
    }

    if ($("#modalIframe").length >= 1) {
        $("#modalIframe").attr("src", "about:blank");
        $("#modalDivBody").html('');
        $("#modalDivBody").css("display", "none");
        $("#modalIframe").attr("src", src);
        $("#modalFormTitle").html(title);
        $("#openModalIFContent").css("width", width);
        $("#openModalIFContent").css("height", height);
        $("#modalIframe").css("width", width - 50);
        $("#modalIframe").css("height", height - 100);

        $("#modalIframe").css("display", "block");


        if (typeof $().modal != 'function')
            reloadBootstrap();

        setTimeout(function () {
            $('#openModalIF').modal();
        }, 1000);
    }
        //Esta dentro de un IFrame
    else {
        var $modalIframe = $("#modalIframe", window.parent.document);
        if ($("#modalIframe", window.parent.document).attr("src") == undefined)
            $modalIframe = $("#modalFormHome", window.parent.document).children("iframe");

        $modalIframe.attr("src", "about:blank");
        $("#modalDivBody", window.parent.document).html('');
        $("#modalDivBody", window.parent.document).css("display", "none");
        $modalIframe.attr("src", src);

        $("#modalFormTitle", window.parent.document).html(title);
        $("#openModalIFContent", window.parent.document).css("width", width);
        $("#openModalIFContent", window.parent.document).css("height", height);
        $modalIframe.css("width", width - 50);
        $modalIframe.css("height", height - 100);
        $modalIframe.css("display", "block");
        if (typeof $().modal != 'function')
            reloadBootstrap();
        setTimeout(function () {
            $("#openModalIF", window.parent.document).modal();
        }, 1000);
    }
}


function ShowIFrameHome(src, height) {
    var $toAppend = $("#tabhome").lenght >= 1 ? $("#tabhome") : $("#tabhome", window.parent.document);

    // Creo el boton para expandir/contraer botones home
    var homeButtonsDiv = document.createElement('div');
    var SpanExpandirContraer = document.createElement('span');
    $(homeButtonsDiv).addClass("toogleIframe");
    $(SpanExpandirContraer).addClass("glyphicon glyphicon-menu-down").appendTo($(homeButtonsDiv));
    $(SpanExpandirContraer).text(" Expandir").appendTo($(homeButtonsDiv));
    // Guardo div con los botones
    var buttonsDiv = $("#ctl00_ContentPlaceHolder_pnlHomeButtons", parent.document);

    $(homeButtonsDiv).click(function () {
        buttonsDiv.slideToggle();
        $(SpanExpandirContraer).text($(SpanExpandirContraer).text() == " Ocultar" ? " Expandir" : " Ocultar");
        $(SpanExpandirContraer).attr("class", $(SpanExpandirContraer).attr("class") == "glyphicon glyphicon-menu-down" ? "glyphicon glyphicon-menu-up" : "glyphicon glyphicon-menu-down");
    });

    // Agrego el boton antes del div con los botones.
    $(homeButtonsDiv).insertBefore(buttonsDiv);
    // Oculto los botones.
    buttonsDiv.slideToggle();

    // Se crea Iframe con la grilla.
    var iframe = document.createElement('iframe');
    iframe.src = src;
    iframe.width = "100%";
    iframe.height = (height - 100);
    iframe.id = "iframeHome";

    //Boton para expandir/contraer grilla
    var d = document.createElement('div');
    var s = document.createElement('span');
    $(s).addClass("glyphicon glyphicon-menu-up").appendTo($(d));
    var sn = document.createElement('span');
    $(sn).text(" Ocultar").appendTo($(d));
    var close = document.createElement('span');
    $(close).attr("class", "glyphicon glyphicon-remove");
    $(close).css("float", "right").appendTo($(d));

    $(d).addClass("toogleIframe").appendTo($toAppend)
        .click(function () {
            $(iframe).slideToggle();
            $(sn).text($(sn).text() == " Ocultar" ? " Expandir" : " Ocultar");
            $(s).attr("class", $(s).attr("class") == "glyphicon glyphicon-menu-down" ? "glyphicon glyphicon-menu-up" : "glyphicon glyphicon-menu-down");
        });

    $toAppend.append(iframe);

    $(close).click(function () {
        $(iframe).remove();
        $(d).remove();

        if ($(SpanExpandirContraer).attr("class") == "glyphicon glyphicon-menu-down") {
            buttonsDiv.slideToggle();
        }
        $(homeButtonsDiv).remove();
    });
}

function ShowChangePassDlg() {
    ShowIFrameModal("Zamba Software - Cambiar Contraseña", "../../Views/Security/UsrPsswrd.aspx", 550, 350);
}
function CloseChangePassDlg() {
    $("#modalDialog").dialog("Close");
}

//Cambia el tamaño del diálogo que genera la llamada al tb_show
function ResizeInsertDialogToShow(height, width) {
    $("#TB_window").find(":first-child").height(height);
    if (width !== undefined) {
        $("#TB_iframeContent").width(width);
        $("#TB_iframeContent").height(height);
    }
}
function HideDivPresentation() {
    var zi = $("#divPrimario").css("z-index");
    if (zi == 1000) {
        $("#divPrimario").css("z-index", "3000");
        $("#divSecundario").css("z-index", "1000");
    }
    else {
        $("#divPrimario").css("z-index", "1000");
        $("#divSecundario").css("z-index", "3000");
    }
}
function SelectTabFromMasterPage(tab) {
    //Antes de seleccionar nada, se elimina el tab dummy anteriormente creado.
    if ($("#DummyTab")) {
        $("#DummyTab").remove();
    }
    var tabIndex;
    switch (tab) {
        case "tabhome":
            tabIndex = 0;
            break;
        case "tabnews":
            tabIndex = 1;
            break;
        case "tabsearch":
            tabIndex = 2;
            break;
        case "tabresults":
            tabIndex = 3;
            break;
            //case "tabInsert":
            //    tabIndex = 4;
            //    break;
        case "tabtasklist":
            tabIndex = 4;
            break;
        case "tabtasks":
            tabIndex = 5;
            break;
    }
    $('#MainTabber').tabs().tabs('option', 'active', tabIndex);
    $("#divPrimario").hide();
    $("#divSecundario").show();
}
function HomeCabPresentation() {
    //SE COMENTA ESTA FUNCION VER SI ES NECESARIA YA QUE SE RENOVO POR BOOTSTRAP MODAL SIN ALTERAR EL MENU

    //Se crea este tab dummy para corregir el problema de renderizado al abrir la home. OBSOLETO JQUERY.UI
    //var divtag = '<div id="DummyTab" style="padding:0px;height:409px"><div/>';
    //$(divtag).appendTo('#MainTabber');
    //$('#MainTabber').tabs("add", '#DummyTab', "DummyTab");
    //$('#MainTabber').tabs("select", "#DummyTab");

    //$("#divPrimario").show();
    //$("#divSecundario").hide();
    //$("#divSemaphore").hide();
    //$("#divReport").hide();
    //$("#dvSiteMap").remove();
    //$("#divTabDOR").remove();
    //$("#SemaphoreIF").hide();
    ////Se remueve la clase y se suma nuevamente para reiniciar la apariencia de la home

    //$("#second-div-presentation-Main").removeClass("second-div-IFrame");
    //$("#second-div-presentation-Main").addClass("second-div-presentation-Main");
}
function SelectTaskFromModal() {
    //Antes de seleccionar nada, se elimina el tab dummy anteriormente creado.
    if ($("#DummyTab")) {
        $('#MainTabber').zTabs("remove", "#DummyTab");
    }

    $("#divPrimario").hide();
    $("#divSecundario").show();
    $('#MainTabber').zTabs("select", '#tabtasks');
    if ($("#divReports")) {
        $("#divReports").dialog("close");
    }
    if ($("#divSemaphore")) {
        $("#divSemaphore").dialog("close");
    }

}
function InsertFormModal(formid) {
    var url = "../WF/DocInsertModal.aspx?formid=" + formid;
    ShowIFrameModal("", url, 800, 600);
    // StartObjectLoadingObserverById("IFDialogContent");
}
//Crea una nueva ventana con la url especificada y le setea el opener
function openWindow(url, features, asMaximized) {
    if (asMaximized) {
        if (features == '')
            features += ",screenX=0,screenY=0,left=0,top=0";
        else
            features += "screenX=0,screenY=0,left=0,top=0";

        features += ",width=" + (screen.availWidth - 10).toString();
        features += ",height=" + (screen.availHeight - 30).toString();
    }
    WindowRef = window.open(url, '', features);
    if (WindowRef != null) {
        WindowRef.opener = self;
    }
    WindowRef.focus();
}

function HideHome() {
    $("#divPrimario").hide();
    $("#divSecundario").show();
}

function ResizeTBIframe(width, height) {
    if (height > 0) {
        $("#TB_iframeContent").height(height);
        $("#TB_window").height(height);
    }

    if (width > 0) {
        $("#TB_iframeContent").width(width);
        $("#TB_window").width(width);
    }

    $("#TB_window").css("overflow", "hidden");
}

function SetNewEntryRulesGroup() {
    SetNewGeneralExecution();
}

function ShowEntryRulesPanel(value) {
    var rulesContent = $("#EntryRulesContent");
    var rulesIF = $("#ctl00_WFExecForEntryRulesFrame");

    if (value) {
        rulesContent.css("top", $(document).height() / 2 - rulesContent.height() / 2);
        rulesContent.css("left", $(document).width() / 2 - rulesContent.width() / 2);
        rulesContent.show();
        rulesIF.show();
    }
    else {
        //rulesIF.hide();
        //rulesContent.hide();
        //rulesContent.css("top", "-1000");
    }
}

function CloseEntryRulesPanel() {
    //$("#ctl00_WFExecForEntryRulesFrame").hide();
    //$("#EntryRulesContent").hide();
    //setTimeout("parent.hideLoading();", 2000);
    $('#openModalIFWF').hide();
}

function ResizeEntryRulesPanel(width, height) {
    //Se suma 30 por la barra del título 
    height = height + 50;
    if (height > 0) {
        $("#EntryRulesContent").height(height);
        $("#ctl00_WFExecForEntryRulesFrame").height(height);
    }

    if (width > 0) {
        $("#EntryRulesContent").width(width);
        $("#ctl00_WFExecForEntryRulesFrame").width(width);
    }
}

function RedirectToErrorPage(url) {
    //Se sobreescribe el beforenunload para lanzar el cierre de tareas.
    window.onbeforeunload = function () {

        showedDialog = true;
    };
    document.location = url;
}

function RedirectToLogin() {
    window.onbeforeunload = function () {

        showedDialog = true;
    };
    var destinationURL = "../Security/Login.aspx";
    document.location = destinationURL;
}

function RedirectToSiteMap() {
    $("#divSem").remove();
    $("#divTabDOR").remove();
    $("#dvSiteMap").remove();
    $("#divReports").remove();
    $("#divTabSemaphore").remove();
    $("#divGR").remove();

    var url = "../Aysa/ZSiteMap.aspx";
    ShowIFrameModal("Mapa del sitio", url, 800, 600);
    //   StartObjectLoadingObserverById("IFDialogContent");
}

function ZDispatcherRedirection_tabResult() {
    SelectTabFromMasterPage('tabsearch');
}

function ZDispatcherRedirection_tabSearchResults() {
    SelectTabFromMasterPage('tabresults');
}

function ZDispatcherRedirection_tabtasklist() {
    $('#MainTabber').tabs("select", '#tabtasklist');
    $("#divPrimario").hide();
    $("#divSecundario").show();
}

function ZDispatcherRedirection_tabtasks() {
    $('#MainTabber').tabs("select", '#tabtasks');
    $("#divPrimario").hide();
    $("#divSecundario").show();
}

function ShowInsertedDialog() {
    $("#divMessages").dialog({ autoOpen: false, modal: true, position: 'center', closeOnEscape: false, draggable: false, resizable: false, open: function (event, ui) { $(".ui-dialog-titlebar-close").hide(); } });
    $("#divMessages").dialog('open');
}

function CloseInsertedDialog() {
    if ($("#divMessages").dialog("isOpen")) {
        $("#divMessages").dialog("close");
    }
}

function getMainMenuHeightFromParent() {
    var headerHeight = $("#mainMenu").outerHeight();
    return headerHeight;
}

function getMTH() {
    return getMTHeight();
}

function getTBH() {
    return getTBHeight();
}

function SwitchToIFrameClass() {
    $("#second-div-presentation-Main").removeClass("second-div-presentation-Main");
    $("#second-div-presentation-Main").removeClass("second-div-IFrame");
    $("#second-div-presentation-Main").addClass("second-div-IFrame");
}

function btnHelpCab_Click() {
    window.open("../../forms/" + getPageTheme() + "/InstructivoZamba.pdf");
}

function getMaxZ() {
    var opt = { inc: 5 };
    var def = { inc: 10, group: "*" };
    $.extend(def, opt);
    var zmax = 0;
    $(def.group).each(function () {
        var cur = parseInt($(this).css('z-index'));
        zmax = cur > zmax ? cur : zmax;
    });
    zmax += def.inc;
    return zmax;
}


function ShowLoadingAnimation() {
    if ($(".blockUI").length == 0) {
        $.blockUI({ message: '<span class="glyphicon glyphicon-refresh glyphicon-refresh-animate" style="display: inline-block;"></span><span style="display: inline-block;"><h4> Procesando...</h4></span>' });
       setTimeout(function () {
            hideLoading();
            //$('.blockMsg').append(
            //    "<button id='btnCloseLoading' onclick='hideLoading()' class='btn btn-danger btn-sm' style='margin-bottom:10px'>Cerrar</button>");
        }, 7500);//10000
    }
}
function hideLoading() {
    if ($(".blockUI").length > 0) {
        //if (loadingTimer !== undefined) clearTimeout(loadingTimer);
        //if ($("#btnCloseLoading").lenght > 0) $("#btnCloseLoading").remove();
        $.unblockUI();
    }
}
function BlockWebPageInteraction() {
    $("#MasterHeader").block({ message: null, overlayCSS: { backgroundColor: 'transparent' } });
    $("#mainMenu").block({ message: null, overlayCSS: { backgroundColor: 'transparent' } });
    $("#TasksDivUL").block({ message: null, overlayCSS: { backgroundColor: 'transparent' } });
}
function UnblockWebPageInteraction() {
    $("#MasterHeader").unblock();
    $("#mainMenu").unblock();
    $("#TasksDivUL").unblock();
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
function ConvertToUpperForm() {
    $('.ConvertToUpper').each(function (i, item) {
        var cadena = $(item).val();
        cadena = cadena.toUpperCase();
        $(item).val(cadena);
    });
}


$.fn.hasAttr = function (name) {
    return this.attr(name) !== undefined;
};

function AddDynamicsTags() {

    if ($('#hdnRuleId').length == 0) {
        $('<input>').attr({
            type: 'hidden',
            id: 'hdnRuleId',
            name: 'hdnRuleId'
        }).appendTo('form');
    }

    if ($('#hdnAsocId').length == 0) {
        $('<input>').attr({
            type: 'hidden',
            id: 'hdnAsocId',
            name: 'hdnAsocId'
        }).appendTo('form');
    }

    if ($('#ZPOSTBACKFUNCTION').length == 0) {
        $('<input>').attr({
            type: 'hidden',
            id: 'ZPOSTBACKFUNCTION',
            name: 'ZPOSTBACKFUNCTION'
        }).appendTo('form');
    }

    if ($('form').length == 1) {
        $('form').each(function () {
            if ($(this).hasAttr('id') == false || $(this).attr('id') == '') {
                $(this).attr('id', 'MainForm');
            }
        })
    }
}

$(document).ready(function () {
    AddDynamicsTags();
    //Compruebo que Bootstrap este cargado para que no de error
    if (typeof $().modal == 'function')
        $('.dropdown-toggle').dropdown();

});

function reloadBootstrap() {
    var docURL = null;
    if (parent.document.config == undefined) {
        docURL = parent.document.URL
    }
    else {
        docURL = document.config == undefined ? parent.document.config.urlBase : document.config.urlBase;
    }
    $('head').append("<script src='" + docURL + "/scripts/bootstrap.min.js' type='text/javascript'></script>");

}
