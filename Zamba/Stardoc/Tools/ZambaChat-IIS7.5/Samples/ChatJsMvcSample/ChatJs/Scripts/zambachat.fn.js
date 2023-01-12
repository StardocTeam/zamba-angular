$(document).ready(function () {
    var $ul = $("<ul />").addClass("custom-menu").css("display", "none")
        .append('<li data-action="info">Ver Información</li>')
        .append('<li data-action="msg">Escribir Mensaje</li>')
        .appendTo("body");
    if (appZChat != undefined && appZChat != "ZambaLink") $ul.append('<li data-action="mail">Enviar Mail</li>');
    $(document).click(function () {
        $ul.hide();
    });
});

function linkify($element) {
    var inputText = $element.html();
    if (inputText.indexOf('<img src="replaceToURLZmb') > -1) {
        inputText = inputText.replaceAll("replaceToURLZmb", thisDomain);
        return $element.html(inputText);
    }
    //Que lo vinculos de zamba no me haga hiperviculo
    if (inputText.indexOf(thisDomain) >= 0 ||
        inputText.indexOf('<div class="shareZmbLnk"><img class="shareZmbLnkImg"') > -1) return inputText;

    if (inputText.indexOf("youtube.com/embed") >= 0) return inputText; // Youtube
    var replacedText, replacePattern1, replacePattern2, replacePattern3;

    //URLs starting with http://, https://, or ftp://
    replacePattern1 = /(\b(https?|ftp):\/\/[-A-Z0-9+&@#\/%?=~_|!:,.;]*[-A-Z0-9+&@#\/%=~_|])/gim;
    replacedText = inputText.replace(replacePattern1, '<a href="$1" target="_blank">$1</a>');

    //URLs starting with "www." (without // before it, or it'd re-link the ones done above).
    replacePattern2 = /(^|[^\/])(www\.[\S]+(\b|$))/gim;
    replacedText = replacedText.replace(replacePattern2, '$1<a href="http://$2" target="_blank">$2</a>');

    //Change email addresses to mailto:: links.
    replacePattern3 = /(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})/gim;
    replacedText = replacedText.replace(replacePattern3, '<a href="mailto:$1">$1</a>');
    replacedText = replacedText.replace("&nbsp;", "");
    return $element.html(replacedText);
}

function emotify($element) {
    var inputText = $element.html();
    var replacedText = inputText;

    var emoticons = [
        { pattern: ":-\)", cssClass: "happy" },
        { pattern: ":\)", cssClass: "happy" },
        { pattern: "=\)", cssClass: "happy" },
        { pattern: ":-D", cssClass: "very-happy" },
        { pattern: ":D", cssClass: "very-happy" },
        { pattern: "=D", cssClass: "very-happy" },
        { pattern: ":-\(", cssClass: "sad" },
        { pattern: ":\(", cssClass: "sad" },
        { pattern: "=\(", cssClass: "sad" },
        { pattern: ":-\|", cssClass: "wary" },
        { pattern: ":\|", cssClass: "wary" },
        { pattern: "=\|", cssClass: "wary" },
        { pattern: ":-O", cssClass: "astonished" },
        { pattern: ":O", cssClass: "astonished" },
        { pattern: "=O", cssClass: "astonished" },
        { pattern: ":-P", cssClass: "tongue" },
        { pattern: ":P", cssClass: "tongue" },
        { pattern: "=P", cssClass: "tongue" },
        { pattern: "chocolito", cssClass: "chocolito" }
    ];

    for (var i = 0; i < emoticons.length; i++) {
        replacedText = replacedText.replace(emoticons[i].pattern, "<span class='" + emoticons[i].cssClass + "'></span>");
    }
    return $element.html(replacedText);
}

function formatDateText(date) {
    return " (" + (date.getDate() <= 9 ? ("0" + date.getDate()) : date.getDate()) + "-"
        + ((date.getMonth() + 1) <= 9 ? ("0" + (date.getMonth() + 1)) : (date.getMonth() + 1)) + "-"
        + (date.getFullYear()).toString().substring(2) + " " + (date.getHours() <= 9 ? ("0" + date.getHours())
            : date.getHours()) + ":" + (date.getMinutes() <= 9 ? ("0" + date.getMinutes()) : date.getMinutes()) + "hs)";
}

function ReduceName(name, len) {
    return name.length >= len ? (name.substring(0, len) + "...") : name;
}

function SetFunctionUpload(ids, useExternal) {
    //ids debe ser "chatidNumero" sino lo busca por usuario
    var idsBar = ids.replace(new RegExp("/", 'g'), "-");
    $("#upFile").attr("id", "upFile" + idsBar);
    var upUserId = useExternal ? thisExtUserId : thisUserId;
    var url = useExternal ? colServer + "chat" : URLBase;
    $("#upFile" + idsBar).fileupload({
        dataType: 'json',
        url: url + '/UploadFiles',
        dropZone: $("div[id='" + ids + "']"),//[id*=
        formData: { id: ids, myUserId: upUserId, useExternal: useExternal },
        // dropZone:$('#fileupload'),
        autoUpload: true,
        done: function (e, data) {
            if (data.result.name == "Error") {
                bootbox.dialog({
                    title: "Se ha producido un error al subir el archivo",
                    message: "Maximo permitido " + (parseInt(data.result.size) / 1000000).toFixed(3) +
                    "Mb. Su archivo " + (parseInt(data.result.type) / 1000000).toFixed(3) + "Mb.",
                    buttons: {
                        success: {
                            label: "Aceptar",
                            className: "btn-success",
                            callback: function () {
                                // Example.show("great success");
                            }
                        },
                    }
                });
            }
            else {
                var replaceTxt = data.result.name.indexOf(new Date().getFullYear() + "-" + ((new Date().getMonth() + 1) <= 9 ? ("0" + (new Date().getMonth() + 1)) : (new Date().getMonth() + 1)));
                var severRute = data.result.name.substr(0, replaceTxt).replace(new RegExp("-", 'g'), "/");

                var rute = severRute + data.result.name.substr(replaceTxt);
                $(this).parents(".chat-window").find(".emoji-wysiwyg-editor.chat-window-text-box").text("*-*¡¿" + rute);
                SendEnterKeyChat($(this).parents(".chat-window"));
            }
        },
        error: function (e, data) {
            console.error(e.toString());
        }
    }).on('fileuploadprogressall', function (e, data) {
        var progress = parseInt(data.loaded / data.total * 100, 10);
        $('.progress .progress-bar').css('width', progress + '%');
    });
    StyleDragDrop(ids);
}

function StyleDragDrop(ids) {
    $("div[id='" + ids + "']").on(
        'dragover',
        function (e) {
            e.preventDefault();
            e.stopPropagation();
            $("div[id='" + ids + "']").addClass("chatFile");
        }
    ).on(
        'dragleave',
        function (e) {
            e.preventDefault();
            $("div[id='" + ids + "']").removeClass("chatFile");
        }
        ).on(
        'drop',
        function (e) {
            e.preventDefault();
            e.stopPropagation();
            if ($(e.currentTarget).hasClass("chatFile")) {//Porque se producia el evento dos veces
                var zmbHip = e.originalEvent.dataTransfer.getData("ZambaURL");
                if (zmbHip != undefined && zmbHip != "") {
                    var windowChat = $(e.toElement).parents(".chat-window")
                    var $txtB = windowChat.find(".emoji-wysiwyg-editor.chat-window-text-box");
                    windowChat.data("zmbLnk", zmbHip);
                    windowChat.data("zmbLnkDesc", e.originalEvent.dataTransfer.getData("ZambaURLDesc"));

                    SendEnterKeyChat(windowChat);
                }
            }
            $("div[id='" + ids + "']").removeClass("chatFile");
        }
        )
}

function SendEnterKeyChat(windowChat) {
    var e = jQuery.Event("keypress");
    e.which = 13;
    e.keyCode = 13;
    windowChat.find(".emoji-wysiwyg-editor.chat-window-text-box").trigger(e);
    windowChat.find(".chat-window-inner-content").scrollTop(windowChat.find(".chat-window-inner-content")[0].scrollHeight);
}
function AjaxDownloadFile(fileURL) {
    //http://stackoverflow.com/questions/16086162/handle-file-download-from-ajax-post
    //window.location.href = URLBase + "/DownloadFile?file=" + fileURL;
    if (appZChat == "ZambaLink") {
        winFormJSCall.downloadFile(fileURL);
    }
    else {
        window.open(fileURL, '_blank');
    }
}

function ExtImg(ext) {
    var img = ext.substring(ext.lastIndexOf(".") + 1);
    switch (img) {
        case "doc": case "docx": case "docm":
            img = "word.gif";
            break;
        case "xls": case "xlsx": case "cvs": case "xlsm":
            img = "excel.gif";
            break;
        case "ppt":
            img = "ppt.png";
            break;
        case "zip": case "rar": case "7z":
            img = "compress.png";
            break;
        case "jpg": case "jpeg": case "bmp": case "gif": case "png":
            img = "image.png";
            break;
        case "pdf":
            img = "pdf.gif";
            break;
        default:
            img = "default.png"
    }
    return 'url("' + URLImage + 'ChatJs/images/ext/' + img + '")';
}

function changeAvatarDB(img) {
    var id = isCS ? thisExtUserId : thisUserId;
    $.ajax({
        type: "POST",
        async: false,
        url: URLBase + '/changeavatar',
        data: { userId: id, avatar: (RemoveBase64Init(img)) },
        success: function (result) {
            if (isCS) {
                myUserChatInfoCol.Avatar = img;
                myUserInfoExt.Update("Avatar", img);
            }
            else {
                thisChatObj.usersById[thisUserId].Avatar = img;
                myUserChatInfo.Avatar = img;
                myUserInfoChat.Update("Avatar", img);
            }
        }
    });
    $("#refreshAvatarId").click();
}

function ChangeStatusDB(status) {
    var id = isCS ? thisExtUserId : thisUserId;
    $.ajax({
        type: "POST",
        async: false,
        url: URLBase + '/changestatus',//URLBase +
        data: { userId: id, status: status },
        success: function (result) {
        }
    });
}

function EditText(append) {
    $img = $("<img/>").addClass("shareImgChat").appendTo(append);//.addClass('fontButton').addClass('editFont').css("opacity", 1).attr("src", URLImage + fonts + 'image.png')
    $editText = $("<div/>").addClass("editText").appendTo(append);

    $editColor = $("<div/>").addClass("editColor").appendTo(append);

    /*Estilos para Internet Explorer */
    if (navigator.userAgent.match(/Trident.*rv:11\./)) {
        $('.editColor').css({
            display: 'none',
            position: 'relative',
            bottom: '24px',
            'margin-left': '5px',


        });
    }
    //Para color se usa label para tamaño span
    $editColor.empty().addColorPicker({
        clickCallback: function (c) {
            var $contentTxt = $editColor.parent().children(".emoji-wysiwyg-editor.chat-window-text-box");
            if ($editColor.data("color") != undefined && $editColor.data("color") != "") {
                var newVal = $contentTxt.html().replace("<label style='color:" + $editColor.data("color") + "'>", "");
                newVal = newVal.replace('<label style=\"color:', "").replace($editColor.data("color"), "").replace(";", "").replace('">', "");//IE
                newVal = newVal.replace("</label>", "");
                $contentTxt.html(newVal);
            }
            var cTxt = $contentTxt.html() == "" ? "Prueba de texto..." : $contentTxt.html();
            $contentTxt.html(("<label style='color:" + c + "'>" + cTxt + "</label>"));
            $editColor.data("color", c);
            setEndOfContenteditable($contentTxt);
        }
    });

    $b = $("<img/>").addClass('editFont').attr("src", URLImage + fonts + 'b.png').css("opacity", "0.5").appendTo($editText);
    $i = $("<img/>").addClass('editFont').attr("src", URLImage + fonts + 'i.png').css("opacity", "0.5").appendTo($editText);
    $u = $("<img/>").addClass('editFont').attr("src", URLImage + fonts + 'u.png').css("opacity", "0.5").appendTo($editText);
    $s = $("<img/>").addClass('editFont').attr("src", URLImage + fonts + 's.png').css("opacity", "0.5").appendTo($editText);
    $('<span />').text("Tamaño").addClass("sizeTxt").appendTo($editText);
    $tA = $('<select />').attr("id", "SelectFontSize").appendTo($editText);

    var $loc = $("<input/>").attr("type", "file").addClass("addImg").css("height", "0px").css("width", "0px")
        .appendTo($editText).change(function () {
            var validExt = "jpg,jpeg,gif,png,bmp,div";
            $loc.attr("id", "locIMG");
            var file = document.getElementById("locIMG").value;
            var extFile = (file).substring(file.lastIndexOf(".") + 1).toLowerCase();
            var fReader = new FileReader();
            if ($.inArray(extFile, validExt.split(",")) > -1) {
                fReader.readAsDataURL($(this)[0].files[0]);
                fReader.onloadend = function (event) {
                    var img = event.target.result;
                    if (img.length < 1400000) {//Que no pese mas que un mega
                        var image;
                        $.ajax({
                            type: "POST",
                            async: false,
                            data: { img: img },
                            url: URLServer + "Chat/ReduceBase64Img",
                            success: function (d) {
                                image = d.Img;
                            },
                        });

                        var $text = $loc.parent().parent().children(".emoji-wysiwyg-editor.chat-window-text-box");
                        $text.html($text.html() + '<img class="embebedIMG" src="data:image/jpeg;base64,' + image + '"/>');
                        SendEnterKeyChat($loc.parents(".chat-window"));
                    }
                    else {
                        toastrInTitle("Error al subir imagen", "Compruebe que la imagen pese menos que 1Mb");
                    }
                    $loc.val("");
                }
            }
            else
                bootbox.alert("Formato no valido de imagen");
            $loc.attr("id", "");
        });

    for (var i = 8; i < 20; i += 2) {
        if (i == 12)
            $('<option selected value/>').text(i).appendTo($tA);
        else
            $('<option />').text(i).appendTo($tA);
    }

    $tA.on('change', function () {
        var $text = $(this).parent().parent().children(".emoji-wysiwyg-editor.chat-window-text-box");
        var txt = ($text.html() == "") ? "Prueba de tamaño..." : $text.html();

        for (var i = 20; i >= 8; i -= 2) {
            txt = txt.replace('<span style="font-size:' + i + 'px">', '')
            //Probar en ie";"
            txt = txt.replace('<span style=\"font-size:' + i + "px>", "").replace("</span>", "");//IE
            txt = txt.replace("</span>", "");
        }
        txt = txt.replace('</span>', '');
        txt = '<span style="font-size:' + (this.value == "" ? 12 : this.value) + 'px">' + txt + "</span>";
        $text.html(txt);
    });

    $('.editFont, .uploadImgDivChat').click(function (e) {
        StopAllEvents(e);

        var img = $(this).attr("src").substring($(this).attr("src").lastIndexOf("/") + 1);
        var type = img.substring(0, img.lastIndexOf("."));
        var $text = $(this).parent().parent().children(".emoji-wysiwyg-editor.chat-window-text-box");
        if (type == 'image') {
            if ($(this).data("uploadImg")) {
                $(this).data("uploadImg", false);
                $loc.click();
            }
            return;
        }

        $(this).data("active", !$(this).data("active"));
        var active = $(this).data("active");
        if (active) {
            var $span = $text.find("span");
            if ($span.length) {
                var $label = $text.find("label");
                if ($label.length) {
                    var $spanLabel = $label.find("span");
                    if ($spanLabel.length)
                        $spanLabel.html("<" + type + ">" + $spanLabel.html() + "</" + type + ">");
                    else
                        $label.html("<" + type + ">" + $label.html() + "</" + type + ">");
                }
                else {
                    $span.html("<" + type + ">" + $span.html() + "</" + type + ">");
                }
            }
            else {
                $text.html("<" + type + ">" + $text.html() + "</" + type + ">");
            }
        }
        else
            $text.html($text.html().replace("<" + type + ">", "").replace("</" + type + ">", ""));

        $(this).css("opacity", active ? '1' : '0.5');
        setEndOfContenteditable($text);
        //if (window.getSelection().getRangeAt(0).startContainer.parentNode == $text[0])//Para saber si esta seleccionado el textedit
        //{
        //    $text.html($text.html().replace(window.getSelection().toString(), "<" + type + ">" +
        //                window.getSelection().toString() + "</" + type + ">"));           
        //    //$text.html("<b>" + $text.text() + "</b>");
        //}
    });
}

function ContextMenu(userList) {
    $(userList).bind("contextmenu", function (event) {
        event.preventDefault();
        var left = (event.pageX + 150) >= $(window).innerWidth() ? ($(window).innerWidth() - 150) : event.pageX;
        var top = (event.pageY + 100) >= $(window).innerHeight() ? ($(window).innerHeight() - 100) : event.pageY;
        $(".custom-menu").toggle(100).
            css({
                top: top + "px",
                left: left + "px"
            });
        //setTimeout(function () {
        //    $(".custom-menu").hide('blind', {}, 300)
        //}, 3000);
    });

    $(document).bind("mousedown", function (e) {
        var $cm = $(".custom-menu");
        var $uli;
        if ($(e.target).hasClass("user-list-item"))
            $uli = $(e.target);
        else
            $uli = $(e.target).parents(".user-list-item");
        var divId = $uli.attr("id");
        var chatId = $uli.attr("chatid");
        //if ($cm.css("display") == "block") $cm.css("display", "none");
        if (divId == undefined)
            return;
        $cm.data({ "chatId": "", "idUser": "" });
        if (divId.substring(0, 5) == 'list-' || (chatId != undefined && chatId > 0)) {

            if (chatId != undefined && chatId > 0)
                $cm.data("chatId", chatId).hide(100);
            else
                $cm.data("idUser", divId).hide(100);
        }
    });

    // If menu element is clicked
    $(".custom-menu li").click(function (e) {
        StopAllEvents(e);
        // This is the triggered action name
        var userLst = $(this).parent().data("idUser");
        var chatId = $(this).parent().data("chatId");
        if (userLst == undefined && chatId == undefined) return;
        var userId = userLst.replace("list-", '');

        switch ($(this).attr("data-action")) {
            case "info":
                ChatUsersInfo(userId, chatId);
                break;
            case "mail":
                var user = [];
                if (userId != "" && !isNaN(userId))
                    user = UserFromWS(userId);
                else
                    user = UserFromWS(chatId);

                if (user != undefined && user.length >= 1) {
                    var mail = '';
                    for (var u in user) {
                        if (user[u].ID != thisUserId && user[u].CORREO != undefined && user[u].CORREO != '')
                            mail += user[u].CORREO + ';';
                    }

                    if (mail != '')
                        window.location.href = 'mailto:' + mail;
                    else
                        bootbox.alert("Dirección de mail no disponible").css("z-index", 99999999999999);
                }
                else
                    bootbox.alert("Información de usuario no disponible").css("z-index", 99999999999999);
                break;

            case "msg":
                if (userId > 0)
                    $(".user-list-item[id='" + userLst + "']").click();
                else
                    $(".user-list-item[chatid='" + chatId + "']").click();
                break;
            case "close": $(".custom-menu").hide(100); break;
        }
        $(".custom-menu").hide(100);
    });
}
function LeaveChatGroup(chatid, e) {
    var $this = $(this);
    e.preventDefault();
    var html = "<button type='button' result='confirm' class='btn btn-success btn-xs' id='leaveChatGroupBtns'>Aceptar" +
        "</button><button type='button' result='cancel' class='btn btn-default btn-xs'id='leaveChatGroupBtns'>Cancelar</button>";
    var t = toastr.info(html, '¿Desea salir del grupo?',
        {
            "closeButton": true,
            allowHtml: true,
            timeOut: 50000,
            positionClass: "toast-bottom-right",
            tapToDismiss: false,
            extendedTimeOut: 100000,
            onShown: function (toast) {
            },
            onTap: function () {
            }
        }).css("width", widthCompact + "px");
    //$(t).parent().css("top", heightCompact + "px");
    $(t).parent().css({ "padding-bottom": "200px", "margin-right": "-15px" });
    $('body').on('click', '#leaveChatGroupBtns', function (e) {
        var btn = $(e.target);
        if (btn.attr("result") == "confirm") {
            var $winChat = $(".chat-window[chatid=" + chatid + "]")
            if ($winChat.length) {
                $winChat.find(".emoji-wysiwyg-editor.chat-window-text-box").text("<zmbChat:small>" + TempUserInfo(thisUserId).Name.split("/")[0] + " ha abandonado el grupo");
                SendEnterKeyChat($winChat);
            }
            $.ajax({
                type: "POST",
                async: false,
                url: URLServer + "Chat/LeaveChatGroup",
                data: { chatId: chatid, userId: thisUserId },
                success: function (d) {
                    // updateListsInGrpChat();
                },
                error: function (a, b, c) {
                    var foo = a;
                },
            });
            chatGroups.Remove(chatid);
            $("body").find(".chat-window[chatid='" + chatid + "']").find(".close").click();
            $("body").find(".user-list-item[chatid='" + chatid + "']").remove();
            $(e.target).parents(".toast.toast-info").children(".toast-close-button").click();
        }
        else {
            btn.parents(".toast.toast-info").children(".toast-close-button").click();
        }
        //function updateListsInGrpChat() {
        //    //actualizo lista
        //    // $(e.target).parents(".chat-window-title").children(".close").click();
        //    // $(".chat-window-inner-content").find("[chatid='" + $(e.target).attr("chatid") + "']").remove();
        //    $("body").find(".chat-window[chatid='" + chatid + "']").find(".close").click();
        //    $("body").find(".user-list-item[chatid='" + chatid + "']").remove();
        //    $(e.target).parents(".toast.toast-info").children(".toast-close-button").click();
        //}
    });
}

function UserFromWS(userId) {
    var users = [];
    if (typeof (userId) == "string" || typeof (userId) == "number") {
        var id = userId;
        userId = [];
        userId.push(id);
    }

    //var ajaxR = [];
    //for (var i = 0; i <= userId.length -1; i++) {
    //    var usId = userId[i];
    //    ajaxR.push($.ajax({
    //        type: "GET",
    //        async: true,
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        url: URLServer + "chat/GetUsersZambaInfo",
    //        data: { id: usId }   
    //    }));
    //}
    // $.when.apply($, ajaxR).then(function (e) {
    //     var objects = arguments;       
    //    return null;
    //});

    for (var us in userId) {
        $.ajax({
            type: "GET",
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: URLServer + "chat/GetUsersZambaInfo",
            data: { id: userId[us] },//userId
            success: function (d) {
                var data = d.length == undefined ? d.d : d;
                if (data == "null") return;
                users.push(data[0]);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                var error = (xhr + ajaxOptions + thrownError);
                //bootbox.alert("Información de usuario no disponible");         
            }
        });
    }
    return users;
}

function bootboxUserInfo(user) {
    if (user == undefined || user.length == 0)
        bootbox.alert("Información de usuario no disponible").css("z-index", 99999999999999);
    else {
        var content = '';
        for (var u in user) {
            var mail = user[u].CORREO == null ? '' : user[u].CORREO;
            var photo = '';
            if (user[u].FOTO != null && user[u].FOTO != '') {
                photo = PathToBase64(user[u].FOTO);
            }
            photo = (photo == '') ? '' : ('<img src= "data:image/jpeg;base64,' + photo + '" height="100" width="100">');
            content += '<label >Usuario</label> <span> ' + user[u].NAME + '</span></br>' +
                '<label >Nombre</label> <span> ' + user[u].NOMBRES + '</span></br>' +
                '<label >Apellido</label> <span> ' + user[u].APELLIDO + '</span> </br>' +
                '<label >Mail</label> <a href ="mailto:' + mail + '"> ' + mail + '</a> </br>' +
                '<label >Puesto</label> <span> ' + user[u].PUESTO + '</span> </br>' +
                '<label >Teléfono</label> <span> ' + user[u].TELEFONO + '</span>  </br> ' +
                photo + '<hr>';
        }
        var btx = bootbox.dialog({
            title: "Información de Contacto",
            message:
            '<div class="row"> <div class="col-md-12"> <form class="form-horizontal"> ' +
            content + '</form> </div>  </div>',
            buttons: {
                success: {
                    label: "Aceptar",
                    className: "btn-success btn-xs",
                    callback: function () {
                    }
                }
            }
        }
        );
        btx.css("z-index", 99999999999999);
        if (appZChat == "ZambaLink")
            btx.find(".modal-footer").css({ "padding": "5px", "margin": "0" });
    }
}
function IncreaseNoReadMsg() {
    //Cuando estoy hablando con otra persona que me avise que me llego msg
    var $noReadMsgG = $(".msgCount.noReadMsg");
    $noReadMsgG.text((parseInt($noReadMsgG.text()) || 0) + 1).fadeIn();
}

function RemoveBase64Init(b) {
    var validExt = "jpg,jpeg,gif,png,bmp,div".split(",");
    for (var i = 0; i <= validExt.length - 1; i++)
        b = b.replace("data:image/" + validExt[i] + ";base64,", "");
    return b;
}

function OrderUsrLstByDate() {
    var usrLst = [];
    $.each($(".user-list-item:not(#list-" + thisUserId + ")"), function (index, value) {
        usrLst.push({ "id": $(value).attr("id"), "date": new Date($(value).attr("date")) });
    });
    usrLst.sort(function (a, b) {
        return new Date(b.date) - new Date(a.date);
    });
    for (var i = 1; i <= usrLst.length - 1; i++)
        $(document.getElementById(usrLst[i].id)).insertBefore($(document.getElementById(usrLst[0].id)));
    if (usrLst.length > 1)
        $(document.getElementById(usrLst[0].id)).insertBefore($(document.getElementById(usrLst[1].id)));
}

function CapitalizeString(string) {
    var arrayWords;
    var returnString = "";
    var len;
    arrayWords = string.split(" ");
    len = arrayWords.length;
    for (var indexCap = 0; indexCap < len; indexCap++) {
        if (indexCap != (len - 1)) {
            returnString = returnString + ucFirst(arrayWords[indexCap]) + " ";
        }
        else {
            returnString = returnString + ucFirst(arrayWords[indexCap]);
        }
    }
    if (returnString.length >= 18) returnString = (returnString.substring(0, 18) + "...");
    return returnString;
}
function ucFirst(string) {
    return string.substr(0, 1).toUpperCase() + string.substr(1, string.length).toLowerCase();
}

function playBuzzSound() {
    var audioElement = document.createElement('audio');
    audioElement.setAttribute('src', thisDomain + '/ChatJs/Sounds/buzz.mp3');
    audioElement.setAttribute('autoplay', 'autoplay');
    //audioElement.load()
    $.get();
    audioElement.addEventListener("load", function () {
        audioElement.play();
    }, true);
}

function getBorderFromStatus(status) {
    var colorRound;
    switch (status) {
        case 0:
            colorRound = "#b7b7b8";
            break;
        case 1:
            colorRound = "rgb(84, 179, 0)";
            break;
        case 2:
            colorRound = "rgba(247, 127, 127, 0.76)";
            break;
    }
    return ({
        "border": ("2px solid " + colorRound),
    });
}

function expandImg($img) {

    if ($img.attr("id") != "mainAvatarId")
        $img.addClass("embebedIMG");
    //$img.css({
    //    "-webkit-transition": "all .4s ease-in-out",
    //    "-moz-transition": "all .4s ease-in-out",
    //    "-o-transition": "all .4s ease-in-out",
    //    "-ms-transition": "all .4s ease-in-out",

    //});
    //$($img).hover(function () {
    //    $(this).data('timeout', window.setTimeout(function () {
    //        $($img).addClass('transitionImg');
    //    }, 100));

    //}, function () {
    //    clearTimeout($(this).data('timeout'));
    //    $($img).removeClass('transitionImg');
    //});
}

function InfoUsersGroupChat(e) {
    var $span = $(e.target);
    var sp = $span.parent();
    var cun = sp.children("#containerUserName");
    $span.data("showing", $span.data("showing") == "properties" ? "groupname" : "properties");
    var imgUrl = $(e.target).css("background-image");
    if ($span.data("showing") == "properties") {
        imgUrl = imgUrl.replace("/info.png", "/delete.png");
        cun.css({ "display": "inline-flex", position: "absolute", top: "11px" });
        cun.children().fadeToggle();
        cun.children(".confirmChat, .lastUserConnChat").css("display", "none");
        sp.children("#containerUsersGroup").css("display", "none").removeAttr('readonly');
        sp.children(".addUser, .showAllUsers").fadeToggle();
        sp.children(".showAllUsers").attr("modeDisplay", "none");
        sp.children(".uploadFile, .shareChat").hide();
        sp.css("padding-bottom", "10px");
        if (cun[0].childNodes[0].textContent != "")
            cun.attr("name", cun[0].childNodes[0].textContent);
        cun[0].childNodes[0].textContent = "";
    }
    else {
        imgUrl = imgUrl.replace("/delete.png", "/info.png");
        sp.children(".addUser, .addUserTxt, .showAllUsers").css("display", "none");
        cun.css("display", "none");
        cun.children().css("display", "none");
        sp.children("#containerUsersGroup").fadeToggle().attr("readonly", "readonly");
        sp.children(".uploadFile").fadeIn("slow");
        $(".remFilterImg").hide();
    }
    $(e.target).css("background-image", imgUrl);
}

function ChangeNameGroupChat(e) {
    var $span = $(e.target);
    $input = $span.parents(".chat-window-title").find("#containerUsersGroup");
    $input.fadeIn().data("name", $input.val()).css({
        "background-color": "white",
        color: "black",
        width: "65%",
        height: "25px",
        top: "0px"
    });

    $span.parents(".chat-window-title").find("#containerUserName, #infoUsersGroup, .addUserTxt, .showAllUsers, .addUser, .uploadFile").css("display", "none");
    $span.parents(".chat-window-title").children(".confirmChat").css("display", "inline");
}
function ChangeNameGroupCancel(e) {
    var span = $(e.target);
    var input = span.parent().find("#containerUsersGroup");
    var name = input.data("name");
    if (name != undefined) {
        input.val(name);
        input.data("name", "");
    }
    RestoreNameGroupChat(span);
}
function ChangeNameGroupOk(chatId, e) {
    var $span = $(e.target);
    var groupName = $span.parent().children("#containerUsersGroup").val();
    $("#changeGroupNameDiv").data({ "chatId": chatId, "name": groupName }).click();
    var $lstGroup = $(".chat-window#" + thisUserId).find(".chat-window-inner-content").find("[chatid=" + chatId + "]");
    if ($lstGroup.length)
        $lstGroup.children(".txtNameGroupChat").text(groupName);
    RestoreNameGroupChat($span);
}

function RestoreNameGroupChat(span) {
    var $spanP = span.parent();
    $spanP.children(".confirmChat").css("display", "none");
    $spanP.children("#containerUsersGroup").css({ "background-color": "rgba(255, 255, 255, 0)", width: "35%", color: "white" }).attr("readonly", "readonly");
    $spanP.children("#infoUsersGroup").data("showing", "groupname").css("display", "inline");
    $spanP.children(".uploadFile").show();
}

$(document).on('click', ".embebedIMG", function (e) {
    e.preventDefault();
    e.stopPropagation();

    var imgClass = (IsZambaLinkRestore() ? "previewEmbebedIMGZL" : "previewEmbebedIMG");
    var bb = bootbox.dialog({
        title: "Previsualización",
        message: '<div class="row">  ' +
        '<div class="col-md-12"> ' +
        '<form class="form-horizontal"> ' +
        '<div class="form-group"> ' +
        '<img class="' + imgClass +
        '" src=' + $(this).attr("src") + '> </div> </form> </div> </div>'
    }
    );
    bb.css("z-index", 1000000000);
});

$(document).on('focusout', ".emoji-wysiwyg-editor.chat-window-text-box", function () {
    var element = $(this);
    if (!element.text().replace(" ", "").length)
        element.empty();
});

Date.prototype.addHours = function (h) {
    this.setHours(this.getHours() + h);
    return this;
}

function ShakeWindow(div) {
    if (appZChat == "ZambaLink") {
        winFormJSCall.action("shake");
    }
    else {
        var l = 20;
        for (var i = 0; i < 26; i++)
            $(div).animate({
                'margin-left': "+=" + (l = -l) + 'px',
                'margin-right': "-=" + l + 'px'
            }, 40);
    }
}

function wait(ms) {
    var start = new Date().getTime();
    var end = start;
    while (end < start + ms) {
        end = new Date().getTime();
    }
}

function toastrInTitle(title, msg) {
    title = title.split("/")[0];
    if (appZChat == "ZambaLink") {
        var winState;
        if (typeof (winFormJSCall) != "undefined") {
            var sMProm = winFormJSCall.showMessage(title, msg.replace("/", " "));
            //Si la ventana de win esta minimizado se muestra con notifyIcon sino dentro de la ventana
            sMProm.then(function (ws) {
                if (ws != "Minimized") {
                    toastrInTitleFn(title, msg);
                }
            });
        }
        else
            toastrInTitleFn(title, msg);
    }
    else {
        toastrInTitleFn(title, msg);
    }
}

function toastrInTitleFn(title, msg) {
    var position = (appZChat == "ZambaLink") ? "toast-up-left" : "toast-bottom-right";
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": position,
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "150",
        "hideDuration": "800",
        "timeOut": "2000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };
    toastr.info(msg == undefined ? "" : msg.replace("/", " "), title).css({ "width": widthCompact + "px", "font-size": "12px" });
    if (!(appZChat == "ZambaLink")) {
        $("#toast-container").css({ "top": (heightCompact + 60) + "px", height: "50px" });
    }
    else {
        $("#toast-container").css({ "padding-top": "30px" });
    }
}

function MainWinChat() {
    return ($(".chat-window#" + thisUserId));
}
String.prototype.replaceAll = function (search, replacement) {
    var target = this;
    return target.replace(new RegExp(search, 'g'), replacement);
};


function GetChatUser(id, external) {
    var user;
    var url = (typeof (isExternal) != "undefined" && (isExternal || isCS)) || external ? zCollServer + "chat" : URLBase;
    $.ajax({
        type: "GET",
        async: false,
        url: url + '/getuserinfo',
        data: { userId: id },
        success: function (d) {
            user = d.User;
        },
        error: function (a, b, c) {
            var d = a;
        }
    });
    return user;
}
function isDate(date) {
    return date instanceof Date && !isNaN(date.valueOf())
}

function GetChatUserCollaboration(mail) {
    var user;
    $.ajax({
        type: "GET",
        async: false,
        url: zCollServer + 'restcollaboration/GetMyUserInfo',
        data: { mail: mail },
        success: function (d) {
            user = d.User;
        },
    });
    return user;
}

$.LogOut = function () {// grabo offline y hora de ultimo acceso 
    var userId;
    var url;
    if (isCS) {
        userId = thisExtUserId;
        url = zCollServer + "chat";
    }
    else {
        userId = thisUserId;
        url = URLBase;
    }
    $.ajax({
        type: "POST",
        async: false,
        url: url + '/leavechat',
        data: { userId: userId },
        cache: false,
        success: function (result) {
            console.log(result);
        }
    });
};

function AddRemoveUsersGroups(id, user, add) {
    var state;
    $.ajax({
        type: "GET",
        async: false,
        url: URLBase + '/AddRemoveUsersGroups',
        data: { chatId: id, userId: user, add: add },
        success: function (d) {
            state = d;
        },
    });
    return state;
}

function GetUsersByChat(id) {
    var users;
    $.ajax({
        type: "GET",
        async: false,//No puedo devolver promise
        url: URLBase + '/GetUsersByChat',
        data: { chatId: id.replace("chatid", "") },
        success: function (d) {
            users = d.Users;
        },
    });
    return users;
}

function GetAllUsersChat() {
    var users;
    $.ajax({
        type: "GET",
        async: false,
        url: URLBase + '/GetAllUsersChat',
        success: function (d) {
            users = d.User;
        },
    });
    return users;
}

function RefreshChatPage() {
    location.reload();
}

function PathToBase64(img) {
    var photo;
    $.ajax({
        type: "POST",
        async: false,
        url: URLServer + "Chat/PathToBase64",
        data: { path: img },
        success: function (d) {
            photo = d;
        },
    });
    return photo;
}

function GetMoreMsgHistory(usersId, chatId, cant) {
    var res;
    $.ajax({
        type: "GET",
        async: false,
        url: URLBase + '/getmoremsghistory',
        data: {
            usersId: usersId,
            chatId: chatId,
            cant: cant
        },
        success: function (result) {
            res = (result);
        },
    });
    return res;
}

function ClearChatContentList() {
    var elem = "#clearOldMsg .chat-window-content .chat-window-inner-content ";
    $(elem + ".chat-message, " + elem + ".chat-date, " + elem + ".spanUserName, " + elem + "br").each(function (indice, elemento) {
        $(elemento).remove();
    });
}

function StopAllEvents(e) {
    e.preventDefault();
    e.stopPropagation();
    e.stopImmediatePropagation();
}

function validateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

function GenerateYoutubeEmbed(txt) {
    var embedYout = "https://www.youtube.com/embed/";
    var f = txt.substring(0, txt.indexOf(youtubePath));
    var fp = f.substring(0, f.lastIndexOf(" ")) + '<iframe width="160" height="120" src="' + embedYout
    var l = txt.substring(txt.indexOf(youtubePath)).replace(youtubePath, "");
    var yout = l.indexOf(" ") == -1 ? l : l.substring(0, l.indexOf(" "));
    var lp = l.indexOf(" ") == -1 ? "" : l.substring(l.indexOf(" "));
    var ahref = '<a  target="_blank" href="http://www.' + youtubePath + yout + '"> Abrir nueva pestaña</a>';
    var embed = fp + yout + '"></iframe>' + ahref + lp;
    return embed;
}

function ChangeChatUserName() {
    var $span = $(".mainTitle");
    var btx = bootbox.dialog({
        title: "Cambiar Nombre - Estado",
        message: '<div class="row">  ' +
        '<div class="col-md-12"> ' +
        '<form class="form-horizontal"> ' +
        '<div class="form-group"> ' +
        '<label class="col-md-4 control-label" for="name">Nombre - Nick</label> ' +
        '<div class="col-md-4"> ' +
        '<input id="bootChangeName" name="name"  maxlength="15" type="text" value="' + $span.text().trim() + '" class="form-control input-md"> ' +
        '</div> </div> ' +
        '<label class="col-md-4 control-label" for="name">Estado - Descripción</label> ' +
        '<div class="col-md-4"> ' +
        '<input id="bootChangeStatus" name="status" type="text"  maxlength="18" value="' + $(".subName").text() + '" class="form-control input-md"> ' +
        '</div> </div> </form> </div>  </div>',
        buttons: {
            success: {
                label: "Guardar",
                className: "btn-success",
                callback: function () {
                    var name = $('#bootChangeName').val();
                    var status = $('#bootChangeStatus').val();
                    if ((name + status).indexOf("/") >= 0 || name.length <= 3)
                        bootbox.alert('Nombre invalido');
                    else {
                        var newName = name + "/" + status;
                        myUserInfoChat.Update("Name", newName);
                        $("#changeNameDiv").data("name", newName).click();
                        $span.text(' ' + name);
                        $(".subName").text(status != '' ? status : ' ');
                    }
                }
            }
        }
    }
    );
    btx.css("z-index", "99999999999999");
}


var tempUserInfoExt = [];
function TempUserInfo(userId, external) {
    var user;
    if (external) {
        if (userId == thisExtUserId) return myUserInfoExt.Get();
        for (var i = 0; i <= tempUserInfoExt.length - 1; i++) {
            if (tempUserInfoExt[i].Id == userId) {
                user = tempUserInfoExt[i];
                break;
            }
        }
        if (user == null) {
            user = GetChatUser(userId, true);
            tempUserInfoExt.push(user);
        }
    }
    else {
        if (userId == thisUserId) return myUserInfoChat.Get();
        if (thisChatObj.usersById[userId] != undefined) return thisChatObj.usersById[userId];
        user = GetChatUser(userId, false);
        thisChatObj.usersById[userId] = user;
    }
    return user;
}

function AddTempUserInfo(userInfo, external) {
    if (external)
        tempUserInfoExt.push(userInfo);
}

function CreateChatUser(u) {
    var id = u.ID;
    var name = u.NOMBRES;
    var mail = u.CORREO;
    var result = 0;
    $.ajax({
        type: "POST",
        async: false,
        url: URLBase + '/CreateChatUser',
        data: { id: id, name: name, mail: mail },
        success: function (d) {
            result = d;
        }
    });
    return result;
}

function setEndOfContenteditable(contentEditableElement) {
    contentEditableElement = contentEditableElement[0];
    var range, selection;
    if (document.createRange)//Firefox, Chrome, Opera, Safari, IE 9+
    {
        range = document.createRange();//Create a range (a range is a like the selection but invisible)
        range.selectNodeContents(contentEditableElement);//Select the entire contents of the element with the range
        range.collapse(false);//collapse the range to the end point. false means collapse to end rather than the start
        selection = window.getSelection();//get the selection object (allows you to change selection)
        selection.removeAllRanges();//remove any selections already made
        selection.addRange(range);//make the range you have just created the visible selection
    }
    else if (document.selection)//IE 8 and lower
    {
        range = document.body.createTextRange();//Create a range (a range is a like the selection but invisible)
        range.moveToElementText(contentEditableElement);//Select the entire contents of the element with the range
        range.collapse(false);//collapse the range to the end point. false means collapse to end rather than the start
        range.select();//Select the range (make it the visible selection
    }
    contentEditableElement.focus();
}

function ChatUsersInfo(userId, chatId) {
    if (userId != "" && !isNaN(userId))
        bootboxUserInfo(UserFromWS(userId.split(",")));
    else {
        var users = [];
        var usersInfo = GetUsersByChat(chatId);
        for (var i = 0; i <= usersInfo.length - 1; i++) {
            if (usersInfo[i].Id != thisUserId)
                users.push(usersInfo[i].Id)
        }
        bootboxUserInfo(UserFromWS(users));
    }
}

window.chatMsj = function (txt) {
    if ($('#loadingModZmb').hasClass('in')) window.closeChatMsj();
    waitingDialog.show(txt);
}

window.closeChatMsj = function () {
    waitingDialog.hide();
    var $m = $('div#loadingModZmb');
    $(".modal-backdrop.fade.in").hide();
    if ($m.hasClass('in')) {
        $m.removeClass("in").hide();
        $(".modal-backdrop").hide();
    }
}

function toDataUrl(url, callback) {
    //Image to base64
    var xhr = new XMLHttpRequest();
    xhr.onload = function () {
        var reader = new FileReader();
        reader.onloadend = function () {
            callback(reader.result);
        }
        reader.readAsDataURL(xhr.response);
    };
    xhr.open('GET', url);
    xhr.responseType = 'blob';
    xhr.send();
}

function CheckCorrectUserInfo() {
    if (localStorage.userId != thisUserId && !(localStorage.isdebbug === "true")) {
        localStorage.clear();
        return false;
    }
    else
        return true;
}