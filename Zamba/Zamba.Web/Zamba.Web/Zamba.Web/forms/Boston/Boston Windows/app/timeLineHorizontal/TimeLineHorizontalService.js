'use strict';
//var serviceBase = "http://localhost/ZambaWeb.RestApi/api"
var serviceBase = ZambaWebRestApiURL;

app.service('timeLineHorizontalService', function ($http) {


    var timelineHorizontalServiceFactory = {};

    var _getResults = function (UsersOrGroupsIds, fnHandler) {
        var genericRequest = {
            //UserId: 14752,
            UserId: parseInt(GetUID()),
            Params: {
                "usersGroupsIds": UsersOrGroupsIds,
                "GetOneMemberIngroup": true
            }
        };

        $.ajax({
            type: "post",
            url: serviceBase + '/search/GetUsersOrGroupsById',
            data: JSON.stringify(genericRequest),
            contentType: "application/json; charset=utf-8",
            //crossDomain: true,
            success: function (data, status, headers, config) {
                fnHandler(data)
            }
        });
    };
    timelineHorizontalServiceFactory.getResults = _getResults;

    return timelineHorizontalServiceFactory;


});