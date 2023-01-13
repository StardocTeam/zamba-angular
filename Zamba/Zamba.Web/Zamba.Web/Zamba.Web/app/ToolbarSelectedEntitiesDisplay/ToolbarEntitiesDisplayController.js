app.controller('ToolbarDisplayEntitiesController', function ($scope) {
    $scope.selectedEntitiesList = [];
    $scope.totalSelected = 0;
    $scope.entitiesTotalQuantity = 0;

    $scope.$on('localTreeDataLoaded', function (event, args) {
        $scope.selectedEntitiesList = [];
        if (args[0] != undefined) {
            if (args[0].items != undefined)
                $scope.LoadToolbarText(args[0]);
        }

    });

    $scope.LoadToolbarText = function (parentNode) {
        $scope.selectedEntitiesList = [];
        $scope.totalSelected = 0;
        $scope.entitiesTotalQuantity = parentNode.items.length;

        parentNode.items.forEach(function (node) {
            if (node.checked) {
                $scope.selectedEntitiesList.push(node.text);
            }

        });
        $scope.totalSelected = $scope.selectedEntitiesList.length;
    };

    $scope.clearAllSearchEntities = function () {
        console.log("Limpiando...");
        $("#treeview .k-checkbox-wrapper input").prop("checked", false).trigger("change");
    };
});

app.directive('displayEntities', function ($sce) {
    return {
        restrict: 'E',
        transclude: true,
        link: function ($scope, element, attributes) {

        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/ToolbarSelectedEntitiesDisplay/ToolbarEntitiesDisplayTemplate.html')
    }
});


