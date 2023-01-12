
var serviceBase = ZambaWebRestApiURL;

function Report(ReportID, FormVariables) {
    var response = null;
    var genericRequest = {
        // UserId: parseInt(GetUID()),
        UserId: parseInt(GetUID()),
        Params:
        {
            "ReportID": ReportID,
            "FormVariables": FormVariables
        }
    };

    $.ajax({
        type: "POST",
        url: serviceBase + '/search/GetResultsByReportId',
		crossDomain: true,
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