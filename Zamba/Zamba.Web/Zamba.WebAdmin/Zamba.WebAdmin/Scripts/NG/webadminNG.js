angular.module('addressFormatter', []).filter('address', function () {
    return function (input) {
        return input.street + ', ' + input.city + ', ' + input.state + ', ' + input.zip;
    };
});

//configFunction.$inject = ['$routeProvider'];
var webadminApp = angular.module('webadminApp', ['ngRoute', 'ngTouch', 'ui.grid', 'ui.grid.edit',
    'ui.grid.rowEdit', 'ui.grid.cellNav', 'addressFormatter'])
    //The Factory used to define the value to
    //Communicate and pass data across controllers
    .factory("ShareData", function () {
        return { value: 0 }
    })

   //.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
   //    // configure the routing rules here
   //    $routeProvider.when('/home/edit:id', {    
   //        controller: 'HelperEditController'
   //    });

   //    // enable HTML5mode to disable hashbang urls
   //    $locationProvider.html5Mode(true);
   //}])

    //Para confirmar la eliminacion de helper
    .directive('ngConfirmClick', [
        function () {
            return {
                link: function (scope, element, attr) {
                    var msg = attr.ngConfirmClick || "Are you sure?";
                    var clickAction = attr.confirmedClick;
                    element.bind('click', function (event) {
                        bootbox.confirm(msg, function (result) {
                            if (result)
                                scope.$eval(clickAction);
                        });
                    });
                }
            };
        }]);


