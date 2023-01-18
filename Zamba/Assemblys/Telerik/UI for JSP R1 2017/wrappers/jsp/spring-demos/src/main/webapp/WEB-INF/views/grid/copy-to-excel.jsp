<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<c:url value="/grid/copy-to-excel/read" var="transportReadUrl" />

<demo:header />
 <div class="box wide">
      <h4>Information</h4>
      <p>Select the cells you want to copy press Ctrl/Command + C to copy into the clipboard then go to Excel and paste</p>
  </div>
    <kendo:grid name="cellSelection" selectable="multiple cell" allowCopy="true" scrollable="false" navigatable="true">
	<kendo:grid-pageable buttonCount="5" />	    		    		
    <kendo:grid-columns>
        <kendo:grid-column title="Order ID" field="orderId" width="300px"  />
        <kendo:grid-column title="Freight" field="freight" width="300px" />
        <kendo:grid-column title="Order Date" field="orderDate" format="{0:dd/MM/yyyy}"/>
    </kendo:grid-columns>
    <kendo:dataSource pageSize="5" serverPaging="true" serverSorting="true" serverFiltering="true" serverGrouping="true">
        <kendo:dataSource-transport>            	
            <kendo:dataSource-transport-read url="${transportReadUrl}" type="POST"  contentType="application/json"/>  
            <kendo:dataSource-transport-parameterMap>
            	function(options){return JSON.stringify(options);}
            </kendo:dataSource-transport-parameterMap>              
        </kendo:dataSource-transport>
        <kendo:dataSource-schema data="data" total="total" groups="data">
                <kendo:dataSource-schema-model>
                    <kendo:dataSource-schema-model-fields>
                        <kendo:dataSource-schema-model-field name="orderId" type="number" />
                        <kendo:dataSource-schema-model-field name="freight" type="number" />
                        <kendo:dataSource-schema-model-field name="orderDate" type="date" />
                    </kendo:dataSource-schema-model-fields>
                </kendo:dataSource-schema-model>
            </kendo:dataSource-schema>
    </kendo:dataSource>
</kendo:grid>
    
<demo:footer />
