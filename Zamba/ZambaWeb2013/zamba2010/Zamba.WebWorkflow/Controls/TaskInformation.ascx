<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/Controls/TaskInformation.ascx.cs"
    Inherits="TaskInformation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="MyUCs" TagName="History" Src="~/Controls/History.ascx" %>
<%@ Register TagPrefix="MyUCs" TagName="Indexs" Src="~/Controls/Indexs.ascx" %>
<%@ Register TagPrefix="MyUCs" TagName="Task" Src="~/Controls/Task.ascx" %>
<asp:HiddenField ID="hfTaskId" runat="server" />
<asp:HiddenField ID="hfTaskIds" runat="server" />
<cc1:TabContainer ID="tbcTasksInformation" runat="server"  Font-Size="XX-Small" BackColor="transparent">
    <cc1:TabPanel runat="server" ID="tbpHistory" HeaderText="Historial">
        <ContentTemplate>
            <MyUCs:History ID="ucHistory" runat="server" />
        </ContentTemplate>
    </cc1:TabPanel>
    <cc1:TabPanel runat="server" ID="tbpIndexs" HeaderText="Indices" >
        <ContentTemplate>
            <MyUCs:Indexs ID="ucIndexs" runat="server" />
        </ContentTemplate>
    </cc1:TabPanel>
    <cc1:TabPanel runat="server" ID="tbpTask" HeaderText="Información" >
        <ContentTemplate>
            <MyUCs:Task ID="ucTask" runat="server" />
        </ContentTemplate>
    </cc1:TabPanel>
</cc1:TabContainer>
