/**
 * @author andres
 */
var _separator = '-';

function ValidateDates(firstElementName, secondElementName, triggeredName){
    var FirstElement = document.getElementById(firstElementName);
    var SecondElement = document.getElementById(secondElementName);
    
    if (FirstElement && SecondElement) {
        if (FirstElement.value != '' && SecondElement.value != '') 
            try {
                var FirstDateValues = FirstElement.value.split(_separator);
                var FirstDate = new Date(FirstDateValues[2], FirstDateValues[1] - 1, FirstDateValues[0]);
                var SecondDateValues = SecondElement.value.split(_separator);
                var SecondDate = new Date(SecondDateValues[2], SecondDateValues[1] - 1, SecondDateValues[0]);
                
                if (FirstDate > SecondDate) {
                    var ErrorMessage = 'La fecha ';
                    ErrorMessage += FirstElement.value;
                    ErrorMessage += ' es mas reciente que la fecha ';
                    ErrorMessage += SecondElement.value;
                    alert(ErrorMessage);
                    
                    var TriggeredElement = document.getElementById(triggeredName);
                    if (TriggeredElement) {
                        TriggeredElement.value = '';
                    }
                }
            } 
            catch (e) {
            }
    }
}

function ValidateRequiredDate(elementName){
    if (!elementName) 
        return false;
    
    var DateElement = document.getElementById(elementName);
    
    if (DateElement) 
        return false;
    
    try {
        var DateValues = DateElement.value.split(_separator);
        var ValidationDate = new Date(DateValues[2], DateValues[1] - 1, DateValues[0]);
    } 
    catch (e) {
        return false;
    }
    
    return true;
}
