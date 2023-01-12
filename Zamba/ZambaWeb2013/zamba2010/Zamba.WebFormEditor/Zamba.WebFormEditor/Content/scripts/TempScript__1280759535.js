
            function SetAsocId(sender){
                document.getElementById("hdnAsocId").name = sender.id;
                frmMain.submit();
            }
        

            function SetRuleId(sender){
                document.getElementById("hdnRuleId").name = sender.id;
                frmMain.submit();
            }
            
            function SetRuleId(sender){
                document.getElementById("hdnRuleId").name = sender.id;
                frmMain.submit();
            }
            
            window.onload = function(){
                try {
                    var hidden = document.getElementById("zamba_index_35");
                    var hidden2 = document.getElementById("zamba_index_34");
		            var hidden3 = document.getElementById("zamba_index_64");
	                var hidden4 = document.getElementById("texto");
		
                    var first = document.getElementById("zamba_rule_1557");
                    var second = document.getElementById("zamba_rule_1964");
                    
                    if (hidden.value > '0') {
                        first.style.visibility = 'visible';
                    }
                    else
{ 
                        first.style.visibility = 'hidden';
		    } 


                    if (hidden2.value > '0') {
                        second.style.visibility = 'visible';
                    }
                    else 
{                        second.style.visibility = 'hidden';
		    } 

                    
		            if (hidden3.value == '') {

                     hidden3.style.visibility = 'hidden';
		     hidden4.style.visibility = 'hidden';
                    }
                    else {

                    hidden3.style.visibility = 'visible';
		    hidden4.style.visibility = 'visible';
		    } 
                 } 
                catch (ex) {
                    alert(ex);
                }
            }
        
