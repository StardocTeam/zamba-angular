


///////////COMMON UTILS//////////////////////////////////////////////////
setTimeout(photoValueConfig, 5000);

function photoValueConfig() {
    try {
        if ($(".clientlogo")[0] != undefined) {
            var photoValue = getValueFromWebConfig("WebClient");

            if (photoValue == "Provincia") {

                $(".clientlogo").css("background", "url(../../App_Themes/Provincia/Images/logo-header-blanco.png) no-repeat");
                $("#lnkWebIcon")[0].attributes[3].nodeValue = "../../App_Themes/Provincia/Images/WebIcon.png";
                $(".clientlogo").css("max-height", "40px !important");
            }
            if (photoValue == "Modoc") {

                $(".clientlogo").css("background", "url(../../App_Themes/Modoc/Images/Logo.jpg) no-repeat");
                $("#lnkWebIcon")[0].attributes[3].nodeValue = "../../App_Themes/Modoc/Images/WebIcon.jpg";
                $(".clientlogo").css("max-height", "40px !important");
            }
            if (photoValue == "Stardoc") {

                $(".clientlogo").css("background", "url(../../App_Themes/Stardoc/Images/Logo.jpg) no-repeat");
                $("#lnkWebIcon")[0].attributes[3].nodeValue = "../../App_Themes/Stardoc/Images/WebIcon.jpg";
                $(".clientlogo").css("max-height", "40px !important");
            }
            if (photoValue == "Marsh") {

                $(".clientlogo").css("background", "url(../../App_Themes/Marsh/Images/logoMarsh.jpg) no-repeat");
                $(".clientlogo").css("height", "24px");
                $(".clientlogo").css("margin-top", "11px");
                $(".clientlogo").css("margin-left", "1px!important");
                $(".clientlogo").css("width", "143px");
                $(".btn.btn-navbar").css("margin-top", "5px");
                $("#dropCabezera").css("margin-top", "5px");
                $(".img-circle.ng-show").css("margin-top", "2px");

                $("#lnkWebIcon")[0].href = "../../App_Themes/Marsh/Images/WebIcon.jpg";
            }
            if (photoValue == "Orazul") {

                $(".clientlogo").css("background", "url(../../App_Themes/Orazul/Images/logo2.png) no-repeat");
                $(".clientlogo").css("height", "24px");
                $(".clientlogo").css("margin-top", "7px");
                $(".clientlogo").css("margin-left", "1px!important");
                $(".btn.btn-navbar").css("margin-top", "5px");
                $("#dropCabezera").css("margin-top", "5px");
                $(".img-circle.ng-show").css("margin-top", "2px");

                $("#lnkWebIcon")[0].href = "../../App_Themes/Orazul/Images/WebIcon.jpg";
            }
            if (photoValue == "Boston") {
                try {

                    $(".clientlogo").css("background", "url(../../App_Themes/Boston/Images/LogoHorizontal.png) no-repeat");
                    $("#lnkWebIcon")[0].attributes[3].nodeValue = "../../App_Themes/Boston/Images/WebIcon.jpg";
                    $(".clientlogo").css("margin-top", "-3px");
                    $(".clientlogo").css("max-height", "46px !important");
                    $(".clientlogo").css("height", "46px !important");
                    $(".clientlogo").css("margin-left", "1px!important");
                    $(".clientlogo").css("width", "143px");
                    $(".btn.btn-navbar").css("margin-top", "5px");
                    $("#dropCabezera").css("margin-top", "5px");
                    $(".img-circle.ng-show").css("margin-top", "2px");
                } catch (e) {
                    console.error(e);
                }

            }
            if (photoValue == "Liberty") {

                $(".clientlogo").css("background", "url(../../App_Themes/Liberty/Images/Logo.png) no-repeat");
                $("#lnkWebIcon")[0].attributes[3].nodeValue = "../../App_Themes/Liberty/Images/WebIcon.ico";
                $(".clientlogo").css("max-height", "40px !important");
            }
			
			if (photoValue == "RPI") {

                $(".clientlogo").css("background", "url(../../App_Themes/RPI/Images/Logo.jpg) no-repeat");
                $("#lnkWebIcon")[0].attributes[3].nodeValue = "../../App_Themes/RPI/Images/WebIcon.ico";
                $(".clientlogo").css("max-height", "40px !important");
				$(".clientlogo").css("margin-top", "-3px");
                $(".clientlogo").css("max-height", "46px !important");
                $(".clientlogo").css("height", "46px !important");
                $(".clientlogo").css("margin-left", "1px!important");
                $(".clientlogo").css("width", "143px");
            } 

            var FechaVersion = getValueFromWebConfig("FechaVersion");

            if (FechaVersion != undefined && FechaVersion != null) {
                if ($("#FechaIdVersion")[0] != undefined)
                    $("#FechaIdVersion")[0].innerText = "Fecha del compilado: " + FechaVersion;
            }
        }
        ////////////////////////////////////////////////////////////////////////
    } catch (e) {
        console.error(e);
    }
}

