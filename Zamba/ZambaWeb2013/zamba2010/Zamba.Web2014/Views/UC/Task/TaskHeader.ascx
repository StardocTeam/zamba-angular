<%@ Control Language="C#" AutoEventWireup="false" CodeFile="TaskHeader.ascx.cs" Inherits="TaskHeader" EnableViewState="False" %>
<%@ Register Src="~/Views/UC/Common/ZDeleteButton.ascx" TagName="ZDeleteButton" TagPrefix="ucz" %>

<nav class="navbar navbar-default" role="navigation" style="min-height: 0; margin: 0; padding:5px">
    <div class="container-fluid collapse navbar-collapse navbar-ex1-collapse" id="bs-TH-navbar-collapse-3" style="padding: 0px;">
        <ul class="nav navbar-nav navbar-left navbar-task">
            <li id="liCerrar">
                <button type="submit" id="BtnClose" runat="server" class="btn btn-danger btn-xs" tooltip="Cerrar"
                    onclick="ShowLoadingAnimation(); $('#__EVENTTARGET').val('BtnClose');" tabindex="-1">
                    Cerrar             
                </button>
            </li>
            <li id="liIniciar">
                <asp:Button type="submit" ID="BtnIniciar" runat="server" CssClass="btn btn-primary btn-xs"
                    ToolTip="Iniciar" OnClick="BtnStart_Click" TabIndex="-1" Text="Iniciar"></asp:Button>
            </li>
            <li id="liDerivar">
                <button type="submit" id="BtnDerivar" runat="server" cssclass="btn btn-info btn-xs" tooltip="Derivar" onclick="BtnReAsign_Click()"
                    tabindex="-1">
                    Derivar   <span class="glyphicon glyphicon-user"></span>
                </button>
            </li>
            <li id="liRemove">
                <button type="submit" id="BtnRemove" runat="server" cssclass="btn btn-info btn-xs" tooltip="Quitar" visible="false" onclick="BtnRemove_Click()"
                    tabindex="-1">
                    Quitar  <span class="glyphicon glyphicon-remove"></span>
                </button>
            </li>
            <li id="liDelete">
                <ucz:ZDeleteButton ID="deleteCtrl" runat="server" Visible="false" />
            </li>
            <li class="navbar-text">
                <span class="boldFont">Etapa: </span>
                <span id="lbletapadata" runat="server">Texto de la etapa</span>
            </li>
            <li class="navbar-text">
                <span class="boldFont">Usuario: </span>
                <span id="lblAsignedTo" runat="server">Nombre de usuario</span>
            </li>
            <li class="navbar-text">
                <span class="boldFont">Estado: </span>
                <asp:DropDownList AutoPostBack="True" ID="CboStates" runat="server">
                </asp:DropDownList>
            </li>
            <li class="navbar-text" id="liFecVenc" runat="server">
                <span class="boldFont">Vencimiento: </span>
                <asp:TextBox AutoPostBack="True" ID="dtpFecVenc" runat="server" CssClass=" input-xs"></asp:TextBox>
            </li>
            <li id="dropOptions">
                <div class="dropdown">
                    <button class="btn btn-default dropdown-toggle btn-xs" type="button" id="dropTaskOptions" data-toggle="dropdown">
                        Opciones <span class="caret"></span>
                    </button>
                    <ul class="dropdown-menu" role="menu" aria-labelledby="dropTaskOptions" id="mnuOptions">
                        <li role="presentation">
                            <asp:HiddenField runat="server" ID="hdnTakeTaskFix" />
                            <asp:CheckBox role="menuitem" AutoPostBack="True" ID="chkTakeTask" runat="server" 
                                Text="Finalizar al Cerrar" OnLoad="chkTakeTask_Load" 
                                Enabled="false" CssClass="checkOptions" Font-Bold="false" Font-Size="Smaller"/>
                        </li>
                        <li role="presentation">
                            <asp:HiddenField runat="server" ID="hdnCloseTaskFix" />
                            <asp:CheckBox role="menuitem" AutoPostBack="True" ID="chkCloseTaskAfterDistribute" runat="server" 
                                Text="Cerrar tarea al cambiar de etapa" OnLoad="chkCloseTaskAfterDistribute_Load" 
                                Enabled="false" CssClass="checkOptions" Font-Bold="false" Font-Size="Smaller"/>
                        </li>
                    </ul>
                </div>
            </li>
            <li class="navbar-text">
                <label id="lblmsj" class="taskMessage" runat="server"></label>
            </li>
        </ul>
        <ul class="nav navbar-nav navbar-left" id="dialoginformacion" style="display: none" title="Información">
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
        </ul>
    </div>
    <div class="navbar-header" style="padding:0px">
        <button type="button" class="navbar-toggle pull-left btn-xs" data-toggle="collapse" data-target="#bs-TH-navbar-collapse-1">
            <span class="sr-only">Acciones de usuario</span>
            <span class="glyphicon glyphicon-flash"></span>
