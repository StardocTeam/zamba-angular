<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<demo:header />
    <div class="demo-section k-content">
    	<h4>Products</h4>       
        <kendo:dropDownList name="products" dataTextField="productName" dataValueField="productId" filter="startswith" style="width: 100%;">
            <kendo:dataSource data="${products}"></kendo:dataSource>
        </kendo:dropDownList>
    </div>
<demo:footer />