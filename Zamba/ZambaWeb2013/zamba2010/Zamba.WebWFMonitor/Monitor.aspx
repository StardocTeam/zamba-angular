<%@ Page Language="VB" EnableViewStateMac="false" MasterPageFile="MasterPage.master"
    AutoEventWireup="false" CodeFile="Monitor.aspx.vb" Inherits="_Monitor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/WorkFlowList.ascx" TagName="WUC_WFList" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/StepList.ascx" TagName="WUC_WFSteps" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/TaskList.ascx" TagName="WUC_WFTasks" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server"></asp:Label></td>
            <td>
                <asp:Button ID="btCalendario" runat="server" Text="Ver Calendario" />
                <asp:Button ID="btInformes" runat="server" Text="Ver Informes" Width="105px" Font-Size="X-Small" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="True">
                </asp:ScriptManager>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upWorkflows">
                    <progresstemplate>
                        <asp:Label ID="lbCargandoWF" runat="server" Text="Cargando ..."></asp:Label>
                    </progresstemplate>
                </asp:UpdateProgress>
            </td>
            <td>
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="upEtapas">
                    <progresstemplate>
                        <asp:Label ID="lbCargandoEtapa" runat="server" Text="Cargando ..."></asp:Label>
                    </progresstemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:UpdatePanel ID="upWorkflows" runat="server">
                    <contenttemplate>
                        <table>
                            <tr>
                                <td>
                                    <uc1:WUC_WFList ID="WUC_WFList1" runat="server"></uc1:WUC_WFList>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upEtapas" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <uc2:WUC_WFSteps ID="WUC_WFSteps1" runat="server"></uc2:WUC_WFSteps>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </contenttemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <uc3:WUC_WFTasks ID="UCTareas" runat="server"></uc3:WUC_WFTasks>
    <asp:HiddenField ID="HiddenField2" runat="server" />
</asp:Content>
