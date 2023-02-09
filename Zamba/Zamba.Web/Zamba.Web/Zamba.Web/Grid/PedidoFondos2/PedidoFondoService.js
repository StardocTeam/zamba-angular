'use strict';
//var serviceBase = ZambaWebRestApiURL.toLowerCase().replace("/api", "/");ZambaWebRestApiURL
var serviceBase = ZambaWebRestApiURL;
app.constant('ngZambaSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngZambaApp'
});

app.factory('gridService', ['$http', '$q', 'ngZambaSettings', function ($http, $q, ngZambaSettings) {

    //var serviceBase = ngZambaSettings.apiServiceBaseUri;
   // var serviceBase = location.origin.trim() + "/ZambaWeb.RestApi/api";
    var gridServiceFactory = {};

    var _getAsociatedResults = function (resultId, entityId, associatedIds) {

        var associatedResults = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "resultId": resultId,
                "entityId": entityId,
                "associatedIds": "2544,1020129"
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
                "favorTo": result.data.favorTo,
                "commitmentNumber": result.data.commitmentNumber,
                "payMethod": result.data.payMethod,
                "cbu": result.data.cbu,
                "cuit": result.data.cuit,
                "concept": result.data.concept,
                "amount": result.data.amount,
                "email": result.data.email,
                "personNumber": result.data.personNumber,
                "associatedIds": "2544,1020129",
                "isPedidoFondo": "true"
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
                "favorTo": result.data.favorTo,
                "commitmentNumber": result.data.commitmentNumber,
                "payMethod": result.data.payMethod,
                "cbu": result.data.cbu,
                "cuit": result.data.cuit,
                "concept": result.data.concept,
                "amount": result.data.amount.replace(".","").replace(",","."),
                "email": result.data.email,
                "personNumber": result.data.personNumber,
                "isPedidoFondo": "true",
                "associatedIds": "2544,1020129"
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

    var _getNewResultId = function (EntityId, userId) {

        var genericRequest = {
            UserId: userId,
            Params: [{ idType: EntityId }]
        };

        return $http.post(serviceBase + 'api/search/getNewId', genericRequest).then(function (response) {
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

    var _loadAttributeList = function (AttributeId, parentValue) {

        return $http.post(serviceBase + 'api/search/loadAttributeList', AttributeId, parentValue).then(function (response) {
            return response;
        });
    };
    
    var _getAssociatedIndex = function (resultId, entityId) {
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "resultId": resultId,
                "entityId": entityId,
                "associatedIds": "2544"
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
    gridServiceFactory.getAssociatedIndex = _getAssociatedIndex;

    return gridServiceFactory;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("gridService");
}

