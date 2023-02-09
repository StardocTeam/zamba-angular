
app.controller('UserController', function ($scope, $filter, $http, ZambaUserService) {

    $scope.hasUserRight = false;

    $scope.getUserPreferences = function (PreferenceName) {

        return ZambaUserService.getUserPreferences(PreferenceName);
    };

    $scope.getSystemPreferences = function (PreferenceName) {

        return ZambaUserService.getSystemPreferences(PreferenceName);
    };

    $scope.getUserRight = function (rightId) {
        var uid = GetUID();
        var inLocalStorage = $scope.getUserRightExisting(rightId,uid);
        if (inLocalStorage == true) {
            var response = ZambaUserService.getUserRight(rightId);
            localStorage.setItem("getUserRight-" + rightId + "-" + uid, response);
            $scope.hasUserRight = (response == 'true');
            return $scope.hasUserRight;
        }
        return (localStorage.getItem("getUserRight-" + rightId + "-" + uid) == "true");
    };

    $scope.getUserRightExisting = function (rightId,uid) {
        var existInLocalStorage = localStorage.getItem("getUserRight-" + rightId + "-" + uid);
        if (existInLocalStorage == undefined) 
            return true
        else
            return false;
    };

});

