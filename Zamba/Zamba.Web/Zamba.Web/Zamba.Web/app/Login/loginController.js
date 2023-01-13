//var app = angular.module('observaciones', []);
var bool = false;

app.controller('loginController', function ($scope, $filter, $http, $timeout,loginServices) {
    
   
    $scope.LoadResult = function () {


        setTimeout(function () {
            $('#myModal').modal('show');
        }, 3000);
        
        // esta seccion carga si tiene un valor por defecto ya que el el index_id no existe en el formulario y el fromBrowser no impacta valores previos
        try {
           
            
        } catch (e) {
            console.error(e);
        }
    }



   
});

app.directive('genericLogin', function ($sce) {
    return {
        restrict: 'E',
        transclude: true,
        link: function ($scope, element, attributes) {
            
            //$scope.ClassForInputSlst = attributes.myClass;
            //$scope.characterLimit = attributes.resultLimitvalue;
            //$scope.Localidad = attributes.nameEntity;
            //$scope.Context = (attributes.nameContext != undefined) ? attributes.nameContext : "zamba_index_";
            //$scope.id = $scope.Context + attributes.id;
            //$scope.entityId = attributes.entityId;

            var url = window.location.href;
            var segments = url.split("&");
            segments.forEach(function (valor) {
                if (valor.includes("docid")) { $scope.parentResultId = valor.split("=")[1]; }
            })

            $scope.version = localStorage.getItem("ZambaVersion");
            $scope.CurrentDate = new Date();
            //if ($scope.parentResultId != undefined) {
            //    $scope.LoadResult($scope.entityId, $scope.parentResultId, attributes.id);
            //}



        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/Login/login.html'),

    }
});
