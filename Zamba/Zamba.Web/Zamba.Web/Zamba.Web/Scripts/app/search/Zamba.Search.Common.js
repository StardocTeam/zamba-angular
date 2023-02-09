function ProcessResults(search) {

    var results = search.SearchResults;

    var resultsIds = [];
    for (var i = 0; i <= results.length - 1; i++) {

        try {
            var docid = results[i].DOC_ID;
            if (resultsIds.indexOf(docid) !== -1) {
                results.splice(i, 1);
                break;
            }
            else {
                resultsIds.push(docid);
            }
        } catch (e) {

        }
        // Icon
        results[i].Icon = GetImgIcon(results[i].ICON_ID);

        // Asignado
        results[i].AsignedTo = SetFieldText(results[i].ASIGNEDTO, results[i].ASIGNADO, "(Sin Asignar)");

        // Nombre de la tarea
        if (results[i].Tarea == undefined)
            results[i].Tarea = SetFieldText(results[i].NAME, results[i].Nombre, "(Sin Nombre)");

        // Etapa
        results[i].Step = SetFieldText(results[i].Step, results[i].Etapa, "(En Ninguna Etapa)");

        // FullPath
        if (results[i].FULLPATH !== undefined && results[i].FULLPATH !== null) {
            results[i].FULLPATH = results[i].FULLPATH.replace(/\//g, '\\');
        }

        try {
            // Mostrar como novedad
            if (results[i].LEIDO == 0 && (results[i].USER_ASIGNED == search.UserId || (search.GroupsIds != null && search.GroupsIds.includes(results[i].USER_ASIGNED)))) {
                results[i].ShowUnread = true;
            }
            else {
                results[i].ShowUnread = false;
            }

        } catch (e) {

        }
    }



    // Cargo los thumbs
    //$.get(serviceBase.toLowerCase().replace("/api", "/") + "api/search/GetThumbsPath")
    //    .done(function (fileServerPath) {
    //        if (fileServerPath) {

    //            if (window.location.origin.includes("https"))
    //                fileServerPath = window.location.origin + "/zambaweb.fs";

    //            SetDocsThumbs(results, fileServerPath);
    //        }
    //    }).fail(function () {
    //        console.log("No se pudo obtener la ruta de los thumbs");
    //    });

            //      SetDocsThumbs(results);

};

function SetDocsThumbs(results) {

    $.each(results, function (index, result) {
        try {
            if (!result.THUMB && !result.Thumb && !result.ThumbImg) {
                GetThumbImg(result);
            }
        } catch (e) {
            console.log(e);
        }
    });
}


// Recordar configurar esto en el servidor
//ver de poder poner async en IE
function GetThumbImg(result) {

    var value = {
        Params: {
            "userId": GetUID(),
            "doctypeId": result.DOC_TYPE_ID,
            "docid": result.DOC_ID,
            "converttopdf": true,
            "includeAttachs": false
        }
    }
    $.ajax({
        type: "POST",
        url: serviceBase + "/search/GetThumb",
        data: JSON.stringify(value),
        contentType: "application/json; charset=utf-8",
        crossDomain: true,
        async: false,
        success: function (data) {
            result.ThumbImg = DD.thumbImage;
        },
        error: function (ex) {
            console.log(ex.responseJSON.Message);
        },
        timeout: 60000
    });


}


function SetFieldText(objectData, responseData, valueIfEmpty) {
    var text = "";

    if (objectData !== undefined && objectData !== null && objectData !== " ") {
        text = objectData;
    }
    else if (responseData !== " " && responseData !== undefined && responseData !== null) {
        text = responseData;
    }
    else {
        text = valueIfEmpty;
    }

    return text;
};


//if (!Array.prototype.includes) {
//    Object.defineProperty(Array.prototype, "includes", {
//        enumerable: false,
//        value: function (obj) {
//            var newArr = this.filter(function (el) {
//                return el == obj;
//            });
//            return newArr.length > 0;
//        }
//    });
//}

//if (!Array.prototype.indexOf) {
//    Array.prototype.indexOf = function (obj, start) {
//        for (var i = (start || 0), j = this.length; i < j; i++) {
//            if (this[i] === obj) { return i; }
//        }
//        return -1;
//    }
//}
