'use strict';
var serviceBase = ZambaWebRestApiURL;


app.factory('DoShowTableServices', ['$http', '$q', function ($http, $q) {

   
    var DoShowTableServiceFactory = {};
    var _getResults = function () {
        var response = null;
        var genericRequest = {
            Params: {}
        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/search/getResultsDoShowTable',
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
   
    var _DoshowtableInsert = function (TableList, parentTaskId, NroHojaRuta, RuleId) {
        var response = null;
        TableList = JSON.stringify(TableList)
        TableList = TableList.replace("[", "").replace("]", "");
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
                "TableList": TableList,
                "TaskId": parentTaskId,
                "NroHojaRuta": NroHojaRuta,
                "RuleId": RuleId
            }

        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/search/getInsertDoShowTable',
            data: JSON.stringify(genericRequest),

            contentType: "application/json; charset=utf-8",
            async: false,
            success:
            function (data, status, headers, config) {
                 response = data;
            }
        });
        return response;
        
        return DoshowtableInsert;
    };
   
    DoShowTableServiceFactory.getResults = _getResults;
    DoShowTableServiceFactory.DoshowtableInsert = _DoshowtableInsert;

    return DoShowTableServiceFactory;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("DoShowTableServices");
}
