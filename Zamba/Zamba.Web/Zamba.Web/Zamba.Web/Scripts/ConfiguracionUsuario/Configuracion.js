// -- Area de panel de usuario --//
var accd = document.getElementsByClassName("accordionConfig");
var i;

for (i = 0; i < accd.length; i++) {
    accd[i].addEventListener("click", function () {
        this.classList.toggle("active");
        var panelConfig = this.nextElementSibling;
        if (panelConfig.style.display === "block") {
            panelConfig.style.display = "none";
        } else {
            panelConfig.style.display = "block";
        }
    });
}
function ConfigurationModuleUser() {
    var data = window.localStorage.getItem('UV|' + GetUID());
    //alert(data );
    if (data == "true") {
        GetUsersPanel();
        GetGroupsPanel();
    } else {
        var userValue = $(".z-userName")[0].title;
        $('.UserSelected')[0].textContent = userValue;
        $('.UserSelected')[0].id = parseInt(GetUID());
        PanelShow();
        UserConfigload();
    }
}

function OpenModalUser() {
    $("#ModalConfig").modal();
    $('#ModalConfig').modal('handleUpdate');
    $('.UserSelected')[0].textContent = "";
    $('#PanelDeConfiguracion').hide();
}

$("#SearchUsersPanel").on("keyup", function () {
    var value = $(this).val().toLowerCase();
    $(".UsersPanel a").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
});

$("#SearchGoupsPanel").on("keyup", function () {
    var value = $(this).val().toLowerCase();
    $(".GroupsPanel a").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
});

function GetGroupsPanel() {
    $(".GroupsPanel").empty();
    var userActivo = GetUID();
    var eia = window.localStorage.getItem('TaskFilterConfig-' + userActivo);
    var eia = JSON.parse(eia);
    var stepId = eia.UserId;

    var data;
    if (window.localStorage) {
        var localUserGroupsByStepId = window.localStorage.getItem('localUserGroupsByStepId' + stepId);
        if (localUserGroupsByStepId != undefined && localUserGroupsByStepId != null && localUserGroupsByStepId != "null") {
            try {
                data = JSON.parse(localUserGroupsByStepId);

            } catch (e) {
                console.error(e);
                data = LoadlocalUserGroupsByStepIdFromDB1(stepId);
            }
        }
        else {
            data = LoadlocalUserGroupsByStepIdFromDB1(stepId);
        }
    }
    else {
        data = LoadlocalUserGroupsByStepIdFromDB1(stepId);
    }
    if (data != undefined && data != null) {

        var cantidadDatos = data.length;
        for (var i = 0; i < cantidadDatos; i++) {
            if (data[i] !== null && data[i] !== undefined) {
                $(".GroupsPanel").append('<a onclick="selectUser(this)" href="#" data-userId="' + data[i]._id + '" class="list-group-item GruposDeUsuarios"> ' + data[i]._name + '</a>')
            }
        }
    }
}

function GetUsersPanel() {
    $(".Users").empty();
    var userActivo = GetUID();
    var eia = window.localStorage.getItem('TaskFilterConfig-' + userActivo);
    var eia = JSON.parse(eia);
    var stepId = eia.UserId;

    var data;
    if (window.localStorage) {
        var localUsersByStepId = window.localStorage.getItem('localUsersByStepId' + stepId);
        if (localUsersByStepId != undefined && localUsersByStepId != null && localUsersByStepId != "null") {
            try {
                data = JSON.parse(localUsersByStepId);

            } catch (e) {
                console.error(e);
                data = LoadlocalUsersByStepIdFromDB1(stepId);
            }
        }
        else {
            data = LoadlocalUsersByStepIdFromDB1(stepId);
        }
    }
    else {
        data = LoadlocalUsersByStepIdFromDB1(stepId);
    }
    var cantidadDatos = data.length;
    for (var i = 0; i < cantidadDatos; i++) {
        if (data[i] !== null && data[i] !== undefined) {
            $(".UsersPanel").append('<a onclick="selectUser(this)" href="#" data-userId="' + data[i]._id + '" class="list-group-item GruposDeUsuarios"> '
                + data[i]._name + '</a>')
        }
    }

}

function selectUser(_this) {

    if ($(_this).hasClass("selectedUserOrGroup")) {
        $(_this).removeClass("selectedUserOrGroup");
    }
    else {
        $(".selectedUserOrGroup").removeClass("selectedUserOrGroup");
        $(_this).addClass("selectedUserOrGroup");
    }

    var title = $(_this)[0].text;
    $('.UserSelected')[0].textContent = title;
    $('.UserSelected')[0].id = _this.dataset.userid;

    PanelShow();
    UserConfigload();
};

