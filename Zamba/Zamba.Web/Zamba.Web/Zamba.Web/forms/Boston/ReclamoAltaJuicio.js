function SetRuleId2(sender) {
    VerificarHabilitacionSiniestro();
    document.getElementById("hdnRuleId").name = sender.id;
}

function DeshabilitarFechaPrimeraNotificacion() {
    if ($("#zamba_index_2682").val() != "" && $("#zamba_index_2682").val() != null) {
        //Dehabilitar fecha Notificacion
        $('#zamba_index_2682').attr('disabled', 'true');

    }
}

function CloseLoading() {
    setTimeout("parent.hideLoading();", 500);
}

function zamba_save_onclick_AltaJuicio(event) {
    VerificarHabilitacionSiniestro();
    CalcularEstimacion();

    if (document.getElementById("zamba_index_2736").value == "") {
        swal("", "Por favor, verifique el Ejercicio antes de continuar", "warning");
        document.getElementById("hdnRuleId").name = "zamba_rule_cancel";
        CloseLoading();
        event.preventDefault();
    } else if (document.getElementById("zamba_index_1147").value == "") {
        swal("", "Por favor, verifique Nro de Siniestro antes de continuar", "warning");
        document.getElementById("hdnRuleId").name = "zamba_rule_cancel";
        CloseLoading();
        event.preventDefault();
    } else if (document.getElementById("zamba_index_2694").value == "") {
        swal("", "Por favor, verifique el Nro de Subsiniestro antes de continuar", "warning");
        document.getElementById("hdnRuleId").name = "zamba_rule_cancel";
        CloseLoading();
        event.preventDefault();
    } else if (document.getElementById("zamba_index_14").value == "") {
        swal("", "Por favor, verifique el Nro de Item antes de continuar", "warning");
        document.getElementById("hdnRuleId").name = "zamba_rule_cancel";
        CloseLoading();
        event.preventDefault();
    } else if (document.getElementById("zamba_index_2701").value == "") {
        swal("", "Por favor, verifique la Carátula antes de continuar", "warning");
        document.getElementById("hdnRuleId").name = "zamba_rule_cancel";
        CloseLoading();
        event.preventDefault();
    } else if (document.getElementById("zamba_index_34").value == "") {
        swal("", "Por favor, verifique el Nro de Póliza antes de continuar", "warning");
        document.getElementById("hdnRuleId").name = "zamba_rule_cancel";
        CloseLoading();
        event.preventDefault();
    } else if (document.getElementById("zamba_index_2683").value == "") {
        swal("", "Por favor, verifique la Fecha de Ocurrencia antes de continuar", "warning");
        document.getElementById("hdnRuleId").name = "zamba_rule_cancel";
        CloseLoading();
        event.preventDefault();
    } else if ($('#zamba_index_2742').val() == "S" && $('#zamba_index_1000002').val() == '') {
        swal("", "Por favor, verifique el Motivo de Rechazo antes de continuar", "warning");
        document.getElementById("hdnRuleId").name = "zamba_rule_cancel";
        CloseLoading();
        event.preventDefault();
    } else if (document.getElementById("zamba_index_2682").value == "") {
        swal("", "Por favor, verifique la Primera Fecha de Notificación antes de continuar", "warning");
        document.getElementById("hdnRuleId").name = "zamba_rule_cancel";
        CloseLoading();
        event.preventDefault();
    } else if ($('input[type=text]').hasClass('error')) {
        swal("", "Verifique que todos los datos de los campos sean válidos", "warning");
        document.getElementById("hdnRuleId").name = "zamba_rule_cancel";
        CloseLoading();
        event.preventDefault();
    } else {
        swal("", "Se guardo exitosamente!", "success");
        document.getElementById("hdnRuleId").name = "zamba_rule_save";
        frmMain.submit();
    }
}

$(function () {
    $(".solonums").each(function () {
        $(this).keypress(function (e) {
            return IntegerCheck(e);
        })
    });
    $(".moneda").each(function () {
        $(this).keyup(function (e) {
            return DecimalCheck($('#' + e.target.id))
        })
    });

    window.onload = function () {
        if ($('#zamba_index_1001014').val() == "") {
            $('#zamba_index_1001014').val('N');
        }
        setTimeout(function () { FuncionesPosCargaDeReclamo(); }, 500);
    };
});

$(document).ready(function () {
    if (document.querySelector("#zamba_index_2695").value != '') {
        document.querySelector('#zamba_index_2749').setAttribute('disabled', 'true');
        document.querySelector('#zamba_index_2736').setAttribute('disabled', 'true');
        document.querySelector('#zamba_index_1147').setAttribute('disabled', 'true');
        document.querySelector('#zamba_index_2694').setAttribute('disabled', 'true');
        document.querySelector('#zamba_rule_9123').setAttribute('disabled', 'true');
        document.querySelector('#zamba_rule_10474').setAttribute('disabled', 'true');
        document.querySelector('#zamba_index_14').setAttribute('disabled', 'true');
        document.querySelector('#zamba_index_2682').setAttribute('disabled', 'true');
    }

    if (document.querySelector("#zamba_index_2695").value != '' && document.querySelector('#zamba_index_2749').value != '' && document.querySelector('#zamba_index_2736').value != '' && document.querySelector('#zamba_index_1147').value != '' && document.querySelector('#zamba_index_2694').value != '') {
        document.querySelector("#zamba_index_2695").setAttribute('disabled', 'true');
        document.querySelector("#zamba_rule_13193").setAttribute('disabled', 'true');
    }

    document.querySelector('#zamba_index_1000002').setAttribute('disabled', 'true');
    document.querySelector('#zamba_index_2832').setAttribute('disabled', 'true');
    document.querySelector('#zamba_index_2833').setAttribute('disabled', 'true');

});


function FuncionesPosCargaDeReclamo() {
    DeshabilitarFechaPrimeraNotificacion();
    if ($('#zamba_index_2695').val() != "" && $('#zamba_index_2695').val() != null) {
        //Deshabilitar Juicio/Mediacion
        $('#zamba_index_2695').attr('disabled', 'true');
    }

    if ($('#zamba_index_2825').val() == 1) {
        $('#zamba_index_2831').removeAttr("disabled");
        $('#zamba_index_2832').removeAttr("disabled");
        $('#zamba_index_2833').removeAttr("disabled");
    } else {
        $('#zamba_index_2831').val('N');
        $('#zamba_index_2832').val('');
        $('#zamba_index_2833').val('');
        $('#zamba_index_2831').attr('disabled', 'disabled');
        $('#zamba_index_2832').attr('disabled', 'disabled');
        $('#zamba_index_2833').attr('disabled', 'disabled');
    }

    //Sirve para guardar los estados previos. Al detectar una modificacion desde 
    //el evento indices de wf se impacta en SISE mediante webservices
    $("#zamba_index_2875").val($("#zamba_index_2740").val());
    $("#zamba_index_2886").val($("#zamba_index_2825").val());

    VerificarHabilitacionSiniestro();
    VerificarHabilitacionRechazo();
}
function VerificarLesiones() {
    if ($('#zamba_index_2825').val() == 1 && $('#zamba_index_2831').val() == "S") {
        $('#zamba_index_2832').removeAttr("disabled");
        $('#zamba_index_2833').removeAttr("disabled");
    }
    else {
        $('#zamba_index_2832').val('');
        $('#zamba_index_2833').val('');
        $('#zamba_index_2832').attr('disabled', 'disabled');
        $('#zamba_index_2833').attr('disabled', 'disabled');
    }
}
