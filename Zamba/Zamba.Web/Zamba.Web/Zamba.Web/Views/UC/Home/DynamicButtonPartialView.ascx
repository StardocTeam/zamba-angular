<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="DynamicButtonPartialView" CodeBehind="DynamicButtonPartialView.ascx.cs" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="Zamba.Core" %>
<%@ Import Namespace="System.Collections.Generic" %>
 <link rel="stylesheet" type="text/css" href="../../Content/css/ZClass.css" />

<%if (RenderButtons.Count == 0)
    {%>
            <div class="notAvailableOptionsMessage ng-scope" style="">
               <p>No hay acciones de usuario para ser ejecutadas</p>
            </div>
            <% } %>


<%foreach (var group in this.RenderButtons.Select(b => new { b.GroupName, b.GroupClass, b.PlaceId }).Distinct())
    {%>
<%if (group.PlaceId != Zamba.Core.ButtonPlace.WebHeader)
    {%>
<div class=" col-sm-2" style="padding: 0 0 0 0">
    <% } %>
    <div style="padding: 5px 5px 5px 5px; text-align: center;" class="<%--DynamicBtnGroup--%> <%--dropdown--%> <%--btn--%> <%=group.GroupClass %> DynamicBtnListHeader">
        <p id="Grn" <%--data-toggle="dropdown"--%> class="" style="text-align: center; font-size: 15px; font-weight: bold; color: var(--ZBlue); min-height: 14px"><%=(group.GroupName.Equals("") ? "Acciones" : group.GroupName) %></p>

        <ul class=" col-sm-12 <%--dropdown-menu--%>" id="dynamicbtnlist" style="font-size: 14px; padding: 1px 0 1px 0"
            <%if (group.PlaceId == Zamba.Core.ButtonPlace.WebHeader)
            {%>
            <% } %>>
            <%foreach (IDynamicButton item in this.RenderButtons.Where(b => b.GroupName == group.GroupName))
                {%>

            <%if (group.GroupName.Contains("Reporte") || item.Caption.Contains("Reporte") || item.Caption.Contains("Listado"))
                {%>
            <li style="text-align: center" class="DynamicBtnLIRpt">
            <% }
                else
                { %>
            <li style="text-align: center" class="DynamicBtnLI">
            <% } %>

                <a href="#" class="<%=item.ViewClass %> list-group-item "  id="<%=item.ButtonId %>" title="<%=item.Caption%>" onclick="<%=(item.TypeId == Zamba.Core.ButtonType.Rule)?"PreventClick(event); RuleButtonClick(this," + item.RuleId + ");" : string.Empty %>" ">
                    <%=item.Caption%>
                </a>
            </li>
            <% } %>
        </ul>
    </div>
    <%if (group.PlaceId != Zamba.Core.ButtonPlace.WebHeader)
        {%>
</div>
<% } %>
<% } %>


