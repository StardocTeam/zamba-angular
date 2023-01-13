var Chat = {};
var taskChat = false;
function LoadChat(thisUserIdChat, URLServer, chatModeOpen, appZC, thisDomain, ZCollLnk, zIsCollserver, zCollServer, taskChat) {
    // return;
    if (typeof jQuery == 'undefined')
        $('head').append('<script src="/Scripts/jquery-2.2.2.min.js""></script>');
    if (typeof (DebbugFN) !== "undefined" && DebbugFN()) {
        var DG = Debbug.GetURLS();
        thisUserIdChat = DG.thisUserIdChat;
        URLServer = DG.URLServer;
        thisDomain = DG.thisDomain;
        ZCollLnk = DG.zCollLnk;
        zIsCollserver = DG.isCS;
        zCollServer = DG.colServer;
    }
    thisDomain = thisDomain.substring(thisDomain.length - 1) == "/" ? thisDomain : thisDomain + "/";
    referenceFiles = thisDomain + "ChatJs";
    referenceFilesJS = referenceFiles + "/Scripts";
    jsLnkEnd = 'type="text/javascript"></script>';
    zIsCollserver = zIsCollserver != undefined && zIsCollserver == "true";
    cn = typeof (conString) === "undefined" ? "" : conString;
    $('head').append('<script src="' + referenceFilesJS + '/zambachat.init.js"' + jsLnkEnd);
    Chat.Urls = ("\n\nthisUserIdChat: " + thisUserIdChat + "\nURLServer: " + URLServer + "\nchatModeOpen: " + chatModeOpen +
        "\nappZC: " + appZC + "\nthisDomain: " + thisDomain + "\nZCollLnk: " + ZCollLnk +
        "\nzIsCollserver: " + zIsCollserver + "\nzCollServer: " + zCollServer + "\nWebConfig: " + cn + "\n\n\n");
    window.onbeforeunload = confirmExit;
    function confirmExit() {
        $.LogOut();
    }

    AwaitInitChatJS(function () {
        ZambaChatInit(thisUserIdChat, URLServer, chatModeOpen, appZC, thisDomain, ZCollLnk, zIsCollserver, zCollServer, taskChat);
    });
    function AwaitInitChatJS(c) {
        if (typeof ZambaChatInit == "undefined") {
            setTimeout(function () {
                AwaitInitChatJS(c);
            }, 100); // wait 100 ms
        } else { c(); }
    }
}

