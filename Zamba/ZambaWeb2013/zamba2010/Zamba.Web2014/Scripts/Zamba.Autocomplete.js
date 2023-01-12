(function () {
    jQuery.fn.zAutocomplete = function () {
        //Por cada autocomplete
        $(this).each(function () {
            //Obtengo las propiedades de configruacion
            var source = $(this).attr("source");
            var displayMember = $(this).attr("displayMember");
            var valueMember = $(this).attr("valueMember");
            var additionalFilters = $(this).attr("additionalFilters");
            var controlId = this.id;
            var wsAdditionalParams;

            //Si tiene las propiedades minimas(source, display y value)
            if (source != "" && source != null
                && displayMember != "" && displayMember != null
                && valueMember != "" && valueMember != null) {

                ShowLoadingAnimation();
                //Instancio el autocomplete
                $(this).autocomplete({
                    //En el source se utiliza una funcion que por medio de ajax completa las opciones
                    // Los filtros adicionales pueden o no estar seteados
                    source: function (request, response) {
                        $.ajax({
                            type: "POST",
                            url: document.config.urlBase + "/Views/WF/TaskViewer.aspx/GetAutoCompleteOptions",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: "{ " +
                                "query: '" + request.term + "'," +
                                "dataSource: '" + source + "'," +
                                "displayMember: '" + displayMember + "'," +
                                "valueMember: '" + valueMember + "'," +
                                "additionalFilters: " + ((additionalFilters == null || additionalFilters == '') ? "''" : "'" + GetPipedSeparateValues(additionalFilters.split('|'))) + "'" +
                            " }",
                            success: function (data) {
                                //Al volver de la llamada de ajax, se mapea la lista de key value a las opciones de ajax
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item.Key,
                                        value: item.Value
                                    }
                                }));
                                hideLoading();
                            },
                            error: function (request, status, err) {
                                alert("Error al obtener el contenido de autocompletar. \r" + request.responseText);
                            }
                        });
                    },
                    minLength: 3,
                    delayType: 100,
                    appendTo: "#aspnetForm"
                });
            }
        });
    }
})();
