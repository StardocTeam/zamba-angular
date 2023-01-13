<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Chat.ascx.cs" Inherits="Zamba.Web.Views.UC.HomeWidget.Chat" %>
    <script src="../../ChatJs/Scripts/zambachat.js" type="text/javascript"></script>

 <script>
     $(document).ready(function () {        
         //Solo se habilita chat desde aca en chromium de windows, en web se hace desde masterpage
         if (enableChat  && typeof (winFormJSCall) != "undefined" )
             LoadChat(userIdGS, chatWidgetURL, 'compact', "HomeWidget", thisDomain, zColl,false);
        });
    </script>