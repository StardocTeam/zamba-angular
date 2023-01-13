<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DynamicButtonPartialView.ascx.cs"
    Inherits="DynamicButtonPartialView" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="Zamba.Core" %>
<%@ Import Namespace="System.Collections.Generic" %>



<%foreach (var group in this.RenderButtons.Select(b => new { b.GroupName, b.GroupClass }).Distinct())
  {%>

  <div style="text-align:center" class="DynamicBtnGroup <%=group.GroupClass %>">
      <span id="Grn" style="display:block; text-align:center "><%=(group.GroupName.Equals("") ? " " : group.GroupName) %></span> 

   <ul  class="list-group DynamicBtnListGroup">
       <%foreach (IDynamicButton item in this.RenderButtons.Where(b => b.GroupName == group.GroupName))
      {%>
       <li   class="DynamicBtnLI btn btn-primary btn-xs <%=item.ViewClass %>">
    <a href="#" id="<%=item.ButtonId %>" onclick="<%=(item.TypeId == Zamba.Core.ButtonType.Rule)?"RuleButtonClick(" + item.RuleId + ");" : string.Empty %>">
        <%=item.Caption%>
    </a>
</li>
   <% } %>
       </ul> 

    </div>
   
<% } %>