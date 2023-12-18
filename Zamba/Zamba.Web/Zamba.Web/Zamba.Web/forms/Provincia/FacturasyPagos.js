﻿$(document).ready(function () {

    //Establece Puntos millares y coma a los importes traidos del origen.
    // setInputSeparator('zamba_index_109');

    //Renderizacion de valor numerico a importe.
    $("#zamba_index_109").on({
        "focusout": render_Importe,
    });

    $("#zamba_index_11535299").on({
        "focusout": render_Importe,
    });

    $("#zamba_index_11535300").on({
        "focusout": render_Importe,
    });

    $("#zamba_index_11535301").on({
        "focusout": render_Importe,
    });

    $("#zamba_index_11535296").on({
        "focusout": render_Importe,
    });


    CheckImporteMoneda();
    CheckMiPyme();
    ValidarEndidad();

    $("#zamba_index_92").on({
        "change": CheckImporteMoneda,
    });

    $("#zamba_index_11535223").on({
        "focusout": CheckImporteMoneda,
    });

    document.querySelector("#zamba_index_11535222").addEventListener("focusout", e => {
        
        render_Importe(e);
        CheckImporteMoneda();
        setMultipleSeparatorForm();
 
    });

    document.querySelector("#zamba_index_2725").addEventListener("change", function () {
        showConceptDependant(this.value);
    });

    //se deshabilita el campo OrdenDeCompra
	if ($('#zamba_index_50').val() != undefined){
    document.querySelector("#zamba_index_50").disabled = true;
    SelectTipoGasto();
	}
	
    $("#zamba_index_11535222").on("focusout", function () {
        CheckImporteMoneda();
        validateImportePesos();
    });

    document.querySelector("#zamba_index_11535222").addEventListener("keydown", e => {
        noPunto(e);
    });

});

function showConceptDependant(conceptValue) {
    let codesToValidate = [208, 232, 254, 270, 507, 511];

    let rv = codesToValidate.some(function (value) {
        return value == conceptValue;
    });
    if (rv)
        $(".concept-dependant").show();
    else
        $(".concept-dependant").hide();
};

//Para los form RO el control es un input en lugar de un select
function showConceptDependantRO(conceptValueText) {
    let conceptValue = conceptValueText.split("-")[0].trim();
    let codesToValidate = [208, 232, 254, 270, 507, 511];

    let rv = codesToValidate.some(function (value) {
        return value == conceptValue;
    });
    if (rv)
        $(".concept-dependant").show();
    else
        $(".concept-dependant").hide();
};

function setMultipleSeparatorForm() {
    try {
        setTimeout(function () {
            setInputSeparator('zamba_index_109');
            setInputSeparator('zamba_index_11535222');
            setInputSeparator('zamba_index_11535299');
            setInputSeparator('zamba_index_11535300');
            setInputSeparator('zamba_index_11535301');
        }, 700);
    } catch (e) {

    }
}

function noPunto(event) {

    var e = event || window.event;
    var key = e.keyCode || e.which;

    if (key === 110 || key === 190 ) {
        swal("","Ingrese los decimales con coma","info")
        e.preventDefault();
    }
}

function setRule() {
    $("#zamba_rule_11550590").click();
}


function ValidateConcepto() {
    let Concepto = document.querySelector("#zamba_index_50").value;
    let TipoGasto = document.querySelector("#zamba_index_110").value;


    // habilito orden de compra
    if (Concepto != "") {
        if (TipoGasto == 100) {
            document.querySelector("#zamba_index_2725").disabled = false;
        } else {
            document.querySelector("#zamba_index_2725").disabled = true;
        }

    } else {
        document.querySelector("#zamba_index_2725").disabled = true;
    }

}


