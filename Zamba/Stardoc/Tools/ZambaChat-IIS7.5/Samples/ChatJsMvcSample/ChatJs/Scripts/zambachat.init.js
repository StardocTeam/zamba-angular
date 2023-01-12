var URLBase;
var URLImage;
var chatMode;
var appZChat;
var thisUserId;
var thisExtUserId;
var zCollLnk;
var zLinkState = "restore";
var myUserInfo;
var myUserChatInfo;
var widthCompact = 300;
var heightCompact = 400;
var titleHeight = 40;
var youtubePath = "youtube.com/watch?v=";
var loadSignalRAdapter = false;
var colServer;
var isCS;
var URLServer;
var thisDomain;
var thisChatObj;
var isTaskChat;

function ZambaChatInit(thisUserIdChat, URLServ, chatModeOpen, appZC, thisDom, CollLnk, zIsCollserver, zCollServer, taskChat) {
    isTaskChat = taskChat;
    URLServer = URLServ;
    thisDomain = thisDom;
    thisUserId = thisUserIdChat;
    chatMode = chatModeOpen == undefined ? "normal" : chatModeOpen;
    isCS = zIsCollserver;
    zCollLnk = CollLnk.substring(CollLnk.length - 1) == "/" ? CollLnk : CollLnk + "/";
    colServer = (zCollServer == undefined || zCollServer == "") ? "" : (zCollServer.substring(zCollServer.length - 1) == "/" ? zCollServer : zCollServer + "/");

    var referenceFilesCSS = referenceFiles + "/Styles";
    appZChat = appZC;
    rightOffset = appZChat == "ZambaLink" ? 0 : 10;
    deltaOffset = appZChat == "ZambaLink" ? 0 : 10;

    fonts = 'ChatJs/Images/fonts/';
    URLBase = URLServer.substring(URLServer.length - 1) == "/" ? URLServer + "chat" : URLServer + "/chat";
    URLImage = thisDomain;

    //Inclusion de librerias JS y CSS
    (function () {
        //Estilos:
        var cssLnkBegin = '<link rel="stylesheet" type="text/css" href="';
        loadStyles([
            "jquery.chatjs.css",
            "Bootstrap/css/bootstrap.min.css",
            "jquery.typeahead.css",
            "toastr.css",
            "fontGoogleArimo.css",
            "font-awesome.min.css",
            //Emoji
            "Emojis/lib/css/nanoscroller.css",
            "Emojis/lib/css/emoji.css",
        ]);

        loadJSAsync([
            'jquery.autosize.min.js',
            'jquery.activity-indicator-1.0.0.min.js',
            'jquery.typeahead.js',
            'jquery.ui.widget.js',
            'jquery.fileupload.js',
            'bootbox.min.js',
            'ColorJs.js',
            'toastr.js',
            'moment-es.js',
            //Emojis
            'Emojis/lib/js/nanoscroller.min.js',
            'Emojis/lib/js/tether.min.js',
            'Emojis/lib/js/config.js',
            'Emojis/lib/js/util.js',
            'Emojis/lib/js/jquery.emojiarea.js',
            'Emojis/lib/js/emoji-picker.js',
        ]);

        var jsSync = [
            'jquery.signalR-2.2.0.min.js',
            'jquery-ui-1.11.4.js',
            'Bootstrap/js/bootstrap.min.js',
            'zambachat.fn.js',
            'zambalink.js',
            'collaboration.js',
            'groups.js',
            'localStorage.js',
        ];
        if (colServer != "")
            jsSync.push(colServer + 'signalr/hubs');
        if (!zIsCollserver)
            jsSync.push(URLServer + 'signalr/hubs');
        jsSync.push('jquery.chatjs.signalradapter.js');
        loadJSSync(jsSync);
 
        function loadJSAsync(urls) {
            jQuery.cachedScript = function (url, options) {
                // Allow user to set any option except for dataType, cache, and url
                options = $.extend(options || {}, {
                    dataType: "script",
                    cache: true,
                    url: url
                });  // Use $.ajax() since it is more flexible than $.getScript
                // Return the jqXHR object so we can chain callbacks
                return jQuery.ajax(options);
            };
            for (var i = 0; i <= urls.length - 1; i++)
                $.cachedScript((urls[i].indexOf("Emoji") > -1 ? referenceFiles : referenceFilesJS) + "/" + urls[i]).done(function (script, textStatus) {
                    // console.log(textStatus);
                });

        }
        function loadJSSync(urls) {
            (function rec() {        
                if (!urls.length) {
                    ChatJSReady();
                }
                else{
                    var url = urls[0];
                    urls = urls.slice(1);
                    loadScript(url, rec);        
                }               
            })();    
            function loadScript(url, callback) {
                var head = document.getElementsByTagName('head')[0];
                var script = document.createElement('script');
                script.type = 'text/javascript';
                script.src = url.indexOf("http")>-1?url: referenceFilesJS + '/' + url;
                script.onreadystatechange = callback;
                script.onload = callback;
                head.appendChild(script);
            }           
            //$('head').append('<script src="' + referenceFilesJS + '/' + urls[i] + '" ' + jsLnkEnd);      
        }
        function loadStyles(urls) {
            for (var i = 0; i <= urls.length - 1; i++) {
                var path;
                if (urls[i].indexOf("Emoji") > -1)
                    path = referenceFiles;
                else if (urls[i].indexOf("Bootstrap") > -1)
                    path = referenceFilesJS;
                else 
                    path = referenceFilesCSS;

                $('head').append(cssLnkBegin + path + "/" + urls[i] + '" />');
            }
        }
    })();

    var userData;
    function ChatJSReady() {
        CheckCorrectUserInfo();
        myUserInfo = myUserInfoZamba.Get();
        myUserChatInfo = myUserInfoChat.Get(); //GetChatUser(thisUserId);
        //No existe usuario en ChatUser, lo creo
        if (myUserChatInfo == undefined || myUserChatInfo.Id == 0) {
            if (myUserInfo == undefined) {
                alert("Usuario no registrado");
                return;
            }
            var userAdded = CreateChatUser(myUserInfo);
            if (!parseInt(userAdded)) {
                alert("Error al registrar nuevo usuario");
                return;
            } else
                myUserChatInfo = GetChatUser(thisUserId);
        }

        // Tomo los de zamba, si esta vacio, tomo los de chatuser
        if (myUserInfo == undefined) myUserInfo = new Object();
        if (myUserInfo.CORREO == undefined || myUserInfo.CORREO == "") myUserInfo.CORREO = myUserChatInfo.Email;

        myUserChatInfoCol = zCollServer == "" ? null : myUserInfoExt.Get();
        if (myUserChatInfoCol != undefined) thisExtUserId = myUserChatInfoCol.Id;

        $(function () {
            //jQuery.support.cors = true;
            (function InitChat(userData) {
                if (userData === null) {
                    toastr.error('Usuario no registrado, por favor contacte al administrador de sistemas.', 'Error al iniciar el chat');
                    return;
                }
                thisChatObj = $.chat({
                    user: userData,
                    typingText: ' esta escribiendo...',
                    titleText: "   " + userData.Name,
                    emptyRoomText: "No hay usuarios online en este momento.",
                    adapter: new SignalRAdapter()
                });
            })(myUserChatInfo);
        });
    };
}