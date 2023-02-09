var localSearchResults;
var _searchUserId;
var isDateField = [];
var columnsString = null;

function KendoGrid(searchResults, searchUserId) {

    try {
        if ($('#Kgrid').data().kendoGrid != undefined) {
            CleanKGrid();
        }
    } catch (e) {
        console.log(e);
    }

    _searchUserId = searchUserId;
    localSearchResults = searchResults;
    var model = generateModel(localSearchResults);

    var columns = null;
    try {
        var columnsStringMyTasks = localSearchResults.columnsStringMyTasks;
        var columnsStringTeam = localSearchResults.columnsStringTeam;
        var columnsStringAll = localSearchResults.columnsStringAll;

        //'[{"selectable":true,"width":65,"filterable":false,"resizable":true},{"field":"Icon","format":"","width":35,"template":"<div class=customer-photo style=background-image: url(../../content /#:Icon#); ></div>","sortable":false,"filterable":false,"resizable":true},{"field":"Tipo_Doc_Notificacion","format":"","width":150,"filterable":true,"resizable":true},{"field":"Fecha_y_Hora_de_Recepcion","format":"","width":150,"filterable":true,"resizable":true},{"field":"Fecha_de_Notificacion","format":"","width":150,"filterable":true,"resizable":true},{"field":"Fecha_de_Vencimiento_Audiencia","format":"","width":150,"filterable":true,"resizable":true},{"field":"Actor","format":"","width":100,"filterable":true,"resizable":true},{"field":"Demandado","format":"","width":100,"filterable":true,"resizable":true},{"field":"Nro_de_Etiqueta","format":"","width":150,"filterable":true,"resizable":true},{"field":"J_o_M","format":"","width":150,"filterable":true,"resizable":true},{"field":"Nro_Juicio_Mediacion","format":"","width":150,"filterable":true,"resizable":true},{"field":"Poliza","format":"","width":150,"filterable":true,"resizable":true},{"field":"Nro_de_Siniestro","format":"","width":50,"filterable":true,"resizable":true},{"field":"Asignado","format":"","width":80,"filterable":true,"resizable":true},{"field":"Etapa","format":"","width":80,"filterable":true,"resizable":true},{"field":"ENTIDAD","format":"","width":0,"filterable":true,"resizable":true},{"field":"LEIDO","format":"","hidden":true,"filterable":false,"width":0,"resizable":true},{"field":"DOC_ID","format":"","hidden":true,"filterable":false,"width":150,"resizable":true},{"field":"DOC_TYPE_ID","format":"","hidden":true,"filterable":false,"width":150,"resizable":true},{"field":"Tarea","format":"","width":150,"filterable":true,"resizable":true},{"field":"INGRESO","format":"{0:dd-MM-yyyy}","width":150,"filterable":true,"resizable":true},{"field":"TASK_ID","format":"","hidden":true,"filterable":false,"width":150,"resizable":true},{"field":"STEP_ID","format":"","hidden":true,"filterable":false,"width":0,"resizable":true},{"field":"Sdo_Vto_2da_Aud","format":"","width":150,"filterable":true,"resizable":true},{"field":"Estado_de_Notificacion","format":"","width":150,"filterable":true,"resizable":true},{"field":"Estudio","format":"","width":80,"filterable":true,"resizable":true},{"field":"Estado_Procesal","format":"","width":150,"filterable":true,"resizable":true},{"field":"Estudio_Negociador","format":"","width":150,"filterable":true,"resizable":true},{"field":"Abogado_Actor","format":"","width":150,"filterable":true,"resizable":true},{"field":"Embargo","format":"","width":150,"filterable":true,"resizable":true}]';
        columnsString = columnsStringMyTasks;
        if (columnsString != undefined && columnsString != null && columnsString.trim() != '') {
            columns = JSON.parse(columnsString.trim());
        }
        else {
            columns = generateColumns(localSearchResults);
        }
    } catch (e) {
        console.log(e);
        columns = generateColumns(localSearchResults);

    }
    kendo.culture("es-AR");

    var grid = $('#Kgrid');
    grid.kendoGrid({
        dataSource: {
            transport: {
                read: function (options) {
                    //ResizeResultsArea();
                    options.success(localSearchResults.data);
                }
            },
            noRecords: {
                template: ""
            },
            pageSize: localSearchResults.data.length,
            serverPaging: true,
            serverSorting: true,
            serverFiltering: true,
            schema: {
                model: model
            }
        },
        scrollable: {
            endless: true
        },
        scrollable: true,
        columns: columns,
        dataBound: onDataBound,
        pageable: {
            numeric: false,
            previousNext: false,
            refresh: false,
            pageSizes: false,
            last: false,
            first: false,
            messages: {
                display: ''
            }
        },
        pageable: false,
        sortable: true,
        groupable: false,
        columnMenu: {
            columns: false,
            messages: {
                sortAscending: "Ascendente",
                sortDescending: "Descendente",
            }
        },
        reorderable: true,
        resizable: true,
        sort: onSorting,
        page: onPaging,
        scroll: onPaging,
        nagivatable: true
    });

    var grid = $('#Kgrid').data('kendoGrid');
    grid.tbody.on("click", "tr", onClick);

    var checkBox_SelectAll = $("#Kgrid").find(".k-checkbox")[0];
    checkBox_SelectAll.setAttribute("onclick", "pushearALista()");

    var actorID = getColumnIndex(grid.columns, "Actor"),
        DemandadoID = getColumnIndex(grid.columns, "Demandado"),
        Juicio_o_MediacionID = getColumnIndex(grid.columns, "Juicio_o_Mediacion"),
        Nro_Juicio_MediacionID = getColumnIndex(grid.columns, "Nro_Juicio_Mediacion"),
        RamoID = getColumnIndex(grid.columns, "Ramo"),
        PolizaID = getColumnIndex(grid.columns, "Nro_de_Poliza"),
        Nro_de_SiniestroID = getColumnIndex(grid.columns, "Nro_de_Siniestro"),
        tipo_notificacionID = getColumnIndex(grid.columns, "Tipo_de_Notificación"),
        IngresoColumnID = getColumnIndex(grid.columns, "INGRESO"),
        EtapaColumnID = getColumnIndex(grid.columns, "Etapa"),
        EstadoColumnID = getColumnIndex(grid.columns, "Estado"),
        AsignadoColumnID = getColumnIndex(grid.columns, "Asignado"),
        EntidadID = getColumnIndex(grid.columns, "ENTIDAD"),
        TareaColumnID = getColumnIndex(grid.columns, "Tarea");

    var actor = grid.columns[actorID],
        Demandado = grid.columns[DemandadoID],
        Juicio_o_Mediacion = grid.columns[Juicio_o_MediacionID],
        Nro_Juicio_Mediacion = grid.columns[Nro_Juicio_MediacionID],
        Ramo = grid.columns[RamoID],
        Poliza = grid.columns[PolizaID],
        Nro_de_Siniestro = grid.columns[Nro_de_SiniestroID],
        IngresoColumn = grid.columns[IngresoColumnID],
        EtapaColumn = grid.columns[EtapaColumnID],
        EstadoColumn = grid.columns[EstadoColumnID],
        AsignadoColumn = grid.columns[AsignadoColumnID],
        Entidad = grid.columns[EntidadID],
        TareaColumn = grid.columns[TareaColumnID],
        tipo_notificacion = grid.columns[tipo_notificacionID];

    var columnsCount = grid.columns.length;

    grid.dataSource.total = getResultsTotal;
    //grid.dataSource.read();
}

