<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaskAsociated.aspx.cs" Inherits="Views_WF_TaskDetails_TaskAsociated" %>
<%@ Register Src="~/Views/UC/Grid/ucDocAssociatedGrid.ascx" TagName="ucDocAssociatedGrid" TagPrefix="uc2" %>
<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>
<html >
<head id="Head1" runat="server">
    <title>Asociados</title>
    <asp:PlaceHolder runat="server">
        <%: Styles.Render("~/bundles/Styles/jquery")%>    
        <%: Styles.Render("~/bundles/Styles/ZStyles")%>
        <%: Styles.Render( "~/bundles/Styles/bootstrap")%>
        <%: Styles.Render("~/Content/bootstrap-responsive.css")%>
        <%: Styles.Render("~/bundles/Styles/grids")%>
        
        <%: GetJqueryCoreScript()%>
        <%: Scripts.Render("~/bundles/jqueryAddIns") %>
        <%: Scripts.Render("~/bundles/modernizr") %>
        <%: Scripts.Render("~/bundles/ZScripts") %>
        <%: Scripts.Render("~/bundles/bootstrap") %>
    </asp:PlaceHolder>
</head>
<body>
    <form id="form2" runat="server">
        <div id="divButtons">

            <button id="btnRefresh" type="button" class="btn btn-default btn-xs"  onclick="Refresh_Click()" style="margin:5px">
                <span class="glyphicon glyphicon-refresh"></span> Refrescar                                   
            </button>


        </div>
        <div id="tbDocAsoc" style="overflow:auto;width:100%">
            <uc2:ucDocAssociatedGrid ID="ucDocAssociatedGrid" runat="server" />
        </div>
    </form>

    <script type="text/javascript">
        $(document).ready(function () {
            var screenHeight = $(window).height() - $("#divButtons").height();
            $("#tbDocAsoc").height(screenHeight);
            loadCliksInRows();
        });

        function loadCliksInRows() {
            if ($('.RowStyleAsoc').find('a') != null) {
                $('.RowStyleAsoc').click(function () {
                    if ($(this).find('a')[0]) {
                        var name = $(this).find(">td")[1].innerText;
                        parent.parent.SwitchDocTaskForResults($(this).find('a')[0], true, name);
                        return false;
                    }
                });

                $('.RowStyleAsoc').find('a').click(function () {
                    var name = $(this).parent().parent().find("td")[1].innerText;
                    parent.parent.SwitchDocTaskForResults($(this), true, name);
                    return false;
                });
            }

            if ($('.AltRowStyleAsoc').find('a') != null) {
                $('.AltRowStyleAsoc').click(function () {
                    if ($(this).find('a')[0]) {
                        var name = $(this).find(">td")[1].innerText;
                        parent.parent.SwitchDocTaskForResults($(this).find('a')[0], true, name);
                        return false;
                    }
                });

                $('.AltRowStyleAsoc').find('a').click(function () {
                    var name = $(this).parent().parent().find("td")[1].innerText;
                    parent.parent.SwitchDocTaskForResults($(this), true, name);
                    return false;
                });
            }
        }

        function Refresh_Click() {
            document.location = document.location;
            ShowLoadingAnimation();
        }

        function getdivAsocTBH() {
            return $("#divAsocTB").height();
        }

        $(window).load(function () {
            parent.parent.hideLoading();
            hideLoading();
        });
    </script>
</body>
</html>
