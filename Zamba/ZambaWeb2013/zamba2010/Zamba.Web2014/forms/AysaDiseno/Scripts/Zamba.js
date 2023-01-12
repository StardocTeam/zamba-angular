//$(function() 
//{
//    $(".fg-button:not(.ui-state-disabled)").hover
//    (
//        function() {
//            $(this).addClass("ui-state-hover");
//        },
//        function() {
//            $(this).removeClass("ui-state-hover");
//        }
//    )
//    .mousedown
//    (
//        function() {
//            $(this).parents('.fg-buttonset-single:first').find(".fg-button.ui-state-active").removeClass("ui-state-active");
//            if ($(this).is('.ui-state-active.fg-button-toggleable, .fg-buttonset-multi .ui-state-active')) {
//                $(this).removeClass("ui-state-active");
//            }
//            else {
//                $(this).addClass("ui-state-active");
//            }
//        }
//    )
//    .mouseup
//    (
//        function() {
//            if (!$(this).is('.fg-button-toggleable, .fg-buttonset-single .fg-button, .fg-buttonset-multi .fg-button')) {
//                $(this).removeClass("ui-state-active");
//            }
//        }
//     );
//});

//var previousWidth;

//function makeCalendar(id) 
//{
//    $(function () {
//        if (id.indexOf("completarindice") != -1) {
//            $('#' + id).datepicker({
//                changeMonth: true,
//                changeYear: true,
//                showOn: 'button',
//                buttonText: 'Abrir calendario',
//                buttonImage: '../../Content/Images/calendar.png',
//                buttonImageOnly: true,
//                duration: "",
//                onClose: function () {
//                    //Restauramos el width original
//                    $("#DivIndices").animate({ width: previousWidth }, "fast");
//                    $("#separator").animate({ width: previousWidth }, "fast");

//                    //Acomodamos el documento a lo restaurado.
//                    var a = $(window).width() - previousWidth - 5;
//                    $("#separator").next().animate({ width: a }, "fast");
//                },
//                beforeShow: function (input, inst) {
//                    //Guardamos el width anterior.  
//                    previousWidth = $("#DivIndices").width();

//                    //Saco el ancho de los labels
//                    var labelWidth = $(input).parent().prev().width();
//                                        
//                    var calcultateWidth = $(inst.dpDiv).width() + labelWidth + 20;
//                    
//                    $("#DivIndices").animate({ width: calcultateWidth }, "slow");
//                    $("#separator").animate({ width: calcultateWidth }, "slow");

//                    //Acomodamos el documento, con la diferencia de pantalla.
//                    var a = $(window).width() - calcultateWidth - 5;
//                    $("#separator").next().animate({ width: a }, "slow");
//                }
//            });
//        }
//        else {
//            $('#' + id).datepicker({
//                changeMonth: true,
//                changeYear: true,
//                showOn: 'button',
//                buttonText: 'Abrir calendario',
//                buttonImage: '../../Content/Images/calendar.png',
//                buttonImageOnly: true,
//                duration: ""
//            });
//        }
//    });
//}

//function windowHeight() 
//{
//    var myHeight = 0;
//    if (typeof (window.innerWidth) == 'number') 
//    {
//        myHeight = window.innerHeight;
//    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
//        myHeight = document.documentElement.clientHeight;
//    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
//        myHeight = document.body.clientHeight;
//    }
//    return myHeight;
//}

//function windowWidth() 
//{
//    var myWidth = 0;
//    if (typeof (window.innerWidth) == 'number') 
//    {
//        myWidth = window.innerWidth;
//    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
//        myWidth = document.documentElement.clientWidth;
//    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
//        myWidth = document.body.clientWidth;
//    }
//    return myWidth;
//}

