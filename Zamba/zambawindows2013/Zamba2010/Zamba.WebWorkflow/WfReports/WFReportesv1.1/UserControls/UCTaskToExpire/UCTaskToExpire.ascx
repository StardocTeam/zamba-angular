<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCTaskToExpire.ascx.cs"
    Inherits="UCTaskToExpire" %>
    
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<link href="../../../default.css" rel="stylesheet" type="text/css" />

<body class="ms-informationbar">

    <table style="margin: 0px 0px 0px 0px">
        <tr class="ms-topnav" >
            <td width="100%" class="ms-topnav" style="margin: 0px 0px 0px 0px" colspan="2">
                <asp:Label ID="lblInvisibleTitle" Width="15" runat="server" />
                <asp:Label ID="lblTitle" Text="Vencimiento de tareas" runat="server" />
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
                    ToolTip="Listado de Work Flows">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="margin: 0px 0px 0px 0px">
                <asp:Label runat="server" ID="LblHoursToExpire" Text="Horas restantes para expirar:"
                           Font-Size="XX-Small" />
            </td>
            <td style="margin: 0px 0px 0px 0px">
                <asp:TextBox ID="txtHoursToExpire" runat="server" Width="35px" 
                    Font-Size="XX-Small" 
                    ToolTip="Ingrese la cantidad de horas que restan para que venza la tarea"></asp:TextBox>
                <asp:Button ID="btnBuscar" runat="server" Text="Aplicar" 
                    OnClick="btnBuscar_Click" 
                    ToolTip="Muestra en la grilla las tareas que estan por vencer seg�n el criterio cargado en las horas restantes para expirar" />
            </td>
        </tr>
        <tr>
            <td style="margin: 0px 0px 0px 0px">
                <asp:RadioButton ID="rdbByStep" AutoPostBack="true" runat="server" Checked="True"
                                 Font-Size="XX-Small" 
                    OnCheckedChanged="rdbByStep_CheckedChanged" Text="Por Etapa" 
                    ToolTip="Muestra las tareas por vencer por etapa" />
                <asp:RadioButton ID="rdbByUser" AutoPostBack="true" runat="server" Font-Size="XX-Small"
                                 OnCheckedChanged="rdbByUser_CheckedChanged" 
                    Text="Por Usuario" ToolTip="Muestra las tareas por vencer por usuario" />
            </td>
        </tr>
    </table>

    <table style="margin: 0px 0px 0px 0px" class="ms-informationbar">
        <tr>
            <td style="margin: 0px 0px 0px 0px" colspan="2">
                <asp:Label ID="lblNoData" runat="server" Font-Bold="True" Font-Size="XX-Small" ForeColor="Red"
                           Visible="False">No se encontraron Tareas
                </asp:Label>
                <asp:GridView runat="server" ID="gvTaskToExpireByWorkflow" CellPadding="4" 
                              ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" >
                              <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                              <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                              <Columns>
                                    <asp:BoundField DataField="TaskToExpire_Source" HeaderText="Etapa/Usuario" />
                                    <asp:BoundField DataField="TaskToExpire_Count" HeaderText="Contador" />
                              </Columns>
                              <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                              <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                              <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                              <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </td>
            
                
            <td style="margin: 0px 0px 0px 0px">
                <asp:Button ID="Button1" runat="server" Text="Ver Gr�fico" 
                    onclick="Button1_Click" Font-Size="Smaller" 
                    ToolTip="Muestra el gr�fico de barras de la grilla" />
                <br />
          <asp:Button ID="SaveGraphic" runat="server" Enabled="true" 
                    onclick="SaveGraphic_Click" Text="Guardar" Font-Size="Smaller" 
                    ToolTip="Guarda en el disco C el gr�fico generado a travez de la tabla actual" />
                <br />
                <br />
                <br />
        </td>      
            
        </tr>
        <tr>
        <td>
        <asp:Button ID="ActualizarTaskToExpire" runat="server" Text="Actualizar" 
                onclick="ActualizarTaskToExpire_Click" Font-Size="Smaller" 
                ToolTip="Actualiza los datos de la tabla actual"/>
        </td>
        </tr>
  </table>
  
  <asp:Timer ID="Timer1" runat="server" Interval="20000" OnTick="Timer1_Tick" />

    </body>