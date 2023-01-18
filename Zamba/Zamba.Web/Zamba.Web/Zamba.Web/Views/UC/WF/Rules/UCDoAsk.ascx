<%@ Control Language="C#" AutoEventWireup="true" Inherits="UC_WF_Rules_UCDoAsk" EnableViewState="true" CodeBehind="UCDoAsk.ascx.cs" %>

<style>
    #ctl00_ContentPlaceHolder_UC_WFExecution_ShowDoAsk_txtvalue {
        border-top-color: #337ab7 !important;
        border-bottom-color: #337ab7 !important;
        border-left-color: #337ab7 !important;
        border-right-color: #337ab7 !important;
    }
</style>
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
            <h2>
                <asp:Label ID="lblmessage" name="_message" runat="server" Width="338px"></asp:Label>
            </h2>
        </td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="txtvalue" runat="server" TextMode="MultiLine" Width="550px" Height="150px"
                EnableViewState="true" BorderStyle="Solid"></asp:TextBox>
            <asp:ListBox ID="lbValue" runat="server" Width="330px" Height="150px"></asp:ListBox>
        </td>
    </tr>
    <tr>
        <td>
            <center>
                <asp:Button ID="_btnok" class="btn btn-success" CssClass="btn btn-success" Text="Ok" runat="server" OnClick="_btnOk_Click" Width="97px" UseSubmitBehavior="false" />
                <asp:Button ID="_btnCancel" class="btn btn-warning" CssClass="btn btn-warning" Text="Cancel" runat="server" Width="102px" UseSubmitBehavior="false"
                    OnClick="_btnCancel_Click" />
            </center>

        </td>
    </tr>
</table>


