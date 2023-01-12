//script para reconocer cuando se cierra el browser y cerrar sesion adecuadamente
 var clicked = false;  
window.onbeforeunload = bodyUnload;
function CheckBrowser()  
{      
    if (clicked == false)   
    {      
        //Browser closed   
    }        else  
    {  
        //redirected
        clicked = false; 
    } 
}  
function bodyUnload() 
{      
    if (clicked == false)//browser is closed  
    {   
        
        var request = GetRequest();  
        request.open  ("GET", "../Security/Logout.aspx", true);    
        request.send();    
    } 
} 
 
function GetRequest()  
{       
    var xmlHttp = null;
    try {
        // Firefox, Opera 8.0+, Safari
        xmlHttp = new XMLHttpRequest();
    }
    catch (e) {
        //Internet Explorer
        try {
            xmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
        }
        catch (e) {
            xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
        }
    }
    return xmlHttp;
}          



//23/08/11: se agregan métodos para finalizar las tareas que el usuario posea abiertas.
window.onunload = function () {
    if (showedDialog) {
        var tabcount = $('#TasksDivUL').find("li").length;
        if (tabcount > 0) {
            var userID = $('#hdnUserId').value;
            try {
                if (userID != undefined) {
                    //ScriptWebServices.TaskService.CloseAllAsignedTask(userID);
                    $.ajax({
                        type: "POST",
                        url:"../../Services/TaskService.asmx/CloseAllAsignedTask",
                        data: "{ userID: " + userID + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                        },
                        error: function (request, status, err) {
                            toastr.error("Error en la ejecucion de reglas \r" + request.responseText);
                        }
                    });
                }
            } catch (e) { }
        }
        var connectionId = $('#hdnConnectionId').value;
        var computer = $('#hdnComputer').value;
        try {
            
            if (connectionId != undefined && computer != undefined) {
                ScriptWebServices.TaskService.RemoveConnectionFromWeb(connectionId, computer);
            }
        } catch (e) { }
    }
};

function CloseModal() {
    $('#modalDialog').dialog('close');
}

//28/07/11: se agrega la posibilidad que para todas las páginas hijas que tengan grillas con los estilos correspondientes, al presionar la row se abra el link
$(document).ready(function () {
    $.culture = Globalize.culture("es-AR");
    $.validator.methods.date = function (value, element) {
        //This is not ideal but Chrome passes dates through in ISO1901 format regardless of locale 
        //and despite displaying in the specified format.

        return this.optional(element)
            || Globalize.parseDate(value, "dd/MM/yyyy", "es-AR")
            || Globalize.parseDate(value, "yyyy-mm-dd");
    }

    var baseUrl = location.origin;

    loadCliksInRows();
    //SetEntryRulesControlObserver();
    StartObjectLoadingObserverById("ctl00_WFExecForEntryRulesFrame");
    document.firstLoad = true;
    //setTimeout("SetLoadingAnimationObserver();", 5000);

    //setTimeout("keepSessionAlive()", document.config.sessionTimeOut * 60000 - 30000); //Se comenta ya que es llamada indefinidamente(ver consola web)

    var now = new Date();
    $("#start").html("<span>" + now + "</span>");
});
//At start set the expand clicked to false
var IsExpandClicked = false;

//If root node clicked set the IsExpandClicked to true
$('.RootAllKeys').click(function () {
    IsExpandClicked = true;
});

//If root node clicked set the IsExpandClicked to true
$('.StepTreeNode').click(function () {
    IsExpandClicked = true;
});

//If parent node clicked set the IsExpandClicked to true
$('.ParentAllKeys').click(function () {
    IsExpandClicked = true;
});

function fnEnableGlobalSearch() {
    if (!enableGlobalSearch) {
        $("#Advfilter1").hide();
        $("#Advfilter3").hide();
    }
    else
        $("#Advfilter2").show();
}