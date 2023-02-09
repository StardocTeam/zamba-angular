<%@ Control Language="C#" AutoEventWireup="true" Inherits="TaskHeader" EnableViewState="False" CodeBehind="TaskHeader.ascx.cs" %>
<%@ Register Src="~/Views/UC/Common/ZDeleteButton.ascx" TagName="ZDeleteButton" TagPrefix="ucz" %>


<nav id="THNavBar" class="navbar navbar-toggleable-sm bg-faded navbar-light navbar-fixed-top" role="navigation" style="z-index: 9999">


    <div class="navbar-header">
        <div class="MenSup">
            <button type="button" class="navbar-toggle pull-left btn-xs" data-toggle="collapse" data-target="#bs-TH-navbar-collapse-1">
                <span class="sr-only">Acciones</span>
                <span class="glyphicon glyphicon-flash"></span>
            </button>
            <button type="button" class="navbar-toggle pull-left btn-xs" onclick="CollapseMenu()">
                <span class="sr-only">Información de tarea</span>
                <span class="glyphicon glyphicon-align-justify"></span>
            </button>
        </div>
        <a id="logo" class="hidden-xs navbar-brand tasklogo" style="height: 37px;" href="#"></a>
    </div>

    <div class="collapse navbar-collapse navbar-ex1-collapse" id="bs-TH-navbar-collapse-3">

        <ul class="nav navbar-nav navbar-left navbar-task row">

            <%--Botones--%>

            <li id="lihome" style="display: block">
                <button id="BtnHome" type="button" class="btn btn-danger btn-xs liButtons" tooltip="Home" 
                    onclick="IraHome()" tabindex="-1" style="background-color: #337ab7; border-color: #2e6da4; width: auto;">
                    <i class="fa fa-home"><span class="hidden-xs" style="margin-left: 5px;"></span></i>
                </button>
            </li>

            <li id="liCerrar" style="display: none">
                <button type="button" id="BtnClose" runat="server" class="btn btn-danger btn-xs liButtons" tooltip="Cerrar"
                    onclick="CloseTaskWindow();" tabindex="-1" style="background-color: #337ab7; border-color: #2e6da4;">
                     <i  class="fa fa-sign-out"></i> <span class="hidden-xs">Cerrar</span>
                </button>
            </li>

            <li id="liFinalizar" style="display: none">
                <button type="button" id="BtnFinalizar" class="btn btn-warning btn-xs liButtons"
                    title="Finalizar" onclick="CloseTask(true); <%--IraHome(true);--%>" tabindex="-1" style="width: 150px">
                    Quitar Tarea
                </button>
            </li>

            <li id="liAcciones" style="display: none">
                <button type="button" id="BtnAcciones" class="btn btn-primary btn-xs liButtons"
                    title="" onclick="AccionesAction();" tabindex="-1" style="width: 95px">
                    Acciones <span id="IconAction" class="glyphicon glyphicon-menu-down"></span>
                </button>
            </li>

            <li id="liIniciar" style="display: none">
                <button type="button" id="BtnIniciar" class="btn btn-success btn-xs liButtons"
                    title="Iniciar" onclick="StartTask();" tabindex="-1"  style="width: 150px">
                    Agregar a Tareas</button>
            </li>
            <li id="liFavorite" >
                <button  ng-if="taskResult._IsFavorite == true" type="button" id="BtnRemoveFavorites" class="btn btn-info btn-xs liButtons"
                    title="Quitar de favoritos" data-ng-click="MarkAsFavorite()"  tabindex="-1" style="background-color: transparent">
                   <i class="fa fa-heart"></i> <span class="hidden-xs">Quitar de favoritos</span></button>
                <button ng-if="taskResult._IsFavorite == false" type="button" id="BtnAddFavorites" class="btn btn-info btn-xs liButtons"
                    title="Agregar a favoritos" data-ng-click="MarkAsFavorite()"  tabindex="-1" style="background-color: transparent">
                   <i  class="fa fa-heart-o"></i> <span class="hidden-xs">Agregar a favoritos</span> </button>
            </li>
            <li id="liRemove" style="display: none; margin-top: 8px">
                <button type="button" id="BtnRemove" runat="server" class="btn btn-warning btn-xs liButtons" tooltip="Quitar" visible="false" onclick="BtnRemove_Click()"
                    tabindex="-1" style="background-color: #337ab7; border-color: #2e6da4; display: none">
                    Quitar Tarea  <%--<i class="glyphicon glyphicon-alert"></i>--%>
                </button>
            </li>
            <li id="liDelete">
                <ucz:ZDeleteButton ID="deleteCtrl" runat="server" Visible="false" style="display: none" />
            </li>
            </ul>
        <ul class="nav navbar-nav navbar-left navbar-task row">

            <%--Etiquetas--%>

            <li class="hidden-xs liButtons" id="liStep" style="margin-top: 8px">
                <i runat="server" id="IconStep" class="icon-task-header ToolbarGlyphicons glyphicon glyphicon-tasks visible-xs hidden-md hidden-sm hidden-lg"></i>
                <span>
                    <label class="MarginIco hidden-xs ">Etapa:</label>
                    <span id="lbletapadata2" runat="server">Etapa</span>
                </span>
            </li>

            <li class="hidden-xs liButtons" id="liState" style="margin-top: 8px">
                <i runat="server" id="IconState" class="icon-task-header glyphicon ToolbarGlyphicons glyphicon-asterisk  visible-xs hidden-md hidden-sm hidden-lg"></i>
                <span >
                    <label class="MarginIco  ">Estado:</label>
                    <span id="CboStates" runat="server">Estado</span>

                    <%--                    <asp:DropDownList AutoPostBack="True" ID="CboStates" runat="server">
                    </asp:DropDownList>--%>
                </span>
            </li>

            <li class="hidden-xs liButtons" id="liAsig" style=" margin-top: 8px">
                <i runat="server" id="IconAsigned" class="icon-task-header glyphicon ToolbarGlyphicons glyphicon-user  visible-xs hidden-md hidden-sm hidden-lg"></i>
                <span>
                    <label class="MarginIco hidden-xs  ">Asignado:</label>
                     <i class="fa fa-user visible-xs"></i>
                    <span id="lblAsignedTo" runat="server">Usuario</span>
                </span>
            </li>
            <li id="liDerivar" style="padding-left: 10px" class="liButtons">
                <button type="button" id="BtnDerivar" runat="server" onclick=" GetUsers(); GetGroups(); OpenDeriveModal();" class="btn btn-xs btn-info liButtons" data-backdrop="static" data-keyboard="false">
                    <i class="fa fa-user"></i>Derivar
                </button>
            </li>
            <li class="liButtons" id="liFecVenc" runat="server"  style=" margin-top: 8px">
                <i runat="server" id="IconValidate" class="glyphicon ToolbarGlyphicons glyphicon-calendar  visible-xs hidden-md hidden-sm hidden-lg "></i>
                <span class="">
                    <label class="MarginIco  ">Vto:</label>
                    <span id="dtpFecVenc" runat="server"></span>
                    <%--                    <asp:TextBox AutoPostBack="True" ID="dtpFecVenc" runat="server" CssClass=" input-xs StyCalendar"></asp:TextBox>--%>
                </span>
            </li>

            <%--<li id="dropOptions" class="hidden-xs">--%>
            <li id="dropOptions" class="hidden-xs liButtons">
                <div class="dropdown">
                    <button class="btn btn-default dropdown-toggle btn-xs Marginbtn" type="button" id="dropTaskOptions" data-toggle="dropdown">
                        <span class="glyphicon glyphicon-menu-hamburger"></span><span class="caret"></span>
                    </button>
                    <ul class="dropdown-menu" role="menu" aria-labelledby="dropTaskOptions" id="mnuOptions" style=" background-color: slategray;">
                        <li role="presentation">
                            <asp:HiddenField runat="server" ID="hdnTakeTaskFix" />
                            <asp:CheckBox role="menuitem" AutoPostBack="True" OnLoad="chkTakeTask_Load" ID="chkTakeTask" runat="server"
                                Text="Finalizar al Cerrar"
                                Enabled="false" CssClass="checkOptions" Font-Bold="false" Font-Size="Smaller" /><!--OnLoad="chkTakeTask_Load"-->
                        </li>
                        <li role="presentation">
                            <asp:HiddenField runat="server" ID="hdnCloseTaskFix" />
                            <asp:CheckBox role="menuitem" AutoPostBack="True" OnLoad="chkCloseTaskAfterDistribute_Load" ID="chkCloseTaskAfterDistribute" runat="server"
                                Text="Cerrar tarea al cambiar de etapa"
                                Enabled="false" CssClass="checkOptions" Font-Bold="false" Font-Size="Smaller" /><!--OnLoad="chkCloseTaskAfterDistribute_Load"-->
                        </li>
                    </ul>
                </div>
            </li>
            <li class="liButtons ">
                <label id="lblmsj" class="taskMessage" runat="server" style="display: none"></label>
            </li>
        </ul>


       <%-- <ul class="nav navbar-nav navbar-left" id="dialoginformacion" style="display: none" title="Información">
            <li>
                <asp:Label ID="lblingreso" runat="server" Text="Fecha de Ingreso:"></asp:Label>
            </li>
            <li>
                <asp:Label ID="lblingresoinfo" runat="server" Text="Fecha de ingreso..."></asp:Label>
            </li>
            <li>
                <asp:Label ID="lblasignado" runat="server" Text="Asignado Por:"></asp:Label>
            </li>
            <li>
                <asp:Label ID="lblasignadoinfo" runat="server" Text="Asignado info..."></asp:Label>
            </li>
            <li>
                <asp:Label ID="lblAssignedDate" runat="server" Text="Fecha de Asignación: "></asp:Label>
            </li>
        </ul>--%>
    </div>
    <div id="NavHeader" class="CollapseHeader">
        <div class="collapse navbar-collapse navHeaderCollapse MarginDiv" id="bs-TH-navbar-collapse-1">
            <ul class="nav navbar-nav navbar-left" runat="server" id="UACCell">
            </ul>
        </div>
    </div>

    

