<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

 <div class="demo-section k-content">
        <div class="loading">
        	<kendo:progressBar name="totalProgressBar" type="chunk" chunkCount="4" min="0" max="4" orientation="vertical" complete="onTotalComplete"></kendo:progressBar>

			<kendo:progressBar name="loadingProgressBar" orientation="vertical" showStatus="false" change="onChange" complete="onComplete">
				<kendo:progressBar-animation duration="0"></kendo:progressBar-animation>
			</kendo:progressBar>
        </div>
        <div class="loadingInfo">
            <h4>Loading styles</h4>
            <div class="statusContainer">
                <p>
                Loaded: <span class="loadingStatus">0%</span> <br />
                Item <span class="chunkStatus">1</span> of 4
                </p>
            </div>
            <button class="k-button reloadButton">Reload</button>
        </div>
    </div>

    <script>
        $(document).ready(function () {
            load();
        });

        function onChange(e) {
            $(".loadingStatus").text(e.value + "%");
        }

        function onComplete(e) {
            var total = $("#totalProgressBar").data("kendoProgressBar");
            total.value(total.value() + 1);

            if (total.value() < total.options.max) {
                $(".chunkStatus").text(total.value() + 1);
                $(".loadingInfo h4").text("Loading " + itemsToLoad[total.value()]);

                load();
            }
        }

        function onTotalComplete(e) {
            $(".loadingInfo h4").text("All items loaded");
            $(".statusContainer").hide();
            $(".reloadButton").show();
        }

        function load() {
            var pb = $("#loadingProgressBar").data("kendoProgressBar");
            pb.value(0);

            var interval = setInterval(function () {
                if (pb.value() < 100) {
                    pb.value(pb.value() + 1);
                } else {
                    clearInterval(interval);
                }
            }, 30);
        }

        $(".reloadButton").click(function () {
            $(this).hide();
            $(".statusContainer").show();

            $("#totalProgressBar").data("kendoProgressBar").value(0);
            $("#loadingProgressBar").data("kendoProgressBar").value(0);
            $(".loadingInfo h4").text("Loading " + itemsToLoad[0]);
            $(".chunkStatus").text(1);

            load();
        });

        var itemsToLoad = ["styles", "scripts", "images", "fonts"];
    </script>
    <style>
        .demo-section {
            overflow: auto;
        }
        .k-progressbar
        {
            width: 8px;
            height: 100px;
        }
        
        #loadingProgressBar
        {
            margin-left: 10px;
        }
        
        .loading
        {
            float: left;
        }
        .loadingInfo
        {
            float: left;
            margin: 20px 0 0 30px;
        }

        .reloadButton
        {
            display: none;
            margin-top: 10px;
        }
    </style>

<demo:footer />