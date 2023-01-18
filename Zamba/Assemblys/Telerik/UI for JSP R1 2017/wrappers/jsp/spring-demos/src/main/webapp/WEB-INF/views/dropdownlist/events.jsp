<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<demo:header />
 <div class="demo-section k-content">
        <h4 class="title">DropDownList</h4>
        <kendo:dropDownList name="dropdownlist" open="onOpen" close="onClose" change="onChange" select="onSelect"
            filtering="onFiltering" dataTextField="text" dataValueField="value" filter="startswith" style="width: 100%;">
            <kendo:dataSource data="${items}">
            </kendo:dataSource>
        </kendo:dropDownList>
 	</div>
    <div class="box">                
        <h4>Console log</h4>
        <div class="console"></div>
    </div>

    <script>
        function onOpen() {
            if ("kendoConsole" in window) {
                kendoConsole.log("event : open");
            }
        }

        function onClose() {
            if ("kendoConsole" in window) {
                kendoConsole.log("event : close");
            }
        }

        function onChange() {
            if ("kendoConsole" in window) {
                kendoConsole.log("event : change");
            }
        }

        function onFiltering(e) {
            if ("kendoConsole" in window) {
                kendoConsole.log("event :: filtering");
            }
        }

        function onSelect(e) {
            if ("kendoConsole" in window) {
                var dataItem = this.dataItem(e.item);
                kendoConsole.log("event :: select (" + dataItem.text + " : " + dataItem.value + ")" );
            }
        }
    </script>
<demo:footer />
