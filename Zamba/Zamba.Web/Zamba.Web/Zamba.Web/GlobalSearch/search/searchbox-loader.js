var app = angular.module('app', ['ui.bootstrap', 'LocalStorageModule', 'ngSanitize', 'ngEmbed', 'ngAnimate', 'ngMessages']);
app.run(['$http', '$rootScope', 'authService', 'cacheService', 'uiService', function ($http, $rootScope, authService, cacheService, uiService) {
    
    authService.checkToken();

    try {

        try {
            cacheService.CheckLastDesignVersion();
        } catch (e) {
            
            console.log(e.message);
        }        
        

        // comentado por sebastian (inicio)

        //Comprueba token en LS y verifica que no haya caducado
        //var token = authService.fillAuthData();
        //if (token == "invalid user") {
        //    authService.logOut();
        //    return;
        //}

        //if (token == null) {
        //    authService.getNewAuthData().then(function (d) {
        //        if (d == "") {
        //            console.log("No se pudo generar RS token");
        //            authService.logOut();
        //            return;
        //        }
        //        else {
        //            var tokenStr = JSON.parse(d).token;
        //            $http.defaults.headers.common['Authorization'] = 'Bearer ' + tokenStr;
        //            $http.defaults.headers.common['UserId'] = GetUID();
        //            //  localStorageService.set('authorizationData', JSON.parse(d));
        //            //  angular.element($('#zambasearchcontrol')).scope().GetEntities();
        //            //  GetTree();
        //            // authService.CheckIfTokenIsValid();
        //            keepSessionAlive();

        //        }
        //    }).catch(function (err) {
        //        console.log(err);
        //        authService.logOut();
        //        return;
        //    });

        //}
        //else {
        //    authService.CheckIfTokenIsValid(token);
        //    $http.defaults.headers.common['Authorization'] = 'Bearer ' + token;
        //    $http.defaults.headers.common['UserId'] = GetUID();
        //    //       angular.element($('#zambasearchcontrol')).scope().GetEntities();
        //    //   GetTree();
        //    keepSessionAlive();
        //}

        // comentado por sebastian (fin)


    } catch (e) {
        console.log(e.message);
        //window.onbeforeunload = function () {
        //    showedDialog = true;
        //};
        //var destinationURL = "../../views/Security/Login.aspx?ReturnUrl=" + window.location;
        //document.location = destinationURL;
        authService.logOut();
        return;
    }


    //var LogOut = function () {
    //    authService._logOut();

    //    window.onbeforeunload = function () {
    //        showedDialog = true;
    //    };
    //    var destinationURL = "../../views/Security/Login.aspx?ReturnUrl=" + window.location;
    //    document.location = destinationURL;
    //}



}]);


//function LogOut() {
//    window.onbeforeunload = function () {
//        showedDialog = true;
//    };
//    var destinationURL = "../../views/Security/Login.aspx?ReturnUrl=" + window.location;
//    document.location = destinationURL;
//}



//Config for Cross Domain
app.config(['$httpProvider', function ($httpProvider) {
    
    $httpProvider.defaults.useXDomain = true;
    delete $httpProvider.defaults.headers.common['X-Requested-With'];
    
}
]);