(function ($) {
    function ChatContainer(options) {
        /// <summary>This is a window container, responsible for hosting both the users list and the chat window </summary>
        /// <param FullName="options" type=""></param>

        this.defaults = {
            objectType: null,
            objectName: null,
            title: null,
            canClose: true,
            showTextBox: true,
            initialToggleState: "maximized",
            onCreated: function (chatContainer) { },
            onClose: function (chatContainer) { },
            // triggers when the window changes it's state: minimized or maximized
            onToggleStateChanged: function (currentState) { }
        };

        //Extending options:
        this.opts = $.extend({}, this.defaults, options);

        //Privates:
        this.$el = null;
        this.$window = null;
        this.$windowTitle = null;
        this.$windowContent = null;
        this.$windowInnerContent = null;
        this.$textBox = null;
    }

    // Separate functionality from object creation
    ChatContainer.prototype = {
        init: function () {
            var _this = this;
            // container
            _this.$window = $("<div/>").addClass("chat-window").appendTo($("body")).css({ "position": "absolute" });
            if (chatMode == "compact" && appZChat != "ZambaLink") {
                _this.$window.draggable({ cancel: ".chat-window-content, .chat-window-text-box,.emoji-wysiwyg-editor, #containerUsersGroup, select, input" });
            }
            else {
                //Que no se vea si hay mas de 4 ventanas
                if ($._chatContainers != undefined && $._chatContainers.length >= 5)
                    _this.$window.css("display", "none").css("right", "0px");
            }

            _this.$windowTitle = $("<div/>").addClass("chat-window-title").appendTo(_this.$window);
            //cuando se clickea en titulo que pueda escribir mensaje
            _this.$windowTitle.click(function (e) {
                if ($(e.target).attr("class") != "addUserTxt") {
                    var $winChat = $(_this.$window);

                    if ($winChat.css("display") == "none") {
                        //Posiciono reemplazando ventana
                        $winChat.css("display", "block").css("right", "730px").css("left", "").css("bottom", "");

                        for (var i = 0; i <= $._chatContainers.length - 1; i++) {
                            if ($._chatContainers[i].$window.css("right") == "730px")//se agrego .$window
                                $._chatContainers[i].$window.css("display", "none");
                        }
                    }
                    $winChat.children(".chat-window-text-box-wrapper").children().select().focus();
                }
            });

            if (_this.opts.canClose) {
                var $closeButton = $("<div/>").addClass("close").attr("data-placement", "bottom").attr("title", "Volver").appendTo(_this.$windowTitle).tooltip();
                if (chatMode == "compact") {
                    $closeButton.css({ float: "left", "background-image": 'url("' + URLImage + 'ChatJs/images/back.png")', "margin-right": "13px", "margin-left": "-3px", "background-repeat": "no-repeat" });
                    var $msgCount = $("<div/>").addClass("msgCount").addClass("noReadMsg").attr("title", "Mensajes sin leer").appendTo(_this.$windowTitle).tooltip();
                }
                if (appZChat == "ZambaLink")
                    _this.$window.on("contextmenu", function (evt) { evt.preventDefault(); });
            }
            if (appZChat != "ZambaLink") {
                var $minimizedButton = $("<div/>").addClass("minimized")
                    .attr("data-placement", "bottom").appendTo(_this.$windowTitle);//.attr("title", "Minimizar/Restaurar").tooltip()
                if (_this.opts.initialToggleState == 'minimized') {
                    $minimizedButton.css('background-image', 'url("' + URLImage + 'ChatJs/images/restore.png")');
                }
                $minimizedButton.click(function () {  //maximiza minimiza antes estaba debajo de  if (_this.opts.canClose) {  
                    if (appZChat == "ZambaLink") {
                        winFormJSCall.action("minimize");
                        return;
                    }
                    _this.$windowContent.toggle();

                    if (chatMode == "compact") {
                        _this.$window.height((_this.$window.height() == heightCompact || _this.$window.css("height") == "400px") ? 38 : heightCompact);
                    }
                    //Reestablezco color azul.
                    _this.$windowTitle.css("background-color", "#636262");
                    if (_this.$windowContent.is(":visible") && _this.opts.showTextBox)
                        _this.$textBox.focus();
                    var doMinimize = !(_this.$windowContent.is(":visible"));
                    _this.opts.onToggleStateChanged(doMinimize ? "maximized" : "minimized");

                    $(_this.$windowTitle.children(".minimized")).css("background-image", _this.$windowContent.is(":visible") ?
                        "url(" + URLImage + "ChatJs/Images/minimize.png)" : "url(" + URLImage + "ChatJs/Images/restore.png)");

                    if (doMinimize) {
                        _this.$window.css({ "top": "auto", "bottom": "", "left": "auto", "right": rightOffset + "px" });
                    }

                    if (_this.$window.attr("groupWin") == "restore" && $._chatContainers.length >= 5) {
                        var flag = false;
                        $("#showMoreWindows").children().text(($._chatContainers.length - 4) + (($._chatContainers.length - 4 == 1) ? " Conversación" : " Conversaciones"));
                        for (var i = 0; i <= $._chatContainers.length - 1; i++) {

                            if (!flag && $._chatContainers[i].$window.css("right").indexOf("730") != -1) {
                                flag = true;
                                var $lastWin = $._chatContainers[i];
                                $lastWin.$window.css("display", "none").css("right", "0px").attr("groupWin", "hidden");
                                var $newWin = _this;
                                $newWin.$window.css("display", "block").css("right", "730px").css("left", "").css("bottom", "");
                                _this.$window.removeAttr("groupwin");
                            }
                            if ($._chatContainers[i].$window.attr("groupWin") == "restore") {
                                $._chatContainers[i].$window.attr("groupWin", "hidden").css({ "display": "none", "right": "0px" });
                            }
                        }
                    }
                });
            }
            if (!_this.opts.canClose) {
                if (chatMode == "normal") {
                    _this.$window.mouseenter(function (e) {//mouseover
                        StopAllEvents(e);
                        if ($(".custom-menu").css("display") != "block") {
                            //  if (_this.$window.css("width")=='230px')
                            HideContentChat(_this, "block");
                            _this.$window.css({ "width": "230px", "opacity": "1", "right": "10px" });
                        }
                    })
                        .mouseleave(function (e) {//mouseout
                            StopAllEvents(e);
                            if (!$("#pinId").data("pin")) {
                                if ($(".custom-menu").css("display") != "block") {
                                    _this.$window.css({ "width": "60px", "opacity": "0.5", "right": "0px" });
                                    HideContentChat(_this, "none");
                                }
                            }
                        });

                    function HideContentChat(win, show) {
                        $(win.$windowInnerContent.children("#Filtro")).css("display", show);
                        var list = win.$windowInnerContent.children(".user-list-item");
                        for (var i = 0; i <= list.length - 1; i++) {
                            for (var item = 0; item <= $(list[i]).children().length - 1; item++) {
                                if ($($(list[i]).children()[item]).attr("class") != 'profile-picture')
                                    $($(list[i]).children()[item]).css("display", show);
                            }
                        }
                        var $title = win.$windowTitle;
                        for (var i = 0; i <= $title.children().length - 1; i++) {
                            if ($($title.children()[i]).attr("id") != 'containerUserName')
                                $($title.children()[i]).css("display", show);
                        }
                    }
                }
                if (!isCS) {
                    CreateCollaborationIcon(_this.$windowTitle);

                    var $createGroup = $("<div/>").addClass("addGroup")
                        .attr("title", "Crear grupo")
                        .attr("data-placement", "bottom")
                        .appendTo(_this.$windowTitle).tooltip()
                        .click(function () {
                            createEmptyGroup($(this));
                        });
                }



                if (taskChat) {
                    var $CreatTopicButton = $("<button/>").addClass("btn btn-default btnCreateTopic").text("Crear Topic").attr("data-toggle", "modal").attr("data-target", "#myModal").attr("title", "Cancelar").attr("data-placement", "bottom").appendTo(".chat-window-title")
                    _this.$window.on("contextmenu", function (evt) { evt.preventDefault(); });
                    $CreatTopicButton.css
                        ({
                            width: "90px",
                            height: "30px",
                            position: "relative",
                            top: "2px",
                            left: "11px"
                        });
                }



                var $configButton = $("<div/>").addClass("config").attr("id", "configId")
                    .attr("data-placement", "bottom").attr("title", "Configurar").appendTo(_this.$windowTitle).tooltip()
                    .click(function () {
                        var iconStr = $(this).css("background-image");
                        if ($("#statusMenuId").length) {
                            $("#statusMenuId").remove();
                            iconStr = iconStr.replace("/delete.png", "/settings.png");
                            if (chatMode == "compact") {
                                var $innerCont = $(this).parents(".chat-window").find(".chat-window-inner-content:last");
                                var newHeigth = $innerCont.height() + 60;
                                $innerCont.height(newHeigth);
                                $innerCont.css({ "max-height": newHeigth + "px", "height": newHeigth + "px" });
                            }
                        }
                        else {
                            iconStr = iconStr.replace("/settings.png", "/delete.png");
                            var $statusMenu = $("<div/>").addClass("statusMenu").attr("id", "statusMenuId").fadeIn().appendTo($(_this.$windowTitle));
                            if (IsZambaLinkMax()) {
                                $statusMenu.css("margin-top", "30px");
                            }
                            $statusMenu.html((_this.$windowTitle.data("viewChangeStatus") != true) ? "Desactivar:" : " ");
                            if (appZChat == "ZambaLink") {
                                var $reload = $("<img/>").addClass("reloadChatIcon").attr("src", URLImage + '/ChatJs/images/reload.png')
                                    .attr("title", "Refrescar")//.tooltip()
                                    .click(function () {
                                        RefreshChatPage();
                                    });
                                $statusMenu.prepend($reload);
                            }
                            var $btnStatus0 = $("<div/>").addClass("status0").attr("id", "status0").appendTo($statusMenu).html("<p>Desconectado</p>");
                            var $btnStatus1 = $("<div/>").addClass("status1").attr("id", "status1").appendTo($statusMenu).html("<p>Conectado</p>");
                            var $btnStatus2 = $("<div/>").addClass("status2").attr("id", "status2").appendTo($statusMenu).html("<p>Ocupado</p>");
                            //var $btnStatus3 = $("<div/>").addClass("status3").attr("id", "status3").appendTo($statusMenu).html("No Molestar");

                            $btnStatus0.click(function () {
                                ChangeStatus(0);
                            });
                            $btnStatus1.click(function () {
                                ChangeStatus(1);
                            });
                            $btnStatus2.click(function () {
                                ChangeStatus(2);
                            });
                            //$btnStatus3.click(function () {
                            //    ChangeStatus(3);
                            //});
                            //Para cuando solo quiere cambiar estado sin opción de deshabilitar chat
                            if (_this.$windowTitle.data("viewChangeStatus") != true) {

                                var $switchDiv = $("<div/>").addClass("onoffswitch").attr("id", "switchId").appendTo($statusMenu);
                                var $switchInput = $("<input type=\"checkbox\"/>").attr("name", "onoffswitch").addClass("onoffswitch-checkbox").attr("id", "myonoffswitch").appendTo($switchDiv);
                                var $switchLabel = $("<label/>").addClass("onoffswitch-label").attr("for", "myonoffswitch").attr("id", "myonoffswitch").appendTo($switchDiv);
                                var $switchSpan1 = $("<span/>").addClass("onoffswitch-inner").appendTo($switchLabel);
                                var $switchSpan2 = $("<span/>").addClass("onoffswitch-switch").appendTo($switchLabel);
                                if (_this.$windowTitle.data("switch") == true) {
                                    $switchInput.attr("checked", true)
                                }

                                $switchInput.change(function () {
                                    var userId = (_this.$window).attr("id");

                                    var disable = ($(this).is(":checked")) ? true : false;
                                    $.ajax({
                                        type: "POST",
                                        async: false,
                                        url: URLBase + '/disablechat',
                                        data: { userId: userId, disable: disable },
                                        success: function (result) {
                                        }
                                    });

                                    $(this).parent().parent().parent().data("switch", disable);
                                    if (disable) {
                                        $("#changeStatusDiv").data("status", "0");
                                        ChangeStatus(0);
                                        $("#noReadUserHistoryDiv").data("minimizeAllWin", true);
                                        $("#noReadUserHistoryDiv").click();
                                        $(_this.$windowContent).fadeOut();
                                    }
                                    else {
                                        ChangeStatus(1);
                                        $("#changeStatusDiv").data("status", "1");
                                        $("#noReadUserHistoryDiv").data("minimizeAllWin", false);
                                        $("#noReadUserHistoryDiv").click();
                                        $(_this.$windowContent).fadeIn();
                                    }
                                });
                            }

                            var $changeAvatar = $("<button/>").addClass("btn btn-primary btn-xs").addClass("btn").css("left", "125px").css("top", "-92px").attr("id", "changeAvatarId").appendTo($statusMenu).html("Avatar");
                            var $changeNameBtn = $("<button/>").addClass("btn btn-primary btn-xs").addClass("btn").css("left", "125px").css("top", "-94px").appendTo($statusMenu).html("Nombre").click(function () {
                                ChangeChatUserName();
                            });

                            $changeAvatar.click(function () {
                                if ($("#statusMenuId").length)
                                    $("#statusMenuId").remove();
                                var $statusMenu = $("<div/>").addClass("statusMenu").attr("id", "statusMenuId").appendTo($(_this.$windowTitle));
                                $statusMenu.css("margin-top", IsZambaLinkMax() ? "25px" : "10px");
                                var $img = $("<img/>").addClass("changeAvatarImg").attr("id", "changeAvatarImgId").attr("src",
                                    _this.$windowTitle.children("#containerUserName").children(".mainAvatar").attr("src")).appendTo($statusMenu);
                                if (IsZambaLinkMax()) $img.css("margin-top", "30px");
                                var $changePicBtn = $("<button/>").addClass("btn btn-primary btn-xs").addClass("btn").css("left", "-40px").attr("id", "changePicBtnId").html("Cambiar avatar").appendTo($statusMenu);
                                var $defaultPicBtn = $("<button/>").addClass("btn btn-primary btn-xs").addClass("btn").css("left", "85px").attr("id", "defaultPicBtnId").html("Avatar predeterminado").appendTo($statusMenu);
                                var $loc = $("<input/>").attr("type", "file").attr("id", "locId").css("height", "0px").css("width", "0px").appendTo($statusMenu);
                                $changePicBtn.click(function () {
                                    $loc.click();
                                });
                                $loc.change(function () {
                                    var validExt = "jpg,jpeg,gif,png,bmp,div";
                                    var file = document.getElementById("locId").value;
                                    var extFile = (file).substring(file.lastIndexOf(".") + 1).toLowerCase();

                                    if ($.inArray(extFile, validExt.split(",")) > -1) {
                                        var fReader = new FileReader();
                                        fReader.readAsDataURL(document.getElementById("locId").files[0]);
                                        fReader.onloadend = function (event) {
                                            var img = event.target.result;
                                            if (img.length < 1400000) { //Que no pese mas que un mega
                                                $("#changeAvatarImgId").attr("src", img);
                                                $("#mainAvatarId").attr("src", img);
                                                img = RemoveBase64Init(img);
                                                myUserInfoChat.Update("Avatar", img);
                                                changeAvatarDB(img);
                                                ChangeStatus(-1);// Para cambiar el Avatar// envio SingalR a otros users                                    
                                                // $("#changeAvatarDiv").data("avatar", img).click();
                                            }
                                            else {
                                                toastrInTitle("Error al subir imagen", "Compruebe que la imagen pese menos que 1Mb");
                                            }
                                            $loc.val("");
                                        }
                                    }
                                    else
                                        bootbox.alert("Formato de imagen no valido");
                                });

                                $defaultPicBtn.click(function () {
                                    var defAvatar = URLImage + 'ChatJs/images/defaultAvatar.png';
                                    $("#changeAvatarImgId").attr("src", defAvatar);
                                    $("#mainAvatarId").attr("src", defAvatar);
                                    toDataUrl(defAvatar, function (base64Img) {
                                        base64Img = base64Img.substring(base64Img.indexOf(",") + 1);
                                        myUserInfoChat.Update("Avatar", base64Img);
                                    });
                                    changeAvatarDB("default");
                                    ChangeStatus(-1);
                                });
                            });
                            if (chatMode == "compact") {
                                var cMC = widthCompact - 100;
                                $changeAvatar.css("left", cMC + "px");
                                $changeNameBtn.css("left", cMC + "px");
                                //$statusMenu.css("text-indent", cMC + "px");
                                var $innerCont = $statusMenu.parents(".chat-window").find(".chat-window-inner-content:last");
                                var heightCont = $innerCont.height() - 60;
                                $innerCont.height(heightCont);
                                $statusMenu.parents(".chat-window").find(".chat-window-content").height(heightCont);
                            }
                        }
                        $(this).css("background-image", iconStr);
                        $("div.colIcon, div.addGroup, div#Filtro").css("display", $("#statusMenuId").length ? "none" : "block");
                    });

                if (chatMode == "normal") {
                    var $hideShowWin = $("<div/>").addClass("hideShowWin").attr("title", "Minimizar todas las conversaciones")
                        .appendTo(_this.$windowTitle).tooltip().click(function () {
                            $("#hideShowWindows").click();
                        });

                    var $pin = $("<div/>").addClass("pinStatus").attr("title", "Fijar - Contraer").attr("id", "pinId").data("pin", false)
                        .css('background-image', 'url("' + URLImage + 'ChatJs/images/unpin.png")').appendTo(_this.$windowTitle).tooltip().click(function () {
                            if ($pin.data("pin"))
                                $pin.css('background-image', 'url("' + URLImage + 'ChatJs/images/unpin.png")')
                            else
                                $pin.css('background-image', 'url("' + URLImage + 'ChatJs/images/pin.png")')

                            $pin.data("pin", !$pin.data("pin"));
                        });
                }
            }
            function ChangeStatus(status) {
                var shouldGetHist = false;
                if ($("#changeStatusDiv").data("status") == 0)
                    shouldGetHist = true;
                var $mAvatar = $(".mainAvatar");
                if (chatMode == "compact") {
                    $mAvatar.css(getBorderFromStatus(status));
                }
                else {
                    switch (status) {
                        case 0:
                            $("#mainStatusId").css('background-image', 'url("' + URLImage + 'ChatJs/images/chat-offline.png")');
                            break;
                        case 1:
                            $("#mainStatusId").css('background-image', 'url("' + URLImage + 'ChatJs/images/chat-online.png")');
                            break;
                        case 2:
                            $("#mainStatusId").css('background-image', 'url("' + URLImage + 'ChatJs/images/chat-busy.png")');
                            break;
                        //case 3:
                        //    $("#mainStatusId").css('background-image', 'url("ChatJs/images/chat-dontdisturb.png")');
                        //    break;
                    }
                }
                ChangeStatusDB(status);
                $("#statusMenuId").remove();
                if (chatMode == "compact") {
                    var $innerCont = $mAvatar.parents(".chat-window").find(".chat-window-inner-content:last");
                    $innerCont.height($innerCont.height() + 60);
                }
                $("#changeStatusDiv").data("status", status).click();

                if (shouldGetHist)
                    $("#noReadUserHistoryDiv").click();
            }

            if (_this.opts.canClose) {
                $closeButton.click(function (e) {
                    $(".chat-window-inner-content").css({ "max-height": "360px", "height": "360px" });
                    e.stopPropagation();
                    // removes this item from the collection
                    for (var i = 0; i < $._chatContainers.length; i++) {
                        if ($._chatContainers[i] == _this) {
                            $._chatContainers.splice(i, 1);
                            break;
                        }
                    }
                    // removes the window
                    _this.$window.remove();
                    // triggers the event
                    (_this.$window).attr("id", "toDelete");
                    _this.opts.onClose(_this);
                    // delete _this;
                    AdjustChatSize();
                });

                var $addUserButton = $("<div/>").addClass("addUser")
                    .attr("title", "Agregar")
                    .attr("data-placement", "bottom")
                    .appendTo(_this.$windowTitle).tooltip()
                    .css("display", "none").click(function (e) {
                        // var myUserId = $('div.chat-window').attr("id");
                        var chatId = (_this.$window).attr("chatid");
                        // var otherUserId1 = otherUserId.split("/")[0];
                        if (document.getElementById("addNewUser-" + chatId)) {// ya existia el txt para agregar usuario                     
                            $("#addNewUser-" + chatId + ", " + "#tildeAddUser-" + chatId).remove();
                        }
                        else {
                            //Agregar usuario al presionar Enter                  
                            function addUserOnClick() {
                                if ($addUserTxt.val().length >= 4) {
                                    $("#tildeAddUser-" + chatId).click();
                                }
                            }
                            var usersName = [];
                            var usersId = [];
                            var data = {
                                Users: []
                            };
                            var usersInChat = GetUsersByChat(chatId);
                            var usersIds = [];
                            for (var i = 0; i <= usersInChat.length - 1; i++)
                                usersIds.push(usersInChat[i].Id);

                            var allUsers = GetAllUsersChat();
                            for (var i = 0; i <= allUsers.length - 1; i++) {
                                var au = allUsers[i];
                                if (usersIds.indexOf(au.Id) > -1) continue;
                                data.Users.push(au.Name);
                                usersName.push(au.Name);
                                usersId.push(au.Id);
                            }

                            var $container = $("<div/>").addClass("typeahead-container").appendTo(_this.$windowTitle);
                            var isGroup = _this.$windowTitle.parent().attr("isgroup") == "2" || _this.$windowTitle.parent().attr("isgroup") == "true";
                            if (isGroup) {
                                $container.css("padding-top", "28px");
                                _this.$windowTitle.children("#infoUsersGroup, .showAllUsers, .uploadFile, .addUser").hide();
                                //Muestro integrantes actuales
                                $(".addUserTxt").show();
                                //var firstUser = _this.$windowTitle.find("#containerUserName").attr("name");
                                //if (firstUser != undefined) {
                                //    $("<div/>").addClass("addUserTxt").text(firstUser).attr("id", "firstUser").appendTo(_this.$windowTitle);
                                //}
                            }

                            var $span = $("<span/>").addClass("typeahead-query").appendTo($container);
                            var $addUserTxt = $("<input/>").attr("autocomplete", "off")
                                .attr("placeholder", "Agregar participante")
                                .attr("id", "addNewUser-" + chatId).appendTo($span).addClass("AddUserToChat form-control");
                            var $remFilter = $("<img/>").attr("src", URLImage + '/ChatJs/images/delete.png').addClass("remFilterImg").appendTo($span)
                                .click(function (e) {
                                    if (isGroup) {
                                        _this.$windowTitle.children("#infoUsersGroup, .showAllUsers").show();
                                        $(".addUserTxt").hide();
                                        _this.$windowTitle.find("#firstUser").remove();
                                    }
                                    //_this.$windowTitle.children(".uploadFile, .addUser").show();
                                    $(this).parents(".chat-window-title").children(".addUser").click();
                                });

                            if (chatMode == "compact")
                                $addUserTxt.parents(".typeahead-container").css("position", "absolute");
                            $addUserTxt.on("input", null, null, addUserOnClick);
                            $('#addNewUser-' + chatId).typeahead({
                                limit: 2,
                                minLength: 1,
                                order: "asc",
                                hint: true,
                                limit: 2,
                                source: {
                                    country: {
                                        data: data.Users
                                    }
                                },
                                callback: {
                                    onClickAfter: function (node, a, item, event) {
                                        $("#tildeAddUser-" + chatId).click();
                                        $(".addUserTxt").hide();
                                        $(node).parents("#windowTitle").find("#firstUser").remove();
                                    }
                                },
                            });
                            $addUserTxt.select().focus();

                            var $okAddUserButton = $("<div/>").addClass("okAddUser").attr("id", "tildeAddUser-" + chatId).appendTo(_this.$windowTitle)
                                .click(function (e) {
                                    if ($addUserTxt.val() == "" || !IsUserSelected($addUserTxt.val())) {
                                    }
                                    else {
                                        selectedUserId = SelectedUserId($addUserTxt.val());
                                        var addedUserName = CapitalizeString($addUserTxt.val().split("/")[0]);
                                        var $wT = _this.$windowTitle;
                                        if (!$wT.children("#usersInGroup").length)
                                            $("<div/>").addClass("addUserTxt").attr("id", "usersInGroup").appendTo(_this.$windowTitle);
                                        var $txtNewUser = $("<div/>")      //nuevo usuario div          
                                            .attr("id", "addedUser" + selectedUserId)//+ otherUserId + "/"
                                            .addClass("addUserTxt")
                                            .html(addedUserName)
                                            .appendTo($wT.children("#usersInGroup")).css("display", "none");

                                        $addUserTxt.remove();
                                        $wT.attr("isGroup", true).data({ "chatOpened": "chatid" + $wT.attr("chatid"), "open": true });

                                        SetFunctionUpload($wT.attr("id"), (isCS || isExternal));

                                        $okAddUserButton.remove();
                                        $(".typeahead-container").remove();
                                        //ver en desuso se puede implementar a futuro
                                        //var $removeUserButton = $("<div/>").addClass("removeUser").attr("id", "removeUser-" + selectedUserId)
                                        //    .appendTo(_this.$windowTitle).css("display", "none").click(function (e) {
                                        //        $("#addNewUser-" + chatId + ", #tildeAddUser-" + chatId).remove(); // que no pueda agregar

                                        //        var userToRemoveId = $removeUserButton.attr("id").split("-")[1];
                                        //        var newIdChat = ((_this.$window).attr("id")).replace("/" + userToRemoveId, "");
                                        //        (_this.$window).attr("id", newIdChat); //lo elimino de ids del chat
                                        //        SetFunctionUpload(newIdChat,  isCS);

                                        //        //titulo de ventana grupal
                                        //        var title = $("div[class=text]", _this.$windowTitle).text();
                                        //        // var usInChat = (_this.$window).attr("id").split("/").length;
                                        //        // if (usInChat == 1) {
                                        //        //si queda un solo usuario en el chat quito icono mostrar/ocultar                                               
                                        //        (_this.$window).children(".chat-window-title").children(".showAllUsers").remove();
                                        //        (_this.$window).attr("isGroup", false);
                                        //        //  }

                                        //        (_this.$window).children(".chat-window-title").children("#addedUser" + userToRemoveId).remove();//elimino txtbox
                                        //        $removeUserButton.remove();//saco boton X     
                                        //        GetHistory(1);
                                        //    });

                                        (_this.$windowTitle).attr("id", "thisWinTitle");// muestro el boton de ver usuarios
                                        $("#thisWinTitle .showAllUsers").css("display", "block");
                                        (_this.$windowTitle).attr("id", "windowTitle");
                                        _this.$windowContent.find(".emoji-wysiwyg-editor.chat-window-text-box").text("<zmbChat:small>Se ha añadido a " + addedUserName);
                                        $(_this.$windowTitle).children(".addUser").show();
                                        AddRemoveUsersGroups(_this.$window.attr("chatid"), selectedUserId, true);
                                        SendEnterKeyChat(_this.$window);//COLOCAR
                                    }
                                });

                            function GetHistory(chatType) {
                                alert("ver funcion debe ser por chatid, ver al momento de eliminar usuarios de grupo");
                                // $("#openGroupDiv").data("usersId", (_this.$window).attr("id").replace("/", ",") + "," + myUserId);
                                $.ajax({
                                    type: "GET",
                                    async: false,
                                    url: URLBase + '/getmessagehistory',
                                    data: { usersId: myUserId + "/" + (_this.$window).attr("id"), chatType: chatType },
                                    success: function (result) {
                                        _this.$windowInnerContent[0].innerHTML = "";
                                        if (result.History.length > 0) {

                                            var $getMoreMsgBtn = $("<div/>").addClass("getMoreMsg") //boton traer mensajes anteriores                                     
                                                .attr("id", "getmoremsg" + (_this.$window).attr("id").replace(new RegExp("/", 'g'), "-"))
                                                .text("Traer mensajes anteriores")
                                                .appendTo(_this.$windowInnerContent);

                                            if (chatMode == "compact") $getMoreMsgBtn.css("margin-left", "5px");
                                            $getMoreMsgBtn.click(function (e) {
                                                var otherUserId = (_this.$window).attr("id");

                                                var msgNum = parseInt((_this.$window).attr("msgNum"));
                                                (_this.$window).attr("msgNum", msgNum > 0 ? msgNum + 1 : 1);

                                                $.ajax({
                                                    type: "GET",
                                                    async: false,
                                                    url: URLBase + '/getmoremsghistory',
                                                    data: {
                                                        usersId: myUserId + "/" + otherUserId,
                                                        cant: parseInt((_this.$window).attr("msgNum"))
                                                    },
                                                    success: function (result) {
                                                        var thisId = (_this.$window).attr("id");
                                                        var getMsgNum = 5;//para saber cuando cargo todos los mensajes
                                                        var msgNum = getMsgNum * (parseInt((_this.$window).attr("msgNum")) + 1);
                                                        if (result.History.length < msgNum) {
                                                            $("#getmoremsg" + thisId.replace(new RegExp("/", 'g'), "-")).remove();

                                                            (_this.$window).attr("msgNum", "");
                                                        }
                                                        (_this.$window).attr("id", "clearOldMsg");

                                                        ClearChatContentList();
                                                        var cantSend;
                                                        for (var i = 0; i < result.History.length; i++) {
                                                            AddMsg(result.History[i]);
                                                        }
                                                        (_this.$window).attr("id", thisId);
                                                        _this.$windowInnerContent.scrollTop(0);
                                                    },
                                                })

                                            });

                                            for (var i = 0; i < result.History.length; i++) {
                                                AddMsg(result.History[i]);
                                            }
                                        }
                                    },
                                });

                                function AddMsg(message) {
                                    var $messageP;
                                    if (message.Message.substr(0, 5) == "*-*¡¿") {
                                        var fileName = message.Message.substr(message.Message.lastIndexOf("/") + 1).substr(22);//.substr(22) sin fecha

                                        $messageP = $('<p>').text(fileName).addClass("sendFileA")
                                            .css('background-image', ExtImg(fileName)).click(function (e) {
                                                (message.Message.substr(5));
                                            });
                                    }
                                    else {
                                        $messageP = $("<p/>").text(message.Message);
                                    }

                                    // gets the last message to see if it's possible to just append the text
                                    var $lastMessage = $("div.chat-message:last", _this.$windowInnerContent);
                                    if ($lastMessage.length && $lastMessage.attr("data-val-user-from") == message.UserFromId) {
                                        // we can just append text then
                                        $messageP.appendTo($(".chat-text-wrapper", $lastMessage));
                                    }
                                    else {
                                        // in this case we need to create a whole new message
                                        var $chatMessage = $("<div/>").addClass("chat-message").attr("data-val-user-from", message.UserFromId);
                                        $chatMessage.appendTo(_this.$windowInnerContent);

                                        var $gravatarWrapper = (myUserId == message.UserFromId) ?
                                            $("<div/>").addClass("chat-gravatar-wrapper").css("float", "right").attr("height", "30px").attr("width", "30px").appendTo($chatMessage) :
                                            $("<div/>").addClass("chat-gravatar-wrapper").appendTo($chatMessage);

                                        var $textWrapper = $("<div/>").addClass("chat-text-wrapper").appendTo($chatMessage);

                                        // add text                               
                                        $messageP.appendTo($textWrapper);

                                        var messageUserFrom;
                                        $.ajax({
                                            type: "GET",
                                            async: false,
                                            url: URLBase + '/getuserinfo',
                                            data: { userId: message.UserFromId },
                                            success: function (result) {
                                                messageUserFrom = result;
                                            },
                                        });


                                        var $img = messageUserFrom.User.Avatar;
                                        if (message.UserFromId != thisUserId) {
                                            $("<img/>").attr("src", "data:image/jpg;base64," + $img).attr("height", "30px").attr("width", "30px").attr("id", "AvatarFromother").addClass("embebedIMG").appendTo($gravatarWrapper);
                                        }
                                        else {
                                            $("<img/>").attr("src", "data:image/jpg;base64," + $img).attr("height", "30px").attr("width", "30px").attr("id", "AvatarUser").addClass("embebedIMG").appendTo($gravatarWrapper);
                                        }
                                        //$("<img/>").attr("src", "data:image/jpg;base64," + $img).attr("height", "30px").attr("width", "30px").attr("id", "AvatarUser").appendTo($gravatarWrapper);
                                    }
                                    // scroll to the bottom
                                    _this.$windowInnerContent.scrollTop(_this.$windowInnerContent[0].scrollHeight);
                                }
                            }
                        };

                        function IsUserSelected(selUser) {
                            for (var i = 0; i < usersName.length; i++) {
                                if (selUser == usersName[i])
                                    return true;
                            }
                            return false;
                        }
                        function SelectedUserId(selUserName) {
                            for (var i = 0; i < usersName.length; i++) {
                                if (selUserName == usersName[i])
                                    return usersId[i];
                            }
                        }
                    });

                var $showAllUsersButton = $("<div/>").addClass("showAllUsers")
                    .attr("title", "Participantes")
                    .attr("data-placement", "bottom").tooltip()
                    .css("display", "none").appendTo(_this.$windowTitle).click(function (e) {
                        $showAllUsersButton.attr("modeDisplay", ($showAllUsersButton.attr("modeDisplay")) == "none" ? "block" : "none");
                        var modeDisplay = $showAllUsersButton.attr("modeDisplay");
                        var actualTitle = $(_this.$windowTitle).attr("id");
                        $(_this.$windowTitle).attr("id", "windowTitleId");

                        $("#windowTitleId .addUserTxt").each(function (indice, elemento) {
                            elemento.style.display = modeDisplay;
                        });
                        $("#windowTitleId .removeUser").each(function (indice, elemento) {
                            elemento.style.display = modeDisplay;
                        });
                        $("#windowTitleId .okAddUser").each(function (indice, elemento) {
                            elemento.style.display = modeDisplay;
                        });

                        var divCUN = $(_this.$windowTitle).children("#containerUserName")[0];
                        var currentTitle;
                        if (divCUN.childNodes.length)
                            for (var i = 0; i < divCUN.childNodes.length; i++) {
                                if (divCUN.childNodes[i].nodeType === 3)
                                    currentTitle = divCUN.childNodes[i].textContent;
                            }

                        //Cuando expande la lista de usuarios
                        if (modeDisplay == "block") {
                            //Le quito el +N porque esta mostrando todos
                            var div = $(_this.$windowTitle).children("#containerUserName")[0];
                            if (div.childNodes.length) {
                                for (var i = 0; i < div.childNodes.length; i++) {
                                    if (div.childNodes[i].nodeType === 3)
                                        div.childNodes[i].textContent = $(div).attr("name");
                                    else
                                        $(div.childNodes[i]).hide();
                                }
                                $(div).parent().children(".addUser, #infoUsersGroup, .uploadFile").hide();
                            }
                        }
                        else {
                            var div = $(_this.$windowTitle).children("#containerUserName")[0];
                            if (div.childNodes.length) {
                                for (var i = 0; i < div.childNodes.length; i++) {
                                    if (div.childNodes[i].nodeType === 3)//&& usersInChat > 0
                                        div.childNodes[i].textContent = "";// currentTitle.split(" +")[0] + " +" + usersInChat;
                                    else
                                        $(div.childNodes[i]).show();
                                }
                            }
                            $(div).parent().find(".lastUserConnChat").hide();
                            $(div).parent().children(".addUser, #infoUsersGroup, .uploadFile").show();
                        }
                        $(_this.$windowTitle).attr("id", actualTitle);
                        if (chatMode == "compact") {
                            var titHeigth = (heightCompact - $(_this.$windowTitle).height() - 60) + "px";//60xtextarea
                            $(_this.$windowTitle).parent().find(".chat-window-inner-content").css({ height: titHeigth, "max-heigth": titHeigth });
                        }
                    });
                var $shareDiv = $("<div/>").addClass("uploadFile")
                    .attr("title", "Compartir").attr("alt", "Compartir").attr("data-placement", "bottom")
                    .appendTo(_this.$windowTitle).tooltip()
                    .click(function (e) {
                        e.preventDefault();
                        var $this = $(this);
                        var $sC = $this.parent().find(".shareChat");
                        $sC.fadeToggle();

                        $(".changeNameGroupChat, .leaveGroupChat").css("display", "none");
                    });

                var $shareDivContainer = $("<div/>").addClass("shareChat").appendTo(_this.$windowTitle);

                var $uploadImgDiv = $("<img/>").addClass("uploadImgDivChat")
                    .attr("title", "Imagen").attr("alt", "Imagen").attr("data-placement", "bottom")
                    .attr("src", URLImage + fonts + 'image.png').appendTo($shareDivContainer).tooltip()
                    .click(function (e) {
                        e.preventDefault();
                        var $this = $(this);
                        $this.data("uploadImg", true);
                        $this.parents(".chat-window").find(".shareImgChat").click();
                        $this.parent().fadeOut();
                    });

                var $uploadFileDiv = $("<div/>").addClass("uploadFile2")
                    .attr("title", "Archivo").attr("alt", "Archivo").attr("data-placement", "bottom")
                    .appendTo($shareDivContainer).tooltip()
                    .click(function (e) {
                        // e.preventDefault();
                        $(this).parent().fadeOut();
                    });

                var $uploadFileButton = $("<input/>").addClass('uploadFileButton')
                    .attr({ type: 'file', id: "upFile" }).appendTo($uploadFileDiv);
            }
            var $containerUserName = $("<div/>").attr("id", "containerUserName").appendTo(_this.$windowTitle).click(function (e) {
                if (e.target.tagName != "IMG") ShowUserInfoInChat($(this));
            });
            var $containerUsersGroup = $("<input/>").attr({ "id": "containerUsersGroup", "maxlength": "15", "readonly": "readonly" })//.prop('disabled', true)
                .css({ border: "none", "top": "3px", "background-color": "rgba(255, 255, 255, 0)", width: "35%", "height": "25px", "cursor": "pointer" })
                .appendTo(_this.$windowTitle).addClass("text").css("display", "none").on('click', function (e) {
                    if ((e.target.tagName == "INPUT" && $(e.target).attr("readonly")))//e.target.tagName !="INPUT" && e.target.tagName != "IMG"
                        ShowUserInfoInChat($(this));
                });
            if (!_this.opts.canClose) {
                var $span = $("<span/>").addClass("mainTitle").text(_this.opts.title.split("/")[0]);

                $span.appendTo(_this.$windowTitle);
                var $subName = $("<div/>").addClass("subName").text(_this.opts.title.split("/").length == 2 ? _this.opts.title.split("/")[1] : ' ')
                    .appendTo(_this.$windowTitle);

                $span.click(function (e) {
                    if (appZChat != "ZambaLink")
                        $(this).parent().find(".minimized").click();
                });
                $subName.click(function (e) {
                    if (appZChat != "ZambaLink")
                        $(this).parent().find(".minimized").click();
                });
            }

            function ShowUserInfoInChat(_this) {
                var $t = _this;
                if ($t.parents(".chat-window").is("[chatid]")) {
                    //Para grupo
                    if ($t.is("[readonly]"))
                        ChatUsersInfo("", $t.parents(".chat-window").attr("chatid"));
                }
                else {
                    //Para usuario simple
                    ChatUsersInfo($t.parents(".chat-window").attr("id"), "");
                }
            }

            // content
            _this.$windowContent = $("<div/>").addClass("chat-window-content").appendTo(_this.$window);
            if (_this.opts.initialToggleState == "minimized")
                _this.$windowContent.fadeOut();//tenia el hide comun
            $("<div/>").attr("id", "loadingChatContent").appendTo(_this.$windowContent);
            _this.$windowInnerContent = $("<div/>").addClass("chat-window-inner-content").appendTo(_this.$windowContent);

            if (!_this.opts.canClose && chatMode == "normal")
                $(_this.$windowInnerContent).css("margin-bottom", "35px");

            // text-box-wrapper
            if (_this.opts.showTextBox) {
                var $windowTextBoxWrapper = $("<div/>").addClass("chat-window-text-box-wrapper").appendTo(_this.$windowContent);
                _this.$textBox = //$("<textarea />").attr("rows", "1").addClass("chat-window-text-box").appendTo($windowTextBoxWrapper);
                    $("<div />").attr({ "contentEditable": "true", "data-emojiable": true }).addClass("chat-window-text-box").attr("placeholder", "Escribe un mensaje").appendTo($windowTextBoxWrapper);
                _this.$textBox.autosize();
                $zumbido = $("<img/>").addClass('fontButton').attr("src", URLImage + '/ChatJs/images/zumbido.png')
                    .attr("data-placement", "left").attr('title', "Enviar alerta").appendTo($windowTextBoxWrapper);
                $fontBtn = $("<img/>").addClass('fontButton').attr("src", URLImage + '/ChatJs/images/font.png')
                    .attr("data-placement", "left").attr('title', "Fuente - Color").appendTo($windowTextBoxWrapper);

                $fontBtn.tooltip();

                $(function () {
                    // https://github.com/OneSignal/emoji-picker
                    window.emojiPicker = new EmojiPicker({
                        emojiable_selector: '[data-emojiable=true]',
                        assetsPath: thisDomain + '/ChatJs/Emojis/lib/img',
                        popupButtonClasses: 'fa fa-smile-o'
                    });
                    window.emojiPicker.discover();
                });

                $(".emoji-picker-icon.emoji-picker").click(function () {
                    var $contText = $(".emoji-wysiwyg-editor.chat-window-text-box");
                    if ($contText.html() == "")
                        $contText.html(" ");
                    //var picker = $(".emoji-menu.tether-element");
                    //var top = 200 + ($(window).height() - $(".emoji-picker-icon").offset().top);
                    //var left = 200 + ($(window).width() - $(".emoji-picker-icon").offset().left);//($(".emoji-picker-icon").offset().left);
                    //picker.css({ "top": -top, "left":-left, "position":"relative" });
                });

                $zumbido.tooltip().click(function () {
                    var windowChat = $(this).parents(".chat-window");//  { direction: "up", times: 4, distance: 101}
                    ShakeWindow(windowChat);
                    windowChat.data("buzz", true);
                    playBuzzSound();
                    SendEnterKeyChat(windowChat);
                });

                $fontBtn.click(function () {
                    var show = $(this).data("show");
                    var $box = $(this).parent().children(".editText");
                    var $colorBox = $(this).parent().children(".editColor");
                    if (show) {
                        $box.fadeOut();
                        $colorBox.fadeOut();
                        $(this).data("show", false).css("opacity", "0.3");
                        if (chatMode == "compact") {
                            if (IsZambaLinkMax() || IsZambaLinkRestore()) {
                                $(".chat-window-text-box-wrapper").css("bottom", "0px");
                            }
                            else {
                                var contHeight = (heightCompact - 85) + "px";
                                $(".chat-window-inner-content").css({ "max-height": contHeight, "height": contHeight });
                            }
                        }
                    }
                    else {
                        $box.fadeIn();
                        $colorBox.fadeIn().css({ display: "inline-block" });
                        $(this).data("show", true).css("opacity", "0.6");
                        if (chatMode == "compact") {
                            if (IsZambaLinkMax()) {
                                $(".chat-window-text-box-wrapper").css("bottom", "50px");
                            }
                            else {
                                var contHeight = (heightCompact - 140) + "px";
                                $colorBox.parents(".chat-window").find(".chat-window-inner-content").css({ "max-height": contHeight, "height": contHeight });
                                if (IsZambaLinkRestore())
                                    $(".chat-window-text-box-wrapper").css("bottom", "50px");
                            }
                        }
                    }
                });
                EditText($windowTextBoxWrapper);
            }

            // enlists this container in the containers
            if (!$._chatContainers)
                $._chatContainers = new Array();
            $._chatContainers.push(_this);

            if ($._chatContainers.length <= 4)
                $.organizeChatContainers();
            //Que no muestre chat hasta que no haya cargado lista de usuarios     
            if (!_this.opts.canClose) {
                _this.$window.css("display", "none");
                if (appZChat == "ZambaLink") {
                    $("#loadingZChatp").text("Cargando interfaz de usuario...");
                }
            }
            _this.opts.onCreated(_this);
        },

        getContent: function () {
            /// <summary>Gets the content of the chat window. This HTML element is the container for any chat window content</summary>
            /// <returns type="Object"></returns>
            var _this = this;
            return _this.$windowInnerContent;
        },

        setTitle: function (title) {
            var _this = this;
            $("#containerUserName", _this.$windowTitle).text(title.split("/")[0]);//"div[class=text]"
        },

        setIdContainer: function (id) {
            var _this = this;
            _this.$window.attr("id", id);
        },

        setVisible: function (visible) {
            /// <summary>Sets the window visible or not</summary>
            /// <param FullName="visible" type="Boolean">Whether it's visible</param>
            var _this = this;
            if (visible)
                _this.$window.show();
            else
                _this.$window.hide();
        },

        getToggleState: function () {
            var _this = this;
            return _this.$windowContent.is(":visible") ? "maximized" : "minimized";
        },

        setToggleState: function (state) {
            var _this = this;
            if (state == "minimized")
                _this.$windowContent.hide();
            else if (state == "maximized")
                _this.$windowContent.show();
        }
    };

    // The actual plugin
    $.chatContainer = function (options) {
        var chatContainer = new ChatContainer(options);
        chatContainer.init();
        return chatContainer;
    };

    $.organizeChatContainers = function () {
        // this is the initial right offset

        if (chatMode == "normal") {
            if ($._chatContainers.length >= 5) {
                $("#showMoreWindows").children().text(($._chatContainers.length - 4) + (($._chatContainers.length - 4 == 1) ? " Conversación" : " Conversaciones"));
            }
            else {
                if ($("#showMoreWindows").length)
                    $("#showMoreWindows").remove();
            }
        }
        for (var i = 0; i < $._chatContainers.length; i++) {
            if (chatMode == "compact") {
                if ($($._chatContainers[i].$window).attr("id") != thisUserId) {
                    $._chatContainers[i].$window.css({ "display": "block", "right": (rightOffset + "px"), "left": "", "bottom": "" });
                    $._chatContainers[i].$windowContent.css("display", "block").width(widthCompact).height(heightCompact - titleHeight);
                }
                continue;
            }
            if (rightOffset <= 730) {
                $._chatContainers[i].$window.css({ "display": "block", "right": (rightOffset + "px"), "left": "", "bottom": "" }).removeAttr("groupwin");
                $._chatContainers[i].$windowContent.css("display", "block");
            }
            else {
                if ($._chatContainers[i].$window.attr("groupwin") == "restore") {
                    $._chatContainers[i].$window.attr("groupwin", "hidden").css("display", "none");
                }
            }
            var width = $._chatContainers[i].$window.outerWidth() == 60 ? 230 : $._chatContainers[i].$window.outerWidth();
            rightOffset += parseInt(width / 10) * 10 + deltaOffset;
        }
    };
})(jQuery);