</nav>

    <!-- Trigger the modal with a button -->
    <!-- Modal -->
<div class="modal fade" id="ModalDerivar" role="dialog">
        <div class="modal-dialog" style="padding-top: 20px">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h4 id="lblDerivar" class="modal-title text-center">Derivar</h4>
                </div>
                <div class="col-md-12 TamaDiv">
                    <div class="col-md-6">
                        <h4 class="text-center">Usuarios</h4>
                        <input type="text" id="SearchUsers" class="form-control" placeholder="Buscar Usuarios">
                        <br />

                        <div class="list-group Users ScrollDiv">
                        </div>
                    </div>
                    <div class="col-md-6">
                        <h4 class="text-center">Grupos</h4>
                        <input type="text" id="SearchGoups" class="form-control" placeholder="Buscar Grupos">
                        <br />
                        <div class="list-group Groups ScrollDiv">
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="padding: 7px">
                    <span class="loadersmall" style="display: none"></span>
                    <h4 class="modal-title text-center">Observaciones</h4>
                    <textarea rows="3" id="deriveMessage" class="form-control TextAreaDerivar" placeholder="Mensaje a enviar..."></textarea>
                    <button type="button" class="btn btn-primary derive" onclick="DeriveTask()">Derivar</button>
                    <button type="button" class="btn btn-primary closeModal" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>


    </div>

