'use strict';
var serviceBase = ZambaWebRestApiURL;

app.service('BCHistoryService', function ($http) {

    var BCHistoryServiceFactory = {};

    var _getBCHistory = function (userId, searchType, onSucces, onError) {

        var url = serviceBase + '/barcode/GetHistory/';

        var data = {
            UserId: userId,
            SearchType: searchType
        };

        $http.post(url, data).then(onSucces, onError);
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

        var url = serviceBase + '/Tasks/SetBCHistoryRead/';

        var data = {
            DocId: docId,
            DocTypeId: docTypeId
        };

        return $http.post(url, data);
    };


    BCHistoryServiceFactory.getBCHistory = _getBCHistory;
    BCHistoryServiceFactory.getTaskId = _getTaskId;
    BCHistoryServiceFactory.setRead = _setRead;

    return BCHistoryServiceFactory;

});