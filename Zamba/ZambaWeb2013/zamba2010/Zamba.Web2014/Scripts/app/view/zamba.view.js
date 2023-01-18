function LoadPage(token, currentuserid, docid, entityid) {
    $.ajax({
        url: "../../Services/viewService.asmx/GetTaskUrl",
        type: "POST",
        dataType: "json",
        cache: true,
        data: "{token:'" + token + "'," + "currentuserid:'" + currentuserid + "',docid:'" + docid + "',entityid:'" + entityid + "'}",
        contentType: "application/json; charset=utf-8",
        success: GetComplete,
        error: GetError
    });

}

function GetComplete(data) {
    kdata = data.d;
    document.location.href = '../../' + kdata;
}

function GetError(e) {
    alert("Error: " + e.responseText + e.status);

}


function loadOriginalFromWeb() {

    var queryVariables = GetQueryStringVariables();
    docid = queryVariables["docid"];
    userid = queryVariables["userid"];
    token = queryVariables["token"];
    entityid = queryVariables["entityid"];

    if (docid == null || userid == null || token == null || entityid == null) {
        alert("Los datos provistos estan incompletos");
    }
    else {

        LoadPage(token, userid, docid, entityid);
    }
}

function loadOriginalFromTemp() {

    var queryVariables = GetQueryStringVariables();
    uri = queryVariables["url"];

    if (uri == null) {
        alert("Los datos provistos estan incompletos");
    }
    else {
        document.location.href = '../officetemp/' + uri;
    }
}

function GetQueryStringVariables() {
    //Toma las variables y sus respectivos valores de la url de la pagina y devuelve el resultado como una lista
    //Los valores deben estar presentadas de la siguiente forma: <url de la pagina>?variable1=valor1&variable2=valor2...&variableN=valorN
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}


var count = 30;
$(document).ready(function () {
    CountDown();
})

function CountDown() {
    setTimeout(function () {
        var div = document.getElementById('LoadingDiv');
        div.innerHTML = "Cargando en " + count;
        count = count - 1;
        if (count <= 0) {
            loadOriginalFromTemp();
        }
        else {
            CountDown();
        }
    }, 1000);
}
