var app = angular.module('appEntities', ['ui.bootstrap', 'zamba-search']);

//Config for Cross Domain
app.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.defaults.useXDomain = true;
    delete $httpProvider.defaults.headers.common['X-Requested-With'];
}
]);


app.controller('appController', ['$http', '$scope', function ($http, $scope) {   
    window.load();
    var userId = parseInt(GetUID());
    $http({
        method: 'GET',
        url: ZambaWebRestApiURL + "/Search/Entities?UserId=" + userId,
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


}]);


