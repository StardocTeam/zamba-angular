//var app = angular.module('observaciones', []);
var bool = false;

app.controller('doShowFormController', function ($scope, $filter, $http, $timeout, doShowFormService) {
    $scope.OpenModal = function (url) {
        try {
            $("#dialog").dialog({
                uiLibrary: 'bootstrap',
                resizable: true,
                modal: true
            });
        } catch (e) {
            console.error(e);
        }
    }

    $scope.CloseModal = function () {
    }
});

app.directive('zambaDoShowForm', function ($sce) {
    return {
        restrict: 'E',
        transclude: true,
        link: function ($scope, element, attributes) {
            //$scope.ultraCompact = false;
            $scope.url = attributes.url;

            //if (document.querySelector("#doShowFormIframe").classList.contains("Abierto")) {
            //if (localStorage.getItem("LocalDoShowform_" + GetUID() + GetDOCID())) {
            //    localStorage.setItem("LocalDoShowform_" + GetUID(), true);
            //    $scope.ultraCompact = true;
            //} else {
            //    localStorage.setItem("LocalDoShowform_" + GetUID() + GetDOCID(), false);
            //    $scope.ultraCompact = false;
            //}
        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/DoShowForm/DoShowFormTemplate.html'),
    }
});
