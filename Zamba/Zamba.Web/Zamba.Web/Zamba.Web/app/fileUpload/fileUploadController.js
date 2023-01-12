//var appFileUpload = angular.module("fileUploadApp", [""]);

app.controller('fileUploadController', function ($scope, $q, $log, $filter, $timeout, $http, gridService) {
    
});

app.directive('zambaFileUpload', function ($sce) {
    return {
        restrict: 'E',
        scope: false,
        replace: false,
        transclude: true,
        link: function ($scope, element, attributes) {           
        },
        templateUrl: $sce.getTrustedResourceUrl("../../app/fileUpload/fileUploadDirective.html")
    };
})