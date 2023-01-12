String.prototype.trim = function () {
    return this.replace(/^\s*/, "").replace(/\s*$/, "");
}
function SetRuleId(sender) {
    document.getElementById("hdnRuleId").name = sender.id;
    frmMain.submit();
}

function SetAsocId(sender) {
    document.getElementById("hdnAsocId").name = sender.id;
    frmMain.submit();
}

function CloseLoading() {
    setTimeout("parent.hideLoading();", 500);
}

function zamba_save_onclick_digitalizacion(e) {
    //alert("Se guardó exitosamente.");
    //Aca puedo realizar las contravalidaciones, osea ademas de realizar las validaciones campo por campo
    //a su vez podria realizar una validacion general en este cuerpo.

    //alert ('zamba_save_onclick');
    var mensaje = MensajeError();
    if (mensaje != "") {
        swal("", mensaje[0], "warning");
        document.getElementById("hdnRuleId").name = e.id;
        CloseLoading();
        e.preventDefault();
    } else {
        //document.getElementById("hdnRuleId").name = "zamba_rule_save";
        swal("", "Se guard\u00F3 de manera exitosa.", "success");
        document.getElementById("hdnRuleId").name = e.id;
        frmMain.submit();
    }
}

function MensajeError() {
    var mensaje = "";
    //return mensaje;


    //Validaciones por tipo de documentacion

    var TipoDocumentacion = document.getElementById('zamba_index_70').value;

    //alert ('MensajeError');
    //alert(TipoDocumentacion);

    //Tipo documentación
    if (TipoDocumentacion == null || TipoDocumentacion == "" || TipoDocumentacion == 0) {

        mensaje = ["El tipo de documentaci\u00F3n es un dato obligatorio", "zamba_index_70"];
        return mensaje;
    }
    //if (TipoDocumentacion == 13) {
    //$('#divMontoFrame').show();

    //Moneda req
    //if (document.getElementById("zamba_index_105").value.trim() == "" || document.getElementById("zamba_index_105").value.trim() < 0)
    //mensaje = ["La moneda es requerida cuando la documentación es un informe de liquidador", "zamba_index_105"];

    //Monto req
    //if (document.getElementById("zamba_index_2809").value.trim() == "" || document.getElementById("zamba_index_2809").value.trim() == 0)
    //mensaje = ["El monto es requerido cuando la documentación es un informe de liquidador", "zamba_index_2809"];

    //}

    return mensaje;
}

/*function CargarFechasyBotones(){
$(function () {
    //---Setea valor N a Reintegro
    mesa = $('#zamba_index_72').val();
    if (mesa !== undefined) {
        if (mesa == "" || mesa == null) {
            document.getElementById('zamba_index_72').value = "N";
        }
    }
    //---Setea valor N a Original/Copia
    original = $('#zamba_index_71').val();
    //alert(original);
    if (original !== undefined) {
        if (original == "" || original == null) {
            document.getElementById('zamba_index_71').value = "N";
        }
    }
});					
}*/


function removeCombo() {
    var y = document.getElementById('zamba_index_68').value;
    if (y == "" || y == null) {
        document.getElementById('zamba_index_68').value = 0;
    }
    /*var x = document.getElementById('zamba_index_68');
    x.remove(0);
    x.remove(0);
    x.remove(0);
    x.remove(0);
    x.remove(0);
    
    x.remove(0);
    x.remove(0);
    x.remove(0);
    x.remove(0);
    x.remove(0);
    
    x.remove(0);
    x.remove(0);
    */
    if ($('#zamba_index_78').val() == 'S') {
    } else {
        $('#EstaEnLegales').hide();
    }

    if ($('#zamba_index_79').val() == 'S') {
    } else {
        $('#EstaEnReaseguros').hide();
    }

}
window.onload = function () {
    setTimeout(function () { removeCombo(); }, 200);
};

