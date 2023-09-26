'use strict';
var serviceBase = ZambaWebRestApiURL.toLowerCase().replace("/api", "/");
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

app.factory('authService', ['$http', '$q', 'localStorageService', 'ngAuthSettings', function ($http, $q, localStorageService, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var authServiceFactory = {};

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

        return $http.post(serviceBase + 'api/account/register', registration).then(function (response) {
            return response;
        });

    };

    var _login = function (loginData) {

        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;
_logOut
        if (loginData.useRefreshTokens) {
            data = data + "&client_id=" + ngAuthSettings.clientId;
        }

        var deferred = $q.defer();

        $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).then(function (response) {

            if (loginData.useRefreshTokens) {
                if (localStorage) localStorage.setItem('authorizationData', { token: response.data.access_token, userName: loginData.userName, refreshToken: response.data.refresh_token, useRefreshTokens: true, UserId: GetUID() });
            }
            else {
                if (localStorage) localStorage.setItem('authorizationData', { token: response.data.access_token, userName: loginData.userName, refreshToken: "", useRefreshTokens: false, UserId: GetUID() });
            }
            _authentication.isAuth = true;
            _authentication.userName = loginData.userName;
            _authentication.useRefreshTokens = loginData.useRefreshTokens;
            _authentication.generateDate = new Date();
            deferred.resolve(response);

        }).catch(function (err) {
            _logOut();
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _logOut = function () {
        try {
            _removeConnectionFromWeb();
            _removeZzsToken();
            localStorage.removeItem('authorizationData');
            localStorage.removeItem('queryStringAuthorization');
            _authentication.isAuth = false;
            _authentication.userName = "";
            _authentication.useRefreshTokens = false;
            _authentication.generateDate = "";
            var token_id_okta = localStorage.getItem('OKTA');
            //if (token_id_okta != undefined && token_id_okta != null && token_id_okta != "") {

            //    //https://{yourOktaDomain}/logout` +
            //    //`?id_token_hint=${idToken}` +`&post_logout_redirect_uri=${postLogoutUri}`;
            //    //location.href = "../../views/Security/OktaAuthenticacion.html?logout=true";
            //    return;
            //}
            //else {
            //    //location.href = "/Zamba.Web";
            //}


        } catch (e) {
            console.log(e);
        }

        //window.onbeforeunload = function () {
        //    showedDialog = true;
        //};
        var destinationURL = "../../views/Security/Login.aspx?ReturnUrl=" + window.location;
        //document.location = destinationURL;

    };

    var _getNewAuthData = function () {
        var UserId = parseInt(GetUID());
        if (UserId > 0) {
            var deferred = $q.defer();
            $http.get(ZambaWebRestApiURL + "/Account/LoginById?userId=" + UserId).then(function (tkn) {
                if (tkn.data == null || tkn.data == "") {
                    console.log("Problema al generar token");
                    return;
                }
                var datenow = new Date().toLocaleDateString();
                var tokenInfo = JSON.parse(typeof (tkn.data) == "string" ? tkn.data : tkn.data.d);
                if (localStorage) localStorage.setItem('authorizationData', {
                    token: tokenInfo.token,
                    userName: tokenInfo.userName,
                    refreshToken: tokenInfo.refreshToken,
                    useRefreshTokens: tokenInfo.useRefreshTokens,
                    generateDate: new Date(), UserId: GetUID()
                });
                _authentication.isAuth = true;
                _authentication.userName = tokenInfo.userName;
                _authentication.useRefreshTokens = tokenInfo.useRefreshTokens;
                deferred.resolve(tkn);
            }).catch(function (err) {
                _logOut();
                deferred.reject(err);
            });

            return deferred.promise;
        }
        else {
            _logOut();
        }
    };
    function _getQueryStringAuthorization() {
        var userId;
        var token;
        var querystring = "";
        if (localStorage.authorizationData != undefined) {
            var authorizationData = JSON.parse(localStorage.authorizationData);
            userId = authorizationData.UserId;
            token = authorizationData.token;
        }
        else {
            userId = getUrlParameter("userid");
            token = getUrlParameter("token");
        }

        querystring = "userid=" + userId + "&token=" + token.substring(0,180);
        return querystring;
    }

    function getUrlParameter(name) {
        name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
        var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
        var results = regex.exec(location.search);
        return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
    };


    function _getHeaderAuthorization() {
        var userId;
        var token;
        if (localStorage.authorizationData != undefined) {
            var authorizationData = JSON.parse(localStorage.authorizationData);
            userId = authorizationData.UserId;
            token = authorizationData.token;
        }
        else {
            userId = getUrlParameter("userid");
            token = getUrlParameter("token");
        }
        var header = "Bearer " + window.btoa(userId + ":" + token);
        return header;
    }
    var _fillAuthData = function () {
        var authData = localStorage.getItem('authorizationData');
        if ( authData == undefined || authData == null || authData == "[object Object]" ) {
            return 'invalid user';
        }
        if (authData != null && (authData.UserId != undefined && authData.UserId != GetUID())) {

            localStorage.removeItem('authorizationData');
            return 'invalid user';
        }
        else {

        }

        var generateDate = new Date(authData.generateDate);
        var expireDate = generateDate.setDate(generateDate.getDate() + 10);
        if (new Date() >= new Date(expireDate)) {
            return;
        }

        _authentication.isAuth = true;
        _authentication.userName = authData.userName;
        _authentication.useRefreshTokens = authData.useRefreshTokens;
        return;
        return authData.token;
    };

    var _refreshToken = function () {
        var deferred = $q.defer();
        var authData = localStorage.getItem('authorizationData');
        if (authData) {
            if (authData.useRefreshTokens) {
                var data = "grant_type=refresh_token&refresh_token=" + authData.refreshToken + "&client_id=" + ngAuthSettings.clientId;
                if (localStorage) localStorage.removeItem('authorizationData');

                $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).then(function (response) {

                    if (localStorage) localStorage.setItem('authorizationData', { token: response.data.access_token, userName: response.data.userName, refreshToken: response.data.refresh_token, useRefreshTokens: true, UserId: GetUID() });

                    deferred.resolve(response);

                }).catch(function (err) {
                    _logOut();
                    deferred.reject(err);
                });
            }
        }
        return deferred.promise;
    };

    var _obtainAccessToken = function (externalData) {

        var deferred = $q.defer();

        $http.get(serviceBase + 'api/account/ObtainLocalAccessToken', { params: { provider: externalData.provider, externalAccessToken: externalData.externalAccessToken } }).then(function (response) {

            if (localStorage) localStorage.setItem('authorizationData', { token: response.data.access_token, userName: response.data.userName, refreshToken: "", useRefreshTokens: false, UserId: GetUID() });

            _authentication.isAuth = true;
            _authentication.userName = response.data.userName;
            _authentication.useRefreshTokens = false;

            deferred.resolve(response);

        }).catch(function (err) {
            _logOut();
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _registerExternal = function (registerExternalData) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/account/registerexternal', registerExternalData).then(function (response) {

            if (localStorage) localStorage.setItem('authorizationData', { token: response.data.access_token, userName: response.data.userName, refreshToken: "", useRefreshTokens: false, UserId: GetUID() });

            _authentication.isAuth = true;
            _authentication.userName = response.data.userName;
            _authentication.useRefreshTokens = false;

            deferred.resolve(response);

        }).catch(function (err) {
            _logOut();
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _getTokenInfo = function () {
        return localStorage.getItem('authorizationData');
    };

    var _removeConnectionFromWeb = function () {
        if (localStorage) {
            if (localStorage.getItem("ConnId") != null) {
                var connectionId = parseInt(localStorage.getItem("ConnId"));
                $http.post(serviceBase + 'api/account/RemoveConnection?ConnId=' + connectionId).then(function (response) {
                }).catch(function (err) {

                });
            }
        }        
    };


    var _removeZzsToken = function () {
        var Userid = parseInt(JSON.parse(localStorage.authorizationData).UserId);
        $http.post(serviceBase + 'api/account/removeZssUser?Userid=' + Userid).then(function (response) {
        }).catch(function (err, status) {
        });
    };

    var _ConfigureAjaxDefaultHeaderAuthentication = function () {

        if (localStorage.authorizationData == undefined || localStorage.authorizationData == null || localStorage.authorizationData == "") {
            var queryStringUserId = getUrlParameter("userid");
            var queryStringToken = getUrlParameter("token");
            if (queryStringUserId != null && queryStringUserId != undefined && queryStringUserId != ""
                &&
                queryStringToken != null && queryStringToken != undefined && queryStringUserId != ""
            ) {
                localStorage.setItem('authorizationData', JSON.stringify({
                    token: queryStringToken,
                    userName: null,
                    refreshToken: null,
                    useRefreshTokens: null,
                    generateDate: null,
                    UserId: queryStringUserId
                }));
            }
        }
        var headerAuthorization = _getHeaderAuthorization();
        var queryStringAuthorization = _getQueryStringAuthorization();
        localStorage.setItem("queryStringAuthorization", queryStringAuthorization);
        $http.defaults.headers.common['Authorization'] = headerAuthorization;
        $.ajaxSetup({
            headers: { 'Authorization': headerAuthorization }

        })
    };

    var _checkToken = function () {
        {
            _ConfigureAjaxDefaultHeaderAuthentication();
            var infotoken = JSON.parse(localStorage.authorizationData)
            var userid = infotoken.UserId
            var token = infotoken.token
            var tokenQueryString = getUrlParameter("token").substring(0, 180);
            var headerAuthorization = _getHeaderAuthorization();
            if (token != undefined && tokenQueryString != "") {
                if (tokenQueryString != token.substring(0, 180)) {
                    var splitQueryString = location.search.replace("?","").split("&");
                    var newQueryString = "?";
                    splitQueryString.forEach(function (param) {
                        var key = param.split('=')[0];
                        var value = param.split('=')[1];
                        if (key != "userid" && key != "token")
                            newQueryString += key + "=" + value & "&";
                    });
                    newQueryString = location.origin + location.pathname + newQueryString;
                    location.href = "/Zamba.Web?ReturnUrl=" + newQueryString;
                    
                }

                $.ajax({
                    type: "POST",
                    url: serviceBase + 'api/account/CheckToken?UserId=' + userid + '&Token=' + token,
                    contentType: 'application/json',
                    async: false,
                    success: function (response) {
                        if (response == false) {
                            _logOut();
                        }
                        else {

                            _authentication.isAuth = true;
                        }
                    },error: function (xhr, status, error) {
                        var err = eval("(" + xhr.responseText + ")");
                        console.log(err.Message);
                        _logOut();
                    }
                });
                //$http.post(serviceBase + 'api/account/CheckToken?UserId=' + userid + '&Token=' + token).success(function (response) {
                //    if (response == false) {
                //        _logOut();
                //    }
                //    else {

                //        _authentication.isAuth = true;
                //    }

                //}).error(function (err, status) {
                //    console.log(err);
                //    _logOut();
                //});
            }
        }
    }

    var _CheckIfTokenIsValid = function (token) {
        checkToken();
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
    authServiceFactory.CheckIfTokenIsValid = _CheckIfTokenIsValid;
    authServiceFactory.checkToken = _checkToken;
    authServiceFactory.getHeaderAuthorization = _getHeaderAuthorization;
    authServiceFactory.getQueryStringAuthorization = _getQueryStringAuthorization()
    authServiceFactory.ConfigureAjaxDefaultHeaderAuthentication = _ConfigureAjaxDefaultHeaderAuthentication();
    return authServiceFactory;
}]);

function RestApiToken() {
    return angular.element(document.body).injector().get("authService");
}

