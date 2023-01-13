function ZFUNCTION()
{
    if (document.getElementById("zamba_index_35").value == "0" || document.getElementById("zamba_index_35").value == "")
        document.getElementById("tablaNroPoliza").style.visibility = 'hidden';
            
    if (document.getElementById("zamba_index_4").value=="0" || document.getElementById("zamba_index_4").value =="")
    {
        document.getElementById("zamba_index_4").style.visibility = 'hidden';
        document.getElementById("tdCodProd").style.visibility = 'hidden';
    }

    if (document.getElementById("zamba_asoc_1015_index_1068").value != "0" && document.getElementById("zamba_asoc_1015_index_1068").value != "")
        document.getElementById("zamba_rule_5122").style.visibility = 'hidden';
        
    if (document.getElementById("zamba_asoc_1016_index_1068").value != "0" && document.getElementById("zamba_asoc_1016_index_1068").value != "")
        document.getElementById("zamba_rule_5142").style.visibility = 'hidden';
        
    if (document.getElementById("zamba_asoc_1018_index_1068").value != "0" && document.getElementById("zamba_asoc_1018_index_1068").value != "")
        document.getElementById("zamba_rule_5223").style.visibility = 'hidden';
}