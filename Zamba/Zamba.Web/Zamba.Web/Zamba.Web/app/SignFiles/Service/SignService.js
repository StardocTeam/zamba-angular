'use strict';
var serviceBase = ZambaWebRestApiURL;
//var serviceBase = 'https://gd.modoc.com.ar/ZambaAFIP.RestApi/api';

app.factory('ZambaSignFileService', ['$http', '$q', function ($http, $q) {
    var ZambaSignFileFactory = {};

    function _doReception(solicitudFirmaDigital) {
        var result;
        $.ajax({
            type: "POST",
            url: serviceBase + "/SignPDF/RecepcionDespacho",
            data: JSON.stringify(solicitudFirmaDigital),
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    console.log("Recepcion Realizada.");
                    result = data;
                },

            error: function (data) {
                console.log(data);
                result = data;
            }
        });
        return result;
    }


    function _signFile(solicitudFirmaDigital) {
        var result;
        $.ajax({
            type: "POST",
            url: serviceBase + "/SignPDF/SignPDF",
            data: JSON.stringify(solicitudFirmaDigital),
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    console.log("Archivos firmados.");
                    result = data;
                },

            error: function (data) {

                console.log(data);
                result = data;
            }
        });
        return result;
    }

    function _doGetLegajo(solicitudFirmaDigital) {
        var result;
        $.ajax({
            type: "POST",
            url: serviceBase + "/SignPDF/ConsultaDespacho",
            data: JSON.stringify(solicitudFirmaDigital),
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    console.log("Recepcion Realizada.");
                    result = data;
                },

            error: function (data) {
                console.log(data);
                result = data;
            }
        });
        return result;
    }

    function _doGetLegajos(solicitudFirmaDigital) {
        var result;
        $.ajax({
            type: "POST",
            url: serviceBase + "/SignPDF/GetLegajos",
            data: JSON.stringify(solicitudFirmaDigital),
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    console.log("Recepcion Realizada.");
                    result = data;
                },

            error: function (data) {
                console.log(data);
                result = data;
            }
        });
        return result;
    }

    function _doGetLegajosAll(solicitudFirmaDigital) {
        var result;
        $.ajax({
            type: "POST",
            url: serviceBase + "/SignPDF/GetLegajosAll",
            data: JSON.stringify(solicitudFirmaDigital),
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    console.log("Recepcion Realizada.");
                    result = data;
                },

            error: function (data) {
                console.log(data);
                result = data;
            }
        });
        return result;
    }

    ZambaSignFileFactory.doGetLegajos = _doGetLegajos;
    ZambaSignFileFactory.doGetLegajosAll = _doGetLegajosAll;

    ZambaSignFileFactory.doReception = _doReception;
    ZambaSignFileFactory.signFile = _signFile;
    ZambaSignFileFactory.GetLegajo = _doGetLegajo;

    return ZambaSignFileFactory;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("ZambaSignFileService");
}