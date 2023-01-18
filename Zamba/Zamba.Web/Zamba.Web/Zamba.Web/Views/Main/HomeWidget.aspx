<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomeWidget.aspx.cs" Inherits="Zamba.Web.Views.Main.HomeWidget" %>

<%@ Register Src="~/Views/UC/HomeWidget/Chat.ascx" TagName="UCHWChat" TagPrefix="zhwc" %>
<%@ Register Src="~/Views/UC/HomeWidget/Recents.ascx" TagName="UCHWRecents" TagPrefix="zhwr" %>
<%@ Register Src="~/Views/UC/HomeWidget/News.ascx" TagName="UCHWNews" TagPrefix="zhwn" %>
<%@ Register Src="~/Views/UC/HomeWidget/Bookmarks.ascx" TagName="UCHWBookmarks" TagPrefix="zhwb" %>
<%@ Register Src="~/Views/UC/HomeWidget/Important.ascx" TagName="UCHWImportant" TagPrefix="zhwi" %>
<%@ Register Src="~/Views/UC/HomeWidget/NewsZamba.ascx" TagName="UCHWNewsZamba" TagPrefix="zhwz" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head> 
    <meta charset="UTF-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
  
    <%--Colocar solo referencias a scripts y estilos comunes a todos los widgets--%>
    <link rel="stylesheet" type="text/css" href="../../Content/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="../../Content/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="../../Content/themes/base/jquery-ui.css" />
    <link rel="stylesheet" type="text/css" href="../../Content/bootstrap-theme.css" />
    <link rel="stylesheet" type="text/css" href="../../Content/HomeWidget.css" />
    <link href="https://fonts.googleapis.com/css?family=Open+Sans" rel="stylesheet" />
    <script src="../../Scripts/jquery-2.2.2.min.js"></script>
    <script src="../../Scripts/jquery-ui-1.11.4.min.js"></script>
    <script src="../../Scripts/angular.min.js"></script>
    <script src="../../Scripts/angular-sanitize.min.js"></script>
    <script src="../../Scripts/angular-animate.min.js"></script>
    <script src="../../Scripts/ng-embed/ng-embed.min.js"></script>
    <script src="../../Scripts/bootstrap.js"></script>
    <script src="../../Scripts/ui-bootstrap-tpls-0.12.0.min.js"></script>
    <script src="../../Scripts/bootstrap-waitingfor.js"></script>
    <script src="../../Scripts/modernizr-custom.js"></script>
    <script src="../../Scripts/bootbox.js"></script>
    <script src="../../Scripts/toastr.js"></script>
        <script src="../../Scripts/sweetalert.min.js"></script>   

    <script src="../../Scripts/Zamba.Fn.js"></script>

    <title>Zamba Home</title>
  
      <script>
          
          if (typeof (winFormJSCall) == "undefined") {
              console.log("No se llamo a winFormJSCall desbbws, verificar");
              //thisDomain = "http://localhost/zamba.web/";
              //homeWidgetDomain = "http://localhost/zamba.web/";
              //ZambaWebRestApiURL = "http://localhost/zambaweb.restapi/api";
              enableGlobalSearch = true;
              enableChat = true;
              //chatWidgetURL = "http://localhost/zambachat/";
              //userIdGS = 0;
              // $.ajax({
              //      type: "POST",
              //      url: "../../Services/TaskService.asmx/GetUserIdWeb",
              //      data: "",
              //      contentType: "application/json; charset=utf-8",
              //      success: function (data) {
              //          userIdGS = data.d;
              //          LoadWidget(userIdGS);
              //          $(".globalSearchonwindows").remove();
              //      },
              //});
              //si es web busca trae el user id desde un servicio
              zColl = "http://localhost/zamba.collaboration/";
          }
          else {
              var conf = winFormJSCall.confVal();
              conf.then(function (confVal) {
                  confVal = JSON.parse(confVal);
                  thisDomain = confVal.zambaWebURL;
                  homeWidgetDomain = confVal.homeWidgetDomain;
                  ZambaWebRestApiURL = confVal.zambaWebRestApiURL;
                  enableGlobalSearch = confVal.enableGlobalSearch;
                  enableChat = confVal.enableChat;
                  chatWidgetURL = confVal.chatWidgetURL;
                  userIdGS = confVal.userIdGS;
                  LoadWidget(userIdGS);
                  zColl = confVal.ZColl;
             
              });
          }

          function GetUID2()
          {
              return userIdGS;
          }
          function LoadWidget(user) {
              userIdGS = user;
              var ZambaWebAdminRestApiURL = "http://localhost/zamba.WebAdmin";
              //news
              $.ajax({
                  type: "GET",
                  dataType: "json",
                  url: ZambaWebAdminRestApiURL + "/News",
                  success: function (data) {
                      var AllData = data;
                      if(AllData.length > 0){
                      for (var i = 0; i < AllData.length; i++) {
                          $('<div class="item">' + AllData[i].shortContent + '</div>').appendTo('.carousel-inner');
                          $('<li data-target="#CarouselNews" data-slide-to="' + i + '"></li>').appendTo('.carousel-indicators')
                          }
                    
                      $('.item').first().addClass('active');
                      $('.carousel-indicators > li').first().addClass('active');
                      $('#CarouselNews').addClass('carousel slide');
                      $('#CarouselNews').carousel();
                      }
                  }
              });

              //importantes
              $.ajax({
                  type: "GET",
                  dataType: "json",
                  url: ZambaWebAdminRestApiURL + "/news/GetImportants",
                  data: { Userid: user },
                  success: function (data) {
                      for (var i = 0; i < data.length; i++) {
                          var url = "../WF/TaskSelector.ashx?" + 'doctype=' + data[i].typeId + '&docid=' + data[i].id + '&taskid=' + 0 + '&wfstepid=' + 0 + "&userId=" + GetUID();
                          $("#listImportants").append("<li id='itemlist'><p onclick='OpenTask(this)' id='" + url + "' href='#'>" + data[i].name + "</p></li");
                          //fadeItem();
                      }
                  }
              });

              //favoritos
              $.ajax({
                  type: "GET",
                  dataType: "json",
                  url: ZambaWebAdminRestApiURL + "/news/GetFavorites",
                  data: { Userid: user },
                  success: function (data) {
                      for (var i = 0; i < data.length; i++) {
                          var url = "../WF/TaskSelector.ashx?" + 'doctype=' + data[i].typeId + '&docid=' + data[i].id + '&taskid=' + 0 + '&wfstepid=' + 0 + "&userId=" + GetUID();
                          $("#listFavorites").append("<li id='ListItem'><p onclick='OpenTask(this)' id='" + url + "' href='#'>" + data[i].name + "</p></li");
                      }
                  }
              });

              //recientes 
              $.ajax({
                  type: "GET",
                  dataType: "json",
                  url: ZambaWebAdminRestApiURL  + "/news/GetRecents",
                  data: { Userid: user },
                  success: function (data) {
                      for (var i = 0; i < data.length; i++) {
                          var url = "../WF/TaskSelector.ashx?" + 'doctype=' + data[i].typeId + '&docid=' + data[i].id + '&taskid=' + 0 + '&wfstepid=' + 0 + "&userId=" + GetUID();
                          $("#listRecents").append("<li id='itemlist'><p onclick='OpenTask(this)' id='" + url + "' href='#'>" + data[i].title + "</p></li");
                          //fadeItem();
                      }
                  }
              });

              //novedades
              $.ajax({
                  type: "GET",
                  dataType: "json",
                  url: ZambaWebAdminRestApiURL  + "/news/GetNovedades",
                  data: { Userid: user },
                  success: function (data) {
                      for (var i = 0; i < data.length; i++) {
                          var url = "../WF/TaskSelector.ashx?" + 'doctype=' + data[i].typeId + '&docid=' + data[i].id + '&taskid=' + 0 + '&wfstepid=' + 0 + "&userId=" + GetUID();
                          $("#listNovedades").append("<li id='itemlist'><p onclick='OpenTask(this)' id='" + url + "' href='#'>" + data[i].name + "</p></li");
                          //fadeItem();
                      }
                  }
              });

          }
          
          function fadeItem() {
              $('#list1 li:hidden:first').delay(520).fadeIn(fadeItem);
          }

          function AddGSearchWindow()
          {
              $(".body").append(".navbar");
          }

          function OpenTask(obj)
          {
              var url = $(obj).attr("id");
              var name = $(obj).text();
              var docId =  parseInt(getParameterByName("docid", url));
              var docTypeId = parseInt(getParameterByName("doctype", url));
              var taskId = 0;
              var stepId = 0;

              if ($('#liTasks', parent.document).css('display') === 'none')
              {
                  $('#liTasks', parent.document).css('display', 'block');
              }
              //si es web
              if (typeof (winFormJSCall) == "undefined")
              {
                  OpenDocTask3(0, docId, docTypeId, false, name, url, GetUID2());
              }
              //si es windows
              else
              {
                  winFormJSCall.openTask(docTypeId, docId, taskId, stepId);
              }
          }
