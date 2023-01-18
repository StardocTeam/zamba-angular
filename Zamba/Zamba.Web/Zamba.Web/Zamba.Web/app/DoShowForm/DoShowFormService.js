'use strict';
var serviceBase = ZambaWebRestApiURL;

app.factory('doShowFormService', ['$http', '$q', function ($http, $q) {
    var doShowFormServices = {};

    var _LoadResult = function (doctypeId, docid, indexId) {
        var response = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
                "doctypeId": doctypeId,
                "docid": docid,
                "indexId": indexId
            }
        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/search/GetValueFromIndex',
            data: JSON.stringify(genericRequest),

            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data) {
                    response = data;
                }
            ,
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Status: " + textStatus); alert("Error: " + errorThrown);
            }

        });
        return response;
    };

    doShowFormServices.LoadResult = _LoadResult

    return doShowFormServices;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("doShowFormServices");
}
