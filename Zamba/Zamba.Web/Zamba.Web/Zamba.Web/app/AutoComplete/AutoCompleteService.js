'use strict';

try {
    if (ZambaWebRestApiURL != undefined) {
        var serviceBase = ZambaWebRestApiURL;
    } else if ($("#urlSite").val() != undefined || $("#urlSite").val() != null) {
        var serviceBase = $("#urlSite").val() + "/api";
    } else {
        console.log("[ERROR]: El atributo 'url' de la peticion ajax no tiene valor.")
    }
} catch (e) {
    if ($("#urlSite").val() != undefined || $("#urlSite").val() != null) {
        var serviceBase = $("#urlSite").val() + "/api";
    } else {
        console.log("[ERROR]: El atributo 'url' de la peticion ajax no tiene valor.")
    }
}

app.factory('AutoCompleteServices', ['$http', '$q', function ($http, $q) {
    var ObjFactory = {};

    //TODO: ENVIAS EL ARRAY DE DOCIDS

    var _GetEmailsUsersOfTask = function (DocIds) {
        var result;
        var ListDocIds = "";

        DocIds.forEach(function (item, i) {
            if (i + 1 != DocIds.length) {
                ListDocIds += item + "/";
            } else {
                ListDocIds += item;
            }

        })

        var valueParams = {
            Params: {
                "DocIds": ListDocIds 
            }
        }

        $.ajax({
            type: "POST",
            url: serviceBase + "/search/GetEmailsUsersOfTask",
            data: JSON.stringify(valueParams),
            contentType: "application/json; charset=utf-8",
            crossDomain: true,
            async: false,
            success: function (data) {
                result = data;
            },
            error: function (ex) {
                console.log(ex.responseJSON.Message);
            }
        });

        return result;
    };

    ObjFactory.GetEmailsUsersOfTask = _GetEmailsUsersOfTask;

    return ObjFactory;
}]);