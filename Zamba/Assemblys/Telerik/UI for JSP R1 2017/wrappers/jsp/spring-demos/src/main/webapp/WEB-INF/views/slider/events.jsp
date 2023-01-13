<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

<div class="demo-section k-content">
    <ul id="fieldlist">
        <li>
            <label>Temperature</label>
            <kendo:slider name="slider" class="temperature" change="sliderOnChange" slide="sliderOnSlide"
                  min="0" max="30" smallStep="1" largeStep="10" value="18">
            </kendo:slider>
        </li>
        <li>
            <label>Humidity</label>
            <kendo:rangeSlider name="rangeSlider" class="humidity" change="rangeSliderOnChange" slide="rangeSliderOnSlide"
                  min="0" max="10" smallStep="1" largeStep="2" tickPlacement="both">
            </kendo:rangeSlider>
        </li>
    </ul>
</div>


<div class="box">
    <h4>Console log</h4>
    <div class="console"></div>
</div>

<script>
	function sliderOnSlide(e) {
	    kendoConsole.log("Slide :: new slide value is: " + e.value);
	}
	
	function sliderOnChange(e) {
	    kendoConsole.log("Change :: new value is: " + e.value);
	}
	
	function rangeSliderOnSlide(e) {
	    kendoConsole.log("Slide :: new slide values are: " + e.value.toString().replace(",", " - "));
	}
	
	function rangeSliderOnChange(e) {
	    kendoConsole.log("Change :: new values are: " + e.value.toString().replace(",", " - "));
	}
</script>


<style>
   #fieldlist {
       margin: 0 0 -2em;
       padding: 0;
       text-align: center;
   }

   #fieldlist > li {
       list-style: none;
       padding-bottom: 2em;
   }

   #fieldlist label {
       display: block;
       padding-bottom: 1em;
       font-weight: bold;
       text-transform: uppercase;
       font-size: 12px;
       color: #444;
   }
</style>

<demo:footer />
