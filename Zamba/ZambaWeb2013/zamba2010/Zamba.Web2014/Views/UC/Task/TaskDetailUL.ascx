<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TaskDetailUL.ascx.cs" Inherits="Views_UC_Task_TaskDetailUL" %>

<asp:HiddenField runat="server" ID="hidLastTab" EnableViewState="true" />
<asp:HiddenField runat="server" ID="HiddenTaskId" EnableViewState="true" />
<script>
    $(function() {
        $("#tabTaskDetailUl").tabs({ 

                ajaxOptions: {
                    error: function(xhr, status, index, anchor) {
                    }
                }
            });
        });
</script>

<div id="tabTaskDetailUl" style="height:550px">
    <ul id="ulPrincipal">
    <li><a href="#tabTaskUL">Tareas</a></li>
    </ul>
    <div ID="tabTaskUL">
        
        <iframe  width="100%" frameborder="0"  runat="server" id="Iframe1" style="height:550px">
        </iframe>
    </div>
</div>