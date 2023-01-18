<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TaskSelector.ascx.cs"
    Inherits="TaskSelector" %>
<%@ Register TagPrefix="MyUCs" TagName="WorkflowList" Src="~/WfMonitor/Controls/TaskSelector/WorkflowList.ascx" %>
<%@ Register TagPrefix="MyUCs" TagName="TasksList" Src="~/WfMonitor/Controls/TaskSelector/TasksList.ascx" %>
<%@ Register TagPrefix="MyUCs" TagName="StepsList" Src="~/WfMonitor/Controls/TaskSelector/StepsList.ascx" %>
<table width="100%">
    <tr>
        <td width="33%" height="100%" valign="top">
            <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="100%" Width="100%">
                <MyUCs:WorkflowList ID="ucWfList" runat="server" OnSelectedWorkflowChanged="ucWfList_SelectedWorkflowsChanged" />
            </asp:Panel>
        </td>
        <td width="33%" height="100%" valign="top">
            <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="100%" Width="100%">
                <MyUCs:StepsList ID="ucStepsList" runat="server" OnSelectedStepChanged="ucStepsList_SelectedStepsChanged"
                    OnForceRefresh="ucStepsList_ForceRefresh" />
            </asp:Panel>
        </td>
        <td width="33%" height="100%" valign="top">
            <asp:Panel ID="Panel3" runat="server" ScrollBars="Auto" Height="100%" Width="100%">
                <MyUCs:TasksList ID="ucTaskList" runat="server" OnSelectedTasksChanged="ucTaskList_SelectedTasksChanged"
                    OnSelectedWorkflowChanged="ucTaskList_SelectedWorkflowChanged" OnForceRefresh="ucTaskList_ForceRefresh"
                    OnSelectedTaskChanged="ucTaskList_SelectedTaskChanged" />
            </asp:Panel>
        </td>
    </tr>
</table>
