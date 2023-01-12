var app = angular.module('TableReportApp', []);
app.controller('TableReportController', function ($scope, $filter, $http, TableReportService) {
    $scope.LoadResults = function () {

        
        if ($scope.reportId == undefined || $scope.reportId == null) {

            $scope.reportId = parametroURL('id');
        }
        var d = TableReportService.Report($scope.reportId, GetUID());

        $scope.Results = JSON.parse(d);
          
    };
});
app.directive('zambaReport', function ($sce) {
    return {
        restrict: 'E',
        //scope: false,
        //replace: true,
        transclude: true,
        link: function ($scope, element, attributes) {
            $scope.reportId = attributes.reportId;
            $scope.LoadResults();
        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/TableReport/TableReport.html'),

    }
});


