'use strict';
var serviceBase = ZambaWebRestApiURL.toLowerCase().replace("/api", "/");

app.constant('ngZambaSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngZambaApp'
});

app.factory('timelineService', ['$http', '$q', 'ngZambaSettings', function ($http, $q, ngZambaSettings) {

    var serviceBase = ngZambaSettings.apiServiceBaseUri;
    var timelineServiceFactory = {};

    var _getResults = function (resultId, entityId, Type) {
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


    timelineServiceFactory.getResults = _getResults;

    return timelineServiceFactory;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("timelineService");
}
