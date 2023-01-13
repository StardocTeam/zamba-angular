<%@ Page Language="C#" AutoEventWireup="true" Inherits="Views_Aysa_ZSiteMap" CodeBehind="ZSiteMap.aspx.cs" %>

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

    <h2 style="border-radius: 5px; text-align:center; background-color:#25557d; color:white;">Mapa del sitio</h2>

    <form runat="server" id="Form" method="post">   
        <asp:ScriptManager runat="server" ID="RadScriptManager2" ScriptMode="Release" />
        <div style="background-color: White; padding-left: 2px;">
            <%--  <div style="text-align: right; background-color: White;">
                <input type="button" id="btnClose" value="Cerrar" style="border-style: solid; background-color: #092282; border-color: #092282; color: White;"
                    onclick="CloseMap();" />
            </div>--%>
           
            <ul >
                <li   id="sitemap-item" style=" border: none; list-style-type:none;">
                    <telerik:RadSiteMap runat="server" ID="RadSiteMap1" CssClass="sitemap-list" ShowNodeLines="true">
                    </telerik:RadSiteMap>
                </li>
            </ul>
        </div>
    
        <%--Acciones de usuario Inicio--%>
        <div style="background-color: White; padding-left: 2px; margin-top:4px;">
            <ul >
                <li id="sitemap-home" style="border:none; list-style-type: none;">    
                    <telerik:RadSiteMap runat="server" ID="RSMHome" CssClass="sitemap-list" ShowNodeLines="true">
                    </telerik:RadSiteMap>
                </li>
            </ul>
        </div>
        
        <%--Acciones de usuario Principal--%>
        <div style="background-color: White; padding-left: 2px; margin-top:4px;">
            <ul >
                <li id="sitemap-header" style="border: none; list-style-type: none;">            
                    <telerik:RadSiteMap runat="server" ID="RSMHeader" CssClass="sitemap-list" ShowNodeLines="true">
                    </telerik:RadSiteMap>
                </li>
            </ul>
        </div>
    </form>



</body>
</html>

    <script type="text/javascript">
        $(".rsmItem").children().css("color", "#045678");
        $(".rsmItem").children().css("font-family", "Times New Roman");
        $(".rsmItem").children().css("font-weight", "bold");

    </script>
