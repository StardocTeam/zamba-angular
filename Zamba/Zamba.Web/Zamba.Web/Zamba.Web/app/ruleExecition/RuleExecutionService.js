'use strict';
var serviceBase = ZambaWebRestApiURL;

app.factory('ruleExecutionService', ['$http', '$q', function ($http, $q) {
    var ruleExecutionFactory = {};

    function _executeRule(ruleId, resultIds) {           
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "ruleId": ruleId,
                "resultIds": resultIds
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

    function _executeRuleWithZvars(ruleId, resultIds, zvars) {
        var result = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "ruleId": ruleId,
                "resultIds": resultIds,
                "zvars": zvars
            }
        }; $.ajax({
            "async": false,
            "url": serviceBase + "/tasks/ExecuteTaskRule",
            "method": "POST",
            "headers": {
                "content-type": "application/json"
            },
            "data": JSON.stringify(genericRequest),
            "success": function (response) {
                result = response;
            }
        }); return result;
    }

    function _getRuleNames(ruleIds) {
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: { "ruleIds": ruleIds }
        };
        var names = null;
        $.ajax({
            "async": false,
            "url": serviceBase + "/tasks/GetRuleNames",
            "method": "POST",
            "headers": {
                "content-type": "application/json"
            },
            "data": JSON.stringify(genericRequest),
            "success": function (response) {
                names = response;
            }
        });     



        return names;
    }

    ruleExecutionFactory.executeRuleWithZvars = _executeRuleWithZvars;
    ruleExecutionFactory.executeRule = _executeRule;
    ruleExecutionFactory.getRuleNames = _getRuleNames;

    return ruleExecutionFactory;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("ruleExecutionService");
}