function generateModel(response) {

    var sampleDataItem = response["data"][0];

    var model = {};
    var fields = {};
    for (var property in sampleDataItem) {
        if (property == ("ID")) {
            model["id"] = property;
        }
        var propType = typeof sampleDataItem[property];

        if (propType === "number") {
            fields[property] = {
                type: "number",
                validation: {
                    required: true
                }
            };
            if (model.id === property) {
                fields[property].editable = false;
                fields[property].validation.required = false;
            }
        } else if (propType === "boolean") {
            fields[property] = {
                type: "boolean"
            };
        } else if (propType === "string") {
            var parsedDate = kendo.parseDate(sampleDataItem[property]);
            if (parsedDate) {

                if (property.indexOf("Fecha") != -1 && property.indexOf("Hora") != -1) {
                    fields[property] = {
                        type: "datetime",
                        validation: {
                            required: true
                        }
                    };

                }
                else

                    fields[property] = {
                        type: "date",
                        validation: {
                            required: true
                        }
                    };
                isDateField[property] = true;
            } else {
                fields[property] = {
                    validation: {
                        required: true
                    }
                };
            }
        } else {
            fields[property] = {
                validation: {
                    required: true
                }
            };
        }
    }

    model.fields = fields;

    return model;
}

function generateColumns(response) {
    var columnNames = response["columns"];
    //inserto columna icono al comienzo de la coleccion
    columnNames.unshift("Icon");
    columnNames.unshift("IsChecked");

    return columnNames.map(function (name) {


        if (name == "IsChecked") {
            return {
                selectable: true, width: 65, filterable: false, resizable: true
            };
        } else if (name == "Icon") {
            return {
                field: name, format: (isDateField[name] ? "{0:dd-MM-yyyy}" : ""), width: 35, template: "<div class='customer-photo'" +
                    "style='background-image: url(" + "../../content/#:Icon#);'></div>",
                sortable: false, filterable: false, resizable: true
            };
        } else if (name == "Importe") {
            return {
                field: name, format: (isDateField[name] ? "{0:dd-MM-yyyy}" : ""), width: GetColumnWidth(name), filterable: true, resizable: true, title: newname
            };
        } else if (name == "DOC_ID" || name == "DOC_TYPE_ID" || name == "STR_ENTIDAD" || name == "THUMB" || name == "FULLPATH" || name == "ICON_ID" || name == "USER_ASIGNED" || name == "EXECUTION" || name == "DO_STATE_ID" || name == "TASK_ID" || name == "RN" || name == "LEIDO" || name == "WORKFLOW" || name == "Proceso" || name == "Estado" || name == "STEP_ID" || name == "ENTIDAD") {
            return {
                field: name, format: (isDateField[name] ? "{0:dd-MM-yyyy}" : ""), hidden: true, filterable: false, width: GetColumnWidth(name), resizable: true
            }
        } else if (name == "ORIGINAL") {
            return {
                field: name, format: (isDateField[name] ? "{0:dd-MM-yyyy}" : ""), width: GetColumnWidth(name), filterable: false, sortable: false, resizable: true
            }

        } else {
            //¿,?,/,&,°,+,-,.,),(,:
            ///\-+-()$%#\ ? - 
            var newname = replaceAll(name, '.', ' ');
            newname = replaceAll(newname, '?', ' ');
            newname = replaceAll(newname, '+', ' ');
            newname = replaceAll(newname, '/', ' ');
            newname = replaceAll(newname, '&', ' ');
            newname = replaceAll(newname, '-', ' ');
            newname = replaceAll(newname, '\\', ' ');
            newname = replaceAll(newname, '%', ' ');
            newname = replaceAll(newname, ')', ' ');
            newname = replaceAll(newname, '(', ' ');
            newname = replaceAll(newname, '#', ' ');
            newname = replaceAll(newname, '$', ' ');
            newname = replaceAll(newname, '+', ' ');
            newname = replaceAll(newname, '°', ' ');
            newname = replaceAll(newname, 'ó', ' ');
            newname = replaceAll(newname, 'ú', ' ');


            return {
                field: name, format: (isDateField[name] ? "{0:dd-MM-yyyy}" : ""), width: GetColumnWidth(name), filterable: true, resizable: true, title: newname
            }
        }
        //};
    })
}

