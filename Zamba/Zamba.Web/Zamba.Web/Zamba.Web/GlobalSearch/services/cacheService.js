'use strict';
var serviceBase = ZambaWebRestApiURL.toLowerCase().replace("/api", "/");
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

app.factory('cacheService', ['$http', '$q', 'localStorageService', 'ngAuthSettings', function ($http, $q, localStorageService, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var cacheServiceFactory = {};
    var _CheckLastDesignVersion = function () {

        var version = 0;
        if (localStorage) {
             version = parseInt(localStorage.getItem("StructureVersion"));
        }

        if (isNaN(version) || version == undefined) version = 0; 

        var UserId = parseInt(GetUID());

        $http.post(serviceBase + 'api/Cache/CheckStructure?userId=' + UserId)
            .then(function (response) {
                if (response.data.Success && localStorage && response.data.Data > version) {
                    localStorage.clear();
                    localStorage.setItem("StructureVersion", response.data.Data);
                    window.location.reload(true);
                }
                
            }).catch(function (err, status) {
                console.log(err);
            });
    }

    cacheServiceFactory.CheckLastDesignVersion = _CheckLastDesignVersion;
    return cacheServiceFactory;
}]);

