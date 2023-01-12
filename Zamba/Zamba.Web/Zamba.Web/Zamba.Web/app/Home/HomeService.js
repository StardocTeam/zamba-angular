'use strict';
var serviceBase = ZambaWebRestApiURL;

app.service('HomeService', function ($http) {

    var HomeServiceFactory = {};

    var _getTabs = function () {

        var url = serviceBase + '/Home/GetHomeTabs';

        return $http.get(url);
    };

    var _getResults = function () {
        var response = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
           };

        $.ajax({
            type: "POST",
            url: serviceBase + '/Home/GetHomeTabs',
            data: JSON.stringify(genericRequest),

            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    response = data;
                }
        });
        return response;
    };

    HomeServiceFactory.GetTabs = _getTabs;
    HomeServiceFactory.getResults = _getResults;
    return HomeServiceFactory;

});
