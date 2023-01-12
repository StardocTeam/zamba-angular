<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCWFExecution.ascx.cs" Inherits="UC_WF_UCWFExecution" %>
<%@ Reference Control="~/Views/UC/WF/Rules/UCDoRequestData.ascx" %>
<%@ Reference Control="~/Views/UC/WF/Rules/UCDoShowTable.ascx" %>
<%@ Reference Control="~/Views/UC/WF/Rules/UCDoScreenMessage.ascx" %>
<%@ Reference Control="~/Views/UC/WF/Rules/UCDoInputIndex.ascx" %>
<%@ Reference Control="~/Views/UC/WF/Rules/UCDoAsk.ascx" %>
<%@ Reference Control="~/Views/UC/WF/Rules/UCDoAskDesition.ascx" %>

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

<asp:HiddenField ID="hdnRuleName" runat="server" />
<asp:HiddenField ID="hdnChecks" runat="server" value=""/>


         <div class="modal fade" id="openModalIFUcRules" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content" id="openModalIFContentUcRules">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                            <h4 class="modal-title" id="modalFormTitleUcRules"></h4>
                        </div>
                        <div class="modal-body" id="modalFormHomeUcRules">
                           <div id="pnlUcRules" runat="server" style="width:600px;" align="center" title="Zamba Software..."></div><%--display:none; overflow:hidden; --%>
                        </div>
                    </div>
                </div>
            </div>