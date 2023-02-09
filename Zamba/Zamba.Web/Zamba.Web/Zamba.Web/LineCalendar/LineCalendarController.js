//var app = angular.module('App', [])

app.controller('CalendarCtrl', function ($scope, $compile, linecalendarservice) {
    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();
    $scope.event = [];

    $scope.LoadResults = function (entityId, titleAttribute, startAttribute, endAttribute, filterColumn, filterValue) {
        var d = linecalendarservice.getResults(entityId, titleAttribute, startAttribute, endAttribute, filterColumn, filterValue);
        if (d == "") {
            console.log("No se pudo obtener el reporte");
            return;
        } else {
            $scope.setEvents(JSON.parse(d));
            $('#calendar').fullCalendar({
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month,basicWeek,basicDay'
                },

                navLinks: true,
                editable: true,
                eventLimit: true,
                events: $scope.events

            });


        }
    };

    //   $scope.formato = function getDateformat(fulldate) {

    //        var day = date.getDay();
    //        var month = date.getMonth();
    //        var year = date.getFullYear();
    //        if (fulldate) {

    //            var format = day + "/" + month + "/" + year;
    //        }

    //        return format;
    //    };

    $scope.setEvents = function (d) {
        $scope.events = d;
    };

});

app.directive('zambaLinecalendar', function ($sce) {
    return {
        restrict: 'E',
        replace: true,
        transclude: true,
        link: function ($scope, element, attributes) {
            //$scope.setEvents();
            var filterColumn = '';
            var filterValue = '';

            if (attributes.filterColumn != undefined) { filterColumn = attributes.filterColumn }
            if (attributes.filterValue != undefined) { filterColumn = attributes.filterValue }
            $scope.LoadResults(attributes.entityid, attributes.titleattribute, attributes.startattribute, attributes.endattribute, filterColumn, filterValue);



        },
        templateUrl: $sce.getTrustedResourceUrl("../../LineCalendar/LineCalendarDirective.html")
    };
});

//function getCurrentDate(isFullDate) {
//    var date = new Date();
//    var currentDate = null;
//    var day = date.getDate();
//    var month = date.getMonth() + 1;
//    var year = date.getFullYear();


//        currentDate = day + "/" + month + "/" + year;



//};

