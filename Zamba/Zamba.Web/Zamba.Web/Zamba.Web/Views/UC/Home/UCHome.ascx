<%@ Control Language="C#" AutoEventWireup="false" Inherits="Views_UC_Home_UCHome" Codebehind="UCHome.ascx.cs" %>
<input id="btnBusqueda-Aysa" type="button" onclick="SelectTabFromMasterPage('tabsearch')" />
<input id="btnListados-Aysa" type="button" onclick="SelectTabFromMasterPage('tabtasklist')" />
<%  if (Page.Theme != null && Page.Theme.ToLower() == "reintegros") {%>
<input id="btnAltaEntidad1" runat="server" type="button" onclick="InsertFormModal('33324')"
    class="btnAltaEntidadReintegro" title="Nuevo Reintegro" />
<input id="btnAltaEntidad6" runat="server" type="button" onclick="InsertFormModal('33337')"
    class="btnAltaEntidadAfiliado" title="Nuevo Afiliado" />
<%  }%>
<%  if (Page.Theme != null && Page.Theme.ToLower() == "mantenimiento") {%>
    <script language="javascript" type="text/javascript">
        function ShowEstablecimientos() {
            ShowLoadingAnimation();
            if ($('#divTabEstablecimientos') == null) {
                var myIFrame = '<div id="divTabEstablecimientos" style="padding-top:20px"><iframe id="TabEstablecimientosIFrame" style="width:95%; height:80%; background-color:white; border: 1px solid transparent" src="../Aysa/Mantenimiento/ListadoEstablecimientos.aspx" frameborder="0"></iframe></div>';
                $(myIFrame).appendTo('#second-div-presentation-Main');
                $("#second-div-presentation-Main").css("background", "#0e2b8d");
            }
            else {
                var IfSem = $("#TabEstablecimientosIFrame");
                if (IfSem != null && IfSem[0] != null) {
                    IfSem[0].contentWindow.location = "about:blank";
                    IfSem[0].contentWindow.location = "../Aysa/Mantenimiento/ListadoEstablecimientos.aspx";

                }
                else {
                    if (IfSem.context != null) {
                        IfSem[0].contentWindow.location = "about:blank";
                        IfSem.context.location = "../Aysa/Mantenimiento/ListadoEstablecimientos.aspx";
                    }
                }
            }
        }
    </script>
<input id="btnAltaEntidad2" runat="server" type="button" onclick="InsertFormModal('601103')"
    class="btnAltaEntidadEstablecimiento" title="Nuevo Establecimiento" />
<input id="btnAltaEntidad3" runat="server" type="button" onclick="InsertFormModal('601105')"
    class="btnAltaEntidadCoordinadorZona" title="Nuevo Coordinador de Zona" />
<input id="btnShowEstablecimientos" runat="server" type="button" onclick="ShowEstablecimientos()"
    class="btnListadoEstablecimientos" title="Visualizar Listado de Establecimientos" />
<%  }%>
<%  if (Page.Theme != null && Page.Theme.ToLower() == "credito") {%>
    <script language="javascript" type="text/javascript">
        function ShowTabGenRepCre() {
            openWindow("../Demos/Creditos/GeneralReportStandAlone2.aspx?userId=" + $("#ctl00_hdnUserId").val(), "", false);
        }
    </script>
<input id="btnAltaCliente" runat="server" type="button" onclick="InsertFormModal('1046')"
    class="btnAltaCliente" title="Nuevo Cliente" />
<input id="btnGeneralReportCred" type="button" onclick="ShowTabGenRepCre();" />
<%  }%>
<%  if (Page.Theme != null && Page.Theme.ToLower() == "aysadiseno") {%>
    <script  type="text/javascript">
        function ShowTabDOR() {
            //ShowLoadingAnimation();
            $("#divReports").remove();
            $("#dvSiteMap").remove();
            $("#divSem").remove();
            $("#divTabSemaphore").remove();
            if ($('#divTabDOR') == null) {
                var myIFrame = '<div id="divTabDOR" style="padding-top:20px"><iframe id="TabDORIF" style="width:95%; height:80%; background-color:white; border: 1px solid transparent" src="../Aysa/TableroDOR.aspx" frameborder="0"></iframe></div>';
                $(myIFrame).appendTo('#second-div-presentation-Main');

                SwitchToIFrameClass();
            }
            else {
                var IfSem = $("#TabDORIF");
                if (IfSem != null && IfSem[0] != null) {
                   // SwitchToIFrameClass();
                    //IfSem[0].contentWindow.location = "about:blank";
                    //IfSem[0].contentWindow.location = "../Aysa/TableroDOR.aspx";
                    ShowIFrameModal("Tablero DOR", "../Aysa/TableroDOR.aspx", 800, 550);
                }
                else {
                    if (IfSem.context != null) {

                        //SwitchToIFrameClass();
                        //IfSem[0].contentWindow.location = "about:blank";
                        //IfSem.context.location = "../Aysa/TableroDOR.aspx";
                        ShowIFrameModal("Tablero DOR", "../Aysa/TableroDOR.aspx", 800, 550);
                    }
                }
            }
        }

        function ShowTabGenRep() {
            openWindow("../Aysa/GeneralReportStandAlone2.aspx?userId=" + $("#hdnUserId").val(), "", false);
        }

        function ShowTabRepors() {
            openWindow("../Aysa/GDI/HomeReports.aspx", "", false);
        }

        function ZDispatcherRedirection_ShowGralReport() {
            parent.HomeCabPresentation();
            openWindow("Views/Aysa/GeneralReportStandAlone2.aspx?userId=" + $("#hdnUserId").val(), "", false);
        }

        function ShowIntimationsReport() {
            window.open("../Aysa/GDI/IntimationsReport.aspx");
            closeReportDialog();
        }

        function ShowComplaintsReport() {
            window.open("../Aysa/GDI/ComplaintsReport.aspx");
            closeReportDialog();
        }
        function ShowIntimationsUnansweredReport() {
            ShowLoadingAnimation();
            closeReportDialog();
            var url = "../Aysa/GDI/IntimationsUnansweredReport.aspx";
           
            ShowIFrameModal("Insertar documentos", url, 800, 550);
           // StartObjectLoadingObserverById("IFDialogContent");
 hideLoading();
        }
        function closeIntimationsUnansweredReport() {
            if ($("#modalDialog").hasClass("ui-dialog-content") && $("#modalDialog").dialog("isOpen"))
            $("#modalDialog").dialog("close");
        }
        function ShowInspectionReport() {
            window.open("../Aysa/GeneralReportStandAlone2.aspx");
            closeReportDialog();
        }
        function closeSemaphoreDialog() {
            if ($("#divSemaphore").hasClass("ui-dialog-content") && $("#divSemaphore").dialog("isOpen"))
                $("#divSemaphore").dialog("close");
        }

        function closeReportDialog() {
            if ($("#divReport").hasClass("ui-dialog-content") && $("#divReport").dialog("isOpen")) {
              $("#divReport").dialog("close");
          }
        }

        function ShowReports() {
            ShowDivModal("Zamba Software - Reportes", $("#divReport"), 600, 150);
            //var windowWidth = $(window).width();
            //var windowHeight = $(window).height();

            //$("#divReport").dialog({ height: 80, width: 750, closeOnEscape: false, open: function (event, ui) { $(".ui-dialog-titlebar-close").hide(); }, autoOpen: true, modal: true, position: 'center', resizable: false });
            //$("#divReport").dialog("open");
        }
        function ShowSemaphores() {
            ShowDivModal("Zamba Software - Semaforos", $("#divSemaphore"), 600, 150);
            //var windowWidth = $(window).width();
            //var windowHeight = $(window).height();

            //$("#divSemaphore").dialog({ height: 80, width: 750, closeOnEscape: false, open: function (event, ui) { $(".ui-dialog-titlebar-close").hide(); }, autoOpen: true, modal: true, position: 'center', resizable: false });
            //$("#divSemaphore").dialog("open");
        }
        function ShowIntimationsSemaphore() {
            window.open("../Aysa/GDI/IntimationsSemaphore.aspx");
        }
        function ShowComplaintsSemaphore() {
            window.open("../Aysa/GDI/ComplaintsSemaphore.aspx");
        }
        function ShowinspectionsSemaphore() {
            window.open("../Aysa/StandAloneSemaphore.aspx");
        }
        function ShowTabIntimationsSemaphore() {
            ShowLoadingAnimation();
            $("#divReports").remove();
            $("#dvSiteMap").remove();
            $("#divSem").remove();
            $("#divTabDOR").remove();
            if ($('#divTabSemaphore') == null) {
                var myIFrame = '<div id="divTabSemaphore" style="padding-top:20px"><iframe id="SemaphoreIF" style="width:95%; height:80%; background-color:white; border: 1px solid transparent" src="../Aysa/GDI/IntimationsSemaphore.aspx" frameborder="0"></iframe></div>';
                $(myIFrame).appendTo('#second-div-presentation-Main');

                SwitchToIFrameClass();
            }
            else {
                var IfSem = $("#SemaphoreIF");
                if (IfSem != null && IfSem[0] != null) {
                    SwitchToIFrameClass();

                    IfSem[0].contentWindow.location = "about:blank";
                    IfSem[0].contentWindow.location = "../Aysa/GDI/IntimationsSemaphore.aspx";
                    $("#SemaphoreIF").show();
                }
                else {
                    if (IfSem.context != null) {
                        SwitchToIFrameClass();
                        IfSem[0].contentWindow.location = "about:blank";
                        IfSem.context.location = "../Aysa/GDI/IntimationsSemaphore.aspx";
                        $("#SemaphoreIF").show();
                    }
                }
            }
            closeSemaphoreDialog();
            hideLoading();
        }
        function ShowTabComplaintsSemaphore() {
            ShowLoadingAnimation();
            $("#divReports").remove();
            $("#dvSiteMap").remove();
            $("#divSem").remove();
            $("#divTabDOR").remove();
            if ($('#divTabSemaphore') == null) {
                var myIFrame = '<div id="divTabSemaphore" style="padding-top:20px"><iframe id="SemaphoreIF" style="width:95%; height:80%; background-color:white; border: 1px solid transparent" src="../Aysa/GDI/ComplaintsSemaphore.aspx" frameborder="0"></iframe></div>';
                $(myIFrame).appendTo('#second-div-presentation-Main');

                SwitchToIFrameClass();
            }
            else {
                var IfSem = $("#SemaphoreIF");
                if (IfSem != null && IfSem[0] != null) {
                    SwitchToIFrameClass();
                    IfSem[0].contentWindow.location = "about:blank";
                    IfSem[0].contentWindow.location = "../Aysa/GDI/ComplaintsSemaphore.aspx";
                    $("#SemaphoreIF").show();
                }
                else {
                    if (IfSem.context != null) {
                        SwitchToIFrameClass();
                        IfSem[0].contentWindow.location = "about:blank";
                        IfSem.context.location = "../Aysa/GDI/ComplaintsSemaphore.aspx";
                        $("#SemaphoreIF").show();
                    }
                }
            }
            closeSemaphoreDialog();
            hideLoading();
        }
        function ShowTabInspectionsSemaphore() {
            ShowLoadingAnimation();
            $("#divReports").remove();
            $("#dvSiteMap").remove();
            $("#divSem").remove();
            $("#divTabDOR").remove();
            if ($('#divTabSemaphore') == null) {
                var myIFrame = '<div id="divTabSemaphore" style="padding-top:20px"><iframe id="SemaphoreIF" style="width:95%; height:80%; background-color:white; border: 1px solid transparent" src="../Aysa/StandAloneSemaphore.aspx" frameborder="0"></iframe></div>';
                $(myIFrame).appendTo('#second-div-presentation-Main');

                SwitchToIFrameClass();

            }
            else {
                var IfSem = $("#SemaphoreIF");
                if (IfSem != null && IfSem[0] != null) {
                    SwitchToIFrameClass();

                    IfSem[0].contentWindow.location = "about:blank";
                    IfSem[0].contentWindow.location = "../Aysa/StandAloneSemaphore.aspx";
                    $("#SemaphoreIF").show();
                }
                else {
                    if (IfSem.context != null) {
                        SwitchToIFrameClass();
                        IfSem[0].contentWindow.location = "about:blank";
                        IfSem.context.location = "../Aysa/StandAloneSemaphore.aspx";
                        $("#SemaphoreIF").show();
                    }
                }
            }
            closeSemaphoreDialog();
            hideLoading();
        }

        $(document).ready(function () {
            
            ShowIntimationsUnansweredReport();
        });
    </script>
<input id="btnAltaEntidad" runat="server" type="button" onclick="InsertFormModal('1050')"
    class="btnAltaEntidad" />
<input id="btnModificacion-Aysa" type="button" onclick="ShowTabDOR();" />
<input id="btnGeneralReport" type="button" onclick="ShowReports();" />
<input id="BtnUnansweredReport" type="button" onclick="ShowIntimationsUnansweredReport();" />
<input id="btnInspecPendientes-Aysa" type="button" onclick="ShowSemaphores();" />
<div id="divReport" title="Zamba Software - Reportes" style="display: none">
    <div style="text-align: center">
        <input id="BtnComplaintsReport" type="button" value="Reporte de Denuncias" onclick="ShowComplaintsReport();" />
        <input id="BtnIntimationsReport" type="button" value="Reporte de Intimaciones" onclick="ShowIntimationsReport();" />
        <input id="BtnInspectionReport" type="button" value="Reporte General" onclick="ShowInspectionReport();" />
    </div>
    <div style="height: 5px">
    </div>
    <div id="divCerrarReports" style="width: 100%; text-align: center">
        <input id="BtnCloseReports" type="button" value="Cerrar" onclick="closeReportDialog();" />
    </div>
</div>
<div id="divSemaphore" title="Zamba Software - Semaforos" style="display: none">
    <div style="text-align: center">
        <input id="BtnComplaintsSemaphore" type="button" value="Semaforo de Denuncias" onclick="ShowTabComplaintsSemaphore();" />
        <input id="BtnIntimationsSemaphore" type="button" value="Semaforo de Intimaciones"
            onclick="ShowTabIntimationsSemaphore();" />
        <input id="Btninspection" type="button" value="Semaforo de Inspecciones" onclick="ShowTabInspectionsSemaphore();" />
    </div>
    <div style="height: 5px">
    </div>
    <div id="divCerrarSemaphore" style="width: 100%; text-align: center">
        <input id="BtnCloseSemaphore" type="button" value="Cerrar" onclick="closeSemaphoreDialog();" />
    </div>
</div>
<%  } %>
<%  if (Page.Theme != null && Page.Theme.ToLower() == "liberty") {%>
<input id="btnInsertar" type="button" onclick="SelectTabFromMasterPage('tabInsert')" />
<%  } %>
<%  if (Page.Theme != null && Page.Theme.ToLower() == "aysagec") {%>
    <script language="javascript" type="text/javascript">
        function ShowTabGenRepGec() {
            openWindow("../Aysa/GEC/GeneralReportStandAlone2.aspx?userId=" + $("#hdnUserId").val(), "", false);
        }

        function ZDispatcherRedirection_ShowGralReportGec() {
            parent.HomeCabPresentation();
            openWindow("Views/Aysa/GEC/GeneralReportStandAlone2.aspx?userId=" + $("#hdnUserId").val(), "", false);
        }
    </script>
    <input id="btnGeneralReport" type="button" onclick="ShowTabGenRepGec();" />
<%  }%>
<%  if (Page.Theme != null && Page.Theme.ToLower() == "aysadal") {%>
    <script language="javascript" type="text/javascript">
        function ShowTabProductCatalog() {
            openWindow("../Aysa/DAL/ProductCatalog.aspx", "", false);
        }
    </script>

    <input id="btnProductCatalogHome" type="button" value="Catalogo Productos" onclick="ShowTabProductCatalog();" />

<%-- <script language="javascript" type="text/javascript">

     function ShowReports() {
         var windowWidth = $(window).width();
         var windowHeight = $(window).height();

         $("#divDALReport").dialog({ height: 80, width: 750, closeOnEscape: false, open: function (event, ui) { $(".ui-dialog-titlebar-close").hide(); }, autoOpen: true, modal: true, position: 'center', resizable: false });
         $("#divDALReport").dialog("open");
     }

     function closeReportDialog() {

         $("#divDALReport").dialog("close");
     }


      function PedidosAysa() {
         window.open("../Aysa/DAL/PedidosAysa.aspx");
         closeReportDialog();
     } function PedidosDAL() {
         window.open("../Aysa/DAL/PedidosDAL.aspx");
         closeReportDialog();
     } function Pedidos24hsReport() {
         window.open("../Aysa/DAL/Pedidos24hsReport.aspx");
         closeReportDialog();
     } function PedidosRechazados() {
         window.open("../Aysa/DAL/PedidosRechazados.aspx");
         closeReportDialog();
     } function PedidosSemaforo() {
         window.open("../Aysa/DAL/PedidosSemaforo.aspx");
         closeReportDialog();
     } function PendientesConformidad() {
         window.open("../Aysa/DAL/PendientesConformidad.aspx");
         closeReportDialog();
     }
                  </script>

<input id="Button3" type="button" value="Reportes" onclick="ShowReports();" />

<div id="divDALReport" title="Zamba Software - Reportes" style="display: none">
    <div style="text-align: center">
        <input id="Button6" type="button" value="Pedidos Aysa" onclick="PedidosAysa();" />
        <input id="Button7" type="button" value="Pedidos DAL" onclick="PedidosDAL();" />
        <input id="Button8" type="button" value="Pedidos Generados en las ultimas 24hs" onclick="Pedidos24hsReport();" />
        <input id="Button1" type="button" value="Pedidos Rechazados" onclick="PedidosRechazados();" />
        <input id="Button2" type="button" value="Pedidos Pendientes de Conformidad" onclick="PendientesConformidad();" />
        <input id="Button4" type="button" value="Semaforo de Pedidos Pendientes" onclick="PedidosSemaforo();" />
    </div>
    <div style="height: 5px">
    </div>
    <div id="div2" style="width: 100%; text-align: center">
        <input id="Button9" type="button" value="Cerrar" onclick="closeReportDialog();" />
    </div>
</div>--%>



<%  }%>
<asp:Panel ID="pnl" runat="server"></asp:Panel>


<div id="MainHome">
    <%--<search></search>
    <grid></grid>

    <script>
        searc.init
        search.addgrid(grid)
    </script>--%>
</div>
