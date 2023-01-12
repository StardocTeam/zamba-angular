//var app = angular.module('observaciones', []);
var bool = false;

app.controller('docInsertModalController', function ($scope, $filter, $http, docInsertModalServices) {
    
    $scope.DocInsertModalName = '';

    $scope.LoadResults = function () {

        try {
            $scope.formid = getElementFromQueryString("formid");
            if ($scope.formid != undefined) {
                var docInsertResult = docInsertModalServices.getResults($scope.formid);
                $scope.DocInsertModalName = 'Agregar ' + docInsertResult;
            }

        } catch (e) {
            console.error(e);
        }
       
       
    };

    $scope.LoadResults();
 
});

app.directive('zambaDocInsertModal', function ($sce) {
    return {
        restrict: 'E',
        transclude: true,
        link: function ($scope, element, attributes) {
         
        }

    }
});
