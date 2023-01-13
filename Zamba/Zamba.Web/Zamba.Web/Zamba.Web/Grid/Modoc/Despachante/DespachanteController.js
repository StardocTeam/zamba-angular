var appEditingGrid = angular.module("DispatcherApp", ["xeditable"]);

appEditingGrid.run(function (editableOptions) {
    editableOptions.theme = 'bs3';
});


appEditingGrid.controller('DispatcherCtrl', function ($scope, $filter, $http, gridService) {

    $scope.isMainView = true;
    $scope.isInserting = false;
    $scope.isEditing = false;
    $scope.deleteItems = false;
    $scope.firtTime = false;

    $scope.asociatedResults = [];


    $scope.asociatedColumns = [
        {
            Id: 1, Name: 'Código de Familia', Field: 'familyCode', Type: 'string', Visible: true, DropDown: 0, Width: 0,
            List: []
        },
        {
            Id: 2, Name: 'Cantidad', Field: 'quatity', Type: 'string', Visible: true, DropDown: 0,
            List: []
        }
    ];

    $scope.MainView = function () {
        return $scope.isInserting == false && $scope.isEditing == false;
    }


    $scope.LoadAsociatedResults = function (parentResultId, parentResultEntityId, associatedIds) {

        var d = gridService.getAsociatedResults(parentResultId, parentResultEntityId, associatedIds);
        if (d != 'null') {

            var associatedResults = getFormattedResults(JSON.parse(d));
            //            .done(function (d) {
            if (associatedResults == "") {
                console.log("No se pudo obtener los asociados");
                $scope.firtTime = true;
                return;
            }
            else {
                $scope.firtTime = false;
                $scope.asociatedResults = associatedResults;
            }
            //}).error(function (err) {
            //    console.log(err);
            //    return;
            //});
        }
    };


    function getFormattedResults(results) {
        var resultsFormated = [];
        var newResult = null;
        if (results.length > 0) {
            for (var result in results) {
                newResult = GetFormattedResult(results[result]);
                resultsFormated.push(newResult);
                newResult = null;
            }
        }
        return resultsFormated;
    };

    function GetFormattedResult(AssociatedResult) {
        var result = {
            id: AssociatedResult.RESULTID,
            resultId: getElementFromQueryString("docid"),
            entityId: $scope.entityId,
            data: {
                familyCode: AssociatedResult.CODIGO_FAMILIA,
                quatity: AssociatedResult.CANTIDAD
            },
            isDeleted: false,
            isNew: false,
            isEdited: false
        };
        return result;
    }

    function getElementFromListoByElementIndex(array, element) {
        var valor = null;
        array.forEach(function (item) {
            if (item.split("-")[0] == String(element)) { valor = item; }
        });
        return valor;
    }


    // $scope.LoadAsociatedResults(18180618, $scope.entityId, $scope.AssociatedIds);

    $scope.getColumn = function (AttributeId) {
        var col = null;
        $($scope.asociatedColumns).each(function (index, column) {
            if (column.Id == AttributeId) {
                col = column;
                return false;
            }
        });
        return col;
    };

    $scope.getColumnDropDown = function (AttributeId) {
        var IsDropDown = false;
        $($scope.asociatedColumns).each(function (index, column) {
            if (column.Id == AttributeId) {
                if (column.DropDown > 0) {
                    IsDropDown = true;
                    return false;
                }
                else {
                    IsDropDown = false;
                    return false;
                }

            }
        });
        return IsDropDown;

    }

    // $scope.AttributeLists = [];
    $scope.loadAttributeList = function (AttributeId) {

        //if ($scope.AttributeLists[AttributeId] != null && $scope.AttributeLists[AttributeId].length > 0) {
        //    return $scope.AttributeLists[AttributeId];
        //}
        //else {
        //    var col = $scope.getColumn(AttributeId);
        //    if (col != null && col.List != undefined && col.List != null && col.List.length > 0) {
        //        return col.List;
        //    }
        //    else {
        //        return gridService.LoadAttributeList(AttributeId).then(function (response) {
        //            $scope.AttributeLists[AttributeId] = response;
        //            return $scope.AttributeLists[AttributeId];

        //        });
        //    }
        //}
        //$scope.AttributeLists = ["Conyuge", "Cónyuge por Sí y por hijos", "Concubina", "Concubina por Sí y por sus hijos", "Hijos", "Hermanos", "Padres", "Otros"];



    };

    //$scope.showGroup = function (user) {
    //    if (user.group && $scope.groups.length) {
    //        var selected = $filter('filter')($scope.groups, { id: user.group });
    //        return selected.length ? selected[0].text : 'Not set';
    //    } else {
    //        return user.groupName || 'Not set';
    //    }
    //};

    $scope.showAttributeDescription = function (Attribute) {
        var selected = [];
        if (Attribute.Data) {
            selected = $filter('filter')($scope.loadAttributeList(Attribute.Id), { Data: Attribute.Data });
        }
        return selected.length ? selected[0].DataDescription : Attribute.Data;
    };

    $scope.checkName = function (data, id) {
        if (id === 2 && data !== 'awesome') {
            return "Username 2 should be `awesome`";
        }
    };

    // filter users to show
    $scope.filterResult = function (result) {
        return (result.isDeleted != undefined || result.isDeleted !== true);
    };

    // mark user as deleted
    $scope.deleteResult = function (result) {
        //var filtered = $filter('filter')($scope.asociatedResults, { id: resultId });
        //if (filtered.length) {
        result.data.isDeleted = true;
        result.data.isEdited = false;
        result.data.isNew = false;
        $scope.deleteItems = true;
        //}
    };

    // add user

    //$scope.entityId = 15;



    $scope.insertResult = function (result) {

        var newObject = GetNewResult();

        newObject.data.familyCode = result.data.familyCode;
        newObject.data.quatity = result.data.quatity;


        newObject.isNew = true;
        if ($scope.asociatedResults == null || $scope.asociatedResults == undefined) {
            $scope.asociatedResults = [];
        }

        $scope.asociatedResults.push(newObject);
        newObject = null;
        result.data.familyCode = null;
        result.data.quatity = null;
    }


    $scope.addResult = function () {

        $scope.isInserting = true;


        //var newObject = GetNewResult();
        //if ($scope.asociatedResults == null || $scope.asociatedResults == undefined) {
        //    $scope.asociatedResults = [];
        //}

        //$scope.asociatedResults.push(newObject);
    };

    $scope.EditResultMode = function () {
        $scope.isInserting = false;
        $scope.deleteItems = false;
        $scope.isMainView = false;
        $scope.isEditing = true;

        for (var result in $scope.asociatedResults) {
            $scope.asociatedResults[result].isDeleted = false;
            $scope.asociatedResults[result].isNew = false;
            $scope.asociatedResults[result].isEdited = true;
        }
    }


    $scope.cancelModification = function () {
        $scope.isEditing = false;
        $scope.isInserting = false;
        $scope.isMainView = true;

        for (var result in $scope.asociatedResults) {
            $scope.asociatedResults[result].isDeleted = false;
            $scope.asociatedResults[result].isNew = false;
            $scope.asociatedResults[result].isEdited = false;
        }
        $scope.LoadAsociatedResults($scope.resultId, $scope.entityId, $scope.associatedIds);
    }

    // cancel all changes
    $scope.cancel = function () {
        $scope.LoadAsociatedResults($scope.resultId, $scope.entityId, $scope.associatedIds);
    };

    // save edits
    $scope.saveTable = function () {
        var array = [];
        for (var result in $scope.asociatedResults) {

            // actually delete user
            if ($scope.asociatedResults[result].isDeleted) {
                gridService.deleteResult($scope.asociatedResults[result]);
                array.push(result);
            }

            // mark as not new
            if ($scope.asociatedResults[result].isNew) {
                $scope.asociatedResults[result].isNew = false;
                gridService.insertResult($scope.asociatedResults[result], $scope.associatedIds);
            }

            // mark as not new
            if ($scope.asociatedResults[result].isEdited) {
                $scope.asociatedResults[result].isEdited = false;
                //gridService.saveResult($scope.asociatedResults[result].data, $scope.asociatedResults[result].id);
                gridService.saveResult($scope.asociatedResults[result], $scope.asociatedResults[result].id, $scope.resultId, $scope.associatedIds);
            }
        }



        $scope.isInserting = false;
        $scope.isEditing = false;
        $scope.deleteItems = false;
        $scope.isMainView = true;


        for (var i = 0; i < array.length; i++) {
            $scope.asociatedResults.splice(array[i], 1);
        }

        $scope.LoadAsociatedResults($scope.resultId, $scope.entityId, $scope.associatedIds);

    };

    //$scope.LoadAsociatedResults($scope.resultId, $scope.entityId, $scope.AssociatedIds);

    function GetNewResult() {
        var result = {
            id: null,
            resultId: getElementFromQueryString("docid"),
            entityId: 106,
            data: {
                familyCode: '',
                quatity: ''
            },
            isDeleted: false,
            isNew: true,
            isEdited: false
        };
        return result;
    }

});



