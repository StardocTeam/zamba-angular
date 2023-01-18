<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<c:url value="/autocomplete/orders/" var="readUrl" />

<demo:header />
    <div class="demo-section k-content">
        <h4>Search for shipping name</h4>

        <kendo:autoComplete name="orders" dataTextField="shipName" filter="contains" style="width:100%;" height="520"
        					template='<span class="order-id">#= orderId #</span> #= shipName #, #= shipCountry #'>
            <kendo:dataSource pageSize="80" serverPaging="true" serverFiltering="true">
	            <kendo:dataSource-transport>
                   <kendo:dataSource-transport-read url="${readUrl}" type="POST" contentType="application/json"/>
                   <kendo:dataSource-transport-parameterMap>
	                	<script>
		                	function parameterMap(options,type) {
		                		return JSON.stringify(options);
		                	}
	                	</script>
	                </kendo:dataSource-transport-parameterMap>
                </kendo:dataSource-transport>
                <kendo:dataSource-schema data="data" total="total">
                </kendo:dataSource-schema>
	        </kendo:dataSource>
            <kendo:autoComplete-virtual>
            	<kendo:autoComplete-virtual-valueMapper>
            		<script>
	            		function valueMapper(options) {
	                        $.ajax({
	                            url: "http://demos.telerik.com/kendo-ui/service/Orders/ValueMapper",
	                            type: "GET",
	                            data: convertValues(options.value),
	                            success: function (data) {
	                                options.success(data);
	                            }
	                        });
	                    }
            		</script>
            	</kendo:autoComplete-virtual-valueMapper>
            </kendo:autoComplete-virtual>
        </kendo:autoComplete>

        <div class="demo-hint">Hint: type "an"</div>
    </div>
    <script>
	    function convertValues(value) {
	        var data = {};
	
	        value = $.isArray(value) ? value : [value];
	
	        for (var idx = 0; idx < value.length; idx++) {
	            data["values[" + idx + "]"] = value[idx];
	        }
	
	        return data;
	    }
    </script>
<demo:footer />
