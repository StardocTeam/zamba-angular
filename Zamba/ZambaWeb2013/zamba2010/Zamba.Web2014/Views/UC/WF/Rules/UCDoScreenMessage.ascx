<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCDoScreenMessage.ascx.cs"
    Inherits="UC_WF_Rules_UCDoScreenMessage" %>
<table>
    <tr>
        <td>
            <div id="_message" runat="server">
            </div>
        </td>
    </tr>
    <tr>
        <td style="align-content:center">
            <div style="padding:10px">
               <asp:Button ID="_btnok" runat="server" Text="Aceptar" OnClick="_btnOk_Click"
                    UseSubmitBehavior="false" />
            </div>
        </td>
    </tr>
</table>

<script type="text/javascript">
</script>

