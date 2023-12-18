"use strict";

var logout = getUrlParameters().logout == "true";
var initSession = getUrlParameters().initSession == "true";
var ZambaWebRestApiURL;
var IsMultipleSesion
var serviceBaseAccount;
var oktaInformation;
var mensajeLegal = "    Este sistema es para ser utilizado solamente por usuarios autorizados. Toda la información contenida en los sistemas es propiedad de la Empresa y pueden ser supervisados, cifrados, leídos, copiados o capturados y dados a conocer de alguna manera, solamente por personas autorizadas.             El uso del sistema por cualquier persona, constituye de su parte un expreso consentimiento al monitoreo, intervención, grabación, lectura, copia o captura y revelación de tal intervención.            El usuario debe saber que en la utilización del sistema no tendrá privacidad frente a los derechos de la empresa responsable del sistema.            El uso indebido o no autorizado de este sistema genera  responsabilidad para el infractor, quién por ello estará sujeto al resultado de las acciones civiles y penales que la Empresa considere pertinente realizar en defensa de sus derechos y resguardo de la privacidad del sistema.            Si Usted no presta conformidad con las reglas precedentes y no está de acuerdo con ellas, desconéctese ahora.";

$(document).ready(function (n) {
    $("#ZambaAuthentication").hide();
    GetIsMultipleSesion();
    if (getUrlParameters().showModal == "true") {
        IniciarOKTA();
    }
    if (logout || !initSession) {
        if (getUrlParameters().code == "" || getUrlParameters().code == undefined) {
            $("#ingresar").show();
            return;
        }
        else {
            IniciarOKTA();
            return;
        }
        if (getUrlParameters().code != "") {
            IniciarOKTA();
        }
    }
});
function IniciarOKTA() {

    //$("#mensajeLegal").text(mensajeLegal);
    $("#ingresar").hide();
    ObtenerConfiguracionOKTA();
    Autenticar();


}
function closeModal(reLoadLogin) {
    if (reLoadLogin) {
        parent.location.reload();
    }
    else {
        parent.$("#modalLogin").modal("hide");
    }
}

function Logout() {

    var authClient = new OktaAuth({
        url: oktaInformation.baseUrl,
        clientId: oktaInformation.clientId,
        redirectUri: oktaInformation.redirectURL
    });
    authClient.signOut().then(function (obj) {
        authClient.token.getWithRedirect({
            responseType: ['code'],
            state: stateValue
        });
    });
    ;
}

