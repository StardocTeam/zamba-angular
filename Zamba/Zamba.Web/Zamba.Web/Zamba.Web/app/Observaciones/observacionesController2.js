//var app = angular.module('observaciones', []);
var bool = false;

app.controller('ObservacionController', function ($scope, $filter, $http, observacionesServices) {

    $scope.EstudioEnabled = false;
    $scope.NegociadorEnabled = false;
    $scope.ReferenteEnabled = false;
    $scope.AdministrativoEnabled = false;
    $scope.LiquidacionesEnabled = false;


    $scope.LoadResults = function (indexId, entityId, parentResultId, InputObservacion, bool) {
        
        var d = observacionesServices.getResults(indexId, entityId, parentResultId, InputObservacion, bool);
        //alert(d);
        if (d == "") {
           // console.log("No se pudo consultar la observacion");
            return;
        }
        else {
            //d = JSON.parse(d);
            $scope.TextareaObservacion = d;
        }
    };

    $scope.InsertResult = function (indexId, entityId, parentResultId, InputObservacion, TextareaObservacion, bool) {

        var d = observacionesServices.InsertResult(indexId, entityId, parentResultId, InputObservacion, TextareaObservacion, bool);
         //alert(d);
        if (d == "") {
            console.log("No se pudo agregar la observacion");
            return false;
        }
        else {
            $scope.TextareaObservacion = d;
            $scope.ExecuteRule($scope.ruleId, $scope.parentResultId, InputObservacion, 'textoObservacion');
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


    $scope.ExecuteRule = function (ruleId, resultIds, InputObservacion, varName) {

            var genericRequest = {
                UserId: parseInt(GetUID()),
                Params: {
                    "ruleId": ruleId,
                    "resultIds": resultIds
                }
            };

            $.ajax({
                "async": false,
                "url": ZambaWebRestApiURL + '/tasks/ExecuteTaskRule',
                "method": "POST",
                "headers": {
                    "content-type": "application/json"
                },
                "success": function () {
                    console.log("Accion de obs ejecutada")

                },
                "error": function (error) {
                    console.log("Error al ejecutar accion: " + error)
                },
                "data": JSON.stringify(genericRequest)
            });


        }
});

app.directive('zambaObservaciones', function ($sce) {
    return {
        restrict: 'E',
        //scope: false,
        //replace: true,
        transclude: true,
        link: function ($scope, element, attributes) {
            $scope.entityId = attributes.entityId;
            $scope.indexId = attributes.indexId;
			$scope.nombreId = attributes.nombreId;
            $scope.ruleId = attributes.ruleId;

            if (attributes.userId != undefined) {
                $scope.userId = attributes.userId;
            }

            $scope.EstudioEnabled = attributes.EstudioEnabled;
$scope.NegociadorEnabled = attributes.NegociadorEnabled;
$scope.ReferenteEnabled = attributes.ReferenteEnabled;
$scope.AdministrativoEnabled = attributes.AdministrativoEnabled;
$scope.LiquidacionesEnabled = attributes.LiquidacionesEnabled;


            var url = window.location.href;
            var segments = url.split("&");
            segments.forEach(function (valor) {
                if (valor.includes("docid")) { $scope.parentResultId = valor.split("=")[1]; }
            }); 

            //$scope.parentResultId = 20455048;
            //alert($scope.parentResultId);

            bool = false;
            //alert($scope.TextareaObservacion);
            $scope.LoadResults($scope.indexId, $scope.entityId, $scope.parentResultId, $scope.InputObservacion, bool);
        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/Observaciones/observaciones2.html'),

    }
});





