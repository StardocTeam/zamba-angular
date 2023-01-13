<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TaskList.ascx.cs" Inherits="UserControls_TaskList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/Filter.ascx" TagName="WUC_Filter" TagPrefix="uc5" %>
    <cc1:TabContainer ID="tcAcciones" runat="server">
            <cc1:TabPanel ID="tbpAsignar" runat="server" HeaderText="Asignar">
                <ContentTemplate>
                    <table>
                        <tr align="left">
                            <td>
                                <asp:Label ID="lbAignarTitulo" runat="server" Text="Seleccione Usuario  "></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAsignarUsuarios" runat="server" AppendDataBoundItems="True"
                                    DataMember="NAME" DataTextField="NAME" DataValueField="ID">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td colspan="2">
                                <asp:CheckBox ID="cboAsignarNotificar" runat="server" Text="Notificar al Usuario" /></td>
                        </tr>
                        <tr align="left">
                            <td colspan="2">
                                <asp:Button ID="btAsignarAceptar" runat="server" OnClick="btAsignarAceptar_Click"
                                    Text="Asignar" /></td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="tbpFiltrar" runat="server" HeaderText="Filtrar">
                <ContentTemplate>
                    <uc5:WUC_Filter ID="WUC_Filter1" runat="server" />
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="tbpQuitar" runat="server" HeaderText="Quitar">
                <ContentTemplate>
                    <asp:RadioButtonList ID="rblQuitarOpciones" runat="server">
                        <asp:ListItem Selected="True" Text="Quitar solo la Tarea" Value="Tarea"></asp:ListItem>
                        <asp:ListItem Text="Quitar Todo" Value="Todo"></asp:ListItem>
                    </asp:RadioButtonList><br />
                    <asp:Button ID="btQuitarTarea" runat="server" OnClick="btQuitarTarea_Click" Text="Quitar" />
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="tbpDistribuir" runat="server" HeaderText="Distribuir">
                <ContentTemplate>
                    <table>
                        <tr align="left">
                            <td>
                                <asp:Label ID="lbDistribuirEtapa" runat="server" Text="Seleccione una etapa  "></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDistribuirEtapa" runat="server" DataMember="Name" DataTextField="Name"
                                    DataValueField="Step_Id">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                                <asp:Label ID="lbDistribuirUsuario" runat="server" Text="Seleccione un usuario  "></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDistribuirUsuarios" runat="server" AppendDataBoundItems="True"
                                    DataMember="NAME" DataTextField="NAME" DataValueField="ID">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td colspan="2">
                                <asp:CheckBox ID="cboDistribuirNotificar" runat="server" Text="Notificar al usuario" />
                            </td>
                        </tr>
                        <tr align="left">
                            <td colspan="2">
                                <asp:Button ID="btDistribuirAceptar" runat="server" Text="Distribuir" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="tbpRenovarVencimiento" runat="server" HeaderText="Renovar Vencimiento">
                <ContentTemplate>
                    <table>
                        <tr align="left">
                            <td colspan="2">
                                <asp:Label ID="lbRenovarFecha" runat="server" Text="Seleccione una fecha de vencimiento">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                                <asp:TextBox ID="tbRenovarFecha" runat="server">
                                </asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tbRenovarFecha">
                                </cc1:CalendarExtender>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRenovarHoras" runat="server">
                                    <asp:ListItem>00:00</asp:ListItem>
                                    <asp:ListItem>00:30</asp:ListItem>
                                    <asp:ListItem>01:00</asp:ListItem>
                                    <asp:ListItem>01:30</asp:ListItem>
                                    <asp:ListItem>02:00</asp:ListItem>
                                    <asp:ListItem>02:30</asp:ListItem>
                                    <asp:ListItem>03:00</asp:ListItem>
                                    <asp:ListItem>03:30</asp:ListItem>
                                    <asp:ListItem>04:00</asp:ListItem>
                                    <asp:ListItem>04:30</asp:ListItem>
                                    <asp:ListItem>05:00</asp:ListItem>
                                    <asp:ListItem>05:30</asp:ListItem>
                                    <asp:ListItem>06:00</asp:ListItem>
                                    <asp:ListItem>06:30</asp:ListItem>
                                    <asp:ListItem>07:00</asp:ListItem>
                                    <asp:ListItem>07:30</asp:ListItem>
                                    <asp:ListItem>08:00</asp:ListItem>
                                    <asp:ListItem>08:30</asp:ListItem>
                                    <asp:ListItem>09:00</asp:ListItem>
                                    <asp:ListItem>09:30</asp:ListItem>
                                    <asp:ListItem>10:00</asp:ListItem>
                                    <asp:ListItem>10:30</asp:ListItem>
                                    <asp:ListItem>11:00</asp:ListItem>
                                    <asp:ListItem>11:30</asp:ListItem>
                                    <asp:ListItem>12:00</asp:ListItem>
                                    <asp:ListItem>12:30</asp:ListItem>
                                    <asp:ListItem>13:00</asp:ListItem>
                                    <asp:ListItem>13:30</asp:ListItem>
                                    <asp:ListItem>14:00</asp:ListItem>
                                    <asp:ListItem>14:30</asp:ListItem>
                                    <asp:ListItem>15:00</asp:ListItem>
                                    <asp:ListItem>15:30</asp:ListItem>
                                    <asp:ListItem>16:00</asp:ListItem>
                                    <asp:ListItem>16:30</asp:ListItem>
                                    <asp:ListItem>17:00</asp:ListItem>
                                    <asp:ListItem>17:30</asp:ListItem>
                                    <asp:ListItem>18:00</asp:ListItem>
                                    <asp:ListItem>18:30</asp:ListItem>
                                    <asp:ListItem>19:00</asp:ListItem>
                                    <asp:ListItem>19:30</asp:ListItem>
                                    <asp:ListItem>20:00</asp:ListItem>
                                    <asp:ListItem>20:30</asp:ListItem>
                                    <asp:ListItem>21:00</asp:ListItem>
                                    <asp:ListItem>21:30</asp:ListItem>
                                    <asp:ListItem>22:00</asp:ListItem>
                                    <asp:ListItem>22:30</asp:ListItem>
                                    <asp:ListItem>23:00</asp:ListItem>
                                    <asp:ListItem>23:30</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td colspan="2">
                                <asp:Button ID="btRenovar" runat="server" OnClick="btDistribuirAceptar_Click" Text="Renovar" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="tbpDesasignar" runat="server" HeaderText="Desasignar">
                <ContentTemplate>
                    <asp:Button ID="btDesasignar" runat="server" OnClick="btDesasignar_Click" Text="Desasignar" />
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="tbpEstado" runat="server" HeaderText="Cambiar Estado">
                <ContentTemplate>
                    <table>
                        <tr align="left">
                            <td>
                                <asp:Label ID="lbEstado" runat="server" Text="Seleccione un estado  "></asp:Label>
                                <asp:DropDownList ID="ddlEstados" runat="server" AppendDataBoundItems="True" DataMember="Name"
                                    DataTextField="Name" DataValueField="Id">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                                <asp:Button ID="btEstado" runat="server" OnClick="btEstado_Click" Text="Cambiar Estado" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="tbpImprimir" runat="server" HeaderText="Imprimir">
                <ContentTemplate>
                    <asp:Button ID="btImprimir" runat="server" OnClick="btImprimir_Click" Text="Imprimir" />
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="tbpMail" runat="server" HeaderText="Enviar Mail">
                <ContentTemplate>
                    <asp:Button ID="btMail" runat="server" OnClick="btMail_Click" Text="Enviar Mail" />
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
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

