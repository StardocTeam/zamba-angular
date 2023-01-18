<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Main.aspx.cs" MasterPageFile="~/MasterPage.Master"
    Inherits="Main" EnableSessionState="True" ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<font face="Arial, Helvetica, sans-serif" size="2">
    <div >
    <strong>
        <center>
            <asp:UpdateProgress ID="UpdProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <center>
                        <asp:Table runat="server" ID="tblUpdate" BackColor="#D6EBFC" Font-Bold="True" ForeColor="#336699"
                            Height="100%">
                            <asp:TableRow ID="TableRow1" runat="server">
                                <asp:TableCell ID="TableCell1" runat="server">AGUARDE UN MOMENTO POR FAVOR</asp:TableCell></asp:TableRow>
                        </asp:Table>
                    </center>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </center>
        <asp:UpdatePanel runat="server" ID="updPnlDocs" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Table runat="server" ID="tablaFiltro" Width="950" CellSpacing="5" CellPadding="3">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" Width="25%">
                            <asp:Label ID="lblDcType" ForeColor="navy" Width="150" runat="server"
                                Text="Tipos de Documento"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left" Width="75%">
                            <asp:Label ID="lblIndexToSearch" runat="server" ForeColor="Navy"
                                Text="Criterios de Búsqueda"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" Width="25%">
                            <asp:DropDownList ID="cmbDocType" ForeColor="navy" runat="server"
                                DataTextField="Name" DataValueField="Id" OnSelectedIndexChanged="cmbDocType_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>&nbsp;
                            <asp:Button ID="btnSeeDocs" Text="Ver Todos" Width="80" runat="server"
                                OnClick="btnSeeDocs_Click" />&nbsp;
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left" Width="75%">
                            <asp:DropDownList ID="cmbFilter" Width="172px" runat="server"
                                OnSelectedIndexChanged="cmbFilter_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>&nbsp;
                            <asp:DropDownList AutoPostBack="true" ID="cmbOperadores" runat="server" 
                                Width="80px" OnSelectedIndexChanged="cmbOperadores_SelectedIndexChanged">
                            </asp:DropDownList>&nbsp;
                            <asp:TextBox ID="txtFilter" runat="server" Width="70px" MaxLength="20"></asp:TextBox>&nbsp;
                            <asp:Label ID="cmbOperadoresDate" runat="server" Visible="false"
                                Text=" AL "></asp:Label>
                            <asp:TextBox ID="txtFilterDate" runat="server" Width="70px" Visible="false"
                                MaxLength="20"></asp:TextBox>&nbsp;
                            <asp:Button ID="btnFilter" runat="server" Text="Buscar" onClick="btnFilter_Click" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowCategory">
                        <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Width="25%">
                            <asp:Label ID="lblIndex" runat="server" ForeColor="navy" Text="Datos del Documento"></asp:Label>
                            <br />
                            <asp:DropDownList ID="cmbIndex" runat="server" Font-Size="X-Small" ForeColor="navy"
                                OnSelectedIndexChanged="cmbIndex_SelectedIndexChanged" AutoPostBack="True" DataTextField="Index_Name"
                                DataValueField="Index_Id" Width="140px">
                            </asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left" VerticalAlign="bottom" Width="75%">
                            <asp:Panel ID="panelAgroup" runat="server" BackColor="GhostWhite" Width="100" BorderColor="AliceBlue"
                                BorderWidth="2px">
                                <asp:CheckBox ID="ChkAgroup" AutoPostBack="true" runat="server" Text="Agrupar" 
                                    ForeColor="navy" OnCheckedChanged="ChkAgroup_OnCheckedChanged" />
                            </asp:Panel>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <ajaxToolKit:ModalPopupExtender ID="TableModal" runat="server" TargetControlID="PopButton"
                    PopupControlID="TablePanel" DropShadow="false" Y="0">
                </ajaxToolKit:ModalPopupExtender>
                <asp:Button ID="PopButton" runat="server" Visible="false" />
            </ContentTemplate>
        </asp:UpdatePanel>
            </strong>
        <asp:Table runat="server" ID="tblAll" Width="990" >
            <asp:TableRow>
                <asp:TableCell></asp:TableCell><asp:TableCell></asp:TableCell><asp:TableCell><strong>
                    <asp:Label ID="lblTotal" runat="server" ForeColor="Navy"></strong></asp:Label></asp:TableCell></asp:TableRow>
            <asp:TableRow Height="100%" VerticalAlign="Top">
                <asp:TableCell>
                    <asp:Label runat="server" ID="lblInvisible" Height="313" ForeColor="whitesmoke" Text="."></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Width="200">
                    <asp:UpdatePanel runat="server" ID="updList" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvDocTypes" runat="server" OnSelectedIndexChanged="gvDocTypes_SelectedIndexChanged"
                                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="ITEM"
                                AutoGenerateColumns="false" Width="170px">
                                <Columns>
                                    <asp:TemplateField HeaderText="CATEGORIAS" SortExpression="CATEGORIAS" FooterStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("ITEM") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField SelectText="Listar" ShowSelectButton="True" />
                                </Columns>
                                <PagerTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("ITEM") %>'></asp:Label>
                                </PagerTemplate>
                                <EmptyDataTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ITEM") %>'></asp:Label>
                                </EmptyDataTemplate>
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <EditRowStyle BackColor="#999999" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Top" Width="100%">
                    <asp:UpdatePanel runat="server" ID="UpdGrid" UpdateMode="conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvDocuments" runat="server" AutoGenerateColumns="false" AllowSorting="true"
                                BackColor="White" BorderColor="White" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                OnSelectedIndexChanged="gvDocuments_SelectedIndexChanged" OnRowCreated="gvDocuments_RowCreated"
                                OnRowDataBound="gvDocuments_OnRowDataBound" OnSorting="sort" AlternatingRowStyle-Font-Underline="false"
                                HeaderStyle-Font-Underline="false" EditRowStyle-Font-Underline="false" EmptyDataRowStyle-Font-Underline="false"
                                Font-Underline="false" FooterStyle-Font-Underline="false" PagerStyle-Font-Underline="false"
                                RowStyle-Font-Underline="false" SelectedRowStyle-Wrap="false" AlternatingRowStyle-Wrap="false"
                                EditRowStyle-Wrap="false" PagerStyle-Wrap="false" RowStyle-Wrap="false" >
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <RowStyle BackColor="#EDF7FE" BorderStyle="None" BorderWidth="1px" ForeColor="Black" />
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#D6EBFC" Font-Bold="True" ForeColor="#336699" HorizontalAlign="Center" />
                                <AlternatingRowStyle BackColor="#F7F7F7" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:TableCell></asp:TableRow>
        </asp:Table>
        <%--<input id="btnShowSustitutionList" onclick="Javascript:abrirModal('Mantenimiento.aspx?even=load&cod=<%# Eval("ITEM") %>')" type="button" style="background-color:#ffcc66;font-size:x-small;width:25;" value="..." />--%>
        <asp:Panel ID="FakeControl" Width="10" Height="15" Style="display: none" runat="server">
        </asp:Panel>
        <asp:Panel ID="StudentModal" Width="800" Height="175" Style="display: none" runat="server">
            <ajaxToolKit:ModalPopupExtender ID="StudentDialog" runat="server" PopupControlID="StudentModal"
                TargetControlID="FakeControl" BackgroundCssClass="modalBackground" DropShadow="true"
                OkControlID="ctrlOk"  />
                <table align="center" style="font-size: 9px; text-transform: uppercase; font-family: Verdana;">
                <tr>
                    <asp:DataGrid ID="dgSustitutionList" runat="server">
                    </asp:DataGrid>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="BtnAceptar" runat="server" Text="Aceptar" />
                        <input id="BtnCancelar" onclick="CerrarSinEvento();" type="button" value="Cancelar" /></td>
                </tr>
            </table>
            <center>
                <asp:LinkButton ID="ctrlOK" Text="Close this Window" runat="server" /></center>
        </asp:Panel>
    </div>
    </font>
</asp:Content>
