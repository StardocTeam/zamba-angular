<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@page import="com.kendoui.spring.models.DropDownListItem"%>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />
    <div id="tshirt-view" class="demo-section k-content">
	    <h4>Customize your Kendo T-shirt</h4>
	    <img id="tshirt" src="<c:url value="/resources/web/combobox/tShirt.png"/>" />
	    <h4>T-shirt Fabric</h4>
	    <kendo:comboBox name="fabric" filter="contains" placeholder="Select fabric..." index="3" suggest="true" 
	                    dataTextField="text" dataValueField="value" style="width: 100%;">
	              <kendo:dataSource data="${fabrics}"></kendo:dataSource>
	    </kendo:comboBox>
	
	    <h4 style="margin-top: 2em;">T-shirt Size</h4> 
	    <kendo:comboBox name="size" placeholder="Select size..." index="0" suggest="true" style="width: 100%;">
	              <kendo:dataSource data="${sizes}"></kendo:dataSource>
	    </kendo:comboBox>
	    
	    <button class="k-button k-primary" id="get" style="margin-top: 2em; float: right;">Customize</button>
	</div>
	<style>
	#tshirt {
	    display: block;
	    margin: 2em auto;
	}
	.k-readonly
	{
	    color: gray;
	}
	</style>
	<script>
	$(document).ready(function() {
        var fabric = $("#fabric").data("kendoComboBox");
        var size = $("#size").data("kendoComboBox");

        $("#get").click(function() {
            alert('Thank you! Your Choice is:\n\nFabric ID: ' + fabric.value() + ' and Size: ' + size.value());
        });
    });
	</script>
<demo:footer />