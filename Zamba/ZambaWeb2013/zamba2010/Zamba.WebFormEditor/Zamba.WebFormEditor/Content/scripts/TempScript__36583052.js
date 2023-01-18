

        function SetAsocId(sender) {
            document.getElementById("hdnAsocId").name = sender.id;
            frmMain.submit();
        }

		$(document).ready(function () {
		    LoadLoadingRuleButtons();
			$("#zamba_rule_7925").click(ValidateBeforeSave);
			$("#zamba_rule_7925").attr('value','Siguiente');
		});
		
		function ValidateBeforeSave(evt) {
    ConvertToUpperForm();
    if (!$('#aspnetForm').valid()) {
        evt.preventDefault();
		setTimeout("parent.hideLoading();", 500);
    }
    else {
        document.getElementById("hdnRuleId").name = "zamba_rule_7925";
    }
}
    
