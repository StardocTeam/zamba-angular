/**
 * @author andres
 */
var ValueOnFocus;
function ValidateInteger(el){
    var InputText = el.value;
    
    if (isNaN(InputText)) {
        alert('Debe ingresar 1 valor del tipo numerico.');
        
        el.value = ValueOnFocus;
        el.setActive();
    }
}

function SetValueOnFocus(el){
    ValueOnFocus = el.value;
}
