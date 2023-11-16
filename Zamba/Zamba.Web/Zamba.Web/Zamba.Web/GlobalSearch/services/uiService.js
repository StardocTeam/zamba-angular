﻿'use strict';
var serviceBase = ZambaWebRestApiURL;
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

app.factory('uiService', ['$http', '$q', 'localStorageService', 'ngAuthSettings', function ($http, $q, localStorageService, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var uiServiceFactory = {};

    var _LoadTaskFiltersConfig = function () {

        var TaskFilterConfig = window.localStorage.getItem("TaskFilterConfig-" + GetUID());

        if (TaskFilterConfig == undefined) {

           
            var genericRequest = {
                UserId: parseInt(GetUID())
            }


            $.ajax({
                type: "POST",
                url: serviceBase + '/search/GetTaskFilterConfig',
                data: JSON.stringify(genericRequest),
                contentType: "application/json; charset=utf-8",
                async: false,
                success:
                    function (data, status, headers, config) {
                        if (data != undefined) {
                            TaskFilterConfig = data;
                            try {
                                window.localStorage.removeItem("TaskFilterConfig-" + GetUID());
                                window.localStorage.setItem("TaskFilterConfig-" + GetUID(), TaskFilterConfig);

                               
                            }
                            catch (e) {
                                console.error(e);
                            }
                        }
                    },
                error: function (err, status) {
                    console.log(err);

                }
            });
            return TaskFilterConfig;
        }
    };

    uiServiceFactory.LoadTaskFiltersConfig = _LoadTaskFiltersConfig;
    return uiServiceFactory;
}]);
