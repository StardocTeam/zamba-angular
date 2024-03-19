'use strict';
var serviceBase = ZambaWebRestApiURL;
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

app.factory('authService', ['$http', '$q', 'localStorageService', 'ngAuthSettings', '$interval', function ($http, $q, localStorageService, ngAuthSettings, $interval) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var authServiceFactory = {};

    //if (window.localStorage.getItem('authorizationData') !== null && window.localStorage.getItem('authorizationData') !== "" )
    connectionUserActive();

    _KeepAlive();

    function _KeepAlive() {

        $interval(function () {

            /* if (window.localStorage.getItem('authorizationData') != null && window.localStorage.getItem('authorizationData') != "")*/
            //_removeCurrentUser();
            try {
                connectionUserActive();
            } catch (e) {
                console.log("Error en el login del usuario" + e)
            }
           

        }, 60000);

    }

    function connectionUserActive() {
        
        const UserIdService = parseInt(GetUID());
        
        /*if (!$("#modalLogin").hasClass("in")) {*/
            if (window.localStorage.getItem('authorizationData') != null && window.localStorage.getItem('authorizationData') != "") {
                //let userToken = JSON.parse(localStorage.getItem('authorizationData'));
                const UrlParameters = window.location.search;
                const urlParams = new URLSearchParams(UrlParameters);
                let token = urlParams.get('t');
                /*let userToken = JSON.parse(localStorage.getItem('authorizationData'));*/
                
                let UserIdStorage = parseInt(localStorage.getItem('UserId'));
               /* let { token } = userToken;*/
                if (token != null) {
                    $.ajax({
                        type: "POST",
                        url: "../../Services/TaskService.asmx/IsConnectionActive",
                        data: jQuery.param({ userId: UserIdService, token: token, userLocalStorage: UserIdStorage }),
                        async: true,
                        success: function (response) {


                            let { IsValid, isReload, RebuildUrl, NewToken } = JSON.parse(response.children[0].textContent);
                            console.log("trace servicio IsValid: " + IsValid);
                            console.log("trace servicio RebuildUrl: " + RebuildUrl);
                            let UserIdStorage = localStorage.getItem('UserId');

                            if (IsValid == "false") {

                                console.log("trace servicio boolValue es false se manda al loguin ");
                                _showModalLoginInSearch(UserIdService, isReload);

                            } else if (RebuildUrl == "True" || UserIdStorage != UserIdService.toString()) {

                                _removeCurrentUser();

                                var ParameterByUrlOld = location.search.replace("?", "").split("&");

                                let ParameterByUrlNew = ParameterByUrlOld.map(function (element) {

                                    if (element.includes("t=")) {
                                        return element = "t=" + NewToken;
                                    } else if (element.includes("user") || element.includes("User")) {

                                        if (element.includes("user"))
                                            return element = element.split("=")[0] + "=" + UserIdStorage;

                                        if (element.includes("User"))
                                            return element = element.split("=")[0] + "=" + UserIdStorage;

                                    } else {

                                        if (element != "")
                                            return element;
                                    }
                                });


                                let FinalUrlParameter = "?";
                                let iteracion = 0;
                                ParameterByUrlNew.forEach(function (element) {
                                    iteracion++;
                                    if (element != undefined)
                                        if (iteracion === element.length) {
                                            FinalUrlParameter += (element)
                                        } else {
                                            if (ParameterByUrlNew[iteracion + 1] != undefined);
                                            FinalUrlParameter += (element + "&");
                                        }
                                });
                                if (FinalUrlParameter != '?&') {
                                    window.location.href = location.href.split("?")[0] + FinalUrlParameter;
                                }
                                else {
                                    console.log("trace servicio boolValue es false se manda al loguin ");
                                    _showModalLoginInSearch(UserIdService, isReload);
                                }
                            } else if (IsValid) {
                                if ($("#modalLogin").hasClass("in")) {
                                    $("#modalLogin").modal('hide');
                                }
                            }
                        },
                        error: function (request, status, error) {
                            console.log(error);
                            window.location.href = thisDomain;
                            //_showModalLoginInSearch(UserIdService, true);
                        }
                    });
                }


            } else {
                _showModalLoginInSearch(UserIdService, true);
            }
      /*  }*/
    }

    var _authentication = {
        isAuth: false,
        userName: "",
        useRefreshTokens: false,
        generateDate: ""
    };

    var _externalAuthData = {
        provider: "",
        userName: "",
        externalAccessToken: ""
    };

    var _saveRegistration = function (registration) {

        _logOut();

        return $http.post(serviceBase + '/account/register', registration).then(function (response) {
            return response;
        });

    };

    var _login = function (loginData) {

        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

        if (loginData.useRefreshTokens) {
            data = data + "&client_id=" + ngAuthSettings.clientId;
        }

        var deferred = $q.defer();

        $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).then(function (response) {

            if (loginData.useRefreshTokens) {

                if (window.localStorage) {
                    if (window.localStorage.getItem('authorizationData') !== undefined) {
                        window.localStorage.removeItem('authorizationData');
                        window.localStorage.setItem('authorizationData', JSON.stringify({ token: response.access_token, userName: loginData.userName, refreshToken: response.refresh_token, useRefreshTokens: true, UserId: GetUID() }));
                    }
                }
            }
            else {


                if (loginData.useRefreshTokens) {

                    if (window.localStorage) {
                        if (window.localStorage.getItem('authorizationData') !== undefined) {
                            window.localStorage.removeItem('authorizationData');
                            window.localStorage.setItem('authorizationData', JSON.stringify({ token: response.access_token, userName: loginData.userName, refreshToken: "", useRefreshTokens: false, UserId: GetUID() }));
                        }
                    }
                }
            }
            _authentication.isAuth = true;
            _authentication.userName = loginData.userName;
            _authentication.useRefreshTokens = loginData.useRefreshTokens;
            _authentication.generateDate = new Date();
            deferred.resolve(response);

        }).catch(function (err, status) {
            _logOut();
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _logOut = function () {
        
        try {            
            _removeConnectionFromWeb();
            //_removeZzsToken();


            //            localStorageService.remove('authorizationData');

            var RecordTree = window.localStorage.getItem('localTreeData|' + GetUID())
            if (window.localStorage)
                window.localStorage.clear();
            if (RecordTree != undefined)
                window.localStorage.setItem('localTreeData|' + GetUID(), RecordTree);
            _authentication.isAuth = false;
            _authentication.userName = "";
            _authentication.useRefreshTokens = false;
            _authentication.generateDate = "";
        } catch (e) {
            console.error(e);
        }

        //_showModalLoginInSearch();
    };



    function _showModalLoginInSearch(UserId, isReload) {
        var UserId = UserId == undefined || UserId == NaN ? parseInt(GetUID()) : UserId;
        var linkSrc = window.location.protocol + '//' + window.location.host + '/' + window.location.pathname.split('/')[1] + "/views/security/login?showModal=true&reloadLogin=" + isReload + "&userid=" + UserId;
        var authMethod = localStorage.getItem("authMethod");
        if (authMethod == "OKTA") {
            linkSrc = window.location.protocol + '//' + window.location.host + '/' + window.location.pathname.split('/')[1] + "/views/security/Okta/OktaAuthentication.html?initSession=false&showModal=true&reloadLogin=" + isReload + "&userid=" + UserId;
        }        
        document.getElementById('iframeModalLogin').src = linkSrc;
        $('#modalLogin').modal('show');
    }

    var _getNewAuthData = function () {
        var UserId = parseInt(GetUID());
        if (UserId > 0) {
            var deferred = $q.defer();


            $http.get(ZambaWebRestApiURL + "/Account/LoginById?userId=" + UserId).then(function (tkn) {
                if (tkn == null || tkn == "") {
                    console.log("Problema al generar token");
                    return;
                }
                var datenow = new Date().toLocaleDateString();
                var tokenInfo = JSON.parse(typeof (tkn.data) == "string" ? tkn.data : tkn.data.d);
                /*if (new Date(tokenInfo.tokenExpire) >= datenow) {*///Token no haya expirado - Fecha exp mayor a ahora

                if (window.localStorage) {
                    if (window.localStorage.getItem('authorizationData') !== undefined) {
                        window.localStorage.removeItem('authorizationData');
                        window.localStorage.setItem('authorizationData', JSON.stringify({
                            token: tokenInfo.token,
                            userName: tokenInfo.userName,
                            refreshToken: tokenInfo.refreshToken,
                            useRefreshTokens: tokenInfo.useRefreshTokens,
                            generateDate: new Date(), UserId: GetUID()
                        }));
                    }
                }
                _authentication.isAuth = true;
                _authentication.userName = tokenInfo.userName;
                _authentication.useRefreshTokens = tokenInfo.useRefreshTokens;
                deferred.resolve(tkn);
                //}
                //else {
                //    //TODO relogin
                //    console.error("Token invalido");             
                //}
            }).catch(function (err, status) {
                _logOut();
                deferred.reject(err);
            });

            return deferred.promise;
        }
        else {
            _logOut();
        }
    };

    var _fillAuthData = function () {
        var authData = window.localStorage.getItem('authorizationData');
        if (authData == undefined || authData == null || authData == "[object Object]") {
            return 'invalid user';
        }

        try {
            authData = JSON.parse(authData);
            if (authData == null || authData.UserId == undefined || authData.UserId != GetUID()) {
                window.localStorage.removeItem('authorizationData');
                return 'invalid user';
            }
        } catch (e) {
            console.error(e);
            var curruserId = window.localStorage.getItem('UserId');
            if (curruserId == null || curruserId == undefined || curruserId != GetUID()) {
                window.localStorage.removeItem('authorizationData');
                return 'invalid user';
            }

        }

        var tokenExpire = moment(authData.tokenExpire);
        var validDate = moment(new Date());
        if (validDate.format("DD/MM/YYYY HH:mm:ss") >= tokenExpire._i) {
            return;
        }
        //var tokenExpire = moment(authData.tokenExpire)
        //if (moment(new Date()) >= tokenExpire) {
        //    return;
        //}


        _authentication.isAuth = true;
        _authentication.userName = authData.userName;
        _authentication.useRefreshTokens = authData.useRefreshTokens;

        return authData.token;
    };

    var _refreshToken = function () {
        var deferred = $q.defer();
        var authData = window.localStorage.getItem('authorizationData');
        if (authData) {
            if (authData.useRefreshTokens) {
                var data = "grant_type=refresh_token&refresh_token=" + authData.refreshToken + "&client_id=" + ngAuthSettings.clientId;


                if (window.localStorage) {
                    if (window.localStorage.getItem('authorizationData') !== undefined) {
                        window.localStorage.removeItem('authorizationData');
                        window.localStorage.removeItem('authorizationData');
                    }
                }

                $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).then(function (response) {

                    if (window.localStorage) {
                        if (window.localStorage.getItem('authorizationData') !== undefined) {
                            window.localStorage.removeItem('authorizationData');
                            window.localStorage.setItem('authorizationData', JSON.stringify({ token: response.access_token, userName: response.userName, refreshToken: response.refresh_token, useRefreshTokens: true, UserId: GetUID() }));
                        }
                    }



                    deferred.resolve(response);

                }).catch(function (err, status) {
                    _logOut();
                    deferred.reject(err);
                });
            }
        }
        return deferred.promise;
    };

    var _obtainAccessToken = function (externalData) {

        var deferred = $q.defer();

        $http.get(serviceBase + '/account/ObtainLocalAccessToken', { params: { provider: externalData.provider, externalAccessToken: externalData.externalAccessToken } }).then(function (response) {

            if (window.localStorage) window.localStorage.setItem('authorizationData', JSON.stringify({ token: response.access_token, userName: response.userName, refreshToken: "", useRefreshTokens: false, UserId: GetUID() }));

            _authentication.isAuth = true;
            _authentication.userName = response.userName;
            _authentication.useRefreshTokens = false;

            deferred.resolve(response);

        }).catch(function (err, status) {
            _logOut();
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _registerExternal = function (registerExternalData) {

        var deferred = $q.defer();

        $http.post(serviceBase + '/account/registerexternal', registerExternalData).then(function (response) {

            if (window.localStorage) {
                if (window.localStorage.getItem('authorizationData') !== undefined) {
                    window.localStorage.removeItem('authorizationData');
                    window.localStorage.setItem('authorizationData', JSON.stringify({ token: response.access_token, userName: response.userName, refreshToken: "", useRefreshTokens: false, UserId: GetUID() }));
                }
            }

            _authentication.isAuth = true;
            _authentication.userName = response.userName;
            _authentication.useRefreshTokens = false;

            deferred.resolve(response);

        }).catch(function (err, status) {
            _logOut();
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _getTokenInfo = function () {
        return window.localStorage.getItem('authorizationData');
    };


    var _removeConnectionFromWeb = function () {
        try {
            var connectionId = parseInt(window.localStorage.getItem("ConnId"));
            if (connectionId != undefined && connectionId > 0) {
                connectionId = window.localStorage.getItem("ConnId");
                $http.post(serviceBase + '/account/RemoveConnection?ConnId=' + connectionId).then(function (response) {
                }).catch(function (err, status) {
                });
            }
        } catch (e) {
            console.error(e);
        }


    };



    var _removeCurrentUser = function () {
        try {
            $.ajax({
                type: "POST",
                url: "../../Services/TaskService.asmx/RemoveConnectionFromWeb",
                data: jQuery.param({ connectionId: 0, computer: "" }),
                async: false,
                success: function (response) {
                    var result = response;
                },
                error: function (request, status, err) {
                    console.log(err);
                }
            });

        } catch (e) {
            console.error(e);
        }


    };


    var _removeZzsToken = function () {
        try {
            var userid = GetUID();
            if (userid != undefined && userid > 0) {
                userid = parseInt(userid);

                $http.post(serviceBase + '/account/removeZssUser?Userid=' + userid).then(function (response) {
                }).catch(function (err, status) {

                });
            }
        } catch (e) {
            console.error(e);
        }
    };



    var _CheckIfTokenIsValid = function (token) {

        if (token != undefined) {
            $http.post(serviceBase + '/account/CheckToken?UserId=' + GetUID() + '&Token=' + token).then(function (response) {
                if (response == false)
                    _logOut();
                else {
                    $http.defaults.headers.common['Authorization'] = 'Bearer ' + token;
                    $http.defaults.headers.common['UserId'] = GetUID();
                }

            }).catch(function (err, status) {
                console.log(err);

                if (window.localStorage.getItem('authorizationData') != undefined) {
                    var currentToken = window.localStorage.getItem('authorizationData').token;
                    token = currentToken;
                }

                if (token != undefined) {
                    $http.post(serviceBase + '/account/CheckToken?UserId=' + GetUID() + '&Token=' + token).then(function (response) {
                        if (response == false)
                            _logOut();
                        else {
                            $http.defaults.headers.common['Authorization'] = 'Bearer ' + currentToken;
                            $http.defaults.headers.common['UserId'] = GetUID();
                        }

                    }).catch(function (err, status) {
                        console.log(err);
                        _logOut();
                    });

                }


            });

        }
    }




    authServiceFactory.saveRegistration = _saveRegistration;
    authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.fillAuthData = _fillAuthData;
    authServiceFactory.authentication = _authentication;
    authServiceFactory.getNewAuthData = _getNewAuthData;
    authServiceFactory.refreshToken = _refreshToken;
    authServiceFactory.getTokenInfo = _getTokenInfo;

    authServiceFactory.obtainAccessToken = _obtainAccessToken;
    authServiceFactory.externalAuthData = _externalAuthData;
    authServiceFactory.registerExternal = _registerExternal;
    authServiceFactory.removeConnectionFromWeb = _removeConnectionFromWeb;
    authServiceFactory.removeZssToken = _removeZzsToken;
    authServiceFactory.removeCurrentUser = _removeCurrentUser;
    authServiceFactory.CheckIfTokenIsValid = _CheckIfTokenIsValid;


    return authServiceFactory;
}]);

function RestApiToken() {
    return angular.element(document.body).injector().get("authService");
}