<style>
    .nav .open > a, .nav .open > a:hover, .nav .open > a:focus {
        background: none !important;
    }

    #userActionHeader > li > a:hover {
        background: none !important;
    }

    .DynamicBtnListHeader {
        font-family: -apple-system, system-ui, BlinkMacSystemFont, "Segoe UI", Roboto, "Helvetica Neue", "Fira Sans", Ubuntu, Oxygen, "Oxygen Sans", Cantarell, "Droid Sans", "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Emoji", "Segoe UI Symbol", "Lucida Grande", Helvetica, Arial, sans-serif;
        -apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue","Fira Sans",Ubuntu,Oxygen,"Oxygen Sans",Cantarell,"Droid Sans","Apple Color Emoji","Segoe UI Emoji","Segoe UI Emoji","Segoe UI Symbol","Lucida Grande",Helvetica,Arial,sans-serifbody font-size: 16px;
    }

    #dynamicbtnlist {
        list-style-type: none;
        /*border-color: rgb(207,207,207);
    border-style: solid;
    border-width: 1px;*/
        background-color: white;
        border-radius: 2px;
        padding: 20px 10px 20px 10px;
        background-color: rgb(255, 255, 255);
        background-image: none;
        background-origin: padding-box;
        background-position-x: 0px;
        background-position-y: 0px;
        border-bottom-color: rgba(0, 0, 0, 0.9);
        border-bottom-style: none;
        border-bottom-width: 0px;
        border-image-outset: 0px;
        border-image-repeat: stretch;
        border-image-slice: 100%;
        border-image-source: none;
        border-image-width: 1;
        border-left-color: rgba(0, 0, 0, 0.9);
        border-left-style: none;
        border-left-width: 0px;
        border-right-color: rgba(0, 0, 0, 0.9);
        border-right-style: none;
        border-right-width: 0px;
        border-top-color: rgba(0, 0, 0, 0.9);
        border-top-style: none;
        border-top-width: 0px;
        box-shadow: rgba(0, 0, 0, 0.15) 0px 0px 0px 1px, rgba(0, 0, 0, 0.2) 0px 2px 3px 0px;
        box-sizing: border-box;
        color: rgba(0, 0, 0, 0.9);
        display: block;
        font-family: -apple-system, system-ui, BlinkMacSystemFont, "Segoe UI", Roboto, "Helvetica Neue", "Fira Sans", Ubuntu, Oxygen, "Oxygen Sans", Cantarell, "Droid Sans", "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Emoji", "Segoe UI Symbol", "Lucida Grande", Helvetica, Arial, sans-serif;
        -apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue","Fira Sans",Ubuntu,Oxygen,"Oxygen Sans",Cantarell,"Droid Sans","Apple Color Emoji","Segoe UI Emoji","Segoe UI Emoji","Segoe UI Symbol","Lucida Grande",Helvetica,Arial,sans-serifbody font-size: 16px;
        font-weight: 400;
        outline-color: rgba(0, 0, 0, 0.9);
        outline-style: none;
        outline-width: 0px;
        padding-bottom: 6px;
        padding-left: 6px;
        padding-right: 6px;
        padding-top: 6px;
        transition-delay: 0s;
        transition-duration: 0.083s;
        transition-property: box-shadow;
        transition-timing-function: ease;
        vertical-align: baseline;
        -webkit-font-smoothing: antialiased;
    }

    .notAvailableOptionsMessage {
        position: relative;
        margin: 20rem 60rem;
        background-color: var(--ZBlue);
        color: white;
        padding: 2rem 0 1rem 0;
        width: 30%;
        text-align: center;
        border-radius: 3px;
        box-shadow: 0 2px 5px 0 rgb(0 0 0 / 26%);
    }

    @media (max-width: 1350px) {
 .notAvailableOptionsMessage {
        position: relative;
        margin: 16rem 35rem;
        background-color: var(--ZBlue);
        color: white;
        padding: 2rem 0 1rem 0;
        width: 40%;
        text-align: center;
        border-radius: 3px;
        box-shadow: 0 2px 5px 0 rgb(0 0 0 / 26%);
    }
}

    .DynamicBtnLI {
        margin: 7px;
    }

        .DynamicBtnLI a {
            max-height: 155px;
            min-height: 24px;
            background-color: var(--ZBlue);
            border-color: var(--ZBlue);
            overflow: hidden;
            white-space: unset;
            text-overflow: ellipsis;
            font-family: Verdana,'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: rgb(255,255,255);
            font-size: 13px;
            padding: 6px 4px 6px 4px;
        }

    .DynamicBtnAction a {
        max-height: 155px;
        min-height: 24px;
        /*background-color: #559cda;*/
        background-color: #398439;
        border-color: #398439;
        overflow: hidden;
        white-space: unset;
        text-overflow: ellipsis;
        font-family: Verdana,'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        color: rgb(255,255,255);
        font-size: 13px;
        padding: 6px 4px 6px 4px;
    }

    .DynamicBtnLIRpt a {
        max-height: 155px;
        min-height: 24px;
        /*background-color: #559cda;*/
        /*background-color: rgb(0,115,177);
            border-color: rgb(0,115,177);*/
        overflow: hidden;
        white-space: unset;
        text-overflow: ellipsis;
        font-family: Verdana, 'Segoe UI', Tahoma, Geneva, sans-serif;
        color: rgb(0,115,177);
        font-size: 13px;
        padding: 6px 4px 6px 4px;
    }


    .DynamicBtnLI a:hover {
        box-shadow: 3px;
    }

    .DynamicBtnLI a:focus {
        box-shadow: 3px;
    }
</style>

<script>
    $(document).ready(function () {

        $("#dropdown-header").hover(

            function () {
                $(this).children("ul").css("display", "flex");
            },

            function () {
                $(this).children("ul").css("display", "none");
            });


        //$(".DynamicBtnGroup").hover(
        //    function () {
        //        $(this).addClass("open");
        //    },

        //    function () {
        //        $(this).removeClass("open");
        //    }
        //);
    });
</script>
