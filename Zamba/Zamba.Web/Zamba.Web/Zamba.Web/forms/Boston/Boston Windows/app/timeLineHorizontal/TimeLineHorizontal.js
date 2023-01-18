// Modulo
//var app = angular.module('TimeLineHorizontal', []);

// Controlador
app.controller('TimeLineHorizontalController', function ($scope, timeLineHorizontalService) {

    $scope.usersOrGroupsIds;
    $scope.userOrGroupIdSelected;
    $scope.usersOrGroups;

    //$scope.defaultUserImg = "imgs/userDefault.jpg";
    $scope.defaultUserImg = "../../content/images/icons/userDefault.jpg";

    $scope.init = function (source, selected) {
        if (source != undefined && source != null) {
            // Obtengo los ids de grupos y usuarios del input
            $scope.usersOrGroupsIds = $("#" + source).val();
            if ($scope.usersOrGroupsIds != undefined && $scope.usersOrGroupsIds != null && $scope.usersOrGroupsIds != '') {
                // Obtengo el usuario actual
                $scope.userOrGroupIdSelected = $("#" + selected).val();
                // Obtengo objetos usuario/grupo 
                timeLineHorizontalService.getResults($scope.usersOrGroupsIds, DataHandler);
            }
        }
    }

    function DataHandler(data) {
        data = JSON.parse(data);
        var _completed = true;
        $.each(data, function (index, usr) {
            if (usr.Id == $scope.userOrGroupIdSelected)
                _completed = false;

            usr.completed = _completed;
        });

        $scope.$apply(function () {
            $scope.usersOrGroups = data;
        });

        var _height;
        $('.icon-description').each(function (ind, element) {
            if (ind === 0)
                _height = $(element).height();

            if (_height < $(element).height())
                _height = $(element).height();
        });

        $('.icon-description > p').css("height", _height);
    }

});


// Directiva Timeline
app.directive('zambaTimelineHorizontal', function ($sce) {
    return {

        restrict: 'E',
        transclude: true,
        //templateUrl: $sce.getTrustedResourceUrl('TimeLineHorizontal.html'),
        templateUrl: $sce.getTrustedResourceUrl('../../app/timeLineHorizontal/TimeLineHorizontal.html'),

        link: function ($scope, element, attributes) {
            $scope.init(attributes.source, attributes.selected);
        }
    }

});


// Directiva tooltip https://www.w3schools.com/bootstrap/bootstrap_ref_js_tooltip.asp
// Ejemplo:
/* <div ng-repeat="usrgroup in usersOrGroups" 
        class="icon-container"
        tooltip 
        data-placement="bottom" 
        title="{{usrgroup.Name}}"> */
app.directive('tooltip', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            $(element).hover(function () {
                // on mouseenter
                $(element).tooltip('show');
            }, function () {
                // on mouseleave
                $(element).tooltip('hide');
            });
        }
    };
});


// Directiva popover https://www.w3schools.com/bootstrap/bootstrap_ref_js_popover.asp
// Ejemplo:
/* <div ng-repeat="usrgroup in usersOrGroups" 
        class="icon-container" 
        popover 
        popover-header="{{usrgroup.Name}}" 
        popover-content="Contenido" 
        popover-placement="bottom"> */
app.directive('popover', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {

            var _header = attrs.popoverHeader;
            var _content = attrs.popoverContent;
            var _placement = attrs.popoverPlacement;

            $(element).popover({
                title: _header,
                content: _content,
                placement: _placement
            });

            $(element).hover(function () {
                $(element).popover('show');
            }, function () {
                $(element).popover('hide');
            });
        }
    };
});