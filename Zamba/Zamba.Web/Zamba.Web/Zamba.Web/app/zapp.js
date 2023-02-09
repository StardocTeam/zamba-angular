'use strict';
var app = angular.module('app', ['ui.bootstrap', 'LocalStorageModule', 'ngSanitize', 'ngEmbed', 'ngAnimate', 'ngMessages', "xeditable", 'angular.filter']);

app.run(['$http', '$rootScope', 'editableOptions', function ($http, $rootScope, editableOptions) {
    editableOptions.theme = 'bs3';
}]);




//Config for Cross Domain
app.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.defaults.useXDomain = true;
    delete $httpProvider.defaults.headers.common['X-Requested-With'];
}
]);

app.factory('Search', function () {
    return {
        SearchId: 0,
        OrganizationId: 0,
        DoctypesIds: [],
        Indexs: [],
        blnSearchInAllDocsType: true,
        TextSearchInAllIndexs: '',
        RaiseResults: false,
        ParentName: '',
        CaseSensitive: false,
        MaxResults: 1000,
        ShowIndexOnGrid: true,
        UseVersion: false,
        UserId: 0,
        GroupsIds: [],
        StepId: 0,
        StepStateId: 0,
        TaskStateId: 0,
        WorkflowId: 0,
        NotesSearch: '',
        Textsearch: '',
        SearchResults: null,
        OrderBy: null,
        Filters: [],

    }
});


app.factory('FieldsService', function ($http) {
    var BaseURL = '';
    var fac = {};
    fac.GetAll = function (IndexId) {
        return $http.post(ZambaWebRestApiURL + '/search/FillIndex?IndexId=' + IndexId).then(function (response) {
            return response;
        },
            function (response) {
                var errors = [];
                for (var key in response.data.modelState) {
                    for (var i = 0; i < response.data.modelState[key].length; i++) {
                        errors.push(response.data.modelState[key][i]);
                    }
                }
                $scope.message = "Failed to register  due to:" + errors.join(' ');
            });
    }
    return fac;
});


app.controller('appController', ['$http', '$scope', '$rootScope',  'Search', function ($http, $scope, $rootScope,  Search) {

    $scope.CurrentUserName = 'Iniciar sesion';

   
    $scope.GetCurrentUserName = function () {
        try {
            var userId = GetUID();

            if (localStorage) {
                var a = localStorage.getItem('UD|' + userId);
                if (a != undefined) {
                    var a = JSON.parse(data.data)
                    $scope.CurrentUserName = a[0];
                    $scope.CurrentApellido = a[1];
                    $scope.CurrentUsuario = a[2];
                    $scope.CurrentPuesto = a[3];
                    $scope.CurrentTelefono = a[4];
                }
                else {
                    $scope.LoadUserNameFromDB();
                }
            }
            else {
                $scope.LoadUserNameFromDB();
            }

        } catch (e) {
            $scope.LoadUserNameFromDB();

        }
    };


    $scope.LoadUserNameFromDB = function () {
        var idusuario = GetUID();
        $http({
            method: 'GET',
            url: ZambaWebRestApiURL + "/search/GetUserData?userId=" + idusuario,
            crossDomain: true,
            //params: { userId: idusuario },
            dataType: 'json',
            headers: { 'Content-Type': 'application/json' }
        }).then(function (data) {
            var a = JSON.parse(data.data)
            $scope.CurrentUserName = a[0];
            $scope.CurrentApellido = a[1];
            $scope.CurrentUsuario = a[2];
            $scope.CurrentPuesto = a[3];
            $scope.CurrentTelefono = a[4];

            if (localStorage) {
                localStorage.setItem('UD|' + idusuario, a);
            }
        });
    };


    $scope.GetCurrentUserName();

}]);




function GetUID() {
    var userid = 0;
    userid = getUrlParameters().user;
    if (userid > 0) return userid;
    userid = getUrlParameters().userid;
    if (userid > 0) return userid;
    userid = getUrlParameters().u;
    if (userid > 0) return userid;

    var e = $('#hdnUserId');

    if (e.length == 0) e = $('#ContentPlaceHolder_hdnUserId');
    if (e.length == 0) e = $('#ctl00_hdnUserId');
    if (e.length == 0) e = $('#hdnUserId');
    if (e.length == 0) e = $('hdnUserId');

    userid = $(e).val();
    if (userid > 0) return userid;


    var authData = localStorage.getItem('authorizationData');
    if (authData == undefined || authData == null || authData == "[object Object]" || authData == "") {
        return;
    }

    authData = JSON.parse(authData);
    if (authData == null || authData.UserId == undefined) {
        userid = authData.UserId;
        if (userid > 0) return userid;

    }

    return;
};

function GetDOCID() {
    var docid = 0;
    docid = getUrlParameters().docid;
    if (docid > 0) return docid;
    docid = getUrlParameters().did;
    if (docid > 0) return docid;
    docid = getUrlParameters().doc_id;
    if (docid > 0) return docid;
    docid = Number($("[id$=Hiddendocid]").val());
    if (docid > 0) return docid;
    return 0;
};

function GetTASKID() {
    var taskid = 0;
    taskid = getUrlParameters().taskid;
    if (taskid > 0) return taskid;
    taskid = getUrlParameters().tid;
    if (taskid > 0) return taskid;
    taskid = getUrlParameters().task_id;
    if (taskid > 0) return taskid;
    return 0;
};

function GetDocTypeId() {

    var docTypeId = 0;
    try {

        docTypeId = getUrlParameters().doctype;

        if (docTypeId > 0)
            return docTypeId;

        docTypeId = getUrlParameters().doc_type_id;

        if (docTypeId > 0)
            return docTypeId;

        docTypeId = getUrlParameters().dt_id;

        if (docTypeId > 0)
            return docTypeId;

        docTypeId = getUrlParameters().eid;

        if (docTypeId > 0)
            return docTypeId;


        return docTypeId;
    } catch (e) {

        console.error("No se pudo obtener el docTypeId", e);
        return docTypeId;

    }
};

function getUrlParameters() {
    var pairs = window.location.search.substring(1).split(/[&?]/);
    var res = {}, i, pair;
    for (i = 0; i < pairs.length; i++) {
        pair = pairs[i].toLowerCase().split('=');
        if (pair[1])
            res[decodeURIComponent(pair[0])] = decodeURIComponent(pair[1]);
    }
    return res;
};


function getUrlParametersUser() {
    var pairs = window.location.search.substring(1).split(/[&?]/);
    var res = {}, i, pair;
    for (i = 0; i < pairs.length; i++) {
        pair = pairs[i].toLowerCase().split('=');
        if (pair[1])
            res[decodeURIComponent(pair[0])] = decodeURIComponent(pair[1]);
    }
    return res;
};

