
function AccionesAction() {
    if ($('#NavHeader').attr('class') == 'CollapseHeader') {
        $('#NavHeader').removeAttr('class');
        $('#NavHeader').addClass('CollapseHeaderBlock');
        $('#IconAction').removeAttr('class');
        $('#IconAction').addClass('glyphicon glyphicon-menu-up');
        $($("#navbarSupportedContent ul")[0]).css('padding-top', '35px')

    } else {
        if ($('#NavHeader').attr('class') == 'CollapseHeaderBlock') {
            $('#NavHeader').removeAttr('class');
            $('#NavHeader').addClass('CollapseHeader');
            $('#IconAction').removeAttr('class');
            $('#IconAction').addClass('glyphicon glyphicon-menu-down');
            $($("#navbarSupportedContent ul")[0]).css('padding-top', '10px')
        }
    }

}

//variable creada para usar de bandera y no cargar deteminados objetos
var isClosingTask = false;

function CollapseMenu() {
    var comp = $("#bs-TH-navbar-collapse-3").hasClass("in");

    if (comp == true) {
        $("#bs-TH-navbar-collapse-3").removeClass("in");
        $(".container-fluid").css("margin-top", "");
    } else {
        $(".container-fluid").css("margin-top", "100px");
        $("#bs-TH-navbar-collapse-3").addClass("in")
    }
}

$(document).ready(function () {

    $("#SearchUsers").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $(".Users a").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#SearchGoups").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $(".Groups a").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $('#BtnIniciar').click(function (e) {
        ShowLoadingAnimation();
    });

    $("#idtasklogo").hide();

    try {
        $("li.navbar-text").find("span")[1].title = $("li.navbar-text").find("span")[1].innerHTML;
    } catch (e) {
        console.error(e);
    }

    if ($("#bs-TH-navbar-collapse-1").height() > 0) {
        if ($("#bs-TH-navbar-collapse-1").height() >= 24 && $("#bs-TH-navbar-collapse-1").height() <= 47) {
            $("#navbarSupportedContent").css("margin-top", "30px");
            $('#Toolbar').removeClass('navbar-default');
        }
        if ($("#bs-TH-navbar-collapse-1").height() >= 48 && $("#bs-TH-navbar-collapse-1").height() <= 71) {
            $("#navbarSupportedContent").css("margin-top", "55px");
            $('#Toolbar').removeClass('navbar-default');
        }

        if ($("#bs-TH-navbar-collapse-1").height() >= 72) {
            $("#navbarSupportedContent").css("margin-top", "80px");
            $("#divPrincipalMasterBlank").css("margin-top", "38px");
            $('#Toolbar').removeClass('navbar-default');
        }

    } else {
        $('#Toolbar').addClass('navbar-default');
    }

    try {
        GetUsers();
        GetGroups();
    } catch (e) {
        console.error(e);
    }

});