//function expandDiv(div, difwidth, difheight) 
//{
//    var myWidth = 0, myHeight = 0;
//    if (typeof (window.innerWidth) == 'number') 
//    {
//        //Non-IE
//        myWidth = window.innerWidth;
//        myHeight = window.innerHeight;
//    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
//        //IE 6+ in 'standards compliant mode'
//        myWidth = document.documentElement.clientWidth;
//        myHeight = document.documentElement.clientHeight;
//    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
//        //IE 4 compatible
//        myWidth = document.body.clientWidth;
//        myHeight = document.body.clientHeight;
//    }
//    myWidth = myWidth - difwidth;
//    myHeight = myHeight - difheight
//    setInterval("doExpand('" + div + "'," + myWidth + "," + myHeight + ")", 500);
//}

//function doExpand(div, w, h) 
//{
//    $("#" + div).width(w);
//    $("#" + div).height(h);
//}


//function modifyTextBoxSize(tb)
//{
//    if(tb.rows == 5)
//    {
//        tb.rows = 1;
//    }
//    else
//    {
//        tb.rows = 5;
//    }
//}
//function maxLenght(tb, max)
//{
//    return (tb.value.length < max);
//}

////Obtiene la height que deben tener los contenedores principales
//function getHeightScreen() {
//    var masterHeader = $("#MasterHeader").css("display") == "none" ? 0 : $("#MasterHeader").height();
//    var barraCabecera = $("#barra-Cabezera").css("display") == "none" ? 0 : $("#barra-Cabezera").innerHeight();
//    var headersHeight = masterHeader + barraCabecera;
//    return document.body.clientHeight - headersHeight;
//}

//function getMTHeight() {
//    var mt = document.getElementById("mainMenu");
//    if (mt) {
//        return $(mt).height();
//    }
//    else{
//        return 0;
//    }
//}

//function getTBHeight() {
//    var mt = document.getElementById("TasksDivUL");
//    if (mt) {
//        return $(mt).height();
//    }
//    else {
//        return 0;
//    }
//}

//function ValidateLength(element, RequiredLength) {
//    var Str = element.value;
//    $(element).valid();
//    if (Str.length < RequiredLength)
//        return true;
//    return false;
//}

//function KeyIsValid(e, TypeToValidate, Obj) {

//    var key = e.charCode || e.keyCode || 0;
//    var isValid = false;

//    switch (TypeToValidate) {
//        case "numeric":
//            if (key >= 48 && key <= 57)
//                isValid = true;
//            break;
//        case "date":
//            if ((key >= 48 && key <= 57) || key == 47)
//                isValid = true;
//            break;
//        default:
//            if(TypeToValidate.indexOf("decimal") < 0)
//                break;
//            
//            var arrayOfConfiguration = TypeToValidate.split('_');
//            if(arrayOfConfiguration.length < 2)
//                break;
//            
//            var decimalCount = arrayOfConfiguration[1];
//            if(!decimalCount)
//                break;
//                
//            var integerCount = arrayOfConfiguration[2];
//            if(!integerCount)
//                break;
//                                
//            var currentValue = $(Obj).val();
//            var commaPos = currentValue.indexOf(',');
//            var caretPos = $(Obj).caret().start;

//            if (key == 44)
//                isValid = (commaPos == -1 && caretPos != 0);
//            else {
//                if ((key >= 48 && key <= 57)) {
//                    if (commaPos == -1) {
//                        isValid = (currentValue.length < integerCount);
//                    }
//                    else {
//                        if (caretPos > commaPos) {
//                            isValid = (currentValue.length - decimalCount < commaPos + 1);
//                        }
//                        else
//                            isValid = (commaPos < integerCount);
//                    }
//                }
//            }
//            break;
//    }

//    return isValid;
//}

//function SetValidationsAction(SubmitButtonId) {
//    SetValidateForLength();
//    SetValidateForDataType();
//    SetValidateForRequired();
//    SetDefaultValues();
//    SetHierarchicalFunctionally();
//    SetListFilters();
//    
//    if(SubmitButtonId){
//        $("#" + SubmitButtonId).click(function(evt){
//            if(!$("#aspnetForm").valid())
//            {
//                evt.preventDefault();
//                setTimeout("parent.hideLoading();", 1000);
//            }
//        });
//    }
//}

