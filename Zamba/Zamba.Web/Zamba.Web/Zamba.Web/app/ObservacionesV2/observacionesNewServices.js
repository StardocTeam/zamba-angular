'use strict';
var serviceBase = ZambaWebRestApiURL;

app.factory('observacionesNewServices', ['$http', '$q', function ($http, $q) {

    var observacionesServicesFactory = {};

    var _getResults = function (entityId, parentResultId, TipoId, AtributeId) {
        var response = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
                "entityId": entityId,
                "parentResultId": parentResultId,
                "TipoId": TipoId,
                "AtributeId": AtributeId
            }

        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/search/getResultsComentariosObservaciones',
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

    var _InsertResult = function (entityId, parentResultId, InputObservacion, TipoId,AtributeId, bool) {
        var response = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
                "entityId": entityId,
                "parentResultId": parentResultId,
                "InputObservacion": InputObservacion,
                "TipoId": TipoId,
                "AtributeId": AtributeId,
                "bool": bool
            }

        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/search/getAddComentariosObservaciones',
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

    var _migracion = function (AtributeId, entityId) {
        var response = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
                "AtributeId": AtributeId,
                "entityId": entityId

            }

        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/search/getMigracionObservaciones',
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
    observacionesServicesFactory.migracion = _migracion;

   return observacionesServicesFactory;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("observacionesnewservices");
}
