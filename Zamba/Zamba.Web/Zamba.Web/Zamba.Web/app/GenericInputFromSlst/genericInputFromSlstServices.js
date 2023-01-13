'use strict';
//var serviceBase = ZambaWebRestApiURL.toLowerCase().replace("/api", "/");
var serviceBase = ZambaWebRestApiURL;
//appTimeLine.constant('ngZambaSettings', {
//    apiServiceBaseUri: serviceBase,
//    clientId: 'ngZambaApp'
//});

app.factory('GenericInputFromSlstServices', ['$http', '$q', function ($http, $q) {

    var GenericInputFromSlstServices = {};

    var _LoadOptionSelect = function (id, valueInput, resultLimitvalue) {
        var response = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
                "id": id,
                "inputValue": valueInput,
                "LimitTo": resultLimitvalue
            }
        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/search/GetIndexDataSelectDinamic',
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

    var _LoadOptionSelectServiceForInsert = function (id, valueInput) {
        var response = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
                "id": id,
                "inputValue": valueInput
            }
        };
        
        $.ajax({
            type: "POST",
            url: serviceBase + '/search/GetIndexDataSelectDinamic',
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

    
    GenericInputFromSlstServices.LoadOptionSelectServiceForInsert = _LoadOptionSelectServiceForInsert
    GenericInputFromSlstServices.LoadOptionSelect = _LoadOptionSelect;
    GenericInputFromSlstServices.LoadResult = _LoadResult

    return GenericInputFromSlstServices;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("GenericInputFromSlstServices");
}
