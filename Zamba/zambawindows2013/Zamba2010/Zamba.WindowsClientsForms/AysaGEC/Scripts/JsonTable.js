var taskid;
var variablename;

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
   
    //Si el atributo src no existiese, quiere decir que el formulario es cargado por fuera del cliente de Zamba.
    if ($("#jsonTable") != undefined) {
		$("#jsonTable").css("cursor", "pointer");      
	    if ($("#jsonTable").attr("src") == undefined) {
            loadFromWeb();
        }
    }
});

function ProcessAllJsonTables() {
    // Esta funcion toma el source de la tabla con id jsonTable y el taskid para luego buscar los datos necesarios para dibujar la tabla
    if ($("#jsonTable").attr("src") != undefined) {
        variablename = $("#jsonTable").attr("src");
        if (variablename != undefined) {
            variablename = variablename.substring((variablename.indexOf("(")) + 1, (variablename.lastIndexOf(")")));
        }
        taskid = $("#taskId").attr("value");
        if ((taskid != undefined) && (variablename != undefined)) {
            ajaxGetJson(taskid, variablename);
        }
    }
}

function loadFromWeb() {
    // Esta funcion es llamada si el formulario es cargado por fuera de zamba
    var queryVariables = GetQueryStringVariables();
    taskid = queryVariables["TaskId"];
    variablename = queryVariables["VarName"];
    if (taskid == null) {
        //alert("ERROR AL RECUPERAR TASKID");
    }
    else {
        ajaxGetJson(taskid, variablename);
    }
}

function ajaxGetJson(taskid, variablename) {
    //Este ajax trae el JSON de la base de datos con los datos necesarios para cargar la tabla
    // getWSPath() se encarga de obtener el path del webservice desde zamba.js
    $.ajax({
        type: "POST",
        cache: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: "{ TaskId: " + taskid + ", ZvarName: '" + variablename + "'}",
        url: getWSPath() + "/GetZVarValue", // CARGAR AQUI LA DIRECCION DEL WEBSERIVCE
        success: function (data) {
            loadTable(JSON.parse(data.d));
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //alert("Error al obtener los datos: "+thrownError+"\nCodigo de error: "+xhr.status);
        }
    });
}