function ObtenerConfiguracionOKTA() {

    ZambaWebRestApiURL = location.origin.trim() + getValueFromWebConfig("RestApiUrl") + "/api";
    serviceBaseAccount = ZambaWebRestApiURL + "/Account/";
    $.ajax({
        url: serviceBaseAccount + "GetOktaInformation",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function success(response) {
            oktaInformation = JSON.parse(response);
        },
        error: function error(_error) {
            ret = _error;
        }
    });


}
function MostrarEstado(texto) {
    $("#estado").text(texto);
}
function Autenticar() {
    var returnUrl = getUrlParameters().returnurl;

    var authClient = new OktaAuth({
        url: oktaInformation.baseUrl,
        clientId: oktaInformation.clientId,
        redirectUri: oktaInformation.redirectURL
    });
    if (getUrlParameters().error_description != "" && getUrlParameters().error_description != null) {
        var error_description = getUrlParameters().error_description;
        MostrarEstado("Se produjo un error al autenticar (" + error_description.replace("+", " ") + ")");
        $("#ingresar").show();
        return;
    }

    if (location.hash && (returnUrl == "" || returnUrl == null)) {
        authClient.token.parseFromUrl().then(function (idToken) {
            if (idToken) {
                ValidarToken('', '', idToken[0].authorizationCode);
            }
        });
    } else {
        if (getUrlParameters().code == "" || getUrlParameters().code == null) {
            if (!(returnUrl == "" || returnUrl == null)) {
                localStorage.setItem("returnUrl", returnUrl);
            }

            //MostrarEstado(mensajeLegal);
            MostrarEstado("Abriendo OKTA para autenticar...")
            returnUrl = localStorage.getItem("returnUrl")
            localStorage.clear();
            sessionStorage.clear();
            if (returnUrl != "null")
                localStorage.setItem("returnUrl", returnUrl);
            var stateValue;
            stateValue = GenerarOktaStateValue();
            if (stateValue == "")
                return;
            authClient.token.getWithRedirect({
                responseType: ['code'],
                state: stateValue
            });
        } else {
            ValidarToken('', '', getUrlParameters().code);
        }
    }
}
function LoginWeb(userid, token) {
    var getUrl = window.location;
    var baseUrl = getUrl.protocol + "//" + getUrl.host + "/" + getUrl.pathname.split('/')[1];
    var ret = 0;
    $.ajax({
        "async": false,
        "crossDomain": true,
        "url": baseUrl + "/Services/ViewsService.asmx/LoginOkta?userid=" + userid + "&token=" + token,
        "method": "GET",
        "headers": {
            "cache-control": "no-cache"
        },
        "success": function (response) {
            var connId = response.getElementsByTagName("long")[0].innerHTML;
            ret = connId;
        }
        ,
        "error": function (data, status, headers, config) {
            ret = 0;
        }
    });
    return ret;
}
function ValidarToken(access_token, id_token, code) {
    MostrarEstado("Autenticando...")
    var returnUrl = localStorage.returnUrl;
    var UrlRedirect;
    $.ajax({
        url: serviceBaseAccount + "LoginOkta?&access_token=" + access_token + "&id_token=" + id_token + "&code=" + code,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function success(response) {

            var zssToken = JSON.parse(response).tokenInfo.token;
            var tokenInfo = JSON.parse(response).tokenInfo;
            var connId = LoginWeb(tokenInfo.UserId, tokenInfo.token);
            if (connId == 0) {
                MostrarEstado("Máximo de licencias conectadas, contáctese con su soporte técnico para adquirir nuevas licencias....");
                return;
            }
            if (zssToken == undefined || zssToken == null) Autenticar(); else {
                var tokenInfo = JSON.parse(response);

                if (localStorage) {
                    localStorage.setItem('OKTA', JSON.stringify({
                        id_token: id_token
                    }));
                    localStorage.removeItem('authMethod');
                    localStorage.setItem('authMethod', 'OKTA');
                    localStorage.setItem('authorizationData', JSON.stringify({
                        token: tokenInfo.tokenInfo.token,
                        userName: tokenInfo.tokenInfo.userName,
                        tokenExpire: tokenInfo.tokenInfo.tokenExpire,
                        refreshToken: tokenInfo.tokenInfo.refreshToken,
                        useRefreshTokens: tokenInfo.tokenInfo.useRefreshTokens,
                        generateDate: new Date(),
                        UserId: tokenInfo.tokenInfo.UserId,
                        oktaAccessToken: tokenInfo.tokenInfo.oktaAccessToken,
                        oktaIdToken: tokenInfo.tokenInfo.oktaIdToken,
                        oktaRedirectLogout: tokenInfo.tokenInfo.oktaRedirectLogout,
                        connectionId: connId
                    }));

                    UrlRedirect = tokenInfo.UrlRedirect;
                    localStorage.setItem('connId', connId);
                    localStorage.setItem('userId', tokenInfo.tokenInfo.userName);
                }
            }
            MostrarEstado("Ingresando a Zamba...");
            var Modal = false;
            var reLoadLogin = false;



            var state = getUrlParameters().state;
            var splitState = state.split('_');
            splitState.forEach(function (o) {
                if (o == "Modal") {
                    Modal = true;
                }
                if (o == "reLoadLogin") {
                    reLoadLogin = true;
                }
            });

            if (reLoadLogin || Modal) {
                closeModal(reLoadLogin);
            }
            else if (returnUrl == "null" || returnUrl == "" || returnUrl == null) {
                window.location.href = location.origin.trim() + UrlRedirect + "userid=" + tokenInfo.tokenInfo.UserId + "&token=" + tokenInfo.tokenInfo.token;
            } else {
                localStorage.removeItem("returnUrl");
                window.location.href = location.origin.trim() + returnUrl + "&userid=" + tokenInfo.tokenInfo.UserId + "&token=" + tokenInfo.tokenInfo.token;
            }
        },
        error: function error(_error2) {
            MostrarEstado("Ocurrio un problema al autenticar, su usuario no esta dado de alta en Zamba o esta bloqueado, contactese con los administradores.")
            $("#ingresar").show();
        }
    });
}
function GetIsMultipleSesion() {
    ZambaWebRestApiURL = location.origin.trim() + getValueFromWebConfig("RestApiUrl") + "/api";
    serviceBaseAccount = ZambaWebRestApiURL + "/Account/";
    $.ajax({
        "async": false,
        "crossDomain": true,
        "url": serviceBaseAccount + "GetIsMultipleSesion",
        "method": "GET",
        "success": function (response) {
            IsMultipleSesion = response;
            if (IsMultipleSesion) {
                $("#ZambaAuthentication").css('display', 'flex');
            }
            else
                $("#ZambaAuthentication").css('display', 'none');
        }
        ,
        "error": function (data, status, headers, config) {

        }
    })
}
function GenerarOktaStateValue() {
    var modal = false;
    var reLoadLogin = false;
    var state;
    if (getUrlParameters().showModal == "true") {
        modal = true;
    }
    if (getUrlParameters().reloading == "true") {
        reLoadLogin = true;
    }
    var genericRequest = {
        UserId: 0,
        Params:
        {
            "Modal": modal,
            "reLoadLogin": reLoadLogin
        }
    };
    $.ajax({
        type: "POST",
        url: serviceBaseAccount + "generateOKTAStateValue",
        contentType: 'application/json',
        async: false,
        data: JSON.stringify(genericRequest),
        success: function (response) {
            state = response;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            result = XMLHttpRequest;
        }
    });
    return state;
}