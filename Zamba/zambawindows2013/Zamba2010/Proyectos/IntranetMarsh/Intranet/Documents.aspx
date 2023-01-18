<%@ Page Language="C#" MasterPageFile="~/IntraMasterPage.Master" AutoEventWireup="true"
    CodeFile="Documents.aspx.cs" Inherits="IntranetMarsh.Documents" Title="Untitled Page" %>

<%@ Register TagPrefix="MControls" TagName="ProcList" Src="~/Controls/ProcList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 193px;
        }
        .style3
        {
            width: 87px;
        }
        .style4
        {
            width: 95px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                        <asp:UpdatePanel ID="updateLinks" runat="server" UpdateMode="Always">
                        <ContentTemplate>
    <table style="height: 380px" width="590px">
        <tr>
            <td>
                <table width="100%" style="height: 10px" bgcolor="#003366">
                    <tr>
                        <td class="style4" style="width:195px">
                        </td>
                            <td bgcolor="#666633" class="style4" style="text-align: center">
                                <asp:LinkButton runat="server" ID="LinkProcs" Text="Procedimientos" Font-Size="Small"
                                    ForeColor="White"></asp:LinkButton>
                            </td>
                            <td bgcolor="#660066" class="style3" style="text-align: center">
                                <asp:LinkButton runat="server" ID="LinkResults" Text="Resultados" Font-Size="Small"
                                    ForeColor="White"></asp:LinkButton>
                            </td>
                            <td>
                            </td>
                    </tr>
            </td>
        </tr>
    </table>
    </td> 
    </tr>
    <tr>
        <td>
            <table width="100%">
                <tr>
                    <td valign="top" style="padding-top: 4px">
                        <asp:UpdatePanel runat="server" ID="PRevUPanel" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:MultiView runat="server" ID="MView" ActiveViewIndex="0">
                                    <asp:View ID="Preview" runat="server">
                                        <fieldset runat="server" id="PreviewField" style="width:590px; height: 350px">
                                            <legend id="Legend1" runat="server">Vista Previa</legend>
                                            <iframe id="PreviewDoc" runat="server" height="330px" width="100%" frameborder="0"></iframe>
                                        </fieldset>
                                    </asp:View>
                                    <asp:View ID="Results" runat="server">
                                        <fieldset runat="server" id="Fieldset1" style="width:590px; height: 350px">
                                            <legend id="Legend2" runat="server">Resultados de la busqueda</legend>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel2" runat="server" Height="100%" ScrollBars="Auto" EnableViewState="true">
                                                        <asp:Table runat="server" ID="SearchTable" EnableViewState="true">
                                                        </asp:Table>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </fieldset>
                                    </asp:View>
                                </asp:MultiView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="CP2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <MControls:ProcList ID="ProcedureList" runat="server" />

</asp:Content>