function replaceAll(string, search, replace) {
    return string.split(search).join(replace);
}

replaceAll('abba', 'a', 'i');          // => 'ibbi'

function onDataBinding(arg) {
    console.log("Grid data binding");
}

function onDataBound(arg) {
    if (localStorage && localStorage.getItem("MultiSelectionIsActive") === "false") {
        $(".k-checkbox-label").parent().hide();
    }

    $('#Kgrid').css('font-size', '11px');
    $('.k-header.k-with-icon')[0].childNodes[0].hidden = true;
    $(".k-header-column-menu").kendoTooltip({ content: "Configuración de columna" });

    // iterate the data items and apply row styles where necessary
    var dataItems = arg.sender.dataSource.view();
    for (var i = 0; i < dataItems.length; i++) {
        var row = arg.sender.tbody.find("[data-uid='" + dataItems[i].uid + "']");
        try {
            if (dataItems[i].ShowUnread) {
                row.addClass("notRead");
            }
        } catch (e) {
            console.log(e);
        }

        try {
            if (dataItems[i].Contestaciones != undefined && dataItems[i].Contestaciones != null && dataItems[i].Contestaciones == 0) {
                row.addClass("RowPending");
            }
        } catch (e) {
            console.log(e);
        }

        try {
            if (dataItems[i].Importe != undefined && dataItems[i].Importe != null && parseFloat(dataItems[i].Importe) >= 0) {
                var TR_Row = $("tr[role='row']");
                var TH_Importe = TR_Row.find("th[data-field='Importe']");
                var CellImporte = row[0].cells[TH_Importe.attr("data-index")];
                $(CellImporte).addClass("text-rigth");
            }
        } catch (e) {
            console.log(e);
        }

        try {
            if (dataItems[i].Informes != undefined && dataItems[i].Informes != null && dataItems[i].Informes == 0) {
                row.addClass("RowPending");
            }
        } catch (e) {
            console.log(e);
        }

        try {
            if (dataItems[i].Contestaciones != undefined && dataItems[i].Contestaciones != null && dataItems[i].Contestaciones == 0) {
                row.addClass("RowPending");
            }
        } catch (e) {
            console.log(e);
        }
    }
}

function RefreshKGrid(searchResults) {

    localSearchResults = searchResults;
    //var grid = $('#Kgrid');
    var grid = $('#Kgrid').data('kendoGrid');
    //grid.dataSource.total = getResultsTotal;
    grid.dataSource.read();
    grid.refresh();
}

