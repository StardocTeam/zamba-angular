<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Test" MasterPageFile="~/MasterBlankpage.Master" %>


<asp:Content ID="Content5" runat="server" ContentPlaceHolderID="ContentPlaceHolder">
    <script type="text/javascript">
    </script>


    <div id="tabstaskheader" class="Table-Task-Detail">

        <ul id="taskheader_detail_ul" class="nav nav-tabs">
            <li id="liViewer" class="active"><a href="#tbDoc">Documento</a></li>
            <li id="liHistory"><a href="#divHistory">Historial</a></li>
            <li id="liForum"><a href="#divForum">Foro</a></li>
            <li id="liAsociated"><a href="#divAsociated">Asociados</a></li>
            <li id="liMails"><a href="#divMails">Mails</a></li>
        </ul>
        <asp:HiddenField ID="hdnShowHistoryTab" runat="server" />
        <asp:HiddenField ID="hdnShowForumTab" runat="server" />
        <asp:HiddenField ID="hdnShowAsociatedTab" runat="server" />
        <asp:HiddenField ID="hdnShowMailsTab" runat="server" />
        <asp:HiddenField runat="server" ID="hidLastTab" />
        <asp:HiddenField runat="server" ID="HiddenTaskId" />
        <asp:HiddenField runat="server" ID="HiddenDocID" />
        <asp:HiddenField runat="server" ID="HiddenCurrentFormID" />
        <div class="tab-content">
            <div id="divHistory" class="tab-pane">
                <iframe src="about:blank" frameborder="0"
                    runat="server" id="historyiframe" style="height: 580px; overflow-y: visible" scrolling="no" enableviewstate="true"></iframe>
            </div>

            <div id="divForum" class="tab-pane">
                <iframe src="about:blank" frameborder="0"
                    runat="server" id="forumiframe" style="height: 500px; overflow-y: visible;" scrolling="no" enableviewstate="true"></iframe>
            </div>

            <div id="divAsociated" class="tab-pane">
                <iframe src="about:blank" frameborder="0"
                    runat="server" id="asociatediframe" style="height: 500px; overflow-y: visible" scrolling="no" enableviewstate="true"></iframe>
            </div>

            <div id="divMails" class="tab-pane">
                <iframe src="about:blank" frameborder="0"
                    runat="server" id="mailsiframe" style="height: 500px; overflow-y: visible" scrolling="yes" enableviewstate="true"></iframe>
            </div>

            <div id="tbDoc" class="tab-pane">
                test            
            </div>
        </div>
    </div>

</asp:Content>