function ServiceUserConfig() {
    var response = null;
    var genericRequest = {
        UserId: $('.UserSelected')[0].id,
        Params: {}
    };
    try {
        $.ajax({
            type: "POST",
            url: ZambaWebRestApiURL + '/search/GetResultsByUserConfigModule',
            data: JSON.stringify(genericRequest),

            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    response = data;


                }
        });
        return response;

    } catch (e) {
        console.log(e.error);
    }

}
function UserConfigload() {
    var response = ServiceUserConfig();

    response = JSON.parse(response);
    // Texto
    if (response.MyTeamTasksText != undefined) {
        $("#TextTareasEquipo")[0].value = response.MyTeamTasksText;
    }
    if (response.MyTasksText != undefined) {
        $("#TextMisTareas")[0].value = response.MyTasksText;
    }
    if (response.AllTasksText != undefined) {
        $("#TextTodosLosCasos")[0].value = response.AllTasksText;
    }
    if (response.MyAllTeamTasksText != undefined) {
        $("#TextTodoElEquipo")[0].value = response.MyAllTeamTasksText;
    }

    //swits
    if (response.ShowMyTasks != undefined && response.ShowMyTasks == "true" || response.ShowMyTasks == "True") {

        $("#checkMisTareas").addClass("switchUserOn");
    }
    if (response.ShowAllTasks != undefined && response.ShowAllTasks == "true" || response.ShowAllTasks == "True") {
        $("#checkTodosLosCasos").addClass("switchUserOn");
    }
    if (response.ShowMyTeamTasks != undefined && response.ShowMyTeamTasks == "true" || response.ShowMyTeamTasks == "True") {
        $("#checkTareasEquipo").addClass("switchUserOn");
    }
    if (response.ShowMyAllTeamTasks != undefined && response.ShowMyAllTeamTasks == "true" || response.ShowMyAllTeamTasks == "True") {
        $("#checkTodosElEquipo").addClass("switchUserOn");
    }
    //combos
    if (response.IdsAllTasks != undefined) {
        
        for (var i = 0; i < response.IdsAllTasks.length; i++) {
            
            $('#multiple-checkboxes-TodosLosCasos').append('<option value=' + response.IdsAllTasks[i].Id + '>' + response.IdsAllTasks[i].Name + '</option>');

        }
        $('#multiple-checkboxes-TodosLosCasos').multiselect({ includeSelectAllOption: true, });
        for (var j = 0; j < response.IdsAllTasks.length; j++) {
            var a = $("#multiple-checkboxes-TodosLosCasos").next();
            if (response.IdsAllTasks[j].visible == "true" && a[0].childNodes[1].childNodes[j + 1].className != "active") {
                a[0].childNodes[1].childNodes[j + 1].firstElementChild.firstElementChild.firstElementChild.click();
            }
        }


    } else {
        for (var i = 0; i < response.ListEntities.length; i++) {
            $('#multiple-checkboxes-TodosLosCasos').append('<option value=' + response.ListEntities[i].Id + '>' + response.ListEntities[i].Name + '</option>');
        }
        $('#multiple-checkboxes-TodosLosCasos').multiselect({ includeSelectAllOption: true, });
    }

    if (response.MyAllTeamEntities != undefined) {
        for (var i = 0; i < response.MyAllTeamEntities.length; i++) {
            $('#multiple-checkboxes-TodoElEquipo').append('<option value=' + response.MyAllTeamEntities[i].Id + '>' + response.MyAllTeamEntities[i].Name + '</option>');
        }
        $('#multiple-checkboxes-TodoElEquipo').multiselect({ includeSelectAllOption: true, });
        for (var j = 0; j < response.MyAllTeamEntities.length; j++) {
            var a = $("#multiple-checkboxes-TodoElEquipo").next();
            if (response.MyAllTeamEntities[j].visible == "true" && a[0].childNodes[1].childNodes[j + 1].className != "active") {
                a[0].childNodes[1].childNodes[j + 1].firstElementChild.firstElementChild.click();
            }
        }

    } else {
        for (var i = 0; i < response.ListEntities.length; i++) {
            $('#multiple-checkboxes-TodoElEquipo').append('<option value=' + response.ListEntities[i].Id + '>' + response.ListEntities[i].Name + '</option>');
        }
        $('#multiple-checkboxes-TodoElEquipo').multiselect({ includeSelectAllOption: true, });
    }

    if (response.MyTasksEntities != undefined) {
        for (var i = 0; i < response.MyTasksEntities.length; i++) {
            $('#multiple-checkboxes-MisTareas').append('<option value=' + response.MyTasksEntities[i].Id + '>' + response.MyTasksEntities[i].Name + '</option>');

        }
        $('#multiple-checkboxes-MisTareas').multiselect({ includeSelectAllOption: true, });
        for (var j = 0; j < response.MyTasksEntities.length; j++) {
            var a = $("#multiple-checkboxes-MisTareas").next();
            if (response.MyTasksEntities[j].visible == "true" && a[0].childNodes[1].childNodes[j + 1].className != "active") {
                a[0].childNodes[1].childNodes[j + 1].firstElementChild.firstElementChild.click();
            }
        }

    } else {
        for (var i = 0; i < response.ListEntities.length; i++) {
            $('#multiple-checkboxes-MisTareas').append('<option value=' + response.ListEntities[i].Id + '>' + response.ListEntities[i].Name + '</option>');
        }
        $('#multiple-checkboxes-MisTareas').multiselect({ includeSelectAllOption: true, });
    }

    if (response.MyTeamEntities != undefined) {
        for (var i = 0; i < response.MyTeamEntities.length; i++) {
            $('#multiple-checkboxes-TextTareasEquipo').append('<option value=' + response.MyTeamEntities[i].Id + '>' + response.MyTeamEntities[i].Name + '</option>');
        }
        $('#multiple-checkboxes-TextTareasEquipo').multiselect({ includeSelectAllOption: true, });
        for (var j = 0; j < response.MyTeamEntities.length; j++) {
            var a = $("#multiple-checkboxes-TextTareasEquipo").next();
            if (response.MyTeamEntities[j].visible == "true" && a[0].childNodes[1].childNodes[j + 1].className != "active") {             
                a[0].childNodes[1].childNodes[j + 1].firstElementChild.firstElementChild.click();
            }
        }

    } else {
        for (var i = 0; i < response.ListEntities.length; i++) {
            $('#multiple-checkboxes-TextTareasEquipo').append('<option value=' + response.ListEntities[i].Id + '>' + response.ListEntities[i].Name + '</option>');
        }

        $('#multiple-checkboxes-TextTareasEquipo').multiselect({ includeSelectAllOption: true, });
    }
}

