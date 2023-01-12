var app = angular.module('app', ['ui.bootstrap', 'LocalStorageModule', 'ngSanitize', 'ngEmbed', 'ngAnimate', 'ngMessages']);
app.run(['$http', '$rootScope', function ($http, $rootScope) {


}]);



app.controller('CtrlTimeLine', function ($scope, $filter, $http, timelineService) {

    $scope.Results = [
        {
            Id: 10, date: '12/04/2018 10:23', title: 'Aprobo Factura', description: 'Jorge Aprobo Factura', link: 'http://www.stardoc.com.ar', avatarUrl: '', thumb: '', isImportant: true
        },
        {
            Id: 11, date: '10/04/2018 9:23', title: 'Ingreso Factura', description: 'Jorge Ingreso la Factura', link: 'http://www.stardoc.com.ar', avatarUrl: '', thumb: '', isImportant: true
        },
    ];




    $scope.LoadResults = function (TimeLineType, ResultId, EntityId) {

        var d = timelineService.getResults(TimeLineType, ResultId, EntityId)

        if (d == "") {
            console.log("No se pudo obtener el timeline");
            return;
        }
        else {
            $scope.Results = d.Results;
        }
    };


    // filter users to show
    $scope.filterResult = function (result) {
        return (result.isImportant != undefined || result.isImportant !== false);
    };

    $scope.entityId = 15;

//    $scope.LoadResults('Asignaciones',12,26);

});



app.directive('zambaTimeline', function ($sce) {
    return {
        restrict: 'E',
        scope: false,
        replace: true,
        transclude: true,
        link: function ($scope, element, attributes) {
            $scope.entityId = attributes.entityId;
            $scope.timelineType = attributes.timelineType;
            $scope.docId = attributes.docId;
        },
        templateUrl: $sce.getTrustedResourceUrl('timeline.html'),

    };
})


