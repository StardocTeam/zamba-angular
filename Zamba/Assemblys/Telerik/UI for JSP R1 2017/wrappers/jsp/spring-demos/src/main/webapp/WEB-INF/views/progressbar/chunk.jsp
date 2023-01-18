<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

  <div class="demo-section k-content">
        <h4>Profile Completeness: <span id="completed">40%</span></h4>
            <kendo:progressBar name="profileCompleteness" type="chunk" chunkCount="5" min="0" max="5" value="2"></kendo:progressBar>

            <ul class="forms">
            <li>
                <label>First Name</label>
                <input type="text" name="firstName" value="" class="k-textbox" style="width: 100%;" />
            </li>
            <li>
                <label>Last Name</label>
                <input type="text" name="lastName" value="" class="k-textbox" style="width: 100%;" />
            </li>
            <li>
                <label>Birthday</label>
                <input id="birthdayInput" type="date" name="birthday" value="" style="width: 100%;" />
            </li>
            <li>
                <label>Gender</label>
                <select id="genderInput" name="gender" style="width: 100%;">
                    <option value="male" selected>Male</option>
                    <option value="female">Female</option>
                    <option value="notsay">Rather not say</option>
                </select>
            </li>
            <li>
                <label>Occupation</label>
                <input type="text" name="occupation" value="Software Developer" class="k-textbox" style="width: 100%;" />
            </li>
        </ul>
    </div>

    <script>
        $(document).ready(function () {
            var pb = $("#profileCompleteness").data("kendoProgressBar");

            $("#genderInput").kendoDropDownList();

            $("#birthdayInput").kendoDatePicker();

            $("#birthdayInput").change(function (e) {
                var currentDate = kendo.parseDate(this.value);
                if (!currentDate) {
                    this.value = "";
                }
            });

            $(".forms input").change(function () {
                var completeness = 5;
                $(".forms input").each(function () {
                    if (this.value == "") {
                        completeness--;
                    }
                });

                pb.value(completeness);
                $(".completenessLevel h2").text((completeness * 20) + "%");
            });
        });
    </script>

    <style>
        #profileCompleteness {
            width: 100%;
        }

        .forms {
            list-style-type: none;
            padding: 2em 0 0;
            margin: 0;
        }
        
        .forms label {
            display: block;
            font-size: 12px;
            line-height: 1em;
            font-weight: bold;
            text-transform: uppercase;
            margin-bottom: 1em;
        }
        
        .forms li {
            margin-bottom: 1.5em;
        }
    </style>

<demo:footer />