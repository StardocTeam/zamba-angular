//-----------------------------------------gLOBAL vARIABLES---------------------------------------

//Esta variable sirve para acceder al la main toolbar de zamba, por ejemplo para cambiar de tab con
var MainTabber;
var ZambaVersion;
var tmrPopupsUnlocked;

//------------------------------------------END GLOBAL VARIABLES---------------------------------------------------------

var windowsMode = false;

var thisDomain;
var ZambaWebRestApiURL;
var zambaApplication = "Zamba";
var URLServer;
var urlGlobalSearch;
var URLServer;
var timerAplicarGrillaEstilos;

function getValueFromWebConfig(key) {

    var pathName = null;

    var baseUrl = window.location.protocol + "//" + window.location.host + "/" + window.location.pathname.split('/')[1];

    $.ajax({
        "async": false,
        "crossDomain": true,
        "url": baseUrl + "/Services/ViewsService.asmx/getValueFromWebConfig?key=" + key,
        "method": "GET",
        "headers": {
            "cache-control": "no-cache"
        },
        "success": function (response) {
            if (response.childNodes[0].innerHTML == undefined) {
                pathName = response.childNodes[0].textContent;
            } else {
                pathName = response.childNodes[0].innerHTML;
            }

        },
        "error": function (data, status, headers, config) {
            console.log(data);
        }
    });
    return pathName;
}


//-----------------------------------------------INIT ZAMBA-----------------------------------------------------------
LoadJquery(function () {
    cargaSync(windowsMode, DocumentReadyEvents);
});


function LoadWinWeb() {


    $("#openModalIF").on('hide.bs.modal', function (e) {
        $("#modalIframe").attr("src", "");
    });

    // Codigo para que la navBar se collapse cuando se precione alguno de los items.
    $(document).on('click', '.navbar-collapse.in', function (e) {
        if ($(e.target).is('a') && $(e.target).attr('class') != 'dropdown-toggle') {
            $(this).collapse('hide');
        }
    });

    $(window).on("resize", setTabsHeight);
    $(window).on("resize", toggleSidePanel);

    $('body').on("shown.bs.collapse", "#toolbarTabTasksList", function () { setTabsHeight(); });
    $('body').on("hidden.bs.collapse", "#toolbarTabTasksList", function () { setTabsHeight(); });
    $('body').on('click', '#btnTabSearch, #anchorTabTasks, #liInsert', function (event) { setTabsHeight(); });
    $('body').on('click', '#btnCloseSidePanel, #btnOpenSidePanel, #btnSidePanelTasksList, #btnCloseSidePanelTasksList', function (e) { toggleSidePanel(e); });
    $('body').on('click', '#toolbarTabTasksList, #ContentPlaceHolder_pnlHomeButtons, .dropdown-toggle', function (event) { EnableDropDown(event); });


    //Funcionalidad drag and drop para incorporacion Chat
    //$(document).ready(function () {
    GetOpenRules();
    $(document).on('dragstart', '.RowStyleTasks, .RowStyleAsoc', function (e) {
        if (navigator.userAgent.match(/Trident\/7\./)) {
            var tr = $(e.originalEvent.target)[0].tagName == "TR" ? $(e.originalEvent.target) : $(e.originalEvent.target).parents("tr");
            var url = tr.find("a").attr("href");
            zambaURL = thisDomain + "/Views/Main/default.aspx" + url.substring(url.indexOf("?"));
            e.originalEvent.dataTransfer.setData("text", zambaURL);
            e.originalEvent.dataTransfer.setData("text", $(tr.find("td")[2]).text());
        }
        else {
            var tr = $(e.toElement)[0].tagName == "TR" ? $(e.toElement) : $(e.toElement).parents("tr");
            var url = tr.find("a").attr("href");
            zambaURL = thisDomain + "/Views/Main/default.aspx" + url.substring(url.indexOf("?"));
            e.originalEvent.dataTransfer.setData("ZambaURL", zambaURL);
            e.originalEvent.dataTransfer.setData("ZambaURLDesc", $(tr.find("td")[2]).text());
        }
    });
    $(document).on("click", "#pagerZmb, #ContentPlaceHolder_Arbol_ArbolProcesos", function (e) {
        RowStyleTasksDraggable();
    });
    function RowStyleTasksDraggable() {
        setTimeout(function () {
            $(".RowStyleTasks, .RowStyleAsoc").attr("draggable", true);
        }, 4000);
    }

    //$('body').on('click', function (e) {
    //    CheckUserTimeOut();
    //});

    $("#IdSubList").val("");
    AddDynamicsTags();
    //Compruebo que Bootstrap este cargado para que no de error
    // if (typeof $().modal != 'function')
    // reloadBootstrap();

    //Manejo de tab header seleccionada
    $("#mainMenu >li").click(function () {
        //setTabsHeight();
        SelHeaderMenu($(this));
    });

    //Manejo de tareas listado seleccionado    
    $("body").on("click", "#TasksDivUL >li", function () {
        SelTaskMenu($(this));
    });
    // Busqueda global Modal cantidad de registros a mostrar
    $(".filterCount li a").click(function () {
        $(this).parents(".dropdown").find('.btn').html($(this).text() + ' <span class="caret"></span>').val($(this).data('value'));
    });

    setTabsHeight();


    //Tamaño ajustable de modal en ucwfexecution.ascx
    $('body').on('shown.bs.modal', '#openModalIFUcRules, parent.document', function (event) {
        var $div = $(this).find(".modal-body").children();
        $(this).find(".modal-content").css("width", $div.width() + 50).css("height", $div.height() + 100);
    });
}

function LoadHdn() {
    if ($('#taskId').length == 0) {
        $('<input>').attr({
            type: 'hidden',
            id: 'taskId',
            name: 'taskId',
            value: '<<Tarea>>.<<TaskId>>'
        }).appendTo('body');
    }

    if ($('#userid').length == 0) {
        $('<input>').attr({
            type: 'hidden',
            id: 'userid',
            name: 'userid',
            value: '<<FuncionesComunes>>.<<UsuarioActualId>>'
        }).appendTo('body');
    }

    if ($('#hdnRuleId').length == 0) {
        $('<input>').attr({
            type: 'hidden',
            id: 'hdnRuleId',
            name: 'hdnRuleId'
        }).appendTo('body');
    }



    if ($('#hdnAsocId').length == 0) {
        $('<input>').attr({
            type: 'hidden',
            id: 'hdnAsocId',
            name: 'hdnAsocId'
        }).appendTo('body');

    }


    if ($('#ZPOSTBACKFUNCTION').length == 0) {
        $('<input>').attr({
            type: 'hidden',
            id: 'ZPOSTBACKFUNCTION',
            name: 'RefreshTables',
            value: 'RefreshTables'
        }).appendTo('body');
    }

    if ($('form').length == 1) {
        $('form').each(function () {
            var attr = $(this).attr('id');

            // For some browsers, `attr` is undefined; for others,
            // `attr` is false.  Check for both.
            if ((typeof attr !== typeof undefined && attr !== false) || $(this).attr('id') == '') {
                $(this).attr('id', 'MainForm');
            }
        })
    }
};

////this function injects today's date in "dd/mm/yyyy" format into '.dateToday' class. 
function getDateToday() {
    $('.dateToday').each(function () {
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!
        var yyyy = today.getFullYear();

        if (dd < 10) {
            dd = '0' + dd
        }

        if (mm < 10) {
            mm = '0' + mm
        }

        today = dd + '/' + mm + '/' + yyyy;

        $(this).val(today);
    });
}

//this function injects today's date in "dd/mm/yyyy" format into '.dateToday' class. 
function getDateTimeToday() {
    $('.dateTimeToday').each(function () {
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!
        var yyyy = today.getFullYear();

        if (dd < 10) {
            dd = '0' + dd
        }

        if (mm < 10) {
            mm = '0' + mm
        }

        var h = today.getHours();
        var m = today.getMinutes();
        var s = today.getSeconds();

        today = dd + '/' + mm + '/' + yyyy + ' ' + h + ':' + m + ':' + s;
        $(this).val(today);
    });
}
//-----------------------------------------------LOAD JS y CSS REFERENCES --------------------------------------------
//This function search if the value of Jquery is undefined (not injected), and then injects it inside the <head>.  
function LoadJquery(callback) {
    if (typeof jQuery == 'undefined') {
        var headTag = document.getElementsByTagName("head")[0];
        var jqTag = document.createElement('script');
        jqTag.type = 'text/javascript';
        jqTag.src = 'Scripts/jquery-3.1.1.min.js';
        jqTag.charset = 'utf-8';
        jqTag.crossorigin = 'anonymous';
        headTag.appendChild(jqTag);
        jqTag.onload = function () {
            callback();
        }
    }
    else {
        callback();
    }
};



//this function is called at the init of the document, injecting js, css and meta links stored in arrays using Jquery.
function cargaSync(windowsMode, DocumentReadyEvents) {

    var cssLnkBegin = '<link rel="stylesheet" type="text/css" href="';
    var metaLnkBegin = '<meta http-equiv=';

    if (windowsMode == false) {
        var cssLnkBegin = '<link rel="stylesheet" type="text/css" href="../../';
    }
    else {
        var cssLnkBegin = '<link rel="stylesheet" type="text/css" href="./';
    }

    var referenceFilesMeta = ([
        '"Content-Type" content="txt/html ; charset=utf-8">',
        '"X-UA-Compatible" content="IE=edge">'
    ]);

    function cargaMeta() {
        for (i = 0; i <= referenceFilesMeta.length; i++) {
            $('head').append(metaLnkBegin + referenceFilesMeta[i]);
            //console.log("dinamic inject: " + referenceFilesMeta[i]);
        }
    }

    var referenceFilesCss = ([
        "Content/jquery-ui.min.css",
        "Content/bootstrap.min.css",
        "Content/bootstrap-datetimepicker.min.css",
        "Content/bootstrap-theme.min.css",
        "Content/toastr.css",
        "Content/font-awesome.min.css",
        //  "Content/zambauiwin.css",
        //  "Content/zambauiwintables.css",
        'Content/jquery.dataTables.css',
        "Scripts/kendoui/styles/kendo.common.min.css",
        "Scripts/kendoui/styles/kendo.default.min.css",
        "Scripts/kendoui/styles/kendo.default.mobile.min.css",
        "Scripts/kendoui/styles/kendo.dataviz.default.min.css",
        "Scripts/kendoui/styles/kendo.dataviz.silver.min.css",
        "Scripts/kendoui/styles/kendo.mobile.all.min.css",
        "Scripts/kendoui/styles/kendo.rtl.min.css",
        "Scripts/kendoui/styles/kendo.silver.min.css",
        "Content/partialSearchIndexs.css?v=248",
        "content/styles/tabber.css",
        //       "content/styles/js_datepicker.css"
        //'Content/datatables.jqueryui.css',
        //'Content/datatables.bootstrap.min.css'
        "Content/App_Themes/Provincia/General.css"
    ]);

    function cargaCss() {
        for (i = 0; i < referenceFilesCss.length; i++) {
            $('head').append(cssLnkBegin + referenceFilesCss[i] + '" type= "text/css" />');
            //console.log("dinamic inject: " + referenceFilesCss[i]);
        }
    }

    var referenceFilesJs = ([
        //'Scripts/jquery.min.js',
        'scripts/jquery-3.1.1.min.js',
        'scripts/jquery-1.12.4.js',
        'scripts/jquery-ui.js',
        'scripts/jquery-ui.min.js',
        //  'scripts/jquery.validate.min.js',
        //  'scripts/jquery.ui.datepicker.validation.min.js',
        'scripts/tabber.js',
        // 'scripts/bootstrap-datepicker.js',
        //     'scripts/jq_datepicker.js',
        //        'scripts/angular.min.js',
        'scripts/kendoui/js/kendo.all.min.js',
        'scripts/kendoui/js/kendo.grid.min.js',
        'scripts/jszip.min.js',
        'scripts/bootstrap.min.js',
        'scripts/moment.min.js',
        'scripts/moment-with-locales.min.js',
        // 'Scripts/bootstrap-datetimepicker.min.js',
        'scripts/toastr.js',
        'scripts/zambawin.js',
        'scripts/jquery.datatables.min.js',
        // 'Scripts/datatables.jqueryui.min.js',
        // 'Scripts/datatables.bootstrap.min.js',

    ]);

    function cargaJs() {
        for (i = 0; i < referenceFilesJs.length; i++) {
            var head = document.getElementsByTagName('head')[0];
            var script = document.createElement('script');
            script.type = 'text/javascript';
            script.async = "true";
            script.charset = 'utf-8';
            script.crossorigin = 'anonymous';

            if (windowsMode) {
                script.src = './' + referenceFilesJs[i];
            }
            else {
                script.src = '.././' + referenceFilesJs[i];
            }

            head.appendChild(script);
            //console.log("dinamic inject: " + referenceFilesJs[i]);
        }
    }




    if (windowsMode) {
        cargaMeta();
        cargaJs();
        cargaCss();
    }


    if (windowsMode == false) {
        if (thisDomain == null || thisDomain == undefined) {
            thisDomain = window.location.protocol + "//" + window.location.host + "/" + window.location.pathname.split('/')[1];
        }
        if (ZambaWebRestApiURL == null || ZambaWebRestApiURL == undefined) {
            ZambaWebRestApiURL = location.origin.trim() + getValueFromWebConfig("RestApiUrl") + "/api";
        }

        zambaApplication = "Zamba";
        URLServer = thisDomain + "/ZambaChat7/";
        urlGlobalSearch = thisDomain + "/Views/Search/";
        URLServer = thisDomain + "/ZambaChat7/";
    }
    DocumentReadyEvents();


};

