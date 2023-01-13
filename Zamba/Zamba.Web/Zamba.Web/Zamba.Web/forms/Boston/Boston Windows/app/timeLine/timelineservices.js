'use strict';
var serviceBase = ZambaWebRestApiURL;

//appTimeLine.constant('ngZambaSettings', {
//    apiServiceBaseUri: serviceBase,
//    clientId: 'ngZambaApp'
//});

app.factory('timelineService', ['$http', '$q', function ($http, $q) {

   
    var timelineServiceFactory = {};

    var _getResults = function (TimeLineType, parentDocId, EntityId, ParentEntityId) {
        var response = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
                "parentDocId": parentDocId,
                "EntityId": EntityId,
                "ParentEntityId": ParentEntityId
            }

        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/search/getTimeLineAprobaciones',
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

    var _Report = function (ReportID) {
        var response = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
                "ReportID": ReportID
            }
        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/search/GetResultsByReportIdTimeline',
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

        timelineServiceFactory.getResults = _getResults;
        timelineServiceFactory.Report = _Report;

    return timelineServiceFactory;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("timelineService");
}
