'use strict';
var serviceBase = ZambaWebRestApiURL;

//appTimeLine.constant('ngZambaSettings', {
//    apiServiceBaseUri: serviceBase,
//    clientId: 'ngZambaApp'
//});

app.factory('TableReportService', ['$http', '$q', function ($http, $q) {


    var TableReportServiceFactory = {};

    var _Report = function (ReportID, UserId) {
        var response = null;
        var genericRequest = {
            UserId: UserId,
            Params:
            {
                "ReportID": ReportID
            }
        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/search/GetResultsByReportIdDoShowTable',
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

    //timelineServiceFactory.getResults = _getResults;
    TableReportServiceFactory.Report = _Report;

    return TableReportServiceFactory;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("TableReportService");
}
