<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

<div class="k-rtl demo-section k-content">
        <h4>ProgressBar</h4>
        <kendo:progressBar name="progressBar" type="percent" complete="onComplete">
                    	<kendo:progressBar-animation duration="600"></kendo:progressBar-animation>
        </kendo:progressBar>
        <button id="startProgress" class="k-button">Start progress</button>
    </div>

    <script>
        $("#startProgress").click(function () {
            if (!$(this).hasClass("k-state-disabled")) {
                $(this).addClass("k-state-disabled");

                progress();
            }
        });

        function onComplete() {
            $("#startProgress").removeClass("k-state-disabled").text("Restart Progress");
        }

        function progress() {
            var pb = $("#progressBar").data("kendoProgressBar");
            pb.value(0);

            var interval = setInterval(function () {
                if (pb.value() < 100) {
                    pb.value(pb.value() + 10);
                } else {
                    clearInterval(interval);
                }
            }, 100);
        }
    </script>

    <style>
        #progressBar {
            width: 100%;
            margin-bottom: 10px;
        }
        .demo-section {
            text-align: right;
        }
    </style> 

<demo:footer />