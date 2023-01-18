<%@ Control Language="C#" AutoEventWireup="false" CodeFile="TaskDetail.ascx.cs" Inherits="TaskDetail" EnableViewState="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Reference Control="~/Views/UC/Viewers/FormBrowser.ascx" %>
<%@ Reference Control="~/Views/UC/Viewers/DocViewer.ascx" %>

<div id="tabstaskheader" class="Table-Task-Detail">
    <asp:HiddenField runat="server" ID="hidLastTab" />
    <asp:HiddenField runat="server" ID="HiddenTaskId" />
    <asp:HiddenField runat="server" ID="HiddenDocID" />
    <asp:HiddenField runat="server" ID="HiddenCurrentFormID" />

    <%if (TaskResult != null) {%>
    <div id="tbDoc">
        <asp:Panel runat="server" ID="pnlViewer">
        </asp:Panel>
    </div>
    <%}%>
</div>

<script type="text/javascript">
    function setHeight(strName, value) {
        var WFIframe;
        switch (strName) {
            case "Forum":
                WFIframe = $("#ctl00_ContentPlaceHolder_ucTaskDetail_forumiframe");
                break;
        }

        WFIframe.height(value);
    }

    function RefreshIframe(tabName, url, oneLoad) {
        var WFIframe;

        switch (tabName) {
            case "Foro":
                WFIframe = $("#ctl00_ContentPlaceHolder_ucTaskDetail_forumiframe");
                break;
        }
        //Primero se setea en en página en blanco para que tome el refresco.
        if (WFIframe != null && WFIframe[0] != null) {
            if (oneLoad && WFIframe[0].contentWindow.location != "about:blank")
                return;
            ShowLoadingAnimation();
            WFIframe[0].contentWindow.location = "about:blank";
            WFIframe[0].contentWindow.location = url;
        }
        else {
            if (WFIframe.context != null) {
                if (oneLoad && WFIframe[0].contentWindow.location != "about:blank")
                    return;
                ShowLoadingAnimation();
                WFIframe[0].contentWindow.location = "about:blank";
                WFIframe.context.location = url;
            }
        }
    }

    function getTabDetH() {
        return $("#docToolbarUl").height();
    }
</script>