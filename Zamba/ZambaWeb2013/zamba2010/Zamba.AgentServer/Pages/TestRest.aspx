<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestRest.aspx.cs" Inherits="Zamba.AgentServer.Pages.TestRest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">

    function Test()
   {
       $.ajax({
        cache: false,
        type: "GET",
        async: false,
        dataType: "json",
        url: "http://localhost:56524/ws/ucmservice.svc/details/Boston",
        success: function (details) {
            alert('ok');
            $.each(details.DetailsResult, populateDropdown);

        },
        error: function (xhr) {
            alert('err');
            alert(xhr.responseText);
        }
    });
}


// Populate drop-down box with JSON data (menu)
populateDropdown = function () {
    $("#OutPut").append(this.Usuario);
};



</script>

<input type="button" onclick="Test();" title="Test" />
<div id="OutPut"></div>

</asp:Content>