app.controller('appController', ['$http', '$scope', '$rootScope', 'authService', 'Search', function ($http, $scope, $rootScope, authService, Search) {


    $scope.MyUnreadTasks = 0;

    //LoadMyTasksCount($("#MyTasksAnchor"));

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

    $scope.IsInitializingThumbs = true;

    $scope.ThumbsPathHome = function () {
        var userid = GetUID();

        //if (userid != undefined && userid != '' && $scope.IsInitializingThumbs === true) {
        //    $scope.IsInitializingThumbs = false;


        if (localStorage) {
            var userPhoto = localStorage.getItem('userPhoto-' + userid);
            if (userPhoto != undefined && userPhoto != null) {
                try {
                    var response = userPhoto;
                    $scope.thumphoto = userPhoto;

                    return response;

                } catch (e) {
                    console.log(e);
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
        //}

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
                    localStorage.setItem('userPhoto-' + userid, response);

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

        var userid = parseInt(GetUID());

        if (userid != undefined && userid != '' && $scope.IsInitializing === true) {
            $scope.IsInitializing = false;


            var DV = null;

            if (localStorage) {
                DV = localStorage.getItem("UV|" + userid);
            }

            if (DV != null && DV != '') {
                $scope.IsAdminUserView = DV;
                return DV;
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
                        if (localStorage) {
                            localStorage.setItem("UV|" + userid, response);
                        }
                    }
            });


        }
        return $scope.IsAdminUserView;
    }

    $scope.GetCurrentUserName();

    $scope.Logout = function () {
        var isOkta = false;
        var token_id_okta = JSON.parse(localStorage.getItem('authorizationData')).oktaAccessToken;
        if (token_id_okta != undefined && token_id_okta != null && token_id_okta != "") {
            var isOkta = true;
        }
        authService.logOut();
        window.onbeforeunload = function () {
            showedDialog = true;
        };
        if (isOkta) {
            var destinationURL = "../../views/Security/OktaAuthentication.html?logout=true";//?ReturnUrl=" + window.location;
        }
        else {
            var destinationURL = "../../views/Security/Login.aspx";//?ReturnUrl=" + window.location;
        }
        localStorage.removeItem('OKTA');
        document.location = destinationURL;
    }

    //if (authService.authentication.isAuth) {
        $rootScope.$emit('GetEntities');
        //        $scope.GetEntities();
    //}
    $scope.GetNextUrl = function (data) {
        $rootScope.$emit('GetNextUrl', data);
    };

    $scope.DoSearchByQS = function (data) {
        $rootScope.$emit('DoSearchByQS', data);
    };
}]);

var listGridAPI = ZambaWebRestApiURL + "/search/GetListGrid";
app.service('ngservice', function ($http) {
    this.getOrders = function () {
        var res = $http.get(listGridAPI);
        return res;
    };
    //The function to read Orders based on filter and its value
    //The function reads all records if value is not entered
    //else based on the filter and its value, the Orders will be returned
    this.getfilteredData = function (filter, value) {
        var res;
        if (value.length == 0) {
            res = $http.get(listGridAPI);
            return res;
        } else {
            res = $http.get(listGridAPI + "/" + filter + "/" + value);
            return res;
        }
    };
});

app.controller('ngcontroller', function ($scope, ngservice) {
    $scope.SelectedCriteria = ""; //The Object used for selecting value from <option>
    $scope.filterValue = ""; //The object used to read value entered into textbox for filtering information from table
    loadOrders();

    //Function  to load all Orders
    function loadOrders() {
        var promise = ngservice.getOrders();
        promise.then(function (resp) {
            var keys = [];
            var data = JSON.parse(resp.data);
            forIn(data[0], function (val, key, o) {
                // result += val;
                keys.push(key);
            });
            $scope.GridData = data;
            $scope.Selectors = keys;
            $scope.GridColumns = keys;
            $scope.Message = data.length + " registros";
        }, function (err) {
            $scope.Message = "Error al traer los datos: " + err.status;
        });
    };

    function forIn(obj, fn, thisObj) {
        var key, i = 0;
        for (key in obj) {
            if (exec(fn, obj, key, thisObj) === false) {
                break;
            }
        }
        function exec(fn, obj, key, thisObj) {
            return fn.call(thisObj, obj[key], key, obj);
        }
        return forIn;
    }

    //Function to load orders based on a criteria
    $scope.getFilteredData = function () {
        var promise = ngservice.getfilteredData($scope.SelectedCriteria, $scope.filterValue);
        promise.then(function (resp) {
            var keys = [];
            var data = JSON.parse(resp.data);
            forIn(data[0], function (val, key, o) {
                keys.push(key);
            });
            $scope.GridData = data;
            $scope.Selectors = keys;
            $scope.GridColumns = keys;
            //$scope.Orders =  JSON.parse(resp.data);
            $scope.Message = data.length + " registros";
        }, function (err) {
            $scope.Message = "Error al traer los datos: " + err.status;
        });
    };
});




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



function setRightstoInsert() {
    var RightstoInsert = false;
    var UserId = GetUID();
    $.ajax({
        type: 'POST',
        url: ZambaWebRestApiURL + '/Insert/GetUserRightToInsert?' + jQuery.param({
            userid: UserId
        }),
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        async: false,
        //data: JSON.stringify(UserId),
        success: function (response) {
            RightstoInsert = response;
        },
        error:
            function (data, status, headers, config) {
                console.log(data);
            }
    });
    if (RightstoInsert == true) {
        $("#liInsert").css("display", "block");
    } else {
        $("#liInsert").css("display", "none");
    }
}

function setRightstoSearchWeb() {
    var RightstoSearchWeb = false;
    var UserId = GetUID();
    $.ajax({
        type: 'POST',
        url: ZambaWebRestApiURL + '/search/GetUserRightToSearchWeb?' + jQuery.param({
            userid: UserId
        }),
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        async: false,
        //data: JSON.stringify(UserId),
        success: function (response) {
            RightstoSearchWeb = response;
        },
        error:
            function (data, status, headers, config) {
                console.log(data);
            }
    });
    if (RightstoSearchWeb == true) {
        $("#searchModeGS").css("display", "block");
    } else {
        $("#searchModeGS").css("display", "none");
    }

}