<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WUC_WFSteps.ascx.vb"
    Inherits="WUC_WFSteps" %>
<table>
    <tr>
        <td>
            <asp:Label ID="lbTitulo" runat="server" Text="Listado de Etapas"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:ListBox ID="lstWF" runat="server" AutoPostBack="True" Height="150px" Width="256px"
                DataSourceID="odsStepList" DataTextField="Name" DataValueField="Step_Id"
                Font-Names="Verdana" Font-Size="X-Small"></asp:ListBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:ObjectDataSource ID="odsStepList" runat="server" SelectMethod="GetDsSteps"
                TypeName="Zamba.WFBusiness.WFStepBussines">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="-1" Name="WFId" SessionField="WfId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
    </tr>
</table>