$(document).ready(function () {
    var userId = GetUID();//$("[id$=HiddenCurrentUserID]").val();
    var taskRes = initializePage(userId);
    var groupList = GetGroupsByUserIds();


    if (taskRes != null) {
        if (taskRes._asignedToId > 0) {
            $("#ctl00_ContentPlaceHolder_ucTaskHeader_lblAsignedTo").html(GetUserorGroupNamebyId(taskRes._asignedToId).replace("Zamba_", "").replace("Zamba", "").trim());
        } else {
            $("#ctl00_ContentPlaceHolder_ucTaskHeader_lblAsignedTo").html("Ninguno");
        }

        if (taskRes._asignedToId == userId || groupList.includes(taskRes._asignedToId)) {
        } else {
        }


    } else {
        $("#ctl00_ContentPlaceHolder_ucTaskHeader_lblAsignedTo").html("Ninguno");
    }


    var buttonsQuantity = $('#ctl00_ContentPlaceHolder_ucTaskHeader_UACCell').children().length;

    if (buttonsQuantity == 0) {
        $('#liAcciones').css('display', 'none');
    }
    else {
        $('#liAcciones').css('display', 'block');
    }

    if (taskRes != null) {
        // La tarea no esta en ejecución.
        if (taskRes._taskState == 0 || taskRes._taskState == undefined || taskRes._taskState == null) {
            //$("#dropOptions").css("display", "none");
            $("#liFinalizar").css("display", "none");
            $("#liIniciar").css("display", "block");

        } // La tarea esta asignada, el usuario es distinto al asignado en la tarea y el asignado no es ninguno.
        else if (taskRes._taskState == 1 && taskRes._asignedToId != userId && taskRes._asignedToId != 0 && !groupList.includes(taskRes._asignedToId)) {
            //si tiene permiso de desasignar
            if (GetUsersRights(taskRes._stepId, 37) == true) {
                $("#liIniciar").css("display", "block");
            } else {
                $("#liIniciar").css("display", "none");
            }
            $('#liAcciones').css('display', 'none');
        }
        // La tarea esta en ejecución, el usuario es distinto al asignado en la tarea y el asignado no es ninguno.

        else if (taskRes._taskState == 2 && taskRes._asignedToId != userId && taskRes._asignedToId != 0 && !groupList.includes(taskRes._asignedToId)) {

            $("#liFinalizar").css("display", "none");
            $('#liAcciones').css('display', 'none');

        } // La tarea esta asignada a un grupo del usuario
        else if (taskRes._taskState == 1 && taskRes._asignedToId != userId && taskRes._asignedToId != 0 && groupList.includes(taskRes._asignedToId)) {
            $("#liIniciar").css("display", "block");
            $("#liAsig").css("display", "block");
            $("#liFinalizar").css("display", "none");
            $('#liAcciones').css('display', 'block');

        } // (para correjir inconsistencia en la DB), tarea en estado Asignada, pero el usuario asignado no es ninguno
        else if (taskRes._taskState == 1 && taskRes._asignedToId != userId && taskRes._asignedToId == 0 && groupList.includes(taskRes._asignedToId)) {
            $("#liIniciar").css("display", "block");
            $("#liAsig").css("display", "block");

            $("#liFinalizar").css("display", "none");
            $('#liAcciones').css('display', 'none');

        }// La tarea esta en ejecución por el current User
        else {

            $("#liIniciar").css("display", "none");
            $("#liAsig").css("display", "block");

            //terminar tarea
            if (GetUsersRights(taskRes._stepId, 38) == true) {
                $("#liFinalizar").css("display", "block");
            } else {
                $("#liFinalizar").css("display", "none");
            }


            //borrar tarea
            if (GetUsersRights(taskRes._stepId, 4) == true) {
                $("#liRemove").css("display", "block");
            } else {
                $("#liRemove").css("display", "none");
            }
            $('#liAcciones').css('display', 'block');
        }
    }


    $('#tbDoc').children(2).children(2).parent(2).css('height', 600);

    var NombreUsuarioZamba = $("#ctl00_ContentPlaceHolder_ucTaskHeader_lblAsignedTo")[0].innerText;
    NombreUsuarioZamba = NombreUsuarioreplace("Zamba_", "").trim();
    NombreUsuarioZamba = NombreUsuarioZamba.replace("Zamba", "").trim();
    $("#ctl00_ContentPlaceHolder_ucTaskHeader_lblAsignedTo")[0].innerText = NombreUsuarioZamba;

    //habilitar el derivar
    if (taskRes != null) {
        if (GetUsersRights(taskRes._stepId, 29) == true) {
            $("#ctl00_ContentPlaceHolder_ucTaskHeader_BtnDerivar").css("display", "block");
        } else {
            $("#ctl00_ContentPlaceHolder_ucTaskHeader_BtnDerivar").css("display", "none");
        }
    }
});