function DocumentReadyEvents() {


    if (windowsMode) {
    }
    else {
        LoadWinWeb();
        FixsIE();
    }

    $(document).ready(function () {
        if (location.pathname.toLocaleLowerCase().toString().indexOf("search") > -1) {
            ReviewPopupsLocked();
        }
        $(".tbox_maxlength5").attr("maxlength", "5");
        $(".tbox_maxlength8").attr("maxlength", "8");

        $(".solonums").each(function () {
            $(this).keypress(function (e) {
                return IntegerCheck(e);
            })
        });

        // Combina las funcionalidades necesarias para mostrar el valor del input completado con ceros a la izquieda.
        $(".ZZerosLeft").each(function (index, elem) {
            setOnInput_ZerosLeftElem(elem);
            $(elem).on({
                "focus": function (event) {
                    $(event.target).select();
                },
                "focusout": AddZerosLeft
            });
        });
        // Combina las funcionalidades necesarias para mostrar el valor como decimal con millares y una coma.        
        $(".ZDecimal").each(function (index, elem) {
            setInputSeparatorElem(elem);
            subscribeItem_ZDecimal(elem);
            var event = new Event('ready');
            event.target = elem;
            render_Importe(event);
        });

        $(".BGGreen").each(function (index, elem) {
            $(this).css("backgroud-color", "#4cae4c");
        });

        // 
        $(".required").each(function (index, elem) {
            console.log("[" + index + "]" + " - " + "[" + elem.attributes.indexname.value + "]" + " - " + "[" + elem.id + "]" + ": es un campo obligatorio.");
            //if (elem.attributes.indexname != undefined ||
            //    elem.attributes.indexname != null ||
            //    elem.attributes.indexname != "" ) {
            //    console.log(elem.attributes.indexname.value +" - "+ elem.id +" es un campo obligatorio.");
            //} else {
            //    console.log(elem.id + " es un campo obligatorio.");
            //}                  
            //$(elem).on({
            //    "focusout": FieldReqValidation
            //})
        });

        $("input").focusout(function () {
            if ($(this).val() == '') {
                $(this).css("border-color", "#FF0000");

            }
            if ($(this).hasClass('datepicker')) {
                $(this).css("border-color", "#CFD5FF");
            }
            if ($(this).val() != '') {
                $(this).css("border-color", "#CFD5FF");
            }
        });


        if ($('.tablesorter').find("th").length >= 34) {
            $(".tablesorter").children(0).css("width", "300%");
        } else if ($('.tablesorter').find("th").length >= 19 && $('.tablesorter').find("th").length < 34) {
            $(".tablesorter").children(0).css("width", "150%");
        }



        //$(".tablesorter").children(0).css("width", "300%");
        setTimeout(
            function () {
                // $(".datepicker").datepicker({ dateFormat: 'dd/mm/yyyy' });
                $(".datepicker").datepicker({
                    changeMonth: true,
                    changeYear: true,
                    showOn: 'focus',
                    monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                    monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                    dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
                    dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
                    dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
                    dateFormat: "dd/mm/yy",
                    duration: "",
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

                LoadHdn();
                getDateTimeToday();
                getDateToday();
                searchForInteligentText();
                // LoadDropZone();
                ZFUNCTION();
                var doctypeId = $("[id$=hdnDocTypeId]").val();
                var docId = $("[id$=hdnDocId]").val();
                if (docId != undefined && $("#previewDocSearch")[0] != undefined) { // && $("#PDFForIE")[0].style.display == "block") {
                    var url = "../../Services/GetDocFile.ashx?DocTypeId=" + doctypeId + "&DocId=" + docId + "&UserId=" + GetUID() + "&ConvertToPDf=true"; //+ window.localStorage.queryStringAuthorization; + "&ConvertToPDf=true";
                    try {
                        //  $.get("../../Services/GetDocFile.ashx?DocTypeId=" + doctypeId + "&DocId=" + docId + "&UserID=" + GetUID() + "&ConvertToPDf=true", function (data) {
                        //console.log("Esta es a data: " + data);
                        //  var Cadena = data;
                        //  if (data.indexOf("Archivo no encontrado") != -1) {
                        $("#previewDocSearch").attr("src", url);
                        //  } else {
                        //   if ($("#previewDocSearch")[0] != undefined) $("#previewDocSearch")[0].contentWindow.OpenUrl(url, -1);
                        //   }
                        //swal("Load was performed.");
                        //   });
                    }
                    catch (error) {
                        $("#previewDocSearch").attr("src", url);
                    }
                }
            }, 3000);

        //try {
        //    var doctypeId = $("[id$=hdnDocTypeId]").val();
        //    var docId = $("[id$=hdnDocId]").val();
        //    if (doctypeId != undefined && doctypeId > 0 && docId != undefined && docId > 0)
        //    ShowDocumentInCommonIframe(GetUID(), doctypeId, docId);

        //} catch (e) {
        //    console.error(e);
        //}

    });





    ButtonsEvents();
    InjectJQueryHasClassFn();

    $.fn.hasAttr = function (name) {
        return this.attr(name) !== undefined;
    };

    $(function () {
        jQuery.support.cors = true;
    });
}

//---------------------------------------------------------------------------------------------------------------------

function convertDataURIToBinary(base64) {
    var raw = window.atob(base64);
    var rawLength = raw.length;
    var array = new Uint8Array(new ArrayBuffer(rawLength));

    for (var i = 0; i < rawLength; i++) {
        array[i] = raw.charCodeAt(i);
    }
    return array;
}




function ShowDocumentInCommonIframe(userId, docTypeId, docid) {
    var RDO = _getDocumentFromApiService(userId, docTypeId, docid, '', true, LoadDocument);

}

var descanso_scroll = false;
var ScrollLimit;
function Scroll_DocumentViewer(event) {
    //valida que el scroll se hace sobre el textArea del Body del mensaje, si no es asi, sigue con la funcion.
    if (event.toElement.id != "txtAreaBody") {
        var ItemList = $(".resultsGridActive").parent().first().children();
        var SelectItem;

        //Obtiene el limite maximo del scrollTop al estar en 0
        if (event.deltaY != 0 && $('#frmMsgViewerDos').scrollTop() == 0) {
            $('#frmMsgViewerDos').scrollTop(10000);
            ScrollLimit = $('#frmMsgViewerDos').scrollTop();
            $('#frmMsgViewerDos').scrollTop(0);
        }

        //wheel-down
        if (event.deltaY > 0) {
            if ($('#frmMsgViewerDos').scrollTop() == ScrollLimit) {
                ItemList.map(function (i) {
                    if (i != "lenght") {
                        if (i + 1 < ItemList.length) {
                            if (ItemList[i].classList.contains("resultsGridActive")) {
                                SelectItem = ItemList[parseInt(i) + 1];
                            }
                        }

                    }
                });
            }
        }
        //wheel-up
        else if (event.deltaY < 0) {
            if ($('#frmMsgViewerDos').scrollTop() == 0) {
                ItemList.map(function (i) {
                    if (i != "lenght") {
                        if (i - 1 >= 0) {
                            if (ItemList[i].classList.contains("resultsGridActive")) {
                                SelectItem = ItemList[parseInt(i) - 1];
                            }
                        }

                    }
                });
            }
        }

        if (SelectItem != null) {
            if (descanso_scroll == false) {
                descanso_scroll = true;
                //Busco el elemento 'img' que al hacerle clic seleccionba el documento..
                $(SelectItem).find("img").click();

                setTimeout(function () {
                    descanso_scroll = false;
                }, 500);
            }
        }
    }
}

function LoadDocument(RDO) {
    if (RDO != undefined) {
        var JsonResult = JSON.parse(RDO);
        if (JsonResult.dataObject != null) {

            $scope.subject = JsonResult.dataObject.subject;
            $scope.from = JsonResult.dataObject.from;
            $scope.to = JsonResult.dataObject.to;
            $scope.date = JsonResult.dataObject.date;
            $scope.body = JsonResult.dataObject.body;
            $scope.isMsg = JsonResult.dataObject.isMsg;
            $scope.attachs = JsonResult.dataObject.attachs;

            switchToDocumentViewer("MSG");
            console.log("Visualización de formato MSG exitosa.");
            try {
                $scope.$applyAsync();

            } catch (e) {

            }
        } else if (!(navigator.userAgent.indexOf('MSIE') !== -1 ||
            navigator.appVersion.indexOf('Trident/') > 0 ||
            navigator.userAgent.toString().indexOf('Edge/') > 0)) {
            if (JsonResult.fileName.indexOf(".html") != -1) {
                JsonResult.ContentType = "text/html";
            }
            var a = "data:" + JsonResult.ContentType + ";base64," + JsonResult.data;
            $("#previewDocSearch").attr("src", a);

        } else {

            var pdfAsDataUri = JsonResult.data;
            var pdfAsArray = convertDataURIToBinary(pdfAsDataUri);
            var url = '../../NGTemplates/PDFViewer/viewer.html?file=';

            var binaryData = [];
            binaryData.push(pdfAsArray);
            var dataPdf = window.URL.createObjectURL(new Blob(binaryData, { type: JsonResult.ContentType }))

            document.getElementById('previewDocSearch').setAttribute('src', url + encodeURIComponent(dataPdf));

        }
    } else {
        console.log("No hay un archivo para mostrar.");
        swal({
            title: "Archivo no disponible!",
            text: 'El archivo no esta disponible',
            icon: "warning",
            timer: 2000
        });
        //TO DO: Podria haber una pantalla de error para este o cualqueir caso.
    }
};

function _getDocumentFromApiService(UserId, DocTypeId, DocId, tokenSearchId, convertToPDF, callBack) {
    var result;
    var value = {
        Params: {
            "userId": UserId,
            "doctypeId": DocTypeId,
            "docid": DocId,
            "convertopdf": convertToPDF
        }
    }
    $.ajax({
        type: "POST",
        url: serviceBase + "/search/GetDocument",
        data: JSON.stringify(value),
        contentType: "application/json; charset=utf-8",
        crossDomain: true,
        headers: { "Authorization": tokenSearchId },
        success: function (data) {
            eval(callBack(data));
        },
        error: function (ex) {
            eval(callBack(null));
            console.error(ex);
        }
    });

    return result;
};


///-------------------------------------------------------------------------------------------------------------------


//Suscribe para cada evento determinado una funcionalidad determinada sobre el elemento pasado por parametro.
function subscribeItem_ZDecimal(item) {
    $(item).unbind('focus', selectEvent);
    $(item).unbind('focusout', render_Importe);
    $(item).unbind('keypress', val_ImporteEvent);

    //LEER: item sera tratado como objeto DOM, revisar que se pasa por parametro
    item.addEventListener("focus", selectEvent);
    item.addEventListener("focusout", render_Importe);
    item.addEventListener("keypress", val_ImporteEvent);
}

//Habilita el calendario para elementos creados fuera del DOM.
function subscribeItem_datepicker(item) {

    $(Document).ready(function () {
        setTimeout(
            function () {
                $(".datepicker").datepicker({
                    changeMonth: true,
                    changeYear: true,
                    showOn: 'focus',
                    monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                    monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                    dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
                    dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
                    dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
                    dateFormat: "dd/mm/yy",
                    duration: "",
                    onClose: function () {
                        $("#DivIndices").animate({ width: previousWidth }, "fast");
                        $("#separator").animate({ width: previousWidth }, "fast");
                        var a = $(window).width() - previousWidth - 5;
                        $("#separator").next().animate({ width: a }, "fast");
                    },
                    beforeShow: function (input, inst) {
                        previousWidth = $("#DivIndices").width();
                        var labelWidth = $(input).parent().prev().width();
                        var calcultateWidth = $(inst.dpDiv).width() + labelWidth + 20;
                        $("#DivIndices").animate({ width: calcultateWidth }, "slow");
                        $("#separator").animate({ width: calcultateWidth }, "slow");
                        var a = $(window).width() - calcultateWidth - 5;
                        $("#separator").next().animate({ width: a }, "slow");
                    }
                });

                LoadHdn();
                getDateTimeToday();
                getDateToday();
                searchForInteligentText();

                var doctypeId = $("[id$=hdnDocTypeId]").val();
                var docId = $("[id$=hdnDocId]").val();
                if (docId != undefined && $("#previewDocSearch")[0] != undefined) {
                    var url = "../../Services/GetDocFile.ashx?DocTypeId=" + doctypeId + "&DocId=" + docId + "&UserID=" + GetUID() + "&ConvertToPDf=true";
                    try {

                        $("#previewDocSearch").attr("src", url);
                    }
                    catch (error) {
                        $("#previewDocSearch").attr("src", url);
                    }
                }
            }, 100);
    });
}


//Notifica si el campo quedo vacio luego de intentar ingresarle un valor.
function FieldReqValidation(event) {
    if (event.target.value == undefined ||
        event.target.value == null ||
        event.target.value == "") {
        swal("El campo '" + event.target.attributes.indexname.value + "' es requerido.");
    }
    //else {
    //    swal("El campo '" + event.target.attributes.indexname.value + "' debe tener una opcion seleccionada.");
    //}
}

//Verifica que los campos requeridos estan completos, en caso contrario, notificara cuales deben ser llenados.
function validateRequired() {
    var mensaje = "El o los campos ";
    var resultado = true;

    $("[required]").each(function (index, element) {
        if (element.value == undefined ||
            element.value == null ||
            element.value == "" ||
            (element.tagName == "Select".toUpperCase() && element.value == "0")) {
            //console.log("[" + element.attributes.indexname.value + "]" + " - " + "[" + element.id + "]" + ": es un campo obligatorio.");
            element.style.borderColor = "#FF0000";
            mensaje += "'" + element.getAttribute("indexname") + "' ";
            resultado = false;
        } else {
            //console.log(element.id + " es un campo obligatorio.");
            element.style.borderColor = "#CFD5FF";
            resultado = true;
        }
    })

    mensaje += "son requeridos, por favor completelos.";

    if (resultado == false)
        swal("", mensaje, "warning");

    return resultado;
}

//Obtiene un resultado de una regla para posteriormente ejecutar una funcion.
function Execute_ZambaRuleId(event) {
    var genericRequest = {
        UserId: parseInt(GetUID()),
        Params: {
            "ruleId": event.target.attributes["ruleid"].value,
            "resultIds": getElementFromQueryString("docid")
        }
    };
    $.ajax({
        "async": false,
        "url": ZambaWebRestApiURL + '/tasks/Execute_ZambaTaskRule',
        "method": "POST",
        "headers": {
            "content-type": "application/json"
        },
        "success": function (data) {
            var scope_taskController = angular.element($("#taskController")).scope();
            var data = JSON.parse(data);
            scope_taskController.EvaluateRuleExecutionResult(data);
            //executeAction(data);
            return true;
        },
        "error": function (data) {
            var scope_taskController = angular.element($("#taskController")).scope();
            var data = JSON.parse(data);
            scope_taskController.EvaluateRuleExecutionResult(data);
            //executeAction(data);
            return false;
        },
        "data": JSON.stringify(genericRequest)
    });
}


function LoadIframe() {
    var doctypeId = $("[id$=hdnDocTypeId]").val();
    var docId = $("[id$=hdnDocId]").val();
    if (docId != undefined) {
        var url = "../../Services/GetDocFile.ashx?DocTypeId=" + doctypeId + "&DocId=" + docId + "&UserID=" + GetUID() + "&ConvertToPDf=true";
        try {
            if ($("#previewDocSearch")[0].contentWindow != undefined) {
                if ($("#previewDocSearch")[0] != undefined) $("#previewDocSearch")[0].contentWindow.OpenUrl(url, -1);
            }
            else {
                if ($("#previewDocSearch")[0] != undefined) {
                    $("#previewDocSearch")[0].attr("src", url);
                }
            }
        }
        catch (error) {
            $("#previewDocSearch").attr("src", url);

            try {
                RefreshIframeDirective(url);
            } catch (e) {
                console.log("Error al obtener el 'scope'. No se pudo viualizar el archivo.");
            }

        }
    }
}

function RefreshIframeDirective(url) {
    var scope = angular.element(".iframeClassPDF").scope();
    scope.ShowDocument(url);
}

function LoadIframeForResult(doctypeId, docId, userId) {
    var url = "../../Services/GetDocFile.ashx?DocTypeId=" + doctypeId + "&DocId=" + docId + "&UserID=" + userId + "&ConvertToPDf=true";
    try {
        if ($("#previewDocSearch")[0] != undefined) $("#previewDocSearch")[0].contentWindow.OpenUrl(url, -1);
    }
    catch (error) {
        $("#previewDocSearch").attr("src", url);
    }
}

function LoadJs(jsfile) {

    var head = document.getElementsByTagName('head')[0];
    var script = document.createElement('script');
    script.type = 'text/javascript';
    script.charset = 'utf-8';
    script.crossorigin = 'anonymous';

    if (windowsMode) {
        script.src = './' + jsfile;
    }
    else {
        script.src = '.././' + jsfile;
    }

    head.appendChild(script);
    //console.log("dinamic inject: " + jsfile);

}

function LoadDropZone() {
    LoadJs('DropFiles/dropfiles.js');
}
function ZFUNCTION() {
}
function ButtonsEvents() {
    $(".fg-button:not(.ui-state-disabled)").hover
        (
            function () {
                $(this).addClass("ui-state-hover");
            },
            function () {
                $(this).removeClass("ui-state-hover");
            }).mousedown(
                function () {
                    $(this).parents('.fg-buttonset-single:first').find(".fg-button.ui-state-active").removeClass("ui-state-active");
                    if ($(this).is('.ui-state-active.fg-button-toggleable, .fg-buttonset-multi .ui-state-active')) {
                        $(this).removeClass("ui-state-active");
                    }
                    else {
                        $(this).addClass("ui-state-active");
                    }
                }).mouseup(
                    function () {
                        if (!$(this).is('.fg-button-toggleable, .fg-buttonset-single .fg-button, .fg-buttonset-multi .fg-button')) {
                            $(this).removeClass("ui-state-active");
                        }
                    });
};
//------------------------------------------------END LOAD JS y CSS REFERENCES---------------------------------

//-----------------------------------------------LOAD DYNAMIC TAGS --------------------------------------------

//Tag's assignation
(function () {
    if ($('#taskId').length == 0) {
        $('<input>').attr({
            type: 'hidden',
            id: 'taskId',
            name: 'taskId',
            value: '<<Tarea>>.<<TaskId>>'
        }).appendTo('body');
    }

    if ($('#userid').length == 0) {
        $('<input>').attr({
            type: 'hidden',
            id: 'userid',
            name: 'userid',
            value: '<<FuncionesComunes>>.<<UsuarioActualId>>'
        }).appendTo('body');
    }

    if ($('#hdnRuleId').length == 0) {
        $('<input>').attr({
            type: 'hidden',
            id: 'hdnRuleId',
            name: 'hdnRuleId'
        }).appendTo('body');
    }

    if ($('#hdnAsocId').length == 0) {
        $('<input>').attr({
            type: 'hidden',
            id: 'hdnAsocId',
            name: 'hdnAsocId'
        }).appendTo('body');
    }

    if ($('#ZPOSTBACKFUNCTION').length == 0) {
        $('<input>').attr({
            type: 'hidden',
            id: 'ZPOSTBACKFUNCTION',
            name: 'RefreshTables',
            value: 'RefreshTables'
        }).appendTo('body');
    }

    if ($('form').length == 1) {
        $('form').each(function () {
            //if it's false or empty..
            if ($(this).hasAttr('id') == false || $(this).attr('id') == '') {
                $(this).attr('id', 'MainForm');
            }
        })
    }
});


//-----------------------------------------------END LOAD DYNAMIC TAGS-----------------------------------------------------------------------------

//-----------------------------------------------END LOAD ZAMBA------------------------------------------------------------------------



//-----------------------------------------------UNLOAD TASK------------------------------------------------------------------------

$(window).bind("beforeunload", function () {

    // return confirm("Guarde su trabajo primero");
})

//-----------------------------------------------END UNLOAD TASK------------------------------------------------------------------------




function InjectJQueryHasClassFn() {
    jQuery.fn.hasClass = function (a) {
        var Aa = /[\n\t]/g, ca = /\s+/, Za = /\r/g, $a = /href|src|style/;
        a = " " + a + " ";
        for (var b = 0, d = this.length; b < d; b++)
            if ((" " + this[b].className + " ").toLowerCase().replace(Aa, " ").indexOf(a.toLowerCase()) > -1)
                return true;
        return false
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
};


//-----------------------------------------------------------------------------------------------------
//
//  Bloque utilizado para agregar funcionalidad a javascript y jQuery de forma nativa
//  Nota: Evaluar si conviene mover estas funcionalidades "nativas" a un js nuevo.
//
//-----------------------------------------------------------------------------------------------------
function InjectJQueryHasClassFn() {
    jQuery.fn.hasClass = function (a) {
        var Aa = /[\n\t]/g, ca = /\s+/, Za = /\r/g, $a = /href|src|style/;
        a = " " + a + " ";
        for (var b = 0, d = this.length; b < d; b++)
            if ((" " + this[b].className + " ").toLowerCase().replace(Aa, " ").indexOf(a.toLowerCase()) > -1)
                return true;
        return false
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
};
function LoadZVarComponents() {

    $(".zvar").each(function () {


        var varname;
        if (this.indexOf('(') > 0) {
            varname = this.replace('zamba_zvar(', '').replace(')', '');
        }
        else {
            varname = this.substr("zamba_zvar_".length);
        }

        $.ajax({
            type: "POST",
            url: thisDomain + "/AsignVarValue",
            data: "{ VarName: '" + varname + "', E: " + this + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                this.InnerHtml = data;
                this.Id = varname;
            },
            cache: true,
            error: function (request, status, err) {
                swal("Error al obtener el cuerpo de tabla. \r" + request.responseText);
            }
        });
    });
}


//Funciones para Formularios con Grillas, para Ejecutar Reglas desde cada Row de la grilla o para obtener los valores de todos los rows de la grilla y ejecutar una regla
function SetRuleIdAndZvar(sender, RuleId, ZVars) {

    document.getElementById("hdnRuleId").name = RuleId;
    document.getElementById("hdnRuleId").value = ZVars;
    frmMain.submit();
}

// //Cada vez que se haga click un un tab, se comprobara si este fue cargado anteriormente. Si no lo hizo, se carga de la tabla que contiene y se modifica su flag (para evitar que se intente cargar la tabla cada vez que se clickie el tab). 
// var tabberOptions = {
// 'onClick': function (argsObj) {
// swal('Tab click');
// var tab = argsObj.tabber;
// var index = argsObj.index;
// if (!loadedTabs[index]) {
// loadedTabs[index] = true;
// var table;
// var tables = [];
// for (var i = 0; i < tab.tabs[index].div.children.length; i++) {
// if (tab.tabs[index].div.children[i].nodeName == "TABLE") {
// table = tab.tabs[index].div.children[i];
// if (table.outerHTML.indexOf("zAjaxTable") > -1) {
// tables.push(table);
// swal('table zAjaxTable Added');
// }
// }
// }
// ProcessAjaxTable(tables);
// }
// }
// };

//LLamada desde formbrowser, le llega el path del webservice y lo releva a la funcion que lo necesite
var pathToWS;
function SetGlobalParams(path) {
    //swal('Set Path');
    pathToWS = path;
}

function getWSPath() {
    //return 'http://win-gs9gmujits8/ZambaWS2014/results.asmx';
    //return 'http://srvflow2/ZambapreWS2014/results.asmx';
    // return 'http://localhost:2323/Results.asmx';
    return pathToWS;
}

function ProcessAjaxFunctions() {
    try {
        // swal('ProcessAjaxFunctions');
        var numberOfTabs = $(".tabbertab").length;
        //	swal('Tabs' + numberOfTabs);

        if (numberOfTabs > 0) {
            loadedTabs = new Array(numberOfTabs);
            for (var i = 0; i < numberOfTabs; i++) {
                loadedTabs[i] = false;
            }
        }
    }
    catch (err) {
    }

    getDateToday();
    getDateTimeToday();

    if (typeof ProcessAllzAutoAjaxTable == 'function') {
        ProcessAllzAutoAjaxTable();
    }
    if (typeof ProcessAllJsonTables == 'function') {
        ProcessAllJsonTables();
    }
};

function searchForInteligentText() {
    // Busca en todo el body del documento los elementos que contengan texto inteligente y concatena el id de dichos elementos (separados por comas),
    // posteriormente crea un input hidden de id=idsConTextoInteligente que tiene como atributo "value" el resultado de la concatenación.
    // Este input es posteriormente procesado por en el FormBrowser donde se resuelve el texto inteligente.
    var listaAttr = [];
    var foundIT = false;
    $("body").children().each(function () {
        //swal(this.id);
        $.each(this.attributes, function () {
            if ((this.value.indexOf(">>.<<")) > -1) {
                foundIT = true;
            }
        });
        if (foundIT) {
            listaAttr.push(this.id);
            // swal('foundIT ' + this.id);
            foundIT = false;
        }
    });
    if (listaAttr.length > 0) {
        var resultAttr = listaAttr.join();
        // swal('idsConTextoInteligente ' + resultAttr);
        $("<input type=hidden id='idsConTextoInteligente' value='" + resultAttr + "'/>").appendTo("body");
    }
}

//ZPOSTBACKFUNCTION

function SetIframeSource(id, path) {
    var iframe = $('#' + id);

    if (iframe.length) {
        iframe.attr('src', path);
    }

}


function View_Doc() {
    try {
        var docPath = $("[id$=hdnFilePath]").val();
        window.open(docPath, '_blank');
    } catch (e) {
        //swal(e.description);
    }
}

var OpenModalIF = {

    show: function () {
        var $modal = $("#openModalIF", window.parent.document);

        let _height = $(window).height() * .95;
        $modal.find(".modal-dialog").css("height", _height + "px");

        let _width = $(window).width();
        $modal.find(".modal-dialog").css("width", _width + "px");

        $modal.modal({
            backdrop: 'static',
            keyboard: false
        });
    },

    fullscreen: function (btn) {

        var state = $(btn).attr("state") || "restore";

        if (state == "restore") {
            var $md = $(btn).parents(".modal-dialog");
            $(btn).attr("lastHeight", $md.css("height")).attr("lastWidth", $md.css("width"));
            $md.css({
                "height": (window.windowHeight() - 20) + "px",
                "width": (window.windowWidth() - 20) + "px",
                "margin": 0,
                "left": 0
            }).parents("#openModalIF").css({ "padding": 0, "left": 0, "top": 0 });
        }
        else {
            $(btn).parents(".modal-dialog").css({
                "height": $(btn).attr("lastHeight"),
                "width": $(btn).attr("lastWidth")
            })
        }
        $(btn).attr("state", state == "restore" ? "maximize" : "restore");
    }

}

function Collapse(collapse) {
    if (collapse) {
        $("#tabContent").attr('src', '');
        //ShowAsociated();
        $("#btnCollapse").hide();
        $("#tabContent").hide('fast');
    } else {
        $("#btnCollapse").show();
        $("#tabContent").show('fast');
        ShowLoadingAnimation();
    }
}

function ShowTaskChat(doctypeid) {
    Collapse(false);
    var doctypeid = sessionStorage.getItem('taskid');
    $("#tabContent").attr('src', '../WF/TaskDetails/TaskChat.aspx?ResultID=<%=DocId %>&Taskid="' + doctypeid + '"').val(doctypeid);

}

function ShowForum() {
    Collapse(false);
    $("#tabContent").attr('src', '../WF/TaskDetails/TaskForum.aspx?ResultID=<%=DocId %>&DocTypeId=<%=DocTypeId%>&currentUserID=<%=CurrentUserID%>');
}

//function ShowAsociated() {
//    Collapse(false);
//    $("#tabContent").attr('src', '../WF/TaskDetails/TaskAsociated.aspx?ResultID=<%=DocId %>&DocTypeId=<%=DocTypeId%>');
//    $("#page-content-wrapper").hide();
//    GrillaEstilos();
//}

function GrillaEstilos() {
    timerAplicarGrillaEstilos = window.setInterval(function () {
        if ($(".header-center > th").length > 0) {
            $(".header-center > th").css("text-align", "center");
            $(".header-center > th").css("padding", "5px");
            clearInterval(timerAplicarGrillaEstilos);
        }
    }, 2000);
}
function ShowDocHistory() {
    Collapse(false);
    $("#tabContent").attr('src', '../WF/TaskDetails/TaskHistory.aspx?ResultID=<%=DocId %>');
}

function ShowMailHistory() {
    Collapse(false);
    $("#tabContent").attr('src', '../WF/TaskDetails/TaskMailhistory.aspx?ResultID=<%=DocId %>');
    $("#page-content-wrapper").hide();
}

function cleanBorder(btn) {
    var InputVal = $(btn).parent().parent().children("input");
    $(InputVal).css("border", "");

};

function ClearBtn() {
    $('.col-xs-8.InputFilter').children("input").val("");

}

function CleanInputModal() {
    $(".btntrash").trigger('click');
    $(".BtnTrashModal").hide();
}

function CleanAllInputs() {
    $(".btntrash").trigger('click');
    $(".hiddenClearAll").trigger('click');
}


function InputSelec() {
    $('#search-form :input')
        .change(function () {
            var $input = $(this);
            if ($input.val() === '') {
                $input.css("border", "");
            }
            else {
                $input.css("border", "solid 1px #1E90FF");
            }
        });
}

function Numeros(string) {//Solo numeros
    var out = '';
    var filtro = '1234567890';//Caracteres validos

    //Recorrer el texto y verificar si el caracter se encuentra en la lista de validos 
    for (var i = 0; i < string.length; i++)
        if (filtro.indexOf(string.charAt(i)) != -1)
            //Se añaden a la salida los caracteres validos
            out += string.charAt(i);

    //Retornar valor filtrado
    return out;
}

function ValidarFechas(DataFrom) {
    var idfrom = DataFrom.id;
    var desde = moment(DataFrom.value, "DD/MM/YYYY");
    var hasta = moment($("#" + idfrom + "-2").val(), "DD/MM/YYYY");

    if (hasta._i != undefined && hasta._i != "") {
        if (desde < hasta) {
            return true;
        } else {
            toastr.warning("La fecha 'DESDE' " + DataFrom.value + " es mayor a la fecha 'HASTA' " + hasta._i + ", vuelva a intentarlo.");
            DataFrom.value = "";
            return false;
        }
    }
}

function validateDateUntil(DataTo) {
    var idfrom = DataTo.id.slice(0, DataTo.id.length - 2);
    var desde = moment($("#" + idfrom.toString()).val(), "DD/MM/YYYY");
    var hasta = moment(DataTo.value, "DD/MM/YYYY");

    if (desde != undefined && desde != "") {
        if (desde < hasta) {
            return true;
        } else {
            toastr.warning("La fecha 'HASTA' " + DataTo.value + " es menor a la fecha 'DESDE' " + $("#" + idfrom.toString()).val() + ", vuelva a intentarlo.");
            DataTo.value = "";
            return false;
        }
    } else {
        toastr.warning("Por favor complete el primer valor 'DESDE'");
        DataTo.value = "";
        return false;
    }
}

function ValidataInputFormatDate(data) {
    if (data != null && data.value != "") {
        var dateFormat = /^((0[1-9]|[12][0-9]|3[01])(\/)(0[13578]|1[02]))|((0[1-9]|[12][0-9])(\/)(02))|((0[1-9]|[12][0-9]|3[0])(\/)(0[469]|11))(\/)\d{4}$/;
        var id = data.id;
        var isValid = data.value.match(dateFormat);
        if (isValid == null) {
            document.getElementById(id).style.border = "solid 1px #FF0000";
        } else {
            var arrayDate = data.value.split("/");
            var dateToValidate = new Date(arrayDate[2], arrayDate[1], arrayDate[0]);
            var minDateValid = new Date(1930, 01, 01);
            if (dateToValidate < minDateValid) {
                document.getElementById(id).style.border = "solid 1px #FF0000";
            }
            else {
                document.getElementById(id).style.border = "";
            }
        }
    } else {
        var id = data.id;
        document.getElementById(id).style.border = "";
    }
}

function validateFormateDateKeyPress(e, data) {
    var foo = '';
    foo = data.value.replace(/[^0-9]/g, '');
    if (foo.length >= 7) {
        setTimeout(validateInputFormatDateOnKeyPress, 100, data.id);
    }
}

function validateInputFormatDateOnKeyPress(id) {
    var data = document.getElementById(id);
    ValidataInputFormatDate(data)
}




//Si el input del modal No contiene datos se Oculta el btn
function BtnTrashHidden() {
    $(".BtnTrashModal").hide();
    if ($("#ModalInput").val() != "") {
        $(".BtnTrashModal").show();
    }
}




var previousWidth;

function makeCalendar(id) {
    //$(function () {
    //    if (id.indexOf("completarindice") != -1) {
    //        $('#' + id).datepicker({
    //            changeMonth: true,
    //            changeYear: true,
    //            dateFormat: 'dd/mm/yy',
    //            showOn: 'button',
    //            duration: "",
    //            autoSize: false,
    //            onClose: function () {
    //                //Restauramos el width original
    //                $("#DivIndices").animate({ width: previousWidth }, "fast");
    //                $("#separator").animate({ width: previousWidth }, "fast");

    //                //Acomodamos el documento a lo restaurado.
    //                var a = $(window).width() - previousWidth - 5;
    //                $("#separator").next().animate({ width: a }, "fast");
    //            },
    //            beforeShow: function (input, inst) {
    //                //Guardamos el width anterior.  
    //                previousWidth = $("#DivIndices").width();

    //                //Saco el ancho de los labels
    //                var labelWidth = $(input).parent().prev().width();

    //                var calcultateWidth = $(inst.dpDiv).width() + labelWidth + 20;

    //                $("#DivIndices").animate({ width: calcultateWidth }, "slow");
    //                $("#separator").animate({ width: calcultateWidth }, "slow");

    //                //Acomodamos el documento, con la diferencia de pantalla.
    //                var a = $(window).width() - calcultateWidth - 5;
    //                $("#separator").next().animate({ width: a }, "slow");
    //            }
    //        });
    //    }
    //    else {
    //        $('#' + id).datepicker({
    //            changeMonth: true,
    //            dateFormat: 'dd/mm/yy',
    //            changeYear: true,
    //            duration: ""
    //        });
    //    }
    //});
}

var previousWidth;
function AddCalendar(id, limit) {
    if (id.indexOf("completarindice") != -1) {
        if ($('#' + id).length > 0) $('#' + id).datepicker({
            changeMonth: true,
            changeYear: true,
            showOn: 'focus',
            dateFormat: "dd/mm/yyyy",
            duration: "",
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
    else {
        $(function () {
            if ($('#' + id).length > 0) {
                // if ($('#' + id).val() != "" && !isNaN(Date.parse($('#' + id).val())))
                //try {
                //    if ($('#' + id).val($.datepicker.CustomFormat))
                //        $('#' + id).val($.datepicker.CustomFormat('dd/mm', new Date($('#' + id).val())));

                //} catch (e) {

                //}
                $('#' + id).datepicker({
                    changeMonth: true,
                    changeYear: true,
                    yearRange: "-150:+100",
                    showOn: 'focus',
                    duration: "",
                    dateFormat: "dd/mm/yy",
                    constrainInput: true
                });
            }
        });
    }
}

function windowHeight() {
    var myHeight = 0;
    if (typeof (window.innerWidth) == 'number') {
        myHeight = window.innerHeight;
    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        myHeight = document.documentElement.clientHeight;
    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        myHeight = document.body.clientHeight;
    }
    return myHeight;
}

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

function ajustarselects() {
    $('select').each(function () {
        if (!$(this).hasClass("notExpand")) {
            if ($(this).attr('multiple') == false) {
                $(this).mousedown(function () {
                    if ($(this).css("width") != "auto") {
                        var width = $(this).width();
                        $(this).data("origWidth", $(this).css("width")).css("width", "auto");

                        // If the width is now less than before then undo 
                        if ($(this).width() < width) {
                            $(this).unbind('mousedown');
                            $(this).css("width", $(this).data("origWidth"));
                        }
                    }
                })
                    // Handle blur if the user does not change the value 
                    .blur(function () {
                        $(this).css("width", $(this).data("origWidth"));
                    })
                    // Handle change of the user does change the value 
                    .change(function () {
                        $(this).css("width", $(this).data("origWidth"));
                    });
            }
        }
    });
}

function expandDiv(div, difwidth, difheight) {
    var myWidth = 0, myHeight = 0;
    if (typeof (window.innerWidth) == 'number') {
        //Non-IE
        myWidth = window.innerWidth;
        myHeight = window.innerHeight;
    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        //IE 6+ in 'standards compliant mode'
        myWidth = document.documentElement.clientWidth;
        myHeight = document.documentElement.clientHeight;
    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        //IE 4 compatible
        myWidth = document.body.clientWidth;
        myHeight = document.body.clientHeight;
    }
    myWidth = myWidth - difwidth;
    myHeight = myHeight - difheight
    setInterval("doExpand('" + div + "'," + myWidth + "," + myHeight + ")", 500);
}

function doExpand(div, w, h) {
    $("#" + div).width(w);
    $("#" + div).height(h);
}

function modifyTextBoxSize(tb) {
    if (tb.rows == 5) {
        tb.rows = 1;
    }
    else {
        tb.rows = 5;
    }
}

function maxlength(tb, max) {
    return (tb.value.length < max);
}

//Obtiene la height que deben tener los contenedores principales
function getHeightScreen() {
    //Obtiene la altura que deben tener los contenedores principales
    var masterHeader = $("#MasterHeader").css("display") == "none" ? 0 : $("#MasterHeader").height();
    var barraCabecera = $("#barra-Cabezera").css("display") == "none" ? 0 : $("#barra-Cabezera").innerHeight();
    var headersHeight = masterHeader + barraCabecera;
    var currentheight = document.body.clientHeight - headersHeight;
    if (currentheight > 500) {
        return currentheight;
    }
    else {
        return 500;
    }
}



function getEnableButton() {
    var user = GetUID();
    var currentUserGrupsIds = GetGroupsIdsByUserId(user);

    $("[enableGroups],[enablegroups]").each(function () {

        var EnabledGroupIds = this.getAttribute("enableGroups");
        if (EnabledGroupIds != null) {
            var EnabledGroupIds = this.getAttribute("enablegroups");
            if (EnabledGroupIds != null) {

                var groupIds = EnabledGroupIds.split(",");
                var UserGrupsIds = currentUserGrupsIds.split(",");

                for (var i = 0; i < groupIds.length; i++) {
                    for (var x = 0; x < UserGrupsIds.length; x++) {
                        if (groupIds[i] == UserGrupsIds[x]) {
                            $(this).css('display', 'inline');
                        }
                    }
                }
            }
        }
    });

    $("[disableGroups],[disablegroups]").each(function () {

        var DisabledGroupIds = this.getAttribute("disableGroups");
        if (DisabledGroupIds != null) {
            var DisabledGroupIds = this.getAttribute("disablegroups");
            if (DisabledGroupIds != null) {

                var groupIds = DisabledGroupIds.split(",");
                var UserGrupsIds = currentUserGrupsIds.split(",");

                for (var i = 0; i < groupIds.length; i++) {
                    for (var x = 0; x < UserGrupsIds.length; x++) {
                        if (groupIds[i] == UserGrupsIds[x]) {
                            $(this).css('display', 'none');
                        }
                    }
                }
            }
        }
    });





    //----------------------------------------




    //----------------------------------------




    var idinput = $("input");
    for (i = 0; i < idinput.length; i++) {
        if (idinput[i].getAttribute("enableGroups")) {
            $(this).addClass("tsen");
            var user = GetUID();
            var consulid = GetGroupsIdsByUserId(user);
            var JsonId = JSON.stringify(consulid);

            if (JsonId.contains("1020160") || JsonId.contains("1020161") || JsonId.contains("13510") || JsonId.contains("13511") || JsonId.contains("13512") || JsonId.contains("13513") || JsonId.contains("13514") || JsonId.contains("13515") || JsonId.contains("13516") || JsonId.contains("13517")) {
                $(".tsen").css("display", "block");
            }
            else {
                $(".tsen").css("display", "none");
            }
        }
    }

}

function getEditGroups() {
    var user = GetUID();
    var currentUserGrupsIds = GetGroupsIdsByUserId(user);
    var ListaItems = $("[editgroups]");

    ListaItems.each(function () {
        var EditGroups = this.getAttribute("editgroups");
        if (EditGroups != null) {
            EditGroups = this.getAttribute("editgroups");
            if (EditGroups != null) {
                var groupIds = EditGroups.split(",");
                var UserGrupsIds = currentUserGrupsIds.split(",");

                for (var i = 0; i < groupIds.length; i++) {
                    for (var x = 0; x < UserGrupsIds.length; x++) {
                        if (groupIds[i] == UserGrupsIds[x]) {
                            $(this).removeAttr("disabled");
                            $(this).removeAttr("readonly");
                        }
                    }

                }
            }
        }
    });
}

function IfUserInGroups(groups) {
    var user = GetUID();
    var currentUserGrupsIds = GetGroupsIdsByUserId(user);

    if (groups != null) {
        var groupIds = groups.split(",");
        var UserGrupsIds = currentUserGrupsIds.split(",");

        for (var i = 0; i < groupIds.length; i++) {
            for (var x = 0; x < UserGrupsIds.length; x++) {
                if (groupIds[i] == UserGrupsIds[x]) {
                    return true;
                }
            }
        }
    }
    return false;
}
//-------//-------//-------//-------//-------//-------

//function setEnableGroupElementsVisibility(classToAdd, elementName, attribute) {
//    setEnableElementsClass(classToAdd, elementName, attribute);
//    var userId = GetUID();
//    var elements = getElementsByClass(classToAdd, elementName);
//    var currentUserGrupsIds = GetGroupsIdsByUserId(userId);
//    for (var i = 0; i < elements.length; i++) {
//        var elementGroupIds = elements[i].getAttribute(attribute);
//        var isAnyElementGroupIdInUsersGroupIds = compareElementsGroupIds(currentUserGrupsIds, elementGroupIds);    
//        if (!isAnyElementGroupIdInUsersGroupIds) {
//            elements[i].classList.add("hiddenButton");
//        }    
//    }
//}


//function setEnableElementsClass(classToAdd, elementName, attribute) {
//    var elements = $(elementName);
//    for (i = 0; i < elements.length; i++) {
//        if (elements[i].getAttribute(attribute) != null) {
//            elements[i].classList.add(classToAdd);
//        }
//    }
//}

//function getElementsByClass(classNameElement, elementName) {
//    return $(elementName + "." + classNameElement);
//}

//function compareElementsGroupIds(usrGroupIdList, elementsGroupIdList) {
//    var anyMatch = false;
//    var list = elementsGroupIdList.split(",");
//    for (var usrGroupIdIndex = 0; usrGroupIdIndex < usrGroupIdList.length; usrGroupIdIndex++) {
//        if (list.indexOf(String(usrGroupIdList[usrGroupIdIndex])) > -1) {
//            anyMatch = true;
//        }
//    }
//    console.log(anyMatch);
//    return anyMatch;
//}

//function GetGroupsIdsByUserId(id) {

//    var groups = [];
//    if (groups != undefined) {

//        $.ajax({
//            type: 'GET',
//            async: false,
//            url: ZambaWebRestApiURL + '/search/GetGroupsByUserIds',
//            data: { usrID: id },
//            success: function (data) {
//                groups = data;
//            },
//            error: function (xhr, status, error) {
//                console.log(error);
//            }
//        });
//    }
//    else {
//        groups = [];
//    }
//    console.log(groups);
//    return groups;

//}


//-------

function getMTHeight() {
    var mt = document.getElementById("mainMenu");
    if (mt) {
        return $(mt).height();
    }
    else {
        return 0;
    }
}

function getTBHeight() {
    var mt = document.getElementById("TasksDivUL");
    if (mt) {
        return $(mt).height();
    }
    else {
        return 0;
    }
}

function GetDocumentAvailableHeight() {
    var mainMenuHeight = (typeof parent.getMainMenuHeightFromParent == "function") ? parent.getMainMenuHeightFromParent() : 0;
    var tbh = (typeof parent.getTBH == "function") ? parent.getTBH() : 0;

    var gtHeightScreen = getHeightScreen();
    var availableHeight = (gtHeightScreen == 0) ? parent.GetTaskAvailableHeight() : gtHeightScreen;

    var tToolBTaskH = (getToolBTaskH() == 0) ? 30 : getToolBTaskH();
    var tTabDetH = (getTabDetH() == 0) ? 30 : getTabDetH();

    return availableHeight - mainMenuHeight - tbh - tToolBTaskH - tTabDetH;
}

function ValidateLength(element, RequiredLength) {
    var Str = element.value;
    //$(element).valid();
    if (Str.length < RequiredLength)
        return true;
    return false;
}

function KeyIsValid(e, TypeToValidate, Obj) {
    if (e.keyCode > 0) return true;

    var key = e.charCode || e.keyCode || 0;
    var isValid = false;

    switch (TypeToValidate) {
        case "numeric":
            if (key >= 48 && key <= 57)
                isValid = true;
            break;
        case "date":
            if ((key >= 48 && key <= 57) || key == 47)
                isValid = true;
            break;
        default:
            if (TypeToValidate.indexOf("decimal") < 0)
                break;

            var arrayOfConfiguration = TypeToValidate.split('_');
            if (arrayOfConfiguration.length < 2)
                break;

            var decimalCount = arrayOfConfiguration[1];
            if (!decimalCount)
                break;

            var integerCount = arrayOfConfiguration[2];
            if (!integerCount)
                break;

            var currentValue = $(Obj).val();
            var commaPos = currentValue.indexOf(',');
            var caretPos = $(Obj).caret().start;

            if (key == 44)
                isValid = (commaPos == -1 && caretPos != 0);
            else {
                if ((key >= 48 && key <= 57)) {
                    if (commaPos == -1) {
                        isValid = (currentValue.length < integerCount);
                    }
                    else {
                        if (caretPos > commaPos) {
                            isValid = (currentValue.length - decimalCount < commaPos + 1);
                        }
                        else
                            isValid = (commaPos < integerCount);
                    }
                }
            }
            break;
    }
    return isValid;
}

function SelectErrorTab(control) {
    if (!tabErrorSelected) {
        var currTabber;
        var i;
        //por cada tabber
        $(".tabberlive").each(function () {
            i = 0;
            currTabber = this;
            //por cada tab
            $(this).find(".tabbertab").each(function () {
                //Si la tab contiene al elemento a resaltar, selecciono la tab
                if ($(this).find("#" + control.id).length > 0) {
                    currTabber.tabber.tabShow(i);
                    i = 0;
                    return;
                }

                //Si el tab no conteien al elemento y es la tad del tabber que estamos recorriendo incremento el contador.
                if ($(this).parent()[0] != null && (currTabber.uniqueID == $(this).parent()[0].uniqueID)) {
                    i++;
                }
            });
        });
        tabErrorSelected = true;
    }
}

var validatorExtended = false;
var tabErrorSelected = false;
var lastErrorElement;


function DoGeneralValidation(objBtn, evt) {
    if ($("#aspnetForm").length && !$("#aspnetForm").valid()) {
        evt.stopPropagation();
        evt.preventDefault();
        setTimeout("parent.hideLoading();", 1000);

        var msgDialog = $("#divValidationFail");

        if (msgDialog) {
            $(msgDialog).dialog({ autoOpen: false, modal: true, position: 'center', closeOnEscape: false, draggable: false, resizable: false, open: function (event, ui) { $(".ui-dialog-titlebar-close").hide(); } });
            $(msgDialog).dialog('open');
        }
        return false;
    }
    else {
        return true;
    }
}

function DoRuleValidation(objBtn, evt) {
    var isValid = true;
    var divContainter = $('[id$="pnlUcRules"]');
    divContainter.find(".RuleField").each(function () {
        if (!$(this).valid()) {
            isValid = false;
            return false;
        }
    });

    if (!isValid) {
        setTimeout("parent.hideLoading();", 1000);
        var msgDialog = $("#divValidationFail");
        if (msgDialog) {
            $(msgDialog).dialog({ autoOpen: false, modal: true, position: 'center', closeOnEscape: false, draggable: false, resizable: false, open: function (event, ui) { $(".ui-dialog-titlebar-close").hide(); } });
            $(msgDialog).dialog('open');
        }
        return false;
    }
    else {
        return true;
    }
}



function SetValidationsAction(SubmitButtonId) {
    try {
        // SetValidateForLength();
    } catch (e) {
        console.error(e);
    }

    try {
        //  SetValidateForDataType();

    } catch (e) {
        console.error(e);
    }

    try {
        SetValidateForRequired();

    } catch (e) {
        console.error(e);
    }

    try {
        SetDefaultValues();

    } catch (e) {
        console.error(e);
    }

    try {
        SetHierarchicalFunctionally();

    } catch (e) {
        console.error(e);
    }

    try {
        SetMinValuesValidations();

    } catch (e) {
        console.error(e);
    }

    try {
        SetMaxValuesValidations();

    } catch (e) {
        console.error(e);
    }
    try {
        SetListFilters();

    } catch (e) {
        console.error(e);
    }

    try {


        if (SubmitButtonId) {
            $("#" + SubmitButtonId).click(function (evt) {
                if ($("#aspnetForm").valid != undefined) {
                    if (!$("#aspnetForm").valid()) {
                        evt.preventDefault();
                        setTimeout("parent.hideLoading();", 1000);
                    }
                }
            });
        }
    } catch (e) {
        console.error(e);
    }


    try {
        if (jQuery.data(document.forms[0], "validator") == null) {
            if ($('#aspnetForm').validate) $('#aspnetForm').validate({ onsubmit: false });
        }
        //Obtenemos la funcion de hightlight
        var validator = jQuery.data(document.forms[0], "validator");
        if (validator == undefined) return;

        var validatorHightlight = validator.settings.highlight;
        validator.settings.highlight = null;
        var i;

        validator.settings.highlight = function (a, b, d) {
            validatorHightlight(a, b, d);

            if (lastErrorElement != a.uniqueID || a[0] == undefined) {
                lastErrorElement = a.uniqueID;
                tabErrorSelected = false;
            }

            SelectErrorTab(a);
        };


        if (!validatorExtended) {
            validatorExtended = ExtendValidator();
        }

        if (SubmitButtonId) {
            var element = document.getElementById(SubmitButtonId);
            if (element) {
                var eClick = element.onclick;
                element.onclick = null;

                $(element).click(function (evt) {
                    if ($(this).attr("id").indexOf("WFExecution") > -1) {

                        if (DoRuleValidation(this, evt)) {
                            ShowLoadingAnimation();
                            if (eClick)
                                eClick();
                        }
                        else {
                            return false;
                        }
                    }
                    else {
                        if (DoGeneralValidation(this, evt)) {
                            if (eClick)
                                eClick();
                        }
                        else {
                            return false;
                        }
                    }
                });
            }
        }
    } catch (e) {
        console.error(e);

    }
}

function SetHeight() {
    var WindowHeight = $(document).outerHeight(true);
    var headerHeight = $("#rowTaskHeader").outerHeight(true);

    $("#rowTaskDetail").css("height", WindowHeight - headerHeight);
}

function SetValidateForLength() {
    $(".length").each(function () {
        if (!$(this).hasClass("readonly") && this.constructor.name != "Window" && $(this)[0].id != 'hdnRuleId') {
            var length = $(this).attr("length");
            if (length) {
                $(this).change(function (evt) {
                    if (!ValidateLength(this, length))
                        evt.preventDefault();
                });

                $(this).keypress(function (evt) {
                    if (!ValidateLength(this, length))
                        evt.preventDefault();
                });
                if ($(this).rules != undefined)
                    $(this).rules("add", {
                        maxlength: length,
                        messages: {
                            maxlength: jQuery.validator.format("Se ha excedido largo de " + length + " caracteres.")
                        }
                    });
            }
        }
    });
}

//$.validator.methods.number = function (value, element) {
//    return this.optional(element) || /\d{1,3}(\.\d{3})*(\.\d\d)?|\.\d\d/.test(value);
//}

function SetValidateForDataType() {
    try {
        SetValidations($(".dataType"));
        function SetValidations($elem) {
            try {
                $elem.each(function () {
                    if (!$(this).hasClass("readonly")) {
                        var dataType = $(this).attr("dataType");
                        if (dataType) {
                            $(this).change(function (evt) {
                                if (!KeyIsValid(evt, dataType, this))
                                    evt.preventDefault();
                            });

                            $(this).keypress(function (evt) {
                                if (!KeyIsValid(evt, dataType, this))
                                    evt.preventDefault();
                            });


                            switch (dataType) {
                                case "numeric":
                                    if ($(this).rules != undefined)
                                        $(this).rules("add", {
                                            digits: true,
                                            messages: {
                                                digits: jQuery.validator.format("Solo se permite un n&uacute;mero entero.")
                                            }
                                        });
                                    break;
                                case "date":
                                    if ($(this).rules != undefined)
                                        $(this).rules("add", {
                                            dateAR: true,
                                            messages: {
                                                dateAR: jQuery.validator.format("La fecha debe estar en formato:<br/> dia/mes/año.")
                                            }
                                        });
                                    MakeDateIndexs(this);
                                    break;
                                default:
                                    if ($(this).rules != undefined)
                                        $(this).rules("add", {
                                            number: true,
                                            messages: {
                                                number: jQuery.validator.format("Solo se permiten n&uacute;meros.")
                                            }
                                        });
                                    break;
                            }
                        }
                    }
                });
            } catch (e) {
                console.error(e);
            }
        }
    } catch (e) {
        console.error(e);
    }
}

function SetMinValuesValidations() {
    $(".haveMinValue").each(function () {
        applySetMinValuesValidations(this);
    });
    //$('.IFTaskContent').contents().children().find(".haveMinValue").each(function () {
    //    applySetMinValuesValidations(this);
    //});
}

function applySetMinValuesValidations(t) {
    if (!$(t).hasClass("readonly")) {
        var minValue = $(t).attr("ZMinValue");
        var dataType = $(t).attr("dataType");
        var aceptEquals = minValue.startsWith("=");
        minValue = minValue.replace("=", "");

        if (minValue) {
            // $(this).parents("form").validate();
            //Si existe la funcion para reemplazar con el dia de hoy

            if (minValue == "ZGetDate") {
                //  minValue = (new Date()).toISOString().split('T')[0];
                minValue = getLocaleShortDateString(new Date());// Se cambio porque tenia formato español y daba error en consola
            }
            // else //Se cambio formato por yyyy/MM/dd (14-04-2016)
            //  minValue= new Date(minValue).toISOString().split('T')[0];

            //Si es menor o igual
            if (aceptEquals) {
                $(t).rules("add", {
                    lessEqualThan: minValue,
                    messages: {
                        lessEqualThan: "El valor debe ser mayor o igual que: " + minValue
                    }
                });
            }
            else {
                $(t).rules("add", {
                    lessThan: minValue,
                    messages: {
                        lessThan: "El valor debe ser mayor que: " + minValue
                    }
                });
            }

            if (dataType == "date") {
                var minDate;
                if (aceptEquals)
                    minDate = CreateDate(Number(minValue.split("/")[0]), minValue.split("/")[1] - 1, minValue.split("/")[2]);
                else
                    minDate = CreateDate(Number(minValue.split("/")[0]) + 1, minValue.split("/")[1] - 1, minValue.split("/")[2]);

                $(t).datepicker("option", "minDate", minDate);
            }
        }
    }
}

function Hidden(objID) {
    var obj = document.getElementById(objID);
    var controlType = obj.nodeName.toLowerCase();

    if (obj) {
        switch (controlType) {
            case "input":
                switch (obj.type.toLowerCase()) {
                    case "checkbox":
                        $(obj).css("display", "none");
                        break;
                    case "text":
                        $(obj).parent().css("display", "none");
                        $(obj).parent().prev().css("display", "none");
                        break;
                    default:
                        $(obj).css("display", "none");
                }
                break;
            case "textarea":
            case "select":
                $(obj).css("display", "none");
                break;
        }
    }
}

function Visible(objID) {
    var obj = document.getElementById(objID);
    var controlType = obj.nodeName.toLowerCase();

    if (obj) {
        switch (controlType) {
            case "input":
                switch (obj.type.toLowerCase()) {
                    case "checkbox":
                        $(obj).css("display", "block");
                        break;
                    case "text":
                        $(obj).parent().css("display", "block");
                        $(obj).parent().prev().css("display", "block");
                        break;
                    default:
                        $(obj).css("display", "block");
                }
                break;
            case "textarea":
                $(obj).css("display", "block");
                break;
            case "select":
                $(obj).css("display", "block");
        }
    }
}

function SetMaxValuesValidations() {
    $(".haveMaxValue").each(function () {
        if (!$(this).hasClass("readonly")) {
            var maxValue = $(this).attr("ZMaxValue");
            var dataType = $(this).attr("dataType");
            var aceptEquals = maxValue.startsWith("=");
            maxValue = maxValue.replace("=", "");

            if (maxValue) {
                if (maxValue == "ZGetDate") {
                    maxValue = getLocaleShortDateString(new Date());
                }
                if (aceptEquals) {
                    if ($(this).rules != undefined)
                        $(this).rules("add", {
                            greaterEqualThan: maxValue,
                            messages: {
                                greaterEqualThan: "El valor debe ser menor o igual que: " + maxValue
                            }
                        });
                }
                else {
                    if ($(this).rules != undefined)
                        $(this).rules("add", {
                            greaterThan: maxValue,
                            messages: {
                                greaterThan: "El valor debe ser menor que: " + maxValue
                            }
                        });
                }
                if (dataType == "date") {
                    var maxDate;
                    if (aceptEquals)
                        maxDate = CreateDate(Number(maxValue.split("/")[0]), maxValue.split("/")[1] - 1, maxValue.split("/")[2]);
                    else
                        maxDate = CreateDate(Number(maxValue.split("/")[0]) - 1, maxValue.split("/")[1] - 1, maxValue.split("/")[2]);

                    $(this).datepicker("option", "maxDate", maxDate);
                }
            }
        }
    });
}

function CreateDate(day, month, year) {
    var d = new Date();
    d.setFullYear(year);
    d.setMonth(month);
    d.setDate(Number(day));
    return d;
}

function SetValidateForRequired() {
    $(".isRequired").each(function () {
        MakeRequired(this.id);
    });
}

function MakeRequired(idObj) {
    var obj = document.getElementById(idObj);

    if (obj) {
        var indexName = $(obj).attr("indexName");
        if (indexName) {
            if ($(obj).rules != undefined) {
                $(obj).rules("add", {
                    required: true,
                    messages: {
                        required: jQuery.validator.format("El campo " + indexName + " es requerido.")
                    }
                });
            }
        }
        else {
            if ($(obj).rules != undefined) {
                $(obj).rules("add", {
                    required: true,
                    messages: {
                        required: jQuery.validator.format("Por favor complete este campo.")
                    }
                });
            }
        }
    }
}

function MakeNonRequired(idObj) {
    //Para hacer no requerido se remueve la regla de requerido
    $("#" + idObj).rules("remove", "required");
}

function Enable(objID) {
    var obj = document.getElementById(objID);
    var controlType = obj.nodeName.toLowerCase();

    if (obj) {
        switch (controlType) {
            case "input":
                switch (obj.type.toLowerCase()) {
                    case "checkbox":
                        $(obj).removeAttr("disabled");
                        $(obj).removeClass("ReadOnly");
                        break;
                    default:
                        $(obj).removeAttr("readOnly");
                        $(obj).removeClass("ReadOnly");
                }
                break;
            case "textarea":
                $(obj).removeAttr("readOnly");
                $(obj).removeClass("ReadOnly");
                break;
            case "select":
                $(obj).removeAttr("disabled");
                break;
        }
    }
}

function Disable(objID) {
    var obj = document.getElementById(objID);
    var controlType = obj.nodeName.toLowerCase();

    if (obj) {
        switch (controlType) {
            case "input":
                switch (obj.type.toLowerCase()) {
                    case "checkbox":
                        $(obj).attr("disabled", "disabled");
                        $(obj).addClass("ReadOnly");
                        break;
                    default:
                        $(obj).attr("readOnly", "readOnly");
                        $(obj).addClass("ReadOnly");
                        $(obj).val('');
                }
                break;
            case "textarea":
                $(obj).attr("readOnly", "readOnly");
                $(obj).addClass("ReadOnly");
                $(obj).val('');
                break;
            case "select":
                $(obj).attr("disabled", "disabled");
                break;
        }
    }
}

function SetDefaultValues() {
    $(".haveDefaultValue").each(function () {
        var DefaultValue = $(this).attr("DefaultValue");
        if ($(this).val() == '')
            $(this).val(DefaultValue);
    });
}

function SetHierarchicalFunctionally() {
    $(".HierarchicalIndex").each(function () {
        $(this).change(function () {
            var childIndexId = $(this).attr("ChildIndexId");
            var parentIndexId = $(this).attr('id').split('_')[2];

            //Se obtienen los hijos
            var childIndexSplitted = childIndexId.split('|');
            if (childIndexSplitted.length > 1) {
                for (var i = 0; i < childIndexSplitted.length; i++) {
                    if (childIndexSplitted[i] && childIndexSplitted[i] != '') {
                        if (typeof $(this).val() == "string") {
                            GetHierarchyOptions(childIndexSplitted[i], parentIndexId, $(this).val());
                        }
                        else {
                            GetHierarchyOptions(childIndexSplitted[i], parentIndexId, $(this).val()[0]);
                        }
                    }
                }
            }
            else {
                if (typeof $(this).val() == "string") {
                    GetHierarchyOptions(childIndexSplitted[0], parentIndexId, $(this).val());
                }
                else {
                    GetHierarchyOptions(childIndexSplitted[0], parentIndexId, $(this).val()[0]);
                }
            }
        });
    });
}

function GetHierarchyOptions(IndexId, ParentIndexId, ParentValue, SenderID, event) {
    if (event !== undefined) {
        event.stopImmediatePropagation();
        event.preventDefault();
    }
    if (document.config == undefined) document.config = parent.document.config;
    var userId;

    var IndexIdTag = $("#zamba_index_" + IndexId);
    if (IndexIdTag) {

        userId = GetUID();

        var url;
        var params;
        if (SenderID) {
            url = thisDomain + "/Services/IndexService.asmx/GetHierarchyOptionsWidthID";
            params = { IndexId: IndexId, ParentIndexId: ParentIndexId, ParentValue: ParentValue, userId: userId, SenderID: SenderID };
        }
        else {
            url = thisDomain + "/Services/IndexService.asmx/GetHierarchyOptions";
            params = params = { IndexId: IndexId, ParentIndexId: ParentIndexId, ParentValue: ParentValue, userId: userId };
        }
        //parent.toastr.clear();
        //parent.toastr.info("Por favor aguarde unos instantes a que se complete los atributos", "Zamba");
        $.ajax({
            type: "POST",
            url: url,
            data: params,
            success: function (d) {
                IndexsCallback(xml2json(d));
                //parent.toastr.clear();
                //parent.toastr.success("Atributos cargados", "Zamba");
            },
            error: function (xhr, ajaxOptions, thrownError) {
                IndexsOnError(xhr);
            }
        });
        return true;
    }
    return true;
}

//Agrega una condicion dinamica.
function InjectCondition(idSource, idTarget, action, rollback, value, comparator) {
    var comparation;
    var comparateValue;
    //Valor a comparar
    var comparateSource = "$(this).val().toLowerCase()";

    //Si el valor viene con <Attribute> usar el valor del atributo en el form.
    if (value.indexOf("<Attribute>") > -1) {
        var attrID = "zamba_index_" + value.replace("<Attribute>(", "").replace(")", "");
        comparateValue = "$('#" + attrID + "').val().toLowerCase()";
    }
    else
        comparateValue = "'" + value.toLowerCase() + "'";

    //Si el comparador es comun, contruye la comparacion normalmente, sino le agrega su funcion
    switch (comparator) {
        case "==": case "!=": case "<": case ">": case "<=": case ">=":
            comparation = comparateSource + comparator + comparateValue;
            break;
        case "starts":
            comparation = comparateSource + ".startsWith(" + comparateValue + ")";
            break;
        case "ends":
            comparation = comparateSource + ".endsWith(" + comparateValue + ")";
            break;
        case "in":
            comparation = comparateSource + ".contains(" + comparateValue + ")";
            break;
        case "into":
            comparation = comparateSource + ".into(" + comparateValue + ")";
            break;
        case "notInto":
            comparation = comparateSource + ".notInto(" + comparateValue + ")";
            break;
    }

    var objSource = document.getElementById(idSource);
    //Si el campo se encuentra
    if (objSource) {
        //Si el input se puede ingresar datos tecleando, agregar la condicion
        if (objSource.nodeName.toLowerCase() == "input" ||
            objSource.nodeName.toLowerCase() == "textarea") {

            $(objSource).keyup(function () {
                //Evalua la comparacion
                if (eval(comparation)) {
                    //Ejecuta la accion
                    eval(action + "('" + idTarget + "')");
                }
                else {
                    //Ejecuta el rollback
                    eval(rollback + "('" + idTarget + "')");
                }
            });
        }
        $(objSource).change(function () {
            if (eval(comparation)) {
                eval(action + "('" + idTarget + "')");
            }
            else {
                eval(rollback + "('" + idTarget + "')");
            }
        });

        $(window).on("load", function () {
            $("#" + idSource).change();
        });
    }
}

function SetListFilters() {
    //Obtiene todos los tags select
    $('select:not(:disabled)').each(function () {
        //Verifica que lo que encontro sea un índice
        if ($(this).attr('id') != undefined && $(this).attr('id').toLowerCase().indexOf('zamba_index_') > -1 && !$(this).hasClass("readonly") && $(this).css('display') != 'none' && $(this).hasClass("addFilterSearch")) {
            //Agrega la lupa para filtrar y buscar valores
            $(this)//.attr('id').onclick(CreateTable(this, false));
                .click(function () { CreateTable(this, false) });
            //AddFilter($(this).attr('id'), false);
        }
    });
}

function IndexsCallback(result) {
    var idReturned = result.toString().split('|')[0];
    var childIndexId;

    if (isNaN(idReturned))
        childIndexId = idReturned;
    else
        childIndexId = "zamba_index_" + result.toString().split('|')[0];

    var indexOptions = result.toString().split('|')[1];
    var childIndexSelect = document.getElementById(childIndexId);

    if (childIndexSelect && childIndexSelect.nodeName && childIndexSelect.nodeName.toLowerCase() != "input") {
        $('#' + childIndexId).html(indexOptions);
        $('#' + childIndexId).change();
    }
}

function IndexsOnError(result) {
    toastr.error("Error: " + result.get_message());
}

function ExecuteFormReadyActions() {
    try {
        var btnzamba_showOriginal = $("#zamba_showOriginal")
        if (btnzamba_showOriginal != null) {
            $("#zamba_showOriginal").click(function () {
                ShowLoadingAnimation();
            });
        };
    } catch (e) {
        console.error(e);
    }


    try {

        $("#zamba_save").click(function () {
            ShowLoadingAnimation();
        });
    } catch (e) {
        console.error(e);
    }


    //Obtiene los textbox de los formularios, y les agrega en el foco la seleccion del texto.
    //Con esto se deberia solucionar el problema del "bloqueo de los textbox".
    try {
        FixFocusError();
    } catch (e) {
        console.error(e);
    }


    // if ($('#aspnetForm').validate) $('#aspnetForm').validate({ onsubmit: false });
    try {
        SetValidationsAction("zamba_save");
    } catch (e) {
        console.error(e);
    }

    try {
        ajustarselects();
    } catch (e) {
        console.error(e);
    }



    try {

        $("input[id^=zamba_rule_]").each(function () {
            if ($(this).hasClass("DontSave")) {
                $(this).click(function () {
                    ShowLoadingAnimation();
                    $("#hdnRuleActionType").val("DontSave");
                    document.ExecuteValidations = false;
                });
            }
            else {
                $(this).click(function () {
                    ShowLoadingAnimation();
                    $("#hdnRuleActionType").val("Save");
                    document.ExecuteValidations = true;
                });
            }
        });

        $("button[id^=zamba_rule_]").each(function () {
            if ($(this).hasClass("DontSave")) {
                $(this).click(function () {
                    ShowLoadingAnimation();
                    $("#hdnRuleActionType").val("DontSave");
                    document.ExecuteValidations = false;
                });
            }
            else {
                $(this).click(function () {
                    ShowLoadingAnimation();
                    $("#hdnRuleActionType").val("Save");
                    document.ExecuteValidations = true;
                });
            }
        });


    } catch (e) {
        console.error(e);
    }


    try {
        //Inicializar los combos dependientes de zvar
        $(".zvarDropDown").zvarDropDown();
    } catch (e) {
        console.error(e);
    }

    try {
        ////Inicializar los tablas dependientes de zvar o ajax
        $(".zAjaxTable").zAjaxTable();
    } catch (e) {
        console.error(e);
    }

    try {
        ////Inicializar los autocomplete
        $(".zAutocomplete").zAutocomplete();
    } catch (e) {
        console.error(e);
    }

    try {
        getEnableButton();
    } catch (e) {
        console.error(e);
    }

    try {
        getEditGroups();
    } catch (e) {
        console.error(e);
    }
}

function disableOperationsMomentarily(elem) {
    elem.value = "Procesando...";
    //$(elem).prop("disabled", true);
    $(elem).removeAttr("click");
    $(elem).unbind("click");
    document.getElementById(elem.id).removeAttribute("onclick");
}

document.ExecuteValidations = true;

//Calendars
function MakeDateIndexs(item) {
    if (!$(item).hasClass("ReadOnly") && !$(item).hasClass("hasDatepicker")) AddCalendar(item.id);
}



// Se encarga de corregir los problemas de foco al hacer postback que pasa en formularios.
// Los input de tipo text y los textarea deben implementar la clase readOnly y tener el
// atributo readonly="readonly" para que funcione. No se debe usar el disabled.
function FixFocusError() {
    //Obtiene los textbox de los formularios, y les agrega en el foco la seleccion del texto.
    //Con esto se deberia solucionar el problema del "bloqueo de los textbox".
    $("input[type=text]").focus(function () {
        if (!$(this).hasClass("hasDatepicker")) {
            $(this).select();
            if (typeof ($(this).caret) != "undefined") {

                //////!!!! El metodo se deja comentado ya que devuelve siempre este error:!!!!/////////

                // Uncaught TypeError: Cannot read property 'msie' of undefined


                //Esto es para intentar solucionar nuevamente el error de foco

                //at r.fn.init.n.fn.caret(VM8704 jqueryAddIns: 1)
                //$(this).caret().start = 0;
                //$(this).caret().end = 0;
            }
        }
    });

    //Obtiene los textbox de los formularios, y les agrega en el foco la seleccion del texto.
    //Con esto se deberia solucionar el problema del "bloqueo de los textbox".
    $("textarea").focus(function () {
        $(this).select();
        if (typeof ($(this).caret) != "undefined") {
            //Esto es para intentar solucionar nuevamente el error de foco
            $(this).caret().start = 0;
            $(this).caret().end = 0;
        }
    });
}

function SetIndexPnlVisibility(obj, divDoc, panel2Indices) {
    var divIndice = document.getElementById("DivIndices");
    var btnOcultar = document.getElementById("btnOcultar");
    var btnVisualizar = document.getElementById("btnVisualizar");
    var tableIndice = $("#ctl00_ContentPlaceHolder_ucTaskDetail_ctl00_completarindice_tblIndices").width();
    var a;
    var scrollDifference;
    if ($(obj).hasClass("Expand")) {
        $(panel2Indices).css("overflow", "auto");
        scrollDifference = $(divIndice)[0].scrollWidth - tableIndice;
        //Muestra el panel de índices
        $(divIndice).animate({ left: 0 }, "slow");

        //Calcula el espacio necesario para desplazar  el documento
        a = $(divDoc).width() - $(divIndice).width();

        //Hace los desplazamientos con animacion       
        $("#separator").animate({ width: $(divIndice).width() }, "slow");
        $(divDoc).animate({ width: a + 16 + scrollDifference }, "slow");

        $(obj).addClass("Collapse");
        $(obj).removeClass("Expand");
    }
    else {
        //Oculta el panel de índices
        $(panel2Indices).css("overflow", "hidden");
        scrollDifference = $(divIndice)[0].scrollWidth - tableIndice;
        a = 0 - $(divIndice).width() + 16 + scrollDifference;

        //Hace la animacion
        $(divIndice).animate({ left: a }, "slow");

        //Acomoda el documento 
        $("#separator").animate({ width: 16 }, "slow");
        a = $(divDoc).width() + $(divIndice).width() - 16 + scrollDifference;
        $(divDoc).animate({ width: a }, "slow");

        $(obj).addClass("Expand");
        $(obj).removeClass("Collapse");

    }
}

//Funcion que agrega un filtro al control
//indexid = id del tag que contiene el indice a aplicar el control
// codecolumn = true/false si se quiere visualizar o no la columna de codigo
function AddFilter(indexid, codecolumn) {

    //var btnid = indexid.toString().split('_')[indexid.toString().split('_').length - 1];
    //var $btnfilter = $('<img id="table_filter_' + btnid + '" onclick="CreateTable(this,' + codecolumn + ');" style="height: 16px; margin-left:10px; display: none;" alt="Filtrar" src="../../Content/Images/UI_blue/search.gif"/>');
    //$("#" + indexid).after($btnfilter);
}
function CreateTable(obj, codecolumn) {
    //parent.ShowLoadingAnimation();
    var winHeight = $(window).height();
    //Creando el nombre del select
    var arrayObj = obj.id.toString().split('_');
    var cBox = "zamba_index_" + arrayObj[arrayObj.length - 1].trim();
    $('#dynamic_filter').data("indexid", cBox);

    var table = LoadTable3(cBox, codecolumn);
    //Limpiando la tabla
    $('#dynamic_filter').html("");

    var initModal = '<div class="modal fade" id="openModalSearch" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">' +
        '<div class="modal-dialog">' +
        ' <div class="modal-content" id="openModalIFSearch">' +
        ' <div class="modal-header">' +
        '<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>' +
        '<h4 class="modal-title" id="modalFormTitleSearch"></h4>' +
        '</div>' +
        '<div class="modal-body" id="">' +
        '<div id="modalDivBodySearch">';
    var endModal = '</div> </div> </div> </div>';

    //Agregando los botones
    var html = initModal + table;

    html += '<center><input id="btnAceptDynamic" type="button" class="btn btn-success" value="Aceptar" onclick="Accept()" />';
    html += '<input id="btnCancelDynamic" type="button" value="Cancelar" class="btn btn-info" style=" margin-left:10px;" onclick="Cancel()" /></center>';
    html += endModal;

    //Agregando la tabla creada dinamicamente
    $('#dynamic_filter').html(html);

    $("#example tbody").click(function (e) { $(oTable.fnSettings().aoData).each(function () { $(this.nTr).removeClass('row_selected'); }); $(e.target.parentNode).addClass('row_selected'); });

    //Agregando el evento click a cada fila
    $('#example tr').click(function () {
        if ($(this).hasClass('row_selected'))
            $(this).removeClass('row_selected');
        else
            $(this).addClass('row_selected');
    });

    /* Add a click handler for the delete row */
    $('#delete').click(function () {
        var anSelected = fnGetSelected(oTable);
        oTable.fnDeleteRow(anSelected[0]);
    });

    /* Init the table */
    var oTable = $('#example').dataTable({ "bPaginate": false, "bLengthChange": false, "bInfo": false, "bAutoWidth": false, "bSort": false });

    //Calculando heights
    var popupHeight = selectHeight >= winHeight ? winHeight - 45 : selectHeight + 45;
    var tableHeight = selectHeight > popupHeight - 70 ? popupHeight - 70 : selectHeight;

    if (tableHeight <= 0) {
        tableHeight = 30;
    }
    selectHeight = selectHeight > 1400 ? selectHeight / 10 : selectHeight;


    if (popupHeight > $(window).width() - 600) {
        $("#dynamic_filter").css('height', selectHeight - 77 + 'px');
        $("#exampleContainer").css('height', selectHeight - 160 + 'px');
        popupHeight = tableHeight - 130;
    }
    else {
        popupHeight = tableHeight - 130;
    }

    //Abriendo el  modal
    $("#example_wrapper").height(tableHeight);
    if (selectHeight > winHeight - 70) {
        tableHeight = winHeight - 100;

        $("#exampleContainer").height(tableHeight);
    }
    $("#dynamic_filter").css('height', '');
    $("#openModalSearch").modal();
    $("#openModalSearch").draggable();

    setTimeout("parent.hideLoading();", 500);
}
function Cancel() {
    $("#openModalSearch").modal('hide');
}

function Accept() {
    var rows = $('#example tbody tr');
    var value = "";
    for (var i = 0; i < rows.length; i++) {
        if ($(rows[i]).hasClass('row_selected')) {
            value = rows[i];
            value = $(value).find("td")[0].innerHTML;
        }
    }
    if (value == "") {
        toastr.error("Por favor seleccione una fila");
    }
    else {
        $('#' + $('#dynamic_filter').data("indexid") + ' option').each(
            function () {
                if ($(this).val().toString().trim() == value.toString().trim()) {
                    $(this).attr("selected", "selected");
                    $(this).parent().change();
                    $(this).parent().valid();
                }
            });
        Cancel();
    }
}
/*
* I don't actually use this here, but it is provided as it might be useful and demonstrates
* getting the TR nodes from DataTables
*/
function fnGetSelected(oTableLocal) {
    var aReturn = new Array();
    var aTrs = oTableLocal.fnGetNodes();

    for (var i = 0; i < aTrs.length; i++) {
        if ($(aTrs[i]).hasClass('row_selected')) {
            aReturn.push(aTrs[i]);
        }
    }
    return aReturn;
}

var selectHeight;

function LoadTable3(cbox, codecolumn) {
    selectHeight = 0;
    var t = "";
    var select = $("#" + cbox + " option");
    t = '<div id="exampleContainer" style="overflow:auto;height:340px !important"><table cellpadding="0" cellspacing="0" border="0" class="display" id="example" style="height:60px;">';

    //Cabecera
    t += "<thead>";
    t += "<tr>";
    if (codecolumn)
        t += "<th>Codigo</th>";
    else
        t += '<th style="display:none;">Codigo</th>';
    t += "<th>Descripcion</th>";
    t += "</tr>";
    t += "</thead>";

    //Cuerpo
    t += "<tbody>";
    for (var i = 0; i < select.length; i++) {
        t += '<tr class="gradeA">';
        if (codecolumn) {
            t += "<td>";
        } else {
            selectHeight = selectHeight + 20;
            t += '<td style="display:none;">';
            t += select[i].value;
            t += "</td>";
            t += "<td>";
            t += select[i].text;
            t += "</td>";
            t += "</tr>";
        }
    }
    t += "</tbody>";

    //Final
    t += "<tfoot>";
    t += "<tr>";
    t += "</tr>";
    t += "</tfoot>";
    t += "</table></div>";

    return t;
}
var TaskIds = {
    Url: function () {
        return $("iframe#IFTaskContent").attr("src") || parent.$("iframe#IFTaskContent").attr("src") || "";
    },
    DocTypeId: function () {
        return this.GetParam("doctype=");
    },
    DocId: function () {
        return this.GetParam("docid=");
    },
    TaskId: function () {
        return this.GetParam("taskid=");
    },
    WFStepId: function () {
        return this.GetParam("wfstepid=");
    },
    GetParam: function (p) {
        var cleanUrl = this.Url().substring(this.Url().indexOf(p)).replace(p, "");
        var r = new RegExp(/^\d+/).exec(cleanUrl);
        return r == null ? "" : r[0];
    }
}

var TaskOptions = {

    SetFav: function (btn) {
        var setFav = $(btn).attr("isset").toLowerCase() == "true" || false;
        toastr.info(setFav ? "se quito de favoritos" : "se agrego a favoritos", "Zamba");
        //KeepTaskOptions("updatefavorite", TaskIds.DocId(), TaskIds.DocTypeId(), !setFav);
        KeepTaskOptions("updatefavorite", getParameterByName("docid"), getParameterByName("DocType"), !setFav);
        $(btn).attr({ "isset": !setFav, "title": setFav ? "Agregar a favoritos" : "Quitar de favoritos" });
        $(btn).children("span").removeClass()
            .addClass(setFav ? "fa fa-heart-o " : "fa fa-heart ");
    },
    SetImportant: function (btn) {
        var setImp = $(btn).attr("isset").toLowerCase() == "true" || false;
        toastr.info(setImp ? "se quito de importantes" : "se agrego a importantes", "Zamba");
        KeepTaskOptions("UpdateImportant", GetDOCID(), GetDocTypeId(), !setImp);
        $(btn).attr({ "isset": !setImp, "title": setImp ? "Marcar como importante" : "Desmarcar importante" });
        $(btn).children("span").removeClass()
            .addClass(setImp ? "fa fa-star-o " : "fa fa-star ");
    },
    AddBCHistory: function (btn) {
        parent.bootbox.dialog({
            message: "<form id='TNfrm' action=''>\
            <textarea type='text' id='textAreaTaskBCHistory' style='max-width: 560px;' class='form-control' rows='8'/>\
            </form>",
            title: "Novedades",
            buttons: [
                {
                    label: "Guardar",
                    className: "btn btn-primary",
                    callback: function (e) {
                        var result = $(e.target).parents(".modal").find("#textAreaTaskBCHistory");
                        if (result == null) return;
                        if (result.val() == "") {
                            toastr.info("No se ingreso la novedad ya que no contenia texto");
                        }
                        else {
                            KeepTaskOptions("addBCHistory", TaskIds.DocId(), TaskIds.DocTypeId(), result.val());
                        }
                    }
                }],
            callback: function () {
                console.log("Hi " + $('#first_name').val());
            }
        });
    },
}

function KeepTaskOptions(type, docId, docTypeId, val) {
    var genericRequest = {
        UserId: parseInt(GetUID()),
        Params: {
            "docId": docId,
            "docTypeId": docTypeId,
            "val": val
        }
    };

    $.ajax({
        type: "POST",
        url: ZambaWebRestApiURL + "/taskoptions/" + type,
        data: JSON.stringify(genericRequest),
        contentType: "application/json; charset=utf-8",
        //  dataType: "json",
        success: function (msg) {
            var a = msg;
        },
        error: function (request, status, err) {
            var a = request;
        }
    });
}

// 07/08/2012 Javier Se modifica funcion para que tambien acepte una url.
function getParameterByName(name, url) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results;

    if (url != undefined)
        results = regex.exec(url);
    else
        results = regex.exec(window.location.search);

    if (results == null)
        return "";
    else
        return decodeURIComponent(results[1].replace(/\+/g, " "));
}

function CheckIfOpenTask() {
    var taskid = getParameterByName("taskid");
    var docid = getParameterByName("docid");
    var doctypeid = getParameterByName("doctype");
    var docname = getParameterByName("docname");
    var url = "../WF/TaskSelector.ashx?TaskId=" + taskid + "&DocId=" + docid + "&DocTypeId=" + doctypeid;
    var userID = document.getElementById('ctl00_hdnUserID').value;

    if (!taskid || taskid == "") taskid = 0;
    if (!docid || docid == "") docid = 0;

    if (taskid != 0 || docid != 0)
        OpenDocTask3(taskid, docid, false, docname, url, userID);
}


//-----------------------------------------------------------------------------------------------------//
//Variables para abir documento
var urlToOpen = '';
var docNameToOpen = '';
var lsTabs = []; //Lista de tareas o documentos para mostrar

function AddDocTaskToOpenList(taskID, docID, docTypeId, asDoc, docName, url, userID) {

    try {
        if (!taskID || taskID == '')
            taskID = 0;
        else
            taskID = parseInt(taskID);

        if (!docID || docID == '')
            docID = 0;
        else
            docID = parseInt(docID);

        if (!userID || userID == '')
            userID = '';
        else
            userID = parseInt(userID);
    } catch (e) {
        toastr.error("Error al abrir el documento. Mensaje: " + e);
    }

    var newTab = {
        taskID: taskID,
        docID: docID,
        docTypeId: docTypeId,
        asDoc: asDoc,
        docName: docName,
        url: url,
        userID: userID
    };
    lsTabs.push(newTab);
}

function OpenPendingTabs(AskUser) {
    if (lsTabs.length == 0) { return false; } // No hay tareas para abrir
    var tasks = lsTabs;
    if (AskUser && AskUser == false) {
        $('.modal-title').html('Abrir ' + ((tasks.length >= 2) ? (tasks.length) + ' tareas' : 'tarea'));
        //Div modal bootstrap
        $('#openTasks').on('show.bs.modal', function (e) {

            var task = '<strong>';
            for (var i = 0; i <= tasks.length - 1; i++)
                task += '<li>' + tasks[i].docName + '</li></br>';
            task += '</strong>';
            $('.tareasDiv').html(task);

            if (tasks.length >= 2) {
                $("#subTitleModal").html('Se cerraron inesperadamente las siguientes tareas:');
                $("#questionModal").html(" ¿Desea volver a abrirlas?");
            }
            else {
                $("#subTitleModal").html('Se cerró inesperadamente la siguiente tarea:');
                $("#questionModal").html(" ¿Desea volver a abrirla?");
            }

            //Ok boton modal bootstrap
            $(this).find('.btn-ok').click(function (e) {
                try {
                    $('#openTasks').modal('hide');
                    $('#NoTaskDiv').css('display', 'none');

                    OpenNewTab(lsTabs[0]);

                } catch (e) {
                }
            });
        });

        $('#openTasks').modal().draggable();
    }
    else {
        OpenNewTab(lsTabs[0]);
    }
    return false;
}




function OpenTaskWithoutTab(taskID, docID, docTypeId, asDoc, docName, url, userID) {

    var Url = (thisDomain + "/views/WF/TaskSelector.ashx?DocTypeId=" + result.DOC_TYPE_ID + "&docid=" + result.DOC_ID + "&taskid=" + result.TASK_ID
        + "&wfstepid=" + stepid + "&userId=" + userid);
    window.open(Url, "R" + result.DOC_ID);
}


function OpenDocTask2(taskID, docID, docTypeId, asDoc, docName, url, userID, wfstepid) {
    //ShowLoadingAnimation();
    //Completo las variables para abrir doc
    urlToOpen = url;
    docNameToOpen = docName;

    try {
        if (!taskID || taskID == '')
            taskID = 0;
        else
            taskID = parseInt(taskID);

        if (!docID || docID == '')
            docID = 0;
        else
            docID = parseInt(docID);

        if (!userID || userID == '')
            userID = '';
        else
            userID = parseInt(userID);

    } catch (e) {
        toastr.error("Error al abrir el documento. Mensaje: " + e);
    }

    try {
        //$('#NoTaskDiv').css('display', 'none');
        //if (docID != 0 && docName != '') {
        //    GetIDSuccess('D' + docID + '|' + docName);
        //}
        //else if (taskID != 0) {
        //    GetIDSuccess('T' + taskID + '|' + docName);
        //}
        //else {
        //    //ShowLoadingAnimation();
        //    $.ajax({
        //        type: "POST",
        //        url: "../../Services/TaskService.asmx/GetTabID",
        //        data: "{ taskID: " + taskID + ", docID: " + docID + ", docTypeId: " + docTypeId + ",asDoc: " + asDoc + ", userID: " + userID + "}",
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (msg) {
        //            GetIDSuccess(msg.d);
        //            //hideLoading();
        //        },
        //        error: function (request, status, err) {

        //            toastr.error("Error al abrir las tareas \r" + request.responseText);
        //            GetIDError(request, status, err)
        //            //hideLoading();
        //        }
        //    });

        //}

        var Url = urlToOpen;
        if (taskID > 0 && Url.indexOf("TaskSelector") <= 0 && Url.indexOf("taskselector") <= 0) {
            Url = ("../../views/WF/TaskSelector.ashx?DocTypeId=" + docTypeId + "&docid=" + docID + "&taskid=" + taskID + "&wfstepid=" + wfstepid
                + "&userId=" + userID);
        }

        window.open(Url, "R" + docID);

    }
    catch (e) {
    }

    return false;
}




function OpenDocTask3(taskID, docID, docTypeId, asDoc, docName, url, userID, wfstepid, openmode, formId, modalFormId) {
    urlToOpen = url;
    docNameToOpen = docName;

    let userToken = JSON.parse(localStorage.getItem('authorizationData'));
    let { token } = userToken;

    try {
        if (!taskID || taskID == '')
            taskID = 0;
        else
            taskID = parseInt(taskID);

        if (!docID || docID == '')
            docID = 0;
        else
            docID = parseInt(docID);

        if (!userID || userID == '')
            userID = '';
        else
            userID = parseInt(userID);

    } catch (e) {
        toastr.error("Error al abrir el documento. Mensaje: " + e);
    }

    try {
        var Url = urlToOpen;
        if ((taskID > 0 && Url.indexOf("TaskSelector") <= 0 && Url.indexOf("taskselector") <= 0) || (Url.indexOf("..") >= 0)) {
            Url = (thisDomain + "/views/WF/TaskSelector.ashx?DocTypeId=" + docTypeId + "&docid=" + docID + "&taskid=" + taskID + "&wfstepid=" + wfstepid
                + "&user=" + userID + "&f=" + formId + "&mf=" + modalFormId + "&t=" + token);
        }

        if (openmode == undefined) {
            openmode = 0;
        }

        if (openmode == 0) {
            window.open(Url, "R" + docID);
        }

        if (openmode == 1 && !(urlToOpen.indexOf("breakmodal=1") > 0)) {
            Url += "&modalmode=true&breakmodal=1";

            var modalIframe = $("#iframeDoShowForm");
            var modalDialog = $("#modalDoShowForm");

            if (typeof $().modal != 'function')
                reloadBootstrap();

            ModalSizeDoShowForm(modalIframe, modalDialog);

            modalDialog.modal();
            modalDialog.draggable();
            modalIframe.attr("src", Url);

        }

        if (openmode == 2) {
            window.open(Url, '_self');
            //            ShowIFrameHome(Url, 600);
        }

        // Nueva ventana
        if (openmode == 3) {
            Url += "&Ed=true";
            window.open(Url, "R" + docID);
        }

        if (openmode == 4) {
            window.open(Url, '_self');
        }
    }
    catch (e) {
    }

    return false;
}

function ModalSizeDoShowForm(modalIframe, modalDialog) {
    var modalDialogHome = $("#modalDoShowFormHome");
    var standarWinScreenHeight = 768;
    var standarWinScreenWidth = 1366;
    var standarHeight = 500;
    var standarWidth = 600;

    var height = window.screen.height * standarHeight / standarWinScreenHeight;
    var width = window.screen.width * standarWidth / standarWinScreenWidth;

    var heightIF = modalIframe.contents().children().height();
    if (heightIF == undefined || heightIF == 0 || heightIF < height)
        heightIF = height;
    modalDialog.css("height", (heightIF + 70) + "px");
    modalDialogHome.css("height", (heightIF) + "px");

    var widthIF = modalIframe.contents().children().width();
    if (widthIF == undefined || widthIF == 0 || widthIF < width)
        widthIF = width;
    modalDialog.css("width", widthIF + "px");
}

function OpenDocTaskWithOutReference(taskID, docID, docTypeId, asDoc, docName, url, userID, wfstepid, openmode) {
    //ShowLoadingAnimation();
    //Completo las variables para abrir doc
    urlToOpen = url;
    docNameToOpen = docName;

    try {
        if (!taskID || taskID == '')
            taskID = 0;
        else
            taskID = parseInt(taskID);

        if (!docID || docID == '')
            docID = 0;
        else
            docID = parseInt(docID);

        if (!userID || userID == '')
            userID = '';
        else
            userID = parseInt(userID);

    } catch (e) {
        toastr.error("Error al abrir el documento. Mensaje: " + e);
    }

    try {

        var Url = urlToOpen;
        if (taskID > 0 && Url.indexOf("TaskSelector") <= 0 && Url.indexOf("taskselector") <= 0) {
            Url = (thisDomain + "/views/WF/TaskSelector.ashx?DocTypeId=" + docTypeId + "&docid=" + docID + "&taskid=" + taskID + "&wfstepid=" + wfstepid
                + "&userId=" + userID);
        }

        if (openmode == undefined) {
            openmode = 0;
        }

        if (openmode == 0) {
            //                                    case OpenType.NewTab:
            window.open(Url, "R" + docID, 'noopener');
        }
        //case OpenType.Modal:
        if (openmode == 1) {
            ShowIFrameModal2('', Url, 800, 600);
        }
        //                          case OpenType.Home:
        if (openmode == 2) {
            ShowIFrameHome(Url, 600);
        }
        //                        case OpenType.NewWindow:
        if (openmode == 3) {
            window.open(Url, "R" + docID, 'noopener');
        }
    }
    catch (e) {
    }

    return false;
}



//Nuevo metodo para apertura de tareas en pestña, en base al id devuelto del WS
//function GetIDSuccess(result) {
//    var tabID = result.split('|')[0];
//    var tabName = result.split('|')[1];

//    if (tabName == "" || tabID == "") {
//        toastr.error('El documento es inexistente o no se tiene permiso para acceder al mismo');
//        hideLoading();
//    }
//    else {
//        var divTasks;
//        var mainTabber;

//        //Verifica si el codigo es ejecutado desde la web en general o desde dentro de una tarea
//        if ($('#TasksDiv').length) {
//            divTasks = $('#TasksDiv');
//            mainTabber = $('#MainTabber');
//        } else {
//            divTasks = window.parent.$('#TasksDiv');
//            mainTabber = window.parent.$('#MainTabber');
//        }

//        if (parent.document.getElementById(tabID) == null) {
//            var masterHeader;
//            var barraCabecera;
//            var mainmenu;

//            if ($("#MasterHeader").length) {
//                masterHeader = $("#MasterHeader").css("display") == "none" ? 0 : $("#MasterHeader").height();
//                barraCabecera = $("#barra-Cabezera").css("display") == "none" ? 0 : $("#barra-Cabezera").innerHeight();
//                mainmenu = $("#mainMenu").css("display") == "none" ? 0 : $("#mainMenu").innerHeight();
//            } else {
//                masterHeader = window.parent.$("#MasterHeader").css("display") == "none" ? 0 : window.parent.$("#MasterHeader").height();
//                barraCabecera = window.parent.$("#barra-Cabezera").css("display") == "none" ? 0 : window.parent.$("#barra-Cabezera").innerHeight();
//                mainmenu = window.parent.$("#mainMenu").css("display") == "none" ? 0 : window.parent.$("#mainMenu").innerHeight();
//            }

//            var divtag = '<div id="' + tabID + '" class="task-tab-div"><iframe id="IFTaskContent" class="IFTaskContent" frameborder="0" src="' + urlToOpen + '"/><div/>';
//            //var divtag = '<div id="' + tabID + '" class="task-tab-div"><div id="IFTaskContent" class="IFTaskContent" frameborder="0" src="' + urlToOpen + '"</div><div/>';

//            //$(divTasks).html(data);
//            $(divtag).appendTo(divTasks);

//            $('#IFTaskContent').on("load", function (e) {
//                parent.SetFieldsValidations();
//                //SetValidateForLength();
//                SetValidateForDataType();
//                SetValidateForRequired();
//                SetDefaultValues();
//                SetHierarchicalFunctionally();
//                SetMinValuesValidations();
//                SetMaxValuesValidations();
//            });
//            divTasks.zTabs("add", tabID, tabName);
//        }
//        if ($('#liTasks').css('display') === 'none') {
//            $('#liTasks').css('display', 'block');
//        }
//        divTasks.zTabs('select', tabID);
//        mainTabber.zTabs("select", 'tabtasks');
//    }

//    urlToOpen = '';
//    docNameToOpen = '';
//}

function ReloadIFTaskContent(tabID) {
    var IFTC = typeof (tabID) == "object" ? tabID.children : tabID;//"#IFTaskContent"
    if ($(IFTC).contents().find('body').children().length || thisDomain == undefined) {
        hideLoading();
        return;
    }
    var currentSrc = $(IFTC).attr("src");
    if (currentSrc.substring(0, 2) == "..")
        currentSrc = currentSrc.substring(3);

    if (currentSrc.toLowerCase().substring(0, 5) != "views" && currentSrc.toLowerCase().indexOf(thisDomain.toLowerCase()) == -1)
        currentSrc = "Views/" + currentSrc;
    else
        currentSrc = currentSrc.toLowerCase().replace(thisDomain.toLowerCase(), '');

    $(IFTC).attr("src",
        (thisDomain.substring(thisDomain.length - 1) != "/") ? thisDomain + "/" + currentSrc : thisDomain + currentSrc);
}
//Error en WS
function GetIDError(result) {
    toastr.error("Error al abrir el documento. Mensaje: " + result.get_message());
}

function parseDate(input) {
    var parts = input.match(/(\d+)/g);
    if (parts == null)
        return null;

    var days = parts[0];
    var month = parts[1] - 1;
    var year = parts[2];

    // new Date(year, month [, date [, hours[, minutes[, seconds[, ms]]]]])
    var d = new Date(year, month, days);

    //Si los dias,meses y años de entrada coinciden con los de salida, entonces la fecha es valida
    if (d && d.getMonth() == month && d.getDate() == days && d.getFullYear() == year) {
        return d;
    }
    else
        return null;
}

function SetLoadingAnimationObserver() {
    if (document.firstLoad == true) {
        var allOfThem = document.getElementsByTagName('*');
        var i = 0;
        if (document.isLoading == true) {
            var stopLoadingAnimation = true;
            var currElement;
            while (allOfThem[i] != null && stopLoadingAnimation == false) {
                currElement = allOfThem[i];
                if (currElement.readyState != "interactive " && currElement.readyState != "complete") {
                    stopLoadingAnimation = false;
                }
                i++;
            }
            if (stopLoadingAnimation == true) {
                document.firstLoad = false;
                hideLoading();
            }
            else {
                setTimeout("SetLoadingAnimationObserver();", 1000);
            }
        }
        else {
            document.firstLoad = false;
            hideLoading();
        }
    }
}

var ZDoc = document;
function StartObjectLoadingObserverById(objEjementId) {
    var element = ZDoc.getElementById(objEjementId);
    StartObjectLoadingObserver(element);
}
function StartObjectLoadingObserver(objEjement) {
    if (objEjement == undefined || objEjement.readyState == undefined)
        AddReadyState(objEjement);

    if (!ZDoc.arrLoadingElements) {
        ZDoc.arrLoadingElements = new Array();
    }
    ZDoc.arrLoadingElements.push(objEjement);

    if (!ZDoc.pendingObjectsObserverStarted) {
        StartPendingObjectsObserver();
    }
}

function AddReadyState(obj) {

    if (obj != null && obj != undefined) {

        obj.readyState = "loading";
        $(obj).on("load", function () {
            this.readyState = "complete";
        });
    }
}

function StartPendingObjectsObserver() {
    ZDoc.pendingObjectsObserverStarted = true;
    var pendingObjects = ZDoc.arrLoadingElements;

    if (pendingObjects) {
        var iterator = pendingObjects.length - 1;
        var itemDeleted;

        while (iterator > -1) {
            itemDeleted = false;

            if (!pendingObjects[iterator]) {
                document.arrLoadingElements.splice(iterator, 1);
            }
            else {
                if ((pendingObjects[iterator].readyState != "interactive " && pendingObjects[iterator].readyState != "complete") && pendingObjects[iterator].id != 'ctl00_WFExecForEntryRulesFrame') {
                    ShowLoadingAnimation();
                    setTimeout("StartPendingObjectsObserver()", 1000);
                    return;
                }
                else {
                    document.arrLoadingElements.splice(iterator, 1);
                    itemDeleted = true;
                }
            }
            iterator--;
        }

        if (document.arrLoadingElements.length == 0) {
            ZDoc.pendingObjectsObserverStarted = false;
            hideLoading();
        }
    }
}

function ShowDeleteConfirmation() {
    ShowLoadingAnimation();
    getBtnDelete().hide("2000");
    $("#divDeleteConfirmation").show("3000");
    hideLoading();
}

function HideDeleteConfirmation() {
    ShowLoadingAnimation();
    $("#divDeleteConfirmation").hide("2000");
    getBtnDelete().show("3000");
    hideLoading();
}

function sortTable(objTable, colId, ascending) {
    var tbl = objTable.tBodies[0];
    if (tbl == undefined) return;

    var store = [];
    for (var i = 1, len = tbl.rows.length; i < len; i++) {
        var row = tbl.rows[i];
        var sortnr = (row.cells[0].textContent || row.cells[colId].innerText);
        if (sortnr != "" && sortnr != null)
            sortnr = parseDate(sortnr);
        else {
            if (ascending) {
                sortnr = new Date(8640000000000000);
            }
            else {
                sortnr = new Date(-8640000000000000);
            }
        }
        if (!isNaN(sortnr)) store.push([sortnr, row]);
    }
    if (ascending)
        store.sort(function (x, y) {
            return x[0] - y[0];
        });
    else
        store.sort(function (x, y) {
            return y[0] - x[0];
        });

    for (var i = 0, len = store.length; i < len; i++) {
        tbl.appendChild(store[i][1]);
    }
    store = null;
}


function SetEditableColumns() {
    $(".editable").each(function () {
        var cols = $(this).attr("editablecolumns");

        /* Create input box and wrap in td and append to tr */
        $("<td>").append($("<input>", {
            type: "text",
            val: $("<td>").val(),
            name: "title",
            "class": "text",
            "css": {
                "width": "50px"
            }
        }));
    });
}

//Funciones para Formularios con Grillas, para Ejecutar Reglas desde cada Row de la grilla o para obtener los valores de todos los rows de la grilla y ejecutar una regla
function SetRuleIdAndZvar(sender, RuleId, ZVars) {
    $("#hdnRuleId")[0].name = RuleId;
    $("#hdnRuleId")[0].value = ZVars;
}

var ZVars;
var currentTag;

function SetRuleIdAndGrid(sender, RuleId, currentzvars, targetTableId) {
    count = 0;
    ZVars = currentzvars;
    var tbody;

    if (targetTableId == null || targetTableId == '')
        tbody = $(sender).closest('table').children('tbody')[0];
    else
        tbody = $('#' + targetTableId).children('tbody')[0];

    var rows = $('tr', tbody).each(function (e) {
        porcadatr(this, ZVars);
    });

    zvargridresult = zvargridresult + ')';

    // a futuro se deberia consumir como un servicio rest con json para la ejecucion en background de la regla y poder pasarle la coleccion como objeto.
    $("#hdnRuleId")[0].name = RuleId;
    $("#hdnRuleId")[0].value = ZVars.split("[")[0].replace('zvar(', '') + "=" + zvargridresult;
}

var count = 0;
var zvargridresult = 'totable(';

function porcadatr(sender, Zvars) {
    currentTag = sender;
    if (count > 0) {
        var zvarslist = Zvars.split(']').join('').split(')').join('').split("[");
        zvarslist.splice(0, 1);
        $(zvarslist).each(function () {
            var varvalue;
            if ($(currentTag).find('td:eq(' + this + ')').children('input').val() != null) {
                varvalue = $(currentTag).find('td:eq(' + this + ')').children('input').val();
            }
            else {
                varvalue = $(currentTag).find('td:eq(' + this + ')').text();
            }
            zvargridresult = zvargridresult + varvalue + "|";
        });
    }
    count++;
    zvargridresult = zvargridresult + ";";
}

function SetRuleIdAndGridWithNonCeros(sender, RuleId, currentzvars, targetTableId) {
    count = 0;
    ZVars = currentzvars;
    var tbody;
    if (targetTableId == null || targetTableId == '')
        tbody = $(sender).closest('table').children('tbody')[0];
    else
        tbody = $('#' + targetTableId).children('tbody')[0];

    var rows = $('tr', tbody).each(function (e) {
        porcadatrWithNonCeros(this, ZVars);
    });
    zvargridresult = zvargridresult.substring(0, zvargridresult.length - 2) + ')';
    // a futuro se deberia consumir como un servicio rest con json para la ejecucion en background de la regla y poder pasarle la coleccion como objeto.
    $("#hdnRuleId")[0].name = RuleId;
    $("#hdnRuleId")[0].value = ZVars.split("[")[0].replace('zvar(', '') + "=" + zvargridresult;
}

function porcadatrWithNonCeros(sender, Zvars) {
    currentTag = sender;
    if (count > 0) {
        var zvargridresulttemp = '';
        var zvarslist = Zvars.split(']').join('').split(')').join('').split("[");
        zvarslist.splice(0, 1);
        $(zvarslist).each(function () {
            var varvalue;
            if ($(currentTag).find('td:eq(' + this + ')').children('input').val() != null) {
                varvalue = $(currentTag).find('td:eq(' + this + ')').children('input').val();
            }
            else {
                varvalue = $(currentTag).find('td:eq(' + this + ')').text();
            }
            if (this == 5 && varvalue == '0') {
                zvargridresulttemp = '';
            }
            else {
                zvargridresulttemp = zvargridresulttemp + varvalue + "|";
            }
        });

        zvargridresult = zvargridresult + zvargridresulttemp;
    }
    count++;
    if (zvargridresulttemp != undefined && zvargridresulttemp != '') zvargridresult = zvargridresult + ";";
}

function EnableDisableRule(RuleId, Enabled) {
    var ruleFormButton = $("#zamba_rule_" + RuleId);

    if (ruleFormButton != undefined && ruleFormButton != null) {
        if (Enabled) {
            ruleFormButton.show();
            ruleFormButton.css('display', 'block');

        } else {
            ruleFormButton.hide();
            ruleFormButton.css('display', 'none');
        }
    }

    var userActionButton = $("#ctl00_ContentPlaceHolder_ucTaskHeader_UAB_Rule_" + RuleId);
    if (userActionButton != undefined && userActionButton != null) {
        if (Enabled) {
            userActionButton.show();
            userActionButton.css('display', 'block');

        } else {
            userActionButton.hide();
            userActionButton.css('display', 'none');
        }
    }

}

function DisableRulesByTaskId(TaskId) {
    if (TaskId > 0) {
        var DisabledRulesByTask = sessionStorage.getItem("DisabledRules-" + TaskId);
        if (DisabledRulesByTask != undefined && DisabledRulesByTask != null) {

            DisabledRulesByTask = DisabledRulesByTask.replaceAll('}"', '}').replaceAll('"{', '{');

            var rulesJson = JSON.parse(DisabledRulesByTask);
            for (var i = 0; i < rulesJson.length; i++) {
                EnableDisableRule(rulesJson[i].currentRuleId, rulesJson[i].IsEnabled)
            }
        }
    }
}

//funcion para ejecutar una regla presionando la tecla "enter" y apuntando al btn que ejecuta la regla
function enterGuide(e, id) {
    if (e.charCode == 13) {
        $("#" + id).click();
    }
}

function SetRuleId(sender) {
    $("#hdnRuleId")[0].name = sender.id;
}

function SetAsocId(sender) {
    $("#hdnAsocId")[0].name = sender.id;
    var x = document.getElementsByTagName("form");
    x[0].submit();// Form submission
}

//Inicia una nueva ejecucion de reglas
function SetNewGeneralExecution() {
    var WFIframe = window.parent.$("#WFExecForEntryRulesFrame");
    if (WFIframe.length == 0) WFIframe = $("#WFExecForEntryRulesFrame");
    var CurrTask = WFIframe.contents().find("#hdnCurrTaskID").val();
    if (WFIframe.length == 0 && typeof (WFIframe[0]) != "undefined") WFIframe[0].contentWindow.location = "../../../Views/WF/WFExecutionForEntryRules.aspx";

    if (WFIframe[0] != null) {
        WFIframe[0].contentWindow.location = "about:blank";
        WFIframe[0].contentWindow.location = "../../../Views/WF/WFExecutionForEntryRules.aspx";
    }
    else {
        if (WFIframe.context != null) {
            WFIframe.context.location = "about:blank";
            WFIframe.context.location = "../../../Views/WF/WFExecutionForEntryRules.aspx";
        }
    }

    //openModalIFWF modal
    WFIframe.on("load", function () {
        var $modal = $(this).parent().parent();
        var heightIF = $(this).contents().children().height();
        if (heightIF > 0 && heightIF <= 550) {
            $modal.css("height", (heightIF + 50) + "px");
            $modal.parent().css("height", (heightIF + 50) + "px");
        }

        var widthIF = $(this).contents().children().width();
        if (widthIF > 0 && widthIF <= 800) {
            $modal.css("width", (widthIF + 40) + "px");
            $modal.parent().css("width", (widthIF + 40) + "px");
        }

        if (heightIF > 0 && heightIF > 550) {
            $modal.css("height", (250) + "px").css("width", (684) + "px");
            $modal.parent().css("height", (320) + "px").css("width", (widthIF + 20) + "px");
        }
    });
}

function ShowGeneralRuleContainer() {
    //Se muestra el contenedor de reglas
    var rulesContent = $("#EntryRulesContent").css("width", "600").css("height", "50%").css("top", "-1000").css("left", (window.screen.width / 2) - (rulesContent.width() / 2)).show();
}

function ResizeGeneralRuleIframe() {
    $("#WFExecForEntryRulesFrame").css("width", $("#EntryRulesContent").width()).css("height", $("#EntryRulesContent").height());
}

function PreventClick(event) {
    event.preventDefault();
}
function isSearchHtml() {
    if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1].toLocaleLowerCase() == 'search.html')
        return true;
    else
        return false
}
function isTaskviewer() {
    if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1].toLocaleLowerCase() == 'taskviewer')
        return true;
    else
        return false
}
//Llamada asincrona al metodo para setear una nueva ejecucion de reglas
function RuleButtonClick(sender, ruleId) {
    var async = false;
    if (async == true) {
        var scope_taskController = angular.element($("#taskController")).scope();
        scope_taskController.Execute_ZambaRule(ruleId, 0);
    }
    else {


        __doPostBack('hdnsender', ruleId);

        return;

        //var MySwal = window.swal({
        //    closeOnClickOutside: false,
        //    title: "Procesando...",
        //    text: "Espere por favor.",
        //    showCancelButton: false,
        //    showConfirmButton: false,
        //    buttons: false
        //});

        $.ajax({
            type: "POST",
            url: "../../../Services/TaskService.asmx/SetNewRuleExecution",
            data: jQuery.param({ ruleId: ruleId, userId: GetUID() }),

            success: function (msg) {
                SetNewGeneralExecution();
            },
            complete: function (data) {
            },
            error: function (request, status, err) {
                toastr.error("Error en la ejecucion de reglas \r" + request.responseText);
            }
        });
    }
}

function ShowSwalForWaiting(myText, myTimer, myClickOutside) {
    window.swal({
        closeOnClickOutside: myClickOutside,
        text: myText,
        showCancelButton: false,
        showConfirmButton: false,
        buttons: false,
        timer: myTimer
    });
}


function GetPipedSeparateValues(idsArray) {
    var values = '';
    for (var i = 0; i < idsArray.length; i++) {
        if (idsArray[i] != null && idsArray[i] != '') {
            if (i > 0)
                values += '|';
            values += $('#' + idsArray[i]).val();
        }
    }
    return values;
}

function clearindexcache(indexid) {
    $.ajax({
        type: 'POST',
        url: '../../Services/IndexService.asmx/ClearIndexCache',
        data: "{'IndexId':" + indexid + "}",
        contentType: 'application/json; utf-8',
        dataType: 'json',
        success: function (data) {
            if (data.d != null) {
                toastr.error(data.d);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}

function keepSessionAlive() {
    try {


        if (document.config == undefined) document.config = parent.document.config;
        $.ajax({
            type: "POST",
            url: "../../Services/TaskService.asmx/KeepSessionAlive",
            data: '',
            contentType: "application/json; charset=utf-8",
            success: function (data) {

            },
            error: function (a, b) {

            },
            dataType: "json"
        });
        if (document.config != null)
            setTimeout("keepSessionAlive()", 5 * 60000 - 30000); //sessionTimeOut is minutes

    } catch (e) {

    }
}

function clearAllCache(reLogin) {
    if (window.localStorage) {
        let ConnId = {};
        let UserId = {};
        let authorizationData = {};
        if (reLogin == false) {
            ConnId = window.localStorage.getItem("ConnId");
            UserId = window.localStorage.getItem("UserId");
            authorizationData = window.localStorage.getItem("authorizationData");

        }

        window.localStorage.clear();

        if (reLogin == false) {
            window.localStorage.setItem("ConnId", ConnId);
            window.localStorage.setItem("UserId", UserId);
            window.localStorage.setItem("authorizationData", authorizationData);

        }

        var appController = angular.element($("#appIdController")).scope();
        appController.GetCurrentUserName();
        setTimeout(photoValueConfig, 5000);


    }

    $.ajax({
        type: "POST",
        url: "../../Services/TaskService.asmx/ClearAllCache",
        data: '',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            toastr.success("Cache limpiado correctamente");

            window.location.reload(true);
        },
        error: function (a, b) {
            toastr.error("Error al limpiar cache");
        },
        dataType: "json"
    });

    $.ajax({
        type: "POST",
        url: ZambaWebRestApiURL + '/cache/ClearAllCache',
        data: '',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            toastr.success("Cache limpiado correctamente");
            window.location.reload(true);
        },
        error: function (a, b) {
            toastr.error("Error al limpiar cache");
        },
        dataType: "json"
    });

}

function clearAllCacheWithoutReload() {
    if (window.localStorage) {
        window.localStorage.clear();
    }

    $.ajax({
        type: "POST",
        url: "../../Services/TaskService.asmx/ClearAllCache",
        data: '',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
        },
        error: function (a, b) {
            toastr.error("Error al limpiar cache");
        },
        dataType: "json"
    });

    $.ajax({
        type: "POST",
        url: ZambaWebRestApiURL + '/cache/ClearAllCache',
        data: '',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
        },
        error: function (a, b) {
            toastr.error("Error al limpiar cache");
        },
        dataType: "json"
    });

}

function clearRulesCache() {
    if (window.localStorage) {
        window.localStorage.clear();
    }

    $.ajax({
        type: "POST",
        url: "../../Services/TaskService.asmx/ClearRulesCache",
        data: '',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            toastr.success("Cache limpiado correctamente");

            window.location.reload(true);
        },
        error: function (a, b) {
            toastr.error("Error al limpiar cache");
        },
        dataType: "json"
    });

    $.ajax({
        type: "POST",
        url: ZambaWebRestApiURL + '/cache/ClearRulesCache',
        data: '',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            toastr.success("Cache limpiado correctamente");
            window.location.reload(true);
        },
        error: function (a, b) {
            toastr.error("Error al limpiar cache");
        },
        dataType: "json"
    });

}

