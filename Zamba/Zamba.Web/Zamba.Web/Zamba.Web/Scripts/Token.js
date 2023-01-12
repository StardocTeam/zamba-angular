var Token = {
    //GetBearerToken: function () {
    //    var ac = this.AjaxConfig();
    //    if (ac.data != null) {
    //        if (ac.data !== "" && ("userId" in ac.data && ac.data.userId === "")) {
    //            //toastr.error("Problema con autenticacion de usuario");
    //            return;
    //        }
    //    }
    //    return $.ajax({
    //        type: ac.type,
    //        url: ac.url,
    //        data: ac.data,
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json"
    //    });
    //},
    ////La aplicacion no usa login de ZambaWeb
    //IsExternalApp: function () {
    //    return (typeof (SearchConfig) !== "undefined" && SearchConfig.IsSearchConfig());
    //},
    //AjaxConfig: function () {
    //    if (this.IsExternalApp()) {
    //       // var l = { UserName: "Administrador", Password: "", ComputerNameOrIp: "::1bxnclqi5rszvhydrfampihfa" };
    //        return {
    //            url: ZambaWebRestApiURL + "/Account/LoginById?userId=" + SearchConfig.UserId(),
    //            //data: {"userId": SearchConfig.UserId()},
    //            type: "GET",
    //        }
    //    }
    //    else {
    //        return {
    //            url: thisDomain + "/views/main/default.aspx/GetBearerToken",
    //            data: "",
    //            type: "POST",
    //        }
    //    }
    //},

    GetNewToken: function () {
        var UserId = parseInt(GetUID());
        if (UserId > 0) {
            $http.get(ZambaWebRestApiURL + "/Account/LoginById?userId=" + UserId()).then(function (response) {
                return response.data;
            }).catch(function (err, status) {
                return;
            });
        }
        else {
            return;
        }
    }
};