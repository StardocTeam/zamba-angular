var localSearchResults;
var _searchUserId;
var isDateField = [];
var isDateTimeField = [];
var columnsString = null;
var TmrZambaSave = null;

function KendoGrid(searchResults, searchUserId, search) {
    try {
        if ($('#Kgrid').data().kendoGrid != undefined) {
            CleanKGrid();
        }
    } catch (e) {
        console.error(e);
    }

    _searchUserId = searchUserId;
    localSearchResults = searchResults;
    var model = generateModel(localSearchResults);

    var columns = null;
    try {
        var columnsStringMyTasks = localSearchResults.columnsStringMyTasks;

        var columnsStringTeam = localSearchResults.columnsStringTeam;
        var columnsStringAll = localSearchResults.columnsStringAll;

        //'[{"selectable":true,"width":65,"filterable":false,"resizable":true},{"field":"Icon","format":"","width":35,"template":"<div class=customer-photo style=background-image: url(../../content /#:Icon#); ></div>","sortable":false,"filterable":false,"resizable":true},{"field":"Tipo_Doc_Notificacion","format":"","width":150,"filterable":true,"resizable":true},{"field":"Fecha_y_Hora_de_Recepcion","format":"","width":150,"filterable":true,"resizable":true},{"field":"Fecha_de_Notificacion","format":"","width":150,"filterable":true,"resizable":true},{"field":"Fecha_de_Vencimiento_Audiencia","format":"","width":150,"filterable":true,"resizable":true},{"field":"Actor","format":"","width":100,"filterable":true,"resizable":true},{"field":"Demandado","format":"","width":100,"filterable":true,"resizable":true},{"field":"Nro_de_Etiqueta","format":"","width":150,"filterable":true,"resizable":true},{"field":"J_o_M","format":"","width":150,"filterable":true,"resizable":true},{"field":"Nro_Juicio_Mediacion","format":"","width":150,"filterable":true,"resizable":true},{"field":"Poliza","format":"","width":150,"filterable":true,"resizable":true},{"field":"Nro_de_Siniestro","format":"","width":50,"filterable":true,"resizable":true},{"field":"Asignado","format":"","width":80,"filterable":true,"resizable":true},{"field":"Etapa","format":"","width":80,"filterable":true,"resizable":true},{"field":"ENTIDAD","format":"","width":0,"filterable":true,"resizable":true},{"field":"LEIDO","format":"","hidden":true,"filterable":false,"width":0,"resizable":true},{"field":"DOC_ID","format":"","hidden":true,"filterable":false,"width":150,"resizable":true},{"field":"DOC_TYPE_ID","format":"","hidden":true,"filterable":false,"width":150,"resizable":true},{"field":"Tarea","format":"","width":150,"filterable":true,"resizable":true},{"field":"INGRESO","format":"{0:dd/MM/yyyy}","width":150,"filterable":true,"resizable":true},{"field":"TASK_ID","format":"","hidden":true,"filterable":false,"width":150,"resizable":true},{"field":"STEP_ID","format":"","hidden":true,"filterable":false,"width":0,"resizable":true},{"field":"Sdo_Vto_2da_Aud","format":"","width":150,"filterable":true,"resizable":true},{"field":"Estado_de_Notificacion","format":"","width":150,"filterable":true,"resizable":true},{"field":"Estudio","format":"","width":80,"filterable":true,"resizable":true},{"field":"Estado_Procesal","format":"","width":150,"filterable":true,"resizable":true},{"field":"Estudio_Negociador","format":"","width":150,"filterable":true,"resizable":true},{"field":"Abogado_Actor","format":"","width":150,"filterable":true,"resizable":true},{"field":"Embargo","format":"","width":150,"filterable":true,"resizable":true}]';

        columnsString = columnsStringMyTasks;

        try {
            if (search.View != undefined) {
                if (search.View.indexOf('MyTasks') != -1) {
                    columnsString = columnsStringMyTasks;
                }
                if (search.View.indexOf('MyTeam') != -1) {
                    columnsString = columnsStringTeam;
                }
                if (search.View.indexOf('MyAllTeam') != -1) {
                    columnsString = columnsStringTeam;
                }
                if (search.View.indexOf('ViewAllMy') != -1) {
                    columnsString = columnsStringAll;
                }
                if (search.View.indexOf('reportid') != -1) {
                    columnsString = '';
                }
            }
        } catch (e) {
            console.error(e);
        }
        if (columnsString != undefined && columnsString != null && columnsString.trim() != '') {
            columns = JSON.parse(columnsString.trim());
        }
        else {
            columns = generateColumns(localSearchResults);
        }
    } catch (e) {
        console.error(e);
        columns = generateColumns(localSearchResults);

    }

    columns = validateColumns(localSearchResults, columns);
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
        columnResize: adjustLastColumn,
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
    hideLoading();
}