function FilterKGrid(searchGridText) {
    var filter = { logic: "or", filters: [] };

    if (searchGridText) {
        $.each($("#Kgrid").data("kendoGrid").columns, function (key, column) {
            if (column.filterable) {
                filter.filters.push({ field: column.field, operator: "contains", value: searchGridText });
            }
        });
    }

    $("#Kgrid").data("kendoGrid").dataSource.filter(filter);
    $("#Kgrid").data("kendoGrid").dataSource.read();
    $("#Kgrid").data("kendoGrid").refresh();
}

function CleanKGrid() {
    if ($('#Kgrid').data() != undefined && $('#Kgrid').data().kendoGrid != undefined) {
        $('#Kgrid').data().kendoGrid.destroy();
        $('#Kgrid').empty();
    }
}

function getResultsTotal() {
    if (localSearchResults == undefined)
        return 0;
    else
        return localSearchResults.total;
}

function onChange(arg) {
    var selected = $.map(this.select(), function (item) {
        checkedIds = [];
        selectedRecords_gridResults = [];

        for (i = 0; i < arg.sender._data.length - 1; i++) {
            checkedIds.push(arg.sender._data[i].RN);
        }

        angular.element($("#ResultsCtrl")).scope().GetTaskDocument(checkedIds);
        return item.className;

    });

    if (selected.length > 0) {
        //  $("#BtnClearCheckbox").css('display', 'block');
        $("#BtnSendEmail").css('display', 'block');
        $("#OpenAllSelected").css('display', 'block');
        $("#BtnSendZip").css('display', 'block');
        $("#Actions").css('display', 'block');
    } else {
        for (j = 0; j < arg.sender._data.length - 1; j++) {
            angular.element($("#ResultsCtrl")).scope().RemoveAttach(j);

        }

        checkedIds = [];
        selectedRecords_gridResults = [];
        // document.getElementById("BtnClearCheckbox").setAttribute("disabled", "disabled");
        document.getElementById("BtnSendEmail").setAttribute("disabled", "disabled");
        document.getElementById("OpenAllSelected").setAttribute("disabled", "disabled");
        document.getElementById("BtnSendZip").setAttribute("disabled", "disabled");

        $("#Actions").css('display', 'none');
    }
}

function onFiltering(arg) {
    //var grid = $('#Kgrid').data("kendoGrid");
    //localStorage["kendo-grid-options"] = kendo.stringify(grid.getOptions());
    angular.element($("#ResultsCtrl")).scope().Filter(arg);
}

function AdjustGridColumns() {
    if ($('#Kgrid').length && $('#Kgrid').data("kendoGrid") !== undefined) {

        var grid = $('#Kgrid').data("kendoGrid");

        var _length = (columnsString != undefined && columnsString != null && columnsString != '') ? 25 : 10;
        if (_length > grid.columns.length)
            _length = grid.columns.length;


        for (var i = 1; i < _length; i++) {
            if (grid.columns[i] != undefined && (grid.columns[i].hidden == undefined || grid.columns[i].hidden == false || grid.columns[i].hidden == 'false')) {
                grid.autoFitColumn(i);
            }
        }
    }
}

function onSorting(arg) {
    //console.log("Sorting on field: " + arg.sort.field + ", direction:" + (arg.sort.dir || "none"));
    angular.element($("#ResultsCtrl")).scope().Sorting(arg);

}

function onScrolling(arg) {
    console.log("Selected: " + selected.length + " item(s), [" + selected.join(", ") + "]");
}

function onPaging(arg) {
    console.log("Paging to page index:" + arg.page);
    angular.element($("#ResultsCtrl")).scope().DoPaging(arg.page);
    if ($("#chkThumbGrid").hasClass("ng-not-empty")) {
        $("#chkThumbGrid").click();
    }
}

function onScroll(arg) {
    angular.element($("#ResultsCtrl")).scope().DoEndlessScroll(arg);
}

function onGrouping(arg) {
    console.log("Group on " + kendo.stringify(arg.groups));
}

function onGroupExpand(arg) {
    console.log("The group to be expanded: " + kendo.stringify(arg.group));
}

function onGroupCollapse(arg) {
    console.log("The group to be collapsed: " + kendo.stringify(arg.group));
}

function setPageNumber(page) {
    var grid = $("#Kgrid").data("kendoGrid");
    if (grid != undefined)
        grid.dataSource.page(page + 1);
}

function resizeGrid() {
    var gridElement = $("#Kgrid");
    var newHeight = gridElement.outerHeight(true)

    var otherElements = gridElement.children().not(".k-grid-content");
    var otherElementsHeight = 0;

    otherElements.each(function () {
        otherElementsHeight += $(this).outerHeight();
    });
    var currentHeight = newHeight - 40;
    if (currentHeight < 100)
        currentHeight = 100;
    gridElement.children(".k-grid-content").height(currentHeight);
}

