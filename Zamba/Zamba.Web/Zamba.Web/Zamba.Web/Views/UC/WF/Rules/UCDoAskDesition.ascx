<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Views_UC_WF_Rules_UCDoAskDesition" Codebehind="UCDoAskDesition.ascx.cs" %>

<script type="text/javascript">
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

<div class="row">
    <asp:Label ID="lblmessage" class="col-xs-offset-1 col-xs-10" name="_message" runat="server" Visible="false"></asp:Label>
    <asp:TextBox ID="txtvalue" class="col-xs-offset-1 col-xs-10" runat="server" TextMode="MultiLine" Style="border: none; overflow: hidden; resize: none; margin-bottom: 30px; user-select: none; height:200px;"></asp:TextBox>
    <div class="style2 col-xs-offset-2 col-xs-8" ">
        <asp:Button ID="_btnok" class="btn btn-primary" Text="Si" runat="server" OnClientClick="hideBtn(this)" OnClick="_btnOk_Click" Style="width: 100px;height:35px" />
        <asp:Button ID="_btnno" class="btn btn-default" Text="No" runat="server" OnClientClick="hideBtn(this)" UseSubmitBehavior="false" OnClick="_btnCancel_Click" Style="width: 100px;height:35px" />
        <asp:Button ID="_btnca" class="btn btn-default" Text="Cancelar" runat="server" OnClientClick="hideBtn(this)" UseSubmitBehavior="false" Style="width: 100px;height:35px" />
    </div>
</div>