function CheckImporteMoneda() {

    var moneda = $("#zamba_index_92").val();
    var importePesos = $("#zamba_index_109").val();
    var tipoCambio = $("#zamba_index_11535223").val();
    var importeOtraMoneda = $("#zamba_index_11535222").val();

    if (moneda == undefined || moneda == null || moneda == '') {
        $("#zamba_index_11535223").attr('readonly', 'readonly');
        $("#zamba_index_11535222").attr('readonly', 'readonly');
        $("#zamba_index_109").attr('readonly', 'readonly');
        $("#DivImporteOtraMoneda1").hide();
        $("#DivImporteOtraMoneda2").hide();
    }
    else if (moneda != "PES") {
        $("#DivImporteOtraMoneda1").show();
        $("#DivImporteOtraMoneda2").show();

        $("#zamba_index_11535223").removeAttr('readonly', 'readonly');
        $("#zamba_index_11535222").removeAttr('readonly', 'readonly');
        //   $("#zamba_index_109").val('');
        //$("#zamba_index_11535223").attr('display', 'block');
        //$("#zamba_index_11535222").attr('display', 'block');

        //Desabilitamos Importe en Pesos
        $("#zamba_index_109").attr('readonly', 'readonly');
        $("#zamba_index_109").attr('readonly', 'readonly');

        if (moneda != undefined && moneda != null && moneda != '') {
            if (importeOtraMoneda != undefined && importeOtraMoneda != null && importeOtraMoneda != '') {




                var scope_taskController = angular.element($("#taskController")).scope();
                scope_taskController.zRule(11550412, [{ 'zamba_index_11535222': 'importe' }, { 'zamba_index_92': 'moneda' }], [{ 'zamba_index_109': 'importepesos' }, { 'zamba_index_11535223': 'tasa' }]);

            }
        }
        else {
            if (importeOtraMoneda != undefined && importeOtraMoneda != null && importeOtraMoneda != '') {
                $("#zamba_index_109").val("0");
                $("#importeError").attr("inneHtml", "Tipo de Cambio obligatorio");
            }
            else {
                $("#zamba_index_11535222").val((importePesos));
                $("#importeError").attr("inneHtml", "Tipo de Cambio obligatorio");
            }
        }
    }
    else {
        $("#DivImporteOtraMoneda1").hide();
        $("#DivImporteOtraMoneda2").hide();

        $("#zamba_index_11535223").val('');
        $("#zamba_index_11535222").val('');
        $("#zamba_index_11535223").attr('readonly', 'readonly');
        $("#zamba_index_11535222").attr('readonly', 'readonly');
        $("#zamba_index_109").removeAttr("readonly");
        $("#zamba_index_109").removeAttr("readonly");
    }
    setMultipleSeparatorForm();
};

function CheckMiPyme() {
    if ($('#zamba_index_11525201').prop('checked')) {
        $('#MiPymeArea').css('display', 'block');


    } else {
        $('#MiPymeArea').css('display', 'none');

    }
};

function validateImportePesos() {

    var moneda = $("#zamba_index_92").val();
    var importeOtraMoneda = $("#zamba_index_11535222").val();

    if (moneda != undefined && moneda != null && moneda != '') {
        if (importeOtraMoneda == '') {
            $("#zamba_index_109").val("0");
            $("#importeError").attr("inneHtml", "Importe en otra moneda obligatorio");
            swal('Datos requeridos', 'Importe es un campo obligatorio', 'warning');
        }
    }
};


function RemoveNonFacItems() {
    console.log('Removing Items NON Fact');

    $('select[name="zamba_index_110"]').find('option[value=300]').remove();
    $('select[name="zamba_index_110"]').find('option[value=400]').remove();
    $('select[name="zamba_index_110"]').find('option[value=500]').remove();
    $('select[name="zamba_index_110"]').find('option[value=600]').remove();

};


function ValidateRequieredFields(sender) {
    var concepto = $("#zamba_index_2725").val();
    var gasto = $("#zamba_index_110").val();
    //var oc = $("#zamba_index_50").val();

    if (concepto != undefined && concepto != '' && gasto != undefined && gasto != '') {
        //if (gasto == 100 && (oc == undefined || oc == '')) {
            //swal('Datos requeridos', 'Tenes que completar el Nro de Orden de Compra', 'warning');
            //return false;
        //}

    }
    else {
        swal('Datos requeridos', 'Tenes que seleccionar el Tipo de Gasto y Concepto', 'warning');
        return false;
    }

};

function SelectTipoGasto() {

    let TipoGasto = document.querySelector("#zamba_index_110").value;
    let Concepto = document.querySelector("#zamba_index_2725").value;

    if (TipoGasto == 100) {
        if (Concepto != "") {
            document.querySelector("#zamba_index_50").disabled = false;
            document.querySelector("#zamba_index_2725").disabled = false;
            $('#AreaRecupero').show();
        } else {
            document.querySelector("#zamba_index_50").disabled = false;
            document.querySelector("#zamba_index_2725").disabled = true;
            $('#AreaRecupero').hide();

        }


    } else {
        document.querySelector("#zamba_index_50").disabled = true;
        document.querySelector("#zamba_index_2725").disabled = false;
        $('#AreaRecupero').hide();

    }

}