//function SetValidateForLength(){
//    $(".length").each(function () {
//        if (!$(this).hasClass("readonly")) {
//            var length = $(this).attr("length");
//            if (length) {
//                $(this).change(function (evt) {
//                    if (!ValidateLength(this, length))
//                        evt.preventDefault();
//                });

//                $(this).keypress(function (evt) {
//                    if (!ValidateLength(this, length))
//                        evt.preventDefault();
//                });

//                $(this).rules("add", {
//                    maxlength: length,
//                    messages: {
//                        maxlength: jQuery.format("Se ha exedido largo de " + length + " caracteres.")
//                    }
//                });
//            }
//        }
//    });
//}

//function SetValidateForDataType(){
//    $(".dataType").each(function () {
//        if (!$(this).hasClass("readonly")) {
//            var dataType = $(this).attr("dataType");
//            if (dataType) {
//                $(this).change(function (evt) {
//                    if (!KeyIsValid(evt, dataType, this))
//                        evt.preventDefault();
//                });

//                $(this).keypress(function (evt) {
//                    if (!KeyIsValid(evt, dataType, this))
//                        evt.preventDefault();
//                });

//                switch (dataType) {
//                    case "numeric":
//                        $(this).rules("add", {
//                            digits: true,
//                            messages: {
//                                digits: jQuery.format("Solo se permite un n&uacute;mero entero.")
//                            }
//                        });
//                        break;
//                    case "date":
//                        $(this).rules("add", {
//                            date: true,
//                            messages: {
//                                date: jQuery.format("La fecha debe estar en formato:<br/> dia/mes/año.")
//                            }
//                        });
//                        MakeDateIndexs(this);
//                        break;
//                    default:
//                        $(this).rules("add", {
//                            number: true,
//                            messages: {
//                                number: jQuery.format("Solo se permiten n&uacute;meros.")
//                            }
//                        });
//                        break;
//                }
//            }
//        }
//    });
//}

//function SetValidateForRequired(){
//    $(".isRequired").each(function () {
//        MakeRequired(this.ID);
//    });
//}

//function MakeRequired(idObj) {
//    var obj = document.getElementById(idObj);

//    var indexName = $(obj).attr("indexName");
//    if (indexName) {
//        $(obj).rules("add", {
//            required: true,
//            messages: {
//                required: jQuery.format("El campo " + indexName + " es requerido.")
//            }
//        });
//    }
//    else {
//        $(obj).rules("add", {
//            required: true,
//            messages: {
//                required: jQuery.format("Por favor complete este campo.")
//            }
//        });
//    }
//}

//function MakeNonRequired(idObj) {
//    //Para hacer no requerido se remueve la regla de requerido
//    $("#" + idObj).rules("remove", "required");
//}

//function Enable(objID) {
//    var obj = document.getElementById(objID);
//    var controlType = obj.nodeName.toLowerCase();

//    if(obj)
//    {
//        switch (controlType) {
//            case "input":
//            case "textarea":
//                $(obj).removeAttr("readOnly");
//                $(obj).removeClass("ReadOnly");
//                break;
//            case "select":
//                $(obj).removeAttr("disabled");
//                break;
//        }    
//    }    
//}

//function Disable(objID) {
//    var obj = document.getElementById(objID);
//    var controlType = obj.nodeName.toLowerCase();

//    if (obj) {
//        switch (controlType) {
//            case "input":
//            case "textarea":
//                $(obj).attr("readOnly", "readOnly");
//                $(obj).addClass("ReadOnly");
//                break;
//            case "select":
//                $(obj).attr("disabled", "disabled");
//                break;
//        }
//    }  
//}

