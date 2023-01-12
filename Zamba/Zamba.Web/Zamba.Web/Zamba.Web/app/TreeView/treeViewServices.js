'use strict';
//var serviceBase = "http://localhost:44301/ZambaWeb.RestApi";
var serviceBase = ZambaWebRestApiURL;

app.factory('treeViewServices', ['$http', '$q', function ($http, $q) {
    var treeViewServicesFactory = {};

    var saveToLocal = function (wftree) {
        try {
            if (localStorage) {
                localStorage.setItem('localWFTree-' + GetUID(), JSON.stringify(wftree));
            }
        } catch (e) {
            console.error(e);
        }
    }

    var loadFromLocal = function () {
        try {
            if (localStorage) {
                let localwftree = null;
                localwftree = localStorage.getItem('localWFTree-' + GetUID());
                if (localwftree != undefined && localwftree != null) {
                    return JSON.parse(localwftree);
                }
            }
            return null;
        } catch (e) {
            console.error(e);
            return null;
        }
    }

    var _getResults = function (reload) {
        var response = null;
        response = loadFromLocal();
        if (response != null) {
            return response;
        }

        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {}
        };

        $.ajax({
            type: "GET",
            url: serviceBase + '/wf/GetWF?userid=' + GetUID(),
            data: JSON.stringify(genericRequest),

            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    if (data != undefined) {
                        if (data.workflows != undefined)
                            response = data.workflows;
                    }
                    if (response == undefined)
                        response = [];
                    saveToLocal(response);
                },
            error:
                function (data, status, headers, config) {
                    console.error(data);
                    response = [];
                }
        });
        return response;
    };

    var _GetWFAndStepIdsAndNamesAndTaskCount = function (useCache) {
        try {
            let genericRequest = {
                UserId: parseInt(GetUID()),
                Params:
                {
                }
            };
            if (useCache === undefined) useCache = true;

            return $http.get(serviceBase + '/wf/GetWFAndStepIdsAndNamesAndTaskCount?userid=' + GetUID() + '&useCache=' + useCache, JSON.stringify(genericRequest));
        }
        catch (e) {
            console.error(e);

        }
    };

    var _SetLastWFSelected = function (id, iconClicked, selectedView) {
        var response = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
            }
        };

        $.ajax({
            type: "GET",
            url: serviceBase + '/wf/SetLastWFSelected?userid=' + GetUID() + '&IDselected=' + id.toString() + '&selectedView=' + selectedView,
            data: JSON.stringify(genericRequest),

            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    response = data;
                }
        });
        return response;

    };

    var _GetLastWFSelected = function (selectedView) {
        var response = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
            }
        };

        $.ajax({
            type: "GET",
            url: serviceBase + '/wf/GetLastWFSelected?userid=' + GetUID() + '&selectedView=' + selectedView,
            data: JSON.stringify(genericRequest),

            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    response = data;
                }
        });
        return response;

    };
    var _GetButtonsTasksTree = function () {
        var response = null;
        var genericRequest = {
            UserId: GetUID(),
            params: {
                placeId: '3'//arbol tareas
            }
        };
        //        alert('get results grid actions');
        $.ajax({
            type: "POST",
            url: serviceBase + '/tasks/GetResultsGridButtonsByPlace',
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


    treeViewServicesFactory.getTaskCount = _GetWFAndStepIdsAndNamesAndTaskCount;

    treeViewServicesFactory.getResults = _getResults;

    treeViewServicesFactory.GetLastWFSelected = _GetLastWFSelected;
    treeViewServicesFactory.GetButtonsTasksTree = _GetButtonsTasksTree
    treeViewServicesFactory.SetLastWFSelected = _SetLastWFSelected;

    return treeViewServicesFactory;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("treeViewServices");
}
