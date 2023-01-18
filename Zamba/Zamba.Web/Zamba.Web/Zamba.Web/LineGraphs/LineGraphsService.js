'use strict';

appGraph.factory('gridService', ['$http', '$q', function ($http, $q) {

    //var serviceBase = ngZambaSettings.apiServiceBaseUri;
    var serviceBase = location.origin.trim() + "/ZambaWeb.RestApi/api";
    var gridServiceFactory = {};
   
    var _getGraphResults = function (resultId, entityId, associatedIds) {

        var associatedResults = null;
        var genericRequest = {
           // UserId: parseInt(GetUID()),
            UserId: parseInt(1154),
            Params: {
                "resultId": resultId,
                "entityId": entityId,
                "associatedId": associatedIds
            }          
        };
     
     
        $.ajax({
            type: "POST",
            url: serviceBase + '/search/GetGraphResults',
            data: JSON.stringify(genericRequest),
           
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
            function (data, status, headers, config) {
                associatedResults =  data;
            }
        });
        return associatedResults;
    }

   

    gridServiceFactory.GetGraphResults = _getGraphResults;

    return gridServiceFactory;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("gridService");
}

