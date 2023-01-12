
// Este archivo juntos al zamba.js function addCalendar unifican los calendarios de la aplicacion

function addcalendar() {


    $('.datepicker').datepicker({
        changeMonth: true,
        changeYear: true,
        showOn: 'focus',
        dateFormat: "dd/mm/yyyy",
        duration: "",
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        yearRange: "-150:+100",
        onClose: function () {
            //Restauramos el width original
            $("#DivIndices").animate({ width: previousWidth }, "fast");
            $("#separator").animate({ width: previousWidth }, "fast");
            //Acomodamos el documento a lo restaurado.
            var a = $(window).width() - previousWidth - 5;
            $("#separator").next().animate({ width: a }, "fast");
        },
        beforeShow: function (input, inst) {
            //Guardamos el width anterior.  
            previousWidth = $("#DivIndices").width();
            //Saco el ancho de los labels
            var labelWidth = $(input).parent().prev().width();
            var calcultateWidth = $(inst.dpDiv).width() + labelWidth + 20;
            $("#DivIndices").animate({ width: calcultateWidth }, "slow");
            $("#separator").animate({ width: calcultateWidth }, "slow");
            //Acomodamos el documento, con la diferencia de pantalla.
            var a = $(window).width() - calcultateWidth - 5;
            $("#separator").next().animate({ width: a }, "slow");
        }
    });

 
  

}



function addCalendarSearch() {
    $(".BusquedaCalendar").datepicker({
        changeMonth: true,
        changeYear: true,
        format: "mm-yyyy",
        viewMode: "months",
        minViewMode: "months",
        yearRange: "-150:+100",
    });


    
}


function AutoCompleteClendarOff() {
    $('.BusquedaCalendar').each(function (index) {
        $($('.BusquedaCalendar')[index]).attr('autocomplete', 'off');
    });

    $('.datepicker').each(function (index) {
        $($('.datepicker')[index]).attr('autocomplete', 'off');
    });
}
