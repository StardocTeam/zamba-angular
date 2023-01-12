<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<c:url value="/dropdownlist/cascadingdropdownlist/categories" var="categoriesUrl" />
<c:url value="/dropdownlist/cascadingdropdownlist/products" var="productsUrl" />
<c:url value="/dropdownlist/cascadingdropdownlist/orders" var="ordersUrl" />

<demo:header />
   <div class="demo-section k-content">

           <h4>Categories:</h4>
            <kendo:dropDownList name="categories" dataTextField="categoryName" dataValueField="categoryId"
                            optionLabel="Select category..." style="width: 100%;">
                <kendo:dataSource>
                    <kendo:dataSource-transport>
                       <kendo:dataSource-transport-read url="${categoriesUrl}" type="POST" contentType="application/json"/>
                       <kendo:dataSource-transport-parameterMap>
                           function(options){return JSON.stringify(options);}
                       </kendo:dataSource-transport-parameterMap>
                    </kendo:dataSource-transport>
                    <kendo:dataSource-schema data="data" total="total">
                    </kendo:dataSource-schema>
                </kendo:dataSource>
            </kendo:dropDownList>
            
      		<h4 style="margin-top: 2em;">Products:</h4>

            <kendo:dropDownList name="products" dataTextField="productName" dataValueField="productId"
                            optionLabel="Select product..." cascadeFrom="categories" autoBind="false" style="width: 100%;">
                <kendo:dataSource serverFiltering="true">
                    <kendo:dataSource-transport>
                       <kendo:dataSource-transport-read url="${productsUrl}" type="POST" contentType="application/json"/>
                       <kendo:dataSource-transport-parameterMap>
                           function(options){return JSON.stringify(options);}
                       </kendo:dataSource-transport-parameterMap>
                    </kendo:dataSource-transport>
                    <kendo:dataSource-schema data="data" total="total">
                    </kendo:dataSource-schema>
                </kendo:dataSource>
            </kendo:dropDownList>
            
        	<h4 style="margin-top: 2em;">Orders:</h4>

            <kendo:dropDownList name="orders" dataTextField="shipCity" dataValueField="orderId"
                            optionLabel="Select order..." cascadeFrom="products" autoBind="false" style="width: 100%;">
                <kendo:dataSource serverFiltering="true">
                    <kendo:dataSource-transport>
                       <kendo:dataSource-transport-read url="${ordersUrl}" type="POST" contentType="application/json"/>
                       <kendo:dataSource-transport-parameterMap>
                           function(options){return JSON.stringify(options);}
                       </kendo:dataSource-transport-parameterMap>
                    </kendo:dataSource-transport>
                </kendo:dataSource>
            </kendo:dropDownList>
       <button class="k-button k-primary" id="get" style="margin-top: 2em; float: right;">View Order</button>
	 </div>
    <script>
        $(document).ready(function () {
            var categories = $("#categories").data("kendoDropDownList"),
                products = $("#products").data("kendoDropDownList"),
                orders = $("#orders").data("kendoDropDownList");

            $("#get").click(function () {
                var categoryInfo = "\nCategory: { id: " + categories.value() + ", name: " + categories.text() + " }",
                    productInfo = "\nProduct: { id: " + products.value() + ", name: " + products.text() + " }",
                    orderInfo = "\nOrder: { id: " + orders.value() + ", name: " + orders.text() + " }";

                alert("Order details:\n" + categoryInfo + productInfo + orderInfo);
            });
        });
    </script>
     <style>
         .k-readonly
         {
             color: gray;
         }
     </style>
<demo:footer />
