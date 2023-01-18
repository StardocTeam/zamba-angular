<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Monitor.aspx.cs" Inherits="Monitor"
    MasterPageFile="~/ZambaMaster.master" %>
                 
<%@ Register TagPrefix="MyUCs" TagName="TaskSelector" Src="~/WfMonitor/Controls/TaskSelector/TaskSelector.ascx" %>
<%@ Register TagPrefix="MyUCs" TagName="TaskActions" Src="~/WfMonitor/Controls/TaskActions.ascx" %>
<%@ Register TagPrefix="MyUCs" TagName="TasksInformation" Src="~/WfMonitor/Controls/TaskInformation/TaskInformation.ascx" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                 
    <script type="text/javascript" src="js/prototype.js"></script>

    <script type="text/javascript" src="js/scriptaculous.js?load=effects"></script>

    <script type="text/javascript" src="js/lightwindow.js"></script>

    <asp:UpdateProgress ID="upgMain" AssociatedUpdatePanelID="upTaskSelector" runat="server"
        DisplayAfter="0">
        <ProgressTemplate>
            <div style="color: Navy; font-size: x-small; text-align: center">
                Espere un momento.
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="upTaskSelector" runat="server">
        <ContentTemplate>
            <div style="width: 100%">
                <MyUCs:TaskSelector ID="ucTaskSelector" runat="server" OnSelectedTasksChanged="ucTaskSelector_SelectedTasksChanged"
                    OnSelectedWorkflowChanged="ucTaskSelector_SelectedWorkflowChanged" OnSelectedTaskChanged="ucTaskSelector_SelectedTaskChanged" />
                <MyUCs:TaskActions ID="ucTaskActions" runat="server" OnDoAssign="ucTaskActions_DoAssign"
                    OnStepsChanged="ucTaskActions_StepsChanged" OnTasksChanged="ucTaskActions_TasksChanged" />
                <MyUCs:TasksInformation ID="ucTasksInformation" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
