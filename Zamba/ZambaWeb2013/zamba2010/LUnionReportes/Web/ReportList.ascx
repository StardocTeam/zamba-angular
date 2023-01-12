<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportList.ascx.cs"
    Inherits="Web.ReportList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

       <div style="padding-right:12px; width:auto">
    <telerik:RadTreeView ID="RadTreeView2" runat="server" CheckBoxes="True" 
        Skin="Office2007" OnNodeCheck="RadTreeView2_NodeCheck"  >
        <Nodes>
            <telerik:RadTreeNode Text="Asignacion" Expanded="true">
                <Nodes>
                    <telerik:RadTreeNode Text="Distribucion de tareas por Usuario" Value="UserDistribution" Category="Report" />
                    <telerik:RadTreeNode Text="Distribucion de tareas por Etapa" Value="TasksByWorkflow" Category="Report" />
                </Nodes>
            </telerik:RadTreeNode>
            <telerik:RadTreeNode Text="Reclamos">
                <Nodes>
                    <telerik:RadTreeNode Text="Reclamos por etapas" Value="Reclamos" Category="Report" />
                    <telerik:RadTreeNode Text="Denuncias por mes" Value="ReclamosXDia" Category="Report" />
                </Nodes>
            </telerik:RadTreeNode>
            <telerik:RadTreeNode Text="Indemnizaciones y Gastos">
                <Nodes>
                    <telerik:RadTreeNode Text="Indemnizaciones" Value="Indemnizaciones" Category="Report" />
                    <telerik:RadTreeNode Text="Gastos de indemn. por mes " Value="IndemnizacionesXImporte" Category="Report" />
                </Nodes>
            </telerik:RadTreeNode>
            <telerik:RadTreeNode Text="Siniestros">
                <Nodes>
                    <telerik:RadTreeNode Text="Siniestros por etapa" Value="Siniestros" Category="Report" />
                </Nodes>
            </telerik:RadTreeNode>
            <telerik:RadTreeNode Text="Consultas">
                <Nodes>
                    <telerik:RadTreeNode Text="Consultas Pendientes" Value="Consultas" Category="Report" />
                </Nodes>
            </telerik:RadTreeNode>
            <telerik:RadTreeNode Text="Documentacion">
                <Nodes>
                    <telerik:RadTreeNode Text="Documentacion faltante" Value="DocumentacionFaltante"
                        Category="Report" />
                </Nodes>
            </telerik:RadTreeNode>
        </Nodes>
    </telerik:RadTreeView>
    </div>