var ThisDomain = window.location.protocol + "//" + window.location.host + "/" + window.location.pathname.split('/')[1];
function setSpinnerImage() {
    try {
        if (window.domainName == undefined) {
            $("#z-img-spinner").attr("src", thisDomain + "/Content/Images/z-simple-spinner-sample.gif");
        } else {
            $("#z-img-spinner").attr("src", window.location.origin + window.domainName + "/Content/Images/z-simple-spinner-sample.gif");
        }
    } catch (e) {
        console.error(e);
    }

}
function getPhoto() {
    try {
        var photoValue = getValueFromWebConfig("WebClient");
        var ThisDomain = window.location.protocol + "//" + window.location.host  + "/" + window.location.pathname.split('/')[1];



        if (photoValue == "Provincia") {
            if (window.domainName == undefined) {
                $("#clientlogo").attr("src", thisDomain + "/App_Themes/Provincia/Images/LoginMain.jpg");
            } else {
                $("#clientlogo").attr("src", window.location.origin + window.domainName + "/App_Themes/Provincia/Images/LoginMain.jpg");
            }
        }
        if (photoValue == "Modoc") {
            if (window.domainName == undefined) {
                $("#clientlogo").attr("src", thisDomain + "/App_Themes/Modoc/Images/Logo.jpg");
            } else { $("#clientlogo").attr("src", window.location.origin + window.domainName + "/App_Themes/Modoc/Images/Logo.jpg"); }
        }
        if (photoValue == "Stardoc") {
            if (window.domainName == undefined) {
                $("#clientlogo").attr("src", thisDomain + "/App_Themes/Stardoc/Images/Logo.jpg");
            } else {
                $("#clientlogo").attr("src", window.location.origin + window.domainName + "/App_Themes/Stardoc/Images/Logo.jpg");
            }
        }
        if (photoValue == "Marsh") {
            if (window.domainName == undefined) {
                $("#clientlogo").attr("src", thisDomain + "/App_Themes/Marsh/Images/logoMarsh.jpg");
            } else {
                $("#clientlogo").attr("src", window.location.origin + window.domainName + "/App_Themes/Marsh/Images/logoMarsh.jpg");
            }
        }
        if (photoValue == "Orazul") {
            if (window.domainName == undefined) {
                $("#clientlogo").attr("src", thisDomain + "/App_Themes/Orazul/Images/logo.png");
            } else {
                $("#clientlogo").attr("src", window.location.origin + window.domainName + "/App_Themes/Orazul/Images/logo.png");
            }
        }
        if (photoValue == "Boston") {
            if (window.domainName == undefined) {
                $("#clientlogo").attr("src", thisDomain + "/App_Themes/Boston/Images/LogoBoston2.png");
            } else {
                $("#clientlogo").attr("src", window.location.origin + window.domainName + "App_Themes/Boston/Images/Logo.jpg");
            }
        }
        if (photoValue == "Liberty") {
            if (window.domainName == undefined) {
                $("#clientlogo").attr("src", thisDomain + "/App_Themes/Liberty/Images/Logo.png");
            } else {
                $("#clientlogo").attr("src", window.location.origin + window.domainName + "App_Themes/Liberty/Images/Logo.png");
            }
        }
		
		if (photoValue == "RPI") {
            if (window.domainName == undefined) {
                $("#clientlogo").attr("src", thisDomain + "/App_Themes/RPI/Images/Logo.jpg");
            } else {
                $("#clientlogo").attr("src", window.location.origin + window.domainName + "App_Themes/RPI/Images/Logo.jpg");
            }
        }

        if (photoValue == "ColegioEscribano") {
            if (window.domainName == undefined) {
                $("#clientlogo").attr("src", thisDomain + "/App_Themes/ColegioEscribano/Images/Logo.png");
            } else {
                $("#clientlogo").attr("src", window.location.origin + window.domainName + "App_Themes/ColegioEscribano/Images/Logo.jpg");
            }
        }         

    } catch (e) {
        console.error(e);
    }
}

function getValueFromWebConfig(key) {
    var pathName = null;

    var baseUrl = window.location.protocol + "//" + window.location.host  + "/" + window.location.pathname.split('/')[1];

    $.ajax({
        "async": false,
        "crossDomain": true,
        "url": baseUrl + "/Services/ViewsService.asmx/getValueFromWebConfig?key=" + key,
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