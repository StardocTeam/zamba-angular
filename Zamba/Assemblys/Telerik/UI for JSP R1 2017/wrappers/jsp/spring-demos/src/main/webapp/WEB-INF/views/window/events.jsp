<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<c:url value="/window/content1" var="remoteUrl" />

<demo:header />

    <div class="box hidden-on-narrow" style="margin-bottom: 30px;">                
        <h4>Console log</h4>
        <div class="console"></div>
    </div>
    
	<kendo:window name="window" title="Rams's Ten Principles of Good Design" draggable="true" 
		width="630"
		height="315"  
		content="${remoteUrl}"
		actions="<%=new String[] { \"Refresh\", \"Close\" } %>"
		close="onClose"
		open="onOpen"
		refresh="onRefresh" 	
		activate="onActivate" 
		deactivate="onDeactivate" 
		resize="onResize" 
		dragstart="onDragStart" 
		dragend="onDragEnd">			
	</kendo:window>

	<span id="undo" style="display:none" class="k-button hidden-on-narrow">Click here to open the window.</span>
	
	<div class="responsive-message"></div>

	<script>
	
	    $(document).ready(function() {
	        $("#undo").bind("click", function() {
                $("#window").data("kendoWindow").open();
                $("#undo").hide();
            });
	    });
	
	    function onOpen(e) {
	        kendoConsole.log("event :: open");
	    }
	
	    function onClose(e) {
	    	$("#undo").show();
	        kendoConsole.log("event :: close");
	    }
	
	    function onActivate(e) {
	        kendoConsole.log("event :: activate");
	    }
	
	    function onDeactivate(e) {
	        kendoConsole.log("event :: deactivate");
	    }
	
	    function onRefresh(e) {
	        kendoConsole.log("event :: refresh");
	    }
	
	    function onResize(e) {
	        kendoConsole.log("event :: resize");
	    }
	
	    function onDragStart(e) {
	        kendoConsole.log("event :: dragstart");
	    }
	
	    function onDragEnd(e) {
	        kendoConsole.log("event :: dragend");
	    }	
	    
	</script>

	<style>

        #example
        {
            min-height:600px;
        }

        #undo {
            text-align: center;
            position: absolute;
            white-space: nowrap;
            padding: 1em;
            cursor: pointer;
        }
        
        @media screen and (max-width: 1023px) {
            div.k-window {
                display: none !important;
            }
        }
    </style>
<demo:footer />
