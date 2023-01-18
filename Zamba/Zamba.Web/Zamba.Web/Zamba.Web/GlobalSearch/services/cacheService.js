'use strict';
var serviceBase = ZambaWebRestApiURL;
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

app.factory('cacheService', ['$http', '$q', 'localStorageService', 'ngAuthSettings', function ($http, $q, localStorageService, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var cacheServiceFactory = {};
    var _CheckLastDesignVersion = function () {

        var version = 0;
        if (window.localStorage) {
             version = parseInt(window.localStorage.getItem("StructureVersion"));
        }

        if (isNaN(version) || version == undefined) version = 0; 

        var UserId = GetUID();
        if (UserId != undefined && UserId > 0) {
            UserId = parseInt(UserId);

            $http.post(serviceBase + '/Cache/CheckStructure?userId=' + UserId)
                .then(function (response) {
                    if (response.data.Success && window.localStorage && response.data.Data > version) {
                        clearAllCache(false);
                        window.localStorage.setItem("StructureVersion", response.data.Data);
                        window.location.reload(true);
                    }

                }).catch(function (err, status) {
                    console.log(err);
                });
        }
    }

    cacheServiceFactory.CheckLastDesignVersion = _CheckLastDesignVersion;
    return cacheServiceFactory;
}]);

