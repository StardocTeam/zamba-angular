<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<demo:header />

<div class="demo-section k-content">
    <h4>Set Value</h4>
    <kendo:numericTextBox name="numerictextbox" accesskey="w" style="width: 100%"></kendo:numericTextBox>
 </div>

 <div class="box">
 <h4>Keyboard legend</h4>
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
     <li>
         <span class="button-preview">
             <span class="key-button wide leftAlign">up arrow</span>
         </span>
         <span class="button-descr">
             increases the widget's value
         </span>
     </li>
     <li>
         <span class="button-preview">
             <span class="key-button wider leftAlign">down arrow</span>
         </span>
         <span class="button-descr">
             decreases the widget's value
         </span>
     </li>
 </ul>
 
<demo:footer />
