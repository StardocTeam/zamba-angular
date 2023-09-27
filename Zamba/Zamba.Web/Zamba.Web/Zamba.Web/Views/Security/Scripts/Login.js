




var domainName = getThisDomain();
var urlLocation = location.origin.trim();
document.getElementById("hdnthisdomian").setAttribute("value", domainName);
document.getElementById("hdnLocation").setAttribute("value", urlLocation);
var dominio = document.getElementById("hdnthisdomian").value;


try {

    $(".particles-js-canvas-el").css("position", "fixed");
} catch (e) {

}

var ZambaWebRestApiURL = location.origin.trim() + getValueFromWebConfig("RestApiUrl");// + "/api";
var URLLoginByGUID = ZambaWebRestApiURL + '/api/Account/LoginByGuid';

$(document).ready(function () {
    CheckIfAuthenticated();
});

function CheckIfAuthenticated() {
    
    if (localStorage != undefined)
        if (localStorage.authorizationData != undefined) {
            if (localStorage.authorizationData == "")
                return true;
            var data = JSON.parse(localStorage.authorizationData);
            var userid = data.UserId;
            var token = data.token;
            $.ajax({
                url: ZambaWebRestApiURL + '/api/Account/CheckToken?UserId=' + userid + '&Token=' + token,
                type: "GET",
                contentType: "application/json; charset=utf-8",
                async: false,
                success: function (response) {
                    if (response == true) {
                        var url = getUrlParameters().returnurl;
                        if (url == undefined)
                            url = "/" + location.pathname.split('/')[1] + "/globalsearch/search/search.html?"
                        if (url.substring(0, 1) == "/")
                            url = location.origin.trim() + url;
                        var splitUrl = url.split('?');
                        if (splitUrl.length == 2) {
                            var queryString = "";
                            var params = splitUrl[1].split('&');
                            // Debo recuperar el querystring sin informacion de userid y token
                            if (params[0] != "") {
                                params.forEach(function (param) {
                                    var key = param.split('=')[0];
                                    var value = param.split('=')[1];
                                    if (key != 'userid' && key != "token") {
                                        queryString += key + "=" + value + "&";
                                    }
                                }
                                );
                            }
                            queryString += "userid=" + userid + "&token=" + token.substring(0, 180);
                            url = splitUrl[0] + "?" + queryString
                            location.href = url;
                        }
                    }
                },
                error: function (error) {
                    ret = error
                }
            });
        }
}

function LoginByGuid(userid, guid, redirectUrl) {
    
    var Params = {
        userid: userid,
        guid: guid
    };
    var ret;
    $.ajax({
        url: URLLoginByGUID + "?userid=" + userid + "&guid=" + guid,
        type: "GET",
        //data: Params,
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (response) {
            if (JSON.parse(response).token == undefined || JSON.parse(response).token == null)
                ret = error;
            else {
                var tokenInfo = JSON.parse(response)
                if (localStorage) {
                    localStorage.setItem('authorizationData', JSON.stringify({
                        token: tokenInfo.token,
                        userName: tokenInfo.userName,
                        refreshToken: tokenInfo.refreshToken,
                        useRefreshTokens: tokenInfo.useRefreshTokens,
                        generateDate: new Date(),
                        UserId: tokenInfo.userid
                    }));
                }
            }
            window.location.href = redirectUrl + "userid=" + tokenInfo.userid + "&token=" + tokenInfo.token.substring(0, 180);
        },
        error: function (error) {
            ret = error
        }
    });
    return ret;
}
function getUrlParameters() {
    var pairs = window.location.search.substring(1).split(/[&?]/);
    var res = {}, i, pair;
    for (i = 0; i < pairs.length; i++) {
        pair = pairs[i].toLowerCase().split('=');
        if (pair[1])
            res[decodeURIComponent(pair[0])] = decodeURIComponent(pair[1]);
    }
    return res;
}
function getValueFromWebConfig(key) {


    var url = "Services/ViewsService.asmx/getValueFromWebConfig?key=" + key;
   if (location.pathname.toLowerCase().split("/").indexOf('login.aspx') > 1) {
        url = "../../Services/ViewsService.asmx/getValueFromWebConfig?key=" + key;
    }
    if (location.pathname.toLowerCase().split("/").indexOf('login') > 1) {
        url = "../../Services/ViewsService.asmx/getValueFromWebConfig?key=" + key;
    }

    $.ajax({
        "async": false,
        "crossDomain": true,
        "url": url,
        "method": "GET",
        "headers": {
            "cache-control": "no-cache"
        },
        "success": function (response) {
            if (response.childNodes[0].innerHTML == undefined) {
                pathName = response.childNodes[0].textContent;
            } else {
                pathName = response.childNodes[0].innerHTML;
            }

        },
        "error": function (data, status, headers, config) {
            console.log(data);
        }
    });
    return pathName;
}
