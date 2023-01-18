$(document).ready(function () {
    $(".solonums").each(function () {
        $(this).keypress(function (e) {
            return IntegerCheck(e);
        })
    });
});
function CompletarVtoFecha() {
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
function zamba_save_onclick_notificaciones() {
    if (document.getElementById("zamba_index_2704").value == "") {
        swal("Por favor, complete la Fecha de Notificación.");
        document.getElementById("hdnRuleId").name = "zamba_rule_cancel";
        CloseLoading();
        event.preventDefault();
    } else if (document.getElementById("zamba_index_2705").value == "") {
        swal("Por favor, complete la fecha de Vencimiento");
        document.getElementById("hdnRuleId").name = "zamba_rule_cancel";
        CloseLoading();
        event.preventDefault();
    } else if (document.querySelector(".dz-success.dz-complete") == null) {
        swal("Por favor, Agregar un archivo");
        document.getElementById("hdnRuleId").name = "zamba_rule_cancel";
        CloseLoading();
        event.preventDefault();
    } else {
        document.getElementById("hdnRuleId").name = "zamba_rule_save";
    }
}


function CloseLoading() {
    setTimeout("parent.hideLoading();", 500);
}
function CargarFechasyBotones() {
    //Ocultamos el primer campo el combobox. Administrativo.
    var x = document.getElementById('zamba_index_2681'); x.remove(0);

    CargarFechaIndice('zamba_index_2704');
    CargarFechaIndice('zamba_index_2705');
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
function IngresoDeFecha(obj) {
    $('#zamba_index_2704').removeClass('error');
    $('#txtCantHabiles').val('');
    $('#zamba_index_2705').val('');
    valFecha(obj);
}

window.onload = function () {
    setTimeout(function () { var x = document.getElementById('zamba_index_2681'); x.remove(0); }, 200);
};
