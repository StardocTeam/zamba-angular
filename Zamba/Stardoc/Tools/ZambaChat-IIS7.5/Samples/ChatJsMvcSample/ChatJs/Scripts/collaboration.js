//Modulo de Zamba Collaboration, colocar scripts unicamente referidos a esta funcionalidad

function CreateCollaborationIcon($title) {
    var $colDiv = $("<div/>").addClass("colIcon")
               .attr("title", "Invitar")
               .attr("data-placement", "left")
               .appendTo($title).tooltip()
                .click(function () {
                    if (validateEmail(myUserInfo.CORREO)) {
                        ShowCollaborationWindow();
                    }
                    else {
                        toastrInTitle("Por favor registre su mail");
                    }
                });
}

function ShowCollaborationWindow() {
    var btx = bootbox.dialog({
        title: "<span class='glyphicon glyphicon-globe'></span> Invitar usuario",
        message: '<div class="row">  ' +
            '<div class="col-md-12"> ' +
                '<form class="form-horizontal"> ' +
            '<div class="form-group"> ' +
            '<label class="col-md-4 control-label" for="name">Nombre y apellido</label> ' +
            '<div class="col-md-8"> ' +
                '<input id="bootUserName" name="name"  maxlength="50" type="text" class="form-control input-sm"> ' +
            '</div> </div> ' +
            '<label class="col-md-4 control-label" for="name">E-Mail</label> ' +
            '<div class="col-md-8"> ' +
            '<input id="bootUserMail" name="status" type="email"  maxlength="50" class="form-control input-sm"> ' +
            '</div>' +
             '<label class="col-md-4 control-label" for="name">Empresa</label> ' +
            '<div class="col-md-8"> ' +
            '<input id="bootUserCompany" name="status" type="text"  maxlength="50" class="form-control input-sm"> ' +
               '</div>' +
             ' </div> </form> <input type="checkbox" name="recibeCopy" id="recibeCopy" >Recibir copia de invitacion<b></b></div>  </div>',
        buttons: {
            success: {
                label: "<span class='glyphicon glyphicon-send'></span> Invitar ",
                className: "btn-success btn-xs",
                callback: function () {
                    var name = $('#bootUserName').val();
                    var mail = $('#bootUserMail').val();
                    var company = $('#bootUserCompany').val();
                    var mailCopy = $('#recibeCopy').is(':checked');
                    if (!validateEmail(mail)) {
                        toastrInTitle('Error al generar la invitacion', 'Direccion de Email invalido');
                        return;
                    }
                    if (name == "") {
                        toastrInTitle('Error al generar la invitacion', 'Por favor ingrese nombre y apellido de invitado');
                        return;
                    }
                    if (company == "") {
                        toastrInTitle('Error al generar la invitacion', 'Por favor ingrese el nombre de la compañia/empresa');
                        return;
                    }
                    InviteZCollaboration(thisUserId, myUserInfo.CORREO, name, mail, company, mailCopy)
                }
            }
        }
    });
    btx.css("z-index", "99999999999999");
}

function InviteZCollaboration(id, myMail, invName, invMail, company, mailCopy) {
    var us = (thisExtUserId == undefined || !thisExtUserId.length) ? new Object() : myUserChatInfo;
    us.avatar = myUserChatInfo.Avatar;
    us.name = myUserChatInfo.Name;
    us.roomId = myUserChatInfo.RoomId;
    us.Company = myUserChatInfo.Company;

    us.Email = myMail;
    us.InvitedUserName = invName;
    us.InvitedEmail = invMail;
    us.InvitedCompany = company;
    us.MailCopy = mailCopy;
    var dataObject = JSON.stringify(us);
    var url = (appZChat != "ZambaLink") ? zCollLnk : zCollLnk.replace("https", "http");

    var res;
    $.ajax({
        type: "POST",
        url: url + 'account/invite',
        contentType: 'application/json',
        //contentType: 'application/x-www-form-urlencoded; charset=UTF-8',//Para HTTPS
        data: dataObject,
        crossDomain: true,
        success: function (d) {
            toastrInTitle("Se envio invitacion a", d.InvitedUserName + " (" + d.InvitedEmail + ")");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            toastrInTitle("Error al invitar usuario", "Por favor intentelo en unos minutos");
            console.info("Ver si HTTPS funciona, abrir pagina y habilitar");
        }
    });
}

