'use strict';
var serviceBase = ZambaWebRestApiURL;

app.factory('ruleExecutionService', ['$http', '$q', function ($http, $q) {
    var ruleExecutionFactory = {};

    function _executeRule(ruleId, resultId) {           
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "ruleId": ruleId,
                "resultId": resultId
            }
        };

        $.ajax({
            "async": false,
            "url": serviceBase + "/tasks/ExecuteTaskRule",
            "method": "POST",
            "headers": {
                "content-type": "application/json"
            },
            "data": JSON.stringify(genericRequest)
        });
    }


    ruleExecutionFactory.executeRule = _executeRule;

    return ruleExecutionFactory;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("ruleExecutionService");
}