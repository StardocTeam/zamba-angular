$(document).ready(function () {
    $(".solonums").each(function () {
        $(this).keypress(function (e) {
            return IntegerCheck(e);
        })
    });

    let inpAsegurado = document.querySelector("#zamba_index_3");

    inpAsegurado.addEventListener("keyup", () => {

        if (inpAsegurado.value.length < 1) {
            document.querySelector("#zamba_rule_9143").disabled = true;
        } else {
            document.querySelector("#zamba_rule_9143").disabled = false;
        }
    });

    // si el campo Asegurado esta vacio al cargar el formulario lo dehabilita
    if (inpAsegurado.value == "")
        document.querySelector("#zamba_rule_9143").disabled  = true;
});


function CompletarVtoNotificacionDemanda() {
    if (document.getElementById("zamba_index_2704").value == "") {
        $("#zamba_index_2704").addClass("error");
    } else {
        $("#zamba_index_2704").removeClass("error");
        var dias = $("#txtCantHabiles").val();
        var fecha = $("#zamba_index_2704").val();
        fecha = CalcularLaborales(dias, fecha);
        $("#zamba_index_2705").val(fecha);
    }
}
function zamba_save_onclick_Notificacion_Demanda() {
    if (document.getElementById("zamba_index_2704").value == "") {
        swal("Por favor, complete la Fecha de Notificacion.");
        document.getElementById("hdnRuleId").name = "zamba_rule_cancel";
        CloseLoading();
        event.preventDefault();
    } else if (document.getElementById("zamba_index_2705").value == "") {
        swal("Por favor, complete la fecha de Vencimiento.");
        document.getElementById("hdnRuleId").name = "zamba_rule_cancel";
        CloseLoading();
        event.preventDefault();
    //} else if (document.getElementById("zamba_index_64") != null && document.getElementById("zamba_index_64").length > document.getElementById("zamba_index_64").maxlength) {
    //    swal("El límite máximo de caracteres para el campo Observacion es de " + document.getElementById("zamba_index_64").maxlength + ".\nPor favor, verifique dicho campo.");
    //    document.getElementById("hdnRuleId").name = "zamba_rule_cancel";
    //    CloseLoading();
    //    event.preventDefault();
    } else if (document.getElementById("zamba_index_2677").value == "") {
        swal("Por favor, identifique un reclamo.");
        document.getElementById("hdnRuleId").name = "zamba_rule_cancel";
        CloseLoading();
        event.preventDefault();
    } else {
        swal("Se guardo exitosamente.");
        document.getElementById("hdnRuleId").name = "zamba_rule_save";
        frmMain.submit();
    }
}

function CloseLoading() {
    setTimeout("parent.hideLoading();", 500);
}
function CargarFechasyBotones() {

    //Ocultamos el primer campo el combobox. Administrativo.
    var x = document.getElementById('zamba_index_2681'); x.remove(0);

    //CargarFechaIndice('zamba_index_2704');
    //CargarFechaIndice('zamba_index_2705');			
}
function VerificarJuiMed(sel) {
    if (sel.value == 'Juicio')
        $("#zamba_index_2717").val(1);
    else
        $("#zamba_index_2717").val(4);
}
function VerificarTipoNotif(sel) {
    if (sel.value == 4)
        $("#zamba_index_2681").val('Mediación');
    else
        $("#zamba_index_2681").val('Juicio');
}
function IDReclamo_KeyPress(e) {
    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        return false;
    }
    var key = window.event ? e.keyCode : e.which;
    if (key == 13) {
        document.getElementById("zamba_rule_11237").click();
    }
}
function Caratula_KeyPress(e) {
    var key = window.event ? e.keyCode : e.which;
    if (key == 13) {
        document.getElementById("zamba_rule_9236").click();
    }
}
function Patente_KeyPress(e) {
    var key = window.event ? e.keyCode : e.which;
    if (key == 13) {
        document.getElementById("zamba_rule_9147").click();
    }
}
function Asegurado_KeyPress(e) {
    var key = window.event ? e.keyCode : e.which;
    if (key == 13) {
        document.getElementById("zamba_rule_9143").click();
    }
}
function IngresoDeFecha(obj) {
    $('#zamba_index_2704').removeClass('error');
    $('#txtCantHabiles').val('');
    $('#zamba_index_2705').val('');
    valFecha(obj);
}

window.onload = function () {
    setTimeout(function () { CargarFechasyBotones(); }, 200);
};
