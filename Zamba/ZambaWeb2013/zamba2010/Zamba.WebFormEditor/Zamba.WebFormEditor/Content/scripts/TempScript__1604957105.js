
			$(document).ready(function()
			{	
				$("#zamba_index_26260").css("background-color","purple");
				$("#zamba_index_26247").css("background-color","red");
	        });	

	
       	    function SetRuleId(sender)
       		{
				document.getElementById("hdnRuleId").name = sender.id;               
            	frmMain.submit();
			}

			function zamba_save_onclick()
			{
		
				document.getElementById("hdnRuleId").name = "zamba_rule_save";
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
				$("#zamba_index_26478").css("background-color","blue");
				$("#zamba_index_28596").css("background-color","green");
	        });					

			
			function loadTime(id)
			{
				document.getElementById(id).value = document.getElementById("zamba_index" + id).value.replace(":", "");
			}
			
			function formatTime(id)
			{
				var ctrl = $('#' + id);						
				var time = ctrl.val().replace(":", "");
				
				var h = time.substring(0, time.length - 2);					
				var m = time.substring(time.length - 2, time.length);
				
				if (h != "" || m != "")
					ctrl.val(h + ":" + m);
					
				if (h > 23 || m > 59)
					ctrl.addClass("error");
				else
					ctrl.removeClass("error");
			}	
			
			function dateToString(date)
			{
				var dia = date.split("/")[0];
				var mes = date.split("/")[1];
				var ano = date.split("/")[2];

				dia = format(dia);
				mes = format(mes);
				
				return ano + mes + dia;			
			}
			
			function format(txt)
			{
				if(txt.length == 1) txt = "0" + txt;
				
				return txt;
			}
				
			
			function validaDatos()
			{		
				return true;		
			}

			//document.write('<style type="text/css">.tabber{display:none;}<\/style>');			

		




