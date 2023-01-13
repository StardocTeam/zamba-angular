<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

<div class="box">
    <div class="box-col">
        <h4>Button API Functions</h4>
        <ul class="options">
            <li>
                <button class="k-button" id="enableButton" type="button">Enable</button>
                <button class="k-button" id="disableButton" type="button">Disable</button>
            </li>
        </ul>
    </div>
</div>

<div class="demo-section k-content">
<kendo:button name="iconTextButton" type="button" icon="ungroup">
    <kendo:button-content>
        Kendo UI Button
    </kendo:button-content>
</kendo:button>
</div>

<style>
    .demo-section {
        text-align: center;
    }
    .box .k-textbox {
        margin: 0;
        width: 80px;
    }
</style>

<script>

$(document).ready(function () {
    var buttonObject = $("#iconTextButton").data("kendoButton");

    $("#enableButton").click(function () {
        buttonObject.enable(true);
    });

    $("#disableButton").click(function () {
        buttonObject.enable(false);
    });
});

</script>

<demo:footer />