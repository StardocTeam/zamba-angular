FormLoad();
function FormLoad() {
    var formActive = document.getElementById("FormName").value;
    switch (formActive) {
        case 'FichaEmpresaInsert':
            document.addEventListener("DOMContentLoaded", function () { InitFormEmpresaInsertar(); });
            break;
        case 'FichaEmpresaEdit':
            document.addEventListener("DOMContentLoaded", function () { InitFormEmpresaEditar(); });            
            break;
    }
}

function InitFormEmpresaInsertar() {
    setValidationCuit();
}
function InitFormEmpresaEditar() {
    setValidationCuit();
}
function setValidationCuit() {
    document.getElementById('zamba_index_109').addEventListener('blur', function () {
        ValidateCuit();
    });
}
function ValidateCuit() {
    // Eliminar cualquier carácter que no sea un número
    document.getElementById('zamba_index_109').value =
        document.getElementById('zamba_index_109').value
            .replace(/[^0-9]/g, '')


    var cuit = document.getElementById('zamba_index_109').value;

    // Verificar la longitud del CUIT
    if (cuit.length !== 11) {
        ShowInvalidCuit(false)
        return false;
    }

    // Array con los coeficientes
    const coeficientes = [5, 4, 3, 2, 7, 6, 5, 4, 3, 2];

    // Calcular la suma de los productos de los dígitos y los coeficientes
    let suma = 0;
    for (let i = 0; i < 10; i++) {
        suma += parseInt(cuit[i]) * coeficientes[i];
    }

    // Calcular el dígito verificador
    const verificador = 11 - (suma % 11);

    // El resultado puede ser 11 (se reemplaza por 0) o 10 (se reemplaza por 9)
    const digitoVerificador = verificador === 11 ? 0 : verificador === 10 ? 9 : verificador;

    // Comparar con el último dígito del CUIT
    var CuitIsValid = digitoVerificador === parseInt(cuit[10]);
    ShowInvalidCuit(CuitIsValid);
    return CuitIsValid;
}
function ShowInvalidCuit(value) {
    if (value) {
        document.getElementById('cuit_invalido').style.display = "none";
    }
    else {
        document.getElementById('cuit_invalido').style.display = "block";
    }
}
function SaveDataCompanyFormInsert(obj) {
   
    if (!ValidateFieldsRequiredFormInsert()) {
        return false;
    }
    else {
        SetRuleId(obj);
        return true;
    }
    return true;
}
function SaveDataCompanyFormEdit(obj) {

    if (!ValidateFieldsRequiredFormEdit()) {
        return false;
    }
    else {
        SetRuleId(obj);
        return true;
    }
    return true;
}

