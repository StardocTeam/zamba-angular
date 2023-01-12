'use strict';
var serviceBase = ZambaWebRestApiURL;

//appTimeLine.constant('ngZambaSettings', {
//    apiServiceBaseUri: serviceBase,
//    clientId: 'ngZambaApp'
//});

app.factory('timelineNewsService', ['$http', '$q', function ($http, $q) {


    var timelineServiceFactory = {};

    var _getResults = function (TimeLineType, DocId, EntityId, AsocNewsIds) {
        var response = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
                "DocId": DocId,
                "EntityId": EntityId,
                "AsocNewsIds": AsocNewsIds
            }

        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/search/getTimeLineNews',
            data: JSON.stringify(genericRequest),

            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    response = data;
                },
            error: function(error) {
                console.log(error);
            } 
        });
        return response;
    };



    timelineServiceFactory.getResults = _getResults;


    return timelineServiceFactory;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("timelineNewsService");
}
