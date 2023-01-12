//var app = angular.module('observaciones', []);
var bool = false;

app.controller('ObservacionController', function ($scope, $filter, $http, observacionesServices) {
    

    $scope.LoadResults = function (indexId, entityId, parentResultId, InputObservacion, bool) {
        
        var d = observacionesServices.getResults(indexId, entityId, parentResultId, InputObservacion, bool, $scope.responseProperty);
        //$scope.applyasync();
    };

    $scope.responseProperty = function(data){
        if (data == "") {
            return;
        }
        else {
            $scope.TextareaObservacion = data;
            $scope.$applyAsync();
        }

    }
    $scope.InsertResult = function (indexId, entityId, parentResultId, InputObservacion, TextareaObservacion, bool) {

        var d = observacionesServices.InsertResult(indexId, entityId, parentResultId, InputObservacion, TextareaObservacion, bool);
         //alert(d);
        if (d == "") {
            console.log("No se pudo agregar la observacion");
            return false;
        }
        else {
            $scope.TextareaObservacion = d;
            $scope.InputObservacion = "";
            return true;
        }
    };

    $scope.AgregarObservacion = function () {
        bool = true;
        //alert($scope.TextareaObservacion);
        if ($scope.InputObservacion != "" && $scope.InputObservacion != null) {
            if ($scope.TextareaObservacion == null) {
                $scope.TextareaObservacion = "";
            }
            $scope.InsertResult($scope.indexId, $scope.entityId, $scope.parentResultId, $scope.InputObservacion, $scope.TextareaObservacion, bool);

        } else {
            swal("", "El campo observacion es requerido", "warning");
        }
    }

 
});

app.directive('zambaObservaciones', function ($sce) {
    return {
        restrict: 'E',
        //scope: false,
        //replace: true,
        transclude: true,
        link: function ($scope, element, attributes) {
            $scope.entityId = attributes.entityId === "" ? GetDocTypeId() : attributes.entityId;
            $scope.indexId = attributes.indexId;
			$scope.nombreId = attributes.nombreId;
            var url = window.location.href;
            var segments = url.split("&");
            segments.forEach(function (valor) {
                if (valor.includes("docid") || valor.includes("DocID") ) { $scope.parentResultId = valor.split("=")[1]; }
            }); 
            //$scope.parentResultId = 20455048;
            //alert($scope.parentResultId);
            bool = false;
            //alert($scope.TextareaObservacion);
            $scope.LoadResults($scope.indexId, $scope.entityId, $scope.parentResultId, $scope.InputObservacion, bool);
        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/Observaciones/observaciones.html'),

    }
});
