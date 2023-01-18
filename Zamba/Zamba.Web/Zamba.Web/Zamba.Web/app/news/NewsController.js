var appNewsGrid = angular.module("appNews", ["xeditable"]);

appNewsGrid.controller('Ctrl', function ($scope, $filter, $http, gridService) {

    $scope.NewsColumns = [{
        Id: 1, Name: 'Tipo de Reclamante', Field: 'Tipo_de_Reclamante', Type: 'string', Visible: true, DropDown: 1, Width: 2,
        List: []
    },
    {
        Id: 2, Name: 'Nombre', Field: 'Nombre', Type: 'string', Visible: true, DropDown: 0, Width: 2,
        List: []
    }];

    $scope.LoadNewsResults = function (parentResultId) {

        var d = gridService.getNewResultId(parentResultId);
        var NewsResults = getFormattedResults(d);
        //            .done(function (d) {
        if (NewsResults == "") {
            console.log("No se pudo obtener las novedades");
            return;
        }
        else {
            $scope.NewsResults = NewsResults;
            $scope.NewsColumns = $scope.NewsColumns;
        }
        //}).error(function (err) {
        //    console.log(err);
        //    return;
        //});
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

    function GetFormattedResult(result) {
        var newResult = {
            data: {
                name: result.name,
                crdate: result.crdate,
                value: result.value,
            },
        };
        return newResult;
    }
       

    function getElementFromQueryString(element) {
        var url = window.location.href;
        var segments = url.split("&");
        var value = null;
        segments.forEach(function (valor) {
            if (valor.includes(element)) { value = valor.split("=")[1]; }
        });
        return value;
    }

    $scope.LoadNewsResults(20450661);


});

    appNewsGrid.directive('zambaGrid', function ($sce) {
        return {
            restrict: 'E',
            scope: false,
            replace: false,
            transclude: true,
            link: function ($scope, element, attributes) {
                var url = window.location.href;
                var segments = url.split("&");
                var resultId = null;
                segments.forEach(function (valor) {
                    if (valor.includes("docid")) { resultId = valor.split("=")[1]; }
                });
                $scope.resultId = resultId;
                //$scope.LoadAsociatedResults($scope.resultId);
                },
            templateUrl: $sce.getTrustedResourceUrl("../../app/news/NewsGrid.html")
        };
    })

