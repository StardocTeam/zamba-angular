<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UsrPsswrd.aspx.cs" Inherits="Views_Security_Default" %>

<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9; IE=8; IE=7; IE=EDGE,chrome=1" />
    <link rel="Stylesheet" type="text/css" href="../../Content/Styles/ZambaUIWeb.css" />
    <link rel="Stylesheet" type="text/css" href="../../Content/Styles/ZambaUIWebTables.css" />
    <link rel="Stylesheet" type="text/css" href="../../Content/Styles/jquery-ui-1.8.6.css" />

    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">

    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>

    <%: Scripts.Render("~/bundles/jqueryCore") %>
    <%: Scripts.Render("~/bundles/jqueryAddIns") %>
    <%: Scripts.Render("~/bundles/bootstrap") %>

    <script src="../../scripts/jq_datepicker.js" type="text/javascript"></script>
    <script src="../../scripts/Zamba.Validations.js" type="text/javascript"></script>
    <script src="../../scripts/zamba.js" type="text/javascript"></script>
    <script src="../../scripts/thickbox-compressed.js" type="text/javascript"></script>

    <script type="text/javascript">
        function CloseChangePassDlg() {
            parent.CloseChangePassDlg();
        }
    </script>

</head>

<body>
    <form id="form" runat="server" style="padding: 5px">
        <div id="container">
            <table id='tblform' style="width: 100%">
                <tr>
                    <td>
                        <div style="padding: 5px">
                            <table style="padding: 5px">
                                <tr>
                                    <td>
                                        <span>Clave actual: </span>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" TextMode="Password" ID='CurrentPassword' class="CurrentPassword form-control" style="margin-bottom: 15px; margin-left: 13px;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span>Nueva Clave: </span>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="NewPassword" TextMode="Password" class="NewPassword form-control" style="margin-bottom: 15px ;  margin-left: 13px;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span>Confirme Nueva Clave: </span>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="NewPassword2" TextMode="Password" class="NewPassword2 form-control" Style="margin-left: 13px;" />
                                    </td>
                                </tr>

                                <tr class="row">
                                    <td class="style1" style="text-align: center; float: right">
                                        <br />
                                        <asp:LinkButton runat="server" title="Guardar" ID="lnkGuardar" OnClick="lnkSave_clic"
                                            class="btn btn-primary" Style="float: right; margin-right: -150px; margin-top: 25px;">Guardar</asp:LinkButton>
                                    </td>

                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <div id="divError" style="display: none; padding-bottom: 5px" runat="server">
                            <asp:Label ID="lblMsj" runat="server" Visible="true" CssClass="error" Style="padding: 0px 0px 10px 0px"></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>

</html>
