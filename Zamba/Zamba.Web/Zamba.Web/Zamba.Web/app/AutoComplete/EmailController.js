'use strict';

app.controller('EmailController', function ($scope, $filter, $http, AutoCompleteServices, ZambaUserService) {

    $scope.GetMails = function (DocIdsChecked) {
        $scope.Emails = AutoCompleteServices.GetEmailsUsersOfTask(DocIdsChecked);
        $scope.$broadcast('EmailsObtained', { Emails: $scope.Emails });
    }

});


app.directive('zambaEmail', function ($sce) {
    return {
        restrict: 'E',
        transclude: true,
        link: function ($scope, element, attributes) {
            $scope.Emails = [];
        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/AutoComplete/AutoCompleteTemplate.html'),
    }
});