function clearRightsCache() {
    if (window.localStorage) {
        window.localStorage.clear();
    }

    $.ajax({
        type: "POST",
        url: "../../Services/TaskService.asmx/ClearRightsCache",
        data: '',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            toastr.success("Cache limpiado correctamente");

            window.location.reload(true);
        },
        error: function (a, b) {
            toastr.error("Error al limpiar cache");
        },
        dataType: "json"
    });

    $.ajax({
        type: "POST",
        url: ZambaWebRestApiURL + '/cache/ClearRightsCache',
        data: '',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            toastr.success("Cache limpiado correctamente");
            window.location.reload(true);
        },
        error: function (a, b) {
            toastr.error("Error al limpiar cache");
        },
        dataType: "json"
    });

}

function clearStructureCache() {
    if (window.localStorage) {
        window.localStorage.clear();
    }

    $.ajax({
        type: "POST",
        url: "../../Services/TaskService.asmx/ClearStructureCache",
        data: '',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            toastr.success("Cache limpiado correctamente");

            window.location.reload(true);
        },
        error: function (a, b) {
            toastr.error("Error al limpiar cache");
        },
        dataType: "json"
    });

    $.ajax({
        type: "POST",
        url: ZambaWebRestApiURL + '/cache/ClearStructureCache',
        data: '',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            toastr.success("Cache limpiado correctamente");
            window.location.reload(true);
        },
        error: function (a, b) {
            toastr.error("Error al limpiar cache");
        },
        dataType: "json"
    });

}

