<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeneralReportViewer.aspx.cs" Inherits="Web.GeneralReportViewer" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ReportList.ascx" TagName="ReportList" TagPrefix="ReportList" %>
<%@ Register Src="~/UserDistribution.ascx" TagName="UserDistribution" TagPrefix="Users" %>
<%@ Register Src="~/TasksByWorkflow.ascx" TagName="TasksDistribution" TagPrefix="Tasks" %>
<%@ Register Src="~/TasksByState.ascx" TagName="TasksByState" TagPrefix="Tasks" %>

<%@ Register Src="~/Consultas.ascx" TagName="Consultas" TagPrefix="Hardcoded" %>
<%@ Register Src="~/DocumentacionFaltante.ascx" TagName="DocumentacionFaltante" TagPrefix="Hardcoded" %>
<%@ Register Src="~/Indemnizaciones.ascx" TagName="Indemnizaciones" TagPrefix="Hardcoded" %>
<%@ Register Src="~/IndemnizacionesXImporte.ascx" TagName="IndemnizacionesXImporte" TagPrefix="Hardcoded" %>
<%@ Register Src="~/Reclamos.ascx" TagName="Reclamos" TagPrefix="Hardcoded" %>
<%@ Register Src="~/ReclamosXDia.ascx" TagName="ReclamosXDia" TagPrefix="Hardcoded" %>
<%@ Register Src="~/Siniestros.ascx" TagName="Siniestros" TagPrefix="Hardcoded" %>

<!DOCTYPE html>
<html>
<head runat="server">
<meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>

    <title>HDI Zamba Reportes</title>

 <!-- Bootstrap -->
    <link href="~/content/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="~/content/css/bootstrap-theme.min.css" rel="stylesheet"/>
    <link href="~/content/css/jquery-ui-1.8.6.css" rel="stylesheet"/>

  <%-- <link href="~/scripts/select2-3.4.6/select2.css" rel="stylesheet"/>

    <script src="~/scripts/select2-3.4.6/select2.js"></script>--%>

  <%--  <link href="Content/css/ZambaUIWeb.css" rel="stylesheet" type="text/css" />--%>
</head>
<body>
    <form id="form1" runat="server">

      <asp:ScriptManager ID="RadScriptManager1" runat="server">
		    <Scripts>
		    </Scripts>
	    </asp:ScriptManager>
  
 <div class="navbar navbar-default navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
        <a class="navbar-brand" href="#">Zamba Reportes</a>
       <%-- <div class="clientPhrase navbar-brand">
                            </div>
                            <div class="clientlogo navbar-brand" >
                            </div>--%>
 
            </div>
 
         <%--   <div class="navbar-collapse collapse">--%>
               
           <div class="btn-group  pull-right">
  <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown">
    Reportes <span class="caret"></span>
  </button>
           <ul class="dropdown-menu" id="reportsul" role="menu">
   
     <li><a href="#">Another action</a></li>
    <li>
    
<asp:UpdatePanel runat="server" id="udplist" >
<ContentTemplate>
   <ReportList:ReportList runat="server" id="ReportList" OnShowReport="ShowReport" OnClearReport="ClearReport"></ReportList:ReportList>
</ContentTemplate>
</asp:UpdatePanel>
  </li>
  
    <li><a href="#">Something else here</a></li>
    <li class="divider"></li>
    <li><a href="#">Separated link</a></li>
  </ul>
            </div>
          
      <%--  </div>--%>
    </div>
  </div>
 
        

  
    <div class="container" style="margin-top:50px">
       
      
<asp:UpdatePanel runat="server" id="udpreports" UpdateMode="Conditional">
<ContentTemplate>
<div runat="server" id="DivReports">
</div>
</ContentTemplate>
</asp:UpdatePanel>
   
       
        <div id="errMsg" title="Consulta Finalizada" style="display:none;"></div> 
        
         <hr />
        <footer>
            <p>&copy; <% = DateTime.Now.Year %> - Stardoc.com.ar</p>
        </footer>
    </div>
    
    

<script type="text/javascript">
   

      

    function setIframeHeight(iframeId) /** IMPORTANT: All framed documents *must* have a DOCTYPE applied **/
    {
        var ifDoc, ifRef = document.getElementById(iframeId);

        try {
            ifDoc = ifRef.contentWindow.document.documentElement;
        }
        catch (e) {
            try {
                ifDoc = ifRef.contentDocument.documentElement;
            }
            catch (ee) {
                alert(ee);
            }
        }

        if (ifDoc) {
            ifRef.height = 1;
            ifRef.height = ifDoc.scrollHeight + 12;
            ifRef.width = 1;
            ifRef.width = ifDoc.scrollWidth + 12 ; 
        }
    }
</script>

     <script type="text/javascript">
         var doc = document;

         $(document).ready(function () {
             ShowLoadingAnimation();

                 
             //SetReportListSize();
         });

        
              

       
         function showError(ReportName) {
             $("#errMsg").html("Se produjo un error al cargar el reporte " + ReportName);
             $("#errMsg").dialog({ closeOnEscape: false, open: function (event, ui) { $(".ui-dialog-titlebar-close").hide(); }, autoOpen: true, modal: true, width: 500, position: 'middle', resizable: false, buttons: { "Close": function () { $(this).dialog("close"); } } });
         }

    </script>
      
                
               
 
 <script type="text/javascript">
     Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
     function EndRequestHandler(sender, args) {
         hideLoading();
      
       
         //SetReportListSize();
      }

     Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(initializeRequestHandler)
     function initializeRequestHandler(sender, args) {
         ShowLoadingAnimation();
     }

     function ShowLoadingAnimation() {
         $(document).scrollTop(0);
         $("#loading").fadeIn("slow", function () {
             $("#loading").css("filter", "alpha(opacity=30)");
             $("#loading").css("opacity", "0.3");
         });
     }
     function hideLoading() {
         $("#loading").fadeOut("slow");
     }

     window.onload = function () {
         hideLoading();
     };

     function setIframeHeight(iframeId) /** IMPORTANT: All framed documents *must* have a DOCTYPE applied **/
     {
         var ifDoc, ifRef = document.getElementById(iframeId);

         try {
             ifDoc = ifRef.contentWindow.document.documentElement;
         }
         catch (e) {
             try {
                 ifDoc = ifRef.contentDocument.documentElement;
             }
             catch (ee) {
                 alert(ee);
             }
         }

         if (ifDoc) {
             ifRef.height = 1;
             ifRef.height = ifDoc.scrollHeight + 12;
             ifRef.width = 1;
             ifRef.width = ifDoc.scrollWidth + 12;
         }
     }
</script>


   
    </form>
       <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
 
    <script src="Scripts/jquery-ui-1.10.4.min.js" type="text/javascript"></script>
    <script src="Scripts/modernizr-2.7.2.js" type="text/javascript"></script>
    <script type="text/javascript">
        var doc = document;
                   
        function showError(ReportName) {
            $("#errMsg").html("Se produjo un error al cargar el reporte " + ReportName);
            $("#errMsg").dialog({ closeOnEscape: false, open: function (event, ui) { $(".ui-dialog-titlebar-close").hide(); }, autoOpen: true, modal: true, width: 500, position: 'middle', resizable: false, buttons: { "Close": function () { $(this).dialog("close"); } } });
        }

    </script>
    
</body>
</html>
