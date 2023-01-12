<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@page import="com.kendoui.spring.models.DropDownListItem"%>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />
	<%
	String[] values = {"Anne King", "Andrew Fuller"};
	%>
    <div class="demo-section k-content">
        <h4>'Single' tag mode</h4>
	    <kendo:multiSelect name="required" placeholder="Select attendees..." value="<%= values %>"
            autoClose="false" tagMode="single">
	        <kendo:dataSource data="${attendees}"></kendo:dataSource>
        </kendo:multiSelect>
        <h4>'Multiple' tag mode</h4>
	    <kendo:multiSelect name="optional" autoClose="false" placeholder="Select attendees...">
	        <kendo:dataSource data="${attendees}"></kendo:dataSource>
	    </kendo:multiSelect>
	</div>
    <style>
        .demo-section * + h4 {
            margin-top: 2em;
        }
    </style>
<demo:footer />
