var dominio = "https://" + window.location.host + "/ZambaWeb.RestApi/api";

function Login() {
    var MiUrl = dominio + "/eInbox/getinbox";

    var genericRequest = {
        user: $("#TboxUser").val(),
        pass: $("#TboxPass").val(),
    }

    $.ajax({
        type: "POST",
        url: MiUrl,
        data: JSON.stringify(genericRequest),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            Response(data);
        },
        error: function (data) {
            Response(data);
        }
    });
}

function GetMails_FromRule() {
    var MiUrl = dominio + "/eInbox/GetEmails";


    var genericRequest = {
        UserId: "1169",
        RuleId: "11545209",
        Params: {
            "Host": $("#TboxIp").val(),
            "Port": $("#TboxPort").val(),
            "UserName": $("#TboxUser").val(),
            "UserPass": $("#TboxPass").val(),
            "ProtCon": $("#TBoxProtCon").val(),
            "Folder": $("#TboxFolder").val(),
            "Filter_Field": $("#TboxFilterField").val(),
            "Filter_Value": $("#TboxFilterValue").val(),
            "Cert": $("#CheckBox_Cert")[0].checked,
            "UnreadEmails": $("#CheckBox_UnreadEmails")[0].checked,
            "NewEmails": $("#CheckBox_NewEmails")[0].checked,
            "RecentEmails": $("#CheckBox_RecentEmails")[0].checked,
            "TrashMails": $("#CheckBox_TrashMails")[0].checked,
            "Todos": $("input[id=Todos]")[0].checked,
            "Filtrado": $("input[id=Filtrado]")[0].checked,
            "Regla": $("#TboxRegla").val(),
            "ruleId": $("#TboxRegla").val(),
            "resultIds": "0",
            //Hardcode
            "userId": "1169"
        }
    }

    $.ajax({
        type: "POST",
        url: MiUrl,
        data: JSON.stringify(genericRequest),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            Response(data);
        },
        error: function (data) {
            Response(data);
        }
    });
}

function _DefaultPS() {
    $("#TboxRegla").val("11545209"),
    $("#TboxIp").val("10.6.110.18");
    $("#TboxPort").val("143");
    $("#TboxUser").val("bpmt@pseguros.com");
    $("#TboxPass").val("Agosto2018");
    $("#TBoxProtCon").val("Ssl");
    $("#TboxFolder").val("Inbox");
    $("#TboxFilterField").val("Subject");
    $("#TboxFilterValue").val("15814");
    $("#CheckBox_Cert")[0].checked = false;
    $("input[id=Filtrado]")[0].checked = true;
    HideFilter();
}

function Response(value) {
    $("#response").val(value);
    console.log(value);
}

function HideFilter() {
    if ($("input[id=Filtrado]")[0].checked == false) {
        $("#TboxFilterField").attr('disabled', 'disabled');
        $("#TboxFilterValue").attr('disabled', 'disabled');
    } else {
        $("#TboxFilterField").removeAttr('disabled', 'disabled');
        $("#TboxFilterValue").removeAttr('disabled', 'disabled');
    }
}

function help() {

    var From = "From: Casilla de correos de origen\n";
    var To = "To: Casillas de correos destino.\n";
    var Cc = "Cc: Lista de casillas de correo que recibiran los mensajes.\n";
    var ReplyTo = "ReplyTo: Lista de casillas de correo que deberían recibir respuestas de los mensajes.\n";
    var Sender = "Sender: Remitente.\n";
    var Date = "Date: Decha del correo enviado.\n";
    var Subject = "Subject: Asunto.\n";
    var IsRead = "IsRead: Si ya se obtuvo o leyo.\n";
    var IsRecent = "IsRecent: Si es reciente.\n";
    var IsFlagged = "IsFlagged: Si esta Marcado.\n";
    var IsAnswered = "IsAnswered: Si fue Respondido.\n";
    var IsDeleted = "IsDeleted: Si fue eliminado.\n";
    var IsDraft = "IsDraft: Si es un borrador.\n";
    var SequenceNumber = "SequenceNumber: número del mensaje en el buzón.\n";
    var Size = "Size: Tamaño en bytes del mensaje.\n";
    var UniqueId = "UniqueId: Identificador unico del mensaje.\n";
    var Attachments_Count = "Attachments_Count: Numero de adjuntos.\n";
    var Attachments = "Attachments: Adjuntos fisicos.\n";
    var Message = "Message: Mensaje completo con todos sus atributos.";

    swal("AYUDA: Claves-Valores", From + To + Cc + ReplyTo + Sender + Date + Subject + IsRead + IsRecent + IsFlagged + IsAnswered + IsDeleted + IsDraft + SequenceNumber + Size + UniqueId + Attachments_Count + Attachments + Message, "warning");
}


