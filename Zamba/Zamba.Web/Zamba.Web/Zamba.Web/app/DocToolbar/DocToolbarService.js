'use strict';
var serviceBase = ZambaWebRestApiURL;

app.service('DocToolbarService', function ($http) {

    var DocToolbarService = {};

    var _getUserRights = function (RightType, ObjectId) {

        var result;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
                "ObjectId": ObjectId,
                "RightType": RightType
            }
        };
        $.ajax({
            type: "POST",
            url: ZambaWebRestApiURL + '/Account/NewGetUserRight',
            contentType: 'application/json',
            async: false,
            data: JSON.stringify(genericRequest),
            success: function (response) {
                result = JSON.parse(response);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                result = XMLHttpRequest;
            }
        });
        return result;
    };

    DocToolbarService.GetUserRights = _getUserRights;
    return DocToolbarService;

});
