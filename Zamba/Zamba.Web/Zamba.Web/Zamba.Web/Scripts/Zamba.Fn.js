//Deshabilita navegar hacia atras backspace 
history.pushState(null, document.title, location.href);
//window.addEventListener('popstate', function (event) {
//    //http://stackoverflow.com/questions/12381563/how-to-stop-browser-back-button-using-javascript
//    history.pushState(null, document.title, location.href);
//});
window.location.hash = "Zamba";
window.location.hash = "Zamba/";//again because google chrome don't insert first hash into history
//window.onhashchange = function () { window.location.hash = "Zamba"; }

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
    //if (parent.document.config == undefined) {
    //    docURL = parent.document.URL
    //}
    //else {
    //    docURL = document.config == undefined ? parent.document.config.urlBase : document.config.urlBase;
    //}
    docURL = location.origin  
    var s = document.createElement("script");
    s.type = "text/javascript";
    s.src = docURL + "/bpm/scripts/bootstrap.min.js";
    $("head").append(s);
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
        console.error(e);
    }
}
//Login timeout
$(document).ready(function () {
    if ($.fn.modal != undefined)
        $.fn.modal.prototype.constructor.Constructor.DEFAULTS.backdrop = 'static';

    $("#timeOutBtn").click(function (evt) {
        var data = {};
        data.TimeOutUserText = $("#timeOutUserTxt").val()
        data.TimeOutPass = $("#timeOutPassTxt").val()
        data.ConnectionId = $("[id$=ConnectionId]").val();
        $.ajax({
            type: "POST",
            url: thisDomain + "/Services/TaskService.asmx/TimeOutLogin",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            //data: '{userTxt: "' + $("#timeOutUserTxt").val() + '",password: "' + $("#timeOutPassTxt").val() + '",connectionId: "' + '"}',
            data: "{data:" + JSON.stringify(data) + "}",
            success: function (d) {
                if (d.d == "loginok")
                    $("#openModalTimeout").modal("hide");
                else
                    toastr.error(d, "Ha ocurrido un error al verificar sus datos");
            },
            error: function (xhr, ajaxOptions, thrownError) {
                toastr.error("Por favor reintente, si el inconveniente persiste por favor actualice la página", "Ha ocurrido un error al verificar sus datos");
            }
        });
    });

    $('body').on('shown.bs.modal', '#openModalTimeout, parent.document', function (e) {
        $("#timeOutUserTxt").val($("#UsuarioDrop").text().trim());
        $("#timeOutPassTxt").val("");
    });
});

function isDate(val) {
    var d = new Date(val);
    return !isNaN(d.valueOf());
}

function esToEnDate(d) {
    var s = (d.indexOf("/")) ? "/" : "-";
    var day = d.substring(0, d.indexOf(s));
    var m = d.substring(0, d.lastIndexOf(s)).substring(day.length + 1);
    var y = d.substring(d.length - 2);
    return m + s + day + s + y;
}

// computer default date format order:
Date.ddmm = (function () {
    return Date.parse('2/6/2009') > Date.parse('6/2/2009');
})()

function GetImgIcon(iconId) {
    return iconId != null ? "Images/icons/" + iconId + ".png" : "Images/icons/" + 30 + ".png";
}

//Obtiene el navegador que esta usando
function browserType() {
    var BrowserDetect = {
        init: function () {
            this.browser = this.searchString(this.dataBrowser) || "Other";
            this.version = this.searchVersion(navigator.userAgent) || this.searchVersion(navigator.appVersion) || "Unknown";
        },
        searchString: function (data) {
            for (var i = 0; i < data.length; i++) {
                var dataString = data[i].string;
                this.versionSearchString = data[i].subString;

                if (dataString.indexOf(data[i].subString) !== -1) {
                    return data[i].identity;
                }
            }
        },
        searchVersion: function (dataString) {
            var index = dataString.indexOf(this.versionSearchString);
            if (index === -1) {
                return;
            }
            var rv = dataString.indexOf("rv:");
            if (this.versionSearchString === "Trident" && rv !== -1) {
                return parseFloat(dataString.substring(rv + 3));
            } else {
                return parseFloat(dataString.substring(index + this.versionSearchString.length + 1));
            }
        },
        dataBrowser: [
            { string: navigator.userAgent, subString: "Edge", identity: "MS Edge" },
            { string: navigator.userAgent, subString: "MSIE", identity: "Explorer" },
            { string: navigator.userAgent, subString: "Trident", identity: "Explorer" },
            { string: navigator.userAgent, subString: "Firefox", identity: "Firefox" },
            { string: navigator.userAgent, subString: "Opera", identity: "Opera" },
            { string: navigator.userAgent, subString: "OPR", identity: "Opera" },
            { string: navigator.userAgent, subString: "Chrome", identity: "Chrome" },
            { string: navigator.userAgent, subString: "Safari", identity: "Safari" }
        ]
    };
    BrowserDetect.init();
    return BrowserDetect.browser.toLowerCase();    
}

