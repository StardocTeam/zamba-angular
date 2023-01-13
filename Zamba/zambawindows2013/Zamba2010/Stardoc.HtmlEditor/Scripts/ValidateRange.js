/**
 * @author andres
 */
function ValidateRange(valueName, fromName, untilName){
    if (valueName && fromName && untilName) {
        var ValueElement = document.getElementById(valueName);
        var FromElement = document.getElementById(fromName);
        var UntilElement = document.getElementById(untilName);
        
        if (ValueElement && UntilElement && FromElement) {
        
            if (ValueElement.value < FromElement.value) {
                var ErrorMessage = 'El valor ingresado es menor a ';
                ErrorMessage += FromElement.value;
                alert(ErrorMessage);
                
                ValueElement.value = '';
            }
            else 
                if (ValueElement.value > UntilElement.value) {
                    var ErrorMessage = 'El valor ingresado es mayor a ';
                    ErrorMessage += UntilElement.value;
                    alert(ErrorMessage);
                    ValueElement.value = '';
                }
        }
    }
}
