(function () {
    jQuery.fn.zvarDropDown = function () {
        //Por cada combo que se llene con zvar
        $(this).each(function () {
            //Obtengo las propiedades de configruacion
            var filterFieldId = $(this).attr("filterField");
            var displayMember = $(this).attr("displayMember");
            var valueMember = $(this).attr("valueMember");
            var zvarSource = $(this).attr("zvarSource");
            var filterColumn = $(this).attr("filterColumn");
            var controlId = this.id;

            //Si tiene las propiedades minimas(source, display y value)
            if (displayMember != "" && displayMember != null
                && valueMember != "" && valueMember != null
                && zvarSource != "" && zvarSource != null) {

                //Pregunto si debe filtrar o no, si no debe filtrar solo lleno el combo, si filtra ademas agrego el change
                if (filterFieldId != "" && filterFieldId != null) {
                    //Si hay mas de un id de filtro
                    if (filterFieldId.indexOf('|') > -1) {
                        SetGetOptionsMultipleFilters(controlId, zvarSource, displayMember, valueMember, filterColumn, filterFieldId);
                    }
                    else {
                        $('#' + filterFieldId).change(function () {
                            GetZvarOptions(controlId, zvarSource, displayMember, valueMember, filterColumn, $(this).val());
                        });
                        GetZvarOptions(controlId, zvarSource, displayMember, valueMember, filterColumn, $('#' + filterFieldId).val());
                    }
                }
                else
                    GetZvarOptions(controlId, zvarSource, displayMember, valueMember, filterColumn, '');
            }
        });
    }
})();

function SetGetOptionsMultipleFilters(controlId, zvarSource, displayMember, valueMember, filterColumn, filterControlsIds) {
    var filters = filterControlsIds.split('|');
    var values = '';
    for (var i = 0; i < filters.length; i++) {
        if (filters[i] != null && filters[i] != '') {
            $('#' + filters[i]).change(function () {
                GetZvarOptions(controlId, zvarSource, displayMember, valueMember, filterColumn, GetPipedSeparateValues(filters));
            });
        }
    }
    GetZvarOptions(controlId, zvarSource, displayMember, valueMember, filterColumn, GetPipedSeparateValues(filters));
}

function GetZvarOptions(controlId, zvarSource, displayMember, valueMember, filterColumn, filterValue) {
    $.ajax({
        type: "POST",
        url: document.config.urlBase + "/Views/WF/TaskViewer.aspx/GetZVarOptions",
        data: "{ controlId: '" + controlId + "', dataSourceName: '" + zvarSource + "', displayMember: '" +
                    displayMember + "', valueMember: '" + valueMember + "', filterColumn: '" + filterColumn + "', filterValue: '" + filterValue + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: ZvarOptionsComplete,
        cache: true,
        error: function (request, status, err) {
            alert("Error al obtener opciones para el atributo \r" + request.responseText);
        }
    });
}

function ZvarOptionsComplete(msg) {
    var controlId = "#" + msg.d.ControlId;
    $(controlId).html(msg.d.SelectOptions);
    FixFocusError();
}