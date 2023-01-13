<%@ Page Language="C#" AutoEventWireup="true" Inherits="Views_Aysa_IntimationsSemaphore" Theme="AysaDiseno" Codebehind="IntimationsSemaphore.aspx.cs" %>

<%@ Import Namespace="System.Web.Optimization" %>

<%: Scripts.Render("~/bundles/jqueryCore") %>
<%: Scripts.Render("~/bundles/jqueryAddIns") %>
<%: Scripts.Render("~/bundles/jqueryval") %>
<%: Scripts.Render("~/bundles/bootstrap") %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Semáforo Intimaciones</title>

    <link rel="Stylesheet" type="text/css" href="../../../Content/Styles/thickbox.css" />
    <link rel="Stylesheet" type="text/css" href="../../../Content/Styles/jquery-ui-1.8.6.css" />
    <link rel="Stylesheet" type="text/css" href="../../../Content/Styles/ZambaUIWeb.css" />
    <link rel="Stylesheet" type="text/css" href="../../../Content/Styles/ZambaUIWebTables.css" />
    <link rel="stylesheet" type="text/css" href="../../../Content/Styles/jq_datepicker.css" />
    <link rel="Stylesheet" type="text/css" href="../../../Content/Styles/GridThemes/WhiteChromeGridView.css" />

    <script type="text/javascript">

        $(document).ready(function () {

            loadCliksInRows();

        });

        $(window).on("load",function () {

            //t = setTimeout("parent.hideLoading();", 500);
        });

        function loadCliksInRows() {

            if ($('.RowStyleSem').find('a') != null) {
                $('.RowStyleSem').click(function () {
                    if ($(this).find('a')[0]) {
                        var name = $(this).find(">td")[1].innerText;
                        parent.SwitchDocTaskForResults($(this).find('a')[0], true, "Intimaciones");
                        parent.HideHome();
                        return false;
                    }
                });

                $('.RowStyleSem').find('a').click(function () {
                    var name = $(this).parent().parent().find("td")[1].innerText;
                    parent.SwitchDocTaskForResults($(this), true, "Intimaciones");
                    parent.HideHome();
                    return false;
                });
            }

            if ($('.AltRowStyleSem').find('a') != null) {
                $('.AltRowStyleSem').click(function () {
                    if ($(this).find('a')[0]) {
                        var name = $(this).find(">td")[1].innerText;
                        parent.SwitchDocTaskForResults($(this).find('a')[0], true, "Intimaciones");
                        parent.HideHome();
                        return false;
                    }
                });

                $('.AltRowStyleSem').find('a').click(function () {
                    var name = $(this).parent().parent().find("td")[1].innerText;
                    parent.SwitchDocTaskForResults($(this), true, "Intimaciones");
                    parent.HideHome();
                    return false;
                });
            }
        }

        function Refresh_Click() {

            parent.ShowLoadingAnimation();
            document.location = document.location;
        }

        $(window).on("load",function () {
            //t = setTimeout("parent.hideLoading();", 500);
        });

        //Evalua si alguien abrio la ventana y si fue zamba, si es asi abre la tarea y selecciona la ventana
        //de Zamba.
        function OpenTaskInOpener(anchor, asTask, taskName) {

            var url = anchor.href;

            if (window.opener != null && (window.opener.document.location.href.indexOf('Zamba') != -1 ||
                    window.opener.document.location.href.indexOf('zamba') != -1)) {

                window.opener.SwitchDocTaskForResults(anchor, asTask, taskName);
                window.opener.SelectTaskFromModal();
                window.opener.focus();
            }
        }

    </script>

    <style type="text/css">
        span {
            color: white;
            font-size: 12pt;
        }

        html, body {
            overflow: hidden !important;
            margin: 0 !important;
            padding: 0 !important;
        }

        #table-wrapper {
            overflow: auto;
            width: 100%;
            height: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="table-wrapper">
            <table id="controlContainer" runat="server">
                <tr>
                    <td>
                        <div id="GridContainer" class="gridContainer">
                            <asp:GridView ID="grdIntimaciones" runat="server"
                                AutoGenerateColumns="True" CssClass="GridViewStyle" GridLines="None">
                                <RowStyle CssClass="RowStyleSem" Wrap="false" />
                                <EmptyDataRowStyle CssClass="RowStyleSem" Wrap="false" />
                                <PagerStyle CssClass="PagerStyle" />
                                <SelectedRowStyle CssClass="RowStyleSem" Wrap="false" />
                                <HeaderStyle CssClass="HeaderStyle" Wrap="false" />
                                <EditRowStyle CssClass="RowStyleSem" Wrap="false" />
                                <AlternatingRowStyle CssClass="AltRowStyleSem" Wrap="false" />
                            </asp:GridView>
                            <asp:Label ID="lblMsg" runat="server" Visible="False"></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
