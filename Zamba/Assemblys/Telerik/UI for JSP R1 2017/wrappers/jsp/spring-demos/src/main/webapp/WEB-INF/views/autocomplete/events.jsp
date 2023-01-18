<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<c:url value="/resources/web/autocomplete/taxi.png" var="taxiImg"/>

<demo:header />

	<div class="demo-section k-content">
    	<h4>Select a state in USA:</h4>
		<kendo:autoComplete name="states" open="onOpen" close="onClose" 
							change="onChange" select="onSelect" 
							dataBound="onDataBound" filtering="onFiltering" style="width:100%;">
			<kendo:dataSource data="${states}">
			</kendo:dataSource>
		</kendo:autoComplete>
	</div>

	<div class="box">                
	    <h4>Console log</h4>
	    <div class="console"></div>
	</div>

    <script>
        function onOpen() {
            if ("kendoConsole" in window) {
                kendoConsole.log("event :: open");
            }
        }

        function onClose() {
            if ("kendoConsole" in window) {
                kendoConsole.log("event :: close");
            }
        }

        function onChange() {
            if ("kendoConsole" in window) {
                kendoConsole.log("event :: change");
            }
        }
        
        function onFiltering(e) {
            if ("kendoConsole" in window) {
                kendoConsole.log("event :: filtering");
            }
        }

        function onSelect(e) {
            if ("kendoConsole" in window) {
                var dataItem = this.dataItem(e.item.index());
                kendoConsole.log("event :: select (" + dataItem + ")" );
            }
        }

        function onDataBound(e) {
            if ("kendoConsole" in window) {
               kendoConsole.log("event :: dataBound");
            }
        }
    </script>
<demo:footer />
