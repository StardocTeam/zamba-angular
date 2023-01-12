<%@page import="java.util.Date"%>
<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>
<c:url value="/resources/shared/js/jszip.min.js" var="jszip" />

<demo:header />
<%
	String[] mergedCells = { "A1:G1", "A2:F2" };	
%>
<div id="example">
<kendo:spreadsheet name="spreadsheet" style="width:100%">
	<kendo:spreadsheet-sheets>
		<kendo:spreadsheet-sheet name="Food Order" mergedCells="<%=mergedCells%>">
			<kendo:spreadsheet-sheet-filter ref="A3:G49">
			</kendo:spreadsheet-sheet-filter>
			<kendo:spreadsheet-sheet-rows>
				<kendo:spreadsheet-sheet-row  height='50' >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='ORDERS LOG'  fontSize='18'  textAlign='center'  background='rgb(144,164,174)'  color='white' />
				     </kendo:spreadsheet-sheet-row-cells>
		     	</kendo:spreadsheet-sheet-row>
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='REPORT'  textAlign='right'  background='rgb(176,190,197)'  color='white' />
				        <kendo:spreadsheet-sheet-row-cell
				         index='6'  background='rgb(176,190,197)'  color='white'  formula='TODAY()'  format='mmm-dd' />
				      </kendo:spreadsheet-sheet-row-cells>
				      </kendo:spreadsheet-sheet-row>
		    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='ID'  textAlign='center'  background='rgb(236,239,241)'  bold='true' color='black' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='DATE'  textAlign='center'  background='rgb(236,239,241)'  bold='true'  color='black' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='TIME'  textAlign='center'  background='rgb(236,239,241)'  bold='true'  color='black' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='CLIENT'  textAlign='center'  background='rgb(236,239,241)'  bold='true'  color='black' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='COMPANY'  textAlign='center'  background='rgb(236,239,241)'  bold='true'  color='black' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='SHIPPING'  textAlign='center'  background='rgb(236,239,241)'  bold='true'  color='black' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='DISCOUNT'  textAlign='center'  background='rgb(236,239,241)'  bold='true'  color='black' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10223'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Mon Jun 30 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Mon Jun 30 2014 09:30:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Terry Lawson'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Excella'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='1 day'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.02'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10247'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Tue Jul 01 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Tue Jul 01 2014 15:15:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Charles Miller'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Complete Tech'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='2 days'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.08'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10251'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Tue Jul 01 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Tue Jul 01 2014 14:13:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Jennie Walker'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Plan Smart'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='2 days'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.1'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10226'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Mon Jun 30 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Mon Jun 30 2014 17:43:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Samuel Green'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Excella'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='regular'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.08'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10227'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Mon Jun 30 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Mon Jun 30 2014 10:27:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='James Smith'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Complete Tech'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='2 days'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.01'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10228'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Mon Jun 30 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Mon Jun 30 2014 11:12:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Nora Allen'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Plan Smart'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='express'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10229'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 13:56:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Robyn Mason'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Excella'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='express'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.07'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10230'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 14:40:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Ralph Burke'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Plan Smart'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='regular'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.06'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10231'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 08:25:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Patty Prince'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Integra Design'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='1 day'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.02'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10232'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 10:09:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Natasha Green'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Excella'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='express'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10233'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 12:54:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='James Smith'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Complete Tech'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='express'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.03'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10259'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Wed Jul 02 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Wed Jul 02 2014 11:28:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Francis Stevens'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Plan Smart'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='2 days'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.08'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10235'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 18:22:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Roger Peters'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Integra Design'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='2 days'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.03'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10236'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 09:07:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Nora Allen'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Plan Smart'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='express'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.02'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10224'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Mon Jun 30 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Mon Jun 30 2014 12:14:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Scott Lewis'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Circuit Design'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='express'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.09'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10225'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Mon Jun 30 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Mon Jun 30 2014 14:58:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Scott Fox'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Zig Zag Coder'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='express'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.1'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10239'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 17:20:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Marian Rodriguez'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Zig Zag Coder'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='1 day'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.06'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10240'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 08:04:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Joe Lawrence'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Complete Tech'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='2 days'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.07'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10241'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 10:49:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Francis Stevens'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Plan Smart'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='regular'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10242'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 13:33:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Lynda Evans'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Complete Tech'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='regular'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.05'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10243'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 16:18:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Keith Clark'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Circuit Design'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='1 day'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10244'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Tue Jul 01 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Tue Jul 01 2014 19:02:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Kara Wood'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Excella'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='2 days'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10245'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Tue Jul 01 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Tue Jul 01 2014 09:46:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Juan Jacobs'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Plan Smart'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='regular'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.07'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10237'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 13:51:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Samuel Green'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Excella'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='express'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.15'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10265'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Wed Jul 02 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Wed Jul 02 2014 14:36:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Alison Thompson'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Circuit Design'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='express'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.1'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10248'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Tue Jul 01 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Tue Jul 01 2014 18:07:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Jerry Wright'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Integra Design'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='regular'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.07'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10234'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 15:38:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Nora Allen'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Plan Smart'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='regular'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.1'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10238'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Sun Jun 29 2014 14:36:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Mark Moore'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Webcom Services'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='regular'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.09'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10246'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Tue Jul 01 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Tue Jul 01 2014 12:31:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Patty Prince'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Integra Design'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='regular'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.08'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10252'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Tue Jul 01 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Tue Jul 01 2014 16:57:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='James Smith'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Complete Tech'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='express'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.02'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10253'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Tue Jul 01 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Tue Jul 01 2014 18:42:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Marian Rodriguez'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Zig Zag Coder'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='regular'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.01'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10254'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Tue Jul 01 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Tue Jul 01 2014 09:46:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Patty Prince'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Integra Design'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='express'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10255'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Tue Jul 01 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Tue Jul 01 2014 12:31:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Jack Sims'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Circuit Design'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='express'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10256'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Wed Jul 02 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Wed Jul 02 2014 15:15:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Hannah Watson'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Excella'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='regular'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.01'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10257'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Wed Jul 02 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Wed Jul 02 2014 18:07:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Gregory Morrison'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Webcom Services'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='2 days'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.04'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10258'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Wed Jul 02 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Wed Jul 02 2014 08:44:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Joe Lawrence'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Complete Tech'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='1 day'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10249'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Tue Jul 01 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Tue Jul 01 2014 08:44:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Edward Hall'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Zig Zag Coder'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='regular'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.08'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10260'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Wed Jul 02 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Wed Jul 02 2014 14:13:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Glenda White'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Webcom Services'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='regular'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.05'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10261'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Wed Jul 02 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Wed Jul 02 2014 16:57:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Lynda Evans'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Complete Tech'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='1 day'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.01'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10262'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Wed Jul 02 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Wed Jul 02 2014 08:48:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Edward Hall'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Zig Zag Coder'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='2 days'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.04'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10250'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Tue Jul 01 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Tue Jul 01 2014 11:28:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Jerry Wright'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Integra Design'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='regular'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.08'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10264'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Wed Jul 02 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Wed Jul 02 2014 13:51:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Jerry Wright'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Integra Design'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='2 days'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10263'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Wed Jul 02 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Wed Jul 02 2014 09:07:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Charles Miller'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Complete Tech'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='regular'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.1'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10266'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Wed Jul 02 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Wed Jul 02 2014 17:20:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Alison Ross'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Excella'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='express'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.02'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10267'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Wed Jul 02 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Wed Jul 02 2014 08:04:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Alexandra Kennedy'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Webcom Services'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='regular'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0.05'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			    
				<kendo:spreadsheet-sheet-row >
				    <kendo:spreadsheet-sheet-row-cells>
				        <kendo:spreadsheet-sheet-row-cell
				         value='10268'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Wed Jul 02 2014 00:00:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='<%=new Date("Wed Jul 02 2014 10:49:00 GMT+0300 (FLE Daylight Time)") %>'  textAlign='center'  format='hh:mm' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Agnes Hill'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='Integra Design'  textAlign='left' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='1 day'  textAlign='center' />
				        <kendo:spreadsheet-sheet-row-cell
				         value='0'  textAlign='center'  format='0%' />
				      </kendo:spreadsheet-sheet-row-cells>
			      </kendo:spreadsheet-sheet-row>
			</kendo:spreadsheet-sheet-rows>
			<kendo:spreadsheet-sheet-columns>
				<kendo:spreadsheet-sheet-column width="80"/>
				<kendo:spreadsheet-sheet-column width="100"/>
				<kendo:spreadsheet-sheet-column width="100"/>
				<kendo:spreadsheet-sheet-column width="150"/>
				<kendo:spreadsheet-sheet-column width="150"/>
				<kendo:spreadsheet-sheet-column width="130"/>
				<kendo:spreadsheet-sheet-column width="130"/>
			</kendo:spreadsheet-sheet-columns>
		</kendo:spreadsheet-sheet>
	</kendo:spreadsheet-sheets>
</kendo:spreadsheet>
</div>
<demo:footer />