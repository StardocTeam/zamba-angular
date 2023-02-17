app.controller('ZambaAssociatedController', function ($scope, $filter, $http, ZambaAssociatedService, Search, ruleExecutionService, $uibModal) {

    $scope.Caller = null;

    $scope.removeCaller = function () {
        try {

            if ($scope.Caller != null && $scope.Caller != undefined) {
                var callerBtn = document.getElementById($scope.Caller);
                $scope.Caller = null;
                if (callerBtn)
                    callerBtn.removeEventListener("click", $scope.refreshGrid, false);
            }
        } catch (e) {
            console.error(e);
        }
    }

    $scope.Mode = Search.mode;
    $scope.Search = Search.state;
    $scope.onlyimportants = false;
    $scope.entities = '';
    //Variables
    var ObjFor_ValClick = { timer: 0, delay: 300, prevent: false };


    $scope.mulipleSelectionGridActive = false;
    if (window.localStorage.getItem("MultipleSelection")) {
        window.localStorage.setItem("MultipleSelection", "false");
    } else {
        window.localStorage.setItem("MultipleSelection", $scope.mulipleSelectionGridActive.toString());
    }

    $scope.rules = [];

    //Obtención de datos

    $scope.LoadResults = function (parentResultId, partentEntityId, associatedIds, parentTaskId, zvar, onlyimportants) {

        try {
            
            $scope.loading = true;

            if (zvar !== undefined) {

                ZambaAssociatedService.getResultsByZvarANdRuleId(GetUID(), $scope.ruleId, parentResultId, null, $scope.zvar).then(function (data) {
                    try {

                        var grid = $("#" + $scope.gridIndex);
                        $("#" + $scope.gridIndex).empty();
                        $scope.filter = null;

                        if (data === null || data === undefined) {
                            $scope.associatedResults = [];
                        }
                        else {
                            $scope.associatedResults = JSON.parse(data).data;

                            if ($scope.associatedResults != null && $scope.associatedResults.length > 0) {

                                $scope.columnsStringAssociated = JSON.parse(data).columnsStringAssociated;
                                $scope.gerateKendoGridView();

                                if ($("#" + $scope.gridIndex).data('kendoGrid') !== undefined && $("#" + $scope.gridIndex).data('kendoGrid') != null) {
                                    $("#" + $scope.gridIndex).data('kendoGrid').refresh();
                                }

                                $scope.generateThumbsGridView();
                                $scope.generatePreviewGridView();

                                $scope.previewItem($scope.associatedResults[0], 0, null, $scope.gridIndex);
                            }
                            else {
                                console.log("No hay resultados para la Regla ", $scope.ruleId, " y para la variable ", $scope.zvar);
                            }
                        }
                        
                        $scope.loading = false;
                    } catch (e) {
                        
                        console.error(e);
                        $scope.loading = false;
                    }
                });

            } else {
                $scope.associatedIds = associatedIds;
                ZambaAssociatedService.getResults(parentResultId, partentEntityId, associatedIds, parentTaskId, onlyimportants).then(function (result) {
                    try {
                        var grid = $("#" + $scope.gridIndex);
                        $("#" + $scope.gridIndex).empty();
                        $scope.filter = null;

                        if (result.data === null || result.data === undefined) {
                            $scope.associatedResults = [];
                        }
                        else {
                            $scope.associatedResults = JSON.parse(result.data).data;
                            if ($scope.associatedResults != null && $scope.associatedResults.length > 0) {

                                $scope.columnsStringAssociated = JSON.parse(result.data).columnsStringAssociated;
                                $scope.gerateKendoGridView();

                                if ($("#" + $scope.gridIndex).data('kendoGrid') !== undefined && $("#" + $scope.gridIndex).data('kendoGrid') != null) {
                                    $("#" + $scope.gridIndex).data('kendoGrid').refresh();
                                }

                                $scope.generateThumbsGridView();
                                $scope.generatePreviewGridView();

                                $scope.previewItem($scope.associatedResults[0], 0, null, $scope.gridIndex);
                            }
                        }
                        
                        $scope.loading = false;

                        
                            if (sessionStorage_zamba_grid = sessionStorage.getItem("zamba_grid_index_all_" + GetUID()) && !$scope.mulipleSelectionGridActive) {
                                if (parent.$("#zamba_grid_index_all") != undefined) {
                                    var gridElement = $("#" + $scope.gridIndex);
                                    var grid = gridElement.data("kendoGrid");
                                    var zamba_grid = parent.$("#zamba_grid_index_all > .k-grid-content")[0].querySelectorAll("tr");
                                    if ((zamba_grid != undefined && zamba_grid != null) && (grid != undefined && grid != null)) {
                                        for (var i = 0; i < grid._data.length; i++) {
                                            if (sessionStorage_zamba_grid != undefined && grid._data[i].DOC_ID == sessionStorage_zamba_grid) {
                                                $(zamba_grid[i]).addClass("k-state-selected");
                                            }
                                        }
                                    }
                                }
                            }
                                                
                      
                    } catch (e) {
                        console.error(e);
                        
                        $scope.loading = false;
                    }

                });
            }
        } catch (e) {
            console.error(e);
            
            $scope.loading = false;
        }

    };

    $scope.sortGridByColumn = function (order, column, rows, model) {
        var opt = $(model.fields)[0][column].type;

        switch (opt) {
            case "string":
                if (order == "asc") {
                    rows.sort(function (a, b) {
                        var Aux_A = eval("a." + column) ? eval("a." + column) : "";
                        var Aux_B = eval("b." + column) ? eval("b." + column) : "";

                        if (Aux_A > Aux_B) {
                            return 1;
                        } else if (Aux_A < Aux_B) {
                            return -1;
                        }
                    });
                } else if (order == "desc") {
                    rows.sort(function (a, b) {
                        var Aux_A = eval("a." + column) ? eval("a." + column) : "";
                        var Aux_B = eval("b." + column) ? eval("b." + column) : "";

                        if (Aux_A < Aux_B) {
                            return 1;
                        } else if (Aux_A > Aux_B) {
                            return -1;
                        }
                    });
                }
                break;

            case "number":
                if (order == "asc") {
                    rows.sort(function (a, b) {
                        var Aux_A = eval("a." + column) ? eval("a." + column) : 0;
                        var Aux_B = eval("b." + column) ? eval("b." + column) : 0;

                        if (Aux_A > Aux_B) {
                            return 1;
                        } else if (Aux_A < Aux_B) {
                            return -1;
                        }
                    });
                } else if (order == "desc") {
                    rows.sort(function (a, b) {
                        var Aux_A = eval("a." + column) ? eval("a." + column) : 0;
                        var Aux_B = eval("b." + column) ? eval("b." + column) : 0;

                        if (Aux_A < Aux_B) {
                            return 1;
                        } else if (Aux_A > Aux_B) {
                            return -1;
                        }
                    });
                }
                break;

            case "boolean":
                if (order == "asc") {
                    rows.sort(function (a, b) {
                        var Aux_A = eval("a." + column) ? eval("a." + column) : 0;
                        var Aux_B = eval("b." + column) ? eval("b." + column) : 0;

                        if (Date.parse(Boolean(Aux_A)) > Date.parse(Boolean(Aux_B))) {
                            return 1;
                        } else if (Date.parse(Boolean(Aux_A)) < Date.parse(Boolean(Aux_B))) {
                            return -1;
                        }
                    });
                } else if (order == "desc") {
                    rows.sort(function (a, b) {
                        var Aux_A = eval("a." + column) ? eval("a." + column) : 0;
                        var Aux_B = eval("b." + column) ? eval("b." + column) : 0;

                        if (Date.parse(Boolean(Aux_A)) < Date.parse(Boolean(Aux_B))) {
                            return 1;
                        } else if (Date.parse(Boolean(Aux_A)) > Date.parse(Boolean(Aux_B))) {
                            return -1;
                        }
                    });
                }
                break;

            case "date":
                if (order == "asc") {
                    rows.sort(function (a, b) {
                        var Aux_A = eval("a." + column) ? eval("a." + column) : 0;
                        var Aux_B = eval("b." + column) ? eval("b." + column) : 0;

                        if (Date.parse(Aux_A) > Date.parse(Aux_B)) {
                            return 1;
                        } else if (Date.parse(Aux_A) < Date.parse(Aux_B)) {
                            return -1;
                        }
                    });
                } else if (order == "desc") {
                    rows.sort(function (a, b) {
                        var Aux_A = eval("a." + column) ? eval("a." + column) : 0;
                        var Aux_B = eval("b." + column) ? eval("b." + column) : 0;

                        if (Date.parse(Aux_A) < Date.parse(Aux_B)) {
                            return 1;
                        } else if (Date.parse(Aux_A) > Date.parse(Aux_B)) {
                            return -1;
                        }
                    });
                }
                break;

            default:
        }
    }



    //Generación de la grilla kendo

    $scope.attachsIds = [];
    $scope.checkedIds = [];
    $scope.gerateKendoGridView = function () {
        if ($scope.associatedResults == null || $scope.associatedResults === "" || $scope.associatedResults.length === 0) {
            console.log("No se encontraron resultados");
            return;
        }
        else {
            //kendo grid
            $scope.LoadKendoGrid($scope.associatedResults);
        }
    }


    var DocToolBarContentHeight = 500;
    $(window).on("resize", function () {
        if (window.innerHeight < 500)
            window.innerHeight = 500;
        DocToolBarContentHeight = window.innerHeight - 150

    });

    $scope.LoadKendoGrid = function (results) {
        var localSearchResults = results;
        var grid = $("#" + $scope.gridIndex);
        var model = generateModel(localSearchResults);
        var columns = generateColumns(localSearchResults);
        var tableHeight = $scope.tableHeight || 300;

        if (tableHeight == 'full') {
            tableHeight = 9999;
        }
        else if (tableHeight == 'content') {
            tableHeight = DocToolBarContentHeight;
        }

        let exactHeight = tableHeight.toString().replace('px', '');
        tableHeight = exactHeight < 100 ? (100 + 'px') : tableHeight;



        if ($scope.sortColumn && $scope.orderBy) {
            $scope.sortGridByColumn($scope.orderBy, $scope.sortColumn, localSearchResults, model);
        }
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
            excel: {
                fileName: "Zamba - Grilla de Asociados.xlsx"
            },
            pdf: {
                fileName: "Zamba - Grilla de Asociados.pdf"
            },
            groupable: true,
            pageable: false,
            filterable: false,
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

        gridToClick.tbody.on("dblclick", "tr", Val_EventDblClick);
        //gridToClick.tbody.on("dblclick", "tr", $scope.onDoubleClick);
        gridToClick.tbody.on("click", "tr", Val_EventClick);
        //gridToClick.tbody.on("click", "tr", $scope.onClick);
        FixAssociatedKendoGrid(gridToClick);
        grid.css("width", $scope.tableWidth);
    }

    $scope.exportGridToExcel = function () {
        var grid = $("#" + $scope.gridIndex).data("kendoGrid");
        grid.saveAsExcel();

    }
    $scope.exportGridToPDF = function () {
        $('#exportPDFloading').modal({ show: 'true' });
        var grid = $("#" + $scope.gridIndex).data("kendoGrid");
        grid.saveAsPDF();
        setTimeout(function () { $('#exportPDFloading').modal('hide') }, 500)
    }

    $scope.onThumbClick = function (result, $index, e) {
        var ScopeDocumentViewer = angular.element($("#GridDocumentViewer")).scope(); 
        sessionStorage.setItem("zamba_grid_index_all_" + GetUID(), result.DOC_ID);
        ScopeDocumentViewer.ShowDocument_FromItem(GetUID(), result.DOC_TYPE_ID, result.DOC_ID);
        //Open Task Mode
        SelectFile($(e.currentTarget));
    }

    //Valida si se hizo un solo click sobre el elemento.
    function Val_EventClick(e) {
        ObjFor_ValClick.timer = setTimeout(function () {
            if (!ObjFor_ValClick.prevent) {
                $scope.onClick(e);
            }
            ObjFor_ValClick.prevent = false;
        }, ObjFor_ValClick.delay);
    }

    //Valida si se hizo doble click sobre el elemento.
    function Val_EventDblClick(e) {
        clearTimeout(ObjFor_ValClick.timer);
        ObjFor_ValClick.prevent = true;
        $scope.onDoubleClick(e);
    }

    $scope.onClick = function (e) {
        //Varible Definition
        var gridElement = $("#" + $scope.gridIndex);
        var grid = gridElement.data("kendoGrid");
        var row = $(e.target).closest("tr");
        var dataItem = grid.dataItem(row);
        var IsSelected = checkValue(row[0].sectionRowIndex, $scope.checkedIds);

        if ($scope.mulipleSelectionGridActive === false) {
            var ScopeDocumentViewer = angular.element($("#GridDocumentViewer")).scope();
            var FirstData = $("#zamba_grid_index_all").data().kendoGrid._data[row[0].sectionRowIndex];
            sessionStorage.setItem("zamba_grid_index_all_" + GetUID(), FirstData.DOC_ID);
            //Open Task Mode

            ScopeDocumentViewer.ShowDocument_FromItem(GetUID(), FirstData.DOC_TYPE_ID, FirstData.DOC_ID);
            SelectFile($(e.currentTarget));
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
        //  ValueEdit(e);
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
            if ($scope.openmode != "notopen")
                $scope.Opentask(dataItem.DOC_ID);

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
        if ($scope.associatedResults != null) {
            var IdToRemove = $scope.associatedResults[arg];

            if (IdToRemove !== undefined) {
                for (i = 0; i < $scope.attachsIds.length; i++) {
                    if ($scope.attachsIds[i].Docid == IdToRemove.DOC_ID) {
                        $scope.attachsIds.splice(i, 1);
                    }
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

    var isDateField = {};

    function generateColumns(response) {
        try {

            if ($scope.columnsStringAssociated != undefined && $scope.columnsStringAssociated != null && $scope.columnsStringAssociated != '') {
                return JSON.parse($scope.columnsStringAssociated);
            }

        } catch (e) {
            console.error(e);
        }


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

        if (JSON.stringify(columnNames).indexOf("Icon") == -1 && JSON.stringify(columnNames).indexOf("ICON_ID") > -1) {
            columnNames.unshift("Icon");
        }
        if (JSON.stringify(columnNames).indexOf("IsChecked") == -1 && JSON.stringify(columnNames).indexOf("ICON_ID") > -1) {
            columnNames.unshift("IsChecked");
        }

        return columnNames.map(function (name) {


            if (name.toUpperCase() === "ISCHECKED") {
                return {
                    selectable: true, width: 50, filterable: false, hidden: isHiddenCheck
                };
            }
            else if (name.toUpperCase() === "MODIFICADO" || name.toUpperCase() === "CREADO" || name.toUpperCase() === "INGRESO") {
                return {
                    field: name,
                    format: "{0:dd/MM/yyyy HH:mm:ss}",
                    width: 150,
                    sortable: true, filterable: true, resizable: true
                }
            }
            else if (name.toUpperCase().indexOf("FECHA") != -1) {
                return {
                    field: name,
                    format: "{0:dd/MM/yyyy}",
                    width: 150,
                    sortable: true, filterable: true, resizable: true
                }
            }
            else if (name.toUpperCase() === "TAREA") {
                return {
                    field: name,
                    format: "",
                    width: 150,
                    sortable: true, filterable: true, resizable: true
                }
            }
            else if (name.toUpperCase() === "ICON") {
                return {
                    field: name.trim(), format: (isDateField[name] ? "{0:dd/MM/yyyy}" : ""), width: 60, template: "<div class='customer-photo'" +
                        "style='background-image: url(" + "../../Content/Images/icons/#:ICON_ID#.png);'></div>",
                    sortable: false, filterable: false, columnMenu: false
                };
            }

            else if (name.toUpperCase() === "Marks") {
                return {
                    field: name.trim(), format: "", width: 60, template: "<div class='marks-icons'></div>",
                    sortable: true, filterable: true, columnMenu: false
                };
            }

            else if (name.toUpperCase() === "StakeHolders") {
                return {
                    field: name.trim(), format: "", width: 60, template: "<div  class='stakeHolders-icons'></div>",
                    sortable: true, filterable: true, columnMenu: false
                };
            }


            else if (name.toUpperCase() == "ID_PEDIDOS_DE_FONDOS") {
                return {
                    field: name.trim(),
                    template: "<div style='font-family:" + 'Libre Barcode 39' + "'>#:Nro_Despacho#</div>",
                    sortable: true, filterable: true, columnMenu: false
                };
            }



            else if (name.toUpperCase() == "DOC_ID" || name.toUpperCase() == "DOC_TYPE_ID" || name.toUpperCase() == "STR_ENTIDAD" || name.toUpperCase() == "THUMB" || name.toUpperCase() == "FULLPATH" || name.toUpperCase() == "ICON_ID" || name.toUpperCase() == "USER_ASIGNED" || name.toUpperCase() == "EXECUTION" || name.toUpperCase() == "DO_STATE_ID" || name.toUpperCase() == "TASK_ID" || name.toUpperCase() == "RN" || name.toUpperCase() == "LEIDO" || name.toUpperCase() == "WORKFLOW" || name.toUpperCase() == "PROCESO" || name.toUpperCase() == "DISK_GROUP_ID"
                || name.toUpperCase() == "DISK_GROUP_ID"
                || name.toUpperCase() == "DISK_GROUP_ID" || name.toUpperCase() == "DISK_GROUP_ID" || name.toUpperCase() == "VOL_ID"
                || name.toUpperCase() == "OFFSET" || name.toUpperCase() == "PLATTER_ID" || name.toUpperCase() == "SHARED" || name.toUpperCase() == "VER_PARENT_ID"
                || name.toUpperCase() == "ROOTID" || name.toUpperCase() == "DISK_VOL_PATH" || name.toUpperCase() == "STEP_ID"
                || name.toUpperCase() == "WORK_ID" || name.toUpperCase() == "DISK_VOL_ID" || name.toUpperCase() == "VERSION" || name.toUpperCase() == "NUMEROVERSION" || name.toUpperCase() == "DOC_FILE" || name.toUpperCase() == "name.toUpperCase()" || name.toUpperCase() == "ASIGNEDTO" || name.toUpperCase() == "STEP" || name.toUpperCase() == "THUMBIMG" || name.toUpperCase() == "SHOWUNREAD"
                || (name.toUpperCase().startsWith("I") && isNumeric(name.slice(1, name.length - 1)))) {
                return {
                    field: name, format: (isDateField[name] ? "{0:dd/MM/yyyy}" : ""), hidden: true, filterable: false, width: 0
                }
            } else if (name == "ORIGINAL") {
                return {
                    field: name, format: (isDateField[name] ? "{0:dd/MM/yyyy}" : ""), width: 150, filterable: true, sortable: true, resizable: true
                }

            }
            else {

                return {

                    field: name, format: (isDateField[name] ? "{0:dd/MM/yyyy}" : ""), width: 150,
                    sortable: true, filterable: true, resizable: true

                }
            }
        })
    }


    function getColumnsArrayFromResult(results) {
        var columns = [];
        var entityId = 0;
        for (var r in results) {
            let currentResult = results[r];
            if (entityId != currentResult.DOC_TYPE_ID) {
                entityId = currentResult.DOC_TYPE_ID;
                for (var name in currentResult) {
                    if (columns.indexOf(name) == -1)
                        columns.push(name.trim().replace(/\s/g, "").replace(/\s/g, "_"));
                }
            }
        }
        return columns;
    }
    function generateModel(results) {
        var model = {};
        var fields = {};

        var entityId = 0;
        for (var r in results) {
            let currentResult = results[r];
            if (entityId != currentResult.DOC_TYPE_ID) {
                entityId = currentResult.DOC_TYPE_ID;

                for (var property in currentResult) {

                    property = property.trim().replace(/\s/g, "").replace(/\s/g, "_");

                    if (property == ("ID")) {
                        model["id"] = property;
                    }
                    var propType = typeof r[property];

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

                            if (property.indexOf("Fecha") != -1 || property.indexOf("Hora") != -1 || property == "CREADO" || property == "MODIFICADO" || property == "INGRESO") {
                                fields[property] = {
                                    type: "date"
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
                    } else if (propType === "object") {

                        if (property.indexOf("Fecha") != -1 || property.indexOf("Hora") != -1 || property == "CREADO" || property == "MODIFICADO" || property == "INGRESO") {
                            fields[property] = {
                                type: "date"
                            };

                        } else {

                            fields[property] = {
                                type: "string"
                            };
                        }

                    } else {
                        if (property.indexOf("Fecha") != -1 || property.indexOf("Hora") != -1 || property == "CREADO" || property == "MODIFICADO" || property == "INGRESO") {
                            fields[property] = {
                                type: "date"
                            };

                        } else {

                            fields[property] = {
                                type: "string"
                            };
                        }
                    }
                }
            }
        }
        model.fields = fields;
        return model;
    }
    function getTypeColumn(property, sampleDataItem) {
        var typeColumn;

        property = property.trim().replace(/\s/g, "").replace(/\s/g, "_");

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

                if (property.indexOf("Fecha") != -1 && property.indexOf("Hora") != -1) {
                    typeColumn = {
                        type: "date"
                    };

                } else {

                    typeColumn = {
                        type: "date"
                    };
                    isDateField[property] = true;
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
            console.error(e);
        }

        return width;

    }

    function onDataBound(arg) {

        var gridElement = $("#" + $scope.gridIndex);

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
        $scope.associatedResults;

        var selected = $.map(this.select(), function (item) {
            //$scope.checkedIds = [];
            for (i = 0; i < arg.sender._data.length - 1; i++) {
                $scope.checkedIds.push(arg.sender._data[i].RN);
            }

            $scope.GetTaskDocument($scope.checkedIds);
            var IdInfo = {}; for (var i = 0; i < arg.sender.tbody[0].children.length; i++) {
                if (arg.sender.tbody[0].children[i].classList.contains("k-state-selected")) {
                    IdInfo.Docid = parseInt($scope.associatedResults[i].DOC_ID);
                    IdInfo.DocTypeid = parseInt($scope.associatedResults[i].DOC_TYPE_ID);
                    $scope.attachsIds.push(IdInfo);
                }
            }
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
        if ($scope.associatedResults == null || $scope.associatedResults == "" || $scope.associatedResults.length == 0) {
            console.log("No se encontraron resultados");
            return;
        }
        else {
            //thumbs         
            $scope.Search = getBCHistoryearch();
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
        if ($scope.associatedResults == null || $scope.associatedResults == "" || $scope.associatedResults.length == 0) {
            console.log("No se encontraron resultados");
            return;
        }
        else {
            //Preview         
            $scope.Search = getBCHistoryearch();
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
            if (window.localStorage) {
                var localGroups = window.localStorage.getItem("localGroups" + GetUID());
                if (localGroups != undefined && localGroups != null && localGroups.length > 0) {
                    groups = localGroups;
                    return groups;

                }
            }
        } catch (e) {
            console.error(e);
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
                            if (window.localStorage) {
                                window.localStorage.setItem("localGroups" + GetUID(), data);
                            }
                        } catch (e) {
                            console.error(e);
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
            $(".k-state-selected").first().removeClass("k-state-selected");
            $scope.mulipleSelectionGridActive = true;
            //grid.showColumn(0);
        }
        $scope.refreshGrid();
        window.localStorage.setItem("MultipleSelection", $scope.mulipleSelectionGridActive.toString());
    }

    //Limpia filas seleccionadas de grilla asociados obteniendo parametro del window.localStorage
    $scope.ClearSelection = function () {
        try {
            if (window.localStorage.getItem("MultipleSelection") == "true") {
                $("span." + $scope.thumbSelectionClass + ".glyphicon-ok-sign").click();
                $scope.mulipleSelectionGridActive = false;
                grid.hideColumn(0);
                grid.clearSelection();
                $scope.resetMultipleSelection();
                $scope.checkedIds = [];
                $scope.attachsIds = [];
            } else if (window.localStorage.getItem("MultipleSelection") == "false") {
                $scope.mulipleSelectionGridActive = true;
                grid.showColumn(0);
            }
            $scope.refreshGrid();
        } catch (e) {
            console.error(e);
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
        var result = window.localStorage.getItem("MultiSelectionIsActive");
        return result;
    }

    //Refresh de grillas
    $scope.refreshGrid = function () {
        
        $scope.loading = true;
        $scope.LoadResults($scope.parentResultId, $scope.partentEntityId, $scope.entities, $scope.parentTaskId, $scope.zvar, $scope.onlyimportants);
        $scope.removeCaller();
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
            var result;
            for (var i = 0; i < $scope.Search.SearchResults.length; i++) {
                if ($scope.Search.SearchResults[i].DOC_ID == arg) {
                    result = $scope.Search.SearchResults[i];
                }
            }
            var userid = GetUID();
            var stepid = result.STEP_ID;
            var taskId = result.TASK_ID;

            if (stepid == undefined) {
                stepid = result.Step_Id;
            }
            if (taskId == undefined) {
                taskId = result.Task_Id;
            }

            var Url = "../WF/TaskSelector.ashx?" +
                'doctype=' + result.DOC_TYPE_ID +
                '&docid=' + result.DOC_ID +
                '&taskid=' + taskId +
                '&wfstepid=' + stepid +
                "&userId=" + GetUID();

            if ($scope.openmode == "view") {
                OpenDocTask3(taskId, result.DOC_ID, result.DOC_TYPE_ID, null, result.Proceso, Url, userid, stepid, 0);
            }
            else {
                Url = (thisDomain + "/Services/GetDocFile.ashx?DocTypeId=" + result.DOC_TYPE_ID + "&DocId=" + result.DOC_ID + "&UserID=" + userid + "&ConvertToPDf=true");

                if ($scope.previewMode == "previewV") {
                    swal({
                        title: "Hay modificaciones en la tarea actual.",
                        text: "Desea guardar los cambios realizados?",
                        icon: "warning",
                        buttons: ["Si", "No", "Cancel"],
                        dangerMode: true,
                    })
                        .then((willDelete) => {
                            if (willDelete) {
                                //GuardarCambios();
                                SwitchZambaAplication(result, Url, userid);
                            } else if ("Nn") {
                                SwitchZambaAplication(result, Url, userid);
                            } else if ("Cancel") {

                            }
                        });
                }

                //SwitchZambaAplication(result, Url, userid);
            }

            try {
                // Notificar la lectura del documento
                //result.LEIDO == 0 && result.USER_ASIGNED == userid
                if (result.LEIDO == 0 && result.USER_ASIGNED == userid) {

                    //if (result.USER_ASIGNED == userid) {
                    var url = ZambaWebRestApiURL + "/search/NotifyDocumentRead?" + jQuery.param({ UserId: userid, DocTypeId: result.DOC_TYPE_ID, DocId: result.DOC_ID });
                    $.post(url, function myfunction() {
                    });
                    //}

                    // Actualiza estado de leido en thumbs y preview
                    result.LEIDO = 1;

                    // Actualizar estado de tareas en la grilla
                    $scope.refreshGrid();

                    $scope.$applyAsync();

                }
            } catch (e) {
                console.error(e);
            }


        } else {
            $scope.onSelectionMode = true;
        }
    };

    function SwitchZambaAplication(result, Url, userid) {
        switch (zambaApplication) {
            case "ZambaWeb":
                OpenDocTask3(result.TASK_ID, result.DOC_ID, result.DOC_TYPE_ID, false, "Reemplazar", Url, userid, 0);
                $('#Advfilter1').modal("hide");
                break;
            case "ZambaWindows":
            case "ZambaHomeWidget":
            case "ZambaQuickSearch":
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
    }

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
                    //                    setInterval(function () {
                    var actualizada = toastr.info("Se ha agregado una nueva tarea");
                    toastr.options.timeOut = 3000;
                    //                  }, 300000);
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
        var resultIds = [];
        var resultIds = [];
        if ($scope.checkedIds != null && $scope.checkedIds.length > 0) {
            var ruleIds = getRuleIdFromdictionaryByName(ruleName);
            var resultIds = JSON.stringify($scope.attachsIds); if ($scope.Zvars != undefined && $scope.Zvar == null) {
                var resultRule = ruleExecutionService.executeRuleWithZvars(ruleIds, resultIds, $scope.Zvars); $scope.EvaluateRuleExecutionResult(JSON.parse(resultRule))
            } else {
                ruleExecutionService.executeRule(ruleIds, resultIds);
            }
        } else {
            swal("No se a podido ejecutar la regla", "Seleccione al menos una tarea.", "warning");
        }
    }

    $scope.RefreshVisibleGrid = function () {
        try {
            $scope.refreshGrid();
            LoadGrilla_ForTableModalShow();
            $scope.setSelectionMode();
        } catch (e) {
            console.error(e);
        }
    }

    function LoadGrilla_ForTableModalShow() {
        try {
            if (document.getElementById("tableModalShow") != undefined) {
                var DoTableProController;
                $("#tableModalShow").each(function (item, elem) {
                    DoTableProController = angular.element((elem)).scope();
                    DoTableProController.RefreshGrid();
                });
            }
        } catch (e) {
            console.error(e);
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

    $scope.thumbZoom = function (result,event) {
        
        var t = $(event.target);
        //var path = '../../app/Grid/Views/zoomModal.html';
        //$scope.showModalWithGridData(result, path, "zoom");
        var rg = $(event.target).parents(".resultsGrid");
        var dpt = $(t).parent().parent().children(".document-photo-thumbs");
        var $detailsButton = $($(t).parents(".resultsGrid")[0]).find(".glyphicon-info-sign");
        var $selectionButton = $($(t).parents(".resultsGrid")[0]).find(".glyphicon-ok-circle");
        if ($(t).attr("mode") === "normal") {

            $detailsButton.hide();
            $selectionButton.hide();
            $(t).parent().css("top", "-12px");
            $(t).attr("class", "glyphicon glyphicon glyphicon-remove");
            $(".resultsGrid.ng-scope").hide();
            $(t).attr({ "mode": "zoom", "src": "../../GlobalSearch/Images/close.png" });

            //se agrego ya que al tener una resolucion chica aparece un scroll que no debe aparecer
            $("#resultsGridSearchBoxThumbs").css("overflow-y", "hidden")

            rg.css("max-width", "500px").show();
            dpt.css("max-height", "480px").show();
            rg.animate({ width: "300px" }, 300);
            dpt.animate({ width: "300px" }, 300);
            
        }
        else {
            $detailsButton.show();
            $selectionButton.show();
            $(t).parent().css("top", "auto");
            $(t).attr("class", "glyphicon glyphicon-zoom-in");
            $(".resultsGrid.ng-scope").show();
            $(t).attr({ "mode": "normal", "src": "../../GlobalSearch/Images/word.png" });
            rg.css("max-width", "150px");
            dpt.css("max-height", "225px");
            dpt.animate({ width: "100%" }, 300);

            $("#resultsGridSearchBoxThumbs").css("overflow-y", "visible");

        }

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
            || name == "NUMEROVERSION" || name == "DOC_FILE" || name == "AsignedTo" || name == "Step"
            || name == "ThumbImg" || name == "ShowUnread" || name == "Icon"
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
                            if ($("#previewGrid_" + $scope.gridIndex)[0] != undefined) {
                                $($("#previewGrid_" + $scope.gridIndex)[0]).css('height', $scope.tableHeight);
                            }
                        } catch (e) {
                            console.error(e);
                        }
                    }
                    catch (error) {
                        console.error(error);
                    }
                }, 300);


            }
            catch (error) {
                console.error(error);
            }
            return url;
        }
    }

    //Establece al viewer de PDF la fila seleccionada previamente.
    $scope.SetSelectFile_InViewer = function (event) {
        var row, rowIndex, dataItem;

        if ($scope.showPreview) {
            if ($(".k-state-selected")[0] != undefined) {
                row = $(".k-state-selected").first();
                rowIndex = row[0].sectionRowIndex;
                dataItem = $("#" + $scope.gridIndex).data("kendoGrid").dataItem(row);
            } else {
                var entities = $("zamba-associated").attr("entities")                //row = $($("#zamba_grid_index_113").find("tr")[1]).first();   //obtiene el "sectionRowIndex" del primer elemento de la grilla.

                if (entities != "") {
                    row = $($("#zamba_grid_index_" + entities).find("tr")[1]).first();   //obtiene el "sectionRowIndex" del primer elemento de la grilla.
                    rowIndex = row[0].sectionRowIndex;
                    dataItem = $("#" + $scope.gridIndex).data("kendoGrid").dataItem(row);

                    SelectFile(row);
                }               
            }

            if (dataItem != undefined && dataItem != null) {
                $scope.previewItem(dataItem, rowIndex, event, $scope.gridIndex);
            } else {
                console.error("No se ha podido obtener la fila deseada.");
            }
        }
        else {
            $(".k-state-selected").first().removeClass("k-state-selected");
            sessionStorage.removeItem("zamba_grid_index_all_" + parent.GetUID());
        }
    }

    //visualiza un archivo anterior en el expediente al hacer scroll hacia arriba sobre la grilla de expedientes.
    $scope.GetPreviousUrl = function (index, Id) {
        //if ($scope.Search == undefined || $scope.Search.SearchResults == undefined) return;
        //index++;
        //var currentresult = $scope.Search.SearchResults[index];
        //$scope.previewItem(currentresult, index, null, Id)

        //var row = $($("#zamba_grid_index_113").find("tr")[index]);
        //SelectFile(row);
    };

    $scope.GetNextUrl = function (index, Id) {
        
        if ($scope.Search == undefined || $scope.Search.SearchResults == undefined) return;
        index++;
        var currentresult = $scope.Search.SearchResults[index];
        $scope.previewItem(currentresult, index, null, Id)
        var entities = $("zamba-associated").attr("entities")
        var row = $($("#zamba_grid_index_" + entities).find("tr")[index + 1]); //+1 por que hay un TR adicional que es la cabecera. ver por consola.
        SelectFile(row);
    };
});



//Selecciona o deselecciona una fila determinada en la grilla.
function SelectFile(row) {
    if (row.hasClass("k-state-selected")) {

    } else {
        $(".k-state-selected").removeClass("k-state-selected");
        row.addClass("k-state-selected");
    }
};

function GetNextUrl(currentIndex, Id) {
    
    return angular.element($('#' + Id)).scope().GetNextUrl(currentIndex, Id);
}

function getBCHistoryearch() {
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




app.directive('zambaAssociated', function ($sce) {
    return {
        restrict: 'E',
        //scope: false,
        //replace: true,
        transclude: true,
        link: function ($scope, element, attributes) {
            // esta zvar no se usa para nada, esta en reload de la pagina analizar de quitar
            $scope.zvar = attributes.zvar;
            if (attributes.onlyimportants != undefined && attributes.onlyimportants != null && attributes.onlyimportants != '' && attributes.onlyimportants == 'true')
                $scope.onlyimportants = true;

            if (attributes.entities != undefined && attributes.entities != null && attributes.entities != '' && attributes.entities != '')
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
            $scope.partentEntityId = GetDocTypeId();
            if ($scope.partentEntityId == null) {
                $scope.parentTaskId = GetTASKID();
            }
            $scope.Zvars = null;
            if (attributes.zVars != null) {
                $scope.Zvars = attributes.zVars;
            }
            $scope.parentResultId = GetDOCID();
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
                console.error(e);
            }
            if (attributes.showPreview != undefined && attributes.showPreview != null) {
                $scope.showPreview = attributes.showPreview;
            }
            else {
                $scope.showPreview = false;
            }
            if ($scope.entities != undefined && $scope.entities != null && $scope.entities != '') {
                $scope.gridIndex = "zamba_grid_index_" + $scope.entities.replace(/\,/g, "_");
            } else if ($scope.zvar != undefined && $scope.zvar != null && $scope.zvar != '') {
                $scope.gridIndex = "zamba_grid_index_" + $scope.zvar.replace(/\,/g, "_");
            } else if ($scope.onlyimportants != undefined && $scope.onlyimportants != null && $scope.onlyimportants == true) {
                $scope.gridIndex = "zamba_grid_index_WF";
            } else {
                $scope.gridIndex = "zamba_grid_index_all";
            }

            $scope.thumbSelectionClass = "thumbSelection_" + $scope.gridIndex;

            if (attributes.orderBy != undefined && attributes.orderBy.split(" ").length == 2) {
                $scope.sortColumn = attributes.orderBy.split(" ")[0];
                $scope.orderBy = attributes.orderBy.split(" ")[1];
            }

            if (attributes.caller != undefined && attributes.caller != null) {
                $scope.Caller = attributes.caller;
            }
            else {
                $scope.showPreview = false;
            }

        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/Grid/Directives/GridDirective.html?v=248')
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

            if ($scope.Caller != null && $scope.Caller != undefined) {
                var callerBtn = document.getElementById($scope.Caller);
                if (callerBtn)
                    callerBtn.addEventListener("click", $scope.refreshGrid, false);
            }
            else {
                setTimeout(function () { $scope.refreshGrid() }, 500);
            }

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

function ValueEdit(e) {
}

//toolbar: kendo.template($('#toolbar-template').html()),
function printGrid() {
    var gridElement = $("#" + $scope.gridIndex),
        printableContent = '',
        win = window.open('', '', 'width=800, height=500, resizable=1, scrollbars=1'),
        doc = win.document.open();
    var htmlStart =
        '<head>' +
        '<meta charset="utf-8" />' +
        '<title>Kendo UI Grid</title>' +
        '<link href="https://kendo.cdn.telerik.com/' + kendo.version + '/styles/kendo.common.min.css" rel="stylesheet" /> ' +
        '<style>' +
        'html { font: 11pt sans-serif; }' +
        '.k-grid { border-top-width: 0; }' +
        '.k-grid, .k-grid-content { height: auto !important; }' +
        '.k-grid-content { overflow: visible !important; }' +
        'div.k-grid table { table-layout: auto; width: 100% !important; }' +
        '.k-grid .k-grid-header th { border-top: 1px solid; }' +
        '.k-grouping-header, .k-grid-toolbar, .k-grid-pager > .k-link { display: none; }' +
        // '.k-grid-pager { display: none; }' + // optional: hide the whole pager
        '</style>' +
        '</head>' +
        '<body>';
    var htmlEnd =
        '</body>';
    var gridHeader = gridElement.children('.k-grid-header');
    if (gridHeader[0]) {
        var thead = gridHeader.find('thead').clone().addClass('k-grid-header');
        printableContent = gridElement
            .clone()
            .children('.k-grid-header').remove()
            .end()
            .children('.k-grid-content')
            .find('table')
            .first()
            .children('tbody').before(thead)
            .end()
            .end()
            .end()
            .end()[0].outerHTML;
    } else {
        printableContent = gridElement.clone()[0].outerHTML;
    }
    doc.write(htmlStart + printableContent + htmlEnd);
    doc.close();
    win.print();
}
$(function () {

    $('#printGrid').click(function () {
        printGrid();
    });
});