//function SetDefaultValues(){
//    $(".haveDefaultValue").each(function(){
//        var DefaultValue = $(this).attr("DefaultValue");
//        if($(this).val() == '')
//            $(this).val(DefaultValue);
//    });
//}

//function SetHierarchicalFunctionally(){
//    $(".HierarchicalIndex").each(function(){
//        $(this).change(function(){
//            var childIndexId = $(this).attr("ChildIndexId");
//            var parentIndexId = $(this).attr('id').split('_')[2];
//            GetHierarchyOptions(childIndexId,parentIndexId,$(this).val());
//        });
//    });
//}

//function GetHierarchyOptions(IndexId,ParentIndexId,ParentValue,SenderID){
//    var userId;
//    var hdnUserId = document.getElementById("ctl00_hdnUsrID");
//    if(hdnUserId)
//    {
//        userId = hdnUserId.value;
//    }
//    else
//    {
//        userId = document.getElementById("hdnUsrID").value;
//    }
//    
//    ScriptWebServices.IndexsService.set_defaultSucceededCallback(IndexsCallback);
//    ScriptWebServices.IndexsService.set_defaultFailedCallback(IndexsOnError);
//    
//    if(SenderID)
//    {
//        ScriptWebServices.IndexsService.GetHierarchyOptionsWidthID(IndexId,ParentIndexId,ParentValue,userId,SenderID);
//    }
//    else
//    {
//        ScriptWebServices.IndexsService.GetHierarchyOptions(IndexId,ParentIndexId,ParentValue,userId);
//    }
//    return true;
//}

////Agrega una condicion dinamica.
//function InjectCondition(idSource,idTarget,action,rollback,value,comparator)
//{
//	var comparation;
//    var comparateValue;
//    //Valor a comparar
//    var comparateSource = "$(this).val().toLowerCase()";

//    //Si el valor viene con <Attribute> usar el valor del atributo en el form.
//    if(value.indexOf("<Attribute>") > -1)
//    {
//        var attrID = "zamba_index_" + value.replace("<Attribute>(", "").replace(")", "");
//        comparateValue = "$('#" + attrID + "').val().toLowerCase()";
//    }
//    else
//        comparateValue = "'" +  value.toLowerCase() + "'";

//    //Si el comparador es comun, contruye la comparacion normalmente, sino le agrega su funcion
//    switch(comparator)
//    {
//        case "==": case "!=":  case "<": case ">": case "<=": case ">=":
//            comparation = comparateSource + comparator + comparateValue;
//            break;
//        case "starts":
//            comparation = comparateSource + ".startsWith("+ comparateValue + ")";
//            break;
//        case "ends":
//            comparation = comparateSource + ".endsWith("+ comparateValue + ")";
//            break;
//        case "in":
//            comparation = comparateSource + ".contains(" + comparateValue + ")";
//    }

//    var objSource = document.getElementById(idSource);

//    //Si el campo se encuentra
//    if (objSource) {
//        //Si el input se puede ingresar datos tecleando, agregar la condicion
//        if (objSource.nodeName.toLowerCase() == "input" ||
//            objSource.nodeName.toLowerCase() == "textarea") {

//            $(objSource).keyup(function () {
//                //Evalua la comparacion
//                if (eval(comparation)) {
//                    //Ejecuta la accion
//                    eval(action + "('" + idTarget + "')");
//                }
//                else {
//                    //Ejecuta el rollback
//                    eval(rollback + "('" + idTarget + "')");
//                }
//            });
//        }

//        $(objSource).change(function () {
//            if (eval(comparation)) {
//                eval(action + "('" + idTarget + "')");
//            }
//            else {
//                eval(rollback + "('" + idTarget + "')");
//            }
//        });

//        $(window).load(function () { 
//            $("#" + idSource).change();
//        });
//    }
//}

