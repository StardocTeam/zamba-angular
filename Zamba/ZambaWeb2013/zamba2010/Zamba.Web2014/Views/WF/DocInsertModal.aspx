<%@ Page Language="C#" AutoEventWireup="false" CodeFile="DocInsertModal.aspx.cs" Inherits="Views_WF_DocInsertModal" MasterPageFile="~/MasterBlankpage.Master" EnableViewState="false"%>
<%@ MasterType TypeName="MasterBlankPage" %>
<%@ Register src="~/Views/UC/Viewers/FormBrowser.ascx" tagname="FormBrowser" tagprefix="uc1" %>

<asp:Content ID="Content5" runat="server" ContentPlaceHolderID="ContentPlaceHolder">  
    <div style="align-content:center" id="Container">
        <div>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
        <div id="divFormContainer" >
            <uc1:FormBrowser ID="FormBrowser" runat="server" />
        </div>
    </div>

    <script type="text/javascript">
        $(window).load(function () {
            
            $(document).find("body").css("overflow", "auto");
            if ($("#divFormContainer").length) {
                $("#divFormContainer").bind("resize", function (event) {
                    ResizeFormBrowser();
                });
            }
            setTimeout("ResizeFormBrowser();", 50);
        });

        function ResizeFormBrowser() {
            
            if (parent !== document) {
                $(document).height($("#Container").height());
                parent.ResizeTBIframe($("#Container").width() + 2, $("#Container").height() + 20);
            }
        }

        function Cerrar() {
            
            parent.CloseInsert();
        }
    </script>
</asp:Content>