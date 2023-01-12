var buttonSaveClick = false;
window.onload = function () {
    document.getElementsByClassName("reModes")[0].style.display = 'none';

    //swal({
    //    title: "Archivo no disponible!",
    //    text: 'El archivo no esta disponible',
    //    icon: "warning",
    //    timer: 2000
    //});
};
window.addEventListener("beforeunload", function (e) {
    if (!buttonSaveClick) {
        e.preventDefault();
        e.returnValue = '';
    }
});
function SaveOK() {
    window.opener.location.reload();
    swal({
        title: "Guardar",
        text: 'El proceso de guardado finalizó exitosamente',
        icon: "success"
    }).then(function () {        
        buttonSaveClick = true;
        window.close();
    });
}

function SaveError() {
    swal({
        title: "Guardar",
        text: 'Ocurrio un error al intentar guardar',
        icon: "error"
    }).then(function () {        
        buttonSaveClick = false;
    });
}


function confirm(message) {
    return swal({
        text: message,
        buttons: true
    });
}
function Save() {
    buttonSaveClick = true;
    __doPostBack('ctl00$ContentPlaceHolder1$BtnExportToDOCXLink', '');

}