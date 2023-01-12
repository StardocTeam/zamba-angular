'use strict';
var serviceBase = ZambaWebRestApiURL;


app.factory('docInsertModalServices', ['$http', '$q', function ($http, $q) {

    var docInsertModalServicesFactory = {};

    var _getResults = function (formid) { 
        var response = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
                "formid": formid
            }

        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/search/GetEntityName',
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

    
    docInsertModalServicesFactory.getResults = _getResults;

    return docInsertModalServicesFactory;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("docInsertModalServices");
}
