<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WUC_WFTasks.ascx.vb"
    Inherits="WUC_WFTasks" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:GridView ID="gvTasks" runat="server" CellPadding="4" Font-Names="Verdana" 
    Font-Size="X-Small" GridLines="None" AutoGenerateColumns="False" PageSize="15" DataSourceID="odsTasks"
    EmptyDataText="NO SE ENCONTRARON TAREAS" EnableViewState="False" 
    ForeColor="#333333" Width="953px" DataKeyNames="Id" AllowPaging="True" AllowSorting="True">
    <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
    <RowStyle BackColor="#EFF3FB" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Names="Verdana"
        Font-Size="Small" Height="10px" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
        Font-Size="Small" />
    <Columns>
              <asp:TemplateField ShowHeader="False">
            <EditItemTemplate>
                <asp:CheckBox ID="CheckBox1" runat="server" />
            </EditItemTemplate>
            <ItemTemplate>
                <asp:CheckBox ID="CheckBox1" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:ButtonField HeaderText="Nombre" DataTextField="Name" CommandName="ShowDetails" SortExpression="Name" >
            <ItemStyle Width="250px" />
        </asp:ButtonField>
        <asp:BoundField DataField="TaskState" HeaderText="Situaci&#243;n" ReadOnly="True" SortExpression="TaskState">
            <ItemStyle ForeColor="Blue" Width="80px" />
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="ExpireDate" HeaderText="Vencido" ReadOnly="True" SortExpression="ExpireDate">
            <ItemStyle Width="200px" />
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="State" HeaderText="Estado" ReadOnly="True" SortExpression="State" >
            <ItemStyle Width="80px" />
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="UserName" HeaderText="Usuario" ReadOnly="True" SortExpression="UserName">
            <ItemStyle HorizontalAlign="Left" Width="80px" />
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="WfStepName" HeaderText="Etapa" ReadOnly="True" SortExpression="WfStepName" >
            <ItemStyle Width="140px" />
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
    </Columns>
    <EditRowStyle BackColor="#2461BF" />
    <AlternatingRowStyle BackColor="White" />
</asp:GridView>
<asp:ObjectDataSource ID="odsTasks" runat="server" SelectMethod="GetDataSource"
    TypeName="Business" OldValuesParameterFormatString="original_{0}">
    <SelectParameters>
        <asp:SessionParameter ConvertEmptyStringToNull="False" Name="wfId" SessionField="WfId"
            Type="String" />
        <asp:SessionParameter DefaultValue="" Name="stepId" SessionField="StepId" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>

