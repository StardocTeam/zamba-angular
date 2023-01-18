<%@ Control Language="C#" AutoEventWireup="true" Inherits="Views_UC_Task_TestContro" Codebehind="TestContro.ascx.cs" %>
<script type="text/javascript">
    $(document).ready(function () {
        
        $("#tabstaskheader").zTabs({
            activate: function (event, ui) {
                var tabText = ui.newTab[0].innerText;

                switch (tabText) {
                    case "Historial":
                        RefreshTaskGrid(tabText, "~/Views/WF/TaskDetails/TaskHistory.aspx?TaskId=" + taskId + "&DocID=" + docId, true);
                        break;
                    case "Foro":
                        RefreshTaskGrid(tabText, "~/Views/WF/TaskDetails/TaskForum.aspx?TaskId=" + taskId + "&DocID=" + docId, true);
                        break;
                    case "Asociados":
                        RefreshTaskGrid(tabText, "~/Views/WF/TaskDetails/TaskAsociated.aspx?TaskId=" + taskId + "&DocID=" + docId, true);
                        break;
                    case "Mails":
                        RefreshTaskGrid(tabText, "~/Views/WF/TaskDetails/TaskMailhistory.aspx?TaskId=" + taskId, true);
                        break;
                }
            }
        });
    });

    function RefreshTaskGrid(tabName, url, oneLoad) {
        alert("refresh");
    }
    </script>


<div id="tabstaskheader" class="Table-Task-Detail" style="height:100%">

        <ul id="docToolbarUl">
            <li id="liViewer"><a href="#tbDoc">Documento</a></li>
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

        <div id="divHistory">
            <iframe src="about:blank" frameborder="0"
                    runat="server" id="historyiframe" style="height:580px; overflow-y:visible" scrolling="no" enableviewstate="true"></iframe>
        </div>
        
        <div id="divForum">
            <iframe src="about:blank" frameborder="0"
                    runat="server" id="forumiframe" style="height:500px; overflow-y:visible;" scrolling="no" enableviewstate="true"></iframe>
        </div> 
        
       <div id="divAsociated">
            <iframe src="about:blank" frameborder="0"
                    runat="server" id="asociatediframe" style="height:500px; overflow-y:visible" scrolling="no" enableviewstate="true"></iframe>
        </div> 
        
        <div id="divMails">
            <iframe src="about:blank" frameborder="0"
                    runat="server" id="mailsiframe" style="height:500px; overflow-y:visible" scrolling="yes" enableviewstate="true"></iframe>
        </div> 

        <div id="tbDoc">              
            test            
        </div>
</div>