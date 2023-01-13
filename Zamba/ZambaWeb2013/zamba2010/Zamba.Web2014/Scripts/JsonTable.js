var taskid;
var variablename;
var urlForAjax;

$(document).ready(function () {
    //-- PARA INTERNET EXPLORER <= 8 --//	
    if (!Object.keys) {
        Object.keys = function (obj) {
            var keys = [];
            for (var i in obj) {
                if (obj.hasOwnProperty(i)) {
                    keys.push(i);
                }
            }
            return keys;
        };
    }
    jQuery.support.cors = true;
    //---------------------------------//
    //Cambia el cursor a un pointer cuando se pasa por encima de la tabla, para hacer enfasis de que se puede hacer click en esta
   
    $("#jsonTable").css("cursor", "pointer");
    //Si el atributo src no existiese, quiere decir que el formulario es cargado por fuera del cliente de Zamba.
    
    if ($("#jsonTable") != undefined && $("#jsonTable").attr("src") == undefined) {
        loadFromWeb();
    }
});

function ProcessAllJsonTables() {
    // Esta funcion toma el source de la tabla con id jsonTable y el taskid para luego buscar los datos necesarios para dibujar la tabla	
    if (document.getElementById("jsonTable").getAttribute("src") != undefined) {
        variablename = document.getElementById("jsonTable").getAttribute("src");
        if (variablename != undefined) {
            variablename = variablename.substring((variablename.indexOf("(")) + 1, (variablename.lastIndexOf(")")));
        }
        taskid = document.getElementById("taskId").getAttribute("value");
        if ((taskid != undefined) && (variablename != undefined)) {
            ajaxGetJson(taskid, variablename);
        }
    }
}

function loadFromWeb() {
    // Esta funcion es llamada si el formulario es cargado por fuera de zamba
    if ($("#jsonTable") != undefined) {
        urlForAjax = $("#jsonTable").attr("zambaws");
        if (urlForAjax != undefined) {
            var queryVariables = GetQueryStringVariables();
            taskid = queryVariables["TaskId"];
            variablename = queryVariables["VarName"];
            if (taskid == null) {
                alert("ERROR AL RECUPERAR TASKID");
            }
            else {
                ajaxGetJson(taskid, variablename);
            }
        } else {
          //  alert("No se puede encontrar una direccion valida al WebService")
        }
    }
}
//Obtiene de zamba.js el path del webservice
function setWSPath(path) {
    urlForAjax = path;
}

function ajaxGetJson(taskid, variablename) {
    //Este ajax trae el JSON de la base de datos con los datos necesarios para cargar la tabla
    $.ajax({
        type: "POST",
        cache: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: "{ TaskId: " + taskid + ", ZvarName: '" + variablename + "'}",
        url: urlForAjax + "/GetZVarValue",
        success: function (data) {
            loadTable(JSON.parse(data.d));
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert("Error al obtener los datos: " + thrownError + "\nCodigo de error: " + xhr.status);
        }
    });
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

function loadTable(data) {
    var columnsToHide = $("#jsonTable").attr("hidecolumns");
    var columnWithLink = $("#jsonTable").attr("columnlink");

    if (columnWithLink != undefined) {
        columnWithLink = validateColumns(columnWithLink);
    }
    if (columnsToHide != undefined) {
        columnsToHide = validateColumns(columnsToHide);
    }
    createTableHeader(data);
    var dataForTable = validateJson(data);
    // CREA LA TABLA
    $('#jsonTable').DataTable({
        data: dataForTable,
        columns: createDataForColumns(dataForTable)
    });

    if (columnsToHide != undefined) {
        HideColummns(columnsToHide);
    }
    // SI EL JSON SOLO DEVUELVE 1 RESULTADO, LO ABRE AUTOMATICAMENTE (SI HAY ALGUN LINK)
    if ((data.length < 2) && (columnWithLink != undefined)) {
        var link = $("td").parent().children('td:nth(' + parseInt(columnWithLink) + ')')[0].innerText;
        InspectionClick(link);
    }
    // REDIRIGE AL LINK QUE FIGURA EN LA COLUMNWITHLINK AL HACER CLIC EN LA FILA (SI ES QUE SE ENCONTRO UN LINK)
    $("td").click(function () {
        if (columnWithLink != undefined) {
            var link = $(this).parent().children('td:nth(' + parseInt(columnWithLink) + ')')[0].innerText;
            InspectionClick(link)
        }
    });
}
var popup;
function InspectionClick(link) {
    // ESTA URL ES EXCLUSIVA PARA HDI
   // var loginurl = 'http://srvdesa/names.nsf?login&username=u01&password=password';
    var loginurl = "";
    if (loginurl != "") {
        var popup = window.open(loginurl);
        setTimeout(function () { OpenLink(link, popup); }, 2000);
    } else {
      var popup =  window.open(link);
    }
}

function OpenLink(link, popup) {
    //Se abre en un pop up ya que sino pierde las credenciales de autenticacion
    popup.location = link;
    popup.focus();
}

function validateColumns(columns) {
    return columns.replace("]", "").replace("[", "").replace(" ", "").split(",");
}

function createTableHeader(data) {
    // Crea el header de la tabla usando las keys originales del JSON obtenido
    $('#jsonTable').append("<thead><tr></tr></thead>");
    var jsonstr = [];
    $.each(data[0], function (key) {
        $('#jsonTable > thead > tr').append("<th>" + key + "</th>");
    });
}

function validateJson(data) {
    // Funcion que toma un JSON y cambia las keys a "colX" donde X es el numero de posicion, esto se hace para evitar errores al mostrar la tabla 
    // si alguna de las keys posee caracteres especiales
    var datosTablaFinal = [];
    var jsonKeys;
    var registro;
    var columns;
    for (value in data) {
        registro = data[value];
        jsonKeys = Object.keys(registro);
        columns = {};
        for (var i = 0; i < jsonKeys.length; i++) {
            columns['col' + i.toString()] = registro[jsonKeys[i]];
        }
        datosTablaFinal.push(columns);
    }
    return datosTablaFinal;
}

function createDataForColumns(data) {
    // crea un objeto JSON de forma [{"data":<nombre de key>},{"data":<nombre de key2>}] necesario para la el plugin que arma la tabla
    var jsonstr = [];
    var column = 0;
    $.each(data[0], function (key) {
        jsonstr.push('{\"data\": \"' + key + '\"}');
        column++;
    });
    jsonstr = jsonstr.join();
    jsonstr = '[' + jsonstr + ']';
    return JSON.parse(jsonstr);
}

function HideColummns(columnsToHide) {
    //Esconde las columnas especificadas en el atributo de la tabla
    for (var i = 0; i <= columnsToHide.length; i++) {
        $('#jsonTable > tbody  > tr').each(function () {
            $(this).children('td:nth(' + parseInt(columnsToHide[i]) + ')').hide();
        });
        $('#jsonTable > thead >tr').children('th:nth(' + parseInt(columnsToHide[i]) + ')').hide();
    }
}