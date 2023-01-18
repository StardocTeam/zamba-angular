app.controller('UserController', function ($scope, $filter, $http, ZambaUserService) {

    $scope.hasUserRight = false;

    $scope.getUserPreferences = function (PreferenceName) {

        return ZambaUserService.getUserPreferences(PreferenceName);
    };

    $scope.getUserPreferencesSync = function (PreferenceName) {

        return ZambaUserService.getUserPreferencesSync(PreferenceName);
    };

    $scope.getSystemPreferences = function (PreferenceName) {

        return ZambaUserService.getSystemPreferences(PreferenceName);
    };

    $scope.getUserRight = function (rightId) {
        var uid = GetUID();
        var inLocalStorage = $scope.getUserRightExisting(rightId, uid);
        if (inLocalStorage == false) {
            var response = ZambaUserService.getUserRight(rightId);
            window.localStorage.setItem("getUserRight-" + rightId + "-" + uid, response);
            $scope.hasUserRight = (response == 'true');
            return $scope.hasUserRight;
        }
        return (window.localStorage.getItem("getUserRight-" + rightId + "-" + uid) == "true");
    };

    $scope.getUserRightExisting = function (rightId, uid) {
        var existInLocalStorage = window.localStorage.getItem("getUserRight-" + rightId + "-" + uid);
        if (existInLocalStorage == undefined || existInLocalStorage == null)
            return false
        else
            return true;
    };

});