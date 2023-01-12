function AdjustChatSize(persistDOM, notCommonZLFn) {
    //notCommonZLFn true = no llamar a commonZLinkFn(), reabre busqueda
    //persistDOM: true mantener DOM activo
    if (IsZambaLink())
        $(".chat-window").css("padding-bottom", "15px");

    if (IsZambaLinkMax()) {
        zLinkState = "maximize";
        if ($(".chat-window").length > 1)
            fullScreenUsersChat(persistDOM, notCommonZLFn);
        else
            fullScreenMainChat(persistDOM, notCommonZLFn);
    }
    else if (IsZambaLinkRestore()) {
        //restoreUsersChat(persistDOM);
        zLinkState = "restore";
        if ($(".chat-window").length > 1)
            restoreUsersChat(persistDOM, notCommonZLFn);
        else
            restoreMainChat(persistDOM, notCommonZLFn);
    }
    if (isTaskChat) {
        if ($(".chat-window").length > 1)
            fullScreenUsersChatForum(persistDOM, notCommonZLFn);
        else
            fullScreenForurm(persistDOM, notCommonZLFn);
    }
}

function fullScreenFromZlink() {
    zLinkState = "maximize";
    if ($(".chat-window").length > 1)
        fullScreenUsersChat();
    else
        fullScreenMainChat();
}

function restoreFromZlink() {
    zLinkState = "restore";
    if ($(".chat-window").length > 1)
        restoreUsersChat();
    else
        restoreMainChat();
}
function minimizeFromZlink() {
    zLinkState = "minimize";
}
var titleIconSizeM = "30px";
var fontSizeMaxM = 17;
var titleHeightM = "55px";

function fullScreenUsersChat(persistDOM, notCommonZLFn) {
    $(".chat-window,.chat-window-content").css({ "width": "100%", "height": "100%", "max-height": "100%", "right": "0" });
    $(".chat-window-inner-content").css({ "width": "100%", "height": "100%", "padding-bottom": "100px", "max-height": "100%" });
    $(".chat-window-text-box-wrapper").css({ "bottom": "0", "position": "absolute", "width": "100%" });
    $(".temp-message, .getMoreMsg").css({ "width": "90%" });
    //$(".chat-window-title>close, .chat-window-title>uploadFile, .chat-window-title>minimized ,#infoUsersGroup,.uploadFile,.chat-window-title>.close")
    //    .css({ "width": titleIconSizeM, "height": titleIconSizeM });
    $("#containerUserName").css({ "font-size": fontSizeMaxM + "px" });
    $(".lastUserConnChat").css({ "font-size": (fontSizeMaxM - 2) + "px" });
    $(".emoji-wysiwyg-editor.chat-window-text-box").css("width", "95%");
    $(".emoji-picker-icon.emoji-picker").css({ "right": "85px" });
    $(".chat-window-title>.glyphicon").css({ "font-size": "20px" });
    // $(".chat-window-title").css("height", titleHeightM);
    fullScreenCommonChat(!persistDOM, notCommonZLFn);
}

function fullScreenMainChat(persistDOM, notCommonZLFn) {
    var avatarSize = "50px";
    var titleNameTop = "15px";
    var heightRow = "75px";
    var heightControl = "45px";
    var widthControl = "300px";
    $("#containerUsersGroup").css({ "height": "40px" });
    $(".chat-window,.chat-window-content,.chat-window-inner-content").css({ "width": "100%", "height": "100%", "max-height": "100%", "right": "0" });
    //Aplico tamaño iconos grandes
    $(".mainAvatar, .profile-picture.embebedIMG").not("[id*='imgGrpChat']").css({ "width": avatarSize, "height": avatarSize });
    //Aplico tamaño iconos grandes a grupo con hasta 2 users
    var gi = $(".profile-picture.embebedIMG[id*='imgGrpChat']");
    for (var i = 0; i <= gi.length - 1; i++) {
        var gc = $(gi[i]).parents(".user-list-item").find("[id*='imgGrpChat']");
        if (gc.length < 3)
            gc.css({ "width": avatarSize, "height": avatarSize });
    }
    $(".mainTitle, .user-list-item>.content").css({ "font-size": fontSizeMaxM + "px", "top": titleNameTop });
    $(".subName, .user-list-item>.subContent").css({ "font-size": (fontSizeMaxM - 2) + "px", "top": titleNameTop });
    $(".user-list-item").css({ height: heightRow });
    $("#Filtro").css({ height: avatarSize });
    $(".filter.form-control").css({ height: heightControl, width: widthControl });
    //Iconos de titulo
    $(".filter-picture,#configId,.addGroup,.colIcon,.minimized,.filter-picture-rem").css({ "width": titleIconSizeM, "height": titleIconSizeM });
    fullScreenCommonChat(!persistDOM, notCommonZLFn);
}

