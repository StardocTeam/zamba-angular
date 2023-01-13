<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StepsList.ascx.cs" Inherits="StepsList" %>
<link href="../../../default.css" rel="stylesheet" type="text/css" />
<fieldset title="Listado de Etapas">
    <legend class="srch-Title">Listado de Etapas</legend>
    <asp:HiddenField ID="hdnSelectedRadioButton" runat="server" />
    <div class="UserControlBody">
        <table width="100%" style="position: static; height: auto; width: auto">
            <tr>
                <td colspan="2" align="center">
                    <asp:Label ForeColor="Navy" ID="lbNoSteps" runat="server" CssClass="ms-informationbar" />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:LinkButton ID="lnkAll" runat="server" Text="Seleccionar todas" OnClick="lnkAll_Click"
                        ToolTip="Seleccionar todas las etapas" />
                </td>
                <td align="right">
                    <asp:LinkButton ID="lnkNone" runat="server" Text="Seleccionar ninguna" OnClick="lnkNone_Click"
                        ToolTip="Deseleccionar todas las tareas" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvSteps" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        Width="100%" OnPageIndexChanging="gvSteps_PageIndexChanging" BackColor="White"
                        BorderColor="White" BorderStyle="None" BorderWidth="1px" CellPadding="2">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chbSelected" runat="server" ToolTip="Seleccionar Etapa" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField Visible="False" />
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
                    <asp:Button ID="btUpdate" runat="server" Text="Actualizar" OnClick="btUpdate_Click"
                        ToolTip="Actualizar Listado" BackColor="#EFF3FB" BorderColor="#CCCCCC" BorderStyle="Solid"
                        BorderWidth="1px" Font-Names="Verdana" Font-Size="X-Small" ForeColor="#284775" />
                </td>
                <td align="right">
                    <asp:Button ID="btView" runat="server" Text="Ver tareas" ToolTip="Ver tareas de las etapas seleccionadas"
                        BackColor="#EFF3FB" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                        Font-Names="Verdana" Font-Size="X-Small" ForeColor="#284775" 
                        onclick="btView_Click" />
                </td>
            </tr>
        </table>
    </div>
</fieldset>
