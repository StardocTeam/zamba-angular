app.directive('zambaLastsearch', function ($sce) {

    return {

        restrict: 'E',
        transclude: true,
        templateUrl: $sce.getTrustedResourceUrl('../../app/LastSearchGrid/LastSearchCardsTemplate.html?v=248'),

        link: function ($scope, element, attributes) {
            $scope.gridType = attributes.gridtype;
            $scope.init();
            $scope.title = attributes.titleBusqueda;
            $scope.emptyMessage = attributes.emptymessage;
        }
    }

});