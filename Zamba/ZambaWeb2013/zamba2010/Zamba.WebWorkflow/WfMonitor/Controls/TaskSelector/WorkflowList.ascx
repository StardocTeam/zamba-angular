<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WorkflowList.ascx.cs"
    Inherits="WorkflowList" EnableTheming="true" %>
<link href="../../../default.css" rel="stylesheet" type="text/css" />
<fieldset title="Listado de Workflows">
    <legend class="srch-Title">Listado de Workflows</legend>
    <div class="UserControlBody">
        <table width="100%" style="position: static; width: auto; height: auto">
            <tr>
                <td colspan="2" align="center">
                    <asp:Label ForeColor="Navy" ID="lbNoWorklows" runat="server" CssClass="ms-informationbar" />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:LinkButton ID="lnkAll" runat="server" Text="Seleccionar todos" OnClick="lnkAll_Click"
                        ToolTip="Seleccionar todas los workflows" />
                </td>
                <td align="right">
                    <asp:LinkButton ID="lnkNone" runat="server" Text="Seleccionar ninguno" OnClick="lnkNone_Click"
                        ToolTip="Deseleccionar todas los workflows" />
                </td>
            </tr>
            <tr>
                <td colspan="2" >
                    <asp:GridView ID="gvWorkflows" runat="server" OnPageIndexChanging="gvWorkflows_PageIndexChanging"
                        Width="100%" AllowPaging="True" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="White" BorderStyle="None" BorderWidth="1px" CellPadding="2">
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" Wrap="True" />
                        <EmptyDataRowStyle Wrap="True" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chbSelected" runat="server" ToolTip="Seleccionar Workflow"  />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField Visible="false" />
                            <asp:BoundField HeaderText="Nombre" />
                            <asp:BoundField HeaderText="Tareas">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Expiradas">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />                                                
                        <RowStyle BackColor="#EDF7FE" BorderStyle="None" BorderWidth="1px" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#D6EBFC" Font-Bold="True" ForeColor="#336699" HorizontalAlign="Center" />
                        <AlternatingRowStyle BackColor="#F7F7F7" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Button ID="btActualizar" runat="server" Text="Actualizar" OnClick="btUpdate_Click"
                        BackColor="#EFF3FB" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                        Font-Names="Verdana" Font-Size="X-Small" ForeColor="#284775" ToolTip="Actualizar Listado" />
                </td>
                <td align="right">
                    <asp:Button ID="btView" runat="server" Text="Ver etapas" BackColor="#EFF3FB" BorderColor="#CCCCCC"
                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="X-Small"
                        ForeColor="#284775" ToolTip="Ver etapas de los workflows seleccionados" OnClick="btView_Click" />
                </td>
            </tr>
        </table>
    </div>
</fieldset>
