<%@ Page Language="C#" AutoEventWireup="true" Inherits="Views_Aysa_GDI_HomeReports" Codebehind="HomeReports.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<script type="text/javascript">
 function ShowReports() {
            ShowLoadingAnimation();
            
            $("#divTabDOR").remove();
            $("#dvSiteMap").remove();
            $("#divSem").remove();
            if (document.getElementById('divReports') == null) {
                var myIFrame = '<div id="divReports" style="padding-top:20px"><iframe id="TabReports" style="width:95%; height:80%; background-color:white; border: 1px solid transparent" src="ComplaintsReport.aspx" frameborder="0"></iframe></div>';
                $(myIFrame).appendTo('#second-div-presentation-Main');
                $("#second-div-presentation-Main").css("background", "#0e2b8d");
            }
            else {
                var IfSem = $("#TabReports");
                if (IfSem != null && IfSem[0] != null) {
                    IfSem[0].contentWindow.location = "about:blank";
                    IfSem[0].contentWindow.location = "IntimationsReport.aspx";

                }
                else {
                    if (IfSem.context != null) {
                        IfSem[0].contentWindow.location = "about:blank";
                        IfSem.context.location = "IntimationsReport.aspx";

                    }
                }
            }
            hideLoading();
        }
        function showReport2() { 
         
        }

        function ShowLoadingAnimation() {
            $(document).scrollTop(0);
            $("#loading").fadeIn("slow",function() {
                                    $("#loading").css("filter", "alpha(opacity=30)");
                                    $("#loading").css("opacity", "0.3");
                                  });
        }
        function hideLoading() {
            $("#loading").fadeOut("slow");
        }

        </script>
<body>
    <form id="form1" runat="server">
    <div>
        <input id="BtnComplaintsReport" type="button" value="Reporte de Denuncias" onclick="ShowReports();" />
        <input id="BtnIntimationsReport" type="button" value="Reporte de Intimaciones" />
        
    </div>
    </form>
</body>
</html>
