<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

<div class="k-rtl">
    <kendo:notification name="popupNotification" button="true">
        <kendo:notification-position top="30" left="30" />
    </kendo:notification>
    <kendo:notification name="staticNotification" appendTo="#appendto" button="true"></kendo:notification>

<div class="demo-section k-content">
               <h4>Show notification</h4>
               <p>
                   <button id="showPopupNotification" class="k-button">As a popup at top-left</button><br />
                   <button id="showStaticNotification" class="k-button">Static in the panel below</button>
               </p>
               <div style="padding-top: 1em;">
            <h4>Hide notification</h4>
            <button id="hideAllNotifications" class="k-button">Hide All Notifications</button>
        </div>
    </div>
    
    <div id="appendto" class="demo-section k-content"></div>
 </div>
 
 <style>
     .demo-section p {
         margin: 3px 0 20px;
         line-height: 40px;
     }
     .demo-section .k-button {
         width: 250px;
     }
 </style>
<script>
    $(document).ready(function() {
        var popupNotification = $("#popupNotification").data("kendoNotification");
        var staticNotification = $("#staticNotification").data("kendoNotification");

        $("#showPopupNotification").click(function(){
            var d = new Date();
            popupNotification.show(kendo.toString(d, 'HH:MM:ss.') + kendo.toString(d.getMilliseconds(), "000"), "error");
        });

        $("#showStaticNotification").click(function(){
            var d = new Date();
            staticNotification.show(kendo.toString(d, 'HH:MM:ss.') + kendo.toString(d.getMilliseconds(), "000"), "info");
            var container = $(staticNotification.options.appendTo);
            container.scrollTop(container[0].scrollHeight);
        });

        $("#hideAllNotifications").click(function(){
            popupNotification.hide();
            staticNotification.hide();
        });

    });
</script>

<demo:footer />