<%@ control language="C#" autoeventwireup="true"
    inherits="Views_UC_WF_Rules_UCDoAskDesition" codebehind="UCDoAskDesition.ascx.cs" %>
<link type="text/css" href="../../Content/css/styleForRules.css?v241" rel="stylesheet" />
<script type="text/javascript">
    var timerFixSizeTxtValue
    $(document).ready(function () {
        try {
            SetValidationsAction("<%=_btnno.ClientID %>");
        } catch (e) {

        }
    });
    function hideBtn(btn) {
        $(btn).parent().hide();
    }
</script>
<div class="col-xs-2"></div>
<div class="col-xs-8">
    <div class="row panel panel-default panelBodyStyle">
        <div class="panel-body">
            <asp:label id="lblmessage" class="col-xs-offset-1 col-xs-10" name="_message" runat="server" visible="false"></asp:label>
            <h1>
                <asp:textbox id="txtvalue" rows="4" class="col-xs-offset-1 col-xs-10 text-center fix-size txtvalueOfDoAskDesition" runat="server" textmode="MultiLine"></asp:textbox>
            </h1>
            <div class="row">
                <div class="col-xs-2"></div>
                <div class="col-xs-8">
                    <asp:button id="_btnok" class="btn btn-primary" text="Si" tooltip="Si" runat="server" onclientclick="hideBtn(this)" onclick="_btnOk_Click" style="color: White; background-color: #42BD3E; height: 35px; width: 102px;" />
                    <asp:button id="_btnno" class="btn btn-default" text="No" tooltip="No" runat="server" onclientclick="hideBtn(this)" usesubmitbehavior="false" onclick="_btnCancel_Click" style="width: 100px; background-color: #ff5722; color: white" />
                    <asp:button id="_btnca" class="btn btn-primary" tooltip="Cancelar" text="Cancelar" runat="server" onclientclick="hideBtn(this)" usesubmitbehavior="false" style="width: 100px;" />
                </div>
                <div class="col-xs-2"></div>
            </div>
        </div>
    </div>
</div>
<div class="col-xs-2"></div>




