
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
                    var first = document.getElementById("zamba_rule_1557");
                    var second = document.getElementById("zamba_rule_1621");
                    
                    hidden2.style.visibility = 'hidden';
                    hidden.style.visibility = 'hidden';
                    
                    if (hidden.value > '0') {
                        first.style.visibility = 'visible';
                    }
                    else 
                        first.style.visibility = 'hidden';
                    if (hidden2.value > '0') {
                        second.style.visibility = 'visible';
                    }
                    else 
                        second.style.visibility = 'hidden';
                } 
                catch (ex) {
                    alert(ex);
                }
            }
        
