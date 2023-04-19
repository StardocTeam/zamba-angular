$(document).ready(function () {

    ValidarTipoDocCartaDocumento();

    $("#zamba_index_25").on({
        "change": ValidarTipoDocCartaDocumento,

    });
});


function ValidarTipoDocCartaDocumento() {

    if ($("#zamba_index_25").val() != "3") {
        $('#zamba_index_7').attr('disabled', 'disabled');
        $("#zamba_rule_11548807").hide();

    } else {
        $('#zamba_index_7').removeAttr("disabled");
        $("#zamba_rule_11548807").show();
        $('#zamba_index_1020170').removeAttr("disabled");

    }


}
$("#zamba_index_19").on("change", function (e) {
    // toastr.info('Cambio el Ramo');
    var target = e.target;
    var childeNodeNumbreFromParent = target.getAttribute("childindexid");
    var parentTag = target.tagName.toLowerCase();
    var childNode = $('#zamba_index_' + childeNodeNumbreFromParent);
    var childOptions = {};
    cleanOptions(childNode)
    if (target.value == null || target.value == "") {
        childNode.prop('disabled', true);
        childOptions = getArrayValues(0);
    } else {
        childNode.prop('disabled', false);
        childOptions = getArrayValues(target.value);
    }
    setDropDownValues(childNode, childOptions);

});