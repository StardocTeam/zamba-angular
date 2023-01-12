/**
 * @author andres
 */
var _requiredAttribute = 'required';
var _Isvalid = true;

function ValidateForm(){
	_Isvalid = true;
    var ChildElements = document.getElementsByTagName("form")[0].elements;
    ValidateEmpty(ChildElements);
    
    return _Isvalid;
}

function ValidateEmpty(elements){
    if (elements) {
        var CurrentElement;
        
        for (var i = 0; i < elements.length; i++) {
            CurrentElement = elements[i];
            
            if (CurrentElement.childNodes.length > 0) 
                ValidateEmpty(CurrentElement, _Isvalid);
            else 
                if (CurrentElement.type == "text" &&
                CurrentElement.hasAttribute(_requiredAttribute) &&
                CurrentElement.attributes.getNamedItem(_requiredAttribute).value == 'true') {
                
                    if (CurrentElement.value == "") 
                        ShowValidator(CurrentElement);                    
                    else 
                        HideValidator(CurrentElement);
                }
        }
    }
}

function ShowValidator(element){
    try {
        if (element && element.nextSibling) 
            element.nextSibling.style.display = "block";
    } 
    catch (e) {
    }
    _Isvalid = false;
    
}

function HideValidator(element){
    try {
        if (element && element.nextSibling) 
            element.nextSibling.style.display = "none";
    } 
    catch (e) {
    }
    
}
