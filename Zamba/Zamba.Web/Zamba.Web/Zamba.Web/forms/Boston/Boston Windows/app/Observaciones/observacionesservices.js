'use strict';
//var serviceBase = ZambaWebRestApiURL.toLowerCase().replace("/api", "/");
var serviceBase = ZambaWebRestApiURL;
//appTimeLine.constant('ngZambaSettings', {
//    apiServiceBaseUri: serviceBase,
//    clientId: 'ngZambaApp'
//});

app.factory('observacionesServices', ['$http', '$q', function ($http, $q) {

    var observacionesServicesFactory = {};

    var _getResults = function (indexId, entityId, parentResultId, InputObservacion, bool) {
        var response = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
                "indexId": indexId,
                "entityId": entityId,
                "parentResultId": parentResultId,
                "InputObservacion": InputObservacion,
                "bool": bool
            }

        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/search/getAddComentarios',
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

    var _InsertResult = function (indexId, entityId, parentResultId, InputObservacion, TextareaObservacion, bool) {
        var response = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
                "indexId": indexId,
                "entityId": entityId,
                "parentResultId": parentResultId,
                "InputObservacion": InputObservacion,
                "TextareaObservacion": TextareaObservacion,
                "bool": bool
            }

        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/search/getAddComentarios',
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



    observacionesServicesFactory.getResults = _getResults;
    observacionesServicesFactory.InsertResult = _InsertResult;

   return observacionesServicesFactory;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("observacionesservices");
}
