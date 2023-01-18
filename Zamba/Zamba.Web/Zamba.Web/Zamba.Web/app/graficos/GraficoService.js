'use strict';
var serviceBase = ZambaWebRestApiURL;

app.factory('GraficoService', ['$http', '$q', function ($http, $q) {


    var GraficoServiceFactory = {};
      

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
        

    GraficoServiceFactory.Report = _Report;

    return GraficoServiceFactory;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("GraficoService");
}
