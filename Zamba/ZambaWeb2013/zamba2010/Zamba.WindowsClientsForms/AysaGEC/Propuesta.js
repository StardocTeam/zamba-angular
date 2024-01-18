/**
 * @author Andres Nagel
 */
/**
 * Esta clase es un wrapper para obtener la posicion de 1 elemento.
 * @param {Object} targetElement Elemento que va a tener
 */
function positionInfo(targetElement){

    var _targetElement = targetElement;
    
    this.getElementLeft = getElementLeft;
    
    function getElementLeft(){
        var x = 0;
        var elm;
        if (typeof(_targetElement) == "object") {
            elm = _targetElement;
        }
        else {
            elm = document.getElementById(_targetElement);
        }
        while (elm != null) {
            x += elm.offsetLeft;
            elm = elm.offsetParent;
        }
        return parseInt(x);
    }
    
    this.getElementWidth = getElementWidth;
    
    function getElementWidth(){
        var elm;
        if (typeof(_targetElement) == "object") {
            elm = _targetElement;
        }
        else {
            elm = document.getElementById(_targetElement);
        }
        return parseInt(elm.offsetWidth);
    }
    
    this.getElementRight = getElementRight;
    
    function getElementRight(){
        return getElementLeft(_targetElement) + getElementWidth(_targetElement);
    }
    
    this.getElementTop = getElementTop;
    
    function getElementTop(){
        var y = 0;
        var elm;
        if (typeof(_targetElement) == "object") {
            elm = _targetElement;
        }
        else {
            elm = document.getElementById(_targetElement);
        }
        while (elm != null) {
            y += elm.offsetTop;
            elm = elm.offsetParent;
        }
        return parseInt(y);
    }
    
    this.getElementHeight = getElementHeight;
    
    function getElementHeight(){
        var elm;
        if (typeof(_targetElement) == "object") {
            elm = _targetElement;
        }
        else {
            elm = document.getElementById(_targetElement);
        }
        return parseInt(elm.offsetHeight);
    }
    
    this.getElementBottom = getElementBottom;
    
    function getElementBottom(){
        return getElementTop(_targetElement) + getElementHeight(_targetElement);
    }
}

/**
 * Clase que representa al control Calendario.
 *
 */
