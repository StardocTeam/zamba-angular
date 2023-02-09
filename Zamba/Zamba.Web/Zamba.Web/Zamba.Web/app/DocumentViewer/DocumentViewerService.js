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

app.factory('DocumentViewerServices', ['$http', '$q', function ($http, $q) {
    var ObjFactory = {};

    var _getDocFileService = function (valueParams, tokenSearchId) {
        var result;

        $.ajax({
            type: "POST",
            url: serviceBase + "/search/GetDocFile",
            data: JSON.stringify(valueParams),
            contentType: "application/json; charset=utf-8",
            crossDomain: true,
            async: false,
            //headers: { "Authorization": tokenSearchId },
            success: function (data) {
                result = data;
            },
            error: function (ex) {
                console.log(ex.responseJSON.Message);
            }
        });

        return result;
    };

    var _getDocumentService = function (UserId, DocTypeId, DocId, tokenSearchId, convertToPDF, callBack, viewer) {
        var result;
        var value = {
            Params: {
                "userId": UserId,
                "doctypeId": DocTypeId,
                "docid": DocId,
                "converttopdf": convertToPDF,
                "includeAttachs": false,
                "viewer": viewer
            }   
        }       
        $.ajax({
            type: "POST",
            url: serviceBase + "/search/GetDocument",
            data: JSON.stringify(value),
            async: false,
            contentType: "application/json; charset=utf-8",
            crossDomain: true,
            //headers: { "Authorization": tokenSearchId },
            success: function (data) {
                eval(callBack(data));
            },
            error: function (ex) {
                //console.log(ex.responseJSON.Message);
                eval(callBack(null));
            },
            timeout: 60000
        });

        return result;
    };

    var _getDocumentServiceAsync = function (UserId, DocTypeId, DocId, tokenSearchId, convertToPDF, viewer, iframeID) {

        var deferred = $q.defer();

        var response = null;

        var result;
        var value = {
            Params: {
                "userId": UserId,
                "doctypeId": DocTypeId,
                "docid": DocId,
                "converttopdf": convertToPDF,
                "includeAttachs": false,
                "viewer": viewer,
                "iframeID": iframeID
            }
        }

        const ipAPI = '//api.ipify.org?format=json'


        $http.post(ZambaWebRestApiURL + '/search/GetDocument', JSON.stringify(value), { headers: { 'Authorization': tokenSearchId }}).then(function (resp) {
            var data = resp.data;
            deferred.resolve(data);
        }, function (err) {
            deferred.reject(err);
            console.log(err);
        });

        return deferred.promise;


       
        //$.ajax({
        //    type: "POST",
        //    url: serviceBase + "/search/GetDocument",
        //    data: JSON.stringify(value),
        //    async: false,
        //    contentType: "application/json; charset=utf-8",
        //    crossDomain: true,
        //    headers: { "Authorization": tokenSearchId },
        //    success: function (data) {
        //        eval(callBack(data));
        //    },
        //    error: function (ex) {
        //        //console.log(ex.responseJSON.Message);
        //        eval(callBack(null));
        //    },
        //    timeout: 60000
        //});

        //return result;
    };

    ObjFactory.getDocFileService = _getDocFileService;
    ObjFactory.getDocumentService = _getDocumentService;
    ObjFactory.getDocumentServiceAsync = _getDocumentServiceAsync;

    return ObjFactory;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("DocumentViewerServices");
}