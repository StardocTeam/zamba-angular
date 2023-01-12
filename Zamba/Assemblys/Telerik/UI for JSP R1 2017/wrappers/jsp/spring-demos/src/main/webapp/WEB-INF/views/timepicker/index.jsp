<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

 <div class="demo-section k-content">
    <h4>Set alarm time</h4>

	<kendo:timePicker name="timepicker" value="${now}" style="width: 100%;"></kendo:timePicker>
  	<h4 style="padding-top: 2em;">Alarm descriprion</h4>
  	<input id="descr" class="k-textbox" type="text" value="Wake up" style="width: 100%" />
</div>

<demo:footer />