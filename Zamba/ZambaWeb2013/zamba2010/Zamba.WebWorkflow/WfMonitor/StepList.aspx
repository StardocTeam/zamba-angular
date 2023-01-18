<%@ Page Language="C#" MasterPageFile="~/ZambaMaster.master" AutoEventWireup="true"
    CodeFile="StepList.aspx.cs" Inherits="StepList" Title="Listado de Etapas" %>

<%@ Register TagPrefix="MyUCs" TagName="StepsList" Src="~/WfMonitor/Controls/TaskSelector/StepsList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="scmMain" runat="server" />
    <table>
        <tr>
            <td align="left">
                <asp:LinkButton ID="lnkWorkgflowList" runat="server" Text="Listado de Workflows" OnClick="lnkWorkgflowList_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel id="upTaskSelector" runat="server">
                    <contenttemplate>

    <MyUCs:StepsList ID="ucStepsList" runat="server" OnForceRefresh="ucStepsList_ForceRefresh" OnSelectedStepChanged="ucStepsList_SelectedStepChanged"  />
    </contenttemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Button ID="btSelect" runat="server" OnClick="btSelect_Click" Text="Seleccionar"
                    BackColor="#EFF3FB" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                    Font-Names="Verdana" Font-Size="X-Small" ForeColor="#284775" />
            </td>
        </tr>
    </table>
</asp:Content>
