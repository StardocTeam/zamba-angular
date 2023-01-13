<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<c:url value="/window/content1" var="remoteUrl" />

<demo:header />
    <div class="box wide hidden-on-narrow" style="z-index:10000">
        <div class="box-col">
        <h4>API Functions</h4>
        <ul class="options">
            <li>
                 <button id="open" class="k-button">Open</button>
                <button id="close" class="k-button">Close</button>
            </li>
        </ul>
        </div>
        <div class="box-col">
        <h4>&nbsp;</h4>
        <ul class="options">
            <li>
                <button id="refresh" class="k-button">Refresh</button>
                <button id="center" class="k-button">Center</button>
            </li>
        </ul>
        </div>
        <div class="box-col">
        <h4>&nbsp;</h4>
        <ul class="options">
            <li>
                <button id="pin" class="k-button">Pin</button>
                <button id="unpin" class="k-button">Unpin</button>
            </li>
        </ul>
        </div>
    </div>
            	
	<kendo:window name="window" title="Rams's Ten Principles of Good Design" draggable="true" resizable="true" 
		width="630" height="315" content="${remoteUrl}" actions="<%=new String[] { \"Pin\", \"Refresh\", \"Maximize\", \"Close\" } %>">			
	</kendo:window>
	
	<div class="responsive-message"></div>
	
	<script>
	
	    $(document).ready(function() {	
			var myWindow = $("#window");

            $("#open").click( function (e) {
                myWindow.data("kendoWindow").open();
            });

            $("#close").click( function (e) {
                myWindow.data("kendoWindow").close();
            });

            $("#refresh").click( function (e) {
                myWindow.data("kendoWindow").refresh();
            });
            
            $("#center").click(function (e) {
                myWindow.data("kendoWindow").center();
            });

            $("#pin").click(function (e) {
                myWindow.data("kendoWindow").pin();
            });

            $("#unpin").click(function (e) {
                myWindow.data("kendoWindow").unpin();
            });
	    });
	    
	</script>

	
	
	<style>

        #example {
             min-height: 600px;
        }

        @media screen and (max-width: 1023px) {
            div.k-window {
                display: none !important;
            }
        }

    </style>
<demo:footer />