$(window).on("resize", function () {
    if (resizeGrid) resizeGrid();
});

var checkedIds = [];
var DocIdschecked = [];
var DocTypesIdschecked = [];
var checkListIdFormDowloadZip = [];
var acctionsRules_ForResultsGrid = []; //Reglas (botones) obtenidos para grilla de resultados.
var selectedRecords_gridResults = []; //Registros seleccionados de la grilla de resultados. 

function SelectionMultipleIsActive() {
    var result = localStorage.getItem("MultiSelectionIsActive");
    return result;
}

function CleanSelectedRows() {
    checkedIds = [];
    checkListIdFormDowloadZip = [];
    DocIdschecked = [];
    DocTypesIdschecked = [];
    selectedRecords_gridResults = [];
    var grid = $("#Kgrid").getKendoGrid();
    if (grid !== undefined) {
        grid.items().removeClass("k-state-selected");
        $("#Kgrid").find("input:checked").each(function (key, value) {
            value.checked = false;
        })
    }
}

//habilita y deshabilita el registro de la grilla de busqueda.
function onClick(e) {
    var grid = $("#Kgrid").data("kendoGrid");

    var rowIndex = $(e.target).closest("tr")[0].sectionRowIndex;
    var row_STEP_ID;

    if (grid._data[rowIndex].STEP_ID) {
        row_STEP_ID = grid._data[rowIndex].STEP_ID;
    } else if (grid._data[rowIndex].Step_id) {
        row_STEP_ID = grid._data[rowIndex].Step_id;
    }

    var row_Selected = $(e.target).closest("tr").hasClass("k-state-selected");

    var row_DOC_ID = grid._data[rowIndex].DOC_ID;
    var row_DOC_TYPE_ID = grid._data[rowIndex].DOC_TYPE_ID;

    if (SelectionMultipleIsActive() == "true") {
        if (!row_Selected) {

            checkedIds.push(rowIndex);
            DocIdschecked.push(row_DOC_ID);
            DocTypesIdschecked.push(row_DOC_TYPE_ID);
            selectedRecords_gridResults.push(row_STEP_ID);

            angular.element($("#ResultsCtrl")).scope().GetTaskDocument(checkedIds);
            $(e.currentTarget).addClass("k-state-selected");
            $(e.currentTarget).find('[type=checkbox]').prop('checked', true);

            let chekedObject = {
                rowIndex: rowIndex,
                DocTypeid: row_DOC_TYPE_ID,
                Docid: row_DOC_ID,
                stepId: row_STEP_ID
            }

            checkListIdFormDowloadZip.push(chekedObject);

            e.preventDefault();
        } else {
            checkListIdFormDowloadZip = checkListIdFormDowloadZip.filter(element => element.rowIndex !== rowIndex);
            remove_array_element(checkListIdFormDowloadZip, rowIndex);
            remove_array_element(checkedIds, rowIndex);
            remove_array_element(DocIdschecked, row_DOC_ID);
            remove_array_element(DocTypesIdschecked, row_DOC_TYPE_ID);
            remove_array_element(selectedRecords_gridResults, row_STEP_ID);

            angular.element($("#ResultsCtrl")).scope().RemoveAttach(rowIndex);
            $(e.currentTarget).removeClass("k-state-selected");
            $(e.currentTarget).find('[type=checkbox]').prop('checked', false);

            e.preventDefault();
        }

        enableActionsRules_ForResultsGrid();

    } else {
        angular.element($("#ResultsCtrl")).scope().Opentask(rowIndex);
        e.preventDefault();
    }

    showBtns_ForResultsGrid();
};

