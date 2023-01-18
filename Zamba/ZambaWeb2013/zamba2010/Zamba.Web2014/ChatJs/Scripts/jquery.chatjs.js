//Query:
//delete from chatusers
//insert into chatusers select id,(nombres + ' ' + apellido) as name,'/9j/4AAQSkZJRgABAQEASABIAAD/4QAWRXhpZgAATU0AKgAAAAgAAAAAAAD/7AARRHVja3kAAQAEAAAAUAAA/9sAQwACAgICAgECAgICAwICAwMGBAMDAwMHBQUEBggHCQgIBwgICQoNCwkKDAoICAsPCwwNDg4PDgkLEBEQDhENDg4O/9sAQwECAwMDAwMHBAQHDgkICQ4ODg4ODg4ODg4ODg4ODg4ODg4ODg4ODg4ODg4ODg4ODg4ODg4ODg4ODg4ODg4ODg4O/8AAEQgAIAAgAwERAAIRAQMRAf/EABsAAAIBBQAAAAAAAAAAAAAAAAcIAAECAwQJ/8QAKRAAAgEDBAEDAwUAAAAAAAAAAQIDBAURAAYHITESE0FSYYEIFFFxsf/EABkBAAIDAQAAAAAAAAAAAAAAAAAHAQUGAv/EACYRAAAFBAEDBQEAAAAAAAAAAAABAgQRAwUSIRMGIkFRYXGx0TH/2gAMAwEAAhEDEQA/AO/mgAGO9OSbZs+5R29aV7lcWjDtCkgjWNT49TEHBPwMePxq6ZWyq9TnMJFE+ulJmrCJUL9l8kWzeFbLRLTSW65JGZPYeQOrqCMlWAGSPkED/dQ9ttZmWZnKRLG50nh4RCvQEvVMLwTQAJfzP7tNzvXtIjIk9PDJGT4YBAhI/KkaZtkxWwKPBn9hXXuUvznyRfQ2OElkqub4pEQskFHLJIR4XICDP9k65vuKGPyZDux5Lf8AwRhyNLQM4Y3kSKJndgiKCWZjgAfyToIpEGcBSP1Abg2vcmsSWu50txvFO8iz/tJRIEjIHTFes5HQzkd6Y3Trd1T5OVBkk4idbC36hcNqvHxrI1FMxvQpwDuXbFrW/U92udPbLrUSRCFqqURiRAD0rE4zk9jPfXnR1C1dVeNVNBmkp/m9g6edNqXImosiUcRPoG3jkSaFZI3WSNgCrKcgj7HS5Mo0YY5HOyCEc578vdz5cu+2lrpIrFb5ViSljb0rI4UFmf6jknGegPHzpvWG30KLNDiO9W5/An79cK9V2tvPYnUfoBXv/ca1+IyOQnv/AHGjEGQPXA2+LxbuYbXtl6+SSxXFnjalkbKxv6GZWX6SSoBx5z34Gsff2NCoxXXjvRG/afI1/T7+vTeooT2KnXvGoH//2Q==',0,getdate(),'Zamba-Chat',0 from usrtable
//pasar a intranet marsh MVC
var URLBase = "http://localhost/ZambaChat";

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
      //  this.$id = _this.user.id;// agregado verificar
        this.$windowContent = null;
        this.$windowInnerContent = null;
        this.$textBox = null;
    }

    // Separate functionality from object creation
    ChatContainer.prototype = {

        init: function () {
            var _this = this;

            // container
            _this.$window = $("<div/>").addClass("chat-window")
                 // .attr("id", "ContenedorPrincipal")
                .appendTo($("body"))
            
            //var $userListItem = $("<div/>")
            //                       .addClass("user-list-item")
            //                       .attr("data-val-id", usersList[i].Id)
            //                       .attr("id", usersList[i].Id)
            //                       .appendTo(_this.chatContainer.getContent());


            _this.$windowTitle = $("<div/>").addClass("chat-window-title").appendTo(_this.$window);
            //var $closeButton = $("<div/>").addClass("close").appendTo(_this.$windowTitle);

            //$closeButton.click(function (e) {//salir del chat
            //    if (!_this.opts.canClose) {
            //        e.preventDefault();
            //        window.location = "/Home/LeaveChat";
            //    }
            //});
            if (_this.opts.canClose)         {
                var $closeButton = $("<div/>").addClass("close").appendTo(_this.$windowTitle);          
            }

            var $minimizedButton = $("<div/>").addClass("minimized").appendTo(_this.$windowTitle);

            $minimizedButton.click(function () {  //maximiza minimiza antes estaba debajo de  if (_this.opts.canClose) {  
                _this.$windowContent.toggle();
                if (_this.$windowContent.is(":visible") && _this.opts.showTextBox)
                    _this.$textBox.focus();
                _this.opts.onToggleStateChanged(_this.$windowContent.is(":visible") ? "maximized" : "minimized");

                $(_this.$windowTitle.children(".minimized")).css("background-image", _this.$windowContent.is(":visible") ?
                    "url(../../ChatJs/images/minimize.png)" : "url(../../ChatJs/images/restore.png)");               
            });

            if (!_this.opts.canClose) {             
                var $configButton = $("<div/>").addClass("config").attr("id","configId").appendTo(_this.$windowTitle);
                $configButton.click(function () {

                    if ($("#statusMenuId").length) {
                        $("#statusMenuId").remove();
                    }
                    else {
                        var $statusMenu = $("<div/>").addClass("statusMenu").attr("id", "statusMenuId").appendTo($(_this.$windowTitle));
                        $statusMenu.html((_this.$windowTitle.data("viewChangeStatus") != true)?"Desactivar chat:":" ");
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
                            //$statusMenu.html("Desactivar chat:");

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
                                    url: URLBase + '/chat/disablechat',
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
                        //$("<div/>").addClass("changeAvatar").appendTo($statusMenu)
                        var $changeAvatar = $("<button/>").addClass("btn btn-primary btn-xs").addClass("btn").css("left", "125px").css("top", "-82px").attr("id", "changeAvatarId").appendTo($statusMenu).html("Cambiar Avatar");
                        $changeAvatar.click(function () {
                            if ($("#statusMenuId").length)
                              $("#statusMenuId").remove();
                            var $statusMenu = $("<div/>").addClass("statusMenu").attr("id", "statusMenuId").appendTo($(_this.$windowTitle));
                            var $img = $("<img/>").addClass("changeAvatarImg").attr("id", "changeAvatarImgId").attr("src",
                                 _this.$windowTitle.children("#containerUserName").children(".mainAvatar").attr("src")).appendTo($statusMenu);
                            var $changePicBtn = $("<button/>").addClass("btn btn-primary btn-xs").addClass("btn").css("left", "-60px").attr("id", "changePicBtnId").html("Cambiar avatar").appendTo($statusMenu);
                            var $defaultPicBtn = $("<button/>").addClass("btn btn-primary btn-xs").addClass("btn").css("left", "65px").attr("id", "defaultPicBtnId").html("Avatar predeterminado").appendTo($statusMenu);
                            var $loc = $("<input/>").attr("type", "file").attr("id","locId").css("height", "0px").appendTo($statusMenu);
                             $changePicBtn.click(function () {
                               $loc.click();                                                        
                            });
                             $loc.change(function () {
                               const validExt = "jpg,jpeg,gif,png,bmp,div";
                               var file=document.getElementById("locId").value;
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
                                   alert("Formato no valido de imagen");                               
                             });

                             $defaultPicBtn.click(function () {   
                                 $("#changeAvatarImgId").attr("src", '../../ChatJs/images/defaultAvatar.jpg');
                                 $("#mainAvatarId").attr("src", '../../ChatJs/images/defaultAvatar.jpg');                                

                                 changeAvatarDB("default");
                                 ChangeStatus(-1);
                             });

                             function changeAvatarDB(img){
                                 $.ajax({
                                     type: "POST",
                                     async: false,
                                     url: URLBase + '/chat/changeavatar',
                                     data: { userId: parseInt((_this.$window).attr("id")), avatar: (img.replace("data:image/jpeg;base64,", "")) },
                                     success: function (result) {
                                     }
                                 });
                                 $("#refreshAvatarId").click();

                             }
                        });                       
                    }
                });
              
                var $hideShowWin = $("<div/>").addClass("hideShowWin").appendTo(_this.$windowTitle);
                $hideShowWin.click(function () {
                    $("#hideShowWindows").click();
                });
            }
            function ChangeStatus(status)
            {
                //var htmlContent = _this.$windowInnerContent.html();
                //_this.$windowInnerContent.html('<p><img src="../../ChatJs/images/ajax-loader.gif" /></p>');
                var shouldGetHist = false;
                if ($("#changeStatusDiv").data("status") == 0) 
                    shouldGetHist = true;
              
                switch (status) {
                    case 0: 
                        $("#mainStatusId").css('background-image', 'url("../../ChatJs/images/chat-offline.png")');
                        break;
                    case 1:
                        $("#mainStatusId").css('background-image', 'url("../../ChatJs/images/chat-online.png")');
                        break;
                    case 2:
                        $("#mainStatusId").css('background-image', 'url("../../ChatJs/images/chat-busy.png")');
                        break;
                    //case 3:
                    //    $("#mainStatusId").css('background-image', 'url("../../ChatJs/images/chat-dontdisturb.png")');
                    //    break;
                }
                ChangeStatusDB(status)
                $("#statusMenuId").remove();
                $("#changeStatusDiv").data("status", status);
                $("#changeStatusDiv").click();

                if(shouldGetHist)
                    $("#noReadUserHistoryDiv").click();
                // _this.opts.changeStatus((_this.$window).attr("id"),status);
                //_this.$windowInnerContent.html(htmlContent);
            }

            function ChangeStatusDB(status) {
                $.ajax({
                    type: "POST",
                    async: false,
                    url:  URLBase + '/chat/changestatus',//URLBase +
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
                        var $addUserTxt = $("<input/>")
                            .attr("type", "text")
                            .attr("list", "datalist"+otherUserId1)
                            .attr("placeholder", "Agregar participante")
                            .attr("id", "addNewUser-" + otherUserId1)
                            .addClass("addUserTxt")
                            .appendTo(_this.$windowTitle);
                        //Agregar usuario al presionar Enter
                        $addUserTxt.keypress(function (e) {
                            if (e.which == 13) 
                                $("#tildeAddUser-" + otherUserId1).click();                            
                        });

                        var $datalist = $("<datalist/>")
                            .attr("id", "datalist"+otherUserId1)
                            .appendTo($addUserTxt);

                        var usersName = [];
                        var usersId = [];
                        $.ajax({
                            type: "GET",
                            async: false,
                            url: URLBase + '/chat/getuserschat',
                            success: function (result) {
                                var otherUsers = otherUserId.split("/");
                                for (var i = 0; i < result.Users.length; i++) {// itero cada usuario retornado por ajax
                                    if (result.Users[i].Id != myUserId) {// no agregarme a la lista
                                        //
                                        var add = true;
                                        for (var j = 0; j < otherUsers.length; j++) {//por cada usuario en este chat
                                            if (result.Users[i].Id == otherUsers[j])
                                                add = false;
                                        }
                                        if (add) {
                                            $("#datalist" + otherUserId1).append($("<option>", {
                                                id: result.Users[i].Id,
                                                text: result.Users[i].Name
                                            }));
                                            usersName.push(result.Users[i].Name);
                                            usersId.push(result.Users[i].Id)
                                        }
                                    }
                                }
                            },
                        });
                       
                        var $okAddUserButton = $("<div/>").addClass("okAddUser").attr("id", "tildeAddUser-" + otherUserId1).appendTo(_this.$windowTitle);
                        $okAddUserButton.click(function (e) {
                    
                                if ($addUserTxt.val() == "" || ! IsUserSelected($addUserTxt.val())) {
                                    $addUserTxt.val("");                     
                                    $addUserTxt.attr("placeholder", "Seleccione de la lista")
                       
                                }
                                else {
                                    selectedUserId = SelectedUserId($addUserTxt.val());
                                
                                    var $txtNewUser = $("<div/>")      //nuevo usuario div                                                
                                         .attr("id", "addedUser" + selectedUserId)//+ otherUserId + "/"
                                         .addClass("addUserTxt")                                
                                         .html($addUserTxt.val())
                                         .appendTo(_this.$windowTitle);

                                    //(_this.$windowTitle).html((_this.$windowTitle).html + $addUserTxt.val());
                                   // $("#containerUserName") ver aca grupo
                                    $addUserTxt.remove();
                                    (_this.$window).attr("id", (_this.$window).attr("id")+"/"+selectedUserId);// por esto hace el split
                                    $okAddUserButton.remove();                              

                                    GetHistory();
                                    //titulo de ventana grupal
                                    var title = $("div[class=text]", _this.$windowTitle).text();
                                    if (title.indexOf(", ") !=-1){
                                    $("div[class=text]", _this.$windowTitle).text(title.substring(0,title.indexOf(",")) + ", y (" +
                                        ((_this.$window).attr("id").split("/").length -1) + ") personas");
                                    }
                                    else {
                                        $("div[class=text]", _this.$windowTitle).text(title + ", " + $addUserTxt.val());
                                    }

                                    var $removeUserButton = $("<div/>").addClass("removeUser").attr("id", "removeUser-" + selectedUserId).appendTo(_this.$windowTitle);
                                    $removeUserButton.click(function (e) {
                                      
                                        $("#addNewUser-" + otherUserId1).remove(); // que no pueda agregar
                                        $("#tildeAddUser-" + otherUserId1).remove();

                                        var userToRemoveId = $removeUserButton.attr("id").split("-")[1];
                                        var newIdChat = ((_this.$window).attr("id")).replace("/" + userToRemoveId, "");
                                        (_this.$window).attr("id", newIdChat); //lo elimino de ids del chat
                                        
                                        //titulo de ventana grupal
                                        var title = $("div[class=text]", _this.$windowTitle).text();                                       
                                            if ((_this.$window).attr("id").split("/").length == 1) {
                                                $("div[class=text]", _this.$windowTitle).text(title.substring(0, title.indexOf(",")));
                                            }
                                            else{
                                                $("div[class=text]", _this.$windowTitle).text(title.substring(0, title.indexOf(",")) + ", +(" +
                                                ((_this.$window).attr("id").split("/").length - 1) + ") persona(s)");
                                            }                                        

                                        $("#addedUser" +userToRemoveId).remove();//elimino txtbox
                                        $removeUserButton.remove();//saco boton X     
                                        GetHistory();
                                    });

                                    (_this.$windowTitle).attr("id", "thisWinTitle");// muestro el boton de ver usuarios
                                    $("#thisWinTitle .showAllUsers").css("display", "block");
                                    (_this.$windowTitle).attr("id", "windowTitle");
                                }                        
                        });

                        function GetHistory(){
                            $.ajax({
                                type: "GET",
                                async: false,
                                url: URLBase + '/chat/getmessagehistory',
                                data: { usersId: myUserId + "/" + (_this.$window).attr("id") },
                                success: function (result) {
                                    _this.$windowInnerContent[0].innerHTML = "";
                                    if (result.History.length > 0) {

                                        var $getMoreMsgBtn = $("<div/>").addClass("getMoreMsg") //boton traer mensajes anteriores
                                            //.attr("id", "getmoremsg" + replaceAll((_this.$window).attr("id"), "/", "-"))
                                            .attr("id", "getmoremsg" + (_this.$window).attr("id").replace(new RegExp ("/",'g'), "-"))
                                            .text("Traer mensajes anteriores")
                                            .appendTo(_this.$windowInnerContent);

                                        //function replaceAll(text, search, newstring) {
                                        //    return text.replace(new RegExp(search, 'g'), newstring);
                                        //}
                                        $getMoreMsgBtn.click(function (e) {
                                            var otherUserId = (_this.$window).attr("id");

                                            var msgNum = parseInt((_this.$window).attr("msgNum"));
                                            (_this.$window).attr("msgNum", msgNum > 0 ? msgNum + 1 : 1);

                                            $.ajax({
                                                type: "GET",
                                                async: false,
                                                url: URLBase + '/chat/getmoremsghistory',
                                                data: {
                                                    usersId: myUserId + "/" + otherUserId,
                                                    cant: parseInt((_this.$window).attr("msgNum"))
                                                },
                                                success: function (result) {
                                                    var thisId = (_this.$window).attr("id");                                                   
                                                    var getMsgNum = 5;//para saber cuando cargo todos los mensajes
                                                    var msgNum = getMsgNum * (parseInt((_this.$window).attr("msgNum")) + 1);
                                                    if (result.History.length < msgNum) {
                                                //.replace("/", "-")).remove();//elimino boton de traer mas
                                                        //   $("#getmoremsg" + replaceAll(thisId,"/","-")).remove();
                                                        $("#getmoremsg" + thisId.replace(new RegExp("/", 'g'), "-")).remove();
                                                        //function replaceAll(text, search, newstring) {
                                                        // return  text.replace(new RegExp(search, 'g'), newstring);                                                          
                                                        //}
                                                        (_this.$window).attr("msgNum", "");
                                                    }
                                                    (_this.$window).attr("id", "clearOldMsg");

                                                    $("#clearOldMsg .chat-window-content .chat-window-inner-content .chat-message").each(function (indice, elemento) {
                                                        $(elemento).remove();
                                                    });
                                                    $("#clearOldMsg .chat-window-content .chat-window-inner-content .chat-date").each(function (indice, elemento) {                                                        
                                                        $(elemento).remove();
                                                    });
                                                    var cantSend;
                                                    for (var i = 0; i < result.History.length ; i++) {

                                                        if (i % getMsgNum == 0 || cantSend) {
                                                            cantSend = false;
                                                            if (i == 0 || result.History[i].UserFromId != result.History[i - 1].UserFromId) {
                                                                var fecha = new Date(parseInt((result.History[i].DateTime).substring(6, (result.History[i].DateTime).length - 2)));
                                                                fecha = "Enviado: (" + (fecha.getDate() > 9 ? fecha.getDate() : ("0" + fecha.getDate())) + "/" + ((fecha.getMonth() + 1) > 9 ?
                                                                    (fecha.getMonth() + 1) : ("0" + (fecha.getMonth() + 1))) + "/" + fecha.getFullYear() + ", " + fecha.getHours() +
                                                                    ":" + (fecha.getMinutes() > 9 ? fecha.getMinutes() : ("0" + fecha.getMinutes())) + ":" + (fecha.getSeconds() > 9 ? fecha.getSeconds() : "0" + fecha.getSeconds()) + ")";
                                                                var $chatDate = $("<div/>").addClass("chat-date").appendTo(_this.$windowInnerContent);
                                                                var $chatMsgDate = $("<p/>").text(fecha).appendTo($chatDate);
                                                            }
                                                            else {
                                                                cantSend = true;
                                                            }
                                                        }
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
                
                            function AddMsg(message)
                            {
                                var $messageP = $("<p/>").text(message.Message);
                                      
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
                                          $("<div/>").addClass("chat-gravatar-wrapper").css("float", "right").appendTo($chatMessage) :
                                          $("<div/>").addClass("chat-gravatar-wrapper").appendTo($chatMessage);                                    
                                 
                                    var $textWrapper = $("<div/>").addClass("chat-text-wrapper").appendTo($chatMessage);

                                    // add text
                                    $messageP.appendTo($textWrapper);

                                    var messageUserFrom;
                                    $.ajax({
                                        type: "GET",
                                        async: false,
                                        url: URLBase + '/chat/getuserinfo',
                                        data: { userId: message.UserFromId },
                                        success: function (result) {
                                            messageUserFrom = result;
                                        },
                                    });
                                      
                                    var $img = messageUserFrom.User.Avatar;
                                    $("<img/>").attr("src", "data:image/jpg;base64," + $img).appendTo($gravatarWrapper);
                                  //$("<img/>").attr("src", decodeURI($img)).appendTo($gravatarWrapper);
                                }
                                // scroll to the bottom
                                _this.$windowInnerContent.scrollTop(_this.$windowInnerContent[0].scrollHeight);
                            }
                        }
                    };

                    function IsUserSelected(selUser)
                        {
                        for (var i = 0; i < usersName.length; i++)
                        {
                            if (selUser==usersName[i])
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
                    $("#windowTitleId .addUserTxt").each(function (indice, elemento) {
                        elemento.style.display = modeDisplay;
                    });
                    $("#windowTitleId .removeUser").each(function (indice, elemento) {
                        elemento.style.display = modeDisplay;
                    });
                    $(_this.$windowTitle).attr("id", actualTitle);
                })
            }
            
            //var $closeButton = $("<div/>").addClass("close").appendTo(_this.$windowTitle);
            
            var containerUserName= $("<div/>").attr("id", "containerUserName").addClass("text").text(_this.opts.title).appendTo(_this.$windowTitle);

            // content
            _this.$windowContent = $("<div/>").addClass("chat-window-content").appendTo(_this.$window);
            if (_this.opts.initialToggleState == "minimized")
                _this.$windowContent.fadeOut();//tenia el hide comun

            _this.$windowInnerContent = $("<div/>").addClass("chat-window-inner-content").appendTo(_this.$windowContent);

            // text-box-wrapper
            if (_this.opts.showTextBox) {
                var $windowTextBoxWrapper = $("<div/>").addClass("chat-window-text-box-wrapper").appendTo(_this.$windowContent);
                _this.$textBox = $("<textarea />").attr("rows", "1").addClass("chat-window-text-box").appendTo($windowTextBoxWrapper);
                _this.$textBox.autosize();
            }
            // wire everything up
            //_this.$windowTitle.click(function () {  //maximiza minimiza
            //    _this.$windowContent.toggle();
            //    if (_this.$windowContent.is(":visible") && _this.opts.showTextBox)
            //        _this.$textBox.focus();
            //    _this.opts.onToggleStateChanged(_this.$windowContent.is(":visible") ? "maximized" : "minimized");
            //});

          
            //_this.$minimizedButton.click(function () {  //maximiza minimiza
            //    _this.$windowContent.toggle();
            //    if (_this.$windowContent.is(":visible") && _this.opts.showTextBox)
            //        _this.$textBox.focus();
            //    _this.opts.onToggleStateChanged(_this.$windowContent.is(":visible") ? "maximized" : "minimized");
            //});

            // enlists this container in the containers
            if (!$._chatContainers)
                $._chatContainers = new Array();
            $._chatContainers.push(_this);

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
            $("div[class=text]", _this.$windowTitle).text(title);
           
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

        //colapsar expandir contraer ventana principal
        //$("#1").mouseenter(function(){
        //    $(this).animate({        
        //        width: '232px'
        //    });
        //    $(this).children(".chat-window-content").children(".chat-window-inner-content").children(".user-list-item").each(function () {
        //        $(this).css("display", "block");

        //    })
        //});
        //$("#1").mouseleave(function () {
        //    $(this).animate({
        //        width: '50px'
        //    });
        //    $(this).children(".chat-window-content").children(".chat-window-inner-content").children(".user-list-item").each(function () {
        //        $(this).css("display", "none");
        //    })
        //});
        return chatContainer;
    };

    $.organizeChatContainers = function () {
        // this is the initial right offset
        var rightOffset = 10;
        var deltaOffset = 10;
        for (var i = 0; i < $._chatContainers.length; i++) {
            $._chatContainers[i].$window.css("right", rightOffset);
            rightOffset += $._chatContainers[i].$window.outerWidth() + deltaOffset;
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
                var $messageP = $("<p/>").text(message.Message);
                if (clientGuid)
                    $messageP.attr("data-val-client-guid", clientGuid).addClass("temp-message");

                linkify($messageP);
                emotify($messageP);

                // gets the last message to see if it's possible to just append the text
                var $lastMessage = $("div.chat-message:last", _this.chatContainer.$windowInnerContent);
                if ($lastMessage.length && $lastMessage.attr("data-val-user-from") == message.UserFromId) {
                    // we can just append text then
                    $messageP.appendTo($(".chat-text-wrapper", $lastMessage));
                }
                else {
                    // in this case we need to create a whole new message
                    var $chatMessage = $("<div/>").addClass("chat-message").attr("data-val-user-from", message.UserFromId);
                    $chatMessage.appendTo(_this.chatContainer.$windowInnerContent);
                  
                    var $gravatarWrapper = (_this.opts.myUser.Id == message.UserFromId) ?
                             $("<div/>").addClass("chat-gravatar-wrapper").css("float", "right").appendTo($chatMessage) :
                             $("<div/>").addClass("chat-gravatar-wrapper").appendTo($chatMessage);

                    var $textWrapper = $("<div/>").addClass("chat-text-wrapper").appendTo($chatMessage);
                    
                    // add text
                    $messageP.appendTo($textWrapper);

                    // add image
                    var messageUserFrom = _this.opts.chat.usersById[message.UserFromId];
                    // ver porque da error 
                //////    if(messageUserFrom.Avatar==null){
                //////        var $img = messageUserFrom.ProfilePictureUrl;
                //////    }
                //////else{
                //////        var $img = messageUserFrom.Avatar;
                //////    };
                    var $img = messageUserFrom.Avatar === null ? messageUserFrom.ProfilePictureUrl : messageUserFrom.Avatar;
                    $("<img/>").attr("src", "data:image/jpg;base64," + $img).appendTo($gravatarWrapper);
                    //$("<img/>").attr("src", decodeURI($img)).appendTo($gravatarWrapper);
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
                var $messageP = $("<p/>").text(msgHistory.Message);
                if (clientGuid)
                    $messageP.attr("data-val-client-guid", clientGuid).addClass("temp-message");

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
                              $("<div/>").addClass("chat-gravatar-wrapper").css("float", "right").appendTo($chatMessage) :
                              $("<div/>").addClass("chat-gravatar-wrapper").appendTo($chatMessage);

                    var $textWrapper = $("<div/>").addClass("chat-text-wrapper").appendTo($chatMessage);

                    // add text
                    $messageP.appendTo($textWrapper);

                    // add image
                    var messageUserFrom = _this.opts.chat.usersById[msgHistory.UserId];
                    // ver porque da error 
                    //if (messageUserFrom.Avatar == null) {
                    //    var $img = messageUserFrom.Avatar;
                    //}
                    //else {
                        var $img = messageUserFrom.Avatar;
                    //};
                        $("<img/>").attr("src", "data:image/jpg;base64," + $img).appendTo($gravatarWrapper);
                      //$("<img/>").attr("src", decodeURI($img)).appendTo($gravatarWrapper);
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
       
        //this.changeStatus= function () {

        //    alert(userId);
        //    //if (_this.chatWindows[otherUserId]) {
        //    //    var otherUser = _this.usersById[otherUserId];
        //    //_this.chatWindows[otherUserId].showTypingSignal(otherUser);
        //         var _this = this;
        //         _this.opts.adapter.server.changeStatus(_this.opts.userId, _this.opts.status);
        //};

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

            //var $tes = $("<div/>").attr("id", "tes");
            //$tes.click(function () {
            //    var a = 22;
            //});

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

            _this.chatContainer.$textBox.keypress(function (e) {
                // if a send typing signal is in course, remove it and create another
                //if (_this.$sendTypingSignalTimeout == undefined) {
                //    _this.$sendTypingSignalTimeout = setTimeout(function () {
                //        _this.$sendTypingSignalTimeout = undefined;
                //    }, 3000);
                //    _this.sendTypingSignal();
                //}
                //Lo saque yo,              

                if (e.which == 13) {
                    e.preventDefault();
                    if ($(this).val()) {
                        // _this.sendMessage($(this).val());
                        var thisId = $(_this.chatContainer.$window).attr("id");// quito agregar participante una vez que se envio mensaje
                        $(_this.chatContainer.$window).attr("id", "removeAddUser");
                        if ($("#removeAddUser .chat-window-title .okAddUser").attr("id") != undefined) {
                            $("#addNewUser-" + ($("#removeAddUser .chat-window-title .okAddUser").attr("id")).replace("tildeAddUser-", "")).remove();
                            $("#removeAddUser .chat-window-title .okAddUser").remove();
                            //    $("#removeAddUser .chat-window-title .addUser").click();
                        }
                        $("#removeAddUser .chat-window-title .addUser").remove();                     
                        $(_this.chatContainer.$window).attr("id", thisId);
                        //Quito las X de usuarios agregados a chat (Se establece chat con los usuarios ya agregados)
                        $(_this.chatContainer.$window).children(".chat-window-title").children(".removeUser").each(function () {
                            if ($.inArray(($(this).attr("id")).replace("removeUser-", ""), thisId.split("/")) > -1)
                                $(this).remove();                          
                        });
                     
                        //$("#tildeAddUser-" + otherUserId1).remove();
                        var $val = $(this).val();
                        $.ajax({
                            type: "GET",
                            async: false,
                            url: URLBase + '/chat/getuserinfo',
                            data:{userId: _this.opts.myUser.Id},
                            success: function (result) {
                                if (result.User.Role == 0) {
                                   //if (result.User.Status!=0){
                                        _this.sendMsgToUsers($val);
                                   // }
                                }
                                else
                                {
                                    var $append = _this.chatContainer.$windowContent.children(".chat-window-inner-content").children(".chat-message").last().children(".chat-text-wrapper")
                                    $("<HR>").css("width", "2%)").css("align","center")
                                        .appendTo($append);
                                    $("<p/>").text("Active el chat para poder enviar el mensaje")
                                        .css("color", "rgb(109, 132, 180)")
                                        .css("font-size", 10).css("text-align", "center")
                                        .appendTo($append);
                                    $("<HR>").css("width", "2%)").css("align", "center")
                                      .appendTo($append);
                                }
                            }                    
                        });                 
                        $(this).val('').trigger("autosize.resize");
                    }
                }
            });

            _this.chatContainer.setTitle(_this.opts.otherUser == null ? "" : _this.opts.otherUser.Name);      
            _this.chatContainer.setIdContainer(_this.opts.otherUser == null ? "" : _this.opts.otherUser.Id);           
     
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
                                    url: URLBase + '/chat/getmoremsghistory',
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
                                        var cantSend;
                                        for (var i = 0; i < result.History.length ; i++) {

                                            if (i % getMsgNum == 0 || cantSend) {
                                                cantSend = false;

                                                if (i == 0 || result.History[i].UserFromId != result.History[i - 1].UserFromId) {
                                                    var fecha = new Date(parseInt((result.History[i].DateTime).substring(6, (result.History[i].DateTime).length - 2)));
                                                    fecha = "(Enviado: " + (fecha.getDate() > 9 ? fecha.getDate() : ("0" + fecha.getDate())) + "/" + ((fecha.getMonth() + 1) > 9 ?
                                                        (fecha.getMonth() + 1) : ("0" + (fecha.getMonth() + 1))) + "/" + fecha.getFullYear() + ", " + fecha.getHours() +
                                                        ":" + (fecha.getMinutes() > 9 ? fecha.getMinutes() : ("0" + fecha.getMinutes())) + ":" + (fecha.getSeconds() > 9 ? fecha.getSeconds() : "0" + fecha.getSeconds()) + ")";

                                                    var $chatDate = $("<div/>").addClass("chat-date").appendTo(_this.chatContainer.$windowInnerContent);
                                                    var $chatMsgDate = $("<p/>").text(fecha).appendTo($chatDate);
                                                }
                                                else {
                                                    cantSend = true;
                                                }
                                            }
                                            _this.addMessage(result.History[i]);
                                        }
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

                    _this.chatContainer.setVisible(true);

                    if (_this.opts.initialFocusState == "focused")
                        _this.chatContainer.$textBox.focus();

                    // scroll to the bottom
                    _this.chatContainer.$windowInnerContent.scrollTop(_this.chatContainer.$windowInnerContent[0].scrollHeight);

                    if (_this.opts.onReady)
                        _this.opts.onReady(_this);
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
    //    var $addUserButton = $("<div/>").addClass("addUser").appendTo(_this.$windowTitle);
    //$addUserButton.click(function (e) {
    //    alert("aa")  ;
    //});

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
           // container.$window.attr("id", _this.opts.user.Id);
            // will create user list chat container
            _this.chatContainer = $.chatContainer({

                title: _this.opts.titleText,
                showTextBox: false,
                canClose: false,
                initialToggleState: mainChatWindowChatState,
                onCreated: function (container) {
                    container.$window.attr("id",_this.opts.user!=null? _this.opts.user.Id: "");// jrojas: id de contenedor principal
                   
                    var $mainStatus = $("<div/>").addClass("mainStatus").appendTo($("#" + container.$window.attr("id") + " .chat-window-title .text"))
                    .attr("id", "mainStatusId").css('background-image', 'url("../../ChatJs/images/chat-online.png")');

                    $("#mainStatusId").click(function () {

                        _this.chatContainer.$windowTitle.data("viewChangeStatus", true);
                        $("#configId").click();
                        _this.chatContainer.$windowTitle.data("viewChangeStatus", false);
                        $("#changeAvatarId").remove();
                    })

                //      $.ajax({
                //    type: "GET",
                //    async: false,
                //    url: URLBase + '/chat/getuserinfo',
                //    data: {
                //    userId: _this.opts.user.Id },
                //        success : function (result) {
                //            switch(result.User.Status) {
                //                case 0:
                //                    $("#mainStatusId").css('background-image', 'url("../../ChatJs/images/chat-offline.png")');
                //                    break;
                //                case 1:
                //                    $("#mainStatusId").css('background-image', 'url("../../ChatJs/images/chat-online.png")');
                //                    break;
                //                case 2:
                //                    $("#mainStatusId").css('background-image', 'url("../../ChatJs/images/chat-busy.png")');
                //                    break;
                //                case 3:
                //                    $("#mainStatusId").css('background-image', 'url("../../ChatJs/images/chat-dontdisturb.png")');
                //                    break;
                //            }
                //    },
                //});
                       var $changeStatusDiv = $("<div/>").attr("id", "changeStatusDiv").appendTo($("#" + container.$window.attr("id")));

                       $changeStatusDiv.click(function () {
                           _this.opts.adapter.server.changeStatus(_this.opts.user.Id, $("#changeStatusDiv").data("status"));                           
                       })
                   
                       var $noReadUserHistoryDiv = $("<div/>").attr("id", "noReadUserHistoryDiv").appendTo($("#" + container.$window.attr("id")));
                       $noReadUserHistoryDiv.click(function () {
                           var minimizeAllWin = $noReadUserHistoryDiv.data("minimizeAllWin") == true;
                           // Imagen minimizar/restaurar
                           _this.chatContainer.$windowTitle.children(".minimized").css("background-image", (minimizeAllWin) ?
                               "url(../../ChatJs/images/restore.png)" : "url(../../ChatJs/images/Minimize.png)");
                           //Color rojo a main title
                           $(_this.chatContainer.$windowTitle).css("background-color", (minimizeAllWin) ? "rgb(204, 0, 0)" : "rgb(109, 132, 180)");
                    
                           if (minimizeAllWin) {
                               $hideShowWindows.click();                             
                           }
                           else{
                               _this.loadWindows();

                           }
                       })

                       var $hideShowWindows = $("<div/>").attr("id", "hideShowWindows").appendTo($("#" + container.$window.attr("id")));
                       $hideShowWindows.click(function () {
                           $(".chat-window").each(function () {
                               {
                                   if (this.id != _this.opts.user.Id){
                                       $(this).children(".chat-window-content").fadeOut();
                                       $(this).children(".chat-window-title").children(".minimized").css("background-image", "url(../../ChatJs/images/restore.png)");
                                   }
                                   //if ($(this).css("display") == "none") {
                                       //    $(this).fadeIn();
                                       //}
                                       //else {
                                       //    $(this).fadeOut();
                                       //}
                               }
                           })
                       });
                       var $refreshAvatar = $("<div/>").attr("id", "refreshAvatarId").appendTo($("#" + container.$window.attr("id")));

                       $refreshAvatar.click(function () {
                           RefreshAvatar();
                       });
                    //Avatar en contenedor principal
                    function RefreshAvatar(){
                       $.ajax({
                           type: "GET",
                           async: false,
                           url: URLBase + '/chat/getuserinfo',
                           data: { userId: _this.opts.user.Id },
                           success: function (result) {
                               _this.opts.user.Avatar = result.User.Avatar;
                           },
                       });
                    }
                    RefreshAvatar();
                    var $mainAvatar = $("<img/>").addClass("mainAvatar").appendTo($("#" + container.$window.attr("id") + " .chat-window-title .text"))
                   .attr("src", "data:image/jpg;base64," + _this.opts.user.Avatar).attr("id","mainAvatarId"); //.css('background-image', 'url(' + _this.opts.user.Avatar + ')');
       
                    $("#mainAvatarId").click(function () {
                        $("#configId").click();
                        $("#changeAvatarId").click();
                    });

                    if (!container.$windowInnerContent.html()) {
                        var $loadingBox = $("<div/>").addClass("loading-box").appendTo(container.$windowInnerContent);
                        if (_this.opts.useActivityIndicatorPlugin)
                            $loadingBox.activity({ segments: 8, width: 3, space: 0, length: 3, color: '#666666', speed: 1.5 });
                    }
                },

                onToggleStateChanged: function (toggleState) {
                    _this.createCookie("main_window_chat_state", toggleState);
                }
            });
           
            // the client functions are functions that must be called by the chat-adapter to interact
            // with the chat
            _this.client = {
           
                // me parece que no se usa
                //sendMessage: function (message) {
                //    /// <summary>Called by the adapter when the OTHER user sends a message to the current user</summary>
                //    /// <param FullName="message" type="Object">Message object</param>
                //    if (message.UserFromId != _this.opts.user.Id) {
                //        // in this case this message did not came from myself
                //        if (!_this.chatWindows[message.UserFromId])
                //            _this.createNewChatWindow(message.UserFromId);
                //        else
                //            _this.chatWindows[message.UserFromId].addMessage(message);
                //        if (_this.opts.playSound)
                //            _this.playSound("/../../ChatJs/sounds/chat");

                //        // play sound here
                //    } else {
                //        if (_this.chatWindows[message.UserToId]) {
                //            _this.chatWindows[message.UserToId].addMessage(message);
                //        }
                //    }
                //},

                sendMsgToUsers: function (message) {                     
                    // Si esta el chat desactivado no recibe mensajes
                    if ($("#" + _this.opts.user.Id).children(".chat-window-title").data("switch")==true ||
                       $("#changeStatusDiv").data("status")=="0")
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

                    for (var i = 0; i < participants.length; i++)
                    {
                        if (participants[i].UserId == _this.opts.user.Id){
                            validUser = true;
                            break;
                        }
                    }
                    if (validUser) {                     

                        //quien envia           - a quien envia
                        if (msgHistory.UserId != _this.opts.user.Id) {                         
                            var participantsIds = [];
                            var usersSorted = [];
                            $.each(_this.chatWindows, function (index, value) {
                                //(value.chatContainer.$window).attr("id") estaba abajo lo reemplace por index porque tenia id toDelete
                                var usersByWindows = index.split("/").sort(function (a, b) { return a - b });
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

                            if (!existChat) {// if (!_this.chatWindows[msgHistory.UserId]) {

                                _this.createNewChatWindowGroup(participants);
                                GetHistory(_this.opts.user.Id + "/" + chatName);
                                var windowTitleName = ($(_this.chatWindows[chatName].chatContainer.$windowTitle).attr("id") == undefined) ?
                                    " " :  $(_this.chatWindows[chatName].chatContainer.$windowTitle).attr("id");
                                $(_this.chatWindows[chatName].chatContainer.$windowTitle).attr("id", "windowTitle");
                                $("#windowTitle .addUser").remove();

                                // Si hay mas de dos participantes cambia el titulo y coloca botón para mostrar/ocultar lista de participantes
                                if ((participants.length - 2) > 0) {
                                    $("#windowTitle .text").text(($("#windowTitle .text").text()) + ", y " + (participants.length - 2) + " persona(s)");
                                    var $showAllUsersButton = $("<div/>").addClass("showAllUsers")
                                           .attr("title", "Mostrar/Ocultar integrantes del chat")
                                           .attr("alt", "Agregar un nuevo integrante al chat")
                                           .css("display", "block").appendTo("#windowTitle .text");
                                    $(_this.chatWindows[chatName].chatContainer.$windowTitle).attr("id", " ");

                                    $showAllUsersButton.click(function (e) {
                                        $showAllUsersButton.attr("modeDisplay", ($showAllUsersButton.attr("modeDisplay")) == "none" ? "block" : "none");
                                        var modeDisplay = $showAllUsersButton.attr("modeDisplay");
                                        $(_this.chatWindows[chatName].chatContainer.$windowTitle).attr("id", "windowTitle");
                                        $("#windowTitle .addUserTxt").each(function (indice, elemento) {
                                            elemento.style.display = modeDisplay;
                                        });                                        
                                    })
                                }
                                // $(_this.chatWindows[chatName].chatContainer.$windowTitle).attr("id", " ");
                                $(_this.chatWindows[chatName].chatContainer.$windowTitle).attr("id", windowTitleName);

                            } else {
                                $.each(_this.chatWindows, function (index, value) {
                                    if (JSON.stringify((value.chatContainer.$window).attr("id").split("/").sort()) ==
                                                                        JSON.stringify(chatName.split("/").sort())) {
                                        if ((_this.chatContainer.$window).data("clearChat") == true) {
                                            $(value.chatContainer.$windowInnerContent).children(".chat-message").each(function () {
                                                $(this).remove();
                                            });
                                            var forCicle=message.ChatHistory.length;
                                            for (var i = 0; i < forCicle; i++) {
                                              //  var msg = message;
                                                //delete msg.ChatHistory;
                                                //msg.ChatHistory[0] = message.ChatHistory[i];

                                                //msg.ChatHistory = message.ChatHistory.reverse()
                                                if (i != 0) {
                                                    delete message.ChatHistory[message.ChatHistory.length];
                                                    message.ChatHistory.length -= 1;
                                                }
                                                else
                                                {
                                                    message.ChatHistory.reverse();
                                                }
                                                value.addMsgUsers(message);
                                            }
                                        }
                                        else{
                                            value.addMsgUsers(message);
                                        }
                                        //Coloco boton de minimizar porque se restaura
                                        value.chatContainer.$windowTitle.children(".minimized").css("background-image", "url(../../ChatJs/images/minimize.png)");
                                    }
                                });
                                //_this.chatWindows[chatName].addMsgUsers(message);
                            }

                         

                            if (_this.opts.playSound)
                                _this.playSound("/../../ChatJs/sounds/chat");

                            // play sound here
                        } else {//lo envie yo y queda en mi chat
                            if (_this.chatWindows[msgHistory.UserId]) {// se envia a 2 _this.opts.user.Id
                                _this.chatWindows[msgHistory.UserId].addMsgUsers(message);
                            }
                        }
                    }

                    function GetHistory(participantsIds) {
                        //   _this.chatWindows[chatName].chatContainer.$windowInnerContent.html("");
                        $.ajax({
                            type: "GET",
                            async: false,
                            url: URLBase + '/chat/getmessagehistory',
                            data: { usersId: participantsIds },
                            success: function (result) {
                                _this.chatWindows[chatName].chatContainer.$windowInnerContent.html("");
                                if (result.History.length == 5) {
                                    var $getMoreMsgBtn = $("<div/>").addClass("getMoreMsg") //boton traer mensajes anteriores
                                           .attr("id", "getmoremsg" + ((_this.chatWindows[chatName].chatContainer.$window).attr("id")).replace(new RegExp("/",'g'), "-"))
                                           .text("Traer mensajes anteriores")
                                           .appendTo(_this.chatWindows[chatName].chatContainer.$windowInnerContent);

                                    //function replaceAll(text, search, newstring) {
                                    //    return text.replace(new RegExp(search, 'g'), newstring);
                                    //}
                                    $getMoreMsgBtn.click(function (e) {
                                        var otherUserId = (_this.chatWindows[chatName].chatContainer.$window).attr("id");

                                        var msgNum = parseInt((_this.chatWindows[chatName].chatContainer.$window).attr("msgNum"));
                                        (_this.chatWindows[chatName].chatContainer.$window).attr("msgNum", msgNum > 0 ? msgNum + 1 : 1);

                                        var myUserId = _this.opts.user.Id;
                                        $.ajax({
                                            type: "GET",
                                            async: false,
                                            url: URLBase + '/chat/getmoremsghistory',
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
                                                var cantSend;
                                                for (var i = 0; i < result.History.length ; i++) {

                                                    if (i % getMsgNum == 0 || cantSend) {
                                                        cantSend = false;
                                                        if (i == 0 || result.History[i].UserFromId != result.History[i - 1].UserFromId) {
                                                            var fecha = new Date(parseInt((result.History[i].DateTime).substring(6, (result.History[i].DateTime).length - 2)));
                                                            fecha = "Enviado: (" + (fecha.getDate() > 9 ? fecha.getDate() : ("0" + fecha.getDate())) + "/" + ((fecha.getMonth() + 1) > 9 ?
                                                                (fecha.getMonth() + 1) : ("0" + (fecha.getMonth() + 1))) + "/" + fecha.getFullYear() + ", " + fecha.getHours() +
                                                                ":" + (fecha.getMinutes() > 9 ? fecha.getMinutes() : ("0" + fecha.getMinutes())) + ":" + (fecha.getSeconds() > 9 ? fecha.getSeconds() : "0" + fecha.getSeconds()) + ")";
                                                            var $chatDate = $("<div/>").addClass("chat-date").appendTo(_this.chatWindows[chatName].chatContainer.$windowInnerContent);
                                                            var $chatMsgDate = $("<p/>").text(fecha).appendTo($chatDate);
                                                        }
                                                        else {
                                                            cantSend = true;
                                                        }
                                                    }
                                                    AddMsg(result.History[i]);
                                                }
                                                (_this.chatWindows[chatName].chatContainer.$window).attr("id", thisId);
                                                _this.chatWindows[chatName].chatContainer.$windowInnerContent.scrollTop(0);
                                                //(_this.$window).attr("id", thisId); esto estaba raro no andaba se cambio por el d arriba
                                                // this.chatWindows[chatName].chatContainer.$windowInnerContent.scrollTop(0);
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
                        var $messageP = $("<p/>").text(message.Message);
                        var $lastMessage = $("div.chat-message:last", _this.chatWindows[chatName].chatContainer.$windowInnerContent);
                        if ($lastMessage.length && $lastMessage.attr("data-val-user-from") == message.UserFromId) {                           
                            $messageP.appendTo($(".chat-text-wrapper", $lastMessage));
                        }
                        else {
                            var $chatMessage = $("<div/>").addClass("chat-message").attr("data-val-user-from", message.UserFromId);
                            $chatMessage.appendTo(_this.chatWindows[chatName].chatContainer.$windowInnerContent);
                            if (_this.opts.user.Id == message.UserFromId) {

                            }
                            var $gravatarWrapper =(_this.opts.user.Id == message.UserFromId)?
                                   $("<div/>").addClass("chat-gravatar-wrapper").css("float", "right").appendTo($chatMessage):
                                $("<div/>").addClass("chat-gravatar-wrapper").appendTo($chatMessage);
                            var $textWrapper = $("<div/>").addClass("chat-text-wrapper").appendTo($chatMessage);                            
                            $messageP.appendTo($textWrapper);

                            var messageUserFrom;
                            $.ajax({
                                type: "GET",
                                async: false,
                                url: URLBase + '/chat/getuserinfo',
                                data: { userId: message.UserFromId },
                                success: function (result) {
                                    messageUserFrom = result;
                                },
                            });
                            var $img = messageUserFrom.User.Avatar;
                            $("<img/>").attr("src", "data:image/jpg;base64," + $img).appendTo($gravatarWrapper);
                         // $("<img/>").attr("src", decodeURI($img)).appendTo($gravatarWrapper);
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

                    //Cambia el avatar
                    if (status == -1) {
                        var avatar = response[2];
                        _this.usersById[userId].Avatar = avatar;
                        $(_this.chatContainer.$windowInnerContent).children("#" + userId).children(".profile-picture").attr("src", "data:image/jpg;base64," + avatar);
                        return;
                    }

                    if ($(_this.chatContainer.$windowInnerContent).children("#" + userId).length > 0) {
                        var $listUser = $(_this.chatContainer.$windowInnerContent).children("#" + userId).children(".profile-status");
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
                    if (usersList.length == 0 || usersList.Name== "No se encontraron usuarios con el nombre especificado") {
                        $("<div/>").addClass("user-list-empty").text(_this.opts.emptyRoomText).appendTo(_this.chatContainer.getContent());
                    }
                    else {
                        for (var i = 0; i < usersList.length; i++) {
                            if (usersList[i].Id != _this.opts.user.Id) {
                                //var dd = $("#filterByUser").val();
                                _this.usersById[usersList[i].Id]= usersList[i];
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
                                    .attr("id", usersList[i].Id)
                                    .appendTo(_this.chatContainer.getContent());

                                $("<img/>")
                                    .addClass("profile-picture")
                                    //.attr("src", usersList[i].Avatar)
                                    .attr("src", "data:image/jpg;base64,"+ usersList[i].Avatar)//
                                    .appendTo($userListItem);

                                $("<div/>")
                                    .addClass("profile-status")
                                    .addClass(statusStr)//usersList[i].Status == 0 ? "offline" : "online"
                                    .appendTo($userListItem);

                                $("<div/>")
                                    .addClass("content")
                                    .text(CapitalizeString(usersList[i].Name))
                                    .appendTo($userListItem);

                                // makes a click in the user to either create a new chat window or open an existing
                                // I must clusure the 'i'
                                (function (otherUserId) {
                                    // handles clicking in a user. Starts up a new chat session

                                    $userListItem.click(function () {

                                        //$.each(_this.chatWindows, function (index, value) {
                                        //    var usersByWindows = (value.chatContainer.$window).attr("id").split("/").sort(function (a, b) { return a - b });
                                        //    usersSorted.push(usersByWindows);
                                        //});

                                        if (_this.chatWindows[otherUserId]) {
                                            if ((_this.chatWindows[otherUserId].chatContainer.$window).attr("id") != otherUserId) {
                                                _this.chatWindows[(_this.chatWindows[otherUserId].chatContainer.$window).attr("id")] = _this.chatWindows[otherUserId];
                                                delete (_this.chatWindows[otherUserId]);
                                                _this.createNewChatWindow(otherUserId);
                                            }
                                            else{
                                                _this.chatWindows[otherUserId].focus();                                           
                                            }
                                        } else
                                            _this.createNewChatWindow(otherUserId);
                                    });


                                    //$userListItem.click(function () {
                                    //    if (_this.chatWindows[otherUserId]) {
                                    //        _this.chatWindows[otherUserId].focus();
                                    //    } else
                                    //        _this.createNewChatWindow(otherUserId);
                                    //});
                                })(usersList[i].Id);
                            }
                        }                       
                    }
                    //filtro de usuarios   
                    //armo div
                        var $userFilter = $("<div/>")
                            .addClass("user-list-filter")
                            .attr("data-val-filter", "Filtro")
                            .attr("id", "Filtro")
                            .appendTo(_this.chatContainer.getContent());
                        $("<img/>")
                            .addClass("filter-picture")
                            .attr("src", "../../ChatJs/images/search.gif")
                            .appendTo($userFilter);             
                       var $textFilter= $("<input/>")
                            .addClass("filter")
                            .attr({ type: 'textbox', id:'filterByUser',name:'filterByUser', value:'', placeholder:'Ingrese nombre...'})                           
                            .text("filtro")
                            .appendTo($userFilter);                      
                    
                           $textFilter.keydown(function (letra) {
                           var box = $("#filterByUser").val();
                           if (letra.keyCode == 8) { //keycode 8= backspace saco el ultimo caracter                            
                               var txtBox = box.substring(0, box.length - 1);
                           }
                           else {
                                var txtBox= box + String.fromCharCode(letra.which); 
                           };
                             
                            if (txtBox == "")
                           {
                              _this.client.usersListChanged(usersList);
                           }
                           else
                           {                                                       
                               var usersFiltered = usersList.filter(filterName);

                               if (usersFiltered.length == 0) {

                                  //_this.chatContainer.getContent().html('');
                                  //$("<div/>").addClass("user-list-empty").text(_this.opts.emptyRoomText).appendTo(_this.chatContainer.getContent());
                                   //$("<div/>").attr("id","lala").text("_this.opts.emptyRoomText");//.appendTo(_this.chatContainer.getContent());
                                   //setTimeout(function () {
                                   //    $("#lala").fadeOut(1500);
                                   //}, 3000);
                                   //var noUser = usersList[0];                         
                                   //noUser.Name = "No se encontraron usuarios con el nombre especificado";
                                   _this.client.usersListChanged(usersList);
                               }
                               else {                            
                                       for (var i = 0; i < usersList.length; i++) {//elimina div
                                           $("#" + usersList[i].Id).remove();                                      
                                       }
                              
                                      for (var i = 0; i < usersFiltered.length; i++) {
                                       if (usersFiltered[i].Id != _this.opts.user.Id) {
                                         
                                           _this.usersById[usersFiltered[i].Id]= usersFiltered[i];
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
                                               .attr("id", usersFiltered[i].Id)
                                               .appendTo(_this.chatContainer.getContent());

                                           $("<img/>")
                                               .addClass("profile-picture")
                                               .attr("src", "data:image/jpg;base64," + usersFiltered[i].Avatar)
                                           //.attr("src", usersFiltered[i].Avatar)
                                               //.attr("src", usersFiltered[i].ProfilePictureUrl)
                                               .appendTo($userListItem);

                                           $("<div/>")
                                               .addClass("profile-status")
                                               .addClass(statusStr)//usersFiltered[i].Status == 0 ? "offline" : "online"
                                               .appendTo($userListItem);
                                           var nameUser = CapitalizeString(usersFiltered[i].Name);
                                           $("<div/>")
                                               .addClass("content")
                                               .text(nameUser)
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

                            //estaba aca
                                
                               function filterName(users) {                               

                                   if (((users.Name).toUpperCase()).indexOf((txtBox).toUpperCase()) > -1) {
                                       return true;
                                   } else {
                                       return false;
                                   };
                               }
                           }

                           });

                    // update the online status of the remaining windows
                    for (var i in _this.chatWindows) {
                        if (_this.usersById && _this.usersById[i])
                            _this.chatWindows[i].setOnlineStatus(_this.usersById[i].Status == 1);
                        else
                            _this.chatWindows[i].setOnlineStatus(false);
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
                url: URLBase + '/chat/getnoreaduserhistory',
                data: { userId: userId },
                success: function (result) {
                   
                    if (result.History.length > 0) {
                        //Quito ventanas viejas
                        //Quito DOM
                                    //$(".chat-window").each(function () {
                                    //    {
                                    //        if (this.id != _this.opts.user.Id) 
                                    //            this.remove();                                
                                    //    }
                                    //});
                                    ////Quito objeto
                                    //for (var window in (_this.chatWindows))                     
                                    //    delete _this.chatWindows[window];

                        //$(".chat-window").each(function () {
                        //    {
                        //        if (this.id != _this.opts.user.Id) 


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
                    // delete _this.chatWindows[otherUser.Id]; // asi elimina por usuario
                    //(_this.chatWindows[15].chatContainer.$window).attr("id")
                    $.each(_this.chatWindows, function (index, value) {
                        //var usersByWindows = (value.chatContainer.$window).attr("id").split("/").sort(function (a, b) { return a - b });
                        //if ((value.chatContainer.$window).attr("id")=="toDelete")
                           // delete value;
                        //delete _this.chatWindows[index];
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
            for (var i = users.length-1 ; i >= 0; i--)
            {
                if (users[i].UserId != _this.opts.user.Id)
                    {
                        otherUser = _this.usersById[users[i].UserId];
                        break;          
                    }
            }
            //var otherUser = _this.usersById[users[users.length - 1].UserId];//quien escribe
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
            var chatName="";
            for (var i = 0;i< users.length; i++)
            {
                if (users[i].UserId != _this.opts.user.Id)
                    chatName += users[i].UserId + "/";   
            }
            // this cannot be in t
            chatName = chatName.substring(0, chatName.length - 1);
            _this.chatWindows[chatName] = newChatWindow;
            (_this.chatWindows[chatName].chatContainer.$window).attr("id", chatName);
            for (var i = 0; i < users.length; i++) {
                if (users[i].UserId != _this.opts.user.Id && users[i].UserId != otherUser.Id) {
                    var $txtNewUser = $("<div/>")
                                    .attr("id", "addedUser" + users[i].UserId)//+ otherUserId + "/"
                                    .addClass("addUserTxt")
                                    .html(_this.usersById[users[i].UserId].Name)
                                    .appendTo(_this.chatWindows[chatName].chatContainer.$windowTitle);

                 //var $txtNewUser = $("<input/>")
                 //                               .attr("type", "text")
                 //                               .attr("id", "addedUser" + users[i].UserId)
                 //                               .addClass("addUserTxt")
                 //                               .attr("disabled", "disabled")
                 //                               .val(_this.usersById[users[i].UserId].Name)
                 //                               .appendTo(_this.chatWindows[chatName].chatContainer.$windowTitle);
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
            url: URLBase + '/chat/leavechat',
            data: { userId: data },
            success: function (result) {
            }
        });    
    };

})(jQuery);

// creates a chat user-list