$(function () {
    if ($("#dialog").length > 0) {
        $("#dialog").dialog({
            autoOpen: false,
            modal: true,
            buttons:
            {
                "Yes": function () {
                    $(this).dialog("close");
                    eval($("#<%= hdnBtnPostback.ClientID %>").val());
                },
                "No": function () {
                    $(this).dialog("close");
                },
                "Maybe": function () {
                    $(this).dialog("close");
                    //what should we do when "Maybe" is clicked? 
                }
            }
        });
    }

    AddCalendar('ctl00_ContentPlaceHolder_ucTaskHeader_vto_calendar');

    $("#btnoptions").click(function (event) {
        event.preventDefault();
        $("#paneloptions").slideToggle();
    });

    $("#btnoptions a").click(function (event) {
        event.preventDefault();
        $("#paneloptions").slideUp();
    });

    $("#wf_main_toolbar_td_last").append($("#Toolbar-div"));
    $("#docToolbarUl").append($("#wf_main_toolbar"));


    if ($("#<%= BtnRemove%>").length == 0)
        $("#liRemove").css('display', 'none');


});

function OpenModalActions() {
    var div = document.getElementById("ctl00_ContentPlaceHolder_ucTaskHeader_UACCell");

    div.childNodes.forEach(function (item_li) {
        var input = item_li.firstChild;

        if (input.getAttribute("style") == "display: none;") {
            item_li.setAttribute("style", "display: none;")
        }
    })

    $("#ModalActions").modal();
}

function CloseModalActions() {
    $("#ModalActions").hide();
}