function enableActionsRules_ForResultsGrid() {
    try {

        var actionRules = JSON.parse(localStorage.getItem("AR|" + GetUID()));

        /// ESTE CODIGO SE GENERO POR BABEL POR QUE EMILIO USO EMSCRIP6 Y NO ES COMPATIBLE CON IE11
        /// VER MAS ADELANTE
        function _toConsumableArray(arr) {
            return (
                _arrayWithoutHoles(arr) || _iterableToArray(arr) || _nonIterableSpread()
            );
        }

        function _nonIterableSpread() {
            throw new TypeError("Invalid attempt to spread non-iterable instance");
        }

        function _iterableToArray(iter) {
            if (
                Symbol.iterator in Object(iter) ||
                Object.prototype.toString.call(iter) === "[object Arguments]"
            )
                return Array.from(iter);
        }

        function _arrayWithoutHoles(arr) {
            if (Array.isArray(arr)) {
                for (var i = 0, arr2 = new Array(arr.length); i < arr.length; i++) {
                    arr2[i] = arr[i];
                }
                return arr2;
            }
        }

        Array.prototype.unique = function (a) {
            return function () { return this.filter(a) }
        }(function (a, b, c) {
            return c.indexOf(a, b + 1) < 0
        });

        var IdsNotIterated = selectedRecords_gridResults.unique();

        //Solo funciona muestra botones de reglas si las tareas selecionadas coinciden con su stepid
        if ($(IdsNotIterated).length > 0 && $(IdsNotIterated).length <= 1) {
            //Oculta Los btns de reglas que se obtuvieron de "getResultsGridActions()"
            hideActionRules_ForResultsGrid(actionRules, true);
            $("[stepid='" + $(IdsNotIterated)[0].toString() + "']").show();
        } else {
            hideActionRules_ForResultsGrid(actionRules, true);
        }


        //habilitacion de derivar si todas son tareas.
        var alltask = true;
        if ($(selectedRecords_gridResults).length > 0) {
            $(selectedRecords_gridResults).each(function (index, item) {
                if (item == 0) {
                    alltask = false;
                }
            });
            if (alltask == true) {
                $("#BtnDerivar").show();
            }
            else {
                $("#BtnDerivar").hide();
            }
        } else {
            $("#BtnDerivar").hide();
        }

    } catch (e) {
        console.log(e);
    }
}

//Oculta botones de reglas por medio de su atribtuo "stepid".
function hideActionRules_ForResultsGrid(List_actionRules, hide) {
    if (hide) {
        List_actionRules.forEach(function (item) {
            $("[stepid='" + item.StepId.toString() + "']").hide()
        })
    } else {
        List_actionRules.forEach(function (item) {
            $("[stepid='" + item.StepId.toString() + "']").show()
        })
    }
}

//Habilita las acciones cuando uno o mas registros setan selecionados, caso contrario, no los mmuestra
function showBtns_ForResultsGrid() {
    try {
        if (checkedIds.length > 0) {
            //     document.getElementById("BtnClearCheckbox").removeAttribute("disabled");
            document.getElementById("BtnSendEmail").removeAttribute("disabled");
            document.getElementById("OpenAllSelected").removeAttribute("disabled");
            document.getElementById("BtnSendZip").removeAttribute("disabled");
            document.getElementById("BtnDerivar").removeAttribute("disabled");
            document.getElementById("panel_ruleActions").removeAttribute("disabled");

            $("#Actions").css('display', 'inline');
        } else {
            //    document.getElementById("BtnClearCheckbox").setAttribute("disabled", "disabled");
            document.getElementById("BtnSendEmail").setAttribute("disabled", "disabled");
            document.getElementById("OpenAllSelected").setAttribute("disabled", "disabled");
            document.getElementById("BtnSendZip").setAttribute("disabled", "disabled");
            document.getElementById("BtnDerivar").setAttribute("disabled", "disabled");
            document.getElementById("panel_ruleActions").setAttribute("disabled", "disabled");

            $("#Actions").css('display', 'none');
        }
    } catch (e) {
        console.log("ERROR: " + e.messages);
    }
}

//Sombrea, selecciona y almacena en una lista, todos los registros de la grilla de resultados.
function pushearALista() {
    var myKGrid = $("#Kgrid");

    var filas = myKGrid.find(".k-checkbox");
    var grid = myKGrid.data("kendoGrid");
    //var CheckBox_SelectAll = filas.splice(0, 1)[0]; //Asigna el registro flitrado (1° checkBox), que es el que selecciona a todos.
    var CheckBox_SelectAll = filas.splice(0, 1)[0].getAttribute("aria-checked"); //Asigna el registro flitrado (1° checkBox), que es el que selecciona a todos.

    var row_Index;
    var row_STEP_ID;
    var id;

    if (CheckBox_SelectAll == "false") {
        checkedIds = [];
        checkListIdFormDowloadZip = [];
        DocIdschecked = [];
        DocTypesIdschecked = [];
        selectedRecords_gridResults = [];

        filas.each(function (index, elemento) {
            id = elemento.id;
            /*row_Index = elemento.closest("tr").sectionRowIndex
             Aclaracion: en IE no esta soportado la funcion closest, por eso se implementa de la siguiente manera,
             se trata de obtner el padre TR de cada fila, para obtener el indice.*/
            row_Index = $("#" + id)[0].parentElement.parentElement.sectionRowIndex;
            row_DOC_ID = grid._data[index].DOC_ID;
            row_DOC_TYPE_ID = grid._data[index].DOC_TYPE_ID;

            if (grid._data[row_Index].STEP_ID) {
                row_STEP_ID = grid._data[row_Index].STEP_ID;
            } else if (grid._data[row_Index].Step_id) {
                row_STEP_ID = grid._data[row_Index].Step_id;
            }

            checkedIds.push(row_Index);
            DocIdschecked.push(row_DOC_ID);
            DocTypesIdschecked.push(row_DOC_TYPE_ID);

            selectedRecords_gridResults.push(row_STEP_ID);

            angular.element($("#ResultsCtrl")).scope().GetTaskDocument(checkedIds);
        });
    } else {
        filas.each(function (index, elemento) {
            id = elemento.id;

            row_Index = $("#" + id)[0].parentElement.parentElement.sectionRowIndex;
            row_DOC_ID = grid._data[index].DOC_ID;
            row_DOC_TYPE_ID = grid._data[index].DOC_TYPE_ID;

            if (grid._data[row_Index].STEP_ID) {
                row_STEP_ID = grid._data[row_Index].STEP_ID;
            } else if (grid._data[row_Index].Step_id) {
                row_STEP_ID = grid._data[row_Index].Step_id;
            }

            remove_array_element(checkedIds, row_Index);
            remove_array_element(DocIdschecked, row_DOC_ID);
            remove_array_element(DocTypesIdschecked, row_DOC_TYPE_ID);
            remove_array_element(selectedRecords_gridResults, row_STEP_ID);

            angular.element($("#ResultsCtrl")).scope().RemoveAttach(row_Index);
        });
    }

    enableActionsRules_ForResultsGrid();
    showBtns_ForResultsGrid();
}

