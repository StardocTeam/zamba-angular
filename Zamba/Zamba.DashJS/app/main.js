'use strict';

$.sound_path = appConfig.sound_path;
$.sound_on = appConfig.sound_on;


$(function () {

    // moment.js default language
    moment.locale('en')

    angular.bootstrap(document, ['app']);
 
    
});

function viewBar() {
   
    var a = '';
    var genericRequest = {
        UserId: getElementFromQueryString("User"),
        Params: {}
    };

    $.ajax({
        type: "POST",
        url: "https://bpm.provinciaseguros.com.ar/ZambaDemo.RestApi/api/search/GetResultsByAllReport",
        data: JSON.stringify(genericRequest),
        crossDomain : true,
        contentType: "application/json; charset=utf-8",
        async: false,
        success:
            function (data, status, headers, config) {
                a = data;
                var ElementoJson = JSON.parse(a);
                // console.log(ElementoJson.length);

                for (var i = 0; i < ElementoJson.Table.length; i++) {
                    var name = ElementoJson.Table[i].NAME,
                        id = ElementoJson.Table[i].ID,
                        varx = ElementoJson.Table[i].VARX,
                        vary = ElementoJson.Table[i].VARY;
                    // $('#AreaResult').append('<li class="ModalClassUlReport" id="' + id + '" name="' + name.toString() + '" onclick="NewViewReport(this);">' + name.toString() + '</li>');
                    $('#AreaResult').append('<li data-ui-sref-active="active"><a data-ui-sref="app.graphs.chartjs" class="CursorLink"  onclick="NewViewReport(this);"  id="' + id + '" name="' + name.toString() + '" varx="' + varx + '" vary="' + vary + '">' + name.toString() + '</a></li>');

                }
            },
            error: function (error){
                console.log(error);

            }
    });

}

function getElementFromQueryString(element) {
    var url = window.location.href;

    var segments = url.split("&");
    var value = null;
    segments.forEach(function (valor) {
        if (valor.includes(element)) { value = valor.split("=")[1]; }
    });
    return value;
}

function NewViewReport(sender) {
    var ReportId = sender.id;
    var VarX = sender.attributes[5].nodeValue;
    var VarY = sender.attributes[6].nodeValue;
    var User = getElementFromQueryString("User");
    
    var genericRequest = {
        Params:
        {
            "ReportID": ReportId,
            "VarX": VarX,
            "VarY": VarY
        }
    };
    $.ajax({
        type: "POST",
        url: "http://localhost/ZambaWeb.RestApi/api/search/GetResultsByReportIdDash",
        data: JSON.stringify(genericRequest),

        contentType: "application/json; charset=utf-8",
        async: false,
        success:
        function (data, status, headers, config) {
                localStorage.setItem("JsonVars", data);

        }
    });

   // window.open('../../Views/Reports/NewReports.html?id=' + ReportId + '&User=' + User + '', "_blank");
   window.location.href ='#/graphs/chartjs?id=' + ReportId + '&User=' + User + '';
    //windows.open(url, "_blank");
}