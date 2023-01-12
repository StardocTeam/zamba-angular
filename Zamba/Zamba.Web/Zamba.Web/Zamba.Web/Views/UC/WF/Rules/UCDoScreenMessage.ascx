<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UC_WF_Rules_UCDoScreenMessage" Codebehind="UCDoScreenMessage.ascx.cs" %>
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
               <asp:Button ID="_btnok" runat="server" CssClass="btn btn-sucess" Text="Aceptar" OnClick="_btnOk_Click"
                     AutopostBack=True />
            </div>
        </td>
    </tr>
</table>

<script type="text/javascript">
</script>

