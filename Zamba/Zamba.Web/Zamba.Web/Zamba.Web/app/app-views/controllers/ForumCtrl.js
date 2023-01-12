'use strict';

app.controller('ZambaForumController', function ($scope, forumServices) {

    $scope.forums = [];

    $scope.LoadForums = function (entityId, parentResultId) {
        $scope.forums = forumServices.getForums(entityId, parentResultId);
    };

    $scope.newForum = {
        title: '',
        usersIds: [],
        createdDate: null,
        lastUpdateDate: null,
        lastmessage: '',
        lastuser: 0,
        createdUser: 0,
        messages: [],
    };

    $scope.GetnewForum = function () {
        var newForum = {
            title: '',
            usersIds: [],
            createdDate: null,
            lastUpdateDate: null,
            lastmessage: '',
            lastuser: 0,
            createdUser: 0,
            messages: [],
        };
        return newForum;
    };

    $scope.AddnewForum = function (newForum) {

      var  currentForum = $scope.GetnewForum();

        currentForum.title = newForum.title;
        currentForum.createdDate = moment().format("DD/MM/YY");;
        currentForum.lastUpdateDate = moment().format("DD/MM/YY");;
        currentForum.createdUser = GetUID();
        newForum.lastuser = GetUID();

        if ($scope.forums == undefined)
            $scope.forums = [];

        $scope.forums.push(currentForum);
        forumServices.insertForum(text, usersIds);

        newForum = $scope.GetnewForum();
    };

    $scope.SetselectedForum = function (forum) {
        $scope.selectedForum = forum;
    };

    $scope.postMessage = function (forum, message) {
        var msg = $scope.GetnewMessage();

        msg.message = message;
        msg.createdDate = moment().format("DD/MM/YY");;
        msg.lastUpdateDate = moment().format("DD/MM/YY");;
        msg.createdUser = GetUID();
        msg.lastuser = GetUID();
        forum.messages.push(msg);
        forumServices.insertForum(text, usersIds);
    };

    $scope.GetnewMessage = function () {
        var newMsg = {
            message: '',
            createdDate: null,
            createdUser: 0,
        };
        return newMsg;
    };

    $scope.selectedForum = null;


});




app.directive('zambaForum', function ($sce) {
    return {
        restrict: 'E',
        //scope: false,
        //replace: true,
        transclude: true,
        link: function ($scope, element, attributes) {
            $scope.entityId = attributes.entityId;


            var url = window.location.href;
            var segments = url.split("&");
            segments.forEach(function (valor) {
                if (valor.includes("docid")) { $scope.parentResultId = valor.split("=")[1]; }
                if (valor.includes("taskid")) { $scope.taskId = valor.split("=")[1]; }
            });

            bool = false;

            $scope.LoadForums($scope.entityId, $scope.parentResultId);
        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/app-views/views/forum.html?v=248'),

    }
});