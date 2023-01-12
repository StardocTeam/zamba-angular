'use strict';
var app = angular.module('app', ['ui.bootstrap', 'LocalStorageModule', 'ngSanitize', 'ngEmbed', 'ngAnimate', 'ngMessages', "xeditable", 'angular.filter', 'pascalprecht.translate']);

app.run(['$http', '$rootScope', 'editableOptions', "$translate", function ($http, $rootScope, editableOptions, $translate) {
    editableOptions.theme = 'bs3';
    try {
        var language = "es-ar";
        $translate.use(language);
    } catch (e) {
        console.error(e);
    }

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
        MaxResults: 100,
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


app.controller('appController', ['$http', '$scope', '$rootScope', 'Search', 'authService', function ($http, $scope, $rootScope, Search, authService) {

    $scope.CurrentUserName = 'Iniciar sesion';


    $scope.GetCurrentUserName = function () {
        try {
            var userId = GetUID();

            if (window.localStorage) {
                var a = window.localStorage.getItem('UD|' + userId);
                if (a != undefined) {
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
            console.error(e);
            $scope.LoadUserNameFromDB();

        }
    };


    $scope.LoadUserNameFromDB = function () {
        var idusuario = GetUID();
        if (idusuario != undefined && idusuario > 0) {
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

                if (window.localStorage) {
                    window.localStorage.setItem('UD|' + idusuario, a);
                }
            });

        }
        return '';
    };


    $scope.GetCurrentUserName();


    $scope.ThumbsPathHome = function () {
        var userid = GetUID();

        if (userid != undefined && userid != '') {
            //    $scope.IsInitializingThumbs = false;


            if (window.localStorage) {
                var userPhoto = window.localStorage.getItem('userPhoto-' + userid);
                if (userPhoto != undefined && userPhoto != null) {
                    try {
                        var response = userPhoto;
                        $scope.thumphoto = userPhoto;

                        return response;

                    } catch (e) {
                        console.error(e);
                        var response = $scope.LoadUserPhotoFromDB(userid);
                        $scope.thumphoto = response;
                        return response;
                    }
                }
                else {
                    var response = $scope.LoadUserPhotoFromDB(userid);
                    $scope.thumphoto = response;
                    return response;
                }
            }
            else {
                var response = $scope.LoadUserPhotoFromDB(userid);
                $scope.thumphoto = response;
                return response;
            }
        }

    };



    $scope.LoadUserPhotoFromDB = function (userid) {

        var response = null;
        var genericRequest = {
            UserId: parseInt(userid),

        };

        $.ajax({
            type: "POST",
            url: ZambaWebRestApiURL + "/Search/GetThumbsPathHome",
            data: JSON.stringify(genericRequest),

            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    response = data;
                    $scope.thumphoto = response;
                    window.localStorage.setItem('userPhoto-' + userid, response);

                }
        });
        return response;
    }


    $scope.ChangePassword = function (inputs) {

        var CurrentPassword = inputs.CurrentPassword || "",
            NewPassword = inputs.NewPassword || "",
            NewPassword2 = inputs.NewPassword2 || "";

        $scope.UserChangePassword = {
            Userid: parseInt(GetUID()),
            CurrentPassword: CurrentPassword,
            NewPassword: NewPassword,
            NewPassword2: NewPassword2
        };
        $http.post(ZambaWebRestApiURL + '/Account/Password', $scope.UserChangePassword).then(function successCallback(response) {

            toastr.info(response.data);
            $("#ModalChangePassword").modal('hide');
            inputs.CurrentPassword = "";
            inputs.NewPassword = "";
            inputs.NewPassword2 = "";

            $scope.UpdateUserCache(GetUID());

        }, function errorCallback(response) {
            toastr.error("Error al actulizar su contraseña");

        });
    }

    $scope.UpdateUserCache = function (userid) {

        //actualizo cache en zamba.web
        ClearUserCache(userid);
        //actualizo cache en rest api
        $http.get(ZambaWebRestApiURL + '/Account/ClearUserCache/' + userid);

    }

    $scope.IsInitializing = true;
    $scope.IsAdminUserView = false;

    $scope.UserView = function () {

        var userid = GetUID();
        if (userid != undefined && userid > 0) {
            userid = parseInt(userid);

            if (userid != undefined && userid != '' && $scope.IsInitializing === true) {
                $scope.IsInitializing = false;


                var UV = null;

                if (window.localStorage) {
                    UV = window.localStorage.getItem("UV|" + userid);
                }

                if (UV != null && UV != '') {
                    $scope.IsAdminUserView = UV;
                    return UV;
                }


                var response = null;
                var genericRequest = {
                    UserId: parseInt(userid),

                };

                $.ajax({
                    type: "POST",
                    url: ZambaWebRestApiURL + "/Account/GetUserRights",
                    data: JSON.stringify(genericRequest),

                    contentType: "application/json; charset=utf-8",
                    async: false,
                    success:
                        function (data, status, headers, config) {
                            response = data;
                            $scope.IsAdminUserView = response;
                            if (window.localStorage) {
                                window.localStorage.setItem("UV|" + userid, response);
                            }
                        }
                });


            }
        }
        return $scope.IsAdminUserView;
    }

    $scope.UserView();

    $scope.Logout = function () {
        authService.logOut();
        window.onbeforeunload = function () {
            showedDialog = true;
        };
        var destinationURL = "../../views/Security/Login.aspx?ReturnUrl=" + window.location;
        document.location = destinationURL;
    }

    $scope.GetNextUrl = function (data) {
        
        $rootScope.$emit('GetNextUrl', data);
    };

    $scope.DoSearchByQS = function (data) {
        $rootScope.$emit('DoSearchByQS', data);
    };




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


    var authData = window.localStorage.getItem('authorizationData');
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

        docTypeId = getUrlParameters().doctypeid;
        if (docTypeId > 0)
            return docTypeId
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

