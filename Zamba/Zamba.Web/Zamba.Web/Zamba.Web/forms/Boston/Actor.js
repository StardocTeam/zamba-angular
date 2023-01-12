var LimitToSlst = 30;
function showList(indexId, filter, showMore) {




    var genericRequest = {
            IndexId: indexId,
            Value: filter,
            LimitTo: LimitToSlst
    };
    var result = false;



    $.ajax({
        type: "post",
        url: ZambaWebRestApiURL + '/search/ListOptions',
        data: JSON.stringify(genericRequest),
        contentType: "application/json; charset=utf-8",
        async: false,
        success:
            function (data, status, headers, config) {
                result = data;
                const tabla = '<div>'
            }
    });








    //$.post(ZambaWebRestApiURL + '/search/ListOptions', JSON.stringify({
    //    IndexId: indexId,
    //    Value: filter,
    //    LimitTo: LimitToSlst
    //}))
    //    .done(function (response) {
    //        var results = JSON.parse(response.data);
    //    });
}

function zamba_save_onclick_Actor(sender) {
    if (document.getElementById("zamba_index_2706").value.trim() == "") {
        swal("Verifique que el Nombre se encuentre completo.");
        document.getElementById("hdnRuleId").name = sender.id;
        CloseLoading();
        event.preventDefault();
    } else if (document.getElementById("zamba_index_116").value > 100) {
        swal("El porcentaje no puede ser superior al 100%.");
        document.getElementById("hdnRuleId").name = sender.id;
        CloseLoading();
        event.preventDefault();
    } else {
        swal("Se guardó de manera exitosa.");
        document.getElementById("hdnRuleId").name = sender.id;
        frmMain.submit();
    }
}

function CloseLoading() {
    setTimeout("parent.hideLoading();", 500);
}


/****** Suma de Rubros *************/

