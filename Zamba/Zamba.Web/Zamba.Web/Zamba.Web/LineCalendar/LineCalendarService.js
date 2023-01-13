var serviceBase = ZambaWebRestApiURL;
//app.controller('LineCalendarController', function ($scope) {
//    /* config object */
//    $scope.uiConfig = {
//        calendar: {
//            height: 450,
//            editable: true,
//            header: {
//                left: 'month basicWeek basicDay agendaWeek agendaDay',
//                center: 'title',
//                right: 'today prev,next'
//            },
//            eventClick: $scope.alertEventOnClick,
//            eventDrop: $scope.alertOnDrop,
//            eventResize: $scope.alertOnResize
//        }
//    };
'use strict';
//var serviceBase = ZambaWebRestApiURL.toLowerCase().replace("/api", "/");
app.factory('linecalendarservice', ['$http', '$q', function ($http, $q) {

    var LineCalendarServiceFactory = {};

    var _getResults = function (entityId, titleAttribute, startAttribute, endAttribute, filterColumn, filterValue) {

        var associatedResults = null;
        var genericRequest = {
            // UserId: parseInt(GetUID()),
            UserId: parseInt(GetUID()),
            Params: {
                "entityId" : entityId,
                "titleAttribute" : titleAttribute,
                "startAttribute" : startAttribute,
                "endAttribute" : endAttribute,
                "filterColumn" : filterColumn,
                "filterValue" : filterValue
            }
        };


        $.ajax({
            type: "POST",
            url: serviceBase + '/search/GetCalendarReport',
            data: JSON.stringify(genericRequest),

            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    associatedResults = data;
                }
        });
        return associatedResults;
    }



    LineCalendarServiceFactory.getResults = _getResults;

    return LineCalendarServiceFactory;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("linecalendarservice");
}