function adjustLastColumn() {
    var grid = $("#Kgrid").data("kendoGrid");
    var contentDiv = grid.wrapper.children(".k-grid-content");
    var masterHeaderTable = grid.thead.parent();
    var masterBodyTable = contentDiv.children("table");
    var gridDivWidth = contentDiv.width() - kendo.support.scrollbar();

    masterHeaderTable.width("");
    masterBodyTable.width("");

    var headerWidth = getMasterColumnsWidth(masterHeaderTable),
        lastHeaderColElement = grid.thead.parent().find("col").last(),
        lastDataColElement = grid.tbody.parent().children("colgroup").find("col").last(),
        delta = parseInt(gridDivWidth, 10) - parseInt(headerWidth, 10);


    if (delta > 0) {
        delta = Math.abs(delta);
        lastHeaderColElement.width(delta);
        lastDataColElement.width(delta);
    } else {
        lastHeaderColElement.width(0);
        lastDataColElement.width(0);
    }

    contentDiv.scrollLeft(contentDiv.scrollLeft() - 1);
    contentDiv.scrollLeft(contentDiv.scrollLeft() + 1);
}
function getMasterColumnsWidth(tbl) {
    var result = 0;
    tbl.children("colgroup").find("col").not(":last").each(function (idx, element) {
        result += parseInt($(element).outerWidth() || 0, 10);
    });

    return result;
}

function setEventsGrid(grid) {
    try {

        grid.tbody.on("click", "tr", onClick);
        grid.tbody.on("dblclick", "tr", Val_EventDblClick);


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

        grid.dataSource.total = getResultsTotal;
        if (grid._data.length == 0) {
            $("#LblSwitchControl").hide();
        } else {
            $("#LblSwitchControl").show();
        }

        let checkBox_SelectAll = $("#Kgrid").find(".k-checkbox")[0];
        if (checkBox_SelectAll !== undefined && checkBox_SelectAll !== null)
            checkBox_SelectAll.setAttribute("onclick", "pushearALista()");
    } catch (e) {
        console.error(e);
    }
}


function columnaSinCaracteresEspeciales(columna) {
    try {
        // for (i = 0; i < columna.length; i++) {
        columna = columna
            .replaceAll(" ", "_").replaceAll("-", "_").replaceAll("%", "_").replaceAll("/", "_")
            .replaceAll("._", "_").replaceAll("*", "_").replaceAll(".", "_").replaceAll("?", "_").replaceAll("¿", "_")
            .replaceAll("+", "_").replaceAll("/", "_").replaceAll("&", "_").replaceAll("-", "_").replaceAll("\\", "_")
            .replaceAll("%", "_").replaceAll(")", "_").replaceAll("(", "_").replaceAll("#", "_")
            .replaceAll("+", "_").replaceAll("°", "_").replaceAll("__", "_")
            .replaceAll("á", "a").replaceAll("é", "e").replaceAll("í", "i").replaceAll("ó", "o").replaceAll("ú", "u");
        //  }
        return columna;
    } catch (e) {
        console.error("ocurrio un problema convertir columna en caracteres especiales");
    }

}

