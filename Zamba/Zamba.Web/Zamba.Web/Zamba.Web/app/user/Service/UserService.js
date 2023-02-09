'use strict';
var serviceBase = ZambaWebRestApiURL;

app.factory('ZambaUserService', ['$http', '$q', function ($http, $q) {
    var zambaUserFactory = {};

    
    function _getUserPreferences(PreferenceName){
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "PreferenceName": PreferenceName
            }
        };
        return $http.post(serviceBase + '/Account/getUserPreferences', JSON.stringify(genericRequest)).then(function (data) {
            return data.d;
        });
    };

    zambaUserFactory.getUserPreferences = _getUserPreferences;

   
    function _getSystemPreferences(PreferenceName) {
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "PreferenceName": PreferenceName
            }
        };
        return $http.post(serviceBase + '/Account/getSystemPreferences', JSON.stringify(genericRequest)).then(function (data) {
            return data.d;
        });
    };

    zambaUserFactory.getSystemPreferences = _getSystemPreferences;

    function _getUserRight(rightId) {
        var response = null;

        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "userRightKey": rightId
            }
        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/Account/GetUserRight',
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

    zambaUserFactory.getUserRight = _getUserRight;

    return zambaUserFactory;
}]);

