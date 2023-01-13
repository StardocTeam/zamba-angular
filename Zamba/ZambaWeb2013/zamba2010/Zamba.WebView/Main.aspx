<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Main.aspx.cs" MasterPageFile="~/MasterPage.Master"
    Inherits="Main" EnableSessionState="True" ValidateRequest="false" EnableEventValidation="false" %>

<%--<%@ Register TagPrefix="MyUc" TagName="CompletarIndices" Src="~/WUCCompletarIndices.ascx" %>--%>
<%--<%@ Register TagPrefix="WCInsert" TagName="Office" Sc="~/Controls/Insert/NewOffice/NewOfficeSelector.ascx" %>--%>
<%@ Register TagPrefix="WCInsert" TagName="WCInsert" Src="~/WCInsert.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="scriptManager" runat="server" />
    <div style="background-image: 'images/fondoPantalla.jpg';">
        <center>
            <asp:UpdateProgress ID="UpdProgress1" runat="server" AssociatedUpdatePanelID="updPnlDocs">
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
        <asp:ImageButton ID="imbtnInsertar" runat="server" OnClick="imbtnInsertar_Click"
            Style="width: 14px; height: 16px;" />
        <%--<WCInsert:WCInsert runat="server" id="WCInsert" OnInserted="RefreshGrid"    />--%><asp:LinkButton
            ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Insertar</asp:LinkButton>
        <asp:UpdatePanel runat="server" ID="updPnlDocs" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Table runat="server" ID="tablaFiltro" Width="950" BackImageUrl="images/fondoPantalla.jpg"
                    CellSpacing="5" CellPadding="3">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" Width="25%">
                            <asp:Label ID="lblDcType" Font-Size="Small" ForeColor="navy" Width="150" runat="server"
                                Text="Tipos de Documento"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left" Width="75%">
                            <asp:Label ID="lblIndexToSearch" runat="server" Font-Size="Small" ForeColor="Navy"
                                Text="Criterios de Búsqueda"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" Width="25%">
                            <asp:DropDownList ID="cmbDocType" Font-Size="X-Small" ForeColor="navy" runat="server"
                                DataTextField="Name" DataValueField="Id" OnSelectedIndexChanged="cmbDocType_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:Button ID="btnSeeDocs" Text="Ver Todos" Width="80" Font-Size="X-Small" runat="server"
                                OnClick="btnSeeDocs_Click" />
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left" Width="75%">
                            <asp:DropDownList ID="cmbFilter" Font-Size="X-Small" Width="172px" runat="server"
                                OnSelectedIndexChanged="cmbFilter_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            &nbsp;
                            <asp:DropDownList AutoPostBack="true" ID="cmbOperadores" runat="server" Font-Size="X-Small"
                                Width="70px" OnSelectedIndexChanged="cmbOperadores_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;
                            <asp:TextBox ID="txtFirstFilter" runat="server" Font-Size="X-Small" Width="70px"
                                MaxLength="20"></asp:TextBox>&nbsp;
                            <asp:Label ID="cmbOperadoresDate" runat="server" Font-Size="X-Small" Visible="false"
                                Text=" AL "></asp:Label>
                            <asp:TextBox ID="txtSecondFilter" runat="server" Font-Size="X-Small" Width="70px"
                                Visible="false" MaxLength="20"></asp:TextBox>&nbsp;
                            <asp:Button ID="btnShowList" Font-Size="x-Small" runat="server" Text="..." BackColor="#ffcc66"
                                Width="25" OnClick="btnShowList_Click" />&nbsp;
                            <asp:Button ID="btnFilter" runat="server" Text="Buscar" OnClick="btnFilter_Click" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowCategory">
                        <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Width="25%">
                            <asp:Label ID="lblIndex" runat="server" Font-Size="Small" ForeColor="navy" Text="Datos del Documento"></asp:Label>
                            <br />
                            <asp:DropDownList ID="cmbIndex" runat="server" Font-Size="X-Small" ForeColor="navy"
                                OnSelectedIndexChanged="cmbIndex_SelectedIndexChanged" AutoPostBack="True" DataTextField="Index_Name"
                                DataValueField="Index_Id" Width="140px">
                            </asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left" VerticalAlign="bottom" Width="75%">
                            <asp:Panel ID="panelAgroup" runat="server" BackColor="GhostWhite" Width="100" BorderColor="AliceBlue"
                                BorderWidth="2px">
                                <asp:CheckBox ID="ChkAgroup" AutoPostBack="true" runat="server" Text="Agrupar" Font-Size="Small"
                                    ForeColor="navy" OnCheckedChanged="ChkAgroup_OnCheckedChanged" />
                            </asp:Panel>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <%--<ajaxToolKit:ModalPopupExtender ID="TableModal" runat="server" TargetControlID="PopButton"
                    PopupControlID="TablePanel" DropShadow="false" Y="0">
                   </ajaxToolKit:ModalPopupExtender>--%>
                <asp:Panel ID="FakeControl" Width="10" Height="15" Style="display: none" runat="server">
                </asp:Panel>
                <asp:Panel ID="StudentModal" Width="300" runat="server">
                    <ajaxToolKit:ModalPopupExtender ID="PopupDialog" runat="server" PopupControlID="StudentModal"
                        TargetControlID="FakeControl" X="580" Y="100" OkControlID="btnCancelarIndex"></ajaxToolKit:ModalPopupExtender> 
                    <table align="center" style="background-color: #ffcc66; font-size: 9px; text-transform: uppercase;
                        font-family: Verdana;">
                        <tr>
                            <td>
                                <div style="overflow: auto; height: 200px; width: 240px;">
                                    <asp:GridView ID="gvSustitutionList" runat="server" AutoGenerateColumns="true" BackColor="White"
                                        BorderColor="White" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="Small"
                                        AlternatingRowStyle-Font-Underline="false" HeaderStyle-Font-Underline="false"
                                        EditRowStyle-Font-Underline="false" EmptyDataRowStyle-Font-Underline="false"
                                        Font-Underline="False" FooterStyle-Font-Underline="false" PagerStyle-Font-Underline="false"
                                        RowStyle-Font-Underline="false" SelectedRowStyle-Wrap="false" AlternatingRowStyle-Wrap="false"
                                        EditRowStyle-Wrap="false" PagerStyle-Wrap="false" RowStyle-Wrap="false" OnSelectedIndexChanged="gvSustitutionList_SelectedIndexChanged">
                                        <FooterStyle Font-Underline="False" />
                                        <EmptyDataRowStyle Font-Underline="False" />
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" EditImageUrl="~/images/arrowPopUp.gif" InsertImageUrl="~/images/arrowPopUp.gif"
                                                NewImageUrl="~/images/arrowPopUp.gif" SelectImageUrl="~/images/arrowPopUp.gif"
                                                ShowSelectButton="True" UpdateImageUrl="~/images/arrowPopUp.gif" />
                                        </Columns>
                                        <RowStyle Font-Underline="False" Wrap="False" />
                                        <EditRowStyle Font-Underline="False" Wrap="False" />
                                        <SelectedRowStyle Wrap="False" />
                                        <PagerStyle Font-Underline="False" Wrap="False" />
                                        <HeaderStyle Font-Underline="False" />
                                        <AlternatingRowStyle Font-Underline="False" Wrap="False" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnCancelarIndex" runat="server" Text="Cancelar" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="PopButton" runat="server" Visible="false" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel runat="server" ID="UpdGrid" UpdateMode="conditional">
            <ContentTemplate>
                <asp:Table runat="server" ID="tblAll" Width="990" BackImageUrl="~/images/fondoPantalla.jpg">
                    <asp:TableRow>
                        <asp:TableCell></asp:TableCell><asp:TableCell></asp:TableCell><asp:TableCell>
                            <asp:Label ID="lblTotal" runat="server" Font-Size="Small" ForeColor="Navy"></asp:Label></asp:TableCell></asp:TableRow>
                    <asp:TableRow Height="100%" VerticalAlign="Top">
                        <asp:TableCell>
                            <asp:Label runat="server" ID="lblInvisible" Height="313" ForeColor="whitesmoke" Text="."></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Width="200">
                            <asp:UpdatePanel runat="server" ID="updList" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="gvDocTypes" runat="server" OnSelectedIndexChanged="gvDocTypes_SelectedIndexChanged"
                                        CellPadding="4" ForeColor="#333333" GridLines="None" Font-Size="Small" DataKeyNames="ITEM"
                                        AutoGenerateColumns="false" Width="170px">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="CATEGORIAS" SortExpression="CATEGORIAS"
                                                FooterStyle-Wrap="false">
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
                            <%--                   <asp:UpdatePanel runat="server" ID="UpdGrid" UpdateMode="conditional">
                        <ContentTemplate>--%>
                            <asp:GridView ID="gvDocuments" runat="server" AutoGenerateColumns="false" AllowSorting="true"
                                BackColor="White" BorderColor="White" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                Font-Size="Small" OnSelectedIndexChanged="gvDocuments_SelectedIndexChanged" OnRowCreated="gvDocuments_RowCreated"
                                OnRowDataBound="gvDocuments_OnRowDataBound" OnSorting="OnSorting" AlternatingRowStyle-Font-Underline="false"
                                HeaderStyle-Font-Underline="false" EditRowStyle-Font-Underline="false" EmptyDataRowStyle-Font-Underline="false"
                                Font-Underline="false" FooterStyle-Font-Underline="false" PagerStyle-Font-Underline="false"
                                RowStyle-Font-Underline="false" SelectedRowStyle-Wrap="false" AlternatingRowStyle-Wrap="false"
                                EditRowStyle-Wrap="false" PagerStyle-Wrap="false" RowStyle-Wrap="false">
                                <%--<Columns>
                                    <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="Ver" SortExpression="CATEGORIAS" ItemStyle-Wrap ="false">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" Text="Ver" Target="_blank" NavigateUrl='<%# Eval("Fullpath", "DocViewer.aspx?fullpath={0}") %>'>
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>--%>
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" Font-Size="Small" />
                                <RowStyle BackColor="#EDF7FE" BorderStyle="None" BorderWidth="1px" ForeColor="Black"
                                    Font-Size="Small" />
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" Font-Size="Small" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Font-Size="Small" />
                                <HeaderStyle BackColor="#D6EBFC" Font-Bold="True" ForeColor="#336699" HorizontalAlign="Center"
                                    Font-Size="Small" />
                                <AlternatingRowStyle BackColor="#F7F7F7" Font-Size="Small" />
                            </asp:GridView>
                            <asp:Label ID="lblPageNumber" runat="server" ForeColor="Navy" />
                            <asp:Button ID="btnFirst" runat="server" Font-Size="X-Small" Text="<<" Width="25"
                                OnClick="btnFirst_OnClick" />
                            <asp:Button ID="BtnBack" runat="server" Font-Size="X-Small" Text="<" Width="25" OnClick="BtnBack_OnClick" />
                            <asp:Button ID="BtnNext" runat="server" Font-Size="X-Small" Text=">" Width="25" OnClick="BtnNext_OnClick" />
                            <asp:Button ID="btnLast" runat="server" Font-Size="X-Small" Text=">>" Width="25"
                                OnClick="btnLast_OnClick" />
                            <%--                        </ContentTemplate>
                    </asp:UpdatePanel>--%>
                        </asp:TableCell></asp:TableRow>
                </asp:Table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--                   <asp:UpdatePanel runat="server" ID="UpdGrid" UpdateMode="conditional">
                        <ContentTemplate>--%>
    </div>
</asp:Content>
