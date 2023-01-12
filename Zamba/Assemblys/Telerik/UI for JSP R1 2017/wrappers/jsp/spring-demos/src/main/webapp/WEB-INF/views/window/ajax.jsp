<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<c:url value="/window/content" var="remoteUrl" />

<demo:header />
	<kendo:window name="window" title="Rams's Ten Principles of Good Design" draggable="true" resizable="true" 
		width="615" close="onClose" content="${remoteUrl }">				
	</kendo:window>    
	
	<span id="undo" style="display:none" class="k-button">Click here to open the window.</span>
	
	<div class="responsive-message"></div>

	<script>
	    function onClose() {
	        $("#undo").show();
	    }
	
	    $(document).ready(function() {
	        $("#undo").bind("click", function() {
	                $("#window").data("kendoWindow").open();
	                $("#undo").hide();
	            });
	    });
	</script>	
	
	<style>
	    #example {
	        min-height: 840px;
	    }
	
	    #undo {
	        text-align: center;
	        position: absolute;
	        white-space: nowrap;
	        border-width: 1px;
	        border-style: solid;
	        padding: 2em;
	        cursor: pointer;
	    }
	    
	    @media screen and (max-width: 1023px) {
	        div.k-window {
	            display: none !important;
	        }
	    }
	</style>
<demo:footer />
