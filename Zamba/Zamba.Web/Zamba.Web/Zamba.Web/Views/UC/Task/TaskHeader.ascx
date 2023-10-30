<%@ Control Language="C#" AutoEventWireup="true" Inherits="TaskHeader" EnableViewState="False" CodeBehind="TaskHeader.ascx.cs" %>
<%@ Register Src="~/Views/UC/Common/ZDeleteButton.ascx" TagName="ZDeleteButton" TagPrefix="ucz" %>

<script src="../../scripts/tasks/TaskHeader.js?v=248"></script>
<link href="../../views/uc/task/TaskHeader.css?v=248" rel="stylesheet" />


<nav id="THNavBar" class="navbar navbar-toggleable-xs bg-faded navbar-light navbar-fixed-top" role="navigation" style="z-index: 9999; background-color: #337ab7 !important;">


    <div class="navbar-header" style="margin-left: 1%;">
        <div class="MenSup">
            <%--<button type="button" class="navbar-toggle pull-left btn-xs" data-toggle="collapse" data-target="#bs-TH-navbar-collapse-1">
                <span class="sr-only">Acciones</span>
                <span class="glyphicon glyphicon-flash"></span>
            </button>--%>
            <button type="button" class="navbar-toggle pull-left btn-xs navbar-toggle-button" onclick="CollapseMenu()">
                               <span class="glyphicon glyphicon-align-justify"></span>
            </button>
        </div>
        <a id="logo" class="hidden-xs navbar-brand tasklogo" style="height: 34px;" href="#"></a>
        <ul class=" navbar-left navbar-task row" style="display: flex; list-style: none">

            <%--Botones--%>

            <%--<li id="lihome" style="display: block" class="hidden-xs">
                <button id="BtnHome" type="button" class="btn btn-danger btn-xs liButtons" tooltip="Home"
                    onclick="IraHome()" tabindex="-1" style="background-color: #337ab7; border-color: #2e6da4; width: auto;">
                    <i class="fa fa-home"><span class="hidden-xs" style="margin-left: 5px;"></span></i>
                </button>
            </li>--%>


            <li id="liFinalizar" style="display: none">
                <button type="button" id="BtnFinalizar" class="btn btn-warning btn-xs liButtons"
                    title="Finalizar" onclick="CloseTask(true)" tabindex="-1" style="width: 10rem">
                    {{'index.RemoveTask' | translate}}
                </button>
            </li>

            <li id="liIniciar" style="display: none">
                <button type="button" id="BtnIniciar" class="btn btn-success btn-xs liButtons"
                    title="Iniciar" onclick="StartTask();" tabindex="-1" style="width: 150px">
                    {{'index.AddMyTasks' | translate}}</button>
            </li>

            <%--<li id="liAcciones" style="display: none">
                <button type="button" id="BtnAcciones" runat="server" class="btn btn-primary btn-xs liButtons"
                    title="" onclick="OpenModalActions();" tabindex="-1" style="width: 95px">
                    Acciones <span id="IconAction" class="glyphicon glyphicon-menu-down"></span>
                </button>
            </li>--%>


            <li id="liRemove" style="display: none">
                <button type="button" id="BtnRemove" runat="server" class="btn btn-warning btn-xs liButtons" tooltip="Quitar" visible="false" onclick="BtnRemove_Click()"
                    tabindex="-1" style="background-color: #337ab7; border-color: #2e6da4; display: none">
                    Quitar Tarea  <%--<i class="glyphicon glyphicon-alert"></i>--%>
                </button>
            </li>
            <li id="liDelete">
                <ucz:ZDeleteButton ID="deleteCtrl" runat="server" Visible="false" style="display: none" />
            </li>

             <li class="liButtons" id="liStep">
                <i runat="server" id="IconStep" class="icon-task-header ToolbarGlyphicons glyphicon glyphicon-tasks  hidden-md hidden-lg "></i>
                <span>
                    <label class="MarginIco  hidden-xs hidden-sm  ">Etapa:</label>
                    <span class="subtitle" id="lbletapadata2" runat="server">Etapa</span>
                </span>
            </li>
        </ul>
    </div>

    <div class="collapse navbar-collapse navbar-ex1-collapse" id="bs-TH-navbar-collapse-3" style="background-color: #337ab7;">


        <ul class="nav navbar-nav navbar-left navbar-task row" style="margin-left: 10px; list-style: none">

            <%--Etiquetas--%>

           

            <li class=" liButtons hidden-xs hidden-sm" id="liState">
                <span>
                    <label class="MarginIco    hidden-xs hidden-sm  ">Estado:</label>
                    <span class="subtitle hidden-xs hidden-sm" id="CboStates" runat="server">Estado</span>

                    <%--                    <asp:DropDownList AutoPostBack="True" ID="CboStates" runat="server">
                    </asp:DropDownList>--%>
                </span>
            </li>

            <li class=" liButtons" id="liAsig">
                <i runat="server" id="IconAssigned" name="IconAssigned" class="icon-task-header glyphicon ToolbarGlyphicons glyphicon-user"></i>
                <span>
                    <span class="subtitle" id="lblAsignedTo" runat="server">Usuario</span>
                </span>
            </li>
            <li id="liDerivar" style="padding-left: 5px" class="liButtons">
                <button type="button" id="BtnDerivar" runat="server" onclick=" GetUsers(); GetGroups(); OpenDeriveModal();" class="btn btn-xs btn-info " data-backdrop="static" data-keyboard="false">
                    <i class="fa fa-user"></i><span class="MarginIco">Derivar</span>
                </button>
            </li>
            <li class="liButtons " id="liFecVenc" runat="server">
                <i runat="server" id="IconValidate" class="glyphicon ToolbarGlyphicons glyphicon-calendar hidden-md hidden-lg "></i>
                <label class="MarginIco hidden-xs hidden-sm  ">Vto:</label>
                <span id="dtpFecVenc" class="subtitle" runat="server"></span>
                <%--                    <asp:TextBox AutoPostBack="True" ID="dtpFecVenc" runat="server" CssClass=" input-xs StyCalendar"></asp:TextBox>--%>
            </li>

            <%--<li id="dropOptions" class="hidden-xs">--%>
            <li id="dropOptions" class="hidden-xs liButtons">
                <div class="dropdown" style="margin-top: 2px;">
                    <button class="btn btn-default dropdown-toggle btn-xs Marginbtn" type="button" id="dropTaskOptions" data-toggle="dropdown">
                        <span class="glyphicon glyphicon-menu-hamburger"></span><span class="caret"></span>
                    </button>
                    <ul class="dropdown-menu" role="menu" aria-labelledby="dropTaskOptions" id="mnuOptions" style="background-color: slategray;">
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

            <li id="liCerrar" style="display: none;" class="navbar-right">
                <button type="button" id="BtnClose" runat="server" class="btn btn-warning btn-xs liButtons" tooltip="Cerrar"
                    onclick="CloseTaskWindow();" tabindex="-1" style="background-color: var(--ZBlue); border-color: #2e6da4; margin-left: 5px">
                    <i class="fa fa-sign-out"></i><span class="MarginIco hidden-xs hidden-sm">Cerrar</span>
                </button>
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
    <%-- <div id="NavHeader" class="CollapseHeader">
        <div class="collapse navbar-collapse navHeaderCollapse MarginDiv" id="bs-TH-navbar-collapse-1">
                <ul class="nav navbar-nav navbar-left" runat="server" id="UACCell">
            </ul>
        </div>
    </div>--%>
