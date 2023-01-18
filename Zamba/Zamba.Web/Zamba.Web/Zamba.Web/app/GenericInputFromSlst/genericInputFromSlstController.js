//var app = angular.module('observaciones', []);
var bool = false;

app.controller('GenericInputFromSlstController', function ($scope, $filter, $http, $timeout, GenericInputFromSlstServices) {
    
    $scope.selected = undefined;


    $scope.LoadResult = function (entityId, docId, indexId) {

        // esta seccion carga si tiene un valor por defecto ya que el el index_id no existe en el formulario y el fromBrowser no impacta valores previos
        try {
            var ReturnValue = GenericInputFromSlstServices.LoadResult(entityId, docId, indexId);
            if (ReturnValue != "") {
                $scope.selected = ReturnValue.replace(/['"]+/g, '');
            }
            
        } catch (e) {
            console.error(e);
        }
    }



    $scope.LoadResultslst = function () {

        if ($scope.id.includes("zamba_index_")) {
            // esta seccion del codigo es solo para los formularios o donde se vaya a implementar y tenga zamba_index
            $scope.CargarOptionSelect($scope.id);

             
        } else if ($scope.id.includes("ucDocTypesIndexs_")) {
            // esta seccion del codigo es solo para los formularios o donde se vaya a implementar y tenga ucDocTypesIndexs

            var id = parseInt(id.replace("ucDocTypesIndexs_", ""));
            $scope.CargarOptionSelectParaInsert(id);
        }
    };

    $scope.CargarOptionSelect = function (id) {

        
        var FiltaFinal = [];
        var valueInput = $scope.selected;
        id = parseInt(id.replace("zamba_index_", ""));
        var lista = GenericInputFromSlstServices.LoadOptionSelect(id, valueInput, $scope.resultLimitvalue);
        lista = JSON.parse(lista);
        FiltaFinal = [];
        try {
            for (var i = 0; i < lista.length; i++) {
                FiltaFinal.push(lista[i]["Codigo"] + " - " + lista[i]["Descripcion"]);
            }

            $scope.states = FiltaFinal;
            
        } catch (e) {
            console.error(e);
        }
    };



    $scope.CargarOptionSelectParaInsert = function (id) {

        var valueInput = $("#ucDocTypesIndexs_" + id + "").val();
        if (valueInput.length < 2)
            return;
        GenericInputFromSlstServices.LoadOptionSelectServiceForInsert(id, valueInput);
        lista = JSON.parse(lista);
        FiltaFinal = [];
        try {
            for (var i = 0; i < lista.length; i++) {
                FiltaFinal.push(lista[i]["Codigo"] + " - " + lista[i]["Descripcion"]);
            }

            $scope.states = FiltaFinal;

        } catch (e) {
            console.error(e);
        }
    }


 
});

app.directive('genericInputFromSlst', function ($sce) {
    return {
        restrict: 'E',
        transclude: true,
        link: function ($scope, element, attributes) {
            
            $scope.ClassForInputSlst = attributes.myClass;
            $scope.characterLimit = attributes.resultLimitvalue;
            $scope.Localidad = attributes.nameEntity;
            $scope.Context = (attributes.nameContext != undefined) ? attributes.nameContext : "zamba_index_";
            $scope.id = $scope.Context + attributes.id;
            $scope.entityId = attributes.entityId;
            //$scope.disabled = (attributes.disabled != undefined) ? false : true;
            $scope.disabled = false;
            

            var url = window.location.href;
            var segments = url.split("&");
            segments.forEach(function (valor) {
                if (valor.includes("docid")) { $scope.parentResultId = valor.split("=")[1]; }
            })


            if ($scope.parentResultId != undefined) {
                $scope.LoadResult($scope.entityId, $scope.parentResultId, attributes.id);
            }



        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/GenericInputFromSlst/GenericInputFromSlst.html'),

    }
});