function GetUserorGroupNamebyId(asignedToId) {
    var UserorGroupName = null;
    $.ajax({
        type: "POST",
        url: location.origin.trim() + getRestApiUrl() + '/api/Tasks/GetUserorGroupNamebyId?' + jQuery.param({ asginedToId: asignedToId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            UserorGroupName = response;
        },
        error: function (response) {
        }
    });
    return UserorGroupName.replace(/_/g, ' ');
}

function getStepNameById(stepId) {
    var stepName = null;
    $.ajax({
        type: "POST",
        url: location.origin.trim() + getRestApiUrl() + '/api/Tasks/getStepNameById?' + jQuery.param({ stepid: stepId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            stepName = response;
        },
        error: function (response) {
        }
    });
    return stepName;
}


function showAssignModal() {
    $(document).ready(function () {



        $("#ContentPlaceHolder_ucTaskHeader_ModalAssign").dialog({
            autoOpen: true, modal: true, width: 400, height: 300
        });
        FixAndPosition($("#ContentPlaceHolder_ucTaskHeader_ModalAssign").parent());
        $("#ContentPlaceHolder_ucTaskHeader_ModalAssign").dialog().parent().appendTo($("form:first"));
    });
}

function showjQueryDialog() {
    $("#dialog").dialog("open");
}

function GetGroups() {
    try {

        $(".Groups").empty();
        var stepId = parseInt($("[id$=HiddenCurrentUserID]").val());

        var data = [];
        if (window.localStorage) {
            var localUserGroupsByStepId = window.localStorage.getItem('localUserGroupsByStepId' + stepId);
            if (localUserGroupsByStepId != undefined && localUserGroupsByStepId != null && localUserGroupsByStepId != "null") {
                try {
                    data = JSON.parse(localUserGroupsByStepId);

                } catch (e) {
                    console.error(e);
                    data = LoadlocalUserGroupsByStepIdFromDB(stepId);
                }
            }
            else {
                data = LoadlocalUserGroupsByStepIdFromDB(stepId);
            }
        }
        else {
            data = LoadlocalUserGroupsByStepIdFromDB(stepId);
        }

        if (data != undefined) {
            for (var i = 0; i < data.length; i++) {
                var datauserId = data[i];
                if (datauserId != null && datauserId != undefined) {
                    $(".Groups").append('<a onclick="selectUserItem(this)" href="#" data-userId="' + datauserId._id + '" class="list-group-item GruposUsuarios"> ' + datauserId._name + '</a>')
                }
            }
        }
    } catch (e) {
        console.error(e);
    }
}


function LoadlocalUserGroupsByStepIdFromDB(stepId) {
    try {

        if (stepId > 0) {
            $.ajax({
                type: "POST",
                url: location.origin.trim() + getRestApiUrl() + '/api/Tasks/GetGroups?stepId=' + stepId,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (window.localStorage) {
                        try {

                            window.localStorage.setItem('localUserGroupsByStepId' + stepId, JSON.stringify(data));
                        } catch (e) {
                            console.error(e);
                        }
                        if (data != undefined) {
                            for (var i = 0; i < data.length; i++) {
                                if (data[i] !== null && data[i] !== undefined && !data[i]._name.startsWith("Rol_")) {
                                    $(".Groups").append('<a onclick="selectUserItem(this)" href="#" data-userId="' + data[i]._id + '" class="list-group-item GruposUsuarios"> ' + data[i]._name + '</a>')
                                }
                            }
                        }
                    }
                    return data;
                }
            });
        }
    } catch (e) {
        console.error(e);
    }
};


function GetGroupsByUserIds() {
    var groups = [];
    try {
        if (window.localStorage) {
            var localGroups = window.localStorage.getItem("localGroups" + GetUID());
            if (localGroups != undefined && localGroups != null && localGroups.length > 0) {
                groups = localGroups;
                return groups;

            }
        }
    } catch (e) {
        console.error(e);
    }
    var userId = GetUID();//parseInt($("[id$=HiddenCurrentUserID]").val());
    var groups = [];
    $.ajax({
        type: 'GET',
        async: false,
        url: location.origin.trim() + getRestApiUrl() + '/api/Tasks/GetGroupsByUserIds',
        data: { usrID: userId },
        success: function (data) {
            groups = data;
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
    return groups;
}

function GetUsers() {
    try {

        $(".Users").empty();
        var stepId = parseInt($("[id$=HiddenCurrentUserID]").val());

        var data;
        if (window.localStorage) {
            var localUsersByStepId = window.localStorage.getItem('localUsersByStepId' + stepId);
            if (localUsersByStepId != undefined && localUsersByStepId != null && localUsersByStepId != "null") {
                try {
                    data = JSON.parse(localUsersByStepId);

                } catch (e) {
                    console.error(e);
                    data = LoadlocalUsersByStepIdFromDB(stepId);
                }
            }
            else {
                data = LoadlocalUsersByStepIdFromDB(stepId);
            }
        }
        else {
            data = LoadlocalUsersByStepIdFromDB(stepId);
        }

        if (data != undefined) {
            for (var i = 0; i < data.length; i++) {
                if (data[i] != null && data[i] != undefined) {
                    $(".Users").append('<a onclick="selectUserItem(this)" href="#" data-userId="' + data[i]._id + '" class="list-group-item GruposUsuarios"> '
                        + data[i]._name + '</a>')
                }
            }
        }
    } catch (e) {
        console.error(e);
    }

}

function LoadlocalUsersByStepIdFromDB(stepId) {

    $.ajax({
        type: "POST",
        url: location.origin.trim() + getRestApiUrl() + '/api/Tasks/GetUsers?stepId=' + stepId,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != null) {
                if (window.localStorage) {
                    try {
                        window.localStorage.setItem('localUsersByStepId' + stepId, JSON.stringify(data));
                    } catch (e) {
                        console.error(e);
                    }
                    for (var i = 0; i < data.length; i++) {
                        if (data[i] !== null && data[i] !== undefined) {
                            $(".Users").append('<a onclick="selectUserItem(this)" href="#" data-userId="' + data[i]._id + '" class="list-group-item GruposUsuarios"> '
                                + data[i]._name + '</a>')
                        }
                    }
                    GetGroups();
                }
            }
            return data;
        }
    });
};


function selectUserItem(_this) {

    if ($(_this).hasClass("selectedUser")) {
        $(_this).removeClass("selectedUser");
    }
    else {
        $(".selectedUser").removeClass("selectedUser");
        $(_this).addClass("selectedUser");
    }

    var lblderivar = "Derivar a ";
    lblderivar += $(_this)[0].text;
    $('.modal-title.text-center')[0].textContent = lblderivar;
};

function OpenDeriveModal() {
    $("#ModalDerivar").modal();
}

function DeriveTask() {
    var UserId = GetUID();//$("[id$=HiddenCurrentUserID]").val();
    var TaskId = $("[id$=HiddenTaskId]").val();
    var userId = $(".selectedUser").attr("data-userId");

    var _isUser = false;
    if ($(".selectedUser").parent().hasClass("Users")) {
        _isUser = true;
    }
    var Url = window.location.href;
    Comments = $("#ModalDerivar").find("#deriveMessage").val();

    $("#ModalDerivar").find(".derive").attr("disabled", true);
    $("#ModalDerivar").find(".closeModal").attr("disabled", true);
    $(".loadersmall").css("display", "block");

    $.ajax({
        type: "POST",
        url: ZambaWebRestApiURL + '/Tasks/DeriveTask?' + jQuery.param({ taskId: TaskId, userIDToAsign: userId, currentUserID: UserId, isUser: _isUser, url: Url, comments: Comments }),
        contentType: "application/json; charset=utf-8;",
        async: false,
        processData: false,
        success: function (data) {
            console.log(data);
            $("#liDerivar").css("display", "none");
            location.reload();
            $('#ModalDerivar').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            toastr.success("La tarea se derivo correctamente");
            $("#liDerivar").css("display", "none");
            $("#ModalDerivar").find(".derive").removeAttr("disabled");
            $("#ModalDerivar").find(".closeModal").removeAttr("disabled");
            $(".loadersmall").css("display", "none");
            $(".selectedUser").removeClass("selectedUser");
            $('#lblDerivar')[0].textContent = "Derivar";
            $("#SearchUsers")[0].value = "";
            $("#SearchGoups")[0].value = "";
            $("#deriveMessage")[0].value = "";
            RefreshResultsGridFromChildWindow();

        },
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            console.log(err.Message);
            toastr.error("Error al derivar tarea");
            $('#ModalDerivar').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $("#ModalDerivar").find(".derive").removeAttr("disabled");
            $("#ModalDerivar").find(".closeModal").removeAttr("disabled");
            $(".loadersmall").css("display", "none");
            $(".selectedUser").removeClass("selectedUser");
            $('#lblDerivar')[0].textContent = "Derivar";
            $("#SearchUsers")[0].value = "";
            $("#SearchGoups")[0].value = "";
            $("#deriveMessage")[0].value = "";
        }
    });


}


function StartTask() {
    var TaskId = Number($("[id$=HiddenTaskId]").val());
    var userId = GetUID();//$("[id$=HiddenCurrentUserID]").val();


    $.ajax({
        type: "POST",
        async: false,
        url: location.origin.trim() + getRestApiUrl() + "/api/Tasks/StartTask?" + jQuery.param({ taskId: TaskId, userid: userId }),
        contentType: "application/json; charset=utf-8;",
        success: function (response) {
            if (response != null) {
                if (response._asignedToId != 0 || response._asignedToId != userId || response._taskState != 1) {
                    $("#ctl00_ContentPlaceHolder_ucTaskHeader_lblAsignedTo").html(GetUserorGroupNamebyId(response._asignedToId).replace("Zamba_", "").replace("Zamba", "").trim());
                    $("#ctl00_ContentPlaceHolder_ucTaskHeader_CboStates").removeAttr("disabled");
                    $("#ctl00_ContentPlaceHolder_ucTaskHeader_dtpFecVenc").removeAttr("disabled");
                    $("#ctl00_ContentPlaceHolder_ucTaskHeader_chkTakeTask").removeAttr("disabled");
                    $("#ctl00_ContentPlaceHolder_ucTaskHeader_chkCloseTaskAfterDistribute").removeAttr("disabled");
                    RefreshResultsGridFromChildWindow();

                }
            }
        },
        error: function (xhr, status, error) {
            console.log(xhr.responseText);
        }
    });
    location.reload();
}


function CloseTaskFromRule(taskId) {
    if (taskId != "") {

        //si la tarea se elimio directamente cierro  la ventana
        window.close();


        //ver si se utiliza la regla sin eliminar tarea

    }
    return;
}
function CloseTask(isFinalizar) {
    parent.$("#liTabRule").css("display", "none");
    parent.$("#rule").css("display", "none");
    parent.$("#tabRules").css("display", "block");
    parent.$("#tabRules").empty();

    var UserId = GetUID();// $("[id$=HiddenCurrentUserID]").val();
    var TaskId = Number($("[id$=HiddenTaskId]").val());

    if (isFinalizar != undefined && isFinalizar != null && isFinalizar == true) {
        takeTaksFix = true;
    } else {
        takeTaksFix = $("[id$=chkTakeTask]")[0].checked;

        var tksRes = initializePage(UserId);
        var userRights = GetUsersRights(tksRes._stepId, 71);

        if (tksRes._taskState == 2 && tksRes._asignedToId != UserId) {
            window.close();
            return;
        }
    }
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8;",
        url: location.origin.trim() + getRestApiUrl() + '/api/Tasks/EndTask?' + jQuery.param({ isTakeTask: takeTaksFix, taskId: TaskId, userId: GetUID() }),
        async: false,
        success: function (response) {
            if (response != null) {
                $("#liIniciar").css("display", "block");
                if ((response._taskState == 2)) {
                    if ((userRights == true)) {
                        $("#ctl00_ContentPlaceHolder_ucTaskHeader_CboStates").css("display", "block");
                    }
                    else {
                        $("#ctl00_ContentPlaceHolder_ucTaskHeader_CboStates").css("display", "none");
                    }
                    if (response._asignedToId == UserId) {
                        $("#ctl00_ContentPlaceHolder_ucTaskHeader_chkTakeTask").css("display", "block");
                    }
                    else {
                        $("#ctl00_ContentPlaceHolder_ucTaskHeader_chkTakeTask").css("display", "none");
                    }
                    $("#ctl00_ContentPlaceHolder_ucTaskHeader_chkCloseTaskAfterDistribute").css("display", "block");
                } else {
                    $("#ctl00_ContentPlaceHolder_ucTaskHeader_CboStates").css("display", "none");
                    $("#ctl00_ContentPlaceHolder_ucTaskHeader_dtpFecVenc").css("display", "none");
                    $("#ctl00_ContentPlaceHolder_ucTaskHeader_chkTakeTask").css("display", "none");
                    $("#ctl00_ContentPlaceHolder_ucTaskHeader_chkCloseTaskAfterDistribute").css("display", "none");
                }
                if (taskRes._asignedToId > 0 && takeTaksFix == true) {
                    $("#ctl00_ContentPlaceHolder_ucTaskHeader_lblAsignedTo").html("Ninguno");
                }
                RefreshResultsGridFromChildWindow();

            }
        },
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            console.log(err.Message);
        }

    });


    try {
        if (window.opener != undefined)
            window.opener.RefreshCurrentResults();
    } catch (e) {
        console.error(e);
    }
    window.close();
}
function CloseTaskWindow() {

    parent.$("#liTabRule").css("display", "none");
    parent.$("#rule").css("display", "none");
    parent.$("#tabRules").css("display", "block");
    parent.$("#tabRules").empty();

    var UserId = GetUID();// $("[id$=HiddenCurrentUserID]").val();
    var TaskId = Number($("[id$=HiddenTaskId]").val());


    takeTaksFix = $("[id$=chkTakeTask]")[0].checked;

    var tksRes = initializePage(UserId);
    var userRights = GetUsersRights(tksRes._stepId, 71);

    if (tksRes._taskState == 2 && tksRes._asignedToId != UserId) {
        window.close();
        return;
    }

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8;",
        url: location.origin.trim() + getRestApiUrl() + '/api/Tasks/EndTask?' + jQuery.param({ isTakeTask: takeTaksFix, taskId: TaskId, userId: GetUID() }),
        async: false,
        success: function (response) {
            if (response != undefined && response != null) {
                $("#liIniciar").css("display", "block");
                if ((response._taskState == 2)) {
                    if ((userRights == true)) {
                        $("#ctl00_ContentPlaceHolder_ucTaskHeader_CboStates").css("display", "block");
                    }
                    else {
                        $("#ctl00_ContentPlaceHolder_ucTaskHeader_CboStates").css("display", "none");
                    }
                    if (response._asignedToId == UserId) {
                        $("#ctl00_ContentPlaceHolder_ucTaskHeader_chkTakeTask").css("display", "block");
                    }
                    else {
                        $("#ctl00_ContentPlaceHolder_ucTaskHeader_chkTakeTask").css("display", "none");
                    }
                    $("#ctl00_ContentPlaceHolder_ucTaskHeader_chkCloseTaskAfterDistribute").css("display", "block");
                } else {
                    $("#ctl00_ContentPlaceHolder_ucTaskHeader_CboStates").css("display", "none");
                    $("#ctl00_ContentPlaceHolder_ucTaskHeader_dtpFecVenc").css("display", "none");
                    $("#ctl00_ContentPlaceHolder_ucTaskHeader_chkTakeTask").css("display", "none");
                    $("#ctl00_ContentPlaceHolder_ucTaskHeader_chkCloseTaskAfterDistribute").css("display", "none");
                }
                if (taskRes._asignedToId > 0 && takeTaksFix == true) {
                    $("#ctl00_ContentPlaceHolder_ucTaskHeader_lblAsignedTo").html("Ninguno");
                }

            }
        },
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            console.log(err.Message);
        }

    });


    try {
        if (window.opener != undefined)
            window.opener.RefreshCurrentResults();
    } catch (e) {
        console.error(e);
    }
    window.close();
}

