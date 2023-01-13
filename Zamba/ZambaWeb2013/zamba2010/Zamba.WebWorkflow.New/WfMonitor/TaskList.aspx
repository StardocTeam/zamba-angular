<%@ Page Language="C#" MasterPageFile="~/ZambaMaster.master"  AutoEventWireup="true"
    CodeFile="TaskList.aspx.cs" Inherits="TaskList" Title="Listado de Tareas" %>

<%@ Register TagPrefix="MyUCs" TagName="TasksList" Src="~/WfMonitor/Controls/TaskSelector/TasksList.ascx"%>
<%@ Register TagPrefix="MyUCs" TagName="TaskActions" Src="~/WfMonitor/Controls/TaskActions.ascx" %>
<%@ Register TagPrefix="MyUCs" TagName="TasksInformation" Src="~/WfMonitor/Controls/TaskInformation/TaskInformation.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="scmMain" runat="server" />
    <table>
        <tr>
            <td align="left">
                <asp:LinkButton ID="lnkWorkflowList" runat="server" Text="Listado de Workflows" OnClick="lnkWorkflowList_Click" />
            </td>
            <td align="right">
                <asp:LinkButton ID="lnkStepList" runat="server" Text="Listado de Etapas" OnClick="lnkStepList_Click" />
            </td>
        </tr>
    </table>
    <asp:UpdatePanel id="upTaskSelector" runat="server">
        <contenttemplate>
          <table>
                <tr>
                    <td >
                        <MyUCs:TasksList ID="ucTaskList" runat="server" 
                         OnSelectedTasksChanged="ucTaskList_SelectedTasksChanged" OnForceRefresh="ucTaskList_ForceRefresh"
                                    OnSelectedWorkflowChanged="ucTaskList_SelectedWorkflowChanged" 
                                    OnSelectedTaskChanged="ucTaskList_SelectedTaskChanged" />
                    </td>
                </tr>
                <tr>
                    <td >
                        <MyUCs:TaskActions ID="ucTaskActions" runat="server" OnDoAssign="ucTaskActions_DoAssign"
                                OnStepsChanged="ucTaskActions_StepsChanged" OnTasksChanged="ucTaskActions_TasksChanged" />
                    </td>
                </tr>
                <tr>
                    <td > 
                        <MyUCs:TasksInformation ID="ucTasksInformation" runat="server" />
                    </td>
                </tr>
            </table>
        </contenttemplate>
    </asp:UpdatePanel>
</asp:Content>