//function SetListFilters() {
//    //Obtiene todos los tags select
//    $('select:not(:disabled)').each(function () {
//        //Verifica que lo que encontro sea un índice
//        if ($(this).attr('id').toLowerCase().indexOf('zamba_index_') > -1 && !$(this).hasClass("readonly") && $(this).css('display') != 'none') {
//            //Agrega la lupa para filtrar y buscar valores
//            AddFilter($(this).attr('id'), false);
//        }
//    });
//}

//function IndexsCallback(result) {
//    var idReturned = result.toString().split('|')[0];
//    var childIndexId;
//    
//    if(isNaN(idReturned))
//        childIndexId = idReturned;
//    else
//        childIndexId = "zamba_index_" + result.toString().split('|')[0];
//        
//    var indexOptions = result.toString().split('|')[1];
//    var childIndexSelect = document.getElementById(childIndexId);
//    
//    if(childIndexSelect && childIndexSelect.nodeName && childIndexSelect.nodeName.toLowerCase() != "input") 
//    {
//        $('#' + childIndexId).html(indexOptions);
//        $('#' + childIndexId).change();
//    }
//}

//function IndexsOnError(result) {
//    alert("Error: " + result.get_message());
//}

//function ExecuteFormReadyActions() {
//    $("#zamba_save").click(function() {
//        ShowLoadingAnimation();
//    });
//    
//    //Obtiene los textbox de los formularios, y les agrega en el foco la seleccion del texto.
//    //Con esto se deberia solucionar el problema del "bloqueo de los textbox".
//    FixFocusError();
//     
//    $('#aspnetForm').validate();
//    SetValidationsAction("zamba_save");
//}

////Calendars //Indices Tipo Fecha
//function MakeDateIndexs(item)
//{
//    if (!$(item).hasClass("ReadOnly") )  makeCalendar(item.id);
//}

//function zamba_save_onclick() {
//    //document.getElementById("hdnRuleId").name = "zamba_save";
//}

//function zamba_cancel_onclick() {
//    //document.getElementById("hdnRuleId").name = "zamba_save";
//}


//// Se encarga de corregir los problemas de foco al hacer postback que pasa en formularios.
//// Los input de tipo text y los textarea deben implementar la clase readOnly y tener el
//// atributo readonly="readonly" para que funcione. No se debe usar el disabled.
//function FixFocusError() {
//    //Obtiene los textbox de los formularios, y les agrega en el foco la seleccion del texto.
//    //Con esto se deberia solucionar el problema del "bloqueo de los textbox".
//    $("input[type=text]").focus(function() {
//        if (!$(this).hasClass("hasDatepicker")) {
//            $(this).select();
//            if (typeof ($(this).caret) != "undefined") {
//                //Esto es para intentar solucionar nuevamente el error de foco
//                $(this).caret().start = 0;
//                $(this).caret().end = 0;
//            }
//        }
//    });

//    //Obtiene los textbox de los formularios, y les agrega en el foco la seleccion del texto.
//    //Con esto se deberia solucionar el problema del "bloqueo de los textbox".
//    $("textarea").focus(function() {
//        $(this).select();
//        if (typeof ($(this).caret) != "undefined") {
//            //Esto es para intentar solucionar nuevamente el error de foco
//            $(this).caret().start = 0;
//            $(this).caret().end = 0;
//        }
//    });
//}

//function SetIndexPnlVisibility(obj, divDoc, panel2Indices) {
//    var divIndice = document.getElementById("DivIndices");
//    var btnOcultar = document.getElementById("btnOcultar");
//    var btnVisualizar = document.getElementById("btnVisualizar");
//    var tableIndice = $("#ctl00_ContentPlaceHolder_ucTaskDetail_ctl00_completarindice_tblIndices").width();    
//    var a;
//    var scrollDifference;
//    if ($(obj).hasClass("Expand")) {
//        $(panel2Indices).css("overflow", "auto");
//        scrollDifference = $(divIndice)[0].scrollWidth - tableIndice;
//        //Muestra el panel de índices
//        $(divIndice).animate({ left: 0 }, "slow");

