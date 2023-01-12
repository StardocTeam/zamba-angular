var URLBase;//"http://localhost/ZambaChat/chat"
var URLImage;//"http://localhost/Marsh/"
var URLServices; //  'http://localhost/Marsh/Service/GetUserInfoById';

function LoadChat(thisUserId, URLServer,URLService, thisDomain) {

    thisDomain=thisDomain.substring(thisDomain.length - 1) == "/" ? thisDomain : thisDomain + "/";
    var referenceFiles = thisDomain +"ChatJs";
    var referenceFilesJS = referenceFiles + "/Scripts";
    var referenceFilesCCS = referenceFiles + "/Styles";  
    if (typeof jQuery == 'undefined')
    $('head').append('<script src="/Scripts/jquery-1.8.1.min.js""></script>');
    $('head').append('<script src="' + referenceFilesJS + '/jquery.autosize.min.js" type="text/javascript"></script>');
    $('head').append('<script src="' + referenceFilesJS + '/jquery.activity-indicator-1.0.0.min.js" type="text/javascript"></script>');
    $('head').append('<script src="'+referenceFilesJS+'/Bootstrap/js/bootstrap.min.js" type="text/javascript"></script>');
    $('head').append('<script src="' + referenceFilesJS + '/jquery.typeahead.js" type="text/javascript"></script>'); 
    $('head').append('<script src="' + referenceFilesJS + '/jquery.ui.widget.js" type="text/javascript"></script>');
    $('head').append('<script src="' + referenceFilesJS + '/jquery.fileupload.js" type="text/javascript"></script>');
    $('head').append('<script src="' + referenceFilesJS + '/bootbox.min.js" type="text/javascript"></script>');
    $('head').append('<script src="' + referenceFilesJS + '/ColorJs.js" type="text/javascript"></script>');

    //SignalR
    $('head').append('<script src="' + referenceFilesJS + '/jquery.signalR-1.1.4.min.js"></script>');
    $('head').append('<script src="' + URLServer + 'signalr/hubs" type="text/javascript"></script>');
    $('head').append('<script src="' + referenceFilesJS + '/jquery.chatjs.signalradapter.js" type="text/javascript"></script>');

    //Estilos:
    $('head').append('<link rel="stylesheet" type="text/css" href="' + referenceFilesCCS + '/jquery.chatjs.css" />');
    $('head').append('<link rel="stylesheet" type="text/css" href="' + referenceFilesJS + '/Bootstrap/css/bootstrap.min.css" />');
    //$('head').append('<link rel="stylesheet" type="text/css" href="' + referenceFilesJS + '/Bootstrap/css/bootstrap-theme.min.css" />');
    $('head').append('<link rel="stylesheet" type="text/css" href="'+referenceFilesJS+'/Bootstrap/css/bootstrap-responsive.min.css" />');
    $('head').append('<link rel="stylesheet" type="text/css" href="' + referenceFilesCCS + '/jquery.typeahead.css" />');

    URLBase = URLServer.substring(URLServer.length - 1) == "/" ? URLServer + "chat" : URLServer + "/chat";
    URLImage = thisDomain;
    URLServices =  URLService;
    var userData;
    $(function () {                    
        jQuery.support.cors = true;
        $.ajax({
            type: "GET",
            async: false,          
           data: { userId: thisUserId },
            url: URLBase + '/joinchat', 
            success: function (result) {             
                userData = result.User;
                InitChat(userData);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                return;
            }
        });
               
          function InitChat(userData){
            $.chat({
                user: {
                    Id: thisUserId,
                    Name: userData.Name,
                    Avatar: userData.Avatar
                },
                typingText: ' esta escribiendo...',
                titleText: "   " + userData.Name,//'  Zamba Chat'+
                emptyRoomText: "No hay usuarios online en este momento.",
                adapter: new SignalRAdapter() // new LongPollingAdapter()
            });
        }
    });

    //window.onbeforeunload = function (e) {
    //    if (link_was_clicked) {
    //        return;
    //    }
    //    $.LogOut(thisUserId);
    //}

    $(window).bind('beforeunload', function () {
        if (userData !=undefined)
            $.LogOut(userData.Id);
    });
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
            _this.$window = $("<div/>").addClass("chat-window").appendTo($("body"));

            //Que no se vea si hay mas de 4 ventanas
            if ($._chatContainers != undefined && $._chatContainers.length >= 5)
                _this.$window.css("display", "none").css("right", "0px");

            _this.$windowTitle = $("<div/>").addClass("chat-window-title").appendTo(_this.$window);
            //cuando se clickea en titulo que pueda escribir mensaje
            _this.$windowTitle.click(function (e) {
                if ($(e.target).attr("class") != "addUserTxt") {
                    var $winChat = $(_this.$window);

                    if ($winChat.css("display") == "none") {
                        //Posiciono reemplazando ventana
                        $winChat.css("display", "block").css("right", "730px").css("left", "").css("bottom", "");

                        for (var i = 0; i <= $._chatContainers.length - 1; i++)
                        {
                            if ($._chatContainers[i].$window.css("right") == "730px")//se agrego .$window
                                $._chatContainers[i].$window.css("display", "none");
                        }
                    }

                    $winChat.children(".chat-window-text-box-wrapper").children().select();
                    $winChat.children(".chat-window-text-box-wrapper").children().focus();
                }
            });

            if (_this.opts.canClose) {
                var $closeButton = $("<div/>").addClass("close").appendTo(_this.$windowTitle);
            }

            var $minimizedButton = $("<div/>").addClass("minimized").attr("title", "Minimizar/Restaurar ventana").appendTo(_this.$windowTitle);
            if (_this.opts.initialToggleState=='minimized')
                $minimizedButton.css('background-image', 'url("'+ URLImage + 'ChatJs/images/restore.png")');

            $minimizedButton.tooltip();
            $minimizedButton.click(function () {  //maximiza minimiza antes estaba debajo de  if (_this.opts.canClose) {  
                _this.$windowContent.toggle();
                //Reestablezco color azul.
                _this.$windowTitle.css("background-color", "rgb(109, 132, 180)");
                if (_this.$windowContent.is(":visible") && _this.opts.showTextBox)
                    _this.$textBox.focus();
                _this.opts.onToggleStateChanged(_this.$windowContent.is(":visible") ? "maximized" : "minimized");

                $(_this.$windowTitle.children(".minimized")).css("background-image", _this.$windowContent.is(":visible") ?
                    "url(" + URLImage + "ChatJs/Images/minimize.png)" : "url(" + URLImage + "ChatJs/Images/restore.png)");

                if (_this.$window.attr("groupWin") == "restore" && $._chatContainers.length >= 5) {
                    var flag = false;
                    $("#showMoreWindows").children().text(($._chatContainers.length - 4) + (($._chatContainers.length - 4 == 1) ? " Conversación" : " Conversaciones"));
                    for (var i = 0; i <= $._chatContainers.length - 1; i++) {

                        if (!flag && $._chatContainers[i].$window.css("right").indexOf("730") != -1) {
                            flag = true;
                            var $lastWin = $._chatContainers[i];
                            $lastWin.$window.css("display", "none").css("right", "0px");
                            $lastWin.$window.attr("groupWin", "hidden");
                            var $newWin = _this;
                            $newWin.$window.css("display", "block").css("right", "730px").css("left", "").css("bottom", "");
                            _this.$window.removeAttr("groupwin");
                        }
                        if ($._chatContainers[i].$window.attr("groupWin") == "restore") {
                            $._chatContainers[i].$window.attr("groupWin", "hidden");
                            $._chatContainers[i].$window.css("display", "none").css("right", "0px");
                        }
                    }
                }
            });

            if (!_this.opts.canClose) {
                _this.$window.mouseover(function (e) {
                    e.preventDefault();
                    e.stopPropagation();
                    e.stopImmediatePropagation();
                    if ($(".custom-menu").css("display") != "block") {

                        _this.$window.css({ "width": "230px", "opacity": "1", "right": "10px" });
                        HideContentChat(_this, "block");
                    }

                })
                  .mouseout(function (e) {
                      e.preventDefault();
                      e.stopPropagation();
                      e.stopImmediatePropagation();
                      if (!$("#pinId").data("pin")){
                          if ($(".custom-menu").css("display") != "block") {
                              _this.$window.css({ "width": "60px", "opacity": "0.5", "right": "0px" });
                              HideContentChat(_this, "none");
                          }
                      }
                  });

                function HideContentChat(win, show) {
                    //for (var i = 0; i <= $(".chat-window").length - 1; i++) {
                    //    if ($($(".chat-window")[i]).attr("id") != win.$window.attr("id")) {
                    //        var px = $($(".chat-window")[1]).css("right").replace("px", '');
                    //        $($(".chat-window")[1]).css("right", (px + 100) + 'px');
                    //    }
                    //}

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


                var $configButton = $("<div/>").addClass("config").attr("id", "configId").attr("title", "Configurar Chat").appendTo(_this.$windowTitle);
                $configButton.tooltip();
                $configButton.click(function () {

                    if ($("#statusMenuId").length) {
                        $("#statusMenuId").remove();
                    }
                    else {
                        var $statusMenu = $("<div/>").addClass("statusMenu").attr("id", "statusMenuId").appendTo($(_this.$windowTitle));
                        $statusMenu.html((_this.$windowTitle.data("viewChangeStatus") != true) ? "Desactivar chat:" : " ");
                        var $btnStatus0 = $("<div/>").addClass("status0").attr("id", "status0").appendTo($statusMenu).html("Desconectado");
                        var $btnStatus1 = $("<div/>").addClass("status1").attr("id", "status1").appendTo($statusMenu).html("Conectado");
                        var $btnStatus2 = $("<div/>").addClass("status2").attr("id", "status2").appendTo($statusMenu).html("Ocupado");
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

                        var $changeAvatar = $("<button/>").addClass("btn btn-primary btn-xs").addClass("btn").css("left", "125px").css("top", "-82px").attr("id", "changeAvatarId").appendTo($statusMenu).html("Avatar");
                        var $changeNameBtn = $("<button/>").addClass("btn btn-primary btn-xs").addClass("btn").css("left", "125px").css("top", "-82px").appendTo($statusMenu).html("Nombre");
                        $changeNameBtn.click(function () {
                            $(".mainTitle").click();
                        });

                        $changeAvatar.click(function () {
                            if ($("#statusMenuId").length)
                                $("#statusMenuId").remove();
                            var $statusMenu = $("<div/>").addClass("statusMenu").attr("id", "statusMenuId").appendTo($(_this.$windowTitle));
                            var $img = $("<img/>").addClass("changeAvatarImg").attr("id", "changeAvatarImgId").attr("src",
                                 _this.$windowTitle.children("#containerUserName").children(".mainAvatar").attr("src")).appendTo($statusMenu);
                            var $changePicBtn = $("<button/>").addClass("btn btn-primary btn-xs").addClass("btn").css("left", "-60px").attr("id", "changePicBtnId").html("Cambiar avatar").appendTo($statusMenu);
                            var $defaultPicBtn = $("<button/>").addClass("btn btn-primary btn-xs").addClass("btn").css("left", "65px").attr("id", "defaultPicBtnId").html("Avatar predeterminado").appendTo($statusMenu);
                            var $loc = $("<input/>").attr("type", "file").attr("id", "locId").css("height", "0px").css("width", "0px").appendTo($statusMenu);
                            $changePicBtn.click(function () {
                                $loc.click();
                            });
                            $loc.change(function () {
                                var validExt = "jpg,jpeg,gif,png,bmp,div";
                                var file = document.getElementById("locId").value;
                                var extFile = (file).substring(file.lastIndexOf(".") + 1);

                                if ($.inArray(extFile, validExt.split(",")) > -1) {
                                    var fReader = new FileReader();
                                    fReader.readAsDataURL(document.getElementById("locId").files[0]);
                                    fReader.onloadend = function (event) {
                                        var img = event.target.result;
                                        $("#changeAvatarImgId").attr("src", img);
                                        $("#mainAvatarId").attr("src", img);
                                        img = img.replace("data:image/" + extFile + ";base64,", "");
                                        changeAvatarDB(img);
                                        ChangeStatus(-1);// Para cambiar el Avatar// envio SingalR a otros users                                    
                                    }
                                }
                                else
                                    bootbox.alert("Formato de imagen no valido");
                            });

                            $defaultPicBtn.click(function () {
                                $("#changeAvatarImgId").attr("src", URLImage + 'ChatJs/images/defaultAvatar.jpg');
                                $("#mainAvatarId").attr("src", URLImage + 'ChatJs/images/defaultAvatar.jpg');

                                changeAvatarDB("default");
                                ChangeStatus(-1);
                            });

                            function changeAvatarDB(img) {
                                $.ajax({
                                    type: "POST",
                                    async: false,
                                    url: URLBase + '/changeavatar',
                                    data: { userId: parseInt((_this.$window).attr("id")), avatar: (img.replace("data:image/jpeg;base64,", "")) },
                                    success: function (result) {
                                    }
                                });
                                $("#refreshAvatarId").click();

                            }
                        });
                    }
                });
               
                var $hideShowWin = $("<div/>").addClass("hideShowWin").attr("title", "Minimizar todas las conversaciones").appendTo(_this.$windowTitle);
                $hideShowWin.tooltip();
                $hideShowWin.click(function () {
                    $("#hideShowWindows").click();
                });

                var $pin = $("<div/>").addClass("pinStatus").attr("title", "Fijar - Contraer").attr("id", "pinId").data("pin",false)
                       .css('background-image', 'url("' + URLImage + 'ChatJs/images/unpin.png")').appendTo(_this.$windowTitle);
                $pin.tooltip();

                $pin.click(function () {
                    if ($pin.data("pin"))
                        $pin.css('background-image', 'url("' + URLImage + 'ChatJs/images/unpin.png")')
                    else
                        $pin.css('background-image', 'url("' + URLImage + 'ChatJs/images/pin.png")')

                    $pin.data("pin", !$pin.data("pin"));
                });

            }
            function ChangeStatus(status) {
                var shouldGetHist = false;
                if ($("#changeStatusDiv").data("status") == 0)
                    shouldGetHist = true;

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
                ChangeStatusDB(status)
                $("#statusMenuId").remove();
                $("#changeStatusDiv").data("status", status);
                $("#changeStatusDiv").click();

                if (shouldGetHist)
                    $("#noReadUserHistoryDiv").click();
            }

            function ChangeStatusDB(status) {
                $.ajax({
                    type: "POST",
                    async: false,
                    url: URLBase + '/changestatus',//URLBase +
                    data: { userId: (_this.$window).attr("id"), status: status },
                    success: function (result) {
                    }
                });
            }
            if (_this.opts.canClose) {
                $closeButton.click(function (e) {
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
                });

                var $addUserButton = $("<div/>").addClass("addUser")
                    .attr("title", "Agregar un nuevo integrante al chat")
                    .attr("alt", "Agregar un nuevo integrante al chat")
                    .appendTo(_this.$windowTitle);

                $addUserButton.click(function (e) {
                    var myUserId = $('div.chat-window').attr("id");
                    var otherUserId = (_this.$window).attr("id");
                    var otherUserId1 = otherUserId.split("/")[0];
                    if (document.getElementById("addNewUser-" + otherUserId1)) {// ya existia el txt para agregar usuario                       
                        $("#addNewUser-" + otherUserId1).remove();
                        $("#tildeAddUser-" + otherUserId1).remove();
                    }
                    else {
                        //Agregar usuario al presionar Enter                  
                        function addUserOnClick() {
                            if ($addUserTxt.val().length >= 4) {
                                $("#tildeAddUser-" + otherUserId1).click();
                            }
                        }
                        var usersName = [];
                        var usersId = [];
                        var data = {
                            Users: []
                        };
                        $.ajax({
                            type: "GET",
                            async: false,
                            url: URLBase + '/getuserschat',
                            success: function (result) {
                                var otherUsers = otherUserId.split("/");
                                for (var i = 0; i < result.Users.length; i++) {// itero cada usuario retornado por ajax
                                    if (result.Users[i].Id != myUserId) {// no agregarme a la lista

                                        var add = true;
                                        for (var j = 0; j < otherUsers.length; j++) {//por cada usuario en este chat
                                            if (result.Users[i].Id == otherUsers[j])
                                                add = false;
                                        }
                                        if (add) {
                                            data.Users.push(result.Users[i].Name);
                                            usersName.push(result.Users[i].Name);
                                            usersId.push(result.Users[i].Id);
                                        }
                                    }
                                }
                            },
                        });

                        var $container = $("<div/>").addClass("typeahead-container").appendTo(_this.$windowTitle);
                        var $span = $("<span/>").addClass("typeahead-query").appendTo($container);
                        var $addUserTxt = $("<input/>").attr("autocomplete", "off").css("color", "darkslateblue")
                                              .attr("placeholder", "Agregar participante")
                                              .attr("id", "addNewUser-" + otherUserId1).appendTo($span);

                        $addUserTxt.on("input", null, null, addUserOnClick);
                        $('#addNewUser-' + otherUserId1).typeahead({
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
                                    $("#tildeAddUser-" + otherUserId1).click();
                                }
                            },
                        });
                        $addUserTxt.select();
                        $addUserTxt.focus();

                        var $okAddUserButton = $("<div/>").addClass("okAddUser").attr("id", "tildeAddUser-" + otherUserId1).appendTo(_this.$windowTitle);
                        $okAddUserButton.click(function (e) {

                            if ($addUserTxt.val() == "" || !IsUserSelected($addUserTxt.val())) {
                            }
                            else {
                                selectedUserId = SelectedUserId($addUserTxt.val());

                                var $txtNewUser = $("<div/>")      //nuevo usuario div                                                
                                     .attr("id", "addedUser" + selectedUserId)//+ otherUserId + "/"
                                     .addClass("addUserTxt")
                                     .html($addUserTxt.val())
                                     .appendTo(_this.$windowTitle.split("/")[0]);

                                $addUserTxt.remove();
                                (_this.$window).attr("id", (_this.$window).attr("id") + "/" + selectedUserId);// por esto hace el split

                                SetFunctionUpload((_this.$window).attr("id"));

                                $okAddUserButton.remove();
                                $(".typeahead-container").remove();//Elimino resto del DIV Twitter
                                GetHistory();

                                var $removeUserButton = $("<div/>").addClass("removeUser").attr("id", "removeUser-" + selectedUserId).appendTo(_this.$windowTitle);
                                $removeUserButton.click(function (e) {
                                    $("#addNewUser-" + otherUserId1).remove(); // que no pueda agregar
                                    $("#tildeAddUser-" + otherUserId1).remove();

                                    var userToRemoveId = $removeUserButton.attr("id").split("-")[1];
                                    var newIdChat = ((_this.$window).attr("id")).replace("/" + userToRemoveId, "");
                                    (_this.$window).attr("id", newIdChat); //lo elimino de ids del chat
                                    SetFunctionUpload(newIdChat);

                                    //titulo de ventana grupal
                                    var title = $("div[class=text]", _this.$windowTitle).text();
                                    if ((_this.$window).attr("id").split("/").length == 1) {
                                        //no se modifica el titulo porque no va el +N
                                        //  $("div[class=text]", _this.$windowTitle).text(title.substring(0, title.indexOf(",")));                                          
                                        //si queda un solo usuario en el chat quito icono mostrar/ocultar                                               
                                        (_this.$window).children(".chat-window-title").children(".showAllUsers").remove();
                                    }

                                    (_this.$window).children(".chat-window-title").children("#addedUser" + userToRemoveId).remove();//elimino txtbox
                                    $removeUserButton.remove();//saco boton X     
                                    GetHistory();
                                });

                                (_this.$windowTitle).attr("id", "thisWinTitle");// muestro el boton de ver usuarios
                                $("#thisWinTitle .showAllUsers").css("display", "block");
                                (_this.$windowTitle).attr("id", "windowTitle");
                            }
                        });
                        //_this.$windowTitle.children(".addUser").click();
                        function GetHistory() {
                            $.ajax({
                                type: "GET",
                                async: false,
                                url: URLBase + '/getmessagehistory',
                                data: { usersId: myUserId + "/" + (_this.$window).attr("id") },
                                success: function (result) {
                                    _this.$windowInnerContent[0].innerHTML = "";
                                    if (result.History.length > 0) {

                                        var $getMoreMsgBtn = $("<div/>").addClass("getMoreMsg") //boton traer mensajes anteriores                                     
                                            .attr("id", "getmoremsg" + (_this.$window).attr("id").replace(new RegExp("/", 'g'), "-"))
                                            .text("Traer mensajes anteriores")
                                            .appendTo(_this.$windowInnerContent);

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

                                                    $("#clearOldMsg .chat-window-content .chat-window-inner-content .chat-message").each(function (indice, elemento) {
                                                        $(elemento).remove();
                                                    });
                                                    $("#clearOldMsg .chat-window-content .chat-window-inner-content .chat-date").each(function (indice, elemento) {
                                                        $(elemento).remove();
                                                    });
                                                    $("#clearOldMsg .chat-window-content .chat-window-inner-content .spanUserName").each(function (indice, elemento) {
                                                        $(elemento).remove();
                                                    });
                                                    $("#clearOldMsg .chat-window-content .chat-window-inner-content br").each(function (indice, elemento) {
                                                        $(elemento).remove();
                                                    });
                                                    var cantSend;
                                                    for (var i = 0; i < result.History.length ; i++) {
                                                        AddMsg(result.History[i]);
                                                    }
                                                    (_this.$window).attr("id", thisId);
                                                    _this.$windowInnerContent.scrollTop(0);
                                                },
                                            })
                                        });

                                        for (var i = 0; i < result.History.length ; i++) {
                                            AddMsg(result.History[i]);
                                        }
                                    }
                                },
                            });

                            function AddMsg(message) {
                                var $messageP;
                                var $pText;
                                if (message.Message.substr(0, 5) == "*-*¡¿") {
                                    var fileName = message.Message.substr(message.Message.lastIndexOf("/") + 1).substr(22);//.substr(22) sin fecha
                                    $pText = $("<p/>").text("Compartío archivo...").addClass("sendFileTxt");//.appendTo($(".chat-text-wrapper :last"));

                                    $messageP = $('<a>').text(fileName).addClass("sendFileA");
                                    $messageP.css('background-image', ExtImg(fileName));
                                    $messageP.click(function (e) {
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
                                    if ($pText != undefined)
                                        $pText.insertBefore($messageP);
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
                                    if ($pText != undefined)
                                        $pText.insertBefore($messageP);

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
                                    $("<img/>").attr("src", "data:image/jpg;base64," + $img).attr("height", "30px").attr("width", "30px").appendTo($gravatarWrapper);
                                }
                                // scroll to the bottom
                                _this.$windowInnerContent.scrollTop(_this.$windowInnerContent[0].scrollHeight);
                            }
                        }
                    };

                    function IsUserSelected(selUser) {
                        for (var i = 0; i < usersName.length; i++) {
                            if (selUser == usersName[i])
                                return true
                        }
                        return false
                    }
                    function SelectedUserId(selUserName) {
                        for (var i = 0; i < usersName.length; i++) {
                            if (selUserName == usersName[i])
                                return usersId[i];
                        }
                    }
                });

                var $showAllUsersButton = $("<div/>").addClass("showAllUsers")
                    .attr("title", "Mostrar/Ocultar integrantes del chat")
                    .attr("alt", "Mostrar/Ocultar integrantes del chat")
                    .css("display", "none").appendTo(_this.$windowTitle);
                $showAllUsersButton.click(function (e) {
                    $showAllUsersButton.attr("modeDisplay", ($showAllUsersButton.attr("modeDisplay")) == "none" ? "block" : "none");
                    var modeDisplay = $showAllUsersButton.attr("modeDisplay");
                    var actualTitle = $(_this.$windowTitle).attr("id");
                    $(_this.$windowTitle).attr("id", "windowTitleId");
                    var usersInChat = 0;
                    $("#windowTitleId .addUserTxt").each(function (indice, elemento) {
                        usersInChat++;
                        elemento.style.display = modeDisplay;
                    });
                    $("#windowTitleId .removeUser").each(function (indice, elemento) {
                        elemento.style.display = modeDisplay;
                    });
                    $("#windowTitleId .okAddUser").each(function (indice, elemento) {
                        elemento.style.display = modeDisplay;
                    });
                    var currentTitle = $(_this.$windowTitle).children("#containerUserName").text();
                    //Cuando expande la lista de usuarios
                    if (modeDisplay == "block") {
                        //Le quito el +N porque esta mostrando todos
                        $(_this.$windowTitle).children("#containerUserName").text(currentTitle.split(" +")[0]);
                    }
                    else {
                        $(_this.$windowTitle).children("#containerUserName").text(currentTitle.split(" +")[0] + " +" + usersInChat);
                    }
                    $(_this.$windowTitle).attr("id", actualTitle);
                })

                var $uploadFileDiv = $("<div/>").addClass("uploadFile")                  
                  .attr("title", "Subir archivo")
                  .attr("alt", "Subir archivo")
                  .appendTo(_this.$windowTitle);

                var $uploadFileButton = $("<input/>").addClass('uploadFileButton').attr({ type: 'file', id: "upFile" }).appendTo($uploadFileDiv);
            }
            var $containerUserName = $("<div/>").attr("id", "containerUserName").addClass("text").appendTo(_this.$windowTitle);
            if (!_this.opts.canClose) {
                var $span = $("<span/>").addClass("mainTitle").text(_this.opts.title.split("/")[0]);

                $span.appendTo(_this.$windowTitle);
                var $subName = $("<div/>").addClass("subName").text(_this.opts.title.split("/").length == 2 ? _this.opts.title.split("/")[1] : ' ')
                    .appendTo(_this.$windowTitle);
                $span.click(function (e) {                   
                    bootbox.dialog({
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
                            '</div> </div> ' + '</form> </div>  </div>',
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
                                        $.ajax({
                                            type: "POST",
                                            async: false,
                                            data: { userId: _this.$window.attr("id"), name: name + "/" + status },
                                            url: URLBase + '/ChangeName',
                                            success: function (result) {
                                                $span.text(' ' + name);
                                                $(".subName").text(status != '' ? status : ' ');
                                            }
                                        });
                                    }
                                }
                            }
                        }
                    }
     );

                });
            }

            // content
            _this.$windowContent = $("<div/>").addClass("chat-window-content").appendTo(_this.$window);
            if (_this.opts.initialToggleState == "minimized")
                _this.$windowContent.fadeOut();//tenia el hide comun

            if (!_this.opts.canClose) {
                $.ajax({
                    type: "GET",
                    async: false,
                    url: URLBase + '/GetGroups',
                    success: function (result) {
                        if (result.GroupList.length >= 1) {
                            $("<div/>").text("Grupos").addClass("groupContactTitle").appendTo(_this.$windowContent);

                            var $groupDiv = $("<div/>").addClass("chat-window-inner-content")
                            .css("max-height", "90px").css("margin-bottom", "0px").attr("id", "groupDiv").appendTo(_this.$windowContent);

                            $("<div/>").text("Contactos").addClass("groupContactTitle").appendTo(_this.$windowContent);

                            for (var i = 0; i <= result.GroupList.length - 1; i++) {
                                var toolTip = "Participantes: ";
                                var participantsIds = [];
                                var $groupListItem = $("<div/>")
                                   .addClass("user-list-item")
                                   .appendTo($groupDiv);
                               
                                //Se muestran hasta 6-7 usuarios
                                for (var j = 0; j <= (result.GroupList[i].length >= 8 ? 6 : (result.GroupList[i].length - 1)) ; j++)//7?5
                                {
                                    toolTip += result.GroupList[i][j].Name.split("/")[0] + ", ";
                                    var $userImg = result.GroupList[i][j].Avatar;
                                    var $div = $("<div/>").attr("title", result.GroupList[i][j].Name).appendTo($groupListItem);
                                    var $groupImgItem = $("<img/>")
                                        .addClass("profile-picture")
                                        .attr("src", "data:image/jpg;base64," + result.GroupList[i][j].Avatar)
                                        .attr("title", result.GroupList[i][j].Name)
                                        .appendTo($div);
                                    $div.tooltip();
                                }

                                for (var j = 0; j <= result.GroupList[i].length - 1; j++)
                                    participantsIds.push(result.GroupList[i][j].Id);

                                if (result.GroupList[i].length >= 8)//7
                                {
                                    var $div = $("<div/>").appendTo($groupListItem);
                                    var usrName = "";
                                    for (var k = 7; k <= result.GroupList[i].length - 1; k++)//6
                                        usrName += result.GroupList[i][k].Name + ", ";

                                    toolTip += usrName;
                                    $("<span/>").addClass("groupContactTitle").css("font-size", "14px").css("font-weight", "bold")
                                      .text(' +' + (result.GroupList[i].length - 7)).appendTo($div);//6
                                    $div.attr("title", usrName.substring(0, usrName.length - 2));
                                }

                                participantsIds = participantsIds.sort(function (a, b) { return a - b });
                                $groupListItem.attr("id", "gID" + participantsIds.toString());
                                $groupListItem.attr("title", toolTip.substring(0, toolTip.length - 2));
                                $groupListItem.tooltip();
                                ContextMenu($groupListItem);

                                $groupListItem.click(function () {
                                    $("#openGroupDiv").data("usersId", $(this).attr("id").replace("gID", "") + "," + _this.$window.attr("id"));
                                    $("#openGroupDiv").click();
                                });
                            }
                        }
                    }
                });
            }
            _this.$windowInnerContent = $("<div/>").addClass("chat-window-inner-content").appendTo(_this.$windowContent);
            if (!_this.opts.canClose)
                $(_this.$windowInnerContent).css("margin-bottom", "35px");

            // text-box-wrapper
            if (_this.opts.showTextBox) {
                var $windowTextBoxWrapper = $("<div/>").addClass("chat-window-text-box-wrapper").appendTo(_this.$windowContent);
                _this.$textBox = //$("<textarea />").attr("rows", "1").addClass("chat-window-text-box").appendTo($windowTextBoxWrapper);
                    $("<div />").attr("contentEditable", "true").addClass("chat-window-text-box").appendTo($windowTextBoxWrapper);
                _this.$textBox.autosize();
                $fontBtn = $("<img/>").addClass('fontButton').attr("src", URLImage + '/ChatJs/images/font.png').attr('title', "Cambiar Fuente  Subir Imagen").appendTo($windowTextBoxWrapper);
                $colorBtn = $("<img/>").addClass('fontButton').attr("src", URLImage + '/ChatJs/images/color.png').attr('title', "Cambiar Color").appendTo($windowTextBoxWrapper);
                $fontBtn.tooltip();
                $colorBtn.tooltip();
                $fontBtn.click(function () {
                    var show = $fontBtn.data("show");
                    if (show) {
                        $(".editText").fadeOut();
                        $fontBtn.data("show", false);
                    }
                    else {
                        $(".editText").fadeIn();
                        $fontBtn.data("show", true);
                    }
                });
                $colorBtn.click(function () {
                    var show = $colorBtn.data("show");
                    if (show) {
                        $(".editColor").fadeOut();
                        $colorBtn.data("show", false);
                    }
                    else {
                        $(".editColor").fadeIn();
                        $colorBtn.data("show", true);
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
            $("div[class=text]", _this.$windowTitle).text(title.split("/")[0]);

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
        var rightOffset = 10;
        var deltaOffset = 10;
        if ($._chatContainers.length >= 5) {
            $("#showMoreWindows").children().text(($._chatContainers.length - 4) + (($._chatContainers.length - 4 == 1) ? " Conversación" : " Conversaciones"));
        }
        else {
            if ($("#showMoreWindows").length)
                $("#showMoreWindows").remove();
        }
        for (var i = 0; i < $._chatContainers.length; i++) {
            if (rightOffset <= 730) {
                $._chatContainers[i].$window.css("display", "block");
                $._chatContainers[i].$window.removeAttr("groupwin");
                $._chatContainers[i].$window.css("right", (rightOffset + "px")).css("left", "").css("bottom", "");
                $._chatContainers[i].$windowContent.css("display", "block");

            }
            else {
                if ($._chatContainers[i].$window.attr("groupwin") == "restore") {
                    $._chatContainers[i].$window.attr("groupwin", "hidden");
                    $._chatContainers[i].$window.css("display", "none");
                }
            }
            var width = $._chatContainers[i].$window.outerWidth() == 60 ? 230 : $._chatContainers[i].$window.outerWidth();
            rightOffset += parseInt(width/ 10) * 10 + deltaOffset;
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

            // takes a jQuery element and replace it's content that seems like an URL with an
            // actual link or e-mail
            function linkify($element) {
                var inputText = $element.html();
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
                    { pattern: "=P", cssClass: "tongue" }
                ];

                for (var i = 0; i < emoticons.length; i++) {
                    replacedText = replacedText.replace(emoticons[i].pattern, "<span class='" + emoticons[i].cssClass + "'></span>");
                }

                return $element.html(replacedText);
            }

            if (message.ClientGuid && $("p[data-val-client-guid='" + message.ClientGuid + "']").length) {
                // in this case, this message is comming from the server AND the current user POSTED the message.
                // so he/she already has this message in the list. We DO NOT need to add the message.
                $("p[data-val-client-guid='" + message.ClientGuid + "']").removeClass("temp-message").removeAttr("data-val-client-guid");
            } else {
                var $messageP;
                var $pText;
                if (message.Message.substr(0, 5) == "*-*¡¿") {
                    var fileName = message.Message.substr(message.Message.lastIndexOf("/") + 1).substr(22);//.substr(22) sin fecha
                    $pText = $("<p/>").text("Compartío archivo...").addClass("sendFileTxt");//.appendTo($(".chat-text-wrapper :last"));
         
                    $messageP = $('<a>').text(fileName).addClass("sendFileA");                
                    $messageP.css('background-image', ExtImg(fileName));
                    $messageP.click(function (e) {
                        AjaxDownloadFile(message.Message.substr(5));         
                    });
                }
                else {
                    $messageP = $("<p/>").html(message.Message);
                }
             
                if (clientGuid)
                    $messageP.attr("data-val-client-guid", clientGuid).addClass("temp-message");

                linkify($messageP);
                emotify($messageP);

                // gets the last message to see if it's possible to just append the text
                var $lastMessage = $("div.chat-message:last", _this.chatContainer.$windowInnerContent);
                if ($lastMessage.length && $lastMessage.attr("data-val-user-from") == message.UserFromId) {
                    // we can just append text then
                    $messageP.appendTo($(".chat-text-wrapper", $lastMessage));
                    if ($pText != undefined)
                        $pText.insertBefore($messageP);
                }
                else {
                    // in this case we need to create a whole new message
                    var $chatMessage = $("<div/>").addClass("chat-message").attr("data-val-user-from", message.UserFromId);
                    $chatMessage.appendTo(_this.chatContainer.$windowInnerContent);
                   
                    // Escribia en gris claro
                    $messageP.css('color', 'black');
                  
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
                    if (_this.opts.myUser.Id != message.UserFromId) {  
                        var spanUsrName = _this.opts.chat.usersById[message.UserFromId].Name.split("/")[0];
                        $spanName = $("<span/>").text(ReduceName(spanUsrName,10) + formatDateText(date)).addClass("spanUserName").css("padding-top", "8px")
                            .insertBefore($("</br>"));//.insertBefore($pText == undefined ? $chatMessage : $pText)
                    }
                    else {                 
                        $("<span/>").text(formatDateText(date)).css("float", "right").addClass("spanUserName").insertBefore($("</br>").insertAfter($("</br>")
                            .insertBefore($chatMessage)));                
                        $chatMessage.css({
                            'background-color': "rgb(230, 230, 230)",
                            background: "-webkit-linear-gradient(left,white, LightGray)"
                        });
                    };

                    var $gravatarWrapper = (_this.opts.myUser.Id == message.UserFromId) ?
                             $("<div/>").addClass("chat-gravatar-wrapper").css("float", "right").attr("height", "30px").attr("width", "30px").appendTo($chatMessage) :
                             $("<div/>").addClass("chat-gravatar-wrapper").appendTo($chatMessage);

                    var $textWrapper = $("<div/>").addClass("chat-text-wrapper").appendTo($chatMessage);

                    // add text
                    
                    if ($spanName != undefined){
                        $spanName.insertBefore($textWrapper);
                        $("<br/>").insertBefore($textWrapper);
                    }
                    if ($pText != undefined)
                        $pText.insertBefore($textWrapper);
                    $messageP.appendTo($textWrapper);

                    // add image
                    var messageUserFrom = _this.opts.chat.usersById[message.UserFromId];

                    var $img = messageUserFrom.Avatar === null ? messageUserFrom.ProfilePictureUrl : messageUserFrom.Avatar;
                    $("<img/>").attr("src", "data:image/jpg;base64," + $img).attr("height", "30px").attr("width", "30px").appendTo($gravatarWrapper);
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

            if (msgHistory.UserId != this.opts.myUser.Id) {
                // the message did not came from myself. Better erase the typing signal
                _this.removeTypingSignal();
            }

            // takes a jQuery element and replace it's content that seems like an URL with an
            // actual link or e-mail
            function linkify($element) {
                var inputText = $element.html();
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
                    { pattern: "=P", cssClass: "tongue" }
                ];

                for (var i = 0; i < emoticons.length; i++) {
                    replacedText = replacedText.replace(emoticons[i].pattern, "<span class='" + emoticons[i].cssClass + "'></span>");
                }

                return $element.html(replacedText);
            }

            if (message.ClientGuid && $("p[data-val-client-guid='" + message.ClientGuid + "']").length) {
                // in this case, this message is comming from the server AND the current user POSTED the message.
                // so he/she already has this message in the list. We DO NOT need to add the message.
                $("p[data-val-client-guid='" + message.ClientGuid + "']").removeClass("temp-message").removeAttr("data-val-client-guid");
            } else {
                var $messageP;
                var $pText;
                if (msgHistory.Message.substr(0, 5) == "*-*¡¿") {
                    var fileName = msgHistory.Message.substr(msgHistory.Message.lastIndexOf("/") + 1).substr(22);//.substr(22) sin fecha
                    $pText = $("<p/>").text("Compartío archivo...").addClass("sendFileTxt");//.appendTo($(".chat-text-wrapper :last"));
         
                    $messageP = $('<a>').text(fileName).addClass("sendFileA");
                    $messageP.css('background-image', ExtImg(fileName));
                    $messageP.click(function (e) {
                        AjaxDownloadFile(msgHistory.Message.substr(5));         
                    });
                }
                else 
                     $messageP = $("<p/>").html(msgHistory.Message);

                if (clientGuid)
                    $messageP.attr("data-val-client-guid", clientGuid).addClass("temp-message");

                linkify($messageP);
                emotify($messageP);

                // gets the last message to see if it's possible to just append the text
                var $lastMessage = $("div.chat-message:last", _this.chatContainer.$windowInnerContent);
                if ($lastMessage.length && $lastMessage.attr("data-val-user-from") == msgHistory.UserId) {
                    if ($pText != undefined)
                        $pText.appendTo($textWrapper);
                    // we can just append text then
                    $messageP.appendTo($(".chat-text-wrapper", $lastMessage));
                    if ($pText != undefined)
                        $pText.insertBefore($messageP);
                }
                else {
                    // in this case we need to create a whole new message
                    var $chatMessage = $("<div/>").addClass("chat-message").attr("data-val-user-from", msgHistory.UserId);
                    $chatMessage.appendTo(_this.chatContainer.$windowInnerContent);


                    var $gravatarWrapper = (_this.opts.myUser.Id == message.UserFromId) ?
                              $("<div/>").addClass("chat-gravatar-wrapper").css("float", "right").attr("height", "30px").attr("width", "30px").appendTo($chatMessage) :
                              $("<div/>").addClass("chat-gravatar-wrapper").appendTo($chatMessage);

                    var $textWrapper = $("<div/>").addClass("chat-text-wrapper").appendTo($chatMessage);

                    // add text              
                    $messageP.appendTo($textWrapper);
                    if ($pText != undefined)
                        $pText.insertBefore($messageP);

                    //Coloco nombre en chat 
                    var date = new Date(Date.parse(msgHistory.Date));
                    if (date == 'Invalid Date')// Trae la hora con formato incorrecto de controller
                        date = new Date(parseInt(msgHistory.Date.substring(6, msgHistory.Date.length - 2)));

                    var otherUserId = msgHistory.UserFromId == undefined ? msgHistory.UserId : msgHistory.UserFromId;
                    var spanUsrName = _this.opts.chat.usersById[otherUserId].Name;
                    $("<span/>").text(ReduceName(spanUsrName,10) + formatDateText(date)).addClass("spanUserName").insertBefore($("</br>").insertBefore($pText == undefined ? $messageP : $pText));

                    // add image
                    var messageUserFrom = _this.opts.chat.usersById[msgHistory.UserId];

                    var $img = messageUserFrom.Avatar;
                    ;
                    $("<img/>").attr("src", "data:image/jpg;base64," + $img).attr("height", "30px").attr("width", "30px").appendTo($gravatarWrapper);
                }

                // scroll to the bottom
                _this.chatContainer.$windowInnerContent.scrollTop(_this.chatContainer.$windowInnerContent[0].scrollHeight);
            }
        };

        this.sendMessage = function (messageText) {
            /// <summary>Sends a message to the other user</summary>
            /// <param FullName="messageText" type="String">Message being sent</param>
            var _this = this;

            var generateGuidPart = function () {
                return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
            };

            var clientGuid = (generateGuidPart() + generateGuidPart() + '-' + generateGuidPart() + '-' + generateGuidPart() + '-' + generateGuidPart() + '-' + generateGuidPart() + generateGuidPart() + generateGuidPart());
            _this.addMessage({
                UserFromId: _this.opts.myUser.Id,
                Message: messageText
            }, clientGuid);
            _this.opts.adapter.server.sendMessage(_this.opts.otherUser.Id, messageText, clientGuid);
        };

        this.sendMsgToUsers = function (messageText) {
            /// <summary>Sends a message to the other user</summary>
            /// <param FullName="messageText" type="String">Message being sent</param>
            var _this = this;
            var generateGuidPart = function () {
                return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
            };

            var clientGuid = (generateGuidPart() + generateGuidPart() + '-' + generateGuidPart() + '-' + generateGuidPart() + '-' + generateGuidPart() + '-' + generateGuidPart() + generateGuidPart() + generateGuidPart());
            _this.addMessage({
                UserFromId: _this.opts.myUser.Id,
                Message: messageText
            }, clientGuid);
            var toUsers = (_this.chatContainer.$window).attr("id").split("/");
            _this.opts.adapter.server.sendMsgToUsers(toUsers, messageText, clientGuid);
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
                    _this.opts.onClose(e);
                },
                onToggleStateChanged: function (toggleState) {
                    _this.opts.onToggleStateChanged(toggleState);
                }
            });

            _this.chatContainer.$textBox.on("keyup", function () {
            
                   // $(this).html('<b>' + $(this).html() + '</b>');
               
            });

            _this.chatContainer.$textBox.keypress(function (e) {

                if (e.which == 13) {
                    e.preventDefault();
                    if ($(this).html()) {
                        $(this).data("applyStyle", false);
                        // _this.sendMessage($(this).val());
                        var thisId = $(_this.chatContainer.$window).attr("id");// quito agregar participante una vez que se envio mensaje
                        $(_this.chatContainer.$window).attr("id", "removeAddUser");
                        if ($("#removeAddUser .chat-window-title .okAddUser").attr("id") != undefined) {
                            $("#addNewUser-" + ($("#removeAddUser .chat-window-title .okAddUser").attr("id")).replace("tildeAddUser-", "")).remove();
                            $("#removeAddUser .chat-window-title .okAddUser").remove();
                        }
                        $("#removeAddUser .chat-window-title .addUser").remove();
                        $(_this.chatContainer.$window).attr("id", thisId);
                        //Quito las X de usuarios agregados a chat (Se establece chat con los usuarios ya agregados)
                        $(_this.chatContainer.$window).children(".chat-window-title").children(".removeUser").each(function () {
                            if ($.inArray(($(this).attr("id")).replace("removeUser-", ""), thisId.split("/")) > -1)
                                $(this).remove();
                        });

                        var $val = $(this).html();
                        $.ajax({
                            type: "GET",
                            async: false,
                            url: URLBase + '/getuserinfo',
                            data: { userId: _this.opts.myUser.Id },
                            success: function (result) {
                                if (result.User.Role == 0)
                                    _this.sendMsgToUsers($val);
                                else {
                                    var $append = _this.chatContainer.$windowContent.children(".chat-window-inner-content").children(".chat-message").last().children(".chat-text-wrapper")
                                    $("<HR>").css("width", "2%)").css("align", "center")
                                        .appendTo($append);
                                    $("<p/>").text("Active el chat para poder enviar el mensaje")
                                        .css("color", "rgb(109, 132, 180)")
                                        .css("font-size", 10).css("text-align", "center").appendTo($append);
                                    $("<HR>").css("width", "2%)").css("align", "center").appendTo($append);
                                }
                            }
                        });                      
                        $(this).text('').trigger("autosize.resize");
                    }
                }
                else
                    //Para estilos negrita/subrayado/italic
                    if (!$(this).data("applyStyle")) {
                        $(this).data("applyStyle", true);
                        var $fonts = $(this).parent().children(".editText").children(".editFont");
                        $(this).html(' ');
                        for (var i = 0; i <= $fonts.length - 1; i++) {

                            if ($($fonts[i]).data("active")) {
                                var img = $($fonts[i]).attr("src").substring($($fonts[i]).attr("src").lastIndexOf("/") + 1);
                                var type = img.substring(0, img.lastIndexOf("."));
                                if (type !='image')
                                    $(this).html('<' + type + '>' + $(this).html() + '</' + type + '>');
                            }
                        }
                        var size = $($(this).parent().children(".editText").children("select")).val();
                        if (size != 12 && size>=8)
                            $(this).html('<span style="font-size:' + size + 'px">' + $(this).html() + "</span>");

                        var color = $($(this).parent().children(".editColor")).data("color");
                        if (color != undefined && color !="")
                            $(this).html('<span style="color:' + color + '">' + $(this).html() + "</span>");
                    }                   
            });

            _this.chatContainer.setTitle(_this.opts.otherUser == null ? "" : ReduceName(_this.opts.otherUser.Name,18));
            _this.chatContainer.setIdContainer(_this.opts.otherUser == null ? "" : _this.opts.otherUser.Id);
            StyleDragDrop(_this.opts.otherUser == null ? "" : _this.opts.otherUser.Id);

            if (_this.opts.otherUser != null) {
                var usersIds = [_this.opts.myUser.Id, _this.opts.otherUser.Id];
                this.opts.adapter.server.getMessageHistory(usersIds, function (messageHistory) {

                    if (_this.opts.typingText != "noHistory") {
                        if (messageHistory.length > 0) {

                            var $getMoreMsgBtn = $("<div/>").addClass("getMoreMsg") //boton traer mensajes anteriores
                                .attr("id", "getmoremsg" + (_this.chatContainer.$window).attr("id"))
                                .text("Traer mensajes anteriores")
                                .appendTo(_this.chatContainer.$windowInnerContent);

                            $getMoreMsgBtn.click(function (e) {
                                var otherUserId = (_this.chatContainer.$window).attr("id");

                                var msgNum = parseInt((_this.chatContainer.$window).attr("msgNum"));
                                (_this.chatContainer.$window).attr("msgNum", msgNum > 0 ? msgNum + 1 : 1);

                                $.ajax({
                                    type: "GET",
                                    async: false,
                                    url: URLBase + '/getmoremsghistory',
                                    data: {
                                        usersId: _this.opts.myUser.Id + "/" + otherUserId,
                                        cant: parseInt((_this.chatContainer.$window).attr("msgNum"))
                                    },
                                    success: function (result) {
                                        var thisId = (_this.chatContainer.$window).attr("id");
                                        var scrollPosition = _this.chatContainer.$windowInnerContent.scrollTop;
                                        var getMsgNum = 5;//para saber cuando cargo todos los mensajes
                                        var msgNum = getMsgNum * (parseInt((_this.chatContainer.$window).attr("msgNum")) + 1);
                                        if (result.History.length < msgNum) {
                                            $("#getmoremsg" + thisId).remove();//elimino boton de traer mas
                                            (_this.chatContainer.$window).attr("msgNum", "");
                                        }
                                        (_this.chatContainer.$window).attr("id", "clearOldMsg");

                                        $("#clearOldMsg .chat-window-content .chat-window-inner-content .chat-message").each(function (indice, elemento) {
                                            $(elemento).remove();
                                        });
                                        $("#clearOldMsg .chat-window-content .chat-window-inner-content .chat-date").each(function (indice, elemento) {
                                            $(elemento).remove();
                                        });
                                        $("#clearOldMsg .chat-window-content .chat-window-inner-content .spanUserName").each(function (indice, elemento) {
                                            $(elemento).remove();
                                        });
                                        $("#clearOldMsg .chat-window-content .chat-window-inner-content br").each(function (indice, elemento) {
                                            $(elemento).remove();
                                        });
                                        var cantSend;
                                        for (var i = 0; i < result.History.length ; i++)
                                            _this.addMessage(result.History[i]);

                                        (_this.chatContainer.$window).attr("id", thisId);
                                        _this.chatContainer.$windowInnerContent.scrollTop(0);
                                    },
                                });
                            });
                            for (var i = 0; i < messageHistory.length; i++) {
                                _this.addMessage(messageHistory[i]);
                            }
                        }
                    }
                    else {
                        _this.opts.typingText = " esta escribiendo...";
                    }

                    if (!$._chatContainers.length >= 5)
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

                            $innerShowMoreWindows.click(function () {
                                var innerShowAttr = $innerShowMoreWindows.attr("show");
                                $innerShowMoreWindows.attr("show", ((innerShowAttr) == "all") ? "none" : "all").css("background-color", "rgb(109, 132, 180)");

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
    };

    // The actual plugin
    $.chatWindow = function (options) {
        var chatWindow = new ChatWindow(options);
        chatWindow.init();

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

            var mainChatWindowChatState = _this.readCookie("main_window_chat_state");
            if (!mainChatWindowChatState)
                mainChatWindowChatState = "maximized";

            // will create user list chat container
            _this.chatContainer = $.chatContainer({

                title: _this.opts.titleText,
                showTextBox: false,
                canClose: false,
                initialToggleState: mainChatWindowChatState,
                onCreated: function (container) {
                    container.$window.attr("id", _this.opts.user != null ? _this.opts.user.Id : "");// jrojas: id de contenedor principal

                    $(container.$window).click(function () {
                        $("#filterByUser").select();
                        $("#filterByUser").focus();
                    });

                
                    var $mainStatus = $("<div/>").addClass("mainStatus").attr("title", "Click para cambiar estado").appendTo($("#" + container.$window.attr("id") + " .chat-window-title .text"))
                    .attr("id", "mainStatusId").css('background-image', 'url("' + URLImage + 'ChatJs/images/chat-online.png")');
                    $mainStatus.tooltip();

    

                    $("#mainStatusId").click(function () {

                        _this.chatContainer.$windowTitle.data("viewChangeStatus", true);
                        $("#configId").click();
                        _this.chatContainer.$windowTitle.data("viewChangeStatus", false);
                        $("#changeAvatarId").remove();
                    });

                    var $changeStatusDiv = $("<div/>").attr("id", "changeStatusDiv").appendTo($("#" + container.$window.attr("id")));

                    $changeStatusDiv.click(function () {
                        _this.opts.adapter.server.changeStatus(_this.opts.user.Id, $("#changeStatusDiv").data("status"));
                    })

                    var $noReadUserHistoryDiv = $("<div/>").attr("id", "noReadUserHistoryDiv").appendTo($("#" + container.$window.attr("id")));
                    $noReadUserHistoryDiv.click(function () {
                        var minimizeAllWin = $noReadUserHistoryDiv.data("minimizeAllWin") == true;
                        // Imagen minimizar/restaurar
                        _this.chatContainer.$windowTitle.children(".minimized").css("background-image", (minimizeAllWin) ?
                            "url(" + URLImage + "ChatJs/Images/restore.png)" : "url(" + URLImage + "ChatJs/Images/Minimize.png)");
                        //Color rojo a main title
                        $(_this.chatContainer.$windowTitle).css("background-color", (minimizeAllWin) ? "rgb(204, 0, 0)" : "rgb(109, 132, 180)");

                        if (minimizeAllWin)
                            $hideShowWindows.click();
                        else
                            _this.loadWindows();
                    });

                    var $openGroupDiv = $("<div/>").attr("id", "openGroupDiv").appendTo($("#" + container.$window.attr("id")));
                    $openGroupDiv.click(function () {
                        var users = $openGroupDiv.data("usersId");
                        //Compruebo si ya esta abierto para no abrirlo dos veces
                        for (var i = 0; i <= $._chatContainers.length - 1; i++) {
                            if ($openGroupDiv.data("usersId").split(",").sort().toString() ==
                                ($._chatContainers[i].$window.attr("id") + "/" + _this.opts.user.Id).split("/").sort().toString()) {
                                $._chatContainers[i].$windowContent.children(".chat-window-text-box-wrapper").children().select();
                                $._chatContainers[i].$windowContent.children(".chat-window-text-box-wrapper").children().focus();
                                return;
                            }
                        }

                        $.ajax({
                            type: "GET",
                            async: false,
                            url: URLBase + '/OpenGroupChat',
                            data: { usersId: users },
                            success: function (result) {
                                _this.client.sendMsgToUsers(result.GroupChat);
                            },
                        });
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
                        $.ajax({
                            type: "GET",
                            async: false,
                            url: URLBase + '/getuserinfo',
                            data: { userId: _this.opts.user.Id },
                            success: function (result) {
                                _this.opts.user.Avatar = result.User.Avatar;
                            },
                        });
                    }
                    RefreshAvatar();
                    var $mainAvatar = $("<img/>").addClass("mainAvatar").attr("title", "Click para cambiar avatar").appendTo($("#" + container.$window.attr("id") + " .chat-window-title .text"))
                   .attr("src", "data:image/jpg;base64," + _this.opts.user.Avatar).attr("id", "mainAvatarId"); //.css('background-image', 'url(' + _this.opts.user.Avatar + ')');
                    $mainAvatar.tooltip();
                    $("#mainAvatarId").click(function () {
                        $("#configId").click();
                        $("#changeAvatarId").click();
                    });

                    if (!container.$windowInnerContent.html()) {
                        var $loadingBox = $("<div/>").addClass("loading-box").appendTo(container.$windowInnerContent);
                        //if (_this.opts.useActivityIndicatorPlugin)
                        //    $loadingBox.activity({ segments: 8, width: 3, space: 0, length: 3, color: '#666666', speed: 1.5 });
                    }
                },

                onToggleStateChanged: function (toggleState) {
                    _this.createCookie("main_window_chat_state", toggleState);
                }
            });

            // the client functions are functions that must be called by the chat-adapter to interact
            // with the chat
            _this.client = {
                sendMsgToUsers: function (message) {
                    // Si esta el chat desactivado no recibe mensajes
                    if ($("#" + _this.opts.user.Id).children(".chat-window-title").data("switch") == true ||
                       $("#changeStatusDiv").data("status") == "0")
                        return;

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
                        if (participants[i].UserId == _this.opts.user.Id) {
                            validUser = true;
                            break;
                        }
                    }
                    if (validUser) {
                        //con el tema de los grupos lo saque y lo coloque abajo, si ult conversacion era mia no lo habria                                          
                        var participantsIds = [];
                        var usersSorted = [];
                        $.each(_this.chatWindows, function (index, value) {
                            //Agregado esto porque se duplicaban las ventanas grupales igualo objeto code con id DOM
                            var id = $(value.chatContainer.$window).attr("id");
                            if (index != id) {
                                _this.chatWindows[id] = this;
                                delete _this.chatWindows[index];
                            }
                            var usersByWindows = id.split("/").sort(function (a, b) { return a - b });
                            usersSorted.push(usersByWindows);
                        });

                        for (var i = 0; i < participants.length; i++)
                        { participantsIds.push(participants[i].UserId.toString()); }

                        var myUser = participantsIds.indexOf(_this.opts.user.Id.toString());//si no me tiene a mi no debe hacer nada
                        if (myUser > -1) { participantsIds.splice(myUser, 1); }

                        participantsIds = participantsIds.sort(function (a, b) { return a - b });

                        var existChat;
                        for (var j = 0; j < usersSorted.length; j++) {
                            if (JSON.stringify(participantsIds) == JSON.stringify(usersSorted[j])) {
                                existChat = true;
                                break;
                            }
                        }
                        var chatName = "";
                        for (var i = 0; i < participants.length; i++) {
                            if (participants[i].UserId != _this.opts.user.Id)
                                chatName += participants[i].UserId + "/";
                        }
                        chatName = chatName.substring(0, chatName.length - 1);

                        if (!existChat) {
                            _this.createNewChatWindowGroup(participants);
                            GetHistory(_this.opts.user.Id + "/" + chatName);
                            var windowTitleName = "tit" + (_this.opts.user.Id + "/" + chatName).replace(new RegExp("/", 'g'), "-");
                            $(_this.chatWindows[chatName].chatContainer.$windowTitle).attr("id", "windowTitle");
                            $("#windowTitle .addUser").remove();

                            // Si hay mas de dos participantes cambia el titulo y coloca botón para mostrar/ocultar lista de participantes
                            if ((participants.length - 2) > 0) {
                                var $showAllUsersButton = $("<div/>").addClass("showAllUsers")
                                       .attr("title", "Mostrar/Ocultar integrantes del chat")
                                       .attr("alt", "Agregar un nuevo integrante al chat")
                                       .css("display", "block").appendTo("#windowTitle .text");

                                $(_this.chatWindows[chatName].chatContainer.$windowTitle).attr("id", " ");

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
                                        //Le quito el +N porque esta mostrando todos                                       
                                        if ($((this.parentElement).children[1]).attr("id") == "spanNum")
                                            $((this.parentElement).children[1]).html("");
                                    }
                                    else {
                                        if ($((this.parentElement).children[1]).attr("id") == "spanNum")
                                            $((this.parentElement).children[1]).html(" +" + usersInChat);
                                        else
                                            var $spanNum = $("<span/>").html(" +" + usersInChat)
                                               .attr("id", "spanNum").appendTo("#windowTitle .text");
                                    }
                                    $("#windowTitle").attr("id", "tit" + (_this.opts.user.Id + "/" + chatName).replace(new RegExp("/", 'g'), "-"));
                                })
                                //Para que me oculte los usuarios del chat cuando me lo abre
                                $showAllUsersButton.click();
                            }
                            SetFunctionUpload(chatName);
                            $(_this.chatWindows[chatName].chatContainer.$windowTitle).attr("id", windowTitleName);

                        } else {
                            // se repetian los mensajes sin esta condición, se escribia dos veces lo mismo
                            if (msgHistory.UserId == _this.opts.user.Id)
                                return;

                            $.each(_this.chatWindows, function (index, value) {
                                if (JSON.stringify((value.chatContainer.$window).attr("id").split("/").sort()) ==
                                                                    JSON.stringify(chatName.split("/").sort())) {
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

                    function GetHistory(participantsIds) {
                        $.ajax({
                            type: "GET",
                            async: false,
                            url: URLBase + '/getmessagehistory',
                            data: { usersId: participantsIds },
                            success: function (result) {
                                _this.chatWindows[chatName].chatContainer.$windowInnerContent.html("");
                                if (result.History.length == 5) {
                                    var $getMoreMsgBtn = $("<div/>").addClass("getMoreMsg") //boton traer mensajes anteriores
                                           .attr("id", "getmoremsg" + ((_this.chatWindows[chatName].chatContainer.$window).attr("id")).replace(new RegExp("/", 'g'), "-"))
                                           .text("Traer mensajes anteriores")
                                           .appendTo(_this.chatWindows[chatName].chatContainer.$windowInnerContent);

                                    $getMoreMsgBtn.click(function (e) {
                                        var otherUserId = (_this.chatWindows[chatName].chatContainer.$window).attr("id");

                                        var msgNum = parseInt((_this.chatWindows[chatName].chatContainer.$window).attr("msgNum"));
                                        (_this.chatWindows[chatName].chatContainer.$window).attr("msgNum", msgNum > 0 ? msgNum + 1 : 1);

                                        var myUserId = _this.opts.user.Id;
                                        $.ajax({
                                            type: "GET",
                                            async: false,
                                            url: URLBase + '/getmoremsghistory',
                                            data: {
                                                usersId: myUserId + "/" + otherUserId,
                                                cant: parseInt((_this.chatWindows[chatName].chatContainer.$window).attr("msgNum"))
                                            },
                                            success: function (result) {
                                                var thisId = (_this.chatWindows[chatName].chatContainer.$window).attr("id");

                                                var getMsgNum = 5;//para saber cuando cargo todos los mensajes
                                                var msgNum = getMsgNum * (parseInt((_this.chatWindows[chatName].chatContainer.$window).attr("msgNum")) + 1);
                                                if (result.History.length < msgNum) {
                                                    $("#getmoremsg" + replaceAll(thisId, "/", "-")).remove();//.replace("/", "-")).remove();//elimino boton de traer mas
                                                    function replaceAll(text, search, newstring) {
                                                        return text.replace(new RegExp(search, 'g'), newstring);
                                                    }
                                                    (_this.chatWindows[chatName].chatContainer.$window).attr("msgNum", "");
                                                }
                                                (_this.chatWindows[chatName].chatContainer.$window).attr("id", "clearOldMsg");

                                                $("#clearOldMsg .chat-window-content .chat-window-inner-content .chat-message").each(function (indice, elemento) {
                                                    $(elemento).remove();
                                                });
                                                $("#clearOldMsg .chat-window-content .chat-window-inner-content .chat-date").each(function (indice, elemento) {
                                                    $(elemento).remove();
                                                });
                                                $("#clearOldMsg .chat-window-content .chat-window-inner-content .spanUserName").each(function (indice, elemento) {
                                                    $(elemento).remove();
                                                });
                                                $("#clearOldMsg .chat-window-content .chat-window-inner-content br").each(function (indice, elemento) {
                                                    $(elemento).remove();
                                                });
                                                var cantSend;
                                                for (var i = 0; i < result.History.length ; i++)
                                                    AddMsg(result.History[i]);

                                                (_this.chatWindows[chatName].chatContainer.$window).attr("id", thisId);
                                                _this.chatWindows[chatName].chatContainer.$windowInnerContent.scrollTop(0);

                                            },
                                        })
                                    });
                                }

                                for (var i = 0; i < result.History.length ; i++)
                                    AddMsg(result.History[i]);
                            },
                        });
                    }

                    function AddMsg(message) {

                        var $messageP;
                        var $pText;
                        if (message.Message.substr(0, 5) == "*-*¡¿") {
                            var fileName = message.Message.substr(message.Message.lastIndexOf("/") + 1).substr(22);//.substr(22) sin fecha
                            $pText = $("<p/>").text("Compartío archivo...").addClass("sendFileTxt");//.appendTo($(".chat-text-wrapper :last"));

                            $messageP = $('<a>').text(fileName).addClass("sendFileA");
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
                            $messageP.appendTo($(".chat-text-wrapper", $lastMessage));
                            if ($pText != undefined)
                                $pText.insertBefore($messageP);
                        }
                        else {
                            var $chatMessage = $("<div/>").addClass("chat-message").attr("data-val-user-from", message.UserFromId);
                            $chatMessage.appendTo(_this.chatWindows[chatName].chatContainer.$windowInnerContent);
                            if (_this.opts.user.Id == message.UserFromId) {
                                //Background gris clarito cuando hablo yo
                                $chatMessage.css({
                                    'background-color': "rgb(230, 230, 230)",
                                    background: "-webkit-linear-gradient(left,white, LightGray)"
                                });

                            }
                            var $gravatarWrapper = (_this.opts.user.Id == message.UserFromId) ?
                                   $("<div/>").addClass("chat-gravatar-wrapper").css("float", "right").attr("height", "30px").attr("width", "30px").appendTo($chatMessage) :
                                $("<div/>").addClass("chat-gravatar-wrapper").appendTo($chatMessage);
                            var $textWrapper = $("<div/>").addClass("chat-text-wrapper").appendTo($chatMessage);                            

                            $messageP.appendTo($textWrapper);
                            if ($pText != undefined)
                                $pText.insertBefore($messageP);

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
                            //Coloco nombre en chat
                            var date = new Date(parseInt(message.DateTime.substring(6, message.DateTime.length - 2)));
                            if (_this.opts.user.Id != message.UserFromId) {
                                var spanUsrName = messageUserFrom.User.Name;
                                $("<span/>").text(ReduceName(spanUsrName,10) + formatDateText(date)).addClass("spanUserName").insertBefore($("</br>").insertBefore($pText == undefined ? $messageP : $pText));
                            }
                            else {
                                $("<span/>").text(formatDateText(date)).css("float", "right").css("padding-top", "8px").addClass("spanUserName").insertBefore($("</br>").insertBefore($pText == undefined ? $chatMessage : $pText));
                            }
                            var $img = messageUserFrom.User.Avatar;
                            $("<img/>").attr("src", "data:image/jpg;base64," + $img).attr("height", "30px").attr("width", "30px").appendTo($gravatarWrapper);
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
                        return;
                    }

                    if ($(_this.chatContainer.$windowInnerContent).children(userIdJQ).length > 0) {
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
                        _this.usersById[userId].Status = status;
                    }
                },

                usersListChanged: function (usersList) {
                    /// <summary>Called by the adapter when the users list changes</summary>
                    /// <param FullName="usersList" type="Object">The new user list</param>

                    // initializes the user list with the current user, because he/she will not be retrieved
                    _this.usersById = {};
                    _this.usersById[_this.opts.user.Id] = _this.opts.user;

                    _this.chatContainer.getContent().html('');
                    if (usersList.length == 0 || usersList.Name == "No se encontraron usuarios con el nombre especificado") {
                        $("<div/>").addClass("user-list-empty").text(_this.opts.emptyRoomText).appendTo(_this.chatContainer.getContent());
                    }
                    else {
                        for (var i = 0; i < usersList.length; i++) {
                            if (usersList[i].Role != 2 && usersList[i].Id != _this.opts.user.Id) {
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
                                var $userListItem = $("<div/>")
                                    .addClass("user-list-item")
                                    .attr("data-val-id", usersList[i].Id)
                                    .attr("id", "list-" + usersList[i].Id)
                                    .appendTo(_this.chatContainer.getContent());
                                ContextMenu($userListItem);

                                $("<img/>")
                                    .addClass("profile-picture")
                                    //.attr("src", usersList[i].Avatar)
                                    .attr("src", "data:image/jpg;base64," + usersList[i].Avatar)//
                                    .appendTo($userListItem);

                                $("<div/>")
                                    .addClass("profile-status")
                                    .addClass(statusStr)//usersList[i].Status == 0 ? "offline" : "online"
                                    .appendTo($userListItem);

                                $("<div/>")
                                    .addClass("content")
                                    .text(CapitalizeString(usersList[i].Name.indexOf("/") >= 0 ? usersList[i].Name.split("/")[0] : usersList[i].Name))
                                    .appendTo($userListItem);

                                $("<div/>")
                                  .addClass("subContent")
                                  .text(usersList[i].Name.indexOf("/") >=0 ?CapitalizeString(usersList[i].Name.split("/")[1]):' ')
                                  .appendTo($userListItem);

                                // makes a click in the user to either create a new chat window or open an existing
                                // I must clusure the 'i'
                                (function (otherUserId) {
                                    // handles clicking in a user. Starts up a new chat session

                                    $userListItem.click(function () {
                                        if (_this.chatWindows[otherUserId]) {
                                            if ((_this.chatWindows[otherUserId].chatContainer.$window).attr("id") != otherUserId) {
                                                _this.chatWindows[(_this.chatWindows[otherUserId].chatContainer.$window).attr("id")] = _this.chatWindows[otherUserId];
                                                delete (_this.chatWindows[otherUserId]);
                                                _this.createNewChatWindow(otherUserId);
                                            }
                                            else {
                                                _this.chatWindows[otherUserId].focus();
                                            }
                                        } else
                                            _this.createNewChatWindow(otherUserId);
                                    });
                                })(usersList[i].Id);
                            }
                        }
                    }

                    //filtro de usuarios 
                    var $userFilter = $("<div/>")
                        .addClass("user-list-filter")
                        .attr("data-val-filter", "Filtro")
                        .attr("id", "Filtro")
                        .appendTo(_this.chatContainer.getContent());
                    $("<img/>")
                        .addClass("filter-picture")
                        .attr("src", URLImage + "ChatJs/Images/search.gif") //URLImage +
                        .appendTo($userFilter);
                    var $textFilter = $("<input/>")
                         .addClass("filter")
                         .attr({ type: 'textbox', id: 'filterByUser', name: 'filterByUser', value: '', placeholder: 'Ingrese nombre...' })
                         .text("filtro")
                         .attr("title", "Ingrese su búsqueda")
                         .appendTo($userFilter);

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
                            _this.client.usersListChanged(usersList);
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


                            if (usersFiltered.length == 0)
                                _this.client.usersListChanged(usersList);

                            else {
                                for (var i = 0; i < usersList.length; i++) {//elimina div
                                    $("#list-" + usersList[i].Id).remove();
                                }

                                for (var i = 0; i < usersFiltered.length; i++) {
                                    if (usersList[i].Role != 2 && usersFiltered[i].Id != _this.opts.user.Id) {

                                        _this.usersById[usersFiltered[i].Id] = usersFiltered[i];
                                        var statusStr;
                                        switch (usersFiltered[i].Status) {
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
                                        var $userListItem = $("<div/>")
                                            .addClass("user-list-item")
                                            .attr("data-val-id", usersFiltered[i].Id)
                                            .attr("id", "list-" + usersFiltered[i].Id)
                                            .appendTo(_this.chatContainer.getContent());
                                        ContextMenu($userListItem);

                                        $("<img/>")
                                            .addClass("profile-picture")
                                            .attr("src", "data:image/jpg;base64," + usersFiltered[i].Avatar)
                                            .appendTo($userListItem);

                                        $("<div/>")
                                            .addClass("profile-status")
                                            .addClass(statusStr)//usersFiltered[i].Status == 0 ? "offline" : "online"
                                            .appendTo($userListItem);
                                        var nameUser = CapitalizeString(usersFiltered[i].Name);

                                        $("<div/>")
                                            .addClass("content")
                                            .text(CapitalizeString(nameUser.indexOf("/") >= 0 ? nameUser.split("/")[0] : nameUser))
                                            .appendTo($userListItem);

                                        $("<div/>")
                                          .addClass("subContent")
                                          .text(nameUser.indexOf("/") >= 0 ? CapitalizeString(nameUser.split("/")[1]) : ' ')
                                          .appendTo($userListItem);                                                                               

                                        (function (otherUserId) {

                                            $userListItem.click(function () {
                                                if (_this.chatWindows[otherUserId]) {
                                                    _this.chatWindows[otherUserId].focus();
                                                } else
                                                    _this.createNewChatWindow(otherUserId);
                                            });
                                        })(usersFiltered[i].Id);
                                    }
                                }
                            }
                        }
                    });

                    // update the online status of the remaining windows
                    for (var i in _this.chatWindows) {
                        if (_this.usersById[i] == undefined)//.Status
                            _this.chatWindows[i].setOnlineStatus(-2);// sin estado (grupo)
                        else
                            _this.chatWindows[i].setOnlineStatus(_this.usersById[i].Status);
                    }

                    _this.chatContainer.setVisible(true);

                    function CapitalizeString(string) {
                        var arrayWords;
                        var returnString = "";
                        var len;
                        arrayWords = string.split(" ");
                        len = arrayWords.length;
                        for (var indexCap = 0; indexCap < len ; indexCap++) {
                            if (indexCap != (len - 1)) {
                                returnString = returnString + ucFirst(arrayWords[indexCap]) + " ";
                            }
                            else {
                                returnString = returnString + ucFirst(arrayWords[indexCap]);
                            }
                        }
                        return returnString;
                    }
                    function ucFirst(string) {
                        return string.substr(0, 1).toUpperCase() + string.substr(1, string.length).toLowerCase();
                    }
                },

                showError: function (errorMessage) {
                    // todo
                }
            };

            _this.opts.adapter.init(_this, function () {
                /// <summary>Called by the adapter when all the adapter initialization is done already</summary>
                /// <param FullName="usersList" type=""></param>

                // gets the user list
                _this.opts.adapter.server.getUsersList(function (usersList) {
                    _this.client.usersListChanged(usersList);
                    _this.loadWindows();// carga historial por cookie
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
                        for (var i = 0; i < result.History.length ; i++)
                            _this.client.sendMsgToUsers(result.History[i]);
                        //Cambio color de título (Verde no leido)
                        for (var chatWin in _this.chatWindows) {
                            _this.chatWindows[chatWin].chatContainer.$windowTitle.css("background-color", "rgb(51, 153, 51)");
                            _this.chatWindows[chatWin].chatContainer.$window.click(function () {
                                $(this).off("click");
                                $(this).children(".chat-window-title").css("background-color", "rgb(109, 132, 180)");
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
            _this.createCookie("chat_state", JSON.stringify(openedChatWindows), 365);
        },

        createNewChatWindow: function (otherUserId, initialToggleState, initialFocusState) {

            if (!initialToggleState)
                initialToggleState = "maximized";

            if (!initialFocusState)
                initialFocusState = "focused";

            var _this = this;

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

            // this cannot be in t
            _this.chatWindows[otherUserId.toString()] = newChatWindow;

             SetFunctionUpload(otherUserId.toString());

            //_this.chatWindows[otherUser.Id.toString()] = newChatWindow;
            _this.saveWindows();
        },

        createNewChatWindowGroup: function (users, initialToggleState, initialFocusState) {

            if (!initialToggleState)
                initialToggleState = "maximized";

            if (!initialFocusState)
                initialFocusState = "focused";

            var _this = this;

            var otherUser;
            for (var i = users.length - 1 ; i >= 0; i--) {
                if (users[i].UserId != _this.opts.user.Id) {
                    otherUser = _this.usersById[users[i].UserId];
                    break;
                }
            }
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
                userIsOnline: otherUser.Status, //== 1
                adapter: _this.opts.adapter,
                typingText: "noHistory",//_this.opts.typingText,
                onClose: function () {
                    for (var win in _this.chatWindows) {
                        if (_this.chatWindows[win].chatContainer.$window.attr("id") == "toDelete")
                            delete _this.chatWindows[win];
                    }
                    //habia problema al cerrar las ventanas solucionado arriba
                    //delete _this.chatWindows[otherUser.Id];
                    $.organizeChatContainers();
                    _this.saveWindows();
                },
                onToggleStateChanged: function (toggleState) {
                    _this.saveWindows();
                }
            });
            var chatName = "";
            for (var i = 0; i < users.length; i++) {
                if (users[i].UserId != _this.opts.user.Id)
                    chatName += users[i].UserId + "/";
            }
            // this cannot be in t
            chatName = chatName.substring(0, chatName.length - 1);

            SetFunctionUpload(chatName);

            _this.chatWindows[chatName] = newChatWindow;
            (_this.chatWindows[chatName].chatContainer.$window).attr("id", chatName);
            for (var i = 0; i < users.length; i++) {
                if (users[i].UserId != _this.opts.user.Id && users[i].UserId != otherUser.Id) {
                    var $txtNewUser = $("<div/>")
                                    .attr("id", "addedUser" + users[i].UserId)//+ otherUserId + "/"
                                    .addClass("addUserTxt")
                                    .html(_this.usersById[users[i].UserId].Name.split("/")[0])
                                    .appendTo(_this.chatWindows[chatName].chatContainer.$windowTitle);

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

    $.LogOut = function (options) {// grabo offline y hora de ultimo acceso 
        var data = (options.userId === undefined) ? options : options.userId;
        $.ajax({
            type: "POST",
            async: false,
            url: URLBase + '/leavechat',
            data: { userId: data },
            cache: false,
            success: function (result) {

            }
        });
    };
})(jQuery);
// creates a chat user-list

function formatDateText(date) {
    return " (" + (date.getDate() <= 9 ? ("0" + date.getDate()) : date.getDate()) + "-"
                         + ((date.getMonth() + 1) <= 9 ? ("0" + (date.getMonth() + 1)) : (date.getMonth() + 1)) + "-"
        + (date.getFullYear()).toString().substring(2) + " " + (date.getHours() <= 9 ? ("0" + date.getHours())
        : date.getHours()) + ":" + (date.getMinutes() <= 9 ? ("0" + date.getMinutes()) : date.getMinutes()) + "hs)"; 
}

function ReduceName(name, len) {
    return name.length >= len ? (name.substring(0, len) + "...") : name;
}

function SetFunctionUpload(ids) {
    var idsBar = ids.replace(new RegExp("/", 'g'), "-");
    $("#upFile").attr("id", "upFile" + idsBar);

    $("#upFile" + idsBar).fileupload({
        dataType: 'json',
        url: URLBase + '/UploadFiles',
        dropZone: $("div[id='" + ids + "']"),//[id*=
        formData: { idUsers: ids },
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
            else
                 {          
                var replaceTxt = data.result.name.indexOf(new Date().getFullYear() + "-" + ((new Date().getMonth() + 1) <= 9 ? ("0" + (new Date().getMonth() + 1)) : (new Date().getMonth() + 1)));
                var severRute = data.result.name.substr(0, replaceTxt).replace(new RegExp("-", 'g'), "/");

                var rute = severRute + data.result.name.substr(replaceTxt);
                $("div[id='" + data.result.type + "'] .chat-window-text-box").text("*-*¡¿" + rute);

                var e = jQuery.Event("keypress");
                e.which = 13; //choose the one you want
                e.keyCode = 13;
                $("div[id='" + data.result.type + "'] .chat-window-text-box").trigger(e);
            }
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
    )
        $("div[id='" + ids + "']").on(
            'dragleave',
            function (e) {
                e.preventDefault();
                $("div[id='" + ids + "']").removeClass("chatFile");
            }
        )
        $("div[id='" + ids + "']").on(
        'drop',
        function (e) {
            e.preventDefault();
            $("div[id='" + ids + "']").removeClass("chatFile");
        }
    )
}

function AjaxDownloadFile(fileURL) {
    //http://stackoverflow.com/questions/16086162/handle-file-download-from-ajax-post
    window.location.href = URLBase + "/DownloadFile?file=" + fileURL; 
}

function ExtImg(ext)
{   
    var img = ext.substring(ext.lastIndexOf(".") + 1);
    switch (img) {
        case "doc": case "docx": case "txt": case "docm":
            img="word.gif";
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
            img="default.png"
    }
    return 'url("' + URLImage + 'ChatJs/images/ext/' + img + '")';
}

function EditText(append) {
    $editText = $("<div/>").addClass("editText").appendTo(append);

    $editColor = $("<div/>").addClass("editColor").appendTo(append);
    $editColor.empty().addColorPicker({
        clickCallback: function (c) {            
            var $contentTxt = $editColor.parent().children(".chat-window-text-box");
            if ($editColor.data("color") != undefined && $editColor.data("color") != "")
                $contentTxt.html($contentTxt.html().replace('<span style="color:' + $editColor.data("color") + '">', '').replace('</span>', ''));
       
            $contentTxt.html(('<span style="color:' + c + '">' + $contentTxt.html() + "</span>"));
            $editColor.data("color", c);         
        }
    });

    var fonts = 'ChatJs/Images/fonts/';
    $b = $("<img/>").addClass('editFont').attr("src", URLImage + fonts + 'b.png').appendTo($editText);
    $i = $("<img/>").addClass('editFont').attr("src", URLImage + fonts + 'i.png').appendTo($editText);
    $u = $("<img/>").addClass('editFont').attr("src", URLImage + fonts + 'u.png').appendTo($editText);
    $s = $("<img/>").addClass('editFont').attr("src", URLImage + fonts + 's.png').appendTo($editText);  
    $('<span />').text("Tamaño").addClass("sizeTxt").appendTo($editText);
    $tA = $('<select />').appendTo($editText);
    $img = $("<img/>").addClass('editFont').addClass("editTextImg").attr("src", URLImage + fonts + 'image.png').appendTo($editText);
    var $loc = $("<input/>").attr("type", "file").css("height", "0px").css("width", "0px").appendTo($editText);

    $loc.change(function () {
        var validExt = "jpg,jpeg,gif,png,bmp,div";
        $loc.attr("id", "locIMG");
        var file = document.getElementById("locIMG").value;
        var extFile = (file).substring(file.lastIndexOf(".") + 1);
        var fReader = new FileReader();
        if ($.inArray(extFile, validExt.split(",")) > -1) {     
            //fReader.onload = (function (theFile) {
            //    var image = new Image();
            //    image.src = theFile.target.result;
            //    if (image.width >= 800)
            //        alert("no");
            //    //image.onload = function() {                   
            //    //    console.log(this.width);            
            //    //};
            //});
            fReader.readAsDataURL(document.getElementById("locIMG").files[0]);
            fReader.onloadend = function (event) {
                var img = event.target.result;
                var $text = $loc.parent().parent().children(".chat-window-text-box");
                $text.html($text.html() + '<img class="embebedIMG" src="' + img + '"/>');            
            }
        }
        else
            bootbox.alert("Formato no valido de imagen");
        $loc.attr("id", "");
    });

    for (var i = 8; i < 20;i+=2){
        if (i == 12)
            $('<option selected value/>').text(i).appendTo($tA);
    else
            $('<option />').text(i).appendTo($tA);
    }

    $tA.on('change', function () {
        var $text = $(this).parent().parent().children(".chat-window-text-box");
   
        for (var i = 8; i < 20; i += 2) 
             $text.html($text.html().replace('<span style="font-size:'+i+'px">', ''));
            $text.html($text.html().replace('</span>',''));
            $text.html('<span style="font-size:'+ this.value+ 'px">' + $text.html() + "</span>");      
    });

    $('.editFont').click(function () {
        var img = $(this).attr("src").substring($(this).attr("src").lastIndexOf("/") + 1);
        var type = img.substring(0, img.lastIndexOf("."));
        var $text = $(this).parent().parent().children(".chat-window-text-box");
        if (type == 'image')
            $loc.click();

        $(this).data("active", !$(this).data("active"));
        var active = $(this).data("active");
        $(this).css("opacity", active ? '1' : '0.5');
        if (active)
            $text.html("<" + type + ">" + $text.html() + "</" + type + ">");
        else
            $text.html($text.html().replace("<" + type + ">", "").replace("</" + type + ">", ""));

        //if (window.getSelection().getRangeAt(0).startContainer.parentNode == $text[0])//Para saber si esta seleccionado el textedit
        //{
        //    $text.html($text.html().replace(window.getSelection().toString(), "<" + type + ">" +
        //                window.getSelection().toString() + "</" + type + ">"));           
        //    //$text.html("<b>" + $text.text() + "</b>");
        //}
    });
}

$(document).ready(function () {
    var $ul = $("<ul />").addClass("custom-menu")
        .append('<li data-action="info">Ver Información</li>')
        .append('<li data-action="mail">Enviar Mail</li>')
        .append('<li data-action="msg">Escribir Mensaje</li>')
        .appendTo("body");
});

function ContextMenu(userList) {
    if (URLServices == '') return;

    // http://jsfiddle.net/u2kJq/241/   
    $(userList).bind("contextmenu", function (event) {
        event.preventDefault();
        $(".custom-menu").toggle(100).
        css({
            top: event.pageY + "px",
            left: event.pageX + "px"
        });
    });

    $(document).bind("mousedown", function (e) {
                //e.preventDefault();
                //e.stopPropagation();
                //e.stopImmediatePropagation();
        var divId = $(e.target).parent().attr("id") == undefined ? $(e.target).parent().parent().attr("id") :
            $(e.target).parent().attr("id");

        if (divId == undefined)
            return;
        if (divId.substring(0, 5) == 'list-' || divId.substring(0, 3) == 'gID' || divId == 'groupDiv') {
              
            if (divId == 'groupDiv')
                divId = $(e.target).attr("id");
            $(".custom-menu").data("idUser", divId);
            $(".custom-menu").hide(100);
        }
    });

    // If the menu element is clicked
    $(".custom-menu li").click(function (event) {
        event.preventDefault();
        event.stopPropagation();
        event.stopImmediatePropagation();
        // This is the triggered action name
        var userId = $(this).parent().data("idUser").replace("list-", '');

        switch ($(this).attr("data-action")) {      
            case "info":
                if (!isNaN(userId))
                    bootboxUserInfo(UserFromWS(userId.split(",")));
                else {
                    if (userId.indexOf("gID") != -1){
                        var users = userId.replace("gID", '').split(",");
                        bootboxUserInfo(UserFromWS(users));
                    }
                }
                break;

            case "mail":            
                var user;
                if (!isNaN(userId)) 
                    user = UserFromWS(userId.split(","));
                else {
                    if (userId.indexOf("gID") != -1) {
                        var users = userId.replace("gID", '').split(",");
                        user=UserFromWS(users);
                    }
                }

                if (user != undefined && user.length >= 1) {
                    var mail='';
                    for (var u in user) {
                        if (user[u].eMail.Mail != undefined && user[u].eMail.Mail != '')
                        mail += user[u].eMail.Mail+';';
                    }

                    if (mail !='')
                            window.location.href='mailto:' + mail;
                        else
                            bootbox.alert("Dirección de mail no disponible");
                }
                else
                    bootbox.alert("Información de usuario no disponible");
                break;

            case "msg": $("div[id='" + $(this).parent().data("idUser") + "']").click(); break;
        }
        $(".custom-menu").hide(100);
    });
}

function UserFromWS(userId) {
    var users = [];
    for (var us in userId){
        $.ajax({
            type: "GET",
            async: false,
            url: URLServices,
            data: { userId: userId[us] },//userId
            success: function (d) {
                 users.push($.parseJSON(d));           
            },
            error: function (xhr, ajaxOptions, thrownError) {
             //   bootbox.alert("Información de usuario no disponible");         
            }
        });
    }
    return users;
}

function bootboxUserInfo(user) {
    if (user == undefined || user.length == 0)
        bootbox.alert("Información de usuario no disponible");
    else {
        var content = '';
        for (var u in user) {

            var mail = user[u].eMail.Mail == null ? '' : user[u].eMail.Mail;
            var photo = '';
            if (user[u].Picture != null && user[u].Picture != '') {
                $.ajax({
                    type: "POST",
                    async: false,
                    url: URLServer + "Chat/PathToBase64",
                    data: { path: user[u].Picture },
                    success: function (d) {
                        photo = d.Base64;
                    },
                });

            }
            photo = photo == '' ? '' : '<img src= "data:image/jpeg;base64,' + photo + '" height="50" width="50"> <hr>';
            content += '<label >Usuario</label> <span> ' + user[u].Name + '</span></br>' +
                                    '<label >Nombre</label> <span> ' + user[u].Nombres + '</span></br>' +
                                    '<label >Apellido</label> <span> ' + user[u].Apellidos + '</span> </br>' +
                                    '<label >Mail</label> <a href ="mailto:' + mail + '"> ' + mail + '</a> </br>' +
                                    '<label >Puesto</label> <span> ' + user[u].puesto + '</span> </br>' +
                                    '<label >Teléfono</label> <span> ' + user[u].telefono + '</span> </br>' +
                                    photo;
        }
        bootbox.dialog({
            title: "Información de Contacto",
            message:
             '<div class="row"> <div class="col-md-12"> <form class="form-horizontal" style="padding-left: 100px"> ' +
                               content + '</form> </div>  </div>',
            buttons: {
                success: {
                    label: "Aceptar",
                    className: "btn-success",
                    callback: function () {
                    }
                }
            }
        }
        );
    }
}

//HTML
       //<script>
       //     //Inicializacion de Chat
       //     $(document).ready(function () {             
       //         var URLServer = "http://localhost/ZambaChat/";
       //         var thisDomain = "http://localhost/Zamba";
    
       //         LoadChat(GetUID(), URLServer, URLService, thisDomain);

       //         $("#logOutSite").click(function () {
       //             $.ajax({
       //                 type: "POST",
       //                 async: false,
       //                 url: URLServer +  "Chat/CookieRemove",
       //                 success: function (d) {   
       //                     $.LogOut(GetUID());
       //                 },
       //             });
       //         });

       //     });
//</script>