function fullScreenCommonChat(remove, notCommonZLFn) {
    zLinkState = "maximize";
    if (appZChat != "ZambaMobile") appZChat = "ZambaLink";
    if (remove) removeSizedDOMS();
    $(".chat-window-title>.glyphicon").css({ "font-size": "20px" });
    if (!IsZambaMobile()) $(".chat-window").css({ "padding-top": "20px" });
    $(".chat-window-content").css({ "padding-bottom": "50px" });
    if (!notCommonZLFn)
        commonZLinkFn();
    //if (taskChat)
    //    $(".chat-window,.chat-window-content").css({ "padding": "0px" });
}

function fullScreenUsersChatForum(persistDOM, notCommonZLFn) {
    var avatarSize = "35px";
    $(".chat-window,.chat-window-content").css({ "width": "100%", "height": "100%", "max-height": "100%", "right": "0" });
    $(".chat-window-inner-content").css({ "width": "100%", "height": "100%", "padding-bottom": "100px", "max-height": "100%" });
    $(".chat-window-text-box-wrapper").css({ "bottom": "0", "position": "absolute", "width": "100%" });
    $(".temp-message, .getMoreMsg").css({ "width": "90%" });
    $(".user-list-item").css("font-size", "18px");
    //$(".chat-window-title>close, .chat-window-title>uploadFile, .chat-window-title>minimized ,#infoUsersGroup,.uploadFile,.chat-window-title>.close")
    //    .css({ "width": titleIconSizeM, "height": titleIconSizeM });

    $(".pencil, .exit").css("top", "20px");


    $("#filtro").css({
        height: "20px",
        width: "20px"
    });

    $(".fontButton").eq(0).css("display", "none");
    $(".filter-picture").css({
        height: "20px",
        width: "20px"
    });
    $(".subName").css("display", "none");
    $("#containerUserName").css({
        "font-size": fontSizeMaxM + "px",
        "position": "relative",
        "top": "10px",
    });
    $(".lastUserConnChat").css({ "font-size": (fontSizeMaxM - 2) + "px" });
    $(".emoji-wysiwyg-editor.chat-window-text-box").css("width", "95%");
    $(".emoji-picker-icon.emoji-picker").css({ "right": "85px" });
    $(".chat-window-title>.glyphicon").css({ "font-size": "20px" });
    //$(".close").css({ height: avatarSize });
    $(".chat-window-title").css("background-color", "rgb(125, 135, 181)");
    // $(".chat-window-title").css("height", titleHeightM);
    fullScreenCommonChat(!persistDOM, notCommonZLFn);
}

function fullScreenForurm(persistDOM, notCommonZLFn) {
    var titleIconSizeM = "20px";
    var fontSizeMaxM = 17;
    var titleHeightM = "20px";

    var avatarSize = "35px";
    var titleNameTop = "15px";
    var heightRow = "55px";
    var heightControl = "45px";
    var widthControl = "300px";
    //$("#containerUsersGroup").css({ "height": "30px" });
    $(".chat-window,.chat-window-content,.chat-window-inner-content").css({ "width": "100%", "height": "100%", "max-height": "100%", "right": "0" });
    //Aplico tamaño iconos grandes
    //$(".mainAvatar, .profile-picture.embebedIMG").not("[id*='imgGrpChat']").css({ "width": avatarSize, "height": avatarSize });
    //Aplico tamaño iconos grandes a grupo con hasta 2 users
    var gi = $(".profile-picture.embebedIMG[id*='imgGrpChat']");
    for (var i = 0; i <= gi.length - 1; i++) {
        var gc = $(gi[i]).parents(".user-list-item").find("[id*='imgGrpChat']");
        if (gc.length < 3)
            gc.css({ "width": avatarSize, "height": avatarSize });
    }
    $("#containerUserName").css("display", "none");
    $(".chat-window-inner-content").css("background-color", "white");
    //$(".fontButton").css("display", "none");
    $(".mainTitle").css("display", "none");
    $(".chat-window").draggable('disable');
    $(".btnCreateTopic").css("display", "inline-block");
    //$("#filtro").css("height", "20px");


    //$(".mainTitle, .user-list-item>.content").css({ "font-size": fontSizeMaxM + "px", "top": titleNameTop });
    //$(".subName, .user-list-item>.subContent").css({ "font-size": (fontSizeMaxM - 2) + "px", "top": titleNameTop });
    $(".user-list-item").css({ height: heightRow });
    $(".filter-picture").css({
        height: "20px",
        width: "20px"
    });
    //$(".close").css({ height: avatarSize });
    $(".filter.form-control").css({ height: heightControl, width: widthControl });
    $(".user-list-item").css("font-size", "18px");
    //Iconos de titulo
    $("#configId,.addGroup,.colIcon,.minimized,.subName").css("display", "none");
    $("#Filtro").css({
        height: "20px",

    });
    $(".filter-picture-rem").css({
        "height": "20px",
        "width": "20px"
    });
    $(".chat-window-title").css("background-color", "rgb(125, 135, 181)");
    fullScreenCommonChat(!persistDOM, notCommonZLFn);
}

var titleIconSizeR = "16px";
var fontSizeMaxR = 12;
var titleHeightR = "55px";

