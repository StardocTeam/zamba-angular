$(document).ready(function () {

    //Establece Puntos millares y coma a los importes traidos del origen.
   // setInputSeparator('zamba_index_109');

    //Renderizacion de valor numerico a importe.
    $("#zamba_index_109").on({
        "focusout": render_Importe,
    });

    CheckImporteMoneda();

    $("#zamba_index_92").on({
        "change": CheckImporteMoneda,
    });

    $("#zamba_index_11535223").on({
        "focusout": CheckImporteMoneda,
    });
    $("#zamba_index_11535222").on({
        "focusout": CheckImporteMoneda,
    });
});


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

        if (tipoCambio != undefined && tipoCambio != null && tipoCambio != '') {
            if (importeOtraMoneda != undefined && importeOtraMoneda != null && importeOtraMoneda != '') {
                importePesos = parseFloat(importeOtraMoneda.replace('.', '').replace(',', '.'), 2) * parseFloat(tipoCambio.replace(',', '.'), 2);
                $("#zamba_index_109").val(new Intl.NumberFormat("es-AR", { style: "decimal", minimumFractionDigits: 2 }).format(importePesos));
            }
            else {
                $("#zamba_index_109").val("0");
                $("#importeError").attr("inneHtml", "Importe en otra moneda obligatorio");
            }
        }
        else {
            if (importeOtraMoneda != undefined && importeOtraMoneda != null && importeOtraMoneda != '') {
            $("#zamba_index_109").val("0");
            $("#importeError").attr("inneHtml", "Tipo de Cambio obligatorio");
             }
             else
            {
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
    var oc = $("#zamba_index_50").val();

    if (concepto != undefined && concepto != '' && gasto != undefined && gasto != '') {
        if (gasto == 100 && (oc == undefined || oc == '')) {
            swal('Datos requeridos', 'Tenes que completar el Nro de Orden de Compra', 'warning');
			return false;
        }
	
    }
    else {
        swal('Datos requeridos', 'Tenes que seleccionar el Tipo de Gasto y Concepto', 'warning');
   return false;
   }
   			
};

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
                        //} else if (document.getElementById("zamba_index_2725").value == "") {
                        //    swal("", "Por favor, ingrese un Concepto.", "error");
                        //    valido = false;
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
                    } else {
                        SetRuleId(sender);
						valido = true;
                    }
                    return valido;
                };