//        //Calcula el espacio necesario para desplazar el documento
//        a = $(divDoc).width() - $(divIndice).width();

//        //Hace los desplazamientos con animacion       
//        $("#separator").animate({ width: $(divIndice).width() }, "slow");
//        $(divDoc).animate({ width: a + 16 + scrollDifference }, "slow");

//        $(obj).addClass("Collapse");
//        $(obj).removeClass("Expand");        
//    }
//    else {
//        //Oculta el panel de índices
//        $(panel2Indices).css("overflow", "hidden");
//        scrollDifference = $(divIndice)[0].scrollWidth - tableIndice;
//        a = 0 - $(divIndice).width() + 16 + scrollDifference;

//        //Hace la animacion
//        $(divIndice).animate({ left: a }, "slow");

//        //Acomoda el documento
//        $("#separator").animate({ width: 16 }, "slow");
//        a = $(divDoc).width() + $(divIndice).width() - 16 + scrollDifference;
//        $(divDoc).animate({ width: a }, "slow");

//        $(obj).addClass("Expand");
//        $(obj).removeClass("Collapse");
//        
//    }
//}

////Funcion que agrega un filtro al control
////indexid = id del tag que contiene el indice a aplicar el control
//// codecolumn = true/false si se quiere visualizar o no la columna de codigo
//function AddFilter(indexid, codecolumn) {
//    var btnid = indexid.toString().split('_')[indexid.toString().split('_').length - 1];
//    var $btnfilter = $('<img id="table_filter_' + btnid + '" onclick="CreateTable(this,' + codecolumn + ');" style="height: 16px; margin-left:10px;" alt="Filtrar" src="../../Content/Images/UI_blue/search.gif"/>');
//    $("#" + indexid).after($btnfilter);
//}
//function CreateTable(obj, codecolumn) {

//    parent.ShowLoadingAnimation();
//    
//    //Creando el nombre del select
//    var arrayObj = obj.id.toString().split('_');
//    var cBox = "zamba_index_" + arrayObj[arrayObj.length - 1].trim();
//    $('#dynamic_filter').data("indexid", cBox);

//    //var table = LoadTable($('#dynamic_filter').data("indexid"), codecolumn);
//    var table = LoadTable3(cBox, codecolumn);

//    //Limpiando la tabla
//    $('#dynamic_filter').html("");

//    //Agregando los botones
//    var html = table;
//    html += '<center><input id="btnAceptDynamic" type="button" value="Aceptar" onclick="Accept()" />';
//    html += '<input id="btnCancelDynamic" type="button" value="Cancelar" style=" margin-left:10px;" onclick="Cancel()" /></center>';

//    //Agregando la tabla creada dinamicamente
//    $('#dynamic_filter').html(html);

//    $("#example tbody").click(function(event) { $(oTable.fnSettings().aoData).each(function() { $(this.nTr).removeClass('row_selected'); }); $(event.target.parentNode).addClass('row_selected'); });

//    //Agregando el evento click a cada fila
//    $('#example tr').click(function() {
//        if ($(this).hasClass('row_selected'))
//            $(this).removeClass('row_selected');
//        else
//            $(this).addClass('row_selected');
//    });

//    /* Add a click handler for the delete row */
//    $('#delete').click(function() {
//        var anSelected = fnGetSelected(oTable);
//        oTable.fnDeleteRow(anSelected[0]);
//    });    

//    /* Init the table */
//    var oTable = $('#example').dataTable({ "bPaginate": false, "bLengthChange": false, "bInfo": false, "bAutoWidth": false, "bSort": false });

//    //Calculando heights
//    var popupHeight = selectHeight >= $(window).height() ? $(window).height() - 45 : selectHeight + 45;
//    var tableHeight = selectHeight > popupHeight - 70 ? popupHeight - 70 : selectHeight;

