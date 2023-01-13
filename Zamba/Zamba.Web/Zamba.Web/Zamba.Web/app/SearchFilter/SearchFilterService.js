'use strict';
var serviceBase = ZambaWebRestApiURL;

app.service('SearchFilterService', function ($http) {

    var SearchFilterService = {};

    var _DeleteUserAssignedFilter = function (doctypeId, filterType) {
        try {

            var genericRequest = {
                UserId: parseInt(GetUID()),
                Params: {
                    "filterType": filterType,
                    "docTypeId": doctypeId,
                }
            };
            $.ajax({
                type: "post",
                url: ZambaWebRestApiURL + '/FiltersServices/DeleteUserAssignedFilter',
                data: JSON.stringify(genericRequest),
                contentType: "application/json; charset=utf-8",
                async: false,
                success:
                    function (data, status, headers, config) {
                        result = data;
                    }
            });
        } catch (e) {
            console.error(e);
        }

    }

    var _DeleteStepFilter = function (doctypeId, filterType) {
        try {

            var genericRequest = {
                UserId: parseInt(GetUID()),
                Params: {
                    "filterType": filterType,
                    "docTypeId": doctypeId,
                }
            };
            $.ajax({
                type: "post",
                url: ZambaWebRestApiURL + '/FiltersServices/DeleteStepFilter',
                data: JSON.stringify(genericRequest),
                contentType: "application/json; charset=utf-8",
                async: false,
                success:
                    function (data, status, headers, config) {
                        result = data;
                    }
            });
        } catch (e) {
            console.error(e);
        }

    }
    
    var _setDisabledAllFiltersByUser = function (filterType) {
        try {
            if (filterType == "MyProcess" || filterType == "Process")
                filterType = "Task"
            if (filterType == "Search")
                filterType = "search"

            var genericRequest = {
                UserId: parseInt(GetUID()),
                Params: {
                    "filterType": filterType,
                }
            };
            var result = false;

            $.ajax({
                type: "post",
                url: ZambaWebRestApiURL + '/FiltersServices/SetDisabledAllFiltersByUser',
                data: JSON.stringify(genericRequest),
                contentType: "application/json; charset=utf-8",
                async: false,
                success:
                    function (data, status, headers, config) {
                        result = data;
                    }
            });
        } catch (e) {
            console.error(e);
        }

    }

    var _SetDisabledAllFiltersByUserViewDoctype = function (filterType, doctypeId) {
        try {
            if (filterType == "MyProcess" || filterType == "Process")
                filterType = "Task"
            if (filterType == "Search")
                filterType = "search"

            var genericRequest = {
                UserId: parseInt(GetUID()),
                Params: {
                    "filterType": filterType,
                    "docTypeId": doctypeId,
                }
            };
            var result = false;

            $.ajax({
                type: "post",
                url: ZambaWebRestApiURL + '/FiltersServices/SetDisabledAllFiltersByUserViewDoctype',
                data: JSON.stringify(genericRequest),
                contentType: "application/json; charset=utf-8",
                async: false,
                success:
                    function (data, status, headers, config) {
                        result = data;
                    }
            });
        } catch (e) {
            console.error(e);
        }

    }

    var _SetEnabledAllFiltersByUserViewDoctype = function (filterType, doctypeId) {
        try {
            if (filterType == "MyProcess" || filterType == "Process")
                filterType = "Task"
            if (filterType == "Search")
                filterType = "search"

            var genericRequest = {
                UserId: parseInt(GetUID()),
                Params: {
                    "filterType": filterType,
                    "docTypeId": doctypeId,
                }
            };
            var result = false;

            $.ajax({
                type: "post",
                url: ZambaWebRestApiURL + '/FiltersServices/SetEnabledAllFiltersByUserViewDoctype',
                data: JSON.stringify(genericRequest),
                contentType: "application/json; charset=utf-8",
                async: false,
                success:
                    function (data, status, headers, config) {
                        result = data;
                    }
            });
        } catch (e) {
            console.error(e);
        }

    }
    
    var _GetFiltersByView = function (doctypeId,filterType) {
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "doctypeId": doctypeId,
                "filterType": filterType
            }
        };
        var result = false;
        $.ajax({
            type: "post",
            url: ZambaWebRestApiURL + '/FiltersServices/GetFiltersByView',
            data: JSON.stringify(genericRequest),
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    result = data;
                }
        });

        return JSON.parse(result);
    }

    var _AddFilter = function (zfilterweb) {
        if (zfilterweb.filterType == "MyProcess")
            zfilterweb.filterType = "Process"
        if (zfilterweb.filterType == "search")
            zfilterweb.filterType = "Search"

        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "indexId": zfilterweb.indexId,
                "attribute": zfilterweb.attribute,
                "dataType": zfilterweb.dataType,
                "comparator": zfilterweb.comparator,
                "filterValue": zfilterweb.filterValue.toString().trim(),
                "docTypeId": zfilterweb.docTypeId,
                "description": zfilterweb.description,
                "additionalType": zfilterweb.additionalType,
                "dataDescription": zfilterweb.dataDescription,
                "filterType": zfilterweb.filterType
            }
        };
        var result = false;

        $.ajax({
            type: "post",
            url: ZambaWebRestApiURL + '/FiltersServices/AddFilter',
            data: JSON.stringify(genericRequest),
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    result = data;
                }
        });

        return result;
    }

    var _RemoveFilterById = function (zfilterwebId) {
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "filterId": zfilterwebId,
            }
        };
        var result = false;

        $.ajax({
            type: "post",
            url: ZambaWebRestApiURL + '/FiltersServices/RemoveFilterById',
            data: JSON.stringify(genericRequest),
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    result = data;
                }
        });

        return result;
    }

    var _RemoveOtherFilters = function (zfilterweb, view) {
        if (view == "MyProcess" || view == "Process")
            view = "manual";

        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "docTypeId": zfilterweb.docTypeId,
                "filterType": view
            }
        };
        var result = false;

        $.ajax({
            type: "post",
            url: ZambaWebRestApiURL + '/FiltersServices/RemoveZambaColumnsFilter',
            data: JSON.stringify(genericRequest),
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    result = data;
                }
        });

        return result;
    }

    var _RemoveAllIndexFilters = function (docTypeId, view) {
        if (view == "MyProcess" || view == "Process")
            view = "manual";

        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "docTypeId": docTypeId,
                "filterType": view
            }
        };
        var result = false;

        $.ajax({
            type: "post",
            url: ZambaWebRestApiURL + '/FiltersServices/RemoveAllZambaColumnsFilter',
            data: JSON.stringify(genericRequest),
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    result = data;
                }
        });

        return result;
    }

    var _UpdateFilterValue = function (valToUpdate, zfilterId) {
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "zfilterId": zfilterId,
                "valToUpdate": valToUpdate
            }
        };
        var result = false;

        $.ajax({
            type: "post",
            url: ZambaWebRestApiURL + '/FiltersServices/UpdateFilterValue',
            data: JSON.stringify(genericRequest),
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    result = data;
                }
        });

        return result;
    }

    var _SetEnabledFilter = function (zfilterweb) {
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "indexId": zfilterweb.indexId,
                "enabled": zfilterweb.enabled,
                "attribute": zfilterweb.attribute,
                "comparator": zfilterweb.comparator,
                "filterValue": zfilterweb.filterValue,
                "docTypeId": zfilterweb.docTypeId,
            }
        };
        var result = false;

        $.ajax({
            type: "post",
            url: ZambaWebRestApiURL + '/FiltersServices/SetEnabledFilter',
            data: JSON.stringify(genericRequest),
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    result = data;
                }
        });

        return result;
    }

    var _SetEnabledFilterById = function (zFilterWebID,IsChecked) {
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "filterId": zFilterWebID,
                "enabled": IsChecked,
            }
        };
        var result = false;

        $.ajax({
            type: "post",
            url: ZambaWebRestApiURL + '/FiltersServices/SetEnabledFilterById',
            data: JSON.stringify(genericRequest),
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    result = data;
                }
        });

        return result;
    }

    SearchFilterService.DeleteStepFilter = _DeleteStepFilter;

    SearchFilterService.SetDisabledAllFiltersByUserViewDoctype = _SetDisabledAllFiltersByUserViewDoctype;

    SearchFilterService.SetEnabledAllFiltersByUserViewDoctype = _SetEnabledAllFiltersByUserViewDoctype;
    
    SearchFilterService.DeleteUserAssignedFilter = _DeleteUserAssignedFilter;

    SearchFilterService.SetDisabledAllFiltersByUser = _setDisabledAllFiltersByUser;

    SearchFilterService.AddFilter = _AddFilter;

    SearchFilterService.SetEnabledFilter = _SetEnabledFilter;

    SearchFilterService.SetEnabledFilterById = _SetEnabledFilterById;

    SearchFilterService.RemoveOtherFilters = _RemoveOtherFilters;

    SearchFilterService.RemoveFilterById = _RemoveFilterById;

    SearchFilterService.RemoveAllIndexFilters = _RemoveAllIndexFilters;
    
    SearchFilterService.UpdateFilterValue = _UpdateFilterValue;

    SearchFilterService.GetFiltersByView = _GetFiltersByView;

    return SearchFilterService;
});