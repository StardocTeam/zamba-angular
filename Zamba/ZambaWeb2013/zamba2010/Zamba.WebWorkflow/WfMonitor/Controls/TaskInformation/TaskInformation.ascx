<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/WfMonitor/Controls/TaskInformation/TaskInformation.ascx.cs"
    Inherits="TaskInformation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="MyUCs" TagName="History" Src="~/WfMonitor/Controls/TaskInformation/History.ascx" %>
<%@ Register TagPrefix="MyUCs" TagName="Indexs" Src="~/WfMonitor/Controls/TaskInformation/Indexs.ascx" %>
<%@ Register TagPrefix="MyUCs" TagName="Task" Src="~/WfMonitor/Controls/TaskInformation/Task.ascx" %>
<%@ Register TagPrefix="MyUCs" TagName="AsignedDocuments" Src="~/WfMonitor/Controls/TaskInformation/AsignedDocuments.ascx.ascx" %>
<asp:HiddenField ID="hfTaskId" runat="server" />
<div id="TaskInformation">
    <ajaxToolkit:TabContainer ID="tbcTasksInformation" runat="server">
        <ajaxToolkit:TabPanel runat="server" ID="tbpHistory" HeaderText="Historial">
            <ContentTemplate>
                <MyUCs:History ID="ucHistory" runat="server" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="tbpIndexs" HeaderText="Indices">
            <ContentTemplate>
                <MyUCs:Indexs ID="ucIndexs" runat="server" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="tbpTask" HeaderText="Información">
            <ContentTemplate>
                <MyUCs:Task ID="ucTask" runat="server" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
    <div>
