'use strict';
var serviceBase = ZambaWebRestApiURL;

app.factory('insertServices', ['$http', '$q', function ($http, $q) {

   
    var insertServiceFactory = {};
    var _autoComplete = function (entityId, indexs, callback) {
       
        var response = null;

            var genericRequest = {
                UserId: parseInt(GetUID()),
                Params:
                {
                    "entityId": entityId,
                    "indexs": JSON.stringify(indexs),
                  
                }
            };

            $.ajax({
            type: "POST",
                url: serviceBase + '/au/Autocomplete',
            data: JSON.stringify(genericRequest),

            contentType: "application/json; charset=utf-8",
            async: false,
            success:
            function (data, status, headers, config) {
                eval(callback( data));
            }
        });
        return response;
    };

    var _generateBC = function (entityId,indexs) {
        var response = null;

        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
                "entityId": entityId,
                "indexs": JSON.stringify(indexs),
            }
        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/barcode/GenerateBarcode',
            data: JSON.stringify(genericRequest),

            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    response = data;
                }
        });
        return response;
    };

    var _replicarBC = function (entityId, indexs, barcodeId) {
        var response = null;

        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
                "entityId": entityId,
                "indexs": JSON.stringify(indexs),
                "barcodeId": barcodeId,
            }
        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/barcode/replicateBarcode',
            data: JSON.stringify(genericRequest),

            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    response = data;
                }
        });
        return response;
    };

    insertServiceFactory.autoComplete = _autoComplete;
    insertServiceFactory.generateBC = _generateBC;
    insertServiceFactory.replicarBC = _replicarBC;

    return insertServiceFactory;
}]);


