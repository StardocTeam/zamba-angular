	
		       	function SetRuleId(sender)
       		 	{
		            document.getElementById("hdnRuleId").name = sender.id;               
            		frmMain.submit();
        		 }
			function SetAsocId(sender){
				document.getElementById("hdnAsocId").name = sender.id;
				frmMain.submit();
			}
			function makeCalendar(id)
			{
				$(function() 
				{
					$('#' + id).datepicker({
						changeMonth: true,
						changeYear: true,
						showOn: 'button', 
						buttonText: 'Abrir calendario',
						buttonImage: 'images/calendar.png', 
						buttonImageOnly: true,
						duration: ""
					});
				});
			}
			
			$(document).ready(function()
			{							
				makeCalendar('zamba_index_26201');
			   	makeCalendar('zamba_index_26175');	
				makeCalendar('zamba_ASOC_26031_index_26280');
				makeCalendar('zamba_ASOC_26031_index_26281');
				$("#zamba_index_26298").css("background-color","green");
				$("#zamba_index_26285").css("background-color","black");
	        });					
			
			/* Optional: Temporarily hide the "tabber" class so it does not "flash"
			   on the page as plain HTML. After tabber runs, the class is changed 
			   to "tabberlive" and it will appear. */

			//document.write('<style type="text/css">.tabber{display:none;}<\/style>');			
		

