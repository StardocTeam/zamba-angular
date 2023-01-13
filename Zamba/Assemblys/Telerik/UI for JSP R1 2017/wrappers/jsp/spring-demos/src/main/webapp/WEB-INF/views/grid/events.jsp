<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<c:url value="/grid/events/read" var="transportReadUrl" />

<demo:header />

	<script>
		function onChange(arg) {
	        var selected = $.map(this.select(), function(item) {
	            return $(item).text();
	        });
	
	        kendoConsole.log("Selected: " + selected.length + " item(s), [" + selected.join(", ") + "]");
	    }
	
	    function onDataBound(arg) {
	        kendoConsole.log("Grid data bound");
	    }
	    
	    function onDataBinding(arg) {
	        kendoConsole.log("Grid data binding");
	    }
	    
	    function onSorting(arg) {
	        kendoConsole.log("Sorting on field: " + arg.sort.field + ", direction:" + (arg.sort.dir || "none"));
	    }
	    
	    function onFiltering(arg) {
	        kendoConsole.log("Filter on " + kendo.stringify(arg.filter));
	    }
	    
	    function onPaging(arg) {
	        kendoConsole.log("Paging to page index:" + arg.page);
	    }
	    
	    function onGrouping(arg) {
	        kendoConsole.log("Group on " + kendo.stringify(arg.groups));
	    }
	</script>
        <kendo:grid name="grid" height="320px" pageable="true" sortable="true" selectable="true" filterable="true" groupable="true"
	    	change="onChange" dataBound="onDataBound" dataBinding="onDataBinding"
	    	sort="onSorting" filter="onFiltering" page="onPaging" group="onGrouping">
	        <kendo:grid-columns>
	            <kendo:grid-column title="Product Name" field="productName" />
	            <kendo:grid-column title="Unit Price" field="unitPrice" format="{0:c}" />
	            <kendo:grid-column title="Units In Stock" field="unitsInStock" />
	        </kendo:grid-columns>
	        <kendo:dataSource pageSize="10">             
	            <kendo:dataSource-transport>
	                <kendo:dataSource-transport-read url="${transportReadUrl}"/>
	            </kendo:dataSource-transport>
	        </kendo:dataSource>       
	    </kendo:grid>       
      
  <div class="box wide">
      <h4>Console log</h4>
      <div class="console"></div>
  </div>
<demo:footer />