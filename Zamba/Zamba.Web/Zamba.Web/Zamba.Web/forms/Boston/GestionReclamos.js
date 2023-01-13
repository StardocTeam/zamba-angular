function SetAsocId(sender) {
    document.getElementById("hdnAsocId").name = sender.id;
    frmMain.submit();
}

function SetRuleId(sender) {
    document.getElementById("hdnRuleId").name = sender.id;
    frmMain.submit();
}

function zamba_save_onclick() {
    alert("Se guardó exitosamente.");
    document.getElementById("hdnAsocId").name = sender.id;
    frmMain.submit();
}

function action(id) {
    document.getElementById(id).style.display = "block";
    var p = parseInt(id);
    for (i = 1; i <= 7; i++) {
        if (i != p) {
            document.getElementById(i.toString()).style.display = "none";
        }
    }
}

function FormLoad() {
    try {

        $("#zamba_index_14_dup").val($("#zamba_index_14").val());
        $("#zamba_index_40_dup").val($("#zamba_index_40").val());
        $("#zamba_index_14_dup").val($("#zamba_index_14").val());

        var zamba_index_125 = $($("#zamba_index_125")).val()
        zamba_index_125 = zamba_index_125.substr(0, 8);
        $($("#zamba_index_125")).val(zamba_index_125);
        $("#zamba_index_2765_dup").val($("#zamba_index_2765").val());
        $("#zamba_index_3_dup").val($("#zamba_index_3").val());

        if ($('#zamba_index_32').val() != 4) {
            $('.tblOtrosRamos').css('display', 'block');
            $('.tblAutos').css('display', 'none');
        } else {
            $('.tblOtrosRamos').css('display', 'none');
            $('.tblAutos').css('display', 'block');
        }
    } catch (e) {
        console.error(e);
    }
}

$(document).ready(function () {
    setTimeout(function () { FormLoad(); }, 200);
});