function restoreUsersChat(persistDOM, notCommonZLFn) {
    if (IsZambaMobile()) {
        fullScreenUsersChat(persistDOM, notCommonZLFn);
        return;
    }
    $(".chat-window").css({ "width": widthCompact + "px", "height": heightCompact + "px", "max-height": heightCompact + "px", "right": "0" });
    //  $(".chat-window-inner-content").css({ "width": widthCompact + "px", "height": heightCompact + "px", "padding-bottom": "100px", "max-height": heightCompact + "px" });
    $(".chat-window-inner-content,.chat-window-content,.chat-window-content").css({
        "width": widthCompact + "px", "height": (heightCompact - titleHeight) + "px",
        "max-height": (heightCompact - titleHeight) + "px", "padding-bottom": "100px"
    });
    $(".chat-window-text-box-wrapper").css({ "bottom": "0", "position": "absolute", "width": widthCompact + "px" });
    $(".temp-message, .getMoreMsg").css({ "width": "150px" });

    $("#containerUserName").css({ "font-size": fontSizeMaxR + "px" });
    $(".lastUserConnChat").css({ "font-size": (fontSizeMaxR - 2) + "px" });
    $(".emoji-wysiwyg-editor.chat-window-text-box").css("width", "87%");
    $(".emoji-picker-icon.emoji-picker").css({ "right": "55px" });
    $(".chat-window-title>.glyphicon").css({ "font-size": "20px" });
    $("#MessageFromChat, #MyMessageChat").css("width", "190px");
    restoreCommonChat(!persistDOM, notCommonZLFn);
}

function restoreMainChat(persistDOM, notCommonZLFn) {
    if (IsZambaMobile()) {
        fullScreenMainChat(persistDOM, notCommonZLFn);
        return;
    }
    var avatarSize = "33px";
    var titleNameTop = "6px";
    var heightRow = "55px";
    var heightControl = "25px";
    var widthControl = "190px";
    $("#containerUsersGroup").css({ "height": "25px" });
    $(".chat-window").css({ "width": widthCompact + "px", "height": heightCompact + "px", "max-height": heightCompact + "px", "right": "0" });

    $(".chat-window-inner-content,.chat-window-content").css({
        "width": widthCompact + "px", "height": (heightCompact - titleHeight) + "px",
        "max-height": (heightCompact - titleHeight) + "px", "padding-bottom": "0px"
    });
    $(".mainAvatar, .profile-picture.embebedIMG").not("[id*='imgGrpChat']").css({ "width": avatarSize, "height": avatarSize });
    //Restauro iconos a grupo con hasta 2 users
    var gi = $(".profile-picture.embebedIMG[id*='imgGrpChat']");
    for (var i = 0; i <= gi.length - 1; i++) {
        var gc = $(gi[i]).parents(".user-list-item").find("[id*='imgGrpChat']");
        if (gc.length < 3)
            gc.css({ "width": avatarSize, "height": avatarSize });
    }
    $(".mainTitle, .user-list-item>.content").css({ "font-size": fontSizeMaxR + "px", "top": titleNameTop });
    $(".subName, .user-list-item>.subContent").css({ "font-size": (fontSizeMaxR - 2) + "px", "top": titleNameTop });
    $(".user-list-item").css({ height: heightRow });
    $("#Filtro").css({ height: avatarSize });
    $(".filter.form-control").css({ height: heightControl, width: widthControl });

    //Iconos de titulo
    $(".filter-picture,#configId,.filter-picture-rem").css({ "width": titleIconSizeR, "height": titleIconSizeR });
    $(".addGroup,.colIcon,.minimized").css({ "width": "24px", "height": "24px" });

    restoreCommonChat(!persistDOM, notCommonZLFn);
}

function restoreCommonChat(remove, notCommonZLFn) {
    zLinkState = "restore";
    if (appZChat != "ZambaMobile") appZChat = "ZambaLink";
    if (remove) removeSizedDOMS();
    $(".chat-window-title>.glyphicon").css({ "font-size": "14px" });
    $(".chat-window").css({ "padding-top": "0px" });
    $(".chat-window-content").css({ "padding-bottom": "0px" });
    if (!notCommonZLFn)
        commonZLinkFn();
}

function removeSizedDOMS() {
    if ($("#statusMenuId").length)
        $("#statusMenuId").remove();
    var $remCC = $(".chat-window-title>.glyphicon.glyphicon-remove.confirmChat");
    if ($remCC.length && $remCC.css("display") != "none")
        $remCC.click();
    if ($(".bootbox").length)
        $(".bootbox").fadeOut()
}

function commonZLinkFn() {
    if ($(".filter-picture.filter-picture-rem").length)
        $(".filter-picture.filter-picture-rem").click();
}

function IsZambaLink() {
    return appZChat == "ZambaLink" || IsZambaMobile();
}
function IsZambaMobile() {
    return appZChat == "ZambaMobile";
}
function IsZambaLinkMax() {
    // return true;
    return IsZambaLink() && zLinkState == "maximize";
}
function IsZambaLinkRestore() {
    // return true;
    return IsZambaLink() && zLinkState == "restore";
}
function IsZambaLinkMin() {
    // return true;
    return IsZambaLink() && zLinkState == "minimize";
}