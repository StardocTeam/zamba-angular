'use strict';
var serviceBase = ZambaWebRestApiURL;

app.factory('DoShowTableServices', ['$http', '$q', function ($http, $q) {

   
    var DoShowTableServiceFactory = {};
    var _getResults = function (ReportID, FormVariables, parentTaskId) {
       
        var response = null;
        //var genericRequest = {

        if (FormVariables.length == 0) {
            var genericRequest = {
                UserId: parseInt(GetUID()),
                //UserId: 1560,
                Params:
                {
                    "ReportID": ReportID,
                    "parentTaskId": parentTaskId
                }
            };
        } else {
            var genericRequest = {
                UserId: parseInt(GetUID()),
                //UserId: 1560,
                Params:
                {
                    "ReportID": ReportID,
                    "FormVariables": FormVariables,
                    "parentTaskId": parentTaskId
                }
            };
        }
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

    var _getResultsOrigin = function (originId, ReportID, FormVariables, parentTaskId, datasourceVariable ) {
        var response = null;

        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
                "originId": originId,
                "ReportID": ReportID,
                "FormVariables": FormVariables,
                "parentTaskId": parentTaskId,
                "datasourceVariable": datasourceVariable
            }
        };
        $.ajax({
            type: "POST",
            url: serviceBase + '/search/GetResultsByReportIdDoShowTablePro',
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
   
    var _DoshowtableInsert = function (RuleId, FormVariables, parentTaskId, SelectedresultList, datasourceColumnId, datasourceColumn) {
        var response = null;

        if (FormVariables.length == 0) {
            var genericRequest = {
                UserId: parseInt(GetUID()),
                //UserId: 1196,
                Params:
                {
                    "RuleId": RuleId,                    
                    "SelectedresultList": SelectedresultList,
                    "parentTaskId": parentTaskId,
                    "datasourceColumnId": datasourceColumnId,
                    "datasourceColumn": datasourceColumn
                }
            };
        } else {
            var genericRequest = {
                UserId: parseInt(GetUID()),
                //UserId: 1196,
                Params:
                {
                    "RuleId": RuleId,
                    "FormVariables": FormVariables,
                    "SelectedresultList": SelectedresultList,
                    "parentTaskId": parentTaskId,
                    "datasourceColumnId": datasourceColumnId,
                    "datasourceColumn": datasourceColumn
                }
            };
        }



        $.ajax({
            type: "POST",
            url: serviceBase + '/search/GetNewInsertDoShowTable',
            data: JSON.stringify(genericRequest),

            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                response = data;
            }
        });
        return response;
        
       // return DoshowtableInsert;
    };
   
    DoShowTableServiceFactory.getResults = _getResults;
    DoShowTableServiceFactory.DoshowtableInsert = _DoshowtableInsert;
    DoShowTableServiceFactory.getResultsOrigin = _getResultsOrigin;

    return DoShowTableServiceFactory;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("DoShowTableServices");
}
