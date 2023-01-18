//var app = angular.module("ZambaAssociatedApp", ['ui.bootstrap']);




app.controller('ZambaAssociatedController', function ($scope, $filter, $http, ZambaAssociatedService, Search, ruleExecutionService, $uibModal) {




    $scope.mulipleSelectionGridActive = false;
    $scope.rules = [];

    //Obtención de datos

    $scope.LoadResults = function (parentResultId, partentEntityId, associatedIds, parentTaskId) {
        try {

            $scope.associatedIds = associatedIds;
            $scope.loading = true;

            ZambaAssociatedService.getResults(parentResultId, partentEntityId, associatedIds, parentTaskId).then(function (result) {
                try {


                    var grid = $("#" + $scope.gridIndex);
                    $("#" + $scope.gridIndex).empty();
                    $scope.filter = null;

                    if (result.data === null || result.data === undefined) {
                        $scope.associatedResults = [];
                    }
                    else {
                        $scope.associatedResults = JSON.parse(result.data).data;
                        $scope.columnsStringAssociated = JSON.parse(result.data).columnsStringAssociated;
                        $scope.gerateKendoGridView();

                        if ($("#" + $scope.gridIndex).data('kendoGrid') !== undefined && $("#" + $scope.gridIndex).data('kendoGrid') != null) {
                            $("#" + $scope.gridIndex).data('kendoGrid').refresh();
                        }

                        $scope.generateThumbsGridView();
                        $scope.generatePreviewGridView();

                        if ($scope.associatedResults.length > 0) {
                            $scope.previewItem($scope.associatedResults[0], 0, null, $scope.gridIndex);
                        }
                    }


                    //toastr.options.timeOut = 5000;
                    //toastr.options.positionClass = 'toast-top-left';
                    //toastr.success("Se ha actualizado la grilla");
                    //toastr.options.positionClass = 'toast-top-right';
                    $scope.loading = false;
                } catch (e) {
                    console.log(e);
                    $scope.loading = false;
                }

            });

        } catch (e) {
            $scope.loading = false;
        }

    };

    //Generación de la grilla kendo

    $scope.attachsIds = [];
    $scope.checkedIds = [];
    $scope.gerateKendoGridView = function () {
        if ($scope.associatedResults === "" || $scope.associatedResults.length === 0) {
            console.log("No se encontraron resultados");
            return;
        }
        else {
            //kendo grid
            $scope.LoadKendoGrid($scope.associatedResults);
        }
    }

    $scope.LoadKendoGrid = function (results) {
        var localSearchResults = results;
        var grid = $("#" + $scope.gridIndex);
        var model = generateModel(localSearchResults);
        var columns = generateColumns(localSearchResults);
        var tableHeight = $scope.tableHeight || 300;

        grid.kendoGrid({
            dataSource: {
                data: {
                    "items": localSearchResults
                },

                schema: {
                    data: "items",
                    model: model
                }
            },
            columns: columns,
            height: tableHeight,
            scrollable: true,
            dataBound: onDataBound,
            sortable: true,
            groupable: false,
            change: onChange,
            columnMenu: {
                columns: false,
                messages: {
                    sortAscending: "Ascendente",
                    sortDescending: "Descendente",
                }
            },
            reorderable: true,
            resizable: true,
            //    filter: onFiltering,
            //     group: onGrouping,


            //      groupExpand: onGroupExpand,
            //      groupCollapse: onGroupCollapse,
            nagivatable: true
        });

        gridToClick = grid.data("kendoGrid");
        gridToClick.tbody.on("click", "tr", $scope.onClick);
        gridToClick.tbody.on("dblclick", "tr", $scope.onDoubleClick);
        FixAssociatedKendoGrid(gridToClick);
        grid.css("width", $scope.tableWidth);
    }

    $scope.onThumbClick = function (result, $index, e) {


        $scope.previewItem(result, $index, e, $scope.gridIndex);
        //  e.stopPropagation();
    }


    $scope.onClick = function (e) {
        //Varible Definition
        var gridElement = $("#" + $scope.gridIndex);
        var grid = gridElement.data("kendoGrid");
        var row = $(e.target).closest("tr");
        var dataItem = grid.dataItem(row);
        var IsSelected = checkValue(row[0].sectionRowIndex, $scope.checkedIds);

        if ($scope.mulipleSelectionGridActive === false) {
            //Open Task Mode
            $scope.previewItem(dataItem, row[0].sectionRowIndex, e, $scope.gridIndex);
            //  $scope.Opentask(row[0].sectionRowIndex);
        } else {
            //Multiple check mode

            if (row.hasClass("k-state-selected")) {
                remove_array_element($scope.checkedIds, row[0].sectionRowIndex);
                //thumb check
                $("span." + $scope.thumbSelectionClass)[row[0].sectionRowIndex].click();

                e.stopPropagation();
                $(e.currentTarget).find('[type=checkbox]').prop('checked', false);
                $(e.currentTarget).removeClass("k-state-selected");
                $scope.RemoveAttach(row[0].sectionRowIndex);
                e.preventDefault();
            } else {
                $(e.currentTarget).addClass("k-state-selected");
                if (!IsSelected) {
                    $(e.currentTarget).addClass("k-state-selected");

                    $(e.currentTarget).find('[type=checkbox]').prop('checked', true);
                    $scope.checkedIds.push(row[0].sectionRowIndex);
                    $scope.GetTaskDocument($scope.checkedIds);
                    $("span." + $scope.thumbSelectionClass)[row[0].sectionRowIndex].click();
                }
            }
            e.preventDefault();
        }
        e.stopPropagation();
    }


    $scope.onDoubleClick = function (e) {
        //Varible Definition
        var gridElement = $("#" + $scope.gridIndex);
        var grid = gridElement.data("kendoGrid");
        var row = $(e.target).closest("tr");
        var dataItem = grid.dataItem(row);
        var IsSelected = checkValue(row[0].sectionRowIndex, $scope.checkedIds);

        if ($scope.mulipleSelectionGridActive === false) {
            //Open Task Mode
            $scope.Opentask(row[0].sectionRowIndex);
        } else {
            //Multiple check mode

            if (row.hasClass("k-state-selected")) {
                remove_array_element($scope.checkedIds, row[0].sectionRowIndex);
                //thumb check
                $("span." + $scope.thumbSelectionClass)[row[0].sectionRowIndex].click();

                e.stopPropagation();
                $(e.currentTarget).find('[type=checkbox]').prop('checked', false);
                $(e.currentTarget).removeClass("k-state-selected");
                $scope.RemoveAttach(row[0].sectionRowIndex);
                e.preventDefault();
            } else {
                $(e.currentTarget).addClass("k-state-selected");
                if (!IsSelected) {
                    $(e.currentTarget).addClass("k-state-selected");

                    $(e.currentTarget).find('[type=checkbox]').prop('checked', true);
                    $scope.checkedIds.push(row[0].sectionRowIndex);
                    $scope.GetTaskDocument($scope.checkedIds);
                    $("span." + $scope.thumbSelectionClass)[row[0].sectionRowIndex].click();
                }
            }
            e.preventDefault();
        }
        e.stopPropagation();
    }


    $scope.RemoveAttach = function (arg) {
        var IdToRemove = $scope.associatedResults[arg];

        if (IdToRemove !== undefined) {
            for (i = 0; i < $scope.attachsIds.length; i++) {
                if ($scope.attachsIds[i].Docid == IdToRemove.DOC_ID) {
                    $scope.attachsIds.splice(i, 1);
                }
            }
        }
    }

    $scope.GetTaskDocument = function (arg) {
        for (i = 0; i < arg.length; i++) {
            var result = $scope.associatedResults[arg[i]];
            if (result != undefined) {
                if (checkValue(result.DOC_ID, $scope.attachsIds, "attach") != true) {
                    var IdInfo = {};
                    IdInfo.Docid = parseInt(result.DOC_ID);
                    IdInfo.DocTypeid = parseInt(result.DOC_TYPE_ID);
                    $scope.attachsIds.push(IdInfo);

                }
            }
        }
    }

    function generateColumns(response) {
        try {

            if ($scope.columnsStringAssociated != undefined && $scope.columnsStringAssociated != null && $scope.columnsStringAssociated != '') {
                return JSON.parse($scope.columnsStringAssociated);
            }

        } catch (e) {
            console.log(e);
        }

        var isDateField = [];
        var columnNames = getColumnsArrayFromResult(response);
        var isHiddenCheck = !$scope.mulipleSelectionGridActive;
        //inserto columna icono al comienzo de la coleccion

        var indexOfIcon = columnNames.indexOf("Icon");
        if (indexOfIcon > -1) {
            columnNames.splice(indexOfIcon, 1);
        }

        var indexOfcheck = columnNames.indexOf("IsChecked");
        if (indexOfcheck > -1) {
            columnNames.splice(indexOfcheck, 1);
        }

        if (JSON.stringify(columnNames).indexOf("Icon") == -1) {
            columnNames.unshift("Icon");
        }
        if (JSON.stringify(columnNames).indexOf("IsChecked") == -1) {
            columnNames.unshift("IsChecked");
        }

        return columnNames.map(function (name) {


            if (name.toUpperCase() === "ISCHECKED") {
                return {
                    selectable: true, width: 50, filterable: false, hidden: isHiddenCheck
                };
            }

            if (name.toUpperCase() === "MODIFICADO" || name.toUpperCase() === "CREADO") {
                return {
                    field: name,
                    format: "{0:dd/MM/yyyy hh:mm:ss tt}",
                    parseFormats: ["yyyy-MM-dd'T'HH:mm:ss"]
                }
            }

            if (name.toUpperCase() === "ICON") {
                return {
                    field: name.trim(), format: (isDateField[name] ? "{0:dd-MM-yyyy}" : ""), width: 60, template: "<div class='customer-photo'" +
                        "style='background-image: url(" + "../../Content/Images/icons/#:ICON_ID#.png);'></div>",
                    sortable: false, filterable: false, columnMenu: false
                };
            }

            if (name.toUpperCase() === "Marks") {
                return {
                    field: name.trim(), format: "", width: 60, template: "<div class='marks-icons'></div>",
                    sortable: false, filterable: false, columnMenu: false
                };
            }

            if (name.toUpperCase() === "StakeHolders") {
                return {
                    field: name.trim(), format: "", width: 60, template: "<div  class='stakeHolders-icons'></div>",
                    sortable: false, filterable: false, columnMenu: false
                };
            }


            if (name.toUpperCase() == "ID_PEDIDOS_DE_FONDOS") {
                return {
                    field: name.trim(),
                    template: "<div style='font-family:" + 'Libre Barcode 39' + "'>#:Nro_Despacho#</div>",
                    sortable: false, filterable: false, columnMenu: false
                };
            }



            if (name.toUpperCase() == "DOC_ID" || name.toUpperCase() == "DOC_TYPE_ID" || name.toUpperCase() == "STR_ENTIDAD" || name.toUpperCase() == "THUMB" || name.toUpperCase() == "FULLPATH" || name.toUpperCase() == "ICON_ID" || name.toUpperCase() == "USER_ASIGNED" || name.toUpperCase() == "EXECUTION" || name.toUpperCase() == "DO_STATE_ID" || name.toUpperCase() == "TASK_ID" || name.toUpperCase() == "RN" || name.toUpperCase() == "LEIDO" || name.toUpperCase() == "WORKFLOW" || name.toUpperCase() == "PROCESO" || name.toUpperCase() == "ESTADO" || name.toUpperCase() == "DISK_GROUP_ID"
                || name.toUpperCase() == "DISK_GROUP_ID"
                || name.toUpperCase() == "DISK_GROUP_ID" || name.toUpperCase() == "DISK_GROUP_ID" || name.toUpperCase() == "VOL_ID"
                || name.toUpperCase() == "OFFSET" || name.toUpperCase() == "PLATTER_ID" || name.toUpperCase() == "SHARED" || name.toUpperCase() == "VER_PARENT_ID"
                || name.toUpperCase() == "ROOTID" || name.toUpperCase() == "DISK_VOL_PATH" || name.toUpperCase() == "STEP_ID"
                || name.toUpperCase() == "WORK_ID" || name.toUpperCase() == "DISK_VOL_ID" || name.toUpperCase() == "VERSION" || name.toUpperCase() == "NUMEROVERSION" || name.toUpperCase() == "DOC_FILE" || name.toUpperCase() == "name.toUpperCase()" || name.toUpperCase() == "ORIGINAL" || name.toUpperCase() == "ASIGNEDTO" || name.toUpperCase() == "STEP" || name.toUpperCase() == "THUMBIMG" || name.toUpperCase() == "TAREA" || name.toUpperCase() == "SHOWUNREAD"
                || (name.toUpperCase().startsWith("I") && isNumeric(name.slice(1, name.length - 1)))) {
                return {
                    field: name, format: (isDateField[name] ? "{0:dd-MM-yyyy}" : ""), hidden: true, filterable: false, width: 0
                }
            }
            else {

                return {

                    field: name, format: (isDateField[name] ? "{0:dd-MM-yyyy}" : ""), width: 150, filterable: true

                }
            }
        })
    }


    function getColumnsArrayFromResult(results) {
        var columns = [];
        for (var name in results[0]) {
            columns.push(name.trim().replace(/\s/g, "").replace(/\s/g, "_"));
        }
        return columns;
    }

    function generateModel(response) {

        var sampleDataItem = response[0];

        var model = {};
        var fields = {};
        for (var property in sampleDataItem) {
            if (property == ("ID")) {
                model["id"] = property;
            }
            var propType = typeof sampleDataItem[property];

            if (propType === "number") {
                fields[property] = {
                    type: "number"
                };
            } else if (propType === "boolean") {
                fields[property] = {
                    type: "boolean"
                };
            } else if (propType === "string") {
                var parsedDate = kendo.parseDate(sampleDataItem[property]);
                if (parsedDate) {

                    if (property.indexOf("Fecha") != -1 && property.indexOf("Hora") != -1) {
                        fields[property] = {
                            type: "datetime"
                        };

                    } else {

                        fields[property] = {
                            type: "date"
                        };
                        isDateField[property] = true;
                    }
                } else {
                    fields[property] = {
                        type: "string"
                    };
                }
            } else {
                fields[property] = {
                    type: "string"
                };
            }
        }

        model.fields = fields;
        console.log(isDateField);
        return model;
    }

    function GetColumnWidth(ColumnName) {

        width = 150;

        try {
            if (ColumnName == 'Asignado') return 100;
            if (ColumnName == 'Juicio_o_Mediacion') return 50;
            if (ColumnName == 'Nro_Jucio_Mediacion') return 50;
            if (ColumnName == 'Nro_de_Siniestro') return 5;
            if (ColumnName == 'Jurisdiccion') return 100;
            if (ColumnName == 'Estudio') return 80;
            if (ColumnName == 'STEP_ID') return 0;
            if (ColumnName == 'Nro_de_Solicitud') return 50;
            if (ColumnName == 'Etapa') return 100;
            if (ColumnName == 'Actor') return 200;
            if (ColumnName == 'Demandado') return 200;
            if (ColumnName == 'Icon') return 50;
        } catch (e) {
            console.log(e);
        }

        return width;

    }

    function onDataBound(arg) {

        var gridElement = $("#" + $scope.gridIndex);

        //if (localStorage && localStorage.getItem("MultiSelectionIsActive") === "false") {
        //    $(".k-checkbox-label").parent().hide();
        //}

        gridElement.css('font-size', '11px');
        $('.k-header.k-with-icon')[0].childNodes[0].hidden = true;
        $('.k-header.k-with-icon')[0].childNodes[1].hidden = true;
        $(".k-header-column-menu").kendoTooltip({ content: "Configuración de columna" });

        // iterate the data items and apply row styles where necessary
        var dataItems = arg.sender.dataSource.view();
        var groupIds = getGroupsIdsByUserId(parseInt(GetUID()));
        for (var i = 0; i < dataItems.length; i++) {
            var row = arg.sender.tbody.find("[data-uid='" + dataItems[i].uid + "']");
            //result.USER_ASIGNED == Search.UserId && result.LEIDO == 0
            //dataItems[i].USER_ASIGNED == _searchUserId && dataItems[i].LEIDO == 0
            var condicitionToCheckRead = dataItems[i].LEIDO == 0 &&
                (dataItems[i].USER_ASIGNED == parseInt(GetUID()) ||
                    groupIds.indexOf(dataItems[i].USER_ASIGNED) > -1);
            var isNotRead = condicitionToCheckRead ? true : false;
            if (isNotRead) {
                row.addClass("notRead");
            }
        }
    }

    function onChange(arg) {
        var selected = $.map(this.select(), function (item) {
            //$scope.checkedIds = [];
            for (i = 0; i < arg.sender._data.length - 1; i++) {
                $scope.checkedIds.push(arg.sender._data[i].RN);
            }

            $scope.GetTaskDocument($scope.checkedIds);
            return item.className;

        });
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
        //    TareaColumnID = getColumnIndex(grid.columns, "Tarea"),
        //    CreateDate = getColumnIndex(grid.columns, "CREADO"),
        //    UpdateDate = getColumnIndex(grid.columns, "MODIFICADO");


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
        //    grid.reorderColumn(4, actor);
        //if (Demandado != undefined)
        //    grid.reorderColumn(5, Demandado);
        //if (Juicio_o_Mediacion != undefined)
        //    grid.reorderColumn(6, Juicio_o_Mediacion);
        //if (Nro_Juicio_Mediacion != undefined)
        //    grid.reorderColumn(7, Nro_Juicio_Mediacion);
        //if (Ramo != undefined)
        //    grid.reorderColumn(8, Ramo);
        //if (Poliza != undefined)
        //    grid.reorderColumn(9, Poliza);
        //if (Nro_de_Siniestro != undefined)
        //    grid.reorderColumn(10, Nro_de_Siniestro);
        ////        if (actor != undefined)
        //// grid.reorderColumn(10, tipo_notificacion);



        //if (AsignadoColumn != undefined)
        //    grid.reorderColumn(columnsCount - 1, AsignadoColumn);
        //if (TareaColumn != undefined)
        //    grid.reorderColumn(columnsCount - 1, TareaColumn);
        //if (EtapaColumn != undefined)
        //    grid.reorderColumn(columnsCount - 1, EtapaColumn);
        //if (Entidad != undefined)
        //    grid.reorderColumn(columnsCount - 1, Entidad);
        ////if (CreateDate != undefined)
        ////    grid.reorderColumn(columnsCount - 1, CreateDate);
        ////if (UpdateDate != undefined)
        ////    grid.reorderColumn(columnsCount - 1, UpdateDate);


    }

    //Generación de la grilla de Thumbs

    $scope.generateThumbsGridView = function () {
        if ($scope.associatedResults == "" || $scope.associatedResults.length == 0) {
            console.log("No se encontraron resultados");
            return;
        }
        else {
            //thumbs         
            $scope.Search = getNewSearch();
            var userId = parseInt(GetUID());
            $scope.Search.UserId = userId;
            $scope.Search.GroupsIds = getGroupsIdsByUserId(userId);
            $scope.Search.SearchResults = $scope.associatedResults;
            //for (var index in $scope.Search.SearchResults) {
            //    if ($scope.Search.SearchResults[index].Tarea != null) {
            //        $scope.Search.SearchResults[index].Tarea = $scope.Search.SearchResults[index].Tarea.split(":")[1].trim();
            //    }
            //}
            ProcessResults($scope.Search);
        }
    }

    //Generación de la grilla de Preview

    $scope.generatePreviewGridView = function () {
        if ($scope.associatedResults == "" || $scope.associatedResults.length == 0) {
            console.log("No se encontraron resultados");
            return;
        }
        else {
            //Preview         
            $scope.Search = getNewSearch();
            var userId = parseInt(GetUID());
            $scope.Search.UserId = userId;
            $scope.Search.GroupsIds = getGroupsIdsByUserId(userId);
            $scope.Search.SearchResults = $scope.associatedResults;

            ProcessResults($scope.Search);
        }
    }


    function getGroupsIdsByUserId(userId) {
        var groups = [];
        try {
            if (localStorage) {
                var localGroups = localStorage.getItem("localGroups" + GetUID());
                if (localGroups != undefined && localGroups != null && localGroups.length > 0) {
                    groups = localGroups;
                    return groups;

                }
            }
        } catch (e) {
            console.log(e);
        }

        if (groups.length == 0) {
            $.ajax({
                type: 'GET',
                async: false,
                url: ZambaWebRestApiURL + "/Tasks/GetGroupsByUserIds",
                data: { usrID: userId },
                success: function (data) {
                    if (data != undefined && data != null) {
                        groups = data;
                        try {
                            if (localStorage) {
                                localStorage.setItem("localGroups" + GetUID(), data);
                            }
                        } catch (e) {
                            console.log(e);
                        }
                    }

                },
                error: function (xhr, status, error) {
                    console.log(error);
                }
            });
        }
        return groups;
    }

    $scope.showSeletionModeByCkeck = function (event, arg) {
        thumbContainerResize(event.target);
        //thumbButtonDisplay();

        if ($(event.target).hasClass("glyphicon-ok-sign")) {
            //addItem(arg, $scope.thumbSelectedIndexs);
            if (!validateIfItemIsInArray(arg, $scope.checkedIds)) {
                $scope.checkedIds.push(arg);
                $scope.GetTaskDocument($scope.checkedIds);
            }
            //Kendo grid
            // $("tr").find('[type=checkbox]')[arg+1].checked = true

            $("#" + $scope.gridIndex).find("input.k-checkbox")[arg + 1].checked = true;
            $("#" + $scope.gridIndex).find("input.k-checkbox")[arg + 1].closest("tr").className = "k-state-selected";

            //$("input.k-checkbox")[arg + 1].checked = true;
            //$("input.k-checkbox")[arg + 1].closest("tr").className = "k-state-selected";
        } else {
            //removeItem(arg, $scope.checkedIds);
            if (validateIfItemIsInArray(arg, $scope.checkedIds)) {
                remove_array_element($scope.checkedIds, arg);
                $scope.RemoveAttach(arg);
            }
            //Kendo grid

            $("#" + $scope.gridIndex).find("input.k-checkbox")[arg + 1].checked = false;
            $("#" + $scope.gridIndex).find("input.k-checkbox")[arg + 1].closest("tr").className = "";


            //$("input.k-checkbox")[arg + 1].checked = false;
            //$("input.k-checkbox")[arg + 1].closest("tr").className = "";
        }
        event.preventDefault();
        event.stopPropagation()
    }

    //Selección  multiple flag
    $scope.setSelectionMode = function () {
        var grid = $("#" + $scope.gridIndex).data("kendoGrid");
        if ($scope.mulipleSelectionGridActive) {
            $("span." + $scope.thumbSelectionClass + ".glyphicon-ok-sign").click();
            $scope.mulipleSelectionGridActive = false;
            grid.hideColumn(0);
            grid.clearSelection();
            $scope.resetMultipleSelection();
            $scope.checkedIds = [];
            $scope.attachsIds = [];
        } else {
            $scope.mulipleSelectionGridActive = true;
            grid.showColumn(0);
        }
    }

    $scope.resetMultipleSelection = function () {
        var thumbsCollection = $(".resultsGridSearchBoxThumbs_" + $scope.gridIndex).find(".glyphicon-ok-sign");
        thumbsCollection.addClass("glyphicon glyphicon-ok-circle");
        thumbsCollection.removeClass("glyphicon-ok-sign");
        $(".resultsGridSearchBoxThumbs_" + $scope.gridIndex).find(".glyphicon-info-sign").show();
        $(".resultsGridSearchBoxThumbs_" + $scope.gridIndex).find(".glyphicon-zoom-in").show();
        $(".resultsGridSearchBoxThumbs_" + $scope.gridIndex).find(".resultsGrid_" + $scope.gridIndex).css("width", "150px");
        $(".resultsGridSearchBoxThumbs_" + $scope.gridIndex).find(".resultsGrid_" + $scope.gridIndex).css("max-height", "170px");
    }

    function SelectionMultipleIsActive() {
        var result = localStorage.getItem("MultiSelectionIsActive");
        return result;
    }

    //Refresh de grillas
    $scope.refreshGrid = function () {
        $scope.loading = true;
        $scope.LoadResults($scope.parentResultId, $scope.partentEntityId, $scope.entities, $scope.parentTaskId);
    }

    //Inicializa modo de la grilla
    $scope.setGridView = function (mode) {
        $scope.enableMode = mode;
    }

    $scope.showPreview = true;

    $scope.checkedIds = [];
    $scope.openmode = 'view';

    //Apertura de tarea 
    $scope.Opentask = function (arg) {
        var thumbsCollection = $(".resultsGridSearchBoxThumbs_" + $scope.gridIndex).find(".glyphicon-ok-sign");
        $scope.thumbsCheckedCount = thumbsCollection.length;

        if (thumbsCollection.length == 0) {
            var result = $scope.Search.SearchResults[arg];
            var userid = GetUID();
            var stepid = result.STEP_ID;
            var taskId = result.TASK_ID;

            if (stepid == undefined) {
                stepid = result.Step_Id;
            }
            if (taskId == undefined) {
                taskId = result.Task_Id;
            }

            if ($scope.openmode == "view") {
                if (stepid != null && stepid != undefined && stepid != 0) {
                    var Url = (thisDomain + "/views/WF/TaskViewer.aspx?DocType=" + result.DOC_TYPE_ID + "&docid=" + result.DOC_ID + "&taskid=" + taskId + "&mode=s"
                        + "&s=" + stepid + "&userId=" + userid);
                }
                else {
                    var Url = (thisDomain + "/views/search/docviewer.aspx?DocType=" + result.DOC_TYPE_ID + "&docid=" + result.DOC_ID + "&mode=s"
                        + "&userId=" + userid);

                }
            }
            else {
                var Url = (thisDomain + "/Services/GetDocFile.ashx?DocTypeId=" + result.DOC_TYPE_ID + "&DocId=" + result.DOC_ID + "&UserID=" + userid + "&ConvertToPDf=true");
            }

            //var Url = (thisDomain + "/views/WF/TaskSelector.ashx?DocTypeId=" + result.DOC_TYPE_ID + "&docid=" + result.DOC_ID + "&taskid=" + result.TASK_ID
            //    + "&wfstepid=" + stepid + "&userId=" + userid);

            switch (zambaApplication) {
                case "ZambaWeb":
                    OpenDocTask3(result.TASK_ID, result.DOC_ID, result.DOC_TYPE_ID, false, "Reemplazar", Url, userid, 0);
                    $('#Advfilter1').modal("hide");
                    break;
                case "ZambaWindows": case "ZambaHomeWidget": case "ZambaQuickSearch":
                    winFormJSCall.openTask(result.DOC_TYPE_ID, result.DOC_ID, result.TASK_ID, result.STEP);
                    $('#Advfilter1').modal("hide");
                    break;
                case "Zamba":
                    //var token = $scope.GetTokenInfo().token;
                    window.open(Url, '_blank');
                    break;
                case "ZambaSearch":
                    //var token = $scope.GetTokenInfo().token;
                    window.open(Url, '_blank');
                    break;
            }

            try {
                // Notificar la lectura del documento
                //result.LEIDO == 0 && result.USER_ASIGNED == userid
                if (result.LEIDO == 0 && result.USER_ASIGNED == userid) {

                    //if (result.USER_ASIGNED == userid) {
                    var url = ZambaWebRestApiURL + "/search/NotifyDocumentRead?" + jQuery.param({ UserId: userid, DocTypeId: result.DOC_TYPE_ID, DocId: result.DOC_ID });
                    $.post(url, function myfunction() {
                    }).success(function () {
                        // Actualiza count de no leidas
                        $scope.LoadMyTasksCount();
                    });
                    //}

                    // Actualiza estado de leido en thumbs y preview
                    result.LEIDO = 1;
                    $scope.$apply();
                    // Actualizar estado de tareas en la grilla
                    $scope.refreshGrid();
                    //$scope.MultipleSelection(false);

                }
            } catch (e) {
                console.log(e.message);
            }


        } else {
            $scope.onSelectionMode = true;
        }
    };

    //Cantidad de tareas no leidas
    $scope.LoadMyTasksCount = function () {
        $.ajax({
            type: 'GET',
            url: ZambaWebRestApiURL + '/search/GetMyUnreadTasksCount?currentUserId=' + GetUID(),
            async: true,
            //data: { currentUserId: GetUID() },
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                //                scope.$apply();
                if ($scope.MyUnreadTasks != data && data != 0) {
                    $scope.MyUnreadTasks = data;
                    setInterval(function () {
                        var actualizada = toastr.info("Se ha agregado una nueva tarea");
                        toastr.options.timeOut = 3000;
                    }, 300000);
                }
            }
        });
    }

    //Filtrar las grillas con un input
    $scope.setFilteredResults = function (word) {
        if (word != "" && word != null) {
            var results = filterResultsByWord(word, $scope.associatedResults);
            $scope.loadFilteredResults(results);
        } else {
            $scope.loadFilteredResults($scope.associatedResults)
        }
        $scope.attachsIds = [];
        $scope.checkedIds = [];
        var grid = $("#" + $scope.gridIndex).data("kendoGrid");
        grid.clearSelection();
    }

    $scope.loadFilteredResults = function (results) {
        $("#" + $scope.gridIndex).empty();

        if (results.length > 0) {
            //kendo grid
            $scope.LoadKendoGrid(results);

            //thumbs         
            $scope.Search = Search;
            $scope.Search.SearchResults = results;
            ProcessResults($scope.Search);

            $("#" + $scope.gridIndex).data('kendoGrid').refresh();
        } else {
            $("#" + $scope.gridIndex).append("<div><p style='color:grey; font-weight: bold;'>No hay coincidencias.</p></div>");
        }
    }

    //Rule Execution
    $scope.executeRule = function () {
        var rules = $scope.ruleId.trim();
        var resultIds = getSelectedDocids();
        $scope.rules = ruleExecutionService.executeRule(rules, resultId);
    }

    $scope.getRuleName = function () {
        var names = [];
        var d = ruleExecutionService.getRuleNames($scope.ruleIds)
        var results = JSON.parse(d);
        $scope.ruleDictionary = results;
        for (var result in results) {
            if (result.indexOf("id") == -1) {
                names.push(results[result])
            }
        }
        return names;
    }

    $scope.executeCurrentRule = function (ruleName) {
        if ($scope.checkedIds != null && $scope.checkedIds.length > 0) {
            var ruleIds = getRuleIdFromdictionaryByName(ruleName);
            var resultIds = getSelectedDocids().toString();
            ruleExecutionService.executeRule(ruleIds, resultIds);
        } else {
            swal("No se a podido ejecutar la regla", "Seleccione al menos una tarea.", "warning");
        }
    }

    function getSelectedDocids() {
        var docIds = [];
        for (i = 0; i < $scope.attachsIds.length; i++) {
            docIds.push($scope.attachsIds[i].Docid);
        }
        return docIds;
    }

    function getRuleIdFromdictionaryByName(ruleName) {
        var ruleDictionary = $scope.ruleDictionary;
        var ruleId = null;
        for (var rule in ruleDictionary) {
            if (ruleDictionary[rule] == ruleName) {
                ruleId = rule;
            }
        }
        return ruleId;
    }


    function validateIfItemIsInArray(item, array) {
        return array.indexOf(item) > -1 ? true : false;
    }

    function format(inputDate) {
        var date = new Date(inputDate);
        if (!isNaN(date.getTime())) {
            // Months use 0 index.
            return date.getDate() + '-' + date.getMonth() + 1 + '-' + date.getFullYear();
        }
    }

    function filterResultsByWord(word, items) {
        var resultsFilteres = [];
        if (word.length > 0) {
            for (var item in items) {
                var attributes = JSON.stringify(Object.values(items[item])).toLowerCase();
                if (attributes.indexOf(word.toLowerCase()) > -1) {
                    resultsFilteres.push(items[item]);
                }
            }
        }
        return resultsFilteres
    }

    $scope.thumbZoom = function (result) {
        var path = '../../app/Grid/Views/zoomModal.html';
        $scope.showModalWithGridData(result, path, "zoom");
    }

    $scope.ShowThumbInfoGS = function (result) {
        var path = '../../app/Grid/Views/infoModal.html';
        $scope.showModalWithGridData(result, path, "moreInfo");
    }

    function thumbContainerResize(_this) {
        var thumbContainer = $(_this).parents(".resultsGrid_" + $scope.gridIndex);
        var thumb = $(_this).parent().parent().children(".document-photo-thumbs");
        var $detailsButton = $($(_this).parents(".resultsGrid_" + $scope.gridIndex)[0]).find(".glyphicon-info-sign");
        var $zoomButton = $($(_this).parents(".resultsGrid_" + $scope.gridIndex)[0]).find(".glyphicon-zoom-in");
        var changeSizeButton;


        if (thumbContainer.css("width") == "130px") {
            $detailsButton.show();
            $zoomButton.show();
            changeSizeButton = $($(_this).parents(".resultsGrid_" + $scope.gridIndex)[0]).find(".glyphicon-ok-sign");
            changeSizeButton.addClass("glyphicon glyphicon-ok-circle");
            changeSizeButton.removeClass("glyphicon-ok-sign");
            thumbContainer.css("width", "150px");
        } else {
            $detailsButton.hide();
            $zoomButton.hide();
            // $("#resultsGridSearchBoxThumbs").find(".document-photo-thumbs").attr("onclick", "showSeletionMode(this)");
            changeSizeButton = $($(_this).parents(".resultsGrid_" + $scope.gridIndex)[0]).find(".glyphicon-ok-circle");
            changeSizeButton.addClass("glyphicon glyphicon-ok-sign");
            changeSizeButton.removeClass("glyphicon-ok-circle");
            thumbContainer.css("width", "130px");
        }
    }


    //modal

    $scope.openModal = function (result, path) {
        $uibModal.open({
            templateUrl: path,
            windowClass: 'modalStyle',
            controller: function ($scope, $uibModalInstance) {
                $scope.resultDetails = result;
                $scope.cancel = function () {
                    $uibModalInstance.dismiss('cancel');
                };
            }
        })
    };

    $scope.showModalWithGridData = function (result, path, mode) {
        if (mode == "zoom") {
            $scope.openModal(result, path);
        } else if (mode == "moreInfo") {
            $scope.setModalData(result);
            $scope.openModal($scope.setModalData(result), path);
        }

    }

    $scope.setModalData = function (result) {
        var newResult = null;
        if (result != null) {
            newResult = mappingResultToModal(result);
        }
        return newResult;
    }

    function mappingResultToModal(result) {
        var newResultDictionary = {};
        for (var attribute in result) {
            if (!validateHiddenAttr(attribute)) {
                newResultDictionary[String(attribute)] = result[attribute] == null ? "" : String(result[attribute]);
            }
        }
        return newResultDictionary;
    }

    function validateHiddenAttr(name) {
        var isHiddenAttr = false;
        if (name == "DOC_ID" || name == "DOC_TYPE_ID" || name == "STR_ENTIDAD" || name == "THUMB" || name == "FULLPATH" || name == "ICON_ID"
            || name == "USER_ASIGNED" || name == "EXECUTION" || name == "DO_STATE_ID" || name == "TASK_ID" || name == "RN" || name == "LEIDO"
            || name == "WORKFLOW" || name == "Proceso" || name == "Estado" || name == "DISK_GROUP_ID"
            || name == "DISK_GROUP_ID" || name == "VOL_ID" || name == "OFFSET" || name == "PLATTER_ID" || name == "SHARED" || name == "VER_PARENT_ID"
            || name == "ROOTID" || name == "DISK_VOL_PATH" || name == "STEP_ID" || name == "WORK_ID" || name == "DISK_VOL_ID" || name == "VERSION"
            || name == "NUMEROVERSION" || name == "DOC_FILE" || name == "ORIGINAL" || name == "AsignedTo" || name == "Step"
            || name == "ThumbImg" || name == "Tarea" || name == "ShowUnread" || name == "Icon"
            || name.toLowerCase().startsWith("i") && isNumeric(name.slice(1, name.length - 1))) {
            isHiddenAttr = true;
        }
        return isHiddenAttr;
    }


    //////////////////////////////PREVIEW//////////////////////////////////////////////////////


    $scope.previewItem = function (result, index, event, Id) {
        if ($scope.showPreview == true && $scope.Search.SearchResults != undefined && $scope.Search.SearchResults.length > 0) {
            //Workaround JQuery desde Search.html no cambia valor NGSelectedRow en vista si desde NG
            var $items = $("#resultsGridSearchBoxPreview_" + $scope.gridIndex + ">.previewListItems");
            var $active = $items.children(".resultsGridActive").index();
            $items.children().removeClass("resultsGridActive");
            if (event != undefined) {
                var item = this.Search.SearchResults[event == undefined ? index - 1 : index];

                //this.Search.SearchResults.forEach(x => x.NGSelectedRow = false);

                for (var i = 0; i < this.Search.SearchResults.length; i++) {
                    $scope.Search.SearchResults.NGSelectedRow = false;
                }

                item.NGSelectedRow = true;
                if (event.type == "click" && event.target.tagName == "IMG") {
                    $(event.target).parents(".resultsGrid_" + $scope.gridIndex).addClass("resultsGridActive");
                }
            }
            else {
                if (index === -1) {
                    $active = -1;
                }
                var scrollPosition = $(".previewListItems").scrollTop() + ($active > -1 ? 223 : 0);
                setTimeout(function () {
                    $($items.children()[$active + 1]).addClass("resultsGridActive");
                    $(".previewListItems").animate({
                        scrollTop: scrollPosition
                    }, 300);
                }, 100);

            }

            var currentresult = result; //$scope.Search.SearchResults[result];
            var url = "../../Services/GetDocFile.ashx?DocTypeId=" + currentresult.DOC_TYPE_ID + "&DocId=" + currentresult.DOC_ID + "&UserID=" + GetUID() + "&ConvertToPDf=true";

            try {

                setTimeout(function () {
                    try {
                        if ($("#previewGrid_" + $scope.gridIndex)[0] != undefined) $("#previewGrid_" + $scope.gridIndex)[0].contentWindow.OpenUrl(url, index, Id);
                        try {
                            if ($("#previewGrid_" + $scope.gridIndex)[0] != undefined) $($("#previewGrid_" + $scope.gridIndex)[0]).css('height', $scope.tableHeight);
                        } catch (e) {
                            console.log(e);
                        }
                    }
                    catch (error) {
                    }
                }, 300);


            }
            catch (error) {
                console.log(error);
            }
            return url;
        }
    }

    $scope.GetNextUrl = function (index, Id) {
        if ($scope.Search == undefined || $scope.Search.SearchResults == undefined) return;
        index++;
        var currentresult = $scope.Search.SearchResults[index];
        $scope.previewItem(currentresult, index, null, Id)
    };

    setTimeout(function () {
        try {
            // if ($("#previewGrid_" + $scope.gridIndex)[0] != undefined) $("#previewGrid_" + $scope.gridIndex)[0].contentWindow.OpenUrl("_AboutBlank", index);
        }
        catch (error) {
        }
    }, 30);


    //////////////////////////////////////////////////////////////////////////////////////////////

});

