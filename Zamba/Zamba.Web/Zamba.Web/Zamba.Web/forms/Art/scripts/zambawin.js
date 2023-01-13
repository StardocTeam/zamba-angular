function zamba_save_onclick() {
    return true;
}

function SetRuleId(sender) {
    document.getElementById("hdnRuleId").name = sender.id;
    frmMain.submit();
}

function SetAsocId(sender) {
    document.getElementById("hdnAsocId").name = sender.id;
    frmMain.submit();
}

function obtiene_fecha() {
    var mydate = new Date();
    var year = mydate.getYear();
    var month = mydate.getMonth() + 1;
    var daym = mydate.getDate();

    if (year < 1000)
        year += 1900;

    if (month < 10)
        month = "0" + month;

    if (daym < 10)
        daym = "0" + daym;


    return daym + "/" + month + "/" + year;
}


function ZCOMMONFUNCTIONS() {
    ProcessColumnsOptions();
}

function ProcessColumnsOptions() {

    //                           <table id="zamba_associated_documents_11062"  class="tablesorter columnsoptions" columnsvisible="Estado:false">
    //                                         <tbody>
    //                                    </tbody>
    //                                </table>

    $(".columnsoptions").each(
            function () {

                var cols = $(this).attr('columnsvisible');

                my_obj = cols.split(",");

                for (var key in my_obj) {

                    if (my_obj.hasOwnProperty(key)) {

                        var item = my_obj[key].split(":");

                        var columnname = item[0];
                        var value = item[1];

                        var index = $('th:contains(' + columnname + ')', this).index() + 1;

                        if (value == 'false') {
                            $('tr *:nth-child(' + index + ')', this).hide();
                        }
                        else {
                            $('tr *:nth-child(' + index + ')', this).show();
                        }
                    }
                }


            }
        );
}


var textAreaMaxChars = 2000;

function limit(element) {
    if (element.value.length > textAreaMaxChars)
        element.value = element.value.substr(0, textAreaMaxChars);
}


function maxLengthPaste(field) {
    event.returnValue = false;
    if ((field.value.length + window.clipboardData.getData("Text").length) > textAreaMaxChars) {
        return false;
    }
    event.returnValue = true;
}



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
            //buttonText: 'Abrir calendario',
            //buttonImage: '/Content/Images/calendar.png',
            //buttonImageOnly: true,
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
function maxlength(tb, max)
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

function ValidateLength(element, RequiredLength) {
    var Str = element.value;
    $(element).valid();
    if (Str.length < RequiredLength)
        return true;
    return false;
}