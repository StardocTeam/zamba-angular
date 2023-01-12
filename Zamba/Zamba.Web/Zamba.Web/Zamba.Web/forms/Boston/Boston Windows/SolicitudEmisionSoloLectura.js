function VerificarHabilitacionMotivoRechazo() {
    if ($('#zamba_index_1138').val() == "") {
        $("#pnlMotivoRechazo").hide();
    } else {
        $("#pnlMotivoRechazo").show();
    }
} 