function ClearUserCache(userid) {
    $.ajax({
        type: "GET",
        url: "../../Services/TaskService.asmx/ClearUserCache",
        data: { userId: userid },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) { },
        error: function (a, b) { }
    });
}

function getAvailableWidth() {
    var screenWidth = $(window).width();
    if (screenWidth == 0)
        screenWidth = $(parent.$("#mainMenu")).width();
    return screenWidth;
}

function addEvent(id, eventName, handler) {
    var elementId = getSelectorId(id);
    $(elementId).on(eventName, handler);
}

function getSelectorId(id) {
    var selectorId;
    if (id.indexOf('#') > -1)
        selectorId = id;
    else
        selectorId = '#' + id;
    return selectorId;
}

//Scripts de la MasterPage
var UseTaskTab = false;
function ResizeCurrentTab(height, url) {
    if (height > 0) {
        if (url.toLowerCase().indexOf("taskviewer") != -1 || url.toLowerCase().indexOf("docviewer") != -1) {
            if ($('#TasksDivUL').find(".ui-tabs-selected").find("a")[0] != undefined) {
                idTabSelected = $('#TasksDivUL').find(".ui-tabs-selected").find("a")[0].href;
                $(idTabSelected).height(getHeightScreen() - getTBH());
            }
        }
    }
}