function getTypeColumn(property, sampleDataItem, response) {
    var typeColumn;
    property = property.trim().replace(/\s/g, "").replace(/\s/g, "_");
    for (var j = 0; j < response.filterIndexs.length; j++) {
        if (columnaSinCaracteresEspeciales(response.filterIndexs[j].Name.toLowerCase()) == property.toLowerCase()) {
            if (response.filterIndexs[j].Type == 4) {
                typeColumn = {
                    type: "date",
                    validation: {
                        required: true
                    }
                };
                isDateTimeField[property] = true;
                return typeColumn;
            }
            if (response.filterIndexs[j].Type == 5) {
                typeColumn = {
                    type: "date",
                    validation: {
                        required: true
                    }

                };
                isDateField[property] = true;
                return typeColumn;
            };
        }
    }


    if (property == ("ID")) {
        typeColumn = property;
    }
    if (sampleDataItem[property] == undefined || sampleDataItem[property] == "" || sampleDataItem[property] == null)
        return undefined;

    var propType = typeof sampleDataItem[property];

    if (propType === "number") {
        typeColumn = {
            type: "number"
        };
    } else if (propType === "boolean") {
        typeColumn = {
            type: "boolean"
        };
    } else if (propType === "string") {
        var parsedDate = kendo.parseDate(sampleDataItem[property]);
        if (parsedDate) {

            if (parsedDate.getHours == 0 && parsedDate.getMinutes == 0 && parsedDate.getSeconds == 0) {
                typeColumn = {
                    type: "date"
                };
                isDateField[property] = true;
            } else {
                typeColumn = {
                    type: "date"
                };
                isDateTimeField[property] = true;
            }
        } else {
            typeColumn = {
                type: "string"
            };
        }
    } else if (propType === "object") {

        if (property.indexOf("Fecha") != -1 && property.indexOf("Hora") != -1) {
            typeColumn = {
                type: "date"
            };

        } else {

        }

    } else {
        typeColumn = {
            type: "string"
        };
    }
    return typeColumn;
}
function generateModel(response) {
    var totalColumns = response.columns.length;
    var model = {};
    var fields = {};
    var columnsCheck = [];
    var counterColumnsCheck = 0;
    var EndProcess = false;
    var typeColumn
    isDateField = [];
    isDateTimeField = [];

    for (var i = 0; i < response.data.length; i++) {
        sampleDataItem = Object.entries(response.data[i]);
        var filteredItems = sampleDataItem.filter(item => item[1] != null)
        var filteredItemsToObject = Object.fromEntries(filteredItems);
        for (var property in filteredItemsToObject) {
            if (!columnsCheck[property]) {
                if (property == ("ID")) {
                    model["id"] = property;
                }
                typeColumn = getTypeColumn(property, response.data[i], response);
                if (columnsCheck[property] != true)
                    if (typeColumn == undefined) {
                        fields[property] = {
                            type: "string"
                        };
                    }
                    else {
                        fields[property] = typeColumn;
                    }
                if (columnsCheck[property] != true && typeColumn != undefined) {
                    fields[property] = typeColumn;
                    columnsCheck[property] = true;
                    counterColumnsCheck++;
                }
                if (counterColumnsCheck == totalColumns) {
                    EndProcess = true;
                    break;
                }
            }
        }
        if (EndProcess)
            break;
    }
    model.fields = fields;
    return model;
}

function validateColumns(results, columns) {
    var newColumns = [];
    var columnNames = results["columns"];

    columns.map(function (col) {

        if (col.field == undefined || col.field == "IsChecked" || col.field == "Icon" || col.field == "DOC_ID" || col.field == "DOC_TYPE_ID" || col.field == "STR_ENTIDAD" || col.field == "THUMB" || col.field == "FULLPATH" || col.field == "ICON_ID" || col.field == "USER_ASIGNED" || col.field == "EXECUTION" || col.field == "DO_STATE_ID" || col.field == "TASK_ID" || col.field == "Task_Id" || col.field == "RN" || col.field == "LEIDO" || col.field == "Leido" || col.field == "WORKFLOW" || col.field == "Proceso" || col.field == "Estado" || col.field == "STEP_ID" || col.field == "ENTIDAD" || col.field == "Task_ID" || col.field == "Doc_ID" || col.field == "Step_ID" || col.field == "ORIGINAL") {
            newColumns.push(col);
        }
        else {

            for (i = 0; i < columnNames.length; i++) {
                if (col.field == columnNames[i])
                    newColumns.push(col);
            }
        }
    })

    return newColumns;
}

