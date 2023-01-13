<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<demo:header />
	<div class="demo-section k-content">
	    <div class="k-rtl">
	        <h4>Select a state in USA:</h4>
            <kendo:autoComplete name="states" placeholder="Select state..." style="width:100%;">
                <kendo:dataSource data="${states}">
                </kendo:dataSource>
            </kendo:autoComplete>
        	<div class="demo-hint">Hint: type <strong>m</strong></div>
    	</div>
    </div>
<demo:footer />
