'use strict';
//var serviceBase = ZambaWebRestApiURL.toLowerCase().replace("/api", "/");
var serviceBase = ZambaWebRestApiURL;
//appTimeLine.constant('ngZambaSettings', {
//    apiServiceBaseUri: serviceBase,
//    clientId: 'ngZambaApp'
//});

app.factory('forumServices', ['$http', '$q', function ($http, $q) {

    var forumServicesFactory = {};

    var _getForums = function (entityId, parentResultId) {
        var response = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
                "entityId": entityId,
                "parentResultId": parentResultId,
            }

        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/Forum/GetForums',
            data: JSON.stringify(genericRequest),

            contentType: "application/json; charset=utf-8",
            async: false,
            success:
            function (data, status, headers, config) {
                 var forums = JSON.parse(data);
            }
        });
    };

    var _insertForum = function (entityId, parentResultId, title) {
        var response = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
                "entityId": entityId,
                "parentResultId": parentResultId,
                "title": title,
            }

        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/Forum/insertForum',
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

    var _insertMessage = function (entityId, parentResultId, forumId, message) {
        var response = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
                "entityId": entityId,
                "parentResultId": parentResultId,
                "forumId": forumId,
                "message": message,
            }

        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/Forum/insertForumMessage',
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


    forumServicesFactory.getForums = _getForums;
    forumServicesFactory.insertForum = _insertForum;

   return forumServicesFactory;
}]);