//    //Abriendo el dialgo modal
//    $("#example_wrapper").height(tableHeight);
//    if (selectHeight > $(window).height() - 70) {
//        tableHeight = $(window).height() - 70;
//        $("#exampleContainer").height(tableHeight);
//    }
//    $("#dynamic_filter").dialog({ autoOpen: false, modal: true, position: 'center', closeOnEscape: true, draggable: true, resizable: false, open: function(event, ui) { $(".ui-dialog-titlebar-close").hide(); }, height: popupHeight, width: 600, position: 'top' });
//    $("#dynamic_filter").dialog("open");

//    setTimeout("parent.hideLoading();", 500);
//}
//function Cancel() {
//    $("#dynamic_filter").dialog("close");
//}
//function Accept() {
//    var rows = $('#example tbody tr');

//    var value = "";
//    for (var i = 0; i < rows.length; i++) {
//        if ($(rows[i]).hasClass('row_selected')) {
//            value = rows[i];
//            value = $(value).find("td")[0].innerHTML;
//        }
//    }
//    if (value == "") {
//        alert("Por favor seleccione una fila");
//    }
//    else {
//        $('#' + $('#dynamic_filter').data("indexid") + ' option').each(
//                     function() {
//                         if ($(this).val().toString().trim() == value.toString().trim()) {
//                             $(this).attr("selected", "selected");
//                             $(this).parent().change();
//                             $(this).parent().valid();
//                         }
//                     });
//        Cancel();
//    }
//}



///*
//* I don't actually use this here, but it is provided as it might be useful and demonstrates
//* getting the TR nodes from DataTables
//*/
//function fnGetSelected(oTableLocal) {
//    var aReturn = new Array();
//    var aTrs = oTableLocal.fnGetNodes();

//    for (var i = 0; i < aTrs.length; i++) {
//        if ($(aTrs[i]).hasClass('row_selected')) {
//            aReturn.push(aTrs[i]);
//        }
//    }
//    return aReturn;
//}

//var selectHeight;

//function LoadTable3(cbox, codecolumn) {
//    selectHeight = 0;
//    var t = "";
//    var select = $("#" + cbox + " option");    
//    t = '<div id="exampleContainer" style="overflow:auto;"><table cellpadding="0" cellspacing="0" border="0" class="display" id="example" style="height:20px;">';

//    //Cabezera
//    t += "<thead>";
//    t += "<tr>";
//    if (codecolumn)
//        t += "<th>Codigo</th>";
//    else
//        t += '<th style="display:none;">Codigo</th>';
//    t += "<th>Descripcion</th>";
//    t += "</tr>";
//    t += "</thead>";

//    //Cuerpo
//    t += "<tbody>";    
//    for (var i = 0; i < select.length; i++) {
//        t += '<tr class="gradeA">';
//        if (codecolumn) {
//            t += "<td>";
//        } else {
//            selectHeight = selectHeight + 20;
//            t += '<td style="display:none;">';
//            t += select[i].value;
//            t += "</td>";
//            t += "<td>";
//            t += select[i].text;
//            t += "</td>";
//            t += "</tr>";
//        }
//    }
//    t += "</tbody>";

//    //Final
//    t += "<tfoot>";
//    t += "<tr>";
//    t += "</tr>";
//    t += "</tfoot>";
//    t += "</table></div>";

//    return t;
//}

//// 07/08/2012 Javier Se modifica funcion para que tambien acepte una url.
//function getParameterByName(name,url) {
//    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
//    var regexS = "[\\?&]" + name + "=([^&#]*)";
//    var regex = new RegExp(regexS);
//    var results;

//    if(url != undefined)
//        results = regex.exec(url);
//    else
//        results = regex.exec(window.location.search);

//    if (results == null)
//        return "";
//    else
//        return decodeURIComponent(results[1].replace(/\+/g, " "));
//}

