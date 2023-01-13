'use strict';
var serviceBase = ZambaWebRestApiURL;

app.service('SearchObjectService', function ($http) {

    var SearchObjectService = {};

    var _SaveSearch = function (ObjSearch, scope) {
        try {

            var genericRequest = {
                UserId: parseInt(GetUID()),
                Params: {
                    "SearchObject": JSON.stringify(ObjSearch),
                    "Mode": ObjSearch.View
                }
            };
            var result = false;

            $.ajax({
                type: "POST",
                url: ZambaWebRestApiURL + '/search/SaveSearch',
                data: JSON.stringify(genericRequest),
                contentType: "application/json; charset=utf-8",
                async: true,
                success:
                    function (data, status, headers, config) {
                        // result = data;
                        if (!isNaN(Date.parse(data))) {
                            scope.Search.ExpirationDate = new Date(data);
                            scope.ShowExpirationDate = scope.GetRowFetchedDate();
                        } else {
                            scope.Search.ExpirationDate = "";
                            scope.ShowExpirationDate = "";
                        }
                    }
            });
            return result;
        } catch (e) {
            console.error(e);
        }

    }

    var _GetSearch = function (currentMode) {
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                Mode: currentMode
            }
        };
        var result = false;

        $.ajax({
            type: "POST",
            url: ZambaWebRestApiURL + '/search/GetLastSearch',
            data: JSON.stringify(genericRequest),
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    switch (status) {
                        case "success":
                            result = data;
                            break;

                        case "nocontent":
                            break;

                        default:
                    }
                }
        });
        return result;
    }

    var _RemoveSearch = function (UserId) {
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                Mode: currentMode
            }
        };
        var result = false;

        $.ajax({
            type: "POST",
            url: ZambaWebRestApiURL + '/search/RemoveSearch',
            data: JSON.stringify(genericRequest),
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    result = data;
                },
            error:
                function (error) {
                    result = error;
                }

        });
        return result;
    }

    var _GetStepEntities = function (stepId) {
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "stepId": stepId.toString()
            }
        };
        var result = false;

        $.ajax({
            type: "POST",
            url: ZambaWebRestApiURL + '/search/GetStepEntities',
            data: JSON.stringify(genericRequest),
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    result = data;
                },
            error:
                function (error) {
                    result = error;
                }

        });
        return result;
    }

    SearchObjectService.SaveSearch = _SaveSearch;
    SearchObjectService.GetSearch = _GetSearch;
    SearchObjectService.RemoveSearch = _RemoveSearch;

    SearchObjectService.GetStepEntities = _GetStepEntities;

    return SearchObjectService;
});