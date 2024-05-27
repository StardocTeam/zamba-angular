$(document).ready(function() {
    ValidateTipoDoc();
});

function ValidateTipoDoc() {

    var JoM = $("#zamba_index_2681").val();
    var TipoDoc = $("#zamba_index_11535309").val();

    if (JoM == 'J') {
        TipoDoc = 'JJ';
    } else if (JoM == 'M') {
        TipoDoc = 'MM';
    }

    // Aquí puedes realizar acciones adicionales si es necesario con el valor de TipoDoc

    // Puedes asignar el valor actualizado de TipoDoc al elemento correspondiente
    $("#zamba_index_11535309").val(TipoDoc);
}
