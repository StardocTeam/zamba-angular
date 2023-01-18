//var app = angular.module('observaciones', []);
var bool = false;

app.controller('ObservacionNewController', function ($scope, $filter, $http, observacionesNewServices) {
    

    $scope.LoadResults = function (entityId, parentResultId, TipoId, AtributeId) {
        
        var d = observacionesNewServices.getResults(entityId, parentResultId, TipoId, AtributeId);
        //alert(d);
        if (d == "") {
           // console.log("No se pudo consultar la observacion");
            return;
        }
        else {
            d = JSON.parse(d);
            $scope.TextareaObservacion = d;
            

        }
    };

    $scope.Migracion = function () {
        observacionesNewServices.migracion($scope.AtributeId, $scope.entityId);
        //Elimintar este load, es solo para prueba de migracion
        $scope.LoadResults($scope.entityId, $scope.parentResultId, $scope.TipoId, $scope.AtributeId);
    }

    $scope.ActualizarNew = function() {
        $scope.LoadResults($scope.entityId, $scope.parentResultId, $scope.TipoId, $scope.AtributeId);
    }

    $scope.InsertResults = function (entityId, parentResultId, InputObservacion, TipoId, AtributeId, bool) {

        try {
            if (InputObservacion != "") {
                observacionesNewServices.InsertResult(entityId, parentResultId, InputObservacion, TipoId, AtributeId, bool);
                $scope.ActualizarNew();
                $("#BtnObservacion").val("");
            }
        } catch (e) {
            console.error(e);
            swal("", "Error al Insertar Observacion", "warning");
        }
       
    };

    $scope.AgregarNewObservacion = function () {
        bool = true;
        //alert($scope.TextareaObservacion);
        if ($scope.InputObservacion != "" && $scope.InputObservacion != null) {
            if ($scope.TextareaObservacion == null) {
                $scope.TextareaObservacion = "";
            }
            $scope.InsertResults($scope.entityId, $scope.parentResultId, $scope.InputObservacion, $scope.TipoId, $scope.AtributeId, bool);
            

        } else {
            swal("", "El campo observacion es requerido", "warning");
        }
    }

 
});

app.directive('zambaObservacionesV2', function ($sce) {
    return {
        restrict: 'E',
        //scope: false,
        //replace: true,
        transclude: true,
        link: function ($scope, element, attributes) {
            $scope.entityId = attributes.entityId === "" ? GetDocTypeId() : attributes.entityId;
            $scope.indexId = attributes.indexId;
            $scope.nombreId = attributes.nombreId;
            $scope.TipoId = attributes.tipoId;
            $scope.AtributeId = attributes.atributoId;
            var url = window.location.href;
            var segments = url.split("&");
            segments.forEach(function (valor) {
                if (valor.includes("docid")) { $scope.parentResultId = valor.split("=")[1]; }
            }); 
            //$scope.parentResultId = 20455048;
            //alert($scope.parentResultId);
            bool = false;
            //alert($scope.TextareaObservacion);
            $scope.LoadResults($scope.entityId, $scope.parentResultId, $scope.TipoId, $scope.AtributeId);
        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/ObservacionesV2/observaciones.html'),

    }
});
