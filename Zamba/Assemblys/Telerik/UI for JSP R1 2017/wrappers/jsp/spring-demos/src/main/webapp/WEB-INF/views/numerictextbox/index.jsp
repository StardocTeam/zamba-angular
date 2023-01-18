<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<c:url value="resources/web/numerictextbox/addProduct.png" var="addProduct"/>
<demo:header />

<div id="add-product" class="demo-section k-content">
              <p class="title">Add new product</p>
              <ul id="fieldlist">
                  <li>
                      <label for="currency">Price:</label>
        <kendo:numericTextBox name="currency" format="c" min="0" max="100" value="30" style="width: 100%;"></kendo:numericTextBox>
    </li>
     <li>
         <label for="percentage">Price Discount:</label>
        <kendo:numericTextBox name="percentage" format="p0" min="0" max="0.9" value="0.05" step="0.01" style="width: 100%;"></kendo:numericTextBox>
    </li>
     <li>
         <label for="custom">Weight:</label>
        <kendo:numericTextBox name="custom" format="#.00 kg" value="2" style="width: 100%;"></kendo:numericTextBox>
  </li>
   <li>
       <label for="numeric">Currently in stock:</label>
        <kendo:numericTextBox name="numeric" placeholder="Enter numeric value" value="17" style="width: 100%;"></kendo:numericTextBox>
   </li>
  </ul>
</div>

 <style>
     .demo-section {
         padding: 0;
     }

     #add-product .title {
         font-size: 16px;
         color: #fff;
         background-color: #1e88e5;
         padding: 20px 30px;
         margin: 0;
    }

    #fieldlist {
        margin: 0 0 -1.5em;
        padding: 30px;
    }

    #fieldlist li {
        list-style: none;
        padding-bottom: 1.5em;
    }

    #fieldlist label {
        display: block;
        padding-bottom: .6em;
        font-weight: bold;
        text-transform: uppercase;
        font-size: 12px;
        color: #444;
    }

 </style>

<demo:footer />
