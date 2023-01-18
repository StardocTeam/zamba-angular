<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<demo:header />

<div class="demo-section k-content">
    <label for="colorpicker">ColorPicker:</label>
    <kendo:colorPicker name="colorpicker" value="#0000ff" toolIcon="k-foreColor">
    </kendo:colorPicker>
  </div>

<div class="box">
    <div class="box-col">
    <h4>Values</h4>
    <ul class="options">
        <li>
            <input id="value" value="#ff0000"  class="k-textbox" style="width: 100px; margin: 0;" />
            <button id="set" class="k-button">Set value</button>
        </li>
        <li><button id="get" class="k-button">Get value</button></li>
    </ul>
    </div>
    <div class="box-col">
    <h4>Enable/Disable</h4>
    <ul class="options">
        <li>
            <button id="enable" class="k-button">Enable</button> 
            <button id="disable" class="k-button">Disable</button>
        </li>
    </ul>
    </div>
    <div class="box-col">
    <h4>Open/Close</h4>
    <ul class="options">
        <li>
            <button id="open" class="k-button">Open</button> 
            <button id="close" class="k-button">Close</button>
        </li>
    </ul>
    </div>
</div>

<script>
    $(document).ready(function() {
        var colorpicker = $("#colorpicker").data("kendoColorPicker");

        $("#enable").click(function(){
            colorpicker.enable();
        });

        $("#disable").click(function(){
            colorpicker.enable(false);
        });

        $("#get").click(function(){
            alert( colorpicker.value() );
        });

        $("#set").click(function(){
            var color = $("#value").val();
            try {
                color = kendo.parseColor(color);
                colorpicker.value(color);
            } catch(ex) {
                alert('Cannot parse color: "' + color + '"');
            }
        });

        $("#open").click(function(){
            colorpicker.open();
        });

        $("#close").click(function(){
            colorpicker.close();
        });
    });
</script>

<style>
  .k-button {
     min-width: 80px;
  }
</style>

<demo:footer />