function GetTaskAvailableHeight() {
    return getHeightScreen() - getTBH();
}

function ExtractIdFromUrl(url, typeId) {
    var start;
    var end;
    if (typeId == "doc") {
        start = url.indexOf('docid', 0) + 6;
        end = url.indexOf('&', start);
    }
    else {
        start = url.indexOf('taskid', 0) + 7;
        end = url.indexOf('&', start);
    }
    return url.substr(start, end - start);
}

function loadCliksInRows() {
    if ($('.AltRowStyle').find('a') != null) {
        $('.AltRowStyle').click(function () {
            if ($(this).find('a')[0]) {
                window.open($(this).find('a')[0], '_blank', '', false);
            }
            return false;
        });
        $('.AltRowStyle').find('a').click(function () {
            window.open($(this).attr('href'), '_blank', '', false);
            return false;
        });
    }
    if ($('.RowStyle').find('a') != null) {
        $('.RowStyle').click(function () {
            if ($(this).find('a')[0]) {
                window.open($(this).find('a')[0], '_blank', '', false);
            }
            return false;
        });
        $('.RowStyle').find('a').click(function () {
            window.open($(this).attr('href'), '_blank', '', false); return false;
        });
    }

    if ($('.AltRowStyleTasks').find('a') != null) {
        $('.AltRowStyleTasks').click(function () {
            if ($(this).find('a')[0]) {
                var name = $(this).find(">td")[TaskColumnPosition(this)].innerText;
                SwitchDocTaskForResults($(this).find('a')[0], true, name);

                return false;
            }
        });
        $('.AltRowStyleTasks').find('a').click(function () {
            var name = $(this).parent().parent().find("td")[TaskColumnPosition(this)].innerText;
            SwitchDocTaskForResults($(this), true, name);

            return false;
        });
    }

    SetRowFn(this, $('.RowStyleTasks'));
    var $tabContentIframe = $("iframe#IFTaskContent").contents().find('iframe#tabContent:visible');
    for (var i = 0; i < $tabContentIframe.length; i++) {
        SetRowFn(this, $($tabContentIframe.contents()[i]).find("tr.RowStyleAsoc"));
    }

    function SetRowFn(_this, $row) {
        if ($row.find('a') != null) {
            $row.click(function () {
                if ($(this).find('a')[0]) {
                    var name = $(this).find(">td")[TaskColumnPosition(this)].innerText;
                    SwitchDocTaskForResults($(this).find('a')[0], true, name);

                    return false;
                }
            });
            $row.find('a').click(function () {
                var name = $(this).parent().parent().find("td")[TaskColumnPosition(this)].innerText;
                SwitchDocTaskForResults($(this), true, name);

                return false;
            });
        }
    }
    if ($('.FormRowStyle').find('a') != null) {
        $('.FormRowStyle').click(function () {
            if ($(this).find('a')[0]) {
                var name = $(this).find(">td")[TaskColumnPosition(this)].innerText;
                SwitchDocTaskForResults($(this).find('a')[0], true, name);
                return false;
            }
        });
        $('.FormRowStyle').find('a').click(function () {
            var name = $(this).parent().parent().find("td")[TaskColumnPosition(this)].innerText;
            SwitchDocTaskForResults($(this), true, name);
            return false;
        });
    }
    if ($('.RowStyleResults').find('a') != null) {
        $('.RowStyleResults').click(function () {
            if ($(this).find('a')[0]) {
                var name = $(this).find(">td")[TaskColumnPosition(this)].innerText;
                SwitchDocTaskForResults($(this).find('a')[0], true, name);
                return false;
            }
        });
        $('.RowStyleResults').find('a').click(function () {
            var name = $(this).parent().parent().find("td")[TaskColumnPosition(this)].innerText;
            SwitchDocTaskForResults($(this), true, name);
            return false;
        });
    }
    if ($('.AltRowStyleResults').find('a') != null) {
        $('.AltRowStyleResults').click(function () {
            if ($(this).find('a')[0]) {
                var name = $(this).find(">td")[TaskColumnPosition(this)].innerText;
                SwitchDocTaskForResults($(this).find('a')[0], true, name);
                return false;
            }
        });
        $('.AltRowStyleResults').find('a').click(function () {
            var name = $(this).parent().parent().find("td")[TaskColumnPosition(this)].innerText;
            SwitchDocTaskForResults($(this), true, name);
            return false;
        });
    }

    function TaskColumnPosition(_this) {
        var index = $(_this).parents("table").attr("id").indexOf("Associated") > -1 ?//Grilla asociados
            $(_this).parent().find(".HeaderStyle").find("th:contains('Name')").index()
            : $(_this).parent().find(".HeaderStyle").find("th:contains('Tarea')").index();
        return index;
    }
}

function SwitchDocTaskForResults(anchor, asTask, name) {
    var userID;
    userID = GetUID();
    var url = anchor.href;
    if (url === undefined) {
        url = anchor[0].href;
    }
    if (url == null || url == "") {
        toastr.error("Error al abrir una tarea, ID no especificado");
        return;
    }
    url = url.toLowerCase();
    //Se obtiene de la url el docID
    var id = getParameterByName("taskid", url);

    sessionStorage.removeItem('taskid');
    sessionStorage.setItem('taskid', id);

    docid = getParameterByName("docid", url);
    var docTypeId2 = getParameterByName("doctypeid", url);

    if (docTypeId2 == "")
        docTypeId2 = getParameterByName("doctype", url);

    //Si lo tiene lo envia para abrirlo
    var objectToSend = {}
    objectToSend.userId = userID;
    //objectToSend.docId = docId;
    objectToSend.docTypeId = docTypeId;
    objectToSend.name = name;
    //objectToSend.crdate = date;

    //insert recents
    //$.ajax({
    //    type: "POST",
    //    dataType: "json",
    //    url: ZambaWebAdminRestApiURL + "/" + "api/BCHistory/InsertRecents",
    //    data: "{ userId: " + userID + ", docId: " + docid + ",docTypeId: " + docTypeId2 + ", name: " + name + ", crdate: " + "10/20/1991" + "}",
    //    //data: "{recentTask:" + JSON.stringify(objectToSend) + "}",
    //    success: function (data) {
    //    }
    //});

    if (id == "" || id == "0") {
        id = getParameterByName("docid", url);
        var docTypeId = getParameterByName("doctypeid", url);
        if (docTypeId == "")
            docTypeId = getParameterByName("doctype", url);
        //Si lo tiene lo envia para abrirlo
        //var ZambaWebAdminRestApiURL = "http://localhost/zamba.WebAdmin";
        OpenDocTask3(0, id, docTypeId, !asTask, name, url, userID);
    }
    else {
        if (id != "") {
            OpenDocTask3(id, 0, -1, false, name, url, userID);
        }
        else {
            toastr.error("Error al abrir una tarea, ID no especificado");
        }
    }
}

function OpenWindow(url) {
    window.open(url);
    var tabcount = $('#TasksDivUL').find("li").length;
    if (tabcount > 0)
        RefreshCurrentTab();
}

function MakeSearch() {
    parent.ViewResults();
}

function OpenDocTaskfronMaster2(n, id, docTypeId, asTask, name, url, userID) {
    OpenDocTask3(n, id, docTypeId, asTask, name, url, userID);
}

function ViewResults() {
    $('#MainTabber').zTabs("select", '#tabresults');
}

function ReloadFrame() {
    var iframe = document.frames ? document.frames["ContentPlaceHolder1_formBrowser"] : $("#ContentPlaceHolder1_formBrowser");
    var ifWin = iframe.contentWindow || iframe;
    var url = ifWin.location;
    ifWin.location.reload();
    ifWin.location = url;
}

var unloadCount = 1;
var showedDialog = false;
var unloadFromInsert = false;

function RefreshGrid(tabName) {
    var WFIframe;
    switch (tabName) {
        case "tareas":
            WFIframe = $("#ContentPlaceHolder_wfiframe");
            break;
    }
}

//Refresca el tab actual
function RefreshCurrentTab() {
    ShowLoadingAnimation();
    //obtengo el objeto que contiene el iframe
    var $id = $('#TasksDiv').zTabs("activeAnchor").attr("href");
    $id = $id.substr($id.indexOf("#"));
    //obtengo el Iframe
    var $idIF = $($id).find(":first-child")[0];
    var Iframe = $($idIF);
    if (Iframe != null && Iframe[0] != null) {
        Iframe[0].src = Iframe[0].src;//Iframe[0].contentWindow.location.href;
    }
    else {
        if (Iframe.context != null) {
            Iframe[0].src = Iframe[0].src;//Iframe[0].contentWindow.location.href;
        }
    }
}

function RefreshTab(tabid) {
    var id = $(tabid).find(":first-child")[0];
    var Iframe = $(id);
    if (Iframe != null && Iframe[0] != null) {
        Iframe[0].src = Iframe[0].src;//Iframe[0].contentWindow.location.href;
        //Iframe[0].contentWindow.location.reload(true);
    }
}

function RefreshTask(taskid, docid) {
    Refresh_C();
}

function CloseCurrentTask(taskid, isCloseFromButton) {
    try {

        if (taskid !== undefined) {
            CloseTask(taskid, isCloseFromButton)
        }
        //RefreshOpenerGrid();
        window.close();
    } catch (e) {
        console.error(e);
    }
}

function RefreshOpenerGrid() {
    try {

        if (window.opener != undefined) {
            window.opener.RefreshResultsGridFromChildWindow();
            //derivo y hago un trigger para actulizar grilla
        }
        else {
            RefreshResultsGridFromChildWindow();
        }
    } catch (e) {
        console.error(e);
    }
}


function CheckAnyTaskOpen() {
    return ($('#TasksDiv').tabs("length") == 0);
}
function InsertOpen() {
    var destinationURL = "/Views/Insert/Insert.aspx";
    var newwindow = window.open(destinationURL, this.target, 'width=630,height=550,left=' + (screen.width - 600) / 2 + ',top=' + (screen.height - 580) / 2 + ',directories=no,status=no,menubar=no,toolbar=no,location=no,resizable=yes');
}
function InsertForm(formid, modal) {
    let userToken = JSON.parse(localStorage.getItem('authorizationData'));
    let { token } = userToken;
    if (!modal) {
        location.href = '/Views/WF/DocInsert.aspx?formid=' + formid + "&t=" + token;
    }
    else {
        ShowIFrameModal("Insertar documentos", "../WF/DocInsertModal.aspx?formid=" + formid + "&modal=true" + "&t=" + token, 900, 700);
        StartObjectLoadingObserverById("IFDialogContent");
    }
}
function AsociateForm(formid, docid, doctypeid, taskid, continueWithCurrentTasks, dontOpenTaskAfterInsert, fillCommonAttributes, haveSpecificAtt) {
    let userToken = JSON.parse(localStorage.getItem('authorizationData'));
    let { token } = userToken;
    var url = "../WF/DocInsertModal.aspx?formid=" + formid + "&DocID=" + docid + "&DocTypeID=" + doctypeid + "&CallTaskID=" +
        taskid + "&userid=" + GetUID() + "&ContinueWithCurrentTasks=" + continueWithCurrentTasks + "&DontOpenTaskAfterInsert=" + dontOpenTaskAfterInsert + "&FillCommonAttributes=" +
        fillCommonAttributes + "&haveSpecificAtt=" + haveSpecificAtt + "&t=" + token;

    window.open(url, "_blank");
    // ShowIFrameModal("Insertar Tarea", url, 0, 0);//550, 950
    // StartObjectLoadingObserverById("IFDialogContent");
}



function CloseInsert() {
    if ($("#openModalIF").length == 0) {
        parent.UnblockWebPageInteraction();
        parent.$("#openModalIF").removeClass("in");
        parent.$(".IFTaskContent").contents().find(".modal-backdrop").remove();
        parent.$("#openModalIF").hide();
        parent.$("#openModalIF").modal("hide");
    }
    else {
        $("#openModalIF").hide().modal("hide");
        if ($("#openModalIF").is(':visible')) {
            $("#openModalIF").removeClass("in");
            if ($(".modal-backdrop").length) {
                $(".modal-backdrop").remove();
            }
            else {
                $(".IFTaskContent").contents().find(".modal-backdrop").remove();
            }
            $("#openModalIF").hide();
        }
        UnblockWebPageInteraction();
    }
}


function FixAndPosition(objDlg) {
    objDlg.css("top", "100px");
    objDlg.css("position", "absolute");

    var zIndexMax = getMaxZ();
    $(objDlg).css({ "z-index": Math.round(zIndexMax) });
}
function OpenHome() {
    window.open($("#hdnLink").val(), $("#hdnTarget").val(), "", false);
}

function ShowDivModal(title, div, width, height) {
    if (div.width() != undefined)
        width = div.width();
    if (div.height() != undefined)
        height = div.height() + 100;

    if ($("#modalIframe").length >= 1) {
        if (title == "") {
            $("#openModalIF >div .modal-header").css("padding", 0);
            $("#openModalIF >div .modal-header >button").css("margin-right", 10);
        }
        $("#modalIframe").attr("src", "aboutblank.html").css("display", "none");
        $("#modalFormTitle").html(title);
        $("#modalDivBody").html(div.html()).css("display", "block");
        OpenModalIF.show();
    }
    else {
        if (title == "") {
            $("#openModalIF >div .modal-header", window.parent.document).css("padding", 0);
            $("#openModalIF >div .modal-header >button", window.parent.document).css("margin-right", 10);
        }
        var $modalIframe = $("#modalIframe", window.parent.document);
        if ($modalIframe.length == 0)
            $modalIframe = $("#modalFormHome", window.parent.document).children("iframe");

        $modalIframe.attr("src", "aboutblank.html").css("display", "none");
        $("#modalFormTitle", window.parent.document).html(title);
        $("#modalDivBody", window.parent.document).html(div.html()).css("display", "block");
        if (typeof $().modal != 'function')
            reloadBootstrap();
        OpenModalIF.show();
    }
}


function ShowInsertAsociated(url, openModal) {
    if (openModal == true) {
        ShowIFrameModal("", url, 0, 0);
    }
    else {
        openWindow(url);
    }
    //var iframeRule = '<iframe src="' + url + '" height="100%" width="100%"></iframe>'

    //$("#genericModalConteiner").html(iframeRule);
    //$("#genericModal").modal();

}





function addpnlUcRulesToSeccion() {
    //parent.$("#TasksDivUL").css("display", "none");
    $("#rowTaskDetail").css("display", "none");
    $("#rowTaskHeader").css("display", "none");
}

function ShowIFrameModal(title, src, width, height) {
    if (title == '') {
        $("#modalFormTitle").css("height", 0);
    }

    if (height == undefined || height == 0)
        height = (window.innerHeight * .95) + 'px';


    if (width == undefined || width == 0)
        width = (window.innerWidth * .95) + 'px';

    if ($("#modalIframe").length >= 1) {
        $("#modalIframe").attr("src", "aboutblank.html");
        $("#modalDivBody").html('').css("display", "none");
        $("#modalIframe").attr("src", src);
        $("#modalFormTitle").html(title);
        $("#modalIframe").css("width", width).css("height", height).css("display", "none");

        if (typeof $().modal != 'function')
            reloadBootstrap();

        $('#openModalIF').modal().draggable();
        $("#modalIframe").ready(function () {
            //modalIframe es el Iframe adentro del Modal
            $("#modalIframe").css("display", "block");

            //openModalIF es el modal, que su hijo .modal-dialog
            //            $("#openModalIF").css("height", "95%");
            //          $("#openModalIF").css("width", "100%");

            $("modal-dialog").css("margin-top", "0");
            //       $("modal-dialog").css("width", "100%");

            let height = $("#modalFormHome").height() * .95;
            let width = $("#modalFormHome").width() * .95;


            //$("#modalIframe").css("height", $("#modalFormHome").height() * .95);
            var Tittle = $('#modalFormTitle').innerHeight(); + 8;
            var PaddinTopBottomIframe = $('#modalFormHome').innerHeight() - $('#modalFormHome').height();
            var result = ($("#openModalIFContent").height() - (Tittle + PaddinTopBottomIframe)).toString() + "px";

            $("#modalIframe").css("height", result);

            //   $("#modalIframe").css("width", width);
        });

    } else { //Esta dentro de un IFrame

        var $modalIframe = $("#modalIframe", window.parent.document);

        if ($("#modalIframe", window.parent.document).attr("src") == undefined)
            $modalIframe = $("#modalFormHome", window.parent.document).children("iframe");

        if (!$modalIframe.length)
            $modalIframe = $("#openModalIF", window.parent.document).find("iframe");

        $("#modalDivBody", window.parent.document).html('').css("display", "none");

        //$modalIframe.css("display", "none");

        if (typeof $().modal != 'function')
            reloadBootstrap();

        OpenModalIF.show();

        $modalIframe.attr("src", "about:blank");

        $modalIframe.on("load", OnLoadHandler);
        $modalIframe.attr("src", src);

        $("#modalFormTitle", window.parent.document).html(title);
        $modalIframe.parent().parent().find("#modalFormTitle").html(title);

        function OnLoadHandler(e) {
            $(this).parent().css("height", "90%");
        }
    }
}


function ShowIFrameModal2(title, src, width, height) {
    var modalIframe = $("#modalIframe");
    var modalDialog = $("#openModalIF");

    if (typeof $().modal != 'function')
        reloadBootstrap();

    modalDialog.on('show', function () {
        var heightIF = $("#modalIframe").contents().children().height();
        if (heightIF == undefined || heightIF == 0 || heightIF < height)
            heightIF = height;
        $("#openModalIF").css("height", (heightIF + 70) + "px");

        var widthIF = $("#modalIframe").contents().children().width();
        if (widthIF == undefined || widthIF == 0 || widthIF < width)
            widthIF = width;
        $("#openModalIF").css("width", widthIF + "px");

    });


    //Una vez que carga el Iframe ajusto tamaño del contenido al modal
    modalIframe.on('load', function () {
        var heightIF = $("#modalIframe").contents().children().height();
        if (heightIF == undefined || heightIF == 0 || heightIF < height)
            heightIF = height;
        $("#openModalIF").css("height", (heightIF + 70) + "px");

        var widthIF = $("#modalIframe").contents().children().width();
        if (widthIF == undefined || widthIF == 0 || widthIF < width)
            widthIF = width;
        $("#openModalIF").css("width", widthIF + "px");

    });

    modalDialog.modal({ show: true });
    modalIframe.attr("src", src);
}

//$('body').on('click', '#closeModalIF, #closeModalIFWF', function (event) {
//        $(this).parent().parent().find("iframe").attr("src", "aboutblank.html");
//});

function ShowIFrameHome(src, height) {
    var $toAppend = $("#tabhome").length >= 1 ? $("#tabhome") : $("#tabhome", window.parent.document);
    var gDown = "glyphicon glyphicon-resize-full";
    var gUp = "glyphicon glyphicon-resize-small";
    var txtH = " Ocultar";
    var txtS = " Expandir";
    // Creo el boton para expandir/contraer botones home
    var homeButtonsDiv = document.createElement('div');
    var SpanExpandirContraer = document.createElement('span');
    var spanGlyphicon = document.createElement('span');
    $(homeButtonsDiv).addClass("toogleIframe");
    $(spanGlyphicon).addClass(gDown).appendTo($(homeButtonsDiv));
    $(SpanExpandirContraer).text(txtS).appendTo($(homeButtonsDiv));

    // Guardo div con los botones
    var buttonsDiv = $("#ContentPlaceHolder_pnlHomeButtons", parent.document);

    $(homeButtonsDiv).click(function () {
        buttonsDiv.slideToggle();
        $(SpanExpandirContraer).text($(SpanExpandirContraer).text() == txtH ? txtS : txtH);
        $(spanGlyphicon).attr("class", $(spanGlyphicon).attr("class") == gDown ? gUp : gDown);
    });

    // Agrego el boton antes del div con los botones.
    $(homeButtonsDiv).insertBefore(buttonsDiv);
    // Oculto los botones.
    buttonsDiv.slideToggle();

    // Se crea Iframe con la grilla.
    var iframe = document.createElement('iframe');
    iframe.src = src;
    iframe.width = "100%";
    iframe.height = (height - 100);
    iframe.id = "iframeHome";

    //Boton para expandir/contraer grilla
    var d = document.createElement('div');
    var s = document.createElement('span');
    $(s).addClass(gUp).appendTo($(d));
    var sn = document.createElement('span');
    $(sn).text(txtH).appendTo($(d));
    var close = document.createElement('span');
    $(close).attr("class", "glyphicon glyphicon-remove-circle");
    $(close).css("float", "right").css("margin-right", "5px").appendTo($(d));

    $(d).addClass("toogleIframe").appendTo($toAppend)
        .click(function () {
            $(iframe).slideToggle();
            $(sn).text($(sn).text() == txtH ? txtS : txtH);
            $(s).attr("class", $(s).attr("class") == gDown ? gUp : gDown);
        });

    $toAppend.append(iframe);
    $(close).click(function () {
        $(iframe).remove();
        $(d).remove();
        if ($(SpanExpandirContraer).text() == txtS) {
            buttonsDiv.slideToggle();
        }
        $(homeButtonsDiv).remove();
    });
}

function ShowChangePassDlg() {


    ShowIFrameModal("", "../../Views/Security/UsrPsswrd.aspx", 450, 250)



}
function CloseChangePassDlg() {
    $("#modalDialog").dialog("Close");
}

//Cambia el tamaño del diálogo que genera la llamada al tb_show
function ResizeInsertDialogToShow(height, width) {
    $("#TB_window").find(":first-child").height(height);
    if (width !== undefined) {
        $("#TB_iframeContent").width(width).height(height);
    }
}
function HideDivPresentation() {
    var zi = $("#divPrimario").css("z-index");
    if (zi == 1000) {
        $("#divPrimario").css("z-index", "3000");
        $("#divSecundario").css("z-index", "1000");
    }
    else {
        $("#divPrimario").css("z-index", "1000");
        $("#divSecundario").css("z-index", "3000");
    }
}
function SelectTabFromMasterPage(tab) {
    //Antes de seleccionar nada, se elimina el tab dummy anteriormente creado.
    if ($("#DummyTab")) {
        $("#DummyTab").remove();
    }
    var tabIndex;
    if ($('#MainTabber >div').length == 6) {
        switch (tab) {
            case "tabhome":
                tabIndex = 0;
                break;
            case "tabBCHistory":
                tabIndex = 1;
                break;
            case "tabsearch":
                tabIndex = 2;
                break;
            case "tabresults":
                tabIndex = 3;
                break;
            case "tabtasklist":
                tabIndex = 4;
                break;
            case "tabtasks":
                tabIndex = 5;
                break;
        }
    }
    else {
        switch (tab) {
            case "tabhome":
                tabIndex = 0;
                break;
            case "tabBCHistory":
                tabIndex = 1;
                break;
            case "tabsearch":
                tabIndex = 2;
                break;
            case "tabresults":
                tabIndex = 3;
                break;
            case "tabInsert":
                tabIndex = 4;
                break;
            case "tabtasklist":
                tabIndex = 5;
                break;
            case "tabtasks":
                tabIndex = 6;
                break;
        }
    }
    $('#MainTabber').tabs().tabs('option', 'active', tabIndex);
    $("#divPrimario").hide();
    $("#divSecundario").show();
}
function HomeCabPresentation() {

}