function CheckUserCredentials(ev, app) {
    ev.preventDefault();
    ev.stopImmediatePropagation();
    var email = $("#loginUserMail").val();
    if (validateEmail(email)) {
        var password = $("#loginUserPassword").val();
        $.ajax({
            type: "GET",
            async: false,
            url: URLServer + 'collaboration/GetUserByCredentials',
            contentType: 'application/json',
            data: { "email": email, "password": password },
            crossDomain: true,
            success: function (d) {
                if (d.indexOf("Error:") > -1)
                    ShowErrorLogin(d);
                else {
                    if (app == "ZambaLink" || app == "ZambaChat") {
                        window.location.replace(URLServer + "collaboration/joinzlink?enc=" + d);
                    }
                    else if (app == "ZambaMobile") {
                        window.location.replace(URLServer + "mobile/join?enc=" + d);
                    }
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                ShowErrorLogin("Se produjo un error al validar sus datos");
            }
        });
    }
    else
        ShowErrorLogin("Formato de mail incorrecto");

    function ShowErrorLogin(msg) {
        $("#errorLoginMsg").show();
        $("#loginDetailError").text(msg);
        $("#loginUserPassword").val("");
    }
}

function GetExtChat() {
    var usersList = [];
    $.ajax({
        type: "GET",
        async: false,
        contentType: 'application/json',
        dataType: 'json',
        data: { myUserId: thisExtUserId },
        url: colServer + 'RestCollaboration/GetGroups',
        success: function (result) {
            if (result != "[]" && result.Groups.length >= 1)
                usersList = result.Groups;
        }
    });
    return usersList;
}

function GetExtChatFn(groups) {
    var result = groups;
    var $groupDiv = $(MainWinChat().find(".chat-window-inner-content"));
    var usersList = [];

    for (var i = 0; i <= result.length - 1; i++) {
        if ($(".user-list-item[extchatid=" + result[i].ChatId + "]").length)
            continue;//Ya agregado

        var participantsIds = [];
        var names = "";
        if (result[i].GroupList ==undefined){
            result[i].GroupList = new Array();
            result[i].GroupList.push(result[i]);
        }
        for (var j = 0; j <= result[i].GroupList.length - 1; j++) {
            participantsIds.push(result[i].GroupList[j].Id);
            names += result[i].GroupList[j].Name + ",";
        }
        participantsIds = participantsIds.sort(function (a, b) { return a - b });
        var users = {};
        users.Name = (result[i].ChatName != undefined) ? (result[i].ChatName + names) : names;
        users.isGroup = result[i].ChatType == 2 ? true : false;
        users.isExternal = true;
        users.chatId = result[i].ChatId;
        users.lastMessage = result[i].LastMessage;
        users.Id = participantsIds;
        usersList.push(users);

        var toolTip = "";
        var $groupListItem = $("<div/>")
           .addClass("user-list-item")
            .css("background-color", "azure")
            .attr("id", "chatext" + users.chatId)
           .appendTo($groupDiv);
        if (!result[i].GroupList.length)
            result[i].GroupList.push(TempUserInfo(thisUserId));

        for (var j = 0; j <= (result[i].GroupList.length >= 4 ? 3 : (result[i].GroupList.length - 1)) ; j++) {
            var uG = result[i].GroupList.length;
            var widthImg = result[i].GroupList.length >= 3 ? 22 : (66 / result[i].GroupList.length);

            if (result[i].GroupList.length == 1)
                widthImg = 33

            var $userImg = result[i].GroupList[j].Avatar;
            var $div = $("<div/>").addClass("DivGruposChat").attr("title", result[i].GroupList[j].Name).appendTo($groupListItem);

            if (result[i].GroupList.length >= 3 && j == 0)
                $div.css("display", "flex");

            if (result[i].GroupList.length <= 2) {
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
        var $divNameGroup = $("<div/>").addClass("txtNameGroupChat")
            .text(ReduceName(result[i].ChatName != "" ? result[i].ChatName : result[i].GroupList[0].Name, 18)).appendTo($groupListItem);
        var $sC = $("<div/>")
                          .addClass("subContentExt")
                          .text(result[i].GroupList[0].Company)
                          .appendTo($groupListItem);

        var $msgCount = $("<div/>").addClass("msgCount").attr("title", "Mensajes sin leer").appendTo($groupListItem).tooltip();

        var $div = $("<div/>").appendTo($groupListItem);
        var usrName = "";
        for (var k = 0; k <= result[i].GroupList.length - 1; k++)
            usrName += result[i].GroupList[k].Name.split("/")[0] + ", ";

        toolTip += usrName;
        $div.attr("title", usrName.substring(0, usrName.length - 2));
        var cId = result[i].ChatId;
        $groupListItem.attr({ "date": result[i].LastMessage, "extchatId": cId, "chatId": cId, "extid": cId, "isgroup": result[i].isGroup, "title": toolTip.substring(0, toolTip.length - 2) })
        .tooltip().click(function (e) {
            if ($(e.target).hasClass("embebedIMG")) return;
            if (chatMode == "compact")
                $(this).children(".msgCount").text("").css("display", "none");

            MainWinChat().data({ "chatOpened": "chat" + $(this).attr("chatid"), "open": true, "external": true });
            $("#openExtGroupDiv").data("chatId", $(this).attr("chatid"));
            $("#openExtGroupDiv").click();
        });
        ContextMenu($groupListItem);
    }
    return usersList;
}

function GetExtChatUser(id) {
    var user;
    $.ajax({
        type: "GET",
        async: false,
        url: colServer + 'chat/getuserinfo',
        data: { userId: id },
        success: function (d) {
            user = d.User;
        },
    });
    return user;
}