</nav>

<nav id="navActions" class="navbar navbar-toggleable-sm navbar-light" role="navigation" style="z-index: 9999; margin-bottom: 5px; min-height: 35px !important; display: contents; color: white !important">
    <div id="UACCell" runat="server" class="Actions" style="display:none">
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

<div class="modal fade" id="ModalActions" role="dialog" hidden="hidden">
    <div class="modal-dialog" style="margin-top: 10%">
        <div class="modal-content">
            <div class="modal-header">
                <h4 id="LblActions" class="modal-title text-center">Acciones</h4>
            </div>

            <%-- <div class="col-md-12">
                <ul id="UACCell" runat="server" class="list-group Actions ScrollDiv" style="margin-top: 10px;">
                </ul>
            </div>--%>

            <div class="modal-footer">
                <button type="button" class="btn btn-primary closeModal" data-dismiss="modal">Cerrar</button>
            </div>
        </div>
    </div>
</div>

<div class="modal fade" id="genericModal" role="dialog" hidden="hidden">
    <div class="modal-dialog" style="margin-top: 10%">
        <div class="modal-content">
            <div class="modal-header">
                <h4 id="lblModalgenericTitle" class="modal-title text-center"></h4>
            </div>

            <div class="col-md-12" id="genericModalConteiner">
            </div>

            <div class="modal-footer">
                <button type="button" class="btn btn-primary closeModal" data-dismiss="modal">Cerrar</button>
            </div>
        </div>
    </div>
</div>

<style>
    .wordwrap {
        white-space: normal !important;
        word-wrap: break-word;
        max-width: 100%;
        width: 100%;
    }

    .ToolbarGlyphicons {
        font-size: 20px;
        margin-left: 15px;
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
        /*width: 572px !important;*/
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
        /*margin-top: 2px;
        margin-left: 5px;
        padding-left: 0px !important;
        padding-right: 0px !important;*/
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
        margin-left: 10px;
        margin-right: 3px;
        font-size: 15px
    }

    .subtitle {
        font-size: 1.4rem;
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
        margin-top: 38px;
    }

    .fixed-top-3 {
        margin-top: 66px;
    }

    .Actions {
        display: -webkit-inline-box;
    }

    .list-group-item-action {
        border-color: #337ab7 !important;
        /*border: none !important;*/
        display: inline !important;
        padding: 12px 15px 12px 15px !important;
        font-size: 10px;
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

