<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />
    
	<div class="demo-section k-content" style="text-align: center;">
        <h4>Pick a date</h4>
		<kendo:calendar name="calendar" style="width:243px" change="onChange" navigate="onNavigate">
    	</kendo:calendar>
	</div>
	
	<div class="box" style="text-align: center;">
        <h4>Events log</h4>
        <div class="console"></div>
    </div>
    
    <script>
        function onChange() {
            if ("kendoConsole" in window) {
                kendoConsole.log("Change :: "+kendo.toString(this.value(), 'd'));
            }
        }
    
        function onNavigate() {
            if ("kendoConsole" in window) {
                kendoConsole.log("Navigate");
            }
        }
    </script>
    
<demo:footer />