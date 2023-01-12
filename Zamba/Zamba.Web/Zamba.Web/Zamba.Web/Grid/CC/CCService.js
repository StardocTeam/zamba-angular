'use strict';
var serviceBase = ZambaWebRestApiURL;
app.constant('ngZambaSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngZambaApp'
});

app.factory('gridService', ['$http', '$q', 'ngZambaSettings', function ($http, $q, ngZambaSettings) {

    //var serviceBase = ngZambaSettings.apiServiceBaseUri;
    var serviceBase = location.origin.trim() + "/ZambaWeb.RestApi/api";
    var gridServiceFactory = {};

    var _getAsociatedResults = function (resultId, entityId, associatedIds) {

        var associatedResults = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "resultId": resultId,
                "entityId": entityId,
                "associatedIds": "106"
            }
        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/search/GetBaremosAsociatedResults',
            data: JSON.stringify(genericRequest),

            contentType: "application/json; charset=utf-8",
            async: false,
            success:
            function (data, status, headers, config) {
                associatedResults = data;
            }
        });
        return associatedResults;
    }

    var _getNewResultId = function (EntityId, userId) {

        var genericRequest = {
            UserId: userId,
            Params: [{ idType: EntityId }]
        };

        return $http.post(serviceBase + '/search/getNewId', genericRequest).then(function (response) {
            return response;
        });
    };

    function getElementFromQueryString(element) {
        var url = window.location.href;
        var segments = url.split("&");
        var value = null;
        segments.forEach(function (valor) {
            if (valor.includes(element)) { value = valor.split("=")[1]; }
        });
        return value;
    }



    var _saveResult = function (result) {

        var parentResultID = getElementFromQueryString("docid");

        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "resultId": result.id,
                "parentResultId": parentResultID,
                "entityId": result.entityId,
                //"reclaimantName": result.reclaimantName,
                //"reclaimentType": result.reclaimentType,
                "Linea": result.data.Linea,
                "Producto": result.data.Producto,
                "Moneda": result.data.Moneda,
                "PrecioUnitario": result.data.PrecioUnitario,
                "Unidad": result.data.Unidad,
                "Precio": result.data.Precio,
                "Cantidad": result.data.Cantidad,
                "associatedIds": "106",
                "isCC": "true"
            }
        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/search/ModifyAssociatedResult',
            data: JSON.stringify(genericRequest),
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
            function (data, status, headers, config) {
                console.log(data);
            }
        });
    };

    var _insertResult = function (result) {
        if (result.parentResultId == null) {
            result.parentResultId = getElementFromQueryString("docid");
        }
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "entityId": result.entityId,
                "parentEntityId": result.parentEntityId,
                "parentResultId": result.parentResultId,
                "Linea": result.data.Linea,
                "Producto": result.data.Producto,
                "Moneda": result.data.Moneda,
                "PrecioUnitario": result.data.PrecioUnitario,
                "Unidad": result.data.Unidad,
                "Precio": result.data.Precio,
                "Cantidad": result.data.Cantidad,
                "isCC": "true",
                "associatedIds": "106"
            }
        };

        var insertionState = null;
        $.ajax({
            type: "POST",
            url: serviceBase + '/search/InsertAssociatedResult',
            data: JSON.stringify(genericRequest),
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
            function (data, status, headers, config) {
                insertionState = data;
            }
        });
        return insertionState;
    };

    var _loadAttributeList = function (AttributeId, parentValue) {

        return $http.post(serviceBase + '/search/loadAttributeList', AttributeId, parentValue).then(function (response) {
            return response;
        });
    };

    var _deleteResult = function (result) {
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "resultId": result.id,
                "entityId": result.entityId
            }
        };
        //return $http.post(location.origin.trim() + "/ZambaWeb.RestApi/api" + '/search/DeleteAsociatedResult', JSON.stringify(genericRequest))
        //    .then(function (response) {
        //        return response;
        //    });


        $.ajax({
            type: "POST",
            url: serviceBase + '/search/DeleteAsociatedResult',
            data: JSON.stringify(genericRequest),
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
            function (data, status, headers, config) {
                console.log(data);
            }
        });
    };

    var _updateResult = function (resultId, entityId, reclaimantName, reclaimentType) {
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "resultId": resultId,
                "entityId": entityId,
                //"reclaimantName": reclaimantName,
                //"reclaimentType": reclaimentType
                "Linea": result.Linea,
                "Producto": result.Producto,
                "Moneda": result.Moneda,
                "PrecioUnitario": result.PrecioUnitario,
                "Unidad": result.Unidad,
                "Precio": result.Precio,
                "associatedIds": "106"
            }
        };
        return $http.post(location.origin.trim() + "/ZambaWeb.RestApi/api" + '/search/ModifyAssociatedResult', JSON.stringify(genericRequest))
            .then(function (response) {
                return response;
            });
    };

    var _getAssociatedIndex = function (resultId, entityId) {
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "resultId": resultId,
                "entityId": entityId,
                "associatedIds": "106"
            }
        };


        $.ajax({
            type: "POST",
            url: serviceBase + '/search/GetResultIndexs',
            data: JSON.stringify(genericRequest),
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
            function (data, status, headers, config) {
                console.log(data);
            }
        });

    };

    gridServiceFactory.getAsociatedResults = _getAsociatedResults;
    gridServiceFactory.loadAttributeList = _loadAttributeList;
    gridServiceFactory.insertResult = _insertResult;
    gridServiceFactory.saveResult = _saveResult;
    gridServiceFactory.getNewResultId = _getNewResultId;
    gridServiceFactory.deleteResult = _deleteResult;
    gridServiceFactory.updateResult = _updateResult;
    gridServiceFactory.getAssociatedIndex = _getAssociatedIndex;

    return gridServiceFactory;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("gridService");
}

