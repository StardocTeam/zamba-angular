'use strict';
var serviceBase = ZambaWebRestApiURL;

app.factory('ZambaTaskService', ['$http', '$q', function ($http, $q) {
    var zambaTaskFactory = {};

    //ver de poner async en IE
    function executeTaskRule(ruleId, resultIds, formVars) {
        var response = null;

        if (formVars == undefined) {
            var genericRequest = {
                UserId: parseInt(GetUID()),
                Params: {
                    "ruleId": ruleId,
                    "resultIds": resultIds,
                    "userid": GetUID()
                }
            };
        } else {
            var genericRequest = {
                UserId: parseInt(GetUID()),
                Params: {
                    "ruleId": ruleId,
                    "resultIds": resultIds,
                    "userid": GetUID(),
                    "FormVariables": formVars
                }
            };
        }
        debugger;
        return $http.post(serviceBase + '/tasks/ExecuteTaskRule', JSON.stringify(genericRequest));
    };

    function _executeAction_onItems(ruleId, resultIds, formVars) {
        resultIds = JSON.stringify(resultIds);
        resultIds = resultIds.replace(/[([^\]^"]*/g, "");
        var response = null;

        if (formVars == undefined) {
            var genericRequest = {
                UserId: parseInt(GetUID()),
                Params: {
                    "ruleId": ruleId,
                    "resultIds": resultIds,
                    "userid": GetUID()
                }
            };
        }
        else {
            var genericRequest = {
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
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "TaskId": TaskId,
                "Mark": Mark
            }
        };
        return $http.post(serviceBase + '/tasks/MarkAsFavorite', JSON.stringify(genericRequest));
    };


    function _LoadUserPhotoFromDB(userid) {
        var response = null;
        if (userid != null && userid != '') {
            var genericRequest = {
                UserId: parseInt(userid),

            };

            $.ajax({
                type: "POST",
                url: ZambaWebRestApiURL + "/Search/GetThumbsPathHome",
                data: JSON.stringify(genericRequest),

                contentType: "application/json; charset=utf-8",
                async: false,
                success:
                    function (data, status, headers, config) {
                        response = data;
                        window.localStorage.setItem('userPhoto-' + userid, response);

                    }
            });
        }
        return response;
    }

    //Obtiene las RuleActions para el usuario conectado.
    function _getResultsGridActions(userId) {
        var response = null;
        var genericRequest = {
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
        var response = null;
        var paramRequest = {
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


    function _LoadUserNameFromDB(idusuario) {

        var response = null;
        if (idusuario != undefined && idusuario > 0) {


            $.ajax({
                type: "POST",
                url: ZambaWebRestApiURL + "/search/GetUserData?userId=" + idusuario,
                dataType: 'json',

                contentType: "application/json; charset=utf-8",
                async: false,
                success:
                    function (data, status, headers, config) {
                        response = data;

                    }
            });
        }
        return response;
    };


    function _getAttributeDescription(AttributeId, AttributeValue) {
        var genericRequest = {
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
        var genericRequest = {
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
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {

                "Ramo": Ramo,
                "reportId": reportId
            }
        };
        return $http.post(serviceBase + '/tasks/getAttributeListMotivoDemanda', JSON.stringify(genericRequest));
    };

    function _SaveSidebarLockedState(LockedState) {
        try {
            $.ajax({
                type: 'POST',
                dataType: 'json',
                url: ZambaWebRestApiURL + '/Account/SetLastSidebarLockedState?' + jQuery.param({ UserId: parseInt(GetUID()), LockedState: LockedState }),
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                },
            });
        } catch (e) {
            console.error(e);
        }
    };

    function _GetSidebarLockedState() {
        var response = null;
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: ZambaWebRestApiURL + '/Account/GetLastSidebarLockedState?' + jQuery.param({ UserId: parseInt(GetUID()) }),
            contentType: "application/json; charset=utf-8",
            async: false,
            success: function (resp) {
                response = resp;
            },
        });
        return response;
    }

    zambaTaskFactory.getAttributeListMotivoDemanda = getAttributeListMotivoDemanda;
    zambaTaskFactory.executeTaskRule = executeTaskRule;
    zambaTaskFactory.executeAction_onItems = _executeAction_onItems;
    zambaTaskFactory.getResultsGridActions = _getResultsGridActions;
    zambaTaskFactory.LoadUserAction_ForMyTaskGrid = _LoadUserAction_ForMyTaskGrid;
    zambaTaskFactory.MarkAsFavorite = MarkAsFavorite;
    zambaTaskFactory.LoadUserPhotoFromDB = _LoadUserPhotoFromDB;
    zambaTaskFactory.LoadUserNameFromDB = _LoadUserNameFromDB;
    zambaTaskFactory.SaveSidebarLockedState = _SaveSidebarLockedState
    zambaTaskFactory.GetSidebarLockedState = _GetSidebarLockedState


    return zambaTaskFactory;
}]);