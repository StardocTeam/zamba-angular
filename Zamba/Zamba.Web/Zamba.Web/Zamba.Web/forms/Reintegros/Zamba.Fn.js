
//Deshabilita navegar hacia atras backspace
(function ($, global) {
    //http://stackoverflow.com/questions/12381563/how-to-stop-browser-back-button-using-javascript
    var _hash = "!",
    noBackPlease = function () {
        global.location.href += "#";

        setTimeout(function () {
            global.location.href += "!";
        }, 50);
    };

    global.setInterval(function () {
        if (global.location.hash != _hash) {
            global.location.hash = _hash;
        }
    }, 100);

    global.onload = function () {
        noBackPlease();

        // disables backspace on page except on input fields and textarea..
        $(document.body).keydown(function (e) {
            var elm = e.target.nodeName.toLowerCase();
            if (e.which == 8 && elm !== 'input' && elm !== 'textarea') {
                e.preventDefault();
            }
            // stopping event bubbling up the DOM tree..
            e.stopPropagation();
        });
    }

})(jQuery, window);

//Date picker en español
$.datepicker.regional['es'] = {
    closeText: 'Cerrar',
    prevText: '<Ant',
    nextText: 'Sig>',
    currentText: 'Hoy',
    monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
    monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
    dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
    dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
    dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
    weekHeader: 'Sm',
    dateFormat: 'dd/mm/yy',
    firstDay: 1,
    isRTL: false,
    showMonthAfterYear: false,
    yearSuffix: ''
};
$.datepicker.setDefaults($.datepicker.regional['es']);

//Ancho de ventana
function windowWidth() {
    var myWidth = 0;
    if (typeof (window.innerWidth) == 'number') {
        myWidth = window.innerWidth;
    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        myWidth = document.documentElement.clientWidth;
    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        myWidth = document.body.clientWidth;
    }
    return myWidth;
}

//Recarga bootstrap en head HTML
function reloadBootstrap() {
    var docURL = null;
    if (parent.document.config == undefined) {
        docURL = parent.document.URL
    }
    else {
        docURL = document.config == undefined ? parent.document.config.urlBase : document.config.urlBase;
    }
    $('head').append("<script src='" + docURL + "/scripts/bootstrap.min.js' type='text/javascript'></script>");

}

//Parsea XML a Json
function xml2json(xml) {
    try {
        var obj = {};
        //En caso de IE
        if (xml.children == undefined) return xml.childNodes[0].firstChild.data;
        if (xml.children.length > 0) {
            for (var i = 0; i < xml.children.length; i++) {
                var item = xml.children.item(i);
                var nodeName = item.nodeName;

                if (typeof (obj[nodeName]) == "undefined") {
                    obj[nodeName] = xml2json(item);
                } else {
                    if (typeof (obj[nodeName].push) == "undefined") {
                        var old = obj[nodeName];

                        obj[nodeName] = [];
                        obj[nodeName].push(old);
                    }
                    obj[nodeName].push(xml2json(item));
                }
            }
        } else {
            obj = xml.textContent;
        }
        return typeof (obj) == "object" ? obj.string : obj;
    } catch (e) {
        console.log(e.message);
    }
}
