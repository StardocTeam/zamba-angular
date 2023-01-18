<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Annual Sales Data (With Fancy JavaScript!)
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            // Populate the Categories and Years DDLs with the values from the server
            $.getJSON('/Api/Years',        // URL that returns data of interest
                      function (result) {       // The function that executes once the data has been returned
                          var years = $("#years");
                          $.each(result, function (index, year) {
                              years.append($("<option />").val(year).text(year));
                          });

                          $.getJSON('<%=Page.ResolveClientUrl("~/Api/Categories")%>',        // URL that returns data of interest
                              function (result) {       // The function that executes once the data has been returned
                                  var categories = $("#categories");
                                  $.each(result, function (index, categoryName) {
                                      categories.append($("<option />").val(categoryName).text(categoryName));
                                  });
                                  
                                  // Load the image
                                  UpdateImage();
                               });
                      });

            // Add client-side event handlers to these DDLs
            $("#years").change(UpdateImage);
            $("#categories").change(UpdateImage);
        });

        function UpdateImage() {
            var selectedYear = $("#years").val();
            var selectedCategory = $("#categories").val();

            $("#chart").fadeOut(function () {
                $(this).attr('src', '/Charts/SalesByYear?CategoryName=' + escape(selectedCategory) + '&OrderYear=' + escape(selectedYear) + '&showTitle=false')
                       .attr('alt', 'Sales for ' + selectedCategory + ' in ' + selectedYear)
                       .attr('title', 'Sales for ' + selectedCategory + ' in ' + selectedYear);

                $(this).fadeIn();
            });
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Annaul Sales Data (With Fancy JavaScript)</h2>
    <p>
        This demo shows how to gussy up the sales data charting example using a tad of JavaScript and jQuery.
    </p>
    <div style="text-align: center">
        <h2 style="text-align: center">Sales For <select id="categories"></select> In <select id="years"></select></h2>
        <img id="chart" style="display:none" />
    </div>
</asp:Content>

