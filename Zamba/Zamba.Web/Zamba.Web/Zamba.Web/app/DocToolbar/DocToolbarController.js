app.controller('DocToolbarController', function ($scope, $filter, $http, DocToolbarService) {

    $scope.HasPermissionToDownloadFile = false;
    $scope.HasPermissionToSendMail = false;

    $scope.LoadUserRights = function () {
        try {
            $scope.HasPermissionToDownloadFile = DocToolbarService.GetUserRights(10, 6);
            $scope.HasPermissionToSendMail = DocToolbarService.GetUserRights(164, 6);
        } catch (e) {
            console.error(e);
        }
        
    };
    $scope.LoadUserRights();
});
