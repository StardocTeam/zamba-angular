//function Debbug() { }

//Debbug.prototype = {
var Debbug = {
    ReplaceGlobalURLS: function () {
        //var DG = Debbug.GetURLS();
        //thisUserIdChat = thisUserId = DG.thisUserIdChat;
        //URLServer = DG.URLServer;
        //thisDomain = DG.thisDomain;
        //ZCollLnk = zCollLnk = DG.ZCollLnk;
        //zIsCollserver = isCS = DG.zIsCollserver;
        //zCollServer = colServer = DG.zCollServer;
    },
    _Urls: function () {
        return [
            ["thisUserIdChat", 0],
            ["URLServer", URLServer],
            ["thisDomain", thisDomain],
            ["zCollLnk", typeof (zCollLnk) !== "undefined" ? zCollLnk : ZColl],
            ["isCS", false],
            ["colServer", zCollServer],
        ]
    },
    Init: function () {
        (function SetDefaults() {
            localStorage.isdebbug = true;
            if (localStorage.zchaturls === undefined) Debbug.SetZUrls();
            Debbug.ReplaceGlobalURLS();
        })();
    },
    Clear: function () {
        localStorage.clear();
        //localStorage.removeItem("isdebbug");
        //localStorage.removeItem("zchaturls");
        //localStorage.removeItem("lastzurls");
    },
    SetZUrls: function () {
        Debbug._Urls().forEach(x => RequestUrl(x));
        function RequestUrl(array) {
            var title = array[0];
            var value = array[1];
            bootbox.prompt({
                title: title,
                value: value || "",
                callback: function (r) {
                    SetNewValues(title, r);
                    Debbug.ReplaceGlobalURLS();
                }
            });
        }
        function SetNewValues(type, value) {
            var ls = Debbug.GetURLS();
            ls[type] = value;
            localStorage.zchaturls = JSON.stringify(ls);
        }
    },

    GetURLS: function () {
        return JSON.parse(localStorage.zchaturls || "{}");
    }
};

//$(document).ready(function () {

function DebbugFN() {
    if (localStorage.isdebbug === "true") { Debbug.Init(); return true; } else return false;
};

//});
