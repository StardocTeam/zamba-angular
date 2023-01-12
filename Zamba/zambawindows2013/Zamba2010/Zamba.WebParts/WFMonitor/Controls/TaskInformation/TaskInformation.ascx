<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/Controls/TaskInformation/TaskInformation.ascx.cs"
    Inherits="TaskInformation" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>--%>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register TagPrefix="MyUCs" TagName="History" Src="~/Controls/TaskInformation/History.ascx" %>
<%@ Register TagPrefix="MyUCs" TagName="Indexs" Src="~/Controls/TaskInformation/Indexs.ascx" %>
<%@ Register TagPrefix="MyUCs" TagName="Task" Src="~/Controls/TaskInformation/Task.ascx" %>
<%@ Register TagPrefix="MyUCs" TagName="AsignedDocuments" Src="~/Controls/TaskInformation/AsignedDocuments.ascx.ascx" %>
<asp:HiddenField ID="hfTaskId" runat="server" />
<ajaxToolkit:TabContainer  ID="tbcTasksInformation" runat="server" OnClientActiveTabChanged>
    <%-- <cc1:TabPanel runat="server" ID="tbpAsociatedDocuments">
        <ContentTemplate>
            <MyUCs:AsignedDocuments ID="ucAsociatedDocuments" runat="server" />
        </ContentTemplate>
    </cc1:TabPanel>--%>
    <ajaxToolkit:TabContainer  runat="server" ID="tbpHistory" HeaderText="Historial">
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
