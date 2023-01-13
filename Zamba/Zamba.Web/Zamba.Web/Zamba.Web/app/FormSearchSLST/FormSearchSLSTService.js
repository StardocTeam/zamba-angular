'use strict';
var serviceBase = ZambaWebRestApiURL;

app.service('FormSearchSLSTService', function ($http) {
    
    var FormSearchSLSTService = {};
    var _LoadResult = function (doctypeId, docid, indexId) {
        var response = null;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
                "doctypeId": doctypeId,
                "docid": docid,
                "indexId": indexId
            }
        };



        $.ajax({
            type: "POST",
            url: serviceBase + '/search/GetValueFromIndex',
            data: JSON.stringify(genericRequest),



            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data) {
                    response = data;
                }
            ,
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Status: " + textStatus); alert("Error: " + errorThrown);
            }



        });
        return response;
    };
    var _GetListOptions = 
        function (indexId, filter, limitToSlst) {
            var genericRequest = {
                IndexId: indexId,
                Value: filter,
                LimitTo: limitToSlst
            };
            var result = false;
            $.ajax({
                type: "post",
                url: ZambaWebRestApiURL + '/search/ListOptions',
                data: JSON.stringify(genericRequest),
                contentType: "application/json; charset=utf-8",
                async: false,
                success:
                    function (data, status, headers, config) {
                        result = JSON.parse(data);
                        
                        return result
                    }
            });
            return result;
    }
    FormSearchSLSTService.GetListOptions = _GetListOptions;
    FormSearchSLSTService.LoadResult = _LoadResult;

    return FormSearchSLSTService;
});