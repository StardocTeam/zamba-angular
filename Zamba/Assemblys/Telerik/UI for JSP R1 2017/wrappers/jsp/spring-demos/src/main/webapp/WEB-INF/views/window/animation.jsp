<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<c:url value="/resources/web/window/egg-chair.png" var="eggchair" />

<demo:header />

	<form class="box hidden-on-narrow" method="Post">
	    <h4>Animation Settings</h4>
	    <ul class="options">
	        <li>
	            <input id="zoom" name="animation" type="radio" ${ animation == "zoom" ? "checked=\"checked\"" : "" } value="zoom" /> <label for="zoom">default/zoom animation</label>
	        </li>
	        <li>
	            <input id="toggle" name="animation" type="radio" ${ animation == "toggle" ? "checked=\"checked\"" : "" } value="toggle" /> <label for="toggle">toggle animation</label>
	        </li>
	        <li>
	            <input id="expand" name="animation" type="radio" ${ animation == "expand" ? "checked=\"checked\"" : "" } value="expand" /> <label for="expand">expand animation</label>
	        </li>  
	        <li> 
	        	<input id="opacity" name="opacity" type="checkbox" ${ opacity ? "checked=\"checked\"" : "" } value="true" /> <label for="opacity">animate opacity</label>
	        	<input name="opacity" type="hidden" value="false">           
	        </li>
	    </ul>
	
	    <button class="k-button">Apply</button>
	</form>

	
	<kendo:window name="window" title="EGG CHAIR" draggable="true" resizable="true" width="500" close="onClose">
	<kendo:window-animation>
		<kendo:window-animation-open effects="${animationConfigOpen}"/>
		<kendo:window-animation-close effects="${animationConfigClose}"/>
	</kendo:window-animation>
		<kendo:window-content>
			<div style="text-align: center;">
                   <img src="${eggchair}" alt="ARNE JACOBSEN EGG CHAIR"/>
                   <p>ARNE JACOBSEN EGG CHAIR<br /> Image by: <a href="http://www.conranshop.co.uk/" title="http://www.conranshop.co.uk/">http://www.conranshop.co.uk/</a></p>
               </div>		            
		</kendo:window-content>	
	</kendo:window>    	
	<span id="undo" style="display:none" class="k-button hidden-on-narrow">Click here to open the window.</span>
	
	<div class="responsive-message"></div>

	<script>
	    function onClose() {
	        $("#undo").show();
	    }
	
	    $(document).ready(function() {
	        $("#undo").bind("click", function() {
	                 $("#window").data("kendoWindow").open();
                     $("#undo").fadeOut(300);
	            });
	    });
	</script>	
	
	<style>

        #example
        {
            min-height:400px;
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
