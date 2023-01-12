<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Web.Models.ZambaEntities>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Annual Sales Data
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Annual Sales Data</h2>
    <p>
       
    </p>
    <% using(Html.BeginForm("SalesData", "Home", FormMethod.Get)) { %>
        <p>
            <b>Category:</b>
    <%--        <%: Html.DropDownListFor(model => model.WFWorkflow.Name, Model.WFWorkflow) %>
    --%>
        </p>
        <p>
            <b>Year:</b>
    <%--        <%: Html.DropDownListFor(model => model.WFStep.Name, Model.WFStep) %>
    --%>
        </p>
        <p>
            <input type="submit" value="Show Sales Data!" />
        </p>
        
        <% 
        // Only show the chart if the inputs have been selected
        if (!string.IsNullOrEmpty(Model.WFWorkflow.Name)) { %>
            <hr />
            <div style="text-align: center">
                <%: Html.Chart("SalesByYear",   // Action
                               "Charts",        // Controller
                               new {            // RouteData
                                   CategoryName = Model.WFWorkflow.Name,
                                   OrderYear = Model.WFStep.Name
                               },
                               new {            // HTML attributes
                                   alt = "Sales for " + Model.WFWorkflow.Name + " in " + Model.WFStep.Name,
                                   title = "Sales for " + Model.WFWorkflow.Name + " in " + Model.WFStep.Name,
                               }) %>
            </div>

        <% } %>
    <% } %>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