function generateColumns(response) {
    var columnNames = response["columns"];

    var indexOfIcon = columnNames.indexOf("Icon");
    if (indexOfIcon > -1) {
        columnNames.splice(indexOfIcon, 1);
    }

    var indexOfcheck = columnNames.indexOf("IsChecked");
    if (indexOfcheck > -1) {
        columnNames.splice(indexOfcheck, 1);
    }

    if (JSON.stringify(columnNames).indexOf("Icon") == -1 && JSON.stringify(columnNames).indexOf("ICON_ID") > -1) {
        columnNames.unshift("Icon");
    }
    if (JSON.stringify(columnNames).indexOf("IsChecked") == -1 && JSON.stringify(columnNames).indexOf("ICON_ID") > -1) {
        columnNames.unshift("IsChecked");
    }

    return columnNames.map(function (name) {


        if (name == "IsChecked") {
            return {
                selectable: true, width: 65, filterable: false, resizable: true, hidden: undefined
            };
        } else if (name == "Icon") {
            return {
                field: name, format: (isDateField[name] ? "{0:dd/MM/yyyy}" : ""), width: 35, template: "<div class='customer-photo'" +
                    "style='background-image: url(" + "../../content/#:Icon#);'></div>",
                sortable: false, filterable: false, resizable: true, hidden: undefined
            };
        } else if (name == "Importe") {
            return {
                field: name, format: (isDateField[name] ? "{0:dd/MM/yyyy}" : ""), width: GetColumnWidth(name), filterable: false, resizable: true, title: newname, hidden: undefined
            };
        } else if (name == "Asignado" || name == "Etapa") {
            return {
                field: name, hidden: undefined, filterable: false, width: 150, resizable: true
            };


        } else if (name == "DOC_ID" || name == "DOC_TYPE_ID" || name == "STR_ENTIDAD" || name == "THUMB" || name == "FULLPATH" || name == "ICON_ID" || name == "User_Asigned" || name == "EXECUTION" || name == "DO_STATE_ID" || name == "TASK_ID"
            || name == "Task_Id" || name == "RN" || name == "LEIDO" || name == "Leido" || name == "WORKFLOW" || name == "Proceso" || name == "STEP_ID" || name == "ENTIDAD" || name == "Task_ID" || name == "Doc_ID" || name == "Step_ID") {
            return {
                field: name, format: (isDateField[name] ? "{0:dd/MM/yyyy}" : ""), hidden: true, filterable: false, width: 0, resizable: true
            }
        } else if (name == "ORIGINAL") {
            return {
                field: name, format: (isDateField[name] ? "{0:dd/MM/yyyy}" : ""), width: GetColumnWidth(name), filterable: false, sortable: false, resizable: true, hidden: undefined
            }
        } else {
            //¿,?,/,&,°,+,-,.,),(,:
            ///\-+-()$%#\ ? - 
            var newname = name;
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
            var formatField = "";
            if (isDateField[name]) {
                formatField = "{0:dd/MM/yyyy}";
            }
            if (isDateTimeField[name]) {
                formatField = "{0:dd/MM/yyyy HH:mm}";
            }
            var ret = {
                field: name,
                format: formatField,
                width: GetColumnWidth(name),
                filterable: false,
                resizable: true,
                hidden: undefined,
                title: name,
                headerAttributes: { title: newname }
            }
            return ret;
        }
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
    if (window.localStorage && window.localStorage.getItem("MultiSelectionIsActive") === "false") {
        $(".k-checkbox-label").parent().hide();
    }
    $('#Kgrid').css('font-size', '11px');
    if ($('.k-header.k-with-icon')[0] != undefined)
        $('.k-header.k-with-icon')[0].childNodes[0].hidden = true;
    $(".k-header-column-menu").kendoTooltip({ content: "Configuración de columna" });

    // iterate the data items and apply row styles where necessary
    var dataItems = arg.sender.dataSource.view();
    for (var i = 0; i < dataItems.length; i++) {
        var row = arg.sender.tbody.find("[data-uid='" + dataItems[i].uid + "']");
        if (dataItems[i].ShowUnread == "false") {
            dataItems[i].ShowUnread = false;
        }
        if (dataItems[i].ShowUnread == "true") {
            dataItems[i].ShowUnread = true;
        }
        try {
            if (dataItems[i].ShowUnread) {
                row.addClass("notRead");
            }
        } catch (e) {
            console.error(e);
        }

        try {
            if (dataItems[i].Contestaciones != undefined && dataItems[i].Contestaciones != null && dataItems[i].Contestaciones == 0) {
                row.addClass("RowPending");
            }
        } catch (e) {
            console.error(e);
        }
        try {
            if (dataItems[i].Delegaciones != undefined && dataItems[i].Delegaciones != null && dataItems[i].Delegaciones.indexOf('CAC') != -1) {
                row.addClass("RowPending");
            }
        } catch (e) {
            console.error(e);
        }
        try {
            let userGroupForPending = "71,11542205,12598,11542206,1526096,12872,1020364,1020363,12871"; if (dataItems[i].Tipo_de_pagos != undefined && dataItems[i].Tipo_de_pagos != null && dataItems[i].Tipo_de_pagos.indexOf('CENTROS DE ATENCION') != -1 && IfUserInGroups(userGroupForPending)) {
                row.addClass("RowPending");
            }
        } catch (e) {
            console.error(e);
        }

        try {
            if (dataItems[i].Importe != undefined && dataItems[i].Importe != null && parseFloat(dataItems[i].Importe) >= 0) {
                var TR_Row = $("tr[role='row']");
                var TH_Importe = TR_Row.find("th[data-field='Importe']");
                var CellImporte = row[0].cells[TH_Importe.attr("data-index")];
                $(CellImporte).addClass("text-rigth");
            }
        } catch (e) {
            console.error(e);
        }

        try {
            if (dataItems[i].Informes != undefined && dataItems[i].Informes != null && dataItems[i].Informes == 0) {
                row.addClass("RowPending");
            }
        } catch (e) {
            console.error(e);
        }

        try {
            if (dataItems[i].Contestaciones != undefined && dataItems[i].Contestaciones != null && dataItems[i].Contestaciones == 0) {
                row.addClass("RowPending");
            }
        } catch (e) {
            console.error(e);
        }
        if (i == 0) SetDisplayColumnNameKGrid();
        adjustLastColumn();
    }


}
function SetDisplayColumnNameKGrid() {
    var KgridColumnsHeaders = $(".k-link");
    localSearchResults.MappingColumnToDisplay.forEach(function (MappingColumnDisplayName) {
        for (var i = 0; i < KgridColumnsHeaders.length; i++) {
            if (KgridColumnsHeaders[i].innerText == MappingColumnDisplayName.ColumnName) {
                KgridColumnsHeaders[i].innerText = MappingColumnDisplayName.DisplayName
                break;
            }
        }
    });
}
function RefreshKGrid(searchResults) {
    try {

        localSearchResults = searchResults;
        //var grid = $('#Kgrid');
        var grid = $('#Kgrid').data('kendoGrid');
        //grid.dataSource.total = getResultsTotal;
        grid.dataSource.read();
        grid.refresh();
    } catch (e) {
        console.error(e);
    }
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
        checkListIdFormDowloadZip = [];
        selectedRecords_gridResults = [];

        //TO DO: Variables docids y doctypesids, para manejo de acciones o envio de mail multiples 
        //DocIdschecked = [];
        //DocTypesIdschecked = [];

        for (i = 0; i < arg.sender._data.length - 1; i++) {
            checkedIds.push(arg.sender._data[i].RN);
        }

        angular.element($("#ResultsCtrl")).scope().GetTaskDocument(checkedIds);
        return item.className;

    });
    if (selected.length > 0) {
        if (document.getElementById("BtnSendEmail") != undefined)
            $("#BtnSendEmail").css('display', 'block');
        $("#OpenAllSelected").css('display', 'block');
        if (document.getElementById("BtnSendZip") != undefined)
            $("#BtnSendZip").css('display', 'block');
        if (document.getElementById("BtnDownloadZip") != undefined)
            $("#BtnDownloadZip").css('display', 'block');
        $("#Actions").css('display', 'block');

        //se fija si tiene workflows, si no tiene workflows como en el caso de RPI no muestra esos dos botones
        var scope_TreeViewController = angular.element($("#SidebarTree")).scope();
        if (scope_TreeViewController != undefined) {
            if (scope_TreeViewController.ChildsEntities.length > 0) {
                $("#OpenAllSelected").css("display", "none");
                $("#BtnDerivar").css("display", "none");
            }
        }
    } else {
        for (j = 0; j < arg.sender._data.length - 1; j++) {
            angular.element($("#ResultsCtrl")).scope().RemoveAttach(j);

        }

        checkedIds = [];
        checkListIdFormDowloadZip = [];
        selectedRecords_gridResults = [];
        if (document.getElementById("BtnSendEmail") != undefined)
            document.getElementById("BtnSendEmail").setAttribute("disabled", "disabled");

        document.getElementById("OpenAllSelected").setAttribute("disabled", "disabled");

        if (document.getElementById("BtnSendZip") != undefined)
            document.getElementById("BtnSendZip").setAttribute("disabled", "disabled");

        if (document.getElementById("BtnDownloadZip") != undefined)
            document.getElementById("BtnDownloadZip").setAttribute("disabled", "disabled");

        $("#Actions").css('display', 'none');
    }
}

