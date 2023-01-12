
        function SetRuleId(sender)
        {
            document.getElementById("hdnRuleId").name = sender.id;               
            frmMain.submit();
        }
       
        function SetAsocId(sender){
                document.getElementById("hdnAsocId").name = sender.id;
                frmMain.submit();
            }
            
        function ZFUNCTION()
        {
            var Historial = document.getElementById("zamba_Index_1148").value
            while (Historial.indexOf(" ", 0) != -1) 
            {
                Historial = Historial.replace(" ", "\n");
            }
           document.getElementById("zamba_Index_1148").value = Historial;
        }
        function prev(sender, path)
		{			
			var iframe = document.getElementById("zamba_innerdoctype_variable");
            path = "file:///" + path;
            path = path.replace(".msg", ".html");

			if (iframe)
			{
				iframe.setAttribute('src', path);
			}
			else
			{
				alert ("Se produjo un error al cargar la previsualizacion");
			}			
		}            
    
