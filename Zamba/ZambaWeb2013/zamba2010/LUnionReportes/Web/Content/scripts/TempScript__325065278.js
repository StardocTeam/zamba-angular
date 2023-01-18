
		    function SetRuleId(sender) {
		        document.getElementById("hdnRuleId").name = sender.id;
		        frmMain.submit();
		    }
		    function SetAsocId(sender) {
		        document.getElementById("hdnAsocId").name = sender.id;
		        frmMain.submit();
		    }

		    function zamba_save_onclick() {
		        document.getElementById("hdnRuleId").name = "zamba_save";
			ConvertToUpperForm();
		    }

		    $(document).ready(function () {
		        makeCalendar('zamba_index_24');
		        makeCalendar('zamba_index_25');

		        makerequired();

		        $("#zamba_save").click(ValidateBeforeSave);
		    });

		    //Crea las validaciones de los campos
		    function makerequired() {
		        $('#aspnetForm').validate(
				{
				    onsubmit: false,
				    ignore: ".disabled",
				    rules: {
				    zamba_index_1235: {
				            maxlength: 10
				        }
				    },
				    messages: {
				    zamba_index_1235: {
				            maxlength: "Se ha exedido el l�mite de 10 caracteres."
				        }
				    }
				});
		    }

		
