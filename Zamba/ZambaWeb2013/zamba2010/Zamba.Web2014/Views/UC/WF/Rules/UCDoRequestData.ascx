<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCDoRequestData.ascx.cs"
    Inherits="UC_WF_Rules_UCDoRequestData" %>

<style>

#ui-datepicker-div
{
    z-index:1002 !important;
}

span
{
    white-space:nowrap;
}

</style>

<asp:HiddenField runat="server" ID="hddocId" />
<asp:HiddenField runat="server" ID="hdDTId" />
<table style="text-align: left">
    <tr>
        <td>
		    <asp:Panel ID="DoRequestDataIndexes" runat="server"  style="overflow:scroll;" >
			    <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">--%>
				    <%--<ContentTemplate>--%>
					    <asp:Table ID="tblIndices" runat="server" CellPadding="0" EnableViewState="true"
						    Style="height: 100%;"/>
				    <%--</ContentTemplate>--%>
			    <%--</asp:UpdatePanel>--%>
		    </asp:Panel>
        </td>
    </tr>
    <tr>
        <td>
            <div style="text-align: center; padding: 10px 10px 10px 10px">
                <asp:Button ID="_btnok" Text="Aceptar" runat="server" Width="97px" OnClick="_btnOk_Click"
                    UseSubmitBehavior="false" />
                <asp:Button ID="_btnCancel" Text="Cancelar" runat="server" Width="102px" UseSubmitBehavior="false"
                    OnClick="_btnCancel_Click" />
            </div>
        </td>
    </tr>
</table>