function onFiltering(arg) {
    //var grid = $('#Kgrid').data("kendoGrid");
    //window.localStorage["kendo-grid-options"] = kendo.stringify(grid.getOptions());
    angular.element($("#ResultsCtrl")).scope().Filter(arg);
}

function countTaskIdSelected() {
    return checkListIdFormDowloadZip;
}

function AdjustGridColumns() {

    setTimeout(function () {
        try {

            if ($('#Kgrid').length && $('#Kgrid').data("kendoGrid") !== undefined) {

                var grid = $('#Kgrid').data("kendoGrid");

                var _length = (columnsString != undefined && columnsString != null && columnsString != '') ? 15 : 10;
                if (_length > grid.columns.length)
                    _length = grid.columns.length;


                //for (var i = 1; i < _length; i++) {
                //    if (grid.columns[i] != undefined && (grid.columns[i].hidden == undefined || grid.columns[i].hidden == false || grid.columns[i].hidden == 'false')) {
                //        console.log('AdjustGridColumns : ', i)
                //        grid.autoFitColumn(i);        
                //    }
                //}
                var columns = grid.columns

                SetWidthColumnMinMax(grid, columns, "Tarea", 100, 200);
                SetWidthColumnMinMax(grid, columns, "Etapa", 60, 150);
                SetWidthColumnMinMax(grid, columns, "Asignado", 80, 150);
                SetWidthColumnMinMax(grid, columns, "CREADO", 80, 150);
                grid.setOptions({ columns: columns });
                setEventsGrid(grid);

            }
            var mainController = angular.element($("#EntitiesCtrl")).scope();
            mainController.$emit('kendoGridReady');
        } catch (e) {
            console.error(e);
        }

    }, 0);
}
function SetWidthColumnMinMax(grid, columns, columnName, minWidth, maxWidth) {

    var indexTaskColumnA = -1
    var indexTaskColumnB = -1
    for (var i = 0; i < columns.length - 1; i++) {
        if (columns[i].field != undefined)
            if (columns[i].hidden == undefined)
                indexTaskColumnA++;
        if (columns[i].field == columnName) {

            if (columnName == 'Tarea')
                grid.autoFitColumn(i - 1);
            indexTaskColumnB = i;

            if (columns[i].width < minWidth) {
                columns[i].width = minWidth;
            }
            if (columns[i].width >= maxWidth) {
                columns[i].width = maxWidth;
            }
            break;
        }
    }

    //if (columns[indexTaskColumnB].width < minWidth) {
    //    columns[indexTaskColumnA].width = minWidth;
    //}
    //if (columns[indexTaskColumnB].width >= maxWidth) {
    //    columns[indexTaskColumnA].width = maxWidth;
    //}

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
    var result = window.localStorage.getItem("MultiSelectionIsActive");
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

//function ViewTask() {
//    var isTaskViewer = false;
//    const TypeRightUse = 19;
//    if (stepid != null && stepid != undefined && stepid != 0) {
//        if (validateUserRight(stepid, TypeRightUse)) {
//            isTaskViewer = true;
//        }
//    }
//    if (isTaskViewer) {

//        var Url = (thisDomain + "/views/WF/TaskViewer.aspx?DocType=" + result.DOC_TYPE_ID + "&docid=" + result.DOC_ID + "&taskid=" + taskId + "&mode=s"
//            + "&s=" + stepid + "&user=" + userid + "&t=" + token);
//    }
//    else {
//        var Url = (thisDomain + "/views/search/docviewer.aspx?DocType=" + result.DOC_TYPE_ID + "&docid=" + result.DOC_ID + "&mode=s"
//            + "&user=" + userid + "&t=" + token);
//    }

//    document.getElementById('IFPreview').setAttribute('src', Url);
//}

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

    var multipleSelectActive = SelectionMultipleIsActive();
    var row_Selected = $(e.target).closest("tr").hasClass("k-state-selected");

    var row_DOC_ID = grid._data[rowIndex].DOC_ID;
    var row_DOC_TYPE_ID = grid._data[rowIndex].DOC_TYPE_ID;

    if (multipleSelectActive == "true" && row_Selected == false) {
        angular.element($("#ResultsCtrl")).scope().GetTaskDocument(checkedIds);
        $(e.currentTarget).find('[type=checkbox]').prop('checked', true);

        $(e.currentTarget).addClass("k-state-selected");

        checkedIds.push(rowIndex);
        DocIdschecked.push(row_DOC_ID);
        DocTypesIdschecked.push(row_DOC_TYPE_ID);
        selectedRecords_gridResults.push(row_STEP_ID);


        let chekedObject = {
            rowIndex: rowIndex,
            DocTypeid: row_DOC_TYPE_ID,
            Docid: row_DOC_ID,
            stepId: row_STEP_ID
        }

        checkListIdFormDowloadZip.push(chekedObject);
    }

    if (multipleSelectActive == "true" && row_Selected == true) {
        checkListIdFormDowloadZip = checkListIdFormDowloadZip.filter(element => element.rowIndex !== rowIndex);
        remove_array_element(checkListIdFormDowloadZip, rowIndex);
        remove_array_element(checkedIds, rowIndex);
        remove_array_element(DocIdschecked, row_DOC_ID);
        remove_array_element(DocTypesIdschecked, row_DOC_TYPE_ID);
        remove_array_element(selectedRecords_gridResults, row_STEP_ID);

        angular.element($("#ResultsCtrl")).scope().RemoveAttach(rowIndex);
        $(e.currentTarget).find('[type=checkbox]').prop('checked', false);
        $(e.currentTarget).removeClass("k-state-selected");
    }

    if (multipleSelectActive == "false") {

        var before_Row_Selected = $(".k-state-selected");
        var before_Row_Step_id;
        if (before_Row_Selected.length == 1) {

            if (grid._data[before_Row_Selected[0].rowIndex].STEP_ID) {
                before_Row_Step_id = grid._data[before_Row_Selected[0].rowIndex].STEP_ID;
            } else if (grid._data[before_Row_Selected[0]].Step_id) {
                before_Row_Step_id = grid._data[before_Row_Selected[0].rowIndex].Step_id;
            }


            remove_array_element(selectedRecords_gridResults, before_Row_Step_id);
            angular.element($("#ResultsCtrl")).scope().RemoveAttach(before_Row_Selected[0].sectionRowIndex);
            $(before_Row_Selected[0]).removeClass("k-state-selected");
        }


        $(e.currentTarget).find('[type=checkbox]').prop('checked', true);
        $(e.currentTarget).addClass("k-state-selected");
    }


    if (SelectionMultipleIsActive() == "true") {
        enableBtnRules_ForResultsGrid();
        ShowActions(grid._data);
        e.preventDefault();
        angular.element($("#taskController")).scope().$apply();
        showBtns_ForResultsGrid();
    }

};