function OpenSelectedRows() {
    for (var i = 0; i < checkedIds.length; i++) {
        angular.element($("#ResultsCtrl")).scope().Opentask(checkedIds[i]);
    }
    //checkedIds = [];
}

function checkValue(value, arr, from) {
    for (var i = 0; i < arr.length; i++) {
        var name = arr[i];

        if (from == "attach") {
            if (name.Docid == value) {
                return true;
            }
        }
        else {
            if (name == value) {
                return true;
            }
        }
    }
    return false;
}

function remove_array_element(array, n) {
    var index = array.indexOf(n);
    if (index > -1) {
        array.splice(index, 1);
    }
    return array;
}

//$(document).ready(function () {
//    $('.k-checkbox').each(function (i) {
//        console.log($(this).attr("aria-checked"));

//    });
//});


//////Para usar a futuro///////////////////////////////////////////////////////////////
//toolbar: ["excel", "pdf"],
//    excel: {
//    fileName: "Lista de Resultados.xlsx",
//        proxyURL: "https://demos.telerik.com/kendo-ui/service/export",
//            filterable: true
//},
//pdf: {
//    allPages: true,
//        avoidLinks: true,
//            paperSize: "A4",
//                margin: { top: "2cm", left: "1cm", right: "1cm", bottom: "1cm" },
//    landscape: true,
//        repeatHeaders: true,
//            template: $("#page-template").html(),
//                scale: 0.8
//},

//-----------------------------------------------------------------Associated Grids ----------------------------------------//

function getColumnIndex(columns, column) {
    for (var i = 1; i < columns.length; i++) {
        if (columns[i].field == column) {
            return i;
        }
    }
}

