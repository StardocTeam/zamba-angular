var app = angular.module('executeRule', []);

app.controller('ZambaExecuteRuleController', function ($scope, $filter, $http, ruleExecutionService) {  
    $scope.executeRule = function (event) {
        var target = e.target || e.srcElement;
        var tableId = target.closest("div.tablesorter").id;
        var ruleId = $("#" + tableId).attr("ruleid");
        var resultId = target.closest("tr").getElementsByClassName("rowDocId")[0].innerText;

        ruleExecutionService.executeRule(ruleId, resultId);   
    }
});

app.directive('ZambaExecuteRule', function ($sce) {
    return {
        restrict: 'E',
        //scope: false,
        //replace: true,
        transclude: true,
        link: function ($scope, element, attributes) {
            $scope.baseUrl = thisDomain;
        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/ruleExecution/RuleExecutionView.html'),

    }
});
