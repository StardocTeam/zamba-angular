var appGraph = angular.module("LineGraphsApp", []);



appGraph.controller('LineGraphCtrl', function ($scope, $filter, $http, gridService) {

    $scope.asociatedColumns = ["Nombre", "Cantidad"];


    $scope.LoadAsociatedResults = function (parentResultId, parentResultEntityId, associatedIds) {

        var d = gridService.GetGraphResults(parentResultId, parentResultEntityId, associatedIds);
        if (d != 'null') {
            var associatedResults = getFormattedResults(JSON.parse(d));

            if (associatedResults == "") {
                console.log("No se pudo obtener los asociados");
            }
            else
            {
                $scope.asociatedResults = associatedResults;
            }
        }
    };

   
    function getFormattedResults(results) {
        var resultsFormated = [];
        var newResult = null;
        if (results.length > 0) {
            for (var result in results) {
                newResult = GetFormattedResult(results[result]);
                resultsFormated.push(newResult);
                newResult = null;
            }
        }
        return resultsFormated;
    };

    function GetFormattedResult(AssociatedResult) {
        var result = {
                label: AssociatedResult.SOLICITANTE,
                value: AssociatedResult.CANTIDAD,
                click: "http://www.google.com.ar"
            };     
       
        return result;
    }

});



appGraph.directive('zambaLineGraph', function ($sce) {
    return {
        restrict: 'E',
        scope: false,
        replace: false,
        transclude: true,
        link: function ($scope, element, attributes) {
            $scope.entityId = 18;
            $scope.indexId = 20;

            var url = window.location.href;
            var segments = url.split("&");
            var resultId = null;
            segments.forEach(function (valor) {
                if (valor.includes("docid")) { resultId = valor.split("=")[1]; }
            });
            $scope.resultId = resultId;
            $scope.graphType = attributes.graphType;
            $scope.id = 'graph_' + $scope.graphType;

            $scope.LoadAsociatedResults($scope.resultId, $scope.entityId, $scope.indexId);

            $('.graph').attr('id', 'graph_' + $scope.graphType);
            $('.graph').removeAttr('class');

            var chart = null;
            if ($scope.graphType == "line") {
                Morris.Line({
                    element: $scope.id,
                    parseTime: false,
                    data: $scope.asociatedResults,
                    xkey: 'label',
                    ykeys: ['value', 'link'],
                    labels: ['Cantidad', 'Link'],
                    resize: true,
                    redraw: true
                });
            } else if ($scope.graphType == "bar") {
                Morris.Bar({
                    element: $scope.id,
                    data: $scope.asociatedResults,
                    xkey: 'label',
                    ykeys: ['value'],
                    labels: ['Cantidad'],
                    resize: true,
                    redraw: true
                });

            } else if ($scope.graphType == "donut") {
                chart = Morris.Donut({
                    element: $scope.id,
                    data: $scope.asociatedResults,
                    resize: true,
                    redraw: true
                });
            }
            chart.redraw();
            //chart.on('click', function (i, row) { window.open(row.click) });
        },
        templateUrl: $sce.getTrustedResourceUrl("../LineGraphs/LineGraphDirective.html")
    };
})


function getElementFromQueryString(element) {
    var url = window.location.href;
    var segments = url.split("&");
    var value = null;
    segments.forEach(function (valor) {
        if (valor.includes(element)) { value = valor.split("=")[1]; }
    });
    return value;
}



