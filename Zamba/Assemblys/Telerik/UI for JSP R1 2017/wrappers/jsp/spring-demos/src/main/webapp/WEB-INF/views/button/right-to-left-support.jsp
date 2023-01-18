<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

<div class="demo-section k-rtl k-content">
    <h4>Basic Button</h4>
    <p>
<kendo:button name="textButton" type="button">
    <kendo:button-content>
       Submit
    </kendo:button-content>
</kendo:button>
    </p>
    
    <h4>Buttons with icons</h4>
    <p>
<kendo:button name="iconTextButton" tag="a" icon="funnel">
    <kendo:button-content>
        Filter
    </kendo:button-content>
</kendo:button>
<kendo:button name="iconButton" tag="em" icon="refresh">
    <kendo:button-content>
        <span class='k-icon'>Refresh</span>
    </kendo:button-content>
</kendo:button>
    </p>
    
    <h4>Disabled Button</h4>
    <p>
<kendo:button name="disabledButton" tag="span" enable="false">
    <kendo:button-content>
        Disabled
    </kendo:button-content>
</kendo:button>
    </p>
</div>

<style>
    .demo-section p {
        margin: 0 0 30px;
        line-height: 4em;
    }
    .demo-section .k-button {
        margin-left: 10px;
    }
    #textButton, #disabledButton {
        width: 150px;
    }
</style>

<demo:footer />