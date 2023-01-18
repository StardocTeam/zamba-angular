<%@ Page Language="C#" AutoEventWireup="true" Inherits="Zamba.WebFormEditor.FormEditorMenu" Codebehind="FormEditorMenu.aspx.cs" %>

<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<html>
<head runat="server">
    <title>Zamba Form Editor</title>
    <style type="text/css">
        .TelerikModalOverlay {
            z-index: 100000 !important;
        }
    </style>

    <link rel="Stylesheet" type="text/css" href="Content/css/thickbox.css" />
    <link rel="Stylesheet" type="text/css" href="Content/css/jquery-ui-1.8.6.css" />
    <link rel="Stylesheet" type="text/css" href="Content/css/ZambaUIWeb.css" />
    <link rel="Stylesheet" type="text/css" href="Content/css/ZambaUIWebTables.css" />
    <link rel="stylesheet" type="text/css" href="Content/css/jq_datepicker.css" />
    <link rel="Stylesheet" type="text/css" href="Content/css/GridThemes/WhiteChromeGridView.css" />
    <link rel="stylesheet" href="Content/css/tabber.css" type="text/css" media="screen" />
    <link rel="Stylesheet" type="text/css" href="Content/css/GridThemes/GridViewGray.css" />
    <link href="Content/css/Zamba.css" type="text/css" rel="stylesheet" />
    <link href="Content/css/Zamba_table.css" type="text/css" rel="stylesheet" />

    <%: Scripts.Render("~/bundles/jqueryCore") %>
    <%: Scripts.Render("~/bundles/jqueryAddIns") %>
    <%: Scripts.Render("~/bundles/jqueryval") %>
    <%: Scripts.Render("~/bundles/bootstrap") %>
    <%: Scripts.Render("~/bundles/ZScripts") %>

    <script src="Content/scripts/jq_datepicker.js" type="text/javascript"></script>
    <script src="Content/scripts/thickbox-compressed.js" type="text/javascript"></script>
    <script type="text/javascript" src="Content/scripts/tabber.js"></script>
    <script type="text/javascript" src="Content/scripts/tabber.js"></script>

    <%--<script type="text/javascript" src="Content/scripts/jquery.validate.min.js"></script>
    <script type="text/javascript" src="Content/Scripts/jquery.quicksearch.js"></script>
    <script type="text/javascript" src="Content/scripts/jquery.decimalMask.1.1.1.min.js"></script>
    <script type="text/javascript" src="Content/scripts/jquery.caret.1.02.min.js"></script>
    <script type="text/javascript" src="Content/scripts/jquery.dataTables.js"></script>
    <script type="text/javascript" src="Content/scripts/jquery.scrollTo.js"></script>
    <script src="Content/scripts/Zamba.js" type="text/javascript"></script>
    <script src="Content/scripts/Zamba.Validations.js" type="text/javascript"></script>--%>

</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <telerik:RadScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />
            <br />
            <table summary="Resource Browser" style="margin: 15px; width: 90%; height: 90%; text-align: left">
                <tr>
                    <td colspan="4" style="background-color: LightSteelBlue; height: 25px; color: White; font-size: 12px; text-align: center; padding: 10px">&nbsp; Zamba Editor de Formularios
                    </td>
                </tr>


                <tr>

                    <td style="vertical-align: top; background-color: White; overflow: auto; padding: 5px" class="module">
                        <center> <strong>Seleccione el formulario que desea editar</strong></center>
                        <br />
                        <br />
                        <telerik:RadTreeView ID="RadTreeView1" runat="server" Width="350px"
                            Skin="Vista" Font-Size="Small" ShowLineImages="true" OnNodeClick="RadTreeView1_NodeClick">
                        </telerik:RadTreeView>
                    </td>
                    <td style="width: 40px">&nbsp;</td>
                    <td style="vertical-align: top; background-color: White; overflow: auto; padding: 5px" class="module">

                        <center>   <strong>Ingrese los datos para crear un nuevo formulario</strong></center>
                        <br />
                        <br />
                        <asp:Label runat="server" ID="lblerror" Text="" ForeColor="Red"></asp:Label>
                        <br />
                        <br />
                        <table id="tblNewForm">
                            <tr>
                                <td>Nombre
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" Style="width: 250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp</td>
                            </tr>
                            <tr>
                                <td>Ruta
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPath" runat="server" Style="width: 250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp</td>
                            </tr>
                            <tr>
                                <td>Tipo de Formulario:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="DropDownListFormTypes" Style="width: 250px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp</td>
                            </tr>
                            <tr>
                                <td>Entidad:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="DropDownListEntity" Style="width: 250px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button runat="server" ID="BtnNewForm" Text="Crear Formulario" OnClick="BtnNewForm_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>

                </tr>


            </table>
        </div>
    </form>
</body>
</html>
