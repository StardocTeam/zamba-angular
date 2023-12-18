﻿(function () {
    jQuery.fn.zAjaxTable = function () {
        //Por cada combo que se llene con zvar
        $(this).each(function () {
            //Obtengo las propiedades de configruacion
            var source = $(this).attr("source");
            var filterFieldId = $(this).attr("filterField");
            var showColumns = $(this).attr("showColumns");
            var editablecolumns = $(this).attr("editablecolumns");
            var editableColumnsAttributes = $(this).attr("editableColumnsAttributes");
            var additionalValidationButton = $(this).attr("additionalValidationButton");
            var postAjaxFuncion = $(this).attr("postAjaxFuncion"); 
            var controlId = this.id;

            //Si tiene las propiedades minimas(source, display y value)
            if (source != "" && source != null
                && filterFieldId != "" && filterFieldId != null
                && showColumns != "" && showColumns != null
                && editablecolumns != "" && editablecolumns != null) {

                //Pregunto si debe filtrar o no, si no debe filtrar solo lleno el combo, si filtra ademas agrego el change
                if (filterFieldId != "" && filterFieldId != null) {
                    //Si hay mas de un id de filtro
                    if (filterFieldId.indexOf('|') > -1) {
                        SetGetRowsMultipleFilters(controlId, source, filterFieldId, showColumns, editablecolumns, editableColumnsAttributes, additionalValidationButton, postAjaxFuncion);
                    }
                    else {
                        $('#' + filterFieldId).change(function () {
                            GetZvarRows(controlId, source, showColumns, filterFieldId, editablecolumns, editableColumnsAttributes, $(this).val(), additionalValidationButton, postAjaxFuncion);
                        });
                        GetZvarRows(controlId, source, showColumns, filterFieldId, editablecolumns, editableColumnsAttributes, $('#' + filterFieldId).val(), additionalValidationButton, postAjaxFuncion);
                    }
                }
                else
                    GetZvarRows(controlId, source, showColumns, filterFieldId, editableColumnsAttributes, editablecolumns, '', additionalValidationButton, postAjaxFuncion);
            }
        });
    }
})();

function SetGetRowsMultipleFilters(controlId, source, filterFieldId, showColumns, editablecolumns, editableColumnsAttributes, additionalValidationButton, postAjaxFuncion) {
    var filters = filterFieldId.split('|');
    var values = '';
    for (var i = 0; i < filters.length; i++) {
        if (filters[i] != null && filters[i] != '') {
            $('#' + filters[i]).change(function () {
                GetZvarRows(controlId, source, showColumns, filterFieldId, editablecolumns, editableColumnsAttributes, GetPipedSeparateValues(filters), additionalValidationButton, postAjaxFuncion);
            });
        }
    }
    GetZvarRows(controlId, source, showColumns, filterFieldId, editablecolumns, editableColumnsAttributes, GetPipedSeparateValues(filters), additionalValidationButton, postAjaxFuncion);
}

function GetZvarRows(controlId, source, showColumns, filterFieldId, editablecolumns, editableColumnsAttributes, filterValues, additionalValidationButton, postAjaxFuncion) {
    ShowLoadingAnimation();
    $.ajax({
        type: "POST",
        url: thisDomain + "/Views/WF/TaskViewer.aspx/GetZDynamicTable",
        data: "{ controlId: '" + controlId + "', dataSource: '" + source + "', showColumns: '" +
                    showColumns + "', filterFieldId: '" + filterFieldId + "', editableColumns: '" + editablecolumns +
                    "', editableColumnsAttributes: '" + editableColumnsAttributes + "',filterValues: '" +
                    filterValues + "', additionalValidationButton: '" + additionalValidationButton + "', postAjaxFuncion: '" + postAjaxFuncion + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: ZvarRowsComplete,
        cache: true,
        error: function (request, status, err) {
            console.log("Error al obtener el cuerpo de tabla. \n" + request.responseText);
        }
    });
}

function ZvarRowsComplete(msg) {
    var controlId = "#" + msg.d.ControlId;
    if ($(controlId).html != msg.d.SelectOptions) {
        $(controlId).html(msg.d.SelectOptions);

        if (msg.d.AdditionalValidationButton != null && msg.d.AdditionalValidationButton != '') {
            SetValidationsAction(msg.d.AdditionalValidationButton);
        }
        else {
            SetFieldsValidations();
        }

        if (msg.d.PostAjaxFunction != null && msg.d.PostAjaxFunction != '') {
            eval(msg.d.PostAjaxFunction + "();");
        }
    }
    FixFocusError();
    hideLoading();
}