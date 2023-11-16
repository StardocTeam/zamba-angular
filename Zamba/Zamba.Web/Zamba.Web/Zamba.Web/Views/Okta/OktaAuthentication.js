$(document).ready(function () {
    document.querySelector("#ingresar").addEventListener("click", function () { IniciarOKTA(); }, false);

});

var returnUrl = getUrlParameters().returnurl;
if (!(returnUrl == "" || returnUrl == null)) {
    localStorage.setItem("returnUrl", returnUrl);
}
$("#lnkWebIcon")[0].href = "../../App_Themes/Marsh/Images/WebIcon.jpg";