function CalendarControl(){

    var calendarId = 'CalendarControl';
    var currentYear = 0;
    var currentMonth = 0;
    var currentDay = 0;
    
    var selectedYear = 0;
    var selectedMonth = 0;
    var selectedDay = 0;
    
    var months = ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'];
    var dateField = null;
    
    /* 
     function getProperty(p_property){
     var _p_elm = calendarId;
     var elm = null;
     
     if (typeof(_p_elm) == "object") {
     elm = _p_elm;
     }
     else {
     elm = document.getElementById(_p_elm);
     }
     if (elm != null) {
     if (elm.style) {
     elm = elm.style;
     if (elm[p_property]) {
     return elm[p_property];
     }
     else {
     return null;
     }
     }
     else {
     return null;
     }
     }
     }
     */
    /**
     * Cambia el valor de 1 propiedad de 1 elemento     *
     * @param {Object} propertyName Nombre de la propiedad
     * @param {Object} propertyValue Valor de la propiedad
     * @param {Object} elementId Id del elemento
     */
    function setElementProperty(propertyName, propertyValue, elementId){
        var CurrentElement = null;
        
        if (typeof(elementId) == "object") 
            CurrentElement = elementId;
        else 
            CurrentElement = document.getElementById(elementId);
        
        if ((CurrentElement != null) && (CurrentElement.style != null)) {
            CurrentElement = CurrentElement.style;
            CurrentElement[propertyName] = propertyValue;
        }
    }
    
    /**
     * Cambia el valor de 1 propiedad del calendario
     * @param {Object} propertyName Nombre de la propiedad
     * @param {Object} propertyValue Valor de la propiedad
     */
    function setProperty(propertyName, propertyValue){
        setElementProperty(propertyName, propertyValue, calendarId);
    }
    
    /**
     * Devuelve el total de dias de un mes en 1 año
     * @param {Number} year Año seleccionado
     * @param {Number} month Mes seleccionado
     * @return {Number} Total de dias
     */
    function getDaysInMonth(year, month){
        return [31, ((!(year % 4) && ((year % 100) || !(year % 400))) ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][month - 1];
    }
    
    /**
     * Devuelve el numero de dia que representa.
     * 0 Domingo
     * 1 Lunes
     * ...etc
     * @param {Object} year Año elegido
     * @param {Object} month Mes elegido
     * @param {Object} day Dia elegido
     */
    function getDayOfWeek(year, month, day){
        var date = new Date(year, month - 1, day)
        return date.getDay();
    }
    
    this.clearDate = clearDate;
    /**
     * Borra el valor seleccionado en el elemento que llamo al calendario y oculta el calendario
     */
    function clearDate(){
        dateField.value = '';
        hide();
    }
    
    this.setDate = setDate;
    
    /**
     * Guarda la fecha en el control asociado al calendario
     * @param {Object} year
     * @param {Object} month
     * @param {Object} day
     */
    function setDate(year, month, day){
        if (dateField) {
            if (month < 10) 
                month = "0" + month;
            if (day < 10) 
                day = "0" + day;
            
            var dateString = day + "/" + month + "/" + year;			
            document.getElementById(dateField.id).value = dateString;
            try {
                document.getElementById(dateField.id).onblur();
            } 
            catch (e) {
            }
            
            hide();
        }
        return;
    }
    
    this.changeMonth = changeMonth;
    
    /**
     * Cambia el mes del calendario
     * @param {Object} variacion. Valor que varia el mes selecionado. Para seleccionar meses anteriores usar negativos
     */
    function changeMonth(variacion){
        currentMonth += variacion;
        currentDay = 0;
        if (currentMonth > 12) {
            currentMonth = 1;
            currentYear++;
        }
        else 
            if (currentMonth < 1) {
                currentMonth = 12;
                currentYear--;
            }
        
        calendar = document.getElementById(calendarId);
        calendar.innerHTML = calendarDrawTable();
    }
    
    this.changeYear = changeYear;
    /**
     * Cambia el año del calendario
     * @param {Object} change Valor que varia el año selecionado. Para seleccionar años anteriores usar negativos
     */
    function changeYear(variacion){
        currentYear += variacion;
        currentDay = 0;
        calendar = document.getElementById(calendarId);
        calendar.innerHTML = calendarDrawTable();
    }
    /**
     * Devuelve el año actual
     * @return {Number} Año actual
     */
    function getCurrentYear(){
        var year = new Date().getYear();
        if (year < 1900) 
            year += 1900;
        return year;
    }
    /**
     * Devuelve el mes actual
     *  @return {Number} Mes actual
     */
    function getCurrentMonth(){
        return new Date().getMonth() + 1;
    }
    /**
     * Devuelve el dia actual
     *  @return {Number} Dia actual
     */
    function getCurrentDay(){
        return new Date().getDate();
    }
    /**
     * Arma el calendario y devuelve el HTML correspondiente
     * @return {String} HTML del calendario
     */
    function calendarDrawTable(){
    
        var dayOfMonth = 1;
        var validDay = 0;
        var startDayOfWeek = getDayOfWeek(currentYear, currentMonth, dayOfMonth);
        var daysInMonth = getDaysInMonth(currentYear, currentMonth);
        var css_class = null; //CSS class for each day
        var table = "<table cellspacing='0' cellpadding='0' border='0'>";
        table = table + "<tr class='header'>";
		table = table + "  <td class='previous' title='Año anterior' onclick='javascript:changeCalendarControlYear(-1);' ><<</td>";
		table = table + "<td class='previous' title='Mes anterior' onclick='javascript:changeCalendarControlMonth(-1);' ><</td>"        
        table = table + "  <td colspan='3' class='title'>" + months[currentMonth - 1] + "<br>" + currentYear + "</td>";        
		table = table + "  <td class='next' title='Mes siguiente' onclick='javascript:changeCalendarControlMonth(1);' >></td> ";
		table = table + "  <td class='next' title='Año siguiente' onclick='javascript:changeCalendarControlYear(1);' >>></td>";
        table = table + "</tr>";
        table = table + "<tr><th>D</th><th>L</th><th>M</th><th>M</th><th>J</th><th>V</th><th>S</th></tr>";
        
        for (var week = 0; week < 6; week++) {
            table = table + "<tr>";
            for (var dayOfWeek = 0; dayOfWeek < 7; dayOfWeek++) {
                if (week == 0 && startDayOfWeek == dayOfWeek) {
                    validDay = 1;
                }
                else 
                    if (validDay == 1 && dayOfMonth > daysInMonth) {
                        validDay = 0;
                    }
                
                if (validDay) {
                    if (dayOfMonth == selectedDay && currentYear == selectedYear && currentMonth == selectedMonth) {
                        css_class = 'current';
                    }
                    else 
                        if (dayOfWeek == 0 || dayOfWeek == 6) {
                            css_class = 'weekend';
                        }
                        else {
                            css_class = 'weekday';
                        }
                    
                    table = table + "<td onclick=\"setCalendarControlDate(" + currentYear + "," + currentMonth + "," + dayOfMonth + ")\"><a class='" + css_class + "'>" + dayOfMonth + "</a></td>";
                    dayOfMonth++;
                }
                else {
                    table = table + "<td class='empty'>&nbsp;</td>";
                }
            }
            table = table + "</tr>";
        }
        
		table = table + "<tr class='header'><td colspan='3' onclick='clearCalendarControl();' title='Borra el valor seleccionado' align='left' >Borrar</td><td colspan='4' onclick='hideCalendarControl();' title='Cierra el calendario' align='right'>Cerrar</td> "
        //table = table + "<tr class='header'><th colspan='7' style='padding: 3px;'><a href='javascript:clearCalendarControl();'>Borrar</a> | <a href='javascript:hideCalendarControl();'>Cerrar</a></td></tr>";
        table = table + "</table>";
        
        return table;
    }
    
    this.show = show;
    /**
     * Asocia el calendario a un elemento HTML y lo muestra.
     * @param {Object} field
     */
    function show(field){
        can_hide = 0;
        
        /* If the calendar is visible and associated with
         this field do not do anything.*/
        if (dateField == field) {
            return;
        }
        else {
            dateField = field;
        }
        
        if (dateField) {
            try {
                var dateString = new String(dateField.value);
                var dateParts = dateString.split("/");
                
                selectedDay = parseInt(dateParts[0], 10);
                selectedMonth = parseInt(dateParts[1], 10);
                selectedYear = parseInt(dateParts[2], 10);
            } 
            catch (e) {
            }
        }
        
        if (!(selectedYear && selectedMonth && selectedDay)) {
            selectedMonth = getCurrentMonth();
            selectedDay = getCurrentDay();
            selectedYear = getCurrentYear();
        }
        
        currentMonth = selectedMonth;
        currentDay = selectedDay;
        currentYear = selectedYear;
        
        if (document.getElementById) {
        
            calendar = document.getElementById(calendarId);
            calendar.innerHTML = calendarDrawTable(currentYear, currentMonth);
            
            setProperty('display', 'block');
            
            var fieldPos = new positionInfo(dateField);
            var calendarPos = new positionInfo(calendarId);
            
            var x = fieldPos.getElementLeft();
            var y = fieldPos.getElementBottom();
            
            setProperty('left', x + "px");
            setProperty('top', y + "px");
            
            if (document.all) {
                setElementProperty('display', 'block', 'CalendarControlIFrame');
                setElementProperty('left', x + "px", 'CalendarControlIFrame');
                setElementProperty('top', y + "px", 'CalendarControlIFrame');
                setElementProperty('width', calendarPos.getElementWidth() + "px", 'CalendarControlIFrame');
                setElementProperty('height', calendarPos.getElementHeight() + "px", 'CalendarControlIFrame');
            }
        }
    }
    this.hide = hide;
    /**
     * Oculta el calendario
     */
    function hide(){
        if (dateField) {
            setProperty('display', 'none');
            setElementProperty('display', 'none', 'CalendarControlIFrame');
            dateField = null;
        }
    }
    
    this.visible = visible;
    function visible(){
        return dateField
    }
    
    this.can_hide = can_hide;
    var can_hide = 0;
}

var calendarControl = new CalendarControl();

function showCalendarControl(textField){
    if (textField.onblur) 
        textField.onblur = hideCalendarControl;
    
    if (!textField.readOnly) 
        calendarControl.show(textField);
}

function clearCalendarControl(){
    calendarControl.clearDate();
}

/**
 * Oculta el calendario
 */
function hideCalendarControl(){
    if (calendarControl.visible()) {
        calendarControl.hide();
    }
}

/**
 *
 * @param {Object} year
 * @param {Object} month
 * @param {Object} day
 */
function setCalendarControlDate(year, month, day){
    calendarControl.setDate(year, month, day);
}

/**
 *
 * @param {Object} change
 */
function changeCalendarControlYear(change){
    calendarControl.changeYear(change);
}

function changeCalendarControlMonth(change){
    calendarControl.changeMonth(change);
}

document.write("<iframe id='CalendarControlIFrame' src='javascript:false;' frameBorder='0' scrolling='no'></iframe>");
document.write("<div id='CalendarControl'></div>");