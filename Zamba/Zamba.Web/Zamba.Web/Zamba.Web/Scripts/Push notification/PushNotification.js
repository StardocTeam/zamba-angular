
function getUserIdExtended() {
    OneSignal.push(function () {
        OneSignal.on('subscriptionChange', function (isSubscribed) {
            if (isSubscribed) {
                OneSignal.getUserId(function (userId) {
                });
            }
        });
    });
}
function getUserID() {
    OneSignal.getUserId(function (userId) {
        var valoractual = $("#PushNotificationID").val();
        if (userId != valoractual) {
            SetPlayerID(GetUID(), userId);
        }
        
    });
}
function importarScript(nombre) {
    var s = document.createElement("script");
    s.src = nombre;
    document.querySelector("head").appendChild(s);
}
function registrarEnNotificaciones() {
    var OneSignal = window.OneSignal || [];
    OneSignal.push(function () {
        OneSignal.init({
            appId: push_notification_app_id
        });
    });
}

function SetPlayerID(userid,playerid) {
    var serviceBasePushNotification = ZambaWebRestApiURL + "/PushNotification/";
    var data = {
        player_id: playerid,
        user_id: userid
    };
    var ret;
    $.ajax({
        url: serviceBasePushNotification + "SetPlayerId",
        type: "POST",
        data: JSON.stringify(data),
        //dataType: 'json',
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (response) {
            ret = response;
        },
        error: function (error) {
            ret = error
        }
    });
    return ret;
}


window.onload = function () {
    if (push_notification_app_id == "")
        return;
    
    //importarScript("../../Scripts/Push Notification/OneSignalSKD.js");
    registrarEnNotificaciones();
    getUserID();
};