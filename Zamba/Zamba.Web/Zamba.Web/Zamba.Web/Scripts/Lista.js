var FilerList;
var lista = [];
var FiltaFinal = [];
var availableTags = [];
var AutoComplet = [];
var FilterMediacionJuicio = [];

$(document).ready(function () {
    CargarListas(); //Los datos se cargan en cascada porque son procesos asincronicos

});

function CargarListas() {

    var ReportID = "1525059";
    //if (sessionStorage.getItem("ListaJurisdicciones") != null && sessionStorage.getItem("ListaJurisdicciones") != "") {
    //    availableTags = $.parseJSON(sessionStorage.getItem("ListaJurisdicciones"));
    //    GetListaFilterMediacionJuicio();
    //}
    //else {
    var genericRequest = {
        UserId: parseInt(GetUID()),
        Params:
        {
            "ReportID": ReportID
        }
    };

    $.ajax({
        type: "POST",
        url: serviceBase + '/search/GetResultsByReportId',
        data: JSON.stringify(genericRequest),

        contentType: "application/json; charset=utf-8",
        async: false,
        success:
            function (data) {
                availableTags = []
                JsonListaJurisdicciones = $.parseJSON(data);
                for (i = 0; i < JsonListaJurisdicciones.length; i++)
                    availableTags.push(JsonListaJurisdicciones[i]["DISTRICTO"]);
                //sessionStorage.setItem("ListaJurisdicciones", JSON.stringify(availableTags));
                GetListaFilterMediacionJuicio();
            }
        ,
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Status: " + textStatus); alert("Error: " + errorThrown);
        }

    });
    //}
}

// Ready 2/4
function GetListaFilterMediacionJuicio() {

    var ReportID = 1525060;
    //if (sessionStorage.getItem("ListaFilterMediacionJuicio") != null && sessionStorage.getItem("ListaFilterMediacionJuicio") != "") {

    //    FilterMediacionJuicio = $.parseJSON(sessionStorage.getItem("ListaFilterMediacionJuicio"));
    //    FinalizarReady();
    //}
    //else {
    var genericRequest = {
        UserId: parseInt(GetUID()),
        Params:
        {
            "ReportID": ReportID
        }
    };


    //ML: Falta pasarle el VARS de J o M y crear otro reporte para cada uno y filtrar por la fecha total.
    $.ajax({
        type: "POST",
        url: serviceBase + '/search/GetResultsByReportId',
        data: JSON.stringify(genericRequest),

        contentType: "application/json; charset=utf-8",
        async: false,
        success:
            function (data) {
                FilterMediacionJuicio = $.parseJSON(data);
                // sessionStorage.setItem("ListaFilterMediacionJuicio", JSON.stringify(data));
                FinalizarReady();
            }
    });
    //}
}
function FinalizarReady() {
    $("#zamba_index_2685").autocomplete({
        source: availableTags
    }).focusout(function () {
        console.log("primero");
        var JurisdiccionValue;
        if ($("#ui-id-1")[0].children.length > 0) {
            JurisdiccionValue = $("#ui-id-1")[0].children[0].children[0].innerHTML;
            if ($("#zamba_index_2685").val().indexOf("-") == "-1") {
                $("#zamba_index_2685").val(JurisdiccionValue);
            }
        }
        FiltaFinal = [];
        AutoComplet = [];
        LoadEstudiosList();
    });
}


