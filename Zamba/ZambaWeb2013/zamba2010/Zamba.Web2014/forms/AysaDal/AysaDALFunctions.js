function HideShowInsertRequestButton() {
    var site = $("#zamba_index_12427").val();
    var depot = $("#zamba_index_12428").val();
    var family = $("#zamba_index_12393").val();
    if (site != null && site != '' && depot != null && depot != '0' && family != null && family != '')
        $("#InsertAll").removeAttr("disabled");
    else
        $("#InsertAll").attr("disabled","disabled");
}

function HideShowGenericProdcutInsert() {
    var family = $("#zamba_index_12393").val();
    var isGeneric = $("#zamba_index_12486").val();

    if (family != null && family != '' && isGeneric == 1) {
        $("#divGenericProducts").show();
    }
    else {
        $("#divGenericProducts").hide();
    }
}

function SetNewProductInsert() {
    var genericProdcutDesc = $("#genericProdcutDesc").val();
    if (genericProdcutDesc != null && genericProdcutDesc != '') {
        SetRuleIdAndZvar(this, "zamba_rule_21889", "genericProdcutDesc=" + genericProdcutDesc);
    }
}

function ValidateProductDesc(sender) {
    if ($(sender).val() != null && $(sender).val().length > 0) {
        $("#InsertNewGenericProduct").removeAttr("disabled");
    }
    else {
        $("#InsertNewGenericProduct").attr("disabled", "disabled");
    }
}