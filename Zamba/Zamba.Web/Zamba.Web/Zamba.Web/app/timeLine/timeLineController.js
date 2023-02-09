//var app = angular.module('timeline', []);

app.controller('TimelineController', function ($scope, $filter, $http, timelineService) {
    

    $scope.LoadResults = function (TimeLineType, parentResultId, EntityId, ParentId, reportId) {  


        if (reportId == "" || reportId == undefined) {
            var d = timelineService.getResults(TimeLineType, parentResultId, EntityId, ParentId)
        } else {
            $scope.parentTaskId = getElementFromQueryString("taskid");

            var d = timelineService.Report(reportId, $scope.parentTaskId);
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

            try {

            
            var url = window.location.href;
            var segments = url.split("&");
            segments.forEach(function (valor) {
                if (valor.indexOf("docid") == 0) {
                    $scope.parentResultId = valor.split("=")[1];
                }
            });       
           
            $scope.LoadResults($scope.timelineType, $scope.parentResultId, $scope.entityId, $scope.parentEntityId, $scope.reportId);

            } catch (e) {

            }
            },
        templateUrl: $sce.getTrustedResourceUrl('../../app/timeLine/timeline.html'),

    }
});
