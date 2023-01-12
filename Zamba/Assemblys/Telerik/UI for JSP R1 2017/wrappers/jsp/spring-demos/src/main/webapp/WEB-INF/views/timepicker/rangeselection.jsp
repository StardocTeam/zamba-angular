<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />
  <div class="demo-section k-content">
    <h4>Start time</h4>
	<kendo:timePicker name="start" value="${startValue}" min="${startMin}" max="${startMax}" change="startChange" style="width: 100%;"></kendo:timePicker>
	
	<h4 style="margin-top: 2em;">End time</h4>
	<kendo:timePicker name="end" value="${endValue}" min="${endMin}" max="${endMax}" style="width: 100%;"></kendo:timePicker>
</div>

<script>
    function startChange() {
        var startTime = this.value(),
            endPicker = $("#end").data("kendoTimePicker");

        if (startTime) {
            startTime = new Date(startTime);

            endPicker.max(startTime);

            startTime.setMinutes(startTime.getMinutes() + this.options.interval);

            endPicker.min(startTime);
            endPicker.value(startTime);
        }
    }
</script>

<demo:footer />