<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />
<div class="demo-section k-content">
    <h4>Start date</h4>
	<kendo:dateTimePicker name="start" value="${today}" max="${today}" change="startChange" style="width: 100%;"></kendo:dateTimePicker>
	
	<h4 style="margin-top: 2em;">End date</h4>
	<kendo:dateTimePicker name="end" value="${today}" min="${today}" change="endChange" style="width: 100%;"></kendo:dateTimePicker>
</div>

<script>
    function startChange() {
        var endPicker = $("#end").data("kendoDateTimePicker"),
            startDate = this.value();

        if (startDate) {
            startDate = new Date(startDate);
            startDate.setDate(startDate.getDate() + 1);
            endPicker.min(startDate);
        }
    }

    function endChange() {
        var startPicker = $("#start").data("kendoDateTimePicker"),
            endDate = this.value();

        if (endDate) {
            endDate = new Date(endDate);
            endDate.setDate(endDate.getDate() - 1);
            startPicker.max(endDate);
        }
    }
</script>

<demo:footer />