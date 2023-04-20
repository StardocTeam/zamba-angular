function onTestChange(sender) {
    var key = window.event.keyCode;

    if (key === 13) {
        sender.value = sender.value + "\n";
        return false;

    }
    else {
        return true;
    }
};

function SumarCamposCapital() {
    try {

        var incapacidadfisica = 0;
        var incapacidadpsicologica = 0;
        var dañomoral = 0;
        var dañomaterial = 0;
        var intereses = 0;
        var totalcapital = 0;
        var costas = 0;
        var otrosgastos = 0;
        var total = 0;

        ///suma Total Capital
        if ($("#zamba_index_2860").val() != '' && parseFloat($("#zamba_index_2860").val().replace('', '0')) > 0)
            incapacidadfisica = parseFloat($("#zamba_index_2860").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

        if ($("#zamba_index_2865").val() != '' && parseFloat($("#zamba_index_2865").val().replace('', '0')) > 0)
            incapacidadpsicologica = parseFloat($("#zamba_index_2865").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

        if ($("#zamba_index_2791").val() != '' && parseFloat($("#zamba_index_2791").val().replace('', '0')) > 0)
            dañomoral = parseFloat($("#zamba_index_2791").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

        if ($("#zamba_index_2792").val() != '' && parseFloat($("#zamba_index_2792").val().replace('', '0')) > 0)
            dañomaterial = parseFloat($("#zamba_index_2792").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

        if ($("#zamba_index_2793").val() != '' && parseFloat($("#zamba_index_2793").val().replace('', '0')) > 0)
            intereses = parseFloat($("#zamba_index_2793").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));
        
        if ($("#zamba_index_2818").val() != '' && parseFloat($("#zamba_index_2818").val().replace('', '0')) > 0)
            otrosgastos = parseFloat($("#zamba_index_2818").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

        totalcapital = Redondear(incapacidadfisica + incapacidadpsicologica + dañomoral + dañomaterial + intereses + otrosgastos);

          $("#zamba_index_11535249").attr('disabled', 'disabled');

            //$("#zamba_index_2719").val(total.toFixed(2).replaceAll('.', ','));
            //parseFloat($("#zamba_index_2719").val(total.toFixed(2).replace('.', ',').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ".")).replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

            $("#zamba_index_11535249").val(totalcapital.toString().replace('.', ',').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, "."));


        


        ///Suma TOTAL
        
        if ($("#zamba_index_10223").val() != '' && parseFloat($("#zamba_index_10223").val().replace('', '0')) > 0)
            costas = parseFloat($("#zamba_index_10223").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

        total = Redondear(totalcapital + costas);

       
            $("#zamba_index_2719").attr('disabled', 'disabled');

            //$("#zamba_index_2719").val(total.toFixed(2).replaceAll('.', ','));
            //parseFloat($("#zamba_index_2719").val(total.toFixed(2).replace('.', ',').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ".")).replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

            $("#zamba_index_2719").val(total.toString().replace('.', ',').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, "."));
        
    }
    catch (e) {
        console.log(e);
    }

};

function SumarReserva() {
    try {

        var PropuestaActual = 0;
        var PagosRealizados = 0;
        var TotalSobreReservado = 0;
        
        ///suma Total Capital
        if ($("#zamba_index_10310").val() != '' && parseFloat($("#zamba_index_10310").val().replace('', '0')) > 0)
        PropuestaActual = parseFloat($("#zamba_index_10310").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

        if ($("#zamba_index_10313").val() != '' && parseFloat($("#zamba_index_10313").val().replace('', '0')) > 0)
        PagosRealizados = parseFloat($("#zamba_index_10313").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

        TotalSobreReservado = Redondear(PropuestaActual + PagosRealizados);

        //$("#zamba_index_2719").val(total.toFixed(2).replaceAll('.', ','));
        //parseFloat($("#zamba_index_2719").val(total.toFixed(2).replace('.', ',').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ".")).replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

         $("#zamba_index_10312").val(TotalSobreReservado.toString().replace('.', ',').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, "."));

        
    }
    catch (e) {
        console.log(e);
    }

};

function SumarCamposRecupero() {
    try {

        var totalrecupero = 0;
        var totalcostas = 0;
        var costas = 0;

        ///Suma TOTAL Recupero
        if ($("#zamba_index_11535250").val() != '' && parseFloat($("#zamba_index_11535250").val().replace('', '0')) > 0)
            totalrecupero = parseFloat($("#zamba_index_11535250").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

        total = Redondear(totalrecupero);

        if (total >= 0) {
            $("#zamba_index_11535249").attr('disabled', 'disabled');

            //$("#zamba_index_2719").val(total.toFixed(2).replaceAll('.', ','));
            //parseFloat($("#zamba_index_2719").val(total.toFixed(2).replace('.', ',').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ".")).replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

            //$("#zamba_index_11535249").val(total.toString().replace('.', ',').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, "."));
        };

        ///Suma TOTAL
        if ($("#zamba_index_11535249").val() != '' && parseFloat($("#zamba_index_11535249").val().replace('', '0')) > 0)
            totalcostas = parseFloat($("#zamba_index_11535249").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

        if ($("#zamba_index_10223").val() != '' && parseFloat($("#zamba_index_10223").val().replace('', '0')) > 0)
            costas = parseFloat($("#zamba_index_10223").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

        total = Redondear(totalcostas + costas);

        if (total >= 0) {
            $("#zamba_index_2719").attr('disabled', 'disabled');

            //$("#zamba_index_2719").val(total.toFixed(2).replaceAll('.', ','));
            //parseFloat($("#zamba_index_2719").val(total.toFixed(2).replace('.', ',').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ".")).replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

            $("#zamba_index_2719").val(total.toString().replace('.', ',').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, "."));
        };

    }
    catch (e) {
        console.log(e);
    }
};



function SumarCostoSiniestro() {
    try {
        var pagosrealizados = 0;
        var propuestaactual = 0;
        var totalsiniestro = 0;

        ///Sumar COSTO Siniestro
        if ($("#zamba_index_10310").val() != '' && parseFloat($("#zamba_index_10310").val().replace('', '0')) > 0)
        propuestaactual = parseFloat($("#zamba_index_10310").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

        if ($("#zamba_index_10313").val() != '' && parseFloat($("#zamba_index_10313").val().replace('', '0')) > 0)
            pagosrealizados = parseFloat($("#zamba_index_10313").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

        totalsiniestro = Redondear(pagosrealizados + propuestaactual);

            //$("#zamba_index_2719").val(total.toFixed(2).replaceAll('.', ','));
            //parseFloat($("#zamba_index_2719").val(total.toFixed(2).replace('.', ',').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ".")).replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

            $("#zamba_index_10312").val(total.toString().replace('.', ',').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, "."));
        

    }
    catch (e) {
        console.log(e);
    }
};



function SumarCostas() {
    try {

        ///Suma TOTAL
        // if ($("#zamba_index_11535249").val() != '' && parseFloat($("#zamba_index_11535249").val().replace('', '0')) > 0)
        //     totalcostas = parseFloat($("#zamba_index_11535249").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

        // if ($("#zamba_index_10223").val() != '' && parseFloat($("#zamba_index_10223").val().replace('', '0')) > 0)
        //     costas = parseFloat($("#zamba_index_10223").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

        // total = Redondear(totalcostas + costas);

        // if (total > 0) {
        //     $("#zamba_index_2719").attr('disabled', 'disabled');

        //     //$("#zamba_index_2719").val(total.toFixed(2).replaceAll('.', ','));
        //     //parseFloat($("#zamba_index_2719").val(total.toFixed(2).replace('.', ',').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ".")).replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

        //     $("#zamba_index_2719").val(total.toString().replace('.', ',').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, "."));
        // };

    }
    catch (e) {
        console.log(e);
    }
};




function SumarMontoDemanda() {
    try {
        var valorvida = 0;
        var danomoralvida = 0;
        var otros = 0;
        var totalvida = 0;

        ///Suma TOTAL
        if ($("#zamba_index_2789").val() != '' && parseFloat($("#zamba_index_2789").val().replace('', '0')) > 0)
             valorvida = parseFloat($("#zamba_index_2789").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

        if ($("#zamba_index_10307").val() != '' && parseFloat($("#zamba_index_10307").val().replace('', '0')) > 0)
             danomoralvida = parseFloat($("#zamba_index_10307").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));
        
        if ($("#zamba_index_2745").val() != '' && parseFloat($("#zamba_index_2745").val().replace('', '0')) > 0)
             otros = parseFloat($("#zamba_index_2745").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

         totalvida = Redondear(valorvida + danomoralvida + otros);

           $("#zamba_index_10308").attr('disabled', 'disabled');

             //$("#zamba_index_2719").val(total.toFixed(2).replaceAll('.', ','));
             //parseFloat($("#zamba_index_2719").val(total.toFixed(2).replace('.', ',').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ".")).replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

           $("#zamba_index_10308").val(totalvida.toString().replace('.', ',').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, "."));
            

    }
    catch (e) {
        console.log(e);
    }
};



$(document).ready(function () {
    $(".ZDecimal").each(function (index, elem) {
        var event = new Event('ready');
        Object.defineProperty(event, 'target', {writable: false, value: elem});
        render_Importe(event);
    });

    SumarCostoSiniestro();
    ValidarCamposEnTipoDocumentacion();

    if ($('#zamba_index_1001014').prop('checked') == false) {
        SumarCamposCapital();
        SumarCostas();
        SumarMontoDemanda();
    }
    else {
        SumarCamposRecupero();
        SumarCostas();
        SumarMontoDemanda();
    }
    $('#zamba_index_1001014').on('change', ValidarCamposEnTipoDocumentacion);
    $('#zamba_index_11535258').on('change', SumarReserva);

    $('#zamba_index_2860').on('change', SumarCamposCapital);
    $('#zamba_index_2865').on('change', SumarCamposCapital);
    $('#zamba_index_2791').on('change', SumarCamposCapital);
    $('#zamba_index_2792').on('change', SumarCamposCapital);
    $('#zamba_index_2793').on('change', SumarCamposCapital);
    $('#zamba_index_2818').on('change', SumarCamposCapital);


    $('#zamba_index_11535250').on('change', SumarCamposRecupero);

    $('#zamba_index_10223').on('change', SumarCamposRecupero);
    $('#zamba_index_11535249').on('change', SumarCamposRecupero);

    $('#zamba_index_2789').on('change', SumarMontoDemanda);
    $('#zamba_index_10307').on('change', SumarMontoDemanda);
    $('#zamba_index_2745').on('change', SumarMontoDemanda);

    $('#zamba_index_10310').on('change', SumarCostoSiniestro);
    $('#zamba_index_10313').on('change', SumarCostoSiniestro);
    $('#zamba_index_10312').on('change', SumarCostoSiniestro);
  

});


function ValidarCamposEnTipoDocumentacion() {
    if ($('#zamba_index_1001014').prop('checked') == false) {
        $('#ResumenArea').show();
        $('#AreaRecupero').hide();
        SumarCamposCapital();

    } else {
        $('#ResumenArea').hide();
        $('#AreaRecupero').show();
        SumarCamposRecupero();
    }



};

function ValidarSobreReserva() {
    if ($('#zamba_index_11535258').prop('checked') == false) {
        $('#ResumenArea').show();
        $('#AreaRecupero').hide();
        SumarCamposCapital();

    } else {
        $('#ResumenArea').hide();
        $('#AreaRecupero').show();
        SumarCamposRecupero();
    }



};