function ValidateFieldsRequiredFormInsert() {
    var errorText = "Se produjeron los siguientes errores: \r\n";
    var isValid = true;

    if (!ValidateFieldRequired("172")) {
        // NOMBRE O RAZON SOCIAL
        isValid = false;
        errorText += "* Falta cargar el campo 'Nombre' \r\n";
    }
    if (!ValidateFieldRequired("109")) {
        // CUIT
        isValid = false;
        errorText += "* Falta cargar el campo 'CUIT' \r\n";
    }
    else {
    } if (!ValidateCuit()) {
        isValid = false;
        errorText += "* El campo 'CUIT' es inválido  \r\n";
    }
    if (!ValidateFieldRequired("204")) {
        // ACTIVIDAD DE LA EMPRESA
        isValid = false;
        errorText += "* Falta cargar el campo 'Actividad de la empresa' \r\n";
    }

    if (document.getElementById("zamba_index_250").value.length < "144") {
        // DESCRIPCION DE LA EMPRESA
        isValid = false;
        errorText += "* El campo 'Descripcion de la empresa' debe tener un mínimo de 144 caracteres \r\n";
    }
    if (!ValidateFieldRequired("177")) {
        // PERSONA DE CONTACTO
        isValid = false;
        errorText += "* Falta cargar el campo 'Persona de contacto' \r\n";
    }
    if (!ValidateFieldRequired("257")) {
        // TELEFONO CELULAR
        isValid = false;
        errorText += "* Falta cargar el campo 'Telefono celular' \r\n";
    }
    if (!ValidateFieldRequired("237")) {
        // EMAIL
        isValid = false;
        errorText += "*  cargar el campo 'Email' \r\n";
    }
    if (!ValidateFieldRequired("152")) {
        // AREA O SECTOR
        isValid = false;
        errorText += "* Falta cargar el campo 'Area o sector' \r\n";
    }
    if (!ValidateFieldRequired("153")) {
        // CARGO
        isValid = false;
        errorText += "* Falta cargar el campo 'Cargo' \r\n";
    }

    if (!ValidateFieldRequired("173")) {
        // DIRECCION FISCAL
        isValid = false;
        errorText += "* Falta cargar el campo 'Direccion fiscal' \r\n";
    }

    if (!ValidateFieldRequired("217")) {
        // FORMA DE PAGO
        isValid = false;
        errorText += "* Falta cargar el campo 'Forma de pago' \r\n";
    }


    if (document.getElementById("zamba_index_216").value = "9999") {
        // MONEDA
        isValid = false;
        errorText += "* Falta cargar el campo 'Moneda' \r\n";
    }

    if (!ValidateFieldRequired("178")) {
        // CONDICION ANTE EL IVA
        isValid = false;
        errorText += "* Falta cargar el campo 'Condicion ante el IVA' \r\n";
    }
    if (!isValid) {
        var msgError = document.getElementById("mensajes_error")
        msgError.style.display = "block";
        msgError.innerText = errorText;
    }
    return isValid;
}

function ValidateFieldsRequiredFormEdit() {
    var errorText = "Se produjeron los siguientes errores: \r\n";
    var isValid = true;

    if (!ValidateFieldRequired("177")) {
        // PERSONA DE CONTACTO
        isValid = false;
        errorText += "* Falta cargar el campo 'Persona de contacto' \r\n";
    }
    if (!ValidateFieldRequired("257")) {
        // TELEFONO CELULAR
        isValid = false;
        errorText += "* Falta cargar el campo 'Telefono celular' \r\n";
    }
    if (!ValidateFieldRequired("237")) {
        // EMAIL
        isValid = false;
        errorText += "*  cargar el campo 'Email' \r\n";
    }
    if (!ValidateFieldRequired("152")) {
        // AREA O SECTOR
        isValid = false;
        errorText += "* Falta cargar el campo 'Area o sector' \r\n";
    }
    if (!ValidateFieldRequired("153")) {
        // CARGO
        isValid = false;
        errorText += "* Falta cargar el campo 'Cargo' \r\n";
    }

    if (!ValidateFieldRequired("173")) {
        // DIRECCION FISCAL
        isValid = false;
        errorText += "* Falta cargar el campo 'Direccion fiscal' \r\n";
    }

    if (!ValidateFieldRequired("217")) {
        // FORMA DE PAGO
        isValid = false;
        errorText += "* Falta cargar el campo 'Forma de pago' \r\n";
    }


    if (document.getElementById("zamba_index_216").value = "9999") {
        // MONEDA
        isValid = false;
        errorText += "* Falta cargar el campo 'Moneda' \r\n";
    }

    if (!ValidateFieldRequired("178")) {
        // CONDICION ANTE EL IVA
        isValid = false;
        errorText += "* Falta cargar el campo 'Condicion ante el IVA' \r\n";
    }
    if (!isValid) {
        var msgError = document.getElementById("mensajes_error")
        msgError.style.display = "block";
        msgError.innerText = errorText;
    }
    return isValid;
}

function ValidateFieldRequired(zamba_index) {
    var element = document.getElementById("zamba_index_" + zamba_index);
    switch (element.type) {
        case 'text':
            return element.value != "";
            break;
        case 'select-one':
            return element.value != "";
            break;

    }
}