function FixAssociatedKendoGrid(grid) {


    //var actorID = getColumnIndex(grid.columns, "Actor"),
    //    DemandadoID = getColumnIndex(grid.columns, "Demandado"),
    //    Juicio_o_MediacionID = getColumnIndex(grid.columns, "Juicio_o_Mediacion"),
    //    Nro_Juicio_MediacionID = getColumnIndex(grid.columns, "Nro_Juicio_Mediacion"),
    //    RamoID = getColumnIndex(grid.columns, "Ramo"),
    //    PolizaID = getColumnIndex(grid.columns, "Nro_de_Poliza"),
    //    Nro_de_SiniestroID = getColumnIndex(grid.columns, "Nro_de_Siniestro"),
    //    tipo_notificacionID = getColumnIndex(grid.columns, "Tipo_de_Notificación"),
    //    IngresoColumnID = getColumnIndex(grid.columns, "INGRESO"),
    //    EtapaColumnID = getColumnIndex(grid.columns, "Etapa"),
    //    EstadoColumnID = getColumnIndex(grid.columns, "Estado"),
    //    AsignadoColumnID = getColumnIndex(grid.columns, "Asignado"),
    //    EntidadID = getColumnIndex(grid.columns, "ENTIDAD"),
    //    TareaColumnID = getColumnIndex(grid.columns, "Tarea");


    //var actor = grid.columns[actorID],
    //    Demandado = grid.columns[DemandadoID],
    //    Juicio_o_Mediacion = grid.columns[Juicio_o_MediacionID],
    //    Nro_Juicio_Mediacion = grid.columns[Nro_Juicio_MediacionID],
    //    Ramo = grid.columns[RamoID],
    //    Poliza = grid.columns[PolizaID],
    //    Nro_de_Siniestro = grid.columns[Nro_de_SiniestroID],
    //    IngresoColumn = grid.columns[IngresoColumnID],
    //    EtapaColumn = grid.columns[EtapaColumnID],
    //    EstadoColumn = grid.columns[EstadoColumnID],
    //    AsignadoColumn = grid.columns[AsignadoColumnID],
    //    Entidad = grid.columns[EntidadID],
    //    TareaColumn = grid.columns[TareaColumnID],
    //    tipo_notificacion = grid.columns[tipo_notificacionID];

    //var columnsCount = grid.columns.length;


    //if (actor != undefined)
    //    grid.reorderColumn(columnsCount - 10, actor);
    //if (Demandado != undefined)
    //    grid.reorderColumn(columnsCount - 9, Demandado);
    //if (Juicio_o_Mediacion != undefined)
    //    grid.reorderColumn(columnsCount - 8, Juicio_o_Mediacion);
    //if (Nro_Juicio_Mediacion != undefined)
    //    grid.reorderColumn(columnsCount - 7, Nro_Juicio_Mediacion);
    //if (Ramo != undefined)
    //    grid.reorderColumn(columnsCount - 6, Ramo);
    //if (Poliza != undefined)
    //    grid.reorderColumn(columnsCount - 5, Poliza);
    //if (Nro_de_Siniestro != undefined)
    //    grid.reorderColumn(columnsCount - 4, Nro_de_Siniestro);


    //if (EtapaColumn != undefined)
    //    grid.reorderColumn(columnsCount - 11, EtapaColumn);
    //if (AsignadoColumn != undefined)
    //    grid.reorderColumn(columnsCount - 3, AsignadoColumn);
    //if (TareaColumn != undefined)
    //    grid.reorderColumn(columnsCount - 2, TareaColumn);
    //if (Entidad != undefined)
    //    grid.reorderColumn(columnsCount - 1, Entidad);



    //for (var i = 0; i < grid.columns.length; i++) {
    //   grid.autoFitColumn(i);
    //}
}

function GetColumnWidth(ColumnName) {

    var width = 150;

    try {
        if (ColumnName == 'ENTIDAD') return 0;
        if (ColumnName == 'ID_Pedido_de_Fondos') return 35;
        if (ColumnName == 'Asignado') return 80;
        if (ColumnName == 'Juicio_o_Mediacion') return 40;
        if (ColumnName == 'Nro_Jucio_Mediacion') return 50;
        if (ColumnName == 'Nro_de_Siniestro') return 50;
        if (ColumnName == 'Jurisdiccion') return 80;
        if (ColumnName == 'Estudio') return 80;
        if (ColumnName == 'STEP_ID') return 0;
        if (ColumnName == 'LEIDO') return 0;
        if (ColumnName == 'Nro_de_Solicitud') return 50;
        if (ColumnName == 'Fecha_Solicitada') return 50;
        if (ColumnName == 'Fecha_de_Solicitud') return 50;
        if (ColumnName == 'Fecha_de_Liquidacion') return 50;
        if (ColumnName == 'Etapa') return 80;
        if (ColumnName == 'Actor') return 100;
        if (ColumnName == 'Demandado') return 100;
        if (ColumnName == 'Icon') return 35;
    } catch (e) {
        console.log(e);
    }

    return width;

}

function onDataBoundAssociated(arg) {

    // iterate the data items and apply row styles where necessary
    //var dataItems = arg.sender.dataSource.view();
    //for (var i = 0; i < dataItems.length; i++) {
    //    var row = arg.sender.tbody.find("[data-uid='" + dataItems[i].uid + "']");
    //     if (dataItems[i]["LEIDO"] == 0 ) {
    //        row.addClass("notRead");
    //    }
    //    if (dataItems[i]["LEIDO"] == 1) {
    //      }
    //}

    //for (var i = 0; i < this.columns.length; i++) {
    //    this.autoFitColumn(i);
    //}

}

function viewTask(a) {
    var docid = getElementFromQueryStringUrl("docid", a);
    var doctypeid = getElementFromQueryStringUrl("doctypeid", a);
    var userId = getElementFromQueryStringUrl("userId", a);
    var UserLength = userId.split(";")[0];
    //var url = ZambaWebRestApiURL + "/search/NotifyDocumentRead?" + jQuery.param({ UserId: UserLength, DocTypeId: doctypeid, DocId: docid });
    $.ajax({
        type: "POST",
        url: ZambaWebRestApiURL + '/search/NotifyDocumentRead?' + jQuery.param({ UserId: UserLength, DocTypeId: doctypeid, DocId: docid }),
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (r) {
        },
    });
}

function getElementFromQueryStringUrl(element, url) {
    url = url + '';
    var segments = url.split("&");
    var value = null;
    segments.forEach(function (valor) {
        if (valor.includes(element)) { value = valor.split("=")[1]; }
    });
    return value;
}