'use strict';

angular.module('SmartAdmin.Layout').directive('bigBreadcrumbs', function () {
    return {
        restrict: 'EA',
        replace: true,
        template: '<div><h2 class="page-title txt-color-blueDark"></h2></div>',
        scope: {
            items: '=',
            icon: '@'
        },
        link: function (scope, element) {
            var first = _.first(scope.items);

            var icon = scope.icon || 'home';
            element.find('h2').append('<i class="fa-fw fa fa-' + icon + '"></i> ' + first);
            _.rest(scope.items).forEach(function (item) {
                element.find('h2').append(' <span>> ' + item + '</span>')
            })
        }
    }
});
