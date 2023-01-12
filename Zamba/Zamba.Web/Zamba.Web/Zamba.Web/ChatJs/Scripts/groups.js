function GetChatGroups() {
    var usersList = [];
    $.ajax({
        type: "GET",
        async: false,
        contentType: 'application/json',
        dataType: 'json',
        data: { myUserId: thisUserId },
        url: URLBase + '/GetGroups',
        success: function (result) {
            if (result != "[]" && result.length >= 1) {
                chatGroups.Set(result);
                usersList = GetChatGroupsFn(usersList, result);
            }
        }
    });
    return usersList;
}

function GetChatGroupsFn(usersList, result) {
    var $groupDiv = $(MainWinChat().find(".chat-window-inner-content"));
    if (!$(".groupContactTitle").length) {
        var gdPx = chatMode == "normal" ? "90px" : "60px";
    }

    for (var i = 0; i <= result.length - 1; i++) {
        if ($(".user-list-item[chatid=" + result[i].ChatId + "]").length)
            continue;

        var participantsIds = [];
        var names = "";
        for (var j = 0; j <= result[i].GroupList.length - 1; j++) {
            if (result[i].GroupList[j] != null) {
                participantsIds.push(result[i].GroupList[j].Id);
                names += result[i].GroupList[j].Name + ",";
            }
        }

        participantsIds = participantsIds.sort(function (a, b) { return a - b });
        var users = {};
        users.Name = (result[i].ChatName != undefined) ? (result[i].ChatName + names) : names;
        users.isGroup = true;
        users.isExternal = false;
        users.chatId = result[i].ChatId;
        users.lastMessage = result[i].LastMessage;
        users.Id = participantsIds;
        usersList.push(users);
        // var toolTip = "";
        var $groupListItem = $("<div/>")
            .addClass("user-list-item")
            .appendTo($groupDiv)
        if (!result[i].GroupList.length)
            result[i].GroupList.push(TempUserInfo(thisUserId));
        var uG = result[i].GroupList.length;
        var toFor = (uG >= 4 ? 3 : (uG - 1));
        for (var j = 0; j <= toFor; j++) {
            var widthImg = uG >= 3 ? 22 : (66 / uG);
            if (result[i].GroupList.length == 1)
                widthImg = 33;

            var $userImg = result[i].GroupList[j].Avatar;
            var $div = $("<div/>").addClass("DivGruposChat").attr("title", result[i].GroupList[j].Name).appendTo($groupListItem);

            if (uG >= 3 && j == 0)
                $div.css("display", "flex");

            if (uG <= 2) {
                $div.removeClass('DivGruposChat');
            }

            var $groupImgItem = $("<img/>")
                .addClass("profile-picture")
                .attr("src", "data:image/jpg;base64," + result[i].GroupList[j].Avatar)
                .attr("title", result[i].GroupList[j].Name)
                .attr("id", "imgGrpChat" + result[i].GroupList[j].Id)
                .appendTo($div);

            if (uG >= 4 && j == 3)
                $groupImgItem.css({ "margin-top": "-23px", "margin-left": "-23px" });

            if (chatMode == "compact") {
                $groupImgItem.css(getBorderFromStatus(result[i].GroupList[j].Status));
                $groupImgItem.css({ height: uG >= 3 ? widthImg : 33 + "px", width: widthImg + "px" });
                $groupListItem.css({ margin: "1px" });
                expandImg($groupImgItem);
            }
            $div.tooltip();
        }
        var $divNameGroup = $("<div/>").addClass("txtNameGroupChat").text(ReduceName(result[i].ChatName, 18)).appendTo($groupListItem);
        var $msgCount = $("<div/>").addClass("msgCount").attr("title", "Mensajes sin leer").appendTo($groupListItem);
        $msgCount.tooltip();

        var $div = $("<div/>").appendTo($groupListItem);
        var usrName = "";
        for (var k = 0; k <= result[i].GroupList.length - 1; k++)
            usrName += result[i].GroupList[k].Name.split("/")[0] + ", ";

        //toolTip += usrName;
        $div.attr("title", usrName.substring(0, usrName.length - 2));
        $groupListItem.attr({ "date": result[i].LastMessage, "chatId": result[i].ChatId, "id": result[i].ChatId })//, "title": toolTip.substring(0, toolTip.length - 2) 
            .click(function (e) {
                if ($(e.target).hasClass("embebedIMG")) return;
                if (chatMode == "compact")
                    $(this).children(".msgCount").text("").css("display", "none");

                MainWinChat().data({ "chatOpened": "chat" + $(this).attr("chatid"), "open": true, "external": false });
                $("#openGroupDiv").data("chatId", $(this).attr("chatid")).click();
            });
        ContextMenu($groupListItem);
        //return usersList;
    }


    return usersList;
}

