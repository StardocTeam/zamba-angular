//var app = angular.module("appBaremos", ["xeditable"]);

//app.run(function (editableOptions) {
//    editableOptions.theme = 'bs3';
//});


app.controller('Ctrl', function ($scope, $filter, $http, gridService) {

    $scope.isMainView = true;
    $scope.isInserting = false;
    $scope.isEditing = false;
    $scope.deleteItems = false;

    $scope.asociatedResults = [];
    $scope.TipoReclamante = [];
    $scope.AttributeLists = ["Conyuge", "Cónyuge por Sí y por hijos", "Concubina", "Concubina por Sí y por sus hijos", "Hijos", "Hermanos", "Padres", "Otros"];
    $scope.asociatedColumns = [{
        Id: 1, Name: 'Tipo de Reclamante', Field: 'Tipo_de_Reclamante', Type: 'string', Visible: true, DropDown: 1, Width: 2,
        List: []
    },
        {
            Id: 2, Name: 'Nombre', Field: 'Nombre', Type: 'string', Visible: true, DropDown: 0, Width: 2,
            List: []
        }];


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
            return;
        }
        else {
            $scope.asociatedResults = associatedResults;
            $scope.asociatedColumns = $scope.asociatedColumns;
        }
        //}).error(function (err) {
        //    console.log(err);
        //    return;
        //});
        }
        setTagValueById("hdnCount", $scope.asociatedResults.length); 
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
            data: {
                resultId: getElementFromQueryString("docid"),
                entityId: 10131,
                reclaimantName: AssociatedResult.NOMBRE,
                reclaimentType: AssociatedResult.TIPO_DE_RECLAMANTE,               
            },
            isDeleted: false,
            isNew: false,
            isEdited: false
        };
        return result;
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
        $scope.AttributeLists = ["Conyuge", "Cónyuge por Sí y por hijos", "Concubina", "Concubina por Sí y por sus hijos", "Hijos", "Hermanos", "Padres", "Otros"];
     
        

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
    $scope.deleteResult = function (resultId) {      
        var filtered = $filter('filter')($scope.asociatedResults, { id: resultId });
        if (filtered.length) {
            filtered[0].isDeleted = true;
            $scope.deleteItems = true;
        }
    };

    // add user

    //$scope.entityId = 15;



    $scope.insertResult = function (){        
        var newObject = GetNewResult();
        newObject.data.reclaimentType = $scope.resultToInsert.reclaimentType; 
        newObject.data.reclaimantName = $scope.resultToInsert.reclaimentName;
        newObject.isNew = true;
        if ($scope.asociatedResults == null || $scope.asociatedResults == undefined) {
            $scope.asociatedResults = [];
        }

        $scope.asociatedResults.push(newObject);      
        newObject = null;
        $scope.resultToInsert.reclaimentType = null;
        $scope.resultToInsert.reclaimentName = null;
        $scope.isMainView = true;
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

    $scope.EditResult = function (result, nombre, tipo) {
        $scope.isEditing = true;
        $scope.isMainView = false;
        $scope.isInserting = false;
        var filtered = $filter('filter')($scope.asociatedResults, { id: result.id });
        if (filtered.length) {
            filtered[0].isEdited = true;
            //nombre, tipo
            //filtered[0].data.reclaimentType = $scope.resultEditing.reclaimantType || '';
            //filtered[0].data.reclaimantName = $scope.resultEditing.reclaimantName || '';    
            filtered[0].data.reclaimentType = tipo || '';
            filtered[0].data.reclaimantName = nombre || ''; 
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
    }

    // cancel all changes
    $scope.cancel = function () {
        $scope.LoadAsociatedResults($scope.resultId, $scope.entityId, $scope.associatedIds);
    };

    // save edits
    $scope.saveTable = function () {           
        var array = [];
        for (var result in  $scope.asociatedResults) {
            
            // actually delete user
            if ($scope.asociatedResults[result].isDeleted) {
                gridService.deleteResult($scope.asociatedResults[result]);
                array.push(result);
            }

            // mark as not new
            if ($scope.asociatedResults[result].isNew) {
                $scope.asociatedResults[result].isNew = false;
                gridService.insertResult($scope.asociatedResults[result].data);
            }

            // mark as not new
            if ($scope.asociatedResults[result].isEdited) {
                $scope.asociatedResults[result].isEdited = false; 
                //gridService.saveResult($scope.asociatedResults[result].data, $scope.asociatedResults[result].id);
                gridService.saveResult($scope.asociatedResults[result].data, $scope.asociatedResults[result].id,$scope.resultId);
            }
        }

      
       
        $scope.isInserting = false;
        $scope.isEditing = false;
        $scope.deleteItems = false;
        $scope.isMainView = true;

        for (var i = 0; i < array.length; i++) {
            $scope.asociatedResults.splice(array[i], 1);
        }

    };

//$scope.LoadAsociatedResults($scope.resultId, $scope.entityId, $scope.AssociatedIds);

    function GetNewResult() {
        var result = {
            id: null,
            data: {
                resultId: getElementFromQueryString("docid"),
                entityId: 10131,
                reclaimantName: '',
                reclaimentType: ''               
            },
            isDeleted: false,
            isNew: true,
            isEdited: false
        };
        return result;
    }

   


});



app.directive('zambaGrid', function ($sce) {
    return {
        restrict: 'E',
        scope: false,
        replace: false,
        transclude: true,
        link: function ($scope, element, attributes) {
            $scope.entityId = attributes.entityId;

            var url = window.location.href;
            var segments = url.split("&");
            var resultId = null;
            segments.forEach(function (valor) {
                if (valor.includes("docid")) { resultId = valor.split("=")[1]; }
            });
            $scope.resultId = resultId;
            //$scope.associatedIds = attributes.associatedIds;
            $scope.associatedIds = "10114";
            $scope.LoadAsociatedResults($scope.resultId, $scope.entityId, $scope.associatedIds);
            $scope.isMainView = true;
            //$scope.indexs = gridServiceFactory.getAssociatedIndex($scope.resultId, $scope.entityId)
        },
        templateUrl: $sce.getTrustedResourceUrl("../../Grid/Baremos/BaremosEditGrid.html")
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



app.controller('ReclamCtrl', function ($scope) {

    $scope.reclamante = {
        Tipo: null,
        Nombre: null
    }

    $scope.reclamantes = [];

    $scope.zamba_remove = function (index) {
        $scope.reclamantes.splice(index, 1);
    };

    $scope.zamba_add_new = function () {
        $scope.reclamantes.push( $scope.reclamante );
    };

    $scope.reclamantes.push( $scope.reclamante );

});

function setTagValueById(tagId, value) {
    $("#" + tagId).val(value);
}