<style>
    .ToolbarGlyphicons {
        font-size: 20px;
        margin-left: 15px;
        margin-right: 15px;
        margin-top: 0px;
    }

   

    .liButtons {
        margin: 0;
        padding: 0;
        margin-top: 5px;
        height: 25px;
    }

    .liButtons2 {
        margin: 0 !important;
        padding: 0 !important;
        margin-top: 15px !important;
        margin-left: 12px !important;
        /*height: 30px;*/
    }



    .navbar-text {
        margin: 0px 5px 0px 5px;
    }

    .boldFont {
        font-weight: bold;
    }

    .checkOptions {
        margin-left: 5px;
        padding-left: 5px;
    }

    #mnuOptions {
        width: 240px;
    }

    .navbar-task li button {
        margin-right: 2px;
    }

    .taskMessage {
        color: red;
        margin-left: 20px;
        font-size: 14px;
    }

    .GruposUsuarios:hover {
        z-index: 2 !important;
        color: #fff !important;
        background-color: #337ab7 !important;
        border-color: #337ab7 !important;
    }

    .TamaDiv {
        width: 572px !important;
        height: 310px;
        float: inherit !important;
    }

    .ScrollDiv {
        overflow: auto;
        height: 206px !important;
    }

    .selectedUser {
        color: #fff !important;
        background-color: #337ab7 !important;
        border-color: #337ab7 !important;
    }

    .Marginbutton {
        /*margin-top: 12px;*/
    }

    .Marginbutton2 {
        margin-top: 2px;
        margin-left: 5px;
        padding-left: 0px !important;
        padding-right: 0px !important;
    }

    .Marginbtn {
        margin-top: 2px;
    }

    .MarginDiv {
        /*margin-bottom: 5px;*/
    }

    .stylebtn {
        /*margin-top: 12px;*/
    }

    .MrgBtn {
        margin-left: 5px;
        margin-right: 5px;
    }

    .StyCalendar {
        width: 91px;
    }

    .MarginIco {
        margin-left: 9px;
    }

    .colorT {
        color: #777 !important;
    }

    .recorte {
        max-width: 150px;
        overflow: hidden;
        white-space: nowrap;
        text-overflow: ellipsis;
    }

    .loadersmall {
        border: 5px solid #f3f3f3;
        border-top: 5px solid #3498db;
        border-radius: 50%;
        width: 40px;
        height: 40px;
        animation: spin 2s linear infinite;
    }


    #dropTaskOptions {
        height: 32px;
        width: 35px;
       
    }

    #dropOptions {
        margin-left: 15px;
        margin-top: -1px;
    }

    .TextAreaDerivar {
        width: 565px;
        height: 70px;
        resize: none;
        margin-bottom: 30px;
    }

    .fixed-top-2 {
        margin-top: 36px;
    }

    .fixed-top-3 {
        margin-top: 66px;
    }
