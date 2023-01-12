'use strict';
var serviceBase = ZambaWebRestApiURL;

app.service('GenericService', function ($http) {

    var GenericServiceFactory = {};

    var _getGeneric = function (userId, searchType, gridType,onSucces, onError) {
        var response;
        var url = serviceBase + '/HomeTabs/GetGenericSummary/';

        var data = {
            UserId: userId,
            SearchType: searchType,
            gridType: gridType
        };

        $http.post(url, JSON.stringify(data)).then(onSucces, onError);
        //$.ajax({
        //    type: "POST",
        //    url: url,
        //    data: JSON.stringify(data),
        //    contentType: "application/json; charset=utf-8",
        //    async: true,
        //    success:
        //        function (data, status, headers, config) {
        //            onSucces(data);
        //        },
        //    error:
        //        function (error){
        //            onError(error);
        //        }
        //});

        return response;
    };


    var _getTaskId = function (docId, docTypeId) {

        var url = serviceBase + '/Tasks/GetTaskId/';

        var data = {
            DocId: docId,
            DocTypeId: docTypeId
        };

        return $http.post(url, data);
    };

    var _setRead = function (docId, docTypeId) {

        var url = serviceBase + '/Tasks/SetGenericRead/';

        var data = {
            DocId: docId,
            DocTypeId: docTypeId
        };
        return $http.post(url, data);
    };


    GenericServiceFactory.getGeneric = _getGeneric;
    GenericServiceFactory.getTaskId = _getTaskId;
    GenericServiceFactory.setRead = _setRead;

    return GenericServiceFactory;

});