<%@ Page Language="C#" AutoEventWireup="true" Inherits="ListadoEstablecimientos" Codebehind="ListadoEstablecimientos.aspx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="System.Web.Optimization" %>

<%: Scripts.Render("~/bundles/jqueryCore") %>
<%: Scripts.Render("~/bundles/jqueryAddIns") %>
<%: Scripts.Render("~/bundles/jqueryval") %>
<%: Scripts.Render("~/bundles/bootstrap") %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Tablero DOR</title>

    <link rel="Stylesheet" type="text/css" href="../../../Content/Styles/thickbox.css" />
    <link rel="Stylesheet" type="text/css" href="../../../Content/Styles/jquery-ui-1.8.6.css" />
    <link rel="Stylesheet" type="text/css" href="../../../Content/Styles/ZambaUIWeb.css" />
    <link rel="Stylesheet" type="text/css" href="../../../Content/Styles/ZambaUIWebTables.css" />
    <link rel="stylesheet" type="text/css" href="../../../Content/Styles/jq_datepicker.css" />
    <link rel="Stylesheet" type="text/css" href="../../../Content/Styles/GridThemes/WhiteChromeGridView.css" />

    <script type="text/javascript">
        $(window).on("load",function () {
            t = setTimeout("parent.hideLoading();", 500);
        });
    </script>

    <style type="text/css">
        .DataTable {
            border-collapse: collapse;
        }

            .DataTable td, table th {
                border: 1px solid black;
            }

        .NoneBorders {
            border: 0px solid black;
        }

        .BoldFont {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center">
            <asp:ScriptManager ID="RadScriptManager1" runat="server" ScriptMode="Release">
                <Scripts>
                </Scripts>
            </asp:ScriptManager>
            <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
            </telerik:RadAjaxManager>
            <center>
            <telerik:RadRotator ID="RadRotator1" runat="server" Width="1000px" ItemWidth="250px"
                Height="350px" ItemHeight="150px" ScrollDuration="500" FrameDuration="2000" EnableRandomOrder="true"
                PauseOnMouseOver="false" Skin="Windows7" RotatorType="CarouselButtons" BorderStyle="None"
                OnItemClick="btnShow_Click">
                <ItemTemplate>
                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("Imagen", "../../../forms/imgMantenimiento/{0}") %>'
                        AlternateText="Customer Image" />
                    <asp:HiddenField ID="hdnIndex" runat="server" Value='<%# Eval("doc_id", "{0}") %>' />
                    <table cellpadding="4">
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="White">Codigo: </asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblCodigo" Font-Bold="true" ForeColor="White" Text='<%# Eval("I601275", "{0}") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Font-Bold="true" ForeColor="White">Nombre: </asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNombre" Font-Bold="true" ForeColor="White" Text='<%# Eval("I601272", "{0}") %>'></asp:Label>
                            </td>
                        </tr>
<%--                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Font-Bold="true" ForeColor="White">Direccion: </asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblDireccion" Font-Bold="true" ForeColor="White" Text='<%# Eval("I601276", "{0}") %>'></asp:Label>
                            </td>
                        </tr>--%>
                    </table>
                </ItemTemplate>
            </telerik:RadRotator>
        </center>
        </div>
    </form>
</body>
</html>