// CHAT WINDOW
(function ($) {

    function ChatWindow(options) {
        /// <summary>This is the chat window for a user.. contains the chat messages</summary>
        /// <param FullName="options" type="Object"></param>
        // Defaults:
        this.defaults = {
            chat: null,
            myUser: null,
            otherUser: null,
            typingText: null,
            initialToggleState: "maximized",
            initialFocusState: "focused",
            userIsOnline: 0,//false
            adapter: null,
            onReady: function () { },
            onClose: function (container) { },
            // triggers when the window changes it's state: minimized or maximized
            onToggleStateChanged: function (currentState) { }
        };

        //Extending options:
        this.opts = $.extend({}, this.defaults, options);

        //Privates:
        this.$el = null;
        this.chatContainer = null;

        this.addMessage = function (message, clientGuid) {
            var _this = this;
            _this.chatContainer.setToggleState("maximized");

            if (message.UserFromId != this.opts.myUser.Id) {
                // the message did not came from myself. Better erase the typing signal
                _this.removeTypingSignal();
            }
            if (message.ClientGuid && $("p[data-val-client-guid='" + message.ClientGuid + "']").length) {
                // in this case, this message is comming from the server AND the current user POSTED the message.
                // so he/she already has this message in the list. We DO NOT need to add the message.
                $("p[data-val-client-guid='" + message.ClientGuid + "']").removeClass("temp-message").removeAttr("data-val-client-guid");
            } else {
                var $messageP;
                if (message.Message.substr(0, 5) == "*-*¡¿") {
                    var fileName = message.Message.substr(message.Message.lastIndexOf("/") + 1).substr(22);//.substr(22) sin fecha
                    //$pText = $("<p/>").text("Compartío archivo...").addClass("sendFileTxt");//.appendTo($(".chat-text-wrapper :last"));

                    $messageP = $('<p>').text(fileName).addClass("sendFileA");
                    $messageP.css('background-image', ExtImg(fileName));
                    $messageP.click(function (e) {
                        AjaxDownloadFile(message.Message.substr(5));
                    });
                }
                else {
                    $messageP = $("<p/>").html(message.Message);
                }

                if (clientGuid) {
                    $messageP.attr("data-val-client-guid", clientGuid).addClass("temp-message").attr("id", "MyMessageChat");
                }
                if (clientGuid == undefined && message.UserFromId == thisUserId) {
                    $messageP.attr("data-val-client-guid", clientGuid).addClass("temp-message").attr("id", "MyMessageChat");
                }
                if (clientGuid == undefined && message.UserFromId != thisUserId) {
                    $messageP.attr("data-val-client-guid", clientGuid).addClass("temp-message").attr("id", "MessageFromChat");

                }
                if (IsZambaLinkMax()) {
                    $messageP.css({ width: "90%" });
                }

                linkify($messageP);
                emotify($messageP);

                // gets the last message to see if it's possible to just append the text

                var $lastMessage = $("div.chat-message:last", _this.chatContainer.$windowInnerContent);
                if ($lastMessage.length && $lastMessage.attr("data-val-user-from") == message.UserFromId) {
                    // we can just append text then 
                    $messageP.appendTo($(".chat-text-wrapper", $lastMessage));
                }
                else {
                    $messageP.appendTo($(".chat-text-wrapper", $lastMessage));
                    // in this case we need to create a whole new message
                    var $chatMessage = $("<div/>").addClass("chat-message").attr("data-val-user-from", message.UserFromId);
                    $chatMessage.appendTo(_this.chatContainer.$windowInnerContent);
                    $messageP.css('color', 'black');// Escribia en gris claro

                    var date;
                    if (message.DateTime == undefined)//fecha actual
                        date = new Date(new Date().setMonth(new Date().getMonth() + 1));
                    else {
                        if (message.DateTime.substring(0, 5) == "/Date")//Por si viene de ajax con formato incorrecto
                            date = new Date(parseInt(message.DateTime.substring(6, message.DateTime.length - 2)));
                        else
                            date = new Date(message.DateTime);
                    }
                    var $spanName;
                    var $span;
                    if (_this.opts.myUser.Id != message.UserFromId) {
                        $messageP.attr("data-val-client-guid", clientGuid).addClass("temp-message").attr("id", "MessageFromChat");
                        $("<div/>").addClass("chat-gravatar-wrapper").css("float", "left").appendTo($chatMessage)
                        var spanUsrName = _this.opts.chat.usersById[message.UserFromId].Name.split("/")[0];
                        var txtName = ReduceName(spanUsrName, 10) + formatDateText(date);
                        $spanName = $("<span/>").text(txtName).attr({ "id": "DateFromOtherChat", "title": txtName }).attr("data-placement", "bottom").tooltip().css("padding-top", "8px")
                            .insertBefore($("</br>"));
                    }
                    else {
                        $messageP.attr("data-val-client-guid", clientGuid).addClass("temp-message").attr("id", "MyMessageChat");
                        //$("<span/>").text(formatDateText(date)).css("float", "right").addClass("spanUserName").insertBefore($("</br>").insertAfter($("</br>")
                        //    .insertBefore($chatMessage)));
                        $span = $("<span/>").text(formatDateText(date)).attr({ "id": "DateFromOtherChat", "title": formatDateText(date) }).attr("data-placement", "bottom").tooltip();
                    };

                    var $gravatarWrapper = (_this.opts.myUser.Id == message.UserFromId) ?
                        $("<div/>").addClass("chat-gravatar-wrapper").css("float", "right").attr("height", "30px").attr("width", "30px").appendTo($chatMessage) :
                        $("<div/>").addClass("chat-gravatar-wrapper").appendTo($chatMessage);
                    var $textWrapper = $("<div/>").addClass("chat-text-wrapper").appendTo($chatMessage);

                    // add text
                    //if ($spanName != undefined) {
                    //    $spanName.insertBefore($textWrapper);
                    //    $("<br/>").insertBefore($textWrapper);
                    //}
                    $messageP.appendTo($textWrapper);

                    if ($span != undefined) $span.appendTo($chatMessage.find(".temp-message"));
                    // add image
                    var messageUserFrom = _this.opts.chat.usersById[message.UserFromId];

                    var $img = messageUserFrom.Avatar === null ? messageUserFrom.ProfilePictureUrl : messageUserFrom.Avatar;
                    if (message.UserFromId != thisUserId) {
                        $("<img/>").attr("src", "data:image/jpg;base64," + $img).attr("height", "30px").attr("width", "30px").attr("id", "AvatarFromother").addClass("embebedIMG").appendTo($gravatarWrapper);
                    }
                    else {
                        $("<img/>").attr("src", "data:image/jpg;base64," + $img).attr("height", "30px").attr("width", "30px").attr("id", "AvatarUser").addClass("embebedIMG").appendTo($gravatarWrapper);
                    }
                }
                // scroll to the bottom
                _this.chatContainer.$windowInnerContent.scrollTop(_this.chatContainer.$windowInnerContent[0].scrollHeight);
            }
        };

        this.addMsgUsers = function (message, clientGuid) {
            var _this = this;
            _this.chatContainer.setToggleState("maximized");

            var msgHistory = message.ChatHistory[message.ChatHistory.length - 1];
            var participants = message.ChatPeople;
            var usId = isExternal ? thisExtUserId : thisUserId;
            if (msgHistory.UserId != usId) {
                // the message did not came from myself. Better erase the typing signal
                _this.removeTypingSignal();
            }

            if (message.ClientGuid && $("p[data-val-client-guid='" + message.ClientGuid + "']").length) {
                // in this case, this message is comming from the server AND the current user POSTED the message.
                // so he/she already has this message in the list. We DO NOT need to add the message.
                $("p[data-val-client-guid='" + message.ClientGuid + "']").removeClass("temp-message").removeAttr("data-val-client-guid");
            } else {
                var $messageP;

                if (msgHistory.Message.substr(0, 5) == "*-*¡¿") {
                    var fileName = msgHistory.Message.substr(msgHistory.Message.lastIndexOf("/") + 1).substr(22);//.substr(22) sin fecha                   
                    $messageP = $('<p>').text(fileName).addClass("sendFileA");
                    $messageP.css('background-image', ExtImg(fileName));
                    $messageP.click(function (e) {
                        AjaxDownloadFile(msgHistory.Message.substr(5));
                    });
                }
                else
                    $messageP = $("<p/>").html(msgHistory.Message);

                if (msgHistory.Message.indexOf("class='spanBuzz'") > 1) {
                    playBuzzSound();
                    ShakeWindow(_this.chatContainer.$window);
                    _this.chatContainer.$windowInnerContent.scrollTop(_this.chatContainer.$windowInnerContent[0].scrollHeight);
                }
                if (clientGuid) {
                    $messageP.attr("data-val-client-guid", clientGuid).addClass("temp-message").attr("id", "MyMessageChat");
                }
                if (clientGuid == undefined && message.UserFromId == thisUserId) {
                    $messageP.attr("data-val-client-guid", clientGuid).addClass("temp-message").attr("id", "MyMessageChat");
                }
                if (clientGuid == undefined && message.UserFromId != thisUserId) {
                    $messageP.attr("data-val-client-guid", clientGuid).addClass("temp-message").attr("id", "MessageFromChat");
                }
                if (IsZambaLinkMax()) {
                    $messageP.css({ width: "90%" });
                }
                linkify($messageP);
                emotify($messageP);

                // gets the last message to see if it's possible to just append the text
                var $lastMessage = $("div.chat-message:last", _this.chatContainer.$windowInnerContent);
                if ($lastMessage.length && $lastMessage.attr("data-val-user-from") == msgHistory.UserId) {
                    // we can just append text then
                    $messageP.appendTo($(".chat-text-wrapper", $lastMessage));
                }
                else {
                    // in this case we need to create a whole new message
                    var $chatMessage = $("<div/>").addClass("chat-message").attr("data-val-user-from", msgHistory.UserId);
                    $chatMessage.appendTo(_this.chatContainer.$windowInnerContent);


                    var $gravatarWrapper = (_this.opts.myUser.Id == message.UserFromId) ?
                        $("<div/>").addClass("chat-gravatar-wrapper").css("float", "right").attr("height", "30px").attr("width", "30px").appendTo($chatMessage) :
                        $("<div/>").addClass("chat-gravatar-wrapper").css("float", "left").appendTo($chatMessage);
                    var $textWrapper = $("<div/>").addClass("chat-text-wrapper").appendTo($chatMessage);

                    // add text              
                    $messageP.appendTo($textWrapper);

                    //Coloco nombre en chat 
                    var date = new Date(Date.parse(msgHistory.Date));
                    if (date == 'Invalid Date')// Trae la hora con formato incorrecto de controller
                        date = new Date(parseInt(msgHistory.Date.substring(6, msgHistory.Date.length - 2)));

                    var otherUserId = msgHistory.UserFromId == undefined ? msgHistory.UserId : msgHistory.UserFromId;
                    var messageUserFrom = (isCS || isExternal) ? GetExtChatUser(otherUserId) : _this.opts.chat.usersById[msgHistory.UserId];
                    var spanUsrName = messageUserFrom.Name;
                    spanUsrName = spanUsrName.indexOf("/") > -1 ? spanUsrName.substring(0, spanUsrName.indexOf("/")) : spanUsrName;
                    var txtName = ReduceName(spanUsrName, 10) + formatDateText(date);
                    var $span = $("<span/>").text(txtName).attr({ "id": "DateFromOtherChat", "title": txtName }).attr("data-placement", "bottom").tooltip();
                    $span.appendTo($messageP);

                    if (msgHistory.UserId == usId) {
                        $messageP.attr("data-val-client-guid", clientGuid).addClass("temp-message").attr("id", "MyMessageChat");
                    }
                    else {
                        $messageP.attr("id", "MessageFromChat");
                    }
                    var $img = messageUserFrom.Avatar;
                    if (message.UserFromId != usId) {
                        $("<img/>").attr("src", "data:image/jpg;base64," + $img).attr("height", "30px").attr("width", "30px").attr("id", "AvatarFromother").addClass("embebedIMG").appendTo($gravatarWrapper);
                    }
                    else {
                        $("<img/>").attr("src", "data:image/jpg;base64," + $img).attr("height", "30px").attr("width", "30px").attr("id", "AvatarUser").addClass("embebedIMG").appendTo($gravatarWrapper);
                    }
                }
                // scroll to the bottom
                _this.chatContainer.$windowInnerContent.scrollTop(_this.chatContainer.$windowInnerContent[0].scrollHeight);
            }
        };

        this.sendMsgToUsers = function (messageText) {
            /// <summary>Sends a message to the other user</summary>
            /// <param FullName="messageText" type="String">Message being sent</param>
            var _this = this;
            var generateGuidPart = function () {
                return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
            };
            var $thisWindow = $(_this.chatContainer.$window);
            var isGroup = ($thisWindow.attr("isGroup") == undefined || $thisWindow.attr("isGroup") == "false") ? 1 : 2;
            var chatId = $thisWindow.attr("chatId") == undefined ? 0 : $thisWindow.attr("chatId");
            var toUsers = isGroup == 2 ? 0 : $thisWindow.attr("id");
            var isExternal = $thisWindow.attr("isexternal") != undefined && $thisWindow.attr("isexternal") == "true";

            var clientGuid = (generateGuidPart() + generateGuidPart() + '-' + generateGuidPart() + '-' + generateGuidPart() + '-' + generateGuidPart() + '-' + generateGuidPart() + generateGuidPart() + generateGuidPart());
            _this.addMessage({
                UserFromId: _this.opts.myUser.Id,
                Message: messageText
            }, clientGuid);
            if (isExternal)
                _this.opts.adapter.serverExternal.sendMsgToUsers(toUsers, chatId, messageText, isGroup, clientGuid);
            else
                _this.opts.adapter.server.sendMsgToUsers(toUsers, chatId, messageText, isGroup, clientGuid);
        };

        this.sendTypingSignal = function () {
            /// <summary>Sends a typing signal to the other user</summary>
            var _this = this;
            //lo saque para que no joda
            //_this.opts.adapter.server.sendTypingSignal(_this.opts.otherUser.Id);
        };

        this.getToggleState = function () {
            var _this = this;
            return _this.chatContainer.getToggleState();
        };

        this.setVisible = function (value) {
            var _this = this;
            _this.chatContainer.setVisible(value);
        };
    }

    // Separate functionality from object creation
    ChatWindow.prototype = {
        init: function () {
            var _this = this;
            _this.chatContainer = $.chatContainer({
                title: _this.opts.userToName,
                canClose: true,
                initialToggleState: _this.opts.initialToggleState,
                onClose: function (e) {
                    //Reinicio valores cuando cierro ventana
                    if (chatMode == "compact") {
                        _this.opts.chat.chatContainer.$window.data({ "chatOpened": "", "open": false })
                            .css({ "display": "block", "top": e.$window.css("top"), "left": e.$window.css("left") });
                        $(".msgCount.noReadMsg").css("display", "none").text("");
                    }
                    OrderUsrLstByDate();
                    _this.opts.onClose(e);
                },
                onToggleStateChanged: function (toggleState) {
                    _this.opts.onToggleStateChanged(toggleState);
                }
            });

            _this.chatContainer.$textBox.keypress(function (e) {
                SendChatMsg(e, $(this));
            });
            $(".emoji-wysiwyg-editor").keypress(function (e) {
                SendChatMsg(e, $(this));
            });

            function SendChatMsg(e, $inp) {
                if (e.which == 13) {
                    e.preventDefault();
                    $(".emoji-menu.tether-element").fadeOut();
                    var buzz = _this.chatContainer.$window.data("buzz");
                    var zmbLnk = _this.chatContainer.$window.data("zmbLnk");
                    var zmbLnkDesc = _this.chatContainer.$window.data("zmbLnkDesc");
                    var sendZmbLnk = (zmbLnk != undefined && zmbLnk.length) ? true : false;
                    if ($inp.html() || buzz || sendZmbLnk) {
                        $inp.data("applyStyle", false);

                        var thisId = $(_this.chatContainer.$window).attr("id");// quito agregar participante una vez que se envio mensaje
                        var isExternal = $(_this.chatContainer.$window).attr("isexternal");
                        isExternal = isExternal != undefined && isExternal == "true";
                        $(_this.chatContainer.$window).attr("id", "removeAddUser");
                        if ($("#removeAddUser .chat-window-title .okAddUser").attr("id") != undefined) {
                            $("#addNewUser-" + ($("#removeAddUser .chat-window-title .okAddUser").attr("id")).replace("tildeAddUser-", "")).remove();
                            $("#removeAddUser .chat-window-title .okAddUser").remove();
                        }
                        //$("#removeAddUser .chat-window-title .addUser").remove(); ahora se puede agregar siempre un nuevo usuario al grupo
                        $(_this.chatContainer.$window).attr("id", thisId);
                        //Quito las X de usuarios agregados a chat (Se establece chat con los usuarios ya agregados)
                        $(_this.chatContainer.$window).children(".chat-window-title").children(".removeUser").each(function () {
                            if ($.inArray(($(this).attr("id")).replace("removeUser-", ""), thisId.split("/")) > -1)
                                $(this).remove();
                        });

                        var $val;
                        //Zumbido
                        if (buzz) {
                            $val = "<span class='spanBuzz'>- " + _this.opts.myUser.Name.split("/")[0] + " ha enviado una alerta -</span>";
                            _this.chatContainer.$window.data("buzz", false);
                        }
                        //Mensajes de chat
                        else if ($inp.text().indexOf("<zmbChat:") == 0) {
                            if ($inp.text().indexOf("<zmbChat:small>" == 0)) {
                                $val = "<span class='spanAddedUsrChat'>" + $inp.text().substring(15) + "</span>";
                            }
                        }
                        else
                            $val = $inp.html();
                        // Videos embebidos youtube
                        if ($val.indexOf(youtubePath) > 0) {
                            $val = GenerateYoutubeEmbed($val);
                        }
                        //Links de zamba
                        if (sendZmbLnk) {
                            _this.chatContainer.$window.data("zmbLnk", false);
                            _this.chatContainer.$window.data("zmbLnkDesc", false);
                            $val = "<div class='shareZmbLnk'><img class='shareZmbLnkImg' src='" + URLImage + "ChatJs/images/zamba.png' />" +
                                "<a target='_blank' href='" + zmbLnk + "' class='shareZmbLnkA'>" + zmbLnkDesc + "</a></div>";
                        }
                        //Url de emojis
                        if ($val.indexOf('<img src="' + thisDomain) > -1 && $val.indexOf("background:url('" + thisDomain) > -1) {
                            var emojiPath = "replaceToURLZmb/";// appZChat == "ZambaLink" ? "../" : "";
                            $val = $val.replaceAll(thisDomain + "/", emojiPath).replaceAll(thisDomain, emojiPath);
                        }
                        //var usId = isExternal ? thisExtUserId : thisUserId;
                        //var myUserData = GetChatUser(usId);
                        //if (myUserData == null) return;
                        //Compruebo si no deshabilito el chat
                        if (myUserChatInfo.Role == 0)
                            _this.sendMsgToUsers($val);
                        else {
                            var $append = _this.chatContainer.$windowContent.children(".chat-window-inner-content").children(".chat-message").last().children(".chat-text-wrapper")
                            $("<HR>").css("width", "2%)").css("align", "center").appendTo($append);
                            $("<p/>").text("Active el chat para poder enviar el mensaje")
                                .css({ "color": "#636262", "font-size": 10, "text-align": "center" }).appendTo($append);
                            $("<HR>").css({ "width": "2%", "align": "center" }).appendTo($append);
                        }
                        $inp.text('').trigger("autosize.resize");
                    }
                }
                else {
                    //Para estilos negrita/subrayado/italic
                    if (!$inp.data("applyStyle")) {
                        $inp.data("applyStyle", true);
                        var $fonts = $inp.parent().children(".editText").children(".editFont");
                        $inp.html(' ');
                        for (var i = 0; i <= $fonts.length - 1; i++) {

                            if ($($fonts[i]).data("active")) {
                                var img = $($fonts[i]).attr("src").substring($($fonts[i]).attr("src").lastIndexOf("/") + 1);
                                var type = img.substring(0, img.lastIndexOf("."));
                                if (type != 'image')
                                    $inp.html('<' + type + '>' + $inp.html() + '</' + type + '>');
                            }
                        }
                        //span para tamaño
                        var size = $($inp.parent().children(".editText").children("select")).val();
                        if (size != 12 && size >= 8)
                            $inp.html('<span style="font-size:' + size + 'px">' + $inp.html() + "</span>");
                        //label para color
                        var color = $($inp.parent().children(".editColor")).data("color");
                        if (color != undefined && color != "")
                            $inp.html('<label style="color:' + color + '">' + $inp.html() + "</label>");
                    }
                }
            }

            var mainWin = MainWinChat();
            if (mainWin.data("isGroup") == true || mainWin.data("isGroup") == "true") {
                var gn = mainWin.data("groupName");
                _this.chatContainer.setTitle(gn);
                _this.opts.isGroup = true;
                mainWin.data({ "isGroup": "false", "groupName": "" });
                $.ajax({
                    type: "GET",
                    async: false, //mejora rendimiento asincronico, analizar si no da error
                    url: URLBase + '/CreateEmptyChat',
                    data: { userId: _this.opts.myUser.Id, groupName: gn },
                    success: function (d) {
                        setAttrChatId(parseInt(d), gn);
                    },
                });
                function setAttrChatId(id, gn) {
                    //  GetChatGroups();
                    CreateGroupList(id, gn);
                    _this.chatContainer.$window.attr({ "isGroup": true, "id": "chatid" + id, "chatid": id });
                    _this.opts.chatid = id;
                    MainWinChat().data("openChat", id);
                }
            }
            else {
                _this.chatContainer.setTitle(_this.opts.otherUser == null ? "" : ReduceName(_this.opts.otherUser.Name, 18));
                var containerId = _this.opts.otherUser == null ? "" : _this.opts.otherUser.Id;
                if (isCS) containerId = "extchatid" + _this.opts.chatid;
                _this.chatContainer.setIdContainer(containerId);
            }
            if (_this.opts.isGroup == 2) {
                _this.chatContainer.$windowTitle.children(".addUser").show();
                _this.chatContainer.setIdContainer("chatid" + _this.opts.chatid);
                StyleDragDrop("chatid" + _this.opts.chatid);
            }
            else {
                StyleDragDrop(_this.opts.otherUser == null ? "" : _this.opts.otherUser.Id);
            }
            if (_this.opts.otherUser != null) {
                var usersIds = [_this.opts.myUser.Id, _this.opts.otherUser.Id];
                var isGroup = (_this.opts.isGroup == undefined || _this.opts.isGroup == false) ? 1 : 2;
                this.opts.adapter.server.getMessageHistory(usersIds, isGroup, function (messageHistory) {//1=Single-2=Group

                    if (_this.opts.typingText != "noHistory") {
                        if (messageHistory.length > 0) {

                            var $getMoreMsgBtn = $("<div/>").addClass("getMoreMsg") //boton traer mensajes anteriores
                                .attr("id", "getmoremsg" + (_this.chatContainer.$window).attr("id"))
                                .text("Traer mensajes anteriores")
                                .appendTo(_this.chatContainer.$windowInnerContent);

                            $getMoreMsgBtn.click(function (e) {
                                var id = (_this.chatContainer.$window).attr("id");

                                var msgNum = parseInt((_this.chatContainer.$window).attr("msgNum"));
                                (_this.chatContainer.$window).attr("msgNum", msgNum > 0 ? msgNum + 1 : 1);

                                var chatId = (_this.chatContainer.$window).attr("chatid") == undefined ? 0 : (_this.chatContainer.$window).attr("chatid").replace("chatid", "");
                                var result = GetMoreMsgHistory(_this.opts.myUser.Id + "/" + id, chatId, parseInt((_this.chatContainer.$window).attr("msgNum")));

                                var scrollPosition = _this.chatContainer.$windowInnerContent.scrollTop;
                                var getMsgNum = 5;//para saber cuando cargo todos los mensajes
                                var msgNum = getMsgNum * (parseInt((_this.chatContainer.$window).attr("msgNum")) + 1);
                                if (result.History.length < msgNum) {
                                    $("#getmoremsg" + id).remove();//elimino boton de traer mas
                                    (_this.chatContainer.$window).attr("msgNum", "");
                                }

                                (_this.chatContainer.$window).attr("id", "clearOldMsg");
                                ClearChatContentList();
                                var cantSend;
                                for (var i = 0; i < result.History.length; i++)
                                    _this.addMessage(result.History[i]);

                                (_this.chatContainer.$window).attr("id", id);
                                _this.chatContainer.$windowInnerContent.scrollTop(0);

                            });
                            for (var i = 0; i < messageHistory.length; i++) {//Sumo +3 x UTC
                                var date = new Date(messageHistory[i].DateTime);
                                messageHistory[i].DateTime = new Date(date.setHours(date.getHours() + 3)).toString();
                                _this.addMessage(messageHistory[i]);
                            }
                        }
                    }
                    else {
                        _this.opts.typingText = " esta escribiendo...";
                    }

                    if (chatMode != "compact" && !$._chatContainers.length >= 5)
                        _this.chatContainer.setVisible(true);

                    if (_this.opts.initialFocusState == "focused")
                        _this.chatContainer.$textBox.focus();

                    // scroll to the bottom
                    _this.chatContainer.$windowInnerContent.scrollTop(_this.chatContainer.$windowInnerContent[0].scrollHeight);

                    if (_this.opts.onReady)
                        _this.opts.onReady(_this);

                    if ($._chatContainers.length >= 5) {
                        //oculto la 3º ventana para colocar la nueva
                        for (var i = 0; i <= $._chatContainers.length - 1; i++) {

                            if ($._chatContainers[i].$window.attr("groupWin") == "restore") {
                                $._chatContainers[i].$window.attr("groupWin", "hidden");
                                $._chatContainers[i].$window.css("display", "none").css("right", "0px");
                            }
                            if ($._chatContainers[i].$window.css("right").indexOf("730") != -1) {
                                var $lastWin = $._chatContainers[i];
                                $lastWin.$window.css("display", "none").css("right", "0px");
                                $lastWin.$window.attr("groupWin", "hidden");
                                var $newWin = $._chatContainers[$._chatContainers.length - 1];
                                $newWin.$window.css("display", "block").css("right", "730px").css("bottom", "");
                                break;
                            }
                        }

                        if ($("#showMoreWindows").children(".chat-window-title").length != 1 &&
                            $("#showMoreWindows").children(".chat-window-title").attr("show") == "all") {
                            //Para que oculte las ventanas
                            $("#showMoreWindows").click();
                        }

                        var hiddenWin = ($._chatContainers.length - 4);
                        if ($("#showMoreWindows").length != 1) {
                            var $showMoreWindows = $("<div/>").addClass("chat-window").attr("id", "showMoreWindows").css("width", "230px").appendTo($("body")).css("right", "970px");
                            var $innerShowMoreWindows = $("<div/>").addClass("chat-window-title").html(hiddenWin + ((hiddenWin == 1) ? " Conversación" : " Conversaciones"))
                                .css("text-align", "center").appendTo($showMoreWindows);

                            $innerShowMoreWindows.click(function (event) {
                                var innerShowAttr = $innerShowMoreWindows.attr("show");
                                $innerShowMoreWindows.attr("show", ((innerShowAttr) == "all") ? "none" : "all").css("background-color", "#636262");

                                event.preventDefault();
                                var leftPosition = $(this).offset().left;
                                var j = 1;

                                for (var i = 0; i <= $._chatContainers.length - 1; i++) {
                                    var $win = $._chatContainers[i].$window;
                                    //ventana de contactos                      
                                    if ($win.attr("id") == _this.opts.myUser.Id) continue;

                                    if ($win.attr("groupWin") == "hidden")//($win.css("display")=="none")
                                    {
                                        $win.attr("groupWin", "restore");
                                        $win.css("display", "block").css("bottom", (30 * j + 1));
                                        $win.children(".chat-window-content").css("display", "none");
                                        $win.offset({ left: leftPosition });
                                        j++;
                                        $win.children(".chat-window-title").children(".minimized").css("background-image", "url(" + URLImage + "ChatJs/Images/restore.png)");
                                        $(this).text("Ocultar");
                                    }
                                    else {
                                        if ($win.attr("groupWin") == "restore") {
                                            $win.attr("groupWin", "hidden");
                                            $win.css("display", "none").css("bottom", (30 * j + 1));
                                            j++;
                                            $(this).text((j - 1) + ((j - 1 == 1) ? " Conversación" : " Conversaciones"));
                                        }
                                    }
                                }
                            });
                        }
                        else {
                            $("#showMoreWindows").children(".chat-window-title").html(hiddenWin + ((hiddenWin == 1) ? " Conversación" : " Conversaciones"));
                        }
                    }
                });

            }
            _this.setOnlineStatus(_this.opts.otherUser.Status);//_this.opts.userIsOnline
        },

        focus: function () {
            var _this = this;
            _this.chatContainer.$textBox.focus();
        },

        showTypingSignal: function (user) {
            /// <summary>Adds a typing signal to this window. It means the other user is typing</summary>
            /// <param FullName="user" type="Object">the other user info</param>
            var _this = this;
            if (_this.$typingSignal)
                _this.$typingSignal.remove();
            _this.$typingSignal = $("<p/>").addClass("typing-signal").text(user.Name + _this.opts.typingText);
            _this.chatContainer.$windowInnerContent.after(_this.$typingSignal);
            if (_this.typingSignalTimeout)
                clearTimeout(_this.typingSignalTimeout);
            _this.typingSignalTimeout = setTimeout(function () {
                _this.removeTypingSignal();
            }, 5000);
        },

        removeTypingSignal: function () {
            /// <summary>Remove the typing signal, if it exists</summary>
            var _this = this;
            if (_this.$typingSignal)
                _this.$typingSignal.remove();
            if (_this.typingSignalTimeout)
                clearTimeout(_this.typingSignalTimeout);
        },

        setOnlineStatus: function (userIsOnline) {
            var _this = this;
            _this.chatContainer.$windowTitle.removeClass("offline");
            _this.chatContainer.$windowTitle.removeClass("online");
            _this.chatContainer.$windowTitle.removeClass("busy");
            _this.chatContainer.$windowTitle.removeClass("dontdisturb");
            if (chatMode == "normal") {
                switch (userIsOnline) {
                    case 0:
                        _this.chatContainer.$windowTitle.addClass("offline");
                        break;
                    case 1:
                        _this.chatContainer.$windowTitle.addClass("online");
                        break;
                    case 2:
                        _this.chatContainer.$windowTitle.addClass("busy");
                        break;
                    case 3:
                        _this.chatContainer.$windowTitle.addClass("dontdisturb");
                        break;
                }
            }
        }
    };

    // The actual plugin
    $.chatWindow = function (options) {
        var chatWindow = new ChatWindow(options);
        chatWindow.init();
        MainWinChat().data("open", false);
        return chatWindow;
    };
})(jQuery);

