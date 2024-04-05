//zamba_index_167 (dias pendientes)
//zamba_index_169 (dias totales)
//zamba_index_166 (dias del periodo)

//zamba_index_227 (fecha desde opcion 1)
//zamba_index_228 (fecha hasta opcion 1)
//zamba_index_168 (dias solicitados opcion 1)
//zamba_index_225 (Autorizar opcion 1)

//zamba_index_229 (fecha desde opcion 2)
//zamba_index_230 (fecha hasta opcion 2)
//zamba_index_239 (dias solicitados opcion 2)
//zamba_index_226 (Autorizar opcion 2)


function InitFormVacationsInsert() {
    window.onload = function () {
        SetEventsfillTotalDays();
        setEventsRequestDays();
    };
    
}
function SetEventsfillTotalDays() {
    
    document.getElementById('zamba_index_166').addEventListener('blur', function () {        
        fillTotalDays();
    });
    document.getElementById('zamba_index_167').addEventListener('blur', function () {
        fillTotalDays();
    });
    fillTotalDays();
}
function setEventsRequestDays(){
    document.getElementById('zamba_index_227').addEventListener('blur', function () {
        fillRequestedDays();
    });
    document.getElementById('zamba_index_228').addEventListener('blur', function () {
        fillRequestedDays();
    });
    document.getElementById('zamba_index_229').addEventListener('blur', function () {
        fillRequestedDays();
    });
    document.getElementById('zamba_index_230').addEventListener('blur', function () {
        fillRequestedDays();
    });

    fillRequestedDays();
}
function fillRequestedDays() {
    var chooseWorkingDays = false;
    var Option1DateFrom = new Date(document.getElementById('zamba_index_227').value);
    var Option1DateTo = new Date(document.getElementById('zamba_index_228').value);
    var Option2DateFrom = new Date(document.getElementById('zamba_index_229').value);;;
    var Option2DateTo = new Date(document.getElementById('zamba_index_230').value);;;
    if (isNaN(Option1DateFrom.getTime()) && isNaN(Option1DateTo.getTime())) {
        if (chooseWorkingDays) {
            document.getElementById('zamba_index_168').value = calculateWorkingDays(Option1DateFrom, Option1DateTo).toString();
        }
        else {
            document.getElementById('zamba_index_168').value = calculateContinuesDays(Option1DateFrom, Option1DateTo).toString();
        }        
    } else {
        document.getElementById('zamba_index_168').value = "";
    }
    if (isNaN(Option2DateFrom.getTime()) && isNaN(Option2DateTo.getTime())) {
        if (chooseWorkingDays) {
            document.getElementById('zamba_index_239').value = calculateWorkingDays(Option2DateFrom, Option2DateTo).toString();
        }
        else {
            document.getElementById('zamba_index_239').value = calculateContinuesDays(Option1DateFrom, Option2DateTo).toString();
        }        
    } else {
        document.getElementById('zamba_index_239').value = "";
    }
}
function calculateWorkingDays(dateFrom, dateTo) {

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
function calculateContinuesDays(dateFrom, DateTo) {    
    return Math.round((dateFrom - DateTo) / (1000 * 60 * 60 * 24));
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