function ChangeTaskStateIdToAssigned() { // metodo sincronico
    var TaskId = $("[id$=HiddenTaskId]").val();
    $.ajax({
        type: "POST",
        url: ZambaWebRestApiURL + '/Tasks/UpdateTaskState?taskId=' + TaskId + '&stateId=1',
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (data) {

        },
        error: function (error) {
            console.log(error);

        }
    });
}

function GetUsersRights(StepId, Right) {
    var permission = false;
    //        var docid = Number($("[id$=Hiddendocid]").val());
    var UserId = GetUID();//$("[id$=HiddenCurrentUserID]").val();
    $.ajax({
        type: "POST",
        url: location.origin.trim() + getRestApiUrl() + '/api/Tasks/GetUsersRights?' + jQuery.param({ stepId: StepId, right: Right, userid: UserId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            permission = response;
        },
        error: function (response) {
        }
    });
    return permission;
}

function initializePage(userid) {
    var TaskId = $("[id$=HiddenTaskId]").val();
    var taskResult = null;
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8;",
        url: location.origin.trim() + getRestApiUrl() + '/api/Tasks/OnLoadPage?' + jQuery.param({ taskId: TaskId, userid: userid }),
        async: false,
        success: function (response) {
            if (response != null) {
                taskResult = response;
                try {
                    var scope = angular.element("#taskController").scope();
                    scope.taskResult = taskResult;
                } catch (e) {
                    console.error(e);
                }
            }
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
    return taskResult;
}

function executeRule(e) {
    var target = e.target || e.srcElement;
    var tableId = target.closest("div.tablesorter").id;
    var ruleId = $("#" + tableId).attr("ruleid") || "0";
    var resultId = target.closest("tr").getElementsByClassName("rowDocId")[0].innerText;
    var genericRequest = {
        UserId: parseInt(GetUID()),
        Params: {
            "ruleId": ruleId,
            "resultId": resultId
        }
    };

    if (parseInt(ruleId) > 0) {
        $.ajax({
            "async": false,
            "url": location.origin.trim() + getRestApiUrl() + "/api/tasks/ExecuteTaskRule",
            "method": "POST",
            "headers": {
                "content-type": "application/json"
            },
            "success": function () {
                toastr.success('Se ha ejecutado la acción');
                target.closest("form").submit();
            },
            "error": function () {
                toastr.error('Error al ejecutar acción');
            },
            "data": JSON.stringify(genericRequest)
        });
    }
    else {
        toastr.warning('La grilla no posee acciones que ejecutar');
    }
}



function IraHome() {
    var UserId = GetUID();// $("[id$=HiddenCurrentUserID]").val();
    //   var url = location.origin.trim() + getThisDomain() + '/globalsearch/search/search.html?' +
    var url = thisDomain + '/globalsearch/search/search.html?' + jQuery.param({ User: UserId }) + '#Zamba/';
    window.location.replace(url);
    //        window.location.assign(url);

    //window.open(url, '_blank', 'location=no')

}



function getRestApiUrl() {
    return '<%=ConfigurationManager.AppSettings["RestApiUrl"] %>';
}

function getThisDomain() {
    return '<%=ConfigurationManager.AppSettings["ThisDomain"] %>';
}


