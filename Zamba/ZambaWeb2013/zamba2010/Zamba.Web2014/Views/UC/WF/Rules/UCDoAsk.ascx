<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCDoAsk.ascx.cs" Inherits="UC_WF_Rules_UCDoAsk"  EnableViewState="true"%>

<script type="text/javascript">
    $(document).ready(function () {
        var lb = $("#<%=lbValue.ClientID %>");
        if (lb.length) {
            $(lb).change(function () {
                getHdnChecks().val($(this).val());
            });

            $(lb).click(function () {
                if ($(this).val()) {
                    $('#<%=_btnok.ClientID %>').removeAttr("disabled");
                } else {
                    $('#<%=_btnok.ClientID %>').attr("disabled", "disabled");
                }
            });
        }
        else {
            $('#<%=_btnok.ClientID %>').removeAttr("disabled");
        }
    });
</script>
<table>
    <tr>
        <td>
            <asp:Label ID="lblmessage" name="_message" runat="server" Width="338px"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="txtvalue" runat="server" TextMode="MultiLine"  Width="500px" Height="150px" 
                EnableViewState="true"></asp:TextBox>
            <asp:ListBox ID="lbValue" runat="server" Width="330px" Height="150px">
            </asp:ListBox>
        </td>
    </tr>
    <tr>
        <td  style="text-align: center ">
            <asp:Button ID="_btnok" Text="Ok" runat="server" OnClick="_btnOk_Click" Width="97px" UseSubmitBehavior="false" disabled="disabled" />
            <asp:Button ID="_btnCancel" Text="Cancel" runat="server" Width="102px" UseSubmitBehavior="false"
                OnClick="_btnCancel_Click"/>
        </td>
    </tr>
    </table>


