//var app = angular.module('timeline', []);

app.controller('TimelineController', function ($scope, $filter, $http, timelineService) {
    $scope.activities =
        [{ id: 0, text: "life", class: "icon-address", active: "active" },
        { id: 1, text: "study", class: "icon-graduation-cap", active: "active" },
        { id: 2, text: "work", class: "icon-briefcase", active: "active" },
        { id: 3, text: "extra", class: "icon-user", active: "active" }
        ];

    $scope.LoadResults = function (TimeLineType, parentResultId, EntityId, ParentId, reportId) {    
        if (reportId == "" || reportId == undefined) {
            var d = timelineService.getResults(TimeLineType, parentResultId, EntityId, ParentId)
        } else {
            var d = timelineService.Report(reportId);
        }        

        if (d == "") {
            console.log("No se pudo obtener el timeline");
            return;
        }
        else {
            $scope.Results = JSON.parse(d);
        }
    };

  $scope.RefreshTimeLine = function () {
      var d = timelineService.getResults($scope.timelineType, $scope.parentResultId, $scope.entityId, $scope.parentEntityId)
      //alert(d);
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

    $scope.events =
        [{
            event_id: 1,
            type: "life",
            glyphicon: "glyphicon-grain",
            title: "Your journey begins here",
            text: "Born on the late 80's to the sound of 'Amanda', by Boston, in the always sunny city of Huelva, in Andalusia"
        },
        {
            event_id: 13,
            type: "extra",
            glyphicon: "glyphicon-baby-formula",
            title: "Liquid gold",
            text: "Tried Wardaamse Triple for the first time. Best beer this side of the Pyrenees.",
            link: "http://www.brouwerijstokhove.be/"
        },
        {
            event_id: 14,
            type: "extra",
            glyphicon: "glyphicon-globe",
            title: "Man of the match!",
            text: "Joined Royal Brussels British FC, an amateur football club. We welcome people of all ages and nationalities.",
            link: "http://rbbfc.org/"
        },
        {
            event_id: 15,
            type: "work",
            glyphicon: "glyphicon-tint",
            title: "Let's do some science",
            text: "Scientist on WIV-ISP, in the Platform Biotechnology and molecular biology. Assisting the masters of next generation DNA sequencing. Cheers Python! Welcome Ruby!",
            link: "https://www.wiv-isp.be"
        }];

    $scope.refreshEvents();
});

app.directive('zambaTimeline', function ($sce) {
    return {
        restrict: 'E',
        //scope: false,
        //replace: true,
        transclude: true,
        link: function ($scope, element, attributes) {
            $scope.timelineType = attributes.timelineType;
            $scope.parentEntityId = attributes.parentEntityId;
            $scope.entityId = attributes.entityId;
            $scope.reportId = attributes.reportId;

            
            var url = window.location.href;
            var segments = url.split("&");
            segments.forEach(function (valor) {
                if (valor.indexOf("docid") == 0) {
                    $scope.parentResultId = valor.split("=")[1];
                }
            });       
           
            $scope.LoadResults($scope.timelineType, $scope.parentResultId, $scope.entityId, $scope.parentEntityId, $scope.reportId);
        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/timeLine/timeline.html'),

    }
});
