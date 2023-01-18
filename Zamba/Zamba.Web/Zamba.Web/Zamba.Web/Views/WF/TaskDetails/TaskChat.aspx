<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskChat.aspx.cs" Inherits="Zamba.Web.Views.WF.TaskDetails.TaskChat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
       
    <script src="../../../Scripts/jquery-2.2.2.min.js"></script>
    <script src="../../../ChatJs/Scripts/jquery.signalR-2.2.0.min.js"></script>      
    <script src="../../../ChatJs/Scripts/groups.js"></script>

    <script src="../../../ChatJs/Scripts/zambachat.js"></script>
    <script src="../../../ChatJs/Scripts/zambachat.fn.js"></script>
    <script src="../../../ChatJs/Scripts/zambachat.init.js"></script>
    <script src="../../../ChatJs/Scripts/localStorage.js"></script>
    <script src="../../../ChatJs/Scripts/debbug.js"></script>
 
</head>
<body>
    
     <div id="ChatTitle" style="display:none" title="Titulo">
         <label>Titulo</label>
         <input />
     </div>
    <form id="form1" runat="server">
    <div>
<%--        <span  class="btn btn-info btn-sm" onclick="" data-toggle="modal" data-target="#myModal">Crear Topic</span>--%>
     <%--   <input hidden="hidden" id="TaskId"/>--%>
    </div>
    </form>

    <input hidden="hidden" id="CurrentUser" runat="server"/>
   
    <div id="myModal" class="modal fade" role="dialog" style="position:relative;z-index:1000000">
  <div class="modal-dialog">
    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Crear Tema</h4>
      </div>
      <div class="modal-body">
          <label>Nombre del Tema</label>
          <input  class="form-control" id="TopicName"/>
      </div>
      <div class="modal-footer" style=" padding: 7px">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
       <button type="button" class="btn btn-default" onclick="InitGroup()" data-dismiss="modal">Crear</button>

      </div>
    </div>

  </div>
</div>
</body>
</html>

  <script type="text/javascript">
        //var URLServer ='<%=ConfigurationManager.AppSettings["ChatURLServer"].ToString() %>';
        //var thisDomain = "http://localhost/zamba.web";   
        //var ZCollLnk ='<%=ConfigurationManager.AppSettings["ZCollLnk"].ToString() %>';   
       // var zCollServer = '<%=ConfigurationManager.AppSettings["zCollServer"].ToString() %>';
        var taskChat = true;
        var DocId;
        var userid = $("[id$=CurrentUser]").val();
        $(document).ready(function ()
        {
            LoadChat(userid, URLServer, "compact", "ZambaWeb", thisDomain, ZCollLnk, false, zCollServer, taskChat);
            DocId = "";
            DocId = parseFloat($("[id$=hdnDocId]", window.parent.document).val());
        });
        function InitGroup()
        {
            var TopicName = $("#TopicName").val()
              $.ajax({
                  type: "POST",
                  //async: false, 
                  url: URLBase + '/CreateEmptyChatForum',
                  data: { userId: userid, groupName: TopicName, docId: DocId },
                  success: function (d) {
                      CreateGroupListForum(parseInt(d), TopicName, DocId);
                  },
              });
       }
 </script>