<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default"
    MasterPageFile="~/ZambaMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register TagPrefix="MyUCs" TagName="TaskSelector" Src="~/Controls/TaskSelector/TaskSelector.ascx" %>
<%@ Register TagPrefix="MyUCs" TagName="TaskActions" Src="~/Controls/TaskActions.ascx" %>
<%@ Register TagPrefix="MyUCs" TagName="TasksInformation" Src="~/Controls/TaskInformation/TaskInformation.ascx" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<asp:ScriptManager ID="scmMain" runat="server" />
    <asp:UpdatePanel id="upTaskSelector" runat="server">
  	  <ContentTemplate>
		<MyUCs:TaskSelector id="ucTaskSelector" runat="server" OnSelectedTasksChanged="ucTaskSelector_SelectedTasksChanged" OnSelectedWorkflowChanged="ucTaskSelector_SelectedWorkflowChanged" OnSelectedTaskChanged="ucTaskSelector_SelectedTaskChanged" />
		<MyUCs:TaskActions id="ucTaskActions" runat="server" OnDoAssign="ucTaskActions_DoAssign" OnStepsChanged="ucTaskActions_StepsChanged" OnTasksChanged="ucTaskActions_TasksChanged" />
		<MyUCs:TasksInformation id="ucTasksInformation" runat="server" />
	  </ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>
