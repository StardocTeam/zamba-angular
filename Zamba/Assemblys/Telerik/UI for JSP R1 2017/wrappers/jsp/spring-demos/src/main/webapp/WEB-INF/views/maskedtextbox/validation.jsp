<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<demo:header />

<div class="demo-section k-content">
               <form id="employeeForm" data-role="validator" novalidate="novalidate">
                   <ul id="fieldlist">
                       <li>
                           <label for="FirstName">First Name:</label>
                           <input type="text" class="k-textbox" name="FirstName" id="FirstName" required="required" />
                       </li>
                       <li>
                           <label for="LastName">Last Name:</label>
                           <input type="text" class="k-textbox" name="LastName" id="LastName" required="required" />
                       </li>
                       <li>
                           <label for="PhoneNumber">Phone Number:</label>
                <kendo:maskedTextBox name="PhoneNumber" mask="(999) 000-0000" required="required" data-validmask-msg="Phone number is incomplete"></kendo:maskedTextBox>
              <span data-for='PhoneNumber' class='k-invalid-msg'></span>
            </li>
            <li class="actions">
                 <button type="button" data-role="button" data-sprite-css-class="k-icon k-i-tick" data-click='save'>Save</button>
            </li>
        </ul>
    </form>
</div>

<script type="text/javascript">
    $(function () {
        var container = $("#employeeForm");

        kendo.init(container);

        container.kendoValidator({
            rules: {
                validmask: function (input) {
                    console.log(input);
                    if (input.is("[data-validmask-msg]") && input.val() != "") {
                        var maskedtextbox = input.data("kendoMaskedTextBox");
                        return maskedtextbox.value().indexOf(maskedtextbox.options.promptChar) === -1;
                    }

                    return true;
                }
            }
        });
    });

    function save(e) {
        var validator = $("#employeeForm").data("kendoValidator");
        if (validator.validate()) {
            alert("Employee Saved");
        }
    }

</script>

<style>

    #fieldlist {
        margin: 0 0 -2em;
        padding: 0;
    }

    #fieldlist li {
        list-style: none;
        padding-bottom: 2em;
    }

    #fieldlist label {
        display: block;
        padding-bottom: 1em;
        font-weight: bold;
        text-transform: uppercase;
        font-size: 12px;
        color: #444;
    }

    #fieldlist input {
        width: 100%;
    }

    span.k-tooltip {
        margin-top: 5px;
        line-height: 1.7em;
        width: 100%;
        box-sizing: border-box;
        text-align: left;
    }

</style>

<demo:footer />
