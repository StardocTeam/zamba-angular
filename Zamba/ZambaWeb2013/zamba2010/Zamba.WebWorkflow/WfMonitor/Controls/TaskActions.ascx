<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TaskActions.ascx.cs" Inherits="TaskActions" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:HiddenField ID="hfStepId" runat="server" />
<asp:HiddenField ID="hfTaskIds" runat="server" />
<div class="TaskActions">
    <cc1:TabContainer ID="tbcTaskActions" runat="server" ActiveTabIndex="0">
        <cc1:TabPanel runat="server" ID="tbpDerivar" HeaderText="Derivar" ForeColor="Navy">
            <ContentTemplate>
                <asp:Label ID="lbDerivarEtapas" runat="server" Text="Etapa" ForeColor="Navy" />
                <asp:DropDownList ID="ddlDerivarEtapas" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                    BorderWidth="1px" Font-Names="Verdana" ForeColor="navy" />
                <asp:Button ID="btDerivarEtapas" runat="server" Text="Derivar" OnClick="btDerivarEtapas_Click"
                    BackColor="#EFF3FB" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                    Font-Names="Verdana" ForeColor="#284775" />
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" ID="tbpAsignar" HeaderText="Asignar" ForeColor="Navy">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lbAsignarUsuarios" Text="Usuario" ForeColor="Navy" runat="server" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAsignarUsuarios" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                BorderWidth="1px" Font-Names="Verdana" ForeColor="navy" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Button ID="btDesasignarUsuarios" runat="server" Text="Desasignar" OnClick="btDesasignarUsuarios_Click"
                                BackColor="#EFF3FB" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="Verdana" ForeColor="#284775" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btAsignarUsuarios" runat="server" Text=" Asignar  " OnClick="btAsignarUsuarios_Click"
                                BackColor="#EFF3FB" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="Verdana" ForeColor="#284775" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" ID="tbpQuitar" HeaderText="Quitar" ForeColor="Navy">
            <ContentTemplate>
                <asp:Button runat="server" ID="btQuitar" Text="Quitar" OnClick="btQuitar_Click" BackColor="#EFF3FB"
                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana"
                    ForeColor="#284775" />
                <asp:CheckBox runat="server" ID="chBorrarDocumento" Text="Borrar documento" />
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</div>
