/*
 * Id del control oculto que contiene el valor que se toma para ocultar los
 * botones. Si su valor es '1' , muestro el primer boton y oculto el 2do. Sino
 * hago lo contrario
 */
var HIDDEN_FIELD_ID = "hdnValue";
/*
 * Id del primer boton
 */
var FIRST_BUTTON_ID = "zamba_rule_108";
/*
 * Id del segundo boton
 */
var SECOND_BUTTON_ID = "zamba_rule_109";

window.onload = function(){
    try {
        var hidden = document.getElementById(HIDDEN_FIELD_ID);
        var first = document.getElementById(FIRST_BUTTON_ID);
        var second = document.getElementById(SECOND_BUTTON_ID);
        
        alert(HIDDEN_FIELD_ID);
        alert(hidden.value);
        if (hidden.value == '1') {
            first.style.visibility = 'visible';
            second.style.visibility = 'hidden';
        }
        else {
            first.style.visibility = 'hidden';
            second.style.visibility = 'visible';
        }
    } 
    catch (ex) {
        alert(ex);
    }
}
