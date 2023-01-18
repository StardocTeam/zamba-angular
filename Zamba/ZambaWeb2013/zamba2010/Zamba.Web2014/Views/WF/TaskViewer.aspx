<%@ Page Language="C#" AutoEventWireup="false" CodeFile="TaskViewer.aspx.cs" Inherits="TaskViewer" MasterPageFile="~/MasterBlankpage.Master" EnableViewState="false" EnableEventValidation="false"%>
<%@ Register src="~/Views/UC/WF/UCWFExecution.ascx" tagname="UCWFExecution" tagprefix="UC6" %>
<%@ MasterType TypeName="MasterBlankPage" %>

<%@ Register src="~/Views/UC/Task/TaskHeader.ascx" TagName="UCTaskHeader" TagPrefix="UC3" %>
<%@ Register Src="~/Views/UC/Task/TaskDetail.ascx" TagName="UCTaskDetail" TagPrefix="UC5" %>
<%--<%@ Register Src="~/Views/UC/Task/TaskDetailUL.ascx" TagName="UCTaskDetailUL" TagPrefix="UC7" %>--%>

<asp:Content ID="Content3" ContentPlaceHolderID="header_js" Runat="Server">	
    
</asp:Content>

<asp:Content ID="Content5" runat="server" ContentPlaceHolderID="ContentPlaceHolder">
  
            <asp:HiddenField ID="hdnTaskID" runat="server" />
           <asp:HiddenField ID="hdnUserId"  runat="server" />
         <asp:HiddenField ID="hdnPostBack" runat="server" Value="False" />
            
            <input id="hdnDummyTabIndex" type="submit" style="position:absolute; top:-20"; left: -30/>
            
            <UC6:UCWFExecution ID="UC_WFExecution" runat="server" height="200" width="200" />  
           
    <div class="row"  ID="rowTaskHeader">
        <div class="col-md-12">
               <uc3:ucTaskHeader runat="server" ID="ucTaskHeader" />
        </div>
    </div>
     <div class="row" ID="rowTaskDetail">
        <div class="col-md-12">
               <%--          <uc7:UCTaskDetailUL runat="server" ID="ucTaskDetail1" />--%>
                        
                        <uc5:UCTaskDetail runat="server" ID="ucTaskDetail"/>
        </div>
    </div>
   
    <script type="text/javascript">
        $(document).ready(function () {
            $("#hdnDummyTabIndex").focus();
            $("#hdnDummyTabIndex").submit(function (evt) {
                evt.preventDefault();
            });
            $("#hdnDummyTabIndex").hide();
        });

<%--        function RegisterPostBack() {
            var hdnEvent = $("#<%= hdnPostBack.ClientID %>");
            hdnEvent.value = "True";
        }--%>

        function RefreshLocation() {
            RegisterPostBack();
            document.location = document.location;
        }

        function getToolBTaskH() {
            return $("#rowTaskHeader").height();
        }
    </script>
</asp:Content> 