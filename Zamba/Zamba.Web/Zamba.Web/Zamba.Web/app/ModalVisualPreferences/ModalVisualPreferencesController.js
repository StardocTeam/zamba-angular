app.controller('ModalVisualPreferencesController', function ($scope, $filter, $http, ModalVisualPreferencesService, UserRightsFactory) {
    $scope.title = 'Selección de vista principal';
    $scope.exitButtonText = 'Salir';
    //Estos ids se corresponden con los valores de la userConfig 'DefaultWebView'



    $scope.$on('PermissionLoaded', function (event, data) {

        $scope.buttonList[0].hasRight = UserRightsFactory.CanShowHomePanel;
        $scope.buttonList[1].hasRight = UserRightsFactory.CanShowSearchPanel;
        $scope.buttonList[2].hasRight = UserRightsFactory.CanShowMyTasksPanel;
        $scope.buttonList[3].hasRight = UserRightsFactory.CanShowTreeViewPanel;

        $scope.buttonList[5].hasRight = UserRightsFactory.CanShowHomePanel;



    });


    $scope.buttonList = [
        {
            name: 'Inicio',
            icon: 'fa fa-home fa-3x',
            id: 'Home',
            selected: false,
            hasRight: false
        },
        {
            name: 'Búsqueda',
            icon: 'fa fa-search fa-3x',
            id: 'search',
            selected: false,
            hasRight: false
        },
        {
            name: 'Tareas',
            icon: 'fa fa-tasks fa-3x',
            id: 'MyTasks',
            selected: false,
            hasRight: false
        },
        {
            name: 'Procesos',
            icon: 'fa fa-indent fa-3x',
            id: 'MyProcess',
            selected: false,
            hasRight: false
        },
        {
            name: 'Ultimo usado',
            icon: 'fa fa-user fa-3x',
            id: 'None',
            selected: false,
            hasRight: true
        },
        {
            name: 'News',
            icon: 'fa fa-file fa-3x',
            id: 'News',
            selected: false,
            hasRight: false
        }

    ];


    $scope.setSelectedButtonDefaultView = function (DefaultViewValue) {
        $scope.buttonList = $scope.buttonList.map(function (button) {
            if (button.id == DefaultViewValue)
                button.selected = true;
            else
                button.selected = false;
            return button;
        });
    };

    $scope.UpdateDefaulViewItem = function (button) {
        $scope.setSelectedButtonDefaultView(button.id);
        var view = button.id;
        $scope.SetDefaultMainMenuItem(view);
    };
    $scope.SetDefaultMainMenuItem = function (DefaultView) {
        try {
            var userid = parseInt(GetUID());
            if (window.localStorage)
                window.localStorage.setItem("DefaultView|" + userid, DefaultView);
            ModalVisualPreferencesService.SetDefaultMainMenuItem(userid, DefaultView);
        } catch (e) {
            console.error(e);
        }
    };

    $scope.LoadDefaultMainMenuItem = function () {
        var userid = GetUID();
        if (userid != undefined && userid > 0) {
            userid = parseInt(userid);

            var DefaultView = null;
            if (window.localStorage) {
                DefaultView = window.localStorage.getItem("DefaultView|" + parseInt(GetUID()));
            }
            if (DefaultView != null && DefaultView != '' && DefaultView != 'null') {
                $scope.setSelectedButtonDefaultView(DefaultView);
            } else {
                DefaultView = ModalVisualPreferencesService.GetDefaultMainMenuItem();


                $scope.setSelectedButtonDefaultView(DefaultView);
                return DefaultView;

            }
        }
    };

    $scope.LoadDefaultMainMenuItem();

});

app.directive('visualPreferences', function ($sce) {
    return {
        restrict: 'E',
        transclude: true,
        link: function ($scope, element, attributes) {

        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/ModalVisualPreferences/ModalVisualPreferencesTemplate.html')
    }
});


