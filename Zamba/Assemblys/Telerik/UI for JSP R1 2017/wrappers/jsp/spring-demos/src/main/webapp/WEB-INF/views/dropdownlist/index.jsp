<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@page import="com.kendoui.spring.models.DropDownListItem"%>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />
    <div id="cap-view" class="demo-section k-content">
	     <h4>Customize your Kendo Cap</h4>
	     <div id="cap" class="black-cap"></div>
	     <h4>Cap Color</h4>
    <kendo:dropDownList name="color" dataTextField="text" dataValueField="value" change="change" value="1" style="width: 100%;">
        <kendo:dataSource data="${colors}"></kendo:dataSource>
    </kendo:dropDownList>  

    <h4 style="margin-top: 2em;">Cap Size</h4>
    <kendo:dropDownList name="size" style="width: 100%;">
        <kendo:dataSource data="${sizes}"></kendo:dataSource>
    </kendo:dropDownList>  
    
 		<button class="k-button k-primary" id="get" style="margin-top: 2em; float: right;">Customize</button>
    </div>
<style>
    #cap {
        width: 242px;
        height: 225px;
        margin: 20px auto;
        background-image: url(<c:url value="/resources/web/dropdownlist/cap.png"/>);
        background-repeat: no-repeat;
        background-color: transparent;
    }
    .black-cap {
        background-position: 0 0;
    }
    .grey-cap {
        background-position: 0 -225px;
    }
    .orange-cap {
        background-position: 0 -450px;
    }
</style>

<script>
    function change() {
        var value = $("#color").val();
        $("#cap")
                .toggleClass("black-cap", value == 1)
                .toggleClass("orange-cap", value == 2)
                .toggleClass("grey-cap", value == 3);
    };

    $(document).ready(function () {
        $("#get").click(function () {
            var color = $("#color").data("kendoDropDownList"),
                size = $("#size").data("kendoDropDownList");

            alert('Thank you! Your Choice is:\n\nColor ID: ' + color.value() + ' and Size: ' + size.value());
        });
    });
</script>
<demo:footer />