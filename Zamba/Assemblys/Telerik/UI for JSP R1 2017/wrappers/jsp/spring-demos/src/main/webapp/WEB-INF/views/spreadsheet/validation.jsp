<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>
<c:url value="/resources/shared/js/jszip.min.js" var="jszip" />

<demo:header />
<%
	String[] mergedCells = { "A1:E1"};

	boolean trueValue = true;
	boolean falseValue = false;
%>

<kendo:spreadsheet name="spreadsheet" rows="26" columns="30" sheetsbar="false" style="width: 100%;">
	<kendo:spreadsheet-sheets>
		<kendo:spreadsheet-sheet name="ContactsForm"
			mergedCells="<%=mergedCells%>">
			<kendo:spreadsheet-sheet-rows>

				<kendo:spreadsheet-sheet-row height='70'>
					<kendo:spreadsheet-sheet-row-cells>
						<kendo:spreadsheet-sheet-row-cell index='0'
							value='CONTACTS FORM' fontSize='32' textAlign='center' enable="false"
							background='rgb(96,181,255)' color='white' />
					</kendo:spreadsheet-sheet-row-cells>
				</kendo:spreadsheet-sheet-row>

				<kendo:spreadsheet-sheet-row height='25'>
					<kendo:spreadsheet-sheet-row-cells>
						<kendo:spreadsheet-sheet-row-cell
							value='Full Name' textAlign='center' enable="false"
							background='rgb(167,214,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell
							value='Email' textAlign='center' enable="false"
							background='rgb(167,214,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell
							value='Date of Birth' textAlign='center' enable="false"
							background='rgb(167,214,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell
							value='Phone' textAlign='center' enable="false"
							background='rgb(167,214,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell
							value='Confirmed' textAlign='center' enable="false"
							background='rgb(167,214,255)' color='rgb(0,62,117)' />
					</kendo:spreadsheet-sheet-row-cells>
				</kendo:spreadsheet-sheet-row>

				<kendo:spreadsheet-sheet-row height='25'>
					<kendo:spreadsheet-sheet-row-cells>
						<kendo:spreadsheet-sheet-row-cell value="Maria Anders">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="custom"
								from="AND(LEN(A3)>3, LEN(A3)200)"
								titleTemplate="Full Name validation error"
								messageTemplate="The full name should be longer than 3 letters and shorter than 200."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="maria.anders@mail.com">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="custom"
								from="AND(NOT(ISERROR(FIND(\"@\", B3))), NOT(ISERROR(FIND(\".\", B3))), ISERROR(FIND(\" \", J1)), LEN(B3)>5)"
								titleTemplate="Email validation error"
								messageTemplate="The value entered is not an valid email address."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="31232" format="m/d/yyyy">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="date"
								comparerType="between"
								from="DATEVALUE(\"1/1/1900\")"
								to="DATEVALUE(\"1/1/1998\")"
								titleTemplate="Birth Date validaiton error"
								messageTemplate="Birth Date should be between 1899 and 1998 year."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="921123465">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="custom"
								from="AND(ISNUMBER(D3),LEN(D3)<14)"
								titleTemplate="Phone validation error"
								messageTemplate="The value entered is not an valid phone number. Please enter numeric value with less than 14 digits."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="<%= trueValue %>">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="list"
								from="ListValues!A1:B1"
								titleTemplate="Invalid value"
								messageTemplate="Valid values are 'true' and 'false'."/>
						</kendo:spreadsheet-sheet-row-cell>
					</kendo:spreadsheet-sheet-row-cells>
				</kendo:spreadsheet-sheet-row>

				<kendo:spreadsheet-sheet-row height='25'>
					<kendo:spreadsheet-sheet-row-cells>
						<kendo:spreadsheet-sheet-row-cell value="Ana Trujillo">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="custom"
								from="AND(LEN(A4)>3, LEN(A4)200)"
								titleTemplate="Full Name validation error"
								messageTemplate="The full name should be longer than 3 letters and shorter than 200."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="ana.trujillo@mail.com">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="custom"
								from="AND(NOT(ISERROR(FIND(\"@\", B4))), NOT(ISERROR(FIND(\".\", B4))), ISERROR(FIND(\" \", J1)), LEN(B4)>5)"
								titleTemplate="Email validation error"
								messageTemplate="The value entered is not an valid email address."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="31222" format="m/d/yyyy">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="date"
								comparerType="between"
								from="DATEVALUE(\"1/1/1900\")"
								to="DATEVALUE(\"1/1/1998\")"
								titleTemplate="Birth Date validaiton error"
								messageTemplate="Birth Date should be between 1899 and 1998 year."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="55554729">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="custom"
								from="AND(ISNUMBER(D4),LEN(D4)<14)"
								titleTemplate="Phone validation error"
								messageTemplate="The value entered is not an valid phone number. Please enter numeric value with less than 14 digits."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="<%= trueValue %>">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="list"
								from="ListValues!A1:B1"
								titleTemplate="Invalid value"
								messageTemplate="Valid values are 'true' and 'false'."/>
						</kendo:spreadsheet-sheet-row-cell>
					</kendo:spreadsheet-sheet-row-cells>
				</kendo:spreadsheet-sheet-row>

				<kendo:spreadsheet-sheet-row height='25'>
					<kendo:spreadsheet-sheet-row-cells>
						<kendo:spreadsheet-sheet-row-cell value="Antonio Moreno">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="custom"
								from="AND(LEN(A5)>3, LEN(A5)200)"
								titleTemplate="Full Name validation error"
								messageTemplate="The full name should be longer than 3 letters and shorter than 200."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="antonio.moreno@mail.com">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="custom"
								from="AND(NOT(ISERROR(FIND(\"@\", B5))), NOT(ISERROR(FIND(\".\", B5))), ISERROR(FIND(\" \", J1)), LEN(B5)>5)"
								titleTemplate="Email validation error"
								messageTemplate="The value entered is not an valid email address."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="32232" format="m/d/yyyy">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="date"
								comparerType="between"
								from="DATEVALUE(\"1/1/1900\")"
								to="DATEVALUE(\"1/1/1998\")"
								titleTemplate="Birth Date validaiton error"
								messageTemplate="Birth Date should be between 1899 and 1998 year."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="(5) 555-3932">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="custom"
								from="AND(ISNUMBER(D5),LEN(D5)<14)"
								titleTemplate="Phone validation error"
								messageTemplate="The value entered is not an valid phone number. Please enter numeric value with less than 14 digits."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="<%= trueValue %>">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="list"
								from="ListValues!A1:B1"
								titleTemplate="Invalid value"
								messageTemplate="Valid values are 'true' and 'false'."/>
						</kendo:spreadsheet-sheet-row-cell>
					</kendo:spreadsheet-sheet-row-cells>
				</kendo:spreadsheet-sheet-row>

				<kendo:spreadsheet-sheet-row height='25'>
					<kendo:spreadsheet-sheet-row-cells>
						<kendo:spreadsheet-sheet-row-cell value="Thomas Hardy">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="custom"
								from="AND(LEN(A6)>3, LEN(A6)200)"
								titleTemplate="Full Name validation error"
								messageTemplate="The full name should be longer than 3 letters and shorter than 200."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="thomas.hardy@mail.com">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="custom"
								from="AND(NOT(ISERROR(FIND(\"@\", B6))), NOT(ISERROR(FIND(\".\", B6))), ISERROR(FIND(\" \", J1)), LEN(B6)>5)"
								titleTemplate="Email validation error"
								messageTemplate="The value entered is not an valid email address."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="21232" format="m/d/yyyy">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="date"
								comparerType="between"
								from="DATEVALUE(\"1/1/1900\")"
								to="DATEVALUE(\"1/1/1998\")"
								titleTemplate="Birth Date validaiton error"
								messageTemplate="Birth Date should be between 1899 and 1998 year."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="1715557788">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="custom"
								from="AND(ISNUMBER(D6),LEN(D6)<14)"
								titleTemplate="Phone validation error"
								messageTemplate="The value entered is not an valid phone number. Please enter numeric value with less than 14 digits."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="<%= trueValue %>">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="list"
								from="ListValues!A1:B1"
								titleTemplate="Invalid value"
								messageTemplate="Valid values are 'true' and 'false'."/>
						</kendo:spreadsheet-sheet-row-cell>
					</kendo:spreadsheet-sheet-row-cells>
				</kendo:spreadsheet-sheet-row>

				<kendo:spreadsheet-sheet-row height='25'>
					<kendo:spreadsheet-sheet-row-cells>
						<kendo:spreadsheet-sheet-row-cell value="Christina Toms">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="custom"
								from="AND(LEN(A7)>3, LEN(A7)200)"
								titleTemplate="Full Name validation error"
								messageTemplate="The full name should be longer than 3 letters and shorter than 200."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="christina.toms">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="custom"
								from="AND(NOT(ISERROR(FIND(\"@\", B7))), NOT(ISERROR(FIND(\".\", B7))), ISERROR(FIND(\" \", J1)), LEN(B7)>5)"
								titleTemplate="Email validation error"
								messageTemplate="The value entered is not an valid email address."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="30102" format="m/d/yyyy">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="date"
								comparerType="between"
								from="DATEVALUE(\"1/1/1900\")"
								to="DATEVALUE(\"1/1/1998\")"
								titleTemplate="Birth Date validaiton error"
								messageTemplate="Birth Date should be between 1899 and 1998 year."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="921123465">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="custom"
								from="AND(ISNUMBER(D7),LEN(D7)<14)"
								titleTemplate="Phone validation error"
								messageTemplate="The value entered is not an valid phone number. Please enter numeric value with less than 14 digits."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="<%= trueValue %>">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="list"
								from="ListValues!A1:B1"
								titleTemplate="Invalid value"
								messageTemplate="Valid values are 'true' and 'false'."/>
						</kendo:spreadsheet-sheet-row-cell>
					</kendo:spreadsheet-sheet-row-cells>
				</kendo:spreadsheet-sheet-row>

				<kendo:spreadsheet-sheet-row height='25'>
					<kendo:spreadsheet-sheet-row-cells>
						<kendo:spreadsheet-sheet-row-cell value="Hanna Moos">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="custom"
								from="AND(LEN(A8)>3, LEN(A8)200)"
								titleTemplate="Full Name validation error"
								messageTemplate="The full name should be longer than 3 letters and shorter than 200."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="hanna.moos@mail.com">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="custom"
								from="AND(NOT(ISERROR(FIND(\"@\", B8))), NOT(ISERROR(FIND(\".\", B8))), ISERROR(FIND(\" \", J1)), LEN(B8)>5)"
								titleTemplate="Email validation error"
								messageTemplate="The value entered is not an valid email address."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="0" format="m/d/yyyy">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="date"
								comparerType="between"
								from="DATEVALUE(\"1/1/1900\")"
								to="DATEVALUE(\"1/1/1998\")"
								titleTemplate="Birth Date validaiton error"
								messageTemplate="Birth Date should be between 1899 and 1998 year."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="62108460">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="custom"
								from="AND(ISNUMBER(D8),LEN(D8)<14)"
								titleTemplate="Phone validation error"
								messageTemplate="The value entered is not an valid phone number. Please enter numeric value with less than 14 digits."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="<%= trueValue %>">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="list"
								from="ListValues!A1:B1"
								titleTemplate="Invalid value"
								messageTemplate="Valid values are 'true' and 'false'."/>
						</kendo:spreadsheet-sheet-row-cell>
					</kendo:spreadsheet-sheet-row-cells>
				</kendo:spreadsheet-sheet-row>

				<kendo:spreadsheet-sheet-row height='25'>
					<kendo:spreadsheet-sheet-row-cells>
						<kendo:spreadsheet-sheet-row-cell value="">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="custom"
								from="AND(LEN(A9)>3, LEN(A9)200)"
								titleTemplate="Full Name validation error"
								messageTemplate="The full name should be longer than 3 letters and shorter than 200."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="custom"
								from="AND(NOT(ISERROR(FIND(\"@\", B9))), NOT(ISERROR(FIND(\".\", B9))), ISERROR(FIND(\" \", J1)), LEN(B9)>5)"
								titleTemplate="Email validation error"
								messageTemplate="The value entered is not an valid email address."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="" format="m/d/yyyy">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="date"
								comparerType="between"
								from="DATEVALUE(\"1/1/1900\")"
								to="DATEVALUE(\"1/1/1998\")"
								titleTemplate="Birth Date validaiton error"
								messageTemplate="Birth Date should be between 1899 and 1998 year."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="custom"
								from="AND(ISNUMBER(D9),LEN(D9)<14)"
								titleTemplate="Phone validation error"
								messageTemplate="The value entered is not an valid phone number. Please enter numeric value with less than 14 digits."/>
						</kendo:spreadsheet-sheet-row-cell>

						<kendo:spreadsheet-sheet-row-cell value="">
							<kendo:spreadsheet-sheet-row-cell-validation allowNulls="true" type="reject"
								dataType="list"
								from="ListValues!A1:B1"
								titleTemplate="Invalid value"
								messageTemplate="Valid values are 'true' and 'false'."/>
						</kendo:spreadsheet-sheet-row-cell>
					</kendo:spreadsheet-sheet-row-cells>
				</kendo:spreadsheet-sheet-row>

			</kendo:spreadsheet-sheet-rows>

			<kendo:spreadsheet-sheet-columns>
				<kendo:spreadsheet-sheet-column width="100"/>
				<kendo:spreadsheet-sheet-column width="215"/>
				<kendo:spreadsheet-sheet-column width="115"/>
				<kendo:spreadsheet-sheet-column width="115"/>
				<kendo:spreadsheet-sheet-column width="115"/>
				<kendo:spreadsheet-sheet-column width="155"/>
			</kendo:spreadsheet-sheet-columns>
		</kendo:spreadsheet-sheet>

		<kendo:spreadsheet-sheet name="ListValues">
			<kendo:spreadsheet-sheet-rows>
				<kendo:spreadsheet-sheet-row>
					<kendo:spreadsheet-sheet-row-cells>
						<kendo:spreadsheet-sheet-row-cell value="<%= trueValue %>" />
						<kendo:spreadsheet-sheet-row-cell value="<%= falseValue %>" />
					</kendo:spreadsheet-sheet-row-cells>
				</kendo:spreadsheet-sheet-row>
			</kendo:spreadsheet-sheet-rows>
		</kendo:spreadsheet-sheet>
	</kendo:spreadsheet-sheets>
</kendo:spreadsheet>

<!-- Load JSZIP library to enable Excel Export -->
<script src="${jszip}"></script>

<demo:footer />