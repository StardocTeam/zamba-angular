'use strict';
var serviceBase = ZambaWebRestApiURL.toLowerCase().replace("/api", "/");
appNewsGrid.constant('ngZambaSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngZambaApp'
});

appNewsGrid.factory('gridService', ['$http', '$q', 'ngZambaSettings', function ($http, $q, ngZambaSettings) {

    var serviceBase = location.origin.trim() + "/ZambaWeb.RestApi/api";
    var gridServiceFactory = {};

    var _getNewResultId = function (resultId) {
        var NewsResults = null;

        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "docid": resultId                                  
            }
        };


        //$.ajax({
        //    type: "POST",
        //    url: serviceBase + '/search/GetNewsResults',
        //    data: JSON.stringify(genericRequest),

        //    contentType: "application/json; charset=utf-8",
        //    async: false,
        //    success:
        //    function (data, status, headers, config) {
        //        NewsResults = data;
        //    }
        //});
        //return NewsResults;


        $.ajax({
            type: "POST",
            url: serviceBase + '/Tasks/GetNewsResults',
            data: JSON.stringify(genericRequest),
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
            function (data, status, headers, config) {
                NewsResults = data;
            },
            error: function (err, status) {
                console.log(err);

            }
        });
        return NewsResults;
    
    }


 
    gridServiceFactory.getNewResultId = _getNewResultId;
 
    return gridServiceFactory;

}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("gridService");
}






