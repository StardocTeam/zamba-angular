$(document).ready(function () {   //Para que no se vean dos buscadores al iniciar en home
    //Header fijo
    $(".adv2").keypress(function (event) {
        openGlobalSearch(event);
        $(this).fadeOut();
    });

    $("#btnGlobalSearch").click(function (event) {
        var display = $("#Advfilter-modal-content").css("display");
        if (display != "none") {
            $("#Advfilter-modal-content").css("display", "none");
            $("#Advfilter1").css("height", "auto");
        } else {
            $("#Advfilter1").css("height", "100%");
            $("#Advfilter-modal-content").css("display", "block");
            $("#SearchControl").css("display", "none");
        }
    });

    $("#btnAdvanceSearch").click(function (event) {
        if ($("#SearchControl").css("display") != "none") {
            $("#SearchControl").css("display", "none");
        } else {
            $("#SearchControl").css('display', 'block');
            $("#Advfilter-modal-content").css('display', 'none');
            $("#Advfilter1").css("height", "auto");
        }
    });


    /*
    $("#LupaSearch").click(function (event) {
        event.stopPropagation();
        event.preventDefault();
        var $m = $('#Advfilter-modal-content');
        $m.slideToggle();
        $m.find(".remove-all-icon.glyphicon-trash").click()
        $m.find("input").focus();
        $m.find("input").val(String.fromCharCode(event.charCode));//keyCode
        $(this).fadeOut();
        $(".favAdvSearch").fadeOut();

    });*/

    //Home
    $(".adv3").keypress(function (event) {
        openGlobalSearch(event);
        $(this).fadeOut();
    });


    function winSize() {
        return (window.innerWidth > 0) ? window.innerWidth : screen.width;
    }

    //Para que en celular funcione al hacer click
    $(".advancedSearchBox").click(function (event) {
        openMobileSearch(event);
    });
    $(".advancedSearchBox").on('touchstart', function (event) {
        openMobileSearch(event);
    });
    $(document).on("vclick", ".advancedSearchBox", function (event) {
        openMobileSearch(event);
    });
    //$(".advancedSearchBox").vclick(function () {
    //    openMobileSearch();
    //});

    function openMobileSearch(event) {
        if (winSize() < 767) {

            var $m = $('#Advfilter-modal-content');
            $m.slideToggle();
            $(event.toElement).fadeOut();
            $m.find("input").focus();
        }
    }
    $('body').on('click', '#advSearchSave', function (event) {

        $('#Advfilter-modal-content').slideToggle();
        $("#Advfilter2").fadeIn().css('display', 'inline-flex');
        $(".favAdvSearch").fadeIn();
        $("#Advfilter2").children(".advancedSearchBox").css({ display: "block", width: "auto" });//, position:"fixed"
    });

    $("#advSearchClose").click(function () {
        var $m = $('#Advfilter-modal-content');
        $m.slideToggle();
        $("#Advfilter2").fadeIn().children(".advancedSearchBox")
            .css({ display: "block", width: (zambaApplication === "ZambaWeb") ? "auto" : "100%" });//, position: "fixed"

        $m.find(".remove-all-icon.glyphicon-trash").click();
        $(".favAdvSearch").fadeOut();
    });

    $(".favAdvSearch").click(function () {
        $('#Advfilter-modal-content').slideToggle();
        $(this).slideToggle();
        $(".adv2").slideToggle();
    });

    $('#Advfilter-modal-content').on('hide.bs.modal', function (e) {
        $("#Advfilter2").fadeIn().css('display', 'inline-flex');
    });

    $("li").click(function () {
        if (enableGlobalSearch !== undefined && enableGlobalSearch) {
            if ($(this).parent().attr("id") !== "mainMenu")
                return;
            if ($(this).attr("id") === "liHome") {
                $("#Advfilter2").fadeOut();
                $(".favAdvSearch").fadeOut();
                $(".adv3").fadeIn();
                $(".adv3").find("input").focus();
            }
            else {
                $("#Advfilter2").fadeIn().css('display', 'inline-flex');
            }
        }
    });

    //if (initaltab != null && initaltab == 'tabhome')
    //    $('#Advfilter2').hide();

    function setGSTooltips() {
        var $m = $('#Advfilter-modal-content');
        $m.find("#lblGSFilAll, #advSearchSave, #advSearchClose, #advSearchHelp,#selectDescGS, #showGSConfig, #advSearchConfig, #advSearchRemAll").tooltip();

        $(".favAdvSearch").tooltip();
    }
    setGSTooltips();
});





function openGlobalSearch(event) {

    event.stopPropagation();
    event.preventDefault();
    event.setGSTooltips;
    var $m = $('#Advfilter-modal-content');
    $m.find(".remove-all-icon.glyphicon-trash").click();
    $m.find("input").focus();
    $m.find("input").val(String.fromCharCode(event.charCode)); //keyCode   
    $("#searchWrapperh").fadeOut();
    $('#MainTabber').zTabs("select", 'tabsearch');
    $("#btnGlobalSearch").show();
    $("#btnAdvanceSearch").show();
    $("#Advfilter-modal-content").show();
}
