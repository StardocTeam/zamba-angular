'use strict';
var serviceBase = ZambaWebRestApiURL;

app.service('NewsService', function ($http) {

    var NewsServiceFactory = {};

    var _getNews = function (userId, searchType, onSucces, onError) {

        var url = serviceBase + '/Tasks/GetNewsSummary/';

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

        var url = serviceBase + '/Tasks/SetNewsRead/';

        var data = {
            DocId: docId,
            DocTypeId: docTypeId
        };

        return $http.post(url, data);
    };


    NewsServiceFactory.getNews = _getNews;
    NewsServiceFactory.getTaskId = _getTaskId;
    NewsServiceFactory.setRead = _setRead;

    return NewsServiceFactory;

});