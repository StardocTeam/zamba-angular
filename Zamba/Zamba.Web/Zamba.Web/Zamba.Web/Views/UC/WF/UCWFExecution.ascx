<%@ Control Language="C#" AutoEventWireup="true" Inherits="UC_WF_UCWFExecution" CodeBehind="UCWFExecution.ascx.cs" %>
<%@ Reference Control="~/Views/UC/WF/Rules/UCDoRequestData.ascx" %>
<%@ Reference Control="~/Views/UC/WF/Rules/UCDoShowTable.ascx" %>
<%@ Reference Control="~/Views/UC/WF/Rules/UCDoInputIndex.ascx" %>
<%@ Reference Control="~/Views/UC/WF/Rules/UCDoAsk.ascx" %>
<%@ Reference Control="~/Views/UC/WF/Rules/UCDoAskDesition.ascx" %>

<%: Scripts.Render("~/bundles/jqueryAddIns") %>
<%: Scripts.Render("~/bundles/ZScripts") %>
<%: Scripts.Render("~/bundles/Scripts/masterblankScripts") %>
<%: Styles.Render("~/bundles/Styles/masterblankStyles") %>

<script type="text/javascript">
    $(document).ready(function () {
        var btnOk = $('#<%= hdnRuleName.ClientID %>').val();
        if (btnOk != "") {
            SetValidationsAction(btnOk);
        }
    });

    function getHdnChecks() {
        return $("#<%=hdnChecks.ClientID %>");
    }

</script>

<asp:HiddenField ID="hdnCurrTaskID" runat="server" Value="-1" />
<asp:HiddenField ID="hdnRuleName" runat="server" />
<asp:HiddenField ID="hdnMustHideLoading" runat="server" Value="" />
<asp:HiddenField ID="hdnChecks" runat="server" Value="" />
<asp:HiddenField ID="hdnSendDocument" runat="server" Value="false" />


<div class="modal fade" id="openModalIFUcRules" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="position: absolute; margin-top: 5%;">
    <div class="modal-dialog" style="width: 100%; height: 90%; margin-top: 0">

        <div class="modal-content" id="openModalIFContentUcRules">

            <div class="modal-header" style="padding: 1px">
                <button type="button" class="close hidden" data-dismiss="modal"><span aria-hidden="true">Titulo;</span><span class="sr-only">Cerrar</span></button>
                <h5 class="modal-title" id="modalFormTitleUcRules"></h5>
            </div>
            <div class="modal-body" id="modalFormHomeUcRules">
                <div id="pnlUcRules" runat="server" align="center" title=""></div>
            </div>
        </div>
    </div>
</div>

<script type="text/javascript">
    function FixAndPosition(objDlg, pnlRules) {
        objDlg.css("top", "0px");
        objDlg.css("left", "0px");
        $("#pnlUcRules").css("height", "auto");
        objDlg.css("position", "absolute");
        $("body").height($("body div:first").height());
        MakeResize(objDlg, pnlRules);
    }
    $(window).on("load", function () {
        if (!isDashBoardRRHH) {
            if ($("#hdnMustHideLoading").val() != "")
                parent.hideLoading();
        }
        
    });
    function ShowContainer(value) {
        if (parent != document) {
            parent.ShowEntryRulesPanel(value);
        }
    }   
    $(document).ready(function () {
        if (!isDashBoardRRHH) {
            if (parent.ShowLoadingAnimation) parent.ShowLoadingAnimation();
            var btnOk = $('#hdnRuleName').val();
            if (btnOk != null && btnOk != "")
                $("#" + btnOk).click(function () { ShowContainer(false); parent.ShowLoadingAnimation(); });

        }
    });
    

    //function AsociateForm(formid, docid, doctypeid, taskid, continueWithCurrentTasks, dontOpenTaskAfterInsert, fillCommonAttributes, haveSpecificAtt) {
    //    parent.AsociateForm(formid, docid, doctypeid, taskid, continueWithCurrentTasks, dontOpenTaskAfterInsert, fillCommonAttributes, haveSpecificAtt);
    //}
</script>
