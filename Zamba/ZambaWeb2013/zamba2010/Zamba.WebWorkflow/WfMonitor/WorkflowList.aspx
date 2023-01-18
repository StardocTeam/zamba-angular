<%@ Page Language="C#" MasterPageFile="~/ZambaMaster.master" AutoEventWireup="true"
    CodeFile="WorkflowList.aspx.cs" Inherits="WorkflowList2" Title="Listado de Workflows" %>

<%@ Register TagPrefix="MyUCs" TagName="WorkflowList" Src="~/WfMonitor/Controls/TaskSelector/WorkflowList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="scmMain" runat="server" ></asp:ScriptManager>
    <table>
        <tr>
            <td>
                <asp:UpdatePanel id="upTaskSelector" runat="server">
                    <contenttemplate>
    <MyUCs:WorkflowList ID="ucWfList" runat="server" OnSelectedWorkflowChanged="ucWfList_SelectedWorkflowsChanged" />
    </contenttemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Button ID="btSelect" runat="server" OnClick="btSelect_Click" Text="Seleccionar"
                    BackColor="#EFF3FB" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                    Font-Names="Verdana" Font-Size="X-Small" ForeColor="#284775" /></td>
        </tr>
    </table>
</asp:Content>
