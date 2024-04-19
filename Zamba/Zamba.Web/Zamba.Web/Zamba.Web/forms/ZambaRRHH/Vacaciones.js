/* referencias (no borrar por favor):

zamba_index_167 (dias pendientes)
zamba_index_169 (dias totales)
zamba_index_166 (dias del periodo)

zamba_index_227 (fecha desde opcion 1)
zamba_index_228 (fecha hasta opcion 1)
zamba_index_168 (dias solicitados opcion 1)
zamba_index_225 (Autorizar opcion 1)

zamba_index_229 (fecha desde opcion 2)
zamba_index_230 (fecha hasta opcion 2)
zamba_index_239 (dias solicitados opcion 2)
zamba_index_226 (Autorizar opcion 2)
*/


var LastFocusInput;
function InitFormVacationsInsert() {
    setEventsfillTotalDays();
    setEventsRequestDays();
    preProcessItemTypeDays();    
}
function InitFormVacationsEdit() {
    setEventsfillTotalDays();   
    setEventsRequestDays();
    preProcessItemTypeDays();    
}
function InitFormVacationsAuthorization() {

    setSingleCheckedAuthorization();
}
function InitFormVacationsView() {
    ShowOnlyVacationApproved();
}
function setSingleCheckedAuthorization() {
    document.getElementById('zamba_index_225').addEventListener('change', function () {
        checkedAuthorizationDateRange(1);
    });
    document.getElementById('zamba_index_226').addEventListener('change', function () {
        checkedAuthorizationDateRange(2);
    });
}


function ShowOnlyVacationApproved() {
    var check1 = document.getElementById("zamba_index_225").checked;
    var check2 = document.getElementById("zamba_index_226").checked;
    if (check1) {
        document.getElementById("row_option_2_a").style.display = "none";
        document.getElementById("row_option_2_b").style.display = "none";
    }
    if (check2) {
        document.getElementById("row_option_1_a").style.display = "none";
        document.getElementById("row_option_1_b").style.display = "none";
    }    
}
function checkedAuthorizationDateRange(option) {
    if (option == 1) {
        if (document.getElementById('zamba_index_225').checked = true) {
            document.getElementById('zamba_index_226').checked = false
        }
    } else {
        if (document.getElementById('zamba_index_226').checked = true) {
            document.getElementById('zamba_index_225').checked = false
        }
    }
    
}
function setEventsfillTotalDays() {

    document.getElementById('zamba_index_166').addEventListener('blur', function () {
        fillTotalDays();
    });
    document.getElementById('zamba_index_167').addEventListener('blur', function () {
        fillTotalDays();
    });
    fillTotalDays();
}
function setEventsRequestDays() {
    document.getElementById('zamba_index_227').addEventListener('blur', function () {
        LastFocusInput = this;
        fillRequestedDays(this);
        
    });
    document.getElementById('zamba_index_228').addEventListener('blur', function () {
        LastFocusInput = this;
        fillRequestedDays(this);
    });
    document.getElementById('zamba_index_229').addEventListener('blur', function () {
        LastFocusInput = this;
        fillRequestedDays(this);
    });
    document.getElementById('zamba_index_230').addEventListener('blur', function () {
        LastFocusInput = this;
        fillRequestedDays(this);
    });
    document.getElementById('zamba_index_224').addEventListener('change', function () {
        LastFocusInput = this;
        fillRequestedDays(this);
    });
    var inputs = document.querySelectorAll('input');
    inputs.forEach(function (input) {
        input.addEventListener('focus', function () {
            fillRequestedDays(input);
        });
    });
}
function DateToStringDDMMYYYY(fecha) {
    let partes = fecha.split('/');
    let mes = partes[1];
    let dia = partes[0];
    let anio = partes[2];
    let nuevaFecha = `${mes}/${dia}/${anio}`;
    return new Date(nuevaFecha);
}
function preProcessItemTypeDays() {
    RemoveItemToDefineInSelect(224);
    chooseItemInSelect(224, 1);
}

