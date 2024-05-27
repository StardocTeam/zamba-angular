//Funcion que permite que los caracteres ingresados sean solo numeros, puntos y una sola coma. v1.1
function val_ImporteEvent(event) {
    if (!isNaN(parseInt(event.key)) || event.key == ',') {
        if (event.target.value.search(',') != -1 && event.key == ',') {
            event.preventDefault();
            return true;
        }
    } else {
        event.preventDefault();
        return false;
    }
}

//Funcion que permite que los caracteres ingresados sean solo numeros, puntos y una sola coma. v1.0
function val_Importe(elem, e) {
    if (!isNaN(parseInt(e.key)) || e.key == ',') {
        if (elem.value.search(',') != -1 && e.key == ',') {
            e.preventDefault();
        }
    } else {
        e.preventDefault();
    }
}

///Funcion que solo permite el ingreso de numeros.
function val_SoloNumeros(event) {
    try {
        if (isNaN(parseInt(event.key))) {
            event.preventDefault();
        }
    } catch (e) {
        console.log(event.target.id + ": " + e + " - Lanzado por: " + "[" + arguments.callee.name + "]");
    }
}

//Funcion que valida que el dato ingresado en el Control es una fecha.
function val_Fecha(elem) {
    //var fecha = moment(elem.value, "DD/MM/YYYY");
    if (elem.value == "")
        return true;

    if (val_FormatoFecha(elem)) {
        if (elem.value != "" && moment(elem.value, "DD/MM/YYYY").isValid() == false) {
            elem.value = "";
            $(elem).css("border-color", "#FF0000");
            swal("", "La Fecha de Pago '" + elem.value + "' no es valida, vuelva a ingresarla", "error");
        }
    }
    else {
        elem.value = "";
        $(elem).css("border-color", "#FF0000");
        swal("", "El formato de la Fecha de Pago '" + elem.value + "' no es valida, use el formato DIA/MES/AÑO y vuelva a intentarlo", "error");
    }
}

//Devuelve un booleano indicando si el formato de la fecha pasada por parametro es correcta o no.
function val_FormatoFecha(formatoFecha) {
    var RegExPattern = /^\d{1,2}\/\d{1,2}\/\d{2,4}$/;
    if ((formatoFecha.value.match(RegExPattern)) && (formatoFecha.value != '')) {
        console.log("Formato de la fecha correcta");
        return true
    } else {
        console.log("Formato de la fecha incorrecta");
        return false;
    }
}

//funcion que renderiza a importe un valor logico obtenido de una fuente.
function render_Importe(event) {
    var target = event.target;

    if (target != undefined) {
        var targetId = target.getAttribute("id");

        if (targetId != undefined) {
            if (document.querySelector("#" + targetId).value != '') {
                target.value = target.value.replaceAll('-', '');

                var valor = parseFloat(target.value.replace(/\./g, '').replace(',', '.'));
                target.value = valor.toLocaleString('es-AR', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
            }
        }
    }
}

//funcion que renderiza a importe un valor logico obtenido de una fuente de AngularJS
function render_ImporteForNgGrid(elemento) {
    if (elemento != undefined) {
        elemento.value = elemento.value.replaceAll('-', '');
        elemento.value = isNaN(parseFloat(elemento.value).toFixed(2)) ? 0 : elemento.value;

        elemento.value = elemento.value.replaceAll('.', '').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ".");
        //TO DO: Desarrollar y abstraer una funcion que realice las siguientes sentencias.
    }
}

//Funcion que renderisa los valores logicos a un importe pasando el ID del elemento. v1.2 
function setInputSeparatorElem(elem) {
    try {
        if (elem != undefined) {
            if (elem.value != 'NaN' || elem.value != '') {
                elem.value = elem.value.replace('.', ',').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ".");
            } else {
                elem.value = '';
            }
        } else {
            throw("ERROR: Elemento no definido.");
        }
    } catch (e) {
        console.error(e);
    }
}

//funcion para establecer millares en tiempo real minetras se cargan numeros.
function setInput_Separator_AtChange(event) {

    //var emi = event.target.value + event.key

    //if (val_ImporteEvent(event)) {
    //}

}

//Funcion que renderisa los valores logicos a un importe pasando el ID del elemento. v1.0 (QUEDARA OBSOSLETO)
function setInputSeparator(id) {
    var elemento = $("#" + id);

    if (elemento.val() == 'NaN' || elemento.val() == '') {
        elemento.val('');
    }
    else if (elemento.val() != undefined) {
        // elemento.val(elemento.val().replace('.', ',').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, "."));
        var newvalue = new Intl.NumberFormat("es-AR", { style: "decimal", minimumFractionDigits: 2 }).format(elemento.val());
        if (newvalue != 'NaN') elemento.val(newvalue);
    }
}

//Funcion que termina de llenar el TextBox numerico con ceros. v1.0
function setOnInput_ZerosLeft(id) {
    var elemento = $("#" + id)[0];
    var MaxLength = elemento.getAttribute("maxlength");

    $("#" + id).val(elemento.value.padStart(MaxLength, "0"));
}

//Funcion que termina de llenar el TextBox numerico con ceros. v1.2
function setOnInput_ZerosLeftElem(elem) {
    try {
        if (elem.value != "") {
            var MaxLength = elem.getAttribute("maxlength");
            elem.value = elem.value.padStart(MaxLength, "0");
        }
    } catch (e) {
        console.log(elem.id + ": " + e + "; Funcion: setOnInput_ZerosLeftElem(elem)");
    }
}

//Obtiene el atributo "maxlength" para terminar de llenar el TextBox numerico de ceros. v1.1
function AddZerosLeft(e) {
    var elemento = e.target
    setOnInput_ZerosLeftElem(elemento);
}

function selectEvent(event) {
    $(event.target).select();
}


//Suscripciones
$(document).ready(function () {
    $(".ZSoloNumeros").each(function (index, elem) {
        $(elem).on({
            "keypress": val_SoloNumeros
        });
    });
})