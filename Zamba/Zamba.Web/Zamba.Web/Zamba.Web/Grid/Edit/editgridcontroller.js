app.controller('Ctrl', function ($scope, $filter, $http, $timeout, gridEditService) {

    $scope.LoadAsociatedResults = function (parentResultId, parentEntityId, associatedIds, parentTaskId) {

        var d = gridEditService.getAsociatedResultsGrid(parentResultId, parentEntityId, associatedIds, parentTaskId)

        if (d == "") {
            console.log("No se pudo obtener los asociados");
            return;
        }
        else {
            d = JSON.parse(d);
            $scope.asociatedResults = d;
            $scope.listFuntion();

        }
    };


    $scope.ShowTags = function (value) {
        var a = value;
        return true;
    };

    $scope.PaseNumber = function (value) {
        var result = value;
        result = parseInt(value);
        return result;
    };


    $scope.getColumn = function (id) {

        var col = null;
        $($scope.ListIndex).each(function (index, column) {
            if (column.ID == id) {
                col = column.List;

            }
        });
        return col;
    };

    $scope.RefreshGrid = function () {
        $scope.LoadAsociatedResults($scope.parentResultId, $scope.partentEntityId, $scope.entityId, $scope.parentTaskId);
        $timeout(function () {
            LoadGrillaForm($scope.entityId);
        }, 2000);
    };

    $scope.getColumnDropDown = function (value) {
        var IsDropDown = false;
        try {

            if (value == 0) {
                IsDropDown = false;
                return false;
            }
            if (value == 1 || value == 2) {
                IsDropDown = true;
                return false;
            }

        }
        catch (error) {
            //console.error(error);
        }
        return IsDropDown;
    }

    //Analiza si se muestra los INPUT 
    $scope.TipeNumber = function (type, DropDown) {
        var value = false;
        if (DropDown == 0 && type == 0 || type == 1 || type == 2 || type == 9 || type == 10 || type == 11 || type == 14) {
            value = true;
        }
        if (DropDown != 0 && type == 2) {
            value = false;
        }
        return value;
    }

    $scope.Tipetext = function (type, DropDown) {
        var value = false;
        if (DropDown == 0 && type == 8 || type == 0 || type == 6 || type == 7 || type == 15 || type == 16) {
            value = true;
        }
        return value;
    }

    //PARA IMPLEMENTAR LA CLASE DE EMILIO ZDecimal
    $scope.Moneda = function (type, DropDown) {
        var value = false;
        if (DropDown == 0 && type == 14) {
            value = true;
        }
        return value;
    }

    //Analiza si se muestra los SELECT 
    $scope.TipeSelect = function (type, DropDown) {
        var value = false;
        if (DropDown != 0 && type == 8 || type == 2) {
            value = true;
        }
        if (DropDown == 0 && type == 2) {
            value = false;
        }
        return value;
    }

    //Analiza si se muestra los CALENDAR 
    $scope.TipeCalendar = function (type, DropDown) {
        var value = false;
        if (DropDown == 0 && type == 3 || type == 4 || type == 12 || type == 13 || type == 5) {
            value = true;
        }
        return value;
    }
    // filter users to show
    $scope.filterResult = function (result) {
        return (result.isDeleted != undefined || result.isDeleted !== true);
    };

    // mark user as deleted
    $scope.deleteResult = function (id) {

        for (var i = 0; i < id.length; i++) {

            if (id[i].Column == "DOC_ID") {
                var Compare = id[i].value;
                if (Compare != "") {
                    for (var y = 0; y < $scope.asociatedResults.length; y++) {
                        var ListArray = $scope.asociatedResults[y];
                        for (var j = 0; j < ListArray.length; j++) {
                            if (ListArray[j].value == Compare) {
                                $scope.asociatedResults[y].isDeleted = true;
                                $($(".GridDinamicResult")[0].children[y]).addClass("DisableGrid");
                                return;
                            }
                        }
                    }
                }
            }
        }
    };
    // cancel all changes
    $scope.cancel = function () {
        $scope.LoadAsociatedResults($scope.parentResultId, $scope.partentEntityId, $scope.entityId, $scope.parentTaskId);
    };

    $scope.EditedResults = [];
    $scope.checkAttribute = function (data, result, DropDown) {
        if (DropDown == "1") {
            var a = $scope.selectValue(data, result.ID, DropDown);
            data = a;
            if (data != result.value) {
                result.edited = true;
                result.newValue = data;
                //$scope.EditedResults = [];
                $scope.EditedResults.push(result);
            }
        }
        if (DropDown == "2") {

            for (var i = 0; i < $scope.ListIndex.length; i++) {
                if ($scope.ListIndex[i].ID == result.ID) {
                    var array = $scope.ListIndex[i].List;
                    for (var f = 0; f < array.length; f++) {
                        if (array[f].Codigo == data) {
                            if (array[f].Descripcion != result.value) {
                                result.edited = true;
                                result.newValue = data;
                                //$scope.EditedResults = [];
                                $scope.EditedResults.push(result);
                            }
                        }

                    }
                }
            }
        }
        if (DropDown == 0) {
            if (data != result.value) {
                result.edited = true;
                result.newValue = data;
                //$scope.EditedResults = [];
                $scope.EditedResults.push(result);
            }
        }

        return true;
    };

    $scope.validarEdit = function (result) {
        for (var i = 0; i < result.length; i++) {
            if (result[i].edited != undefined && result[i].edited != "undefined") {
                result.isEdited = "true";
            }
        }
    };

    // save edits
    $scope.saveTable = function () {
        var DocID, DocTypeId, TaskId;
        for (var i = $scope.asociatedResults.length; i--;) {
            var result = $scope.asociatedResults[i];

            $scope.validarEdit(result);
            // actually delete user
            if (result.isDeleted == true) {
                for (var z = 0; z < result.length; z++) {

                    if (result[z].Column == "DOC_ID") {
                        DocID = result[z].value;
                    }
                    if (result[z].Column == "DOC_TYPE_ID") {
                        DocTypeId = result[z].value;
                    }
                    if (result[z].Column == "TASK_ID") {
                        TaskId = result[z].value;
                    }

                    if (DocID != undefined && DocTypeId != undefined && TaskId != undefined) {
                        gridEditService.deleteResult(DocTypeId, DocID, TaskId);
                        DocID = undefined;
                        DocTypeId = undefined;
                        DocTypeId = undefined;
                    }


                }
            }

            // mark as not new
            if (result.isNew == "true") {
                result.isNew == "false";
                gridEditService.insertResult(result, $scope.entityId, $scope.partentEntityId, $scope.parentResultId);
            }

            // mark as not new
            if (result.isEdited == "true" && result.isNew != "true" ) {
                result.isEdited = "false";

                for (var j = 0; j < result.length; j++) {

                    var ListResultSave = [];

                    if (result[j].edited != undefined && result[j].edited != "undefined") {

                        for (var e = 0; e < result.length; e++) {

                            if (result[e].Column == "DOC_ID") {
                                DocID = result[e].value;
                            }
                            if (result[e].Column == "DOC_TYPE_ID") {
                                DocTypeId = result[e].value;
                            }
                            if (result[e].Column == "TASK_ID") {
                                TaskId = result[e].value;
                            }
                        }

                        ListResultSave.push({ ID: result[j].ID, Data: result[j].newValue });
                    }

                    if (DocID != undefined && DocTypeId != undefined && TaskId != undefined) {
                        gridEditService.saveResult(ListResultSave, DocTypeId, DocID, TaskId);
                        DocID = undefined;
                        DocTypeId = undefined;
                        DocTypeId = undefined;
                    }


                }

            }
        }
        $scope.RefreshGrid();
        //$scope.LoadGrillaFormGrid($scope.entityId);
    };

    $scope.selectValue = function (result, id, drop) {
        var arrayVars = [];
        var value = result;

        $($scope.ListIndex).each(function (valor, column) {
            if (column.ID == id) {

                for (var i = 0; i < column.List.length; i++) {
                    if (column.List[i].Codigo == value) {
                        value = column.List[i].Descripcion;
                    }
                }

            }
        });

        return value;

    };

    $scope.selectValueCheked = function (result, id) {
        var arrayVars = [];
        var value = result;
        $($scope.ListIndex).each(function (valor, column) {
            if (column.ID == id) {

                for (var i = 0; i < column.List.length; i++) {
                    if (column.List[i].Descripcion == value) {
                        value = column.List[i].Codigo;
                    }
                }

            }
        });

        return value;
    };

    $scope.Validate = ["STEP_ID", "TASK_ID", "MODIFICADO", "CREADO", "DOC_TYPE_ID", "DOC_ID", "LEIDO", "Tipo Doc Notificacion", "ENTIDAD", "Estado", "Etapa", "Proceso", "Asignado", "ICON_ID", "ORIGINAL", "Tarea", "DOC_ID", "DOC_TYPE_ID", "STR_ENTIDAD", "THUMB", "FULLPATH", "ICON_ID", "USER_ASIGNED", "VOL_ID", "DISK_GROUP_ID", "DISK_GROUP_ID", "EXECUTION", "DO_STATE_ID", "DOC_FILE", "NUMEROVERSION", "VER_PARENT_ID", "SHOWUNREAD", "ORIGINAL", "ASIGNEDTO", "LEIDO", "RN", "TASK_ID", "WORKFLOW", "PROCESO", "ESTADO", "THUMBIMG", "DISK_GROUP_ID", "TAREA", "STEP", "OFFSET", "ROOTID", "WORK_ID", "DISK_VOL_ID", "DISK_GROUP_ID", "PLATTER_ID", "SHARED", "DISK_VOL_PATH", "VERSION", "STEP_ID"];

    /// SE OBTIENEN LAS LISTAS DEL SERVICIO Y SE ALMACENAN EN EL STORAGE 
    // SE ALAMACENAN PARA NO CONSULTAR TANTAS VECES AL SERVICIO
    // NOTA: SOLO ITERA LA PRIMERA FILA DE LA TABLA PATA SACAR LOS RESULTADOS

    $scope.listFuntion = function () {
        $scope.ListIndex = [];
        var asociatedindex = $scope.asociatedResults;
        try {
            for (var i = 0; i < asociatedindex[0].length; i++) {

                if (asociatedindex[0][i]["DropDown"] != 0) {

                    var a = asociatedindex[0][i]["ID"];
                    var value = asociatedindex[0][i]["DropDown"];

                    var b = gridEditService.getList(a, value);
                    b = JSON.parse(b);
                    $scope.ListIndex.push({ ID: a, List: b });
                }
            }
        } catch (e) {
            //console.error(e);
        }
    }

    // add user
    $scope.addUser = function (count) {
        if (count >= 1) {
            $scope.NewListItem = [];
            var ResultAntiguo = $scope.asociatedResults.length;
            for (var i = 0; i < $scope.asociatedResults[0].length; i++) {

                var valorPadre = $("#zamba_index_" + $scope.asociatedResults[0][i].ID).val();
                if (valorPadre == undefined || valorPadre == "undefined") {
                    valorPadre = "";
                }

                $scope.NewListItem.push({
                    Column: $scope.asociatedResults[0][i].Column,
                    DefaultValue: $scope.asociatedResults[0][i].DefaultValue,
                    IconId: $scope.asociatedResults[0][i].IconId,
                    Type: $scope.asociatedResults[0][i].Type,
                    DropDown: $scope.asociatedResults[0][i].DropDown,
                    DropDownList: $scope.asociatedResults[0][i].DropDownList,
                    ID: $scope.asociatedResults[0][i].ID,
                    Len: $scope.asociatedResults[0][i].Len,
                    Name: $scope.asociatedResults[0][i].Name,
                    value: valorPadre,
                });
            }
            $scope.NewListItem;
            $scope.asociatedResults.push($scope.NewListItem);

            if ($scope.asociatedResults.length != ResultAntiguo) {
                for (var i = 0; i < $scope.asociatedResults.length; i++) {
                    if (ResultAntiguo < i + 1) {
                        $scope.asociatedResults[i].isNew = "true";

                    }
                }
            }

            $scope.asociatedResults;
        }
        if (count == 0) {
            try {
                gridEditService.insertResultOneRegister($scope.entityId, $scope.partentEntityId, $scope.parentResultId);
                $scope.RefreshGrid();
            } catch (e) {
                console.log(e);
            } 
        }
    };

});


//Solo permite introducir numeros.
function soloNumeros(e, field) {
    var key = window.event ? e.which : e.keyCode;
    if (key < 48 || key > 57) {
        e.preventDefault();
    }
}



app.directive('zambaEditGrid', function ($sce) {
    return {
        restrict: 'E',
        scope: false,
        replace: true,
        transclude: true,
        link: function ($scope, element, attributes) {
            // $scope.entityId = attributes.entityId;
            $scope.parentResultId = getElementFromQueryString("docid");
            $scope.parentTaskId = 0;
            $scope.partentEntityId = getElementFromQueryString("DocType");
            if ($scope.partentEntityId == null) {
                $scope.parentTaskId = getElementFromQueryString("taskid");
            }
            $scope.entityId = attributes.associatedIds;
            $scope.gridTitle = attributes.gridTitle;

            $scope.LoadAsociatedResults($scope.parentResultId, $scope.partentEntityId, $scope.entityId, $scope.parentTaskId);

        },
        templateUrl: $sce.getTrustedResourceUrl('../../Grid/Edit/editgrid.html'),

    };
})


