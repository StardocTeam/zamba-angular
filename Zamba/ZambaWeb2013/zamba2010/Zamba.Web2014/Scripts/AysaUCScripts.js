
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
    openWindow("../Aysa/GeneralReportStandAlone2.aspx?userId=" + $("#ctl00_hdnUserId").val(), "", false);
}

function ShowTabRepors() {
    openWindow("../Aysa/GDI/HomeReports.aspx", "", false);
}

function ZDispatcherRedirection_ShowGralReport() {
    parent.HomeCabPresentation();
    openWindow("Views/Aysa/GeneralReportStandAlone2.aspx?userId=" + $("#ctl00_hdnUserId").val(), "", false);
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