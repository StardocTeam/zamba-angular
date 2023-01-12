<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

<div class="demo-section k-content k-rtl">
    <h4>RTL Slider</h4>
    <kendo:slider name="slider" class="temperature" min="0" max="30" smallStep="1" largeStep="10" value="18">
	</kendo:slider>
</div>

<div class="demo-section k-content k-rtl">
    <h4>RTL RangeSlider</h4>
	<kendo:rangeSlider name="rangeSlider" class="humidity" min="0" max="10" smallStep="1" largeStep="2" tickPlacement="both">
    </kendo:rangeSlider>
</div>


<demo:footer />