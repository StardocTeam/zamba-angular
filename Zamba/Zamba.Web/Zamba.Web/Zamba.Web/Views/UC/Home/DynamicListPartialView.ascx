<%@ Control Language="C#" AutoEventWireup="true" 
    Inherits="Views_UC_Home_DynamicListPartialView" Codebehind="DynamicListPartialView.ascx.cs" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="Zamba.Core" %>
<%@ Import Namespace="System.Collections.Generic" %>

<%foreach (var group in this.RenderButtons.Select(b => new { b.GroupName, b.GroupClass }).Distinct())
  {%>
    <div class="<%=group.GroupClass %>"><%-- style="margin-top: -20px;"--%>

 <% if (group.GroupName != null && group.GroupName !="") {%>
    <li role="presentation" class="dropdown-header"><%=(group.GroupName) %></li>
<% } %>

    <%foreach (IDynamicButton item in this.RenderButtons.Where(b => b.GroupName == group.GroupName))
        {%>
    <li class="<%=item.ViewClass %>">
        <span class="glyphicon glyphicon-chevron-right"></span>
        <a href="#" id="<%=item.ButtonId %>" onclick="<%=(item.TypeId == Zamba.Core.ButtonType.Rule)?"RuleButtonClick(" + item.RuleId + ");" : string.Empty %>">
            <%=item.Caption%>
        </a>
    </li>
    <% } %>
    </div>
<li class="divider"></li>
<% } %>