<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCClients.ascx.cs" Inherits="Zamba.AgentServer.Controls.UCClients" %>
    
<div class="row">
    <div class="col-md-2">
        <asp:Button ID="PostBack1" Text="Actualizar" runat="server"></asp:Button>
     </div>

     <div class="col-md-3">
         Clientes: 
         <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
            DataSourceID="ClientesDataSource" DataTextField="Client" 
            DataValueField="Client">
        </asp:DropDownList>
        <asp:SqlDataSource ID="ClientesDataSource" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ZambaConnectionString %>" 
            SelectCommand="select distinct Client  from ucmclientsset">
        </asp:SqlDataSource>
    </div>

    <div class="col-md-2">
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Ver Detalle" />
    </div>
    <div class="col-md-2">
        <div style="display:none">
                &nbsp; Mail: <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    &nbsp;  &nbsp;
                    <asp:Button ID="BtnSendReportByMail" Text="Enviar Informes" runat="server" 
                    onclick="BtnSendReportByMail_Click"></asp:Button>
        </div>
    </div>

    <div class="col-md-3">
    </div> 
</div>
<br />

<div class="row">
    <div class="col-sm-6">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
           DataSourceID="MaxDataSource" class="table table-striped table-bordered table-condensed">
            <Columns>
            <asp:BoundField DataField="Cliente" HeaderText="Cliente" 
                SortExpression="Cliente" /> 
                 <asp:BoundField DataField="Anio" HeaderText="A&ntilde;o" ReadOnly="True" 
                SortExpression="Anio" />
            <asp:BoundField DataField="Maximo" HeaderText="Maximo" ReadOnly="True" 
                SortExpression="Maximo" />
            <asp:BoundField DataField="Minimo" HeaderText="Minimo" ReadOnly="True" 
                SortExpression="Minimo" />
            <asp:BoundField DataField="Promedio" HeaderText="Promedio" ReadOnly="True" 
                SortExpression="Promedio" />
        </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="MaxDataSource" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ZambaConnectionString %>" 
            SelectCommand="select Cliente,Anio, MAX(Cantidad) as Maximo,MIN(Cantidad) as Minimo,AVG(Cantidad) as Promedio from (select Cliente,Anio, Mes, Dia, Hora, SUM(Cantidad) as Cantidad from (select  Client as Cliente,  count(1) as Cantidad, YEAR(UpdateDate) as Anio, MONTH(UpdateDate) as Mes, DAY(UpdateDate) as Dia,  datepart(hh,UpdateDate) as Hora  from (SELECT 1 as Cantidad, Client, UpdateDate FROM UCMClientSset where winuser not in ('bpm-1','Servicio') AND (([Client] <> 'HDI') OR ([Client] = 'HDI' AND WINPC LIKE 'DEVPC%' AND WINPC NOT IN ('DEVPC119', 'DEVPC118', 'DEVPC108')))  and datepart(hh,UpdateDate) > 8 and datepart(hh,UpdateDate) <19 group by user_id, winuser, Client, UpdateDate) as LicxTipoSinUsuaDupxPC  group by Client,UpdateDate  ) as Sub  group by Cliente,Anio, Mes, Dia, Hora  ) as Sub2  where anio > 2015  group by Cliente, Anio  order by Cliente, Anio">   
        </asp:SqlDataSource>
    </div>

    <div class="col-sm-6">
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
        DataSourceID="LastUpdateDataSource" class="table table-striped table-bordered table-condensed">
        <Columns>
            <asp:BoundField DataField="Cliente" HeaderText="Cliente" 
                SortExpression="Cliente" /> 
                 <asp:BoundField DataField="Ultima Actualizacion" HeaderText="Ultima Actualizacion" ReadOnly="True" 
                SortExpression="Ultima Actualizacion" />
        </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="LastUpdateDataSource" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ZambaConnectionString %>" 
            SelectCommand="SELECT Client as Cliente, MAX(UpdateDate) as [Ultima Actualizacion] FROM UCMClientSset Group by client">
        </asp:SqlDataSource>
    </div>
</div>

<div class="row">
    <div class="col-sm-12">
       <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="True" 
        DataSourceID="ClientsLicensesReportSource" class="table table-striped table-bordered table-condensed">
        </asp:GridView>
        <asp:SqlDataSource ID="ClientsLicensesReportSource" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ZambaConnectionString %>" 
            SelectCommand="SELECT * FROM CLIENTSLICENSESREPORT">
        </asp:SqlDataSource>
    </div>
</div>