</script>
      <!-- Polyfill(s) for older browsers -->
<%--<script src="../../node_modules/core-js/client/shim.min.js"></script>
    <script src="../../node_modules/zone.js/dist/zone.js"></script>
    <script src="../../node_modules/systemjs/dist/system.src.js"></script>--%>

<%--<script src="../../systemjs.config.js"></script>
 <script>
        System.import('../../app/main.js').catch(function (err) { console.error(err); });
    </script>--%>
</head>

<body>
  
 <form runat="server">
      <asp:HiddenField  runat="server" ID="USERID"/>
  </form>
    <%--Inicializacion de variables necesarias para los widgets--%>
    <script type="text/javascript">
        function GetUID()
        {
            return userIdGS;
        }
        initaltab = "";
        zambaApplication = "ZambaHomeWidget";
        var URLServer = chatWidgetURL;
        var thisDomain = homeWidgetDomain;

        //loadWigetsData(GetUID());
        // var _enableChat = enableChat;
    </script>

    <nav class="navbar">
       <%-- <div class="col-xs-3 col-sm-offset-4 globalSearchonwindows">
            <zhwgs:UCHWGlobalSearch runat="server" ID="GlobalSearch" />
         </div>--%>
    </nav>
      <%--  <div class="row HWRowTitle">
            <img src="../../Content/Images/pwbyzamba.png" class="HWTitleImg" />
            <div class="HWTitle"></div>
            <button style="float: right;" class="btn btn-default btn-xs" onclick="location.reload();">
                <span class="glyphicon glyphicon-refresh"></span>
            </button>
        </div>--%>
     
         <div class="container" style="position:relative;bottom:66px">
            <div class="col-xs-12 MainDivHome" style="height:500px;">
                 <div class="col-sm-4 scrollstyle"  style="height:300px;background-color:white;color:#676767;right:15px;padding:15px;overflow:auto;border-right:1px solid #e9e9e9">
                    <div class="HWWidgetImportant scrollstyle">
                        <zhwi:UCHWImportant runat="server" ID="Important" />
                    </div>
                  </div>
              
            <div class="col-sm-4 scrollstyle"  style="height:300px;color:#676767;padding:15px;overflow:auto;border-right:1px solid #e9e9e9">
                    <div class="HWWidgetBookmarks " style="overflow:auto">
                        <zhwb:UCHWBookmarks runat="server" ID="Bookmarks" />
                    </div>
             </div>

             <div class="col-sm-4 HWWidgetNews " style="height:100px">
                <div class="HWWidgetNews">
                       <zhwn:UCHWNews runat="server" ID="News" />
                 </div> 
             </div>

            </div>
              <div class="col-sm-8 scrollstyle" style="position:relative;bottom:200px;border-top:1px solid #e9e9e9;background-color:white;
                     padding:70px;border-left:21px solid #b561c9;width:754px;left:1px;height:200px;overflow:auto;border-bottom:1px solid #e9e9e9">
                     <div class="novedades col-xs-3">
                       <h3 style="color:#b561c9" >Novedades</h3>
                   </div>
                    <div class="col-xs-3 col-sm-offset-3 ">
                         <div class="HWWidgetNewsZamba">
                            <zhwz:UCHWNewsZamba runat="server" ID="NewsZamba" />
                           </div> 
                    </div>
              </div>

                <div class="col-sm-4 scrollstyle" style="position:relative;bottom:200px;border:1px solid #e9e9e9;background-color:white;
                     padding:5px;width:384px;left:1px;height:200px;overflow:auto">
                     <div class="HWWidgetRecents" style="overflow:auto;">
                        <zhwr:UCHWRecents runat="server" ID="UCHWRecents1" />
                           
                    </div>
              </div>
              
         </div>

        <zhwc:UCHWChat runat="server" ID="Chat" />
</body>
</html>



<style>
.scrollstyle::-webkit-scrollbar 
{
    width: 2px;
    
}

 .scrollstyle::-webkit-scrollbar-thumb
 {
    -webkit-border-radius: 10px;
     border-radius: 10px;
     background: rgba(206, 206, 206, 0.91);
    -webkit-box-shadow: inset 0 0 6px rgb(220, 220, 220);
}
</style>

<%--Tener en cuenta configurar en Zopt
    ChatWidgetURL
    HomeWidgetURL
    HomeWidgetDomain
    EnableChatWidget
    EnableGlobalSearchWidget
    ZambaWebRestApiURL
--%>