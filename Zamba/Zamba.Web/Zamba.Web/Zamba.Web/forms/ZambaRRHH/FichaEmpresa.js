
function InitFormEmpresaInsertar() {
    setValidationCuit();
}


function setValidationCuit() {
    document.getElementById('zamba_index_109').addEventListener('blur', function () {
        debugger;
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
