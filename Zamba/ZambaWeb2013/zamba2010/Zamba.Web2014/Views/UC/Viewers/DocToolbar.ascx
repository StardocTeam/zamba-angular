<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DocToolbar.ascx.cs" Inherits="Views_UC_Viewers_DocToolbar" %>

<asp:HiddenField ID="hdnShowHistoryTab" runat="server" />
<asp:HiddenField ID="hdnShowForumTab" runat="server" />
<asp:HiddenField ID="hdnShowAsociatedTab" runat="server" />
<asp:HiddenField ID="hdnShowMailsTab" runat="server" />
<asp:HiddenField ID="hdnLastTab" runat="server"/>
<asp:HiddenField ID="hdnCurrentFormID" runat="server"/>
<asp:HiddenField ID="hdnDocId" runat="server" />
<asp:HiddenField ID="hdnFilePath" runat="server" />
<asp:HiddenField ID="hdnDocTypeId" runat="server" />
<asp:HiddenField ID="hdnWfstepid" runat="server" />
<asp:HiddenField ID="hdnDocExt" runat="server" />
<asp:HiddenField ID="hdnImprimir" runat="server" />
<asp:HiddenField ID="hdnDocContainer" runat="server" />

<div class="btn-toolbar" role="toolbar">
    <div class="btn-group">
        <button type="button" class="btn btn-success btn-xs" onclick="Collapse(true)">Documento</button>
    </div>

    <div class="btn-group">
        <button id="btnRefresh" type="button" runat="server" class="btn btn-default btn-xs" onclick="Refresh_Click()">
            <span class="glyphicon glyphicon-refresh"> </span>
        </button>
        <button id="btnEmail" type="button" runat="server" class="btn btn-default btn-xs" onclick="Email_Click()">
            <span class="glyphicon glyphicon-envelope"> </span>
        </button>
        <button id="btnAttach" type="button" runat="server" class="btn btn-default btn-xs" onclick="IncorporarDoc_Click()">
            <span class="glyphicon glyphicon-file"> </span>
        </button>
        <button id="btnOpenNewTab" type="button" runat="server" class="btn btn-default btn-xs btn-openNewTab" onclick="Download_Click()">
            <span class="glyphicon glyphicon-new-window" style="height: 16px" ></span>
        </button>
        <button id="btnPrint" type="button" runat="server" class="btn btn-default btn-xs" onclick="Imprimir_Click()" >
            <span class="glyphicon glyphicon-print"> </span>
        </button>
    </div>

    <div class="btn-group">
        <button id="liHistory" type="button" class="btn btn-default btn-xs" onclick="ShowDocHistory()">Historial</button>
        <button id="liForum" type="button" class="btn btn-default btn-xs" onclick="ShowForum()">Foro</button>
        <button id="liAsociated" type="button" class="btn btn-default btn-xs" onclick="ShowAsociated()">Asociados</button>
        <button id="liMails" type="button" class="btn btn-default btn-xs" onclick="ShowMailHistory()">Mails</button>
    </div>

    <div id="btnCollapse" class="btn btn-default btn-xs" style="display:none" onclick="Collapse(true);">
        <div class="glyphicon glyphicon-chevron-up" style="height: 16px" ></div>
    </div>          
</div>

<iframe id="tabContent" style="display:none;height:450px;width:100%;margin-top:5px"></iframe>

