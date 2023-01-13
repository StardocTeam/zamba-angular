<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TasksHistory.aspx.cs" Inherits="RequestAction_TasksHistory" %>

<%@ Register Src="~/RequestAction/Controls/ListadoTareasTerminadas.ascx" TagPrefix="MyUcs"
    TagName="FinishedTaskList" %>
<%@ Register Src="~/RequestAction/Controls/TaskInformation.ascx" TagPrefix="MyUcs"
    TagName="TaskInformation" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Historial de tareas</title>
</head>
<body>
    <form id="frmMain" runat="server">
    <asp:ScriptManager ID="smMain" runat="server" />
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <MyUcs:FinishedTaskList ID="UcTasks" runat="server" OnSelectedTaskChanged="UcTasks_SelectedTaskChanged" />
            <MyUcs:TaskInformation ID="UcTaskInformation" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:LinkButton ID="lnkViewRequest" runat="server" Text="Volver" 
        OnClick="lnkViewRequest_Click" />
    </form>
</body>
</html>