function fillRequestedDays(sender) {
    
    var TypeCalculateDays = document.getElementById("zamba_index_224").value;
    var chooseWorkingDays = TypeCalculateDays=='2';

    var Option1DateFrom = DateToStringDDMMYYYY(document.getElementById('zamba_index_227').value);
    var Option1DateTo = DateToStringDDMMYYYY(document.getElementById('zamba_index_228').value);
    var Option2DateFrom = DateToStringDDMMYYYY(document.getElementById('zamba_index_229').value);
    var Option2DateTo = DateToStringDDMMYYYY(document.getElementById('zamba_index_230').value);

    if (!isNaN(Option1DateFrom.getTime()) && !isNaN(Option1DateTo.getTime())) {
        if (chooseWorkingDays) {
            document.getElementById('zamba_index_168').value = calculateWorkingDays(Option1DateFrom, Option1DateTo,1,sender).toString();
        }
        else {
            document.getElementById('zamba_index_168').value = calculateContinuesDays(Option1DateFrom, Option1DateTo,1,sender).toString();
        }
    } else {
        document.getElementById('zamba_index_168').value = "";
    }
    if (!isNaN(Option2DateFrom.getTime()) && !isNaN(Option2DateTo.getTime())) {
        if (chooseWorkingDays) {
            document.getElementById('zamba_index_239').value = calculateWorkingDays(Option2DateFrom, Option2DateTo,2,sender).toString();
        }
        else {
            document.getElementById('zamba_index_239').value = calculateContinuesDays(Option2DateFrom, Option2DateTo,2,sender).toString();
        }
    } else {
        document.getElementById('zamba_index_239').value = "";
    }
}
function calculateWorkingDays(dateFrom, dateTo, option,sender) {
    document.getElementById("error_opcion_" + option).style.display = "none"
    if (dateFrom <= dateTo) {
        var totalDays = Math.round((dateTo - dateFrom) / (1000 * 60 * 60 * 24)) + 1;
        var workingDays = 0;

        for (var i = 0; i < totalDays; i++) {
            var day = new Date(dateFrom);
            day.setDate(day.getDate() + i);

            if (day.getDay() !== 0 && day.getDay() !== 6) {
                workingDays++;
            }
        }
        return workingDays;
    }
    else {
        ShowInvalidDate(option,sender);
        return "";
    }
}
function calculateContinuesDays(dateFrom, DateTo, option,sender) {
    document.getElementById("error_opcion_" + option).style.display = "none"
    if (dateFrom <= DateTo) {
        
        return Math.round((DateTo - dateFrom) / (1000 * 60 * 60 * 24)) + 1;
        
    }
    else {
        ShowInvalidDate(option,sender);        
        return "";
    }
    
}
function ShowInvalidDate(option,sender) {
    document.getElementById("error_opcion_" + option).style.display = "block";
    debugger;
    LastFocusInput= undefined;
}
function fillTotalDays() {
    var pendingDays = parseFloat(document.getElementById('zamba_index_166').value);
    var periodDays = parseFloat(document.getElementById('zamba_index_167').value);
    if (!isNaN(pendingDays) && pendingDays >= 0 && !isNaN(periodDays) && periodDays >= 0) {
        var totalDays = pendingDays + periodDays;
        document.getElementById('zamba_index_169').value = totalDays.toString();
    } else {
        document.getElementById('zamba_index_169').value = "";
    }
}

function RemoveItemToDefineInSelect(index) {
    var select = document.getElementById("zamba_index_" + index.toString());
    for (var i = 0; i < select.options.length; i++) {
        if (select.options[i].text === "A Definir") {
            select.remove(i);
            break;
        }
    }
}
function ButtonRequestVacations(obj) {
    if (document.getElementById("zamba_index_168").value == ""
        ||
        document.getElementById("zamba_index_239").value == ""
    ) {
        if (document.getElementById("zamba_index_168").value == "") {
            ShowInvalidDate(1);
        }
        if (document.getElementById("zamba_index_239").value == "") {
            ShowInvalidDate(2);
        }
        return false;
    }    
    SetRuleId(obj);
    return true;
}
function chooseItemInSelect(index, item) {
    document.getElementById("zamba_index_" + index.toString()).value = item;
}
    



