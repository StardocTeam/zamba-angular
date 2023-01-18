var lista
var FiltaFinal = [];
var id;
var serviceBase  = location.origin.trim() + getValueFromWebConfig("RestApiUrl") + "/api";


var controladorTiempo = "";

function CodigoAjax(id) {
    id = parseInt(id.replace("zamba_index_", ""));
    CargarOptionSelect(id);
}

$("#zamba_index_16").on("keyup", function () {

    clearTimeout(controladorTiempo); 
    controladorTiempo = setTimeout(CodigoAjax("zamba_index_16"),2500);

});

//for (var i = 0; i < $(".generatelistdinamic").length; i++) {
//    id = $(".generatelistdinamic")[i].id;
//    CargarOptionSelect($(".generatelistdinamic")[i].id, i)

//}

function CargarOptionSelect(id) {

    
        // do a thing, possibly async, then…
        
        var valueInput = $("#zamba_index_" + id + "").val(); 
        CargarOptionSelectService(id,valueInput);
        lista = JSON.parse(lista);
        FiltaFinal = [];
        try {
            for (var i = 0; i < lista.length; i++) {
                FiltaFinal.push(lista[i]["Codigo"] + " - " + lista[i]["Descripcion"]);
            }

            $(".generatelistdinamic").autocomplete({
                source: FiltaFinal
            }).focus(function () {
                $(this).autocomplete("search");
            });

        } catch (e) {
          
        }
    

}



function CargarOptionSelectService(id, valueInput) {
    var genericRequest = {
        UserId: parseInt(GetUID()),
        Params:
        {
            "id": id,
            "inputValue": valueInput
        }
    };

    $.ajax({
        type: "POST",
        url: serviceBase + '/search/GetIndexDataSelectDinamic',
        data: JSON.stringify(genericRequest),

        contentType: "application/json; charset=utf-8",
        async: false,
        success:
            function (data) {
                lista = data;
            }
        ,
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Status: " + textStatus); alert("Error: " + errorThrown);
        }

    });
}

//------------------------//------------------------//------------------------//------------------------
//------------------------//--------------  INSERTAR.APX ---------------------//------------------------
//------------------------//------------------------//------------------------//------------------------


//------------------------ Este codigo es excepcional y sirve solo para el campo cliente I16 ---------------------//------------------------
//------------------------ modifica el drop down list por autocomplete --------------------

function SetAutocompleteIndex16() {
    $("#ucDocTypesIndexs_16").on("keyup", function () {

        clearTimeout(controladorTiempo);
        controladorTiempo = setTimeout(CodigoAjaxParaInsert("ucDocTypesIndexs_16"), 2500);

    });
}


function CodigoAjaxParaInsert(id) {
    id = parseInt(id.replace("ucDocTypesIndexs_", ""));
    CargarOptionSelectParaInsert(id);
}

function CargarOptionSelectParaInsert(id) {
    var valueInput = $("#ucDocTypesIndexs_" + id + "").val();
    if (valueInput.length < 2)
        return;
    CargarOptionSelectServiceParaInsert(id, valueInput);
    lista = JSON.parse(lista);
    FiltaFinal = [];
    try {
        for (var i = 0; i < lista.length; i++) {
            FiltaFinal.push(lista[i]["Codigo"] + " - " + lista[i]["Descripcion"]);
        }

        $("#ucDocTypesIndexs_" + id).autocomplete({
            source: FiltaFinal
        }).focus(function () {
            $(this).autocomplete("search");
        });

    } catch (e) {
        
    }
}

function CargarOptionSelectServiceParaInsert(id, valueInput) {
    var genericRequest = {
        UserId: parseInt(GetUID()),
        Params:
        {
            "id": id,
            "inputValue": valueInput,
            "LimitTo" : 30
        }
    };

    $.ajax({
        type: "POST",
        url: serviceBase + '/search/GetIndexDataSelectDinamic',
        data: JSON.stringify(genericRequest),

        contentType: "application/json; charset=utf-8",
        async: false,
        success:
            function (data) {
                lista = data;
            }
        ,
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Status: " + textStatus); alert("Error: " + errorThrown);
        }

    });
}

  // ESTE CODICO ES PARA TODAS LAS LISTAS SEAN DINAMICAS SE COMENTA POR Q SE CAMBIO EL CODIGO PARA SOLUCIONAR 
   // LA CARGA DE LA LISTA LENTA DE MARSH
    // MAS ADELANTE  VERIFICAR ESTE CODIGO YA QUE PUEDE SER DE UTILIDAD PARA CARGA LA WEB MAS RAPIDO


//for (var i = 0; i < $(".generatelistdinamic").length; i++) {
//        id = $(".generatelistdinamic")[i].id;
//        CargarOptionSelect($(".generatelistdinamic")[i].id,i)
        
//    }

//function CargarOptionSelect(id,posicion) {

//    var promise = new Promise(function (resolve, reject) {
//        // do a thing, possibly async, then…
//        id = parseInt(id.replace("zamba_index_", ""));
//        CargarOptionSelectService(id);
//        lista = JSON.parse(lista);
//        FiltaFinal = [];
//        try {
//            for (var i = 0; i < lista.length; i++) {
//                FiltaFinal.push(lista[i]["Codigo"] + " - " + lista[i]["Descripcion"]);
//            }

//            resolve(FiltaFinal);

//        } catch (e) {
//            reject("Error en el for");
//        }
//    });


//    promise.then(function (result) {
//        FiltaFinal = result;
//        $($(".generatelistdinamic")[posicion]).autocomplete({
//            source: FiltaFinal
//        }).focus(function () {
//            $(this).autocomplete("search");
//        });
//        // "Stuff worked!"
//    }, function (err) {
//        console.log(err); // Error: "It broke"
//    });

//}

//function CargarOptionSelectService(id) {
//    var genericRequest = {
//        UserId: parseInt(GetUID()),
//        Params:
//        {
//            "id": id
//        }
//    };

//    $.ajax({
//        type: "POST",
//        url: serviceBase + '/search/GetIndexDataSelect',
//        data: JSON.stringify(genericRequest),

//        contentType: "application/json; charset=utf-8",
//        async: false,
//        success:
//            function (data) {
//                lista = data;
//            }
//        ,
//        error: function (XMLHttpRequest, textStatus, errorThrown) {
//            alert("Status: " + textStatus); alert("Error: " + errorThrown);
//        }

//    });
//}
