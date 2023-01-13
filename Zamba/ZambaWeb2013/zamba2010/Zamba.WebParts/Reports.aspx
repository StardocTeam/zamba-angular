<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.Master" CodeFile="Reports.aspx.cs" Inherits="Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        VENCIMIENTO<br />
        <br />
        <a href="UserControls/UCTaskToExpire/UCTaskToExpireByStep.aspx" target="_blank">Tareas por Vencer de una Etapa</a><br />
        <br />
        <a href="UserControls/UCTaskToExpire/UCTaskToExpireByUser.aspx" target="_blank">Tareas por Vencer por Usuario</a><br />
        <br />
        <a href="UserControls/UCTaskToExpire/UCTaskToExpireByWorkflow.aspx" target="_blank">Tareas por Vencer de un WorkFlow</a><br />
        <br />
        <br />
        ASIGNACION<br />
        <br />
        <a href="UserControls/UCUsersAsigned/UCUsersAsignedByStep.aspx" target="_blank">Asignacion de Tareas por Etapa</a><br />
        <br />
        <a href="UserControls/UCUsersAsigned/UCUsersAsignedByWorkflow.aspx" target="_blank">Asignacion de Tareas por WorkFlow</a><br />
        <br />
        <a href="UserControls/UCTaskBalances/StepBalances.aspx" target="_blank">Balance de Tareas por Etapas</a><br />
        <br />
        <a href="UserControls/UCTaskBalances/UserBalances.aspx" target="_blank">Balance de Tareas por Usuario</a><br />
        <br />
        <a href="UserControls/UCTaskBalances/WfBalances.aspx" target="_blank">Balance de Tareas por WorkFlow</a><br />
        <br />
        <br />
        PERFORMANCE<br />
        <br />
        <a href="UserControls/UCStepsPerformance/UCStepsPerformanceByStep.aspx" target="_blank">Performance por Etapas</a><br />
        <br />
        <a href="UserControls/UCStepsPerformance/UCStepsPerformanceByWorkflow.aspx" target="_blank">Performance por WorkFlow</a><br />
        <br />
        <br />
        PROMEDIOS<br />
        <br />
        <a href="UserControls/UCAverageTimeInSteps/UCAverageTimeInStepsByWorkflow.aspx" target="_blank">Tiempo promedio de cumplimiento de etapa por WorkFlow</a><br />
        <br />
        <a href="UserControls/UCAverageTimeInSteps/AverageTimeInStepsByStep.aspx" target="_blank">Tiempo promedio de cumplimiento de etapa por etapa</a><br />
        <br />
        <a href="UserControls/UCAverageTimeInSteps/AverageTimeInStepByUser.aspx" target="_blank">Tiempo promedio de cumplimiento de etapa por usuario</a>
        </div>
</asp:Content>
