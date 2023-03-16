'use strict';
var serviceBase = ZambaWebRestApiURL;

app.factory('ZambaTaskService', ['$http', '$q', function ($http, $q) {
    let zambaTaskFactory = {};

    //ver de poner async en IE
    function executeTaskRule(ruleId, resultIds, formVars) {
        let response = null;
        let genericRequest;

        if (formVars == undefined) {
            genericRequest = {
                UserId: parseInt(GetUID()),
                Params: {
                    "ruleId": ruleId,
                    "resultIds": resultIds,
                    "userid": GetUID()
                }
            };
        } else {
            genericRequest = {
                UserId: parseInt(GetUID()),
                Params: {
                    "ruleId": ruleId,
                    "resultIds": resultIds,
                    "userid": GetUID(),
                    "FormVariables": formVars
                }
            };
        }
        return $http.post(serviceBase + '/tasks/ExecuteTaskRule', JSON.stringify(genericRequest));
    };


    function executeRuleForTask(ruleId, itemResults, formVars) {
        let response = null;
        let genericRequest;
        debugger;

        if (formVars == undefined) {
            genericRequest = {
                UserId: parseInt(GetUID()),
                Params: {
                    "ruleId": ruleId,
                    "itemResults": JSON.stringify(itemResults),
                    "userid": GetUID()
                }
            };
        } else {
            genericRequest = {
                UserId: parseInt(GetUID()),
                Params: {
                    "ruleId": ruleId,
                    "itemResults": JSON.stringify(itemResults),
                    "userid": GetUID(),
                    "FormVariables": formVars
                }
            };
        }
        debugger;

        return $http.post(serviceBase + '/tasks/ExecuteRuleForTask', JSON.stringify(genericRequest));
    };


    function _executeAction_onItems(ruleId, resultIds, formVars) {
        resultIds = JSON.stringify(resultIds);
        resultIds = resultIds.replace(/[([^\]^"]*/g, "");
        let response = null;
        let genericRequest;

        if (formVars == undefined) {
            genericRequest = {
                UserId: parseInt(GetUID()),
                Params: {
                    "ruleId": ruleId,
                    "resultIds": resultIds,
                    "userid": GetUID()
                }
            };
        }
        else {
            genericRequest = {
                UserId: parseInt(GetUID()),
                Params: {
                    "ruleId": ruleId,
                    "resultIds": resultIds,
                    "userid": GetUID(),
                    "FormVariables": formVars
                }
            };
        }

        return $http.post(serviceBase + '/tasks/ExecuteTaskRule', JSON.stringify(genericRequest));
    };

    function MarkAsFavorite(TaskId, UserId, Mark) {
        let genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "TaskId": TaskId,
                "Mark": Mark
            }
        };
        return $http.post(serviceBase + '/tasks/MarkAsFavorite', JSON.stringify(genericRequest));
    };

    //Obtiene las RuleActions para el usuario conectado.
    function _getResultsGridActions(userId) {
        let response = null;
        let genericRequest = {
            UserId: parseInt(userId)
        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/tasks/GetResultsGridButtons',
            data: JSON.stringify(genericRequest),

            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    response = data;
                }
        });
        return response;
    }

    //Obtiene las RuleActions para el usuario conectado.
    function _LoadUserAction_ForMyTaskGrid(stepId, docId) {
        let response = null;
        let paramRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "stepId": stepId,
                "docId": docId
            }
        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/Tasks/LoadUserAction_ForMyTaskGrid',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(paramRequest),
            async: false,
            success: function (data, status, headers, config) {
                response = data;
            }
        });
        return response;
    }



    zambaTaskFactory.executeTaskRule = executeTaskRule;
    zambaTaskFactory.executeRuleForTask = executeRuleForTask;
    zambaTaskFactory.executeAction_onItems = _executeAction_onItems;
    zambaTaskFactory.getResultsGridActions = _getResultsGridActions;
    zambaTaskFactory.LoadUserAction_ForMyTaskGrid = _LoadUserAction_ForMyTaskGrid;
    zambaTaskFactory.MarkAsFavorite = MarkAsFavorite;



    function _getAttributeDescription(AttributeId, AttributeValue) {
        let genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "AttributeId": AttributeId,
                "AttributeValue": AttributeValue
            }
        };
        return $http.post(serviceBase + '/tasks/getAttributeDescription', JSON.stringify(genericRequest));
    };

    zambaTaskFactory.getAttributeDescription = _getAttributeDescription;



    function _getAttributeDescriptionMotivoDemanda(Motivo, Ramo, reportId) {
        let genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "Motivo": Motivo,
                "Ramo": Ramo,
                "reportId": reportId
            }
        };
        return $http.post(serviceBase + '/tasks/getAttributeDescriptionMotivoDemanda', JSON.stringify(genericRequest));
    };

    zambaTaskFactory.getAttributeDescriptionMotivoDemanda = _getAttributeDescriptionMotivoDemanda;



    function getAttributeListMotivoDemanda(Ramo, reportId) {
        let genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {

                "Ramo": Ramo,
                "reportId": reportId
            }
        };
        return $http.post(serviceBase + '/tasks/getAttributeListMotivoDemanda', JSON.stringify(genericRequest));
    };

    zambaTaskFactory.getAttributeListMotivoDemanda = getAttributeListMotivoDemanda;




    return zambaTaskFactory;
}]);