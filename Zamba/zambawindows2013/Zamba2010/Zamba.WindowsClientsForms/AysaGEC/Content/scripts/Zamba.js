$(function() 
{
    $(".fg-button:not(.ui-state-disabled)").hover
    (
        function() {
            $(this).addClass("ui-state-hover");
        },
        function() {
            $(this).removeClass("ui-state-hover");
        }
    )
    .mousedown
    (
        function() {
            $(this).parents('.fg-buttonset-single:first').find(".fg-button.ui-state-active").removeClass("ui-state-active");
            if ($(this).is('.ui-state-active.fg-button-toggleable, .fg-buttonset-multi .ui-state-active')) {
                $(this).removeClass("ui-state-active");
            }
            else {
                $(this).addClass("ui-state-active");
            }
        }
    )
    .mouseup
    (
        function() {
            if (!$(this).is('.fg-button-toggleable, .fg-buttonset-single .fg-button, .fg-buttonset-multi .fg-button')) {
                $(this).removeClass("ui-state-active");
            }
        }
     );
});

function makeCalendar(id) 
{
    $(function() {
        $('#' + id).datepicker({
            changeMonth: true,
            changeYear: true,
            showOn: 'button',
            buttonText: 'Abrir calendario',
            buttonImage: '/ZambaWeb/Content/Images/calendar.png',
            buttonImageOnly: true,
            duration: ""
        });
    });
}

function windowHeight() 
{
    var myHeight = 0;
    if (typeof (window.innerWidth) == 'number') 
    {
        myHeight = window.innerHeight;
    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        myHeight = document.documentElement.clientHeight;
    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        myHeight = document.body.clientHeight;
    }
    return myHeight;
}

function windowWidth() 
{
    var myWidth = 0;
    if (typeof (window.innerWidth) == 'number') 
    {
        myWidth = window.innerWidth;
    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        myWidth = document.documentElement.clientWidth;
    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        myWidth = document.body.clientWidth;
    }
    return myWidth;
}

function expandDiv(div, difwidth, difheight) 
{
    var myWidth = 0, myHeight = 0;
    if (typeof (window.innerWidth) == 'number') 
    {
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

function doExpand(div, w, h) 
{
    $("#" + div).width(w);
    $("#" + div).height(h);
}


function modifyTextBoxSize(tb)
{
    if(tb.rows == 5)
    {
        tb.rows = 1;
    }
    else
    {
        tb.rows = 5;
    }
}
function maxLenght(tb, max)
{
    return (tb.value.length < max);
}

//Obtiene la height que deben tener los contenedores principales
function getHeightScreen() {
    var height = screen.height;
    switch (height) {
        case 1024:
        return document.body.clientHeight - 175;
            return 650;
            break;
        case 960:
        return document.body.clientHeight - 175;
            return 600;
            break;
        case 768:
        return document.body.clientHeight - 175;
            return 437;
            break;
        default:
        return document.body.clientHeight - 175;
            return 500;
            break;
    }
}