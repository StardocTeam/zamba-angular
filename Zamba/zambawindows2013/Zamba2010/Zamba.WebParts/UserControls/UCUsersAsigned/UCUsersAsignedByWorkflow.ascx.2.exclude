<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCUsersAsignedByWorkflow.ascx.cs"
    Inherits="UserControls_UCUsersAsigned_UCUsersAsignedByWorkflow" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<table>
    <tr>
        <td>
        <asp:Label runat="server" ID="lblWorkflow" Text="Workflow:" Font-Size="XX-Small" />
        </td>
        <td>
            <asp:DropDownList ID="cmbWorkflows" AutoPostBack =true runat="server" Width="145px" Font-Size="XX-Small" OnSelectedIndexChanged="cmbWorkflows_SelectedIndexChanged">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td style="width: 6px; height: 21px">
        </td>
        <td style="height: 21px">
        <asp:RadioButton ID="rdbVerGrafico" AutoPostBack =true runat="server" Checked="True"  Font-Size="XX-Small"
            Text="Ver Gráfico" OnCheckedChanged="rdbVerGrafico_CheckedChanged" />
            <asp:RadioButton ID="rdbVerTabla" AutoPostBack =true runat="server"  Font-Size="XX-Small"
                Text="Ver Tabla" OnCheckedChanged="rdbVerTabla_CheckedChanged" /></td>
    </tr>
</table>
<table>
<tr>
<td>
<rsweb:reportviewer id="rpvUsersAsignedByWorkflow" runat="server" font-names="Verdana"
    font-size="8pt" height="400px" showfindcontrols="False" showpagenavigationcontrols="False"
    width="400px">
<LocalReport ReportPath="Reports\rptUsersAsigned.rdlc">
</LocalReport>
</rsweb:reportviewer>
</td>
<td>
    <rsweb:ReportViewer ID="rpvGrdUsersAsignedByWorkflow" runat="server" Font-Names="Verdana"
        Font-Size="8pt" Height="400px" ShowFindControls="False" ShowPageNavigationControls="False"
        Width="400px">
        <LocalReport ReportPath="Reports\RptGrdUsersAsigned.rdlc">
        </LocalReport>
    </rsweb:ReportViewer>
</td>
</tr>
</table>

