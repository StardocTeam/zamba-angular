'use strict';


app.service('RequestServices', ['$http', '$q', function ($http, $q) {

    var RequestServicesFactory = {};

    var _getResults = function (userId, reportId) {
        var response = null;
        var usernameResponse = null;


        $.ajax({
            type: "GET",
            //url: ZambaWebRestApiURL + '/rp/11535116',
            url: ZambaWebRestApiURL + '/rp/' + reportId + '-' + userId,
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    response = data;
                }
        });
        return response;

    };

    var _getUsername = function (userid) {
        var usernameResponse = null;
        $.ajax({
            type: "GET",
            url: ZambaWebRestApiURL + '/search/GetUserName?userid=' + userid,
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    usernameResponse = data;
                }

        });
        return usernameResponse;
    }; 

    var _getValidateUser = function (UserName) {
        var usernameResponse = null;
        $.ajax({
            type: "GET",
            url: ZambaWebRestApiURL + '/search/GetUserInfoForName?UserName=' + UserName,
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    usernameResponse = data;
                }

        });
        return usernameResponse;
    };

    var _loginResult = function (username, password) {
        var loginResult = null;
        var data = {
            UserName: username,
            Password: password,
            ComputerNameOrIp: "127.1.1.5",
            name: "",
            lastName: "",
            eMail: "",
            type: ""
        }

        $.ajax({
            type: "POST",
            url: ZambaWebRestApiURL + '/ExternalSearch/Login',
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    loginResult = data;
                },
            error: function (data) {
                loginResult = data;
            }

        });
        return loginResult;
    }


    var _LoadUserPhotoFromDB = function (userid) {

        var response = null;
        var genericRequest = {
            UserId: parseInt(userid),

        };

        $.ajax({
            type: "POST",
            url: ZambaWebRestApiURL + '/search/GetThumbsPathHome',
            data: JSON.stringify(genericRequest),

            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    response = data;

                }
        });
        return response;
    }

    var _ChangePass = function (userid, inputs) {

        var CurrentPassword = inputs.CurrentPassword || "",
            NewPassword = inputs.NewPassword || "",
            NewPassword2 = inputs.NewPassword2 || "";


        var genericRequest = {
            Userid: parseInt(userid),
            CurrentPassword: CurrentPassword,
            NewPassword: NewPassword,
            NewPassword2: NewPassword2
        };
        $http.post(ZambaWebRestApiURL + '/Account/Password', genericRequest).then(function successCallback(response) {


            $(".btn.btn-default.modal-cancel-button").click();
            inputs.CurrentPassword = "";
            inputs.NewPassword = "";
            inputs.NewPassword2 = "";
            Swal.fire(response.data)



        }, function errorCallback(response) {
            $(".btn.btn-default.modal-cancel-button").click();
            inputs.CurrentPassword = "";
            inputs.NewPassword = "";
            inputs.NewPassword2 = "";
            Swal.fire("Error al actulizar su contrase�a", "error");

        });
    }

    var _executeTaskRule = function (userId, ruleId, docId, formVariables, callBack) {

        var deferred = $q.defer();

        var response = null;

        if (formVariables != undefined) {
            var genericRequest = {
                userId: parseInt(userId),
                Params: {
                    ruleId: parseInt(ruleId),
                    resultIds: parseInt(docId),
                    FormVariables: formVariables
                }
            };
        } else {
            var genericRequest = {
                userId: parseInt(userId),
                Params: {
                    ruleId: parseInt(ruleId),
                   resultIds: parseInt(docId)
                //    resultIds: resultIds
                }
            };
        }

        $http.post(ZambaWebRestApiURL + "/Tasks/ExecuteTaskRule", JSON.stringify(genericRequest)).then(function (resp) {
            var data = resp.data;
            deferred.resolve(data);
        }, function (err) {
            deferred.reject(err);
            console.log(err);
        })

        return deferred.promise;
    }



    RequestServicesFactory.LoadUserPhotoFromDB = _LoadUserPhotoFromDB
    RequestServicesFactory.getResults = _getResults;
    RequestServicesFactory.getUsername = _getUsername;
    RequestServicesFactory.getValidateUser = _getValidateUser;
    RequestServicesFactory.getLoginResult = _loginResult;
    RequestServicesFactory.executeTaskRule = _executeTaskRule;
    RequestServicesFactory.ChangePass = _ChangePass;


    return RequestServicesFactory;
}]);


