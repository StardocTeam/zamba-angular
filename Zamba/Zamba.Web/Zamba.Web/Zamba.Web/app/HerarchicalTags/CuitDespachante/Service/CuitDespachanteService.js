'use strict';
var serviceBase = ZambaWebRestApiURL;


app.factory('ZambaHerarchicalTagsService', ['$http', '$q', function ($http, $q) {
    var zambaHerarchicalTagsFactory = {};


    function _getHerarchicalValues(userid, parentValue) {
        var items = null;
        $.ajax({
            type: "POST",
            url: serviceBase + '/tasks/getValuesToHerarchicalTag?' +
            jQuery.param({ UserId: userid, parentTagValue: parentValue }),
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
            function (data, status, headers, config) {
                items = data;
            }
        });
        return items;
    }

    zambaHerarchicalTagsFactory.getHerarchicalValues = _getHerarchicalValues;

    return zambaHerarchicalTagsFactory;
}]);