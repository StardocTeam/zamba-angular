'use strict';
var serviceBase = ZambaWebRestApiURL;

app.service('LastSearchService', function ($http) {

    var LastSearchServiceFactory = {};

    var _getLastSearch = function (userId, onSucces, onError) {
        var response;
        var url = serviceBase + '/search/GetLastSearchs/';

        var data = {
            UserId: userId,
        };

        $http.post(url, JSON.stringify(data)).then(onSucces, onError);

        return response;
    };




    LastSearchServiceFactory.getLastSearch = _getLastSearch;

    return LastSearchServiceFactory;

});