// CHAT
(function ($) {
    // creates a chat user-list
    function Chat(options) {
        var _this = this;

        // Defaults:
        _this.defaults = {
            user: null,
            adapter: null,
            titleText: 'Chat',
            emptyRoomText: "No hay usuarios",
            typingText: " esta escribiendo...",
            useActivityIndicatorPlugin: true,
            playSound: true
        };

        //Extending options:
        _this.opts = $.extend({}, _this.defaults, options);

        //Privates:
        _this.$el = null;

        // there will be one property on this object for each user in the chat
        // the property FullName is the other user id (toStringed)
        _this.chatWindows = new Object();
        _this.lastMessageCheckTimeStamp = null;
        _this.chatContainer = null;
        _this.usersById = {};
    }

    // Separate functionality from object creation
    Chat.prototype = {
        init: function () {
            var _this = this;
            //var mainChatWindowChatState = _this.readCookie("main_window_chat_state");
            //if (!mainChatWindowChatState)
            //    mainChatWindowChatState = "maximized";
            var mainChatWindowChatState = "maximized";
            // will create user list chat container
            _this.chatContainer = $.chatContainer({
                title: _this.opts.titleText,
                showTextBox: false,
                canClose: false,
                initialToggleState: mainChatWindowChatState,
                onCreated: function (container) {
                    container.$window.attr("id", _this.opts.user != null ? _this.opts.user.Id : "");//id de contenedor principal
                    if (chatMode == "compact")
                        container.$window.height(heightCompact).width(widthCompact);

                    $(container.$window).click(function () {
                        $("#filterByUser").select().focus();
                    });
                    if (chatMode == "normal") {
                        var $mainStatus = $("<div/>").addClass("mainStatus").attr("title", "Cambiar estado").appendTo($("#" + container.$window.attr("id") + " .chat-window-title #containerUserName"))
                            .attr("id", "mainStatusId").css('background-image', 'url("' + URLImage + 'ChatJs/images/chat-online.png")');
                        $mainStatus.tooltip();

                        $("#mainStatusId").click(function () {
                            _this.chatContainer.$windowTitle.data("viewChangeStatus", true);
                            $("#configId").click();
                            _this.chatContainer.$windowTitle.data("viewChangeStatus", false);
                            $("#changeAvatarId").remove();
                        });
                    }

                    $("<div/>").attr("id", "changeStatusDiv").appendTo($("#" + container.$window.attr("id"))).click(function () {
                        _this.opts.adapter.server.changeStatus(_this.opts.user.Id, $("#changeStatusDiv").data("status"));
                    });
                    $("<div/>").attr("id", "changeAvatarDiv").appendTo($("#" + container.$window.attr("id"))).click(function () {
                        _this.opts.adapter.server.changeAvatar(_this.opts.user.Id, $("#changeAvatarDiv").data("avatar"));
                    });
                    $("<div/>").attr("id", "changeNameDiv").appendTo($("#" + container.$window.attr("id"))).click(function () {
                        _this.opts.adapter.server.changeName(_this.opts.user.Id, $("#changeNameDiv").data("name"));
                    });
                    $("<div/>").attr("id", "changeGroupNameDiv").appendTo($("#" + container.$window.attr("id"))).click(function () {
                        _this.opts.adapter.server.changeGroupName(thisUserId, $("#changeGroupNameDiv").data("chatId"), $("#changeGroupNameDiv").data("name"));
                    });

                    var $noReadUserHistoryDiv = $("<div/>").attr("id", "noReadUserHistoryDiv").appendTo($("#" + container.$window.attr("id")));
                    $noReadUserHistoryDiv.click(function () {
                        var minimizeAllWin = $noReadUserHistoryDiv.data("minimizeAllWin") == true;
                        // Imagen minimizar/restaurar
                        _this.chatContainer.$windowTitle.children(".minimized").css("background-image", (minimizeAllWin) ?
                            "url(" + URLImage + "ChatJs/Images/restore.png)" : "url(" + URLImage + "ChatJs/Images/minimize.png)");
                        //Color rojo a main title

                        $(_this.chatContainer.$windowTitle).css("background-color", (minimizeAllWin) ? "rgb(255, 68, 68) !important" : "rgba(103, 103, 103, 0.91) !important");

                        $(".chat-window").css("background-color", (minimizeAllWin) ? "rgba(255, 255, 255, 0) !important" : "rgba(255, 255, 255, 0)");
                        $(".chat-window").css("border-color", (minimizeAllWin) ? "rgba(255, 255, 255, 0)" : "rgba(202, 202, 202, 0.3)");

                        if (minimizeAllWin)
                            $hideShowWindows.click();
                        else
                            _this.loadWindows();
                    });

                    var $openGroupDiv = $("<div/>").attr("id", "openGroupDiv").appendTo($("#" + container.$window.attr("id")));
                    $openGroupDiv.click(function () {
                        //Apertura de grupo
                        $("div#loadingChatContent").fadeIn();
                        var chatId = $openGroupDiv.data("chatId");
                        //Compruebo si ya esta abierto para no abrirlo dos veces                   
                        var method; var ajaxData;
                        method = '/OpenGroupChatById';
                        ajaxData = { chatId: chatId };
                        $.ajax({
                            type: "GET",
                            async: false,
                            url: URLBase + method,
                            data: ajaxData,
                            success: function (result) {
                                _this.client.sendMsgToUsers(result.GroupChat, true);
                            },
                        });
                        AdjustChatSize();
                        $("div#loadingChatContent").fadeOut();
                    });
                    var openExtGroupDiv = $("<div/>").attr("id", "openExtGroupDiv").appendTo($("#" + container.$window.attr("id")))
                        .click(function () {
                            //Apertura de grupo
                            $("div#loadingChatContent").fadeIn();
                            var chatId = $(this).data("chatId");
                            //Compruebo si ya esta abierto para no abrirlo dos veces                   
                            var method; var ajaxData;
                            method = 'chat/OpenGroupChatById';
                            ajaxData = { chatId: chatId };
                            $.ajax({
                                type: "GET",
                                async: false,
                                url: colServer + method,
                                data: ajaxData,
                                success: function (result) {
                                    if (!result.GroupChat.length && !result.GroupChat.ChatHistory.length) {
                                        var hist = new Object();
                                        hist.ChatId = chatId;
                                        hist.Date = new Date();
                                        hist.Id = 0;
                                        hist.Message = "Nueva conversacion";
                                        hist.UserId = thisExtUserId;
                                        result.GroupChat.ChatHistory.push(hist);
                                    }
                                    result.GroupChat.isExternal = true;
                                    _this.client.sendMsgToUsers(result.GroupChat, (result.GroupChat.ChatType == 2));
                                },
                            });
                            AdjustChatSize();
                            $("div#loadingChatContent").fadeOut();
                        });


                    var $hideShowWindows = $("<div/>").attr("id", "hideShowWindows").appendTo($("#" + container.$window.attr("id")));
                    $hideShowWindows.click(function () {
                        $(".chat-window").each(function () {
                            {
                                if (this.id != _this.opts.user.Id) {
                                    $(this).children(".chat-window-content").fadeOut();
                                    $(this).children(".chat-window-title").children(".minimized").css("background-image", 'url("' + URLImage + 'ChatJs/images/restore.png")');
                                }
                            }
                        })
                    });
                    var $refreshAvatar = $("<div/>").attr("id", "refreshAvatarId").appendTo($("#" + container.$window.attr("id")));

                    $refreshAvatar.click(function () {
                        RefreshAvatar();
                    });
                    //Avatar en contenedor principal
                    function RefreshAvatar() {
                        var user = GetChatUser(_this.opts.user.Id);
                        _this.opts.user.Avatar = user.Avatar;
                    }
                    if (myUserChatInfo.Avatar != "") {
                        _this.opts.user.Avatar = myUserChatInfo.Avatar;
                    }
                    else {
                        RefreshAvatar();
                    }
                    var $mainAvatar = $("<img/>").addClass("mainAvatar").attr("title", "Cambiar avatar")
                        .attr("data-placement", "bottom").appendTo($("#" + container.$window.attr("id") + " .chat-window-title #containerUserName"))
                        .attr("src", "data:image/jpg;base64," + _this.opts.user.Avatar).attr("id", "mainAvatarId").tooltip();
                    $("#mainAvatarId").click(function () {
                        $("#configId").click();
                        $("#changeAvatarId").click();
                    });
                    if (chatMode == "compact") {
                        $mainAvatar.css({
                            "border": "2px solid rgb(84, 179, 0)",
                            height: "30px",
                            width: "30px",
                            "background-size": "40px 40px"
                        });
                        $(".mainAvatar").parents(".chat-window-title").css("padding", "2px");
                        expandImg($mainAvatar);
                    }
                    if (!container.$windowInnerContent.html()) {
                        var $loadingBox = $("<div/>").addClass("loading-box").appendTo(container.$windowInnerContent);
                        //if (_this.opts.useActivityIndicatorPlugin)
                        //    $loadingBox.activity({ segments: 8, width: 3, space: 0, length: 3, color: '#666666', speed: 1.5 });
                    }
                },
                onToggleStateChanged: function (toggleState) {
                    //    _this.createCookie("main_window_chat_state", toggleState);
                }
            });

            // the client functions are functions that must be called by the chat-adapter to interact
            // with the chat
            _this.client = {
                sendMsgToUsers: function (message, noAlerts) {//noAlerts = no mostrar mensajes sin leer
                    if (!message.ChatHistory.length) return;
                    isExternal = (message.isExternal != undefined && message.isExternal) ? true : false;
                    // Si esta el chat desactivado no recibe mensajes
                    if ($("#" + _this.opts.user.Id).children(".chat-window-title").data("switch") == true ||
                        $("#changeStatusDiv").data("status") == "0")
                        return;
                    var isGroup = noAlerts != undefined ? noAlerts : (message.ChatType == 2 ? true : false);
                    var validUser = false;
                    var msgHistoryUser;
                    var participants = []; //= message.ChatPeople;
                    var msgHistory = message.ChatHistory[message.ChatHistory.length - 1];
                    for (var i = 0; i < message.ChatPeople.length; i++) {
                        if (message.ChatPeople[i].UserId != msgHistory.UserId) {
                            participants.push(message.ChatPeople[i]);
                        }
                        else {
                            msgHistoryUser = message.ChatPeople[i];
                        }
                    }
                    participants.push(msgHistoryUser);//lo coloco a lo ultimo asi se que es quien lo envia

                    for (var i = 0; i < participants.length; i++) {
                        if (participants[i] != undefined && participants[i].UserId == _this.opts.user.Id) {
                            validUser = true;
                            break;
                        }
                    }
                    if (validUser || isExternal) {
                        //con el tema de los grupos lo saque y lo coloque abajo, si ult conversacion era mia no lo habria                                          
                        var participantsIds = [];
                        var usersSorted = [];
                        $.each(_this.chatWindows, function (index, value) {
                            //Agregado esto porque se duplicaban las ventanas grupales igualo objeto code con id DOM
                            var id = $(value.chatContainer.$window).attr("id");
                            if (id != undefined) {
                                if (index != id) {
                                    _this.chatWindows[id] = this;
                                    delete _this.chatWindows[index];
                                }
                                var usersByWindows = id;
                                usersSorted.push(usersByWindows);
                            }
                        });

                        for (var i = 0; i < participants.length; i++) {
                            if (participants[i] != undefined)
                                participantsIds.push(participants[i].UserId.toString());
                        }

                        var myUser = participantsIds.indexOf(_this.opts.user.Id.toString());//si no me tiene a mi no debe hacer nada
                        if (myUser > -1) { participantsIds.splice(myUser, 1); }

                        participantsIds = participantsIds.sort(function (a, b) { return a - b });

                        var existChat;
                        var chatName = "";
                        for (var i = 0; i < participants.length; i++) {
                            if (participants[i] != undefined) {
                                if (participants[i].UserId != _this.opts.user.Id)
                                    chatName += participants[i].UserId + "/";
                            }
                        }
                        chatName = chatName.substring(0, chatName.length - 1);
                        if (isGroup) chatName = "chatid" + message.Id;
                        if (isExternal || isCS) chatName = "extchatid" + message.Id;
                        var usId = (isExternal || isCS) ? thisExtUserId : thisUserId;
                        //Cuando otro envia mensaje                        
                        var chatOpened = MainWinChat().data("chatOpened");
                        var opening = MainWinChat().data("open");
                        MainWinChat().data("open", false);
                        if (chatMode == "compact" && !opening) {
                            var $noReadMsg;
                            if (!isGroup) {
                                var msgObj = message.ChatHistory[message.ChatHistory.length - 1];
                                var $lstUsDiv = (isExternal || isCS) ? $(".user-list-item[extchatid=" + msgObj.ChatId + "]") : $("div[id='list-" + chatName + "']");
                                var senderMsg = (isExternal || isCS) ? GetExtChatUser(msgObj.UserId) : _this.usersById[msgObj.UserId];
                                //apertura ventana simple//&& chatOpened == ""
                                if (!isExternal && !$(".chat-window#" + participantsIds.toString())) {
                                    var $lstDiv = $("div[id='list-" + chatName + "']");
                                    $lstDiv.attr("date", new Date().toString());
                                    $noReadMsg = $lstDiv.children(".msgCount");
                                    $noReadMsg.text((parseInt($noReadMsg.text()) || 0) + 1).fadeIn();
                                    IncreaseNoReadMsg();
                                    OrderUsrLstByDate();
                                    return;
                                }
                                var dataCO = MainWinChat().data("chatOpened");
                                if (dataCO != undefined && dataCO.length) {//Si tengo una conversacion abierta
                                    var isOpened = (isCS || isExternal) ? (dataCO == "chat" + message.Id) : (dataCO == participantsIds.toString());
                                    if (isOpened) {
                                        $lstUsDiv.attr("date", new Date().toString());
                                        OrderUsrLstByDate();
                                        existChat = true;
                                        if (IsZambaLinkMin()) {
                                            toastrInTitle(senderMsg.Name, msgObj.Message);
                                        }
                                        //Tengo la ventana de la conversacion abierta- si dejo return no envia
                                    }
                                    else {
                                        //tengo abierto a otra persona
                                        IncreaseNoReadMsg();
                                        toastrInTitle(senderMsg.Name, msgObj.Message);
                                        //var $lstUsDiv = $("div[id='list-" + chatName + "']");
                                        $lstUsDiv.attr("date", new Date().toString());
                                        $noReadMsg = $lstUsDiv.children(".msgCount");
                                        $noReadMsg.text((parseInt($noReadMsg.text()) || 0) + 1).fadeIn();
                                        OrderUsrLstByDate();
                                        return;
                                    }
                                }
                                else {//No abri ninguna ventana, me hablan   
                                    if (senderMsg.Id != usId) {
                                        toastrInTitle(senderMsg.Name, msgObj.Message);
                                        // var $lstUsDiv = isCS ? $(".user-list-item[extchatid=" + msgObj.ChatId+ "]") : $("div[id='list-" + chatName + "']");
                                        $lstUsDiv.attr("date", new Date().toString());
                                        $noReadMsg = $lstUsDiv.children(".msgCount");
                                        $noReadMsg.text((parseInt($noReadMsg.text()) || 0) + 1).fadeIn();
                                        if (msgObj.Message.indexOf("class='spanBuzz'") > -1) {
                                            playBuzzSound();
                                            ShakeWindow($(".chat-window"));
                                        }
                                    }
                                    OrderUsrLstByDate();
                                    return;
                                }
                            } else {
                                //apertura grupo
                                if ($(".chat-window[chatid=" + message.Id + "]").length) {
                                    existChat = true;//Estoy hablando con ese grupo
                                }
                                else {
                                    var $lstGdiv = $(".user-list-item[chatid=" + chatName.replace("chatid", "") + "]");
                                    if (!$lstGdiv.length) {

                                        if (taskChat) {

                                            GetChatGroupsForum();
                                        }
                                        else {
                                            GetChatGroups();

                                        }


                                    }
                                    $lstGdiv.attr("date", new Date().toString());
                                    var lastMsg = message.ChatHistory[message.ChatHistory.length - 1];
                                    if (lastMsg.UserId != usId) {
                                        $noReadMsg = $lstGdiv.children(".msgCount");
                                        $noReadMsg.text((parseInt($noReadMsg.text()) || 0) + 1).fadeIn();
                                        IncreaseNoReadMsg();
                                        var senderMsg = _this.usersById[lastMsg.UserId];
                                        var usName = senderMsg.Name.indexOf("/") > -1 ? senderMsg.Name.substring(0, senderMsg.Name.indexOf("/")) : senderMsg.Name;
                                        toastrInTitle("Nuevo Mensaje " + message.ChatName, usName);
                                        OrderUsrLstByDate();
                                        if (lastMsg.Message.indexOf("class='spanBuzz'") > -1) {
                                            playBuzzSound();
                                            ShakeWindow($(".chat-window"));
                                        }
                                    }
                                    OrderUsrLstByDate();
                                    return;
                                }
                            }
                        }

                        if (!existChat) {
                            var isGroupN = isGroup ? 2 : 1;
                            _this.createNewChatWindowGroup(participants, isGroupN, message.Id, isExternal);
                            GetHistory(_this.opts.user.Id + "/" + chatName, isGroupN, message.ChatHistory, isExternal);

                            var $tit = $(_this.chatWindows[chatName].chatContainer.$windowTitle);
                            $tit.find(".addUser").css("display", "none");

                            //Coloco attr chatid y chatName en grupos
                            if (message.Id != undefined) {
                                $(_this.chatWindows[chatName].chatContainer.$window).attr("chatid", message.Id).attr("chatname", message.ChatName);
                                if (isGroup) {

                                    var $tit = _this.chatWindows[chatName].chatContainer.$windowTitle;
                                    $tit.find("#containerUserName").css("display", "none");

                                    var $containerUsersGroup = $tit.find("#containerUsersGroup")
                                        .css("display", "inline").attr("value", message.ChatName);

                                    var $infoUsersGroup = $("<span/>").attr("id", "infoUsersGroup")
                                        .insertBefore($containerUsersGroup.parent().children(".addUser"))
                                        .addClass("info")
                                        .css("margin-left", "3px").attr("data-placement", "bottom").attr("title", "Opciones").tooltip()
                                        .click(function (e) { InfoUsersGroupChat(e); });
                                }
                            }
                            // Si grupo cambia el titulo y coloca botón para mostrar/ocultar lista de participantes
                            if (isGroup) {
                                $tit.attr("id", "windowTitle");
                                if (!$(_this.chatWindows[chatName].chatContainer.$windowTitle).find(".showAllUsers").length) {
                                    var $showAllUsersButton = $("<div/>").addClass("showAllUsers")
                                        .attr("title", "Participantes")
                                        .attr("alt", "Agregar nuevo integrante")
                                        .attr("data-placement", "bottom")
                                        .css("display", "block").appendTo("#windowTitle>#containerUserName");

                                    $showAllUsersButton.click(function (e) {
                                        $showAllUsersButton.attr("modeDisplay", ($showAllUsersButton.attr("modeDisplay")) == "none" ? "block" : "none");

                                        var modeDisplay = $showAllUsersButton.attr("modeDisplay");
                                        if (modeDisplay == undefined) modeDisplay = "none";
                                        $(_this.chatWindows[chatName].chatContainer.$windowTitle).attr("id", "windowTitle");
                                        var usersInChat = 0;
                                        $("#windowTitle .addUserTxt").each(function (indice, elemento) {
                                            usersInChat++;
                                            elemento.style.display = modeDisplay;
                                        });
                                        //Cuando expande la lista de usuarios
                                        if (modeDisplay == "block") {
                                            if ($((this.parentElement)).children("#spanNum").length)
                                                $((this.parentElement)).children("#spanNum").html("");
                                        }
                                        else {
                                            if ($((this.parentElement)).children("#spanNum").length)
                                                $((this.parentElement)).children("#spanNum").html(" +" + usersInChat);
                                            else {
                                                if (usersInChat > 0)
                                                    var $spanNum = $("<span/>").html(" +" + usersInChat)
                                                        .attr("id", "spanNum").appendTo("#windowTitle #containerUserName");
                                            }
                                        }
                                        $("#windowTitle").attr("id", "tit" + (_this.opts.user.Id + "/" + chatName).replace(new RegExp("/", 'g'), "-"));
                                        if (chatMode == "compact") {
                                            var titHeigth = (heightCompact - $(this).parents(".chat-window-title").height() - 60) + "px";//60xtextarea
                                            $(this).parents(".chat-window").find(".chat-window-inner-content").css({ height: titHeigth, "max-heigth": titHeigth });
                                        }
                                    });
                                    //Para que me oculte los usuarios del chat cuando me lo abre
                                    $showAllUsersButton.click();
                                }

                                //  var lst = document.getElementById("gID" + chatName.split("/").sort(function (a, b) { return a - b }));
                                var lst = $(".chat-window[chatid=" + message.Id + "]");
                                if (lst.attr("id") == undefined || lst.attr("id") == "") lst.attr("id", "chatid" + message.Id);

                                var $changeNameGroup = $("<span/>").addClass("pencil changeNameGroupChat")
                                    .attr("title", "Cambiar nombre").attr("data-placement", "bottom")
                                    .css({ "float": "right", "display": "none", "right": "35px" }).appendTo("#windowTitle>#containerUserName");
                                $changeNameGroup.click(function (e) { ChangeNameGroupChat(e); return false; });
                                $changeNameGroup.tooltip();

                                var $leaveGroup = $("<span/>").addClass("exit leaveGroupChat")
                                    .attr("title", "Abandonar").attr("data-placement", "bottom")
                                    .attr("chatId", $(lst).length ? $(lst).attr("chatid") : "")
                                    .css({ "color": "white", "float": "right", "display": "none", "right": "35px" }).appendTo("#windowTitle>#containerUserName");
                                $leaveGroup.click(function (e) { LeaveChatGroup($(e.target).attr("chatid"), e); return false; });
                                $leaveGroup.tooltip();

                                var $changeNameGroupCancel = $("<span/>").addClass("glyphicon glyphicon-remove confirmChat")
                                    .attr("title", "Cancelar").attr("data-placement", "bottom")
                                    .css({ "color": "rgb(251, 167, 167)", "float": "right", "display": "none", "top": "7px", "margin-left": "7px", "right": "2px" }).appendTo("#windowTitle")
                                    .click(function (e) { ChangeNameGroupCancel(e); return false; }).tooltip();
                                var $changeNameGroupOk = $("<span/>").addClass("glyphicon glyphicon-ok confirmChat")
                                    .attr("title", "Cambiar nombre").attr("data-placement", "bottom")
                                    .attr("chatId", $(lst).length ? $(lst).attr("chatid") : "")
                                    .css({ "color": "rgb(200, 255, 200)", "float": "right", "display": "none", "top": "7px", "margin-left": "5px" }).appendTo("#windowTitle")
                                    .click(function (e) { ChangeNameGroupOk($(e.target).attr("chatid"), e); return false; }).tooltip();

                                $(_this.chatWindows[chatName].chatContainer.$windowTitle).attr("id", " ");
                            }
                            SetFunctionUpload(chatName, (isCS || isExternal));

                        } else {
                            // se repetian los mensajes sin esta condición, se escribia dos veces lo mismo
                            if (msgHistory.UserId == usId)
                                return;

                            $.each(_this.chatWindows, function (index, value) {
                                //if (JSON.stringify((value.chatContainer.$window).attr("id").split("/").sort()) ==
                                //                                    JSON.stringify(chatName.split("/").sort())) {
                                if (value.chatContainer.$window.attr("id") == chatName) {
                                    if ((_this.chatContainer.$window).data("clearChat") == true) {
                                        $(value.chatContainer.$windowInnerContent).children(".chat-message").each(function () {
                                            $(this).remove();
                                        });
                                        $(value.chatContainer.$windowInnerContent).children(".spanUserName").each(function () {
                                            $(this).remove();
                                        });
                                        var forCicle = message.ChatHistory.length;
                                        for (var i = 0; i < forCicle; i++) {
                                            if (i != 0) {
                                                delete message.ChatHistory[message.ChatHistory.length];
                                                message.ChatHistory.length -= 1;
                                            }
                                            else {
                                                message.ChatHistory.reverse();
                                            }
                                            value.addMsgUsers(message);
                                        }
                                    }
                                    else {
                                        if (value.chatContainer.$window.attr("groupwin") == "hidden") {
                                            //Color verde, no leidos
                                            $("#showMoreWindows").children().css("background-color", "rgb(51, 153, 51)");
                                            value.chatContainer.$windowTitle.css("background-color", "rgb(51, 153, 51)");
                                        }
                                        value.addMsgUsers(message);
                                    }
                                    //Coloco boton de minimizar porque se restaura
                                    value.chatContainer.$windowTitle.children(".minimized").css("background-image", "url(" + URLImage + "ChatJs/Images/minimize.png)");
                                }
                            });
                            //_this.chatWindows[chatName].addMsgUsers(message);
                        }

                        if (_this.opts.playSound)
                            _this.playSound(URLImage + "ChatJs/sounds/chat");
                    }

                    function GetHistory(participantsIds, chatType, chatHistory, isExternal) {
                        var isGroup = (chatType == 2 || chatType == "true" || (chatType == true && chatType != 1));
                        if (!isGroup) {
                            if (isExternal)
                                ProcessHistory(chatHistory);
                            else {
                                $.ajax({
                                    type: "GET",
                                    async: false,
                                    url: URLBase + '/getmessagehistory',
                                    data: { usersId: participantsIds, chatType: chatType },
                                    success: function (result) {
                                        ProcessHistory(result.History);
                                    },
                                });
                            }
                        }
                        else {
                            ProcessHistory(chatHistory);
                        }

                        function ProcessHistory(result) {
                            _this.chatWindows[chatName].chatContainer.$windowInnerContent.html("");
                            if (result.length == 5) {
                                var $getMoreMsgBtn = $("<div/>").addClass("getMoreMsg") //boton traer mensajes anteriores
                                    .attr("id", "getmoremsg" + ((_this.chatWindows[chatName].chatContainer.$window).attr("id")).replace(new RegExp("/", 'g'), "-"))
                                    .text("Traer mensajes anteriores")
                                    .appendTo(_this.chatWindows[chatName].chatContainer.$windowInnerContent)
                                    .click(function (e) {
                                        GetMoreMsgBtnClick(e);
                                    });
                            }
                            for (var i = 0; i < result.length; i++) {
                                var r = result[i];
                                if (r.Avatar == undefined) {
                                    r.UserFromId = r.UserId;
                                    r.DateTime = r.Date;
                                    r.Avatar = TempUserInfo(r.UserFromId, isExternal).Avatar;
                                    r.isExternal = isExternal;
                                }
                                AddMsg(r);
                            }
                        }

                        function GetMoreMsgBtnClick(e) {
                            var id = (_this.chatWindows[chatName].chatContainer.$window).attr("id");
                            id = id.indexOf("chatid") > -1 ? 0 : id;
                            var msgNum = parseInt((_this.chatWindows[chatName].chatContainer.$window).attr("msgNum"));
                            (_this.chatWindows[chatName].chatContainer.$window).attr("msgNum", msgNum > 0 ? msgNum + 1 : 1);

                            var myUserId = _this.opts.user.Id;
                            var chatId = (_this.chatWindows[chatName].chatContainer.$window).attr("chatid");
                            chatId = chatId == undefined ? 0 : chatId;

                            var result = GetMoreMsgHistory(myUserId + "/" + id, chatId, parseInt((_this.chatWindows[chatName].chatContainer.$window).attr("msgNum")));

                            LoadMoreMsgHistory(result);

                        }

                        function LoadMoreMsgHistory(result) {
                            var $cW = (_this.chatWindows[chatName].chatContainer.$window);
                            var thisId = $cW.attr("id");

                            var getMsgNum = 5;//para saber cuando cargo todos los mensajes
                            var msgNum = getMsgNum * (parseInt($cW.attr("msgNum")) + 1);
                            if (result.History.length < msgNum) {
                                $("#getmoremsg" + replaceAll(thisId, "/", "-")).remove();//.replace("/", "-")).remove();//elimino boton de traer mas
                                function replaceAll(text, search, newstring) {
                                    return text.replace(new RegExp(search, 'g'), newstring);
                                }
                                $cW.attr("msgNum", "");
                            }
                            $cW.attr("id", "clearOldMsg");
                            ClearChatContentList();

                            var cantSend;
                            for (var i = 0; i < result.History.length; i++)
                                AddMsg(result.History[i]);

                            $cW.attr("id", thisId);
                            _this.chatWindows[chatName].chatContainer.$windowInnerContent.scrollTop(0);
                            if (taskChat)
                                $(".temp-message, .getMoreMsg").css({ "width": "90%" });
                        }
                    }

                    function AddMsg(message) {
                        var $messageP;
                        var usId = message.isExternal != undefined && message.isExternal ? thisExtUserId : thisUserId;
                        if (message.Message.substr(0, 5) == "*-*¡¿") {
                            var fileName = message.Message.substr(message.Message.lastIndexOf("/") + 1).substr(22);//.substr(22) sin fecha

                            $messageP = $('<p>').text(fileName).addClass("sendFileA");
                            $messageP.css('background-image', ExtImg(fileName));
                            $messageP.click(function (e) {
                                AjaxDownloadFile(message.Message.substr(5));
                            });
                        }
                        else {
                            $messageP = $("<p/>").html(message.Message);
                        }

                        var $lastMessage = $("div.chat-message:last", _this.chatWindows[chatName].chatContainer.$windowInnerContent);
                        if ($lastMessage.length && $lastMessage.attr("data-val-user-from") == message.UserFromId) {

                            if (message.UserFromId == usId) {
                                $messageP.addClass("temp-message").attr("id", "MyMessageChat");
                            }

                            if (message.UserFromId != usId) {
                                $messageP.addClass("temp-message").attr("id", "MessageFromChat");
                            }

                            $messageP.appendTo($(".chat-text-wrapper", $lastMessage));
                        }
                        else {
                            var $chatMessage = $("<div/>").addClass("chat-message").attr("data-val-user-from", message.UserFromId);
                            $chatMessage.appendTo(_this.chatWindows[chatName].chatContainer.$windowInnerContent);

                            var $gravatarWrapper = (usId == message.UserFromId) ?
                                $("<div/>").addClass("chat-gravatar-wrapper").css("float", "right").attr("height", "30px").attr("width", "30px").appendTo($chatMessage) :
                                $("<div/>").addClass("chat-gravatar-wrapper").css("float", "left").appendTo($chatMessage);
                            var $textWrapper = $("<div/>").addClass("chat-text-wrapper").appendTo($chatMessage);

                            if (message.UserFromId == usId) {
                                $messageP.addClass("temp-message").attr("id", "MyMessageChat");
                            }

                            if (message.UserFromId != usId) {
                                $messageP.addClass("temp-message").attr("id", "MessageFromChat");
                            }
                            $messageP.appendTo($textWrapper);

                            // var messageUserFrom = (isExternal || isCS) ? GetExtChatUser(message.UserFromId) : GetChatUser(message.UserFromId);
                            //Se optimiza velocidad usando info temporal, analizar actualizar estado y avatar
                            var messageUserFrom = TempUserInfo(message.UserFromId, (isExternal || isCS));
                            //Coloco nombre en chat
                            var date = isDate(message.DateTime) ? message.DateTime : new Date(parseInt(message.DateTime.substring(6, message.DateTime.length - 2)));
                            if (usId != message.UserFromId) {
                                var spanUsrName = messageUserFrom.Name.indexOf("/") > -1 ? messageUserFrom.Name.substring(0, messageUserFrom.Name.indexOf("/")) : messageUserFrom.Name;
                                var $span = $("<span/>").text(ReduceName(spanUsrName, 10) + formatDateText(date)).attr("id", "DateFromOtherChat");
                                $span.appendTo($messageP);
                            }
                            else {
                                $span = $("<span/>").text(formatDateText(date)).attr({ "id": "DateFromOtherChat", "title": formatDateText(date) }).attr("data-placement", "bottom").tooltip();
                                $span.appendTo($chatMessage.find(".temp-message"));
                            }
                            var $img = messageUserFrom.Avatar;
                            if (message.UserFromId != usId) {
                                $("<img/>").attr("src", "data:image/jpg;base64," + $img).attr("height", "30px").attr("width", "30px").attr("id", "AvatarFromother").addClass("embebedIMG").appendTo($gravatarWrapper);
                            }
                            else {
                                $("<img/>").attr("src", "data:image/jpg;base64," + $img).attr("height", "30px").attr("width", "30px").attr("id", "AvatarUser").addClass("embebedIMG").appendTo($gravatarWrapper);
                            }
                        }
                        //Para emojis
                        if ($messageP.html().indexOf('<img src="replaceToURLZmb') > -1) {
                            $messageP.html($messageP.html().replaceAll("replaceToURLZmb", thisDomain));
                        }
                        _this.chatWindows[chatName].chatContainer.$windowInnerContent.scrollTop(_this.chatWindows[chatName].chatContainer.$windowInnerContent[0].scrollHeight);
                    }
                },

                sendTypingSignal: function (otherUserId) {
                    /// <summary>Called by the adapter when the OTHER user is sending a typing signal to the current user</summary>
                    /// <param FullName="otherUser" type="Object">User object (the other sending the typing signal)</param>
                    //if (_this.chatWindows[otherUserId]) {
                    //    var otherUser = _this.usersById[otherUserId];
                    //    _this.chatWindows[otherUserId].showTypingSignal(otherUser);
                    //} lo saque yo
                },

                changeStatus: function (response) {
                    var userId = parseInt(response[0]);
                    var status = parseInt(response[1]);
                    var userIdJQ = "#list-" + userId;
                    //Cambia el avatar
                    if (status == -1) {
                        var avatar = response[2];
                        _this.usersById[userId].Avatar = avatar;
                        $(_this.chatContainer.$windowInnerContent).children(userIdJQ).children(".profile-picture").attr("src", "data:image/jpg;base64," + avatar);
                        $(".profile-picture#imgGrpChat" + userId).attr("src", "data:image/jpg;base64," + avatar);
                        return;
                    }

                    if ($(_this.chatContainer.$windowInnerContent).children(userIdJQ).length > 0) {
                        if (chatMode == "normal") {
                            var $listUser = $(_this.chatContainer.$windowInnerContent).children(userIdJQ).children(".profile-status");
                            var currentStatus = $listUser.attr("class").replace("profile-status ", "");
                            $listUser.removeClass(currentStatus);
                            var statusStr;
                            switch (status) {
                                case 0:
                                    statusStr = "offline";
                                    break;
                                case 1:
                                    statusStr = "online";
                                    break;
                                case 2:
                                    statusStr = "busy";
                                    break;
                                case 3:
                                    statusStr = "dontdisturb";
                                    break;
                            }
                            $listUser.addClass(statusStr);
                        }
                        else {
                            var $img = $(_this.chatContainer.$windowInnerContent).children(userIdJQ).children(".profile-picture");
                            $img.css(getBorderFromStatus(status));
                            $(".profile-picture#imgGrpChat" + userId).css(getBorderFromStatus(status));
                        }
                        _this.usersById[userId].Status = status;
                    }
                },
                changeAvatar: function (response) {
                    var userId = response[0];
                    var userIdJQ = "#list-" + userId;
                    var avatar = response[1];
                    _this.usersById[userId].Avatar = avatar;
                    $(_this.chatContainer.$windowInnerContent).children(userIdJQ).children(".profile-picture").attr("src", "data:image/jpg;base64," + avatar);
                },
                changeName: function (response) {
                    var userId = response[0];
                    var name = response[1].split("/")[0];
                    var subContent = response[1].split("/")[1];
                    var $usrLst = $(".user-list-item#list-" + userId);
                    if ($usrLst.length) {
                        $usrLst.find(".content").text(name);
                        $usrLst.find(".subContent").text(subContent);
                    }
                    var $usrChat = $(".chat-window#" + userId);
                    if ($usrChat.length) {
                        $usrChat.find("#containerUserName").text(name);
                    }
                },
                changeGroupName: function (response) {
                    var userId = response[0];
                    var chatId = response[1];
                    var name = response[2];
                    var lst = $(".user-list-item[chatid='" + chatId + "']");
                    if (lst.length) {
                        lst.children(".txtNameGroupChat").text(name);
                    }
                    var chat = $(".chat-window[chatid='" + chatId + "']");
                    if (chat.length) {
                        chat.find("#containerUsersGroup").val(name);
                    }
                },

                usersListChanged: function (usersList) {
                    /// <summary>Called by the adapter when the users list changes</summary>
                    /// <param FullName="usersList" type="Object">The new user list</param>

                    // initializes the user list with the current user, because he/she will not be retrieved
                    var mainWinChatStatus = $(".chat-window#" + thisUserId).css("display");
                    _this.usersById = {};
                    _this.usersById[_this.opts.user.Id] = _this.opts.user;

                    _this.chatContainer.getContent().html('');
                    if (!isCS && (usersList.length == 0 || usersList.Name == "No se encontraron usuarios con el nombre especificado")) {
                        $("<div/>").addClass("user-list-empty").text(_this.opts.emptyRoomText).appendTo(_this.chatContainer.getContent());
                    }


                    if (taskChat) {
                        usersList = new Array();
                        $("#loadingZChatp").text("Cargando grupos...");
                        //var usersListGroup = chatGroupsForum.Get().length ? GetChatGroupsFnForum(new Array(), chatGroupsForum.Get()) : GetChatGroupsForum();
                        var usersListGroup = GetChatGroupsForum();

                        for (var i = 0; i < usersListGroup.length; i++)
                            usersList.push(usersListGroup[i]);
                    }

                    else {



                        if (isCS)   //Solo debe mostrar los aceptados, reinicia usersList mejorar
                            usersList = new Array();
                        $("#loadingZChatp").text("Cargando grupos...");
                        var usersListGroup = chatGroups.Get().length ? GetChatGroupsFn(new Array(), chatGroups.Get()) : GetChatGroups();
                        for (var i = 0; i < usersListGroup.length; i++)
                            usersList.push(usersListGroup[i]);
                        if (myUserChatInfoCol != null && colServer != "") {
                            $("#loadingZChatp").text("Cargando usuarios externos...");
                            var usersExtList = GetExtChatFn(extUsers.Get());//GetExtChat();
                            for (var i = 0; i < usersExtList.length; i++)
                                usersList.push(usersExtList[i]);
                        }
                        usersList.push(_this.opts.user);//Para generar grupos

                        for (var i = 0; i < usersList.length; i++) {
                            if (usersList[i].isGroup || usersList[i].isExternal) continue;

                            if (usersList[i].Role != 2) {
                                _this.usersById[usersList[i].Id] = usersList[i];
                                var statusStr;
                                switch (usersList[i].Status) {
                                    case 0:
                                        statusStr = "offline";
                                        break;
                                    case 1:
                                        statusStr = "online";
                                        break;
                                    case 2:
                                        statusStr = "busy";
                                        break;
                                    case 3:
                                        statusStr = "dontdisturb";
                                        break;
                                }
                                var $userListItem;
                                if (usersList[i].Id == _this.opts.user.Id) {
                                    if (!_this.chatContainer.getContent().find(".user-list-item[creategroup=true]").length) {
                                        $userListItem = $("<div/>")
                                            .addClass("user-list-item")
                                            .attr("data-val-id", usersList[i].Id)
                                            .attr("date", usersList[i].LastMessage)
                                            .attr("id", "list-" + usersList[i].Id)
                                            .appendTo(_this.chatContainer.getContent())
                                            .attr("createGroup", true).hide();
                                    }
                                }
                                else {
                                    $userListItem = $("<div/>")
                                        .addClass("user-list-item")
                                        .attr("data-val-id", usersList[i].Id)
                                        .attr("date", usersList[i].LastMessage)
                                        .attr("id", "list-" + usersList[i].Id)
                                        .appendTo(_this.chatContainer.getContent());
                                    ContextMenu($userListItem);

                                    var $pPicture = $("<img/>")
                                        .addClass("profile-picture")
                                        .attr("src", "data:image/jpg;base64," + usersList[i].Avatar)//
                                        .appendTo($userListItem);
                                    if (chatMode == "compact") {
                                        $("<div/>").addClass("msgCount").appendTo($userListItem);
                                        $pPicture.css(getBorderFromStatus(usersList[i].Status));
                                        $pPicture.css({ height: "40px", width: "40px" });
                                        $userListItem.css({ margin: "1px" });
                                        expandImg($pPicture);
                                    }
                                    else {
                                        $("<div/>")
                                            .addClass("profile-status")
                                            .addClass(statusStr)//usersList[i].Status == 0 ? "offline" : "online"
                                            .appendTo($userListItem);
                                    }
                                    var $divC = $("<div/>")
                                        .addClass("content")
                                        .text(CapitalizeString(usersList[i].Name.indexOf("/") >= 0 ? usersList[i].Name.split("/")[0] : usersList[i].Name))
                                        .appendTo($userListItem);

                                    var $sC = $("<div/>")
                                        .addClass("subContent")
                                        .css({ display: "flex" })
                                        .text(usersList[i].Name.indexOf("/") >= 0 ? CapitalizeString(usersList[i].Name.split("/")[1]) : ' ')
                                        .appendTo($userListItem);
                                    if (chatMode == "compact") {
                                        $divC.css("padding-top", "5px");
                                    }
                                }
                                // makes a click in the user to either create a new chat window or open an existing
                                // I must clusure the 'i'
                                (function (otherUserId) {
                                    // handles clicking in a user. Starts up a new chat session
                                    $userListItem.click(function (e) {
                                        if ($(e.target).hasClass("embebedIMG")) return;
                                        e.preventDefault();
                                        if (chatMode == "compact") {
                                            $(this).children(".msgCount").text("").css("display", "none");
                                            $(this).parents(".chat-window").data({ "chatOpened": $(this).attr("id").replace("list-", ""), "open": true });
                                        }
                                        if (_this.chatWindows[otherUserId]) {
                                            if ((_this.chatWindows[otherUserId].chatContainer.$window).attr("id") != otherUserId) {
                                                _this.chatWindows[(_this.chatWindows[otherUserId].chatContainer.$window).attr("id")] = _this.chatWindows[otherUserId];
                                                delete (_this.chatWindows[otherUserId]);
                                                _this.createNewChatWindow(otherUserId);
                                            }
                                            else {
                                                _this.chatWindows[otherUserId].focus();
                                            }
                                        } else {
                                            _this.createNewChatWindow(otherUserId);
                                        }
                                        AdjustChatSize();
                                    });
                                })(usersList[i].Id);
                            }
                        }
                    }

                    //filtro de usuarios 
                    var usFilApp;
                    var usFilClass;
                    if (chatMode == "compact") {
                        usFilApp = _this.chatContainer.$windowTitle.find(".config");
                        usFilApp.css("margin-right", "-3px");
                        var cW = _this.chatContainer.$window.find(".chat-window-inner-content");
                        if (cW.length > 1) {
                            var hC = (heightCompact - 100);
                            if ($(cW).children().length > 1) hC -= 25;
                            $(cW[1]).height(hC).css("max-height", hC + "px");
                        }
                        else {
                            var hC = (heightCompact - 40);
                            $(cW).height(hC).css("max-height", hC + "px");
                        }
                    }
                    else {
                        usFilApp = _this.chatContainer.getContent();
                        usFilClass = "user-list-filter";
                    }
                    if ($("#Filtro").length) $("#Filtro").remove();
                    var $userFilter = $("<div/>")
                        .addClass(usFilClass)
                        .attr("data-val-filter", "Filtro")
                        .css({ "float": "left", "padding-left": "10px" })
                        .attr("id", "Filtro");

                    if (chatMode == "compact")
                        $userFilter.insertAfter($(".config"));
                    else
                        $userFilter.appendTo(usFilApp);
                    //$(".filter-picture").length ? $(".filter-picture") :
                    var $fpImg = $("<img/>")
                        .addClass("filter-picture").attr("data-placement", "left")//.attr("title", "Buscar contacto").tooltip()
                        .attr("src", URLImage + "ChatJs/Images/search.png")
                        .appendTo($userFilter);

                    var $textFilter = $("<input/>")
                        .addClass("filter form-control")
                        .attr({ type: 'textbox', id: 'filterByUser', name: 'filterByUser', value: '', placeholder: 'Ingrese nombre...' })
                        .text("filtro").css({ color: "black" })
                        .attr({ "title": "Ingrese su búsqueda", "data-placement": "bottom" })
                        .appendTo($userFilter);
                    if (chatMode == "compact") {
                        $fpImg.css({ height: "16px", width: "16px" });
                        $fpImg.click(function () {
                            var $inp = $(this).parent().children("input");
                            var $p = $(this).parents(".chat-window-title");
                            if ($inp.css("display") == "none") {
                                $inp.fadeToggle();
                                if ($p.children(".statusMenu").length)
                                    $p.children(".config").click();
                                $p.children(".mainTitle, .subName, .config, .colIcon, .addGroup, #containerUserName, .addUser").css("display", "none");
                                $fpImg.attr("src", URLImage + 'ChatJs/images/delete.png')
                                    .toggleClass("filter-picture-rem").removeAttr('title');
                            }
                            else {
                                $inp.text("").css("display", "none");
                                $p.children(".mainTitle, .config, .colIcon, .addGroup, #containerUserName, .addUser").css("display", "inline");
                                $p.children(".subName").css("display", "table");
                                $fpImg.attr("src", URLImage + 'ChatJs/images/search.png')
                                    .toggleClass("filter-picture");
                                $(".chat-window").find("#filterByUser").val("").keydown();
                            }


                            if (taskChat) {
                                var $p = $(this).parents(".chat-window-title");
                                //$p.children(".mainTitle, .config").css("display", "inline"); 
                                $p.children(".mainTitle, .subName, .config, .colIcon, .addGroup, #containerUserName, .addUser").css("display", "none");
                                var box = $("#filterByUser");
                                box.css("height", "30px");

                            }


                        });

                        $textFilter.css({ "display": "none", float: "left" });
                        AdjustChatSize();
                    }
                    $textFilter.tooltip();

                    $textFilter.keydown(function (letra) {
                        var box = $("#filterByUser").val();
                        if (letra.keyCode == 8) { //keycode 8= backspace saco el ultimo caracter                            
                            var txtBox = box.substring(0, box.length - 1);
                        }
                        else {
                            var txtBox = box + String.fromCharCode(letra.which);
                        };

                        if (txtBox == "") {
                            if (chatMode == "compact") {
                                var $p = $(this).parents(".chat-window-title");
                                //$p.children(".mainTitle, .config").css("display", "inline"); 
                                $p.children(".mainTitle, .subName, .config, .colIcon, .addGroup, #containerUserName, .addUser").css("display", "inline");
                                $p.children(".subName").css("display", "table");
                                $(this).css("display", "none");
                                $(this).val("");
                                $fpImg.attr("src", URLImage + 'ChatJs/images/search.png');
                            }

                            if (taskChat) {
                                var $p = $(this).parents(".chat-window-title");
                                //$p.children(".mainTitle, .config").css("display", "inline"); 
                                $p.children(".mainTitle, .subName, .config, .colIcon, .addGroup, #containerUserName, .addUser").css("display", "none");
                                //$fpImg.attr("src", URLImage + 'ChatJs/images/search.png');

                            }


                            for (var i = 0; i <= usersList.length - 1; i++) { //Al borrar la busqueda obtiene de nuevo los usuarios
                                if (usersList[i].isGroup) {
                                    var $gDiv = $(".user-list-item[chatid=" + usersList[i].chatId + "]");
                                    if ($gDiv != undefined)
                                        $gDiv.css("display", "block");
                                }
                                else
                                    if ($("#list-" + usersList[i].Id) != undefined && usersList[i].Id != _this.opts.user.Id)
                                        $("#list-" + usersList[i].Id).css("display", "block");

                            }
                            OrderUsrLstByDate();
                        }
                        else {
                            function filterName(users) {

                                if (((users.Name).toUpperCase()).indexOf((txtBox).toUpperCase()) > -1) {
                                    return true;
                                } else {
                                    return false;
                                };
                            }
                            var usersFiltered = usersList.filter(filterName);

                            if (usersFiltered.length == 0) {
                                if (chatMode == "compact") {
                                    var $p = $(this).parents(".chat-window-title");
                                    //$p.children(".mainTitle, .config, .addUser, #containerUserName,.addGroup,.colIcon").css("display", "inline");
                                    $p.children(".subName").css("display", "table");
                                    //$(this).css("display", "none");
                                    //$(this).val("");
                                    //$fpImg.attr("src", URLImage + 'ChatJs/images/search.png');
                                    $fpImg.css({
                                        "position": "relative",
                                        "top": "6px",
                                        "right": "3px",
                                    })
                                }
                                for (var i = 0; i <= usersList.length - 1; i++) { //Al borrar la busqueda obtiene de nuevo los usuarios
                                    if (usersList[i].isGroup) {
                                        var $gDiv = $(".user-list-item[chatid=" + usersList[i].chatId + "]");
                                        if ($gDiv != undefined)
                                            $gDiv.css("display", "block");
                                    }
                                    else
                                        if ($("#list-" + usersList[i].Id) != undefined && usersList[i].Id != _this.opts.user.Id)
                                            $("#list-" + usersList[i].Id).css("display", "block");

                                }
                                //_this.client.usersListChanged(usersList); //Se comento ya que al borrar busqueda provocaba low performance
                                if (txtBox.length > 1) {
                                    toastrInTitle("No se encontraron resultados con", txtBox.toLowerCase());
                                }
                            }
                            else {
                                for (var i = 0; i <= usersList.length - 1; i++) {//elimina div
                                    if (usersList[i].isGroup) {
                                        var $gDiv = $(".user-list-item[chatid=" + usersList[i].chatId + "]");
                                        if ($gDiv != undefined)
                                            $gDiv.css("display", "none");
                                    }
                                    else
                                        if ($("#list-" + usersList[i].Id) != undefined)
                                            $("#list-" + usersList[i].Id).css("display", "none");
                                }

                                for (var i = 0; i <= usersFiltered.length - 1; i++) {
                                    if (!usersFiltered[i].isGroup && usersList[i].Role != 2 && usersFiltered[i].Id != _this.opts.user.Id) {
                                        $("#list-" + usersFiltered[i].Id).css("display", "block");
                                    }
                                    else {
                                        //Grupos
                                        if (usersFiltered[i].isGroup) {
                                            $(".user-list-item[chatid=" + usersFiltered[i].chatId + "]").css("display", "block");
                                        }
                                    }
                                }
                            }
                        }
                        AdjustChatSize(null, true);//true = no llamar a commonZLinkFn(), reabre busqueda
                    });

                    // update the online status of the remaining windows
                    for (var i in _this.chatWindows) {
                        if (_this.usersById[i] == undefined)//.Status
                            _this.chatWindows[i].setOnlineStatus(-2);// sin estado (grupo)
                        else
                            _this.chatWindows[i].setOnlineStatus(_this.usersById[i].Status);
                    }
                    if (mainWinChatStatus != "none")//Ver si esta condicion esta bien, la idea es que no abra dos ventanas de chat
                        _this.chatContainer.setVisible(true);
                },

                showError: function (errorMessage) {
                    // todo
                }
            };

            _this.opts.adapter.init(_this, function () {
                /// <summary>Called by the adapter when all the adapter initialization is done already</summary>
                /// <param FullName="usersList" type=""></param>

                $("#loadingZChatp").text("Cargando usuarios internos...");
                _this.opts.adapter.server.getUsersList(function (usersList) {
                    _this.client.usersListChanged(usersList);
                    // _this.loadWindows();// carga historial por cookie
                    OrderUsrLstByDate();
                    //Que muestre chat una vez haya cargado lista de usuarios
                    $(_this.chatContainer.$window).css("display", "block");
                    if ($("#loadingZChat").length) $("#loadingZChat").remove();

                    if (IsZambaLink())
                        if (typeof (winFormJSCall) !== "undefined") winFormJSCall.action("ready");

                    if (IsZambaMobile()) {
                        fullScreenUsersChat();
                        $(".minimized").remove();
                    }
                });
            });
        },

        playSound: function (filename) {
            /// <summary>Plays a notification sound</summary>
            /// <param FullName="fileFullName" type="String">The file path without extension</param>
            var $soundContainer = $("#soundContainer");
            if (!$soundContainer.length)
                $soundContainer = $("<div>").attr("id", "soundContainer").appendTo($("body"));
            $soundContainer.html('<audio autoplay="autoplay"><source src="' + filename + '.mp3" type="audio/mpeg" /><source src="' + filename + '.ogg" type="audio/ogg" /><embed hidden="true" autostart="true" loop="false" src="' + filename + '.mp3" /></audio>');
        },

        loadWindows: function () {
            var _this = this;
            var userId = _this.opts.user.Id;

            $.ajax({
                type: "GET",
                async: false,
                url: URLBase + '/getnoreaduserhistory',
                data: { userId: userId },
                success: function (result) {

                    if (result.History.length > 0) {
                        (_this.chatContainer.$window).data("clearChat", true);

                        //Armo los chats y cargo historial
                        for (var i = 0; i < result.History.length; i++)
                            _this.client.sendMsgToUsers(result.History[i]);
                        //Cambio color de título (Verde no leido)
                        for (var chatWin in _this.chatWindows) {
                            _this.chatWindows[chatWin].chatContainer.$windowTitle.css("background-color", "rgb(51, 153, 51)");
                            _this.chatWindows[chatWin].chatContainer.$window.click(function () {
                                $(this).off("click");
                                $(this).children(".chat-window-title").css("background-color", "#636262");
                            });
                        }
                        (_this.chatContainer.$window).data("clearChat", false);
                    }
                }
            });
            //var cookie = _this.readCookie("chat_state");
            //if (cookie) {
            //    var openedChatWindows = JSON.parse(cookie);
            //    for (var i = 0; i < openedChatWindows.length; i++) {
            //        var otherUserId = openedChatWindows[i].userId;
            //        _this.opts.adapter.server.getUserInfo(otherUserId, function (user) {
            //            if (user) {
            //                if (!_this.chatWindows[otherUserId])
            //                    _this.createNewChatWindow(otherUserId, null, "blured");
            //            } else {
            //                // when an error occur, the state of this cookie invalid
            //                // it must be destroyed
            //                _this.eraseCookie("chat_state");
            //            }
            //        });
            //    }
            //}
        },

        saveWindows: function () {
            var _this = this;
            var openedChatWindows = new Array();
            for (var otherUserId in _this.chatWindows) {
                openedChatWindows.push({
                    userId: otherUserId,
                    toggleState: _this.chatWindows[otherUserId].getToggleState()
                });
            }
            // _this.createCookie("chat_state", JSON.stringify(openedChatWindows), 365); reveer si es necesario
        },

        createNewChatWindow: function (otherUserId, initialToggleState, initialFocusState) {
            var _this = this;
            if (chatMode == "normal") {
                if (!initialToggleState)
                    initialToggleState = "maximized";

                if (!initialFocusState)
                    initialFocusState = "focused";
            }
            else {
                initialToggleState = "maximized";
                initialFocusState = "focused";
                $(_this.chatContainer.$window).css("display", "none");
            }


            var otherUser = _this.usersById[otherUserId]//[(otherUserId.toString()).replace("single", "")];
            if (!otherUser)
                throw "Cannot find the other user in the list";

            // if this particular chat-window does not exist yet, create it
            var newChatWindow = $.chatWindow({
                chat: _this,
                myUser: _this.opts.user,
                otherUser: otherUser,
                newMessageUrl: _this.opts.newMessageUrl,
                messageHistoryUrl: _this.opts.messageHistoryUrl,
                initialToggleState: initialToggleState,
                initialFocusState: initialFocusState,
                userIsOnline: otherUser.Status,// == 1
                adapter: _this.opts.adapter,
                typingText: _this.opts.typingText,
                onClose: function () {

                    $.each(_this.chatWindows, function (index, value) {
                        //var id = $(value.chatContainer.$window).attr("id");
                        //if (index != id) {
                        //    _this.chatWindows[id] = this;
                        //    delete _this.chatWindows[index];
                        //}
                        //var usersByWindows = (value.chatContainer.$window).attr("id").split("/").sort(function (a, b) { return a - b });
                        //Probar, lo puse porque al estar filtrado la lista de users abrir y cerrar conversacion no volvia a abrir
                        if ((value.chatContainer.$window).attr("id") == "toDelete") {
                            delete value;
                            delete _this.chatWindows[index];
                            return false;
                        }
                    });

                    $.organizeChatContainers();
                    _this.saveWindows();
                },
                onToggleStateChanged: function (toggleState) {
                    _this.saveWindows();
                }
            });


            _this.chatWindows[otherUserId.toString()] = newChatWindow;
            var $thisWindow = $(newChatWindow.chatContainer.$window);
            if ($thisWindow.attr("isGroup") == undefined)
                $thisWindow.attr("isGroup", false);

            if (chatMode == "compact") {
                $thisWindow.width(widthCompact).height(heightCompact)
                    .css({ top: $("#" + thisUserId).css("top"), left: $("#" + thisUserId).css("left") });
                //$(".chat-window-text-box").css("width", "88%");
                var contHeight = (heightCompact - 85) + "px";
                newChatWindow.chatContainer.$windowInnerContent.css({ "max-height": contHeight, "height": contHeight, "overflow-x": "hidden" });

                var $lastCon = $("<div/>").addClass("lastUserConnChat")
                    .text(moment(otherUser.LastActiveOn).fromNow())
                    .appendTo(newChatWindow.chatContainer.$window.find("#containerUserName"));
            }
            SetFunctionUpload(otherUserId.toString(), isCS);

            _this.saveWindows();
        },

        createNewChatWindowGroup: function (users, isGroup, chatid, isExternal, initialToggleState, initialFocusState) {
            var _this = this;

            if (chatMode == "normal") {
                if (!initialToggleState)
                    initialToggleState = "maximized";

                if (!initialFocusState)
                    initialFocusState = "focused";
            }
            else {
                initialToggleState = "maximized";
                initialFocusState = "focused";
                $(_this.chatContainer.$window).css("display", "none");
            }
            var otherUser;
            for (var i = users.length - 1; i >= 0; i--) {
                var usId = isExternal ? thisExtUserId : thisUserId;
                if (users[i] != undefined && users[i].UserId != usId) {
                    if (isExternal)
                        otherUser = TempUserInfo(users[i].UserId, true);
                    else
                        otherUser = _this.usersById[users[i].UserId];
                    break;
                }
            }
            if (!otherUser && isGroup == 2) {
                otherUser = _this.opts.user;
                otherUser.Status == 1;
            }

            var newChatWindow = $.chatWindow({
                chat: _this,
                myUser: _this.opts.user,
                otherUser: otherUser,
                newMessageUrl: _this.opts.newMessageUrl,
                messageHistoryUrl: _this.opts.messageHistoryUrl,
                initialToggleState: initialToggleState,
                initialFocusState: initialFocusState,
                userIsOnline: otherUser.Status, //== 1
                adapter: _this.opts.adapter,
                isGroup: isGroup,
                chatid: chatid,
                isExternal: isExternal,
                typingText: "noHistory",//_this.opts.typingText,
                onClose: function () {
                    for (var win in _this.chatWindows) {
                        if (_this.chatWindows[win].chatContainer.$window.attr("id") == "toDelete")
                            delete _this.chatWindows[win];
                    }
                    $.organizeChatContainers();
                    _this.saveWindows();
                },
                onToggleStateChanged: function (toggleState) {
                    _this.saveWindows();
                }
            });
            var chatName = "";
            if (isGroup == 2) {
                chatName = "chatid" + chatid;
            }
            else {
                for (var i = 0; i < users.length; i++) {
                    if (users[i] != undefined && users[i].UserId != _this.opts.user.Id)
                        chatName += users[i].UserId + "/";
                }
                chatName = chatName.substring(0, chatName.length - 1);
            }
            if (isExternal || isCS)
                chatName = "extchatid" + chatid;
            var $cUN = newChatWindow.chatContainer.$window.find("#containerUserName");
            $cUN.text("");
            var $lastCon = $("<div/>").addClass("lastUserConnChat")
                .text(moment(otherUser.LastActiveOn).fromNow())
                .appendTo($cUN);

            SetFunctionUpload(chatName, (isExternal || isCS));

            _this.chatWindows[chatName] = newChatWindow;
            (_this.chatWindows[chatName].chatContainer.$window).attr("id", chatName);
            var $thisWindow = newChatWindow.chatContainer.$window;

            $thisWindow.attr({ "isGroup": isGroup, "isExternal": isExternal });

            if (chatMode == "compact") {
                $thisWindow.width(widthCompact).height(heightCompact)
                    .css({ top: $("#" + thisUserId).css("top"), left: $("#" + thisUserId).css("left") });

                $(".chat-window-text-box").css("width", "88%");
                var contHeight = (heightCompact - 85) + "px";
                newChatWindow.chatContainer.$windowInnerContent
                    .css({ "max-height": contHeight, "height": contHeight, "overflow-x": "hidden" });
            }
            var $wT = _this.chatWindows[chatName].chatContainer.$windowTitle;
            if (!$wT.children("#usersInGroup").length)
                $("<div/>").addClass("addUserTxt").attr("id","usersInGroup").appendTo(_this.chatWindows[chatName].chatContainer.$windowTitle);

            for (var i = 0; i < users.length; i++) {
                var usId = (isExternal || isCS) ? thisExtUserId : thisUserId;
                if (users[i] != undefined && users[i].UserId != usId) {
                    var $txtNewUser = $("<div/>")
                        .attr("id", "addedUser" + users[i].UserId)//+ otherUserId + "/"
                        .addClass("addUserTxt").css("display", "none")
                        .html(_this.usersById[users[i].UserId].Name.split("/")[0])
                        .appendTo($wT.children("#usersInGroup"));
                }
            }
            _this.saveWindows();
        },

        eraseCookie: function (name) {
            var _this = this;
            _this.createCookie(name, "", -1);
        },

        readCookie: function (name) {
            var nameEq = name + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                if (c.indexOf(nameEq) == 0) return c.substring(nameEq.length, c.length);
            }
            return null;
        },

        createCookie: function (name, value, days) {
            var expires;
            if (days) {
                var date = new Date();
                date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
                expires = "; expires=" + date.toGMTString();
            } else {
                expires = "";
            }
            document.cookie = name + "=" + value + expires + "; path=/";
        }
    };

    // The actual plugin
    $.chat = function (options) {
        var chat = new Chat(options);
        chat.init();
        return chat;
    };
})(jQuery);