function createEmptyGroup($cg) {
    $cg.parent().children("#statusMenuId").remove();
    $cg.parent().children(".mainTitle, .subName, .mainAvatar, #containerUserName, .config, .colIcon, #Filtro, .addGroup").hide();
    $cg.parent().children("#containerUsersGroup").css({
        "background-color": "white",
        "color": "black",
        "height": "20px",
        "top": "0px",
        "margin": "5px 0 0 30px",
    }).fadeToggle()
        .prop("disabled", false).attr("placeholder", "Nombre grupo").css({
            "width": "60%",
        }).keypress(function (e) {
            if (e.which == 13) {
                e.preventDefault();
                $(this).parent().children(".confirmChat").click();
            }
        })
        .focus().removeAttr("readonly");

    $("<span/>").addClass("glyphicon glyphicon-remove confirmChat")
        .attr("title", "Cancelar").attr("data-placement", "bottom")
        .css({ "color": "rgb(255, 136, 136)", "float": "right", "top": "7px", "margin-right": "7px" }).appendTo($cg.parent())
        .click(function (e) { CancelEmptyGroup(e, $cg); return false; }).tooltip();
    $("<span/>").addClass("glyphicon glyphicon-ok confirmChat")
        .attr("title", "Crear grupo").attr("data-placement", "bottom")
        .css({ "color": "rgb(167, 255, 167)", "float": "right", "top": "7px", "margin-right": "7px" }).appendTo($cg.parent())
        .click(function (e) { CreateGroup(e, $cg); return false; }).tooltip();

    AdjustChatSize(true);
}

function CancelEmptyGroup(e, $cg) {
    $cg.parent().children(".mainTitle, .subName, .mainAvatar, #containerUserName, .config, .colIcon, #Filtro, .addGroup").show();
    $cg.parent().children(".confirmChat, #containerUsersGroup").hide();
}

function CreateGroup(e, $cg) {
    var inp = $cg.parent().children("#containerUsersGroup");
    if (inp.val() == "") {
        toastrInTitle("Error al crear grupo", "Por favor coloque nombre");
    } else {
        $cg.parent().children(".mainTitle, .subName, .mainAvatar, #containerUserName, .config, .colIcon, #Filtro, .addGroup").show();
        $cg.parent().children(".confirmChat, #containerUsersGroup").hide();
        var $cw = $cg.parents(".chat-window");
        $cw.data("groupName", inp.val());
        $cw.data("isGroup", true);
        inp.val("");

        $cw.find(".user-list-item[createGroup=true]").click();
        $(".chat-window[isgroup=true]").find(".close").click();
        //$(".user-list-item[chatid=" + MainWinChat().data("openChat") + "]").click();
        MainWinChat().data("openChat", "");
    }
}

function CreateGroupList(id, gn) {
    var groups = chatGroups.Get();
    groups.push({
        ChatId: id,
        ChatName: gn,
        GroupList: new Array(),
        LastMessage: new Date().toISOString()
    });
    chatGroups.Add(groups[groups.length - 1]);

    GetChatGroupsFn(new Array(), groups);
}

///////////////////////////////////////////////////Metodos que se utilizan en el foro//////////////////////////////////////////////////

function GetChatGroupsForum() {
    var usersList = [];
    $.ajax({
        type: "GET",
        async: false,
        contentType: 'application/json',
        dataType: 'json',
        data: { myUserId: thisUserId },
        url: URLBase + '/GetGroupsForum',
        success: function (result) {
            if (result !== "[]" && result.length >= 1) {
                chatGroupsForum.Set(result);
                Groups = GetChatGroupsFnForum(usersList, result);
            }
        }
    });


    return usersList;
}

