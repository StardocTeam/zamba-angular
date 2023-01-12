app.directive('zambaHome', function ($sce) {

    return {

        restrict: 'E',
        transclude: true,
        templateUrl: $sce.getTrustedResourceUrl('../../app/Home/HomeCardsTemplate.html'),

        link: function ($scope, element, attributes) {           
            $scope.init();
        }
    }

});