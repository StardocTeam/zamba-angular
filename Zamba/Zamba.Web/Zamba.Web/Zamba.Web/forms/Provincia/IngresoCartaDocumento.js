$(document).ready(function () {

    ValidarTipoDocumentacion();

});

function ValidarTipoDocumentacion() {
    if ($('#zamba_index_2734').val() == 'F') {
        $('#AreaDropzone').hide();
        $('#AreaBoton').hide();

    } else {
        $('#AreaDropzone').show();
        $('#AreaBoton').show();
    }
}
