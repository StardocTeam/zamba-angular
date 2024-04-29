
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
    window.addEventListener('message', event => {
        var url = new URL(environment['zambaWeb']);
        var origin = scheme + "://" + domainDashboardRRHH;
        if (event.origin != origin) {
            return;
        }
        var message = JSON.parse(event.data);
        this.handleMessage(message);
    });

}

function handleMessage(message) {
    try {
        switch (message.type) {
            case 'request vacation':
                console.log(message.data);
                break;
        }
    } catch (e) {
        console.error("Error al procesar el mensaje: ", e);
    }
}
InitFormGeneric();












