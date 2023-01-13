var appChat = angular.module('appChat', [  'LocalStorageModule', 'ngSanitize', 'ngEmbed', 'ngAnimate']);
appChat.run(['$http',  function ($http) {
    
}]);


appChat.directive('chatView', function ($sce) {
    return {
        restrict: 'E',
       
        transclude: true,
        link: function (scope, element, attributes) {
        },
        templateUrl: $sce.getTrustedResourceUrl('../../scripts/app/view/chat_view.html'),
        controllerAs: 'vm',
        controller: [
            '$scope', '$http', '$attrs', '$element', '$timeout', '$filter', '$rootScope', 'ForumService',
            function ($scope, $http, $attrs, $element, $timeout, $filter, $rootScope,  ForumService) {

                $scope.result = null;
                $scope.forumSearch = {
                    id:0,
                    name : '',
                    lastmessagedate : '',
                    messages :[],
                    UserId : 0,
                    ResultId : 0
                };

                $scope.GetMessages = function (ResultId) {
                    $scope.forumSearch.UserId = GetUID();
                    ForumService.GetAll($scope.forumSearch).then(function (d) {
                        $scope.forum = JSON.parse(d.data);
                      
                    }, function (response) {
                        console.log(response);
                      
                    });
                };




            }]
    };
})



appChat.factory('ForumService', function ($http) {
    var fac = {};
    fac.GetAll = function (ForumSearch) {
        return $http.post(ZambaWebRestApiURL + '/forum/forum', ForumSearch).then(function (response) {
            return response;
        });
    }
    return fac;
});
