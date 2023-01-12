<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

<div class="box wide">
        <div class="box-col">
            <h4>Toggle state</h4>
            <ul class="options">
                <li>
                    <button class="k-button" id="enableProgressBar">Enable</button>
                    <button class="k-button" id="disableProgressBar">Disable</button>
                </li>
            </ul>
        </div>
        <div class="box-col">
            <h4>Value</h4>
            <ul class="options">
                <li>
                    <input type="text" id="newValue" value="25" class="k-textbox" placeholder="e.g. 23"/>             
                    <button class="k-button" id="setProgressBarValue">Set value</button>
                    <button class="k-button" id="getProgressBarValue">Get value</button>
                </li>
            </ul>
        </div>
        <div class="box-col">
            <h4>Indeterminate</h4>
            <ul class="options">
                <li>
                    <button class="k-button" id="setIndeterminate">Set indeterminate</button>
                </li>
            </ul>
        </div>
    </div>
    <div class="demo-section k-content">
        <kendo:progressBar name="progressBar" type="value" min="0" max="100" style="width: 100%;">
                	<kendo:progressBar-animation duration="400"></kendo:progressBar-animation>
        </kendo:progressBar>
    </div>

    <script>
        $(document).ready(function () {
            var pb = $("#progressBar").data("kendoProgressBar");

            $("#setProgressBarValue").click(function () {
                pb.value($("#newValue").val());
            });

            $("#getProgressBarValue").click(function () {
                alert("Current ProgressBar value is: " + pb.value());
            });

            $("#setIndeterminate").click(function () {
                pb.value(false);
            });

            $("#enableProgressBar").click(function () {
                pb.enable();
            });

            $("#disableProgressBar").click(function () {
                pb.enable(false);
            });
        });
    </script>

    <style>
        .box .k-textbox {
            margin: 0;
            width: 80px;
        }
        .k-button {
            min-width: 80px;
        }
    </style>
<demo:footer />