//function CheckIfOpenTask() {
//    var taskid = getParameterByName("taskid");
//    var docid = getParameterByName("docid");
//    var doctypeid = getParameterByName("doctype");
//    var docname = getParameterByName("docname");
//    var url = "../WF/TaskSelector.ashx?TaskId=" + taskid + "&DocId=" + docid + "&DocTypeId=" + doctypeid;
//    var userID = document.getElementById('ctl00_hdnUserID').value;

//    if (!taskid || taskid == "") taskid = 0;
//    if (!docid || docid == "") docid = 0;

//    if(taskid != 0 || docid != 0)
//        OpenDocTask2(taskid, docid, false, docname, url, userID);
//}


////-----------------------------------------------------------------------------------------------------//
////Variables para abir documento
//var urlToOpen = '';
//var docNameToOpen = '';

//function OpenDocTask2(taskID, docID, asDoc, docName, url, userID) {
//    ShowLoadingTaskDialog();
//    ScriptWebServices.TasksService.set_defaultSucceededCallback(GetIDSuccess);
//    ScriptWebServices.TasksService.set_defaultFailedCallback(GetIDError);

//    //Completo las variables para abrir doc
//    urlToOpen = url;
//    docNameToOpen = docName;
//    
//    ScriptWebServices.TasksService.GetTabID(taskID, docID, asDoc, userID);
//}

////Nuevo metodo para apertura de tareas en pestña, en base al id devuelto del WS
//function GetIDSuccess(result) {
//    var tabID = result;

//    if (document.getElementById("DummyTab")) {
//        $('#MainTabber').tabs("remove", "#DummyTab");
//        $("#DummyTab").remove();
//    }

//    $('#MainTabber').tabs("select", '#tabtasks');
//    if (document.getElementById(tabID) == null) {
//        var divtag = '<div id="' + tabID + '" style="padding:0px;height:409px"><iframe id="IF" width="100%" height="100%" frameborder="0" src="' + urlToOpen + '"/><div/>';
//        $(divtag).appendTo('#TasksDiv');
//        $('#TasksDiv').tabs("add", '#' + tabID, docNameToOpen);
//        parent.ShowLoadingTaskDialog();
//    }
//    else {
//        hideLoading();
//    }

//    $('#TasksDiv').tabs('select', '#' + tabID);
//    $('#MainTabber').tabs("select", '#tabtasks');

//    urlToOpen = '';
//    docNameToOpen = '';
//}

////Error en WS
//function GetIDError(result) {
//    alert("Error al abrir documento: " + result.get_message());
//}


////-----------------------------------------------------------------------------------------------------
////
////  Bloque utilizado para agregar funcionalidad a javascript y jQuery de forma nativa
////  Nota: Evaluar si conviene mover estas funcionalidades "nativas" a un js nuevo.
////
////-----------------------------------------------------------------------------------------------------
//(function(){
//    jQuery.fn.hasClass = function (a)
//    {
//        var Aa=/[\n\t]/g,ca=/\s+/,Za=/\r/g,$a=/href|src|style/;
//        a=" "+a+" ";
//	    for(var b=0,d=this.length;b<d;b++)
//            if((" "+this[b].className+" ").toLowerCase().replace(Aa," ").indexOf(a.toLowerCase())>-1)
//	            return true;
//	    return false
//    }

//    if (typeof String.prototype.startsWith != 'function') {
//      String.prototype.startsWith = function (str){
//        return this.slice(0, str.length) == str;
//      };
//    }

//    if (typeof String.prototype.endsWith != 'function') {
//      String.prototype.endsWith = function (str){
//        return this.slice(-str.length) == str;
//      };
//    }

//    if (typeof String.prototype.contains != 'function') {
//      String.prototype.contains = function (str){
//        return this.indexOf(str) > -1;
//      };
//    }
//})();