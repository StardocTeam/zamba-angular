<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCTaskBalances.ascx.cs"
    Inherits="UCTaskBalances" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
       <link href="../../../default.css" rel="stylesheet" type="text/css" />

<body class="ms-informationbar">

    <table style="margin: 0px 0px 0px 0px">
        <tr>
            <td width="100%" class="ms-topnav" style="margin: 0px 0px 0px 0px" colspan="2">
                <asp:Label ID="lblInvisibleTitle" Width="15" runat="server" />
                <asp:Label ID="lblTitle" Text="Distribución de tareas" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="margin: 0px 0px 0px 0px">
                <asp:Label runat="server" ID="lblWorkflow" Text="Workflow:" Font-Size="XX-Small" />
            </td>
            <td style="margin: 0px 0px 0px 0px">
                <asp:DropDownList ID="cmbWorkflow" runat="server" AutoPostBack="true" Font-Size="XX-Small"
                                  Width="130px" 
                    OnSelectedIndexChanged="cmbWorkflow_SelectedIndexChanged" 
                    ToolTip="Listado de Work Flows que puede visualizar">
                </asp:DropDownList>
            </td>
        </tr>
        <tr runat="server" id="RowSteps">
            <td style="margin: 0px 0px 0px 0px">
                <asp:Label runat="server" ID="lblEtapas" Text="Etapa:" Font-Size="XX-Small" />
            </td>
            <td style="margin: 0px 0px 0px 0px">
                <asp:DropDownList ID="cmbEtapas" runat="server" AutoPostBack="true" Font-Size="XX-Small"
                                  OnSelectedIndexChanged="DropEtapas_SelectedIndexChanged" 
                    Width="145px" ToolTip="Listado de etapas del Work Flow">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="margin: 0px 0px 0px 0px">
                <asp:RadioButton ID="rdbByWf" AutoPostBack="true" runat="server" Checked="True"
                                 Font-Size="XX-Small" 
                    OnCheckedChanged="rdbByWf_CheckedChanged" Text="Por Workflow" 
                    ToolTip="Muestra la distribucion de tareas por Work Flow" />
                <asp:RadioButton ID="rdbByStep" AutoPostBack="true" runat="server" 
                    Font-Size="XX-Small" OnCheckedChanged="rdbByStep_CheckedChanged"
                                 Text="Por Etapa" 
                    ToolTip="Muestra la distribucion de tareas por etapa" />&nbsp;
            </td>
        </tr>
    </table>
    
    <table style="margin: 0px 0px 0px 0px" class="ms-informationbar">
        <tr>
            <td style="margin: 0px 0px 0px 0px">
                <asp:Label ID="lblNoData" runat="server" Font-Bold="True" Font-Size="XX-Small" ForeColor="Red"
                           Visible="False">No se encontraron Tareas
                </asp:Label>
                <asp:GridView runat="server" ID="gvTasksBalance" CellPadding="4" 
                              ForeColor="#333333" GridLines="None" 
                    AutoGenerateColumns="False" >
                              <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                              <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                              <Columns>
                                    <asp:BoundField DataField="TasksBalance_State" HeaderText="Estado" />
                                    <asp:BoundField DataField="TasksBalance_Count" HeaderText="Cantidad" />
                              </Columns>
                              <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                              <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                              <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                              <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </td>
            <td>
                    <asp:Button ID="btnTaskBalance" runat="server" onclick="Button1_Click" 
                        style="height: 26px" Text="Ver Gráfico" Font-Size="Smaller" Height="16px" 
                        Width="70px" 
                        ToolTip="Muestra un gráfico generado a travez de lo datos de la tabla" />
                    <br />
                    <asp:Button id="TaskBalances" runat="server"  Enabled="true" 
                        onclick="TaskBalances_Click" Text="Guardar" Font-Size="Smaller" 
                        
                        ToolTip="Guarda en el disco C el gráfico generado a travez de la tabla actual" />
                    <br />
            </td>
        </tr>
        <tr>
        <td>
            <asp:Button ID="ActualizarTaskBalances" runat="server" Text="Actualizar" 
                onclick="ActualizarTaskBalances_Click" Font-Size="Smaller" 
                ToolTip="Actualiza los datos de la tabla actual" />
        </td>
        </tr>
    </table>
    
    <asp:Timer ID="Timer1" runat="server" Interval="20000" OnTick="Timer1_Tick" />

</body>







