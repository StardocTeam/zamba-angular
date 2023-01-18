<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WFExecutionForEntryRules.aspx.cs" Inherits="Views_WF_WFExecutionForEntryRules" EnableViewState="false"%>
<%@ Reference Control="~/Views/UC/WF/Rules/UCDoRequestData.ascx" %>
<%@ Reference Control="~/Views/UC/WF/Rules/UCDoShowTable.ascx" %>
<%@ Reference Control="~/Views/UC/WF/Rules/UCDoScreenMessage.ascx" %>
<%@ Reference Control="~/Views/UC/WF/Rules/UCDoInputIndex.ascx" %>
<%@ Reference Control="~/Views/UC/WF/Rules/UCDoAsk.ascx" %>
<%@ Reference Control="~/Views/UC/WF/Rules/UCDoAskDesition.ascx" %>
<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html >
	<head id="Head1" runat="server">
		<title>Historial</title>
		<asp:PlaceHolder runat="server">
		<%: GetJqueryCoreScript()%>
		<%: Scripts.Render("~/bundles/jqueryAddIns") %>
		<%: Scripts.Render("~/bundles/modernizr") %>
		<%: Scripts.Render("~/bundles/tabber") %>
		<%: Scripts.Render("~/bundles/ZScripts") %>
		<%: Styles.Render("~/bundles/Styles/jquery")%>
		<%: Styles.Render("~/bundles/Styles/ZStyles")%>
		<link rel="Stylesheet" type="text/css" href="../../Content/Styles/GridThemes/WhiteChromeGridView.css" />
		<link rel="Stylesheet" type="text/css" href="../../Content/Styles/GridThemes/GridViewGray.css" />	
		</asp:PlaceHolder>
	</head>
	<body>
		<form id="form2" runat="server">
			<%--<script type="text/javascript">
				function getHdnChecks() {
					return $('#<%= hdnChecks.ClientID %>');
				}
				function MakeResize(objDlg, pnlRules) {
					if (parent !== document) {
						var height = (pnlRules.css("display") != "none") ? pnlRules.height() : $("#divMsg").height();

						parent.ResizeEntryRulesPanel($("#divContainer").width(), height - 400);
					}
				}
			</script>--%>

		<asp:HiddenField ID="hdnCurrTaskID" runat="server" value="-1"/>
		<asp:HiddenField ID="hdnRuleName" runat="server" value="-1"/>
		<asp:HiddenField ID="hdnMustHideLoading" runat="server" value=""/>
		<asp:HiddenField ID="hdnChecks" runat="server" value=""/>

		<div id="divContainer" style="height:auto">
            <div id="pnlUcRules" runat="server" style="overflow: hidden;"></div>
		<%--	<div id="divMsg" runat="server" style="text-align: center; display:none !important" title="Zamba Software...">--%>
				<%--<span id="spnMsg"></span>--%>
			
<%--				<input type="button" value="OK" id="msgOk" onclick="ShowContainer(false);" />--%>
			</div>
		<%--</div>--%>

        <script type="text/javascript">
            function FixAndPosition(objDlg, pnlRules) {
                objDlg.css("top", "0px");
                objDlg.css("left", "0px");
                $("#pnlUcRules").css("height", "auto");
                objDlg.css("position", "absolute");
                $("body").height($("body div:first").height());
                MakeResize(objDlg, pnlRules);
            }
            $(window).load(function () {
                if ($("#hdnMustHideLoading").val() != "")
                    parent.hideLoading();
            });
            function ShowContainer(value) {
                if (parent != document) {
                    parent.ShowEntryRulesPanel(value);
                }
            }
            $(document).ready(function () {
                if (parent.ShowLoadingAnimation) parent.ShowLoadingAnimation();
                var btnOk = $('#hdnRuleName').val();
                if (btnOk != null && btnOk != "")
                    $("#" + btnOk).click(function () { ShowContainer(false); parent.ShowLoadingAnimation(); });
            });
            function AsociateForm(formid, docid, doctypeid, taskid, continueWithCurrentTasks, dontOpenTaskAfterInsert, fillCommonAttributes, haveSpecificAtt) {
                parent.AsociateForm(formid, docid, doctypeid, taskid, continueWithCurrentTasks, dontOpenTaskAfterInsert, fillCommonAttributes, haveSpecificAtt);
            }
		</script>
		</form>
	</body>
</html>