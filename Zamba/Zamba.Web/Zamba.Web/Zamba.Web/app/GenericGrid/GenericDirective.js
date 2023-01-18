app.directive('zambaGeneric', function ($sce) {

    return {

        restrict: 'E',
        transclude: true,
        templateUrl: $sce.getTrustedResourceUrl('../../app/GenericGrid/GenericCardsTemplate.html?v=248'),

        link: function ($scope, element, attributes) {
            $scope.gridType = attributes.gridtype;
            $scope.init();
            $scope.title = attributes.title;
            $scope.emptyMessage = attributes.emptymessage;
        }
    }

});