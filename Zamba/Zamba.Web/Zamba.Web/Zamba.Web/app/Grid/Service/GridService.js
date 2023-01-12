'use strict';
var serviceBase = ZambaWebRestApiURL;

app.factory('ZambaAssociatedService', ['$http', '$q', function ($http, $q) {
    var zambaAssociatedFactory = {};

    //ver de poner async con IE
    function _getResults(parentResultId, parentEntityId, associatedIds, parentTaskId, onlyimportants) {

        if (parentEntityId == null) {
            parentEntityId = 0;
        }
        var response = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "parentResultId": String(parentResultId),
                "parentEntityId": String(parentEntityId),
                "parentTaskId": String(parentTaskId),
                "AsociatedIds": String(associatedIds),
                "onlyimportants": String(onlyimportants)
            }
         };

        return $http.post(serviceBase + '/search/getAssociatedResults', JSON.stringify(genericRequest));
    };

    function _getResultsByZvarANdRuleId(userId, ruleId, docId, formVariables,zvar) {

            var deferred = $q.defer();

            var response = null;

            if (formVariables != undefined) {
                var genericRequest = {
                    userId: parseInt(userId),
                    Params: {
                        ruleId: parseInt(ruleId),
                        resultIds: parseInt(docId),
                        FormVariables: formVariables,
                        zvar: zvar
                    }
                };
            } else {
                var genericRequest = {
                    userId: parseInt(userId),
                    Params: {
                        ruleId: parseInt(ruleId),
                        resultIds: parseInt(docId),
                        zvar: zvar
                    }
                };
            }


        $http.post(ZambaWebRestApiURL + "/Tasks/getResultsByZvarANdRuleId", JSON.stringify(genericRequest)).then(function (resp) {
            var data = resp.data;
            deferred.resolve(data);
        }, function (err) {
            deferred.reject(err);
            console.log(err);
        })

        return deferred.promise;
    };

    zambaAssociatedFactory.getResults = _getResults;
    zambaAssociatedFactory.getResultsByZvarANdRuleId = _getResultsByZvarANdRuleId;

    return zambaAssociatedFactory;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("ruleExecutionService");
};