function SelectTaskFromModal() {
    //Antes de seleccionar nada, se elimina el tab dummy anteriormente creado.
    if ($("#DummyTab")) {
        $('#MainTabber').zTabs("remove", "#DummyTab");
    }

    $("#divPrimario").hide();
    $("#divSecundario").show();
    $('#MainTabber').zTabs("select", '#tabtasks');
    if ($("#divReports")) {
        $("#divReports").dialog("close");
    }
    if ($("#divSemaphore")) {
        $("#divSemaphore").dialog("close");
    }
}
function InsertFormModal(formid) {
    var url = "../WF/DocInsertModal.aspx?formid=" + formid + "&userid=" + GetUID();
    ShowIFrameModal("", url, 800, 600);
}
//Crea una nueva ventana con la url especificada y le setea el opener
function openWindow(url, features, asMaximized) {
    if (asMaximized) {
        if (features == '')
            features += ",screenX=0,screenY=0,left=0,top=0";
        else
            features += "screenX=0,screenY=0,left=0,top=0";

        features += ",width=" + (screen.availWidth - 10).toString();
        features += ",height=" + (screen.availHeight - 30).toString();
    }
    WindowRef = window.open(url, '', features);
    if (WindowRef != null) {
        WindowRef.opener = self;
    }
    WindowRef.focus();
}

function HideHome() {
    $("#divPrimario").hide();
    $("#divSecundario").show();
}

function ResizeTBIframe(width, height) {
    if (height > 0) {
        $("#TB_iframeContent").height(height);
        $("#TB_window").height(height);
    }

    if (width > 0) {
        $("#TB_iframeContent").width(width);
        $("#TB_window").width(width);
    }
    $("#TB_window").css("overflow", "hidden");
}

function SetNewEntryRulesGroup() {
    SetNewGeneralExecution();
}

function ShowEntryRulesPanel(value) {
    var rulesContent = $("#EntryRulesContent");
    var rulesIF = $("#WFExecForEntryRulesFrame");

    if (value) {
        rulesContent.css("top", $(document).height() / 2 - rulesContent.height() / 2).css("left", $(document).width() / 2 - rulesContent.width() / 2).show();
        rulesIF.show();
    }
}

function CloseEntryRulesPanel() {
    $('#openModalIFWF').hide();
}

function ResizeEntryRulesPanel(width, height) {
    //Se suma 50 por la barra del título 
    height = height + 50;
    if (height > 0) {
        $("#EntryRulesContent").height(height);
        $("#WFExecForEntryRulesFrame").height(height);
    }

    if (width > 0) {
        $("#EntryRulesContent").width(width);
        $("#WFExecForEntryRulesFrame").width(width);
    }
}

function RedirectToErrorPage(url) {
    //Se sobreescribe el beforenunload para lanzar el cierre de tareas.
    window.onbeforeunload = function () {
        showedDialog = true;
    };
    document.location = url;
}

function RedirectOpenerToLogin() {
    try {
        if (window.opener != null && !window.opener.closed) {
            window.opener.RedirectToLogin();
        }
        else if (window.parent != null) {
            window.parent.RedirectToLogin();
        }

    } catch (e) {

    }
}
function RedirectToLogin() {
    window.onbeforeunload = function () {
        showedDialog = true;
    };
    var destinationURL = "../Security/Login.aspx";
    document.location = destinationURL;
}

function RedirectToSiteMap() {
    $("#divSem, #divTabDOR, #dvSiteMap, #divReports, #divTabSemaphore, #divGR").remove();
    var url = "../Aysa/ZSiteMap.aspx";
    ShowIFrameModal("", url, 400, 200);
}

function ZDispatcherRedirection_tabResult() {
    SelectTabFromMasterPage('tabsearch');
}

function ZDispatcherRedirection_tabSearchResults() {
    SelectTabFromMasterPage('tabresults');
}

function ZDispatcherRedirection_tabtasklist() {
    SelectTabFromMasterPage("tabtasklist");
    $("#divPrimario").hide();
    $("#divSecundario").show();
}

function ZDispatcherRedirection_tabtasks() {
    SelectTabFromMasterPage("tabtasks");
    $("#divPrimario").hide();
    $("#divSecundario").show();
}

function ShowInsertedDialog() {
    $("#divMessages").dialog({ autoOpen: false, modal: true, position: 'center', closeOnEscape: false, draggable: false, resizable: false, open: function (event, ui) { $(".ui-dialog-titlebar-close").hide(); } });
    $("#divMessages").dialog('open');
}

function CloseInsertedDialog() {
    if ($("#divMessages").dialog("isOpen")) {
        $("#divMessages").dialog("close");
    }
}

function getMainMenuHeightFromParent() {
    var headerHeight = $("#mainMenu").outerHeight();
    return headerHeight;
}

function getMTH() {
    return getMTHeight();
}

function getTBH() {
    return getTBHeight();
}

function SwitchToIFrameClass() {
    $("#second-div-presentation-Main").removeClass("second-div-presentation-Main").removeClass("second-div-IFrame").addClass("second-div-IFrame");
}

function btnHelpCab_Click() {
    window.open("../../forms/" + getPageTheme() + "/InstructivoZamba.pdf");
}

function getMaxZ() {
    var opt = { inc: 5 };
    var def = { inc: 10, group: "*" };
    $.extend(def, opt);
    var zmax = 0;
    $(def.group).each(function () {
        var cur = parseInt($(this).css('z-index'));
        zmax = cur > zmax ? cur : zmax;
    });
    zmax += def.inc;
    return zmax;
}

function Refresh_C() {
    sessionStorage.setItem('abriendo_formulario', '1')
    document.location = document.location;
    document.location.reload(true);
}


var loadingShowing = false;

function ShowLoadingAnimationNoClose() {

    try {
        //if (loadingShowing === false) {
        //    loadingShowing = true;
        //    if (waitingDialog) waitingDialog.show('Procesando');
        //}
    } catch (e) {
        console.error(e);
    }
}
function hideLoading() {
    try {
        // setTimeout(hideLoading, 500);
    } catch (e) {
        console.error(e);
    }
}

function hideLoading() {
    loadingShowing = false;
    if (waitingDialog) waitingDialog.hide();
}

function BlockWebPageInteraction() {
    $("#MasterHeader").block({ message: null, overlayCSS: { backgroundColor: 'transparent' } });
    $("#mainMenu").block({ message: null, overlayCSS: { backgroundColor: 'transparent' } });
    $("#TasksDivUL").block({ message: null, overlayCSS: { backgroundColor: 'transparent' } });
}
function UnblockWebPageInteraction() {
    //$("#MasterHeader").unblock();
    //$("#mainMenu").unblock();
    //$("#TasksDivUL").unblock();
}
//Función que evalua si habilitar o no un control en base a uno o dos valores
function ConditionalEnable(ObjSource, ObjDestiny, Value, Value2) {
    if ($("#" + ObjSource).val() == Value || (Value2 != undefined && $("#" + ObjSource).val() == Value2)) {

        //Quita la clase que ignora el validate y valida el control nuevamente
        $("#" + ObjDestiny).removeClass("disable");
        $("#" + ObjDestiny).valid();
        //Habilita el datepicker
        $("#" + ObjDestiny).datepicker("enable");
        //Muestra el asterisco para marcar que es un campo requerido.
        $("#lbl_" + ObjDestiny).show();
        $("#" + ObjDestiny).attr('disabled', '');

        if ($("#" + ObjDestiny).datepicker("option", "disabled") === undefined) {
            $("#" + ObjDestiny).attr('disabled', 'disabled');
        }
    }
    else {
        $("#" + ObjDestiny).attr('disabled', 'disabled');
        $("#" + ObjDestiny).val("");
        //Agrega la clase que ignora el validate y valida el control nuevamente
        $("#" + ObjDestiny).addClass("disable");
        $("#" + ObjDestiny).valid();
        //Oculta el asterisco para marcar que es un campo requerido.
        $("#lbl_" + ObjDestiny).hide();
        //deshabilita el datepicker
        $("#" + ObjDestiny).datepicker("disable");
    }
}
//Función que evalua si habilitar o no un control en base a uno o dos valores(evalua por distinto)
function ConditionalEnable(ObjSource, ObjDestiny, Value, Value2) {
    if ($("#" + ObjSource).val() == Value || (Value2 != undefined && $("#" + ObjSource).val() == Value2)) {

        //Quita la clase que ignora el validate y valida el control nuevamente
        $("#" + ObjDestiny).removeClass("disable");
        $("#" + ObjDestiny).valid();
        //Habilita el datepicker
        $("#" + ObjDestiny).datepicker("enable");
        //Muestra el asterisco para marcar que es un campo requerido.
        $("#lbl_" + ObjDestiny).show();
        $("#" + ObjDestiny).attr('disabled', '');

        if ($("#" + ObjDestiny).datepicker("option", "disabled") === undefined) {
            $("#" + ObjDestiny).attr('disabled', 'disabled');
        }
    }
    else {
        $("#" + ObjDestiny).attr('disabled', 'disabled');
        $("#" + ObjDestiny).val("");
        //Agrega la clase que ignora el validate y valida el control nuevamente
        $("#" + ObjDestiny).addClass("disable");
        $("#" + ObjDestiny).valid();
        //Oculta el asterisco para marcar que es un campo requerido.
        $("#lbl_" + ObjDestiny).hide();
        //deshabilita el datepicker
        $("#" + ObjDestiny).datepicker("disable");
    }
}
//Función que evalua si habilitar o no un control en base a uno o dos valores(evalua por distinto)
function ConditionalEnableFalse(ObjSource, ObjDestiny, Value, Value2) {
    if ($("#" + ObjSource).val() != Value && (Value2 != undefined && $("#" + ObjSource).val() != Value2)) {
        //Quita la clase que ignora el validate y valida el control nuevamente
        $("#" + ObjDestiny).removeClass("disable");
        $("#" + ObjDestiny).valid();
        //Habilita el datepicker
        $("#" + ObjDestiny).datepicker("enable");
        //Muestra el asterisco para marcar que es un campo requerido.
        $("#lbl_" + ObjDestiny).show();
        $("#" + ObjDestiny).attr('disabled', '');

        if ($("#" + ObjDestiny).datepicker("option", "disabled") === undefined) {
            $("#" + ObjDestiny).attr('disabled', 'disabled');
        }
    }
    else {
        $("#" + ObjDestiny).attr('disabled', 'disabled');
        $("#" + ObjDestiny).val("");
        //Agrega la clase que ignora el validate y valida el control nuevamente
        $("#" + ObjDestiny).addClass("disable");
        $("#" + ObjDestiny).valid();
        //Oculta el asterisco para marcar que es un campo requerido.
        $("#lbl_" + ObjDestiny).hide();
        //deshabilita el datepicker
        $("#" + ObjDestiny).datepicker("disable");
    }
}
//Función que evalua si hacer requerido o no un control en base a uno o dos valores
function ConditionalRequire(ObjSource, ObjDestiny, Value, Value2) {
    if ($("#" + ObjSource).val() == Value || (Value2 != undefined && $("#" + ObjSource).val() == Value2)) {
        $("#" + ObjDestiny).valid();
        $("#lbl_" + ObjDestiny).show();
        $("#" + ObjDestiny).addClass("required");
    }
    else {
        $("#" + ObjDestiny).removeClass("required");
        $("#" + ObjDestiny).valid();
        $("#lbl_" + ObjDestiny).hide();
    }
}
function ConvertToUpperForm() {
    $('.ConvertToUpper').each(function (i, item) {
        var cadena = $(item).val();
        cadena = cadena.toUpperCase();
        $(item).val(cadena);
    });
}

function AddDynamicsTags() {
    if ($('#hdnRuleId').length == 0) {
        $('<input>').attr({
            type: 'hidden',
            id: 'hdnRuleId',
            name: 'hdnRuleId'
        }).appendTo('form');
    }

    if ($('#hdnAsocId').length == 0) {
        $('<input>').attr({
            type: 'hidden',
            id: 'hdnAsocId',
            name: 'hdnAsocId'
        }).appendTo('form');
    }

    if ($('#ZPOSTBACKFUNCTION').length == 0) {
        $('<input>').attr({
            type: 'hidden',
            id: 'ZPOSTBACKFUNCTION',
            name: 'ZPOSTBACKFUNCTION'
        }).appendTo('form');
    }

    if ($('form').length == 1) {
        $('form').each(function () {
            if ($(this).attr('id') == false || $(this).attr('id') == '') {
                $(this).attr('id', 'MainForm');
            }
        })
    }
}

function SelHeaderMenu($t) {
    if ($t.parent().attr("id") == "TasksDivUL") return;
    if ($t.children().attr("id") == "tasklist") $(".RowStyleTasks, RowStyleAsoc").attr("draggable", true);
    var $tA = $t.children("a");
    var $pA = $t.parent().find("a");
    if ($tA.attr("id") == $("#mainMenu").data("activeHeader")) return;
    $("#mainMenu").data("activeHeader", $tA.attr("id"));

    $pA.removeClass("inActiveHeaderTab").removeClass("activeHeaderTab").addClass("inActiveHeaderTab");
    $tA.removeClass("inActiveHeaderTab").addClass("activeHeaderTab");
}

function SelTaskMenu($t) {
    if ($t.parent().attr("id") == "mainMenu") return;
    $t.attr("draggable", true);
    var $tA = $t.children("a");
    var $pA = $t.parent().find("a");
    //if ($tA.attr("id") == $("#TasksDivUL").data("activeTask")) return;
    //$("#TasksDivUL").data("activeTask", $tA.attr("id"));
    $pA.removeClass("inActiveTaskTab").removeClass("activeTaskTab").addClass("inActiveTaskTab");
    $tA.removeClass("inActiveTaskTab").addClass("activeTaskTab").addClass("btn btn-default btn-xs");
}



// funcion para ajustar altos de las tabs.
function setTabsHeight() {
    //Obtengo alto y ancho del navegador.
    var windowHeight = $(window).height();
    var windowWidth = $(window).width();

    //Obtengo alto del header.
    var navBarHeight = $('#MasterHeader > .navbar-header').height();

    //Calculo alto disponible para las tabs.
    var availableHeight = windowHeight - navBarHeight;

    //Asigno el alto disponible a las tabs.
    $('div#MainTabber div.MainTabberTabs').each(function () {
        $(this).height(availableHeight);
    });

    //Aplico padding al body para que no quede nada detras de la navbar.
    $('body').css("padding-top", navBarHeight);

    //Ajuste tab busqueda
    //setTabSearchSize();
    //Ajuste tab grilla de tareas
    //setTabTaskListSize(windowWidth, availableHeight);
    //Ajuste tab tareas
    setTabTasksSize(availableHeight);
    //Ajuste tab Insertar
    setTabInsertSize(availableHeight);
}

function setTabInsertSize(availableHeight) {
    //$('#tabInsert', parent.document).height(availableHeight);
    //$('#tabInsert').height(availableHeight);
    // $('#tabInsert').css("margin-top", "40px");
}

// Establece alto para arbol y panel de indices en tabSearch.
//function setTabSearchSize() {

//    var MasterHeaderHeight = $('#MasterHeader').outerHeight(false);
//    var sbcHeight = $('#GlobalSearchNavBar').length ? $('#GlobalSearchNavBar').outerHeight(true) : 70;
//    $('#SearchControl').css("height", (window.innerHeight - MasterHeaderHeight - sbcHeight) + "px");

//    //var $treeview = $('#treeview').length ? $('#treeview') : null;
//    //var $spnToolbar = $('#spnToolbar').length ? $('#spnToolbar') : null;
//    //var $barraTop = $('#barratop').length ? $('#barratop') : null;
//    //var $dvDocTypesIndexs = $('#dvDocTypesIndexs').length ? $('#dvDocTypesIndexs') : null;

//    //if ($treeview !== null) {
//    //    // Ajuste Arbol
//    //    if ($spnToolbar !== null) {
//    //        $treeview.height((availableHeight - $spnToolbar.outerHeight()));
//    //    }
//    //    else {
//    //        $treeview.height(availableHeight);
//    //    }
//    //    // Ajuste panel de indices
//    //    if ($barraTop !== null) {
//    //        $dvDocTypesIndexs.height(availableHeight - $barraTop.outerHeight(true));
//    //    }
//    //    else {
//    //        $dvDocTypesIndexs.height(availableHeight);
//    //    }
//    //}
//}

// Establece el alto del arbol de tareas en tanTasksList
function setTabTaskListSize(windowWidth, availableHeight) {
    // si existe el arbol
    if ($('#divTreeContainer').length > 0) {
        $('#divTreeContainer').css('overflow', 'auto');
        // le establezco alto de la pantalla menos barra botones
        $('#divTreeContainer').height(availableHeight - $("#spnToolbarTasksTree").outerHeight());
    }

    // A la grilla tambien
    if ($('#TaskGridContent').length) {
        $('#TaskGridContent').css('overflow', 'auto');
        var navbarFilters = $('#ContentPlaceHolder_TaskGrid_ucTaskGrid_pnlFilters');

        if (navbarFilters.length && navbarFilters.outerHeight() > 0) {
            $('#TaskGridContent').height(availableHeight - navbarFilters.outerHeight());
        }
        else {
            $('#TaskGridContent').height($('#divTreeContainer').height());
        }
    }
}

// Establece el alto de tabTask.
function setTabTasksSize(availableHeight) {
    // Para el tabTasks, al iframe con la tarea, le doy el mismo alto menos el alto de las tabs
    if ($('#rowTaskDetail').length > 0) {
        var $taskDivdUlHeight = $('#TasksDivUL').length ? $('#TasksDivUL').height() : $('#TasksDivUL', parent.document).height();
        var $rowTaskHeaderHeight = $('#rowTaskHeader').length ? $('#rowTaskHeader').height() : 0;

        if ($taskDivdUlHeight > 0) {
            if ($rowTaskHeaderHeight > 0) {
                $('#rowTaskDetail').css('height', (availableHeight - $taskDivdUlHeight - $rowTaskHeaderHeight));
            }
            else {
                $('#rowTaskDetail').css('height', (availableHeight - $taskDivdUlHeight));
            }
        }
        else {
            $('#rowTaskDetail').css('height', availableHeight);
        }
    }
}

//funcion para ocultar o mostrar panel lateral en listado de tareas y tab busqueda.
function toggleSidePanel(e) {
    if ($(e.target).parents('#tabtasklist').length) {
        togglePanel($('#tabtasklist').find('.slideSidePanel'), $('#tabtasklist').find('.slideMainPanel'), e)
    }
    else {
        togglePanel($('#tabsearch').find('.slideSidePanel'), $('#tabsearch').find('.slideMainPanel'), e)
    }
};

function togglePanel(sidePanel, mainPanel, e) {
    if (sidePanel !== null) {
        var visible = (sidePanel.width() > 0) ? true : false;
        if (e.type === 'click') {
            if (visible) {
                hidePanel(sidePanel, mainPanel);
                //event.stopPropagation();
                //e.stopimmediatepropagation()
            }

            else
                showPanel(sidePanel, mainPanel);
            //e.stopimmediatepropagation()

        }
        else {
            if ($(window).width() > 768) {
                if (!visible)
                    showPanel(sidePanel, mainPanel);
                //e.stopimmediatepropagation()

            }
        }
    }
}

function showPanel(sidePanel, mainPanel) {
    sidePanel.removeClass("sidePanelHidden");
    mainPanel.removeClass("contentPanel");
    $('.btnToggleSidePanel').removeClass("glyphicon glyphicon-menu-right").addClass("glyphicon glyphicon-menu-left");

    if ($('#ContentPlaceHolder_Arbol_btnInsertar').length)
        $('#ContentPlaceHolder_Arbol_btnInsertar').removeClass("nonVisible");
}

function hidePanel(sidePanel, mainPanel) {
    sidePanel.addClass("sidePanelHidden");
    mainPanel.addClass("contentPanel");

    $('.btnToggleSidePanel').removeClass("glyphicon glyphicon-menu-left").addClass("glyphicon glyphicon-menu-right");

    if ($('#ContentPlaceHolder_Arbol_btnInsertar').length)
        $('#ContentPlaceHolder_Arbol_btnInsertar').addClass("nonVisible");
}






function GetUID() {
    var userid = 0;
    userid = getUrlParameters().user;
    if (userid > 0) return userid;
    userid = getUrlParameters().userid;
    if (userid > 0) return userid;
    userid = getUrlParameters().u;
    if (userid > 0) return userid;

    var e = $('#hdnUserId');

    if (e.length == 0) e = $('#ContentPlaceHolder_hdnUserId');
    if (e.length == 0) e = $('#ctl00_hdnUserId');
    if (e.length == 0) e = $('#hdnUserId');
    if (e.length == 0) e = $('hdnUserId');

    userid = $(e).val();
    if (userid > 0) return userid;


    var authData = window.localStorage.getItem('authorizationData');
    if (authData == undefined || authData == null || authData == "[object Object]" || authData == "") {
        return;
    }

    authData = JSON.parse(authData);
    if (authData == null || authData.UserId == undefined) {
        userid = authData.UserId;
        if (userid > 0) return userid;

    }

    return;
};

function hideEmailList(evento) {
    var IdInput = evento.target.id

    $("#AutoCompleteController input").each(function (i, item) {
        if (evento.target.tagName == "INPUT" && item.id != IdInput.toString()) {
            if ($("#ListEmails" + item.id)[0].style.display != "none") {
                $("#ListEmails" + item.id)[0].style.display = "none";
            }

        } else if (IdInput == "hidePanel" || IdInput == "hidePanelZip") {
            $(".ListEmails").each(function (A, B) {
                B.style.display = "none";

                $("#hidePanel")[0].style.display = "none";

                if ($("#hidePanelZip")[0]) {
                    $("#hidePanelZip")[0].style.display = "none";
                }
            })
        }
        else if (evento.target.tagName != "INPUT" || evento.target.tagName == "BUTTON") {
            $(".ListEmails").each(function (A, B) {
                B.style.display = "none";
                $("#hidePanel")[0].style.display = "none";

                if ($("#hidePanelZip")[0]) {
                    $("#hidePanelZip")[0].style.display = "none";
                }

            })
        }
    });
}

function GetDOCID() {
    var docid = 0;
    docid = getUrlParameters().docid;
    if (docid > 0) return docid;
    docid = getUrlParameters().did;
    if (docid > 0) return docid;
    docid = getUrlParameters().doc_id;
    if (docid > 0) return docid;
    docid = Number($("[id$=Hiddendocid]").val());
    if (docid > 0) return docid;
    docid = currentDOCID;
    if (docid > 0) return docid;
    return 0;
}

function GetTASKID() {
    var taskid = 0;
    taskid = getUrlParameters().taskid;
    if (taskid > 0) return taskid;
    taskid = getUrlParameters().tid;
    if (taskid > 0) return taskid;
    taskid = getUrlParameters().task_id;
    if (taskid > 0) return taskid;
    return 0;
}

function GetDocTypeId() {

    var docTypeId = 0;
    try {

        docTypeId = getUrlParameters().doctype;

        if (docTypeId > 0)
            return docTypeId;

        docTypeId = getUrlParameters().doc_type_id;

        if (docTypeId > 0)
            return docTypeId;

        docTypeId = getUrlParameters().dt_id;

        if (docTypeId > 0)
            return docTypeId;

        docTypeId = getUrlParameters().eid;

        if (docTypeId > 0)
            return docTypeId;


        return docTypeId;
    } catch (e) {

        console.error("No se pudo obtener el docTypeId", e);
        return docTypeId;

    }
}

function getUrlParameters() {
    try {

        console.log('window.location: ', window.location);
        console.log('window.location.search: ', window.location.search);
        var pairs = window.location.search.substring(1).split(/[&?]/);
        var res = {}, i, pair;
        for (i = 0; i < pairs.length; i++) {
            pair = pairs[i].toLowerCase().split('=');
            if (pair[1])
                res[decodeURIComponent(pair[0])] = decodeURIComponent(pair[1]);
        }
        return res;

    } catch (e) {
        console.error(e);
        return 0;
    }
}


function getUrlParametersUser() {
    var pairs = window.location.search.substring(1).split(/[&?]/);
    var res = {}, i, pair;
    for (i = 0; i < pairs.length; i++) {
        pair = pairs[i].toLowerCase().split('=');
        if (pair[1])
            res[decodeURIComponent(pair[0])] = decodeURIComponent(pair[1]);
    }
    return res;
}

function CheckUserTimeOut() {
    var CurrentUserId = window.localStorage.getItem('UserId')
    var ConnectionId = window.localStorage.getItem('ConnId');
    if (CurrentUserId != null && ConnectionId != null) {
        $.ajax({
            type: "POST",
            url: "../../Services/TaskService.asmx/CheckTimeOut",
            data: "{CurrentUserId: " + CurrentUserId + ", ConnectionId: " + ConnectionId + "}",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.d == true) {
                    parent.$('#openModalTimeout').modal(); $('#openModalTimeout').draggable();
                    ConnectionId = 0;
                    CurrentUserId = 0;
                }
                else {
                    return false;
                }
            },
        });
    }
    else {
        $('#openModalTimeout').modal().draggable();
    }
}

function showfilters() {
    if ($("#InputData").hasClass("IsShowing")) {
        $("#filterz").css({
            "display": "inline-table !important",
            "left": "73% "
        });
    }
    else {
        $("#filterz").css({
            "display": "inline-table !important",
            "left": "49%"
        });
    }
    $("#filterz").slideToggle();
}

function EnableDropDown() {
    $(".dropdown-toggle, .customBtn").dropdown();
}

function GetSelectedTextValue(ddlComputedColumns) {
    var selectedText = ddlComputedColumns.options[ddlComputedColumns.selectedIndex].innerHTML;
    var IndexId = ddlComputedColumns.value;
    $('#IndexValue').val(IndexId);
    $("#InputData").css("display", "block").addClass("IsShowing"); //Muesto Tipo de Indice    
}

function getTextValue() {
    var dropvalue = $('#DropVal').find(":selected").text();
    $("#CompareOpe").val(dropvalue);
};

function takeIdSubList(d) {
    $("#IdSubList").val("");
    var scope = angular.element($("body")).scope(); //toma el Id cuando el dropdown es una tabla de sustitucion
    var SubList = $(d).find("td").html().replace(/\s/g, '');
    var SubListName = $(d).find("td").next().html().replace(/\s/g, '');
    //$("#SubListName").val(SubListName); // guardo nombre para utilzarlos en los filtros aplicados
    $("#IdSubList").val(SubList); // guardo Id para enviarlo al service
    sessionStorage.setItem('SubListName', SubListName);
}

function NewFilter() {
    if (document.config == undefined) document.config = parent.document.config;
    var filterdata = {};
    filterdata.CurrentUserId = $("[id$=hdnUserId]").val();
    filterdata.StepId = $("[id$=StepId]").val();
    filterdata.IndexId = $("#IndexValue").val();
    filterdata.CompareOperator = $('#CompareOpe').val();

    if ($("#IdSubList").val() != "") {
        filterdata.ValueString = $("#IdSubList").val();
    }
    else {
        filterdata.ValueString = $(".InputVal").children().val();
    }

    if (filterdata && $(".InputVal").children().val() != "") {
        $.ajax({
            type: "POST",
            url: thisDomain + "/Services/TaskService.asmx/filterdata",
            //data: "{CurrentUserId: " + CurrentUserId + ", ConnectionId: " + ConnectionId + "}",
            //data: JSON.stringify("{data: " + obj + "}"),
            data: "{filterdata:" + JSON.stringify(filterdata) + "}",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                toastr.success("Filtro aplicado");
                $("#IdSubList").val("");
                GetUsedFilters();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                toastr.error('Error al aplicar filtro filtro');
            }
        });
    }
    else {
        toastr.error('Complete todos los campos');
    }
}

function DeleteFilter(button) {
    if (document.config == undefined) document.config = parent.document.config;
    var id = $(button.parentNode).children("a").attr("data-index");
    var FilterDelete = {};
    FilterDelete.CurrentUserId = $("[id$=hdnUserId]").val();
    FilterDelete.StepId = $("[id$=StepId]").val();
    FilterDelete.Id = id;
    $.ajax({
        type: "POST",
        url: thisDomain + "/Services/TaskService.asmx/deleteFilter",
        //data: "{CurrentUserId: " + CurrentUserId + ", ConnectionId: " + ConnectionId + "}",
        //data: "{filterId: " + id + "}",
        data: "{deleteFilter:" + JSON.stringify(FilterDelete) + "}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#IdSubList").val("");
        },
    });
}

//obtengo los filtros aplicados y los muestro en el dropdown
function GetUsedFilters() {
    var usedfilters = {};
    usedfilters.CurrentUserId = $("[id$=hdnUserId]").val();
    usedfilters.StepId = $("[id$=StepId]").val();
    var SubListName = sessionStorage.getItem('SubListName');
    $.ajax({
        type: "POST",
        url: "../../Services/TaskService.asmx/getUsedFilters",
        //data: "{CurrentUserId: " + CurrentUserId + ", ConnectionId: " + ConnectionId + "}",
        //data: JSON.stringify("{data: " + obj + "}"),
        data: "{usedfilters:" + JSON.stringify(usedfilters) + "}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var swf = data.d;
            //$('#filterz, #counter').empty();
            $.each(swf, function (key, val) {
                $('#filterz').append('<li><a name="tabs" data-index="' + val + '" href=\"#\">"' + key + '" </a><button id="btnDeleteFilter" onclick="DeleteFilter(this)" class="btn btn-default btn-xs fa fa-trash"></button> </li>');
            });
            var count = 0;
            for (var i in swf) {
                if (swf.hasOwnProperty(i)) count++;
                $("#counter").text('(' + count.toString() + ')');    //agrego los filtros a la lista desplegable
            }
        },
    });
}

function GetOpenRules() {
    //solucionar al cerra session,al cerrar ventana ya esta
    // var value = sessionStorage.getItem('key');
    // if (value == null) {
    // $.ajax({
    // type: "POST",
    // url: "../../Services/TaskService.asmx/OpenRules",
    // data: "",
    // contentType: "application/json; charset=utf-8",
    // success: function (data) {
    // var ruleid = data.d;
    // sessionStorage.removeItem('key');
    // sessionStorage.setItem('key', ruleid);
    // if (ruleid != "") {
    // RuleButtonClick(ruleid);
    // }
    // },
    // });
    // }
}

function GoToTabHome() {
    if ($('#MainTabber').length > 0)
        $('#MainTabber').zTabs("select", 'tabhome');
    else
        parent.$('#MainTabber').zTabs("select", 'tabhome');
}

function LocationUrl() {
    var url = window.location.href.split('/');
    var baseUrl = url[0] + '//' + url[2] + '//' + url[3];
    return baseUrl;
}



//-----------------------------------------------------------------------------Zamba.Angular---------------------------------

