<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<demo:header />

 <div class="demo-section hidden-on-narrow k-content wide">
        <div id="bike">
            <div id="bike-tail"></div><div id="bike-head"></div>
        </div>

        <div class="picker-wrapper">
            <h4>Tail color</h4>
       	<kendo:flatColorPicker name="tail" class="picker" value="#000" change="select" preview="false">
       	</kendo:flatColorPicker>
    </div>
        <div class="picker-wrapper">
            <h4>Front &amp; side color</h4>
       	<kendo:flatColorPicker name="head" class="picker" value="#e15613" change="select" preview="false">
       	</kendo:flatColorPicker>
   </div>
    </div>

    <div class="responsive-message"></div>

<script>
    function select(e) {
        $("#bike-" + this.element.attr("id")).css("background-color", e.value);
    }
</script>

<style>
    .demo-section {
        text-align: center;
        padding: 0 0 16px;
        }

    #bike {
        margin: 0 0 10px;
        background: url(../resources/colorpicker/background.png);
    }

    #bike-head, #bike-tail {
        background: url(../resources/web/colorpicker/motor.png);
        display: inline-block;
        height: 299px;
        width: 241px;
    }

    #bike-head {
        background-color: #e15613;
        background-position: -241px 0;
     }

    .picker-wrapper {
        display: inline-block;
        text-align: left;
        width: 252px;
        margin: 0 18px;
    }

    .picker-wrapper .k-hsv-gradient {
        height: 140px;
     }

    .picker-wrapper h3 {
        padding: 13px 0 5px;
        text-align: left;
    }
</style>

<demo:footer />