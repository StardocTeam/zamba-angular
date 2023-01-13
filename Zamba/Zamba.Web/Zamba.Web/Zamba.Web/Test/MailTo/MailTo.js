var dominio_MailTo = "https://" + window.location.host +"/ZambaWeb.RestApi/api";

function TestMailTo(e) {
    var a = document.getElementById("MailTo");
    var genericRequest = {
        Params: {
            "ruleId": e.target.attributes.ruleid.value
        }
    };

    $.ajax({
        type: "POST",
        url: dominio_MailTo + "/tasks/ExecuteTaskRule",
        data: JSON.stringify(genericRequest),
        contentType: "application/json; charset=utf-8",        
        success: function (data) {
            RDO = JSON.parse(data);
        },
        error: function (data) {
            RDO = JSON.parse(data);
        }
    });
}


function OpenMailBox(tagA, settings) {
    tagA.setAttribute("href", settings);
    tagA.click();

    //Medida de seguridad para no mostrar el link que usara href de este elemento.
    //recordar no armar settings en HTML para conservar seguridad
    tagA.setAttribute("href", "");
}