<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WorkFlowList.ascx.cs"
    Inherits="UserControls_WorkFlowList" %>
<table>
    <caption>Listado de Workflows</caption>
    <tr>
        <td>
            <asp:ListBox ID="lstWorkFlow" runat="server" AutoPostBack="True" DataSourceID="odsWorkflowList"
                DataTextField="Name" DataValueField="Work_ID" Font-Names="Verdana"></asp:ListBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:ObjectDataSource ID="odsWorkflowList" runat="server" SelectMethod="GetAllWorkflows"
                TypeName="Zamba.WFBusiness.WFBusiness"></asp:ObjectDataSource>
        </td>
    </tr>
</table>