<script type="text/javascript">
    $(document).ready(function () {
        var showForumTab = $("#<%=hdnShowForumTab.ClientID %>").val().toLowerCase();
        var showHistoryTab = $("#<%=hdnShowHistoryTab.ClientID %>").val().toLowerCase();
        var showAsociatedTab = $("#<%=hdnShowAsociatedTab.ClientID %>").val().toLowerCase();
        var showMailsTab = $("#<%=hdnShowMailsTab.ClientID %>").val().toLowerCase();

        if (showHistoryTab != "true") {
            $("#liHistory").remove();
            $("#divHistory").remove();
        }
        if (showForumTab != "true") {
            $("#divForum").remove();
            $("#liForum").remove();
        }
        if (showAsociatedTab != "true") {
            $("#divAsociated").remove();
            $("#liAsociated").remove();
        }
        if (showMailsTab != "true") {
            $("#divMails").remove();
            $("#liMails").remove();
        }

        var path = document.getElementById('<%=hdnFilePath.ClientID %>').value;
        if (path.length == 0) {
            $("#divOpenNewTab").remove();
        }
    });

    function Collapse(collapse) {
        if (collapse) {
            $("#tabContent").attr('src', '');
            $("#btnCollapse").hide();
            $("#tabContent").hide('fast');
        } else {
            $("#btnCollapse").show();
            $("#tabContent").show('fast');
            ShowLoadingAnimation();
        }
    }

    function ShowForum() {
        Collapse(false);
        $("#tabContent").attr('src', '../WF/TaskDetails/TaskForum.aspx?ResultID=<%=DocId %>&DocTypeId=<%=DocTypeId%>');
    }

    function ShowAsociated() {
        Collapse(false);
        $("#tabContent").attr('src', '../WF/TaskDetails/TaskAsociated.aspx?ResultID=<%=DocId %>&DocTypeId=<%=DocTypeId%>');
    }

    function ShowDocHistory() {
        Collapse(false);
        $("#tabContent").attr('src', '../WF/TaskDetails/TaskHistory.aspx?ResultID=<%=DocId %>');
    }

    function ShowMailHistory() {
        Collapse(false);
        $("#tabContent").attr('src', '../WF/TaskDetails/TaskMailhistory.aspx?ResultID=<%=DocId %>');
    }

    function Imprimir_Click() {
        var pending = true;
        var office = document.getElementById('<%=hdnImprimir.ClientID %>').value;

        try {
            if (window.frames['formBrowser']) {
                window.frames['formBrowser'].focus();
                window.frames['formBrowser'].print();
                pending = false;
            } else if ($('#divViewer').length) {
                PrintDiv($('#divViewer').html());
                pending = false;
            }            
        }
        catch (e) {
            alert(e.description);
        }

        if (pending) {
            if (print == "true") {
                try {
                    var destinationURL = "../Tools/ToolPrint.aspx?docid=<%=DocId %>&doctypeid=<%=DocTypeId%>";
                    var newwindow = window.open(destinationURL, '_blank', 'width=620,height=580,left=' + (screen.width - 600) / 2 + ',top=' + (screen.height - 580) / 2 + ',directories=no,status=no,menubar=no,toolbar=no,location=no,resizable=no,toolbar=no');
                }
                catch (e) {
                    alert(e.description);
                }
            }
            else {
                $("#<%=btnPrint.ClientID %>").hide();
                $("#docToolbarUl").append("<li id='legend' style='display:none;'>La Impresión del documento se realiza externamente (en caso de no visualizarse la barra de impresion, guarde el documento o utilice click secundario->Imprimir)”</li>");
                $("#legend").show("3000");
            }
        }      
    }

    function PrintDiv(divHtml) {
        var mywindow = window.open('', '_blank', 'height=400,width=600');
        var html = '<html><head><title>impresion</title></head><body >' + divHtml + '</body></html>';
        mywindow.document.write(html);
        mywindow.document.close();
        mywindow.print();
        mywindow.close();

        return true;
    }

    function CheckIsIE() {
        if (navigator.appName.toUpperCase() == 'MICROSOFT INTERNET EXPLORER') {
            return true;
        }
        else {
            return false;
        }
    }

    function Email_Click() {
        try {
            var doctypeId = document.getElementById('<%=hdnDocTypeId.ClientID %>');
            var docId = document.getElementById('<%=hdnDocId.ClientID %>');
            var wfstepid = document.getElementById('<%=hdnWfstepid.ClientID %>');
            var docExt = document.getElementById('<%=hdnDocExt.ClientID %>');

            if (!doctypeId)
                doctypeId = document.getElementById("ctl00_ContentPlaceHolder_TabContainer_TabDocumento_ucDocViewer_hdnDocTypeId");

            if (!docId)
                docId = document.getElementById("ctl00_ContentPlaceHolder_TabContainer_TabDocumento_ucDocViewer_hdnDocId");

            if (!wfstepid)
                wfstepid = document.getElementById("ctl00_ContentPlaceHolder_TabContainer_TabDocumento_ucDocViewer_hdnWfstepid");

            if (!docExt)
                docExt = document.getElementById("ctl00_ContentPlaceHolder_TabContainer_TabDocumento_ucDocViewer_hdnDocExt");

            var destinationURL = "../../Notifications/SendMails.aspx?docid=" + docId.value + "&doctypeid=" + doctypeId.value + "&wfstepid=" + wfstepid.value + "&docext=" + docExt.value;
            var newwindow = window.open(destinationURL, this.target, 'width=620,height=400,left=' + (screen.width - 600) / 2 + ',top=' + (screen.height - 580) / 2 + ',directories=no,status=no,menubar=no,toolbar=no,location=no,resizable=no,toolbar=no');
        }
        catch (e) {
            alert(e.description);
        }
    }

    function Refresh_Click() {
        try {
            parent.RefreshCurrentTab();
        }
        catch (e) {
            alert(e.description);
        }
    }

    function IncorporarDoc_Click() {
        try {
            var doctypeId = document.getElementById('<%=hdnDocTypeId.ClientID %>');
            var docId = document.getElementById('<%=hdnDocId.ClientID %>');

            if (!doctypeId)
                doctypeId = document.getElementById("ctl00_ContentPlaceHolder_TabContainer_TabDocumento_ucDocViewer_hdnDocTypeId");

            if (!docId)
                docId = document.getElementById("ctl00_ContentPlaceHolder_TabContainer_TabDocumento_ucDocViewer_hdnDocId");

            var destinationURL = "../../Views/Insert/Insert.aspx?docid=" + docId.value + "&doctypeid=" + doctypeId.value + "&isview=true";
            $('#IFDialogContent').unbind('load');
            parent.ShowInsertAsociated(destinationURL);
        }
        catch (e) {
            alert(e.description);
        }
    }

    function Download_Click() {
        try {
            var docPath = document.getElementById('<%=hdnFilePath.ClientID %>');
            window.open(docPath.value);
        } catch (e) {
            alert(e.description);
        }
    }
</script>

