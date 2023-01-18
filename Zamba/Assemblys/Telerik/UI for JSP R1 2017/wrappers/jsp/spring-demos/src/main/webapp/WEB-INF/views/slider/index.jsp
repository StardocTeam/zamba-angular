<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

<div class="demo-section k-content">
	<h4>Balance</h4>
	<kendo:slider name="slider" increaseButtonTitle="Right" decreaseButtonTitle="Left" min="-10" max="10"
				  smallStep="2" largeStep="1" value="0" class="balSlider">
	</kendo:slider>

    <h4 style="padding-top: 2em;">Equalizer</h4>
    <div id="equalizer">
	    <kendo:slider name="eqSlider1" orientation="vertical" min="-20" max="20"
				  smallStep="1" largeStep="20" showButtons="false" value="10" class="eqSlider">
		</kendo:slider>
		
		<kendo:slider name="eqSlider2" orientation="vertical" min="-20" max="20"
				  smallStep="1" largeStep="20" showButtons="false" value="5" class="eqSlider">
		</kendo:slider>
		
		<kendo:slider name="eqSlider3" orientation="vertical" min="-20" max="20"
				  smallStep="1" largeStep="20" showButtons="false" value="0" class="eqSlider">
		</kendo:slider>

		<kendo:slider name="eqSlider4" orientation="vertical" min="-20" max="20"
				  smallStep="1" largeStep="20" showButtons="false" value="10" class="eqSlider">
		</kendo:slider>
		
		<kendo:slider name="eqSlider5" orientation="vertical" min="-20" max="20"
				  smallStep="1" largeStep="20" showButtons="false" value="15" class="eqSlider">
		</kendo:slider>
	</div>
</div>
	
<style>
    .demo-section {
        text-align: center;
    }

    #equalizer {
        padding-right: 15px;
    }

    div.balSlider {
        width: 100%;
    }

    div.balSlider .k-slider-selection {
        display: none;
    }

    div.eqSlider {
        display: inline-block;
        margin: 1em;
        height: 122px;
        vertical-align: top;
    }
</style>

<demo:footer />