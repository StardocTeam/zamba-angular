<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WUC_WFList.ascx.vb" Inherits="WUC_WFList" %>
<table>
    <tr>
        <td>
            <asp:Label ID="lbTitulo" runat="server" Text="Listado de Workflows"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:ListBox ID="lstWorkFlow" runat="server" AutoPostBack="True" Height="150px" Width="296px"
                DataSourceID="odsWorkflowList" DataTextField="Name" DataValueField="Work_ID" Font-Names="Verdana"
                Font-Size="X-Small"></asp:ListBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:ObjectDataSource ID="odsWorkflowList" runat="server" SelectMethod="GetAllWorkflows"
                TypeName="Zamba.WFBusiness.WFBusiness"></asp:ObjectDataSource>
        </td>
    </tr>
</table>
