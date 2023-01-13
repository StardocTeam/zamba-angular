function ValueEdit(e) {
    var MultiSelection = window.localStorage.getItem("MultipleSelection");
    if (MultiSelection == "false") {
        var idgrilla = $("zamba-associated-kendo:visible")[0].children[0].id;
        var gridElement = $("#" + idgrilla);
        var grid = gridElement.data("kendoGrid");
        var row = $(e.target).closest("tr");
        var dataItem = grid.dataItem(row);

        var MyNewInput = newInputConfiguration_ForSWAL();
        swal("Modifique el valor NroInterno:", {
            content: MyNewInput,
            buttons: ["Cancelar", "Aceptar"]
        })
            .then((Aceptar) => {
                if (Aceptar) {
                    InsertIndex($("#swal-content__input").val(), 139072, 160660, dataItem.DOC_ID);
                }
            });
    }
}

function InsertIndex(indexValue, entityId, indexId, parentResultId) {
    if (indexValue != null && indexValue != "null") {
        saveIndexs(indexValue, entityId, indexId, parentResultId);
    }
}

function saveIndexs(indexValue, entityId, indexId, parentResultId) {
    var isIndexUpdated = false;
    var genericRequest = {
        UserId: parseInt(GetUID()),
        Params:
        {
            "indexValue": indexValue,
            "entityId": entityId,
            "indexId": indexId,
            "parentResultId": parentResultId,
        }

    };

    $.ajax({
        type: "POST",
        url: serviceBase + '/search/setTaskIndex',
        data: JSON.stringify(genericRequest),

        contentType: "application/json; charset=utf-8",
        async: false,
        success:
            function (data, status, headers, config) {
                isIndexUpdated = data;

                swal("", "Se guardo con exito!", "success");
                LoadGrillaForm($("zamba-associated")[0].attributes[0].value);
            },
        error: function (data) {
            swal("Error en el guardado", "por favor, verifique los campos ingresados!", "error");
        }
    });
}

//////////// DOSHOWTABLE ////////////////////////


function LoadIndexEdit(e) {
    var docid = e.target.getAttribute("doc_id");

    var MyNewInput = newInputConfiguration_ForSWAL();

    swal("Modifique el valor NroInterno:", {
        content: MyNewInput,
        buttons: ["Cancelar", "Aceptar"]
    })
        .then((Aceptar) => {
            if (Aceptar) {
                InsertIndexDo($("#swal-content__input").val(), 139072, 160660, docid);
            }
        });

}


function newInputConfiguration_ForSWAL() {
    try {
        var attribute_Class = "";
        attribute_Class += "swal-content__input ";
        var newInput = document.createElement("input");
        newInput.setAttribute("id", "swal-content__input");
        newInput.setAttribute("autocomplete", "off");

        //Solo permite numeros
        //attribute_Class += "solonums";
        newInput.setAttribute("class", attribute_Class);
        newInput.setAttribute("onkeypress", "soloNumerosYLetras(event, this)");
        //Local_detectSubscriptionClass(newInput, "solonums");

        switch (newInput.type) {
            case "text":
            default:
                newInput.clientHeight = 37;
                newInput.clientWidth = 436;

                newInput.offsetHeight = newInput.clientHeight + 2;
                newInput.offsetWidth = newInput.clientWidth + 2;
                newInput.offsetLeft = 20;
                newInput.offsetTop = 178;

                newInput.scrollHeight = newInput.clientHeight;
                newInput.scrollWidth = newInput.clientWidth;
        }

        newInput.isConnected = true;
        return newInput;
    } catch (e) {
        console.error(e + " - Lanzado por: newInputConfiguration_ForSWAL");
    }
}


function InsertIndexDo(indexValue, entityId, indexId, parentResultId) {
    if (indexValue != null && indexValue != "null") {
        saveIndexsDo(indexValue, entityId, indexId, parentResultId);
    }
}

function saveIndexsDo(indexValue, entityId, indexId, parentResultId) {
    var isIndexUpdated = false;
    var genericRequest = {
        UserId: parseInt(GetUID()),
        Params:
        {
            "indexValue": indexValue,
            "entityId": entityId,
            "indexId": indexId,
            "parentResultId": parentResultId,
        }

    };

    $.ajax({
        type: "POST",
        url: serviceBase + '/search/setTaskIndex',
        data: JSON.stringify(genericRequest),

        contentType: "application/json; charset=utf-8",
        async: false,
        success:
            function (data, status, headers, config) {
                isIndexUpdated = data;

                swal("", "Se guardo con exito!", "success");

                //Solo refresca la grilla de forma local.
                $("button[doc_id=" + parentResultId + "]").parent().parent()[0].children[5].innerHTML = indexValue;
            },
        error: function (data) {
            swal("Error en el guardado", "por favor, verifique los campos ingresados!", "error");
        }
    });
}

//Solo permite introducir numeros.
function soloNumeros(e, field) {
    var key = window.event ? e.which : e.keyCode;
    if (key < 48 || key > 57) {
        e.preventDefault();
    }
}

//Solo permite introducir numeros, letras, barras y guiones.
function soloNumerosYLetras(e, field) {
    var key = window.event ? e.which : e.keyCode;
    var flag = false;

    if (key > 47 && key < 58)
        flag = true;

    if (key > 64 && key < 91)
        flag = true;

    if (key > 96 && key < 123)
        flag = true;

    //Guiones y Barras
    if (key == 45 || key == 47)
        flag = true;

    if (!flag)
        e.preventDefault();
}