function GetNextUrl(currentIndex, Id) {
    return angular.element($('#' + Id)).scope().GetNextUrl(currentIndex, Id);
}

function getNewSearch() {
    return {
        SearchId: 0,
        OrganizationId: 0,
        DoctypesIds: [],
        Indexs: [],
        blnSearchInAllDocsType: true,
        TextSearchInAllIndexs: '',
        RaiseResults: false,
        ParentName: '',
        CaseSensitive: false,
        MaxResults: 1000,
        ShowIndexOnGrid: true,
        UseVersion: false,
        UserId: 0,
        GroupsIds: [],
        StepId: 0,
        StepStateId: 0,
        TaskStateId: 0,
        WorkflowId: 0,
        NotesSearch: '',
        Textsearch: '',
        SearchResults: null,
        OrderBy: null,
        Filters: [],

    }
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

function getElementFromQueryString(element) {
    var url = window.location.href;

    var segments = url.split("&");
    var value = null;
    segments.forEach(function (valor) {
        if (valor.includes(element)) { value = valor.split("=")[1]; }
    });
    try {
        if (value == null && element == 'DocType') {
            value = $("[id$=hdnDocTypeId]").val();
        }
    } catch (e) {

    }
    return value;
}


app.directive('zambaAssociated', function ($sce) {
    return {
        restrict: 'E',
        //scope: false,
        //replace: true,
        transclude: true,
        link: function ($scope, element, attributes) {
            $scope.entities = attributes.entities;
            //$scope.enableMode = attributes.enableMode;
            $scope.defaultEnableMode = attributes.defaultEnableMode;
            $scope.enableMode = $scope.defaultEnableMode
            $scope.ruleId = attributes.ruleid;
            $scope.tableHeight = attributes.tableHeight
            if (attributes.tableWidth != null && (attributes.tableWidth.indexOf("px") > -1 ||
                attributes.tableWidth.toLowerCase().indexOf("%") > -1)) {
                $scope.tableWidth = attributes.tableWidth;
            } else {
                $scope.tableWidth = "100%";
            }
            $scope.parentTaskId = 0;
            $scope.partentEntityId = getElementFromQueryString("DocType");
            if ($scope.partentEntityId == null) {
                $scope.parentTaskId = getElementFromQueryString("taskid");
            }
            $scope.parentResultId = getElementFromQueryString("docid");
            $scope.ruleIds = attributes.ruleIds;
            $scope.gridTitle = attributes.gridTitle;
            if ($scope.ruleIds != null) {
                $scope.rules = $scope.getRuleName();
            } else {
                $scope.rules = [];
            }

            try {
                if (attributes.openmode != null) {
                    $scope.openmode = attributes.openmode;
                } else {
                    $scope.openmode = "view";
                }

            }
            catch (e) {
            }
            if (attributes.showPreview != undefined && attributes.showPreview != null) {
                $scope.showPreview = attributes.showPreview;
            }
            else {
                $scope.showPreview = false;
            }
            $scope.gridIndex = "zamba_grid_index_" + $scope.entities.replace(/\,/g, "_");
            $scope.thumbSelectionClass = "thumbSelection_" + $scope.gridIndex;
        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/Grid/Directives/GridDirective.html')
    }
});


app.directive('zambaAssociatedKendo', function ($sce) {
    return {
        restrict: 'E',
        //scope: false,
        //replace: true,
        transclude: true,
        link: function ($scope, element, attributes) {
            var kendoGridDiv = null, kendoGridStyle = null;
            kendoGridDiv = "<div id='" + $scope.gridIndex + "' style=' table-layout: fixed, height:" + $scope.tableHeight +
                ";display: flex;flex-direction: column;flex-wrap: nowrap;justify-content: space-between;" +
                "align-items: stretch  margin: 0;padding: 0;height: 100%;' class='ZKGrid center'></div >";
            kendoGridStyle = "<style> .k-grid-content{height: 100% !important;}</style>";
            element.append(kendoGridDiv + kendoGridStyle);
            $scope.LoadResults($scope.parentResultId, $scope.partentEntityId, $scope.entities, $scope.parentTaskId);
        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/Grid/Directives/GridKendoDirective.html')
    }
});

app.directive('zambaAssociatedThumbs', function ($sce) {
    return {
        restrict: 'E',
        //scope: false,
        //replace: true,
        transclude: true,
        link: function ($scope, element, attributes) {
            $scope.thumbStyle = { "height": $scope.tableHeight, "width": $scope.tableWidth };
            $scope.resultDetails = {};

        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/Grid/Directives/GridThumbDirective.html')
    }
});


app.directive('zambaAssociatedPreview', function ($sce) {
    return {
        restrict: 'E',
        //scope: false,
        //replace: true,
        transclude: true,
        link: function ($scope, element, attributes) {
            $scope.resultDetails = {};
        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/Grid/Directives/GridPreviewDirective.html')
    }
});

