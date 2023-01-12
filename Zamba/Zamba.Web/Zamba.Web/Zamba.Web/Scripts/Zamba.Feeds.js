var currFeeds;
var feedTemplate = '<div id="zF_{0}" class="zFeedItem_R{1}"><span class="zFeedTime">{2}</span><span class="zFeedText">{3}</span></div>';
var refreshInterval;
var linesCount;
var contentHeight;

function InitializeFeeds() {
    //Get config variables
    refreshInterval = parseInt(getRfInterval());
    linesCount = parseInt(getFLCount());
    contentHeight = (linesCount) * 1.563;

    //If user can see feeds
    if (getFView().toLowerCase() != "false") {
        //Set Dialog
        var fDialog = $("#divZFeeds");
        fDialog.dialog({
            autoOpen: false, position: 'left top', closeOnEscape: false, draggable: false, resizable: false, show: "fast", open: SetDialogPreferences, width:280
        });

        //Start feeds get process
        UpdateFeedsLoop();

        //Add minimize button and feed header features
        var dHeader = fDialog.parent().find(":first-child");
        dHeader.addClass("feedHeader");
        var $newButton = $('<div id="feedMinimize" class="feedMinimize">&nbsp;</div>');
        $(dHeader[0]).append($newButton);
        dHeader.find("#feedMinimize").click(function () {
            if ($(this).hasClass("feedMinimize")) {
                MinimizeFeedContent();
            }
            else {
                RestoreFeedContent();
            }
        });
    }
}

function SetDialogPreferences(event, ui) {
    var fDialog = $("#divZFeeds").parent();
    fDialog.css("z-index", "1");
    fDialog.css("padding","0px");
}

function MinimizeFeedContent() {
    var fDialog = $("#divZFeeds");
    fDialog.height(1);
    fDialog.parent().css("padding", "0px");
    fDialog.parent().css("z-index", "1");
    $("#feedMinimize").removeClass("feedMinimize");
    $("#feedMinimize").addClass("feedRestore");
}

function RestoreFeedContent() {
    var fDialog = $("#divZFeeds");
    fDialog.parent().css("padding", "0px");
    fDialog.css("height", contentHeight + "em");
    fDialog.dialog("open");
    fDialog.parent().css("z-index", "1");
    fDialog.css("height", contentHeight + "em");
    $("#feedMinimize").removeClass("feedRestore");
    $("#feedMinimize").addClass("feedMinimize");
}

//Start feed get proces
function UpdateFeedsLoop() {
    //Call inicialization
    GetFeeds();
    //Intance next loop
    setTimeout("UpdateFeedsLoop();", refreshInterval);
}

function GetFeeds() {
    var userId = GetUID();
    $.ajax({
        url: "../../Services/TaskService.asmx/GetUserFeeds",
        type: "POST",
        dataType: "json",
        cache: true,
        data: "{userId:'" + userId + "'}",
        contentType: "application/json; charset=utf-8",
        success: GetFeedsComplete,
        error: GetFeedError
    });
}

function GetFeedsComplete(msg) {
    var getedFeeds = msg.d;
    var distinct = false;
    var i = 0;

    //If there arent previous feeds, set gettedfeeds
    if (currFeeds == null) {
        currFeeds = getedFeeds;
        distinct = true;
    }
    else {
    //Check if there is a new feed
        while (!distinct && getedFeeds != null && getedFeeds[i] != null) {
            if (currFeeds[i].ID != getedFeeds[i].ID)
                distinct = true;

            i++;
        } 
    }

    var newFeeds = "";
    //if there is a new feed
    if (distinct && getedFeeds != null) {
        //build feeds content
        for (i = 0; getedFeeds[i] != null; i++) {
            var date = new Date(parseInt(getedFeeds[i].CreateDate.substr(6)));
            //Fecha completa dia mes ano hora minutos segundos
            //var parsedDate = $.datepicker.formatDate("dd/mm/yy", date) + " " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
            var parsedDate = $.datepicker.formatDate("dd/mm/y", date) + " " + date.getHours() + ":" + date.getMinutes();

            newFeeds += feedTemplate.replace("{0}", getedFeeds[i].ID).replace("{1}", getedFeeds[i].Readed).replace("{2}", parsedDate).replace("{3}", getedFeeds[i].Text);
        }
        
        //Set content in control
        var fDialog = $("#divZFeeds");
        fDialog[0].innerHTML = newFeeds;
        RestoreFeedContent();
        currFeeds = getedFeeds;
    }
}

function GetFeedError(e) {
    var fDialog = $("#divZFeeds");
    fDialog[0].innerHTML = "<span>Ha ocurrido un error al obtener las novedades</span>";
    RestoreFeedContent();
}