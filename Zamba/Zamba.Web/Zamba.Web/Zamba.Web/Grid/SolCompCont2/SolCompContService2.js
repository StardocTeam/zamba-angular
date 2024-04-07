﻿'use strict';
var serviceBase = ZambaWebRestApiURL;
app.constant('ngZambaSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngZambaApp'
});

app.factory('gridService', ['$http', '$q', 'ngZambaSettings', function ($http, $q, ngZambaSettings) {

  
    var gridServiceFactory = {};
   
    var _getAsociatedResults = function (resultId, entityId, associatedIds) {
        var associatedResults = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "resultId": resultId,
                "entityId": entityId,
                "associatedIds": "18, 16"
            }          
        };
     
     
        $.ajax({
            type: "POST",
            url: serviceBase + '/search/GetRequestAsociatedResults2',
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



    var _saveResult = function (result, resultId, parentResultID) { 

        parentResultID = getElementFromQueryString("docid");



        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "resultId": resultId,
                "parentResultId": parentResultID,
                "entityId": result.entityId,
                //"reclaimantName": result.reclaimantName,
                //"reclaimentType": result.reclaimentType,
                "line": result.data.line,
                "product": result.data.product,
                "unitPrice": result.data.unitPrice,
                "measure": result.data.measure,
                "quantity": result.data.quantity,
                "typeOfCurrency": result.data.typeOfCurrency,
                "price": result.data.price,
                "description": result.data.description,
                "costCenter": result.data.costCenter,
                "delegations": result.data.delegations,
                "isRequest": "true",
                //cambiar 
                "associatedIds": "18, 16"
            }
        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/search/ModifyAssociatedResult2',
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
        if (result.data.price.toString().indexOf(",") != -1 && result.data.price.toString().indexOf(".") != -1) {
            result.data.price = result.data.price.toString().replace(".", "");
        }
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {                
                "parentResultId": result.resultId,
                "entityId": result.entityId,
                "line": result.data.line,
                "product": result.data.product,
                "unitPrice": result.data.unitPrice,
                "measure": result.data.measure,
                "quantity": result.data.quantity,
                "typeOfCurrency": result.data.typeOfCurrency,
                "price": result.data.price, 
                "description": result.data.description,
                "costCenter": result.data.costCenter,
                "delegations": result.data.delegations,
                "isRequest": "true",
                //cambiar
                "associatedIds": "18, 16"
            }
        };
       
        var insertionState = null;
        $.ajax({
            type: "POST",
            url: serviceBase + '/search/InsertAssociatedResult2',
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

   

    var _getAssociatedIndex = function (resultId, entityId) {
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "resultId": resultId,
                "entityId": entityId,
                "associatedIds": "10114"
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
    //gridServiceFactory.updateResult = _updateResult;
    gridServiceFactory.getAssociatedIndex = _getAssociatedIndex;

    return gridServiceFactory;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("gridService");
}
