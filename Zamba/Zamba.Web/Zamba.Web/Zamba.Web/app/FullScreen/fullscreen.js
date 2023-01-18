app.directive('fullscreenButton', function ($document) {
    return {
        restrict: 'EA',
        scope: {
            target: '@fullscreenButton',
            isFullscreen: '=?',
            onChange: '&'
        },
        link: function (scope, element, attrs) {
            var doc = $document[0];
            var onChange = function () {
                scope.$apply(function () {
                    scope.isFullscreen = !!(document.fullscreenElement || document.mozFullScreenElement || document.webkitFullscreenElement);
                    scope.onChange({ fullscreen: scope.isFullscreen });
                });
            }

            angular.forEach(['', 'moz', 'webkit'], function (prefix) {
                $document.bind(prefix + 'fullscreenchange', onChange);
            });

            element.bind('click', function (event) {
                event && event.preventDefault();

                var el;
                if (scope.target) {
                    if (angular.isString(scope.target)) {
                        el = doc.querySelector(scope.target);
                    }
                } else {
                    el = doc.documentElement;
                }

                if (!el) return;

                if (el.requestFullscreen) {
                    el.requestFullscreen();
                } else if (el.mozRequestFullScreen) {
                    el.mozRequestFullScreen();
                } else if (el.webkitRequestFullscreen) {
                    el.webkitRequestFullscreen();
                } else {
                    window.open(doc.location.href, '_blank');
                }
            });
        }
    }
});