//Bootbox language
$(document).ready(function () {
    if (!isDashBoardRRHH) {
        if (typeof (parent.bootbox) != "undefined")
            parent.bootbox.setDefaults({
                locale: "es"
            });
        if (typeof (bootbox) != "undefined")
            bootbox.setDefaults({
                locale: "es"
            });
    }


});

function getLocaleShortDateString(d) {
    var f = { "ar-SA": "dd/MM/yy", "bg-BG": "dd.M.yyyy", "ca-ES": "dd/MM/yyyy", "zh-TW": "yyyy/M/d", "cs-CZ": "d.M.yyyy", "da-DK": "dd-MM-yyyy", "de-DE": "dd.MM.yyyy", "el-GR": "d/M/yyyy", "en-US": "M/d/yyyy", "fi-FI": "d.M.yyyy", "fr-FR": "dd/MM/yyyy", "he-IL": "dd/MM/yyyy", "hu-HU": "yyyy. MM. dd.", "is-IS": "d.M.yyyy", "it-IT": "dd/MM/yyyy", "ja-JP": "yyyy/MM/dd", "ko-KR": "yyyy-MM-dd", "nl-NL": "d-M-yyyy", "nb-NO": "dd.MM.yyyy", "pl-PL": "yyyy-MM-dd", "pt-BR": "d/M/yyyy", "ro-RO": "dd.MM.yyyy", "ru-RU": "dd.MM.yyyy", "hr-HR": "d.M.yyyy", "sk-SK": "d. M. yyyy", "sq-AL": "yyyy-MM-dd", "sv-SE": "yyyy-MM-dd", "th-TH": "d/M/yyyy", "tr-TR": "dd.MM.yyyy", "ur-PK": "dd/MM/yyyy", "id-ID": "dd/MM/yyyy", "uk-UA": "dd.MM.yyyy", "be-BY": "dd.MM.yyyy", "sl-SI": "d.M.yyyy", "et-EE": "d.MM.yyyy", "lv-LV": "yyyy.MM.dd.", "lt-LT": "yyyy.MM.dd", "fa-IR": "MM/dd/yyyy", "vi-VN": "dd/MM/yyyy", "hy-AM": "dd.MM.yyyy", "az-Latn-AZ": "dd.MM.yyyy", "eu-ES": "yyyy/MM/dd", "mk-MK": "dd.MM.yyyy", "af-ZA": "yyyy/MM/dd", "ka-GE": "dd.MM.yyyy", "fo-FO": "dd-MM-yyyy", "hi-IN": "dd-MM-yyyy", "ms-MY": "dd/MM/yyyy", "kk-KZ": "dd.MM.yyyy", "ky-KG": "dd.MM.yy", "sw-KE": "M/d/yyyy", "uz-Latn-UZ": "dd/MM yyyy", "tt-RU": "dd.MM.yyyy", "pa-IN": "dd-MM-yy", "gu-IN": "dd-MM-yy", "ta-IN": "dd-MM-yyyy", "te-IN": "dd-MM-yy", "kn-IN": "dd-MM-yy", "mr-IN": "dd-MM-yyyy", "sa-IN": "dd-MM-yyyy", "mn-MN": "yy.MM.dd", "gl-ES": "dd/MM/yy", "kok-IN": "dd-MM-yyyy", "syr-SY": "dd/MM/yyyy", "dv-MV": "dd/MM/yy", "ar-IQ": "dd/MM/yyyy", "zh-CN": "yyyy/M/d", "de-CH": "dd.MM.yyyy", "en-GB": "dd/MM/yyyy", "es-MX": "dd/MM/yyyy", "fr-BE": "d/MM/yyyy", "it-CH": "dd.MM.yyyy", "nl-BE": "d/MM/yyyy", "nn-NO": "dd.MM.yyyy", "pt-PT": "dd-MM-yyyy", "sr-Latn-CS": "d.M.yyyy", "sv-FI": "d.M.yyyy", "az-Cyrl-AZ": "dd.MM.yyyy", "ms-BN": "dd/MM/yyyy", "uz-Cyrl-UZ": "dd.MM.yyyy", "ar-EG": "dd/MM/yyyy", "zh-HK": "d/M/yyyy", "de-AT": "dd.MM.yyyy", "en-AU": "d/MM/yyyy", "es-ES": "dd/MM/yyyy", "fr-CA": "yyyy-MM-dd", "sr-Cyrl-CS": "d.M.yyyy", "ar-LY": "dd/MM/yyyy", "zh-SG": "d/M/yyyy", "de-LU": "dd.MM.yyyy", "en-CA": "dd/MM/yyyy", "es-GT": "dd/MM/yyyy", "fr-CH": "dd.MM.yyyy", "ar-DZ": "dd-MM-yyyy", "zh-MO": "d/M/yyyy", "de-LI": "dd.MM.yyyy", "en-NZ": "d/MM/yyyy", "es-CR": "dd/MM/yyyy", "fr-LU": "dd/MM/yyyy", "ar-MA": "dd-MM-yyyy", "en-IE": "dd/MM/yyyy", "es-PA": "MM/dd/yyyy", "fr-MC": "dd/MM/yyyy", "ar-TN": "dd-MM-yyyy", "en-ZA": "yyyy/MM/dd", "es-DO": "dd/MM/yyyy", "ar-OM": "dd/MM/yyyy", "en-JM": "dd/MM/yyyy", "es-VE": "dd/MM/yyyy", "ar-YE": "dd/MM/yyyy", "en-029": "MM/dd/yyyy", "es-CO": "dd/MM/yyyy", "ar-SY": "dd/MM/yyyy", "en-BZ": "dd/MM/yyyy", "es-PE": "dd/MM/yyyy", "ar-JO": "dd/MM/yyyy", "en-TT": "dd/MM/yyyy", "es-AR": "dd/MM/yyyy", "ar-LB": "dd/MM/yyyy", "en-ZW": "M/d/yyyy", "es-EC": "dd/MM/yyyy", "ar-KW": "dd/MM/yyyy", "en-PH": "M/d/yyyy", "es-CL": "dd-MM-yyyy", "ar-AE": "dd/MM/yyyy", "es-UY": "dd/MM/yyyy", "ar-BH": "dd/MM/yyyy", "es-PY": "dd/MM/yyyy", "ar-QA": "dd/MM/yyyy", "es-BO": "dd/MM/yyyy", "es-SV": "dd/MM/yyyy", "es-HN": "dd/MM/yyyy", "es-NI": "dd/MM/yyyy", "es-PR": "dd/MM/yyyy", "am-ET": "d/M/yyyy", "tzm-Latn-DZ": "dd-MM-yyyy", "iu-Latn-CA": "d/MM/yyyy", "sma-NO": "dd.MM.yyyy", "mn-Mong-CN": "yyyy/M/d", "gd-GB": "dd/MM/yyyy", "en-MY": "d/M/yyyy", "prs-AF": "dd/MM/yy", "bn-BD": "dd-MM-yy", "wo-SN": "dd/MM/yyyy", "rw-RW": "M/d/yyyy", "qut-GT": "dd/MM/yyyy", "sah-RU": "MM.dd.yyyy", "gsw-FR": "dd/MM/yyyy", "co-FR": "dd/MM/yyyy", "oc-FR": "dd/MM/yyyy", "mi-NZ": "dd/MM/yyyy", "ga-IE": "dd/MM/yyyy", "se-SE": "yyyy-MM-dd", "br-FR": "dd/MM/yyyy", "smn-FI": "d.M.yyyy", "moh-CA": "M/d/yyyy", "arn-CL": "dd-MM-yyyy", "ii-CN": "yyyy/M/d", "dsb-DE": "d. M. yyyy", "ig-NG": "d/M/yyyy", "kl-GL": "dd-MM-yyyy", "lb-LU": "dd/MM/yyyy", "ba-RU": "dd.MM.yy", "nso-ZA": "yyyy/MM/dd", "quz-BO": "dd/MM/yyyy", "yo-NG": "d/M/yyyy", "ha-Latn-NG": "d/M/yyyy", "fil-PH": "M/d/yyyy", "ps-AF": "dd/MM/yy", "fy-NL": "d-M-yyyy", "ne-NP": "M/d/yyyy", "se-NO": "dd.MM.yyyy", "iu-Cans-CA": "d/M/yyyy", "sr-Latn-RS": "d.M.yyyy", "si-LK": "yyyy-MM-dd", "sr-Cyrl-RS": "d.M.yyyy", "lo-LA": "dd/MM/yyyy", "km-KH": "yyyy-MM-dd", "cy-GB": "dd/MM/yyyy", "bo-CN": "yyyy/M/d", "sms-FI": "d.M.yyyy", "as-IN": "dd-MM-yyyy", "ml-IN": "dd-MM-yy", "en-IN": "dd-MM-yyyy", "or-IN": "dd-MM-yy", "bn-IN": "dd-MM-yy", "tk-TM": "dd.MM.yy", "bs-Latn-BA": "d.M.yyyy", "mt-MT": "dd/MM/yyyy", "sr-Cyrl-ME": "d.M.yyyy", "se-FI": "d.M.yyyy", "zu-ZA": "yyyy/MM/dd", "xh-ZA": "yyyy/MM/dd", "tn-ZA": "yyyy/MM/dd", "hsb-DE": "d. M. yyyy", "bs-Cyrl-BA": "d.M.yyyy", "tg-Cyrl-TJ": "dd.MM.yy", "sr-Latn-BA": "d.M.yyyy", "smj-NO": "dd.MM.yyyy", "rm-CH": "dd/MM/yyyy", "smj-SE": "yyyy-MM-dd", "quz-EC": "dd/MM/yyyy", "quz-PE": "dd/MM/yyyy", "hr-BA": "d.M.yyyy.", "sr-Latn-ME": "d.M.yyyy", "sma-SE": "yyyy-MM-dd", "en-SG": "d/M/yyyy", "ug-CN": "yyyy-M-d", "sr-Cyrl-BA": "d.M.yyyy", "es-US": "M/d/yyyy" };

    var l = navigator.language ? navigator.language : navigator['userLanguage'], y = d.getFullYear(), m = d.getMonth() + 1, d = d.getDate();
    f = (l in f) ? f[l] : "dd/MM/yyyy";
    function z(s) {
        s = '' + s; return s.length > 1 ? s : '0' + s;
    }
    f = f.replace(/yyyy/, y); f = f.replace(/yy/, String(y).substr(2));
    f = f.replace(/MM/, z(m)); f = f.replace(/M/, m);
    f = f.replace(/dd/, z(d)); f = f.replace(/d/, d);
    return f;
}

