<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WUC_WFTasksByUser.ascx.vb" Inherits="WUC_WFTasks" %>
<table style="width: 728px">
    <tr>
        </>
        <td style="width: 5423px">
<asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" Width="184px" AppendDataBoundItems="True" DataSourceID="UsersDataSource" DataTextField="Name" DataValueField="Name" Font-Names="Verdana" Font-Size="X-Small">
    <asp:ListItem Selected="True">Todos Los Usuarios</asp:ListItem>
</asp:DropDownList></td>
        <td style="width: 2532px">
        </td>
        <td style="width: 79957px">
            <asp:Button ID="Button1" runat="server" Text="Actualizar" Font-Size="X-Small" /></td>
    </tr>
</table>
<asp:ObjectDataSource ID="UsersDataSource" runat="server" SelectMethod="GetAllUsers"
    TypeName="Business"></asp:ObjectDataSource>
<br />
<asp:GridView ID="GridView1" runat="server" CellPadding="4" Font-Names="Verdana" Font-Size="X-Small" GridLines="Horizontal" AutoGenerateColumns="False" AllowSorting="True" PageSize="15" EmptyDataText="NO SE ENCONTRARON TAREAS" EnableViewState="False" DataSourceID="ObjectDataSource1" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="0px">
    <FooterStyle BackColor="White" ForeColor="#333333" />
    <RowStyle BackColor="White" ForeColor="#333333" />
    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" Font-Names="Verdana" Font-Size="Small" Height="10px" />
    <HeaderStyle BackColor="CornflowerBlue" Font-Bold="True" ForeColor="White" Font-Names="Verdana" Font-Size="Small" Font-Underline="False" />
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
        <asp:ImageField DataImageUrlField="ImageURL" HeaderText="Expir&#243;" ReadOnly="True" SortExpression="ImageURL">
            <ItemStyle HorizontalAlign="Center" Width="80px" />
            <HeaderStyle HorizontalAlign="Center" />
        </asp:ImageField>
        <asp:BoundField DataField="ExpireDate" HeaderText="Expira" ReadOnly="True" SortExpression="ExpireDate">
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
</asp:GridView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="ShowTasksByStepid"
    TypeName="Business">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="-1" Name="WfId" SessionField="WfId" Type="Int32" />
        <asp:SessionParameter DefaultValue="-1" Name="StepId" SessionField="StepId" Type="Int32" />
        <asp:SessionParameter DefaultValue="" Name="Filter1" SessionField="Filter1" Type="Int32" />
        <asp:SessionParameter DefaultValue="" Name="Filter2" SessionField="Filter2" Type="String" />
        <asp:SessionParameter DefaultValue="" Name="Filter3" SessionField="Filter3" Type="String" />
        <asp:SessionParameter DefaultValue="" Name="Filter4" SessionField="Filter4" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
<%--<script language="javascript" type="text/javascript">
// <!CDATA[


    var newwindow;
    function poptastic(url)
    {
        alert("1");
        var id = "<%= Label6.ClientID %>";
        alert(id.value);
        var elm = document.getElementById( id );
        alert(elm.value);   
    }



// ]]>
</script>--%>
<asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="RoyalBlue"
    Text="FILTRO: TODAS LAS TAREAS" Visible="False"></asp:Label>
