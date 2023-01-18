<%@ Page Language="C#" AutoEventWireup="true" Inherits="Views_Aysa_StandAloneSemaphore"
    Theme="AysaDiseno" Codebehind="StandAloneSemaphore.aspx.cs" %>

<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>
    <%: Scripts.Render("~/bundles/jqueryCore") %>
    <%: Scripts.Render("~/bundles/jqueryAddIns") %>
    <%: Scripts.Render("~/bundles/jqueryval") %>
    <%: Scripts.Render("~/bundles/bootstrap") %>

<html>
<head runat="server">
    <title>Semáforo Inspecciones</title>

    <link rel="Stylesheet" type="text/css" href="../../Content/Styles/thickbox.css" />
    <link rel="Stylesheet" type="text/css" href="../../Content/Styles/jquery-ui-1.8.6.css" />
    <link rel="Stylesheet" type="text/css" href="../../Content/Styles/ZambaUIWeb.css" />
    <link rel="Stylesheet" type="text/css" href="../../Content/Styles/ZambaUIWebTables.css" />
    <link rel="stylesheet" type="text/css" href="../../Content/Styles/jq_datepicker.css" />
    <link rel="Stylesheet" type="text/css" href="../../Content/Styles/GridThemes/WhiteChromeGridView.css" />
    
    <script>
        $(document).ready(function () {
            loadCliksInRows();
        });

        $(window).on("load",function () {

        });

        function loadCliksInRows() {
            if ($('.RowStyleSem').find('a') != null) {
                $('.RowStyleSem').click(function () {
                    if ($(this).find('a')[0]) {
                        var name = $(this).find(">td")[1].innerText;
                        parent.SwitchDocTaskForResults($(this).find('a')[0], true, "Inspecciones");
                        parent.HideHome();
                        return false;
                    }
                });

                $('.RowStyleSem').find('a').click(function () {
                    var name = $(this).parent().parent().find("td")[1].innerText;
                    parent.SwitchDocTaskForResults($(this), true, "Inspecciones");
                    parent.HideHome();
                    return false;
                });
            }

            if ($('.AltRowStyleSem').find('a') != null) {
                $('.AltRowStyleSem').click(function () {
                    if ($(this).find('a')[0]) {
                        var name = $(this).find(">td")[1].innerText;
                        parent.SwitchDocTaskForResults($(this).find('a')[0], true, "Inspecciones");
                        parent.HideHome();
                        return false;
                    }
                });

                $('.AltRowStyleSem').find('a').click(function () {
                    var name = $(this).parent().parent().find("td")[1].innerText;
                    parent.SwitchDocTaskForResults($(this), true, "Inspecciones");
                    parent.HideHome();
                    return false;
                });
            }
        }

        function Refresh_Click() {

        }

        $(window).on("load",function () {

        });
    </script>

    <style type="text/css">
        span {
            color: white;
            font-size: 12pt;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table id="controlContainer" runat="server" style="height: 100%; width: 100%">
                <tr style="height: 75%; width: 100%; border-width: 0px">
                    <td>
                        <div id="GridContainer" class="gridContainer" style="border-width: 0px">
                            <center>
                            <asp:GridView ID="grdInspecciones" runat="server"
                                AutoGenerateColumns="True" CssClass="GridViewStyle" GridLines="None">
                                <RowStyle CssClass="RowStyleSem" Wrap="false" />
                                <EmptyDataRowStyle CssClass="RowStyleSem" Wrap="false" />
                                <PagerStyle CssClass="PagerStyle" />
                                <SelectedRowStyle CssClass="RowStyleSem" Wrap="false"/>
                                <HeaderStyle CssClass="HeaderStyle" Wrap="false"/>
                                <EditRowStyle CssClass="RowStyleSem" Wrap="false"/>
                                <AlternatingRowStyle CssClass="AltRowStyleSem" Wrap="false"/>
                            </asp:GridView>
                            <asp:Label ID="lblMsg" runat="server" Visible="False"></asp:Label>
                        </center>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
