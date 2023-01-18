
var Debbug = {
    ReplaceGlobalURLS: function () {
        //thisDomain = document.config.urlBase = Debbug.GetURLS().thisDomain;
        //ZambaWebRestApiURL = Debbug.GetURLS().ZambaWebRestApiURL;
        //urlGlobalSearch = ZambaWebRestApiURL + "/Search/";
    },
    _Urls: function () {
        return [
            ["thisDomain", thisDomain],
            ["ZambaWebRestApiURL", ZambaWebRestApiURL],
            ["urlGlobalSearch" ,ZambaWebRestApiURL + "/Search/"]
        ];
    },
    Init: function () {
        (function SetDefaults() {
            localStorage.isdebbug = true;
            if (localStorage.zurls === undefined) Debbug.SetZUrls();
           // if (localStorage.lastzurls === undefined) localStorage.lastzurls = localStorage.zurls;//!= localStorage.zurls
            Debbug.ReplaceGlobalURLS();
        })();     
    },
    Clear: function () {
        localStorage.removeItem("isdebbug");
        localStorage.removeItem("zurls");
        localStorage.removeItem("lastzurls");
    },
    SetZUrls: function () {
        //Debbug._Urls().forEach(x => RequestUrl(x)); se comenta ya que no es compatible con IE11
        for (var i = 0; i < Debbug._Urls().length; i++) {
            RequestUrl(Debbug._Urls()[i]);
        }

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
            localStorage.zurls = JSON.stringify(ls);
        }
    },

    GetURLS: function () {
        return JSON.parse(localStorage.zurls || "{}");
    }
};

$(document).ready(function () {
    (function ($) {
        if (localStorage.isdebbug === "true") {        
            Debbug.Init();
        }
    })(jQuery);
});
