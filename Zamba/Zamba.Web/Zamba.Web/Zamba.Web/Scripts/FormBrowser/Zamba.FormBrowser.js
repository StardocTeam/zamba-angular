
function SetValidateForLength() {
    try {

        $(".length").each(function () {
            try {

                if (!$(this).hasClass("readonly") && this.constructor.name != "Window" && $(this)[0].id != 'hdnRuleId') {
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
                        if ($(this).rules != undefined)
                            $(this).rules("add", {
                                maxlength: length,
                                messages: {
                                    maxlength: jQuery.validator.format("Se ha excedido largo de " + length + " caracteres.")
                                }
                            });
                    }
                }
            } catch (e) {
                console.error(e);

            }
        });
    } catch (e) {
        console.error(e);

    }
}






function SetValidateForDataType() {
    try {

        SetValidations($(".dataType"));
        function SetValidations($elem) {
            try {
                $elem.each(function () {
                    try {

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
                                        if ($(this).rules != undefined) {
                                            $(this).rules("add", {
                                                digits: true,
                                                messages: {
                                                    digits: jQuery.validator.format("Solo se permite un n&uacute;mero entero.")
                                                }
                                            });
                                        }
                                        break;
                                    case "date":
                                        MakeDateIndexs(this);
                                        if ($(this).rules != undefined) {
                                            $(this).rules("add", {
                                                dateAR: true,
                                                messages: {
                                                    dateAR: jQuery.validator.format("La fecha debe estar en formato:<br/> dia/mes/año.")
                                                }
                                            });
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }

                    } catch (e) {
                        console.error(e);
                    }

                });
            } catch (e) {
                console.error(e);
            }
        }
    } catch (e) {
        console.error(e);
    }
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

function GetHierarchyOptions(IndexId, ParentIndexId, ParentValue, SenderID, event) {
    if (event !== undefined) {
        event.stopImmediatePropagation();
        event.preventDefault();
    }
    if (document.config == undefined) document.config = parent.document.config;
    var userId;

    var IndexIdTag = $("#zamba_index_" + IndexId);
    if (IndexIdTag) {

        userId = GetUID();

        var url;
        var params;
        if (SenderID) {
            url = thisDomain + "/Services/IndexService.asmx/GetHierarchyOptionsWidthID";
            params = { IndexId: IndexId, ParentIndexId: ParentIndexId, ParentValue: ParentValue, userId: userId, SenderID: SenderID };
        }
        else {
            url = thisDomain + "/Services/IndexService.asmx/GetHierarchyOptions";
            params = params = { IndexId: IndexId, ParentIndexId: ParentIndexId, ParentValue: ParentValue, userId: userId };
        }
        //parent.toastr.clear();
        //parent.toastr.info("Por favor aguarde unos instantes a que se complete los atributos", "Zamba");
        $.ajax({
            type: "POST",
            url: url,
            data: params,
            success: function (d) {
                IndexsCallback(xml2json(d));
                //parent.toastr.clear();
                //parent.toastr.success("Atributos cargados", "Zamba");
            },
            error: function (xhr, ajaxOptions, thrownError) {
                IndexsOnError(xhr);
            }
        });
        return true;
    }
    return true;
}


function SetMinValuesValidations() {
    $(".haveMinValue").each(function () {
        applySetMinValuesValidations(this);
    });
    //$('.IFTaskContent').contents().children().find(".haveMinValue").each(function () {
    //    applySetMinValuesValidations(this);
    //});
}

function applySetMinValuesValidations(t) {
    if (!$(t).hasClass("readonly")) {
        var minValue = $(t).attr("ZMinValue");
        var dataType = $(t).attr("dataType");
        var aceptEquals = minValue.startsWith("=");
        minValue = minValue.replace("=", "");

        if (minValue) {
            // $(this).parents("form").validate();
            //Si existe la funcion para reemplazar con el dia de hoy

            if (minValue == "ZGetDate") {
                //  minValue = (new Date()).toISOString().split('T')[0];
                minValue = getLocaleShortDateString(new Date());// Se cambio porque tenia formato español y daba error en consola
            }
            // else //Se cambio formato por yyyy/MM/dd (14-04-2016)
            //  minValue= new Date(minValue).toISOString().split('T')[0];

            //Si es menor o igual
            if (aceptEquals) {
                if ($(t).rules) {
                    $(t).rules("add", {
                        lessEqualThan: minValue,
                        messages: {
                            lessEqualThan: "El valor debe ser mayor o igual que: " + minValue
                        }
                    });
                }
            }
            else {
                if ($(t).rules) {
                    $(t).rules("add", {
                        lessThan: minValue,
                        messages: {
                            lessThan: "El valor debe ser mayor que: " + minValue
                        }
                    });
                }
            }

            if (dataType == "date") {
                var minDate;
                if (aceptEquals)
                    minDate = CreateDate(Number(minValue.split("/")[0]), minValue.split("/")[1] - 1, minValue.split("/")[2]);
                else
                    minDate = CreateDate(Number(minValue.split("/")[0]) + 1, minValue.split("/")[1] - 1, minValue.split("/")[2]);

                $(t).datepicker("option", "minDate", minDate);
            }
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
                    if ($(this).rules != undefined)
                        $(this).rules("add", {
                            greaterEqualThan: maxValue,
                            messages: {
                                greaterEqualThan: "El valor debe ser menor o igual que: " + maxValue
                            }
                        });
                }
                else {
                    if ($(this).rules != undefined)
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




function SetListFilters() {
    //Obtiene todos los tags select
    $('select:not(:disabled)').each(function () {
        //Verifica que lo que encontro sea un índice
        if ($(this).attr('id') != undefined && $(this).attr('id').toLowerCase().indexOf('zamba_index_') > -1 && !$(this).hasClass("readonly") && $(this).css('display') != 'none' && $(this).hasClass("addFilterSearch")) {
            //Agrega la lupa para filtrar y buscar valores
            $(this)//.attr('id').onclick(CreateTable(this, false));
                .click(function () { CreateTable(this, false) });
            //AddFilter($(this).attr('id'), false);
        }
    });
}




function SetValidationsAction(SubmitButtonId) {
    try {
        SetValidateForLength();
    } catch (e) {
        console.error(e);
    }

    try {
        SetValidateForDataType();

    } catch (e) {
        console.error(e);
    }

    try {
        SetValidateForRequired();

    } catch (e) {
        console.error(e);
    }

    try {
        SetDefaultValues();

    } catch (e) {
        console.error(e);
    }

    try {
        SetHierarchicalFunctionally();

    } catch (e) {
        console.error(e);
    }

    try {
        SetMinValuesValidations();

    } catch (e) {
        console.error(e);
    }

    try {
        SetMaxValuesValidations();

    } catch (e) {
        console.error(e);
    }
    try {
        SetListFilters();

    } catch (e) {
        console.error(e);
    }

    try {

        if (SubmitButtonId) {
            $("#" + SubmitButtonId).click(function (evt) {
                if ($("#aspnetForm").valid != undefined) {
                    if (!$("#aspnetForm").valid()) {
                        evt.preventDefault();
                        setTimeout("parent.hideLoading();", 1000);
                    }
                }
            });
        }

    } catch (e) {
        console.error(e);
    }


    try {
        if (jQuery.data(document.forms[0], "validator") == null) {
            if ($('#aspnetForm').validate) $('#aspnetForm').validate({ onsubmit: false });
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
    } catch (e) {
        console.error(e);

    }
}



function ExecuteFormReadyActions() {
    try {
        var btnzamba_showOriginal = $("#zamba_showOriginal")
        if (btnzamba_showOriginal != null) {
            $("#zamba_showOriginal").click(function () {
                ShowLoadingAnimation();
            });
        };
    } catch (e) {
        console.error(e);
    }


    try {

        $("#zamba_save").click(function () {
            ShowLoadingAnimation();
        });
    } catch (e) {
        console.error(e);
    }


    //Obtiene los textbox de los formularios, y les agrega en el foco la seleccion del texto.
    //Con esto se deberia solucionar el problema del "bloqueo de los textbox".
    try {
        FixFocusError();
    } catch (e) {
        console.error(e);
    }


    // if ($('#aspnetForm').validate) $('#aspnetForm').validate({ onsubmit: false });
    try {
        SetValidationsAction("zamba_save");
    } catch (e) {
        console.error(e);
    }

    try {
        ajustarselects();
    } catch (e) {
        console.error(e);
    }



    try {

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

        $("button[id^=zamba_rule_]").each(function () {
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


    } catch (e) {
        console.error(e);
    }


    try {
        //Inicializar los combos dependientes de zvar
        $(".zvarDropDown").zvarDropDown();
    } catch (e) {
        console.error(e);
    }

    try {
        ////Inicializar los tablas dependientes de zvar o ajax
        $(".zAjaxTable").zAjaxTable();
    } catch (e) {
        console.error(e);
    }

    try {
        ////Inicializar los autocomplete
        $(".zAutocomplete").zAutocomplete();
    } catch (e) {
        console.error(e);
    }

    try {
        getEnableButton();
    } catch (e) {
        console.error(e);
    }

    try {
        getEditGroups();
    } catch (e) {
        console.error(e);
    }
}

