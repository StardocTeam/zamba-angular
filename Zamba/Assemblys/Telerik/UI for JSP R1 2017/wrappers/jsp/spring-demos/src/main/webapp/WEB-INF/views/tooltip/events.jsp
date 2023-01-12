<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />   
   <kendo:tooltip name="#autohide-true" position="top"  content="Hello!" show="onShow" hide="onHide" />
   <kendo:tooltip name="#autohide-false" position="top" content="Hello!" show="onShow" hide="onHide" autoHide="false" />
   
	<div class="demo-section k-content">
	    <span id="autohide-true" class="k-button wider">Hover me!</span>
	    <span id="autohide-false" class="k-button wider">Hover me too!</span>
	</div>
    
	<div class="box">
       <h4>Console log</h4>
       <div class="console"></div>
   </div>
            
    <script>
	    function onShow(e) {
	        kendoConsole.log("event :: show");
	    }
	
	    function onHide(e) {
	        kendoConsole.log("event :: hide");
	    }
    </script>
    
 <style>
    .demo-section {
        text-align: center;
    }
    .wider {
        display: block;
        margin: 20px 0;
        padding: 15px 8px;
        line-height: 23px;
        width: 100%;
    }
</style>                 
    
<demo:footer />