<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<c:url value="/resources/web/calendar/calendar.png" var="calendarImg"/>
<demo:header />
<div class="demo-section k-content" style="text-align: center;">
    <h4>Pick a date</h4>	
	<kendo:calendar name="calendar">
	</kendo:calendar>
</div>
<demo:footer />