function PanelShow() {
    $('#PanelDeConfiguracion').show();
}
        // Fin del area

function LoadUserConfigModul() {
    var arrayList = [];
    var arrayinsert

    //----- TODOS LOS CASOS -----//

    //CHECK
    if ($("#checkTodosLosCasos")[0].className.includes("switchUserOn")) {
        arrayinsert = { key: "ShowAllTasks", value: true };
        arrayList.push(arrayinsert);
    } else {
        arrayinsert = { key: "ShowAllTasks", value: false };
        arrayList.push(arrayinsert);
    }

    //TEXTO
    AllTextInsert = $("#TextTodosLosCasos")[0].value;
    arrayinsert = { key: "AllTasksText", value: AllTextInsert };
    arrayList.push(arrayinsert);


    //COMBO
    var cbxTodosLosasos = $("#multiple-checkboxes-TodosLosCasos").next();
    var cbxTodosLosasosArray = [];
    for (var j = 0; j < cbxTodosLosasos[0].childNodes[1].childNodes.length; j++) {
        if ($(cbxTodosLosasos[0].childNodes[1].childNodes[j])[0].className == "active") {
            cbxTodosLosasosArray.push(cbxTodosLosasos[0].childNodes[1].childNodes[j].lastElementChild.lastElementChild.lastElementChild.value);
        }
    }
    arrayList.push({ key: "IdsAllTasks", value: cbxTodosLosasosArray });


    //----- TODOS EL EQUIPO -----//

    //CHECK
    if ($("#checkTodosElEquipo")[0].className.includes("switchUserOn")) {
        arrayinsert = { key: "ShowMyAllTeamTasks", value: true };
        arrayList.push(arrayinsert);
    } else {
        arrayinsert = { key: "ShowMyAllTeamTasks", value: false };
        arrayList.push(arrayinsert);
    }

    //TEXTO
    AllTextInsert = $("#TextTodoElEquipo")[0].value;
    arrayinsert = { key: "MyAllTeamTasksText", value: AllTextInsert };
    arrayList.push(arrayinsert);


    //COMBO
    //------ NOTA: ESTA CODIGO SE MANTIENE  COMENTADO MIENTRAS NO EXISTAN VARIABLES INDEPENDIENTES PARA LOS COMBOBOX ---------///

    var cbxTodoElEquipo = $("#multiple-checkboxes-TodoElEquipo").next();
    var cbxTodoElEquipoArray = [];
    for (var j = 0; j < cbxTodoElEquipo[0].childNodes[1].childNodes.length; j++) {
        if ($(cbxTodoElEquipo[0].childNodes[1].childNodes[j])[0].className == "active") {
            cbxTodoElEquipoArray.push(cbxTodoElEquipo[0].childNodes[1].childNodes[j].lastElementChild.lastElementChild.lastElementChild.value);
        }
    }
    arrayList.push({ key: "MyAllTeamEntities", value: cbxTodoElEquipoArray });

    //----- MIS TAREAS -----//

    //CHECK
    if ($("#checkMisTareas")[0].className.includes("switchUserOn")) {
        arrayinsert = { key: "ShowMyTasks", value: true };
        arrayList.push(arrayinsert);
    } else {
        arrayinsert = { key: "ShowMyTasks", value: false };
        arrayList.push(arrayinsert);
    }

    //TEXTO
    AllTextInsert = $("#TextMisTareas")[0].value;
    arrayinsert = { key: "MyTasksText", value: AllTextInsert };
    arrayList.push(arrayinsert);


    //COMBO

    var cbxTodosLosasos = $("#multiple-checkboxes-MisTareas").next();
    var cbxTodosLosasosArray = [];
    for (var j = 0; j < cbxTodosLosasos[0].childNodes[1].childNodes.length; j++) {
        if ($(cbxTodosLosasos[0].childNodes[1].childNodes[j])[0].className == "active") {
            cbxTodosLosasosArray.push(cbxTodosLosasos[0].childNodes[1].childNodes[j].lastElementChild.lastElementChild.lastElementChild.value);
        }
    }
    arrayList.push({ key: "MyTasksEntities", value: cbxTodosLosasosArray });

    //----- TAREA DEL EQUIPO -----//

    //CHECK
    if ($("#checkTareasEquipo")[0].className.includes("switchUserOn")) {
        arrayinsert = { key: "ShowMyTeamTasks", value: true };
        arrayList.push(arrayinsert);
    } else {
        arrayinsert = { key: "ShowMyTeamTasks", value: false };
        arrayList.push(arrayinsert);
    }

    //TEXTO
    AllTextInsert = $("#TextTareasEquipo")[0].value;
    arrayinsert = { key: "MyTeamTasksText", value: AllTextInsert };
    arrayList.push(arrayinsert);

    ServiceModuleInsert(arrayList);
    //COMBO
    ////------ NOTA: ESTA CODIGO SE MANTIENE  COMENTADO MIENTRAS NO EXISTAN VARIABLES INDEPENDIENTES PARA LOS COMBOBOX ---------///

    var cbxTareaEquipo = $("#multiple-checkboxes-TextTareasEquipo").next();
    var cbxTareaEquipoArray = [];
    for (var j = 0; j < cbxTareaEquipo[0].childNodes[1].childNodes.length; j++) {
        if ($(cbxTareaEquipo[0].childNodes[1].childNodes[j])[0].className == "active") {
            cbxTodosLosasosArray.push(cbxTareaEquipo[0].childNodes[1].childNodes[j].lastElementChild.lastElementChild.lastElementChild.value);
        }
    }
    arrayList.push({ key: "MyTeamEntities", value: cbxTareaEquipoArray });

    clearAllCache(false);
}


function ServiceModuleInsert(arrayList) {
    var response = null;
    var results = JSON.stringify(arrayList);
    var genericRequest = {
        UserId: $('.UserSelected')[0].id,
        Params:
        {
            "arrayList": results
        }
    };
    try {
        $.ajax({
            type: "POST",
            url: ZambaWebRestApiURL + '/search/GetInsertByUserConfigModule',
            data: JSON.stringify(genericRequest),

            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    response = data;
                    response = JSON.parse(response);
                    swal("", "Cambios guardados con exito", "success");
                }
        });
        return response;

    } catch (e) {
        console.log(e.error);
    }

}