function ajaxGetAsociatedJson(controlId, tbody) {
    //Este ajax trae el JSON de la base de datos con los datos necesarios para cargar la tabla
    // getWSPath() se encarga de obtener el path del webservice desde zamba.js

    taskid = document.getElementById("taskId").getAttribute("value");
    // alert('taskid ' + taskid);
    userId = document.getElementById("userid").getAttribute("value");
   //  alert('userId ' + userId);
    // alert('controlId ' + controlId);
    // alert('tbody ' + tbody);
    // alert('URL ' + getWSPath() + "/GetZAsociatedResults");

    $("#" + controlId).before('<img id="loadingImg' + controlId + '" src="images/loading.gif" />'); 
    $.ajax({
        type: "POST",
        cache: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: "application/json; charset=utf-8",
        data: "{ ControlId: '" + controlId + "', TaskId: " + taskid + ", UserId: " +
              userId + ",  TBodyId: '" + $(tbody).attr("id") + "'}",
        url: getWSPath() + "/GetZAsociatedResults", // CARGAR AQUI LA DIRECCION DEL WEBSERIVCE
        success: function (data) {
                //   alert(data.d);
            //    loadAsociatedTable(controlId, JSON.parse(data.d));
         
            if (data != null && data.d != "[]") loadAsociatedTable(controlId, data.d);
           $("#loadingImg" + controlId).remove();
        },
        error: function (xhr, ajaxOptions, thrownError) {
           $("#loadingImg" + controlId).remove();
           alert("Error al obtener los datos: " +thrownError + "\nCodigo de error: " +xhr.status);          
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
    if (data != undefined) {
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
        var dataForColumns = createDataForColumns(dataForTable);
        // CREA LA TABLA
        $('#jsonTable').DataTable({
            data: dataForTable,
            columns: dataForColumns
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
}

function loadAsociatedTable(controlId, data) {
    if (data != undefined) {
        var columnsToHide = $("#" + controlId).attr("hidecolumns");
        var columnWithLink = $("#" + controlId).attr("columnlink");

        if (columnWithLink != undefined) {
            columnWithLink = validateColumns(columnWithLink);
        }
        if (columnsToHide != undefined) {
            columnsToHide = validateColumns(columnsToHide);
        }
        //createTableHeader(data);      
        var dataForTable = validateJson(data);
        var dataForColumns = createDataForColumns(dataForTable);
        //   CREA LA TABLA  (Primer columna vacia para colocar IMG hipervinculo)       
        var header = ("<th></th>");
        var cont = 0;
        var values = [];       
        for (value in $.parseJSON(data)[0]) {            
            if (columnsToHide == undefined || columnsToHide.indexOf(cont.toString()) == -1) {
                values.push(value)               
                header += ("<th>" + dataForColumns[cont].data + "</th>");
            }         
            cont += 1;
            }
        }
        //Armo y lleno tabla
        $("#" + controlId).append("<thead><tr>" + header + "</tr></thead>");
        $("#" + controlId).dynatable({
            table: {
                headRowSelector: 'thead tr'
            },
            dataset: {
                records: dataForTable
            }
        });
        //Coloco títulos
        cont = -1;
        $("#" + controlId + " >thead >tr >th").each(function () {
            if (cont >= 0) this.innerHTML = values[cont];                
         cont += 1;
        });
        //Cambia cursor a mano en cada fila de datos
        $("#" + controlId + " >tbody >tr >td").each(function () {
            $(this).css('cursor', 'pointer');
        });
       
        if (columnsToHide != undefined) {
            HideColummns(columnsToHide);
        }
        // SI EL JSON SOLO DEVUELVE 1 RESULTADO, LO ABRE AUTOMATICAMENTE (SI HAY ALGUN LINK)
        if ((data.length < 2) && (columnWithLink != undefined)) {
            var link = $("td").parent().children('td:nth(' + parseInt(columnWithLink) + ')')[0].innerText;
            InspectionClick(link);
        }

        CrearLinksAsociados(controlId, cont);
        // REDIRIGE AL LINK QUE FIGURA EN LA COLUMNWITHLINK AL HACER CLIC EN LA FILA (SI ES QUE SE ENCONTRO UN LINK)
        $("td").click(function () {
            if (columnWithLink != undefined) {
                var link = $(this).parent().children('td:nth(' + parseInt(columnWithLink) + ')')[0].innerText;
                InspectionClick(link)
            }
        });
    }

function CrearLinksAsociados(controlId, registers) {
    try {      
        $("#" + controlId + ' >tbody >tr').each(function () {
            //Ultimo y anteultimo en JSON
                var DocTypeId = $(this).children('td:nth(' + (registers - 1) + ')').text();
                var DocId = $(this).children('td:nth(' + (registers) + ')').text();
                var htmlName = 'zamba_asoc_' + DocId + '_' + DocTypeId;
                var newInput = (DocTypeId == 26033)?
                '<img alt=\"\" id=' + htmlName + ' onclick=SetAsocId(this); value=\"Ver\" Name=\"' + htmlName + '\"  src=\"images/toolbars/note_pinned.png\" />':
                '<img alt=\"\" id=' + htmlName + ' onclick=SetAsocId(this); value=\"Ver\" Name=\"' + htmlName + '\"  src=\"images/toolbars/bullet_ball_blue.png\" />';         
               $(this).children('td:nth(0)').html(newInput);         
        });

        //Si hace clic en cualquier parte de la fila, abre el link en de la columna ejecutar(que esta oculto)
        $("#" + controlId + ' td').click(function () {
            var inputHtml = $(this).parent().children('td:nth(0)').html();
            var botonId = inputHtml.substring(inputHtml.indexOf("id"), inputHtml.indexOf(" onclick")).replace("id=", "");
            var boton = document.getElementById(botonId.split('"').join('')); // Quito comillas de más
            if (boton != undefined) boton.click();
        });
    }
    catch (err)
    { }
}

    function InspectionClick(link) {
    var loginurl = "";
    // ESTA URL ES EXCLUSIVA PARA HDI
    //loginurl = 'http://srvdesa/names.nsf?login&username=u01&password=password';
    if (loginurl != "") {
        var popup = (loginurl, 'popup');
        setTimeout(function () { OpenLink(link, popup); }, 2000);
    } else {
        window.open(link);
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
    if (data != undefined) {
        // Crea el header de la tabla usando las keys originales del JSON obtenido
        $('#jsonTable').append("<thead><tr></tr></thead>");
        var jsonstr = [];
        $.each(data[0], function (key) {
            $('#jsonTable > thead > tr').append("<th>" + key + "</th>");
        });
    }
}

function validateJson(data) {
    // Funcion que toma un JSON y cambia las keys a "colX" donde X es el numero de posicion, esto se hace para evitar errores al mostrar la tabla 
    // si alguna de las keys posee caracteres especiales
    var datosTablaFinal = [];  
    for (var i = 0; i < $.parseJSON(data).length;i++){
        var jsonKeys = Object.keys($.parseJSON(data)[i]);
        columns = {};
        for (var j = 0; j< jsonKeys.length; j++) {
            columns['col' + j.toString()] = $.parseJSON(data)[i][jsonKeys[j]];
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