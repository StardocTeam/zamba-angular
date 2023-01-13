<%@ Page Language="C#" AutoEventWireup="true" Inherits="DoExportToPDF" Codebehind="DoExportToPDF.aspx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html>

<html  style="background-color:#efefef">
<head runat="server">
	<title>Zamba Web - Exportación</title>
	<telerik:RadStyleSheetManager id="RadStyleSheetManager1" runat="server" />
    <link href="../../../../Content/bootstrap.css" rel="stylesheet" />

	<style>
	EditorBody
	{
	    background-color:#efefef;
	    height:100%;
	    width:100%;
	    padding:0px;
	    margin:0px;
	}
	</style>
	<script>
	    function Page_Load()
	    {
    	    $("#RadEditor1Top").hide();
    	    $("#RadEditor1_BottomTable").hide();
    	    $("#RadEditor1Center").height(550);
	    }
	</script>
</head>
<body class="EditorBody" onload="Page_Load();">
    <form id="form1" runat="server" style="height:100%;width:100%;padding:0px">
	    <asp:ScriptManager ID="RadScriptManager1" runat="server" ScriptMode="Release">
		    <Scripts>
			    <%--Needed for JavaScript IntelliSense in VS2010--%>
			    <%--For VS2008 replace RadScriptManager with ScriptManager--%>
			    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
			    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
			    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
		    </Scripts>
	    </asp:ScriptManager>
	    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
	    </telerik:RadAjaxManager>
	    
	    <script>
	        Sys.Application.add_load(function loadHandler() {
	            $("#RadEditor1Top").hide();
    	        $("#RadEditor1_BottomTable").hide();
    	        $("#RadEditor1Center").height(550);
	        });
	    </script>
	    
	    <div style="background-color:white;height:100%;width:100%" >
	        <asp:Label ID="lblError" runat="server" Text="" Visible="False"/>
	        <asp:Button ID="btnExport" runat="server" OnClick="Export" Text="Exportar a PDF"  CssClass="btn btn-primary btn-xs" style="position:fixed" />
            <telerik:RadEditor ID="RadEditor1" runat="server" ContentFilters="DefaultFilters,PdfExportFilter"
                ToolsFile="DoExportToPDF_ToolsFile.xml" Height="600px" Width="100%">
                <ExportSettings FileName="Export" OpenInNewWindow="true">
                    <Pdf PageHeight="297mm" PageWidth="210mm" PageBottomMargin="10mm" 
                        PageTopMargin="10mm" PageLeftMargin="20mm" PageRightMargin="10mm" />
                </ExportSettings>
                <Content>
                </Content>
            </telerik:RadEditor>
	    </div>
           
	</form>
</body>
</html>