appEditingGrid.directive('zambaGrid', function ($sce) {
    return {
        restrict: 'E',
        scope: false,
        replace: false,
        transclude: true,
        link: function ($scope, element, attributes) {
            $scope.entityId = attributes.entityId;
            $scope.isReadOnly = attributes.readOnly == 'true';

            var url = window.location.href;
            var segments = url.split("&");
            var resultId = null;
            segments.forEach(function (valor) {
                if (valor.includes("docid")) { resultId = valor.split("=")[1]; }
            });
            $scope.resultId = resultId;
            //$scope.associatedIds = attributes.associatedIds;
            $scope.associatedIds = "139072,139600";
            $scope.LoadAsociatedResults($scope.resultId, $scope.entityId, $scope.associatedIds);
            $scope.isMainView = true;
            //$scope.indexs = gridServiceFactory.getAssociatedIndex($scope.resultId, $scope.entityId)
        },
        templateUrl: $sce.getTrustedResourceUrl("../../Grid/Modoc/Despachante/DespachanteEditGrid.html")
    };
})


function getElementFromQueryString(element) {
    var url = window.location.href;
    var segments = url.split("&");
    var value = null;
    segments.forEach(function (valor) {
        if (valor.includes(element)) { value = valor.split("=")[1]; }
    });
    return value;
}



