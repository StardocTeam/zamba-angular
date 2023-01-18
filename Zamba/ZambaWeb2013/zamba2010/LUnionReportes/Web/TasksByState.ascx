<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TasksByState.ascx.cs"
    Inherits="Web.TasksByState" %>
<%@ Register Src="~/WFFilters.ascx" TagName="Filters" TagPrefix="Filters" %>
<div class="design">
    <span class="title2">Tareas por estado</span>
</div>
<div onclick="Imprimir_Click1('<%=Chart1.ClientID  %>')" style="height: 100%; cursor: pointer;
    width: 100%;">
    <img id="lnkImprimir" src="Content/Images/print.png" alt="Imprimir documento" style="height: 16px" />
    <asp:Label ID="lblImprimir" Text="" runat="server" ToolTip="Imprimir documento"
        Height="90%"></asp:Label>
</div>
<Filters:Filters runat="server" ID="Filter"></Filters:Filters>
<asp:Chart ID="Chart1" runat="server" DataSourceID="ZSQL">
    <Series>
        <asp:Series Name="Series1" ChartType="Pie" XValueMember="Estado" YValueMembers="Cantidad">
        </asp:Series>
    </Series>
    <ChartAreas>
        <asp:ChartArea Name="ChartArea1">
        </asp:ChartArea>
    </ChartAreas>
</asp:Chart>
<asp:SqlDataSource ID="ZSQL" runat="server" ConnectionString="<%$ ConnectionStrings:ZambaConnectionString %>"
    SelectCommand="SELECT COUNT(1) AS Cantidad, USRTABLE.NAME AS Usuario, DOC_TYPE.DOC_TYPE_NAME AS Entidad, WFStepStates.Name AS Estado, WFWorkflow.Name AS Proceso, WFStep.Name AS Etapa FROM USRTABLE RIGHT OUTER JOIN DOC_TYPE INNER JOIN WFStepStates INNER JOIN WFStep ON WFStepStates.Step_Id = WFStep.step_Id INNER JOIN WFWorkflow ON WFStep.work_id = WFWorkflow.work_id INNER JOIN WFDocument ON WFStepStates.Doc_State_ID = WFDocument.Do_State_ID ON DOC_TYPE.DOC_TYPE_ID = WFDocument.DOC_TYPE_ID ON USRTABLE.ID = WFDocument.User_Asigned WHERE ([WFWorkflow].[work_id] = @work_id) and ([WFStep].[step_id] in (@step_id)) and ([WFDocument].[Do_State_ID] in (@Doc_State_ID)) GROUP BY USRTABLE.NAME, DOC_TYPE.DOC_TYPE_NAME, WFStepStates.Name, WFWorkflow.Name, WFStep.Name ">
    <SelectParameters>
        <asp:ControlParameter ControlID="DDWF" DefaultValue="0" Name="work_id" PropertyName="SelectedValue"
            Type="Decimal" />
        <asp:ControlParameter ControlID="CHKLSTEPS" DefaultValue="0" Name="step_id" PropertyName="SelectedValue"
            Type="Decimal" />
        <asp:ControlParameter ControlID="CHKLSTATES" DefaultValue="0" Name="Doc_State_ID"
            PropertyName="SelectedValue" Type="Decimal" />
    </SelectParameters>
</asp:SqlDataSource>
