function initChat(thisUserId) {
    (function ($) {
        //Id	Name
        // 34971	Alejandro Gerbasi
        //32547	CLAUDIO APARICIO
   
        var URLBase = "http://localhost/ZambaChat/";
        var referenceFiles = "/ChatJs";
        var referenceFilesJS = referenceFiles+"/Scripts";
        // document.title = "Zamba Chat";
 
        $('head').append('<script src="'+referenceFilesJS+'/Bootstrap/js/bootstrap.min.js" type="text/javascript"></script>');
        $('head').append('<script src="' + referenceFilesJS + '/jquery.signalR-1.1.2.min.js"></script>');
        $('head').append('<script src="'+URLBase+'signalr/hubs" type="text/javascript"></script>');
        $('head').append('<script src="' + referenceFilesJS + '/jquery.chatjs.signalradapter.js" type="text/javascript"></script>');
        $('head').append('<script src="' + referenceFilesJS + '/jquery.autosize.min.js" type="text/javascript"></script>');
        $('head').append('<script src="' + referenceFilesJS + '/jquery.activity-indicator-1.0.0.min.js" type="text/javascript"></script>');
        $('head').append('<script src="' + referenceFilesJS + '/jquery.chatjs.js" type="text/javascript"></script>');
        $('head').append('<script src="' + referenceFilesJS + '/scripts.js" type="text/javascript"></script>');

        $('head').append('<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js"></script>');

        //$('head').append('<link rel="stylesheet" type="text/css" href="'+referenceFiles+'/Styles/styles.css" />');
        $('head').append('<link rel="stylesheet" type="text/css" href="'+referenceFiles+'/Styles/jquery.chatjs.less" />');
        $('head').append('<link rel="stylesheet" type="text/css" href="'+referenceFilesJS+'/Bootstrap/css/bootstrap.min.css" />');
        $('head').append('<link rel="stylesheet" type="text/css" href="'+referenceFilesJS+'/Bootstrap/css/bootstrap-responsive.min.css" />');

        $(function () {
            var userData;

            jQuery.support.cors = true;

            $.ajax({
                type: "GET",
                async: false,
                beforeSend: function (xhr) {
                    xhr.withCredentials = false;
                },
                xhrFields: {
                    withCredentials: false
                },
                crossDomain: true,
                data: { userId: thisUserId },
                url: URLBase + 'chat/joinchat',
                success: function (result) {                   
                    userData = result.User;
                }
            });

            if (userData != undefined) {
                $.chat({
                    user: {
                        Id: thisUserId,
                        Name: userData.Name,
                        Avatar: userData.Avatar
                    },

                    typingText: ' esta escribiendo...',
                    // the title for the user's list window '@this.Model.UserName' +
                    titleText: '  Zamba Chat',
                    emptyRoomText: "No hay usuarios online en este momento.",
                    adapter: new SignalRAdapter() // new LongPollingAdapter()
                });
            }
        });
        //  $("#loginModal").modal('show');     

        var link_was_clicked = false;
        document.addEventListener("click", function (e) {
            if (e.target.nodeName.toLowerCase() === 'a') {
                link_was_clicked = true;
            }
        }, true);

        window.onbeforeunload = function (e) {
            if (link_was_clicked) {
                return;
            }
            $.LogOut(thisUserId);
        }

    })(jQuery);

}