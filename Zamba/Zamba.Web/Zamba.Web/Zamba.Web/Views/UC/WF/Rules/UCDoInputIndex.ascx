<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UC_WF_Rules_UCDoInputIndex" Codebehind="UCDoInputIndex.ascx.cs" %>
<table>
    <tr>
        <td>
            <center>
                <asp:Label ID="lblmessage" name="_message" runat="server"></asp:Label>
            </center>
        </td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="txtvalue" runat="server" TextMode="MultiLine" Width="330px" 
                Height="150px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="padding: 10px 10px 10px 10px" align="center">
            <asp:Button ID="_btnok" Text="Ok" runat="server" OnClick="_btnOk_Click" Width="97px" />
            <asp:Button ID="_btnCancel" Text="Cancel" runat="server" OnClick="_btnCancel_Click" Width="102px" UseSubmitBehavior="false" />
        </td>
    </tr>
</table>
