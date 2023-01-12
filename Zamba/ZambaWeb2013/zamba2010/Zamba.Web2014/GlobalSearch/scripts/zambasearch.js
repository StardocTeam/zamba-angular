//Header fijo
$(".adv2").keypress(function (event) {
    event.stopPropagation();
    event.preventDefault();
    $('#modal-content').slideToggle();
    $('#modal-content').find(".remove-all-icon.glyphicon-trash").click()
    $('#modal-content').find("input").focus();
    $('#modal-content').find("input").val(String.fromCharCode(event.charCode));//keyCode
    $(this).fadeOut();
    $(".favAdvSearch").fadeOut();
});
//Home
$(".adv3").keypress(function (event) {
    event.stopPropagation();
    event.preventDefault();
    $('#modal-content').slideToggle();
    $('#modal-content').find(".remove-all-icon.glyphicon-trash").click();
    $('#modal-content').find("input").focus();
    $('#modal-content').find("input").val(String.fromCharCode(event.charCode)); //keyCode
    $(this).fadeOut();
    $(".favAdvSearch").fadeOut();
});
function winSize() {
    return (window.innerWidth > 0) ? window.innerWidth : screen.width;
}

//Para que en celular funcione al hacer click
$(".advancedSearchBox").click(function (event) { 
    openMobileSearch();
});
$(".advancedSearchBox").on('touchstart', function () {
    openMobileSearch();
});
$(document).on("vclick", ".advancedSearchBox", function () {
    openMobileSearch();
});
//$(".advancedSearchBox").vclick(function () {
//    openMobileSearch();
//});

function openMobileSearch() {
    if (winSize() < 767) {
        $('#modal-content').slideToggle();
        $(event.toElement).fadeOut();
        $('#modal-content').find("input").focus();
    }
}

$("#advSearchSave").click(function () {
    $('#modal-content').slideToggle();
    $("#Advfilter2").fadeIn();
    $(".favAdvSearch").fadeIn();
    $("#Advfilter2").children(".advancedSearchBox").css("display", "block");
});

$("#advSearchClose").click(function () {
    $('#modal-content').slideToggle();
    $("#Advfilter2").fadeIn();
    $("#Advfilter2").children(".advancedSearchBox").css("display", "block");
    $('#modal-content').find(".remove-all-icon.glyphicon-trash").click();
    $(".favAdvSearch").fadeOut();
    //$(".favAdvSearch").fadeIn();
});

$(".favAdvSearch").click(function () {
    $('#modal-content').slideToggle();
    $(this).slideToggle();
    $(".adv2").slideToggle();
});

$('#modal-content').on('hide.bs.modal', function (e) {
    $("#Advfilter2").fadeIn();
});

$("li").click(function () {
    if (enableGlobalSearch != undefined && enableGlobalSearch) {
        if ($(this).parent().attr("id") != "mainMenu")
            return;
        if ($(this).attr("id") == "liHome") {
            $("#Advfilter2").fadeOut();
            $(".favAdvSearch").fadeOut();
            $(".adv3").fadeIn();
            $(".adv3").find("input").focus();
        }
        else {
            $("#Advfilter2").fadeIn();
        }
    }
});

$(document).ready(function () {   //Para que no se vean dos buscadores al iniciar en home
    if (initaltab == 'tabhome')
        $('#Advfilter2').hide();

});