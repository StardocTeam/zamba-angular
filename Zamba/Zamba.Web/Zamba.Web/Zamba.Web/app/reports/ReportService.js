'use strict';
var serviceBase = ZambaWebRestApiURL;

//appTimeLine.constant('ngZambaSettings', {
//    apiServiceBaseUri: serviceBase,
//    clientId: 'ngZambaApp'
//});

app.factory('reportService', ['$http', '$q', function ($http, $q) {


    var reportServiceFactory = {};

    var _getResults = function (reportId) {
        var results = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
                "reportId": reportId
            }

        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/tasks/GetResultsByReportId',
            data: JSON.stringify(genericRequest),

            contentType: "application/json; charset=utf-8",
            async: false,
            success:
            function (data, status, headers, config) {
                results = data;
            }
        });
        return results;
    };


    reportServiceFactory.getResults = _getResults;

    return reportServiceFactory;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("reportService");
}