function zamba_save_onclick_ingresoPago(sender) {

    var valido = true;
    if (document.getElementById("zamba_index_105").value == "") {
        swal("", "Por favor, ingrese un Nombre y Apellido o Razón Social.", "error");
        try {
            $("#zamba_index_105").focus();
            $("#zamba_index_105").css("bordercolor", "red");
        } catch (e) {

        }
        valido = false;
    } else if (document.getElementById("zamba_index_110").value == "") {
        swal("", "Por favor, ingrese un Tipo de Gasto.", "error");
        valido = false;
    } else if (document.getElementById("zamba_index_2725").value == "0") {
        swal("", "Por favor, ingrese un Concepto.", "error");
        valido = false;
    } else if (document.getElementById("zamba_index_82").value == "") {
        swal("", "Por favor, ingrese una delegación.", "error");
        valido = false;
    } else if (document.getElementById("zamba_index_126").value == "") {
        swal("", "Por favor, ingrese el Sector.", "error");
        valido = false;
    } else if (document.getElementById("zamba_index_53", "error").value == "") {
        swal("", "Por favor, ingrese el  Solicitante", "error");
        valido = false;
    } else if (document.getElementById("zamba_index_92").value == "") {
        swal("", "Por favor, ingrese Tipo de Moneda", "error");
        valido = false;
    } else if (document.getElementById("zamba_index_109").value == "") {
        swal("", "Por favor, ingrese Importe", "error");
        valido = false;
	} else if (document.getElementById("zamba_index_11535296").value == "") {
        swal("", "Por favor, ingrese Importe Total", "error");
        valido = false;
    } else if (document.getElementById("zamba_index_86").value == "") {
        swal("", "Por favor, ingrese el  ID trámite", "error");
        valido = false;
    } else if (document.getElementById("zamba_index_73").value == "") {
        swal("", "Por favor, ingrese Fecha Comprobante", "error");
        valido = false;
    } else if (document.getElementById("zamba_index_11525202").value == "") {
        swal("", "Por favor, ingrese Fecha Vencimiento", "error");
        valido = false;
    } else if (document.getElementById("zamba_index_4").value == "") {
        swal("", "Por favor, ingrese el  Recepcion", "error");
        valido = false;
    } else if (document.getElementById("zamba_index_1020169").value == "") {
        swal("", "Por favor, ingrese Metodos de Pago", "error");
        valido = false;
    } else if (ValidationExtFctura() == false) {
        swal("", "El campo ingrese factura debe ser SI", "error");
        valido = false;
    } else if (ValidateTipoComprobante() == false) {
        swal("", "Por favor, ingrese tipo de Comprobante", "error");
        valido = false;
    } else if (ValidateTipoLetra() == false) {
        swal("", "Por favor, ingrese tipo de Letra", "error");
        valido = false;
    } else if (ValidatePuntoVenta() == false) {
        swal("", "Por favor, ingrese el punto de venta", "error");
        valido = false;
    } else if (ValidateNroComprobante() == false) {
        swal("", "Por favor, ingrese Nro Comprobante", "error");
        valido = false;
    } else if (ValidateFechaCAI() == false) {
        swal("", "Por favor, ingrese Fecha CAI", "error");
        valido = false;
    } else if (ValidateOrdenComrpra() == false) {
        swal("", "Por favor, ingrese Orden de Compra", "error");
        valido = false;
    } else if (ValidarTasadecambioPago() == false) {
        // swal("", "Por favor, ingrese MONEDA, IMPORTE Y TASA DE CAMBIO", "error");
        valido = false;
    } else {
        SetRuleId(sender);
        valido = true;
    }
    return valido;

};

function ValidarTasadecambioPago() {

    var validoPago = false;
    if ($("#zamba_index_92").val() == "EUR" || $("#zamba_index_92").val() == "USD") {

        if ($("#zamba_index_11535222").val() == "") {
            validoPago = false;
            swal("error", "Por favor, ingrese Importe", "error");
            return validoPago;
        } else if ($("#zamba_index_11535223").val() != "") {
            validoPago = true;

        } else {
            validoPago = false;
            swal("error", "Por favor, ingrese Tipo de Cambio", "error");
            return validoPago;
        }


        if (validoPago == true && $("#zamba_index_11535223").val() == "") {
            validoPago = false;
            swal("error", "Por favor, ingrese Tasa de Cambio", "error");
            return validoPago;
        } else if ($("#zamba_index_11535222").val() != "") {
            validoPago = true;
        } else {
            validoPago = false;
            swal("error", "Por favor, ingrese Importe", "error");
            return validoPago;
        }

        //Valido Fecha Comprobante No este vacia
        if ($('#zamba_index_73').val() == '') {
            swal("error", "Fecha Comprobante No puede estar vacia", "error");

            validoPago = false;
            return validoPago;

        }
        else {
            validoPago = true;

        }

        //Valido Fecha vewncimiento No este vacia
        if ($('#zamba_index_74').val() == '') {
            swal("error", "Fecha Vto CAI No puede estar vacia", "error");

            validoPago = false;
            return validoPago;

        }
        else {
            validoPago = true;

        }



    } else {
        validoPago = true;

    }

    return validoPago;

}

function ValidarEndidad() {
    if ($('#zamba_index_1220191').val() == '26') {
        $('#AreaObservacionFactura').show();
        $('#AreaObservacionPago').hide();
        
    } else {
        $('#AreaObservacionFactura').hide();
        $('#AreaObservacionPago').show();
    }
}