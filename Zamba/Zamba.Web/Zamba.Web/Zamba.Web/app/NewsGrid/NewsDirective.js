app.directive('zambaNews', function ($sce) {

    return {

        restrict: 'E',
        transclude: true,
        templateUrl: $sce.getTrustedResourceUrl('../../app/NewsGrid/NewsCardsTemplate.html'),

        link: function ($scope, element, attributes) {
            $scope.init();
        }
    }

});