<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<demo:header />

<div class="demo-section k-content">
    <div class="box-col">
        <h4>Simple (palette-based) picker:</h4>

<kendo:colorPicker name="basic" value="#cc2222" palette="websafe" accessKey="s">
</kendo:colorPicker>

<ul class="keyboard-legend" style="padding-top: 2em;">
            <li>
            <span class="button-preview">
                <span class="key-button leftAlign wider"><a target="_blank" href="http://en.wikipedia.org/wiki/Access_key">Access key</a></span>
                +
                <span class="key-button">s</span>
            </span>
            <span class="button-descr">Focuses the simple selector</span>
            </li>
        </ul>
    </div>
    <div class="box-col">
        <h4>Keyboard legend</h4>
        <ul class="keyboard-legend">
            <li>
            <span class="button-preview">
                <span class="key-button leftAlign wider">enter, down</span>
            </span>
            <span class="button-descr">(when popup is closed) opens the popup</span>
            </li>
            <li>
            <span class="button-preview">
                <span class="key-button leftAlign wider">left arrow</span>
            </span>
            <span class="button-descr">selects previous color</span>
            </li>
            <li>
            <span class="button-preview">
                <span class="key-button leftAlign wider">right arrow</span>
            </span>
            <span class="button-descr">selects next color</span>
            </li>
        </ul>
    </div>
    <div class="box-col">
        <h4>&nbsp;</h4>
        <ul class="keyboard-legend">
            <li>
            <span class="button-preview">
                <span class="key-button leftAlign wider">up/down</span>
            </span>
            <span class="button-descr">move one row up/down</span>
            </li>
            <li>
            <span class="button-preview">
                <span class="key-button leftAlign wider">enter</span>
            </span>
            <span class="button-descr">select current color</span>
            </li>
            <li>
            <span class="button-preview">
                <span class="key-button leftAlign wider">escape</span>
            </span>
            <span class="button-descr">cancel the selection</span>
            </li>
        </ul>
    </div>
</div>

<div class="demo-section k-content">
    <div class="box-col" style="width: 400px;">
        <h4>HSV picker:</h4>

<kendo:colorPicker name="hsv" value="#22cc22" opacity="true" accessKey="h">
</kendo:colorPicker>

 <ul class="keyboard-legend" style="padding-top: 2em;">
            <li>
                <span class="button-preview">
                <span class="key-button leftAlign wider"><a target="_blank" href="http://en.wikipedia.org/wiki/Access_key">Access key</a></span>
                +
                <span class="key-button">h</span>
                </span>
                <span class="button-descr">Focuses the HSV selector</span>
            </li>
         </ul>
    </div>

    <div class="box-col">
        <h4>Keyboard legend</h4>
        <ul class="keyboard-legend">
            <li>
                <span class="button-preview">
                <span class="key-button leftAlign wider">enter, down</span>
                </span>
                <span class="button-descr">(when popup is closed) opens the popup</span>
            </li>
            <li>
                <span class="button-preview">
                <span class="key-button leftAlign wider">arrows</span>
                </span>
                <span class="button-descr">update saturation/value in the big rectangle</span>
            </li>
            <li>
                <span class="button-preview">
                <span class="key-button leftAlign wider">ctrl + left/right</span>
                </span>
                <span class="button-descr">update hue slider</span>
            </li>
            <li>
                <span class="button-preview">
                <span class="key-button leftAlign wider">ctrl + up/down</span>
                </span>
                <span class="button-descr">update opacity slider (when opacity present)</span>
            </li>
        </ul>
    </div>
    <div class="box-col">
        <h4>&nbsp;</h4>
        <ul class="keyboard-legend">
            <li>
                <span class="button-preview">
                <span class="key-button leftAlign wider">shift</span>
                </span>
                <span class="button-descr">hold shift for fine-tuning</span>
            </li>
            <li>
                <span class="button-preview">
                <span class="key-button leftAlign wider">enter</span>
                </span>
                <span class="button-descr">select current color</span>
            </li>
            <li>
                <span class="button-preview">
                <span class="key-button leftAlign wider">escape</span>
                </span>
                <span class="button-descr">cancel the selection</span>
            </li>
        </ul>
    </div>
</div>

<demo:footer />