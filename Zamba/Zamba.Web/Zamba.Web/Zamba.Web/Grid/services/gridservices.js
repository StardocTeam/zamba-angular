'use strict';
var serviceBase = ZambaWebRestApiURL.toLowerCase().replace("/api", "/");
app.constant('ngZambaSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngZambaApp'
});

app.factory('gridService', ['$http', '$q', 'ngZambaSettings', function ($http, $q, ngZambaSettings) {

    var serviceBase = ngZambaSettings.apiServiceBaseUri;
    var gridServiceFactory = {};

    var _getAsociatedResults = function (resultId, entityId, AsociatedIds) {
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: [
                { resultId: resultId },
                { entityId: entityId },
                { AsociatedIds: AsociatedIds }
            ]
        }

        return $http.post(serviceBase + 'api/search/getAsociatedResults', JSON.stringify({ genericRequest })).then(function (response) {
            return response;
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

    var _saveResult = function (Result) {

        return $http.post(serviceBase + 'api/search/saveResult', Result).then(function (response) {
            return response;
        });
    };

    var _insertResult = function (Result) {

        return $http.post(serviceBase + 'api/search/insertResult', Result).then(function (response) {
            return response;
        });
    };

    var _loadAttributeList = function (AttributeId, parentValue) {

        return $http.post(serviceBase + 'api/search/loadAttributeList', AttributeId, parentValue).then(function (response) {
            return response;
        });
    };

    gridServiceFactory.getAsociatedResults = _getAsociatedResults;
    gridServiceFactory.loadAttributeList = _loadAttributeList;
    gridServiceFactory.insertResult = _insertResult;
    gridServiceFactory.saveResult = _saveResult;
    gridServiceFactory.getNewResultId = _getNewResultId;

    return gridServiceFactory;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("gridService");
}

//return $http.post(ZambaWebRestApiURL + '/search/DoSearch', $scope.Search).then(function (response) {

//    var SearchResultsObject = JSON.parse(response.data);

//    // Si no trajo resultados
//    if (SearchResultsObject == undefined || SearchResultsObject == null || SearchResultsObject.data == undefined || SearchResultsObject.data.length == 0) {

//    }


//}).then(function onSuccess(data, response) {

//    }).catch(function (data, status, headers, config) {
//    var r = data.data == undefined ? data.message : data.data.ExceptionMessage;
//    console.log(data.message);
//    toastr.options.timeOut = 5000;
//    toastr.error("No se encontraron resultados");
//});