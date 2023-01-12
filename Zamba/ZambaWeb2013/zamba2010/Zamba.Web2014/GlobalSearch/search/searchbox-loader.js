var app = angular.module('app', ['ui.bootstrap', 'zamba-search']);

//Config for Cross Domain
app.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.defaults.useXDomain = true;
    delete $httpProvider.defaults.headers.common['X-Requested-With'];
}
]);
//app.controller('test', ['$http', '$scope', function ($http, $scope) {
//    $scope.tes = 'World';
//}]);




app.controller('appController', ['$http', '$scope', function ($http, $scope) {
    //$http.get(urlGlobalSearch + "Entities").success(function (data) {
    //    $scope.availableSearchParams = data;
    //});

    if (enableGlobalSearch != undefined && enableGlobalSearch) {
        window.load();
        $http({
            method: 'GET',
            url: urlGlobalSearch + "Entities",
            crossDomain: true, // enable this
            dataType: 'json',
            headers: { 'Content-Type': 'application/json' }
        }).
         success(function (data, status, headers, config) {
             window.ready();
             $scope.availableSearchParams = data;
         }).
         error(function (data, status, headers, config) {
             window.ready();
             $scope.message = data;
         });
    }

}]);