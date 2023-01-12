app.controller('progressDialog', function ($scope, $rootScope, $mdDialog) {
    $scope.status = '  ';
    $scope.customFullscreen = false;
    $scope.showdialog = false;

    $scope.showAdvanced = function (ev) {

        $mdDialog.show({
            controller: DialogController,
            templateUrl: '../../app/progressdialog/progress-dialog.component.html?v=260',
            // Appending dialog to document.body to cover sidenav in docs app
            // Modal dialogs should fully cover application to prevent interaction outside of dialog
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: false,
            fullscreen: $scope.customFullscreen // Only for -xs, -sm breakpoints.
        }).then(function (answer) {
        }, function () {
        });

    };

    $rootScope.$on('showLoading', function (event, data) {
        try {
            if ($scope.showdialog === false) {
                $scope.showdialog = true;
                $scope.showAdvanced();
                setInterval($scope.CloseDialog, 3000);
            }
        } catch (e) {
            console.error(e);
        }
    });

    $scope.CloseDialog = function () {
        $mdDialog.hide();
        $scope.showdialog = false;
    };

    $rootScope.$on('hideLoading', function (event, data) {
        try {
            $scope.CloseDialog();
        } catch (e) {
            console.error(e);
        }
    });

    function DialogController($scope, $mdDialog) {
        $scope.hide = function () {
            $mdDialog.hide();
            $scope.showdialog = false;
        };

        $scope.cancel = function () {
            $mdDialog.cancel();
            $scope.showdialog = false;
        };

        $scope.answer = function (answer) {
            $mdDialog.hide(answer);
            $scope.showdialog = false;
        };
    }
});