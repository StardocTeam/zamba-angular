'use strict';
var serviceBase = ZambaWebRestApiURL;
app.constant('ngZambaSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngZambaApp'
});

app.factory('gridEditService', ['$http', '$q', 'ngZambaSettings', function ($http, $q, ngZambaSettings) {

    var serviceBase = ngZambaSettings.apiServiceBaseUri;
    var gridServiceFactory = {};

    var _getAsociatedResultsGrid = function (parentResultId, parentEntityId, AsociatedIds, parentTaskId) {
        var response = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "parentResultId": parentResultId,
                "parentEntityId": parentEntityId,
                "AsociatedIds": AsociatedIds,
                "parentTaskId": parentTaskId
            }

        }

        $.ajax({
            type: "POST",
            url: serviceBase + '/search/getAssociatedEDITResults',
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

    var _getList = function (id, value) {
        var response = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "id": id,
                "value": value,
            }

        }

        $.ajax({
            type: "POST",
            url: serviceBase + '/search/NewgetAssociatedlist',
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

    var _getNewResultId = function (EntityId, userId) {

        var genericRequest = {
            UserId: userId,
            Params: [{ idType: EntityId }]
        };

        return $http.post(serviceBase + '/search/getNewId', genericRequest).then(function (response) {
            return response;
        });
    };

    var _saveResult = function (indexs, entityId, parentResultId, taskId) {

        var response = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: 
            {
                "Indexs": JSON.stringify(indexs),
                "entityId": entityId,
                "resultId": parentResultId,
                "taskId": taskId,
            }

        }


        $.ajax({
            type: "POST",
            url: serviceBase + '/search/setTaskIndexsSaveTable',
            data: JSON.stringify(genericRequest),

            contentType: "application/json; charset=utf-8",
            async: false,
            success: function (data, status, headers, config) {

                    response = data;
            },
            error: function (data) {
                swal("Error en el guardado", "por favor, verifique los campos ingresados!", "error");
            }
        });
        return response;


        //return $http.post(serviceBase + '/search/saveResult', Result).then(function (response) {
        //    return response;
        //});
    };

    var _deleteResult = function ( entityId, parentResultId, taskId) {

        var response = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
                "entityId": entityId,
                "resultId": parentResultId,
                "taskId": taskId,
            }

        }

        $.ajax({
            type: "POST",
            url: serviceBase + '/search/DeleteAsociatedResult',
            data: JSON.stringify(genericRequest),

            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    response = data;
                }
        });
        return response;


        //return $http.post(serviceBase + '/search/saveResult', Result).then(function (response) {
        //    return response;
        //});
    };

    var _insertResult = function (Result, EntityId, parentEntityid, ParantDocId) {

        var response = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
                "result": JSON.stringify(Result),
                "parentEntityid": parentEntityid,
                "ParantDocId": ParantDocId,
                "EntityId": EntityId
            }

        }

        $.ajax({
            type: "POST",
            url: serviceBase + '/search/InsertResultGrid',
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

    var _insertResultOneRegister = function (EntityId, parentEntityid, ParantDocId) {

        var response = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
               
                "parentEntityid": parentEntityid,
                "ParantDocId": ParantDocId,
                "EntityId": EntityId
            }

        }

        $.ajax({
            type: "POST",
            url: serviceBase + '/search/InsertResultGridOneResult',
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

    var _loadAttributeList = function (AttributeId, parentValue) {

        return $http.post(serviceBase + '/search/loadAttributeList', AttributeId, parentValue).then(function (response) {
            return response;
        });
    };

    gridServiceFactory.getAsociatedResultsGrid = _getAsociatedResultsGrid;
    gridServiceFactory.loadAttributeList = _loadAttributeList;
    gridServiceFactory.insertResult = _insertResult;
    gridServiceFactory.insertResultOneRegister = _insertResultOneRegister;
    gridServiceFactory.saveResult = _saveResult;
    gridServiceFactory.getNewResultId = _getNewResultId;
    gridServiceFactory.getList = _getList;
    gridServiceFactory.deleteResult = _deleteResult;

    return gridServiceFactory;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("gridService");
}