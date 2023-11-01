"use strict";

var logout = getUrlParameters().logout == "true";
var ZambaWebRestApiURL;
var IsMultipleSesion
var serviceBaseAccount;
var oktaInformation;
var mensajeLegal = "    Este sistema es para ser utilizado solamente por usuarios autorizados. Toda la información contenida en los sistemas es propiedad de la Empresa y pueden ser supervisados, cifrados, leídos, copiados o capturados y dados a conocer de alguna manera, solamente por personas autorizadas.             El uso del sistema por cualquier persona, constituye de su parte un expreso consentimiento al monitoreo, intervención, grabación, lectura, copia o captura y revelación de tal intervención.            El usuario debe saber que en la utilización del sistema no tendrá privacidad frente a los derechos de la empresa responsable del sistema.            El uso indebido o no autorizado de este sistema genera  responsabilidad para el infractor, quién por ello estará sujeto al resultado de las acciones civiles y penales que la Empresa considere pertinente realizar en defensa de sus derechos y resguardo de la privacidad del sistema.            Si Usted no presta conformidad con las reglas precedentes y no está de acuerdo con ellas, desconéctese ahora.";

$(document).ready(function (n) {
    $("#ZambaAuthentication").hide();
    GetIsMultipleSesion();
    if (location.search == "" || logout) {
        $("#ingresar").show();
        return;
    }
    else
        IniciarOKTA();
});
function IniciarOKTA() {
    
    //$("#mensajeLegal").text(mensajeLegal);
    $("#ingresar").hide();
    ObtenerConfiguracionOKTA();
    Autenticar();
    

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

    $.ajax({
        "async": false,
        "crossDomain": true,
        "url": baseUrl + "/Services/ViewsService.asmx/LoginOkta?userid=" + userid + "&token=" + token,
        "method": "GET",
        "headers": {
            "cache-control": "no-cache"
        },
        "success": function (response) {

        }
        ,
        "error": function (data, status, headers, config) {

        }
    });
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
            if (zssToken == undefined || zssToken == null) Autenticar(); else {
                var tokenInfo = JSON.parse(response);

                if (localStorage) {
                    localStorage.setItem('OKTA', JSON.stringify({
                        id_token: id_token
                    }));

                    localStorage.setItem('authorizationData', JSON.stringify({
                        token: tokenInfo.tokenInfo.token,
                        userName: tokenInfo.tokenInfo.userName,
                        refreshToken: tokenInfo.tokenInfo.refreshToken,
                        useRefreshTokens: tokenInfo.tokenInfo.useRefreshTokens,
                        generateDate: new Date(),
                        UserId: tokenInfo.tokenInfo.userid,
                        oktaAccessToken: tokenInfo.tokenInfo.oktaAccessToken,
                        oktaIdToken: tokenInfo.tokenInfo.oktaIdToken,
                        oktaRedirectLogout: tokenInfo.tokenInfo.oktaRedirectLogout
                    }));

                    UrlRedirect = tokenInfo.UrlRedirect;
                }
            }

            LoginWeb(tokenInfo.tokenInfo.userid, tokenInfo.tokenInfo.token);
            MostrarEstado("Ingresando a Zamba...");

            if (returnUrl == "null" || returnUrl == "" || returnUrl == null) {
                /*window.location.href = location.origin.trim() + UrlRedirect + "userid=" + tokenInfo.tokenInfo.userid + "&token=" + tokenInfo.tokenInfo.token.substring(0, 180);*/
                window.location.href = location.origin.trim() + UrlRedirect + "userid=" + tokenInfo.tokenInfo.userid + "&token=" + tokenInfo.tokenInfo.token;
            } else {
                localStorage.removeItem("returnUrl");
                /*window.location.href = location.origin.trim() + returnUrl + "&userid=" + tokenInfo.tokenInfo.userid + "&token=" + tokenInfo.tokenInfo.token.substring(0, 180);*/
                window.location.href = location.origin.trim() + returnUrl + "&userid=" + tokenInfo.tokenInfo.userid + "&token=" + tokenInfo.tokenInfo.token;
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
                $("#ZambaAuthentication").css('display','flex');                
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

    var state;
    $.ajax({
        "async": false,
        "crossDomain": true,
        "url": serviceBaseAccount + "generateOKTAStateValue",
        "method": "POST",
        "headers": {
            "cache-control": "no-cache"
        },
        "success": function (response) {
            state = response;
        }
        ,
        "error": function (data, status, headers, config) {
            MostrarEstado("Se produjo un error al autenticar (" + error_description.replace("+", " ") + ")");
            $("#ingresar").show();
            return "";
        }
    })
    return state;
}