</style>

<div id="ModalAssign" runat="server" style="display: none;" align="center" title="zamba Software">
    <table style="align-content: center">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Usuarios"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ListBox ID="lvwUsers" runat="server" Width="292px"></asp:ListBox>
            </td>
        </tr>
        <tr>
            <td style="padding: 10px 10px 10px 10px">
                <asp:Button ID="BtnAsignar" runat="server" Text="Asignar" />
            </td>
        </tr>
    </table>
</div>

<input runat="server" type="hidden" id="hdnBtnPostback" />
<asp:HiddenField runat="server" ID="HiddenCurrentUserID" EnableViewState="true" />
<asp:HiddenField runat="server" ID="HiddenTaskId" EnableViewState="true" />
<asp:HiddenField runat="server" ID="Hiddendocid" EnableViewState="true" />
<asp:HiddenField runat="server" ID="HiddenDocTypeId" EnableViewState="true" />
<asp:HiddenField runat="server" ID="Hiddenwfstepid" EnableViewState="true" />
<asp:HiddenField runat="server" ID="HiddentaskEnded" EnableViewState="true" />

<script type="text/javascript">



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

        $("li.navbar-text").find("span")[1].title = $("li.navbar-text").find("span")[1].innerHTML;

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
            console.log(e);
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
        var buttonsQuantity = $('#ctl00_ContentPlaceHolder_ucTaskHeader_UACCell').children().length;

        if (buttonsQuantity == 0) {
            $('#liAcciones').css('display', 'none');
        }

        $('#tbDoc').children(2).children(2).parent(2).css('height', 600);

        var NombreUsuarioZamba = $("#ctl00_ContentPlaceHolder_ucTaskHeader_lblAsignedTo")[0].innerText;
        NombreUsuarioZamba = NombreUsuarioZamba.replace("Zamba_", "").trim();
        NombreUsuarioZamba = NombreUsuarioZamba.replace("Zamba", "").trim();
        $("#ctl00_ContentPlaceHolder_ucTaskHeader_lblAsignedTo")[0].innerText = NombreUsuarioZamba;

        //habilitar el derivar
        if (GetUsersRights(taskRes._stepId, 29) == true) {
            $("#ctl00_ContentPlaceHolder_ucTaskHeader_BtnDerivar").css("display", "block");
        } else {
            $("#ctl00_ContentPlaceHolder_ucTaskHeader_BtnDerivar").css("display", "none");
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

            var data;
            if (localStorage) {
                var localUserGroupsByStepId = localStorage.getItem('localUserGroupsByStepId' + stepId);
                if (localUserGroupsByStepId != undefined && localUserGroupsByStepId != null && localUserGroupsByStepId != "null") {
                    try {
                        data = JSON.parse(localUserGroupsByStepId);

                    } catch (e) {
                        console.log(e);
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

            for (var i = 0; i < data.length; i++) {
                if (data[i] !== null && data[i] !== undefined) {
                    $(".Groups").append('<a onclick="selectUserItem(this)" href="#" data-userId="' + data[i]._id + '" class="list-group-item GruposUsuarios"> ' + data[i]._name + '</a>')
                }
            }
        } catch (e) {
            console.log(e);
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
                        if (localStorage) {
                            try {

                                localStorage.setItem('localUserGroupsByStepId' + stepId, JSON.stringify(data));
                            } catch (e) {

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
            console.log(e);
        }
    };


    function GetGroupsByUserIds() {
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
            if (localStorage) {
                var localUsersByStepId = localStorage.getItem('localUsersByStepId' + stepId);
                if (localUsersByStepId != undefined && localUsersByStepId != null && localUsersByStepId != "null") {
                    try {
                        data = JSON.parse(localUsersByStepId);

                    } catch (e) {
                        console.log(e);
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
                    if (data[i] !== null && data[i] !== undefined) {
                        $(".Users").append('<a onclick="selectUserItem(this)" href="#" data-userId="' + data[i]._id + '" class="list-group-item GruposUsuarios"> '
                            + data[i]._name + '</a>')
                    }
                }
            }
        } catch (e) {
            console.log(e);
        }

    }

    function LoadlocalUsersByStepIdFromDB(stepId) {

        $.ajax({
            type: "POST",
            url: location.origin.trim() + getRestApiUrl() + '/api/Tasks/GetUsers?stepId=' + stepId,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data != null) {
                    if (localStorage) {
                        try {
                            localStorage.setItem('localUsersByStepId' + stepId, JSON.stringify(data));
                        } catch (e) {
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
                //window.opener.$('#MyTasksAnchor').trigger('click');//derivo y hago un trigger para actulizar grilla
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

        if (isFinalizar) {
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

                }
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                console.log(err.Message);
            }

        });


        try {
            if (window.opener != undefined)
                window.opener.document.getElementById("MyTasksAnchor").click();
        } catch (e) {

        }
        window.close();

    }
    function CloseTaskWindow() {
        window.close();
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
                        console.log(e);
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
        var url = thisDomain + '/globalsearch/search/search.html?' + localStorage.queryStringAuthorization //jQuery.param({ User: UserId }) + '#Zamba/';
        //window.location.replace(url);
        //        window.location.assign(url);

        window.open(url, '_blank', 'location=no');

    }



    function getRestApiUrl() {
        return '<%=ConfigurationManager.AppSettings["RestApiUrl"] %>';
    }

    function getThisDomain() {
        return '<%=ConfigurationManager.AppSettings["ThisDomain"] %>';
    }


</script>
