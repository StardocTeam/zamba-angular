<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<c:url value="/treeview/remote-data-binding/read" var="transportReadUrl" />

<demo:header />
<div class="demo-section k-content">
 <kendo:treeView name="treeview" dataTextField="fullName">
     <kendo:dataSource>
         <kendo:dataSource-transport>
             <kendo:dataSource-transport-read url="${transportReadUrl}" type="POST"  contentType="application/json"/>    
             <kendo:dataSource-transport-parameterMap>
             	<script>
              	function parameterMap(options,type) {
              		return JSON.stringify(options);
              	}
             	</script>
             </kendo:dataSource-transport-parameterMap>         
         </kendo:dataSource-transport>
         <kendo:dataSource-schema>
             <kendo:dataSource-schema-hierarchical-model id="employeeId" hasChildren="hasEmployees" />
         </kendo:dataSource-schema>
     </kendo:dataSource>
 </kendo:treeView>
</div>

<demo:footer />