function PrintBarcode(path, CopiesCount, Width, Height) {

    console.log("Ejecutando PrintBarcode. Archivo: " + path + ". Copias: " + CopiesCount + ". Ancho: " + Width + ". Alto: " + Height);

    $.ajax({
        type: "POST",
        url: ZambaWebRestApiURL + '/Tasks/GeneratePdfCoverPage?' + jQuery.param({ TempPath: path, CopiesCount: CopiesCount, Width: Width, Height: Height }),
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (r) {
            PrintFile(LocationUrl() + '/' + r)
        },
    });

}

function PrintFile(path) {
    console.log("Ejecutando PrintFile con documento: " + path);
    if (IsIE()) {
        window.open(path);
    }
    else {
        window.parent.$('#printFrame').on('load', function () {
            console.log("Frame cargado");
            var frame = window.parent.document.getElementById('printFrame');
            frame = frame.contentWindow;
            frame.focus();
            frame.print();
        });
        window.parent.$('#printFrame').attr('src', path);
    }
}

function IsIE() {

    try {
        var ua = window.navigator.userAgent;
        var msie = ua.indexOf("MSIE ");
        if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))
            return true;
        return false;
    }
    catch (e) {
        console.error(e);
    }
}



function resizeGridHeight() {
    try {
        var resultsGrid = $("#resultsGrid")[0];
        var resultsGridHeight = resultsGrid.clientHeight;

        var ToolbarResults = $("#ToolbarResults")[0];
        var ToolbarResultsHeight = ToolbarResults.clientHeight;

        var resultsGridActions = $("#resultsGridActions")[0];
        var resultsGridActionsHeight = resultsGridActions.clientHeight;

        var rdo = resultsGridHeight - (ToolbarResultsHeight + resultsGridActionsHeight)

        var KAutoScrollable = $(".k-grid-content.k-auto-scrollable")[0];
        var KGridHeader = $(".k-grid-header")[0];
        var ThisTaskIsOpen = $("#IFThisTaskIsOpen")[0];

        $("#IFPreview").css("height", rdo);
        $(ThisTaskIsOpen).css("height", rdo);
        $(KAutoScrollable).css("height", rdo - (KGridHeader != undefined ? KGridHeader.clientHeight : 0));
    } catch (e) {
        console.error(e);
    }
}

//----------------------------------------------------------------------------------------------------------------------------




//agrega ceros a la izquierda sobre el texto Punto de Venta y Nro Comprobante



function tab_btn(event) {

    if ($("#zamba_index_72").val().length > 5) {


        swal("Se puede ingresar solo 4 caracteres");


        document.getElementById("zamba_index_72").value = "";

        return false;
    }

    else






        var t = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;

    if (t == 9) {

        var variablekey = document.getElementById("zamba_index_72").value;
        var VariableConcatenada;

        var ConteoDeNumeros = variablekey.length;



        switch (ConteoDeNumeros) {

            case 1:

                VariableConcatenada = "000" + variablekey;

                document.getElementById("zamba_index_72").value = VariableConcatenada;

                break;
            case 2:

                VariableConcatenada = "00" + variablekey;

                document.getElementById("zamba_index_72").value = VariableConcatenada;



                break;

            case 3:

                VariableConcatenada = "0" + variablekey;

                document.getElementById("zamba_index_72").value = VariableConcatenada;


                break;

            case 4:


                document.getElementById("zamba_index_72").value = variablekey;


                break;


            default:


                if (ConteoDeNumeros > 4) {



                    document.getElementById("zamba_index_72").value = variablekey.substring(0, 4);



                }





        }



    }



    var elementoactivo = document.activeElement.name



    if (elementoactivo == "zamba_index_72") {


        window.onclick = FuncionCeros

    }

}



function FuncionCeros() {


    var variablekey = document.getElementById("zamba_index_72").value;
    var VariableConcatenada;

    var ConteoDeNumeros = variablekey.length;



    switch (ConteoDeNumeros) {

        case 1:

            VariableConcatenada = "0000" + variablekey;

            document.getElementById("zamba_index_72").value = VariableConcatenada;

            break;
        case 2:

            VariableConcatenada = "000" + variablekey;

            document.getElementById("zamba_index_72").value = VariableConcatenada;



            break;

        case 3:

            VariableConcatenada = "00" + variablekey;

            document.getElementById("zamba_index_72").value = VariableConcatenada;


            break;

        case 4:

            VariableConcatenada = "0" + variablekey;

            document.getElementById("zamba_index_72").value = VariableConcatenada;


            break;

        case 5:


            document.getElementById("zamba_index_72").value = variablekey;


            break;


        default:


            if (ConteoDeNumeros > 5) {



                document.getElementById("zamba_index_72").value = variablekey.substring(0, 5);



            }





    }




}


///////////////////////////////////////////////////////////////////////






function tab_btn1(event) {

    if ($("#zamba_index_10").val().length >= 8) {


        swal("Se puede ingresar solo 8 caracteres");


        document.getElementById("zamba_index_10").value = "";

        return false;
    }

    else



        var tt = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;


    if (tt == 9) {

        var variablekey1 = document.getElementById("zamba_index_10").value;
        var VariableConcatenada1;

        var ConteoDeNumeros1 = variablekey1.length;



        switch (ConteoDeNumeros1) {

            case 1:

                VariableConcatenada1 = "0000000" + variablekey1;

                document.getElementById("zamba_index_10").value = VariableConcatenada1;

                break;
            case 2:

                VariableConcatenada1 = "000000" + variablekey1;

                document.getElementById("zamba_index_10").value = VariableConcatenada1;



                break;

            case 3:

                VariableConcatenada1 = "00000" + variablekey1;

                document.getElementById("zamba_index_10").value = VariableConcatenada1;


                break;

            case 4:

                VariableConcatenada1 = "0000" + variablekey1;


                document.getElementById("zamba_index_10").value = VariableConcatenada1;


                break;

            case 5:

                VariableConcatenada1 = "000" + variablekey1;


                document.getElementById("zamba_index_10").value = VariableConcatenada1;


                break;

            case 6:

                VariableConcatenada1 = "00" + variablekey1;


                document.getElementById("zamba_index_10").value = VariableConcatenada1;




                break;


            case 7:

                VariableConcatenada1 = "0" + variablekey1;


                document.getElementById("zamba_index_10").value = VariableConcatenada1;




                break;




            case 8:


                document.getElementById("zamba_index_10").value = variablekey1;



                break;


            default:




                if (ConteoDeNumeros1 > 8) {



                    document.getElementById("zamba_index_10").value = variablekey1.substring(0, 8);



                }





        }



    }



    var elementoactivo1 = document.activeElement.name



    if (elementoactivo1 == "zamba_index_10") {


        window.onclick = FuncionCeros1

    }





}

function FuncionCeros1() {


    var variablekey = document.getElementById("zamba_index_10").value;
    var VariableConcatenada;

    var ConteoDeNumeros = variablekey.length;



    switch (ConteoDeNumeros) {

        case 1:

            VariableConcatenada = "0000000" + variablekey;

            document.getElementById("zamba_index_10").value = VariableConcatenada;

            break;
        case 2:

            VariableConcatenada = "000000" + variablekey;

            document.getElementById("zamba_index_10").value = VariableConcatenada;



            break;

        case 3:

            VariableConcatenada = "00000" + variablekey;

            document.getElementById("zamba_index_10").value = VariableConcatenada;


            break;

        case 4:


            VariableConcatenada = "0000" + variablekey;

            document.getElementById("zamba_index_10").value = VariableConcatenada;


            break;


        case 5:


            VariableConcatenada = "000" + variablekey;

            document.getElementById("zamba_index_10").value = VariableConcatenada;


            break;


        case 6:


            VariableConcatenada = "00" + variablekey;

            document.getElementById("zamba_index_10").value = VariableConcatenada;


            break;

        case 7:


            VariableConcatenada = "0" + variablekey;

            document.getElementById("zamba_index_10").value = VariableConcatenada;

            break;


        default:

            if (ConteoDeNumeros > 8) {

                document.getElementById("zamba_index_10").value = variablekey.substring(0, 8);

            }



    }


}




function ponerdecimales(numero) {


    var tt = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;

    if (tt == 13 || tt == 9 || tt == undefined) {

        if (tt == 13) {
            event.preventDefault();
        }
        var variablekey = document.getElementById("zamba_index_109").value;



        if (variablekey == "") {

            document.getElementById("zamba_index_109").value = "000";


        }

        if (variablekey.indexOf(',') == -1 & variablekey != "" & variablekey.indexOf(',00') == -1) {

            document.getElementById("zamba_index_109").value = variablekey;


        }


        if (variablekey.indexOf(',') == -1 && variablekey != "") {

            document.getElementById("zamba_index_109").value = variablekey + ",00";


        }

        document.getElementById("zamba_index_109").value = document.getElementById("zamba_index_109").value.replace('.', '').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ".")

    }
    if (tt == 188) {
        event.preventDefault();
    }
}



function AssociatedIndexsModalselectBtn(sender) {
    var IndexListSelectedValue = sender.id;
    var NewAssociatedEntityId = window.localStorage.getItem("NewAssociatedEntityId");
    var NewAssociatedDocIds = window.localStorage.getItem("NewAssociatedDocIds");
    var NewAssociatedIndexIds = window.localStorage.getItem("NewAssociatedIndexIds");
    var ruleId = window.localStorage.getItem("NewAssociatedruleId");
    var docId = window.localStorage.getItem("NewAssociateddocId");

    var List_FilesIds = JSON.parse(sessionStorage.getItem('Lista_Archivos'));

    if (typeof NewAssociatedDocIds === 'string') {
        NewAssociatedDocIds = [NewAssociatedDocIds];
    }

    //for (i = 0; i < NewAssociatedDocIds.length; i++) {
    for (i = 0; i < List_FilesIds.length; i++) {
        $.ajax({
            type: "POST",
            url: ZambaWebRestApiURL + '/Tasks/UpdateTaskIndex?' + jQuery.param({ DoCTypeId: NewAssociatedEntityId, DocId: List_FilesIds[i], IndexId: NewAssociatedIndexIds, Data: IndexListSelectedValue }),
            //url: ZambaWebRestApiURL + '/Tasks/UpdateTaskIndex?' + jQuery.param({ DoCTypeId: NewAssociatedEntityId, DocId: NewAssociatedDocIds[i], IndexId: NewAssociatedIndexIds, Data: IndexListSelectedValue }),
            //url: ZambaWebRestApiURL + '/Tasks/Call_UpdateTaskIndex?' + jQuery.param({ DoCTypeId: NewAssociatedEntityId, DocId: emi[i], IndexId: NewAssociatedIndexIds, Data: IndexListSelectedValue }),
            contentType: "application/json; charset=utf-8",
            async: false,
            success: function (data) {
                toastr.success("Archivo actualizado exitosamente");
                $("#ModalSearchDropzone").modal('hide');
                try {
                    var NewAssociated = window.localStorage.getItem("NewAssociatedEntityId");
                    LoadGrillaForm(NewAssociated);

                } catch (e) {

                }
            },
            error: function (error) {
                console.log(error);
                toastr.success("Archivo actualizado exitosamente");
                $("#ModalSearchDropzone").modal('hide');
            }
        });

        if (ruleId != "undefined" && ruleId != null) {
            ExecuteRuleIdDropzone(ruleId, List_FilesIds[i]);
        }
        sessionStorage.removeItem('Lista_Archivos');
    }
}


function ExecuteRuleId(ruleId, resultIds) {
    var genericRequest = {
        UserId: parseInt(GetUID()),
        Params: {
            "ruleId": ruleId,
            "resultIds": resultIds
        }
    };

    $.ajax({
        "async": false,
        "url": ZambaWebRestApiURL + '/tasks/ExecuteTaskRule',
        "method": "POST",
        "headers": {
            "content-type": "application/json"
        },
        "success": function () {
            console.log('Se ha ejecutado la acción');
            return true;
        },
        "error": function () {
            console.log('Error al ejecutar acción');
            return false;
        },
        "data": JSON.stringify(genericRequest)
    });


}

function selectOpcionMultiIndex(sender) {

    window.localStorage.setItem("NewAssociatedIndexIdsMultiDropzone", sender.id);

    $("#AsignedEntity")[0].innerHTML = sender.outerText.split("-")[1];

    $("#ModalMultiIndexDropzone").modal('hide');//ocultamos el modal
    $('body').removeClass('modal-open');//eliminamos la clase del body para poder hacer scroll
    $('.modal-backdrop').remove();//eliminamos el backdrop del modal

    $("#filtrarIndex").val("");
}

function LoadIndexForDropzone(indexId) {
    var ElementoJson = "";
    $.ajax({
        type: "POST",
        url: ZambaWebRestApiURL + '/Tasks/GetMultiIndexsDropzon?' + jQuery.param({ indexId: indexId }),
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (data) {
            ElementoJson = JSON.parse(data);

        }, error: function (error) {
            console.log(error);
        }

    });
    return ElementoJson;
}


function FuncionDropzone(indexIds, entityId, newDocIds) {

    window.localStorage.setItem("NewAssociatedIndexIds", indexIds);
    window.localStorage.setItem("NewAssociatedEntityId", entityId);
    window.localStorage.setItem("NewAssociatedDocIds", newDocIds);
    if ($('#listaID')[0].childElementCount == 0) {

        // ajax pedir los datos
        var a = '';
        $.ajax({
            type: "POST",
            url: ZambaWebRestApiURL + '/Tasks/GetAsociatedIndexsDropzon?' + jQuery.param({ indexId: indexIds, entityId: entityId }),
            contentType: "application/json; charset=utf-8",
            async: false,
            success: function (data) {
                a = data;
                var ElementoJson = JSON.parse(a);
                // console.log(ElementoJson.length);
                $("#ModalSearchDropzone").modal();
                if ($(".modal-backdrop").hasClass("in") == true) {
                    $(".modal-backdrop").css("position", "relative");
                }
                $("#listaID").empty();
                for (var i = 0; i < ElementoJson.length; i++) {
                    var name = ElementoJson[i].Value,
                        id = ElementoJson[i].$id,
                        Code = ElementoJson[i].Code;
                    $('#listaID').append('<li class="ModalClassUl" id="' + Code + '" name="' + name.toString() + '"  onclick="AssociatedIndexsModalselectBtn(this);">' + Code + ' - ' + name.toString() + '</li>');
                }
            }, error: function (error) {
                console.log(error);
            }

        });

    }
    else {
        $("#ModalSearchDropzone").modal();
        if ($(".modal-backdrop").hasClass("in") == true) {
            $(".modal-backdrop").css("position", "relative");
        }
    }

}


function dropzoneCallAjax(indexIds, entityId) {
    // ajax pedir los datos
    var a = '';
    $.ajax({
        type: "POST",
        url: ZambaWebRestApiURL + '/Tasks/GetAsociatedIndexsDropzon?' + jQuery.param({ indexId: indexIds, entityId: entityId }),
        contentType: "application/json; charset=utf-8",
        async: true,
        success: function (data) {
            a = data;
            var ElementoJson = JSON.parse(a);
            // console.log(ElementoJson.length);
            $("#ModalSearchDropzone").modal();
            if ($(".modal-backdrop").hasClass("in") == true) {
                $(".modal-backdrop").css("position", "relative");
            }
            $("#listaID").empty();
            for (var i = 0; i < ElementoJson.length; i++) {
                var name = ElementoJson[i].Value,
                    id = ElementoJson[i].$id,
                    Code = ElementoJson[i].Code;
                if (Code != 'PF') {
                    $('#listaID').append('<li class="ModalClassUl" id="' + Code + '" name="' + name.toString() + '"  onclick="AssociatedIndexsModalselectBtn(this);">' + Code + ' - ' + name.toString() + '</li>');
                }
            }
            var NewAssociated = window.localStorage.getItem("NewAssociatedEntityId") + window.localStorage.getItem("NewAssociatedIndexIds");
            window.localStorage.setItem("ItemCache", NewAssociated);
        }, error: function (error) {
            console.log(error);
        }

    });
}

function ExecuteRuleIdDropzone(ruleId, resultIds) {
    var genericRequest = {
        UserId: parseInt(GetUID()),
        Params: {
            "ruleId": ruleId,
            "resultIds": resultIds
        }
    };

    $.ajax({
        "async": false,
        "url": ZambaWebRestApiURL + '/tasks/ExecuteTaskRule',
        "method": "POST",
        "headers": {
            "content-type": "application/json"
        },
        "success": function () {
            toastr.success('Se ha ejecutado la acción');

        },
        "error": function () {
            toastr.error('Error al ejecutar acción');
        },
        "data": JSON.stringify(genericRequest)
    });
}

function RemoveStyleMultiDropzone() {
    var CountDropzone = $("zamba-drop");
    for (var i = 0; i < CountDropzone.length; i++) {
        var dropzamba = $("zamba-drop")[i].id;
        if (dropzamba > 0) {
            var eventoStyle = $($("zamba-drop")[i]).children(2)[2].children["0"].style[0];
            if (eventoStyle == "pointer-events") {
                $($("zamba-drop")[i]).children(2)[2].children["0"].removeAttribute("style");
            }
        }
    }
}

function ReloadParentPage() {
    opener.location.reload();

    setTimeout(function () {
        window.close();
    }, 1000);
}

function FixsIE() {
    try {
        if (!Array.prototype.includes) {
            Object.defineProperty(Array.prototype, "includes", {
                enumerable: false,
                value: function (obj) {
                    var newArr = this.filter(function (el) {
                        return el == obj;
                    });
                    return newArr.length > 0;
                }
            });
        }

        if (!Array.prototype.indexOf) {
            Array.prototype.indexOf = function (obj, start) {
                for (var i = (start || 0), j = this.length; i < j; i++) {
                    if (this[i] === obj) { return i; }
                }
                return -1;
            }
        }

        //https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String/repeat
        if (!String.prototype.repeat) {
            String.prototype.repeat = function (count) {
                'use strict';
                if (this == null)
                    throw new TypeError('can\'t convert ' + this + ' to object');

                var str = '' + this;
                // To convert string to integer.
                count = +count;
                // Check NaN
                if (count != count)
                    count = 0;

                if (count < 0)
                    throw new RangeError('repeat count must be non-negative');

                if (count == Infinity)
                    throw new RangeError('repeat count must be less than infinity');

                count = Math.floor(count);
                if (str.length == 0 || count == 0)
                    return '';

                if (str.length * count >= 1 << 28)
                    throw new RangeError('repeat count must not overflow maximum string size');

                var maxCount = str.length * count;
                count = Math.floor(Math.log(count) / Math.log(2));
                while (count) {
                    str += str;
                    count--;
                }
                str += str.substring(0, maxCount - str.length);
                return str;
            }
        }

        // https://github.com/uxitten/polyfill/blob/master/string.polyfill.js
        // https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String/padStart
        if (!String.prototype.padStart) {
            String.prototype.padStart = function padStart(targetLength, padString) {
                targetLength = targetLength >> 0; //truncate if number, or convert non-number to 0;
                padString = String(typeof padString !== 'undefined' ? padString : ' ');
                if (this.length >= targetLength) {
                    return String(this);
                } else {
                    targetLength = targetLength - this.length;
                    if (targetLength > padString.length) {
                        padString += padString.repeat(targetLength / padString.length); //append to original to ensure we are longer than needed
                    }
                    return padString.slice(0, targetLength) + String(this);
                }
            };
        }

    } catch (e) {

    }
}

//Funcion que permite reemplazar todas las coincidencias de un caracter por otro pasados por parametro.
//String.prototype.replaceAll = function (search, replacement) {
//    return this.split(search).join(replacement);
//};

function escapeRegExp(string) {
    return string.replace(/[.*+?^${}()|[\]\\]/g, '\\$&'); // $& means the whole matched string
}

function replaceAll(str, find, replace) {
    return str.replace(new RegExp(escapeRegExp(find), 'g'), replace);
}

function CompletarVto() {
    if (document.getElementById("zamba_index_57").value == "") {
        $("#zamba_index_57").addClass("error");
    } else {
        $("#zamba_index_57").removeClass("error");
        var dias = $("#zamba_index_1525190").val();
        var fecha = $("#zamba_index_57").val();

        fecha = CalcularLaborales(dias, fecha);
        if (dias == "" || dias == null) {
            $("#zamba_index_2705").val("0");

        } else {

            $("#zamba_index_2705").val(fecha);
        }

    }
}


function CalcularLaborales(n, fecIni) {
    //Suma a una fecha determinada X cantidad de días hábiles y devuelve la fecha calculada.
    var dd = GetDD(fecIni);
    var mm = GetMM(fecIni);
    var yy = GetYY(fecIni);
    var fec = new Date(yy, mm, dd);
    return fec.SumarLaborables(n).format("d/m/Y");
}

Date.prototype.SumarLaborables = function (n) {
    //Método para sumar n dias hábiles.
    for (var i = 0; i < n; i++) {
        this.setTime(this.getTime() + 24 * 60 * 60 * 1000);
        //Si es sábado o domingo se cuenta otro día. 
        if ((this.getDay() == 6) || (this.getDay() == 0))
            i--;
    }
    return this;
}

Date.prototype.format = function (format) {
    var returnStr = '';
    var replace = Date.replaceChars;
    for (var i = 0; i < format.length; i++) {
        var curChar = format.charAt(i);
        if (i - 1 >= 0 && format.charAt(i - 1) == "\\") {
            returnStr += curChar;
        }
        else if (replace[curChar]) {
            returnStr += replace[curChar].call(this);
        } else if (curChar != "\\") {
            returnStr += curChar;
        }
    }
    return returnStr;
};


Date.replaceChars = {
    shortMonths: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
    longMonths: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
    shortDays: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
    longDays: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],

    //Day
    d: function () { return (this.getDate() < 10 ? '0' : '') + this.getDate(); },
    D: function () { return Date.replaceChars.shortDays[this.getDay()]; },
    j: function () { return this.getDate(); },
    l: function () { return Date.replaceChars.longDays[this.getDay()]; },
    N: function () { return this.getDay() + 1; },
    S: function () { return (this.getDate() % 10 == 1 && this.getDate() != 11 ? 'st' : (this.getDate() % 10 == 2 && this.getDate() != 12 ? 'nd' : (this.getDate() % 10 == 3 && this.getDate() != 13 ? 'rd' : 'th'))); },
    w: function () { return this.getDay(); },
    z: function () { var d = new Date(this.getFullYear(), 0, 1); return Math.ceil((this - d) / 86400000); }, //Fixed now
    //Week
    W: function () { var d = new Date(this.getFullYear(), 0, 1); return Math.ceil((((this - d) / 86400000) + d.getDay() + 1) / 7); }, //Fixed now
    //Month
    F: function () { return Date.replaceChars.longMonths[this.getMonth()]; },
    m: function () { return (this.getMonth() < 9 ? '0' : '') + (this.getMonth() + 1); },
    M: function () { return Date.replaceChars.shortMonths[this.getMonth()]; },
    n: function () { return this.getMonth() + 1; },
    t: function () { var d = new Date(); return new Date(d.getFullYear(), d.getMonth(), 0).getDate() }, //Fixed now, gets #days of date
    //Year
    L: function () { var year = this.getFullYear(); return (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0)); },	//Fixed now
    o: function () { var d = new Date(this.valueOf()); d.setDate(d.getDate() - ((this.getDay() + 6) % 7) + 3); return d.getFullYear(); }, //Fixed now
    Y: function () { return this.getFullYear(); },
    y: function () { return ('' + this.getFullYear()).substr(2); },
    //Time
    a: function () { return this.getHours() < 12 ? 'am' : 'pm'; },
    A: function () { return this.getHours() < 12 ? 'AM' : 'PM'; },
    B: function () { return Math.floor((((this.getUTCHours() + 1) % 24) + this.getUTCMinutes() / 60 + this.getUTCSeconds() / 3600) * 1000 / 24); }, //Fixed now
    g: function () { return this.getHours() % 12 || 12; },
    G: function () { return this.getHours(); },
    h: function () { return ((this.getHours() % 12 || 12) < 10 ? '0' : '') + (this.getHours() % 12 || 12); },
    H: function () { return (this.getHours() < 10 ? '0' : '') + this.getHours(); },
    i: function () { return (this.getMinutes() < 10 ? '0' : '') + this.getMinutes(); },
    s: function () { return (this.getSeconds() < 10 ? '0' : '') + this.getSeconds(); },
    u: function () { return "Not Yet Supported"; },
    //Timezone
    e: function () { return "Not Yet Supported"; },
    I: function () { return "Not Yet Supported"; },
    O: function () { return (-this.getTimezoneOffset() < 0 ? '-' : '+') + (Math.abs(this.getTimezoneOffset() / 60) < 10 ? '0' : '') + (Math.abs(this.getTimezoneOffset() / 60)) + '00'; },
    P: function () { return (-this.getTimezoneOffset() < 0 ? '-' : '+') + (Math.abs(this.getTimezoneOffset() / 60) < 10 ? '0' : '') + (Math.abs(this.getTimezoneOffset() / 60)) + ':00'; }, //Fixed now
    T: function () { var m = this.getMonth(); this.setMonth(0); var result = this.toTimeString().replace(/^.+ \(?([^\)]+)\)?$/, '$1'); this.setMonth(m); return result; },
    Z: function () { return -this.getTimezoneOffset() * 60; },
    //Full Date/Time
    c: function () { return this.format("Y-m-d\\TH:i:sP"); }, //Fixed now
    r: function () { return this.toString(); },
    U: function () { return this.getTime() / 1000; }
};
function GetDD(fec) {
    return parseInt(fec.substr(0, 2), 10);
}
function GetMM(fec) {
    return (parseInt(fec.substr(3, 2), 10) - 1);
}
function GetYY(fec) {
    return parseInt(fec.substr(6));
}
function comprobarSiBisisesto(anio) {
    if ((anio % 100 != 0) && ((anio % 4 == 0) || (anio % 400 == 0))) {
        return true;
    }
    else {
        return false;
    }
}






function finMes(oTxt) {
    var nMes = parseInt(oTxt.value.substr(3, 2), 10);
    var nRes = 0;
    switch (nMes) {
        case 1: nRes = 31; break;
        case 2: nRes = 29; break;
        case 3: nRes = 31; break;
        case 4: nRes = 30; break;
        case 5: nRes = 31; break;
        case 6: nRes = 30; break;
        case 7: nRes = 31; break;
        case 8: nRes = 31; break;
        case 9: nRes = 30; break;
        case 10: nRes = 31; break;
        case 11: nRes = 30; break;
        case 12: nRes = 31; break;
    }
    return nRes;
}



function valSep(oTxt) {
    var sep;
    //Busca algún caracter separador y lo guarda (acá podrían agregarse nuevos formatos)
    if (oTxt.value.indexOf('/') > -1) {
        sep = '/';
    } else if (oTxt.value.indexOf('-') > -1) {
        sep = '-';
    }
    //Verifica que haya encontrado algo
    if (sep.length > 0) {
        //Verifica que haya más de un separador
        if (oTxt.value.indexOf(sep) != oTxt.value.lastIndexOf(sep)) {
            //Verifica que existan dos digitos antes del primer separador
            if (oTxt.value.indexOf(sep) != 2) {
                //En caso de no existir agrega un cero delante
                oTxt.value = "0" + oTxt.value;
                //Verifica nuevamente los dos dígitos
                if (oTxt.value.indexOf(sep) != 2) {
                    //Si pasa por acá es porque tenía más de 2 dígitos de entrada
                    return false;
                }
            }
            //Verifica lo mismo pero con el siguiente separador
            if (oTxt.value.lastIndexOf(sep) != 5) {
                var i = oTxt.value.substring(0, 3);
                var f = oTxt.value.substring(3);
                oTxt.value = i + "0" + f;
                if (oTxt.value.lastIndexOf(sep) != 5) {
                    return false;
                }
            }
            return true;
        }
    } else {
        swal("Los días, meses y años deben estar separados por el caracter / o el -");
        return false;
    }
}

function valDia(oTxt) {
    var bOk = false;
    var nDia = parseInt(oTxt.value.substr(0, 2), 10);
    bOk = bOk || ((nDia >= 1) && (nDia <= finMes(oTxt)));
    return bOk;
}
function valMes(oTxt) {
    var bOk = false;
    var nMes = parseInt(oTxt.value.substr(3, 2), 10);
    bOk = bOk || ((nMes >= 1) && (nMes <= 12));
    return bOk;
}
function valAno(oTxt) {
    var bOk = true;
    var nAno = oTxt.value.substr(6);
    bOk = bOk && ((nAno.length == 2) || (nAno.length == 4));
    if (bOk) {
        for (var i = 0; i < nAno.length; i++) {
            bOk = bOk && esDigito(nAno.charAt(i));
        }
    }
    return bOk;
}


/*
 *	Funciones genéricas para los formularios de Boston.
 */
