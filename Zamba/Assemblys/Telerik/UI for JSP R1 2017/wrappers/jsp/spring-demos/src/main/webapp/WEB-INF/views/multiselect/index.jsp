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
        <h2>Invite Attendees</h2>
        <label for="required">Required</label>
	    <kendo:multiSelect name="required" placeholder="Select attendees..." value="<%= values %>">
	        <kendo:dataSource data="${attendees}"></kendo:dataSource>
	    </kendo:multiSelect>
	    <label for="optional">Optional</label>
	    <kendo:multiSelect name="optional" autoClose="false" placeholder="Select attendees...">
	        <kendo:dataSource data="${attendees}"></kendo:dataSource>
	    </kendo:multiSelect>
	    <button class="k-button" id="get">Send Invitation</button>
	</div>
    <script>
        $(document).ready(function() {
            var required = $("#required").data("kendoMultiSelect");
            var optional = $("#optional").data("kendoMultiSelect");

            $("#get").click(function() {
                alert("Attendees:\n\nRequired: " + required.value() + "\nOptional: " + optional.value());
            });
        });
    </script>
	<style>
	    .demo-section label {
	        display: block;
	        margin: 15px 0 5px 0;
	    }
	    #get {
	        float: right;
	        margin: 25px auto 0;
	    }
	</style>
<demo:footer />
