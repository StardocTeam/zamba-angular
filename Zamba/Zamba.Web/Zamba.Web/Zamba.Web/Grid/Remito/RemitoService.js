'use strict';
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
                "associatedIds": "104,86"
            }          
        };
     
     
        $.ajax({
            type: "POST",
            url: serviceBase + '/search/GetRemitoAsociatedResults',
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
                "measure": result.data.measure,
                "quantity": result.data.quantity,                
                "costCenter": result.data.costCenter,
                "delegations": result.data.delegations,
                "isRemito": "true",
                "amountsReceived": result.data.amountsReceived,
                //cambiar 
                "associatedIds": "104,86"
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

   

    var _loadAttributeList = function (AttributeId, parentValue) {

        return $http.post(serviceBase + 'api/search/loadAttributeList', AttributeId, parentValue).then(function (response) {
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

   
    var _insertResult = function (result) {
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "parentResultId": result.resultId,
                "entityId": result.entityId,
                "line": result.data.line,
                "product": result.data.product,
                "measure": result.data.measure,
                "quantity": result.data.quantity,
                "costCenter": result.data.costCenter,
                "delegations": result.data.delegations,
                "isRemito": "true",
                "amountsReceived": result.data.amountsReceived,
                //cambiar
                "associatedIds": "104, 86"
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
    gridServiceFactory.saveResult = _saveResult;
    gridServiceFactory.insertResult = _insertResult;
    gridServiceFactory.deleteResult = _deleteResult;
    //gridServiceFactory.updateResult = _updateResult;
    gridServiceFactory.getAssociatedIndex = _getAssociatedIndex;

    return gridServiceFactory;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("gridService");
}

