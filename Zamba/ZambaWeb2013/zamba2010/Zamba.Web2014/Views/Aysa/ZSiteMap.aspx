<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZSiteMap.aspx.cs" Inherits="Views_Aysa_ZSiteMap" %>

<%@ Import Namespace="System.Web.Optimization" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
    <%: Scripts.Render("~/bundles/jqueryCore") %>
    <%: Scripts.Render("~/bundles/jqueryAddIns") %>
    <%: Scripts.Render("~/bundles/jqueryval") %>
    <%: Scripts.Render("~/bundles/bootstrap") %>

<!DOCTYPE html>
<html>
<head id="Head2" runat="server">


    <script type="text/javascript">
        function CloseMap() {
            parent.CloseInsert();
        }

        $(document).ready(function () {
           // $('a[href=#]').attr("disabled", "disabled"); da error
        });
    </script>

    <title></title>

</head>
<body style="background-color: White">
    <form runat="server" id="Form1" method="post">
        <asp:ScriptManager runat="server" ID="RadScriptManager2" ScriptMode="Release" />
        <div style="background-color: White; padding-left: 2px">
          <%--  <div style="text-align: right; background-color: White;">
                <input type="button" id="btnClose" value="Cerrar" style="border-style: solid; background-color: #092282; border-color: #092282; color: White;"
                    onclick="CloseMap();" />
            </div>--%>
            <ul class="sitemap-list">
                <li id="sitemap-item" style="border: none">
                    <telerik:RadSiteMap runat="server" ID="RadSiteMap1" CssClass="sitemap-list" ShowNodeLines="true">
                    </telerik:RadSiteMap>
                </li>
            </ul>
        </div>
    </form>
</body>
</html>