function SumarCampos() {

    var value1 = 0;
    var value2 = 0;
    var value3 = 0;
    var value4 = 0;
    var value5 = 0;


    if ($("#zamba_index_2790").val() != '' && parseFloat($("#zamba_index_2790").val().replace('.', '').replace(',', '.')) > 0)
        value1 = parseFloat($("#zamba_index_2790").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

    if ($("#zamba_index_2792").val() != '' && parseFloat($("#zamba_index_2792").val().replace('.', '').replace(',', '.')) > 0)
        value2 = parseFloat($("#zamba_index_2792").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

    if ($("#zamba_index_2789").val() != '' && parseFloat($("#zamba_index_2789").val().replace('.', '').replace(',', '.')) > 0)
        value3 = parseFloat($("#zamba_index_2789").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

    if ($("#zamba_index_2791").val() != '' && parseFloat($("#zamba_index_2791").val().replace('.', '').replace(',', '.')) > 0)
        value4 = parseFloat($("#zamba_index_2791").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

    if ($("#zamba_index_114").val() != '' && parseFloat($("#zamba_index_114").val().replace('.', '').replace(',', '.')) > 0)
        value5 = parseFloat($("#zamba_index_114").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));


    result = Redondear(value1 + value2 + value3 + value4 + value5);

    $("#zamba_index_115").val(result.toString().replace('.', ',').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, "."));


}

function setNumericos() {
    if ($('#zamba_index_2790').val() == "" || $('#zamba_index_2790').val() == null) {
        $('#zamba_index_2790').val(0.00);
    }
    if ($('#zamba_index_2792').val() == "" || $('#zamba_index_2792').val() == null) {
        $('#zamba_index_2792').val(0.00);
    }
    if ($('#zamba_index_2789').val() == "" || $('#zamba_index_2789').val() == null) {
        $('#zamba_index_2789').val(0.00);
    }
    if ($('#zamba_index_2791').val() == "" || $('#zamba_index_2791').val() == null) {
        $('#zamba_index_2791').val(0.00);
    }
    if ($('#zamba_index_114').val() == "" || $('#zamba_index_114').val() == null) {
        $('#zamba_index_114').val(0.00);
    }
    if ($('#zamba_index_117').val() == "" || $('#zamba_index_117').val() == null) {
        $('#zamba_index_117').val(0.00);
    }
}

function LimpiarCamposRubros() {
    document.getElementById("zamba_index_2790").value = 0.00;
    document.getElementById("zamba_index_2792").value = 0.00;
    document.getElementById("zamba_index_2789").value = 0.00;
    document.getElementById("zamba_index_2791").value = 0.00;
    document.getElementById("zamba_index_114").value = 0.00;
    document.getElementById("zamba_index_115").value = 0.00;
}

function OcultarCamposFrom() {

    if ($('#zamba_index_113').val() == 'Porcentual') {
        $('.porcentaje').css('display', 'table');
        $('.montoReclamado').css('display', 'table');
         $('.rubro').hide();
        LimpiarCamposRubros();
    } else if ($('#zamba_index_113').val() == 'Rubro') {
        $('.rubro').css('display', 'table');
        $('.porcentaje').hide();
        $('.montoReclamado').hide();
        document.getElementById("zamba_index_116").value = "";
        document.getElementById("zamba_index_117").value = "";
    } else {
        $('.porcentaje').hide();
        $('.montoReclamado').hide();
        $('.rubro').hide();
        LimpiarCamposRubros();
    }
}

function formatearNumeros() {
    document.getElementById('zamba_index_2790').value = parseFloat(document.getElementById('zamba_index_2790').value);
    document.getElementById('zamba_index_2792').value = parseFloat(document.getElementById('zamba_index_2792').value);
    document.getElementById('zamba_index_2789').value = parseFloat(document.getElementById('zamba_index_2789').value);
    document.getElementById('zamba_index_2791').value = parseFloat(document.getElementById('zamba_index_2791').value);
    document.getElementById('zamba_index_114').value = parseFloat(document.getElementById('zamba_index_114').value);
    document.getElementById('zamba_index_117').value = parseFloat(document.getElementById('zamba_index_117').value);
}

/**********************************/


function FormLoad() {
    setNumericos();
    if ($('#zamba_index_113').val() == 'Porcentual') {
        $('.tblPorcentaje').css('display', 'block');
        $('.tblRubros').css('display', 'none');
    }

    if ($('#zamba_index_113').val() == 'Rubro') {
        $('.tblRubros').css('display', 'block');
        $('.tblPorcentaje').css('display', 'none');
    }
    CalcularMonto(document.getElementById('zamba_index_116'));
}

function SeleccionarProvincia() {
    /* $('#zamba_index_2766').text('');*/
}

function OcultarCampos() {
    if ($('#zamba_index_113').val() == 'Porcentual') {
        $('.tblPorcentaje').css('display', 'block');
        $('.tblRubros').css('display', 'none');
        LimpiarCamposRubros();
    } else if ($('#zamba_index_113').val() == 'Rubro') {
        $('.tblRubros').css('display', 'block');
        $('.tblPorcentaje').css('display', 'none');
        document.getElementById("zamba_index_116").value = "";
        document.getElementById("zamba_index_117").value = "";
    } else {
    }
}

function CalcularMonto(porcentaje) {
    if (porcentaje.value == '')
        return;
    if (parseInt(porcentaje.value) <= 100) {
        var porc = 0.00;
        if (porcentaje.value != '') {
            porc = parseFloat((parseInt(porcentaje.value) * parseInt($('#zamba_index_2691').val()) / 100));
            porc = Redondear(porc);
        }
        /*   $('#zamba_index_117').val(porc);*/

        $("#zamba_index_117").val(porc.toString().replace('.', ',').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, "."));
    } else { swal("El porcentaje no puede ser superior al 100%."); }
}

$(document).ready(function () {
    OcultarCamposFrom();
    SumarCampos();
    FormLoad();
    $(".solonums").each(function () {
        $(this).keypress(function (e) {
            return IntegerCheck(e);
        })
    });
    $(".solodecimals").each(function () {
        $(this).keypress(function (e) {
            if (e.which != 8 && e.which != 0 && e.which != 46 && (e.which < 48 || e.which > 57)) {
                return false;
            }
        })
    });
});

function validarEmail(email) {
    email = email.value;
    if (email != '' && email != null) {
        expr = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!expr.test(email)) alert("Error: La dirección de correo " + email + " es incorrecta.");
    }
}

function LimpiarCamposRubros() {
    document.getElementById("zamba_index_2790").value = 0.00;
    document.getElementById("zamba_index_2792").value = 0.00;
    document.getElementById("zamba_index_2789").value = 0.00;
    document.getElementById("zamba_index_2791").value = 0.00;
    document.getElementById("zamba_index_115").value = 0.00;
}

//window.onload = function () {
//    setTimeout(function () { FormLoad(); }, 200);
//};