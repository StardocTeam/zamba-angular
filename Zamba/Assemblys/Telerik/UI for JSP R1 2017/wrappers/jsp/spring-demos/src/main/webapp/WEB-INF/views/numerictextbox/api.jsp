<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<demo:header />

 <div class="demo-section k-content">
    <h4>Set value</h4>
	<kendo:numericTextBox name="numerictextbox" style="width: 100%;"></kendo:numericTextBox>
 </div>

 <div class="box wide">
     <div class="box-col">
         <h4>Get / Set Value</h4>
         <ul class="options">
             <li>
                <button id="get" class="k-button">Get value</button> or <button id="focus" class="k-button">Focus</button>
            </li>
            <li>
                <input id="value" value="10" style="float:none" />
                <button id="set" class="k-button">Set value</button>
            </li>
         </ul>
     </div>
     <div class="box-col">
         <h4>Enable / Disable</h4>
         <ul class="options">
             <li>
                 <button id="enable" class="k-button">Enable</button>
                 <button id="disable" class="k-button">Disable</button>
             </li>
             <li>
                 <button id="readonly" class="k-button">Readonly</button>
             </li>
         </ul>
     </div>
 </div>
<script>
    $(document).ready(function () {
        var numerictextbox = $("#numerictextbox").data("kendoNumericTextBox");

        var setValue = function () {
            numerictextbox.value($("#value").val());
        };

        $("#enable").click(function () {
            numerictextbox.enable();
        });

        $("#disable").click(function () {
            numerictextbox.enable(false);
        });

        $("#readonly").click(function() {
            numerictextbox.readonly();
        });

        $("#value").kendoNumericTextBox({
            change: setValue
        });

        $("#set").click(setValue);

        $("#get").click(function () {
            alert(numerictextbox.value());
        });
    });
</script>

<demo:footer />