function ShowErrorMessage(evt, msg) {
    swal(msg);
    $("#hdnRuleId").name = "zamba_cancel";
    setTimeout(function () { parent.hideLoading(); }, 250);
    evt.preventDefault();
}
function SaveDocument(doc) {
    doc.getElementById("hdnRuleId").name = "zamba_save";
}
function CheckMaxLength(Object) {
    var len = parseInt(Object.getAttribute("maxlength"), 10);
    return (Object.value.length < len);
}
function CheckMaxLengthAfterLosingFocus(Object) {
    var len = parseInt(Object.getAttribute("maxlength"), 10);
    if (Object.value.length > len) {
        swal("Se ha superado el límite máximo de " + len + " caracteres.\Se truncará dicho texto hasta el límite.");
        Object.value = Object.value.substring(0, len);
    }
}
function DecimalCheck(obj) {
    //Verifica que un numero sea decimal de 2 posiciones
    var RE = /^\d*\.?\d{0,2}$/;
    if (RE.test(obj.val())) {
        obj.removeClass("error");
        return true;
    } else {
        obj.addClass("error");
        return false;
    }
}
function IntegerCheck(e) {
    //Verifica que el número sea un entero
    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57))
        return false;
}
function isNumeric(value) {
    return typeof value === 'number' || /^\+|-?[0-9]+([,\.][0-9]*)?$/.test(value);
}
function PorcentualCheck(obj) {
    //Verifica un numero porcentual de 2 decimales
    var RE = /^\d*\.?\d{0,2}$/;
    var value = obj.val();
    if (value >= 0 && value <= 100 && RE.test(value)) {
        obj.removeClass("error");
        return true;
    } else {
        obj.addClass("error");
        return false;
    }
}
//Redondea a 2 dígitos después de la coma
function Redondear(num) {
    return Math.round(num * 100) / 100;
}
function CargarFechaIndice(indice) {
    $('#' + indice).datepicker({
        changeMonth: true,
        changeYear: true,
        showOn: 'button',
        buttonText: 'Abrir calendario',
        buttonImage: 'calendar.png',
        buttonImageOnly: true,
        duration: ""
    });
}
function esDigito(sChr) {
    var sCod = sChr.charCodeAt(0);
    return ((sCod > 47) && (sCod < 58));
}
function valSep(oTxt) {
    var sep;
    //Busca algún caracter separador y lo guarda (acá podrían agregarse nuevos formatos)
    if (oTxt.value.indexOf('/') > -1) {
        sep = '/';
    } else if (oTxt.value.indexOf('-') > -1) {
        sep = '-';
    }
    //Verifica que haya encontrado algo
    if (sep.length > 0) {
        //Verifica que haya más de un separador
        if (oTxt.value.indexOf(sep) != oTxt.value.lastIndexOf(sep)) {
            //Verifica que existan dos digitos antes del primer separador
            if (oTxt.value.indexOf(sep) != 2) {
                //En caso de no existir agrega un cero delante
                oTxt.value = "0" + oTxt.value;
                //Verifica nuevamente los dos dígitos
                if (oTxt.value.indexOf(sep) != 2) {
                    //Si pasa por acá es porque tenía más de 2 dígitos de entrada
                    return false;
                }
            }
            //Verifica lo mismo pero con el siguiente separador
            if (oTxt.value.lastIndexOf(sep) != 5) {
                var i = oTxt.value.substring(0, 3);
                var f = oTxt.value.substring(3);
                oTxt.value = i + "0" + f;
                if (oTxt.value.lastIndexOf(sep) != 5) {
                    return false;
                }
            }
            return true;
        }
    } else {
        swal("Los días, meses y años deben estar separados por el caracter / o el -");
        return false;
    }
}
function finMes(oTxt) {
    var nMes = parseInt(oTxt.value.substr(3, 2), 10);
    var nRes = 0;
    switch (nMes) {
        case 1: nRes = 31; break;
        case 2: nRes = 29; break;
        case 3: nRes = 31; break;
        case 4: nRes = 30; break;
        case 5: nRes = 31; break;
        case 6: nRes = 30; break;
        case 7: nRes = 31; break;
        case 8: nRes = 31; break;
        case 9: nRes = 30; break;
        case 10: nRes = 31; break;
        case 11: nRes = 30; break;
        case 12: nRes = 31; break;
    }
    return nRes;
}
function valDia(oTxt) {
    var bOk = false;
    var nDia = parseInt(oTxt.value.substr(0, 2), 10);
    bOk = bOk || ((nDia >= 1) && (nDia <= finMes(oTxt)));
    return bOk;
}
function valMes(oTxt) {
    var bOk = false;
    var nMes = parseInt(oTxt.value.substr(3, 2), 10);
    bOk = bOk || ((nMes >= 1) && (nMes <= 12));
    return bOk;
}
function valAno(oTxt) {
    var bOk = true;
    var nAno = oTxt.value.substr(6);
    bOk = bOk && ((nAno.length == 2) || (nAno.length == 4));
    if (bOk) {
        for (var i = 0; i < nAno.length; i++) {
            bOk = bOk && esDigito(nAno.charAt(i));
        }
    }
    return bOk;
}
function valFecha(oTxt) {
    var bOk = true;
    if (oTxt.value != "") {
        bOk = bOk && (valSep(oTxt));
        bOk = bOk && (valAno(oTxt));
        bOk = bOk && (valMes(oTxt));
        bOk = bOk && (valDia(oTxt));

        if (!bOk) {
            swal("La fecha ingresada es invalida");
            //oTxt.focus();
        }
    }
}

function CalcularLaborales(n, fecIni) {
    //Suma a una fecha determinada X cantidad de días hábiles y devuelve la fecha calculada.
    var dd = GetDD(fecIni);
    var mm = GetMM(fecIni);
    var yy = GetYY(fecIni);
    var fec = new Date(yy, mm, dd);
    return fec.SumarLaborables(n).format("d/m/Y");
}

String.prototype.trim = function () {
    return this.replace(/^\s*/, "").replace(/\s*$/, "");
}

Date.prototype.SumarLaborables = function (n) {
    //Método para sumar n dias hábiles.
    for (var i = 0; i < n; i++) {
        this.setTime(this.getTime() + 24 * 60 * 60 * 1000);
        //Si es sábado o domingo se cuenta otro día.
        if ((this.getDay() == 6) || (this.getDay() == 0))
            i--;
    }
    return this;
}

Date.prototype.format = function (format) {
    var returnStr = '';
    var replace = Date.replaceChars;
    for (var i = 0; i < format.length; i++) {
        var curChar = format.charAt(i);
        if (i - 1 >= 0 && format.charAt(i - 1) == "\\") {
            returnStr += curChar;
        }
        else if (replace[curChar]) {
            returnStr += replace[curChar].call(this);
        } else if (curChar != "\\") {
            returnStr += curChar;
        }
    }
    return returnStr;
};

Date.replaceChars = {
    shortMonths: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
    longMonths: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
    shortDays: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
    longDays: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],

    //Day
    d: function () { return (this.getDate() < 10 ? '0' : '') + this.getDate(); },
    D: function () { return Date.replaceChars.shortDays[this.getDay()]; },
    j: function () { return this.getDate(); },
    l: function () { return Date.replaceChars.longDays[this.getDay()]; },
    N: function () { return this.getDay() + 1; },
    S: function () { return (this.getDate() % 10 == 1 && this.getDate() != 11 ? 'st' : (this.getDate() % 10 == 2 && this.getDate() != 12 ? 'nd' : (this.getDate() % 10 == 3 && this.getDate() != 13 ? 'rd' : 'th'))); },
    w: function () { return this.getDay(); },
    z: function () { var d = new Date(this.getFullYear(), 0, 1); return Math.ceil((this - d) / 86400000); }, //Fixed now
    //Week
    W: function () { var d = new Date(this.getFullYear(), 0, 1); return Math.ceil((((this - d) / 86400000) + d.getDay() + 1) / 7); }, //Fixed now
    //Month
    F: function () { return Date.replaceChars.longMonths[this.getMonth()]; },
    m: function () { return (this.getMonth() < 9 ? '0' : '') + (this.getMonth() + 1); },
    M: function () { return Date.replaceChars.shortMonths[this.getMonth()]; },
    n: function () { return this.getMonth() + 1; },
    t: function () { var d = new Date(); return new Date(d.getFullYear(), d.getMonth(), 0).getDate() }, //Fixed now, gets #days of date
    //Year
    L: function () { var year = this.getFullYear(); return (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0)); },	//Fixed now
    o: function () { var d = new Date(this.valueOf()); d.setDate(d.getDate() - ((this.getDay() + 6) % 7) + 3); return d.getFullYear(); }, //Fixed now
    Y: function () { return this.getFullYear(); },
    y: function () { return ('' + this.getFullYear()).substr(2); },
    //Time
    a: function () { return this.getHours() < 12 ? 'am' : 'pm'; },
    A: function () { return this.getHours() < 12 ? 'AM' : 'PM'; },
    B: function () { return Math.floor((((this.getUTCHours() + 1) % 24) + this.getUTCMinutes() / 60 + this.getUTCSeconds() / 3600) * 1000 / 24); }, //Fixed now
    g: function () { return this.getHours() % 12 || 12; },
    G: function () { return this.getHours(); },
    h: function () { return ((this.getHours() % 12 || 12) < 10 ? '0' : '') + (this.getHours() % 12 || 12); },
    H: function () { return (this.getHours() < 10 ? '0' : '') + this.getHours(); },
    i: function () { return (this.getMinutes() < 10 ? '0' : '') + this.getMinutes(); },
    s: function () { return (this.getSeconds() < 10 ? '0' : '') + this.getSeconds(); },
    u: function () { return "Not Yet Supported"; },
    //Timezone
    e: function () { return "Not Yet Supported"; },
    I: function () { return "Not Yet Supported"; },
    O: function () { return (-this.getTimezoneOffset() < 0 ? '-' : '+') + (Math.abs(this.getTimezoneOffset() / 60) < 10 ? '0' : '') + (Math.abs(this.getTimezoneOffset() / 60)) + '00'; },
    P: function () { return (-this.getTimezoneOffset() < 0 ? '-' : '+') + (Math.abs(this.getTimezoneOffset() / 60) < 10 ? '0' : '') + (Math.abs(this.getTimezoneOffset() / 60)) + ':00'; }, //Fixed now
    T: function () { var m = this.getMonth(); this.setMonth(0); var result = this.toTimeString().replace(/^.+ \(?([^\)]+)\)?$/, '$1'); this.setMonth(m); return result; },
    Z: function () { return -this.getTimezoneOffset() * 60; },
    //Full Date/Time
    c: function () { return this.format("Y-m-d\\TH:i:sP"); }, //Fixed now
    r: function () { return this.toString(); },
    U: function () { return this.getTime() / 1000; }
};
function GetDD(fec) {
    return parseInt(fec.substr(0, 2), 10);
}
function GetMM(fec) {
    return (parseInt(fec.substr(3, 2), 10) - 1);
}
function GetYY(fec) {
    return parseInt(fec.substr(6));
}
function comprobarSiBisisesto(anio) {
    if ((anio % 100 != 0) && ((anio % 4 == 0) || (anio % 400 == 0))) {
        return true;
    }
    else {
        return false;
    }
}

function forceKeyPressUppercase(e) {
    var charInput = e.keyCode;
    if ((charInput >= 97) && (charInput <= 122)) { // lowercase
        if (!e.ctrlKey && !e.shiftKey && !e.metaKey && !e.altKEY) { // no modifier key
            var newChar = charInput - 32;
            var start = e.target.selectionStart;
            var end = e.target.selectionEnd;
            e.target.value = e.target.value.substring(0, start) + String.fromCharCode(newChar) + e.target.value.substring(end);
            e.target.setSelectionRange(start + 1, start + 1);
            e.preventDefault();
        }
    }
}
//toggle iframe start
function iframetog() {
    var x = document.getElementById("iframeStyle");
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }

}
//toggle iframe end


function requerido() {
    var requeridoInput = $("form").find("input.requerido").length;
    var namesArray = new Array();

    for (i = 0; i < requeridoInput; i++) {
        if ($("form").find("input.requerido")[i].value == "") {
            var sincomillas = $("form").find("input.requerido")[i].id;
            var indexname = $("#" + sincomillas).parent()[0].innerText;
            indexname = indexname.replace(":", "");
            namesArray.push(indexname);
        }
    }

    var requeridoSelect = $("form").find("select.requerido").length;
    var SelectArray = new Array();
    for (i = 0; i < requeridoSelect; i++) {
        if ($("form").find("select.requerido")[i].value == "") {
            var sincomillas = $("form").find("select.requerido")[i].id;
            var indexname = $("#" + sincomillas).parent()[0].innerText;
            indexname = indexname.replace(":", "");
            namesArray.push(indexname);
        }
    }
    return namesArray;
}

function isAnyRequeredFieldEmpty() {

    var namesArray = requerido();
    var campoRequeridoVacio = false;
    if (namesArray.length > 0) {
        swal("", "Por favor, ingrese " + namesArray.toString().replace(/,/g, " - "), "error");
        campoRequeridoVacio = true
    }
    return campoRequeridoVacio;

}


function ValidateNumberOfCharacters(numberOfCharactersRequered, valueToValidate, nameinput) {

    if (valueToValidate != null && (numberOfCharactersRequered != valueToValidate.length) && numberOfCharactersRequered != 0) {
        swal("", "El campo " + nameinput + " debe tener " + numberOfCharactersRequered + " digitos", "warning");
        return;
    }

}

function dateFormatMMDDYYYY(date) {
    var dateParts = date.split("/");
    return new Date(dateParts[2], dateParts[1] - 1, dateParts[0]);
}

$(document).ready(function () {
    $("form").keypress(function (e) {
        if (e.which == 13 && e.target.nodeName != "TEXTAREA") {
            return false;
        }
    });
});


/* Esta funcion se crea para inabilitar el click en la grilla expediente
   Se eliminara cuando se cree la grilla con angular
   la misma se coloca en el zamba.js ya que en los formularios rompe la grilla
*/
window.setTimeout("BtnGrillaClick()", 3000);
function BtnGrillaClick() {
    $(".rowTzamba_associated_documents_101 > td").off("click");
}


function OpenHelp(File, Title) {
    var options = '"directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=yes,top=100,left=100,width=800,height=480,title=' + Title + '"'
    window.open(File, "_blank", options);
}

function myfunction() {
    swal('The Internet?', 'That thing is still around?', 'question');
}


function check(e) {
    tecla = (document.all) ? e.keyCode : e.which;

    //Tecla de retroceso para borrar, siempre la permite
    if (tecla == 8) {
        return true;
    }

    // Patron de entrada, en este caso solo acepta numeros y letras
    patron = /^[A-Za-z0-9\s]+$/g;
    tecla_final = String.fromCharCode(tecla);
    return patron.test(tecla_final);
}
function GetGroupsIdsByUserId(id) {

    var groups = [];

    try {
        if (window.localStorage) {
            var localGroups = window.localStorage.getItem("localGroups" + GetUID());
            if (localGroups != undefined && localGroups != null && localGroups.length > 0) {
                groups = localGroups;
                return groups;

            }
        }
    } catch (e) {
        console.error(e);
    }

    if (groups.length == 0) {


        $.ajax({
            type: 'GET',
            async: false,
            url: ZambaWebRestApiURL + '/search/GetGroupsByUserIds',
            data: { usrID: id },
            success: function (data) {
                if (data != undefined && data != null) {
                    groups = data;
                    try {
                        if (window.localStorage) {
                            window.localStorage.setItem("localGroups" + GetUID(), data);
                        }
                    } catch (e) {
                        console.error(e);
                    }
                }
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });
    }

    return groups;

}

function ValidarEstudioConsulta() {
    var EstudioList = [];
    $.ajax({
        type: 'GET',
        async: false,
        url: ZambaWebRestApiURL + '/search/GetGroupsByUserIds',
        data: { usrID: id },
        success: function (data) {
            groups = data;
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}





function validateAttribute(tagName) {
    var tagValue = $("#" + tagName).val();
    var isNotNullOrEmpty = (tagValue != null && tagValue != "");
    return isNotNullOrEmpty;
}

function saveIndexValidated(indexId, entityId, parentResultId, taskId, indexValue) {
    var isIndexUpdated = false;
    var genericRequest = {
        UserId: parseInt(GetUID()),
        Params:
        {
            "indexId": indexId,
            "entityId": entityId,
            "parentResultId": parentResultId,
            "taskId": taskId,
            "indexValue": indexValue
        }
    };

    $.ajax({
        type: "POST",
        url: serviceBase + '/search/setTaskIndex',
        data: JSON.stringify(genericRequest),

        contentType: "application/json; charset=utf-8",
        async: false,
        success:
            function (data, status, headers, config) {
                isIndexUpdated = data;
            }
    });
    return isIndexUpdated;
}

function saveIndexsValidated(indexs, entityId, parentResultId, taskId) {
    var isIndexUpdated = false;
    var genericRequest = {
        UserId: parseInt(GetUID()),
        Params:
        {
            "indexs": indexs,
            "entityId": entityId,
            "resultId": parentResultId,
            "taskId": taskId,
        }

    };

    $.ajax({
        type: "POST",
        url: serviceBase + '/search/setTaskIndexs',
        data: JSON.stringify(genericRequest),

        contentType: "application/json; charset=utf-8",
        async: false,
        success:
            function (data, status, headers, config) {
                isIndexUpdated = data;
            }
    });
    return isIndexUpdated;
}

function executeTaskRule(ruleId, resultIds, callback, errorCallBack) {
    var genericRequest = {
        UserId: parseInt(GetUID()),
        Params: {
            "ruleId": ruleId,
            "resultIds": resultIds
        }
    };

    try {
        var scope = angular.element($("#taskController")).scope();
        scope.ExecuteRule(ruleId, resultIds)
            .then(function (data) {
                console.log("Se ejecutó correctamente.");
                if (data.data != undefined && data.data != '') {
                    var GenericExecutionresponse = JSON.parse(data.data);
                    if (GenericExecutionresponse.results !== null && GenericExecutionresponse.results !== undefined)
                        scope.Task = GenericExecutionresponse.results[0];
                    if (callback)
                        callback(GenericExecutionresponse);
                }
                //location.reload();
            })
            .catch(function (fallback) {
                console.log(fallback);
                if (errorCallBack)
                    errorCallBack(fallback);
            });
    } catch (e) {
        console.error(e);
        if (errorCallBack)
            errorCallBack(fallback);
    }
}

//var GenericExecutionresponse {
//     long ruleId1;
//     RulePendingEvents pendigEvent;
//     RuleExecutionResult executionResult;
//     Hashtable @params;
//     bool refreshRule;
//     long ruleId2;
//        public List<ITaskResult> results { get; private set; }
//}

function processResultUpdates(GenericExecutionresponse, CustomIndexsIds) {
    try {

        GenericExecutionresponse.results.forEach(function (result) {
            result.Indexs.forEach(function (index) {
                if (CustomIndexsIds == undefined || CustomIndexsIds == null || CustomIndexsIds.indexOf(index.ID) != -1) {

                    if (index.Data != '') {
                        var e = $("#zamba_index_" + index.ID);
                        if (e) {
                            if (index.dataDescription != '') {
                                e.val(index.dataDescription);
                            }
                            else {
                                e.val(index.Data);
                            }
                        }
                    }
                }
            });

        });

        toastr.success("Datos actualizados");

    } catch (e) {
        toastr.success("Error al obtener datos");
    }

}

function cleanIndexs(CustomIndexsIds) {
    try {
        CustomIndexsIds.forEach(function (index) {
            var e = $("#zamba_index_" + index);
            if (e) {
                e.val('');
            }
        });

    } catch (e) {
        toastr.success("Error al obtener datos");
    }
}



function getElementFromQueryString(element) {
    var url = window.location.href;

    var segments = url.split("&");
    var value = null;
    segments.forEach(function (valor) {
        if (valor.includes(element)) { value = valor.split("=")[1]; }
    });
    return value;
}

function NotRecharged() {
    return "Esta seguro que desea salir";
}


function EnableAttributeBIfAttributeAIsEmpty(A, B) {
    if ($(A) != undefined && $(A).val() != '') {
        $(B).hide();
    }
    else {
        $(B).show();
    }
}

function EnableAttributeBIfAttributeAIsNotEmpty(A, B) {
    if ($(A) == undefined || $(A).val() == '') {
        $(B).hide();
    }
    else {
        $(B).show();
    }
}

function EnableIfIsEmptyA(A, sender) {
    if ($(A) != undefined && $(A).val() != '') {
        $(sender).show();
    }
    else {
        $(sender).hide();
    }
}

function parametroURL(_par) {
    var _p = null;
    if (location.search) location.search.substr(1).split("&").forEach(function (pllv) {
        var s = pllv.split("="), //separamos llave/valor
            ll = s[0],
            v = s[1] && decodeURIComponent(s[1]); //valor hacemos encode para prevenir url encode
        if (ll == _par) { //solo nos interesa si es el nombre del parametro a buscar
            if (_p == null) {
                _p = v; //si es nula, quiere decir que no tiene valor, solo textual
            } else if (Array.isArray(_p)) {
                _p.push(v); //si ya es arreglo, agregamos este valor
            } else {
                _p = [_p, v]; //si no es arreglo, lo convertimos y agregamos este valor
            }
        }
    });
    return _p;
}
$(document).ready(function () {
    $('.confirm').bind("click", function () {
        var message = 'Esta seguro que desea continuar?';
        var sender = this;
        if ($(this).attr("confirmMessage") != undefined) {
            message = $(this).attr("confirmMessage")
        }
        ConfirmMessage(message, sender);

        //addEventListener(click, message, callback, ConfirmMessage);

    });

    $('.TextIndexer').bind("click", function () {
        $(".TextIndexer").each(function () {
            var message = 'Esta seguro que desea continuar?';
            var sender = this;
            if (message = $(this).attr("confirmMessage")) {
                message = $(this).attr("confirmMessage")
                MessageForTextArea(message, sender);
            }


            //addEventListener(click, message, callback, ConfirmMessage);
        });
    });





});

//mensaje de aviso para confirmacion
function ExecutetaskRuleWithConfirmMessage(message, ruleid, doneCallBack, errorCallBack) {
    swal(message, "", "warning", {
        buttons: {
            cancel: "No",
            catch: {
                text: "Si",
                value: "catch"
            }
        }
    }).then(function (value) {
        switch (value) {
            case "catch":
                executeTaskRule(ruleid, GetDOCID(), doneCallBack, errorCallBack);
                return true;
                break;

            default:
                return false;
                break;
        }
    });
    return false;
}

//Mensaje de aviso para rechazos
function MessageForTextArea(message, sender) {
    swal({
        title: "Motivo de Rechazo",
        text: "Observaciones",
        icon: "info",
        content: {
            element: "input",
            attributes: {
                type: "text",
                id: "InputObservacion",
                placeholder: "Agregar Observacion"
            }
        },
        buttons: {
            cancel: "cancelar",
            catch: {
                text: "Guardar",
                value: "catch"
            }
        }
    }).then(function (value) {
        switch (value) {
            case "catch":
                var indexValue = $("#InputObservacion").val();
                swal("", message, "warning", {
                    buttons: {
                        cancel: "Cancelar",
                        catch: {
                            text: "Confirmar",
                            value: "catch"
                        }
                    }
                }).then(function (value) {
                    switch (value) {
                        case "catch":
                            var id = $(".TextIndexer").attr("id");
                            var indexId = id.split("_")[2];
                            var entityId = getElementFromQueryString("DocType");
                            var parentResultId = getElementFromQueryString("docid");
                            var taskId = 0;

                            if (entityId == null) {
                                taskId = getElementFromQueryString("taskid");
                                entityId = 0;
                            }

                            var saveIndex = saveIndexValidated(
                                indexId,
                                entityId,
                                parentResultId,
                                taskId,
                                indexValue
                            );

                            if (saveIndex == false) {
                                var scope = angular.element($("#appobs")).scope();
                                scope.InputObservacion = indexValue;
                                scope.AgregarObservacion();
                                SetRuleId(sender);
                                swal("", "Observacion agregada exitosamente", "success");
                            } else {
                                swal("", "Error al agregar la observacion", "error");
                            }

                            break;
                    }
                });
        }
    });
}

//Metodo mas generico de validacion de campos en formularios.
function zamba_save_onclick(sender, id) {
    var valido = false;
    var currentDate = new Date();
    var message = "Esta seguro que desea avanzar?";
    try {

        if (id != undefined && id != null) {
            var ids = id.split(",");

            for (var id in ids) {
                var data = $("#zamba_index_" + ids[id]);
                var datam = $("#zamba_index_" + ids[id - 1]);
                if (data.val() == "" || data.val() == null) {
                    swal("", "Por favor verifique el campo " + data.attr("indexname"), "");
                    valido = false;

                }
                else if (data.val() != "" && datam.val() == "") {
                    swal("", "Por favor verifique el campo " + datam.attr("indexname"), "");
                    valido = false;
                }
                else {

                    if (ConfirmMessage(message, sender)) {
                        valido = true;
                    }
                    else { valido = false }
                }
            }
        } else {
            valido = true;
        }
    }
    catch (ex) {
        swal("", "Por favor, verificar los datos ingresados");
        console.log(ex);
    }

    localStorage.setItem("ZambaSaveResult", true);
    return valido;
}


function RefreshCurrentPage() {
    window.location.reload();
}

function GenericFunctionNumericStye() {

    for (var i = 0; i < $("input").length; i++) {

        if ($("input")[i].getAttribute("class") != null && $("input")[i].getAttribute("class") != "null") {

            if ($("input")[i].getAttribute("class").contains("form-control")) {

                if ($("input")[i].getAttribute("datatype") != null && $("input")[i].getAttribute("datatype") != "null") {

                    if ($("input")[i].getAttribute("datatype").contains("numeric")) {

                        $("input")[i].className += " inpusize";

                    }

                }

            }

        }

    }

}

function HelperCommas(value) {
    try {
        value = value.Replace(",", "");
    } catch (ex) {
        console.log("Error a quitar las comas");
        console.log(ex);
    }
    return value;
}

function showMailSwal(ID) {
    var dominio = ZambaWebRestApiURL;
    var msgMail = "";
    var genericRequest = {
        UserId: parseInt(GetUID()),
        Params: {
            "ID": ID
        }
    };
    $.ajax({
        type: "POST",
        async: false,
        url: dominio + "/eInbox/GetMailBody",
        data: JSON.stringify(genericRequest),
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            msgMail = msg;
        },
        error: function (request, status, err) {
            var a = request;
        }
    });
    if (msgMail.OriginalDocumentFileName.toLocaleLowerCase().endsWith(".html")) {
        var ResponseHTML = "";
        ResponseHTML = "<div align='left' style='font-size:15px'>";
        ResponseHTML += "<h4>" + msgMail.Subject + "</h4><hr/>";
        ResponseHTML += "<table >"
        ResponseHTML += "<tr><td style='padding-right: 29px' ><b> Fecha: </b>" + moment(msgMail.MailDateTime).format("DD/MM/YYYY hh:mm:ss") + "</td>";
        ResponseHTML += "<td><b> Para: </b>" + msgMail.MailTo + "</td></tr>";
        if (msgMail.Cc != "")
            ResponseHTML += "<tr><td colspan='2'><b> CC: </b>" + msgMail.Cc + "</td></tr>";
        if (msgMail.Cco != "")
            ResponseHTML += "<tr><td colspan='2'><b> CCo: </b>" + msgMail.Cco + "</td></tr>";
        ResponseHTML += "<td><b> Adjuntos: </b>" + msgMail.Attachs + "</td></tr>";
        ResponseHTML += "</table><hr/>";
        ResponseHTML += msgMail.Body;
        ResponseHTML += "</div>";

        Swal.fire({
            title: '',
            html: ResponseHTML,
            confirmButtonText: 'Aceptar',
            confirmButtonColor: '#337ab7',
            focusConfirm: true,
            width: '800px'
        }).then((result) => {
            $(".swal2-container.swal2-center.swal2-backdrop-show").height(720);

        });
    }
    else if (msgMail.OriginalDocumentFileName.toLocaleLowerCase().endsWith(".msg")) {
        //modalViewerMail
        var scope_documentViewer = angular.element($("#modalViewerMail")).scope();
        var result = scope_documentViewer.ShowDocument_FromItem(msgMail.UserId, msgMail.SourceDocTypeId, msgMail.SourceDocId, msgMail.OriginalDocumentFileName);
        if (result) {
            document.getElementById('id01').style.display = 'block';
            $(".swal2-container.swal2-center.swal2-backdrop-show").height(720);
        }
        else {
            Swal.fire({
                icon: 'error',
                title: 'Mostrar correo',
                text: 'El documento solicitado no pudo ser cargado. Esto puede deberse a que el mismo no exista o que se haya producido un error al obtenerlo. Por favor, comuníquese con el administrador del sistema',
                confirmButtonText: 'Aceptar',
                confirmButtonColor: '#337ab7'
            }).then((result) => { });
        }
    }

}

function PermisosInsert() {

    var userid = GetUID();
    if (userid != undefined && userid > 0) {

        if (window.localStorage.getItem("InsertBtn-" + GetUID()) === null || window.localStorage.getItem("InsertBtn-" + GetUID()) == "false") {


            var response = null;
            var genericRequest = {
                UserId: parseInt(GetUID()),
                Params:
                    {}
            };
            $.ajax({
                type: "POST",
                url: serviceBase + '/search/getPermisosInsert',
                data: JSON.stringify(genericRequest),

                contentType: "application/json; charset=utf-8",
                async: false,
                success:
                    function (data, status, headers, config) {
                        response = data;
                        if (true) {
                            localStorage.setItem("InsertBtn-" + GetUID(), response);
                            $("#BtnHomeInsert").removeClass("disableBtnInsert");
                        } else {
                            window.localStorage.setItem("InsertBtn-" + GetUID(), false)
                        }
                    }
            });

        } else {
            if (window.localStorage.getItem("InsertBtn-" + GetUID()) == "true") {
                $("#BtnHomeInsert").removeClass("disableBtnInsert");
            }
        }

    }
}


function isFireFox() {
    return (
        navigator.appVersion.indexOf('Mozilla/') > 0 ||
        navigator.userAgent.toString().indexOf('Firefox/') > 0)
}

function isInternetExplorer() {
    return (navigator.userAgent.indexOf('MSIE') !== -1 ||
        navigator.appVersion.indexOf('Trident/') > 0 ||
        navigator.userAgent.toString().indexOf('Edge/') > 0)
}

function popupsIsLocked() {
    if (localStorage.getItem("LockedPopup") == 'true')
        return true;
    if (localStorage.getItem("LockedPopup") == 'false')
        return false;
    if (isInternetExplorer())
        return false;
    var ret = false;
    var testNavigator = window.open('https://www.google.com', '_blank');
    if (testNavigator == undefined || testNavigator == null)
        ret = true;
    else {
        testNavigator.close();
        ret = false;
    }
    localStorage.setItem("LockedPopup", ret)
    return ret;
}
function popupsUnlocked() {
    var ret = false;
    if (localStorage.getItem("LockedPopup") == 'false')
        return true;
    var testNavigator = window.open('https://www.google.com', '_blank');
    if (testNavigator == undefined || testNavigator == null)
        ret = false;
    else {
        testNavigator.close();
        ret = true;
    }
    localStorage.setItem("LockedPopup", !ret)
    return ret;
}

function ReviewPopupsLocked() {
    var pathManual = thisDomain + "/Views/Security/Help/";
    if (isInternetExplorer()) {
        return;
    }
    if (popupsIsLocked()) {
        if (isFireFox()) {
            pathManual += "PopupsUnlockedFireFox.pdf";

        }
        else {
            pathManual += "PopupsUnlockedChromeEdge.pdf";
        }
        $("#popupslocked").show();
        tmrPopupsUnlocked = setInterval(function () {
            if (popupsUnlocked()) {
                clearInterval(tmrPopupsUnlocked);
                $("#popupslocked").hide();
            }
        }, 2500);

    }
    $("#linkDownloadManual")[0].download = pathManual;
    $("#linkDownloadManual")[0].href = pathManual;

}
function openZambaEditor() {
    let paramRequest = window.location.search;
    var urlZEditor = getValueFromWebConfig("ZEditorUrl");
    let url = window.location.protocol + "//" + window.location.host + urlZEditor + "/ZEditor.aspx" + paramRequest;
    window.open(url, '_blank');
}

// FUNCION ANTERIOR
//function closeModalIFs() {

//    let IframeUrl = $("#modalFormHome")[0].children[1].src;
//    IframeUrl.contains("Insert") ? location.reload() : $("#openModalIF").modal("hide")

//}

function closeModalIFs() {

    let IframeUrl = $("#modalFormHome")[0].children[1].src;
    if (IframeUrl.contains("Insert")) {
        let iframe = document.getElementById("ctl00_ContentPlaceHolder_modalIframe");
        let doc = iframe.contentWindow.document;
        let btnEliminarDropzone = doc.querySelectorAll(".dz-remove")
        if (btnEliminarDropzone.length > 0) {
            swal("Posee documentos agregados, eliminelos antes de cerrar el modal")
        } else {
            $("#openModalIF").modal("hide")
        }
    } else {
        $("#openModalIF").modal("hide")
    }

}

function TriggerOnChange(value) {
    $("#" + value + "").trigger("change");
}



