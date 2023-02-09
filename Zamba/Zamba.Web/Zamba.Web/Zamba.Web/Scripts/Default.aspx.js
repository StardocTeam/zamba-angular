$(document).ready(function () {
    //sirve para el toogle de las acciones de los usuarios en el navbar
    $(document).click(function () {
        if ($('#dropdown-header').data('open')) {
            $('#dropdown-header').data('open', false);
            $("#ContentPlaceHolder_pnlHeaderButtons").hide();
        }

    });

    $("#dropCabezera").click(function () {
        if ($('#dropdown-header').data('open')) {
            $('#dropdown-header').data('open', false);
            $("#ContentPlaceHolder_pnlHeaderButtons").hide();
        }
        else {
            $('#dropdown-header').data('open', true);
            $("#ContentPlaceHolder_pnlHeaderButtons").show();
        }
    });

    //Doy el alto a todos los tabs cuando inicia la aplicacion.
    setTabsHeight(null);

    // VER ESTO, PARA CONTROL DEL SWIPE.
    //$("#spnToolbar").swipe({
    //    //Generic swipe handler for all directions
    //    swipeLeft: function (event, direction, distance, duration, fingerCount, fingerData) {
    //        if (direction == 'left') {
    //            toggleSidePanel();
    //        }
    //    },

    //    threshold: 30,
    //    fingers: 'all'
    //});

    //reloadBootstrap();
});

function getInitTab() {
    var params = getInitTabparams();
    var aux1 = new Array();
    var page;

    initTab = null;
    if (params.length > 0) {
        params = params.substr(1);
        aux1 = params.split("=");

        if (aux1[0] == "tabRedirect") {
            page = aux1[1];
            switch (page) {
                case "Novedades":
                    initTab = "tabnew";
                    break;
                case "Busqueda":
                    initTab = "tabsearch";
                    break;
                case "Tareas":
                    initTab = "tabtasklist";
                    break;
            }
        }
    }
    return initTab;
}

function GetHeight() {
    var masterHeader = $("#MasterHeader").css("display") == "none" ? 0 : $("#MasterHeader").height();
    var barraCabecera = $("#barra-Cabezera").css("display") == "none" ? 0 : $("#barra-Cabezera").innerHeight();
    var mainmenu = $("#mainMenu").css("display") == "none" ? 0 : $("#mainMenu").innerHeight();
    var headersHeight = masterHeader + barraCabecera + mainmenu;
    return screen.availHeight - headersHeight - 30
}

function RefreshGeneralGrid(tabName, url, oneLoad) {
    var WFIframe;

    switch (tabName) {
        case "Insertar":
            WFIframe = $("#ContentPlaceHolder_insertIframe");
            break;
    }
    //<%-- Primero se setea en en página en blanco para que tome el refresco.--%>
   if (WFIframe != null) {
       if (WFIframe[0] != null) {
           if (oneLoad && WFIframe[0].contentWindow.location != "about:blank")
               return;
           ShowLoadingAnimation();
           WFIframe[0].contentWindow.location = "about:blank";

           try {
               WFIframe[0].contentWindow.location = "about:blank";
               WFIframe[0].contentWindow.location = url;
               StartObjectLoadingObserver(WFIframe[0]);
           }
           catch (e) {
               if (WFIframe.context != null) {

                   WFIframe.context.location = "about:blank";
                   WFIframe.context.location = url;
               }
           }
       }
   }
}

var initaltab = "tabhome";
$(function () {
    ShowLoadingAnimation();
    $("#TasksDiv").zTabs({});
    var viewNews = $("#ContentPlaceHolder_viewsNews").val();
    var viewInsert =viewInsertAspx();

    if (viewNews != null) {
        if (viewNews.toLowerCase() == "true") {
            initaltab = "tabnew";
            $("#liNews").css("display", "block");
        }
        else {
           // <%-- todo: una vez terminado la busqueda, volver a descomentar esta linea de codigo
            //initaltab = "tabhome";--%>
            // initaltab = "tabtasklist";
            $("#liNews").css("display", "none");
        }
    }
    else {
        // initaltab = "tabtasklist";
        $("#liNews").css("display", "none");
    }

    if (viewInsert.toLowerCase() == "true") {
        $("#liInsert").css("display", "block");
    }
    else {
        $("#liInsert").css("display", "none");
    }

    var tab = getInitTab();
    if (tab) {
        initaltab = tab;
    }

    MainTabber = $("#MainTabber");

    $("#MainTabber").zTabs({
        //selected: initaltab,
        ajaxOptions: {
            error: function (xhr, status, index, anchor) {
            }
        }, //23/08/11: Cuando se selecciona la tab de WF se refresca la grilla.
        // loading spinner
        beforeSend: function () {
            ShowLoadingAnimation();
        },
        complete: function () {
            $('.MainTabberTabs').show();
            $('#mainMenu').tabs('option', 'visibility', 'visible');
        },
        activate: function (event, ui) {
            //Antes de seleccionar nada, se elimina el tab dummy anteriormente creado.
            if ($("#DummyTab")) {
                $('#MainTabber').zTabs("remove", "DummyTab");
            }

            var tabText = $(ui.newTab[0]).text().trim();

            switch (tabText) {
                case "Insertar":
                    RefreshGeneralGrid(tabText, "../Insert/Insert.aspx", false);
                    break;
            }
        }
    });
    //Hago que se muestre todo el contenido una vez que se haya cargado
    $("#mainMenu").css("display", "block");
    //$("#userActionHeader").css("display", "block");
    if ($('#ContentPlaceHolder_pnlHeaderButtons li').length) {
        $("#userActionHeader").show();
    }
    else {
        $("#userActionHeader").hide();
    }
    $("#MasterHeader").css("display", "block");

    if ($("#DummyTab")) {
        $('#MainTabber').zTabs("remove", "DummyTab");
    }
    if (viewInsert.toLowerCase() == "false") {
        $('#MainTabber').zTabs("remove", "tabInsert");
    }
    $('#MainTabber').zTabs("select", initaltab);
});

$(document).ready(function () {
   // <%-- Que se actualice la grilla cuando se presiona tab "Listados" --%>
    //$('#tasklist').click(function () {
    //    document.getElementById('ContentPlaceHolder_Arbol_RefreshClick').click();
    //    hideLoading();
    //});

  //  <%-- Se agrega el script en esta parte por una cuestion de resolucion de namespaces en AJAX --%>
    //<%--Obtiene los textbox de los formularios, y les agrega en el foco la seleccion del texto.
   // Con esto se deberia solucionar el problema del "bloqueo de los textbox".--%>
    $("input[type=text]").focus(function () {
        $(this).select();
    });
});

function beginRequestHandler() {
    ShowLoadingAnimation();
}

$(document).ready(function () {
    ExecuteFormReadyActions();
    $('.EntryIndex').keypress(function (event) {
        if (event.which == 13) {
            $("#ContentPlaceHolder_btnSearch").click();
            event.preventDefault();
        }
    });
    mostrarMensaje();

    //resizeHeaderMenu();
});

//

$('.navigation-item').on('click', function (e) {
    var current = $(this);
    if (current.attr('id') == "liTabSearch") {
        $("#btnGlobalSearch").show();
        $("#btnAdvanceSearch").show();
    } else {
        $("#Advfilter1").css("height", "auto");
        $("#btnGlobalSearch").hide();
        $("#btnAdvanceSearch").hide();
        $("#SearchControl").hide();
        $("#Advfilter-modal-content").hide();
    }
});

