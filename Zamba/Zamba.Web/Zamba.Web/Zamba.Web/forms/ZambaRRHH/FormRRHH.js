
var domainDashboardRRHH = "";
var scheme = "";
function SendMessageToDashBoard(type, data, error) {
    var message = {
        type: type,
        data: { data },
        error: error
    }
    var messageJSON = JSON.stringify(message);
    parent.postMessage(messageJSON, scheme + "://" + domainDashboardRRHH);
}
function GetDomain() {
    var key = "RRHHApiServerName";
    domainDashboardRRHH = getValueFromWebConfig(key);
    scheme = getValueFromWebConfig("Scheme");
}
function InitFormGeneric() {
    GetDomain();
}
InitFormGeneric();