function GetChatGroupsFnForum(usersList, result) {

    var $groupDiv = $(MainWinChat().find(".chat-window-inner-content"));
    if (!$(".groupContactTitle").length) {
        var gdPx = chatMode === "normal" ? "90px" : "60px";
    }
    for (var i = 0; i <= result.length - 1; i++) {
        if ($(".user-list-item[chatid=" + result[i].ChatId + "]").length)
            continue;

        var participantsIds = [];
        var names = "";
        for (var j = 0; j <= result[i].GroupList.length - 1; j++) {
            if (result[i].GroupList[j] !== null) {
                participantsIds.push(result[i].GroupList[j].Id);
                names += result[i].GroupList[j].Name + ",";
            }
        }

        participantsIds = participantsIds.sort(function (a, b) { return a - b });
        var users = {};
        users.Name = (result[i].ChatName != undefined) ? (result[i].ChatName + names) : names;
        users.isGroup = true;
        users.DocId = result[i].DocId;
        users.isExternal = false;
        users.chatId = result[i].ChatId;
        users.lastMessage = result[i].LastMessage;
        users.Id = participantsIds;
        var CurrentDocId = "";
        CurrentDocId = parseInt($("[id$=hdnDocId]", window.parent.document).val());

        if (result[i].DocId === CurrentDocId) {
            usersList.push(users);
            // var toolTip = "";
            var $groupListItem = $("<div/>")
                .addClass("user-list-item")
                .appendTo($groupDiv)
                .css({ "background-color": "white", "border-bottom": "1px solid #dadada" });
            if (!result[i].GroupList.length)
                result[i].GroupList.push(TempUserInfo(thisUserId));
            var uG = result[i].GroupList.length;
            var toFor = (uG >= 4 ? 3 : (uG - 1));
            for (var j = 0; j <= toFor; j++) {
                var widthImg = uG >= 3 ? 22 : (66 / uG);
                if (result[i].GroupList.length === 1)
                    widthImg = 33;

                var $userImg = result[i].GroupList[j].Avatar;
                var $div = $("<div/>").addClass("DivGruposChat").attr("title", result[i].GroupList[j].Name).appendTo($groupListItem);

                if (uG >= 3 && j === 0)
                    $div.css("display", "flex");

                if (uG <= 2) {
                    $div.removeClass('DivGruposChat');
                }

                var $groupImgItem = $("<img/>")
                    .addClass("profile-picture")
                    .attr("src", "data:image/jpg;base64," + result[i].GroupList[j].Avatar)
                    .attr("title", result[i].GroupList[j].Name)
                    .attr("id", "imgGrpChat" + result[i].GroupList[j].Id)
                    .appendTo($div);

                if (uG >= 4 && j === 3)
                    $groupImgItem.css({ "margin-top": "-23px", "margin-left": "-23px" });

                if (chatMode === "compact") {
                    $groupImgItem.css(getBorderFromStatus(result[i].GroupList[j].Status));
                    $groupImgItem.css({ height: uG >= 3 ? widthImg : 33 + "px", width: widthImg + "px" });
                    $groupListItem.css({ margin: "1px" });
                    expandImg($groupImgItem);
                }
                $div.tooltip();
            }
            var $divNameGroup = $("<div/>").addClass("txtNameGroupChat").text(ReduceName(result[i].ChatName, 18)).appendTo($groupListItem);
            var $msgCount = $("<div/>").addClass("msgCount").attr("title", "Mensajes sin leer").appendTo($groupListItem);
            $msgCount.tooltip();
            $(".user-list-item").css("font-size", "16px");

            var $div = $("<div/>").appendTo($groupListItem);
            var usrName = "";
            for (var k = 0; k <= result[i].GroupList.length - 1; k++)
                usrName += result[i].GroupList[k].Name.split("/")[0] + ", ";

            //toolTip += usrName;
            $div.attr("title", usrName.substring(0, usrName.length - 2));
            $groupListItem.attr({ "date": result[i].LastMessage, "chatId": result[i].ChatId, "id": result[i].ChatId })//, "title": toolTip.substring(0, toolTip.length - 2) 
                .click(function (e) {
                    if ($(e.target).hasClass("embebedIMG")) return;
                    if (chatMode === "compact")
                        $(this).children(".msgCount").text("").css("display", "none");

                    MainWinChat().data({ "chatOpened": "chat" + $(this).attr("chatid"), "open": true, "external": false });
                    $("#openGroupDiv").data("chatId", $(this).attr("chatid")).click();
                }); $(".chat-window-content").css
                    ({

                        "padding": "0px"
                    });
            ContextMenu($groupListItem);

        }
    }
    return usersList;
}

function createEmptyGroupForum($cg) {
    $cg.parent().children("#statusMenuId").remove();
    $cg.parent().children(".mainTitle, .subName, .mainAvatar, #containerUserName, .config, .colIcon, #Filtro, .addGroup").hide();
    $cg.parent().children("#containerUsersGroup").css({
        "background-color": "white",
        "color": "black",
        "height": "20px",
        "top": "0px",
        "margin": "5px 0 0 30px",
    }).fadeToggle()
        .prop("disabled", false).attr("placeholder", "Nombre grupo").css({
            "width": "60%",
        }).keypress(function (e) {
            if (e.which === 13) {
                e.preventDefault();
                $(this).parent().children(".confirmChat").click();
            }
        })
        .focus().removeAttr("readonly");

    $("<span/>").addClass("glyphicon glyphicon-remove confirmChat")
        .attr("title", "Cancelar").attr("data-placement", "bottom")
        .css({ "color": "rgb(255, 136, 136)", "float": "right", "top": "7px", "margin-right": "7px" }).appendTo($cg.parent())
        .click(function (e) { CancelEmptyGroup(e, $cg); return false; }).tooltip();
    $("<span/>").addClass("glyphicon glyphicon-ok confirmChat")
        .attr("title", "Crear grupo").attr("data-placement", "bottom")
        .css({ "color": "rgb(167, 255, 167)", "float": "right", "top": "7px", "margin-right": "7px" }).appendTo($cg.parent())
        .click(function (e) { CreateGroup(e, $cg); return false; }).tooltip();

    AdjustChatSize(true);
}

function CreateGroupListForum(id, TopicName, DocId) {
    var groups = chatGroupsForum.Get();
    groups.push({
        ChatId: id,
        ChatName: TopicName,
        GroupList: new Array(),
        DocId: DocId,
        LastMessage: new Date().toISOString()
    });
    chatGroupsForum.Add(groups[groups.length - 1]);

    GetChatGroupsFnForum(new Array(), groups);


}