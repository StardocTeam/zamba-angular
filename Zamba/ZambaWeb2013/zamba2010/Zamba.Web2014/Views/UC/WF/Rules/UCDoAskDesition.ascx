<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCDoAskDesition.ascx.cs"
    Inherits="Views_UC_WF_Rules_UCDoAskDesition" %>

<script type="text/javascript">
    $(document).ready(function () {
        
        SetValidationsAction("<%=_btnno.ClientID %>");
    });
</script>

<table>
    <tr>
        <td>
            <asp:Label ID="lblmessage" name="_message" runat="server" Height="80px" 
                Width="327px" Visible="false"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="style2">
            <asp:TextBox ID="txtvalue" runat="server" TextMode="MultiLine"  Width="500px" Height="150px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="text-align: center " class="style2" >
            <asp:Button ID="_btnok" Text="Si" runat="server" OnClick="_btnOk_Click" Width="97px"/>
            <asp:Button ID="_btnno" Text="No" runat="server" Width="102px" UseSubmitBehavior="false"
                OnClick="_btnCancel_Click" />
        </td>
    </tr>
</table>