function Val_EventDblClick(e) {
    var ScopeResultsCtrl = angular.element($("#ResultsCtrl")).scope();

    //TODO: Refactorizar y separar la validacion por swal con la ejecucion del codigo.
    checkSaveChanges(ScopeResultsCtrl, e);

    e.preventDefault();
    angular.element($("#taskController")).scope().$applyAsync();
    showBtns_ForResultsGrid();
};

function OpenTaskFromKendoGrid(ScopeResultsCtrl, e) {
    var rowIndex = $(e.target).closest("tr")[0].sectionRowIndex;

    var ScopeResultsCtrl = angular.element($("#ResultsCtrl")).scope();
    if (document.getElementById("chkThumbGrid").checked == false) {
        if (ScopeResultsCtrl.PreviewMode == "noPreview") {
            ScopeResultsCtrl.Opentask(rowIndex);
        } else {
            ScopeResultsCtrl.OpenTaskInPreview(rowIndex);
        }
    }
}

function checkSaveChanges(ScopeResultsCtrl, e) {
    if (ScopeResultsCtrl.PreviewMode != "noPreview") {
        var PreviewTaskChanged = localStorage.getItem("PreviewTaskChanged");
        if (PreviewTaskChanged == "true") {
            localStorage.removeItem("PreviewTaskChanged");
            swal({
                title: "Hay modificaciones en la tarea actual.",
                text: "Desea guardar los cambios realizados?",
                icon: "warning",
                allowClickOutSide: false,
                buttons: ["No", "Si"],
                dangerMode: true,
            })
                .then((willSave) => {
                    if (willSave) {
                        console.log("Guardando Cambios");
                        var ElementPreview = document.getElementById("IFPreview");
                        $($(ElementPreview.contentDocument).find("#zamba_save")).click();

                        TmrZambaSave = setInterval(function () {
                            var ZambaSaveResult = localStorage.getItem("ZambaSaveResult");

                            if (ZambaSaveResult) {
                                clearInterval(TmrZambaSave);

                                OpenTaskFromKendoGrid(ScopeResultsCtrl, e);
                                localStorage.removeItem("ZambaSaveResult");
                            }
                        }, 1000);

                    } else {
                        OpenTaskFromKendoGrid(ScopeResultsCtrl, e);
                    }
                });
        } else {
            OpenTaskFromKendoGrid(ScopeResultsCtrl, e);
        }
    } else {
        OpenTaskFromKendoGrid(ScopeResultsCtrl, e);
    }

}