//  Utilizado para agregar funcionalidad a javascript y jQuery de forma nativa
(function () {
    jQuery.fn.hasClass = function (a) {
        var Aa = /[\n\t]/g, ca = /\s+/, Za = /\r/g, $a = /href|src|style/;
        a = " " + a + " ";
        for (var b = 0, d = this.length; b < d; b++)
            if ((" " + this[b].className + " ").toLowerCase().replace(Aa, " ").indexOf(a.toLowerCase()) > -1)
                return true;
        return false;
    }

    if (typeof String.prototype.startsWith != 'function') {
        String.prototype.startsWith = function (str) {
            return this.slice(0, str.length) == str;
        };
    }

    if (typeof String.prototype.endsWith != 'function') {
        String.prototype.endsWith = function (str) {
            return this.slice(-str.length) == str;
        };
    }

    if (typeof String.prototype.contains != 'function') {
        String.prototype.contains = function (str) {
            return this.indexOf(str) > -1;
        };
    }

    if (typeof String.prototype.into != 'function') {
        String.prototype.into = function (str) {
            var returnValue = false;
            var splitedValues = str.split('|');

            if (splitedValues.length > 0) {
                var max = splitedValues.length;
                for (var i = 0; i < max; i++) {
                    if (this == splitedValues[i])
                        return true;
                }
            }
            return returnValue;
        };
    }

    if (typeof String.prototype.notInto != 'function') {
        String.prototype.notInto = function (str) {
            var returnValue = true;
            var splitedValues = str.split('|');

            if (splitedValues.length > 0) {
                var max = splitedValues.length;
                for (var i = 0; i < max; i++) {
                    if (this == splitedValues[i])
                        return false;
                }
            }
            return returnValue;
        };

        if (typeof String.prototype.textWidth != 'function') {
            String.prototype.textWidth = function (font) {
                var f = font || '12px arial',
                    o = $('<div>' + this + '</div>')
                        .css({ 'position': 'absolute', 'float': 'left', 'white-space': 'nowrap', 'visibility': 'hidden', 'font': f })
                        .appendTo($('body')),
                    w = o.width();

                o.remove();

                return w;
            };
        }
    }
})();

$.fn.hasAttr = function (name) {
    return typeof this.attr(name) !== typeof undefined && this.attr(name) !== false;
};

function hasAttr(element, name) {
    return typeof $(element).attr(name) !== typeof undefined && $(element).attr(name) !== false;
};