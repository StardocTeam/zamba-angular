<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@page import="com.kendoui.spring.models.DropDownListItem"%>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />
<div class="demo-section k-content">
    <h4>T-shirt Sizes</h4>
	<kendo:multiSelect name="fabric" filter="contains" dataTextField="text" dataValueField="value" accesskey="w">
	          <kendo:dataSource data="${sizes}"></kendo:dataSource>
	</kendo:multiSelect>
</div>

<div class="box wide">
    <div class="box-col">
    <h4>Focus</h4>
    <ul class="keyboard-legend">
        <li>
            <span class="button-preview">
                <span class="key-button leftAlign wider"><a target="_blank" href="http://en.wikipedia.org/wiki/Access_key">Access key</a></span>
                +
                <span class="key-button">w</span>
            </span>
            <span class="button-descr">
                focuses the widget
            </span>
        </li>
    </ul>
    </div>
    <div class="box-col">
    <h4>Closed popup:</h4>
    <ul class="keyboard-legend">
        <li>
            <span class="button-preview">
                <span class="key-button wide leftAlign">left arrow</span>
            </span>
            <span class="button-descr">
                highlights previous selected item
            </span>
        </li>
        <li>
            <span class="button-preview">
                <span class="key-button wider leftAlign">right arrow</span>
            </span>
            <span class="button-descr">
                highlights next selected item
            </span>
        </li>
        <li>
            <span class="button-preview">
                <span class="key-button">home</span>
            </span>
            <span class="button-descr">
                highlights first selected item
            </span>
        </li>
        <li>
            <span class="button-preview">
                <span class="key-button">end</span>
            </span>
            <span class="button-descr">
                highlights last selected item
            </span>
        </li>
        <li>
            <span class="button-preview">
                <span class="key-button wider rightAlign">delete</span>
            </span>
            <span class="button-descr">
                deletes highlighted item
            </span>
        </li>
        <li>
            <span class="button-preview">
                <span class="key-button wider rightAlign">backspace</span>
            </span>
            <span class="button-descr">
                deletes previous selected item
            </span>
        </li>
        <li>
            <span class="button-preview">
                <span class="key-button wider leftAlign">down arrow</span>
            </span>
            <span class="button-descr">
                opens the popup
            </span>
        </li>
        <li>
            <span class="button-preview">
                <span class="key-button">esc</span>
            </span>
            <span class="button-descr">
                clears highlighted item
            </span>
        </li>
    </ul>
    </div>
    <div class="box-col">
    <h4>Opened popup:</h4>
    <ul class="keyboard-legend">
        <li>
            <span class="button-preview">
                <span class="key-button wide leftAlign">up arrow</span>
            </span>
            <span class="button-descr">
                highlights previous item
            </span>
        </li>
        <li>
            <span class="button-preview">
                <span class="key-button wider leftAlign">down arrow</span>
            </span>
            <span class="button-descr">
                highlights next item
            </span>
        </li>
        <li>
            <span class="button-preview">
                <span class="key-button">home</span>
            </span>
            <span class="button-descr">
                highlights first item in the popup
            </span>
        </li>
        <li>
            <span class="button-preview">
                <span class="key-button">end</span>
            </span>
            <span class="button-descr">
                highlights last item in the popup
            </span>
        </li>
        <li>
            <span class="button-preview">
                <span class="key-button wider rightAlign">enter</span>
            </span>
            <span class="button-descr">
                selects highlighted item
            </span>
        </li>
       <li>
            <span class="button-preview">
                <span class="key-button">esc</span>
            </span>
            <span class="button-descr">
                closes the popup
            </span>
        </li>
        <li>
            <span class="button-preview">
                <span class="key-button wide leftAlign">up arrow</span>
            </span>
            <span class="button-descr">
                closes the popup if the first item is highlighted
            </span>
        </li>
    </ul>
    </div>
</div>
<demo:footer />