function ShowActions(gridData) {

    var STEP_IDs = enableUserActionRules_ForResultsGrid();
    if (STEP_IDs.length == 1) {
        var List_DOC_ID = checkDocId(gridData, STEP_IDs[0]);

        if (angular.element($("#taskController")).scope().actionRules == null) {
            angular.element($("#taskController")).scope().LoadUserAction_ForMyTaskGrid(STEP_IDs[0], List_DOC_ID[0]);
        }

    } else {
        angular.element($("#taskController")).scope().actionRules = null;
    }
}

function checkDocId(allRows, STEP_ID) {
    var List = [];
    allRows.forEach(function (elem, i) {
        if (elem.STEP_ID == STEP_ID)
            List.push(elem.DOC_ID);
    })

    return List;
}

function fixUnique() {
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
}

function enableUserActionRules_ForResultsGrid() {
    fixUnique();
    var IdsNotIterated = selectedRecords_gridResults.unique();

    return IdsNotIterated;
}

//obsoleto
function enableBtnRules_ForResultsGrid() {
    try {
        var BtnRules = JSON.parse(window.localStorage.getItem("AR|" + GetUID()));

        /// ESTE CODIGO SE GENERO POR BABEL POR QUE EMILIO USO EMSCRIP6 Y NO ES COMPATIBLE CON IE11
        /// VER MAS ADELANTE
        fixUnique();
        var IdsNotIterated = selectedRecords_gridResults.unique();

        //Solo funciona muestra botones de reglas si las tareas selecionadas coinciden con su stepid
        if ($(IdsNotIterated).length > 0 && $(IdsNotIterated).length <= 1) {
            //Oculta Los btns de reglas que se obtuvieron de "getResultsGridActions()"
            hideActionRules_ForResultsGrid(BtnRules, true);
            $("[stepid='" + $(IdsNotIterated)[0].toString() + "']").show();
        } else {
            hideActionRules_ForResultsGrid(BtnRules, true);
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
        console.error(e);
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
            if (document.getElementById("BtnSendEmail") != undefined)
                document.getElementById("BtnSendEmail").removeAttribute("disabled");
            document.getElementById("OpenAllSelected").removeAttribute("disabled");

            if (document.getElementById("BtnSendZip") != undefined)
                document.getElementById("BtnSendZip").removeAttribute("disabled");

            if (document.getElementById("BtnDownloadZip") != undefined)
                document.getElementById("BtnDownloadZip").removeAttribute("disabled");

            document.getElementById("BtnDerivar").removeAttribute("disabled");
            document.getElementById("panel_ruleActions").removeAttribute("disabled");

            //se fija si tiene workflows, si no tiene workflows como en el caso de RPI no muestra esos dos botones
            var scope_TreeViewController = angular.element($("#SidebarTree")).scope();
            if (scope_TreeViewController != undefined) {
                if (scope_TreeViewController.ChildsEntities.length == 0) {
                    $("#OpenAllSelected").css("display", "none");
                    $("#BtnDerivar").css("display", "none");
                }
            }

            $("#Actions").css('display', 'inline');
        } else {
            if (document.getElementById("BtnSendEmail") != undefined)
                document.getElementById("BtnSendEmail").setAttribute("disabled", "disabled");
            document.getElementById("OpenAllSelected").setAttribute("disabled", "disabled");

            if (document.getElementById("BtnSendZip") != undefined)
                document.getElementById("BtnSendZip").setAttribute("disabled", "disabled");

            if (document.getElementById("BtnDownloadZip") != undefined)
                document.getElementById("BtnDownloadZip").setAttribute("disabled", "disabled");

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
    angular.element($("#taskController")).scope().actionRules = null;
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
    enableBtnRules_ForResultsGrid();
    ShowActions(grid._data);
    angular.element($("#taskController")).scope().$apply();
    showBtns_ForResultsGrid();
}

function OpenSelectedRows() {
    var ResultsController = angular.element($("#ResultsCtrl")).scope();

    for (var i = 0; i < checkedIds.length; i++) {
        ResultsController.Opentask(checkedIds[i]);
    }

    if (ResultsController.checkStatus)
        document.getElementById("chkThumbGrid").click();

    RefreshKGrid(ResultsController.Search.SearchResultsObject);

    ResultsController.AUXOpenedTasks.forEach(function (item) {
        ResultsController.OpenedTasks.push(item);
    });

    ResultsController.AUXOpenedTasks = [];
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
        if (ColumnName == 'TAREA') return 150;
        else if (ColumnName == 'Tarea') return 150;
        else if (ColumnName == 'ORIGINAL') return 150;
        else if (ColumnName == 'ENTIDAD') return 0;
        else if (ColumnName == 'ID_Pedido_de_Fondos') return 35;
        else if (ColumnName == 'Asignado') return 80;
        else if (ColumnName == 'Juicio_o_Mediacion') return 40;
        else if (ColumnName == 'Nro_Jucio_Mediacion') return 50;
        else if (ColumnName == 'Nro_de_Siniestro') return 50;
        else if (ColumnName == 'Jurisdiccion') return 80;
        else if (ColumnName == 'Estudio') return 80;
        else if (ColumnName == 'STEP_ID') return 0;
        else if (ColumnName == 'LEIDO') return 0;
        else if (ColumnName == 'Leido') return 0;
        else if (ColumnName == 'Nro_de_Solicitud') return 50;
        else if (ColumnName == 'Fecha_Solicitada') return 50;
        else if (ColumnName == 'Fecha_de_Solicitud') return 50;
        else if (ColumnName == 'Fecha_de_Liquidacion') return 50;
        else if (ColumnName == 'Etapa') return 80;
        else if (ColumnName == 'Actor') return 100;
        else if (ColumnName == 'Demandado') return 100;
        else if (ColumnName == 'Icon') return 35;
        else return width;
    } catch (e) {
        console.error(e);
    }

    return width;

}

function onDataBoundAssociated(arg) {
    try {
        // iterate the data items and apply row styles where necessary
        var dataItems = arg.sender.dataSource.view();
        for (var i = 0; i < dataItems.length; i++) {
            var row = arg.sender.tbody.find("[data-uid='" + dataItems[i].uid + "']");
            if (dataItems[i]["LEIDO"] == 0) {
                row.addClass("notRead");
            }
            if (dataItems[i]["LEIDO"] == 1) {
            }
        }

        //for (var i = 0; i < this.columns.length; i++) {
        //    this.autoFitColumn(i);
        //}

    } catch (e) {
        console.error(e);
    }

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