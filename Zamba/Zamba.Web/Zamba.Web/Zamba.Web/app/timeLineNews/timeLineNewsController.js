//var app = angular.module('timeline', []);

app.controller('TimeLineNewsController', function ($scope, $filter, $http, timelineNewsService) {
    $scope.activities =
        [{ id: 0, text: "life", class: "icon-address", active: "active" },
        { id: 1, text: "study", class: "icon-graduation-cap", active: "active" },
        { id: 2, text: "work", class: "icon-briefcase", active: "active" },
        { id: 3, text: "extra", class: "icon-user", active: "active" }
        ];

    $scope.LoadResults = function (TimeLineType, ResultId, EntityId, AsocNewsIds) {


        var d = timelineNewsService.getResults(TimeLineType, ResultId, EntityId, AsocNewsIds)

        if (d == "") {
            console.log("No se pudo obtener el timeline");
            return;
        }
        else {
            $scope.Results = JSON.parse(d);
        }
    };

    $scope.RefreshTimeLine = function () {
        var d = timelineNewsService.getResults($scope.timelineType, $scope.ResultId, $scope.entityId)
        if (d == "") {
            console.log("No se pudo obtener el timeline");
            return;
        }
        else {
            $scope.Results = JSON.parse(d);
        }
    }

    $scope.toggleAllActivities = function () {
        $scope.activities.map(function (activity) {
            activity.active = "active";
        });
        $scope.refreshEvents();
    }

    $scope.selectActivity = function (selected) {
        $scope.activities.map(function (activity) {
            if (activity.id === selected.id) {
                activity.active = "active";
            } else {
                activity.active = null;
            }
        });
        $scope.refreshEvents();
    }

    $scope.refreshEvents = function () {
        $scope.active_events = [];
        $scope.events.map(function (event) {
            $scope.activities.map(function (activity) {
                if (activity.text == event.type && activity.active == "active") {
                    $scope.active_events.push(event);
                }
            });
        });
    };

//    $scope.refreshEvents();
});

app.directive('zambaTimelinenews', function ($sce) {
    return {
        restrict: 'E',
        //scope: false,
        //replace: true,
        transclude: true,
        link: function ($scope, element, attributes) {
            $scope.timelineType = attributes.timelineType;
            $scope.entityId = attributes.entityId;
            $scope.AsocNewsIds = attributes.AsocNewsIds;

            var url = window.location.href;
            var segments = url.split("&");
            segments.forEach(function (valor) {
                if (valor.indexOf("docid") == 0) {
                    $scope.ResultId = valor.split("=")[1];
                }
            });

            $scope.LoadResults($scope.timelineType, $scope.ResultId, $scope.entityId, $scope.AsocNewsIds);
        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/timeLinenews/timeline.html'),

    }
});
