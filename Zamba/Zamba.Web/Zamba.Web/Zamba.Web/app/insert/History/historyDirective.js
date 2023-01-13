app.directive('zambaBCHistory', function ($sce) {

    return {

        restrict: 'E',
        transclude: true,
        templateUrl: $sce.getTrustedResourceUrl('../../app/insert/history/BCHistoryTemplate.html'),

        link: function ($scope, element, attributes) {
            $scope.init();
        }
    }

});