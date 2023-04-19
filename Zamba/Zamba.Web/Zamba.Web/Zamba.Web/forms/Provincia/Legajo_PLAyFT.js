$(document).ready(function () {

    ValidarDuplicidadDatos();
    
    ValidarTipoDocVacioDrop();

});

function ValidarDuplicidadDatos() {
    if ($('#zamba_index_2734').val() == 'F') {
        $('#AreaDropzone').hide();
        $('#AreaBoton').hide();
        $('#AreaAtributos').hide();
        $('#AreaBoton2').show();
        $('#AreaAtributos2').hide();
        $('#AreaCUITLupa').show();
        $('#AreaDNILupa').show();
        $('#AreaCLIENTELupa').show();

    } else {
        $('#AreaDropzone').show();
        $('#AreaBoton').show();
        $('#AreaAtributos').show();
        $('#AreaBoton2').hide();
        $('#AreaAtributos2').hide();
        $('#AreaCUITLupa').hide();
        $('#AreaDNILupa').hide();
        $('#AreaCLIENTELupa').hide();
        $("#zamba_index_11").attr("readonly","readonly");
        $("#zamba_index_2").attr("readonly","readonly");
        $("#zamba_index_5").attr("readonly","readonly");
    }
}

function ValidarTipoDocumentacion() {
    if ($('#zamba_index_1020090').val() == '3' || $('#zamba_index_1020090').val() == '4' || $('#zamba_index_1020090').val() == '5' || $('#zamba_index_1020090').val() == '6' || $('#zamba_index_1020090').val() == '7') {
        swal("", "Se debe completar el campo año", "warning");
        $('#AreaAtributos2').show();
        $('#AreaDropzone').hide();
        $("#zamba_index_117").val('0')    

    } else {
        $('#AreaAtributos2').hide();
        $('#AreaDropzone').show();
        $("#zamba_index_117").val('0')
    }
}

function ValidarTipoDocVacioDrop() {
    if($("#zamba_index_1020090").val() == ''){
        $('#AreaDropzone').hide();
    
    }    
}

$("#zamba_index_1020090").on({
    "change": ValidarTipoDocumentacion,
});

$("#zamba_index_117").on({
    "change": HabilitarDropzoneTipoDoc,
});

function HabilitarDropzoneTipoDoc() {
    if($("#zamba_index_117").val() != ''){
        $('#AreaDropzone').show();
    
    }    
}