<%--            <span class="icon-bar"></span>
            <span class="icon-bar"></span>--%>
        </button>
        <button type="button" class="navbar-toggle pull-left btn-xs" data-toggle="collapse" data-target="#bs-TH-navbar-collapse-3">
            <span class="sr-only">Información de tarea</span>
            <span class="glyphicon glyphicon-align-justify"></span>
<%--            <span class="icon-bar"></span>
            <span class="icon-bar"></span>--%>
        </button>
    </div>
    <div class="collapse navbar-collapse navHeaderCollapse" id="bs-TH-navbar-collapse-1" style="padding:0px">
        <ul class="nav navbar-nav navbar-left" runat="server" id="UACCell">
        </ul>
    </div>
</nav>

<style>
    .navbar-text {
        margin:0px 5px 0px 5px;
    }
    .boldFont {
        font-weight:bold;
    }
    .checkOptions {
        margin-left:5px;
        padding-left:5px;
    }
    #mnuOptions {
        width:240px;
    }
    .navbar-task li button {
        margin-right: 2px;
    }
    .taskMessage {
        color: red;
    }
</style>

<div id="ModalAssign" runat="server" style="display: none;" align="center" title="zamba Software">
    <table style="align-content:center">
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
<asp:HiddenField runat="server" ID="HiddenTaskId" EnableViewState="true" />
<asp:HiddenField runat="server" ID="Hiddendocid" EnableViewState="true" />
<asp:HiddenField runat="server" ID="HiddenDocTypeId" EnableViewState="true" />
<asp:HiddenField runat="server" ID="Hiddenwfstepid" EnableViewState="true" />

<script type="text/javascript">
    //variable creada para usar de bandera y no cargar deteminados objetos
    var isClosingTask = false;

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

        makeCalendar('ctl00_ContentPlaceHolder_ucTaskHeader_vto_calendar');

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

        if ($("#<%= BtnDerivar%>").length == 0)
            $("#liDerivar").css('display', 'none');

        if ($("#<%= BtnRemove%>").length == 0)
            $("#liRemove").css('display', 'none');

        if ($("#<%= deleteCtrl%>").length == 0)
            $("#liDelete").css('display', 'none');
    });

    function showAssignModal() {
        $(document).ready(function () {
            
            $("#ctl00_ContentPlaceHolder_ucTaskHeader_ModalAssign").dialog({
                autoOpen: true, modal: true, width: 400, height: 300
            });
            FixAndPosition($("#ctl00_ContentPlaceHolder_ucTaskHeader_ModalAssign").parent());
            $("#ctl00_ContentPlaceHolder_ucTaskHeader_ModalAssign").dialog().parent().appendTo($("form:first"));
        });
    }

    function showjQueryDialog() {
        $("#dialog").dialog("open");
    }

    $(document).ready(function()
    {
        $('#<%= BtnIniciar.ClientID%>').click(function(e) 
        { 
                ShowLoadingAnimation();
        });
     });
    
</script>