function LoadEstudiosList() {

    FilerList = FilterMediacionJuicio;
    if ($("#zamba_index_2681").val() != undefined && $("#zamba_index_2681").val() != null && $("#zamba_index_2681").val() != '' && $("#zamba_index_2685").val() != undefined && $("#zamba_index_2685").val() != null && $("#zamba_index_2685").val() != '') {
        var filterIdJurisdiccion = $("#zamba_index_2685").val().split("-")[0].trim();
        //var ListaString = JSON.stringify(FilerList);
        lista = [];
        var found = false;
        var estudioActual = $("#zamba_index_2732").val();
        // a = JSON.parse(FilerList);
        a = FilerList;
        for (var i = 0; i < a.length; i++) {
            var valueFilter = a[i]["CD_JURISDICCION"];
            valueFilter = valueFilter.toString();

            var JOM = a[i]["JOM"];
            //JOM = JOM.toString();
            

            if (valueFilter == filterIdJurisdiccion && JOM == $("#zamba_index_2681").val()) {
                lista.push(a[i]);
                try {
                    if ((a[i]["CD_ESTUDIO"] + " - " + a[i]["DESC_ESTUDIO"] == estudioActual) || (a[i]["CD_ESTUDIO"] + " - " + a[i]["DESC_ESTUDIO"] + " - " + a[i]["DESC_JURISDICCION"] == estudioActual)) {
                        found = true;
                    }
                } catch (e) {
                }
            }
            FiltaFinal = [];
        }

        for (var i = 0; i < lista.length; i++) {

            FiltaFinal.push(lista[i]["CD_ESTUDIO"] + " - " + lista[i]["DESC_ESTUDIO"] + " - " + lista[i]["DESC_JURISDICCION"]);
            AutoComplet.push(lista[i]);
        }


        if (found == false) {
           // $("#zamba_index_2732").val("");
         //   $("#zamba_index_10170").val("");
           // $("#zamba_index_10250").val("");
          //  $("#zamba_index_1001011").val("");
        }
        //FiltaFinal = JSON.stringify(FiltaFinal);

        $("#zamba_index_2732").autocomplete({
            source: FiltaFinal
        }).focusout(function () {

            var ui;
            for (var i = 0; i < document.getElementsByTagName("ul").length; i++) {
                if (document.getElementsByTagName("ul")[i].id.indexOf("ui-id") != -1) {
                    if (document.getElementsByTagName("ul")[i].id != "ui-id-1") {
                        ui = document.getElementsByTagName("ul")[i].id;
                    }
                }
            }

            if ($("#zamba_index_2732").val().indexOf("-") == "-1") {
                $("#zamba_index_2732").val($("#" + ui)[0].children[0].children[0].innerHTML);
            }

            var CdEstudio = $("#zamba_index_2732").val().substring(0, 3).trim();

            for (var i = 0; i < AutoComplet.length; i++) {
                if (CdEstudio == AutoComplet[i]["CD_ESTUDIO"]) {

                    if ($("#zamba_index_2681").val() != 'M' && $("#zamba_index_2681").val() != 'J') {
                        //if ($("#zamba_index_10170")[0].type == 'select-one') {
                        //    try {
                        //        if (AutoComplet[i]["NEGOCIADOR"] != '') {
                        //            $($("#zamba_index_10170")[0].options).each(function (option) {
                        //                if ($($($("#zamba_index_10170")[0].options)[option]).val() == AutoComplet[i]["NEGOCIADOR"]) {
                        //                    $("#zamba_index_10170")[0].selectedIndex = option;
                        //                }
                        //            });
                        //        }
                        //    } catch (e) {
                        //    }
                        //} else {

                        //    $("#zamba_index_10170").val(AutoComplet[i]["NEGOCIADOR"]);

                        //    ObteneryAsignarAtributoDescripcion(10170, $("#zamba_index_10170").val());
                        //}
                    }


                    if ($("#zamba_index_10250")[0].type == 'select-one') {
                        try {
                            if (AutoComplet[i]["IDGRUPO"] != '') {
                                $($("#zamba_index_10250")[0].options).each(function (option) {
                                    if ($($($("#zamba_index_10250")[0].options)[option]).val() == AutoComplet[i]["IDGRUPO"] + " - " + AutoComplet[i]["GRUPO"]) {
                                        $("#zamba_index_10250")[0].selectedIndex = option;
                                    }
                                });
                            }
                        } catch (e) {
                        }
                    } else {
                        $("#zamba_index_10250").val(AutoComplet[i]["IDGRUPO"] + " - " + AutoComplet[i]["GRUPO"]);
                    }

                    if ($("#zamba_index_1001011")[0].type == 'select-one') {
                        try {
                            if (AutoComplet[i]["REFERENTE"] != '') {
                                $($("#zamba_index_1001011")[0].options).each(function (option) {
                                    if ($($($("#zamba_index_1001011")[0].options)[option]).val() == AutoComplet[i]["REFERENTE"]) {
                                        $("#zamba_index_1001011")[0].selectedIndex = option;
                                    }
                                });
                            }
                        } catch (e) {
                        }
                    } else {
                        $("#zamba_index_1001011").val(AutoComplet[i]["REFERENTE"]);
                        ObteneryAsignarAtributoDescripcion(1001011, $("#zamba_index_1001011").val());
                    }


                }
            }
        }).on("focus", function () {
            $(this).autocomplete("search", " ");
        });
    }

}


function ObteneryAsignarAtributoDescripcionByelement(element) {
    try {

        var AttributeValue = $(element).val();
        var AttributeId = $(element).attr("id");
        var AttributeId = String(AttributeId).replace('zamba_index_', '');
        ObteneryAsignarAtributoDescripcion(AttributeId, AttributeValue);
    } catch (e) {
        console.error(e);
    }

};
function ObteneryAsignarAtributoDescripcion(AttributeId, AttributeValue) {
    try {

        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "AttributeId": AttributeId,
                "AttributeValue": AttributeValue
            }
        };


        $.ajax({
            "async": false,
            "url": serviceBase + "/tasks/getAttributeDescription",
            "method": "POST",
            "headers": {
                "content-type": "application/json"
            },
            "data": JSON.stringify(genericRequest),
            success:
                function (data, status, headers, config) {
                    if (data != undefined && data != '') {
                        $("#zamba_index_" + AttributeId).val(AttributeValue + ' - ' + data);
                    }
                }
        });


    } catch (e) {
        console.error(e);
        return;
    }

}





$(document).ready(function () {
    LoadEstudiosList();
    if ($("#zamba_index_2681")[0].value == "M" )
        $("#zamba_index_10170")[0].value = "";
});

